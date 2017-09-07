using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;

using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;


namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
	public class MasterSubscriptionController:Controller
    {
        [MasterMvcTokenAuthorize]
        public ActionResult Index(long? id)
        {
            var subscription = new Subscription();

            try
            {
                bool result = false;
                Exception exception = null;
                MasterUserSession MasterUserSession = null;
                if (id != null)
                {
                    subscription.Id = (long)id;
					if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }

                    result = MasterSubscriptionManagement.GetSubscriptionById(MasterUserSession,subscription.Id, out subscription, out exception);

                    if (exception != null)
                        throw exception;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.ViewBag.Exception = ex.InnerException.Message;
                else
                    this.ViewBag.Exception = ex.Message;
            }

			return (this.View("~/Views/Master/Subscription.cshtml",subscription));
        }

        //[TenantMvcTokenAuthorize]
        public ActionResult CreateUpdateSubscription(Subscription subscription)
        {
            var result = false;
            Exception exception = null;
            Subscription outsubscription = null;

            try
            {

                MasterUserSession MasterUserSession = null;

				if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                if (subscription.Id > 0)
                {
                    result = MasterSubscriptionManagement.UpdateSubscription(MasterUserSession, subscription, out outsubscription, out exception);
                    if (exception != null)
                        throw exception;

                    if (result)
                        this.ViewBag.Message = "Record has been saved successfully";
                }
                else
                {

                    subscription.SubscriptionType = EntityMasterTenantType.Master;
                    result = MasterSubscriptionManagement.CreateSubscription(MasterUserSession, subscription, out outsubscription, out exception);
                    if (exception != null)
                        throw exception;

                    if (result)
                        this.ViewBag.Message = "Record has been saved successfully";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.ViewBag.Exception = ex.InnerException.Message;
                else
                    this.ViewBag.Exception = ex.Message;
            }
            return (this.View("~/Views/Master/Subscription.cshtml", outsubscription));

        }
    }


}