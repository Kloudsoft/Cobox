using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace HouseOfSynergy.PowerTools.Library.Signals
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Sample<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
		public readonly T Value;
		public readonly TimeSpan Time;

		public Sample (T value) { this.Value = value; this.Time = TimeSpan.Zero; }
		public Sample (T value, TimeSpan time) { this.Value = value; this.Time = time; }

		public static readonly int SizeInBytes = -1;
		public static readonly Sample<T> Zero = new Sample<T>();

		static Sample ()
		{
			var type = typeof(T);

			if
			(
				(type == typeof(byte))
				|| (type == typeof(sbyte))
				|| (type == typeof(short))
				|| (type == typeof(ushort))
				|| (type == typeof(int))
				|| (type == typeof(uint))
				|| (type == typeof(long))
				|| (type == typeof(ulong))
				|| (type == typeof(float))
				|| (type == typeof(double))
				|| (type == typeof(decimal))
			)
			{
				Sample<T>.SizeInBytes
					= Marshal.SizeOf(typeof(T))
					+ Marshal.SizeOf(typeof(TimeSpan))
					;
			}
			else
			{
				throw (new ArgumentException("The generic argument [<T>] must be a primitive integral or floating point type."));
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SampleSingle
	{
		public readonly float Value;
		public readonly TimeSpan Time;

		public SampleSingle (float value) { this.Value = value; this.Time = TimeSpan.Zero; }
		public SampleSingle (float value, TimeSpan time) { this.Value = value; this.Time = time; }

		public static readonly int SizeInBytes = -1;
		public static readonly SampleSingle Zero = new SampleSingle();

		static SampleSingle ()
		{
			SampleSingle.SizeInBytes
				= sizeof(float)
				+ Marshal.SizeOf(typeof(TimeSpan))
				;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SampleDouble
	{
		public readonly double Value;
		public readonly TimeSpan Time;

		public SampleDouble (double value) { this.Value = value; this.Time = TimeSpan.Zero; }
		public SampleDouble (double value, TimeSpan time) { this.Value = value; this.Time = time; }

		public static readonly int SizeInBytes = -1;
		public static readonly SampleDouble Zero = new SampleDouble();

		static SampleDouble ()
		{
			SampleDouble.SizeInBytes
				= sizeof(double)
				+ Marshal.SizeOf(typeof(TimeSpan))
				;
		}
	}
}