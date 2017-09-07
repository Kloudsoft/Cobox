using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using HouseOfSynergy.PowerTools.Library.Collections;
using Microsoft.Win32;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class FileUtilities
	{
		/// <summary>
		/// Structure that encapsulates basic information of icon embedded in a file.
		/// </summary>
		public struct EmbeddedIconInfo
		{
			public string FileName;
			public int IconIndex;
		}

		[DllImport("shell32.dll", EntryPoint = "ExtractIconA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr ExtractIcon (int hInst, string lpszExeFileName, int nIconIndex);

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern uint ExtractIconEx (string szFileName, int nIconIndex, IntPtr [] phiconLarge, IntPtr [] phiconSmall, uint nIcons);

		[DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
		private static extern int DestroyIcon (IntPtr hIcon);

		public static ReadOnlyDictionary<string, string> GetRegisteredFileIcons ()
		{
			object value = null;
			var dictionary = new Dictionary<string, string>();

			try
			{
				using (var keyRoot = Registry.ClassesRoot)
				{
					var names = keyRoot.GetSubKeyNames();

					foreach (var name in names)
					{
						if (!string.IsNullOrEmpty(name))
						{
							if (name.IndexOf('.') == 0)
							{
								using (var keyFileType = keyRoot.OpenSubKey(name))
								{
									if (keyFileType != null)
									{
										value = keyFileType.GetValue("");

										if (value != null)
										{
											using (var keyFileIcon = keyRoot.OpenSubKey(value.ToString() + @"\DefaultIcon"))
											{
												if (keyFileIcon != null)
												{
													value = keyFileIcon.GetValue("");

													if (value != null)
													{
														dictionary.Add(name, value.ToString().Replace("\"", ""));
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			finally
			{
			}

			return (new ReadOnlyDictionary<string, string>(dictionary));
		}

		public static Bitmap GetFileIconAsBitmap (string filenameWithParameters)
		{
			Icon icon = null;
			Bitmap bitmap = null;

			icon = FileUtilities.ExtractIconFromFile(filenameWithParameters);

			if (icon != null)
			{
				bitmap = icon.ToBitmap();

				icon.Dispose();
			}

			return (bitmap);
		}

		public static Bitmap GetFileIconAsBitmap (string filenameWithParameters, bool isLarge)
		{
			Icon icon = null;
			Bitmap bitmap = null;

			icon = FileUtilities.ExtractIconFromFile(filenameWithParameters, isLarge);

			if (icon != null)
			{
				bitmap = icon.ToBitmap();

				icon.Dispose();
			}

			return (bitmap);
		}

		/// <summary>
		/// Extract the icon from file.
		/// </summary>
		/// <param name="filenameWithParameters">The params string,
		/// such as ex: "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
		/// <returns>This method always returns the large size of the icon (may be 32x32 px).</returns>
		public static Icon ExtractIconFromFile (string filenameWithParameters)
		{
			try
			{
				EmbeddedIconInfo embeddedIcon = GetEmbeddedIconInfo(filenameWithParameters);

				//Gets the handle of the icon.
				IntPtr hIcon = ExtractIcon(0, embeddedIcon.FileName, embeddedIcon.IconIndex);

				//Gets the real icon.
				return (Icon.FromHandle(hIcon));
			}
			finally
			{
			}
		}

		/// <summary>
		/// Extract the icon from file.
		/// </summary>
		/// <param name="filenameWithParameters">The params string,
		/// such as ex: "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
		/// <param name="isLarge">
		/// Determines the returned icon is a large (may be 32x32 px)
		/// or small icon (16x16 px).</param>
		public static Icon ExtractIconFromFile (string filenameWithParameters, bool isLarge)
		{
			if (filenameWithParameters == null) { throw (new ArgumentNullException("filenameWithParameters")); }
			//if (!File.Exists(filenameWithParameters)) { throw (new System.IO.FileNotFoundException("The file [" + filenameWithParameters + "] was not found.", filenameWithParameters)); }

			uint readIconCount = 0;
			IntPtr [] hDummy = null;
			IntPtr [] hIconEx = null;
			EmbeddedIconInfo embeddedIcon = default(EmbeddedIconInfo);

			try
			{
				hDummy = new IntPtr [1] { IntPtr.Zero };
				hIconEx = new IntPtr [1] { IntPtr.Zero };
				embeddedIcon = GetEmbeddedIconInfo(filenameWithParameters);

				if (isLarge) { readIconCount = ExtractIconEx(embeddedIcon.FileName, 0, hIconEx, hDummy, 1); }
				else { readIconCount = ExtractIconEx(embeddedIcon.FileName, 0, hDummy, hIconEx, 1); }

				if ((readIconCount > 0) && (hIconEx [0] != IntPtr.Zero))
				{
					// Get first icon.
					Icon extractedIcon = (Icon) Icon.FromHandle(hIconEx [0]).Clone();

					return (extractedIcon);
				}
				else
				{
					return (null);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				foreach (IntPtr ptr in hIconEx) { if (ptr != IntPtr.Zero) { DestroyIcon(ptr); } }
				foreach (IntPtr ptr in hDummy) { if (ptr != IntPtr.Zero) { DestroyIcon(ptr); } }
			}
		}

		/// <summary>
		/// Parses the parameters string to the structure of EmbeddedIconInfo.
		/// </summary>
		/// <param name="filenameWithParameters">The params string,
		/// such as ex: "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
		/// <returns></returns>
		private static EmbeddedIconInfo GetEmbeddedIconInfo (string filenameWithParameters)
		{
			int iconIndex = 0;
			string filename = "";
			string iconIndexString = "";
			EmbeddedIconInfo embeddedIcon = default(EmbeddedIconInfo);

			if (filenameWithParameters == null) { throw (new ArgumentNullException("filenameWithParameters")); }
			//if (!File.Exists(filenameWithParameters)) { throw (new System.IO.FileNotFoundException("The file [" + filenameWithParameters + "] was not found.", filenameWithParameters)); }

			// Example: "C:\\Program Files\\NetMeeting\\conf.exe,1".
			int commaIndex = filenameWithParameters.IndexOf(",");

			if (commaIndex > 0)
			{
				filename = filenameWithParameters.Substring(0, commaIndex);
				iconIndexString = filenameWithParameters.Substring(commaIndex + 1);
			}
			else
			{
				filename = filenameWithParameters;
			}

			if (!String.IsNullOrEmpty(iconIndexString))
			{
				iconIndex = int.Parse(iconIndexString);

				// To avoid the invalid index.
				if (iconIndex < 0) { iconIndex = 0; }
			}

			embeddedIcon.FileName = filename;
			embeddedIcon.IconIndex = iconIndex;

			return (embeddedIcon);
		}
	}
}