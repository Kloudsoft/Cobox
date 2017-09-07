using System;
using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public class DirectoryCopierErrorEventArgs:
		HouseOfSynergy.PowerTools.Library.IO.DirectoryCopierEventArgs
	{
		public Exception Exception { get; private set; }

		public DirectoryCopierErrorEventArgs (DirectoryInfo source, DirectoryInfo destination, Exception exception)
			: base(source, destination)
		{
			this.Exception = exception;
		}
	}
}