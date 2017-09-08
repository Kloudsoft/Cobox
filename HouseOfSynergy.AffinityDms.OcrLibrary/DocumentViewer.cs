using Leadtools.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class DocumentViewer
    {
        public static Document ltDocument { get; set; }
        public static  Document LoadDocumentFromStream(Stream stream, LoadDocumentOptions loadDocumentOptions = null)
        {
            if (loadDocumentOptions == null) {
                var options = new LoadDocumentOptions();
                options.UseCache = false;
            }
            Document document = null;
            if (stream != null)
            {
                stream.Seek(0, SeekOrigin.Begin);
                if (stream.Length > 0)
                {
                    document = DocumentFactory.LoadFromStream(stream, loadDocumentOptions);
                }
            }
            return document;
        }
    }
}
