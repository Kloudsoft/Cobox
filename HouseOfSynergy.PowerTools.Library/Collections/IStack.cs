using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public interface IStack<T>
	{
		int Count { get; }

		T Peek ();
		T Push (T item);
		T Pop ();
	}
}