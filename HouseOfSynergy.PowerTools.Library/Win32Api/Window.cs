using System;
using System.Linq;
using System.Text;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public sealed class Window
	{
		public IntPtr Handle { get; set; }
		public string ClassName { get; set; }
		public string ClassNameFriendly { get; set; }
		public System.Drawing.Rectangle Bounds { get; set; }
		public User32.ShowWindowCommands WindowState { get; set; }

		public int Style { get; set; }
		public string Caption { get; set; }
		public WindowCollection Parent { get; private set; }
		public WindowCollection Windows { get; private set; }

		public Window ()
		{
			this.Parent = null;
			this.Windows = new WindowCollection(this);

			this.Initialize();
		}

		internal Window (WindowCollection parent)
			: this()
		{
			this.Parent = parent;
		}

		public void Initialize ()
		{
			this.Handle = IntPtr.Zero;
			this.ClassName = "";
			this.ClassNameFriendly = "";
			this.Bounds = new System.Drawing.Rectangle();
			this.Caption = "";
			this.Style = 0;
			this.WindowState = default(User32.ShowWindowCommands);

			this.Windows.ForEach(window => window.Initialize());

			this.Windows.Clear();
		}

		public void Initialize (IntPtr handle)
		{
			this.Initialize();

			this.Handle = handle;
		}

		public void PopulateProperties ()
		{
			int length = 0;
			StringBuilder builder = null;
			User32.WINDOWPLACEMENT placement = default(User32.WINDOWPLACEMENT);

			length = 1000;
			builder = new StringBuilder(length);
			length = User32.GetClassName(this.Handle, builder, length);
			this.ClassName = builder.ToString().Substring(0, length);

			this.Caption = User32.GetWindowText(this.Handle);

			placement = User32.GetWindowPlacement(this.Handle);
			this.Bounds = placement.rcNormalPosition;
			this.WindowState = placement.showCmd;

			switch (this.ClassName)
			{
				case "#32768": { this.ClassNameFriendly = @"Menu"; break; }
				case "#32769": { this.ClassNameFriendly = @"Desktop"; break; }
				case "#32770": { this.ClassNameFriendly = @"Dialog"; break; }
				case "#32771": { this.ClassNameFriendly = @"Task Switch"; break; }
				case "#32772": { this.ClassNameFriendly = @"Icon Title"; break; }
				default: { this.ClassNameFriendly = this.ClassName; break; }
			}
		}

		public void PopulateChildWindows ()
		{
			User32.EnumChildWindows(this.Handle, new User32.EnumWindowsCallbackIntPtr(this.EnumWindowsCallback), this.Handle);
		}

		private int EnumWindowsCallback (IntPtr hWnd, IntPtr lParam)
		{
			Window window = null;

			window = new Window(this.Windows);

			window.Initialize(hWnd);
			window.PopulateProperties();

			if (!this.GetTopMostWindow().Exists(hWnd))
			{
				//System.Diagnostics.Debug.Print(new string(' ', this.GetDepth()) + "Window: " + this.ClassName);
				this.Windows.Add(window);
				window.PopulateChildWindows();
			}

			return (1);
		}

		public bool Exists (IntPtr handle)
		{
			return (this.GetWindowsByHandle(handle).Any());
		}

		public int GetDepth ()
		{
			int count = 0;
			Window window = null;

			window = this;

			while ((window.Parent != null) && (window.Parent.Parent != null)) { count++; window = window.Parent.Parent; }

			return (count);
		}

		public Window GetTopMostWindow ()
		{
			Window window = null;

			window = this;

			while ((window.Parent != null) && (window.Parent.Parent != null)) { window = window.Parent.Parent; }

			return (window);
		}

		public WindowCollection GetWindowsByHandle (IntPtr handle)
		{
			WindowCollection windows = null;

			windows = new WindowCollection();

			if (this.Handle == handle)
			{
				windows.Add(this);
			}

			this.Windows.ForEach(window => windows.AddRange(window.GetWindowsByHandle(handle)));

			return (windows);
		}

		public WindowCollection GetWindowsByClassName (string windowClassName)
		{
			WindowCollection windows = null;

			windows = new WindowCollection();

			if (this.ClassName.Trim().ToLower() == windowClassName.Trim().ToLower())
			{
				windows.Add(this);
			}

			this.Windows.ForEach(window => windows.AddRange(window.GetWindowsByClassName(windowClassName)));

			return (windows);
		}

		public WindowCollection GetWindowsByName (string windowClassName, string windowCaption)
		{
			WindowCollection windows = null;

			windows = new WindowCollection();

			if (this.ClassName.Trim().ToLower() == windowClassName.Trim().ToLower() && this.Caption.Trim().ToLower() == windowCaption.Trim().ToLower())
			{
				windows.Add(this);
			}

			this.Windows.ForEach(window => windows.AddRange(window.GetWindowsByName(windowClassName, windowCaption)));

			return (windows);
		}

		public System.Xml.XmlElement ToXmlElement (System.Xml.XmlDocument document)
		{
			System.Xml.XmlElement element = null;

			element = document.CreateElement("Window");

			element.Attributes.Append(document, "Handle", this.Handle.ToString());
			element.Attributes.Append(document, "ClassNameFriendly", this.ClassNameFriendly.ToString());
			element.Attributes.Append(document, "ClassName", this.ClassName.ToString());
			element.Attributes.Append(document, "Caption", this.Caption.ToString());

			element.AppendChild(this.Windows.ToXmlElement(document));

			return (element);
		}

		public System.Windows.Forms.TreeNode ToTreeNode ()
		{
			System.Windows.Forms.TreeNode node = null;

			node = new System.Windows.Forms.TreeNode();

			node.Name = this.ClassNameFriendly;

			node.Text
				= this.Handle.ToString()
				+ ": "
				+ this.ClassNameFriendly
				;
			node.Tag = this;

			this.Windows.ForEach(window => node.Nodes.Add(window.ToTreeNode()));

			return (node);
		}
	}
}