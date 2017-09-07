using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public sealed class FileCopyEntry:
		CopyEntry
	{
		public FileInfo Source { get; private set; }
		public FileInfo Destination { get; private set; }

		public FileCopyEntry (FileInfo source, FileInfo destination)
			: base(CopyEntry.EnumType.File)
		{
			this.Source = source;
			this.Destination = destination;
		}
	}
}