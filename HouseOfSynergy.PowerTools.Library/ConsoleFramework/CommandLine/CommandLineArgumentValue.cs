using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public class CommandLineArgumentValue:
		ICommandLineArgumentValue
	{
		public string Text { get; private set; }
		public bool IsValid { get; private set; }
		public bool IsPresent { get; private set; }

		public CommandLineArgumentValue ()
		{
		}

		public CommandLineArgumentValue (string text)
		{
		}
	}

	public sealed class CommandLineArgumentValue<T>:
		CommandLineArgumentValue
	{
		public T Value { get; private set; }

		public CommandLineArgumentValue ()
			: base()
		{
		}

		public CommandLineArgumentValue (string text)
			: base(text)
		{
		}
	}
}