using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
    public partial class ClassifiedFileIndexs
    {

        public virtual long Id { get; set; }
        
        public virtual long userid { get; set; }

        public virtual long documentid { get; set; }

        public virtual string userdomain { get; set; }

        public virtual string indexname { get; set; }

        public virtual string indexvalue { get; set; }

        public virtual string indexbounding { get; set; }

        public virtual string classification { get; set; }

        public virtual string userfilepathO { get; set; }

        public virtual string userfilepathR { get; set; }
    }
}
