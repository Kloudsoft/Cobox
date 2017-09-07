using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Mathematics.Fractions
{
	public class DenominatorZeroArgumentException:
		ArgumentException
	{
		public DenominatorZeroArgumentException () : base() { }
		public DenominatorZeroArgumentException (string message) : base(message) { }
		public DenominatorZeroArgumentException (string message, Exception innerException) : base(message, innerException) { }
		public DenominatorZeroArgumentException (string message, string paramName) : base(message, paramName) { }
		public DenominatorZeroArgumentException (string message, string paramName, Exception innerException) : base(message, paramName, innerException) { }
	}
}