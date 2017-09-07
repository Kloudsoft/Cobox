using System;
using System.Collections.Generic;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.IO;

namespace HouseOfSynergy.PowerTools.Library.Signals.Generators
{
	public interface ISingleChannelSignalGenerator<T>:
		ISignal<T>,
		ISignalSettings<T>,
		ISignalGenerator<T>,
		IStreamReadable<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
	}
}