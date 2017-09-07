using System.Diagnostics;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class Compatibility
	{
		public static bool IsRunningOnAcPower { get { return (System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online); } }

		public static bool IsRunningOnBattery { get { return (System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus != System.Windows.Forms.PowerLineStatus.Online); } }

		public static bool IsSessionNative { get { return (!System.Windows.Forms.SystemInformation.TerminalServerSession); } }

		public static bool IsSessionTerminalServices { get { return (System.Windows.Forms.SystemInformation.TerminalServerSession); } }

		public static bool IsOsCompatible { get { return ((System.Environment.OSVersion.Platform == System.PlatformID.Win32NT) && (System.Environment.OSVersion.Version.Major >= 5)); } }

		public static System.Windows.Forms.BootMode BootMode { get { return (System.Windows.Forms.SystemInformation.BootMode); } }

		public static bool IsBootModeCompatible { get { return (BootMode == System.Windows.Forms.BootMode.Normal); } }

		public static bool IsDebuggerAttached { get { return ((HouseOfSynergy.PowerTools.Library.Global.Instance.Debug) || (HouseOfSynergy.PowerTools.Library.Global.Instance.Release && !System.Diagnostics.Debugger.IsAttached)); } }

		public static bool IsNetworkAvailable { get { return (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()); } }

		public static bool IsInternetAvailable
		{
			get
			{
				string html = "";

				try
				{
					using (var client = new System.Net.WebClient())
					{
						html = client.DownloadString(HouseOfSynergy.PowerTools.Library.Global.Instance.ApplicationInfo.ManufacturerUrl.ToString());
					}

					return (true);
				}
				catch
				{
					return (false);
				}
			}
		}

		public static bool IsCompatible ()
		{
			if (HouseOfSynergy.PowerTools.Library.Global.Instance.Debug)
			{
				return (true);
			}
			else
			{
				return
				(
					IsDebuggerAttached
					&&
					IsOsCompatible
					&&
					IsBootModeCompatible
					&&
					IsRunningOnAcPower
					&&
					IsSessionNative
					&&
					IsNetworkAvailable
					&&
					IsInternetAvailable
				);
			}
		}

		public static string Message
		{
			get
			{
				string message = "";

				message
					= "This application is not compatible with the current operating system"
					+ " configuration. Please contact your system administrator for support."
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ "Your system is currently running:"
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ " - "
					+ System.Environment.OSVersion.VersionString
					+ (System.Environment.Is64BitOperatingSystem ? " (64 Bit)" : " (32 Bit)")
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ " - Boot Mode: "
					+ BootMode.ToString()
					+ (BootMode == System.Windows.Forms.BootMode.Normal ? " (OK)" : " (Not Allowed)")
					+ System.Environment.NewLine
					+ " - Power Source: "
					+ (System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online ? "AC Power" : (System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Offline ? "Battery Power" : "Unknonwn"))
					+ (System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online ? " (OK)" : " (Not Allowed)")
					+ System.Environment.NewLine
					+ " - Terminal: "
					+ (IsSessionTerminalServices ? "Remote Desktop" : "Native")
					+ (!IsSessionTerminalServices ? " (OK)" : " (Not Allowed)")
					+ System.Environment.NewLine
					+ " - Network: "
					+ (IsNetworkAvailable ? "Available (OK)" : "Not Available (OK)")
					+ System.Environment.NewLine
					+ " - Internet: "
					+ (IsInternetAvailable ? "Available (OK)" : "Not Available (OK)")
					;

				if (!IsOsCompatible)
				{
					message
						= message
						+ System.Environment.NewLine
						+ System.Environment.NewLine
						+ "One of the following platforms is required:"
						+ System.Environment.NewLine
						+ " - Windows XP SP3, Vista SP1 or later, 7, 8"
						;
				}

				return (message);
			}
		}

		/// <summary>
		/// ~~~TODO~~~: Check for VNC and other remote servers.
		/// </summary>
		/// <returns></returns>
		public static bool IsRunningRemoteSession ()
		{
			bool result = false;
			string command = "";
			string arguments = "";
			Process process = null;
			ProcessStartInfo processStartInfo = null;

			command = "netstat";
			arguments = "";

			processStartInfo = new ProcessStartInfo(command, arguments);
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardOutput = true;

			process = Process.Start(processStartInfo);
			process.WaitForExit();

			// Read and discard the first 4 lines. They don't contain any information we need to get
			for (int i = 0; i < 4; i++)
				process.StandardOutput.ReadLine();

			while (true)
			{
				string strLine = process.StandardOutput.ReadLine();
				if (strLine == null)
				{
					break;
				}
				else
				{
					// Analyze the line
					// Line is in following structure:
					// Protocol (TCP/UDP)   Local Address(host:port) Foreign Address(host:port) State(ESTABLISHED, ...)
				}
			}

			return (result);
		}
	}
}