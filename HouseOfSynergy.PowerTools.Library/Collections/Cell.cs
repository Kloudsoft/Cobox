using System.Drawing;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public class Cell
	{
		public object Entity { get; set; }
		public int X { get; private set; }
		public int Y { get; private set; }
		public Row Row { get; private set; }
		public Grid Grid { get; private set; }
		public Column Column { get; private set; }
		public Point Position { get; private set; }

		public Cell (Grid grid, Column column, Row row, int x, int y)
		{
			this.X = x;
			this.Y = y;
			this.Row = row;
			this.Grid = grid;
			this.Column = column;
			this.Position = new Point(x, y);
		}

		public Cell (Grid grid, Column column, Row row, int x, int y, object entity)
			: this(grid, column, row, x, y)
		{
			this.Entity = entity;
		}

		public override string ToString ()
		{
			return ("{" + this.X.ToString() + ":" + this.Y.ToString() + "}");
		}
	}

	public class Cell<T>
	{
		public T Entity { get; set; }
		public int X { get; private set; }
		public int Y { get; private set; }
		public Row<T> Row { get; private set; }
		public Grid<T> Grid { get; private set; }
		public Point Position { get; private set; }
		public Column<T> Column { get; private set; }

		public Cell (Grid<T> grid, Column<T> column, Row<T> row, int x, int y)
		{
			this.X = x;
			this.Y = y;
			this.Row = row;
			this.Grid = grid;
			this.Column = column;
			this.Position = new Point(x, y);
		}

		public Cell (Grid<T> grid, Column<T> column, Row<T> row, int x, int y, T entity)
			: this(grid, column, row, x, y)
		{
			this.Entity = entity;
		}

		public override string ToString ()
		{
			return ("{" + this.X.ToString() + ":" + this.Y.ToString() + "}");
		}
	}
}