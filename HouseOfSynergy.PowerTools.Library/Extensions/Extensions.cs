using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static byte Median (this IEnumerable<byte> list) { return ((byte) ((list.Max() - list.Min()) / 2F)); }
		public static sbyte Median (this IEnumerable<sbyte> list) { return ((sbyte) ((list.Max() - list.Min()) / 2F)); }
		public static short Median (this IEnumerable<short> list) { return ((short) ((list.Max() - list.Min()) / 2F)); }
		public static ushort Median (this IEnumerable<ushort> list) { return ((ushort) ((list.Max() - list.Min()) / 2F)); }
		public static int Median (this IEnumerable<int> list) { return ((int) ((list.Max() - list.Min()) / 2F)); }
		public static uint Median (this IEnumerable<uint> list) { return ((uint) ((list.Max() - list.Min()) / 2F)); }
		public static long Median (this IEnumerable<long> list) { return ((long) ((list.Max() - list.Min()) / 2F)); }
		public static ulong Median (this IEnumerable<ulong> list) { return ((ulong) ((list.Max() - list.Min()) / 2F)); }
		public static float Median (this IEnumerable<float> list) { return ((list.Max() - list.Min()) / 2F); }
		public static double Median (this IEnumerable<double> list) { return ((list.Max() - list.Min()) / 2D); }

		public static void DrawRectangle (this Graphics graphics, Pen pen, RectangleF rectangle) { graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height); }

		public static void SafeInvoke<T> (this EventHandler<T> @event, object sender, T e, bool throwOnError = false)
			where T: EventArgs
		{
			if (@event != null)
			{
				try
				{
					@event(sender, e);
				}
				catch
				{
					if (throwOnError) { throw; }
				}
			}
		}

		public static void Write (this Stream stream, byte [] buffer) { stream.Write(buffer, 0, buffer.Length); }
		public static int Read (this Stream stream, byte [] buffer) { return (stream.Read(buffer, 0, buffer.Length)); }

		public static bool Read (this NetworkStream stream, int count, TimeSpan timeout, out byte [] data, out Exception exception)
		{
			var read = 0;
			var result = false;
			var watch = Stopwatch.StartNew();

			if (stream == null) { throw (new ArgumentNullException("stream")); }
			if (count <= 0) { throw (new ArgumentOutOfRangeException("count", "The argument [count] must be greater than zero.")); }
			if (timeout <= TimeSpan.Zero) { throw (new ArgumentOutOfRangeException("timeout", "The argument [timeout] must be greater than zero.")); }

			exception = null;
			data = new byte [count];

			try
			{
				do
				{
					if (stream.DataAvailable)
					{
						read += stream.Read(data, read, count - read);

						if (read == count) { result = true; break; }
					}
					else
					{
						Thread.Sleep(TimeSpan.FromMilliseconds(10));
					}

					if (watch.Elapsed >= timeout) { break; }
				}
				while (true);
			}
			catch (Exception e)
			{
				exception = e;
			}
			finally
			{
				watch.Stop();
			}

			return (result);
		}

		public static bool MethodHasParameterArray (this MethodInfo method)
		{
			var parameter = method.GetParameters().LastOrDefault();

			return ((parameter != null) && (parameter.GetCustomAttributes(typeof(ParamArrayAttribute), true).Any()));
		}
	}
}