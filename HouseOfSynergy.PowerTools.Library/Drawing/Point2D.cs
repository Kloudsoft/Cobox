using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public class Point2D<T>:
		Point<T>,
		IPoint<T>,
		IPoint2D<T>
		where T: struct
	{
		public T X { get; protected set; }
		public T Y { get; protected set; }

		public Point2D (T x, T y) : this() { this.Initialize(x, y); }
		protected Point2D (PointType type) : base(type) { this.Initialize(); }
		public Point2D () : base(PointType.TwoDimentional) { this.Initialize(); }

		private void Initialize (T x, T y) { this.X = x; this.Y = y; }
		private void Initialize () { this.X = default(T); this.Y = default(T); }

		public override T Distance { get { return (Point<T>.OperatorAdd(Point<T>.OperatorMultiply(this.X, this.X), Point<T>.OperatorMultiply(this.Y, this.Y))); } }

		public Point2D<T> Clone () { return (new Point2D<T>(this.X, this.Y)); }
	}
}