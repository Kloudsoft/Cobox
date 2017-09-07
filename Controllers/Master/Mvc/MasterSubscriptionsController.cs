using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
	public class MasterSubscriptionsController:
		Controller
    {
        public ActionResult Index()
        {
            List<Entities.Common.Subscription> subscription = null;
            Exception exception=null;
            MasterUserSession MasterUserSession = null;

            try
            {
				if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }

                if (!MasterSubscriptionManagement.GetSubcriptions(MasterUserSession, out subscription, out exception)) { throw (exception); }

                if (exception != null)
                    throw exception;
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                    this.ViewBag.Exception = ex.InnerException.Message;
                else
                    this.ViewBag.Exception = ex.Message;
            }
           return (this.View("~/Views/Master/Subscriptions.cshtml", subscription));
        }
    }
}