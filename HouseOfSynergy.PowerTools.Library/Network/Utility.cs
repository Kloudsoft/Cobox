using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Network
{
	public static class Utility
	{
		/// <summary>
		/// Reads the specified number of bytes within a given time frame.
		/// </summary>
		/// <param name="stream">The network stream to read from.</param>
		/// <param name="size">The number of bytes to read from the System.Net.Sockets.NetworkStream.</param>
		/// <param name="threshold">The maximum amount of time to wait until data arrives before giving up.</param>
		/// <param name="data">The buffer which receieved data will be written to.</param>
		/// <param name="exception">The exception encountered or generated if any.</param>
		/// <returns>Returns true if the correct number of bytes is read within the given time frame. False otherwise.</returns>
		public static bool ReadBytes (System.Net.Sockets.NetworkStream stream, int size, System.TimeSpan threshold, ref bool terminate, out byte [] data, out Exception exception)
		{
			int count = 0;
			int countTotal = 0;
			bool result = false;
			byte [] buffer = null;
			System.Diagnostics.Stopwatch watch = null;

			data = null;
			exception = null;

			if (stream == null) { throw (new ArgumentNullException("stream")); }
			if (!stream.CanRead) { throw (new System.IO.IOException("The argument [stream] cannot be read from.")); }
			if (size <= 0) { throw (new ArgumentException("The argument [size] must be greater than zero.", "size")); }
			if (threshold <= System.TimeSpan.Zero) { throw (new ArgumentException("The argument [threshold] must be greater than zero.", "threshold")); }

			try
			{
				data = new byte [size];
				buffer = new byte [size];
				watch = System.Diagnostics.Stopwatch.StartNew();

				while ((!terminate) && (watch.Elapsed < threshold) && (countTotal < size))
				{
					if (stream.DataAvailable)
					{
						count = stream.Read(buffer, 0, size - countTotal);

						if (count > 0)
						{
							System.Array.Copy(buffer, 0, data, countTotal, count);

							countTotal += count;
						}
					}
					else
					{
						System.Threading.Thread.Sleep(System.TimeSpan.FromMilliseconds(20));
					}
				}

				result = ((!terminate) && (watch.Elapsed < threshold) && (countTotal == size));

				if (!result)
				{
					exception = new Exception("The attempt to read from the stream failed due to user intervention or a timeout.");
				}
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
	}
}