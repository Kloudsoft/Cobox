using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace HouseOfSynergy.PowerTools.Library.Security.Cryptography
{
	public sealed class Rijndael
	{
		public static string Encrypt (string text, byte [] key, byte [] initializationVector)
		{
			byte [] data = null;
			string encrypted = "";

			if (string.IsNullOrWhiteSpace(text)) { throw new ArgumentNullException("plainText"); }
			if ((key == null) || (key.Length <= 0)) { throw new ArgumentNullException("Key"); }
			if ((initializationVector == null) || (initializationVector.Length <= 0)) { throw new ArgumentNullException("Key"); }

			using (var algorithm = new RijndaelManaged())
			{
				algorithm.Key = key;
				algorithm.IV = initializationVector;

				using (var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
				{
					using (var memoryStream = new MemoryStream())
					{
						using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
						{
							using (var streamWriter = new StreamWriter(cryptoStream))
							{
								streamWriter.Write(text);
							}

							data = memoryStream.ToArray();
							encrypted = Convert.ToBase64String(data);
						}
					}
				}
			}

			return (encrypted);
		}

		public static string Decrypt (string text, byte [] key, byte [] initializationVector)
		{
			byte [] data = null;
			string decrypted = "";

			if ((text == null) || (text.Length <= 0)) { throw new ArgumentNullException("plainText"); }
			if ((key == null) || (key.Length <= 0)) { throw new ArgumentNullException("Key"); }
			if ((initializationVector == null) || (initializationVector.Length <= 0)) { throw new ArgumentNullException("Key"); }

			using (RijndaelManaged rijAlg = new RijndaelManaged())
			{
				rijAlg.Key = key;
				rijAlg.IV = initializationVector;
				data = Convert.FromBase64String(text);
				var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

				using (var msDecrypt = new MemoryStream(data))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new StreamReader(csDecrypt))
						{
							decrypted = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return (decrypted);
		}
	}
}