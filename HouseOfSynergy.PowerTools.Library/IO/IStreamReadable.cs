using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public interface IStreamReadable<T>:
		IDisposable
		where T: struct, IComparable, IFormattable, IConvertible
	{
		bool CanRead { get; }
		bool HasData { get; }

		int Read (T [] buffer);
		int Read (T [] buffer, int offset, int count);
	}
}