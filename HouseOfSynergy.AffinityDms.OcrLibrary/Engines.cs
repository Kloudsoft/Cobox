using Leadtools.Codecs;
using Leadtools.Forms.DocumentWriters;
using Leadtools.Forms.Ocr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class Engines
    {
    //    /// <summary>
    //    /// Loads OCR Engine
    //    /// </summary>
    //    /// <param name="formcodec"></param>
    //    /// <param name="docwrite"></param>
    //    /// <param name="workdir"></param>
    //    /// <param name="runtimedir"></param>
    //    /// <param name="ocrengine"></param>
    //    /// <param name="exception"></param>
    //    public static bool LoadEngine(RasterCodecs formcodec, DocumentWriter docwrite, string workdir, string runtimedir, out IOcrEngine ocrengine, out Exception exception)
    //    {
    //        ocrengine = null;
    //        exception = null;
    //        bool result = false;

    //        try
    //        {
    //            ocrengine = OcrEngineManager.CreateEngine(LeadToolsSettings.OcrEngineType, false);
    //            if (!ocrengine.IsStarted)
    //            {
    //                ocrengine.Startup(formcodec, docwrite, workdir, runtimedir);
    //            }
    //            result = true;
    //        }
    //        catch (Exception ex)
    //        {
				//exception = ex;
    //            //throw;
    //        }
    //        return result;
    //    }

		public static IOcrEngine LoadEngine (RasterCodecs formcodec, DocumentWriter docwrite, string workdir, string runtimedir)
		{
			var ocrEngine = OcrEngineManager.CreateEngine (LeadToolsSettings.OcrEngineType, false);

			ocrEngine.Startup (formcodec, docwrite, workdir, runtimedir);

			return (ocrEngine);
		}
	}
}
