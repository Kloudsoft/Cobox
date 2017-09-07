using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Media.Video;

namespace HouseOfSynergy.PowerTools.Library.Media
{
	public sealed class MediaInfoVideo:
		MediaInfo,
		IEquatable<MediaInfo>
	{
		public Size Size { get; private set; }
		public Rectangle Bounds { get; private set; }
		public double AspectRatio { get; private set; }
		public VideoSize VideoSize { get; private set; }
		public PixelFormat PixelFormat { get; private set; }
		public VideoSampleType SampleType { get; private set; }
		public VideoSampleRate VideoSampleRate { get; private set; }
		public VideoAspectRatio VideoAspectRatio { get; private set; }

		/// <summary>
		/// Initializes a new instance of the CaptureVideo class from the specified arguments.
		/// </summary>
		public MediaInfoVideo (PixelFormat pixelFormat, VideoSize size, VideoSampleRate sampleRate, VideoSampleType sampleType)
			: base(type : MediaType.Video, sampleRate : ((int) sampleRate))
		{
			if (!Enum.IsDefined(typeof(VideoSize), size)) { throw (new ArgumentException("The argument [size] must be a valid member of the [VideoSize] enumeration.", "size")); }
			if (!Enum.IsDefined(typeof(PixelFormat), pixelFormat)) { throw (new ArgumentException("The argument [pixelFormat] must be a valid member of the [PixelFormat] enumeration.", "pixelFormat")); }
			if (!Enum.IsDefined(typeof(VideoSampleRate), sampleRate)) { throw (new ArgumentException("The argument [sampleRate] must be a valid member of the [VideoSampleRate] enumeration.", "sampleRate")); }
			if (!Enum.IsDefined(typeof(VideoSampleType), sampleType)) { throw (new ArgumentException("The argument [sampleType] must be a valid member of the [VideoSampleType] enumeration.", "sampleType")); }
			if (!MediaInfoVideo.AllowedPixelFormats.Contains(pixelFormat)) { throw (new ArgumentException("The argument [pixelFormat] must be on of the following: [" + string.Join(", ", MediaInfoVideo.AllowedPixelFormats) + "].", "pixelFormat")); }

			this.VideoSize = size;
			this.SampleType = sampleType;
			this.PixelFormat = pixelFormat;
			this.VideoSampleRate = sampleRate;
			this.Size = VideoUtilities.Sizes [size];
			this.Bounds = new Rectangle(new Point(0, 0), this.Size);

			this.VideoAspectRatio = VideoAspectRatio.Unknown;
			this.AspectRatio = ((double) this.Size.Width) / ((double) this.Size.Height);
		}

		public bool Equals (MediaInfoVideo other)
		{
			bool result = false;

			if (object.ReferenceEquals(this, other))
			{
				result = true;
			}
			else if (!object.ReferenceEquals(other, null))
			{
				result
					= (this.MediaType == other.MediaType)
					&& (this.Size == this.Size)
					&& (this.Bounds == this.Bounds)
					&& (this.VideoSize == this.VideoSize)
					&& (this.SampleType == this.SampleType)
					&& (this.SampleRate == this.SampleRate)
					&& (this.VideoAspectRatio == this.VideoAspectRatio)
					&& (this.PixelFormat == this.PixelFormat)
					;
			}

			return (result);
		}

		public override bool Equals (object obj)
		{
			return ((obj is MediaInfoVideo) && (this.Equals(obj as MediaInfoVideo)));
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			unchecked // Overflow is fine, just wrap.
			{
				hash = 17;
				hash = hash * 23 + base.GetHashCode();
				hash = hash * 23 + this.Size.GetHashCode();
				hash = hash * 23 + this.Bounds.GetHashCode();
				hash = hash * 23 + this.VideoSize.GetHashCode();
				hash = hash * 23 + this.SampleType.GetHashCode();
				hash = hash * 23 + this.SampleRate.GetHashCode();
				hash = hash * 23 + this.VideoAspectRatio.GetHashCode();
				hash = hash * 23 + this.PixelFormat.GetHashCode();
			}

			return (hash);
		}

		public override string ToString ()
		{
			return ("Media Info (Media Type: " + this.MediaType.ToString() + ")");
		}

		public static readonly ReadOnlyCollection<PixelFormat> AllowedPixelFormats = null;

		static MediaInfoVideo ()
		{
			// TODO: Decide.
			//MediaInfoVideo.AllowedPixelFormats = EnumUtilities.GetValues<PixelFormat>().AsReadOnly();
			MediaInfoVideo.AllowedPixelFormats = new ReadOnlyCollection<PixelFormat>(new PixelFormat [] { PixelFormat.Format16bppRgb555, PixelFormat.Format24bppRgb, PixelFormat.Format32bppArgb, });
		}
	}
}