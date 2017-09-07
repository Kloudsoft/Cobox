using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Signals.Generators
{
	public sealed class MultiChannelSignalGenerator:
		ISignalGenerator<float>
	{
		public TimeSpan Time { get; private set; }
		public float SampleRate { get; private set; }
		public TimeSpan Interval { get; private set; }

		private List<ISignalGenerator<float>> _SignalGenerators { get; set; }
		public ReadOnlyCollection<ISignalGenerator<float>> SignalGenerators { get; set; }

		public MultiChannelSignalGenerator (float sampleRate)
		{
			this._SignalGenerators = new List<ISignalGenerator<float>>();
			this.SignalGenerators = new ReadOnlyCollection<ISignalGenerator<float>>(this._SignalGenerators);

			this.SampleRate = sampleRate;
			this.Interval = TimeSpan.FromSeconds(1D / this.SampleRate);
		}

		public MultiChannelSignalGenerator (float sampleRate, IEnumerable<ISignalGenerator<float>> generators)
			: this(sampleRate)
		{
			if (generators == null) { throw (new ArgumentNullException("generators")); }
			if (generators.Any(generator => generator == null)) { throw (new ArgumentException("The argument [generators] must not contain null elements.", "generators")); }
			if (generators.Any(generator => generator.SampleRate != sampleRate)) { throw (new ArgumentException("The argument [generators] must contain elements with the same sample rate as the signal aggregator.", "generators")); }

			this._SignalGenerators.AddRange(generators);
		}

		public bool CanRead { get { return (true); } }
		public bool HasData { get { return (true); } }
		public float Amplitude { get { return (float.NaN); } }
		public float Frequency { get { return (float.NaN); } }
		public float PhaseShift { get { return (float.NaN); } }
		public int SampleSizeInBits { get { return (this.SampleSizeInBytes * 8); } }
		public int SampleSizeInBytes { get { return ((int) (sizeof(float))); } }
		public int SampleSizePerSecondInBytes { get { return ((int) (this.SampleSizeInBytes * this.SampleRate)); } }

		public double CalculateAmplitude (double time)
		{
			double amplitude = 0;

			if (this._SignalGenerators.Count == 1)
			{
				amplitude += this._SignalGenerators [0].CalculateAmplitude(time);
			}
			else if (this._SignalGenerators.Count > 1)
			{
				for (int i = 0; i < this._SignalGenerators.Count; i++)
				{
					amplitude += this._SignalGenerators [i].CalculateAmplitude(time);
				}

				amplitude /= this._SignalGenerators.Count;
			}

			return (amplitude);
		}

		public int Read (float [] buffer)
		{
			return (this.Read(buffer, 0, buffer.Length));
		}

		public int Read (float [] buffer, int offset, int sampleCount)
		{
			var countSamplesRead = 0;
			var sampleRate = (double) this.SampleRate;

			if (buffer == null) { throw (new ArgumentNullException("buffer")); }

			for (int i = 0; i < sampleCount; i++)
			{
				var time = this.Time.Ticks / sampleRate;
				buffer [offset + i] = (float) this.CalculateAmplitude(time);

				countSamplesRead++;
				this.Time = TimeSpan.FromTicks(this.Time.Ticks + 1);
				if (this.Time.Ticks >= sampleRate) { this.Time = TimeSpan.Zero; }
			}

			return (countSamplesRead);
		}

		public void Clear ()
		{
			this._SignalGenerators.Clear();
		}

		public bool Remove (ISingleChannelSignalGenerator<float> generator)
		{
			return (this._SignalGenerators.Remove(generator));
		}

		public void Add (double frequency, double amplitude, double phaseShift)
		{
			this._SignalGenerators.Add(new SineSignalGenerator(this.SampleRate, frequency, amplitude, phaseShift));
		}

		public void Add (ISingleChannelSignalGenerator<float> generator)
		{
			if (generator.SampleRate != this.SampleRate) { throw (new ArgumentException("The argument [generator] must have the same sample rate as the signal aggregator.", "generator")); }

			this._SignalGenerators.Add(generator);
		}

		public void Add (IEnumerable<ISingleChannelSignalGenerator<float>> generators)
		{
			if (generators == null) { throw (new ArgumentNullException("generators")); }
			if (generators.Any(generator => generator == null)) { throw (new ArgumentException("The argument [generators] must not contain null elements.", "generators")); }
			if (generators.Any(generator => generator.SampleRate != this.SampleRate)) { throw (new ArgumentException("The argument [generators] must contain elements with the same sample rate as the signal aggregator.", "generators")); }

			this._SignalGenerators.AddRange(generators);
		}

		public void Dispose ()
		{
		}
	}
}