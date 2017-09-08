using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Log;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
	public sealed class Global:
		GlobalBase
	{
		//====================================================================================================
		#region Constants.
		//====================================================================================================

		//====================================================================================================

		#endregion Constants.
		//====================================================================================================

		//====================================================================================================
		#region Members.
		//====================================================================================================

		public bool Disposed { get; private set; }

		//public ILogger Logger { get; private set; }
		public new Options Options { get; private set; }
		//public ApplicationInfo ApplicationInfo { get; private set; }

		//====================================================================================================
		#endregion Members.
		//====================================================================================================

		//====================================================================================================
		#region Constructors.
		//====================================================================================================

		public Global ()
			: base()
		{
		}

		//====================================================================================================
		#endregion Constructors.
		//====================================================================================================

		//====================================================================================================
		#region Initializers.
		//====================================================================================================

		protected override void OnInitialize ()
		{
			this.ApplicationInfo = ApplicationInfo.GetApplicationInfo(Assembly.GetExecutingAssembly());

			this.Logger = new Logger(AffinityConfigurationDesktop.DirectoryApplicationData);

			this.Options = new Options();
			this.Options.Initialize(this);
			this.Options.Load();
			this.Options.Save();

			this.ReInitialize();
		}

		public void ReInitialize ()
		{
		}

		//====================================================================================================
		#endregion Initializers.
		//====================================================================================================

		//====================================================================================================
		#region IDisposable.
		//====================================================================================================

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				this.Disposed = true;

				if (disposing)
				{
					// Managed.
				}

				// Unmanaged.
			}

			base.Dispose(disposing);
		}

		//====================================================================================================
		#endregion IDisposable.
		//====================================================================================================
	}
}