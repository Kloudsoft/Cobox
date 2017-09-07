using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class ReadOnlyCells:
		ReadOnlyList<Cell>
	{
		public Grid Grid { get; private set; }

		public ReadOnlyCells (Grid grid, IList<Cell> list)
			: base(list, false, false)
		{
			this.Grid = grid;
		}
	}

	public sealed class ReadOnlyCells<T>:
		ReadOnlyList<Cell<T>>
	{
		public Grid<T> Grid { get; private set; }

		public ReadOnlyCells (Grid<T> grid, IList<Cell<T>> list)
			: base(list, false, false)
		{
			this.Grid = grid;
		}
	}
}