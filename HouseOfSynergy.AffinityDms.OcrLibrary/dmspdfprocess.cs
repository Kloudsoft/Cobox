using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using Leadtools;
using Leadtools.Barcode;
using Leadtools.Codecs;
//using Leadtools.Codecs.Cmp;
using Leadtools.Codecs.Png;
using Leadtools.Codecs.Tif;
using Leadtools.Documents;
using Leadtools.Documents.Converters;
using Leadtools.Documents.Pdf;
using Leadtools.Documents.Raster;
using Leadtools.Drawing;
using Leadtools.Forms;
using Leadtools.Forms.Auto;
using Leadtools.Forms.DocumentReaders;
using Leadtools.Forms.DocumentWriters;
using Leadtools.Forms.Ocr;
using Leadtools.Forms.Processing;
using Leadtools.Forms.Recognition;
using Leadtools.Forms.Recognition.Ocr;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Core;
using Leadtools.Pdf;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class dmspdfprocess
    {
        public bool PDFtoIMG()
        {
            try
            {
                var DirectorRoot = @"D:\DEVMVC\TFS-Current\Github Cobox Prototype\HouseOfSynergy.AffinityDms.WebRole\UploadedFiles\TemplateIndexList\Original\";
                
              // Load the input PDF document
              PDFDocument document = new PDFDocument(DirectorRoot + "Delivery-Report.pdf");
                using (RasterCodecs codecs = new RasterCodecs())
                {
                    // Loop through all the pages in the document
                    for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
                    {
                        // Render the page into a raster image
                        using (RasterImage image = document.GetPageImage(codecs, pageNumber))
                        {
                            // Append to (or create if it does not exist) a TIFF file
                            codecs.Save(image, DirectorRoot +"/Output.png", RasterImageFormat.TifJpeg, 24, 1, 1, -1, CodecsSavePageMode.Append);
                        }
                    }
                }

            }
            catch (Exception exx) { }

            return true;
        } 

    }
}
