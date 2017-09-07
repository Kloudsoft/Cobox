using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public class CommandLineArgumentMap:
		ICommandLineArgumentMap
	{
		public ReadOnlyCollection<ICommandLineArgumentKey> Keys { get; private set; }

		public CommandLineArgumentMap ()
		{
			this.Keys = new ReadOnlyCollection<ICommandLineArgumentKey>(new ICommandLineArgumentKey [] { });
		}

		internal CommandLineArgumentMap (IList<ICommandLineArgumentKey> keys)
		{
			if (keys == null) { throw (new ArgumentNullException("keys")); }

			this.Keys = new ReadOnlyCollection<ICommandLineArgumentKey>(keys.ToList());
		}

		internal CommandLineArgumentMap (IEnumerable<ICommandLineArgumentKey> keys)
			: this (keys.ToList())
		{
		}
	}
}