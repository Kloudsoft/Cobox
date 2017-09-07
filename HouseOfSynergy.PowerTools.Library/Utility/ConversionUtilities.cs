using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using HouseOfSynergy.PowerTools.Library.Extensions;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class ConversionUtilities
	{
		#region Common.

		//====================================================================================================
		// Common.
		//====================================================================================================

		#endregion Common.

		#region Number to ISU Notation.

		//====================================================================================================
		// Number to ISU Notation.
		//====================================================================================================

		public static string ToIsuBitsNotation (long value)
		{
			return (ConversionUtilities.ToIsuBitsNotation((double) value));
		}

		public static string ToIsuBitsNotation (double value)
		{
			value = value * 8;

			if (value < 0)
			{
				return ("N/A");
			}
			else if (value == 0)
			{
				return (value.ToString("N0") + " b");
			}
			else if (value == 1)
			{
				return (value.ToString("N0") + " b");
			}
			else if (value < 1000)
			{
				return (value.ToString("N0") + " b");
			}
			else
			{
				var log = Math.Log(value, 2);

				if (log < 20) { return ((value / Math.Pow(1024, 1D)).ToString("N2") + " Kb"); }
				else if (log < 30) { return ((value / Math.Pow(1024, 2D)).ToString("N2") + " Mb"); }
				else if (log < 40) { return ((value / Math.Pow(1024, 3D)).ToString("N2") + " Gb"); }
				else if (log < 50) { return ((value / Math.Pow(1024, 4D)).ToString("N2") + " Tb"); }
				else if (log < 60) { return ((value / Math.Pow(1024, 5D)).ToString("N2") + " Pb"); }
				else if (log < 70) { return ((value / Math.Pow(1024, 6D)).ToString("N2") + " Eb"); }
				else if (log < 80) { return ((value / Math.Pow(1024, 7D)).ToString("N2") + " Zb"); }
				else { return ((value / Math.Pow(1024, 8D)).ToString("N2") + " Yb"); }
			}
		}

		public static string ToIsuBytesNotation (long value)
		{
			return (ConversionUtilities.ToIsuBytesNotation((double) value));
		}

		public static string ToIsuBytesNotation (double value)
		{
			if (value < 0)
			{
				return ("N/A");
			}
			else if (value == 0)
			{
				return (value.ToString("N0") + " Bytes");
			}
			else if (value == 1)
			{
				return (value.ToString("N0") + " Byte");
			}
			else if (value < 1000)
			{
				return (value.ToString("N0") + " Bytes");
			}
			else
			{
				var log = Math.Log(value, 2);

				if (log < 20) { return ((value / Math.Pow(1024, 1D)).ToString("N2") + " KB"); }
				else if (log < 30) { return ((value / Math.Pow(1024, 2D)).ToString("N2") + " MB"); }
				else if (log < 40) { return ((value / Math.Pow(1024, 3D)).ToString("N2") + " GB"); }
				else if (log < 50) { return ((value / Math.Pow(1024, 4D)).ToString("N2") + " TB"); }
				else if (log < 60) { return ((value / Math.Pow(1024, 5D)).ToString("N2") + " PB"); }
				else if (log < 70) { return ((value / Math.Pow(1024, 6D)).ToString("N2") + " EB"); }
				else if (log < 80) { return ((value / Math.Pow(1024, 7D)).ToString("N2") + " ZB"); }
				else { return ((value / Math.Pow(1024, 8D)).ToString("N2") + " YB"); }
			}
		}

		#endregion Number to ISU Notation.

		#region Signed and Unsigned Bytes.

		//====================================================================================================
		// Signed and Unsigned Bytes
		//====================================================================================================

		public static byte ToByte (sbyte value) { return ((byte) (((int) value) + 128)); }
		public static sbyte ToSByte (byte value) { return ((sbyte) (((int) value) - 128)); }

		#endregion Signed and Unsigned Bytes.

		#region Web Data.

		//====================================================================================================
		// Web Data.
		//====================================================================================================

		public static string Encode (string text, Encoding encoding)
		{
			return (Convert.ToBase64String(encoding.GetBytes(text)));
		}

		public static string Decode (string text, Encoding encoding)
		{
			return (encoding.GetString(Convert.FromBase64String(text)));
		}

		#endregion Web Data.

		#region Binary.

		//====================================================================================================
		// Binary.
		//====================================================================================================

		#region Binary: Enums.

		//====================================================================================================
		// Binary: Enums.
		//====================================================================================================

		public static byte [] ToBinaryEnum (Enum value)
		{
			if (value == null) { throw (new ArgumentNullException("value")); }

			var type = value.GetType();
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }

			if (underlyingType == typeof(Byte)) { return (BitConverter.GetBytes((Byte) ((object) value))); }
			if (underlyingType == typeof(SByte)) { return (BitConverter.GetBytes((SByte) ((object) value))); }
			if (underlyingType == typeof(Int16)) { return (BitConverter.GetBytes((Int16) ((object) value))); }
			if (underlyingType == typeof(Int32)) { return (BitConverter.GetBytes((Int32) ((object) value))); }
			if (underlyingType == typeof(Int64)) { return (BitConverter.GetBytes((Int64) ((object) value))); }
			if (underlyingType == typeof(UInt16)) { return (BitConverter.GetBytes((UInt16) ((object) value))); }
			if (underlyingType == typeof(UInt32)) { return (BitConverter.GetBytes((UInt32) ((object) value))); }
			if (underlyingType == typeof(UInt64)) { return (BitConverter.GetBytes((UInt64) ((object) value))); }

			throw (new NotImplementedException("The conversion for type [" + type.FullName + "] has not been implemented."));
		}

		public static byte [] ToBinaryEnum<TEnum> (TEnum value)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }

			if (underlyingType == typeof(Byte)) { return (BitConverter.GetBytes((Byte) ((object) value))); }
			if (underlyingType == typeof(SByte)) { return (BitConverter.GetBytes((SByte) ((object) value))); }
			if (underlyingType == typeof(Int16)) { return (BitConverter.GetBytes((Int16) ((object) value))); }
			if (underlyingType == typeof(Int32)) { return (BitConverter.GetBytes((Int32) ((object) value))); }
			if (underlyingType == typeof(Int64)) { return (BitConverter.GetBytes((Int64) ((object) value))); }
			if (underlyingType == typeof(UInt16)) { return (BitConverter.GetBytes((UInt16) ((object) value))); }
			if (underlyingType == typeof(UInt32)) { return (BitConverter.GetBytes((UInt32) ((object) value))); }
			if (underlyingType == typeof(UInt64)) { return (BitConverter.GetBytes((UInt64) ((object) value))); }

			throw (new NotImplementedException("The conversion for type [" + type.FullName + "] has not been implemented."));
		}

		public static TEnum FromBinaryEnum<TEnum> (byte [] value, int startIndex = 0)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }

			if (underlyingType == typeof(Byte)) { return ((TEnum) ((object) value [startIndex])); }
			if (underlyingType == typeof(SByte)) { return ((TEnum) ((object) value [startIndex])); }
			if (underlyingType == typeof(Int16)) { return ((TEnum) ((object) BitConverter.ToInt16(value, startIndex))); }
			if (underlyingType == typeof(Int32)) { return ((TEnum) ((object) BitConverter.ToInt32(value, startIndex))); }
			if (underlyingType == typeof(Int64)) { return ((TEnum) ((object) BitConverter.ToInt64(value, startIndex))); }
			if (underlyingType == typeof(UInt16)) { return ((TEnum) ((object) BitConverter.ToUInt16(value, startIndex))); }
			if (underlyingType == typeof(UInt32)) { return ((TEnum) ((object) BitConverter.ToUInt32(value, startIndex))); }
			if (underlyingType == typeof(UInt64)) { return ((TEnum) ((object) BitConverter.ToUInt64(value, startIndex))); }

			throw (new NotImplementedException("The conversion for type [" + type.FullName + "] has not been implemented."));
		}

		public static byte [] ToBinaryEnumByte<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Byte)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Byte].", "TEnum")); }

			return (new Byte [] { (Byte) ((object) value) });
		}

		public static TEnum FromBinaryEnumByte<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Byte)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Byte].", "TEnum")); }

			return ((TEnum) ((object) value [startIndex]));
		}

		public static byte [] ToBinaryEnumSByte<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(SByte)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [SByte].", "TEnum")); }

			return (new byte [] { ConversionUtilities.ToByte((SByte) ((object) value)) });
		}

		public static TEnum FromBinaryEnumSByte<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(SByte)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [SByte].", "TEnum")); }

			return ((TEnum) ((object) ConversionUtilities.ToSByte(value [startIndex])));
		}

		public static byte [] ToBinaryEnumInt16<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Int16)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Int16].", "TEnum")); }

			return (BitConverter.GetBytes((Int16) ((object) value)));
		}

		public static TEnum FromBinaryEnumInt16<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Int16)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Int16].", "TEnum")); }

			return ((TEnum) ((object) BitConverter.ToInt16(value, startIndex)));
		}

		public static byte [] ToBinaryEnumInt32<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Int32)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Int32].", "TEnum")); }

			return (BitConverter.GetBytes((Int32) ((object) value)));
		}

		public static TEnum FromBinaryEnumInt32<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Int32)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Int32].", "TEnum")); }

			return ((TEnum) ((object) BitConverter.ToInt32(value, startIndex)));
		}

		public static byte [] ToBinaryEnumInt64<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Int64)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Int64].", "TEnum")); }

			return (BitConverter.GetBytes((Int64) ((object) value)));
		}

		public static TEnum FromBinaryEnumInt64<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(Int64)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [Int64].", "TEnum")); }

			return ((TEnum) ((object) BitConverter.ToInt64(value, startIndex)));
		}

		public static byte [] ToBinaryEnumUInt16<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(UInt16)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [UInt16].", "TEnum")); }

			return (BitConverter.GetBytes((UInt16) ((object) value)));
		}

		public static TEnum FromBinaryEnumUInt16<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(UInt16)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [UInt16].", "TEnum")); }

			return ((TEnum) ((object) BitConverter.ToUInt16(value, startIndex)));
		}

		public static byte [] ToBinaryEnumUInt32<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(UInt32)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [UInt32].", "TEnum")); }

			return (BitConverter.GetBytes((UInt32) ((object) value)));
		}

		public static TEnum FromBinaryEnumUInt32<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(UInt32)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [UInt32].", "TEnum")); }

			return ((TEnum) ((object) BitConverter.ToUInt32(value, startIndex)));
		}

		public static byte [] ToBinaryEnumUInt64<TEnum> (TEnum value)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(UInt64)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [UInt64].", "TEnum")); }

			return (BitConverter.GetBytes((UInt64) ((object) value)));
		}

		public static TEnum FromBinaryEnumUInt64<TEnum> (byte [] value, int startIndex = 0)
		  where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(TEnum);
			var underlyingType = type.GetEnumUnderlyingType();

			if (!type.IsEnum) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration.", "TEnum")); }
			if (underlyingType != typeof(UInt64)) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration with an underlying type of [UInt64].", "TEnum")); }

			return ((TEnum) ((object) BitConverter.ToUInt64(value, startIndex)));
		}

		#endregion Binary: Enums.

		#region Binary: TimeSpan.

		//====================================================================================================
		// Binary: TimeSpan.
		//====================================================================================================

		public static byte [] ToBinary (TimeSpan value)
		{
			return (BitConverter.GetBytes(value.Ticks));
		}

		public static TimeSpan FromBinaryTimeSpan (byte [] value, int startIndex = 0)
		{
			return (TimeSpan.FromTicks(BitConverter.ToInt64(value, startIndex)));
		}

		#endregion Binary: TimeSpan.

		#region Binary: DateTime.

		//====================================================================================================
		// Binary: DateTime.
		//====================================================================================================

		//public static byte [] ToBinary (DateTime value)
		//{
		//    return (BitConverter.GetBytes(value.ToBinary()));
		//}

		//public static DateTime FromBinaryDateTime (byte [] value, int startIndex = 0)
		//{
		//    return (DateTime.FromBinary(BitConverter.ToInt64(value, startIndex));
		//}

		#endregion Binary: DateTime.

		#region Binary: DateTimeOffset.

		//====================================================================================================
		// Binary: DateTimeOffset.
		//====================================================================================================

		public static byte [] ToBinary (DateTimeOffset value)
		{
			return (BitConverter.GetBytes(value.DateTime.ToBinary()).Concat(BitConverter.GetBytes(value.Offset.Ticks)).ToArray());
		}

		public static DateTimeOffset FromBinaryDateTimeOffset (byte [] value, int startIndex = 0)
		{
			return (new DateTimeOffset(DateTime.FromBinary(BitConverter.ToInt64(value, startIndex)), TimeSpan.FromTicks(BitConverter.ToInt64(value, startIndex + sizeof(long)))));
		}

		#endregion Binary: DateTimeOffset.

		#region Binary: Point & PointF.

		//====================================================================================================
		// Binary: Point & PointF.
		//====================================================================================================

		public static byte [] ToBinary (System.Drawing.Point value)
		{
			return (BitConverter.GetBytes(value.X).Concat(BitConverter.GetBytes(value.Y)).ToArray());
		}

		public static System.Drawing.Point FromBinaryPoint (byte [] value, int startIndex = 0)
		{
			return (new System.Drawing.Point(BitConverter.ToInt32(value, startIndex), BitConverter.ToInt32(value, startIndex + sizeof(int))));
		}

		public static byte [] ToBinary (PointF value)
		{
			return (BitConverter.GetBytes(value.X).Concat(BitConverter.GetBytes(value.Y)).ToArray());
		}

		public static PointF FromBinaryPointF (byte [] value, int startIndex = 0)
		{
			return (new PointF(BitConverter.ToSingle(value, startIndex), BitConverter.ToSingle(value, startIndex + sizeof(float))));
		}

		#endregion Binary: Point & PointF.

		#region Binary: Size & SizeF.

		//====================================================================================================
		// Binary: Size & SizeF.
		//====================================================================================================

		public static byte [] ToBinary (System.Drawing.Size value)
		{
			return (BitConverter.GetBytes(value.Width).Concat(BitConverter.GetBytes(value.Height)).ToArray());
		}

		public static System.Drawing.Size FromBinarySize (byte [] value, int startIndex = 0)
		{
			return (new System.Drawing.Size(BitConverter.ToInt32(value, startIndex), BitConverter.ToInt32(value, startIndex + sizeof(int))));
		}

		public static byte [] ToBinary (SizeF value)
		{
			return (BitConverter.GetBytes(value.Width).Concat(BitConverter.GetBytes(value.Height)).ToArray());
		}

		public static SizeF FromBinarySizeF (byte [] value, int startIndex = 0)
		{
			return (new SizeF(BitConverter.ToSingle(value, startIndex), BitConverter.ToSingle(value, startIndex + sizeof(float))));
		}

		#endregion Binary: Size & SizeF.

		#region Binary: Rectangle & RectangleF.

		//====================================================================================================
		// Binary: Rectangle & RectangleF.
		//====================================================================================================

		public static byte [] ToBinary (Rectangle value)
		{
			return (BitConverter.GetBytes(value.X).Concat(BitConverter.GetBytes(value.Y)).Concat(BitConverter.GetBytes(value.Width)).Concat(BitConverter.GetBytes(value.Height)).ToArray());
		}

		public static Rectangle FromBinaryRectangle (byte [] value, int startIndex = 0)
		{
			return (new Rectangle(BitConverter.ToInt32(value, startIndex + (sizeof(int) * 0)), BitConverter.ToInt32(value, startIndex + (sizeof(int) * 1)), BitConverter.ToInt32(value, startIndex + (sizeof(int) * 2)), BitConverter.ToInt32(value, startIndex + (sizeof(int) * 3))));
		}

		public static byte [] ToBinary (RectangleF value)
		{
			return (BitConverter.GetBytes(value.X).Concat(BitConverter.GetBytes(value.Y)).Concat(BitConverter.GetBytes(value.Width)).Concat(BitConverter.GetBytes(value.Height)).ToArray());
		}

		public static RectangleF FromBinaryRectangleF (byte [] value, int startIndex = 0)
		{
			return (new RectangleF(BitConverter.ToSingle(value, startIndex + (sizeof(float) * 0)), BitConverter.ToSingle(value, startIndex + (sizeof(float) * 1)), BitConverter.ToSingle(value, startIndex + (sizeof(float) * 2)), BitConverter.ToSingle(value, startIndex + (sizeof(float) * 3))));
		}

		#endregion Binary: Rectangle & RectangleF.

		#region Binary: IPEndPoint & IPAddress.

		//====================================================================================================
		// Binary: IPEndPoint & IPAddress.
		//====================================================================================================

		public static byte [] ToBinary (IPEndPoint value)
		{
			return (BitConverter.GetBytes(value.Port).Concat(ConversionUtilities.ToBinary(value.Address)).ToArray());
		}

		public static IPEndPoint FromBinaryIPEndPoint (byte [] value, int startIndex = 0)
		{
			return (new IPEndPoint(ConversionUtilities.FromBinaryIPAddress(value, sizeof(int)), BitConverter.ToInt32(value, 0)));
		}

		public static byte [] ToBinary (IPAddress value)
		{
			var bytes = value.GetAddressBytes();
			var length = BitConverter.GetBytes(bytes.Length);
			var family = BitConverter.GetBytes((int) value.AddressFamily);
			var scope = BitConverter.GetBytes((value.AddressFamily == AddressFamily.InterNetwork) ? 0L : value.ScopeId);

			return (family.Concat(scope).Concat(length).Concat(bytes).ToArray());
		}

		public static IPAddress FromBinaryIPAddress (byte [] value, int startIndex = 0)
		{
			var family = BitConverter.ToInt32(value, startIndex);
			var scope = BitConverter.ToInt64(value, startIndex + sizeof(int));
			var length = BitConverter.ToInt32(value, startIndex + sizeof(int) + sizeof(long));
			var bytes = value.GetRange(startIndex + sizeof(int) + sizeof(long) + sizeof(int), length);

			return ((((AddressFamily) family) == AddressFamily.InterNetwork) ? new IPAddress(bytes) : new IPAddress(bytes, scope));
		}

		#endregion Binary: IPEndPoint & IPAddress.

		#region Binary: Primitive Arrays.

		//====================================================================================================
		// Binary: Primitive Arrays.
		//====================================================================================================

		public static byte [] ToBinary (sbyte [] value)
		{
			var data = new byte [value.Length];

			for (int i = 0; i < value.Length; i++)
			{
				data [i] = (byte) (value [i] + 128);
			}

			return (data);
		}

		public static sbyte [] FromBinaryArraySByte (byte [] value, int startIndex = 0)
		{
			var bytes = new sbyte [value.Length];

			for (int i = 0; i < value.Length; i++)
			{
				bytes [i] = (sbyte) (value [i] - 128);
			}

			return (bytes);
		}

		public static byte [] ToBinary (short [] value)
		{
			var size = sizeof(short);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static short [] FromBinaryInt16 (byte [] value, int startIndex = 0)
		{
			var size = sizeof(short);
			var data = new short [value.Length / size];

			for (int i = 0; i < value.Length; i += size)
			{
				data [i] = BitConverter.ToInt16(value, startIndex + (size * i));
			}

			return (data);
		}

		public static byte [] ToBinary (ushort [] value)
		{
			var size = sizeof(ushort);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinary (int [] value)
		{
			var size = sizeof(int);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinary (uint [] value)
		{
			var size = sizeof(uint);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinary (long [] value)
		{
			var size = sizeof(long);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinary (ulong [] value)
		{
			var size = sizeof(ulong);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinary (float [] value)
		{
			var size = sizeof(float);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinary (double [] value)
		{
			var size = sizeof(double);
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var bytes = BitConverter.GetBytes(value [i]);

				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinary (decimal [] value)
		{
			var size = sizeof(decimal);
			var bytes = new byte [] { };
			var data = new byte [value.Length * size];

			for (int i = 0; i < value.Length; i += size)
			{
				var index = 0;
				var ints = decimal.GetBits(value [i]);

				bytes = BitConverter.GetBytes(ints [0]);
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];

				bytes = BitConverter.GetBytes(ints [1]);
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];

				bytes = BitConverter.GetBytes(ints [2]);
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];

				bytes = BitConverter.GetBytes(ints [3]);
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
				data [i + index] = bytes [index++];
			}

			return (data);
		}

		public static byte [] ToBinaryMarshal (float [] value)
		{
			var ptr = IntPtr.Zero;
			var size = sizeof(float);

			ptr = Marshal.AllocHGlobal(value.Length * size);

			try
			{
				Marshal.Copy(value, 0, ptr, value.Length);
				var data = new byte [value.Length * size];
				Marshal.Copy(ptr, data, 0, data.Length);

				return (data);
			}
			finally
			{
				Marshal.FreeHGlobal(ptr);
			}
		}

		public static float [] FromBinarySingleMarshal (byte [] value)
		{
			var ptr = IntPtr.Zero;
			var size = sizeof(float);

			ptr = Marshal.AllocHGlobal(value.Length / size);

			try
			{
				Marshal.Copy(value, 0, ptr, value.Length);
				var data = new float [value.Length / size];
				Marshal.Copy(ptr, data, 0, data.Length);

				return (data);
			}
			finally
			{
				Marshal.FreeHGlobal(ptr);
			}
		}

		#region Binary: Primitive Arrays: Unsafe.

		public static unsafe byte [] ToBinaryUnsafe (short [] value) { return (ConversionUtilities.ToBinaryUnsafe(value, 0, value.Length)); }

		public static unsafe byte [] ToBinaryUnsafe (short [] value, int offset, int count)
		{
			fixed (void* pFloats = value)
			{
				var size = sizeof(short);
				var pBytes = (byte*) pFloats;
				var bytes = new byte [count * size];

				Marshal.Copy((IntPtr) pBytes, bytes, offset * size, count * size);

				return (bytes);
			}
		}

		public static unsafe short [] FromBinaryInt16Unsafe (byte [] bytes) { return (ConversionUtilities.FromBinaryInt16Unsafe(bytes, 0, bytes.Length)); }

		public static unsafe short [] FromBinaryInt16Unsafe (byte [] bytes, int offset, int count)
		{
			fixed (void* pBytes = bytes)
			{
				var size = sizeof(short);
				var source = (short*) pBytes;
				var destination = new short [count / size];

				Marshal.Copy((IntPtr) source, destination, offset / size, count / size);

				return (destination);
			}
		}

		public static unsafe byte [] ToBinaryUnsafe (int [] value) { return (ConversionUtilities.ToBinaryUnsafe(value, 0, value.Length)); }

		public static unsafe byte [] ToBinaryUnsafe (int [] value, int offset, int count)
		{
			fixed (void* pFloats = value)
			{
				var size = sizeof(int);
				var pBytes = (byte*) pFloats;
				var bytes = new byte [count * size];

				Marshal.Copy((IntPtr) pBytes, bytes, offset * size, count * size);

				return (bytes);
			}
		}

		public static unsafe int [] FromBinaryInt32Unsafe (byte [] bytes) { return (ConversionUtilities.FromBinaryInt32Unsafe(bytes, 0, bytes.Length)); }

		public static unsafe int [] FromBinaryInt32Unsafe (byte [] bytes, int offset, int count)
		{
			fixed (void* pBytes = bytes)
			{
				var size = sizeof(int);
				var source = (int*) pBytes;
				var destination = new int [count / size];

				Marshal.Copy((IntPtr) source, destination, offset / size, count / size);

				return (destination);
			}
		}

		public static unsafe byte [] ToBinaryUnsafe (long [] value) { return (ConversionUtilities.ToBinaryUnsafe(value, 0, value.Length)); }

		public static unsafe byte [] ToBinaryUnsafe (long [] value, int offset, int count)
		{
			fixed (void* pFloats = value)
			{
				var size = sizeof(long);
				var pBytes = (byte*) pFloats;
				var bytes = new byte [count * size];

				Marshal.Copy((IntPtr) pBytes, bytes, offset * size, count * size);

				return (bytes);
			}
		}

		public static unsafe long [] FromBinaryInt64Unsafe (byte [] bytes) { return (ConversionUtilities.FromBinaryInt64Unsafe(bytes, 0, bytes.Length)); }

		public static unsafe long [] FromBinaryInt64Unsafe (byte [] bytes, int offset, int count)
		{
			fixed (void* pBytes = bytes)
			{
				var size = sizeof(long);
				var source = (long*) pBytes;
				var destination = new long [count / size];

				Marshal.Copy((IntPtr) source, destination, offset / size, count / size);

				return (destination);
			}
		}

		public static unsafe byte [] ToBinaryUnsafe (float [] value) { return (ConversionUtilities.ToBinaryUnsafe(value, 0, value.Length)); }

		public static unsafe byte [] ToBinaryUnsafe (float [] value, int offset, int count)
		{
			fixed (void* pFloats = value)
			{
				var size = sizeof(float);
				var pBytes = (byte*) pFloats;
				var bytes = new byte [count * size];

				Marshal.Copy((IntPtr) pBytes, bytes, offset * size, count * size);

				return (bytes);
			}
		}

		public static unsafe float [] FromBinarySingleUnsafe (byte [] bytes) { return (ConversionUtilities.FromBinarySingleUnsafe(bytes, 0, bytes.Length)); }

		public static unsafe float [] FromBinarySingleUnsafe (byte [] bytes, int offset, int count)
		{
			fixed (void* pBytes = bytes)
			{
				var size = sizeof(float);
				var source = (float*) pBytes;
				var destination = new float [count / size];

				Marshal.Copy((IntPtr) source, destination, offset / size, count / size);

				return (destination);
			}
		}

		public static unsafe byte [] ToBinaryUnsafe (double [] value) { return (ConversionUtilities.ToBinaryUnsafe(value, 0, value.Length)); }

		public static unsafe byte [] ToBinaryUnsafe (double [] value, int offset, int count)
		{
			fixed (void* pFloats = value)
			{
				var size = sizeof(double);
				var pBytes = (byte*) pFloats;
				var bytes = new byte [count * size];

				Marshal.Copy((IntPtr) pBytes, bytes, offset * size, count * size);

				return (bytes);
			}
		}

		public static unsafe double [] FromBinaryDoubleUnsafe (byte [] bytes) { return (ConversionUtilities.FromBinaryDoubleUnsafe(bytes, 0, bytes.Length)); }

		public static unsafe double [] FromBinaryDoubleUnsafe (byte [] bytes, int offset, int count)
		{
			fixed (void* pBytes = bytes)
			{
				var size = sizeof(double);
				var source = (double*) pBytes;
				var destination = new double [count / size];

				Marshal.Copy((IntPtr) source, destination, offset / size, count / size);

				return (destination);
			}
		}

		#endregion Binary: Primitive Arrays: Unsafe.

		#endregion Binary: Primitive Arrays.

		#endregion Binary.
	}
}