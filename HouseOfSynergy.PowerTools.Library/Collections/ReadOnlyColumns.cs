using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class ReadOnlyColumns:
		ReadOnlyList<Column>
	{
		public Grid Grid { get; private set; }

		public ReadOnlyColumns (Grid grid, IList<Column> list)
			: base(list, false, false)
		{
			this.Grid = grid;
		}
	}

	public sealed class ReadOnlyColumns<T>:
		ReadOnlyList<Column<T>>
	{
		public Grid<T> Grid { get; private set; }

		public ReadOnlyColumns (Grid<T> grid, IList<Column<T>> list)
			: base(list, false, false)
		{
			this.Grid = grid;
		}
	}
}