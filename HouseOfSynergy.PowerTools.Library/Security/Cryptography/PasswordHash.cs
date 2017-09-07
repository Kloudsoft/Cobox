using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.Security.Cryptography
{
	/// <summary>
	/// Salted password hashing with PBKDF2-SHA1.
	/// </summary>
	public static class PasswordHash
	{
		/// <summary>
		/// The size of the salt in bytes.
		/// </summary>
		public const int SizeSalt = 24;
		/// <summary>
		/// The size of the hash in bytes.
		/// </summary>
		public const int SizeHash = 24;
		/// <summary>
		/// Number of iterations.
		/// </summary>
		public const int IterationCountPBKDF2 = 1000;

		private const int IndexSalt = 1;
		private const int IndexPBKDF2 = 2;
		private const string Delimiter = ":";
		private const int IndexIterations = 0;

		/// <summary>
		/// Creates a salted PBKDF2 hash of the password.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <returns>The hash of the password.</returns>
		public static string CreateHash (string password)
		{
			using (var csprng = new RNGCryptoServiceProvider())
			{
				var salt = csprng.GetBytes(PasswordHash.SizeSalt);
				var hash = PasswordHash.PBKDF2(password, salt, PasswordHash.IterationCountPBKDF2, PasswordHash.SizeHash);

				return (string.Join(PasswordHash.Delimiter, new string [] { PasswordHash.IterationCountPBKDF2.ToString(), Convert.ToBase64String(salt), Convert.ToBase64String(hash), }));
			}
		}

        public static  byte[] GetSaltByHash(string hash)
        {
            var split = hash.Split(PasswordHash.Delimiter);
            var iterations = int.Parse(split[PasswordHash.IndexIterations]);
            var salt = Convert.FromBase64String(split[PasswordHash.IndexSalt]);
            return salt;
        }

		/// <summary>
		/// Validates a password given a hash of the correct one.
		/// </summary>
		/// <param name="password">The password to check.</param>
		/// <param name="correctHash">A hash of the correct password.</param>
		/// <returns>True if the password is correct. False otherwise.</returns>
		public static bool ValidatePassword (string password, string correctHash)
		{
			var split = correctHash.Split(PasswordHash.Delimiter);
			var iterations = int.Parse(split [PasswordHash.IndexIterations]);
			var salt = Convert.FromBase64String(split [PasswordHash.IndexSalt]);
			var hash = Convert.FromBase64String(split [PasswordHash.IndexPBKDF2]);

			var testHash = PasswordHash.PBKDF2(password, salt, iterations, hash.Length);

			return (PasswordHash.SlowEquals(hash, testHash));
		}

		/// <summary>
		/// Compares two byte arrays in length-constant time. This comparison
		/// method is used so that password hashes cannot be extracted from
		/// on-line systems using a timing attack and then attacked off-line.
		/// </summary>
		/// <param name="a">The first byte array.</param>
		/// <param name="b">The second byte array.</param>
		/// <returns>True if both byte arrays are equal. False otherwise.</returns>
		private static bool SlowEquals (byte [] a, byte [] b)
		{
			uint diff = (((uint) a.Length) ^ ((uint) b.Length));

			for (int i = 0; i < a.Length && i < b.Length; i++)
			{
				diff |= (uint) (a [i] ^ b [i]);
			}

			return (diff == 0);
		}

		/// <summary>
		/// Computes the PBKDF2-SHA1 hash of a password.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <param name="salt">The salt.</param>
		/// <param name="iterations">The PBKDF2 iteration count.</param>
		/// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
		/// <returns>A hash of the password.</returns>
		private static byte [] PBKDF2 (string password, byte [] salt, int iterations, int outputBytes)
		{
			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
			{
				return (pbkdf2.GetBytes(outputBytes));
			}
		}
	}
}