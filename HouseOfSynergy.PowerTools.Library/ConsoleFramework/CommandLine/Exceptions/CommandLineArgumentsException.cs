using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public class CommandLineArgumentsException:
		Exception
	{
		public CommandLineArgumentsException () : base() { }
		public CommandLineArgumentsException (string message) : base(message) { }
		public CommandLineArgumentsException (string message, Exception innerException) : base(message, innerException) { }
	}
}