using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Utility;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDiscourseController : Controller
    {
        // GET: TenantDiscourse
        public ActionResult Index(long? id)
        {
            Exception exception = null;
            Discourse discourse = null;
            TenantUserSession tenantUserSession = null;
            this.ViewBag.CurrentUser = 0;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, Entities.Lookup.SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                if (id > 0)
                {
                    bool result = DiscourseManagement.GetDiscourseById(tenantUserSession, (long)id, out discourse, out exception);
                    if (exception != null) { throw exception; }
                    if (tenantUserSession.User.Id != discourse.Posts.First().UserId)
                    {
                        if (discourse.Users != null)
                        {
                            var founduser = discourse.Users.Where(x => (x.UserId == tenantUserSession.User.Id) && (x.IsActive)).ToArray();
                            if (founduser.Count() > 0)
                            {
                                var discourseusers = discourse.Users.Where(x => x.IsActive).ToList();
                                discourse.Users.Clear();
                                foreach (var discourseUser in discourseusers)
                                {
                                    discourse.Users.Add(discourseUser);
                                }
                            }
                            else
                            {
                                return RedirectToAction("Index", "TenantDiscourses", new { ErrorMessage = "You are not invited to view this chat", SuccessMessage = string.Empty });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "TenantDiscourses", new { ErrorMessage = "You are not invited to view this chat", SuccessMessage = string.Empty });
                        }
                    }
                    else
                    {
                        var discourseusers = discourse.Users.Where(x => x.IsActive).ToList();
                        discourse.Users.Clear();
                        foreach (var discourseUser in discourseusers)
                        {
                            discourse.Users.Add(discourseUser);
                        }
                    }

                }
                else
                {
                    discourse = new Discourse();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.CurrentUser = 0;
                return RedirectToAction("Index", "TenantDiscourses", new { ErrorMessage = "Unable to find the following chat", SuccessMessage = string.Empty });
            }
            return this.View("~/Views/Tenants/Discourses/Discourse.cshtml", discourse);
        }

        public ActionResult CreateDiscourse()
        {
            Exception exception = null;
            Discourse discourse = null;
            TenantUserSession tenantUserSession = null;
            List<User> users = null;
            this.ViewBag.CurrentUser = 0;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, Entities.Lookup.SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                bool result = TenantUserManagement.GetUsers(tenantUserSession, out users, out exception);
                if (exception != null) { throw exception; }
                if (users.Count > 0)
                {
                    this.ViewBag.UserList = users.Where(x => x.Id != tenantUserSession.User.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return PartialView("~/Views/Tenants/Discourses/_CreateDiscourse.cshtml");
        }
        public ActionResult PartialDiscourse(long? id)
        {
            Exception exception = null;
            Discourse discourse = null;
            TenantUserSession tenantUserSession = null;
            this.ViewBag.CurrentUser = 0;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, Entities.Lookup.SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                if (id > 0)
                {
                    bool result = DiscourseManagement.GetDiscourseById(tenantUserSession, (long)id, out discourse, out exception);
                    if (exception != null) { throw exception; }
                    if (tenantUserSession.User.Id != discourse.Posts.First().UserId)
                    {
                        if (discourse.Users != null)
                        {
                            var founduser = discourse.Users.Where(x => (x.UserId == tenantUserSession.User.Id) && (x.IsActive)).ToArray();
                            if (founduser.Count() > 0)
                            {
                                var discourseusers = discourse.Users.Where(x => x.IsActive).ToList();
                                discourse.Users.Clear();
                                foreach (var discourseUser in discourseusers)
                                {
                                    discourse.Users.Add(discourseUser);
                                }
                            }
                            else
                            {
                                return RedirectToAction("Index", "TenantDiscourses", new { ErrorMessage = "You are not invited to view this chat", SuccessMessage = string.Empty });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "TenantDiscourses", new { ErrorMessage = "You are not invited to view this chat", SuccessMessage = string.Empty });
                        }
                    }
                    else
                    {
                        var discourseusers = discourse.Users.Where(x => x.IsActive).ToList();
                        discourse.Users.Clear();
                        foreach (var discourseUser in discourseusers)
                        {
                            discourse.Users.Add(discourseUser);
                        }
                    }

                }
                else
                {
                    discourse = new Discourse();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.CurrentUser = 0;
                return RedirectToAction("Index", "TenantDiscourses", new { ErrorMessage = "Unable to find the following chat", SuccessMessage = string.Empty });
            }
            return PartialView("~/Views/Tenants/Discourses/_Discourse.cshtml", discourse);
        }
        public ActionResult GetAllUsersForDiscourse(long discourseId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            List<User> users = null;
            try
            {
                if (discourseId > 0)
                {
                    if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                    result = TenantUserManagement.GetUsers(tenantUserSession, out users, out exception);
                    if (exception != null) { throw exception; }
                    if (users.Count > 0)
                    {
                        Discourse discourse = null;
                        DiscourseManagement.GetDiscourseById(tenantUserSession, discourseId, out discourse, out exception);
                        if (exception != null) { throw exception; }
                        List<DiscourseUser> discourseUsers = null;
                        result = DiscourseManagement.GetDiscourseUsersByDiscourseId(tenantUserSession, discourseId, out discourseUsers, out exception);
                        if (exception != null) { throw exception; }
                        if (discourseUsers.Count > 0)
                        {
                            List<User> userList = users.ToList();
                            users.Clear();

                            foreach (var user in userList)
                            {

                                if (user.Id != discourse.Posts.First().UserId)
                                {
                                    foreach (var discourseUser in discourseUsers)
                                    {
                                        if (discourseUser.User.Id == user.Id)
                                        {
                                            user.IsUserSelected = true;
                                        }
                                    }
                                    users.Add(user);
                                }


                            }
                        }
                        else
                        {
                            users = users.Where(x => x.Id != discourse.Posts.First().User.Id).ToList();
                        }
                    }
                    else
                    {
                        users = new List<Entities.Tenants.User>();
                    }
                }
                else
                {
                    tenantUserSession = null;
                    if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                    result = TenantUserManagement.GetUsers(tenantUserSession, out users, out exception);
                    if (exception != null) { throw exception; }
                    if (users.Count > 0)
                    {
                        //var usersList = users;
                        //users.Clear();
                        users = users.Where(x => x.Id != tenantUserSession.User.Id).ToList();
                        // usersList.Where(x=> x.Id != tenantUserSession.User.Id).ToList().ForEach(x => { users.Add(x); });
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(exception);
                return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", null);
            }
            return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", users);
        }

        public ActionResult GetAllDocumentsForDiscourse()
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            List<Document> documents = null;
            try
            {
                if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }





                result = DocumentManagement.GetAllDocumentsFirstVersion(tenantUserSession, out documents, out exception);//GetAllDocuments
                if (exception != null) { throw exception; }
                if (documents.Count <= 0)
                {
                    documents = new List<Document>();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(exception);
                return PartialView("~/Views/Tenants/Documents/_TenantDocumentsSelection.cshtml", null);
            }
            return PartialView("~/Views/Tenants/Documents/_TenantDocumentsSelection.cshtml", documents);
        }


        public ActionResult GetDiscourseUsers(long discourseId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            List<DiscourseUser> Users = null;
            this.ViewBag.IsUserChatCreator = false;
            try
            {
                if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                result = DiscourseManagement.GetDiscourseUsersByDiscourseId(tenantUserSession, discourseId, out Users, out exception);
                if (Users != null)
                {
                    if (Users.First().Discourse.Posts.First().UserId == tenantUserSession.User.Id)
                    {
                        this.ViewBag.IsUserChatCreator = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Users = new List<DiscourseUser>();
            }
            return PartialView("~/Views/Tenants/Discourses/_DiscourseChatParticipents.cshtml", Users);
        }

        public ActionResult GetAllTemplatesForDiscourse(TemplateType templateType)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            List<Template> templates = null;
            try
            {
                if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                result = ElementManagement.GetAllTemplatesFirstVersion(tenantUserSession, out templates, out exception);//GetAllFinalizedTemplates
                if (exception != null) { throw exception; }
                if (templates.Count <= 0)
                {
                    templates = new List<Template>();
                }
                if (templateType == TemplateType.Template)
                {
                    templates = templates.Where(x => x.TemplateType == TemplateType.Template).ToList();
                }
                else if (templateType == TemplateType.Form)
                {
                    templates = templates.Where(x => x.TemplateType == TemplateType.Form).ToList();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(exception);
                return PartialView("~/Views/Tenants/Templates/_TenantTemplatesSelection.cshtml", null);
            }
            return PartialView("~/Views/Tenants/Templates/_TenantTemplatesSelection.cshtml", templates);
        }
        public ActionResult GetAllAttachemnts(long discourseId)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            List<DiscoursePostVersionAttachment> attachments = new List<DiscoursePostVersionAttachment>();
            try
            {
                if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                DiscourseManagement.GetDiscourseMaxPostVersionAttachmentByDiscourseId(tenantUserSession, discourseId, out attachments, out exception);
                if (exception != null) { throw (exception); }
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Views/Tenants/Discourses/_DiscourseAttachmentsGrid.cshtml", attachments);
        }
        private List<User> DiscourseUpdateInternalUsers(long discourseId, List<long> selectedUsers)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            List<User> users = new List<User>();
            if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
            if (selectedUsers == null) { selectedUsers = new List<long>(); }
            if (selectedUsers.Count <= 0) { throw (new Exception("No Users Selected")); }
            if (discourseId > 0)
            {

                foreach (var selectedUserId in selectedUsers)
                {
                    User user = null;
                    bool dbresult = TenantUserManagement.GetUserById(tenantUserSession, selectedUserId, out user, out exception);
                    if (exception != null) { throw exception; }
                    users.Add(user);
                }
                if (users.Count > 0)
                {
                    DiscourseManagement.AddRemoveDiscourseUsers(tenantUserSession, discourseId, users, out exception);
                    if (exception != null) { throw exception; }
                    Discourse discourse = null;
                    DiscourseManagement.GetDiscourseById(tenantUserSession, discourseId, out discourse, out exception);
                    foreach (var attachment in discourse.PostVersionAttachments)
                    {
                        if (attachment.AttachmentType == DiscussionPostAttachmentType.Document)
                        {
                            try
                            {
                                DocumentManagement.AddDocumentUser(tenantUserSession, (attachment.DocumentId ?? 0), discourse.Users.Select(x => x.User).ToList());
                            }
                            catch (Exception ex)
                            {
                                throw (new Exception(ExceptionUtilities.ExceptionToString(ex)));
                            }
                        }
                    }
                }
                else
                {
                    throw (new Exception("No Users Found"));
                }
            }
            else
            {
                throw (new Exception("Unable to find the following discussion"));
            }
            return users;
        }
        public JsonResult AddRemoveUser(long discourseId, List<long> selectedUsers)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            List<User> users = new List<User>();

            try
            {
                users = DiscourseUpdateInternalUsers(discourseId, selectedUsers);
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
        public JsonResult RemoveAllUsers(long discourseId)
        {
            TenantUserSession tenantUserSession = null;
            bool result = false;
            Exception exception = null;
            try
            {
                if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                if (discourseId > 0)
                {
                    result = DiscourseManagement.RemoveAllDiscourseUsers(tenantUserSession, discourseId, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    throw (new Exception("Unable to find the following chat"));
                }

            }
            catch (Exception ex)
            {
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }
            return Json(result);
        }


        public JsonResult ResendInvite(long discourseId, string email)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            var user = new User();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                if ((discourseId > 0))
                {
                    Discourse discourse = null;
                    DiscourseManagement.GetDiscourseById(tenantUserSession, discourseId, out discourse, out exception);
                    if (exception != null) { throw exception; }
                    if (discourse == null) { throw (new Exception("Unable to find the following chat")); }
                    if (discourse.Users == null) { throw (new Exception("You not have permisssion to perform the following action")); }
                    if (discourse.Posts == null) { throw (new Exception("Unable to find the following chat")); }
                    if (discourse.Posts.First().UserId != tenantUserSession.User.Id) { throw (new Exception("You not have permisssion to perform the following action")); }
                    TenantUserManagement.GetUserByEmail(tenantUserSession, email, out user, out exception);
                    if (user == null) { throw (new Exception("Unable to find the following user")); }
                    if (user.Id <= 0) { throw (new Exception("Unable to find the following user")); }
                    if (user.InviteGuid == null) { throw (new Exception("User already accepted the invitation")); }
                    if (string.IsNullOrEmpty(user.InviteUrl)) { throw (new Exception("User was not invited")); }
                    if (!(discourse.Users.Any(x => x.UserId == user.Id))) { throw (new Exception("You not have permisssion to perform the following action")); }
                    var AppConfig = System.Configuration.ConfigurationManager.AppSettings;
                    var password = string.Empty;
                    Guid guid = new Guid();
                    TenantUserManagement.UpdateUserRandomPasswordAndInviteGuid(tenantUserSession, user, out password, out guid, out exception);
                    user.InviteGuid = guid;
                    //inviteUrl = AppConfig["WebUrl"].ToString() + "TenantSignIn/AcceptInvite?key=" + user.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain + "&userName=" + user.UserName + "&password=" + password;
                   // string recieverName = user.UserName;
                    string senderName = tenantUserSession.User.NameFull;
                    string url = "http://" +(new Uri(tenantUserSession.Tenant.UrlApi).Host) + "/TenantSignIn/AcceptInvite?key=" + user.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;// + "&userName=" + userObj.UserName + "&password=" + password;//["WebUrl"].ToString() + "TenantSignIn/AcceptInvite?key=" + userObj.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;
                    string emailBody = EmailTemplateConstants.InviteExternalUserByEmail.Replace("[{RecieverEmail}]", user.Email).Replace("[{SenderUsername}]", tenantUserSession.User.NameFull).Replace("[{SenderCompanyName}]", tenantUserSession.Tenant.CompanyName).Replace("[{EnitityType}]", EntityType.Chat.ToString()).Replace("[{sharedEntityName}]", discourse.Topic).Replace("[{LinkForInvite}]", url).Replace("[{RecieverEmail}]", user.Email).Replace("[{RecieverPassword}]", password);

                    // $"<strong>Hi {recieverName},</strong> <br>You have been invited by {senderName} for a chat. Please click <a href='{ url }'><strong>here</strong></a> or copy-paste the link in your browser and login with the following credentials:<br>" + url + "<br>UserName:" + userName + "<br>Password:" + password;
                    string subject = "Invitation for Chat";
                    EmailUtilities emailUtil = new EmailUtilities(user.Email.ToString(), AppConfig["EmailFrom"].ToString(), AppConfig["EmailPassword"].ToString(), emailBody, senderName, subject, int.Parse(AppConfig["EmailPort"]), AppConfig["EmailHost"], out exception, Boolean.Parse(AppConfig["IsBodyHtml"]), Boolean.Parse(AppConfig["IsBodyHtml"]));
                    if (exception != null) { throw exception; }
                    result = emailUtil.SendEmail(out exception);
                }
            }
            catch (Exception ex)
            {
                return Json("Exception: " + ExceptionUtilities.ExceptionToString(ex));
            }
            return Json(result);
        }


        private bool DiscourseInviteExternalUser(long discourseId, string email)
        {

            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            var user = new User();
            var userObj = new User();
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
            if ((discourseId > 0))
            {
                Discourse discourse = null;
                DiscourseManagement.GetDiscourseById(tenantUserSession, discourseId, out discourse, out exception);

                if (discourse == null) { throw (new Exception("Unable to find the following form")); }
                if (discourse.Posts.First().UserId != tenantUserSession.User.Id) { throw (new Exception("You do not have permission to invite users")); }
                if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrEmpty(email))
                {
                    result = TenantUserManagement.GetUserByEmail(tenantUserSession, email, out user, out exception);
                    if (exception != null) { throw exception; }
                    if (user.Id > 0)
                    {

                        //DiscourseManagement.AddUserWithoutRemovingDiscourseUsers(tenantUserSession, discourseId, user, out exception);
                        throw (new Exception("User already exists."));
                        // if (exception != null) { throw exception; }
                    }
                    else
                    {
                        //var userName = email;
                        var password = "";
                        result = TenantUserManagement.InviteUser(tenantUserSession, email, Url.Action("Index", "TenantDiscourse", new { id = discourseId }), discourseId, "Discourse", out password, out userObj, out exception);
                        if (exception != null) { throw exception; }

                        if (userObj.Id > 0)
                        {

                            foreach (var attachment in discourse.PostVersionAttachments)
                            {
                                if (attachment.AttachmentType == DiscussionPostAttachmentType.Document)
                                {
                                    try
                                    {
                                        DocumentManagement.AddDocumentUser(tenantUserSession, (attachment.DocumentId ?? 0), discourse.Users.Select(x => x.User).ToList());
                                    }
                                    catch (Exception ex)
                                    {
                                        throw (new Exception(ExceptionUtilities.ExceptionToString(ex)));
                                    }
                                }
                            }

                            var AppConfig = System.Configuration.ConfigurationManager.AppSettings;
                          //  string recieverName = userObj.UserName;
                            string senderName = tenantUserSession.User.NameFull;
                            string url = "http://" + (new Uri(tenantUserSession.Tenant.UrlApi).Host) + "/TenantSignIn/AcceptInvite?key=" + userObj.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;// + "&userName=" + userObj.UserName + "&password=" + password;//["WebUrl"].ToString() + "TenantSignIn/AcceptInvite?key=" + userObj.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;
                            string emailBody = EmailTemplateConstants.InviteExternalUserByEmail.Replace("[{RecieverEmail}]", userObj.Email).Replace("[{SenderUsername}]", tenantUserSession.User.NameFull).Replace("[{SenderCompanyName}]", tenantUserSession.Tenant.CompanyName).Replace("[{EnitityType}]", EntityType.Chat.ToString()).Replace("[{sharedEntityName}]", discourse.Topic).Replace("[{LinkForInvite}]", url).Replace("[{RecieverEmail}]", userObj.Email).Replace("[{RecieverPassword}]", password);
                            // $"<strong>Hi {recieverName},</strong> <br>You have been invited by {senderName} for a chat. Please click <a href='{ url }'><strong>here</strong></a> or copy-paste the link in your browser and login with the following credentials:<br>" + url + "<br>UserName:" + userName + "<br>Password:" + password;
                            string subject = "Invitation for Chat";
                            EmailUtilities emailUtil = new EmailUtilities(userObj.Email.ToString(), AppConfig["EmailFrom"].ToString(), AppConfig["EmailPassword"].ToString(), emailBody, senderName, subject, int.Parse(AppConfig["EmailPort"]), AppConfig["EmailHost"], out exception, Boolean.Parse(AppConfig["IsBodyHtml"]), Boolean.Parse(AppConfig["IsBodyHtml"]));
                            if (exception != null) { throw exception; }
                            result = emailUtil.SendEmail(out exception);
                        }
                        else { result = false; }
                    }

                }
                else
                { throw (new Exception("UserName and Email is Required")); }
            }
            else
            { throw (new Exception("Unable to find the following group chat")); }
            return result;
        }
        public JsonResult InviteUser(long discourseId, string email)
        {
            bool result = false;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
                result = DiscourseInviteExternalUser(discourseId, email);
            }
            catch (Exception ex)
            {
                result = false;
                return Json(ex.Message);
            }
            return Json(result);
        }

        public JsonResult CreatePost(string title, string comment, List<long> documentIds, List<long> templateIds, List<string> external, List<string> live, List<long> userIds = null, List<string> emailList = null)
        {
            this.ViewBag.CurrentUser = 0;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Discourse discourse = null;
            var filePath = string.Empty;
            var fileEntries = new List<FileUploadStatus>();
            List<string> externalFile = new List<string>();
            bool result = false;
            var uploadedExternalFiles = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(title?.Trim())){ throw (new Exception("Title is required")); }
                if (documentIds == null) { documentIds = new List<long>(); }
                if (templateIds == null) { templateIds = new List<long>(); }
                if (external == null) { external = new List<string>(); }
                if (live == null) { live = new List<string>(); }
                if (userIds == null) { userIds = new List<long>(); }
                if (emailList == null) { emailList = new List<string>(); }

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                #region Read External Documents from local folder
                var folderPath = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/DiscourceExternalDocuments/" + "" + tenantUserSession.User.Id + "");
                DirectoryHelper.GetExternalFiles(folderPath, out uploadedExternalFiles, out exception);
                if (uploadedExternalFiles.Count < external.Count) { throw (new Exception($"Unable to find the following attachments {string.Join(", ", external)}. Please remove and reattach the following files.")); }
                foreach (var uploadedExternalFile in uploadedExternalFiles)
                {
                    foreach (var selectedExternalFile in external)
                    {
                        if (Path.GetFileName(uploadedExternalFile).Contains(selectedExternalFile))
                        {
                            //var folderId = selectedExternalFile.Split(new string[1] { "._." }, StringSplitOptions.None)[1];
                            externalFile.Add(uploadedExternalFile);// + "._." + folderId
                        }
                    }
                }
                #endregion

                result = DiscourseManagement.AddPost(tenantUserSession, title, comment, documentIds, templateIds, externalFile, live, out discourse, out fileEntries, out exception);
                if (exception != null) { throw exception; }
                if (discourse != null)
                {
                    if (discourse.Id <= 0) { throw (new Exception("Unable to complete the Following Operation")); }

                    if (userIds.Count > 0)
                    {
                        var users = DiscourseUpdateInternalUsers(discourse.Id, userIds);
                    }
                    else
                    {
                        result = DiscourseManagement.RemoveAllDiscourseUsers(tenantUserSession, discourse.Id, out exception);
                        if (exception != null) { throw exception; }
                    }
                    if (emailList.Count > 0)
                    {
                        foreach (var email in emailList)
                        {
                            result = DiscourseInviteExternalUser(discourse.Id, email);
                        }
                    }
                    DiscourseManagement.GetDiscourseById(tenantUserSession, discourse.Id, out discourse, out exception);
                    foreach (var attachment in discourse.PostVersionAttachments)
                    {
                        if (attachment.AttachmentType == DiscussionPostAttachmentType.Document)
                        {
                            try
                            {
                                DocumentManagement.AddDocumentUser(tenantUserSession, (attachment.DocumentId ?? 0), discourse.Users.Select(x => x.User).ToList());
                            }
                            catch (Exception ex)
                            {
                                throw (new Exception(ExceptionUtilities.ExceptionToString(ex)));
                            }
                        }
                    }
                    if (exception != null) { throw exception; }
                }
                else { throw (new Exception("Unable to complete the Following Operation")); }
            }
            catch (Exception ex)
            {
                return Json($"Exception:: {ExceptionUtilities.ExceptionToString(ex)}");
            }
            finally
            {
                DirectoryHelper.DeleteFile(uploadedExternalFiles, out exception);
            }
            return Json(discourse.Id.ToString());
        }
        public JsonResult PostComment(long discourseId, string comment, List<long> documentIds, List<long> templateIds, List<string> external, List<string> live)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            DiscoursePost discoursePost = null;
            bool result = false;
            var filePath = string.Empty;
            var fileEntries = new List<FileUploadStatus>();
            List<string> externalFile = new List<string>();
            this.ViewBag.CurrentUser = 0;
            var uploadedExternalFiles = new List<string>();
            try
            {
                if (discourseId <= 0) { throw (new Exception("Unable to find the following chat")); }
                if (documentIds == null) { documentIds = new List<long>(); }
                if (templateIds == null) { templateIds = new List<long>(); }
                if (external == null) { external = new List<string>(); }
                if (live == null) { live = new List<string>(); }
                if (discourseId > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                    this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                    #region Read External Documents from local folder
                    var folderPath = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/DiscourceExternalDocuments/" + "" + tenantUserSession.User.Id + "");
                    DirectoryHelper.GetExternalFiles(folderPath, out uploadedExternalFiles, out exception);
                    if (uploadedExternalFiles.Count < external.Count) { throw (new Exception($"Unable to find the following attachments {string.Join(", ", external)}. Please remove and reattach the following files.")); }
                    foreach (var uploadedExternalFile in uploadedExternalFiles)
                    {
                        foreach (var selectedExternalFile in external)
                        {
                            if (Path.GetFileName(uploadedExternalFile).Contains(selectedExternalFile))
                            {
                                //var folderId = selectedExternalFile.Split(new string[1] { "._." }, StringSplitOptions.None)[1];
                                externalFile.Add(uploadedExternalFile);// + "._." + folderId
                            }
                        }
                    }
                    #endregion
                    //foreach (var externalFileObj in externalFile)
                    //{

                    //}
                    result = DiscourseManagement.AddPostComment(tenantUserSession, discourseId, comment, documentIds, templateIds, externalFile, live, out discoursePost, out fileEntries, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    Discourse discourse = null;
                    DiscourseManagement.GetDiscourseById(tenantUserSession, discourseId, out discourse, out exception);
                    foreach (var attachment in discourse.PostVersionAttachments)
                    {
                        if (attachment.AttachmentType == DiscussionPostAttachmentType.Document)
                        {
                            try
                            {
                                DocumentManagement.AddDocumentUser(tenantUserSession, (attachment.DocumentId ?? 0), discourse.Users.Select(x => x.User).ToList());
                            }
                            catch (Exception ex)
                            {
                                throw (new Exception(ExceptionUtilities.ExceptionToString(ex)));
                            }
                        }
                    }
                    if (exception != null) { throw exception; }
                }
                else
                { throw (new Exception("Unable to find the following groupd chat")); }
            }
            catch (Exception ex)
            {
                var exStr = (ex.InnerException != null) ? (ex.InnerException.Message) : (ex.Message);
                return Json(exStr, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                DirectoryHelper.DeleteFile(uploadedExternalFiles, out exception);
            }
            long maxPostVersionId = 0;
            if (discoursePost != null)
            {
                maxPostVersionId = discoursePost.Id;
                //if (discoursePost.Versions != null)
                //{
                //    maxPostVersionId = discoursePost.Versions.Select(x => x.Id).Max();
                //}
            }
            return Json(maxPostVersionId, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult EditPostComments(long postId, string comment, List<long> documentIds, List<long> templateIds, List<string> external, List<long> existingExternal, List<string> live)
        //{
        //    Exception exception = null;
        //    TenantUserSession tenantUserSession = null;
        //    DiscoursePost discoursePost = null;
        //    bool result = false;
        //    var filePath = string.Empty;
        //    var fileEntries = new List<FileUploadStatus>();
        //    List<string> externalFile = new List<string>();
        //    try
        //    {
        //        if (postId <= 0) { throw (new Exception("Unable to find the following post")); }
        //        if (documentIds == null) { documentIds = new List<long>(); }
        //        if (templateIds == null) { templateIds = new List<long>(); }
        //        if (external == null) { external = new List<string>(); }
        //        if (existingExternal == null) { existingExternal = new List<long>(); }
        //        if (live == null) { live = new List<string>(); }
        //        if (postId > 0)
        //        {
        //            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
        //            #region Read External Documents from local folder
        //            var uploadedExternalFiles = GetExternalFiles(tenantUserSession);

        //            foreach (var uploadedExternalFile in uploadedExternalFiles)
        //            {
        //                foreach (var selectedExternalFile in external.ToList())
        //                {
        //                    if (Path.GetFileName(uploadedExternalFile) == selectedExternalFile)
        //                    {
        //                        external.Remove(selectedExternalFile);
        //                        externalFile.Add(uploadedExternalFile);
        //                    }
        //                }
        //            }


        //            #endregion
        //            result = DiscourseManagement.AddPostVersion(tenantUserSession, postId, comment, documentIds, templateIds, externalFile, existingExternal, live, out discoursePost, out fileEntries, out exception);
        //            if (exception != null) { throw exception; }
        //            else { RemoveLocalFiles(uploadedExternalFiles); }
        //        }
        //        else
        //        { throw (new Exception("Unable to Find the Following Discourse")); }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json($"Exception:: {ex.ToString()}");
        //    }
        //    if (discoursePost != null)
        //    {
        //        if (discoursePost.Versions != null)
        //        {
        //            if (discoursePost.Versions.Last().Attachments != null)
        //            {
        //                foreach (var attachment in discoursePost.Versions.Last().Attachments)
        //                {
        //                    if (attachment.AttachmentType == DiscussionPostAttachmentType.Template)
        //                    {
        //                        attachment.Template.TemplateImage = null;
        //                    }
        //                }
        //            }

        //        }

        //    }
        //    var serialized = JsonConvert.SerializeObject(discoursePost, Formatting.Indented,
        //                                                new JsonSerializerSettings
        //                                                {
        //                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //                                                });
        //    return Json(serialized);
        //}
        public ActionResult EditPostComment(long postId, string comment, List<long> documentIds, List<long> templateIds, List<string> external, List<long> existingExternal, List<string> live)
        {
            Exception exception = null;
            this.ViewBag.CurrentUser = 0;
            TenantUserSession tenantUserSession = null;
            DiscoursePost discoursePost = null;
            bool result = false;
            var filePath = string.Empty;
            var fileEntries = new List<FileUploadStatus>();
            List<string> externalFile = new List<string>();
            var maxPostVersion = new DiscoursePostVersion();
            var uploadedExternalFiles = new List<string>();
            try
            {
                if (postId <= 0) { throw (new Exception("Unable to find the following post")); }
                if (documentIds == null) { documentIds = new List<long>(); }
                if (templateIds == null) { templateIds = new List<long>(); }
                if (external == null) { external = new List<string>(); }
                if (existingExternal == null) { existingExternal = new List<long>(); }
                if (live == null) { live = new List<string>(); }
                if (postId > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                    this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                    #region Read External Documents from local folder
                    var folderPath = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/DiscourceExternalDocuments/" + "" + tenantUserSession.User.Id + "");
                    DirectoryHelper.GetExternalFiles(folderPath, out uploadedExternalFiles, out exception);
                    if (uploadedExternalFiles.Count < external.Count) { throw (new Exception($"Unable to find the following attachments {string.Join(", ", external)}. Please remove and reattach the following files.")); }
                    foreach (var uploadedExternalFile in uploadedExternalFiles)
                    {
                        foreach (var selectedExternalFile in external)
                        {
                            if (Path.GetFileName(uploadedExternalFile).Contains(selectedExternalFile))
                            {
                                //var folderId = selectedExternalFile.Split(new string[1] { "._." }, StringSplitOptions.None)[1];
                                externalFile.Add(uploadedExternalFile);// + "._." + folderId
                            }
                        }
                    }
                    #endregion
                    result = DiscourseManagement.AddPostVersion(tenantUserSession, postId, comment, documentIds, templateIds, externalFile, existingExternal, live, out discoursePost, out fileEntries, out exception);
                    if (exception != null) { throw exception; }
                    Discourse discourse = null;
                    DiscourseManagement.GetDiscourseById(tenantUserSession, discoursePost.DiscourseId, out discourse, out exception);
                    foreach (var attachment in discourse.PostVersionAttachments)
                    {
                        if (attachment.AttachmentType == DiscussionPostAttachmentType.Document)
                        {
                            try
                            {
                                DocumentManagement.AddDocumentUser(tenantUserSession, (attachment.DocumentId ?? 0), discourse.Users.Select(x => x.User).ToList());
                            }
                            catch (Exception ex)
                            {
                                throw (new Exception(ExceptionUtilities.ExceptionToString(ex)));
                            }
                        }
                    }
                }
                else
                { throw (new Exception("Unable to find the following chat")); }
            }
            catch (Exception ex)
            {
                var exStr = (ex.InnerException != null) ? (ex.InnerException.Message) : (ex.Message);
                return Json(exStr, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                DirectoryHelper.DeleteFile(uploadedExternalFiles, out exception);
            }
            long maxPostVersionId = 0;
            if (discoursePost != null)
            {
                if (discoursePost.Versions != null)
                {
                    maxPostVersionId = discoursePost.Versions.Select(x => x.Id).Max();
                }
            }
            return Json(maxPostVersionId, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPost(long id)
        {
            this.ViewBag.CurrentUser = 0;
            DiscoursePostVersion discoursePostVersion = null;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            this.ViewBag.EnableHistory = false;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                if (id > 0)
                {
                    DiscourseManagement.GetDiscourseMaxPostVersionByPostId(tenantUserSession, id, out discoursePostVersion, out exception);
                    if (exception != null) { throw exception; }
                    if (discoursePostVersion == null) { discoursePostVersion = new DiscoursePostVersion(); }
                    if (discoursePostVersion.Post.Versions.Count > 1)
                    {
                        this.ViewBag.EnableHistory = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return PartialView("~/Views/Tenants/Discourses/_DiscoursePostComment.cshtml", discoursePostVersion);
        }
        public ActionResult GetMaxPostVersionByDocumentId(long id)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Document document = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                if (id <= 0) { throw (new Exception("Unable to find the following document")); }
                DocumentManagement.GetDocumentDiscourseById(tenantUserSession, id, out document, out exception);
                if (exception != null) { throw exception; }
                if (document == null) { throw (new Exception("Unable to find the following document.")); }
            }
            catch (Exception ex)
            {
                var exStr = (ex.InnerException != null) ? (ex.InnerException.Message) : (ex.Message);
                this.ViewBag.ErrorMessage = exStr;
            }
            return PartialView("~/Views/Tenants/Documents/_DocumentDiscourseMaxPostMaxVersion.cshtml", document);
        }

        public ActionResult GetLatestPostVersionsByLastRecieved(long id)
        {
            this.ViewBag.CurrentUser = 0;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            this.ViewBag.EnableHistory = false;
            List<DiscoursePost> discoursePosts = new List<DiscoursePost>();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                if (id > 0)
                {
                    DiscourseManagement.GetDiscoursePostsByLastPostId(tenantUserSession, id, out discoursePosts, out exception);
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Views/Tenants/Discourses/_DiscoursePostsLive.cshtml", discoursePosts);
        }

        public ActionResult GetPostComment(long id)
        {
            this.ViewBag.CurrentUser = 0;
            DiscoursePostVersion postVersion = null;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            this.ViewBag.EnableHistory = false;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                this.ViewBag.CurrentUser = tenantUserSession.User.Id;
                if (id > 0)
                {
                    DiscourseManagement.GetDiscoursePostVersionById(tenantUserSession, id, out postVersion, out exception);
                    if (postVersion.Post.Versions.Count > 1)
                    {
                        this.ViewBag.EnableHistory = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Views/Tenants/Discourses/_DiscoursePostComment.cshtml", postVersion);
        }
        public ActionResult UploadExternalDocumentsToFolder(IEnumerable<HttpPostedFileBase> files,long folderId)
        {
            if (files != null)
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;
                try
                {
                    if (folderId <= 1) { throw new Exception("File can not be uploaded to the following folder."); }
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    if (exception != null)
                        throw exception;

                    foreach (var file in files)
                    {
                        var containsDuplicateFiles = files.Where(x => ((Path.GetFileName(x.FileName) == Path.GetFileName(file.FileName)) && (x.ContentLength == file.ContentLength))).ToList();
                        if (containsDuplicateFiles.Count <= 1)
                        {
                            Document document = null;
                            var fileName = Path.GetFileName(file.FileName);
                            if (DocumentManagement.CreateDocumentEntry(tenantUserSession, fileName, file.ContentLength, folderId, tenantUserSession.User, out document, out exception))
                            {

                                var pathTenantDoc = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/DiscourceExternalDocuments/" + "" + tenantUserSession.User.Id + "");
                                if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }
                                var physicalPath = Path.Combine(pathTenantDoc, fileName + "._." + document.Id.ToString());
                                file.SaveAs(physicalPath);
                            }
                            else
                            {
                                throw (exception);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult RemoveExternalDocumentsToFolder(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;

                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                    if (exception != null)
                        throw exception;

                    foreach (var fullName in fileNames)
                    {
                        var fileName = Path.GetFileName(fullName);

                        var pathTenantDoc = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/DiscourceExternalDocuments/" + "" + tenantUserSession.User.Id + "");
                        if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }

                        var physicalPath = Path.Combine(pathTenantDoc, fileName);

                        // TODO: Verify user permissions

                        if (System.IO.File.Exists(physicalPath))
                        {
                            // The files are not actually removed in this demo
                            System.IO.File.Delete(physicalPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            // Return an empty string to signify success
            return Content("");
        }

        private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }

        ////////List<string> GetExternalFiles(TenantUserSession tenantUserSession)
        ////////{
        ////////    var filePath = string.Empty;
        ////////    var fileEntries = new List<FileUploadStatus>();

        ////////    //FileInfo fileInfo = null;

        ////////    List<string> files = new List<string>();
        ////////    filePath = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/DiscourceExternalDocuments/" + "" + tenantUserSession.User.Id + "");
        ////////    if (Directory.Exists(filePath))
        ////////    {
        ////////        string[] filesArray = Directory.GetFiles(filePath);

        ////////        fileEntries = filesArray.ToList().ConvertAll<FileUploadStatus>(f => new FileUploadStatus() { File = f, FileName = null, StatusMessage = null, Finalized = false, });

        ////////        foreach (var fileEntry in fileEntries)
        ////////        {
        ////////            files.Add(fileEntry.File.ToString());
        ////////        }
        ////////    }
        ////////    return files;
        ////////}

        ////////void RemoveLocalFiles(List<FileUploadStatus> fileUploadStatus)
        ////////{
        ////////    FileInfo fileInfo = null;

        ////////    foreach (var fileEntry in fileUploadStatus)
        ////////    {
        ////////        if (!string.IsNullOrEmpty(fileEntry.File.ToString()))
        ////////        {
        ////////            fileInfo = new FileInfo(fileEntry.File.ToString());
        ////////            fileInfo.Delete();
        ////////        }
        ////////    }
        ////////}

        ////////void RemoveLocalFiles(List<String> fileUploadStatus)
        ////////{
        ////////    FileInfo fileInfo = null;

        ////////    foreach (var fileEntry in fileUploadStatus)
        ////////    {
        ////////        if (!string.IsNullOrEmpty(fileEntry))
        ////////        {
        ////////            fileInfo = new FileInfo(fileEntry);
        ////////            fileInfo.Delete();
        ////////        }
        ////////    }
        ////////}

        public FileStreamResult DownloadExternalFile(long id)
        {
            FileStreamResult fileStreamResult = null;
            try
            {
                if (id >= 0)
                {
                    TenantUserSession tenantUserSession = null;
                    Exception exception = null;
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                    AzureCloudStorageAccountHelper azureHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                    var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                    bool dbresult = DiscourseManagement.GetDiscoursePostVersionAttachmentById(tenantUserSession, id, out discoursePostVersionAttachment, out exception);
                    if (exception != null) { throw exception; }
                    Stream stream = null;
                    azureHelper.GetDiscourseDocumentFileStream(tenantUserSession.Tenant, discoursePostVersionAttachment, out stream, out exception);
                    if (exception != null) { throw exception; }
                    if (stream != null)
                    {
                        if (stream.Length > 0)
                        {
                            stream.Seek(0, SeekOrigin.Begin);
                            fileStreamResult = new FileStreamResult(stream, "application/octet-stream");
                            fileStreamResult.FileDownloadName = discoursePostVersionAttachment.FileNameClient;
                        }
                    }
                }
            }
            catch (Exception) { }

            return fileStreamResult;
        }

        public ActionResult GetPostVersionHistory(long id)
        {
            Exception exception = null;
            List<DiscoursePostVersion> discoursePostVersions = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, Entities.Lookup.SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                if (id > 0)
                {
                    bool result = DiscourseManagement.GetDiscoursePostVersionByPostId(tenantUserSession, (long)id, out discoursePostVersions, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    discoursePostVersions = new List<DiscoursePostVersion>();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.Exception = ex.Message.ToString();
            }
            return PartialView("~/Views/Tenants/Discourses/_DiscoursePostVersionHistory.cshtml", discoursePostVersions.OrderByDescending(x => x.DateTime).ToList());
        }

    }
}