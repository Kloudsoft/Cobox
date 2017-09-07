using System;
using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public static class Utility
	{
		public static bool DeleteDirectory (string directory, bool recursively, out Exception exception)
		{
			if (directory == null) { throw (new ArgumentNullException("directory")); }

			return (Utility.DeleteDirectory(new DirectoryInfo(directory), recursively, out exception));
		}

		public static bool DeleteDirectory (DirectoryInfo directory, bool recursively, out Exception exception)
		{
			bool result = false;

			exception = null;

			if (directory == null) { throw (new ArgumentNullException("directory")); }

			try
			{
				directory.Refresh();

				directory.Delete(true);

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