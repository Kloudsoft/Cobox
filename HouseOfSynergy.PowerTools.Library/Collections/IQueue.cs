using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public interface IQueue<T>
	{
		int Count { get; }

		T Peek ();
		T Enqueue (T item);
		T Dequeue ();
	}
}