using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Imaging;
using HouseOfSynergy.PowerTools.Library.Media;

namespace HouseOfSynergy.PowerTools.Library.Captures.Samples
{
	public sealed class SampleVideo:
		SampleBase
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public byte [] Data { get; private set; }
		public Image ImageObject { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public SampleVideo (DateTimeOffset dateTimeOffset, Image image)
			: base(MediaType.Video, dateTimeOffset)
		{
			if (image == null) { throw (new ArgumentNullException("bitmap")); }

			this.ImageObject = image;
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

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

					this.Data = null;

					if (this.ImageObject != null)
					{
						this.ImageObject.Dispose();
						this.ImageObject = null;
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

		public static readonly Encoder EncoderQuality = null;
		public static readonly ImageCodecInfo EncoderJpeg = null;
		public static readonly EncoderParameter EncoderParameter = null;
		public static readonly EncoderParameters EncoderParameters = null;

		static SampleVideo ()
		{
			SampleVideo.EncoderQuality = Encoder.Quality;
			SampleVideo.EncoderParameters = new EncoderParameters(1);
			SampleVideo.EncoderParameter = new EncoderParameter(SampleVideo.EncoderQuality, 50L);
			SampleVideo.EncoderParameters.Param [0] = SampleVideo.EncoderParameter;
			SampleVideo.EncoderJpeg = ImagingUtilities.ImageEncoders [ImageFormat.Jpeg.Guid].First();
		}

		#endregion Static.
	}
}