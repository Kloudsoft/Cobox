using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class ClassificationImagelist
    {
        public IEnumerable<string> Images { get; set; }

        public IEnumerable<string> TemplateNames { get; set; }

    }
}