using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class Menu:
		IRenderable
	{
		public InputType Type { get; private set; }
		public TimeSpan Timeout { get; private set; }
		public List<Option> Options { get; private set; }
		public Option OptionSelected { get; private set; }
		public Option OptionDefault { get; protected set; }
		public string MenuTextAfterOptions { get; set; }
		public string MenuTextBeforeOptions { get; set; }

		protected Menu (InputType type)
		{
			this.Type = type;
			this.OptionDefault = null;
			this.OptionSelected = null;
			this.Timeout = TimeSpan.Zero;
			this.MenuTextAfterOptions = "";
			this.MenuTextBeforeOptions = "";
			this.Options = new List<Option>();
		}

		public virtual void Render (IConsoleApplication application)
		{
			application.Console.Write(this.MenuTextBeforeOptions);
			application.Console.WriteLine();

			foreach (var option in this.Options)
			{
				application.Console.WriteLine();

				option.Render(application);
			}

			if (this.Options.Count > 0) { application.Console.WriteLine(); }

			application.Console.WriteLine();
			application.Console.Write(this.MenuTextAfterOptions);
		}

		public virtual void WaitForInput (IConsoleApplication application)
		{
		}
	}
}