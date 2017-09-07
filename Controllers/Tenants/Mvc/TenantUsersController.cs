using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Newtonsoft.Json;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantUsersController :
        Controller
    {
        [HttpGet]
        public ActionResult Index(string ErrorMessage, string SuccessMessage)
        {
            Exception exception = null;
            var userEntities = new List<Entities.Tenants.User>();
            TenantUserSession tenantUserSession = null;
            var userViewModels = new List<UserViewModel>();
            this.ViewBag.Action = "Index";
            this.ViewBag.Controller = "TenantUsers";
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
                return (this.View("~/Views/Tenants/Users/Users.cshtml", userViewModels));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (this.View("~/Views/Tenants/Users/Users.cshtml", userViewModels));
        }
        [HttpPost]
        public ActionResult Index(UserViewModel[] item)
        {
            var userEntities = new List<Entities.Tenants.User>();
            
            var userViewModels = new List<UserViewModel>();
            Exception exception = null;
            bool result = false;
            TenantUserSession tenantUserSession = null;
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            this.ViewBag.Action = "Index";
            this.ViewBag.Controller = "TenantUsers";
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                userViewModels = UpdateUserRoles(tenantUserSession, item);
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            // TODO: Update DB.
            return (this.View("~/Views/Tenants/Users/Users.cshtml", userViewModels));
        }
        public static List<UserViewModel> UpdateUserRoles(TenantUserSession tenantUserSession, UserViewModel[] item)
        {
            var userEntities = new List<Entities.Tenants.User>();

            

            var userViewModels = new List<UserViewModel>();
            Exception exception = null;
            bool result = false;

            for (int i = 0; i < item.Length; i++)
            {

                var admin = item[i].RoleAdministrator;

                var sitemanager = item[i].RoleSiteManager;
                var sitepo = item[i].RoleSitePO;
                var portfoliom = item[i].RolePoftfolioManager;

                var ssoad = item[i].RoleSSOAD;
                var hqap = item[i].RoleHQAP;

                var custom = item[i].RoleCustom;
                var reporting = item[i].RoleReporting;
                var formcreator = item[i].RoleFormCreator;
                var indexer = item[i].RoleIndexer;
                var scanner = item[i].RoleScanner;
                var templatecreator = item[i].RoleTemplateCreator;
                var uploader = item[i].RoleUploader;
                var workflowactor = item[i].RoleWorkflowActor;
                var workflowcreator = item[i].RoleWorkflowCreator;
                long userid = item[i].Id;
                List<TenantRoleType> userroles = new List<TenantRoleType>();

                if (admin)
                {
                    userroles.Add(TenantRoleType.Administrator);
                }
                if (sitemanager)
                {
                    userroles.Add(TenantRoleType.SiteManager);
                }

                if (sitepo)
                {
                    userroles.Add(TenantRoleType.SitePO);
                }

                if (portfoliom)
                {
                    userroles.Add(TenantRoleType.PoftfolioManager);
                }

                //====

                if (ssoad)
                {
                    userroles.Add(TenantRoleType.SSOAD);
                }
                if (hqap)
                {
                    userroles.Add(TenantRoleType.HQAP);
                }
                //====

                if (custom)
                {
                    userroles.Add(TenantRoleType.Custom);
                }

                if (reporting)
                {
                    userroles.Add(TenantRoleType.Reporting);
                }
                if (formcreator)
                {
                    userroles.Add(TenantRoleType.FormCreator);
                }
                if (indexer)
                {
                    userroles.Add(TenantRoleType.Indexer);
                }
                if (scanner)
                {
                    userroles.Add(TenantRoleType.Scanner);
                }
                if (templatecreator)
                {
                    userroles.Add(TenantRoleType.TemplateCreator);
                }
                if (uploader)
                {
                    userroles.Add(TenantRoleType.Uploader);
                }
                if (workflowactor)
                {
                    userroles.Add(TenantRoleType.WorkflowActor);
                }
                if (workflowcreator)
                {
                    userroles.Add(TenantRoleType.WorkflowCreator);
                }
                result = TenantUserManagement.UpdateUserRoles(tenantUserSession, userid, userroles, out exception);
                if (exception != null)
                {
                    throw exception;
                }
            }
            result = TenantUserManagement.GetUsers(tenantUserSession, out userEntities, out exception);
            if (exception != null) { throw (exception); }
            //foreach (var userEntity in userEntities)
            //{
            //    userViewModels.Add(new UserViewModel().FromEntity(userEntity));
            //}
            return userViewModels;
        }
        public static bool GetAllUsers(TenantUserSession tenantUserSession, out List<Entities.Tenants.User> users, out Exception exception)
        {
            exception = null;
            bool result = false;
            users = null;
            try
            {
                if (tenantUserSession != null)
                {
                    result = TenantUserManagement.GetUsers(tenantUserSession, out users, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    throw (new TokenInvalidException());
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static List<AzureTenantConflictViewModel> SyncAdUsers(TenantUserSession tenantUserSession)
        {
            List<AzureTenantConflictViewModel> azureuserconflictsVM = new List<AzureTenantConflictViewModel>();
            ActiveDirectoryClient activeDirectoryClient = null;
            Exception exception = null;
            bool result = AzureHelper.SetActiveDirectoryClient(out activeDirectoryClient, out exception);
            if (exception != null) { throw exception; }
            if (activeDirectoryClient != null && result)
            {
                List<IUser> azureuserlist = null;
                result = AzureHelper.GetUsers(activeDirectoryClient, out azureuserlist, out exception);

                if (exception != null) { throw exception; }
                if (result)
                {
                    foreach (var azureuser in azureuserlist)
                    {
                        Entities.Tenants.User user = new Entities.Tenants.User();
                        user.ActiveDirectory_AuthenticationEmail = azureuser.UserPrincipalName.ToString();
                        user.ActiveDirectory_AuthenticationPhone = (azureuser.TelephoneNumber != null) ? (azureuser.TelephoneNumber) : ("");
                        user.ActiveDirectory_AuthenticationPhoneAlternate = "";
                        user.ActiveDirectory_Department = azureuser.Department;
                        user.ActiveDirectory_JobTitle = azureuser.JobTitle;
                        user.ActiveDirectory_ManagerId = (azureuser.Manager != null) ? (Guid.Parse(azureuser.Manager.ObjectId)) : (Guid.Empty);
                        user.ActiveDirectory_NameDisplay = azureuser.DisplayName;
                        user.ActiveDirectory_ObjectId = Guid.Parse(azureuser.ObjectId);
                        user.ActiveDirectory_UsageLocation = azureuser.UsageLocation;
                        user.Address1 = (azureuser.StreetAddress != null) ? (azureuser.StreetAddress) : ("");
                        user.AuthenticationType = AuthenticationType.ActiveDirectory;
                        user.City = (azureuser.City != null) ? (azureuser.City) : ("");
                        user.Country = (azureuser.Country != null) ? (azureuser.Country) : ("");
                        user.DateTimeCreated = DateTime.UtcNow;
                        user.Email = azureuser.UserPrincipalName.ToString();
                        user.DepartmentId = 1;
                        user.TenantId = tenantUserSession.User.TenantId;
                        user.NameGiven = (azureuser.GivenName != null) ? (azureuser.GivenName) : ("");
                        user.PasswordHash = (azureuser.PasswordProfile != null) ? ((!(string.IsNullOrEmpty(azureuser.PasswordProfile.Password))) ? (azureuser.PasswordProfile.Password) : ("passworddefault")) : ("passworddefault");
                        user.PasswordSalt = (azureuser.PasswordProfile != null) ? ((!(string.IsNullOrEmpty(azureuser.PasswordProfile.Password))) ? (azureuser.PasswordProfile.Password) : ("saltdefault")) : ("saltdefault");
                        user.NameFamily = (azureuser.Surname != null) ? (azureuser.Surname) : ("");
                        user.UserName = azureuser.UserPrincipalName.Split('@').First();
                        user.Address2 = "";
                        user.PhoneWork = "";
                        //USER ROLES ARE DIFFERENT IN AZURE
                        //user.ActiveDirectory_RoleDisplayName = azureuser.AppRoleAssignments.CurrentPage.First().PrincipalType.ToString();
                        user.ZipOrPostCode = (azureuser.PostalCode != null) ? (azureuser.PostalCode) : ("");
                        //??
                        var ut = azureuser.UserType;
                        user.PhoneMobile = (azureuser.Mobile != null) ? (azureuser.Mobile) : ("");
                        //??
                        var company = azureuser.CompanyName;
                        bool UserMatched = false;
                        Entities.Tenants.User outtenantuser = null;
                        result = TenantUserManagement.AddADUser(tenantUserSession, user, out UserMatched, out outtenantuser, out exception);
                        if (exception != null) { throw exception; }
                        if (UserMatched)
                        {
                            AzureTenantConflictViewModel azureConflictVM = new AzureTenantConflictViewModel();
                            azureConflictVM.AzureUser = user;
                            azureConflictVM.TenantUser = outtenantuser;
                            azureConflictVM.Conflicted = true;
                            azureConflictVM.Reason = "Conflict between AD User and Local User Occurred";
                            azureuserconflictsVM.Add(azureConflictVM);
                        }

                    }
                }
            }
            return azureuserconflictsVM;
        }
        public ActionResult SyncFromAD()
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
                return (RedirectToAction("Index", "TenantUsers", new { ErrorMessage = ("Unable to Sync: " + ex.Message).ToString(), SuccessMessage = string.Empty }));
            }
            if (azureuserconflictsVM.Count > 0)
            {
                return (this.View("~/Views/Tenants/Users/TenantAzureUserConflicts.cshtml", azureuserconflictsVM));
            }
            else
            {
                return (RedirectToAction("Index", "TenantUsers", new { ErrorMessage = string.Empty, SuccessMessage = "Sync completed successfully. No conflicts found" }));
            }

        }
        public ActionResult GetUserRightsList(long Id, string Type)
        {

            Exception exception = null;
            var userEntities = new List<Entities.Tenants.User>();
            TenantUserSession tenantUserSession = null;
            var userViewModels = new List<UserViewModel>();
            Document document = null;
            Folder folder = null;
            Template template = null;
            Discourse discourse = null;
            bool result = false;
            try
            {
                if (Id > 0 && (!string.IsNullOrEmpty(Type)))
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    result = TenantUserManagement.GetUsers(tenantUserSession, out userEntities, out exception);
                    if (exception != null) { throw (exception); }
                    foreach (var userEntity in userEntities)
                    {
                        var userVM = new UserViewModel().UserRights(userEntity, Type, Id);
                        userViewModels.Add(userVM);
                    }
                    switch (Type)
                    {
                        case "Document":
                        {
                            result = DocumentManagement.GetDocumentById(tenantUserSession, Id, out document, out exception);
                            if (exception != null) { throw (exception); }
                            if (document != null)
                            {
                                var docCreator = userViewModels.Where(x => x.Id == document.UserId).FirstOrDefault();
                                if (docCreator != null)
                                {
                                    userViewModels.Remove(docCreator);
                                }
                            }
                            break;
                        }
                        case "Form":
                        case "Template":
                        {
                            result = ElementManagement.GetTemplateByTemplateId(tenantUserSession, Id, out template, out exception);
                            if (exception != null) { throw (exception); }
                            if (template != null)
                            {
                                var tempCreator = userViewModels.Where(x => x.Id == template.UserId).FirstOrDefault();
                                if (tempCreator != null)
                                {
                                    userViewModels.Remove(tempCreator);
                                }
                            }
                            break;
                        }
                        
                        case "Discourse":
                        {
                            result = DiscourseManagement.GetDiscourseById(tenantUserSession, Id, out discourse, out exception);
                            if (exception != null) { throw (exception); }
                            if (discourse != null)
                            {
                                var discourseCreator = userViewModels.Where(x => x.Id == discourse.Posts.First().UserId).FirstOrDefault();
                                if (discourseCreator != null)
                                {
                                    userViewModels.Remove(discourseCreator);
                                }
                            }
                            break;
                        }
                        
                        case "Folder":
                        {

                            result = FolderManagement.GetFolderById(tenantUserSession, Id, out folder, out exception);
                            if (exception != null) { throw (exception); }
                            if (folder != null)
                            {
                                var folderCreator = userViewModels.Where(x => x.Id == folder.UserCreatedById).FirstOrDefault();
                                if (folderCreator != null)
                                {
                                    userViewModels.Remove(folderCreator);
                                }
                            }
                            break;
                        }
                        default:
                        break;
                    }
                    result = true;
                }

            }
            catch (Exception ex)
            {
                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex);
                return PartialView("~/Views/Tenants/Users/_TenantUserRightsSelection.cshtml", null);
            }
            return PartialView("~/Views/Tenants/Users/_TenantUserRightsSelection.cshtml", userViewModels);
        }
        [HttpGet]
        public JsonResult GetIndexUsers()
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            List<Entities.Tenants.User> users = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                result = TenantUserManagement.GetUsersByRoles(tenantUserSession, TenantRoleType.Indexer, out users, out exception);
                if (users == null) { users = new List<Entities.Tenants.User>(); }
            }
            catch (Exception ex) { exception = ex; }
            if (exception != null)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
            var userStr = JsonConvert.SerializeObject(users);
            return Json(userStr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubmitAssignment(long id, long userId, DiscussionPostAttachmentType docType)//id: hdocId.value, userId:userid, docType: DiscussionPostAttachmentType.Document }),
        {
            bool result = false;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            if (id > 0)
            {
                if (userId > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    result = TenantUserManagement.AssignUser(tenantUserSession, id, userId, docType, out exception);
                }
            }
            if (exception != null)
            {
                return Json(exception.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DoSomething()
        {
            return Content("<script language='javascript' type='text/javascript'>confirm('Press a button!');</script>");
            //return Json(new { isok = true, message = "Your Message", data = "~/Views/Tenants/Users/Users.cshtml" },JsonRequestBehavior.AllowGet);

            //True / False Json Return
            //return UserObj == null ?
            //Json(true, JsonRequestBehavior.AllowGet) :
            //Json(string.Format("YourObject '{0}' to String", YourObject),
            //JsonRequestBehavior.AllowGet);

        }



        //[HttpGet]
        //      public ActionResult Index()
        //      {
        //	Exception exception = null;
        //	var userEntities = new List<User>();
        //	TenantUserSession tenantUserSession = null;
        //	var userViewModels = new List<UserViewModel>();

        //	if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

        //	if (!TenantUserManagement.GetUsers(tenantUserSession, out userEntities, out exception)) { throw (exception); }

        //	foreach (var userEntity in userEntities)
        //	{
        //		userViewModels.Add(new UserViewModel().FromEntity(userEntity));
        //	}

        //	return (this.View("~/Views/Tenants/Users/Users.cshtml", userViewModels));
        //      }

        //[HttpPost]
        //public ActionResult Index (IEnumerable<UserViewModel> users)
        //{
        //	// TODO: Update DB.
        //	return (this.View("~/Views/Tenants/Users/Users.cshtml", users));
        //}
    }
}