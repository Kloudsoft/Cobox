using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
	/// <summary>
	/// Determines the state of a document entry.
	/// </summary>
	public enum DocumentEntryState
	{
		/// <summary>
		/// No state (useful for default values).
		/// </summary>
		None,

		/// <summary>
		/// Imported for editing and ready to be queued for upload.
		/// </summary>
		Imported,

		/// <summary>
		/// The document entry is queued for upload.
		/// </summary>
		Queued,

		/// <summary>
		/// The document entry is currently uploading and a lock has been acquired.
		/// </summary>
		Uploading,

		/// <summary>
		/// The document entry has been uploaded successfully.
		/// </summary>
		Uploaded,
	}
}