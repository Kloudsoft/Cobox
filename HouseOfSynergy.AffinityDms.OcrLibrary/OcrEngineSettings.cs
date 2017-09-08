using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.PowerTools.Library.Utility;
using Leadtools;
using HouseOfSynergy.PowerTools.Library.Log;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public sealed class OcrEngineSettings:
		Disposable
	{
		private static readonly int LengthPadding = long.MaxValue.ToString ().Length;

		private bool Disposed = false;

		public ILogger Logger { get; private set; }

		public string LicenseKey { get; private set; }
        public FileInfo LicenseFile { get; private set; }

		private DirectoryInfo PathData { get; set; }
		public DirectoryInfo PathRuntime { get; private set; }
        public DirectoryInfo WorkDirectory { get; set; }

        public OcrEngineSettings (ILogger logger, string licenseKey, string licenseFile, string pathData, string pathRuntime)
		{
			if (logger == null) { throw (new ArgumentNullException ("logger")); }

            this.Logger = logger;

            this.Logger.Write("Entered OcrEngineSettings constructor.");

            if (string.IsNullOrWhiteSpace (licenseKey)) { throw (new ArgumentException ("The argument [licenseKey] is null or empty.", "licenseKey")); }
			if (string.IsNullOrWhiteSpace (licenseFile)) { throw (new ArgumentException ("The argument [licenseFile] is null or empty.", "licenseFile")); }
			if (string.IsNullOrWhiteSpace (pathData)) { throw (new ArgumentException ("The argument [pathData] is null or empty.", "pathData")); }
			if (string.IsNullOrWhiteSpace (pathRuntime)) { throw (new ArgumentException ("The argument [pathRuntime] is null or empty.", "pathRuntime")); }

			if (!File.Exists (licenseFile)) { throw (new ArgumentException ("The argument [licenseFile] referes to a file that does not exist.", "licenseFile")); }

			if (!Directory.Exists (pathData)) { Directory.CreateDirectory(pathData); }
			if (!Directory.Exists (pathData)) { throw (new ArgumentException ("The argument [pathData] referes to a directory that does not exist.", "pathData")); }

			if (!Directory.Exists (pathRuntime)) { Directory.CreateDirectory(pathRuntime); }
			if (Directory.GetFiles(pathRuntime).Length == 0) { throw (new ArgumentException ("The argument [pathRuntime] referes to a directory without any files.", "pathRuntime")); }

			this.LicenseKey = licenseKey;
			this.LicenseFile = new FileInfo (licenseFile);

			this.PathData = new DirectoryInfo (pathData);
			this.PathRuntime = new DirectoryInfo (pathRuntime);

            this.Logger.Write($"License File: {this.LicenseFile.FullName}, Key: {this.LicenseKey}.");
            RasterSupport.SetLicense (this.LicenseFile.FullName, this.LicenseKey);

			var message = "";
			var unlocked = true;
			var rasterSupportTypes = new RasterSupportType []
			{
				RasterSupportType.Forms,
				RasterSupportType.DocumentWriters,
				RasterSupportType.OcrAdvantage,
			};

			foreach (var rasterSupportType in rasterSupportTypes)
			{
				if (RasterSupport.IsLocked (rasterSupportType)) { unlocked = false; message += Environment.NewLine + $"The raster support type [{rasterSupportType}] did not unlock."; }
			}

            if (!unlocked)
            {
                this.Logger.Write($"Unlocking Errors: {message}.");
            }

            if (!unlocked) { throw (new Exception("The following errors occured:" + Environment.NewLine + message)); }
		}

		public DirectoryInfo GetPathData ()
		{
			var directory = this.PathData;
			if (!directory.Exists) { directory.Create (); }
			return (directory);
		}

		public DirectoryInfo GetPathDataTenants ()
		{
			var directory = new DirectoryInfo (Path.Combine (this.GetPathData ().FullName, "Tenants"));
			if (!directory.Exists) { directory.Create (); }
			return (directory);
		}

        public TemporaryFolderContainer CreateTempFolderContainer(Tenant tenant)
        {
            return (new TemporaryFolderContainer(this.GetPathDataTenant(tenant)));
        }

        public DirectoryInfo GetPathDataTenant (Tenant tenant)
		{
			var directory = new DirectoryInfo (Path.Combine (this.GetPathDataTenants ().FullName, tenant.Id.ToString ().PadLeft (OcrEngineSettings.LengthPadding, '0')));
			if (!directory.Exists) { directory.Create (); }
			return (directory);
		}

		public DirectoryInfo GetPathDataTenantTemplates (Tenant tenant)
		{
            var directory = new DirectoryInfo(Path.Combine (this.GetPathDataTenant (tenant).FullName, "Templates"));
            //var directory = new DirectoryInfo(@"D:\Projects\Affinity\Source\HouseOfSynergy.AffinityDms.WebRole\App_Data\Tenants\0000000000000000001\Templates");//Path.Combine (this.GetPathDataTenant (tenant).FullName, "Templates"));
			if (!directory.Exists) { directory.Create (); }
			return (directory);
		}

		public DirectoryInfo GetPathDataTenantTemplate (Tenant tenant, Template template)
		{
			var directory = new DirectoryInfo (Path.Combine (this.GetPathDataTenantTemplates (tenant).FullName, template.Id.ToString ().PadLeft (OcrEngineSettings.LengthPadding, '0')));
			if (!directory.Exists) { directory.Create (); }
			return (directory);
		}

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.
					RasterSupport.ResetLicense ();
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose (disposing);
		}
	}
}
