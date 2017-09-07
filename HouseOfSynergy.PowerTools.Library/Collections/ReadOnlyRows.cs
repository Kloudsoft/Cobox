using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class ReadOnlyRows:
		ReadOnlyList<Row>
	{
		public Grid Grid { get; private set; }

		public ReadOnlyRows (Grid grid, IList<Row> list)
			: base(list, false, false)
		{
			this.Grid = grid;
		}
	}

	public sealed class ReadOnlyRows<T>:
		ReadOnlyList<Row<T>>
	{
		public Grid<T> Grid { get; private set; }

		public ReadOnlyRows (Grid<T> grid, IList<Row<T>> list)
			: base(list, false, false)
		{
			this.Grid = grid;
		}
	}
}