using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public interface IScreen
	{
		string Text { get; }
		string Title { get; }

		void Run (IConsoleApplication application);
		void Render (IConsoleApplication application);
	}

	public interface IScreen<T>:
		IScreen
		where T: SingletonDisposable<T>
	{
		ApplicationBase<T> ApplicationBase { get; }
	}
}