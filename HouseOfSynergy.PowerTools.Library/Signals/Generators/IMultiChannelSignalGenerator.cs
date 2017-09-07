using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace HouseOfSynergy.PowerTools.Library.Signals.Generators
{
	public interface IMultiChannelSignalGenerator<T>:
		ISignalGenerator<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
		ReadOnlyCollection<ISignalGenerator<T>> SignalGenerators { get; }

		void Clear ();
		bool Remove (SingleChannelSignalGenerator generator);
		void Add (double frequency, double amplitude, double phaseShift);
		void Add (ISingleChannelSignalGenerator<T> generator);
		void Add (IEnumerable<ISingleChannelSignalGenerator<T>> generators);
	}
}