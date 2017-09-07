using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class Columns:
		List<Column>
	{
		public Grid Grid { get; private set; }

		public Columns (Grid grid)
		{
			this.Grid = grid;
		}
	}

	public sealed class Columns<T>:
		List<Column<T>>
	{
		public Grid<T> Grid { get; private set; }

		public Columns (Grid<T> grid)
		{
			this.Grid = grid;
		}
	}
}