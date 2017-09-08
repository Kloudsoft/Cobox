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
	/// Determines the current state of a document.
	/// </summary>
	public enum DocumentState
	{
		/// <summary>
		/// No state (useful for default values).
		/// </summary>
		None, // 0

		/// <summary>
		/// The document entry has been created in the database and the actual file is currently uploading.
		/// </summary>
		Uploading, // 1

		/// <summary>
		/// The document has been uploaded and queued for Auto OCR.
		/// </summary>
		QueuedAuto, // 2

		/// <summary>
		/// The document has been uploaded and queued for Manual OCR.
		/// </summary>
		QueuedManual, // 3

		/// <summary>
		/// The document has been uploaded, OCR has been performed, and a single template has been identified.
		/// In this case, Document.TemplateId will be set and no entries will appear in the DocumentTemplates table.
		/// </summary>
		Matched, // 4

		/// <summary>
		/// The document has been uploaded, OCR has been performed, and a multiple templates have been identified.
		/// In this case, Document.TemplateId will be set and entries will appear in the DocumentTemplates table (one entry per match).
		/// </summary>
		MatchedMultiple, // 5

		/// <summary>
		/// The document has been uploaded, OCR has been performed, and a no templates have been identified.
		/// In this case, Document.TemplateId will be null and no entries will appear in the DocumentTemplates table.
		/// </summary>
		UnMatched, // 6

        /// <summary>
		/// The document has been uploaded, OCR has been performed, and a single template has been identified. Its confidence is in between minimum recognition and minimum ocr confidence.
		/// In this case, Document.TemplateId will be set and no entries will appear in the DocumentTemplates table.
		/// </summary>
        MatchedUnverified, //7
        /// <summary>
        /// The document has been uploaded, OCR has been performed, and a single template has been identified. Its confidence is greater than the minimum ocr confidence.
        /// In this case, Document.TemplateId will be set and no entries will appear in the DocumentTemplates table.
        /// </summary>
        Verified, //8
        /// <summary>
        /// A document that is queued and have a count less than 4 was found and ready for pcr processing.
        /// In this case, Document will be sent for Ocr Processing.
        /// </summary>
        Processing, //9
    }
}