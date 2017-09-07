using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace HouseOfSynergy.PowerTools.Library.Security.Cryptography
{
	public struct RsaKeyPair
	{
		private RSAParameters _KeyPublic;
		private RSAParameters _KeyPrivate;

		public RsaKeyPair (RSAParameters keyPublic)
		{
			this._KeyPublic = keyPublic;
			this._KeyPrivate = new RSAParameters();
		}

		public RsaKeyPair (RSAParameters keyPublic, RSAParameters keyPrivate)
		{
			this._KeyPublic = keyPublic;
			this._KeyPrivate = keyPrivate;
		}

		public RSAParameters KeyPublic { get { return (this._KeyPublic); } }
		public RSAParameters KeyPrivate { get { return (this._KeyPrivate); } }
	}

	public static class Rsa
	{
		public static RsaKeyPair GenerateKeyPair (int keySizeInBits)
		{
			using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(keySizeInBits))
			{
				return (new RsaKeyPair(provider.ExportParameters(false), provider.ExportParameters(true)));
			}
		}

		/// <summary>
		/// Generates a pair of public/private RSA keys.
		/// </summary>
		/// <param name="keySizeInBits">The size of the key to use in bits.</param>
		/// <param name="exception">The first encountered exception, if any.</param>
		/// <returns>Returns true if the operation succeeded, false otherwise.</returns>
		public static bool GenerateKeyPair (int keySizeInBits, out RsaKeyPair keyPair, out Exception exception)
		{
			bool result = false;

			exception = null;
			keyPair = new RsaKeyPair();

			try
			{
				using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(keySizeInBits))
				{
					keyPair = new RsaKeyPair(provider.ExportParameters(false), provider.ExportParameters(true));
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		/// <summary>
		/// Generates a pair of public/private RSA keys.
		/// </summary>
		/// <param name="keySizeInBits">The size of the key to use in bits.</param>
		/// <param name="exception">The first encountered exception, if any.</param>
		/// <returns>Returns true if the operation succeeded, false otherwise.</returns>
		public static bool GenerateKeyPair (int keySizeInBits, out RSAParameters keyPublic, out RSAParameters keyPrivate, out Exception exception)
		{
			bool result = false;

			exception = null;

			try
			{
				using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(keySizeInBits))
				{
					keyPublic = provider.ExportParameters(false);
					keyPrivate = provider.ExportParameters(true);
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
				keyPublic = new System.Security.Cryptography.RSAParameters();
				keyPrivate = new System.Security.Cryptography.RSAParameters();
			}

			return (result);
		}

		/// <summary>
		/// Encrypts data with the System.Security.Cryptography.RSA algorithm.
		/// </summary>
		/// <param name="data">The data to be encrypted.</param>
		/// <param name="encoding">The encoding to use for the data to be encrypted.</param>
		/// <param name="keyPublic">The public key to use for encryption.</param>
		/// <param name="output">The encrypted data.</param>
		/// <param name="exception">The first exception encountered, if any.</param>
		/// <param name="useOptimalAsymmetricEncryptionPadding">Set true to perform direct System.Security.Cryptography.RSA encryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.</param>
		/// <returns>Returns true if the operation succeeded, false otherwise.</returns>
		public static bool Encrypt (string data, Encoding encoding, RSAParameters keyPublic, out byte [] output, out Exception exception, bool useOptimalAsymmetricEncryptionPadding = true)
		{
			bool result = false;
			byte [] bytes = null;

			output = null;
			exception = null;

			if (encoding == null)
			{
				exception = new ArgumentNullException("encoding");
			}
			else
			{
				if (data == null)
				{
					exception = new ArgumentNullException("data");
				}
				else if (data.Length == 0)
				{
					exception = new ArgumentException("The argument data is an empty string.", "data");
				}
				else
				{
					result = Rsa.Encrypt(encoding.GetBytes(data), keyPublic, out bytes, out exception, useOptimalAsymmetricEncryptionPadding);

					if (result)
					{
						try
						{
							output = bytes;
						}
						catch (Exception e)
						{
							output = null;
							exception = e;
						}
					}
				}
			}

			return (result);
		}

		/// <summary>
		/// Encrypts data with the System.Security.Cryptography.RSA algorithm.
		/// </summary>
		/// <param name="data">The data to be encrypted.</param>
		/// <param name="keyPublic">The public key to use for encryption.</param>
		/// <param name="output">The encrypted data.</param>
		/// <param name="exception">The first exception encountered, if any.</param>
		/// <param name="useOptimalAsymmetricEncryptionPadding">Set true to perform direct System.Security.Cryptography.RSA encryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.</param>
		/// <returns>Returns true if the operation succeeded, false otherwise.</returns>
		public static bool Encrypt (byte [] data, RSAParameters keyPublic, out byte [] output, out Exception exception, bool useOptimalAsymmetricEncryptionPadding = true)
		{
			bool result = false;

			output = null;
			exception = null;

			if (data == null)
			{
				exception = new ArgumentNullException("data");
			}
			else if (data.Length == 0)
			{
				exception = new ArgumentException("The argument data is an empty string.", "data");
			}
			else
			{
				try
				{
					using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
					{
						provider.ImportParameters(keyPublic);

						output = provider.Encrypt(data, useOptimalAsymmetricEncryptionPadding);

						result = true;
					}
				}
				catch (Exception e)
				{
					output = null;
					exception = e;
				}
			}

			return (result);
		}

		/// <summary>
		/// Decrypts data with the System.Security.Cryptography.RSA algorithm.
		/// </summary>
		/// <param name="data">The data to be decrypted.</param>
		/// <param name="encoding">The encoding to use for the data to be decrypted.</param>
		/// <param name="keyPublic">The private key to use for encryption.</param>
		/// <param name="output">The decrypted data, which is the original plain text before encryption.</param>
		/// <param name="exception">The first exception encountered, if any.</param>
		/// <param name="useOptimalAsymmetricEncryptionPadding">Set true to perform direct System.Security.Cryptography.RSA decryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.</param>
		/// <returns>Returns true if the operation succeeded, false otherwise.</returns>
		public static bool Decrypt (byte [] data, Encoding encoding, RSAParameters keyPrivate, out string output, out Exception exception, bool useOptimalAsymmetricEncryptionPadding = true)
		{
			bool result = false;
			byte [] bytes = null;

			output = null;
			exception = null;

			if (encoding == null)
			{
				exception = new ArgumentNullException("encoding");
			}
			else
			{
				if (data == null)
				{
					exception = new ArgumentNullException("data");
				}
				else if (data.Length == 0)
				{
					exception = new ArgumentException("The argument data is an empty string.", "data");
				}
				else
				{
					result = Rsa.Decrypt(data, keyPrivate, out bytes, out exception, useOptimalAsymmetricEncryptionPadding);

					if (result)
					{
						try
						{
							output = encoding.GetString(bytes);
						}
						catch (Exception e)
						{
							output = null;
							exception = e;
						}
					}
				}
			}

			return (result);
		}

		/// <summary>
		/// Decrypts data with the System.Security.Cryptography.RSA algorithm.
		/// </summary>
		/// <param name="data">The data to be decrypted.</param>
		/// <param name="keyPublic">The private key to use for encryption.</param>
		/// <param name="output">The decrypted data, which is the original plain text before encryption.</param>
		/// <param name="exception">The first exception encountered, if any.</param>
		/// <param name="useOptimalAsymmetricEncryptionPadding">Set true to perform direct System.Security.Cryptography.RSA decryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.</param>
		/// <returns>Returns true if the operation succeeded, false otherwise.</returns>
		public static bool Decrypt (byte [] data, RSAParameters keyPrivate, out byte [] output, out Exception exception, bool useOptimalAsymmetricEncryptionPadding = true)
		{
			bool result = false;

			output = null;
			exception = null;

			if (data == null)
			{
				exception = new ArgumentNullException("data");
			}
			else if (data.Length == 0)
			{
				exception = new ArgumentException("The argument data is an empty string.", "data");
			}
			else
			{
				try
				{
					using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
					{
						provider.ImportParameters(keyPrivate);

						output = provider.Decrypt(data, useOptimalAsymmetricEncryptionPadding);

						result = true;
					}
				}
				catch (Exception e)
				{
					output = null;
					exception = e;
				}
			}

			return (result);
		}

		public static string KeyToString (RSAParameters key)
		{
			string text = "";
			Exception exception = null;

			Rsa.KeyToString(key, out text, out exception);

			return (text);
		}

		public static bool KeyToString (RSAParameters key, out string text, out Exception exception)
		{
			bool result = false;

			text = "";
			exception = null;

			try
			{
				using (var writer = new System.IO.StringWriter())
				{
					new XmlSerializer(typeof(RSAParameters)).Serialize(writer, key);

					text = writer.ToString();
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static RSAParameters KeyFromString (string text)
		{
			RSAParameters key;
			Exception exception = null;

			Rsa.KeyFromString(text, out key, out exception);

			return (key);
		}

		public static bool KeyFromString (string text, out RSAParameters key, out Exception exception)
		{
			bool result = false;

			exception = null;
			key = default(RSAParameters);

			try
			{
				using (var reader = new StringReader(text))
				{
					key = (RSAParameters) new XmlSerializer(typeof(RSAParameters)).Deserialize(reader);
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
				key = default(RSAParameters);
			}

			return (result);
		}

		public static string KeyToBase64String (RSAParameters key)
		{
			string text = "";
			Exception exception = null;

			Rsa.KeyToBase64String(key, out text, out exception);

			return (text);
		}

		public static bool KeyToBase64String (RSAParameters key, out string text, out Exception exception)
		{
			bool result = false;

			text = "";
			exception = null;

			try
			{
				using (var writer = new System.IO.StringWriter())
				{
					new XmlSerializer(typeof(RSAParameters)).Serialize(writer, key);

					text = Convert.ToBase64String(Encoding.ASCII.GetBytes(writer.ToString()));
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static RSAParameters KeyFromBase64String (string text)
		{
			RSAParameters key;
			Exception exception = null;

			Rsa.KeyFromBase64String(text, out key, out exception);

			return (key);
		}

		public static bool KeyFromBase64String (string text, out RSAParameters key, out Exception exception)
		{
			bool result = false;

			exception = null;
			key = default(RSAParameters);

			try
			{
				using (var reader = new StringReader(Encoding.ASCII.GetString(Convert.FromBase64String(text))))
				{
					key = (RSAParameters) new XmlSerializer(typeof(RSAParameters)).Deserialize(reader);
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
				key = default(RSAParameters);
			}

			return (result);
		}

		public static bool KeyToFile (RSAParameters key, string filename, out Exception exception)
		{
			bool result = false;
			System.IO.StringWriter writer = null;
			System.Xml.Serialization.XmlSerializer serializer = null;

			exception = null;

			try
			{
				writer = new System.IO.StringWriter();
				serializer = new XmlSerializer(typeof(RSAParameters));

				serializer.Serialize(writer, key);
				File.WriteAllText(filename, writer.ToString());

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool KeyFromFile (string filename, out RSAParameters key, out Exception exception)
		{
			bool result = false;
			StringReader reader = null;
			XmlSerializer serializer = null;

			exception = null;
			key = default(RSAParameters);

			try
			{
				reader = new StringReader(File.ReadAllText(filename));
				serializer = new XmlSerializer(typeof(RSAParameters));

				key = (RSAParameters) serializer.Deserialize(reader);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
				key = default(RSAParameters);
			}

			return (result);
		}
	}
}