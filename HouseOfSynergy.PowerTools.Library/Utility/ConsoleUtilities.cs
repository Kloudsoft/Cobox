using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using HouseOfSynergy.PowerTools.Library.Win32Api;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class ConsoleUtilities
	{
		public const int SWP_NOSIZE = 1;
		public const int SW_MAXIMIZE = 3;

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllocConsole ();

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FreeConsole ();

		[DllImport("user32.dll")]
		private static extern bool ShowWindow (IntPtr hWnd, int cmdShow);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern IntPtr GetConsoleWindow ();

		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		private static extern IntPtr SetWindowPos (IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		public static void MaximizeConsoleWindow ()
		{
			using (var process = Process.GetCurrentProcess())
			{
				ConsoleUtilities.ShowWindow(process.MainWindowHandle, ConsoleUtilities.SW_MAXIMIZE);
			}
		}

		public static void PositionConsole (int left, int top, int width, int height)
		{
			ConsoleUtilities.SetWindowPos
			(
				ConsoleUtilities.GetConsoleWindow(),
				0,
				left,
				top,
				width,
				height,
				ConsoleUtilities.SWP_NOSIZE
			);
			//ConsoleUtilities.SetWindowPos
			//(
			//    ConsoleUtilities.GetConsoleWindow(),
			//    0,
			//    Screen.PrimaryScreen.WorkingArea.Width / 2,
			//    Screen.PrimaryScreen.WorkingArea.Top,
			//    Screen.PrimaryScreen.WorkingArea.Width / 2,
			//    Screen.PrimaryScreen.WorkingArea.Height,
			//    ConsoleUtilities.SWP_NOSIZE
			//);
		}
	}
}