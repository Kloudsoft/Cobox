using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public sealed class ParserException:
		ExpressionBuilderException
	{
		public int? Position { get; private set; }

		public ParserException () : base() { this.Position = null; }
		public ParserException (string message, int? position = null) : base(message) { this.Position = position; }
		public ParserException (string message, Exception innerException, int? position = null) : base(message, innerException) { this.Position = position; }
	}
}