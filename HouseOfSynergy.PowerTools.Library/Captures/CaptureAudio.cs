using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Captures.Samples;
using HouseOfSynergy.PowerTools.Library.EventArguments;
using HouseOfSynergy.PowerTools.Library.Media;
using HouseOfSynergy.PowerTools.Library.Media.Audio;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Captures
{
	public sealed class CaptureAudio:
		CaptureBase<SampleAudio>
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public bool Recording { get; private set; }
		private object SyncRootSamples { get; set; }
		public MediaInfoAudio MediaInfoInput { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public CaptureAudio (MediaInfoAudio audioInfo)
			: base(type : MediaType.Audio, sampleRate : audioInfo.SampleRate, isBackground : true)
		{
			this.Recording = false;
			this.MediaInfoInput = audioInfo;
			this.SyncRootSamples = new object();
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public override int BufferSizeMinimumRequired { get { return (this.MediaInfoInput.AverageBytesPerSecond); } }
		public override bool DataAvailable { get { lock (this.SyncRootSamples) { return (this.Samples.Sum(s => s.Data.Length) >= this.BufferSizeMinimumRequired); } } }
		public override ReadOnlyCollection<double> AllowedSampleRates { get { return (new ReadOnlyCollection<double>(EnumUtilities.GetValues<AudioSampleRate>().ConvertAll<double>(v => ((double) ((int) v))))); } }

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
			this.Recording = false;

			base.OnStopped();
		}

		protected override void Grab () { }

		protected override void Iteration ()
		{
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

		#endregion Static.
	}
}