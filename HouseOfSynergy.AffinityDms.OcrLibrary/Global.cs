using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Log;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public sealed class Global:
		GlobalBase
	{
		public bool Disposed { get; private set; }

		public ILogger Logger { get; private set; }
		public ApplicationInfo ApplicationInfo { get; private set; }

		protected override void OnInitialize ()
		{
			this.ApplicationInfo = AffinityConfiguration.GetApplicationInfo (Assembly.GetExecutingAssembly ());

			this.Logger = new Logger (Global.DirectoryApplicationData);
		}

		public static DirectoryInfo DirectoryApplicationData
		{
			get
			{
				var directory = "";

				directory = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);
				directory = Path.Combine (directory, AffinityConfiguration.ApplicationInfo.ManufacturerName);
				directory = Path.Combine (directory, AffinityConfiguration.ApplicationInfo.CompanyName);
				directory = Path.Combine (directory, AffinityConfiguration.ApplicationInfo.ProductName);
				directory = Path.Combine (directory, AffinityConfiguration.ApplicationInfo.ProductVersion.ToString ());
				directory = Path.Combine (directory, AffinityConfiguration.ApplicationInfo.AssemblyName);
				directory = Path.Combine (directory, AffinityConfiguration.ApplicationInfo.AssemblyVersion.ToString ());

				if (!Directory.Exists (directory)) { Directory.CreateDirectory (directory); }

				return (new DirectoryInfo (directory));
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
						try { this.Logger.Dispose (); }
						finally { this.Logger = null; }
					}
				}

				// Unmanaged.
			}

			base.Dispose (disposing);
		}
	}
}