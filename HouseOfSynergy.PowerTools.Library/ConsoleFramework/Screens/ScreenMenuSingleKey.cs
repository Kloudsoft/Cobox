using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class ScreenMenuSingleKey<T>:
		ScreenMenu<T>
		where T: SingletonDisposable<T>
	{
		public new MenuSingleKey Menu { get; private set; }

		public ScreenMenuSingleKey (ApplicationBase<T> application)
			: base(application)
		{
			this.Menu = new MenuSingleKey();
		}

		public ScreenMenuSingleKey (ApplicationBase<T> application, string text)
			: base(application, text)
		{
			this.Menu = new MenuSingleKey();
		}

		public ScreenMenuSingleKey (ApplicationBase<T> application, string title, string text)
			: base(application, title, text)
		{
			this.Menu = new MenuSingleKey();
		}

		public override void Render (IConsoleApplication application)
		{
			application.Console.Write(this.Text);

			application.Console.WriteLine();
			application.Console.WriteLine();

			this.Menu.Render(application);
		}
	}
}