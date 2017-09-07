using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.Captures.Samples;
using HouseOfSynergy.PowerTools.Library.EventArguments;
using HouseOfSynergy.PowerTools.Library.Media;
using HouseOfSynergy.PowerTools.Library.Threading;

namespace HouseOfSynergy.PowerTools.Library.Captures
{
	public abstract class CaptureBase<T>:
		ThreadBase
		where T: SampleBase
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

		public bool Mute { get; set; }
		public double SampleRate { get; private set; }
		public TimeSpan Interval { get; private set; }
		protected Queue<T> Samples { get; private set; }
		public TimeSpan LastCapture { get; private set; }
		public MediaType CaptureType { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		protected CaptureBase (MediaType type, double sampleRate, bool isBackground = true)
			: base(isBackground)
		{
			if (sampleRate <= 0) { throw (new ArgumentException("The argument [sampleRate] must be greater than 0.", "sampleRate")); }
			if (!this.AllowedSampleRates.Contains(sampleRate)) { throw (new ArgumentException("The argument [sampleRate] must be one of the following: [" + string.Join(", ", this.AllowedSampleRates) + "].", "sampleRate")); }

			this.Mute = false;
			this.CaptureType = type;
			this.Samples = new Queue<T>();

			this.SampleRate = sampleRate;
			this.Interval = TimeSpan.FromTicks((long) ((1D / sampleRate) * TimeSpan.TicksPerSecond));

			if (this.Interval.Ticks <= 0)
			{
				throw (new ArgumentOutOfRangeException("sampleRate", "The argument [sampleRate] must allow an interval of at least 10 milliseconds."));
			}
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		/// <summary>
		/// Determines wherether data should be reported as available.
		/// It is up to the inheriting class to override both [BufferSizeMinimumRequired] and [DataAvailable].
		/// As an example, videos may want to interpret each sample as a frame whereas audio derivatives may want to
		/// interpret each sample in terms of number of bytes.
		/// </summary>
		public abstract bool DataAvailable { get; }
		/// <summary>
		/// Determines the minimum size of the buffer.
		/// It is up to the inheriting class to override both [BufferSizeMinimumRequired] and [DataAvailable].
		/// As an example, videos may want to interpret each sample as a frame whereas audio derivatives may want to
		/// interpret each sample in terms of number of bytes.
		/// </summary>
		public abstract int BufferSizeMinimumRequired { get; }
		/// <summary>
		/// Provides a list of allowable sampling rates.
		/// </summary>
		public abstract ReadOnlyCollection<double> AllowedSampleRates { get; }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		protected override void OnStarting (CancelEventArgs e) { }
		protected override void OnStopping (CancelEventArgs e) { }

		protected override void OnStarted ()
		{
			this.LastCapture = TimeSpan.Zero;
		}

		protected override void OnStopped ()
		{
			while (this.Samples.Count > 0)
			{
				var sample = this.Samples.Dequeue();

				sample.Dispose();
				sample = null;
			}
		}

		protected sealed override void OnProcess (CancellationToken cancellationToken)
		{
			var watch = new Stopwatch();

			while (!cancellationToken.IsCancellationRequested)
			{
				watch.Restart();
				this.Grab();
				watch.Stop();

				this.Iteration();

				if (watch.Elapsed < this.Interval)
				{
					Thread.Sleep(this.Interval - watch.Elapsed);
				}
			}
		}

		protected virtual void Iteration () { }

		public T GetSample ()
		{
			return (this.Samples.Dequeue());
		}

		protected abstract void Grab ();

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