using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using HouseOfSynergy.PowerTools.Library.Captures.Samples;
using HouseOfSynergy.PowerTools.Library.EventArguments;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Imaging;
using HouseOfSynergy.PowerTools.Library.Media;
using HouseOfSynergy.PowerTools.Library.Media.Video;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Captures
{
	public sealed class CaptureVideo:
		CaptureBase<SampleVideo>
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private Bitmap ImageCache { get; set; }
		private Pen PenCursorFill { get; set; }
		private Pen PenCursorBorder { get; set; }

		public Size CaptureSizeToTransmit { get; private set; }
		public PixelFormat PixelFormatCapture { get; private set; }
		public PixelFormat PixelFormatTransmit { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		/// <summary>
		/// Initializes a new instance of the CaptureVideo class from the specified arguments.
		/// </summary>
		/// <param name="pixelFormat">The pixel format to capture in.</param>
		/// <param name="sampleRate">The number of samples to get per second.</param>
		/// <param name="screenSizeToCapture">The size of the image to transfer.</param>
		public CaptureVideo (VideoSampleRate sampleRate, PixelFormat pixelFormatCapture, PixelFormat pixelFormatTransmit, VideoSize captureSizeToTransmit)
			: base(type : MediaType.Video, sampleRate : ((int) sampleRate), isBackground : true)
		{
			if (!Enum.IsDefined(typeof(VideoSize), captureSizeToTransmit)) { throw (new ArgumentException("The argument [captureSizeToTransmit] must be a valid member of the VideoSize enumeration.", "captureSizeToTransmit")); }
			if (!Enum.IsDefined(typeof(PixelFormat), pixelFormatCapture)) { throw (new ArgumentException("The argument [pixelFormatCapture] must be a valid member of the [PixelFormat] enumeration.", "pixelFormatCapture")); }
			if (!Enum.IsDefined(typeof(PixelFormat), pixelFormatTransmit)) { throw (new ArgumentException("The argument [pixelFormatTransmit] must be a valid member of the [PixelFormat] enumeration.", "pixelFormatTransmit")); }
			if (!MediaInfoVideo.AllowedPixelFormats.Contains(pixelFormatCapture)) { throw (new ArgumentException("The argument [pixelFormatCapture] must be on of the following: [" + string.Join(", ", MediaInfoVideo.AllowedPixelFormats) + "].", "pixelFormatCapture")); }
			if (!MediaInfoVideo.AllowedPixelFormats.Contains(pixelFormatTransmit)) { throw (new ArgumentException("The argument [pixelFormatTransmit] must be on of the following: [" + string.Join(", ", MediaInfoVideo.AllowedPixelFormats) + "].", "pixelFormatTransmit")); }

			this.PixelFormatCapture = pixelFormatCapture;
			this.PixelFormatTransmit = pixelFormatTransmit;
			this.CaptureSizeToTransmit = VideoUtilities.Sizes [captureSizeToTransmit];

			this.RenderCusror = true;
			this.PenCursorFill = this.AddDisposableObject(new Pen(Color.FromArgb(100, Color.Yellow)));
			this.PenCursorBorder = this.AddDisposableObject(new Pen(Color.FromArgb(255, Color.Black)));
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public bool RenderCusror { get; set; }
		public override int BufferSizeMinimumRequired { get { return (10); } }
		public override bool DataAvailable { get { return (this.Samples.Count > 0); } }
		public override ReadOnlyCollection<double> AllowedSampleRates { get { return (new ReadOnlyCollection<double>(EnumUtilities.GetValues<VideoSampleRate>().ConvertAll<double>(v => ((double) ((int) v))))); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		protected override void OnStarting (CancelEventArgs e)
		{
			base.OnStarting(e);
		}

		protected override void OnStopping (CancelEventArgs e)
		{
			base.OnStopping(e);
		}

		protected override void OnStarted ()
		{
			base.OnStarted();
		}

		protected override void OnStopped ()
		{
			base.OnStopped();
		}

		// TODO: Remove.
		private Font Font = new Font(FontFamily.GenericMonospace, 20);
		private Pen PenFillupdatedRegions = new Pen(Color.FromArgb(100, Color.Yellow));
		protected override void Grab ()
		{
			var exception = new Exception();
			ImageDifferentialResult result = null;
			var imageTransfer = new Bitmap(this.CaptureSizeToTransmit.Width, this.CaptureSizeToTransmit.Height, this.PixelFormatTransmit);

			using (var imageLocal = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, this.PixelFormatCapture))
			{
				using (var graphicsLocal = Graphics.FromImage(imageLocal))
				{
					graphicsLocal.Clear(Color.White);
					graphicsLocal.CopyFromScreen(0, 0, 0, 0, imageLocal.Size);

					var imageCache = new Bitmap(imageLocal.Width, imageLocal.Height, imageLocal.PixelFormat);
					using (var graphicsCache = Graphics.FromImage(imageCache)) { graphicsCache.DrawImage(imageLocal, 0, 0); }

					if (this.ImageCache != null)
					{
						if (ImagingUtilities.GetDifferencialRegions(this.ImageCache, imageLocal, out result, out exception))
						{
							if (result.RegionsChanged.Count > 0)
							{
								foreach (var rectangle in result.RegionsUnchanged)
								{
									// Blacken out unchanged areas for better compression.
									graphicsLocal.FillRectangle(Brushes.Black, rectangle);
									// Draw highlights on changed regions.
									//graphicsLocal.FillRectangle(this.PenFillupdatedRegions.Brush, rectangle);
								}
							}
						}

						this.ImageCache.Dispose();
					}

					// Cache image.
					this.ImageCache = imageCache;
				}

				using (var graphicsTransfer = Graphics.FromImage(imageTransfer))
				{
					graphicsTransfer.Clear(Color.White);
					graphicsTransfer.DrawImage(imageLocal, new Rectangle(0, 0, this.CaptureSizeToTransmit.Width, this.CaptureSizeToTransmit.Height));

					graphicsTransfer.CompositingQuality = CompositingQuality.HighQuality;
					graphicsTransfer.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphicsTransfer.SmoothingMode = SmoothingMode.HighQuality;
					graphicsTransfer.TextRenderingHint = TextRenderingHint.AntiAlias;

					if (this.RenderCusror)
					{
						var ratio = new PointF(((float) this.CaptureSizeToTransmit.Width) / ((float) Screen.PrimaryScreen.Bounds.Width), ((float) this.CaptureSizeToTransmit.Height) / ((float) Screen.PrimaryScreen.Bounds.Height));
						var size = Cursor.Current.Size;
						var position = Cursor.Position;
						var hotspot = Cursor.Current.HotSpot;

						hotspot = new Point((int) (hotspot.X * ratio.X), (int) (hotspot.Y * ratio.Y));
						position = new Point((int) (position.X * ratio.X), (int) (position.Y * ratio.Y));

						var rectangle = new RectangleF(position.X - hotspot.X, position.Y - hotspot.Y, size.Width, size.Height);

						graphicsTransfer.DrawString("Position: " + position.ToString() + ".", this.Font, this.PenCursorBorder.Brush, 10, 10);

						// TODO: Scale position to transfered image size.
						// TODO: Find out if scaling size based on transfered image size matters.
						graphicsTransfer.FillRectangle(this.PenCursorFill.Brush, rectangle);
						Cursor.Current.Draw(graphicsTransfer, rectangle.ToRectangle());
						graphicsTransfer.DrawRectangle(this.PenCursorBorder, rectangle);
					}
				}
			}

			// Dequeue and dispose.
			if (this.Samples.Count >= 10) { while (this.Samples.Count >= 10) { using (var sample = this.Samples.Dequeue()) { } } }

			this.Samples.Enqueue(new SampleVideo(DateTimeOffset.Now, imageTransfer));
		}

		#endregion Methods.

		#region Interface Implementation: System.IDisposable.

		//====================================================================================================
		// Interface Implementation: System.IDisposable.
		//====================================================================================================

		private bool Disposed { get; set; }

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.

					if (this.ImageCache != null)
					{
						try { this.ImageCache.Dispose(); }
						finally { this.ImageCache = null; }
					}

					if (this.PenCursorBorder != null)
					{
						try { this.PenCursorBorder.Dispose(); }
						finally { this.PenCursorBorder = null; }
					}

					if (this.PenCursorFill != null)
					{
						try { this.PenCursorFill.Dispose(); }
						finally { this.PenCursorFill = null; }
					}
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion Interface Implementation: System.IDisposable.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		static CaptureVideo ()
		{
		}

		#endregion Static.
	}
}