using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.ResourceProvider;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Common;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using System.Text;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantSignInController :
        Controller
    {
        [HttpGet]
        public ActionResult Index(string ErrorMessage, string SuccessMessage)
        {
            var model = new SignInViewModel();

            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;

            if (AffinityConfiguration.IsConfigurationDebug)
            {
                if (this.Request.Url.Authority.Contains("localhost"))
                {
                    model.Domain = "kloud-soft.com";

                    if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
                    {
                        model.UserName = "admin";
                        model.Password = "audience";
                    }
                }
                else if (this.Request.Url.Authority.Contains("coboxdev"))
                {
                    model.Domain = "kloud-soft.com";

                    if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
                    {
                        model.UserName = "admin";
                        model.Password = "audience";
                    }
                }
                else if (this.Request.Url.Authority.Contains("cobox-demo"))
                {
                    model.Domain = "cobox.com";

                    if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
                    {
                        model.UserName = "admin";
                        model.Password = "audience";
                    }
                }
                else
                {
                    model.Domain = "";
                    model.UserName = "";
                    model.Password = "";
                }
            }
            if (ErrorMessage != null) { this.ViewBag.ErrorMessage = ErrorMessage; }
            if (SuccessMessage != null) { this.ViewBag.SuccessMessage = SuccessMessage; }

            return (this.View("~/Views/Tenants/SignIn.cshtml", model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInViewModel signInViewModel)
        {
            Tenant tenant = null;
            var passwordHash = "";
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!MasterTenantManagement.GetTenantByDomain(signInViewModel.Domain, out tenant, out exception)) { throw (exception); }
                if (!Sha.GenerateHash((signInViewModel.Password ?? ""), GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind, out passwordHash, out exception)) { throw (exception); }

                if
                (
                    AuthenticationManagement.SignIn
                    (
                        SessionType.Mvc,
                        signInViewModel.Domain,
                        signInViewModel.UserName,
                        passwordHash,
                        this.Request.UserHostAddress,
                        this.Request.UserAgent,
                        long.MaxValue,
                        this.Session.SessionID,
                        out tenantUserSession,
                        out exception
                    )
                )
                {
                    HttpCookie cookie = null;

                    if (!TenantUserSessionResolver.GetTenantUserSessionHttpCookieFromTenantUserSession(tenantUserSession, out cookie, out exception)) { throw (exception); }

                    this.Response.Cookies.Remove(TenantUserSessionResolver.TenantUserSessionCookieName);
                    this.Response.SetCookie(cookie);

                    var userViewModel = new UserCurrentViewModel();
                    userViewModel.NameGiven = tenantUserSession.User.NameGiven;
                    userViewModel.NameFamily = tenantUserSession.User.NameGiven;
                    userViewModel.Email = tenantUserSession.User.Email;
                    userViewModel.UserName = tenantUserSession.User.UserName;
                    userViewModel.UserType = tenantUserSession.User.AuthenticationType;
                    userViewModel.Company = tenantUserSession.Tenant.CompanyName;
                    userViewModel.Domain = tenantUserSession.Tenant.Domain;
                    if (tenantUserSession.User.AuthenticationType == AuthenticationType.External)
                    {
                        return RedirectToAction("Index", "TenantDocumentsFolderWise");
                    }
                    return (this.RedirectToRoute("TenantDashboard"));
                }
                else
                {
                    throw (exception);
                }
            }
            catch (Exception ex)
            {
                //this.ModelState.AddModelError(string.Empty, ExceptionUtilities.ExceptionToString(ex));
                //return (this.View("~/Views/Tenants/SignIn.cshtml", signInViewModel));
                return RedirectToAction("Index", "TenantSignIn", new { ErrorMessage = ExceptionUtilities.ExceptionToString(ex), SuccessMessage = string.Empty });
            }

        }
        [HttpGet]
        public ActionResult AcceptInvite(Guid? key, string domain, string password = "")
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            UserViewModel userViewModel = new UserViewModel();
            Tenant tenant = null;
            if (key == null) { throw (new Exception("Invalid Key")); }
            if (string.IsNullOrEmpty(domain) || string.IsNullOrWhiteSpace(domain)) { throw (new Exception("Domain can not be left blank")); }

            try
            {
                bool result = AuthenticationManagement.ValidateInviteKey(SessionType.Mvc, (Guid)key, domain, double.Parse(System.Configuration.ConfigurationManager.AppSettings["InviteTimeOutHrs"]), out tenant, out exception);
                if (exception != null) { throw exception; }
                if (result == true)
                {
                    userViewModel.Domain = domain;
                    userViewModel.Key = key.ToString();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "TenantSignIn", new { ErrorMessage = ExceptionUtilities.ExceptionToString(ex), SuccessMessage = string.Empty });
            }
            return this.View("~/Views/Tenants/AcceptInvite.cshtml", userViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptInvite(UserViewModel userViewModel, string currentPassword, string confirmPassword)
        {
            Exception exception = null;
            Tenant tenant = null;
            var user = new User();
            User UserObj = null;
            try
            {
                if (userViewModel == null) { throw (new Exception("Invalid Key")); }
                if (string.IsNullOrEmpty(userViewModel.NameGiven.Trim())) { throw (new Exception("Name  Given can not be left blank")); }
                if (string.IsNullOrEmpty(userViewModel.NameFamily.Trim())) { throw (new Exception("Name Family can not be left blank")); }
                if (string.IsNullOrEmpty(userViewModel.Password.Trim())) { throw (new Exception("Password can not be left blank")); }
                if (string.IsNullOrEmpty(confirmPassword.Trim())) { throw (new Exception("Confirm password can not be left blank")); }
                if (string.IsNullOrEmpty(userViewModel.UserName.Trim())) { throw (new Exception("User Name can not be left blank")); }
                if (string.IsNullOrEmpty(userViewModel.Domain.Trim())) { throw (new Exception("Domain can not be left blank")); }
                if (string.IsNullOrEmpty(userViewModel.Key.Trim())) { throw (new Exception("Key can not be left blank")); }
                if (confirmPassword != userViewModel.Password) { throw (new Exception("Password and Confirm password didn't match")); }
                if (string.IsNullOrEmpty(currentPassword.Trim())) { throw (new Exception("Current password can not be left blank")); }
                if (string.IsNullOrEmpty(userViewModel.Email.Trim())) { throw (new Exception("Email is required.")); }

                user.NameGiven = userViewModel.NameGiven;
                user.NameFamily = userViewModel.NameFamily;
                user.PasswordHash = userViewModel.Password;
                user.UserName = userViewModel.UserName;
                user.Tenant = new Tenant();
                user.Tenant.Domain = userViewModel.Domain;
                user.InviteGuid = (Guid)Guid.Parse(userViewModel.Key);
                bool result = AuthenticationManagement.ValidateInviteKeyAndCredentials(SessionType.Mvc, (Guid)Guid.Parse(userViewModel.Key), userViewModel.Domain, userViewModel.Email, currentPassword, double.Parse(System.Configuration.ConfigurationManager.AppSettings["InviteTimeOutHrs"]), out tenant, out exception);
                if (exception != null) { throw exception; }
                if (result)
                {

                    result = TenantUserManagement.UpdateUserByGuid(tenant, user, out UserObj, out exception);
                    if (exception != null) { throw exception; }
                    if (!result) { throw new Exception("Unable to Update User"); }

                    if (UserObj.Id > 0)
                    {
                        var AppConfig = System.Configuration.ConfigurationManager.AppSettings;
                        string recieverName = UserObj.UserName;
                        string senderName = AppConfig["EmailSenderName"];
                        //string webName = AppConfig["WebName"];
                        string url = "http://" + (new Uri(tenant.UrlApi).Host) + "/";
                        string emailBody = EmailTemplateConstants.GreetingsMessage.Replace("[{RecieverName}]", recieverName).Replace("[{URL}]", url).Replace("[{RecieverCompanyName}]", UserObj.Tenant.CompanyName);
                        string subject = EmailTemplateConstants.GreetingsSubject.Replace("[{RecieverName}]", recieverName).Replace("[{RecieverCompanyName}]", UserObj.Tenant.CompanyName);
                        EmailUtilities emailUtil = new EmailUtilities(UserObj.Email.ToString(), AppConfig["EmailFrom"].ToString(), AppConfig["EmailPassword"].ToString(), emailBody, senderName, subject, int.Parse(AppConfig["EmailPort"]), AppConfig["EmailHost"], out exception, Boolean.Parse(AppConfig["IsBodyHtml"]), Boolean.Parse(AppConfig["IsBodyHtml"]));
                        if (exception != null) { throw exception; }
                        result = emailUtil.SendEmail(out exception);
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
                userViewModel.Password = "";
                userViewModel.UserName = "";
                return this.View("~/Views/Tenants/AcceptInvite.cshtml", userViewModel);
            }
            return RedirectToAction("Index");
        }


    }
}