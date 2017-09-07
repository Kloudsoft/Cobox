using System.Drawing;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public class Grid
	{
		private Rows _Rows { get; set; }
		private Cells _AllCells { get; set; }
		private Cell [,] _Cells { get; set; }
		private Columns _Columns { get; set; }

		public Size Size { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public ReadOnlyRows Rows { get; private set; }
		public ReadOnlyColumns Columns { get; private set; }
		public ReadOnlyCells AllCells { get; private set; }

		public Grid (int width, int height)
		{
			Cell cell = null;

			this.Width = width;
			this.Height = height;
			this._Rows = new Rows(this);
			this._AllCells = new Cells(this);
			this._Columns = new Columns(this);
			this.Size = new Size(width, height);
			this._Cells = new Cell [width, height];

			for (int i = 0; i < height; i++) { this._Rows.Add(new Row(this, i)); }
			for (int i = 0; i < width; i++) { this._Columns.Add(new Column(this, i)); }

			this.Rows = new ReadOnlyRows(this, this._Rows);
			this.Columns = new ReadOnlyColumns(this, this._Columns);

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					cell = new Cell(this, this.Columns [x], this.Rows [y], x, y);

					this._AllCells.Add(cell);
					this._Cells [x, y] = cell;

					this.Rows [y].Cells.Add(cell);
					this.Columns [x].Cells.Add(cell);
				}
			}

			this.AllCells = new ReadOnlyCells(this, this._AllCells);
		}

		public Cell this [int x, int y] { get { return (this._Cells [x, y]); } }
	}

	public class Grid<T>
	{
		private Rows<T> _Rows { get; set; }
		private Cell<T> [,] Cells { get; set; }
		private Cells<T> _AllCells { get; set; }
		private Columns<T> _Columns { get; set; }

		public Size Size { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public ReadOnlyRows<T> Rows { get; private set; }
		public ReadOnlyCells<T> AllCells { get; private set; }
		public ReadOnlyColumns<T> Columns { get; private set; }

		public Grid (int width, int height)
		{
			Cell<T> cell = null;

			this.Width = width;
			this.Height = height;
			this._Rows = new Rows<T>(this);
			this._AllCells = new Cells<T>(this);
			this._Columns = new Columns<T>(this);
			this.Size = new Size(width, height);
			this.Cells = new Cell<T> [width, height];

			for (int i = 0; i < height; i++) { this._Rows.Add(new Row<T>(this, i)); }
			for (int i = 0; i < width; i++) { this._Columns.Add(new Column<T>(this, i)); }

			this.Rows = new ReadOnlyRows<T>(this, this._Rows);
			this.Columns = new ReadOnlyColumns<T>(this, this._Columns);

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					cell = new Cell<T>(this, this.Columns [x], this.Rows [y], x, y);

					this._AllCells.Add(cell);
					this.Cells [x, y] = cell;

					this.Rows [y].Cells.Add(cell);
					this.Columns [x].Cells.Add(cell);
				}
			}

			this.AllCells = new ReadOnlyCells<T>(this, this._AllCells);
		}

		public Cell<T> this [int x, int y] { get { return (this.Cells [x, y]); } }
	}
}