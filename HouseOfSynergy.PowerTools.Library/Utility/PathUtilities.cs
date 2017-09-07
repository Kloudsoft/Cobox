using System;
using System.IO;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class PathUtilities
	{
		public static string GetNormalizedPath (string path)
		{
			return (Path.GetFullPath(new Uri(path).LocalPath).Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).ToLowerInvariant());
		}

		public static string GetNormalizedPath (FileInfo file)
		{
			return (PathUtilities.GetNormalizedPath(file.FullName));
		}

		public static string GetNormalizedPath (DirectoryInfo directory)
		{
			return (PathUtilities.GetNormalizedPath(directory.FullName));
		}

		public static DirectoryInfo GetTempDirectory (DirectoryInfo directoryBase, bool keepOnDisk = true)
		{
			var directory = "";
			var guid = Guid.Empty;

			directoryBase.Refresh();
			if (!directoryBase.Exists) { throw (new System.IO.DirectoryNotFoundException()); }

			do
			{
				directory = Path.Combine
				(
					directoryBase.FullName,
					Guid.NewGuid().ToString(GuidUtilities.EnumGuidFormat.FileSystem)
				);

				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);

					if (!keepOnDisk) { Directory.Delete(directory); }

					break;
				}
			}
			while (true);

			return (new DirectoryInfo(directory));
		}

		public static FileInfo GetTempFile (string extension, bool keepOnDisk = true)
		{
			return (PathUtilities.GetTempFile(new DirectoryInfo(Path.GetTempPath()), extension, keepOnDisk));
		}

		public static FileInfo GetTempFile (DirectoryInfo directory, string extension, bool keepOnDisk = true)
		{
			var filename = "";
			FileInfo file = null;
			var guid = Guid.Empty;

			directory.Refresh();
			if (!directory.Exists) { throw (new System.IO.DirectoryNotFoundException()); }
			if (!PathUtilities.ValidateFileExtension(extension)) { throw (new ArgumentException("The supplied [extension] is not valid.", "extension")); }

			while (true)
			{
				filename = Path.Combine
				(
					directory.FullName,
					Guid.NewGuid().ToString(GuidUtilities.EnumGuidFormat.FileSystem) + (extension.StartsWith(".") ? extension : ("." + extension))
				);

				if (!File.Exists(filename))
				{
					File.Create(filename).Close();

					if (!keepOnDisk) { File.Delete(filename); }

					file = new FileInfo(filename);

					break;
				}
			}

			return (file);
		}

		public static bool ValidateFileExtension (string extension)
		{
			if (extension != null)
			{
				if (extension.Length > 0)
				{
					extension = extension.StartsWith(".") ? extension.Substring(1) : extension;

					if (extension.Length > 0)
					{
						if (extension.All(c => char.IsLetterOrDigit(c) || (c == '.')))
						{
							if (!extension.Contains(".."))
							{
								if (!extension.StartsWith(".") && !extension.EndsWith("."))
								{
									return (true);
								}
							}
						}
					}
				}
			}

			return (false);
		}
	}
}