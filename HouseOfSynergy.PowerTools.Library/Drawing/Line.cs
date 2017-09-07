using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public struct Line:
		IEquatable<Line>,
		ILine
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private float _Slope;

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public Line (float slope)
		{
			this._Slope = slope;
		}

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public float Slope { get { return (this._Slope); } }
		public bool IsVertical { get { return (this._Slope == 0F); } }
		public bool IsHorizontal { get { return (float.IsInfinity(this._Slope)); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

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
				if (obj is Line)
				{
					return (this.Equals((Line) obj));
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
				hash = hash * 23 + this._Slope.GetHashCode();
			}

			return (hash);
		}

		public override string ToString () { return ("{Slope=" + this._Slope.ToString() + "}"); }

		#endregion Base Overrides.

		#region Interface Implementation: System.IEquatable<T>.

		//====================================================================================================
		// Interface Implementation: System.IEquatable<T>.
		//====================================================================================================

		public bool Equals (Line other) { return (this._Slope == other._Slope); }
		bool IEquatable<ILine>.Equals (ILine other) { return (this._Slope == other.Slope); }

		#endregion Interface Implementation: System.IEquatable<T>.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static readonly Line Empty = new Line();

		#endregion Static.
	}
}