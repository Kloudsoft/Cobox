using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public class Cells:
		List<Cell>
	{
		public Grid Grid { get; private set; }

		public Cells (Grid grid)
		{
			this.Grid = grid;
		}
	}

	public class Cells<T>:
		List<Cell<T>>
	{
		public Grid<T> Grid { get; private set; }

		public Cells (Grid<T> grid)
		{
			this.Grid = grid;
		}
	}
}