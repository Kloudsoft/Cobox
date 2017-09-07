using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public class OptionSingleKey:
		Option
	{
		public OptionSingleKey (string text, ConsoleKeyInfo key) : base(InputType.SingleKey, text, key) { }

		public new ConsoleKeyInfo Key { get { return ((ConsoleKeyInfo) base.Key); } }

		internal override void Render (IConsoleApplication application)
		{
			application.Console.Write(" - " + this.Text);
		}
	}
}