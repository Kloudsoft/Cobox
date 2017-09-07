using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class AzureTenantConflictViewModel
    {
        public User AzureUser { get; set; }
        public User TenantUser { get; set; }
        public bool Conflicted { get; set; }
        public string Reason { get; set; }

    }
}