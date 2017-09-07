using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class DocumentIndexViewModel
    {
        public Document Document { get; set; }
        public List<DocumentIndex> DocumentIndexes { get; set; }
    }
}