using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public abstract class Screen<T>:
		IRenderable,
		IScreen<T>,
		IScreen
		where T: SingletonDisposable<T>
	{
		public string Text { get; protected set; }
		public string Title { get; protected set; }
		public ApplicationBase<T> ApplicationBase { get; private set; }

		protected Screen (ApplicationBase<T> applicationBase)
		{
			Console.CursorVisible = false;

			this.Text = "";
			this.Title = "";
			this.ApplicationBase = applicationBase;
		}

		protected Screen (ApplicationBase<T> application, string text)
			: this(application)
		{
			this.Text = text ?? "";
			this.Title = application.ApplicationInfo.ProductName;
		}

		protected Screen (ApplicationBase<T> application, string title, string text)
			: this(application)
		{
			this.Text = text ?? "";
			this.Title = title ?? "";
		}

		internal void Initialize (IConsoleApplication application)
		{
			this.ApplicationBase = (ApplicationBase<T>) application;
		}

		protected virtual void OnRun (IConsoleApplication application) { }
		protected virtual void OnClose (IConsoleApplication application) { }

		public void Run (IConsoleApplication application)
		{
			this.OnRun(application);

			//this.Render(application);
			//application.Console.WriteLine();
			//application.Console.WriteLine();

			//this.WaitForInput(this.ApplicationBase);

			this.OnClose(application);
		}

		public virtual void Render (IConsoleApplication application)
		{
			Console.Clear();
			application.Console.Title = application.ApplicationInfo.ProductName;

			application.Console.Write(application.ApplicationInfo.ProductName);
			application.Console.Write(" ");
			application.Console.Write("[");
			application.Console.Write("Version");
			application.Console.Write(" ");
			application.Console.Write(application.ApplicationInfo.ProductVersion.ToString());
			application.Console.Write("]");
			application.Console.WriteLine();
			application.Console.Write(application.ApplicationInfo.CompanyName);
			application.Console.WriteLine();
			application.Console.Write(application.ApplicationInfo.Copyright);
			application.Console.WriteLine();
			application.Console.WriteLine();

			application.Console.Write(this.Text);
		}

		public virtual void WaitForInput (IConsoleApplication application) { }
	}
}