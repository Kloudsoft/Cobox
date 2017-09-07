using System;
namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static string ToSentenceCase (this string text)
		{
			var value = text;

			if (value.Length == 1)
			{
				value = value [0].ToString().ToUpper();
			}
			else if (value.Length > 1)
			{
				value = value [0].ToString().ToUpper() + value.Substring(1);
			}

			return (value);
		}

		public static string [] Split (this string text, params string [] separator)
		{
			return (text.Split(separator, StringSplitOptions.None));
		}

		public static string [] Split (this string text, StringSplitOptions options, params string [] separator)
		{
			return (text.Split(separator, options));
		}

		public static bool IsHex (this string value)
		{
			if (value.StartsWith("0x"))
				value = value.Substring(2).ToLower();

			foreach (char c in value)
			{
				if (!char.IsDigit(c) && ((c < 'a') || (c > 'f')))
				{
					return (false);
				}
			}

			return (true);
		}

		public static byte FromHexToByte (string value)
		{
			return (byte.Parse(value.StartsWith("0x") ? value.Substring(2) : value, global::System.Globalization.NumberStyles.HexNumber));
		}

		public static int FromHexToInt32 (string value)
		{
			return (int.Parse(value.StartsWith("0x") ? value.Substring(2) : value, global::System.Globalization.NumberStyles.HexNumber));
		}

		public static byte ToByteFromHex (this string value)
		{
			return (FromHexToByte(value));
		}

		public static int ToInt32FromHex (this string value)
		{
			return (FromHexToInt32(value));
		}

		public static string ToHexString (this int value)
		{
			return (string.Format("0x{0:X}", value));
		}
	}
}