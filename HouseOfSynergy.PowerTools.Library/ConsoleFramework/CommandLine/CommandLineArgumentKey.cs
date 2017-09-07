using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public sealed class CommandLineArgumentKey:
		ICommandLineArgumentKey
	{
		public string Name { get; private set; }
		public bool IsRequired { get; private set; }
		public bool AllowMultiple { get; private set; }

		private CommandLineArgumentKey ()
		{
		}

		public CommandLineArgumentKey (string name, bool required = false, bool allowMultiple = false)
		{
			if (name == null) { throw (new ArgumentNullException("name")); }
			if (!CommandLineArgumentKey.Validate(name)) { throw (new CommandLineArgumentKeyInvalidNameException(name)); }

			this.Name = name;
			this.IsRequired = required;
			this.AllowMultiple = allowMultiple;
		}

		public bool IsOptional { get { return (this.IsRequired); } }

		public static readonly char Prefix = '\0';
		public static readonly ReadOnlyCollection<char> ValidCharacters = null;

		static CommandLineArgumentKey ()
		{
			var list = new List<char>();

			CommandLineArgumentKey.Prefix = '-';

			list.Add('_');
			for (int i = ((int) '0'); i <= ((int) '9'); i++) { list.Add((char) i); }
			for (int i = ((int) 'a'); i <= ((int) 'z'); i++) { list.Add((char) i); }
			for (int i = ((int) 'A'); i <= ((int) 'Z'); i++) { list.Add((char) i); }

			CommandLineArgumentKey.ValidCharacters = new ReadOnlyCollection<char>(list);
		}

		public static bool Validate (string key)
		{
			if (key == null) { return (false); }
			if (key.Trim().Length == 0) { return (false); }
			if (key.Any(c => (!CommandLineArgumentKey.ValidCharacters.Contains(c)))) { return (false); }

			return (true);
		}
	}
}