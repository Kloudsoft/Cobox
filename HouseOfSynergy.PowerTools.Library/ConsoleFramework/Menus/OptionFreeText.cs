using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public class OptionFreeText:
		Option
	{
		public OptionFreeText (string text, string key) : base(InputType.FreeText, text, key) { }

		public new string Key { get { return ((string) base.Key); } }

		internal override void Render (IConsoleApplication application)
		{
			application.Console.Write(" - " + this.Text);
		}
	}
}