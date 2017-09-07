using System;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Threading
{
	public class ApplicationMutex:
		Disposable
	{
		private Mutex MutexObject { get; set; }
		public bool Created { get; private set; }
		public bool Disposed { get; private set; }
		public object SyncRoot { get; private set; }
		public IApplicationInfo ApplicationInfo { get; private set; }

		private ApplicationMutex (IApplicationInfo applicationInfo)
		{
			this.Created = false;
			this.Disposed = false;
			this.MutexObject = null;
			this.SyncRoot = new object();
			this.ApplicationInfo = applicationInfo;
		}

		public string MutexGuidString { get { return (this.ApplicationInfo.ProductGuid.ToString(GuidUtilities.EnumGuidFormat.Database)); } }
		public string MutexName { get { return (string.Format(@"[{0}].[{1}].[{2}].[{3}]", this.ApplicationInfo.CompanyName, this.ApplicationInfo.ProductName, this.ApplicationInfo.ProductVersion, this.MutexGuidString)); } }

		public static bool Create (IApplicationInfo applicationInfo, out ApplicationMutex mutex, out Exception exception)
		{
			if (applicationInfo == null) { throw (new ArgumentNullException("applicationInfo")); }

			var result = false;
			var created = false;
			var applicationMutex = new ApplicationMutex(applicationInfo);

			mutex = null;
			exception = null;

			try
			{
				applicationMutex.MutexObject = new Mutex(true, applicationMutex.MutexName, out created);

				result = true;
				mutex = applicationMutex;
				applicationMutex.Created = created;
				//applicationMutex.Created = true;//created; // TODO: Revert!!!
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.

					if (this.MutexObject != null)
					{
						try
						{
							if (this.Created) { this.MutexObject.ReleaseMutex(); }
						}
						finally
						{
							try { this.MutexObject.Dispose(); }
							finally { this.MutexObject = null; }
						}
					}
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}
	}
}