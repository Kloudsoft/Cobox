using System;
using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public class DirectoryCopierEventArgs:
		EventArgs
	{
		public DirectoryInfo Source { get; private set; }
		public DirectoryInfo Destination { get; private set; }

		public DirectoryCopierEventArgs (DirectoryInfo source, DirectoryInfo destination)
		{
			this.Source = source;
			this.Destination = destination;
		}
	}
}