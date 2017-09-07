using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public interface IConsoleProvider
	{
		ConsoleWrapper Console { get; }
	}
}