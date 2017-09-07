using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class DocumentsViewModel
    {
        public bool MoveItem { get; set; }
        public long Id { get; set; }
        public string TemplateTitle { get; set; }
        public string FolderName { get; set; }
		public string Name { get; set; }
		public string FileNameClient { get; set; }
        public string FileNameServer { get; set; }
        public string DateTime { get; set; }
        public bool Cancelled { get; set; }
        public bool Transit { get; set; }
        public bool IsPrivate { get; set; }
        public List<DocumentTemplate> DocumentTemplates { get; set; }

        
    }
}