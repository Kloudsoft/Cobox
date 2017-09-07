using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public sealed class ExecutionException:
		ExpressionBuilderException
	{
		public ExecutionException () : base() { }
		public ExecutionException (string message) : base(message) { }
		public ExecutionException (string message, Exception innerException) : base(message, innerException) { }
	}
}