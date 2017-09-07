using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.IO;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	public class Graph:
		IDisposable,
		IStreamWritable<float>,
		ISurface
	{
		#region Fields.

		//====================================================================================================
		// Fields.
		//====================================================================================================

		private float _Zoom = 1F;
		private SizeF _Offset = SizeF.Empty;
		private RectangleF _Bounds = RectangleF.Empty;
		private readonly LiveStream<float> LiveStream = null;

		/// <summary>
		/// Buffer declared at class level to avoid repeated re-allocation.
		/// </summary>
		private readonly float [] Buffer = null;

		#endregion Fields.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public Graph (int bufferSize)
		{
			if (bufferSize <= 0) { throw (new ArgumentException("The argument [bufferSize] must be greater then zero.", "bufferSize")); }

			this._Zoom = 1F;
			this._Offset = SizeF.Empty;
			this._Bounds = RectangleF.Empty;
			this.Buffer = new float [bufferSize];
			this.LiveStream = new LiveStream<float>();
		}

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		// TODO: Remove.
		public long BufferLength { get { return (this.LiveStream.BufferLength); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		#endregion Methods.

		#region Interface: IStreamWritable<T>.

		//====================================================================================================
		// Interface: IStreamWritable<T>.
		//====================================================================================================

		public bool CanWrite { get { return (true); } }

		public int Write (float [] buffer)
		{
			return (this.LiveStream.Write(buffer));
		}

		public int Write (float [] buffer, int offset, int count)
		{
			return (this.LiveStream.Write(buffer, offset, count));
		}

		#endregion Interface: IStreamWritable<T>.

		#region Interface: IPannable.

		//====================================================================================================
		// Interface: IPannable.
		//====================================================================================================

		public SizeF Offset { get { return (this._Offset); } }

		#endregion Interface: IPannable.

		#region Interface: IZoomable.

		//====================================================================================================
		// Interface: IZoomable.
		//====================================================================================================

		public float Zoom
		{
			get
			{
				return (this._Zoom);
			}
			set
			{
				if (value < Graph.ZoomMinimum) { this._Zoom = Graph.ZoomMinimum; }
				else if (value > Graph.ZoomMaximum) { this._Zoom = Graph.ZoomMaximum; }
				else { this._Zoom = value; }
			}
		}

		#endregion Interface: IZoomable.

		#region Interface: IDrawable.

		//====================================================================================================
		// Interface: IDrawable.
		//====================================================================================================

		public RectangleF Bounds { get { return (this._Bounds); } }

		public void SetBounds (RectangleF bounds)
		{
			this._Bounds = bounds;
		}

		public void Render (Graphics graphics)
		{
			Array.Clear(this.Buffer, 0, this.Buffer.Length);

			if (this.LiveStream.HasData)
			{
				var x = 0F;
				var y = 0F;
				var px = 0F;
				var py = 0F;
				var min = 0F;
				var max = 0F;
				var count = 0;
				var delta = 0F;

				min = this.Buffer.Min();
				max = this.Buffer.Max();
				delta = max - min;
				px = this._Bounds.X;
				py = this._Bounds.Y + (this._Bounds.Height / 2F);

				count = this.LiveStream.Read(this.Buffer);

				for (var i = 0; i < this.Buffer.Length; i++)
				{
					x = ((float) i) / ((float) this.Buffer.Length);
					x = this._Bounds.X + (x * this._Bounds.Width);

					y = this.Buffer [i] * (this._Bounds.Height / 2F);
					y = this._Bounds.Y + ((this._Bounds.Height / 2F) + y);

					graphics.DrawLine(Pens.Black, px, py, x, y);

					px = x;
					py = y;
				}
			}
		}

		#endregion Interface: IDrawable.

		#region Interface: IDisposable.

		//====================================================================================================
		// Interface: IDisposable.
		//====================================================================================================

		private bool Disposed = false;

		~Graph ()
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

					if (this.LiveStream != null)
					{
						try { this.LiveStream.Dispose(); }
						finally { }
					}
				}

				// Unmanaged.

				this.Disposed = true;
			}
		}

		#endregion Interface: IDisposable.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public const float ZoomMinimum = 1;
		public const float ZoomMaximum = 1000;

		static Graph ()
		{
		}

		#endregion Static.
	}
}