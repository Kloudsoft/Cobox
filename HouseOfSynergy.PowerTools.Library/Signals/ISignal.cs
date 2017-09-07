using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Signals
{
	public interface ISignal<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
		/// <summary>
		/// Tha sample rate (samples per second) for the given signal.
		/// </summary>
		T SampleRate { get; }

		/// <summary>
		/// The time interval between samples (1 / SampleRate).
		/// </summary>
		TimeSpan Interval { get; }

		/// <summary>
		/// The sample size in bits per sample.
		/// </summary>
		int SampleSizeInBits { get; }

		/// <summary>
		/// The sample size in bytes per sample.
		/// </summary>
		int SampleSizeInBytes { get; }

		/// <summary>
		/// The sample size in bytes per second.
		/// </summary>
		int SampleSizePerSecondInBytes { get; }
	}
}