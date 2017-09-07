using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class Rows:
		List<Row>
	{
		public Grid Grid { get; private set; }

		public Rows (Grid grid)
		{
			this.Grid = grid;
		}
	}

	public sealed class Rows<T>:
		List<Row<T>>
	{
		public Grid<T> Grid { get; private set; }

		public Rows (Grid<T> grid)
		{
			this.Grid = grid;
		}
	}
}