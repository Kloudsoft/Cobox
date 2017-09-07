using System;
using System.Drawing;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public struct LineSegmentF:
		IEquatable<LineSegmentF>,
		ILineSegmentF,
		ILine
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private float _X1;
		private float _Y1;
		private float _X2;
		private float _Y2;

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public LineSegmentF (float x1, float y1, float x2, float y2) { this._X1 = x1; this._Y1 = y1; this._X2 = x2; this._Y2 = y2; }
		public LineSegmentF (int x1, int y1, int x2, int y2) : this((float) x1, (float) y1, (float) x2, (float) y2) { }
		public LineSegmentF (Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y) { }
		public LineSegmentF (PointF p1, PointF p2) : this(p1.X, p1.Y, p2.X, p2.Y) { }

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public float X1 { get { return (this._X1); } }
		public float Y1 { get { return (this._Y1); } }
		public float X2 { get { return (this._X2); } }
		public float Y2 { get { return (this._Y2); } }

		public PointF Point1 { get { return (new PointF(this._X1, this._Y1)); } }
		public PointF Point2 { get { return (new PointF(this._X2, this._Y2)); } }

		public float Width { get { return (this.X2 - this.X1); } }
		public float Height { get { return (this.Y2 - this.Y1); } }

		public float AbsoluteWidth { get { return (Math.Abs(this.Width)); } }
		public float AbsoluteHeight { get { return (Math.Abs(this.Height)); } }

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

		public static bool operator == (LineSegmentF x, LineSegmentF y) { return (x.Equals(y)); }
		public static bool operator != (LineSegmentF x, LineSegmentF y) { return (!x.Equals(y)); }

		#endregion Operator Overloads.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public bool Contains (int x, int y) { return (this.Contains(new PointF(x, y))); }
		public bool Contains (float x, float y) { return (this.Contains(new PointF(x, y))); }
		public bool Contains (Point point) { return (this.Contains(new PointF(point.X, point.Y))); }
		public bool Contains (PointF point) { throw (new NotImplementedException()); }
		public bool Contains (LineSegmentF other) { return (this.Contains(this.Point1) && this.Contains(this.Point2)); }

		/// <summary>
		/// Calculates a intersection point between [this] and [other].
		/// </summary>
		/// <param name="other"></param>
		/// <returns>Returns the intersection of two line segments, new Point(int.MinValue, int.MinValue) otherwise.</returns>
		public PointF Intersects (LineSegmentF other)
		{
			double newX = 0D;
			double posAB = 0D;
			double distAB = 0D;
			double theCos = 0D;
			double theSin = 0D;
			double ax = 0D, ay = 0D;
			double bx = 0D, by = 0D;
			double cx = 0D, cy = 0D;
			double dx = 0D, dy = 0D;
			PointF point = new PointF();

			point = new PointF(float.MinValue, float.MinValue);

			//  Fail if either line segment is zero-length.
			if ((this._X1 == this._X2) || (this._Y1 == this._Y2) || (other._X1 == other._X2) || (other._Y1 == other._Y2))
			{
			}
			else
			{
				// Detect early if the lines share a common end-point.
				if ((this._X1 == other._X1) || (this._Y1 == other._Y1))
				{
					point = new PointF(this._X1, this.Y1);
				}
				else if ((this._X1 == other._X2) || (this._Y1 == other._Y2))
				{
					point = new PointF(this._X1, this.Y1);
				}
				else if ((this._X2 == other._X1) || (this._Y2 == other._Y1))
				{
					point = new PointF(this._X2, this.Y2);
				}
				else if ((this._X2 == other._X2) || (this._Y2 == other._Y2))
				{
					point = new PointF(this._X2, this.Y2);
				}
				else
				{
					// Detect early if the lines are co-linier.
					if ((this.ToRectangleF().IntersectsWith(other.ToRectangleF())) && (Math.Abs(this.Slope - other.Slope) <= float.Epsilon))
					{
					}
					else
					{
						//  (1) Translate the system so that point A is on the origin.
						bx -= ax;
						by -= ay;
						cx -= ax;
						cy -= ay;
						dx -= ax;
						dy -= ay;

						//  Discover the length of segment A-B.
						distAB = Math.Sqrt((bx * bx) + (by * by));

						//  (2) Rotate the system so that point B is on the positive X axis.
						theCos = bx / distAB;
						theSin = by / distAB;
						newX = (cx * theCos) + (cy * theSin);
						cy = (cy * theCos) - (cx * theSin);
						cx = newX;
						newX = (dx * theCos) + (dy * theSin);
						dy = (dy * theCos) - (dx * theSin);
						dx = newX;

						if (((cy < 0D) && (dy < 0D)) || ((cy >= 0D) && (dy >= 0D)))
						{
							//  Fail if segment C-D doesn't cross line A-B.
						}
						else
						{
							// (3) Discover the position of the intersection point along line A-B.
							posAB = dx + (cx - dx) * dy / (dy - cy);

							if ((posAB < 0D) || (posAB > distAB))
							{
								// Fail if segment C-D crosses line A-B outside of segment A-B.
							}
							else
							{
								// (4) Apply the discovered position to line A-B in the original coordinate system.
								point = new PointF
								(
									(float) (ax + (posAB * theCos)),
									(float) (ay + (posAB * theSin))
								);
							}
						}
					}
				}
			}

			return (point);
		}

		public bool Intersects (LineSegmentF other, out PointF point)
		{
			PointF p = new PointF();

			p = this.Intersects(other);
			point = new PointF(float.MinValue, float.MinValue);

			if ((p.X == float.MinValue) && (p.Y == float.MinValue))
			{
				return (false);
			}
			else
			{
				point = p;

				return (true);
			}
		}

		public bool ParallelTo (LineSegmentF other) { return (((other.X2 - this.X2) == (other.X1 - this.X1)) || ((other.Y2 - this.Y2) == (other.Y1 - this.Y1))); }
		public LineSegment ToLine () { return (new LineSegment((int) this._X1, (int) this._Y1, (int) this._X2, (int) this._Y2)); }
		public Size ToSize () { return (new Size((int) this.Width, (int) this.Height)); }
		public Size ToAbsoluteSize () { return (new Size((int) this.AbsoluteWidth, (int) this.AbsoluteHeight)); }
		public Rectangle ToRectangle () { return (Rectangle.FromLTRB((int) this.X1, (int) this.Y1, (int) this.X2, (int) this.Y2)); }
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
				if (obj is LineSegmentF)
				{
					return (this.Equals((LineSegmentF) obj));
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
		public bool Equals (LineSegmentF other) { return ((this.X1 == other.X1) && (this.Y1 == other.Y1) && (this.X2 == other.X2) && (this.Y2 == other.Y2)); }
		bool IEquatable<ILineSegmentF>.Equals (ILineSegmentF other) { return ((this.X1 == other.X1) && (this.Y1 == other.Y1) && (this.X2 == other.X2) && (this.Y2 == other.Y2)); }

		#endregion Interface Implementation: System.IEquatable<T>.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static readonly LineSegmentF Empty = new LineSegmentF();

		#endregion Static.
	}
}