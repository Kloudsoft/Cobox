using HouseOfSynergy.AffinityDms.Entities.Tenants;
using Leadtools.Forms.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class ConfidenceMatchedTemplates
    {
        public Template Template { get; set; }
        public FormRecognitionAttributes MasterFormAttributes { get; set; }
        public FormRecognitionAttributes FormAttributes { get; set; }
        public int? Confidence { get; set; }
        public string XMLOCR { get; set; }
        public string TEXTOCR { get; set; }
        public long? TemplateElementID { get; set; }
        public long? TemplateElementDetailId { get; set; }
        //public Enties.TemplateElement? TemplateElement { get; set; }
        //public Enties.TemplateElementDetail? TemplateElementDetail { get; set; }
    }
}
