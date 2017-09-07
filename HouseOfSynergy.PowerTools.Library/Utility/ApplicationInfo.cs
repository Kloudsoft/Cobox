using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using HouseOfSynergy.PowerTools.Library.Attributes;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public class ApplicationInfo:
		IApplicationInfo
	{
		public string ProductName { get; private set; }
		public Version ProductVersion { get; private set; }
		public Guid ProductGuid { get; private set; }
		public string AssemblyName { get; private set; }
		public Version AssemblyVersion { get; private set; }
		public Guid AssemblyGuid { get; private set; }
		public string CompanyName { get; private set; }
		public string ManufacturerName { get; private set; }
		public Uri ManufacturerUrl { get; private set; }
		public string Copyright { get; private set; }

		public ApplicationInfo ()
		{
			this.ProductName = "Power Tools";
			this.ProductVersion = typeof(ApplicationInfo).Assembly.GetName().Version;
			this.ProductGuid = Guid.Parse("{B9B816B2-E7B2-4004-A187-D36F109324D1}");
			this.AssemblyName = "Library";
			this.AssemblyVersion = typeof(ApplicationInfo).Assembly.GetName().Version;
			this.AssemblyGuid = Guid.Parse("{B9B816B2-E7B2-4004-A187-D36F109324D1}");
			this.CompanyName = "House of Synergy (SMC-Private) Limited";
			this.ManufacturerName = "House of Synergy (SMC-Private) Limited";
			this.ManufacturerUrl = new Uri("http://www.houseofsynergy.com/");
			this.Copyright = "Copyright © " + DateTime.Now.ToString("yyyy") + " " + this.CompanyName + ". All rights reserved.";
		}

		public ApplicationInfo
		(
			string productName,
			Version productVersion,
			Guid productGuid,
			string assemblyName,
			Version assemblyVersion,
			Guid assemblyGuid,
			string companyName,
			string manufacturerName,
			Uri manufacturerUrl,
			string copyright
		)
		{
			this.ProductName = productName;
			this.ProductGuid = productGuid;
			this.ProductVersion = productVersion;
			this.AssemblyName = assemblyName;
			this.AssemblyGuid = assemblyGuid;
			this.AssemblyVersion = assemblyVersion;
			this.CompanyName = companyName;
			this.ManufacturerName = manufacturerName;
			this.ManufacturerUrl = manufacturerUrl;
			this.Copyright = copyright;
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
	}
}