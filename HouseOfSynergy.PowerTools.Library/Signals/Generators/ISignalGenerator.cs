using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.IO;

namespace HouseOfSynergy.PowerTools.Library.Signals.Generators
{
	public interface ISignalGenerator<T>:
		ISignal<T>,
		ISignalSettings<T>,
		IStreamReadable<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
		TimeSpan Time { get; }
		double CalculateAmplitude (double time);
	}
}