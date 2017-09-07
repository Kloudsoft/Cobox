using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IDatabaseFile:
		IDatabase
	{
		FileInfo Filename { get; }
		DirectoryInfo Directory { get; }
	}
}