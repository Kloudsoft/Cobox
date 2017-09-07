using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using HouseOfSynergy.PowerTools.Library.Win32Api;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class ShellUtilities
	{
		public static bool ParseCommandLineString (string commandLineString, out ReadOnlyCollection<string> commandLineArguments, out Exception exception)
		{
			var count = 0;
			var result = false;
			var ptr = IntPtr.Zero;
			var list = new List<string>();

			exception = null;
			commandLineArguments = null;

			if (commandLineString == null) { throw (new ArgumentNullException("commandLineString")); }

			try
			{
				ptr = Shell32.CommandLineToArgvW(/*"Executable.exe " + */commandLineString, out count);
				if (ptr == IntPtr.Zero) { throw (new Win32Exception("The call to [CommandLineToArgvW] failed.")); }

				if (count > 0)
				{
					for (var i = 0; i < count; i++)
					{
						var p = Marshal.ReadIntPtr(ptr, i * IntPtr.Size);

						list.Add(Marshal.PtrToStringUni(p));
					}
				}

				commandLineArguments = list.AsReadOnly();

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}
			finally
			{
				Marshal.FreeHGlobal(ptr);
			}

			return (result);
		}

		public static bool LaunchExplorerWithFileSelection (string directory, IEnumerable<string> filesToSelect, out Exception exception)
		{
			bool result = false;

			exception = null;

			try
			{
				if (directory == null) { throw (new ArgumentNullException("directory")); }
				if (!Directory.Exists(directory)) { throw (new DirectoryNotFoundException("The directory [" + directory + "] could not be found.")); }

				if (filesToSelect == null) { throw (new ArgumentNullException("filesToSelect")); }
				if (!filesToSelect.Any()) { throw (new ArgumentException("The argument [filesToSelect] must not be empty.", "filesToSelect")); }

				var list = filesToSelect.ToList().ConvertAll<FileInfo>(f => new FileInfo(f));

				result = ShellUtilities.LaunchExplorerWithFileSelection(new DirectoryInfo(directory), list, out exception);
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool LaunchExplorerWithFileSelection (DirectoryInfo directory, IEnumerable<FileInfo> filesToSelect)
		{
			Exception exception = null;
			return (ShellUtilities.LaunchExplorerWithFileSelection(directory, filesToSelect, out exception));
		}

		public static bool LaunchExplorerWithFileSelection (DirectoryInfo directory, IEnumerable<FileInfo> filesToSelect, out Exception exception)
		{
			var result = false;
			var dir = IntPtr.Zero;
			var pointers = new IntPtr [] { };

			exception = null;

			try
			{
				if (directory == null) { throw (new ArgumentNullException("directory")); }
				if (!Directory.Exists(directory.FullName)) { throw (new DirectoryNotFoundException("The directory [" + directory.FullName + "] could not be found.")); }

				if (filesToSelect == null) { throw (new ArgumentNullException("filesToSelect")); }
				if (!filesToSelect.Any()) { throw (new ArgumentException("The argument [filesToSelect] must not be empty.", "filesToSelect")); }

				directory.Refresh();

				var list = filesToSelect.ToList();

				for (int i = 0; i < list.Count; i++)
				{
					if (PathUtilities.GetNormalizedPath(directory) != PathUtilities.GetNormalizedPath(list [i].Directory))
					{
						throw (new ArgumentNullException("filesToSelect", "All files in the argument [filesToSelect] must have the same directory as the argument [directory]."));
					}
				}

				pointers = new IntPtr [list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					pointers [i] = Shell32.ILCreateFromPath(list [i].FullName);
				}

				dir = Shell32.ILCreateFromPath(directory.FullName);

				var hresult = Shell32.SHOpenFolderAndSelectItems(dir, (uint) list.Count, pointers, 0);

				result = hresult == WinError.S_OK;

				if (!result)
				{
					exception = new Exception("The call to [SHOpenFolderAndSelectItems] failed.");
				}
			}
			catch (EntryPointNotFoundException e)
			{
				exception = e;
			}
			finally
			{
				ComUtilities.ReleaseComObject(dir);
				ComUtilities.ReleaseComObject(pointers);
			}

			return (result);
		}

		public static void LaunchExplorerWithFileSelection (FileInfo file)
		{
			ShellUtilities.LaunchExplorerWithFileSelection(file.FullName);
		}

		public static void LaunchExplorerWithFileSelection (string filename)
		{
			Process.Start("explorer.exe", "/Select," + filename);
		}

		public static bool LaunchExplorerWithFileSelection (FileInfo file, out Exception exception)
		{
			return (ShellUtilities.LaunchExplorerWithFileSelection(file.FullName, out exception));
		}

		public static bool LaunchExplorerWithFileSelection (string filename, out Exception exception)
		{
			var result = false;

			exception = null;

			try
			{
				Process.Start("explorer.exe", "/Select," + filename);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}
	}
}