using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using HouseOfSynergy.PowerTools.Library;
using HouseOfSynergy.PowerTools.Library.Utility;
using HouseOfSynergy.PowerTools.Library.Attributes;
using System.Net;

namespace HouseOfSynergy.AffinityDms.Library
{
	public static class AffinityConfiguration
	{
        private static readonly string DnsName = Dns.GetHostName().ToLower();

        public static List<string> Messages { get; private set; }
        public static ApplicationInfo ApplicationInfo { get; private set; }

        static AffinityConfiguration ()
		{
            AffinityConfiguration.Messages = new List<string>();
			AffinityConfiguration.ApplicationInfo = AffinityConfiguration.GetApplicationInfo(MethodBase.GetCurrentMethod().ReflectedType.Assembly);
		}

		public static DeploymentLocation DeploymentLocation
		{
			get
			{
				//return (DeploymentLocation.Live);
				switch (AffinityConfiguration.DnsName)
				{
					case @"w10eevs2015ceu2": { return (DeploymentLocation.W10EeVs2015CeU2); }
					case @"hoslaptophp": { return (DeploymentLocation.HosHp); }
					case @"monster": { return (DeploymentLocation.HosMonster); }
					case @"raheelkhan-pc": { return (DeploymentLocation.HosRkPc); }
					case @"kausarpc": { return (DeploymentLocation.HosKausar); }
					case @"CEO-PC": { return (DeploymentLocation.BtsRizwan); }
					case @"uzma-pc": { return (DeploymentLocation.BtsUzma); }
					case @"benztech-danish": { return (DeploymentLocation.BtsDanish); }
					case @"benztech-pc": { return (DeploymentLocation.BtsSaleem); }
					case @"benzfaraz-pc": { return (DeploymentLocation.BtsFaraz); }
					case @"home-pc": { return (DeploymentLocation.BtsUsmanJamal); }
					case @"fabeha-hdk": { return (DeploymentLocation.BtsFabehaMalik); }
                    case @"lenovo": { return (DeploymentLocation.KsManjuLenovo); }
                    case @"vaio_hfteh": { return (DeploymentLocation.KsThiya); }
                    case @"adminntech": { return (DeploymentLocation.KsAdmintech); }
                    default: { return (DeploymentLocation.Live); }
				}
			}
		}

		public static bool IsConfigurationDebug
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

		public static bool IsConfigurationRelease
		{
			get
			{
				return (!AffinityConfiguration.IsConfigurationDebug);
			}
		}

		public static ApplicationInfo GetApplicationInfo (Assembly assembly)
		{
			var info = new ApplicationInfo
			(
				(assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false).First() as AssemblyProductAttribute).Product,
				(assembly.GetCustomAttributes(typeof(ProductVersionAttribute), false).First() as ProductVersionAttribute).Version,
				(assembly.GetCustomAttributes(typeof(ProductGuidAttribute), false).First() as ProductGuidAttribute).Guid,
				(assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First() as AssemblyTitleAttribute).Title,
				Version.Parse((assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).First() as AssemblyInformationalVersionAttribute).InformationalVersion),
				Guid.Parse((assembly.GetCustomAttributes(typeof(GuidAttribute), false).First() as GuidAttribute).Value),
				(assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).First() as AssemblyCompanyAttribute).Company,
				(assembly.GetCustomAttributes(typeof(ProductManufacturerNameAttribute), false).First() as ProductManufacturerNameAttribute).Company,
				(assembly.GetCustomAttributes(typeof(ProductManufacturerUrlAttribute), false).First() as ProductManufacturerUrlAttribute).Uri,
				(assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).First() as AssemblyCopyrightAttribute).Copyright
			);

			return (info);
		}

		public static DirectoryInfo DirectoryApplicationData
		{
			get
			{
				var directory = "";

				directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.ManufacturerName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.CompanyName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.ProductName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.ProductVersion.ToString());
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.AssemblyName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.AssemblyVersion.ToString());

				if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

				return (new DirectoryInfo(directory));
			}
		}

		public static DirectoryInfo DirectoryApplicationDataTemp
		{
			get
			{
				var directory = AffinityConfiguration.DirectoryApplicationData.FullName;

				directory = Path.Combine(directory, "Temp");

				if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

				return (new DirectoryInfo(directory));
			}
		}
	}
}