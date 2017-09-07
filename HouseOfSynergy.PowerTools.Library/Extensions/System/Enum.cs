using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static sbyte ToSByte (this Enum value) { return ((sbyte) ((object) value)); }
		public static byte ToByte (this Enum value) { return ((byte) ((object) value)); }
		public static short ToInt16 (this Enum value) { return ((short) ((object) value)); }
		public static ushort ToIntU16 (this Enum value) { return ((ushort) ((object) value)); }
		public static int ToInt32 (this Enum value) { return ((int) ((object) value)); }
		public static uint ToIntU32 (this Enum value) { return ((uint) ((object) value)); }
		public static long ToInt64 (this Enum value) { return ((long) ((object) value)); }
		public static ulong ToIntU64 (this Enum value) { return ((ulong) ((object) value)); }
	}
}