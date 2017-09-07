using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IApplicationInfo
	{
		string ProductName { get; }
		Version ProductVersion { get; }
		Guid ProductGuid { get; }
		string AssemblyName { get; }
		Version AssemblyVersion { get; }
		Guid AssemblyGuid { get; }
		string ManufacturerName { get; }
		Uri ManufacturerUrl { get; }
		string CompanyName { get; }
		string Copyright { get; }
	}
}