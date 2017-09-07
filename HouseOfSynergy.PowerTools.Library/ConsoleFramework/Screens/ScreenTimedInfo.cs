using System;
using System.Diagnostics;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class ScreenTimedInfo<T>:
		ScreenInfo<T>
		where T: SingletonDisposable<T>
	{
		private Stopwatch Stopwatch { get; set; }
		public TimeSpan Elapsed { get; private set; }

		public ScreenTimedInfo (ApplicationBase<T> application)
			: base(application)
		{
			this.Elapsed = TimeSpan.Zero;
			this.Stopwatch = new Stopwatch();
		}

		public ScreenTimedInfo (ApplicationBase<T> application, string text)
			: base(application, text)
		{
			this.Elapsed = TimeSpan.Zero;
			this.Stopwatch = new Stopwatch();
		}

		public ScreenTimedInfo (ApplicationBase<T> application, string title, string text)
			: base(application, title, text)
		{
			this.Elapsed = TimeSpan.Zero;
			this.Stopwatch = new Stopwatch();
		}

		protected override void OnRun (IConsoleApplication application)
		{
			this.Stopwatch.Restart();
		}

		protected override void OnClose (IConsoleApplication application)
		{
			this.Stopwatch.Stop();
		}

		public override void WaitForInput (IConsoleApplication application)
		{
			this.WaitForInput(application, TimeSpan.FromSeconds(5));
		}

		public virtual void WaitForInput (IConsoleApplication application, TimeSpan timeout)
		{
			Stopwatch watch = null;
			ConsoleKeyInfo key = default(ConsoleKeyInfo);
			ConsolePosition positionOld = default(ConsolePosition);
			ConsolePosition positionNew = default(ConsolePosition);

			watch = Stopwatch.StartNew();

			if (timeout > TimeSpan.Zero)
			{
				Console.WriteLine();
				Console.WriteLine();
				positionOld = new ConsolePosition(Console.CursorLeft, Console.CursorTop);
				Console.Write("This screen will automatically close in " + timeout.ToString() + ".");
			}

			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Press escape to return to the previous screen...");
			positionNew = new ConsolePosition(Console.CursorLeft, Console.CursorTop);

			do
			{
				if (timeout > TimeSpan.Zero)
				{
					Console.SetCursorPosition(positionOld.Left, positionOld.Top);
					Console.Write("This screen will automatically close in " + ((timeout + TimeSpan.FromSeconds(1)) - watch.Elapsed).ToString(@"hh\:mm\:ss") + ".");
					Console.SetCursorPosition(positionNew.Left, positionNew.Top);
				}

				if (Console.KeyAvailable)
				{
					key = Console.ReadKey(true);
				}
				else
				{
					System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.10D));
				}
			}
			while
			(
				(key.Key != ConsoleKey.Escape)
				&&
				((timeout > TimeSpan.Zero) && (timeout > (watch.Elapsed - TimeSpan.FromSeconds(0.5D))))
			);

			watch.Stop();
		}
	}
}