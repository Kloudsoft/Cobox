using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public interface ICommandLineArgument:
		ICommandLineArgumentElement
	{
		ICommandLineArgumentKey Key { get; }
		ICommandLineArgumentValue Value { get; }
	}
}