using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public struct Vector4:
		IEquatable<Vector4>
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public readonly float X;
		public readonly float Y;
		public readonly float Z;
		public readonly float W;
		public readonly float Distance;
		//public float Distance { get { return (this.X * this.Y * this.Z * this.W); } }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public Vector4 (Vector2 vector) : this(vector.X, vector.Y, 0, 0) { }
		public Vector4 (Vector3 vector) : this(vector.X, vector.Y, vector.Z, 0) { }
		public Vector4 (Vector4 vector) : this(vector.X, vector.Y, vector.Z, vector.W) { }
		public Vector4 (float x, float y, float z, float w) { this.X = x; this.Y = y; this.Z = z; this.W = w; this.Distance = x * y * z * w; }

		#endregion Constructors.

		#region Operator Overloads.

		//====================================================================================================
		// Operator Overloads.
		//====================================================================================================

		public static Vector4 operator + (Vector4 v1, int v2) { return (new Vector4(v1.X + v2, v1.Y + v2, v1.Z + v2, v1.W + v2)); }
		public static Vector4 operator + (Vector4 v1, float v2) { return (new Vector4(v1.X + v2, v1.Y + v2, v1.Z + v2, v1.W + v2)); }
		public static Vector4 operator + (Vector4 v1, Vector2 v2) { return (new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z, v1.W)); }
		public static Vector4 operator + (Vector4 v1, Vector3 v2) { return (new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W)); }
		public static Vector4 operator + (Vector4 v1, Vector4 v2) { return (new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W)); }
		public static Vector4 operator - (Vector4 v1, int v2) { return (new Vector4(v1.X - v2, v1.Y - v2, v1.Z - v2, v1.W - v2)); }
		public static Vector4 operator - (Vector4 v1, float v2) { return (new Vector4(v1.X - v2, v1.Y - v2, v1.Z - v2, v1.W - v2)); }
		public static Vector4 operator - (Vector4 v1, Vector2 v2) { return (new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z, v1.W)); }
		public static Vector4 operator - (Vector4 v1, Vector3 v2) { return (new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W)); }
		public static Vector4 operator - (Vector4 v1, Vector4 v2) { return (new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W)); }
		public static Vector4 operator * (Vector4 v1, int v2) { return (new Vector4(v1.X * v2, v1.Y * v2, v1.Z * v2, v1.W * v2)); }
		public static Vector4 operator * (Vector4 v1, float v2) { return (new Vector4(v1.X * v2, v1.Y * v2, v1.Z * v2, v1.W * v2)); }
		public static Vector4 operator * (Vector4 v1, Vector2 v2) { return (new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z, v1.W)); }
		public static Vector4 operator * (Vector4 v1, Vector3 v2) { return (new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W)); }
		public static Vector4 operator * (Vector4 v1, Vector4 v2) { return (new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W)); }
		public static Vector4 operator / (Vector4 v1, int v2) { return (new Vector4(v1.X / v2, v1.Y / v2, v1.Z / v2, v1.W / v2)); }
		public static Vector4 operator / (Vector4 v1, float v2) { return (new Vector4(v1.X / v2, v1.Y / v2, v1.Z / v2, v1.W / v2)); }
		public static Vector4 operator / (Vector4 v1, Vector2 v2) { return (new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z, v1.W)); }
		public static Vector4 operator / (Vector4 v1, Vector3 v2) { return (new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W)); }
		public static Vector4 operator / (Vector4 v1, Vector4 v2) { return (new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W / v2.W)); }

		public static bool operator != (Vector4 v1, int v2) { return (!(v1 == v2)); }
		public static bool operator != (Vector4 v1, float v2) { return (!(v1 == v2)); }
		public static bool operator != (Vector4 v1, Vector4 v2) { return (!(v1 == v2)); }
		public static bool operator == (Vector4 v1, int v2) { return ((v1.X == v2) && (v1.Y == v2) && (v1.Z == v2) && (v1.W == v2)); }
		public static bool operator == (Vector4 v1, float v2) { return ((v1.X == v2) && (v1.Y == v2) && (v1.Z == v2) && (v1.W == v2)); }
		public static bool operator == (Vector4 v1, Vector4 v2) { return ((v1.X == v2.X) && (v1.Y == v2.Y) && (v1.Z == v2.Z) && (v1.W == v2.W)); }

		public static Vector4 operator + (Vector4 v) { return (new Vector4(v)); }
		public static Vector4 operator - (Vector4 v) { return (new Vector4(-v.X, -v.Y, -v.Z, -v.W)); }
		public static Vector4 operator ++ (Vector4 v1) { return (new Vector4(v1.X + 1, v1.Y + 1, v1.Z + 1, v1.W + 1)); }
		public static Vector4 operator -- (Vector4 v1) { return (new Vector4(v1.X - 1, v1.Y - 1, v1.Z - 1, v1.W - 1)); }

		public static bool operator > (Vector4 v1, Vector4 v2) { return (v1.Distance > v2.Distance); }
		public static bool operator < (Vector4 v1, Vector4 v2) { return (v1.Distance < v2.Distance); }
		public static bool operator >= (Vector4 v1, Vector4 v2) { return (v1.Distance >= v2.Distance); }
		public static bool operator <= (Vector4 v1, Vector4 v2) { return (v1.Distance <= v2.Distance); }

		public static implicit operator Vector4 (Vector2 v) { return (new Vector4(v)); }
		public static implicit operator Vector4 (Vector3 v) { return (new Vector4(v)); }

		#endregion Operator Overloads.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public Vector4 Squared { get { return (new Vector4(this.X * this.X, this.Y * this.Y, this.Z * this.Z, this.W * this.W)); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public Vector2 ToVector2 () { return (new Vector2(this)); }
		public Vector3 ToVector3 () { return (new Vector3(this)); }

		#endregion Methods.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		public override bool Equals (object obj)
		{
			return ((obj != null) && (this.Equals((Vector4) obj)));
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			unchecked
			{
				hash = 17;
				hash = hash * 23 + this.X.GetHashCode();
				hash = hash * 23 + this.Y.GetHashCode();
				hash = hash * 23 + this.Z.GetHashCode();
				hash = hash * 23 + this.W.GetHashCode();
			}

			return (hash);
		}

		public override string ToString ()
		{
			return (string.Format(@"Vector4 [X={0}, Y={1}, Z={2}, W={3}]", this.X, this.Y, this.Z, this.W));
		}

		#endregion Base Overrides.

		#region Interface: IEquatable<Vector4>.

		//====================================================================================================
		// Interface: IEquatable<Vector4>.
		//====================================================================================================

		public bool Equals (Vector4 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y) && (this.Z == other.Z) && (this.W == other.W));
		}

		#endregion Interface: IEquatable<Vector4>.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static readonly Vector4 Empty = new Vector4();

		static Vector4 ()
		{
		}

		#endregion Static.
	}
}