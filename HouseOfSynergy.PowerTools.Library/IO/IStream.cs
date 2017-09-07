using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public interface IStream<T>:
		IStreamReadable<T>,
		IStreamWritable<T>,
		IDisposable
		where T: struct, IComparable, IFormattable, IConvertible
	{
	}
}
