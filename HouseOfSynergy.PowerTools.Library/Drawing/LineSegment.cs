using System;
using System.Drawing;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public struct LineSegment:
		IEquatable<LineSegment>,
		ILineSegment,
		ILine
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private int _X1;
		private int _Y1;
		private int _X2;
		private int _Y2;

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public LineSegment (int x1, int y1, int x2, int y2) { this._X1 = x1; this._Y1 = y1; this._X2 = x2; this._Y2 = y2; }
		public LineSegment (float x1, float y1, float x2, float y2) : this((int) x1, (int) y1, (int) x2, (int) y2) { }
		public LineSegment (Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y) { }
		public LineSegment (PointF p1, PointF p2) : this(p1.X, p1.Y, p2.X, p2.Y) { }

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public int X1 { get { return (this._X1); } }
		public int Y1 { get { return (this._Y1); } }
		public int X2 { get { return (this._X2); } }
		public int Y2 { get { return (this._Y2); } }

		public Point Point1 { get { return (new Point(this._X1, this._Y1)); } }
		public Point Point2 { get { return (new Point(this._X2, this._Y2)); } }

		public int Width { get { return (this.X2 - this.X1); } }
		public int Height { get { return (this.Y2 - this.Y1); } }

		public int AbsoluteWidth { get { return (Math.Abs(this.Width)); } }
		public int AbsoluteHeight { get { return (Math.Abs(this.Height)); } }

		public float Slope { get { return (this.Height / this.Width); } }
		public float Length { get { return ((float) Math.Sqrt(Math.Pow(this.Width, 2) + Math.Pow(this.Height, 2))); } }

		public bool IsEmpty { get { return ((this.X1 == 0F) && (this.Y1 == 0F) && (this.X2 == 0F) && (this.Y2 == 0F)); } }
		public bool IsVertical { get { return (this.X1 == this.X2); } }
		public bool IsHorizontal { get { return (this.Y1 == this.Y2); } }
		public bool IsPoint { get { return ((this.X1 == this.X2) && (this.Y1 == this.Y2)); } }

		#endregion Properties.

		#region Operator Overloads.

		//====================================================================================================
		// Operator Overloads.
		//====================================================================================================

		public static bool operator == (LineSegment x, LineSegment y) { return (x.Equals(y)); }
		public static bool operator != (LineSegment x, LineSegment y) { return (!x.Equals(y)); }

		#endregion Operator Overloads.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public bool Contains (int x, int y) { throw (new NotImplementedException()); }
		public bool Contains (Point point) { throw (new NotImplementedException()); }
		public bool Contains (PointF point) { throw (new NotImplementedException()); }
		public bool Contains (LineSegmentF other) { throw (new NotImplementedException()); }
		public bool Intersects (LineSegmentF other) { throw (new NotImplementedException()); }
		public bool ParallelTo (LineSegmentF other) { return (((other.X2 - this.X2) == (other.X1 - this.X1)) || ((other.Y2 - this.Y2) == (other.Y1 - this.Y1))); }
		public LineSegmentF ToLineF () { return (new LineSegmentF(this._X1, this._Y1, this._X2, this._Y2)); }
		public Size ToSize () { return (new Size(this.Width, this.Height)); }
		public Size ToAbsoluteSize () { return (new Size(this.AbsoluteWidth, this.AbsoluteHeight)); }
		public Rectangle ToRectangle () { return (Rectangle.FromLTRB(this.X1, this.Y1, this.X2, this.Y2)); }
		public SizeF ToSizeF () { return (new SizeF(this.Width, this.Height)); }
		public SizeF ToAbsoluteSizeF () { return (new SizeF(this.AbsoluteWidth, this.AbsoluteHeight)); }
		public RectangleF ToRectangleF () { return (RectangleF.FromLTRB(this.X1, this.Y1, this.X2, this.Y2)); }

		#endregion Methods.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		public override bool Equals (object obj)
		{
			if (object.ReferenceEquals(obj, null))
			{
				return (false);
			}
			else if (object.ReferenceEquals(this, obj))
			{
				return (true);
			}
			else
			{
				if (obj is LineSegment)
				{
					return (this.Equals((LineSegment) obj));
				}
				else
				{
					return (false);
				}
			}
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			unchecked // Overflow is fine, just wrap.
			{
				hash = 17;
				hash = hash * 23 + this.X1.GetHashCode();
				hash = hash * 23 + this.Y1.GetHashCode();
				hash = hash * 23 + this.X2.GetHashCode();
				hash = hash * 23 + this.Y2.GetHashCode();
			}

			return (hash);
		}

		public override string ToString () { return ("{X1=" + this.X1.ToString() + ", Y1=" + this.Y1.ToString() + ", X2=" + this.X2.ToString() + ", Y2=" + this.Y2.ToString() + "}"); }

		#endregion Base Overrides.

		#region Interface Implementation: System.IEquatable<T>.

		//====================================================================================================
		// Interface Implementation: System.IEquatable<T>.
		//====================================================================================================

		bool IEquatable<ILine>.Equals (ILine other) { return (this.Slope == other.Slope); }
		public bool Equals (LineSegment other) { return ((this.X1 == other.X1) && (this.Y1 == other.Y1) && (this.X2 == other.X2) && (this.Y2 == other.Y2)); }
		bool IEquatable<ILineSegment>.Equals (ILineSegment other) { return ((this.X1 == other.X1) && (this.Y1 == other.Y1) && (this.X2 == other.X2) && (this.Y2 == other.Y2)); }

		#endregion Interface Implementation: System.IEquatable<T>.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static readonly LineSegment Empty = new LineSegment();

		#endregion Static.
	}
}