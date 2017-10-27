using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
    public class azureblob
    {

        public virtual long Id { get; set; }

        public virtual string batchno { get; set; }

        public virtual int nooffiles { get; set; }

        public virtual int status { get; set; }

        public virtual string inputblob { get; set; }

        public virtual string outputblob { get; set; }

        public virtual string filetype { get; set; }

        public virtual DateTime createdon { get; set; }

    }
}
