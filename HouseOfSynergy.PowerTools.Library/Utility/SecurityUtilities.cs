using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class SecurityUtilities
	{
		public static bool IsRunningAsAdmin ()
		{
			try
			{
				var windowsIdentityCurrent = System.Security.Principal.WindowsIdentity.GetCurrent();

				foreach (System.Security.Principal.IdentityReference identityReference in windowsIdentityCurrent.Groups)
				{
					if (identityReference.IsValidTargetType(typeof(System.Security.Principal.SecurityIdentifier)))
					{
						var securityIdentifier = (System.Security.Principal.SecurityIdentifier) identityReference.Translate(typeof(System.Security.Principal.SecurityIdentifier));

						if ((securityIdentifier.IsWellKnown(System.Security.Principal.WellKnownSidType.AccountAdministratorSid)) || (securityIdentifier.IsWellKnown(System.Security.Principal.WellKnownSidType.BuiltinAdministratorsSid)))
						{
							return (true);
						}
					}
				}
			}
			catch
			{
			}

			return (false);
		}

		public static bool Encrypt (string source, Encoding encoding, SymmetricAlgorithm algorithm, out string output, out Exception exception)
		{
			var result = false;

			output = "";
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (encoding == null) { throw (new ArgumentNullException("encoding")); }
			if (algorithm == null) { throw (new ArgumentNullException("algorithm")); }

			try
			{
				var bytesEncrypted = new byte [] { };
				var bytesSource = encoding.GetBytes(source);

				if (SecurityUtilities.Encrypt(bytesSource, algorithm, out bytesEncrypted, out exception))
				{
					output = encoding.GetString(bytesEncrypted);

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool Encrypt (string source, Encoding encoding, SymmetricAlgorithm algorithm, out byte [] output, out Exception exception)
		{
			var result = false;

			output = null;
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (encoding == null) { throw (new ArgumentNullException("encoding")); }
			if (algorithm == null) { throw (new ArgumentNullException("algorithm")); }

			try
			{
				var bytesEncrypted = new byte [] { };
				var bytesSource = encoding.GetBytes(source);

				if (SecurityUtilities.Encrypt(bytesSource, algorithm, out bytesEncrypted, out exception))
				{
					output = bytesEncrypted;

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool Encrypt (byte [] source, SymmetricAlgorithm algorithm, out byte [] output, out Exception exception)
		{
			var result = false;

			output = null;
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (algorithm == null) { throw (new ArgumentNullException("algorithm")); }

			try
			{
				using (var memoryStream = new MemoryStream())
				{
					using (var transform = algorithm.CreateEncryptor())
					{
						using (var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(source, 0, source.Length);
						}
					}

					output = memoryStream.ToArray();
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool Decrypt (string source, Encoding encoding, SymmetricAlgorithm algorithm, out string output, out Exception exception)
		{
			var result = false;

			output = "";
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (encoding == null) { throw (new ArgumentNullException("encoding")); }
			if (algorithm == null) { throw (new ArgumentNullException("algorithm")); }

			try
			{
				var bytesDecrypted = new byte [] { };
				var bytesSource = encoding.GetBytes(source);

				if (SecurityUtilities.Decrypt(bytesSource, algorithm, out bytesDecrypted, out exception))
				{
					output = encoding.GetString(bytesDecrypted);

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool Decrypt (byte [] source, Encoding encoding, SymmetricAlgorithm algorithm, out string output, out Exception exception)
		{
			var result = false;

			output = "";
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (encoding == null) { throw (new ArgumentNullException("encoding")); }
			if (algorithm == null) { throw (new ArgumentNullException("algorithm")); }

			try
			{
				var bytesSource = source;
				var bytesDecrypted = new byte [] { };

				if (SecurityUtilities.Decrypt(bytesSource, algorithm, out bytesDecrypted, out exception))
				{
					output = encoding.GetString(bytesDecrypted);

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool Decrypt (byte [] source, SymmetricAlgorithm algorithm, out byte [] output, out Exception exception)
		{
			var result = false;

			output = null;
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (algorithm == null) { throw (new ArgumentNullException("algorithm")); }

			try
			{
				using (var memoryStream = new MemoryStream(source))
				{
					using (var transform = algorithm.CreateDecryptor())
					{
						using (var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
						{
							cryptoStream.Read(source, 0, source.Length);
						}
					}

					output = memoryStream.ToArray();
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static string GeneratePassword ()
		{
			return (SecurityUtilities.GeneratePassword(8));
		}

		public static string GeneratePassword (int length)
		{
			return (SecurityUtilities.GeneratePassword(8, true, true, true, true));
		}

		public static string GeneratePassword
		(
			int length,
			bool includeLowerCaseLetters,
			bool includeUpperCaseLetters,
			bool includeDigits,
			bool includeSpecialCharacters
		)
		{
			int count = 0;
			char temp = '\0';
			bool result = false;
			byte [] data = null;
			byte [] buffer1 = null;
			byte [] buffer2 = null;
			string digitCharacters = "";
			string specialCharacters = "";
			string lowerCaseCharacters = "";
			string upperCaseCharacters = "";
			StringBuilder source = null;
			StringBuilder password = null;
			RNGCryptoServiceProvider random = null;

			if (length < 1)
				throw (new ArgumentException("Password length must be at least 1.", "length"));
			if (length > 100)
				throw (new ArgumentException("Password length must be at most 100.", "length"));
			if (!(includeLowerCaseLetters | includeUpperCaseLetters | includeDigits | includeSpecialCharacters))
				throw (new ArgumentException("At least one of the arguments [includeLowerCaseLetters, includeUpperCaseLetters, includeDigits, includeSpecialCharacters] must be true.", "includeLowerCaseLetters, includeUpperCaseLetters, includeDigits, includeSpecialCharacters"));

			count = (includeLowerCaseLetters ? 1 : 0) + (includeDigits ? 1 : 0) + (includeUpperCaseLetters ? 1 : 0) + (includeSpecialCharacters ? 1 : 0);
			if (length < count)
				throw (new ArgumentException("Password length must be at least " + count + ".", "length"));

			digitCharacters = @"0123456789";
			lowerCaseCharacters = @"abcdefghijklmnopqrstuvwxyz";
			upperCaseCharacters = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			specialCharacters = @"`~!@#$%^&*()-_=+[{]};:',<.>/?";
			source = new StringBuilder();
			random = new RNGCryptoServiceProvider();

			// Build source (allowed characters).
			while (source.Length < 256)
			{
				source.Append
				(
					(includeLowerCaseLetters ? lowerCaseCharacters : "")
					+ (includeDigits ? digitCharacters : "")
					+ (includeUpperCaseLetters ? upperCaseCharacters : "")
					+ (includeSpecialCharacters ? specialCharacters : "")
				);
			}
			source = new StringBuilder(source.ToString().Substring(0, 256));

			// Shuffle source (allowed characters).
			buffer1 = new byte [source.Length * length];
			buffer2 = new byte [source.Length * length];
			random.GetBytes(buffer1);
			random.GetBytes(buffer2);
			for (int i = 0; i < buffer1.Length; i++)
			{
				temp = source [buffer1 [i]];
				source [buffer1 [i]] = source [buffer2 [i]];
				source [buffer2 [i]] = temp;
			}
			buffer1 = null;
			buffer2 = null;

			// Build random source index.
			data = new byte [length];
			password = new StringBuilder(new string(' ', length));

			// Attempt to generate legal password.
			result = false;
			while (!result)
			{
				random.GetBytes(data);
				for (int i = 0; i < length; i++)
				{
					password [i] = source [data [i]];
				}

				result
					= ((includeLowerCaseLetters & password.ToString().Any(c => char.IsLower(c))) || (!includeLowerCaseLetters))
					& ((includeUpperCaseLetters & password.ToString().Any(c => char.IsUpper(c))) || (!includeUpperCaseLetters))
					& ((includeDigits & password.ToString().Any(c => char.IsDigit(c))) || (!includeDigits))
					& ((includeSpecialCharacters & password.ToString().Any(c => specialCharacters.Contains(c))) || (!includeSpecialCharacters))
					;
			}

			return (password.ToString());
		}
	}
}