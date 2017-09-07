using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.IO;

namespace HouseOfSynergy.PowerTools.Library.Signals.Streams
{
	public interface ISignalStream<T>:
		ISignal<T>,
		IStream<T>
		where T: struct, IComparable, IFormattable, IConvertible
	{
	}
}