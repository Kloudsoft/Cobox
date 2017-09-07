using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public class CommandLineArgumentKeyDuplicateException:
		CommandLineArgumentsException
	{
		public ICommandLineArgumentKey Key { get; private set; }

		public CommandLineArgumentKeyDuplicateException (ICommandLineArgumentKey key)
			: base()
		{
			this.Key = key;
		}

		public CommandLineArgumentKeyDuplicateException () : base() { }
		public CommandLineArgumentKeyDuplicateException (ICommandLineArgumentKey key, string message) : base(message) { this.Key = key; }
		public CommandLineArgumentKeyDuplicateException (ICommandLineArgumentKey key, string message, Exception innerException) : base(message, innerException) { this.Key = key; }
	}
}