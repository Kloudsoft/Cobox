using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public static class Ole32
	{
		public enum CoInit:
			uint
		{
			COINIT_MULTITHREADED = 0x0, // Initializes the thread for multi-threaded object concurrency.
			COINIT_APARTMENTTHREADED = 0x2, // Initializes the thread for apartment-threaded object concurrency.
			COINIT_DISABLE_OLE1DDE = 0x4, // Disables DDE for OLE1 support.
			COINIT_SPEED_OVER_MEMORY = 0x8, // Trade memory for speed.
		}

		[DllImport("ole32.dll", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		public static extern int CoInitializeEx ([In, Optional, MarshalAs(UnmanagedType.U4)]  uint pvReserved, [In, MarshalAs(UnmanagedType.U4)]  CoInit dwCoInit);

		public static int CoInitializeEx ([In, MarshalAs(UnmanagedType.U4)]  CoInit dwCoInit) { return (Ole32.CoInitializeEx(0, dwCoInit)); }

		[DllImport("ole32.dll")]
		public static extern void CoUninitialize ();
	}
}