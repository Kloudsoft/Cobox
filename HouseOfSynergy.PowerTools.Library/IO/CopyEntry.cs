using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public abstract class CopyEntry
	{
		public enum EnumType { Directory, File }

		public bool Attempted { get; internal set; }
		public bool Completed { get; internal set; }
		public bool HasErrors { get; internal set; }
		public CopyEntry.EnumType Type { get; private set; }
		internal List<Exception> _Exceptions { get; private set; }

		protected CopyEntry (CopyEntry.EnumType type)
		{
			this._Exceptions = new List<Exception>();

			this.Type = type;
		}

		public ReadOnlyCollection<Exception> Exceptions { get { return (this._Exceptions.AsReadOnly()); } }
	}
}