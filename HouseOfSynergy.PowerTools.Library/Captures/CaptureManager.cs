using System;
using System.Drawing.Imaging;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Media;
using HouseOfSynergy.PowerTools.Library.Media.Audio;
using HouseOfSynergy.PowerTools.Library.Media.Video;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Captures
{
	public sealed class CaptureManager:
		Disposable
	{
		#region Events.

		//====================================================================================================
		// Events.
		//====================================================================================================

		#endregion Events.

		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public bool Running { get; private set; }
		public object SyncRoot { get; private set; }
		public CaptureVideo CaptureVideo { get; private set; }
		public CaptureAudio CaptureAudio { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public CaptureManager ()
		{
			this.Running = false;
			this.SyncRoot = new object();
			this.CaptureVideo = new CaptureVideo(VideoSampleRate.VideoSampleRate_0010, PixelFormat.Format24bppRgb, PixelFormat.Format16bppRgb555, VideoSize.VideoSize_1024_0768);
			this.CaptureAudio = new CaptureAudio(new MediaInfoAudio(AudioSampleRate.AudioSampleRate_016000, AudioBitsPerSample.AudioBitsPerSample_16, AudioChannelCount.AudioChannelCount_Stereo));

			// TODO: Document.
			VideoUtilities.Sizes.Clear();
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

		public void Start ()
		{
			lock (this.SyncRoot)
			{
				if (!this.Running)
				{
					this.CaptureAudio.Start();
					this.CaptureVideo.Start();

					this.Running = true;
				}
			}
		}

		public void Stop ()
		{
			lock (this.SyncRoot)
			{
				if (this.Running)
				{
					this.CaptureAudio.Stop();
					this.CaptureVideo.Stop();

					this.Running = false;
				}
			}
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

					this.Stop();
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion Interface Implementation: System.IDisposable.
	}
}