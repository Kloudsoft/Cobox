using System;
using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public sealed class DirectoryCopyEntry:
		CopyEntry
	{
		public DirectoryInfo Source { get; private set; }
		public DirectoryInfo Destination { get; private set; }

		public DirectoryCopyEntry (DirectoryInfo source, DirectoryInfo destination)
			: base(CopyEntry.EnumType.Directory)
		{
			this.Source = source;
			this.Destination = destination;
		}

		internal bool Create ()
		{
			bool result = false;

			try
			{
				this.Attempted = true;

				this.Destination.Create();

				this.Completed = true;

				result = true;
			}
			catch (Exception exception)
			{
				this.HasErrors = true;
				this._Exceptions.Add(exception);
			}

			return (result);
		}
	}
}