using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Security.Cryptography
{
	public static class Sha
	{
		public enum EnumShaKind { Sha1, Sha256, Sha384, Sha512 }

		public static string GenerateHash (string data, Encoding encoding, EnumShaKind kind)
		{
			var hash = "";
			bool result = false;
			Exception exception = null;

			result = Sha.GenerateHash(data, encoding, kind, out hash, out exception);

			if (!result) { throw (exception); }

			return (hash);
		}

		public static bool GenerateHash (string data, Encoding encoding, EnumShaKind kind, out string hash)
		{
			bool result = false;
			Exception exception = null;

			result = Sha.GenerateHash(data, encoding, kind, out hash, out exception);

			if (!result) { throw (exception); }

			return (result);
		}

		public static bool GenerateHash (string data, Encoding encoding, EnumShaKind kind, out string hash, out Exception exception)
		{
			bool result = false;
			byte [] bytes = null;

			hash = null;
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
				if (encoding == null)
				{
					exception = new ArgumentNullException("encoding");
				}
				else
				{
					try
					{
						result = Sha.GenerateHash(encoding.GetBytes(data), kind, out bytes, out exception);

						if (result)
						{
							hash = Convert.ToBase64String(bytes);
						}
					}
					catch (Exception e)
					{
						hash = null;
						exception = e;
						result = false;
					}
				}
			}

			return (result);
		}

		public static bool GenerateHash (byte [] data, EnumShaKind kind, out string hash, out Exception exception)
		{
			bool result = false;
			byte [] bytes = null;

			hash = "";

			result = Sha.GenerateHash(data, kind, out bytes, out exception);

			if (result)
			{
				hash = Convert.ToBase64String(bytes);
			}

			return (result);
		}

		public static bool GenerateHash (string data, Encoding encoding, EnumShaKind kind, out byte [] hash, out Exception exception)
		{
			bool result = false;
			string value = null;
			byte [] bytes = null;

			hash = null;

			bytes = encoding.GetBytes(data);

			result = Sha.GenerateHash(bytes, kind, out value, out exception);

			return (result);
		}

		public static bool GenerateHash (byte [] data, EnumShaKind kind, out byte [] hash)
		{
			bool result = false;
			Exception exception = null;

			result = Sha.GenerateHash(data, kind, out hash, out exception);

			if (!result)
			{
				throw (exception);
			}

			return (result);
		}

		public static bool GenerateHash (byte [] data, EnumShaKind kind, out byte [] hash, out Exception exception)
		{
			bool result = false;
			byte [] buffer = null;
			HashAlgorithm algorithm = null;

			hash = null;
			exception = null;

			if (data == null)
			{
				exception = new ArgumentNullException("data");
			}
			else if (data.Length == 0)
			{
				exception = new ArgumentException("The argument data is an empty array.", "data");
			}

			try
			{
				switch (kind)
				{
					case EnumShaKind.Sha1: { algorithm = new SHA1Managed(); break; }
					case EnumShaKind.Sha256: { algorithm = new SHA256Managed(); break; }
					case EnumShaKind.Sha384: { algorithm = new SHA384Managed(); break; }
					case EnumShaKind.Sha512: { algorithm = new SHA512Managed(); break; }
					default: { exception = new NotImplementedException("The argument [kind] has a value that is not implemented."); break; }
				}

				if (algorithm != null)
				{
					buffer = algorithm.ComputeHash(data);

					hash = buffer;

					result = true;
				}
			}
			catch (Exception e)
			{
				hash = null;
				exception = e;
				result = false;
			}
			finally
			{
				if (algorithm != null)
				{
					try { algorithm.Dispose(); }
					finally { algorithm = null; }
				}
			}

			return (result);
		}

		public static bool GenerateHash (System.IO.Stream stream, EnumShaKind kind, out byte [] hash, out Exception exception)
		{
			bool result = false;
			byte [] buffer = null;
			HashAlgorithm algorithm = null;

			hash = null;
			exception = null;

			if (stream == null)
			{
				exception = new ArgumentNullException("stream");
			}
			else if (stream.Length == 0)
			{
				exception = new ArgumentException("The argument stream is empty.", "stream");
			}

			try
			{
				switch (kind)
				{
					case EnumShaKind.Sha1: { algorithm = new SHA1Managed(); break; }
					case EnumShaKind.Sha256: { algorithm = new SHA256Managed(); break; }
					case EnumShaKind.Sha384: { algorithm = new SHA384Managed(); break; }
					case EnumShaKind.Sha512: { algorithm = new SHA512Managed(); break; }
					default: { exception = new NotImplementedException("The argument [kind] has a value that is not implemented."); break; }
				}

				if (algorithm != null)
				{
					buffer = algorithm.ComputeHash(stream);

					hash = buffer;

					result = true;
				}
			}
			catch (Exception e)
			{
				hash = null;
				exception = e;
				result = false;
			}
			finally
			{
				if (algorithm != null)
				{
					try { algorithm.Dispose(); }
					finally { algorithm = null; }
				}
			}

			return (result);
		}

		public static bool GenerateHash (Stream stream, EnumShaKind kind, out string hash, out Exception exception)
		{
			bool result = false;
			byte [] buffer = null;
			HashAlgorithm algorithm = null;

			hash = null;
			exception = null;

			if (stream == null)
			{
				exception = new ArgumentNullException("stream");
			}
			else if (stream.Length == 0)
			{
				exception = new ArgumentException("The argument stream is empty.", "stream");
			}

			try
			{
				switch (kind)
				{
					case EnumShaKind.Sha1: { algorithm = new SHA1Managed(); break; }
					case EnumShaKind.Sha256: { algorithm = new SHA256Managed(); break; }
					case EnumShaKind.Sha384: { algorithm = new SHA384Managed(); break; }
					case EnumShaKind.Sha512: { algorithm = new SHA512Managed(); break; }
					default: { exception = new NotImplementedException("The argument [kind] has a value that is not implemented."); break; }
				}

				if (algorithm != null)
				{
					buffer = algorithm.ComputeHash(stream);

					hash = ASCIIEncoding.ASCII.GetString(buffer);

					result = true;
				}
			}
			catch (Exception e)
			{
				hash = null;
				exception = e;
				result = false;
			}
			finally
			{
				if (algorithm != null)
				{
					try { algorithm.Dispose(); }
					finally { algorithm = null; }
				}
			}

			return (result);
		}
	}
}