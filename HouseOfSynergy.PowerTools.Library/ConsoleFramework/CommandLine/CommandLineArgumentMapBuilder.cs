using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public sealed class CommandLineArgumentMapBuilder
	{
		private List<ICommandLineArgumentKey> Keys { get; set; }

		public CommandLineArgumentMapBuilder ()
		{
			this.Keys = new List<ICommandLineArgumentKey>();
		}

		public void AddKey (ICommandLineArgumentKey key)
		{
			if (key == null) { throw (new ArgumentNullException("key")); }

			foreach (var item in this.Keys)
			{
				if (string.Compare(item.Name, key.Name, true, CultureInfo.InvariantCulture) == 0)
				{
					throw (new CommandLineArgumentKeyDuplicateException(key));
				}
			}

			this.Keys.Add(key);
		}

		public void AddKey (string name, bool required = false, bool allowMultiple = false)
		{
			var key = new CommandLineArgumentKey(name, required, allowMultiple);

			foreach (var item in this.Keys)
			{
				if (string.Compare(item.Name, key.Name, true, CultureInfo.InvariantCulture) == 0)
				{
					throw (new CommandLineArgumentKeyDuplicateException(key));
				}
			}

			this.Keys.Add(key);
		}

		public ICommandLineArgumentMap ToCommandLineArgumentMap ()
		{
			return (new CommandLineArgumentMap(this.Keys));
		}
	}
}