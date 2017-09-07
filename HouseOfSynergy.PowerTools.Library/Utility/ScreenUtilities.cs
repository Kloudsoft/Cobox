using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class ScreenUtilities
	{
		[DllImport("user32.dll")]
		public static extern bool SetProcessDPIAware ();
	}
}