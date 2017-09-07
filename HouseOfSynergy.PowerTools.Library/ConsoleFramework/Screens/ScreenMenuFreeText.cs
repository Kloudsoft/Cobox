using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class ScreenMenuFreeText<T>:
		ScreenMenu<T>
		where T: SingletonDisposable<T>
	{
		public new MenuFreeText Menu { get; private set; }

		public ScreenMenuFreeText (ApplicationBase<T> application)
			: base(application)
		{
			this.Menu = new MenuFreeText();
		}

		public ScreenMenuFreeText (ApplicationBase<T> application, string text)
			: base(application, text)
		{
			this.Menu = new MenuFreeText();
		}

		public ScreenMenuFreeText (ApplicationBase<T> application, string title, string text)
			: base(application, title, text)
		{
			this.Menu = new MenuFreeText();
		}
	}
}