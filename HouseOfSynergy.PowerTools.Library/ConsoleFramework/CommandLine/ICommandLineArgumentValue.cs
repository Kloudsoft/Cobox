using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public interface ICommandLineArgumentValue:
		ICommandLineArgumentElement
	{
		bool IsValid { get; }
		bool IsPresent { get; }
	}

	public interface ICommandLineArgumentValue<T>:
		ICommandLineArgumentValue
	{
		T Value { get; }
	}
}