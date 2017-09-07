using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public static class Win32Api
	{
		private static object SyncRoot = new object();

		private static List<IntPtr> WindowHandles { get; set; }

		public static bool FindChildWindow (string processName, string className, string windowText)
		{
			int length = 0;
			string text = "";
			bool result = false;
			StringBuilder builder = null;
			List<Process> processes = null;

			lock (Win32Api.SyncRoot)
			{
				builder = new StringBuilder();
				processes = Process.GetProcessesByName(processName).ToList();
				Win32Api.WindowHandles = new List<IntPtr>();

				foreach (Process process in processes)
				{
					foreach (ProcessThread thread in process.Threads)
					{
						User32.EnumWindows(Win32Api.FindChildWindowCallback, thread.Id);
					}
				}

				foreach (IntPtr handle in Win32Api.WindowHandles)
				{
					length = 1000;
					builder = new StringBuilder(length);
					length = User32.GetClassName(handle, builder, length);

					if (length > 0)
					{
						text = User32.GetWindowText(handle);

						result
							|= (builder.ToString().Trim().ToLower() == className.Trim().ToLower())
							&& (text.Trim().ToLower() == windowText.Trim().ToLower())
							;

						if (result)
							break;
					}
				}
			}

			return (result);
		}

		private static int FindChildWindowCallback (IntPtr hWnd, int lParam)
		{
			int threadID = 0;
			int processID = 0;

			threadID = User32.GetWindowThreadProcessId(hWnd, out processID);

			if (threadID == lParam)
			{
				Win32Api.WindowHandles.Add(hWnd);

				User32.EnumChildWindows(hWnd, Win32Api.FindChildWindowCallback, threadID);
			}

			return (1);
		}
	}
}