using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
    public class MasterTenantSubscriptionController :
        Controller
    {
        [TenantMvcTokenAuthorize]
        public ActionResult Index(long? id)
        {
            var tenantsubscrption = new TenantSubscription();
            this.ViewBag.Message = string.Empty;
            this.ViewBag.Exception = string.Empty;
            try
            {
                bool result = false;
                Exception exception = null;
                MasterUserSession MasterUserSession = null;
                if (id != null)
                {
                    if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                    result = MasterTenantSubscriptionManagement.GetTenantSubscriptionById(MasterUserSession, (long)id, out tenantsubscrption, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex);
                return (this.View("~/Views/Master/TenantSubscription.cshtml", tenantsubscrption));
            }
            return (this.View("~/Views/Master/TenantSubscription.cshtml", tenantsubscrption));
        }

        [TenantMvcTokenAuthorize]
        public ActionResult CreateUpdateTenantSubscription(TenantSubscription tenantsubscription)
        {
            var result = false;
            Exception exception = null;
            TenantSubscription outtenantsubscription = null;
            this.ViewBag.Message = string.Empty;
            this.ViewBag.Exception = string.Empty;

            try
            {
                MasterUserSession MasterUserSession = null;

                if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                if (tenantsubscription.Id > 0)
                {
                    result = MasterTenantSubscriptionManagement.UpdateTenantSubscription(MasterUserSession, tenantsubscription, out outtenantsubscription, out exception);
                    if (exception != null)
                        throw exception;

                    if (result)
                        this.ViewBag.Message = "Record has been saved successfully";
                }
                else
                {
                    tenantsubscription.TenantSubscriptionType = EntityMasterTenantType.Master;
                    result = MasterTenantSubscriptionManagement.CreateTenantSubscription(MasterUserSession, tenantsubscription, out outtenantsubscription, out exception);
                    if (exception != null)
                        throw exception;

                    if (result)
                        this.ViewBag.Message = "Record has been saved successfully";
                       
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex);
                return (this.View("~/Views/Master/TenantSubscription.cshtml", outtenantsubscription));
            }
            return (this.View("~/Views/Master/TenantSubscription.cshtml", outtenantsubscription));

        }
    }
}