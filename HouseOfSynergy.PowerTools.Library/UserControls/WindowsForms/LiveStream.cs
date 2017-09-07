using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using HouseOfSynergy.PowerTools.Library.Collections;
using HouseOfSynergy.PowerTools.Library.Generics;
using HouseOfSynergy.PowerTools.Library.IO;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	public sealed class LiveStream<T>:
		GenericStream<T>,
		IStream<T>,
		IStreamReadable<T>,
		IStreamWritable<T>,
		IDisposable
		where T: struct, IComparable, IFormattable, IConvertible
	{
		private readonly MemoryStream MemoryStream = null;

		public LiveStream ()
		{
			lock (this.SyncRoot)
			{
				GenericUtilities<T>.ThrowOnIncompatibleGenericType();

				this.MemoryStream = new MemoryStream();
			}
		}

		// TODO: Remove.
		public long BufferLength { get { return (this.MemoryStream.Length); } }

		public sealed override bool HasData { get { this.ThrowOnDisposed(); return (true); } }
		public sealed override bool CanRead { get { this.ThrowOnDisposed(); return (true); } }
		public sealed override bool CanWrite { get { this.ThrowOnDisposed(); return (true); } }

		public void Clear ()
		{
			lock (this.SyncRoot)
			{
				if (this.MemoryStream.Length > 0)
				{
					var buffer = new byte [this.MemoryStream.Length];
					this.MemoryStream.Read(buffer, 0, buffer.Length);
				}
			}
		}

		/// <summary>
		/// Writes data to the stream.
		/// </summary>
		/// <param name="buffer">An value to write to the stream.</param>
		public sealed override int Write (T value)
		{
			this.ThrowOnDisposed();

			lock (this.SyncRoot) { return (this.Write(new T [] { value })); }
		}

		/// <summary>
		/// Writes data to the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that contains the data to write to the stream.</param>
		public sealed override int Write (T [] buffer)
		{
			this.ThrowOnDisposed();

			lock (this.SyncRoot) { return (this.Write(buffer, 0, buffer.Length)); }
		}

		/// <summary>
		/// Writes data to the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that contains the data to write to the stream.</param>
		/// <param name="offset">The location in buffer from which to start writing data.</param>
		/// <param name="count">The number of elements to write to the stream.</param>
		/// <returns>The number of elements written to the stream.</returns>
		public sealed override int Write (T [] buffer, int offset, int count)
		{
			this.ThrowOnDisposed();

			lock (this.SyncRoot)
			{
				var processed = 0;

				if (buffer == null) { throw (new ArgumentNullException("buffer")); }
				if (offset < 0) { throw (new ArgumentOutOfRangeException("offset", offset, "Non-negative number required.")); }
				if (count < 0) { throw (new ArgumentOutOfRangeException("count", count, "Non-negative number required.")); }
				if ((buffer.Length - offset) < count) { throw (new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.", "buffer.Length, offset, count")); }

				var bytes = new byte [count * GenericUtilities<T>.SizeOfT];
				Buffer.BlockCopy(buffer, offset * GenericUtilities<T>.SizeOfT, bytes, 0, count * GenericUtilities<T>.SizeOfT);

				//var handleToFloatBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
				//var pointerToFloatBuffer = handleToFloatBuffer.AddrOfPinnedObject();
				//Marshal.Copy(pointerToFloatBuffer, bytes, offset * this.SizeOfT, count * this.SizeOfT);
				//handleToFloatBuffer.Free();

				this.MemoryStream.Position = this.MemoryStream.Length;
				this.MemoryStream.Write(bytes, 0, bytes.Length);
				processed = bytes.Length;

				return (processed);
			}
		}

		/// <summary>
		/// Reads data from the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that is the location in memory to store data read from the stream.</param>
		/// <returns>The number of elements read from the stream.</returns>
		public sealed override int Read (T [] buffer)
		{
			this.ThrowOnDisposed();

			lock (this.SyncRoot) { return (this.Read(buffer, 0, buffer.Length)); }
		}

		/// <summary>
		/// Reads data from the stream.
		/// </summary>
		/// <param name="buffer">An array of type [T] that is the location in memory to store data read from the stream.</param>
		/// <param name="offset">The location in buffer to begin storing the data to.</param>
		/// <param name="count">The number of elements to read from the stream.</param>
		/// <returns>The number of elements read from the stream.</returns>
		public sealed override int Read (T [] buffer, int offset, int count)
		{
			this.ThrowOnDisposed();

			lock (this.SyncRoot)
			{
				var processed = 0;

				if (buffer == null) { throw (new ArgumentNullException("buffer")); }
				if (offset < 0) { throw (new ArgumentOutOfRangeException("offset", offset, "Non-negative number required.")); }
				if (count < 0) { throw (new ArgumentOutOfRangeException("count", count, "Non-negative number required.")); }
				if ((buffer.Length - offset) < count) { throw (new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.", "buffer.Length, offset, count")); }

				var bytes = new byte [count * GenericUtilities<T>.SizeOfT];
				if (this.MemoryStream.Length != 0)
				{
					this.MemoryStream.Position = 0;
					processed = this.MemoryStream.Read(bytes, 0, bytes.Length);
					Buffer.BlockCopy(bytes, 0, buffer, offset * GenericUtilities<T>.SizeOfT, count * GenericUtilities<T>.SizeOfT);
				}

				//var handleToFloatBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
				//var pointerToFloatBuffer = handleToFloatBuffer.AddrOfPinnedObject();
				//Marshal.Copy(bytes, 0 * this.SizeOfT, pointerToFloatBuffer, processed * this.SizeOfT);
				//handleToFloatBuffer.Free();

				return (processed);
			}
		}

		#region Interface: IDisposable.

		//====================================================================================================
		// Interface: IDisposable.
		//====================================================================================================

		private bool Disposed = false;

		protected sealed override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.
					lock (this.SyncRoot)
					{
						//this._Buffer = null;
					}
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion Interface: IDisposable.
	}
}