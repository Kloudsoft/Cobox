using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public sealed class ConsoleFrameworkException:
		HouseOfSynergy.PowerTools.Library.Exceptions.ApplicationException
	{
		public ConsoleFrameworkException () : base() { }
		public ConsoleFrameworkException (string message) : base(message) { }
		public ConsoleFrameworkException (string message, Exception innerException) : base(message, innerException) { }
	}
}