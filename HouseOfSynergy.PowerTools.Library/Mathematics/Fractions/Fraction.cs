using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Mathematics.Fractions
{
	public struct Fraction
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public readonly FractionType Type;
		public readonly int Numerator;
		public readonly int Denominator;

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public Fraction (int numerator)
		{
			this.Denominator = 1;
			this.Numerator = numerator;
			this.Type = (Math.Abs(this.Numerator) < Math.Abs(this.Denominator)) ? FractionType.Proper : FractionType.Improper;
		}

		public Fraction (int numerator, int denominator)
		{
			if (denominator == 0) { throw (new DenominatorZeroArgumentException("The argument [denominator] cannot be zero.", "denominator")); }

			this.Numerator = numerator;
			this.Denominator = denominator;
			this.Type = (Math.Abs(this.Numerator) < Math.Abs(this.Denominator)) ? FractionType.Proper : FractionType.Improper;
		}

		public Fraction (float value)
		{
			if (float.IsNaN(value)) { throw (new DenominatorZeroArgumentException("The argument [value] cannot be [NaN].", "value")); }
			if (float.IsNegativeInfinity(value)) { throw (new DenominatorZeroArgumentException("The argument [value] cannot be [Negative Infinity].", "value")); }
			if (float.IsPositiveInfinity(value)) { throw (new DenominatorZeroArgumentException("The argument [value] cannot be [Positive Infinity].", "value")); }
			if (float.IsInfinity(value)) { throw (new DenominatorZeroArgumentException("The argument [value] cannot be [+/-(Infinity)].", "value")); }

			if (value.ToString().Contains("."))
			{
				this.Numerator = int.Parse(value.ToString().Replace(".", ""));
				this.Denominator = int.Parse("1" + new string('0', value.ToString().Length - value.ToString().IndexOf(".") - 1));
			}
			else
			{
				this.Denominator = 1;
				this.Numerator = (int) value;
			}

			this.Type = (Math.Abs(this.Numerator) < Math.Abs(this.Denominator)) ? FractionType.Proper : FractionType.Improper;
		}

		#endregion Constructors.

		#region Operators.

		//====================================================================================================
		// Operator Overloads.
		//====================================================================================================

		public static implicit operator Fraction (int value)
		{
			return (new Fraction(value));
		}

		public static implicit operator Fraction (float value)
		{
			return (new Fraction(value));
		}

		public static implicit operator float (Fraction value)
		{
			return (value.ValueSingle);
		}

		public static implicit operator Fraction (MixedFraction value)
		{
			return (value.ToFraction());
		}

		public static bool operator == (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (x.Numerator == y.Numerator);
			}
			else
			{
				return ((x.Numerator * y.Denominator) == (x.Denominator * y.Numerator));
			}
		}

		public static bool operator != (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (x.Numerator != y.Numerator);
			}
			else
			{
				return ((x.Numerator * y.Denominator) != (x.Denominator * y.Numerator));
			}
		}

		public static bool operator < (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (x.Numerator < y.Numerator);
			}

			if (x.Numerator == y.Numerator)
			{
				return (x.Denominator > y.Denominator);
			}

			return ((x.Numerator * y.Denominator) < (x.Denominator * y.Numerator));
		}

		public static bool operator < (Fraction x, int y)
		{
			return (x < new Fraction(y, 1));
		}

		public static bool operator < (int x, Fraction y)
		{
			return (new Fraction(x, 1) < y);
		}

		public static bool operator > (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (x.Numerator > y.Numerator);
			}

			if (x.Numerator == y.Numerator)
			{
				return (x.Denominator < y.Denominator);
			}

			return ((x.Numerator * y.Denominator) > (x.Denominator * y.Numerator));
		}

		public static bool operator > (Fraction x, int y)
		{
			return (x > new Fraction(y, 1));
		}

		public static bool operator > (int x, Fraction y)
		{
			return (new Fraction(x, 1) > y);
		}

		public static bool operator <= (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (x.Numerator <= y.Numerator);
			}

			if (x.Numerator == y.Numerator)
			{
				return (x.Denominator >= y.Denominator);
			}

			return ((x.Numerator * y.Denominator) <= (x.Denominator * y.Numerator));
		}

		public static bool operator <= (Fraction x, int y)
		{
			return (x <= new Fraction(y, 1));
		}

		public static bool operator <= (int x, Fraction y)
		{
			return (new Fraction(x, 1) <= y);
		}

		public static bool operator >= (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (x.Numerator >= y.Numerator);
			}

			if (x.Numerator == y.Numerator)
			{
				return (x.Denominator <= y.Denominator);
			}

			return ((x.Numerator * y.Denominator) >= (x.Denominator * y.Numerator));
		}

		public static bool operator >= (Fraction x, int y)
		{
			return (x >= new Fraction(y, 1));
		}

		public static bool operator >= (int x, Fraction y)
		{
			return (new Fraction(x, 1) >= y);
		}

		public static Fraction operator + (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (new Fraction(x.Numerator + y.Numerator, x.Denominator));
			}
			else
			{
				return (new Fraction(x.Numerator * y.Denominator, x.Denominator * y.Denominator) + new Fraction(y.Numerator * x.Denominator, y.Denominator * x.Denominator));
			}
		}

		public static Fraction operator + (Fraction x, int y)
		{
			return (x + new Fraction(y, 1));
		}

		public static Fraction operator + (int x, Fraction y)
		{
			return (new Fraction(x, 1) + y);
		}

		public static Fraction operator - (Fraction x, Fraction y)
		{
			if (x.Denominator == y.Denominator)
			{
				return (new Fraction(x.Numerator - y.Numerator, x.Denominator));
			}
			else
			{
				return (new Fraction(x.Numerator * y.Denominator, x.Denominator * y.Denominator) - new Fraction(y.Numerator * x.Denominator, y.Denominator * x.Denominator));
			}
		}

		public static Fraction operator - (Fraction x, int y)
		{
			return (x - new Fraction(y, 1));
		}

		public static Fraction operator - (int x, Fraction y)
		{
			return (new Fraction(x, 1) - y);
		}

		public static Fraction operator * (Fraction x, Fraction y)
		{
			return (new Fraction(x.Numerator * y.Numerator, x.Denominator * y.Denominator));
		}

		public static Fraction operator * (Fraction x, int y)
		{
			return (new Fraction(x.Numerator * y, x.Denominator * 1));
		}

		public static Fraction operator * (int x, Fraction y)
		{
			return (new Fraction(x * y.Numerator, 1 * y.Denominator));
		}

		public static Fraction operator / (Fraction x, Fraction y)
		{
			return (x * y.Reciprocal);
		}

		public static Fraction operator / (Fraction x, int y)
		{
			return (new Fraction(x.Numerator, x.Denominator * y));
		}

		public static Fraction operator / (int x, Fraction y)
		{
			return (new Fraction(y.Numerator, y.Denominator * x));
		}

		#endregion Operators.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public int ValueInt32 { get { return (this.Numerator / this.Denominator); } }
		public float ValueSingle { get { return (((float) this.Numerator) / ((float) this.Denominator)); } }
		public double ValueDouble { get { return (((double) this.Numerator) / ((double) this.Denominator)); } }
		public int Sign { get { return ((int) (Math.Sign(this.Numerator) * Math.Sign(this.Denominator))); } }
		public Fraction Squared { get { return (new Fraction(this.Denominator * this.Denominator, this.Numerator * this.Numerator)); } }
		public Fraction Reciprocal { get { return (new Fraction(this.Denominator, this.Numerator)); } }
		public bool IsUnit { get { return (this.Numerator == 1); } }
		public bool IsWhole { get { return ((this.Denominator == 1) || (this.Numerator == this.Denominator) || ((this.Numerator >= this.Denominator) && (this.Numerator % this.Denominator == 0))); } }
		public bool IsProper { get { return (this.Type == FractionType.Proper); } }
		public bool IsImproper { get { return (this.Type == FractionType.Improper); } }
		public bool IsZero { get { return (this.Numerator == 0); } }
		public bool IsOne { get { return (this.Numerator == this.Denominator); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public int GetGreatestCommonDivisor ()
		{
			return (HouseOfSynergy.PowerTools.Library.Mathematics.Utility.GetGreatestCommonDivisor(this.Numerator, this.Denominator));
		}

		public int GetLeastCommonMultiple ()
		{
			return (HouseOfSynergy.PowerTools.Library.Mathematics.Utility.GetLeastCommonMultiple(this.Numerator, this.Denominator));
		}

		public Fraction GetNormalized ()
		{
			int gcd = 0;

			if (this.Numerator == this.Denominator) { return (new Fraction(1, 1)); }

			gcd = HouseOfSynergy.PowerTools.Library.Mathematics.Utility.GetGreatestCommonDivisor(this.Numerator, this.Denominator);

			return (new Fraction(this.Numerator / gcd, this.Denominator / gcd));
		}

		public MixedFraction ToMixedFraction ()
		{
			return (new MixedFraction(this.Numerator, this.Denominator));
		}

		#endregion Methods.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		public override bool Equals (object o)
		{
			return (((o == null) ? false : (this == ((Fraction) o))));
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			hash = 37;
			hash = hash * 23 + base.GetHashCode();
			hash = hash * 23 + this.Numerator.GetHashCode();
			hash = hash * 23 + this.Denominator.GetHashCode();

			return (hash);
		}

		public override string ToString ()
		{
			return (this.Numerator.ToString() + "/" + this.Denominator.ToString());
		}

		#endregion Base Overrides.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static Fraction One { get { return (new Fraction(1, 1)); } }
		public static Fraction Zero { get { return (new Fraction(0, 1)); } }
		public static Fraction Pi { get { return (new Fraction(314159265, 100000000)); } }

		static Fraction ()
		{
		}

		#endregion Static.
	}
}