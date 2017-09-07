using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Master;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
    public class MasterTenantSubscriptionsController :
        Controller
    {
        /// <summary>
        /// Get All Tenant Subscription record
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(long? id)
        {

            Exception exception = null;
            MasterUserSession masterUserSession = null;
            List<TenantSubscription> tenantsubscription = null;
            TenantSubscriptionsViewModel tenantsubscriptionviewmodel = null;
            bool result = false;
            ViewBag.Message = string.Empty;
            try
            {
                if (id > 0)
                {
                    this.ViewBag.TenantId = id;
                    if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out masterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                    //if (!MasterUserManagement.IsUserActionAllowed(masterUserSession.User, MasterActionType.UsersAdd, MasterActionType.UsersEdit)) { throw (new UserNotAuthorizedException()); }

                    result = MasterTenantSubscriptionManagement.GetTenantSubcriptionsByTenantId(masterUserSession, (long)id, out tenantsubscription, out exception);

                    if (exception != null)
                        throw exception;


                    List<Subscription> subscriptions = null;
                    result = MasterSubscriptionManagement.GetSubcriptions(masterUserSession, out subscriptions, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    tenantsubscriptionviewmodel= new TenantSubscriptionsViewModel();
                    tenantsubscriptionviewmodel.TenantSubscriptions = tenantsubscription;
                    tenantsubscriptionviewmodel.Subscriptions = subscriptions;
                }
                else
                {
                    return RedirectToAction("Index", "MasterTenants");
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            if (exception != null)
                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(exception);
            else
                this.ViewBag.Exception = string.Empty;
            return (this.View("~/Views/Master/TenantSubscriptions.cshtml", tenantsubscriptionviewmodel));
        }

        public JsonResult GetSubscriptionById(long id,long tenantid)
        {
            Exception exception = null;
            MasterUserSession MasterUserSession = null;
            Subscription subscription = null;
            TenantSubscription tenantsubscription = null;
            bool result = false;
            TenantSubscriptionsViewModel tenantsubscriptionviewmodel = new TenantSubscriptionsViewModel();
            try
            {
                if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                //if (!MasterUserManagement.IsUserActionAllowed(MasterUserSession.User, MasterActionType.UsersAdd,MasterActionType.UsersEdit)) { throw (new UserNotAuthorizedException()); }

                result = MasterSubscriptionManagement.GetSubscriptionById(MasterUserSession, id, out subscription, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (subscription != null)
                {
                    result = MasterTenantSubscriptionManagement.GetActiveTenantSubscriptionById(MasterUserSession, tenantid, out tenantsubscription, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    //if (tenantsubscription.Count >= 0)
                    //{
                        tenantsubscriptionviewmodel.Subscriptions = new List<Subscription>();
                        tenantsubscriptionviewmodel.Subscriptions.Add(subscription);
                        tenantsubscriptionviewmodel.TenantSubscriptions = new List<TenantSubscription>();
                        tenantsubscriptionviewmodel.TenantSubscriptions.Add(tenantsubscription);
                    //}
                        
                }
                return Json(tenantsubscriptionviewmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ApplySubscription(long subscriptionid, long tenantid, bool Scanning, bool Branding, bool TemplateWorkflow,string StartDate,string ExpiryDate)
        {
            
            Exception exception = null;
            MasterUserSession MasterUserSession = null;
            Subscription subscription = null;
            TenantSubscription tenantsubscription = null;
            List<TenantSubscription> tenantsubscriptionlist = null;
            bool result = false;
            TenantSubscriptionsViewModel tenantsubscriptionviewmodel = new TenantSubscriptionsViewModel();
            try
            {
                if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(ExpiryDate))
                {
                    return Json("Start Date and Expiry date are required ");
                }

                if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                //if (!MasterUserManagement.IsUserActionAllowed(MasterUserSession.User, MasterActionType.UsersAdd, MasterActionType.UsersEdit)) { throw (new UserNotAuthorizedException()); }

                result = MasterSubscriptionManagement.GetSubscriptionById(MasterUserSession, subscriptionid, out subscription, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (subscription != null)
                {
                    result = MasterTenantSubscriptionManagement.GetActiveTenantSubscriptionById(MasterUserSession, tenantid, out tenantsubscription, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (tenantsubscription == null)
                    {
                        tenantsubscription = new TenantSubscription();
                    }
                    var allowedforms = tenantsubscription.NumberOfFormsAllowed + subscription.NumberOfFormsAllowed;
                    var allowedpages = tenantsubscription.NumberOfPagesAllowed + subscription.NumberOfPagesAllowed;
                    var allowedusers = tenantsubscription.NumberOfUsersAllowed + subscription.NumberOfUsersAllowed;
                    var allowedtemplates = tenantsubscription.NumberOfTemplatesAllowed + subscription.NumberOfTemplatesAllowed;
                    var allowedtemplateworkflow = tenantsubscription.AllowTemplateWorkflows || subscription.AllowTemplateWorkflows;

                    var usedpages = tenantsubscription.NumberOfPagesUsed + subscription.NumberOfPagesUsed;
                    var usedforms = tenantsubscription.NumberOfFormsUsed + subscription.NumberOfFormsUsed;
                    var usedtemplates = tenantsubscription.NumberOfTemplatesUsed+ subscription.NumberOfTemplatesUsed;
                    var usedusers = tenantsubscription.NumberOfUsersUsed + subscription.NumberOfUsersUsed;
                    
                    tenantsubscription.IsActive = false;

                    var newtenantsubscriptionMaster = new TenantSubscription();
                    newtenantsubscriptionMaster.NumberOfFormsAllowed = allowedforms;
                    newtenantsubscriptionMaster.NumberOfPagesAllowed = allowedpages;
                    newtenantsubscriptionMaster.NumberOfUsersAllowed = allowedusers;
                    newtenantsubscriptionMaster.NumberOfTemplatesAllowed = allowedtemplates;
                    newtenantsubscriptionMaster.NumberOfPagesUsed = usedpages;
                    newtenantsubscriptionMaster.NumberOfFormsUsed = usedforms;
                    newtenantsubscriptionMaster.NumberOfTemplatesUsed = usedtemplates;
                    newtenantsubscriptionMaster.NumberOfUsersUsed = usedusers;
                    newtenantsubscriptionMaster.AllowBranding = Branding;
                    newtenantsubscriptionMaster.AllowScanning = Scanning;
                    newtenantsubscriptionMaster.IsActive = true;
                    newtenantsubscriptionMaster.IsDemo = true;
                    newtenantsubscriptionMaster.DateTimeStart = DateTime.Parse(StartDate);
                    newtenantsubscriptionMaster.DateTimeExpires = DateTime.Parse(ExpiryDate);
                    newtenantsubscriptionMaster.AllowTemplateWorkflows = TemplateWorkflow;
                    newtenantsubscriptionMaster.RequireDelegationAcceptance = true;
                    newtenantsubscriptionMaster.Time = DateTime.UtcNow;
                    newtenantsubscriptionMaster.TenantId = tenantid;
                    newtenantsubscriptionMaster.TenantSubscriptionType = EntityMasterTenantType.Master;
                    newtenantsubscriptionMaster.SubscriptionId = subscriptionid;
                    result = MasterTenantSubscriptionManagement.CreateTenantSubscriptionAndDeactivateExistingTenantSubscription(MasterUserSession, tenantid, subscription, newtenantsubscriptionMaster, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    result = MasterTenantSubscriptionManagement.GetTenantSubcriptionsByTenantId(MasterUserSession, tenantid, out tenantsubscriptionlist, out exception);
                    if (exception != null)
                        throw exception;
                }
                return Json(tenantsubscriptionlist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}



