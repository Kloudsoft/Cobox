using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Mathematics.Fractions
{
	public struct MixedFraction
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private readonly int Number;
		private readonly Fraction Fraction;

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public MixedFraction (int number)
		{
			this.Number = number;
			this.Fraction = new Fraction(0, 1);
		}

		public MixedFraction (int number, Fraction fraction)
		{
			if ((number != 0) && (fraction.IsImproper))
			{
				throw (new InvalidMixedFractionFormatException("The mixed fraction is in an invalid format. A mixed fraction can either have a whole part plus a proper fraction or an improper fraction without a whole part."));
			}

			this.Number = number;
			this.Fraction = fraction;
		}

		public MixedFraction (int numerator, int denominator)
		{
			this.Number = 0;
			this.Fraction = new Fraction(numerator, denominator);
		}

		public MixedFraction (int number, int numerator, int denominator)
		{
			if ((number != 0) && (numerator > denominator))
			{
				throw (new InvalidMixedFractionFormatException("The mixed fraction is in an invalid format. A mixed fraction can either have a whole part plus a proper fraction or an improper fraction without a whole part."));
			}

			this.Number = number;
			this.Fraction = new Fraction(numerator, denominator);
		}

		#endregion Constructors.

		#region Operator Overloads.

		//====================================================================================================
		// Operator Overloads.
		//====================================================================================================

		public static implicit operator MixedFraction (int value)
		{
			return (new Fraction(value).ToMixedFraction());
		}

		public static implicit operator MixedFraction (float value)
		{
			return (new Fraction(value).ToMixedFraction());
		}

		public static implicit operator float (MixedFraction value)
		{
			return (value.ToFraction().ValueSingle);
		}

		public static implicit operator MixedFraction (Fraction value)
		{
			return (value.ToMixedFraction());
		}

		#endregion Operator Overloads.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public MixedFraction GetNormalized ()
		{
			if (this.Number == 0)
			{
				if (this.Fraction.IsProper)
				{
					return (this);
				}
				else
				{
					return (new MixedFraction(this.Fraction.Numerator / this.Fraction.Denominator, this.Fraction.Numerator % this.Fraction.Denominator, this.Fraction.Denominator));
				}
			}
			else
			{
				return (this);
			}
		}

		public Fraction ToFraction ()
		{
			if (this.Number == 0)
			{
				return (this.Fraction);
			}
			else
			{
				return (new Fraction((this.Fraction.Denominator * this.Number) + this.Fraction.Numerator, this.Fraction.Denominator));
			}
		}

		#endregion Methods.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		public override bool Equals (object o)
		{
			return (false);
			//return (((o == null) ? false : (this == ((MixedFraction) o))));
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			hash = 37;
			hash = hash * 23 + this.Number.GetHashCode();
			hash = hash * 23 + this.Fraction.GetHashCode();

			return (hash);
		}

		public override string ToString ()
		{
			if (this.Number == 0)
			{
				if (this.Fraction.IsZero)
				{
					return ("0");
				}
				else
				{
					return (this.Fraction.ToString());
				}
			}
			else
			{
				if (this.Fraction.IsZero)
				{
					return (this.Number.ToString());
				}
				else
				{
					return (this.Number.ToString() + " " + this.Fraction.ToString());
				}
			}
		}

		#endregion Base Overrides.
	}
}