using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class ScreenModal<T>:
		Screen<T>
		where T: SingletonDisposable<T>
	{
		public ConsoleKeyInfo ConsoleKeyInfo { get; private set; }

		protected ScreenModal (ApplicationBase<T> application)
			: this(application, application.ApplicationInfo.ProductName, "")
		{
		}

		protected ScreenModal (ApplicationBase<T> application, string text)
			: this(application, application.ApplicationInfo.ProductName, text)
		{
		}

		protected ScreenModal (ApplicationBase<T> application, string title, string text)
			: base(application, title, text)
		{
			this.ConsoleKeyInfo = new ConsoleKeyInfo((char) 27, ConsoleKey.Escape, false, false, false);
		}

		public override void WaitForInput (IConsoleApplication application)
		{
			while (true)
			{
				if (application.Console.KeyAvailable)
				{
					var key = application.Console.ReadKey(true);

					if (key.Key == this.ConsoleKeyInfo.Key)
					{
						break;
					}
				}

				System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.1D));
			}
		}
	}
}