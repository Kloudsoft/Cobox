using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public class CommandLineArgumentKeyInvalidNameException:
		CommandLineArgumentsException
	{
		public string Name { get; private set; }

		public CommandLineArgumentKeyInvalidNameException (string name)
			: base(string.Format(CommandLineArgumentKeyInvalidNameException.MessagePlaceHolder, name))
		{
			this.Name = name;
		}

		public CommandLineArgumentKeyInvalidNameException () : base() { this.Name = ""; }
		public CommandLineArgumentKeyInvalidNameException (string name, string message) : base(message) { this.Name = name; }
		public CommandLineArgumentKeyInvalidNameException (string name, string message, Exception innerException) : base(message, innerException) { this.Name = name; }

		private static readonly string MessagePlaceHolder = "";

		static CommandLineArgumentKeyInvalidNameException ()
		{
			CommandLineArgumentKeyInvalidNameException.MessagePlaceHolder
				= "The argument [name] is not valid as a command line argument key [{0}]."
				+ Environment.NewLine
				+ Environment.NewLine
				+ "The key must be at least one character long and the valid range of characters allowed as a key include: ["
				+ string.Join("", CommandLineArgumentKey.ValidCharacters)
				+ "]."
				;
		}
	}
}