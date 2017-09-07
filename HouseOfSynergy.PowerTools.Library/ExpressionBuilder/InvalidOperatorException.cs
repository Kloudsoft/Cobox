using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public sealed class InvalidOperatorException:
		Exception
	{
		public int? Position { get; private set; }

		public InvalidOperatorException () : base() { this.Position = null; }
		public InvalidOperatorException (string message, int? position = null) : base(message) { this.Position = position; }
		public InvalidOperatorException (string message, Exception innerException, int? position = null) : base(message, innerException) { this.Position = position; }
	}
}