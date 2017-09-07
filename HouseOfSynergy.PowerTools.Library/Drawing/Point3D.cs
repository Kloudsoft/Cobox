using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public sealed class Point3D<T>:
		Point2D<T>,
		IPoint<T>,
		IPoint2D<T>,
		IPoint3D<T>
		where T: struct
	{
		public T Z { get; private set; }

		public Point3D (T x, T y) : this() { this.Initialize(x, y, default(T)); }
		public Point3D (T x, T y, T z) : this(x, y) { this.Initialize(x, y, z); }
		public Point3D () : base(PointType.ThreeDimensional) { this.Initialize(); }
		public Point3D (Point2D<T> point2D, T z) : this() { this.Initialize(point2D.X, point2D.Y, z); }
		//protected Point3D (PointType type, int dimensionCount) : base(type) { this.X = default(T); this.Y = default(T); }

		private void Initialize (T x, T y, T z) { this.X = x; this.Y = y; this.Z = z; }
		private void Initialize () { this.X = default(T); this.Y = default(T); this.Z = default(T); }

		public override T Distance { get { return (Point<T>.OperatorAdd(Point<T>.OperatorMultiply(this.X, this.X), Point<T>.OperatorMultiply(this.Y, this.Y))); } }

		public new Point3D<T> Clone () { return (new Point3D<T>(this.X, this.Y, this.Z)); }
	}
}