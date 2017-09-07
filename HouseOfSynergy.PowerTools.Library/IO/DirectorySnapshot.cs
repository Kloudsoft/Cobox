using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public sealed class DirectorySnapshot
	{
		private List<FileInfo> _Files { get; set; }
		public DirectoryInfo Source { get; private set; }
		private List<DirectoryInfo> _Directories { get; set; }

		public DirectorySnapshot (DirectoryInfo source)
		{
			if (source == null) { throw (new ArgumentNullException("source")); }

			this._Files = new List<FileInfo>();
			this._Directories = new List<DirectoryInfo>();

			this.Source = source;

			this.Populate();
		}

		public ReadOnlyCollection<FileInfo> Files { get { return (this._Files.AsReadOnly()); } }
		public ReadOnlyCollection<DirectoryInfo> Directories { get { return (this._Directories.AsReadOnly()); } }

		private void Populate ()
		{
			this.Populate(this.Source);
		}

		private void Populate (DirectoryInfo directory)
		{
			List<DirectoryInfo> directories = null;

			this._Files.AddRange(directory.GetFiles().ToList());

			directories = directory.GetDirectories().ToList();
			foreach (var d in directories)
			{
				this._Directories.Add(d);

				this.Populate(d);
			}
		}
	}
}