using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Signals.Generators
{
	public sealed class SineSignalGenerator:
		SingleChannelSignalGenerator
	{
		public SineSignalGenerator (float sampleRate, double frequency, double amplitude, double phaseShift)
			: base(sampleRate, frequency, amplitude, phaseShift)
		{
		}

		public override double CalculateAmplitude (double time)
		{
			return (Math.Sin((time * this._Frequency * SingleChannelSignalGenerator.TwoPi) + this._PhaseShift) * this._Amplitude);
		}
	}
}