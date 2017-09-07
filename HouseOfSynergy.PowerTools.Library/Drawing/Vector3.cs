using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public struct Vector3:
		IEquatable<Vector3>
	{
		#region Fields.

		//====================================================================================================
		// Fields.
		//====================================================================================================

		public readonly float X;
		public readonly float Y;
		public readonly float Z;
		public readonly float Distance;

		#endregion Fields.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public Vector3 (Vector2 vector) : this(vector.X, vector.Y, 0) { }
		public Vector3 (Vector3 vector) : this(vector.X, vector.Y, vector.Z) { }
		public Vector3 (Vector4 vector) : this(vector.X, vector.Y, vector.Z) { }
		public Vector3 (float x, float y, float z) { this.X = x; this.Y = y; this.Z = z; this.Distance = x * y * z; }

		#endregion Constructors.

		#region Operator Overloads.

		//====================================================================================================
		// Operator Overloads.
		//====================================================================================================

		public static Vector3 operator + (Vector3 v1, int v2) { return (new Vector3(v1.X + v2, v1.Y + v2, v1.Z + v2)); }
		public static Vector3 operator + (Vector3 v1, float v2) { return (new Vector3(v1.X + v2, v1.Y + v2, v1.Z + v2)); }
		public static Vector3 operator + (Vector3 v1, Vector2 v2) { return (new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z)); }
		public static Vector3 operator + (Vector3 v1, Vector3 v2) { return (new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z)); }
		public static Vector3 operator + (Vector3 v1, Vector4 v2) { return (new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z)); }
		public static Vector3 operator - (Vector3 v1, int v2) { return (new Vector3(v1.X - v2, v1.Y - v2, v1.Z - v2)); }
		public static Vector3 operator - (Vector3 v1, float v2) { return (new Vector3(v1.X - v2, v1.Y - v2, v1.Z - v2)); }
		public static Vector3 operator - (Vector3 v1, Vector2 v2) { return (new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z)); }
		public static Vector3 operator - (Vector3 v1, Vector3 v2) { return (new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z)); }
		public static Vector3 operator - (Vector3 v1, Vector4 v2) { return (new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z)); }
		public static Vector3 operator * (Vector3 v1, int v2) { return (new Vector3(v1.X * v2, v1.Y * v2, v1.Z * v2)); }
		public static Vector3 operator * (Vector3 v1, float v2) { return (new Vector3(v1.X * v2, v1.Y * v2, v1.Z * v2)); }
		public static Vector3 operator * (Vector3 v1, Vector2 v2) { return (new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z)); }
		public static Vector3 operator * (Vector3 v1, Vector3 v2) { return (new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z)); }
		public static Vector3 operator * (Vector3 v1, Vector4 v2) { return (new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z)); }
		public static Vector3 operator / (Vector3 v1, int v2) { return (new Vector3(v1.X / v2, v1.Y / v2, v1.Z / v2)); }
		public static Vector3 operator / (Vector3 v1, float v2) { return (new Vector3(v1.X / v2, v1.Y / v2, v1.Z / v2)); }
		public static Vector3 operator / (Vector3 v1, Vector2 v2) { return (new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z)); }
		public static Vector3 operator / (Vector3 v1, Vector3 v2) { return (new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z)); }
		public static Vector3 operator / (Vector3 v1, Vector4 v2) { return (new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z)); }

		public static bool operator != (Vector3 v1, int v2) { return (!(v1 == v2)); }
		public static bool operator != (Vector3 v1, float v2) { return (!(v1 == v2)); }
		public static bool operator != (Vector3 v1, Vector3 v2) { return (!(v1 == v2)); }
		public static bool operator == (Vector3 v1, int v2) { return ((v1.X == v2) && (v1.Y == v2) && (v1.Z == v2)); }
		public static bool operator == (Vector3 v1, float v2) { return ((v1.X == v2) && (v1.Y == v2) && (v1.Z == v2)); }
		public static bool operator == (Vector3 v1, Vector3 v2) { return ((v1.X == v2.X) && (v1.Y == v2.Y) && (v1.Z == v2.Z)); }

		public static Vector3 operator + (Vector3 v) { return (new Vector3(v)); }
		public static Vector3 operator - (Vector3 v) { return (new Vector3(-v.X, -v.Y, -v.Z)); }
		public static Vector3 operator ++ (Vector3 v1) { return (new Vector3(v1.X + 1, v1.Y + 1, v1.Z + 1)); }
		public static Vector3 operator -- (Vector3 v1) { return (new Vector3(v1.X - 1, v1.Y - 1, v1.Z - 1)); }

		public static implicit operator Vector3 (Vector2 v) { return (new Vector3(v)); }
		public static explicit operator Vector3 (Vector4 v) { return (new Vector3(v)); }

		#endregion Operator Overloads.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public Vector3 Squared { get { return (new Vector3(this.X * this.X, this.Y * this.Y, this.Z * this.Z)); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public Vector2 ToVector2 () { return (new Vector2(this)); }
		public Vector4 ToVector4 () { return (new Vector4(this)); }

		#endregion Methods.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		public override bool Equals (object obj)
		{
			return ((obj != null) && (this.Equals((Vector3) obj)));
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
			}

			return (hash);
		}

		public override string ToString ()
		{
			return (string.Format(@"Vector4 [X={0}, Y={1}, Z={2}]", this.X, this.Y, this.Z));
		}

		#endregion Base Overrides.

		#region Interface: IEquatable<Vector4>.

		//====================================================================================================
		// Interface: IEquatable<Vector4>.
		//====================================================================================================

		public bool Equals (Vector3 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y) && (this.Z == other.Z));
		}

		#endregion Interface: IEquatable<Vector4>.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static readonly Vector3 Empty = new Vector3();

		static Vector3 ()
		{
		}

		#endregion Static.
	}
}