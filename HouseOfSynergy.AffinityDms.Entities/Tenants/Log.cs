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
    public partial class Log
    {
        public virtual long Id { get; set; }
        public virtual long documentid { get; set; }
        public virtual long parentdocumentid { get; set; }
        public virtual DateTime datetimecreated { get; set; }
        public virtual string action { get; set; }
        public virtual long userid { get; set; }
        public virtual string comments { get; set; }
        

        public Log()
        {
        }

        public void Initialize()
        {
        }

        public Log Clone()
        {
            return (new Log().CopyFrom(this));
        }

        public Log CopyTo(Log destination)
        {
            return (destination.CopyFrom(this));
        }

        public Log CopyFrom(Log source)
        {
            return (ReflectionUtilities.Copy(source, this));
        }

    }
}
