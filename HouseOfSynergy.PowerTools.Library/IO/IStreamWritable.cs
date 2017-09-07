using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public interface IStreamWritable<T>:
		IDisposable
		where T: struct, IComparable, IFormattable, IConvertible
	{
		bool CanWrite { get; }

		int Write (T [] buffer);
		int Write (T [] buffer, int offset, int count);
	}
}