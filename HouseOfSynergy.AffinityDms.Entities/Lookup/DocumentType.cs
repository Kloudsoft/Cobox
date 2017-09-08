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
	/// Determines the type of a document.
	/// </summary>
	public enum DocumentType
	{
		/// <summary>
		/// No state (useful for default values).
		/// </summary>
		None,

		/// <summary>
		/// The document requires OCR to obtain content.
		/// </summary>
		Raster,

		/// <summary>
		/// The document does not require OCR. Contents can be accessed via a third-party library depending on the file extension of the document.
		/// </summary>
		Digital,

		/// <summary>
		/// The type of operation required to access contents cannot be determined.
		/// </summary>
		Unknown,
	}
}