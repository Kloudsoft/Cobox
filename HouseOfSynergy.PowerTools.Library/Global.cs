using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library
{
	internal sealed class Global:
		Singleton<Global>
	{
		public ApplicationInfo ApplicationInfo { get; private set; }

		private Global ()
		{
			this.ApplicationInfo = ApplicationInfo.Instance;
		}

		public bool Debug
		{
			get
			{
#if (DEBUG)
				return (true);
#else
				return (false);
#endif
			}
		}

		public bool Release { get { return (!this.Debug); } }
	}
}