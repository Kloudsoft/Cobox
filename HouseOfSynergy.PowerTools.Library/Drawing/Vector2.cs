using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public struct Vector2:
		IEquatable<Vector2>
	{
		#region Fields.

		//====================================================================================================
		// Fields.
		//====================================================================================================

		public readonly float X;
		public readonly float Y;
		public readonly float Distance;

		#endregion Fields.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public Vector2 (Vector2 vector) : this(vector.X, vector.Y) { }
		public Vector2 (Vector3 vector) : this(vector.X, vector.Y) { }
		public Vector2 (Vector4 vector) : this(vector.X, vector.Y) { }
		public Vector2 (float x, float y) { this.X = x; this.Y = y; this.Distance = x * y; }

		#endregion Constructors.

		#region Operator Overloads.

		//====================================================================================================
		// Operator Overloads.
		//====================================================================================================

		public static Vector2 operator + (Vector2 v1, int v2) { return (new Vector2(v1.X + v2, v1.Y + v2)); }
		public static Vector2 operator + (Vector2 v1, float v2) { return (new Vector2(v1.X + v2, v1.Y + v2)); }
		public static Vector2 operator + (Vector2 v1, Vector2 v2) { return (new Vector2(v1.X + v2.X, v1.Y + v2.Y)); }
		public static Vector2 operator + (Vector2 v1, Vector3 v2) { return (new Vector2(v1.X + v2.X, v1.Y + v2.Y)); }
		public static Vector2 operator + (Vector2 v1, Vector4 v2) { return (new Vector2(v1.X + v2.X, v1.Y + v2.Y)); }
		public static Vector2 operator - (Vector2 v1, int v2) { return (new Vector2(v1.X - v2, v1.Y - v2)); }
		public static Vector2 operator - (Vector2 v1, float v2) { return (new Vector2(v1.X - v2, v1.Y - v2)); }
		public static Vector2 operator - (Vector2 v1, Vector2 v2) { return (new Vector2(v1.X - v2.X, v1.Y - v2.Y)); }
		public static Vector2 operator - (Vector2 v1, Vector3 v2) { return (new Vector2(v1.X - v2.X, v1.Y - v2.Y)); }
		public static Vector2 operator - (Vector2 v1, Vector4 v2) { return (new Vector2(v1.X - v2.X, v1.Y - v2.Y)); }
		public static Vector2 operator * (Vector2 v1, int v2) { return (new Vector2(v1.X * v2, v1.Y * v2)); }
		public static Vector2 operator * (Vector2 v1, float v2) { return (new Vector2(v1.X * v2, v1.Y * v2)); }
		public static Vector2 operator * (Vector2 v1, Vector2 v2) { return (new Vector2(v1.X * v2.X, v1.Y * v2.Y)); }
		public static Vector2 operator * (Vector2 v1, Vector3 v2) { return (new Vector2(v1.X * v2.X, v1.Y * v2.Y)); }
		public static Vector2 operator * (Vector2 v1, Vector4 v2) { return (new Vector2(v1.X * v2.X, v1.Y * v2.Y)); }
		public static Vector2 operator / (Vector2 v1, int v2) { return (new Vector2(v1.X / v2, v1.Y / v2)); }
		public static Vector2 operator / (Vector2 v1, float v2) { return (new Vector2(v1.X / v2, v1.Y / v2)); }
		public static Vector2 operator / (Vector2 v1, Vector2 v2) { return (new Vector2(v1.X / v2.X, v1.Y / v2.Y)); }
		public static Vector2 operator / (Vector2 v1, Vector3 v2) { return (new Vector2(v1.X / v2.X, v1.Y / v2.Y)); }
		public static Vector2 operator / (Vector2 v1, Vector4 v2) { return (new Vector2(v1.X / v2.X, v1.Y / v2.Y)); }

		public static bool operator != (Vector2 v1, int v2) { return (!(v1 == v2)); }
		public static bool operator != (Vector2 v1, float v2) { return (!(v1 == v2)); }
		public static bool operator != (Vector2 v1, Vector2 v2) { return (!(v1 == v2)); }
		public static bool operator == (Vector2 v1, int v2) { return ((v1.X == v2) && (v1.Y == v2)); }
		public static bool operator == (Vector2 v1, float v2) { return ((v1.X == v2) && (v1.Y == v2)); }
		public static bool operator == (Vector2 v1, Vector2 v2) { return ((v1.X == v2.X) && (v1.Y == v2.Y)); }

		public static Vector2 operator + (Vector2 v) { return (new Vector2(v)); }
		public static Vector2 operator - (Vector2 v) { return (new Vector2(-v.X, -v.Y)); }
		public static Vector2 operator ++ (Vector2 v1) { return (new Vector2(v1.X + 1, v1.Y + 1)); }
		public static Vector2 operator -- (Vector2 v1) { return (new Vector2(v1.X - 1, v1.Y - 1)); }

		public static explicit operator Vector2 (Vector3 v) { return (new Vector2(v)); }
		public static explicit operator Vector2 (Vector4 v) { return (new Vector2(v)); }

		#endregion Operator Overloads.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public Vector2 Squared { get { return (new Vector2(this.X * this.X, this.Y * this.Y)); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public Vector3 ToVector3 () { return (new Vector3(this)); }
		public Vector4 ToVector4 () { return (new Vector4(this)); }

		#endregion Methods.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		public override bool Equals (object obj)
		{
			return ((obj != null) && (this.Equals((Vector2) obj)));
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			unchecked
			{
				hash = 17;
				hash = hash * 23 + this.X.GetHashCode();
				hash = hash * 23 + this.Y.GetHashCode();
			}

			return (hash);
		}

		public override string ToString ()
		{
			return (string.Format(@"Vector4 [X={0}, Y={1}]", this.X, this.Y));
		}

		#endregion Base Overrides.

		#region Interface: IEquatable<Vector4>.

		//====================================================================================================
		// Interface: IEquatable<Vector4>.
		//====================================================================================================

		public bool Equals (Vector2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}

		#endregion Interface: IEquatable<Vector4>.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static readonly Vector2 Empty = new Vector2();

		static Vector2 ()
		{
		}

		#endregion Static.
	}
}