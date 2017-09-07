using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IGlobal:
		IDisposable
	{
		bool Initialized { get; }
		IApplicationInfo ApplicationInfo { get; }

		void Initialize ();
	}
}