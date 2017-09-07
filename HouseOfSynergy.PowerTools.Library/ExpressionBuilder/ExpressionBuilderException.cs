using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public class ExpressionBuilderException:
		Exception
	{
		public ExpressionBuilderException () : base() { }
		public ExpressionBuilderException (string message) : base(message) { }
		public ExpressionBuilderException (string message, Exception innerException) : base(message, innerException) { }
	}
}