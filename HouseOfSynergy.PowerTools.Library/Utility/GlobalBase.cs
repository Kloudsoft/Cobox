using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Log;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public class GlobalAlreadyInitializedException:
			Exception
	{
		public GlobalAlreadyInitializedException () : base("The global class has already been intialized.") { }
		public GlobalAlreadyInitializedException (string message) : base(message) { }
		public GlobalAlreadyInitializedException (string message, Exception innerException) : base(message, innerException) { }
	}

	public abstract class GlobalBase:
		Disposable
	{
		private bool Disposed = false;

		public bool Initialized { get; private set; }
		public ILogger Logger { get; protected set; }
		public IOptions Options { get; protected set; }
		public ApplicationInfo ApplicationInfo { get; protected set; }

		protected abstract void OnInitialize ();

		public void Initialize ()
		{
			if (this.Initialized)
			{
				throw (new GlobalAlreadyInitializedException());
			}
			else
			{
				this.OnInitialize();

				this.Initialized = true;
			}
		}

		public bool Initialize (out Exception exception)
		{
			var result = false;

			exception = null;

			try
			{
				if (this.Initialized)
				{
					throw (new GlobalAlreadyInitializedException());
				}
				else
				{
					this.OnInitialize();

					result = true;
					this.Initialized = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public DirectoryInfo DirectoryApplicationData
		{
			get
			{
				var directory = "";

				directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				directory = Path.Combine(directory, this.ApplicationInfo.ManufacturerName);
				directory = Path.Combine(directory, this.ApplicationInfo.CompanyName);
				directory = Path.Combine(directory, this.ApplicationInfo.ProductName);
				directory = Path.Combine(directory, this.ApplicationInfo.ProductVersion.ToString());
				directory = Path.Combine(directory, this.ApplicationInfo.AssemblyName);
				directory = Path.Combine(directory, this.ApplicationInfo.AssemblyVersion.ToString());

				return (new DirectoryInfo(directory));
			}
		}

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				this.Disposed = true;

				if (disposing)
				{
					// Managed.

					if (this.Logger != null)
					{
						try { this.Logger.Dispose(); }
						finally { this.Logger = null; }
					}
				}

				// Unmanaged.
			}

			base.Dispose(disposing);
		}
	}
}