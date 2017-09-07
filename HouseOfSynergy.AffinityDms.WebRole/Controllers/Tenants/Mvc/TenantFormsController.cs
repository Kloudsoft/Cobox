using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantFormsController : Controller
    {
        [HttpGet]
        public ActionResult Index(string ErrorMessage, string SuccessMessage)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            List<Template> templates = new List<Template>();
            try
            {
                User u = new User();
                // bool dbresult = ElementManagement.GetAllTemplates(tenantUserSession, out templates, out exception);
                bool dbresult = ElementManagement.GetAllMaxVersionTemplates(tenantUserSession, out templates, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (templates.Count>0)
                {
                    templates = templates.Where(x => x.TemplateType == TemplateType.Form).ToList();
                }
                if (!(string.IsNullOrEmpty(ErrorMessage)))
                {
                   throw (new Exception(ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            if (!(string.IsNullOrEmpty(SuccessMessage)))
            {
                this.ViewBag.SuccessMessage = SuccessMessage;
            }
            else
            {
                this.ViewBag.SuccessMessage = string.Empty;
            }
            return this.View("~/Views/Tenants/Forms/Forms.cshtml", templates);

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
            return RedirectToAction("Index", "TenantFormDesign", new { id = template.Id });
            //return this.View("~/Views/Tenants/Templates/TemplateDesign.cshtml", model);
        }


        public JsonResult SetUserRights(List<long> UserList, long Id)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                result = TenantUserManagement.UserRightsForTemplates(tenantUserSession, Id, UserList, out exception);
                if (!result) { if (exception != null) { throw exception; } }
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(exception);
            }
            return Json(result);
        }

        public ActionResult GetAllUsersForForm(long templateId)
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
                    result = TemplateAndFormHelper.GetUsersForSelection(tenantUserSession, templateId, TemplateType.Form, out users, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    throw (new Exception("Unable to find the following Form"));
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
                bool res = TemplateAndFormHelper.SetSelectedUsers(tenantUserSession, templateId, selectedUsers, TemplateType.Form, out users, out exception);
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
                    if (template == null) { throw (new Exception("Unable to find the following form")); }
                    if (template.UserId != tenantUserSession.User.Id) { throw (new Exception("You do not have permission to invite users")); }
                    if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrEmpty(email))
                    {
                        result = TenantUserManagement.GetUserByEmail(tenantUserSession, email,out user, out exception);
                        if (exception != null) { throw exception; }
                        if (user.Id > 0) { throw (new Exception("User already esists.")); }
                        //var userName = email;
                        var password = "";
                        result = TenantUserManagement.InviteUser(tenantUserSession, email, Url.Action("Index", "TenantFormProtectedView", new { id = templateId }), templateId, "Form", out password, out userObj, out exception);
                        if (exception != null) { throw exception; }
                        if (userObj.Id > 0)
                        {
                            var AppConfig = System.Configuration.ConfigurationManager.AppSettings;
                            //string recieverName = userObj.UserName;
                            string senderName = tenantUserSession.User.NameFull;
                            string url = "http://" + (new Uri(tenantUserSession.Tenant.UrlApi).Host) + "/TenantSignIn/AcceptInvite?key=" + userObj.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;
                            string emailBody = EmailTemplateConstants.InviteExternalUserByEmail.Replace("[{RecieverEmail}]", userObj.Email).Replace("[{SenderUsername}]", tenantUserSession.User.NameFull).Replace("[{SenderCompanyName}]", tenantUserSession.Tenant.CompanyName).Replace("[{EnitityType}]", EntityType.Form.ToString()).Replace("[{sharedEntityName}]", template.Title).Replace("[{LinkForInvite}]", url).Replace("[{RecieverEmail}]", userObj.Email).Replace("[{RecieverPassword}]", password);
                            // string emailBody = $"<strong>Hi {recieverName},</strong> <br>You have been invited by {senderName} for a discussion. Please click <a href='{ url }'><strong>here</strong></a> or copy-paste the link in your browser and login with the following credentials:<br>" + url + "<br>UserName:" + userName + "<br>Password:" + password;
                            string subject = "Invitation for Form";
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



    }
}