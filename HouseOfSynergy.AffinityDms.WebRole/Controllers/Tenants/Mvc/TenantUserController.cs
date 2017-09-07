using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantUserController :
        Controller
    {
        public ActionResult Index(long? id)
        {
            Exception exception = null;
            var userEntity = new User();
            TenantUserSession tenantUserSession = null;
            var userProfileViewModel = new UserProfileViewModel();
            try
            {
                //IEnumerable<SelectListItem> authtype = EnumUtilities.GetSelectItemListFromEnum<AuthenticationType>(out exception);
                //if (exception != null) { throw exception; }
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                List<Department> departmentlist = new List<Department>();
                bool dbresult = TenantUserManagement.GetDepartments(tenantUserSession, out departmentlist, out exception);
                if (exception != null) { throw (exception); }
                this.ViewBag.Departments = departmentlist;
                if (id > 0)
                {
                   

                    // TODO: Decide how self profile editing will work. What about inactive accounts, etc.?

                    bool dbreesult = TenantUserManagement.GetUserById(tenantUserSession, (long)id, out userEntity, out exception);
                    if (exception != null) { throw (exception); }
                    userProfileViewModel.Id = userEntity.Id;
                    userProfileViewModel.NameGiven = userEntity.NameGiven;
                    userProfileViewModel.NameFamily = userEntity.NameFamily;
                    userProfileViewModel.Email = userEntity.Email;
                    userProfileViewModel.UserName = userEntity.UserName;
                    userProfileViewModel.Password = "";
                    userProfileViewModel.PasswordConfirmation = "";
                    userProfileViewModel.Address1 = userEntity.Address1;
                    userProfileViewModel.Address2 = userEntity.Address2;
                    userProfileViewModel.City = userEntity.City;
                    userProfileViewModel.ZipOrPostCode = userEntity.ZipOrPostCode;
                    userProfileViewModel.Country = userEntity.Country;
                    userProfileViewModel.PhoneWork = userEntity.PhoneWork;
                    userProfileViewModel.PhoneMobile = userEntity.PhoneMobile;
                    userProfileViewModel.DepartmentId = userEntity.DepartmentId;
                    userProfileViewModel.ActiveDirectory_ManagerId = userEntity.ActiveDirectory_ManagerId;
                    userProfileViewModel.ActiveDirectory_ObjectId = userEntity.ActiveDirectory_ObjectId;
                   
                    return (this.View("~/Views/tenants/users/User.cshtml", userProfileViewModel));
                }
                else
                {
                    userProfileViewModel = new UserProfileViewModel();
                    //this.ViewBag.AuthenticationType = authtype;
                    return (this.View("~/Views/tenants/users/User.cshtml", userProfileViewModel));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (this.View("~/Views/Tenants/Users/Users.cshtml", new { ErrorMessage = exception.Message, SuccessMessage = string.Empty }));
        }
        [HttpPost]
        public ActionResult Index(UserProfileViewModel userProfileViewModel)
        {
            TenantUserSession tenantUserSession = null;
            bool dbresult = false;
            Exception exception = null;
            bool result = false;
            try
            {
                //Check if user is invited and loging in for the first time

                if (userProfileViewModel.ActiveDirectory_ManagerId == null) { userProfileViewModel.ActiveDirectory_ManagerId = Guid.Empty; }
                if (userProfileViewModel.ActiveDirectory_ObjectId == null) { userProfileViewModel.ActiveDirectory_ObjectId = Guid.Empty; }

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                List<Department> departmentlist = new List<Department>();
                dbresult = TenantUserManagement.GetDepartments(tenantUserSession, out departmentlist, out exception);
                if (exception != null) { throw (exception); }
                this.ViewBag.Departments = departmentlist;
                ModelState.Remove("ActiveDirectory_ManagerId");
                if (!ModelState.IsValid) { throw (new Exception("There Were Some Invalid Values")); }
                if (userProfileViewModel.Id > 0)
                {
                    User user = new User();
                    User userEntity = new User();
                    user.Address1 = userProfileViewModel.Address1;
                    user.Address2 = userProfileViewModel.Address2;
                    user.City = userProfileViewModel.City;
                    user.Country = userProfileViewModel.Country;
                    user.Email = userProfileViewModel.Email;
                    user.Id =(long) userProfileViewModel.Id;
                    user.NameFamily = userProfileViewModel.NameFamily;
                    user.NameGiven = userProfileViewModel.NameGiven;
                    user.PasswordHash = userProfileViewModel.Password;
                    user.PasswordSalt = userProfileViewModel.Password;
                    user.PhoneMobile = userProfileViewModel.PhoneMobile;
                    user.PhoneWork = userProfileViewModel.PhoneWork;
                    user.AuthenticationType = AuthenticationType.Both;
                    user.DepartmentId = userProfileViewModel.DepartmentId;
                    user.ZipOrPostCode = userProfileViewModel.ZipOrPostCode;
                    user.ActiveDirectory_ManagerId = userProfileViewModel.ActiveDirectory_ManagerId;
                    user.ActiveDirectory_ObjectId = userProfileViewModel.ActiveDirectory_ObjectId;
                    result = TenantUserManagement.UpdateUser(tenantUserSession, user, out userEntity, out exception);
                    if (exception != null) { throw (exception); }
                    userProfileViewModel.Password = string.Empty;
                    userProfileViewModel.PasswordConfirmation = string.Empty;
                    this.ViewBag.SuccessMessage = "User Successfully Updated.";
                    return RedirectToAction("Index", "TenantUsers", new { ErrorMessage = string.Empty, SuccessMessage = "User Successfully Updated." });
                    //return RedirectToAction("Index", "TenantUser",new { id= userProfileViewModel.Id } );
                }
                else
                {
                    User user = null;
                    dbresult = TenantUserManagement.FindUserByUsernameOrEmail(tenantUserSession, userProfileViewModel.UserName, userProfileViewModel.Email,out user, out exception);
                    if (user!=null)
                    {
                        if (user.UserName == userProfileViewModel.UserName)
                        {
                            ModelState.AddModelError("UserName", "Username Already Exist");
                        }
                        if (user.Email == userProfileViewModel.Email)
                        {
                            ModelState.AddModelError("Email", "Email Already Exist");
                        }
                        throw (new Exception("There were some invalid values"));
                    }
                    user = new User();
                    user.Address1 = userProfileViewModel.Address1;
                    user.Address2 = userProfileViewModel.Address2;
                    user.City = userProfileViewModel.City;
                    user.Country = userProfileViewModel.Country;
                    user.Email = userProfileViewModel.Email;
                    user.NameFamily = userProfileViewModel.NameFamily;
                    user.NameGiven = userProfileViewModel.NameGiven;
                    var clientPasswordHash = PowerTools.Library.Security.Cryptography.Sha.GenerateHash(userProfileViewModel.Password, System.Text.Encoding.UTF8, PowerTools.Library.Security.Cryptography.Sha.EnumShaKind.Sha512);
                    var serverPasswordHash = PowerTools.Library.Security.Cryptography.PasswordHash.CreateHash(clientPasswordHash);
                    user.PasswordHash = serverPasswordHash;
                    user.PasswordSalt = serverPasswordHash;
                    user.PhoneMobile = userProfileViewModel.PhoneMobile;
                    user.PhoneWork = userProfileViewModel.PhoneWork;
                    user.UserName = userProfileViewModel.UserName;
                    user.AuthenticationType = AuthenticationType.Both;
                    user.DepartmentId = userProfileViewModel.DepartmentId;
                    user.ZipOrPostCode = userProfileViewModel.ZipOrPostCode;
                    user.DateTimeCreated = DateTime.UtcNow;
                    Guid guid = Guid.NewGuid();
                    user.ActiveDirectory_ManagerId = guid;
                    user.ActiveDirectory_ObjectId = guid;
                    user.TenantId = tenantUserSession.User.TenantId;
                    result = TenantUserManagement.AddUser(tenantUserSession, user, out exception);
                    if (exception != null) { throw exception; }
                    //return RedirectToAction("Index", "TenantUsers", new { ErrorMessage = string.Empty, SuccessMessage = "User Successfully Added." });
                    return RedirectToAction("Index", "TenantSettings", new { ErrorMessage = string.Empty, SuccessMessage = "User Successfully Added.", settingsType = SettingsType.UserManagement });
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
        
            return (this.View("~/Views/tenants/users/User.cshtml", userProfileViewModel));
        }
        public JsonResult RemoveUser(long id)
        {
            Exception exception = null;
            var userEntity = new User();
            TenantUserSession tenantUserSession = null;
            var userProfileViewModel = new UserProfileViewModel();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                string IsUserDeletionEnabledVal = string.Empty;
                if ((IsUserDeletionEnabledVal = System.Configuration.ConfigurationManager.AppSettings["IsUserDeletionEnabled"])!=null)
                {
                    bool IsUserDeletionEnabled = false;
                    var result = bool.TryParse(IsUserDeletionEnabledVal, out IsUserDeletionEnabled);
                    if (!IsUserDeletionEnabled) { throw (new Exception("DevOnly!! The following operation is available for testing")); }
                }
                if (id <= 0) { throw (new Exception("Unable to find the following user")); }
                if (!(tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.Administrator))) { throw (new Exception("You do npt have permission to perform the following action")); }
                bool isUserRemoved = TenantUserManagement.RemoveUser(tenantUserSession, id);
                return (Json(isUserRemoved, JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                return (Json(ExceptionUtilities.ExceptionToString(ex), JsonRequestBehavior.AllowGet));
            }
        }
    }
}