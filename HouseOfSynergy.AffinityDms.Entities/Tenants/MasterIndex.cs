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
    public partial class MasterIndex
    {
        public virtual long Id { get; set; }
        public virtual string master_index_name { get; set; }
        public virtual int index_group_id { get; set; }
        public virtual string display_name { get; set; }
        public virtual string prefix_file_name { get; set; }
        

        public MasterIndex()
        {
        }

        public void Initialize()
        {
        }

        public MasterIndex Clone()
        {
            return (new MasterIndex().CopyFrom(this));
        }

        public MasterIndex CopyTo(MasterIndex destination)
        {
            return (destination.CopyFrom(this));
        }

        public MasterIndex CopyFrom(MasterIndex source)
        {
            return (ReflectionUtilities.Copy(source, this));
        }


    }
}
