using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Signals
{
	public interface ISignalSettings<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
		/// <summary>
		/// Gets a value between 0 and 1 representing an amplitude limiter percentage for a given signal.
		/// </summary>
		T Amplitude { get; }

		/// <summary>
		/// Gets a value representing the sampling frequency in seconds (samples per second) for a given signal.
		/// </summary>
		T Frequency { get; }

		/// <summary>
		/// Gets a value representing the phase shift in seconds for a given signal.
		/// </summary>
		T PhaseShift { get; }
	}
}