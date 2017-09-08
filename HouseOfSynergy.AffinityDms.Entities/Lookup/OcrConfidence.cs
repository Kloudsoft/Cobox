using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
    public enum OcrConfidence
	{
        /// <summary>
        /// Default
        /// </summary>
		None, //0
        /// <summary>
        /// Confidence to perform OCR
        /// </summary>
		MinimumOCRConfidence = 80, //1
        /// <summary>
        /// Confidence of templates considered to  be recognized.
        /// </summary>
        MinimumRecognitionConfidence = 60, //2
	}
}