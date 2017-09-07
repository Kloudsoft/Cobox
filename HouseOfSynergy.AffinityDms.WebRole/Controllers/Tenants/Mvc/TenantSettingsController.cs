using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantSettingsController : Controller
    {
        // GET: TenantSettings
        [HttpGet]
        public ActionResult Index(string ErrorMessage, string SuccessMessage, SettingsType settingsType = SettingsType.General)
        {
            Exception exception = null;
            var userEntities = new List<Entities.Tenants.User>();
            TenantUserSession tenantUserSession = null;
            var userViewModels = new List<UserViewModel>();
            this.ViewBag.Action = "UpdateUserSettings";
            this.ViewBag.Controller = "TenantSettings";
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                if (settingsType == SettingsType.UserManagement || settingsType == SettingsType.All)
                {
                    bool result = TenantUserManagement.GetUsers(tenantUserSession, out userEntities, out exception);
                    if (exception != null) { throw (exception); }
                    foreach (var userEntity in userEntities)
                    {
                        userViewModels.Add(new UserViewModel().FromEntity(userEntity));
                    }
                    this.ViewBag.UserList = userViewModels;
                }
                else if (settingsType == SettingsType.AuditTrail || settingsType == SettingsType.All)
                {
                    ContextTenant context = null;
                    List<AuditTrailEntry> audittrailentry = null;
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        AuditTrailManagement.GetAllAuditTrail(tenantUserSession, context, out audittrailentry);
                    }
                    ViewBag.AuditTrailList = audittrailentry;
                }
                if (!(string.IsNullOrEmpty(ErrorMessage))) { ViewBag.ErrorMessage = ErrorMessage; } else { ViewBag.ErrorMessage = string.Empty; }
                if (!(string.IsNullOrEmpty(SuccessMessage))) { ViewBag.SuccessMessage = SuccessMessage; } else { ViewBag.SuccessMessage = string.Empty; }
                return (this.View("~/Views/Tenants/Settings/Settings.cshtml"));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (this.View("~/Views/Tenants/Settings/Settings.cshtml"));
        }
        [HttpPost]
        public ActionResult UpdateUserSettings(UserViewModel[] item)
        {
            var userEntities = new List<Entities.Tenants.User>();
            var userViewModels = new List<UserViewModel>();
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            this.ViewBag.Action = "UpdateUserSettings";
            this.ViewBag.Controller = "TenantSettings";
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                userViewModels = TenantUsersController.UpdateUserRoles(tenantUserSession, item);
                List<User> users = new List<User>();

                foreach (var userViewModel in item)
                {
                    users.Add(userViewModel.ToEntity());
                }
                bool res = TenantUserManagement.UpdateUserRolesAndMarkActiveInactive(tenantUserSession, users,  out exception);
                if (exception != null) { throw (exception); }
                List<User> usersList = null;
                TenantUserManagement.GetUsers(tenantUserSession, out usersList, out exception);
                if (exception != null) { throw (exception); }
                if (usersList != null)
                {
                    foreach (var user in usersList)
                    {
                        UserViewModel userVM = new UserViewModel();
                        userViewModels.Add(userVM.FromEntity(user));
                    }
                }
                
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            this.ViewBag.UserList = userViewModels;
            return (this.View("~/Views/Tenants/Settings/Settings.cshtml"));
        }
        public ActionResult SyncFromADUserSettings()
        {

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<AzureTenantConflictViewModel> azureuserconflictsVM = new List<AzureTenantConflictViewModel>();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                azureuserconflictsVM = TenantUsersController.SyncAdUsers(tenantUserSession);
            }
            catch (Exception ex)
            {
                exception = ex;
                return (RedirectToAction("Index", "TenantSettings", new { ErrorMessage = ("Unable to Sync: " + ex.Message).ToString(), SuccessMessage = string.Empty, settingsType = SettingsType.UserManagement }));
            }
            if (azureuserconflictsVM.Count > 0)
            {
                return (this.View("~/Views/Tenants/Users/TenantAzureUserConflicts.cshtml", azureuserconflictsVM));
            }
            else
            {
                return (RedirectToAction("Index", "TenantSettings", new { ErrorMessage = string.Empty, SuccessMessage = "Sync completed successfully. No conflicts found", settingsType = SettingsType.UserManagement }));
            }

        }

        [HttpGet]
        public ActionResult GetUsersSettings(string ErrorMessage, string SuccessMessage)
        {
            Exception exception = null;
            var userEntities = new List<Entities.Tenants.User>();
            TenantUserSession tenantUserSession = null;
            var userViewModels = new List<UserViewModel>();
            this.ViewBag.Action = "UpdateUserSettings";
            this.ViewBag.Controller = "TenantSettings";
            try
            {

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                bool result = TenantUserManagement.GetUsers(tenantUserSession, out userEntities, out exception);
                if (exception != null) { throw (exception); }
                foreach (var userEntity in userEntities)
                {
                    userViewModels.Add(new UserViewModel().FromEntity(userEntity));
                }
                if (!(string.IsNullOrEmpty(ErrorMessage))) { ViewBag.ErrorMessage = ErrorMessage; } else { ViewBag.ErrorMessage = string.Empty; }
                if (!(string.IsNullOrEmpty(SuccessMessage))) { ViewBag.SuccessMessage = SuccessMessage; } else { ViewBag.SuccessMessage = string.Empty; }
                return (PartialView("~/Views/Tenants/Users/_Users.cshtml", userViewModels));
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return (PartialView("~/Views/Tenants/Users/_Users.cshtml", userViewModels));
        }
        public ActionResult UpdateAuditTrailSettings(string ErrorMessage, string SuccessMessage)
        {
            var userViewModels = new List<UserViewModel>();
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            List<AuditTrailEntry> audittrailentry = new List<AuditTrailEntry>();

            //this.ViewBag.Action = "UpdateUserSettings";
            //this.ViewBag.Controller = "TenantSettings";
            try
            {

                ContextTenant context = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    AuditTrailManagement.GetAllAuditTrail(tenantUserSession, context, out audittrailentry);
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return (PartialView("~/Views/Tenants/AuditTrailEntry/_TenantAuditTrailEntries.cshtml", audittrailentry));
        }

        public ActionResult SearchAuditTrail(DateTime DateTo, DateTime DateFrom)
        {
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            List<AuditTrailEntry> audittrailentry = new List<AuditTrailEntry>();
            if ((DateFrom > DateTo)) { throw new Exception("DateTimeFrom cannot be less than DateTimeTo "); }
            else if ((DateTo < DateFrom)) { throw new Exception("DateTimeTo cannot be less than DateTimeFrom "); }
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            try
            {
                ContextTenant context = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    AuditTrailManagement.GetAuditTrailByDateRange(tenantUserSession, context, DateTo, DateFrom, out audittrailentry);
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return (PartialView("~/Views/Tenants/AuditTrailEntry/_TenantAuditTrailEntries.cshtml", audittrailentry));

        }


    }
}