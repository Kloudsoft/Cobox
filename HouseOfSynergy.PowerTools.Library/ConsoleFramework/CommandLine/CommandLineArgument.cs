using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public sealed class CommandLineArgument:
		ICommandLineArgumentElement,
		ICommandLineArgument
	{
		public string Text { get; private set; }
		public ICommandLineArgumentKey Key { get; private set; }
		public ICommandLineArgumentValue Value { get; private set; }

		public CommandLineArgument (string name, bool required)
		{
			this.Text = "";
		}

		public static readonly char KeyValueDelimiter = '\0';
		public static readonly CommandLineArgument Empty = null;

		static CommandLineArgument ()
		{
			CommandLineArgument.KeyValueDelimiter = ':';
		}

		public static CommandLineArgument Parse (string text)
		{
			return (null);
		}

		public override string ToString ()
		{
			return (base.ToString());
		}
	}
}