using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class OcrResultInfo
    {
        public long DocumentId { get; set; }
        public Document Document { get; set; }
        public string OcrFullTextData { get; set; }
        public string OcrFullXmlData { get; set; }
        public List<MatchedTemplates> MatchedTemplates { get; set; }
        public DocumentState DoucmentState { get; set; }
        public Image Image { get; set; }
        public int MatchedTemplatesCount { get; set; }
        public TemplateType TemplateType { get; set; }
        public string ImagePath { get; set; }
    }
    public class MatchedTemplates
    {
        public DocumentTemplate DocumentTemplates { get; set; }
        public long? TemplateId { get; set; }
        public string TemplateName { get; set; }
        public int? Confidence { get; set; }
    }
}