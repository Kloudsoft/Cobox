using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public class Column
	{
		public int Index { get; private set; }
		public Grid Grid { get; private set; }
		public Cells Cells { get; private set; }

		public Column (Grid grid, int index) : this(grid) { this.Index = index; }
		public Column (Grid grid) { this.Index = 0; this.Grid = grid; this.Cells = new Cells(grid); }
		public Column (Grid grid, int index, IEnumerable<Cell> cells) : this(grid, index) { this.Cells.AddRange(cells); }
	}

	public class Column<T>
	{
		public int Index { get; private set; }
		public Grid<T> Grid { get; private set; }
		public Cells<T> Cells { get; private set; }

		public Column (Grid<T> grid, int index) : this(grid) { this.Index = index; }
		public Column (Grid<T> grid) { this.Index = 0; this.Grid = grid; this.Cells = new Cells<T>(grid); }
		public Column (Grid<T> grid, int index, IEnumerable<Cell<T>> cells) : this(grid, index) { this.Cells.AddRange(cells); }
	}
}