using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public static class User32
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left, Top, Right, Bottom;

			public RECT (int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public RECT (System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

			public int X
			{
				get { return Left; }
				set { Right -= (Left - value); Left = value; }
			}

			public int Y
			{
				get { return Top; }
				set { Bottom -= (Top - value); Top = value; }
			}

			public int Height
			{
				get { return Bottom - Top; }
				set { Bottom = value + Top; }
			}

			public int Width
			{
				get { return Right - Left; }
				set { Right = value + Left; }
			}

			public Point Location
			{
				get { return new System.Drawing.Point(Left, Top); }
				set { X = value.X; Y = value.Y; }
			}

			public Size Size
			{
				get { return new System.Drawing.Size(Width, Height); }
				set { Width = value.Width; Height = value.Height; }
			}

			public static implicit operator Rectangle (RECT r)
			{
				return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
			}

			public static implicit operator RECT (Rectangle r)
			{
				return new RECT(r);
			}

			public static bool operator == (RECT r1, RECT r2)
			{
				return r1.Equals(r2);
			}

			public static bool operator != (RECT r1, RECT r2)
			{
				return !r1.Equals(r2);
			}

			public bool Equals (RECT r)
			{
				return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
			}

			public override bool Equals (object obj)
			{
				if (obj is RECT)
					return Equals((RECT) obj);
				else if (obj is System.Drawing.Rectangle)
					return Equals(new RECT((System.Drawing.Rectangle) obj));
				return false;
			}

			public override int GetHashCode ()
			{
				return ((System.Drawing.Rectangle) this).GetHashCode();
			}

			public override string ToString ()
			{
				return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
			}

			public static readonly RECT Empty = new RECT();
		}

		/// <summary>
		/// The current show state of the window. This member can be one of the following values.
		/// </summary>
		public enum ShowWindowCommands: int
		{
			/// <summary>
			/// Hides the window and activates another window.
			/// </summary>
			SW_HIDE = 0,

			/// <summary>
			/// Maximizes the specified window.
			/// </summary>
			SW_MAXIMIZE = 3,

			/// <summary>
			/// Minimizes the specified window and activates the next top-level window in the z-order.
			/// </summary>
			SW_MINIMIZE = 6,

			/// <summary>
			/// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
			/// </summary>
			SW_RESTORE = 9,

			/// <summary>
			/// Activates the window and displays it in its current size and position.</item>
			/// </summary>
			SW_SHOW = 5,

			/// <summary>
			/// Activates the window and displays it as a maximized window.
			/// </summary>
			SW_SHOWMAXIMIZED = 3,

			/// <summary>
			/// Activates the window and displays it as a minimized window.
			/// </summary>
			SW_SHOWMINIMIZED = 2,

			/// <summary>
			/// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
			/// </summary>
			SW_SHOWMINNOACTIVE = 7,

			/// <summary>
			/// Displays the window in its current size and position. This value is similar to SW_SHOW, except the window is not activated.
			/// </summary>
			SW_SHOWNA = 8,

			/// <summary>
			/// Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except the window is not activated.
			/// </summary>
			SW_SHOWNOACTIVATE = 4,

			/// <summary>
			/// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
			/// </summary>
			SW_SHOWNORMAL = 1,
		}

		/// <summary>
		/// Contains information about the placement of a window on the screen.
		/// </summary>
		[System.Serializable]
		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		public struct WINDOWPLACEMENT
		{
			/// <summary>
			/// The length of the structure, in bytes.
			/// </summary>
			public int length;

			/// <summary>
			/// The flags that control the position of the minimized window and the method by which the window is restored. This member can be one or more of the following values.
			/// <list type="bullet">
			///		<listheader>
			///			<term>Term</term>
			///			<description>Description</description>
			///		</listheader>
			///		<item><term>WPF_ASYNCWINDOWPLACEMENT (0x0004):</term><description>If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.</description></item>
			///		<item><term>WPF_RESTORETOMAXIMIZED (0x0002):</term><description>The restored window will be maximized, regardless of whether it was maximized before it was minimized. This setting is only valid the next time the window is restored. It does not change the default restoration behavior. This flag is only valid when the SW_SHOWMINIMIZED value is specified for the showCmd member.</description></item>
			///		<item><term>WPF_SETMINPOSITION (0x0001):</term><description>The coordinates of the minimized window may be specified. This flag must be specified if the coordinates are set in the ptMinPosition member.</description></item>
			/// </list>
			/// </summary>
			public int flags;

			/// <summary>
			/// The current show state of the window. This member can be one of the following values.
			/// <list type="bullet">
			///		<item>SW_HIDE				(0): Hides the window and activates another window.</item>
			///		<item>SW_MAXIMIZE			(3): Maximizes the specified window.</item>
			///		<item>SW_MINIMIZE			(6): Minimizes the specified window and activates the next top-level window in the z-order.</item>
			///		<item>SW_RESTORE			(9): Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.</item>
			///		<item>SW_SHOW				(5): Activates the window and displays it in its current size and position.</item>
			///		<item>SW_SHOWMAXIMIZED		(3): Activates the window and displays it as a maximized window.</item>
			///		<item>SW_SHOWMINIMIZED		(2): Activates the window and displays it as a minimized window.</item>
			///		<item>SW_SHOWMINNOACTIVE	(7): Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.</item>
			///		<item>SW_SHOWNA				(8): Displays the window in its current size and position. This value is similar to SW_SHOW, except the window is not activated.</item>
			///		<item>SW_SHOWNOACTIVATE		(4): Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except the window is not activated.</item>
			///		<item>SW_SHOWNORMAL			(1): Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.</item>
			/// </list>
			/// </summary>
			public ShowWindowCommands showCmd;

			/// <summary>
			/// The coordinates of the window's upper-left corner when the window is minimized.
			/// </summary>
			public System.Drawing.Point ptMinPosition;

			/// <summary>
			/// The coordinates of the window's upper-left corner when the window is maximized.
			/// </summary>
			public System.Drawing.Point ptMaxPosition;

			/// <summary>
			/// The window's coordinates when the window is in the restored position.
			/// </summary>
			public System.Drawing.Rectangle rcNormalPosition;
		}

		/// <summary>
		/// Special window handles
		/// </summary>
		public enum SpecialWindowHandles: int
		{
			/// <summary>
			/// Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
			/// </summary>
			HWND_TOP = 0,

			/// <summary>
			/// Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
			/// </summary>
			HWND_BOTTOM = 1,

			/// <summary>
			/// Places the window at the top of the Z order.
			/// </summary>
			HWND_TOPMOST = -1,

			/// <summary>
			/// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
			/// </summary>
			HWND_NOTOPMOST = -2
		}

		[System.Flags]
		public enum SetWindowPosFlags: uint
		{
			/// <summary>
			/// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
			/// </summary>
			SWP_ASYNCWINDOWPOS = 0x4000,

			/// <summary>
			/// Prevents generation of the WM_SYNCPAINT message.
			/// </summary>
			SWP_DEFERERASE = 0x2000,

			/// <summary>
			/// Draws a frame (defined in the window's class description) around the window.
			/// </summary>
			SWP_DRAWFRAME = 0x0020,

			/// <summary>
			/// Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
			/// </summary>
			SWP_FRAMECHANGED = 0x0020,

			/// <summary>
			/// Hides the window.
			/// </summary>
			SWP_HIDEWINDOW = 0x0080,

			/// <summary>
			/// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
			/// </summary>
			SWP_NOACTIVATE = 0x0010,

			/// <summary>
			/// Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
			/// </summary>
			SWP_NOCOPYBITS = 0x0100,

			/// <summary>
			/// Retains the current position (ignores X and Y parameters).
			/// </summary>
			SWP_NOMOVE = 0x0002,

			/// <summary>
			/// Does not change the owner window's position in the Z order.
			/// </summary>
			SWP_NOOWNERZORDER = 0x0200,

			/// <summary>
			/// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
			/// </summary>
			SWP_NOREDRAW = 0x0008,

			/// <summary>
			/// Same as the SWP_NOOWNERZORDER flag.
			/// </summary>
			SWP_NOREPOSITION = 0x0200,

			/// <summary>
			/// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
			/// </summary>
			SWP_NOSENDCHANGING = 0x0400,

			/// <summary>
			/// Retains the current size (ignores the cx and cy parameters).
			/// </summary>
			SWP_NOSIZE = 0x0001,

			/// <summary>
			/// Retains the current Z order (ignores the hWndInsertAfter parameter).
			/// </summary>
			SWP_NOZORDER = 0x0004,

			/// <summary>
			/// Displays the window.
			/// </summary>
			SWP_SHOWWINDOW = 0x0040,
		}

		[DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern IntPtr GetParent (IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos (IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

		public enum EnumSystemMetric: int
		{
			/// <summary>
			/// The value that specifies how the system is started:
			/// 0 Normal boot
			/// 1 Fail-safe boot
			/// 2 Fail-safe with network boot
			/// A fail-safe boot (also called SafeBoot, Safe Mode, or Clean Boot) bypasses the user startup files.
			/// </summary>
			SM_CLEANBOOT = 67,

			/// <summary>
			/// The number of display monitors on a desktop. For more information, see the Remarks section in this topic.
			/// </summary>
			SM_CMONITORS = 80,

			/// <summary>
			/// The number of buttons on a mouse, or zero if no mouse is installed.
			/// </summary>
			SM_CMOUSEBUTTONS = 43,

			/// <summary>
			/// The least significant bit is set if a network is present; otherwise, it is cleared. The other bits are reserved for future use.
			/// </summary>
			SM_NETWORK = 63,

			/// <summary>
			/// This system metric is used in a Terminal Services environment to determine if the current Terminal Server session is being remotely controlled. Its value is nonzero if the current session is remotely controlled; otherwise, 0.
			/// You can use terminal services management tools such as Terminal Services Manager (tsadmin.msc) and shadow.exe to control a remote session. When a session is being remotely controlled, another user can view the contents of that session and potentially interact with it.
			/// </summary>
			SM_REMOTECONTROL = 0x2001,

			/// <summary>
			/// This system metric is used in a Terminal Services environment. If the calling process is associated with a Terminal Services client session, the return value is nonzero. If the calling process is associated with the Terminal Services console session, the return value is 0.
			/// Windows Server 2003 and Windows XP:  The console session is not necessarily the physical console. For more information, see WTSGetActiveConsoleSessionId.
			/// </summary>
			SM_REMOTESESSION = 0x1000,

			/// <summary>
			/// Nonzero if the current session is shutting down; otherwise, 0.
			/// Windows 2000:  This value is not supported.
			/// </summary>
			SM_SHUTTINGDOWN = 0x2000,

			/// <summary>
			/// Nonzero if the computer has a low-end (slow) processor; otherwise, 0.
			/// </summary>
			SM_SLOWMACHINE = 73,

			/// <summary>
			/// Nonzero if the meanings of the left and right mouse buttons are swapped; otherwise, 0.
			/// </summary>
			SM_SWAPBUTTON = 23,

			/// <summary>
			/// Nonzero if the current operating system is the Windows XP Tablet PC edition or if the current operating system is Windows Vista or Windows 7 and the Tablet PC Input service is started; otherwise, 0. The SM_DIGITIZER setting indicates the type of digitizer input supported by a device running Windows 7 or Windows Server 2008 R2. For more information, see Remarks.
			/// </summary>
			SM_TABLETPC = 86,
		}

		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics (EnumSystemMetric index);

		[DllImport("user32.dll", SetLastError = false)]
		public static extern IntPtr GetDesktopWindow ();

		public delegate int EnumWindowsCallback (IntPtr hWnd, int lParam);

		public delegate int EnumWindowsCallbackIntPtr (IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.Dll")]
		public static extern bool EnumWindows (User32.EnumWindowsCallback lpEnumCallbackFunc, int lParam);

		[DllImport("user32.Dll")]
		public static extern bool EnumWindows (User32.EnumWindowsCallbackIntPtr lpEnumCallbackFunc, IntPtr lParam);

		[DllImport("user32")]
		public static extern bool EnumChildWindows (IntPtr hWnd, User32.EnumWindowsCallback lpEnumCallbackFunc, int lParam);

		[DllImport("user32")]
		public static extern bool EnumChildWindows (IntPtr hWnd, User32.EnumWindowsCallbackIntPtr lpEnumCallbackFunc, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId (IntPtr handle, out int processId);

		[DllImport("user32.dll")]
		public static extern int GetClassName (IntPtr handle, StringBuilder name, int maxCount);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetWindowRect (IntPtr hwnd, out RECT lpRect);

		[DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText (IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern int GetWindowTextLength (IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage (IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage (IntPtr hWnd, uint Msg, IntPtr wParam, ref WinUser.COPYDATASTRUCT lParam);

		public static string GetWindowText (IntPtr hWnd)
		{
			int length = 0;
			StringBuilder builder = null;

			length = GetWindowTextLength(hWnd);
			builder = new StringBuilder(length + 1);

			User32.GetWindowText(hWnd, builder, builder.Capacity);

			return (builder.ToString());
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowPlacement (IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		public static WINDOWPLACEMENT GetWindowPlacement (IntPtr hwnd)
		{
			WINDOWPLACEMENT placement = new WINDOWPLACEMENT();

			placement.length = System.Runtime.InteropServices.Marshal.SizeOf(placement);
			GetWindowPlacement(hwnd, ref placement);

			return (placement);
		}

		[DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public static extern int SystemParametersInfo (System.UInt32 uAction, int uParam, string lpvParam, int fuWinIni);
	}
}