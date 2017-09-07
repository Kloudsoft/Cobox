using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static string KeyToString (this RSAParameters key)
		{
			return (Rsa.KeyToString(key));
		}

		public static RSAParameters KeyFromString (this RSAParameters key, string text)
		{
			return (Rsa.KeyFromString(text));
		}

		public static byte [] GetBytes (this RNGCryptoServiceProvider generator, int count)
		{
			var buffer = new byte [count];

			generator.GetBytes(buffer);

			return (buffer);
		}

		public static byte [] GetNonZeroBytes (this RNGCryptoServiceProvider generator, int count)
		{
			var buffer = new byte [count];

			generator.GetNonZeroBytes(buffer);

			return (buffer);
		}
	}
}