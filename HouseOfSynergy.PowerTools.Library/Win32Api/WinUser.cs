using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public static class WinUser
	{
		public const int WM_COPYDATA = 0x4A;

		public struct COPYDATASTRUCT
		{
			public int cbData;
			public IntPtr dwData;
			[MarshalAs(UnmanagedType.LPStr)]
			public string lpData;
		}
	}
}