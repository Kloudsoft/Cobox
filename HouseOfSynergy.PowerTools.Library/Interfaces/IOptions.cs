using System;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using Microsoft.Win32;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public interface IOptions:
		IInitializable,
		ICloneable<IOptions>,
		ICopyable<IOptions>
	{
		bool Load ();
		bool Load (bool throwOnError = true);
		bool Save ();
		bool Save (bool throwOnError = true);

		RegistryKey GetRegistryKeyApplication ();
		RegistryKey GetRegistryKeyApplicationOptions ();
	}
}