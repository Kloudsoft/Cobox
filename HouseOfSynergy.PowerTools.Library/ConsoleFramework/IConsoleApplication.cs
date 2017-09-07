using System.Collections.ObjectModel;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public interface IConsoleApplication:
		IApplicationInfoProvider,
		IConsoleProvider
	{
		bool AllowMultipleInstances { get; }
		ReadOnlyCollection<string> CommandLineArguments { get; }
	}
}