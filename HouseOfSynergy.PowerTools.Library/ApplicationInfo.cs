using System;
using System.Linq;
using System.Reflection;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library
{
	public sealed class ApplicationInfo:
		Singleton<ApplicationInfo>,
		IApplicationInfo
	{
		private ApplicationInfo ()
		{
		}

		public string ProductName { get { return ("Power Tools"); } }
		public Version ProductVersion { get { return (Assembly.GetAssembly(typeof(ApplicationInfo)).GetName().Version); } }
		public Guid ProductGuid { get { return (Guid.Parse("{E436C59E-84B4-4B9B-A05B-B013E4A0F09A}")); } }

		public string  AssemblyName { get { return ("Library"); } }
		public Version AssemblyVersion { get { return (Assembly.GetAssembly(typeof(ApplicationInfo)).GetName().Version); } }
		public Guid    AssemblyGuid { get { return (Guid.Parse("{E436C59E-84B4-4B9B-A05B-B013E4A0F09A}")); } }

		public string CompanyName { get { return ("House of Synergy (SMC-Private) Limited"); } }
		public string ManufacturerName { get { return ("House of Synergy (SMC-Private) Limited"); } }
		public Uri ManufacturerUrl { get { return (new Uri("http://www.houseofsynergy.com/")); } }
		public string Copyright { get { return ("Copyright © " + DateTime.Now.Year.ToString().PadLeft(4, '0') + " " + this.CompanyName + ". All rights reserved."); } }
	}
}