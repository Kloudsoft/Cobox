using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using HouseOfSynergy.PowerTools.Library.Collections;
using HouseOfSynergy.PowerTools.Library.Generics;
using HouseOfSynergy.PowerTools.Library.IO;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	public static class GenericStream
	{
	}

	public abstract class GenericStream<T>:
		IStream<T>,
		IStreamReadable<T>,
		IStreamWritable<T>,
		IDisposable
		where T: struct, IComparable, IFormattable, IConvertible
	{
		public readonly object SyncRoot = new object();

		public GenericStream ()
		{
			lock (this.SyncRoot)
			{
				GenericUtilities<T>.ThrowOnIncompatibleGenericType();
			}
		}

		public abstract bool CanRead { get; }
		public abstract bool CanWrite { get; }
		public abstract bool HasData { get; }

		/// <summary>
		/// Writes data to the stream.
		/// </summary>
		/// <param name="buffer">An value to write to the stream.</param>
		public abstract int Write (T value);

		/// <summary>
		/// Writes data to the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that contains the data to write to the stream.</param>
		public abstract int Write (T [] buffer);

		/// <summary>
		/// Writes data to the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that contains the data to write to the stream.</param>
		/// <param name="offset">The location in buffer from which to start writing data.</param>
		/// <param name="count">The number of elements to write to the stream.</param>
		public abstract int Write (T [] buffer, int offset, int count);

		/// <summary>
		/// Reads data from the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that is the location in memory to store data read from the stream.</param>
		/// <returns>The number of elements read from the stream.</returns>
		public abstract int Read (T [] buffer);

		/// <summary>
		/// Reads data from the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that is the location in memory to store data read from the stream.</param>
		/// <param name="offset">The location in buffer to begin storing the data to.</param>
		/// <param name="count">The number of elements to read from the stream.</param>
		/// <returns>The number of elements read from the stream.</returns>
		public abstract int Read (T [] buffer, int offset, int count);

		protected void ThrowOnDisposed ()
		{
			if (this.Disposed)
			{
				throw (new ObjectDisposedException("GenericStream<T>"));
			}
		}

		#region Interface: IDisposable.

		//====================================================================================================
		// Interface: IDisposable.
		//====================================================================================================

		private bool Disposed = false;

		~GenericStream ()
		{
			this.Dispose(false);
		}

		public void Dispose ()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.
				}

				// Unmanaged.

				this.Disposed = true;
			}
		}

		#endregion Interface: IDisposable.
	}
}