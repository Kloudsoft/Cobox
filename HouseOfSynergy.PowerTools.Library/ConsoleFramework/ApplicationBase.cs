using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Threading;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	/// <summary>
	/// Represents an abstract Application class that can be used to implement custom console applications.
	/// </summary>
	/// <typeparam name="T">
	/// The type of application to create. T should always be the derived class extending this one.
	/// <example>
	/// Example of an implementation of <see cref="Application&lt;T&gt;"/>.
	/// <code>
	/// public sealed class DerivedApplication: Application<DerivedApplication>
	/// {
	///		...
	/// }
	/// </code>
	/// </example>
	/// </typeparam>
	public abstract class ApplicationBase<T>:
		SingletonDisposable<T>,
		IConsoleApplication,
		IApplicationInfoProvider,
		IConsoleProvider,
		IDisposable
		where T: SingletonDisposable<T>
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private bool Loaded { get; set; }
		private IScreen<T> ScreenDefault { get; set; }
		private List<string> _CommandLineArguments { get; set; }

		/// <summary>
		/// The main application screen to launch.
		/// </summary>
		public IScreen<T> Screen { get; protected set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		protected ApplicationBase ()
		{
			this.Console = new ConsoleWrapper();
			this._CommandLineArguments = new List<string>();
			this.CommandLineArguments = new ReadOnlyCollection<string>(this._CommandLineArguments);

			this.ApplicationInfo = new Utility.ApplicationInfo
			(
				"Power Tools",
				Assembly.GetExecutingAssembly().GetName().Version,
				Guid.Empty,
				"Console Framework",
				Assembly.GetExecutingAssembly().GetName().Version,
				Guid.Empty,
				"House of Synergy (SMC-Private) Limited",
				"House of Synergy (SMC-Private) Limited",
				new Uri("http://www.houseofsynergy.com/"),
				"Copyright © " + DateTime.Now.ToString("yyyy") + " House of Synergy (SMC-Private) Limited. All rights reserved."
			);

			var assembly = Assembly.GetExecutingAssembly();
			using (var stream = assembly.GetManifestResourceStream("HouseOfSynergy.PowerTools.Library.ConsoleFramework.ConsoleFramework.txt"))
			{
				using (var reader = new StreamReader(stream))
				{
					this.ScreenDefault = this.Screen = new ScreenInfo<T>(this, "Sample Info Screen.", reader.ReadToEnd());
				}
			}
		}

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		protected virtual void OnLoad () { }
		protected virtual void OnUnLoad () { }
		private void UnLoad () { if (this.Loaded) { this.OnUnLoad(); } }
		private void Load () { if (!this.Loaded) { this.Loaded = true; this.OnLoad(); } }

		public void Run (IEnumerable<string> args)
		{
			this._CommandLineArguments.AddRange(args);

			this.Run();
		}

		public void Run ()
		{
			Exception exception = null;
			ApplicationMutex mutex = null;

			this.Console.Title = this.ApplicationInfo.ProductName;
			ConsoleUtilities.PositionConsole
			(
				System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Left,
				System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Top,
				System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2,
				System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
			);

			this.Console.Write(this.ApplicationInfo.ProductName);
			this.Console.Write(" [Version ");
			this.Console.Write(this.ApplicationInfo.ProductVersion.ToString());
			this.Console.Write("]");
			this.Console.WriteLine();
			this.Console.Write(this.ApplicationInfo.CompanyName);
			this.Console.WriteLine();
			this.Console.Write(this.ApplicationInfo.Copyright);
			this.Console.WriteLine();
			this.Console.WriteLine();

			if (this.AllowMultipleInstances)
			{
				this.Load();
			}
			else
			{
				if (this.ApplicationInfo.ProductGuid == Guid.Empty)
				{
					throw (new ConsoleFrameworkException("If [Application.AllowMultipleInstances] is overriden and set to false, [Application.ApplicationInfo.ProductGuid] must be set to a value other than [Guid.Empty]."));
				}
				else
				{
					if (ApplicationMutex.Create(this.ApplicationInfo, out mutex, out exception))
					{
						if (mutex.Created)
						{
							this.Load();
						}
						else
						{
							this.Console.Write("An instance of this application is already running.");
							this.Console.WriteLine();
							this.Console.WriteLine();
						}
					}
					else
					{
						this.Console.Write("An instance of this application is already running.");
						this.Console.WriteLine();
						this.Console.WriteLine();
					}
				}
			}

			if (this.Loaded)
			{
				if (this.Screen == null)
				{
					this.Console.Write("Press any key to exit...");
					this.Console.ReadKey();
				}
				else
				{
					if (this.Screen == this.ScreenDefault)
					{
						this.Screen.Render(this);

						this.Console.WriteLine();
						this.Console.WriteLine();
						this.Console.Write("Press any key to exit...");
						this.Console.ReadKey();
					}
					else
					{
						this.Screen.Run(this);
					}
				}
			}
			else
			{
				this.Console.Write("Press any key to exit...");
				this.Console.ReadKey();
			}

			this.UnLoad();

			if (mutex != null)
			{
				try { mutex.Dispose(); }
				finally { mutex = null; }
			}
		}

		#endregion Methods.

		#region Interface: IConsoleApplication.

		//====================================================================================================
		// Interface: IConsoleApplication.
		//====================================================================================================

		/// <summary>
		/// Determines whether multiple instances of derived applications can be run simultaneously.
		/// </summary>
		public virtual bool AllowMultipleInstances { get { return (true); } }

		/// <summary>
		/// The command line arguments passed to the application. This is a <see cref="ReadOnlyCollection&lt;T&gt;"/> of strings.
		/// </summary>
		public ReadOnlyCollection<string> CommandLineArguments { get; private set; }

		/// <summary>
		/// The application info object containing descriptive information about the application and a unique Guid if applicable.
		/// <example>
		/// <code>
		/// this.ApplicationInfo = new ApplicationInfo
		/// (
		///		productName : "Project Euler",
		///		productVersion : new HouseOfSynergy.PowerTools.Library.Utility.Version(1, 0, 0, 0),
		///		companyName : "House of Synergy (SMC-Private) Limited",
		///		manufacturerName : "House of Synergy (SMC-Private) Limited",
		///		manufacturerUrl : new Uri("http://www.houseofsynergy.com/"),
		///		productGuid : new Guid("{AF07318A-33D8-4B7D-9DD5-4C60341CE1C1}"),
		///		copyright : "Copyright (c) " + DateTime.Now.ToString("yyyy") + " House of Synergy (SMC-Private) Limited. All rights reserved."
		///	);
		/// </code>
		/// </example>
		/// </summary>
		public IApplicationInfo ApplicationInfo { get; protected set; }

		/// <summary>
		/// A console wrapper object for streamlined access.
		/// </summary>
		public ConsoleWrapper Console { get; private set; }

		#endregion Interface: IConsoleApplication.

		#region Interface: IDisposable.

		//====================================================================================================
		// Interface: IDisposable.
		//====================================================================================================

		private bool Disposed = false;

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					try
					{
						// Dispose managed resources.
					}
					catch
					{
					}
				}

				// Dispose unmanaged resources.
				try { }
				catch { }
				finally { }

				this.Disposed = true;
			}
		}

		#endregion Interface: IDisposable.
	}
}