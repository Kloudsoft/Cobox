using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class DocumentsNameId
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public SelectList DocsNamelist { get; set; }
    }
}