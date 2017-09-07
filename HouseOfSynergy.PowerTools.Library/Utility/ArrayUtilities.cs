using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class ArrayUtilities
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

		// TODO: Chance to unsafe code for performance.
		public static void ByteArrayToTArray<T> (byte [] byteArray, T [] tArray)
			where T: struct, IComparable, IFormattable, IConvertible
		{
			int size = 0;
			Type type = null;

			type = typeof(T);

			if ((type != typeof(byte)) && (type != typeof(sbyte)) && (type != typeof(short)) && (type != typeof(ushort)) && (type != typeof(int)) && (type != typeof(uint)) && (type != typeof(long)) && (type != typeof(ulong)))
			{
				throw (new ArgumentException("This method only accepts types [Byte], [SByte], [Int16], [UInt16], [Int32], [UInt32], [Int64] or [UInt64].", "<T>"));
			}
			else
			{
				if (byteArray == null)
				{
					throw (new ArgumentNullException("byteArray"));
				}
				else
				{
					if (tArray == null)
					{
						throw (new ArgumentNullException("tArray"));
					}
					else
					{
						size = Marshal.SizeOf(typeof(T));

						if (byteArray.Length != (tArray.Length * size))
						{
							throw (new ArgumentException("The size of byteArray should be [tArray.Length * Marshal.SizeOf(typeof(T))].", "byteArray, tArray"));
						}
						else
						{
							if (type == typeof(byte))
							{
								var temp = ((byte []) ((object) tArray));

								for (int i = 0; i < byteArray.Length; i++)
								{
									temp [i] = byteArray [i];
								}
							}
							else if (type == typeof(sbyte))
							{
								var temp = ((sbyte []) ((object) tArray));

								for (int i = 0; i < byteArray.Length; i++)
								{
									temp [i] = (sbyte) (byteArray [i] - 128);
								}
							}
							else if (type == typeof(short))
							{
								var structure = new StructInt16();
								var temp = ((short []) ((object) tArray));

								for (int i = 0, j = 0; i < byteArray.Length; i += size, j++)
								{
									structure.Byte0 = byteArray [i + 0];
									structure.Byte1 = byteArray [i + 1];
									temp [j] = structure.Value;
								}
							}
							else if (type == typeof(ushort))
							{
								var structure = new StructUInt16();
								var temp = ((ushort []) ((object) tArray));

								for (int i = 0, j = 0; i < byteArray.Length; i += size, j++)
								{
									structure.Byte0 = byteArray [i + 0];
									structure.Byte1 = byteArray [i + 1];
									temp [j] = structure.Value;
								}
							}
							else if (type == typeof(int))
							{
								var structure = new StructInt32();
								var temp = ((int []) ((object) tArray));

								for (int i = 0, j = 0; i < byteArray.Length; i += size, j++)
								{
									structure.Byte0 = byteArray [i + 0];
									structure.Byte1 = byteArray [i + 1];
									structure.Byte2 = byteArray [i + 2];
									structure.Byte3 = byteArray [i + 3];
									temp [j] = structure.Value;
								}
							}
							else if (type == typeof(uint))
							{
								var structure = new StructUInt32();
								var temp = ((uint []) ((object) tArray));

								for (int i = 0, j = 0; i < byteArray.Length; i += size, j++)
								{
									structure.Byte0 = byteArray [i + 0];
									structure.Byte1 = byteArray [i + 1];
									structure.Byte2 = byteArray [i + 2];
									structure.Byte3 = byteArray [i + 3];
									temp [j] = structure.Value;
								}
							}
							else if (type == typeof(long))
							{
								var structure = new StructInt64();
								var temp = ((long []) ((object) tArray));

								for (int i = 0, j = 0; i < byteArray.Length; i += size, j++)
								{
									structure.Byte0 = byteArray [i + 0];
									structure.Byte1 = byteArray [i + 1];
									structure.Byte2 = byteArray [i + 2];
									structure.Byte3 = byteArray [i + 3];
									structure.Byte4 = byteArray [i + 4];
									structure.Byte5 = byteArray [i + 5];
									structure.Byte6 = byteArray [i + 6];
									structure.Byte7 = byteArray [i + 7];
									temp [j] = structure.Value;
								}
							}
							else if (type == typeof(ulong))
							{
								var structure = new StructUInt64();
								var temp = ((ulong []) ((object) tArray));

								for (int i = 0, j = 0; i < byteArray.Length; i += size, j++)
								{
									structure.Byte0 = byteArray [i + 0];
									structure.Byte1 = byteArray [i + 1];
									structure.Byte2 = byteArray [i + 2];
									structure.Byte3 = byteArray [i + 3];
									structure.Byte4 = byteArray [i + 4];
									structure.Byte5 = byteArray [i + 5];
									structure.Byte6 = byteArray [i + 6];
									structure.Byte7 = byteArray [i + 7];
									temp [j] = structure.Value;
								}
							}
						}
					}
				}
			}
		}
	}
}