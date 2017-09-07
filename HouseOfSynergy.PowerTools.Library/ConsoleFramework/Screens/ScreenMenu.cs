using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class ScreenMenu<T>:
		Screen<T>
		where T: SingletonDisposable<T>
	{
		public Menu Menu { get; private set; }

		public ScreenMenu (ApplicationBase<T> application)
			: base(application)
		{
		}

		public ScreenMenu (ApplicationBase<T> application, string text)
			: base(application, text)
		{
		}

		public ScreenMenu (ApplicationBase<T> application, string title, string text)
			: base(application, title, text)
		{
		}
	}
}