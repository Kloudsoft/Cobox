using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public interface ICommandLineArgumentMap
	{
		ReadOnlyCollection<ICommandLineArgumentKey> Keys { get; }
	}
}