using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public class ScreenInfo<T>:
		ScreenModal<T>
		where T: SingletonDisposable<T>
	{
		public ScreenInfo (ApplicationBase<T> application) : base(application) { }
		public ScreenInfo (ApplicationBase<T> application, string text) : base(application, text) { }
		public ScreenInfo (ApplicationBase<T> application, string title, string text) : base(application, title, text) { }
	}
}