using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leadtools.Forms.Ocr;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public static class LeadToolsSettings
	{
		public static OcrEngineType OcrEngineType { get; } = OcrEngineType.Advantage;
	}
}
