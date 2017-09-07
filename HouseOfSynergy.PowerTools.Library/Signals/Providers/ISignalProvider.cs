using System;
using System.Collections.Generic;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.IO;

namespace HouseOfSynergy.PowerTools.Library.Signals.Providers
{
	public interface ISignalProvider<T>:
		ISignal<T>,
		IStreamReadable<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
		TimeSpan Time { get; }
	}
}