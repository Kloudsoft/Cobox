using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.Screens
{
	public sealed class ScreenTask<T>:
		Screen<T>
		where T: SingletonDisposable<T>
	{
		public ScreenTask (ApplicationBase<T> application)
			: this(application, application.ApplicationInfo.ProductName, "")
		{
		}

		public ScreenTask (ApplicationBase<T> application, string text)
			: this(application, application.ApplicationInfo.ProductName, text)
		{
		}

		public ScreenTask (ApplicationBase<T> application, string title, string text)
			: base(application, title, text)
		{
		}
	}
}