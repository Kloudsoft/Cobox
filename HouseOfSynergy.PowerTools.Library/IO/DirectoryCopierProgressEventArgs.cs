using System.IO;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public class DirectoryCopierProgressEventArgs:
		HouseOfSynergy.PowerTools.Library.IO.DirectoryCopierEventArgs
	{
		private bool _SkipCopyEntry { get; set; }

		public CopyEntry CurrentCopyEntry { get; private set; }

		public DirectoryCopierProgressEventArgs (DirectoryInfo source, DirectoryInfo destination, CopyEntry currentCopyEntry)
			: base(source, destination)
		{
			this.CurrentCopyEntry = currentCopyEntry;
		}

		/// <summary>
		/// Set this flag to true to skip the CurrentCopyEntry.This can be helpful is a custom action is required instead. This applies only to FileCopyEntry objects and not DirectoryCopyEntry objects.
		/// </summary>
		public bool SkipCopyEntry
		{
			get
			{
				return (this._SkipCopyEntry);
			}
			set
			{
				if (this.CurrentCopyEntry is FileCopyEntry)
				{
					this._SkipCopyEntry = value;
				}
				else
				{
					throw (new System.InvalidOperationException("The SkipCopyEntry flag is only valid for FileCopyEntry objects."));
				}
			}
		}
	}
}