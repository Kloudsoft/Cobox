using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public sealed class WindowCollection:
		List<Window>
	{
		public Window Parent { get; private set; }

		public WindowCollection () { }

		public WindowCollection (Window parent) : this() { this.Parent = parent; }

		public void Initialize () { this.ForEach(window => window.Initialize()); base.Clear(); }

		public new void Clear () { this.Initialize(); }

		public WindowCollection GetWindowsByHandle (IntPtr handle)
		{
			WindowCollection windows = null;

			windows = new WindowCollection();

			this.ForEach(window => windows.AddRange(window.GetWindowsByHandle(handle)));

			return (windows);
		}

		/// <summary>
		/// Populates top-level windows using EnumWindows.
		/// </summary>
		public void PopulateWindows ()
		{
			this.Initialize();

			User32.EnumWindows(new User32.EnumWindowsCallback(this.EnumWindowsCallback), 0);
		}

		private int EnumWindowsCallback (IntPtr hWnd, int lParam)
		{
			Window window = null;

			window = new Window();

			this.Add(window);

			window.Initialize(hWnd);
			window.PopulateProperties();

			return (1);
		}

		public System.Xml.XmlElement ToXmlElement (System.Xml.XmlDocument document)
		{
			System.Xml.XmlElement element = null;

			element = document.CreateElement("Windows");

			this.ForEach(window => element.AppendChild(window.ToXmlElement(document)));

			return (element);
		}
	}
}