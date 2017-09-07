using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class ReadOnlyRow
	{
		public int Index { get; private set; }
		public Grid Grid { get; private set; }
		public Cells _Cells { get; private set; }
		public ReadOnlyCells Cells { get; private set; }

		public ReadOnlyRow (Grid grid, int index) : this(grid) { this.Index = index; }
		public ReadOnlyRow (Grid grid, int index, IEnumerable<Cell> cells) : this(grid, index) { this._Cells.AddRange(cells); }
		public ReadOnlyRow (Grid grid) { this.Index = 0; this.Grid = grid; this._Cells = new Cells(grid); this.Cells = new ReadOnlyCells(grid, this._Cells); }
	}

	public sealed class ReadOnlyRow<T>
	{
		public int Index { get; private set; }
		public Grid<T> Grid { get; private set; }
		public Cells<T> _Cells { get; private set; }
		public ReadOnlyCells<T> Cells { get; private set; }

		public ReadOnlyRow (Grid<T> grid, int index) : this(grid) { this.Index = index; }
		public ReadOnlyRow (Grid<T> grid, int index, IEnumerable<Cell<T>> cells) : this(grid, index) { this._Cells.AddRange(cells); }
		public ReadOnlyRow (Grid<T> grid) { this.Index = 0; this.Grid = grid; this._Cells = new Cells<T>(grid); this.Cells = new ReadOnlyCells<T>(grid, this._Cells); }
	}
}