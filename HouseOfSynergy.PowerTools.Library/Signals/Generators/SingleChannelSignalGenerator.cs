using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Signals.Generators
{
	/// <summary>
	/// An abstract class representing a Ieee Float signal.
	/// </summary>
	public abstract class SingleChannelSignalGenerator:
		ISingleChannelSignalGenerator<float>
	{
		public const double TwoPi = (2D * Math.PI);

		protected double _Frequency = 01.00D;
		protected double _Amplitude = 01.00D;
		protected double _PhaseShift = 00.00D;

		public TimeSpan Time { get; private set; }
		public float SampleRate { get; private set; }
		public TimeSpan Interval { get; private set; }

		protected SingleChannelSignalGenerator (float sampleRate, double frequency, double amplitude, double phaseShift)
		{
			if (sampleRate <= 0) { throw (new ArgumentOutOfRangeException("sampleRate", "The argument [sampleRate] must be greater than zero.")); }
			if (frequency == 0) { throw (new ArgumentOutOfRangeException("frequency", "The argument [frequency] cannot be zero.")); }
			if (amplitude <= 0) { throw (new ArgumentOutOfRangeException("amplitude", "The argument [amplitude] must be between 0 and 1 inclusive.")); }

			this.SampleRate = sampleRate;
			this._Frequency = frequency;
			this._Amplitude = amplitude;
			this._PhaseShift = phaseShift;

			this.Interval = TimeSpan.FromSeconds(1D / this.SampleRate);
		}

		public bool CanRead { get { return (true); } }
		public bool HasData { get { return (true); } }
		public int SampleSizeInBits { get { return (this.SampleSizeInBytes * 8); } }
		public int SampleSizeInBytes { get { return ((int) (sizeof(float))); } }
		public int SampleSizePerSecondInBytes { get { return ((int) (this.SampleSizeInBytes * this.SampleRate)); } }
		public float PhaseShift { get { return ((float) this._PhaseShift); } private set { this._PhaseShift = value; } }
		public float Amplitude { get { return ((float) this._Amplitude); } private set { this._Amplitude = MiscellaneousUtilities.Between(-01.00D, +01.00D, value); } }
		public float Frequency { get { return ((float) this._Frequency); } private set { this._Frequency = Math.Max(double.Epsilon, value); /*MiscellaneousUtilities.Between(double.Epsilon, this.SampleRate, value);*/ } }

		public abstract double CalculateAmplitude (double time);

		public int Read (float [] buffer)
		{
			return (this.Read(buffer, 0, (buffer.Length / this.SampleSizeInBytes)));
		}

		public int Read (float [] buffer, int offset, int sampleCount)
		{
			var countSamplesRead = 0;
			var sampleRate = (double) this.SampleRate;

			if (buffer == null) { throw (new ArgumentNullException("buffer")); }

			for (int i = 0; i < sampleCount; i++)
			{
				double time = this.Time.Ticks / sampleRate;
				buffer [offset + i] = (float) this.CalculateAmplitude(time);

				countSamplesRead++;
				this.Time = TimeSpan.FromTicks(this.Time.Ticks + 1);
				if (this.Time.Ticks >= sampleRate) { this.Time = TimeSpan.Zero; }
			}

			return (countSamplesRead);
		}

		public void Dispose ()
		{
		}
	}
}