using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class Option
	{
		/// <summary>
		/// Option type.
		/// </summary>
		public InputType Type { get; private set; }

		/// <summary>
		/// Text to display against this option.
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		/// The key text or console key that activates this option.
		/// </summary>
		public object Key { get; private set; }

		protected Option (InputType type, string text, object key)
		{
			this.Key = key;
			this.Type = type;
			this.Text = text ?? "";
		}

		internal virtual void Render (IConsoleApplication application)
		{
			application.Console.Write(" - " + this.Text);
		}
	}
}