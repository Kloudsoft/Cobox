using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Mathematics.Fractions
{
	public class InvalidMixedFractionFormatException:
		System.FormatException
	{
		public InvalidMixedFractionFormatException () : base() { }
		public InvalidMixedFractionFormatException (string message) : base(message) { }
		public InvalidMixedFractionFormatException (string message, Exception innerException) : base(message, innerException) { }
	}
}