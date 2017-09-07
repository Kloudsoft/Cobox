﻿using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using Newtonsoft.Json;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TemplatesWorkflowController : Controller
    {
        // GET: TenantTemplates
        [HttpGet]
        public ActionResult Index(string ErrorMessage, string SuccessMessage)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            List<Document> document = new List<Document>();
            try
            {
                //TenantRoleType.SitePO

                long DraftFolder = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);


                //GetMaxVersionDocuments
                bool dbresult = false;
                if (tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.Administrator))  // Admin
                {
                    dbresult = DocumentManagement.GetAllDocuments(tenantUserSession, out document, out exception);
                    document = DocumentManagement.GetMaxVersionDocuments(tenantUserSession, document);
                    if (exception != null){throw exception;}
                    if (document.Count > 0){
                        document = document.Where(x => x.WorkflowState == DocumentWorkflowState.Draft
                                                         || x.WorkflowState == DocumentWorkflowState.Submitted
                                                         || x.WorkflowState == DocumentWorkflowState.Rework
                                                         || x.WorkflowState == DocumentWorkflowState.ReworkPM
                                                         || x.WorkflowState == DocumentWorkflowState.ReworkSM
                                                         || x.WorkflowState == DocumentWorkflowState.ReworkSSOAD
                                                         || x.WorkflowState == DocumentWorkflowState.Recommend
                                                         || x.WorkflowState == DocumentWorkflowState.Advised
                                                         || x.WorkflowState == DocumentWorkflowState.Approved
                                                         || x.WorkflowState == DocumentWorkflowState.ProcessPayment
                                                         || x.WorkflowState == DocumentWorkflowState.Closed).ToList();

                        //document = document.Where(x => x.FolderId != DraftFolder).ToList();
                        //document = document.Join  

                        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                        {

                            var Docs = from s in document
                                       join sa in context.Folders on s.FolderId equals sa.Id
                                       where sa.ParentId == DraftFolder
                                       select s;

                            document = Docs.ToList();
                        }

                    }
                }
                else if (tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.SitePO))  // SitePo   SELECT Draft,ReworkSM  and DO Recommend
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        document = context.Documents.Where(x => x.WorkflowState == DocumentWorkflowState.Draft
                                                                || x.WorkflowState == DocumentWorkflowState.ReworkSM).ToList();
                        document = DocumentManagement.GetAllMaxVersionDocuments(tenantUserSession, document);
                        document = document.Where(x => x.FolderId != DraftFolder).ToList();

                        var Docs = from s in document
                                   join sa in context.Folders on s.FolderId equals sa.Id
                                   where sa.ParentId == DraftFolder
                                   select s;

                        document = Docs.ToList();

                    }
                }
                else if (tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.SiteManager))  // SiteManager  SELECT Recommend,ReworkPM  and DO Advise
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        document = context.Documents.Where(x => x.WorkflowState == DocumentWorkflowState.Recommend
                                                                || x.WorkflowState == DocumentWorkflowState.ReworkPM).ToList();
                        document = DocumentManagement.GetAllMaxVersionDocuments(tenantUserSession, document);
                        document = document.Where(x => x.FolderId != DraftFolder).ToList();

                        var Docs = from s in document
                                   join sa in context.Folders on s.FolderId equals sa.Id
                                   where sa.ParentId == DraftFolder
                                   select s;

                        document = Docs.ToList();

                    }
                }

                else if (tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.PoftfolioManager))  // PortFolio Manager SELECT Advised,ReworkSSOAD  and DO Approve
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        document = context.Documents.Where(x => x.WorkflowState == DocumentWorkflowState.Advised
                                                                || x.WorkflowState == DocumentWorkflowState.ReworkSSOAD).ToList();
                        document = DocumentManagement.GetAllMaxVersionDocuments(tenantUserSession, document);
                        document = document.Where(x => x.FolderId != DraftFolder).ToList();

                        var Docs = from s in document
                                   join sa in context.Folders on s.FolderId equals sa.Id
                                   where sa.ParentId == DraftFolder
                                   select s;

                        document = Docs.ToList();

                    }
                }

                else if (tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.SSOAD))  // SSOAD SELECT Approved and DO Process Payment
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        document = context.Documents.Where(x => x.WorkflowState == DocumentWorkflowState.Approved).ToList();
                        document = DocumentManagement.GetAllMaxVersionDocuments(tenantUserSession, document);
                        document = document.Where(x => x.FolderId != DraftFolder).ToList();

                        var Docs = from s in document
                                   join sa in context.Folders on s.FolderId equals sa.Id
                                   where sa.ParentId == DraftFolder
                                   select s;

                        document = Docs.ToList();

                    }
                }
                else if (tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.HQAP))  // HQ AP SELECT Process Payment and DO Complete
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        document = context.Documents.Where(x => x.WorkflowState == DocumentWorkflowState.ProcessPayment).ToList();
                        document = DocumentManagement.GetAllMaxVersionDocuments(tenantUserSession, document);
                        document = document.Where(x => x.FolderId != DraftFolder).ToList();

                        var Docs = from s in document
                                   join sa in context.Folders on s.FolderId equals sa.Id
                                   where sa.ParentId == DraftFolder
                                   select s;

                        document = Docs.ToList();

                    }
                }


            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            if (!(string.IsNullOrEmpty(ErrorMessage)))
            {
                this.ViewBag.ErrorMessage = ErrorMessage;
            }
            else
            {
                this.ViewBag.ErrorMessage = string.Empty;
            }
            if (!(string.IsNullOrEmpty(SuccessMessage)))
            {
                this.ViewBag.SuccessMessage = SuccessMessage;
            }
            else
            {
                this.ViewBag.SuccessMessage = string.Empty;
            }
            this.ViewBag.CurrentUser = tenantUserSession.User.Id;

            return this.View("~/Views/Tenants/Templates/TemplatesWorkflow.cshtml", document);

        }

      

        public ActionResult CheckoutTemplate(long id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Template template = null;
            List<TemplateElement> templateElement = null;
            List<TemplateElementDetail> templateElementDetail = null;
            bool result = false;
            TemplateElementListAndElementDetailListViewModel model = new TemplateElementListAndElementDetailListViewModel();

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                result = ElementManagement.CheckoutTemplateAndMakeNewVersion(tenantUserSession, id, out template, out templateElement, out templateElementDetail, out exception);
                if (exception != null) { throw exception; }

                model.template = new Template();
                model.elements = new List<TemplateElement>();
                model.elementsdetails = new List<TemplateElementDetail>();
                if (template != null)
                {
                    model.template = template;
                    if (templateElement != null)
                        model.elements = templateElement;
                    if (templateElementDetail != null)
                        model.elementsdetails = templateElementDetail;
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { id = -1, ErrorMessage = ExceptionUtilities.ExceptionToString(ex) });
            }
            return RedirectToAction("Index", "TenantTemplateTestDesign", new { id = template.Id });
            //return this.View("~/Views/Tenants/Templates/TemplateDesign.cshtml", model);
        }

        //[HttpPost]
        public ActionResult DeleteTemplate(Template template, string H_Templateid)
        {
            Exception exception = null;
            bool dbresult = false;
            long templateid = 0;
            try
            {
                H_Templateid = Request.QueryString["id"].ToString();
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                if (H_Templateid != null)
                {
                    templateid = long.Parse(H_Templateid);
                    Template sourcetemplate = null;
                    dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateid, out sourcetemplate, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((sourcetemplate != null) && dbresult)
                    {
                        if (sourcetemplate.TemplateType == TemplateType.Template)
                        {

                            dbresult = ElementManagement.DeleteTemplate(tenantUserSession, sourcetemplate, out exception);

                            if (exception != null)
                            {
                                throw exception;
                            }
                            if (dbresult)
                            {
                                this.ViewBag.ErrorMessage = string.Empty;
                                return RedirectToAction("Index", "TenantTemplates", null);
                            }
                            else
                            {
                                Exception ex = new Exception("Unable to update the template");
                                throw ex;
                            }
                        }
                        else
                        {
                            throw (new Exception("The following template is not a form type."));
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
                return this.View("~/Views/Tenants/Templates/", template);
            }
            return RedirectToAction("Index");
        }



        public JsonResult SetUserRights(List<long> UserList, long Id)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (Id > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                    result = TenantUserManagement.UserRightsForTemplates(tenantUserSession, Id, UserList, out exception);
                    if (!result) { if (exception != null) { throw exception; } }
                }
                else
                {
                    throw (new Exception("Unable to proccess user rights"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(exception);
            }
            return Json(result);
        }
        public ActionResult GetAllUsersForTemplate(long templateId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            List<User> users = null;
            try
            {
                if (templateId > 0)
                {
                    if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                    result = TemplateAndFormHelper.GetUsersForSelection(tenantUserSession, templateId, TemplateType.Template, out users, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    throw (new Exception("Unable to find the following Template"));
                }
            }
            catch (Exception ex)
            {

                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex);
                return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", null);
            }
            return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", users);
        }
        public JsonResult AddRemoveUser(long templateId, List<long> selectedUsers)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            List<User> users = new List<User>();

            try
            {
                if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                bool res = TemplateAndFormHelper.SetSelectedUsers(tenantUserSession, templateId, selectedUsers, TemplateType.Template, out users, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }

            var serialized = JsonConvert.SerializeObject(users, Formatting.Indented,
                                                            new JsonSerializerSettings
                                                            {
                                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                            });
            return Json(serialized);
        }
        public JsonResult InviteUser(long templateId, string email)
        {
            bool result = false;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            var userObj = new User();
            var user = new User();
            try
            {

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                if ((templateId > 0))
                {
                    Template template = null;
                    ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateId, out template, out exception);
                    if (template == null) { throw (new Exception("Unable to find the following template")); }
                    if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrEmpty(email))
                    {
                        result = TenantUserManagement.GetUserByEmail(tenantUserSession, email, out user, out exception);
                        if (exception != null) { throw exception; }
                        if (user.Id > 0) { throw (new Exception("User already esists.")); }
                        // var userName = email;
                        var password = "";
                        result = TenantUserManagement.InviteUser(tenantUserSession, email, Url.Action("Index", "TenantTemplateProtectedView", new { id = templateId }), templateId, "Template", out password, out userObj, out exception);
                        if (exception != null) { throw exception; }
                        if (userObj.Id > 0)
                        {
                            var AppConfig = System.Configuration.ConfigurationManager.AppSettings;
                            // string recieverName = userObj.UserName;
                            string senderName = tenantUserSession.User.NameFull;
                            string url = "http://" + (new Uri(tenantUserSession.Tenant.UrlApi).Host) + "/TenantSignIn/AcceptInvite?key=" + userObj.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;
                            string emailBody = EmailTemplateConstants.InviteExternalUserByEmail.Replace("[{RecieverEmail}]", userObj.Email).Replace("[{SenderUsername}]", tenantUserSession.User.NameFull).Replace("[{SenderCompanyName}]", tenantUserSession.Tenant.CompanyName).Replace("[{EnitityType}]", EntityType.Template.ToString()).Replace("[{sharedEntityName}]", template.Title).Replace("[{LinkForInvite}]", url).Replace("[{RecieverEmail}]", userObj.Email).Replace("[{RecieverPassword}]", password);
                            //string emailBody = $"<strong>Hi {recieverName},</strong> <br>You have been invited by {senderName} for a discussion. Please click <a href='{ url }'><strong>here</strong></a> or copy-paste the link in your browser and login with the following credentials:<br>" + url + "<br>UserName:" + userName + "<br>Password:" + password;
                            string subject = "Invitation for Template";
                            EmailUtilities emailUtil = new EmailUtilities(userObj.Email.ToString(), AppConfig["EmailFrom"].ToString(), AppConfig["EmailPassword"].ToString(), emailBody, senderName, subject, int.Parse(AppConfig["EmailPort"]), AppConfig["EmailHost"], out exception, Boolean.Parse(AppConfig["IsBodyHtml"]), Boolean.Parse(AppConfig["IsBodyHtml"]));
                            if (exception != null) { throw exception; }
                            result = emailUtil.SendEmail(out exception);
                        }
                        else
                        { result = false; }
                    }
                    else
                    { throw (new Exception("UserName and Email is Required")); }
                }
                else
                { throw (new Exception("Unable to find the following Template")); }
            }
            catch (Exception ex)
            {
                result = false;
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }
            return Json(result);
        }

        public static List<Template> GetAllTemplates(TenantUserSession tenantUserSession, out Exception exception)
        {
            exception = null;
            List<Template> templates = new List<Template>();
            bool dbresult = ElementManagement.GetAllMaxVersionTemplates(tenantUserSession, out templates, out exception);
            if (exception != null)
            {
                throw exception;
            }
            if (templates != null)
            {
                if (templates.Count > 0)
                {
                    templates = templates.Where(x => x.TemplateType == TemplateType.Template).ToList();
                }
            }
            else
            {
                templates = new List<Template>();
            }

            return templates;
        }
    }
}