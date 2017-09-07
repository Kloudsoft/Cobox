using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
    public class DocumentStatusViewModel
    {
        public DateTime CreatedOn { get; set; }
        public User CreatedBy { get; set; }
        public DateTime LastEdited { get; set; }
        public List<User> SharedWith { get; set; }
        public List<DocumentElement> Indexes { get; set; }
    }
}