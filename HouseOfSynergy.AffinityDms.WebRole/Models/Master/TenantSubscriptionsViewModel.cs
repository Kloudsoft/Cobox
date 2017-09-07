using HouseOfSynergy.AffinityDms.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Master
{
    public class TenantSubscriptionsViewModel
    {
        public List<TenantSubscription> TenantSubscriptions { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
    
}