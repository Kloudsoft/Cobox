using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public sealed class CompilerException:
		ExpressionBuilderException
	{
		public CompilerException () : base() { }
		public CompilerException (string message) : base(message) { }
		public CompilerException (string message, Exception innerException) : base(message, innerException) { }
	}
}