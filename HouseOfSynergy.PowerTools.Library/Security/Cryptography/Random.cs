using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Security.Cryptography
{
	public static class Random
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct StructInt16 { [FieldOffset(0)] public Int16 Value; [FieldOffset(0)] public byte Byte0; [FieldOffset(1)] public byte Byte1; }

		[StructLayout(LayoutKind.Explicit)]
		private struct StructUInt16 { [FieldOffset(0)] public UInt16 Value; [FieldOffset(0)] public byte Byte0; [FieldOffset(1)] public byte Byte1; }

		[StructLayout(LayoutKind.Explicit)]
		private struct StructInt32 { [FieldOffset(0)] public Int32 Value; [FieldOffset(0)] public byte Byte0; [FieldOffset(1)] public byte Byte1; [FieldOffset(2)] public byte Byte2; [FieldOffset(3)] public byte Byte3; }

		[StructLayout(LayoutKind.Explicit)]
		private struct StructUInt32 { [FieldOffset(0)] public UInt32 Value; [FieldOffset(0)] public byte Byte0; [FieldOffset(1)] public byte Byte1; [FieldOffset(2)] public byte Byte2; [FieldOffset(3)] public byte Byte3; }

		[StructLayout(LayoutKind.Explicit)]
		private struct StructInt64 { [FieldOffset(0)] public Int64 Value; [FieldOffset(0)] public byte Byte0; [FieldOffset(1)] public byte Byte1; [FieldOffset(2)] public byte Byte2; [FieldOffset(3)] public byte Byte3; [FieldOffset(4)] public byte Byte4; [FieldOffset(5)] public byte Byte5; [FieldOffset(6)] public byte Byte6; [FieldOffset(7)] public byte Byte7; }

		[StructLayout(LayoutKind.Explicit)]
		private struct StructUInt64 { [FieldOffset(0)] public UInt64 Value; [FieldOffset(0)] public byte Byte0; [FieldOffset(1)] public byte Byte1; [FieldOffset(2)] public byte Byte2; [FieldOffset(3)] public byte Byte3; [FieldOffset(4)] public byte Byte4; [FieldOffset(5)] public byte Byte5; [FieldOffset(6)] public byte Byte6; [FieldOffset(7)] public byte Byte7; }

		public static bool Generate (byte [] data, bool nonZeroOnly = false)
		{
			bool result = false;

			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				result = Random.Generate(generator, data, nonZeroOnly);
			}

			return (result);
		}

		public static void Generate<T> (T [] data, bool nonZeroOnly = false)
			where T: struct, IComparable, IFormattable, IConvertible
		{
			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				Random.Generate<T>(generator, data, nonZeroOnly);
			}
		}

		public static void Generate<T> (RandomNumberGenerator generator, T [] data, bool nonZeroOnly = false)
			where T: struct, IComparable, IFormattable, IConvertible
		{
			int size = 0;
			byte [] bytes = null;

			if ((typeof(T) != typeof(byte)) && (typeof(T) != typeof(sbyte)) && (typeof(T) != typeof(short)) && (typeof(T) != typeof(ushort)) && (typeof(T) != typeof(int)) && (typeof(T) != typeof(uint)) && (typeof(T) != typeof(long)) && (typeof(T) != typeof(ulong)))
			{
				throw (new ArgumentException("This method only accepts types [Byte], [SByte], [Int16], [UInt16], [Int32], [UInt32], [Int64] or [UInt64].", "<T>"));
			}

			if (typeof(T) == typeof(byte))
			{
				if (nonZeroOnly) { generator.GetNonZeroBytes((byte []) ((object) data)); }
				else { generator.GetBytes((byte []) ((object) data)); }
			}
			else
			{
				size = Marshal.SizeOf(typeof(T));
				bytes = new byte [data.Length * size];

				if (nonZeroOnly) { generator.GetNonZeroBytes(bytes); }
				else { generator.GetBytes(bytes); }

				using (MemoryStream stream = new MemoryStream(bytes))
				{
					using (BinaryReader reader = new BinaryReader(stream))
					{
						if (typeof(T) == typeof(sbyte)) { sbyte [] temp = ((sbyte []) ((object) data)); for (int i = 0; i < data.Length; i++) { temp [i] = reader.ReadSByte(); } }
						else if (typeof(T) == typeof(short)) { short [] temp = ((short []) ((object) data)); for (int i = 0; i < data.Length; i++) { temp [i] = reader.ReadInt16(); } }
						else if (typeof(T) == typeof(ushort)) { ushort [] temp = ((ushort []) ((object) data)); for (int i = 0; i < data.Length; i++) { temp [i] = reader.ReadUInt16(); } }
						else if (typeof(T) == typeof(int)) { int [] temp = ((int []) ((object) data)); for (int i = 0; i < data.Length; i++) { temp [i] = reader.ReadInt32(); } }
						else if (typeof(T) == typeof(uint)) { uint [] temp = ((uint []) ((object) data)); for (int i = 0; i < data.Length; i++) { temp [i] = reader.ReadUInt32(); } }
						else if (typeof(T) == typeof(long)) { long [] temp = ((long []) ((object) data)); for (int i = 0; i < data.Length; i++) { temp [i] = reader.ReadInt64(); } }
						else if (typeof(T) == typeof(ulong)) { ulong [] temp = ((ulong []) ((object) data)); for (int i = 0; i < data.Length; i++) { temp [i] = reader.ReadUInt64(); } }
					}
				}
			}
		}

		public static void GenerateNew<T> (T [] data, bool nonZeroOnly = false)
			where T: struct, IComparable, IFormattable, IConvertible
		{
			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				GenerateNew<T>(generator, data, nonZeroOnly);
			}
		}

		public static void GenerateNew<T> (RandomNumberGenerator generator, T [] data, bool nonZeroOnly = false)
			where T: struct, IComparable, IFormattable, IConvertible
		{
			int size = 0;
			Type type = null;
			byte [] bytes = null;

			type = typeof(T);

			if ((type != typeof(byte)) && (type != typeof(sbyte)) && (type != typeof(short)) && (type != typeof(ushort)) && (type != typeof(int)) && (type != typeof(uint)) && (type != typeof(long)) && (type != typeof(ulong)))
			{
				throw (new ArgumentException("This method only accepts types [Byte], [SByte], [Int16], [UInt16], [Int32], [UInt32], [Int64] or [UInt64].", "<T>"));
			}

			if (type == typeof(byte))
			{
				if (nonZeroOnly) { generator.GetNonZeroBytes((byte []) ((object) data)); }
				else { generator.GetBytes((byte []) ((object) data)); }
			}
			else
			{
				size = Marshal.SizeOf(typeof(T));
				bytes = new byte [data.Length * size];

				if (nonZeroOnly) { generator.GetNonZeroBytes(bytes); }
				else { generator.GetBytes(bytes); }

				ArrayUtilities.ByteArrayToTArray<T>(bytes, data);
			}
		}

		public static bool Generate (short [] data, bool nonZeroOnly = false)
		{
			bool result = false;

			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				result = Random.Generate(generator, data, nonZeroOnly);
			}

			return (result);
		}

		public static bool Generate (int [] data, bool nonZeroOnly = false)
		{
			bool result = false;

			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				result = Random.Generate(generator, data, nonZeroOnly);
			}

			return (result);
		}

		public static bool GenerateRandom (long [] data, bool nonZeroOnly = false)
		{
			bool result = false;

			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				result = Random.Generate(generator, data, nonZeroOnly);
			}

			return (result);
		}

		public static bool Generate (RandomNumberGenerator generator, byte [] data, bool nonZeroOnly = false)
		{
			bool result = false;

			if (generator == null)
			{
				throw (new ArgumentNullException("generator"));
			}

			if (data == null)
			{
				throw (new ArgumentNullException("data"));
			}
			else if (data.Length == 0)
			{
				throw (new ArgumentException("The argument [data] cannot be an empty array.", "data"));
			}

			if (nonZeroOnly)
			{
				generator.GetNonZeroBytes(data);
			}
			else
			{
				generator.GetBytes(data);
			}

			return (result);
		}

		public static bool Generate (RandomNumberGenerator generator, short [] data, bool nonZeroOnly = false)
		{
			int length = 0;
			bool result = false;
			byte [] bytes = null;

			if (generator == null)
			{
				throw (new ArgumentNullException("generator"));
			}

			if (data == null)
			{
				throw (new ArgumentNullException("data"));
			}
			else if (data.Length == 0)
			{
				throw (new ArgumentException("The argument [data] cannot be an empty array.", "data"));
			}

			length = sizeof(short);
			bytes = new byte [data.Length * length];

			result = Random.Generate(generator, bytes, nonZeroOnly);

			using (MemoryStream stream = new MemoryStream(bytes))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					for (int i = 0; i < data.Length; i++)
					{
						data [i] = reader.ReadInt16();
					}
				}
			}

			return (result);
		}

		public static bool Generate (RandomNumberGenerator generator, int [] data, bool nonZeroOnly = false)
		{
			int length = 0;
			bool result = false;
			byte [] bytes = null;

			if (generator == null)
			{
				throw (new ArgumentNullException("generator"));
			}

			if (data == null)
			{
				throw (new ArgumentNullException("data"));
			}
			else if (data.Length == 0)
			{
				throw (new ArgumentException("The argument [data] cannot be an empty array.", "data"));
			}

			length = sizeof(int);
			bytes = new byte [data.Length * length];

			result = Random.Generate(generator, bytes, nonZeroOnly);

			using (MemoryStream stream = new MemoryStream(bytes))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					for (int i = 0; i < data.Length; i++)
					{
						data [i] = reader.ReadInt32();
					}
				}
			}

			return (result);
		}

		public static bool Generate (RandomNumberGenerator generator, long [] data, bool nonZeroOnly = false)
		{
			int length = 0;
			bool result = false;
			byte [] bytes = null;

			if (generator == null)
			{
				throw (new ArgumentNullException("generator"));
			}

			if (data == null)
			{
				throw (new ArgumentNullException("data"));
			}
			else if (data.Length == 0)
			{
				throw (new ArgumentException("The argument [data] cannot be an empty array.", "data"));
			}

			length = sizeof(long);
			bytes = new byte [data.Length * length];

			result = HouseOfSynergy.PowerTools.Library.Security.Cryptography.Random.Generate(generator, bytes, nonZeroOnly);

			using (MemoryStream stream = new MemoryStream(bytes))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					for (int i = 0; i < data.Length; i++)
					{
						data [i] = reader.ReadInt64();
					}
				}
			}

			return (result);
		}

		public static bool Generate (RandomNumberGenerator generator, int length, char [] validCharacters, out string data)
		{
			bool result = false;
			byte [] bytes = null;
			char [] chars = null;

			data = null;

			if (generator == null)
			{
				throw (new ArgumentNullException("generator"));
			}

			if (length <= 0)
			{
				throw (new ArgumentException("The argument [length] cannot be less than 1.", "length"));
			}

			if (validCharacters == null)
			{
				throw (new ArgumentNullException("charList"));
			}
			else if (validCharacters.Length == 0)
			{
				throw (new ArgumentException("The argument [validCharacters] cannot be an empty array.", "validCharacters"));
			}

			chars = new char [validCharacters.Length * 256];
			bytes = new byte [chars.Length * 2];

			if (Random.Generate(bytes))
			{
				for (int i = 0; i < bytes.Length; i += 2)
				{
				}
			}

			return (result);
		}
	}
}