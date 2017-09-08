using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.Entities.Containers
{
	public sealed class FileContainer
	{
		public FileType FileType { get; private set; }
		public FileFormatType FileFormatType { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public ReadOnlyCollection<string> Extensions { get; private set; }

		public FileContainer (FileType fileType, FileFormatType fileFormatType, string name, string description, string extension)
			: this(fileType, fileFormatType, name, description, new string [] { extension })
		{
		}

		public FileContainer (FileType fileType, FileFormatType fileFormatType, string name, string description, params string [] extensions)
			: this(fileType, fileFormatType, name, description, extensions as IEnumerable<string>)
		{
			this.Extensions = new ReadOnlyCollection<string>(extensions);
		}

		public FileContainer (FileType fileType, FileFormatType fileFormatType, string name, string description, IEnumerable<string> extensions)
		{
			this.FileType = fileType;
			this.FileFormatType = fileFormatType;
			this.Name = name;
			this.Description = description;
			this.Extensions = new ReadOnlyCollection<string>(extensions.ToList());
		}

		public override string ToString ()
		{
			return (this.FileType.ToString() + " (" + this.FileFormatType.ToString() + "): " + this.Name);
		}
	}
}