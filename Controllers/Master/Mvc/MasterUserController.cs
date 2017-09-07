using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;

using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
//using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.WebRole.Models.Master;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities.Master;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
    public class MasterUserController :
        Controller
    {
        public ActionResult Index(long? id)
        {
            Exception exception = null;
            MasterUserSession masterUserSession = null;
            MasterUser userEntity = new MasterUser();
            MasterUserProfileViewModel masterUserProfileViewModel=null;
            try
            {
                List<SelectListItem> authtype = EnumUtilities.GetSelectItemListFromEnum<AuthenticationType>(out exception);
                if (exception != null) { throw exception; }
                if (id > 0)
                {
                    masterUserProfileViewModel =new MasterUserProfileViewModel();
                   
                    if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out masterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                    // TODO: Decide how self profile editing will work. What about inactive accounts, etc.?
                    //if (!MasterUserManagement.IsUserActionAllowed(masterUserSession.User, MasterActionType.UsersAdd, MasterActionType.UsersEdit)) { throw (new Exception("You do not have have access to perform these actions.")); }
                    bool result = MasterUserManagement.GetUserUserById(masterUserSession, (long)id, out userEntity, out exception);
                    if (exception != null) { throw exception; }
                    if (userEntity == null) { throw (new Exception("Unable to find User")); }
                    masterUserProfileViewModel.Id = userEntity.Id;
                    masterUserProfileViewModel.NameGiven = userEntity.NameGiven;
                    masterUserProfileViewModel.NameFamily = userEntity.NameFamily;
                    masterUserProfileViewModel.Email = userEntity.Email;
                    masterUserProfileViewModel.UserName = userEntity.UserName;
                    masterUserProfileViewModel.Password = "";
                    masterUserProfileViewModel.PasswordConfirmation = "";
                    masterUserProfileViewModel.Address1 = userEntity.Address1;
                    masterUserProfileViewModel.Address2 = userEntity.Address2;
                    masterUserProfileViewModel.City = userEntity.City;
                    masterUserProfileViewModel.ZipOrPostCode = userEntity.ZipOrPostCode;
                    masterUserProfileViewModel.Country = userEntity.Country;
                    masterUserProfileViewModel.PhoneWork = userEntity.PhoneWork;
                    masterUserProfileViewModel.PhoneMobile = userEntity.PhoneMobile;
                    masterUserProfileViewModel.AuthenticationType = userEntity.AuthenticationType;
                    masterUserProfileViewModel.ActiveDirectoryId = userEntity.ActiveDirectoryId;
                    masterUserProfileViewModel.ActiveDirectory_AuthenticationEmail = userEntity.ActiveDirectory_AuthenticationEmail;
                    masterUserProfileViewModel.ActiveDirectory_AuthenticationPhone = userEntity.ActiveDirectory_AuthenticationPhone;
                    masterUserProfileViewModel.ActiveDirectory_AuthenticationPhoneAlternate = userEntity.ActiveDirectory_AuthenticationPhoneAlternate;
                    masterUserProfileViewModel.ActiveDirectory_Department = userEntity.ActiveDirectory_Department;
                    masterUserProfileViewModel.ActiveDirectory_JobTitle = userEntity.ActiveDirectory_JobTitle;
                    masterUserProfileViewModel.ActiveDirectory_ManagerId = userEntity.ActiveDirectory_ManagerId;
                    masterUserProfileViewModel.ActiveDirectory_NameDisplay = userEntity.ActiveDirectory_NameDisplay;
                    masterUserProfileViewModel.ActiveDirectory_ObjectId = userEntity.ActiveDirectory_ObjectId;
                    masterUserProfileViewModel.ActiveDirectory_RoleDisplayName = userEntity.ActiveDirectory_RoleDisplayName;
                    masterUserProfileViewModel.ActiveDirectory_UsageLocation = userEntity.ActiveDirectory_UsageLocation;
                    foreach (var seleclist in authtype)
                    {
                        if (seleclist.Text == userEntity.AuthenticationType.ToString())
                        {
                            seleclist.Selected = true;
                        }
                    }

                    this.ViewBag.AuthenticationType = authtype;
                    return (this.View("~/Views/Master/User.cshtml", masterUserProfileViewModel));
                }
                else
                {
                    masterUserProfileViewModel = new MasterUserProfileViewModel();
                    masterUserProfileViewModel.ActiveDirectory_ManagerId = Guid.Empty;
                    masterUserProfileViewModel.ActiveDirectory_ObjectId = Guid.Empty;
                    this.ViewBag.AuthenticationType = authtype;
                    return (this.View("~/Views/Master/User.cshtml", masterUserProfileViewModel));
                }
                //else { throw (new Exception("Unable to find User")); }

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (RedirectToAction("Index","MasterUsers", new { ErrorMessage = exception.Message, SuccessMessage = string.Empty }));
            
        }
        [HttpPost]
        public ActionResult Index(MasterUserProfileViewModel masterUserProfileViewModel)
        {
            MasterUserSession masterUserSession = null;
            
            Exception exception = null;
            bool result = false;
            try
            {
                  
                if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out masterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                bool CheckingModel = ModelState.IsValid;
                if (!ModelState.IsValid){throw (new Exception("There Were Some Invalid Values"));}
                if (masterUserProfileViewModel.Id > 0)
                {
                    MasterUser user = new MasterUser();
                    MasterUser userEntity = new MasterUser();
                    user.Address1 = masterUserProfileViewModel.Address1; 
                    user.Address2 = masterUserProfileViewModel.Address2;
                    user.City = masterUserProfileViewModel.City;
                    user.Country = masterUserProfileViewModel.Country;
                    user.Email = masterUserProfileViewModel.Email;
                    user.Id = masterUserProfileViewModel.Id;
                    user.NameFamily = masterUserProfileViewModel.NameFamily;
                    user.NameGiven = masterUserProfileViewModel.NameGiven;
                    user.PasswordHash = masterUserProfileViewModel.Password;
                    user.PasswordSalt = "salt";
                    user.PhoneMobile = masterUserProfileViewModel.PhoneMobile;
                    user.PhoneWork = masterUserProfileViewModel.PhoneWork;
                    user.ActiveDirectoryId = masterUserProfileViewModel.ActiveDirectoryId;
                    //user.UserName = masterUserProfileViewModel.UserName;
                    user.AuthenticationType = masterUserProfileViewModel.AuthenticationType;
                    user.ZipOrPostCode = masterUserProfileViewModel.ZipOrPostCode;
                    user.ActiveDirectory_AuthenticationEmail = masterUserProfileViewModel.ActiveDirectory_AuthenticationEmail;
                    user.ActiveDirectory_AuthenticationPhone = masterUserProfileViewModel.ActiveDirectory_AuthenticationPhone;
                    user.ActiveDirectory_AuthenticationPhoneAlternate = masterUserProfileViewModel.ActiveDirectory_AuthenticationPhoneAlternate;
                    user.ActiveDirectory_Department = masterUserProfileViewModel.ActiveDirectory_Department;
                    user.ActiveDirectory_JobTitle = masterUserProfileViewModel.ActiveDirectory_JobTitle;
                    user.ActiveDirectory_ManagerId = masterUserProfileViewModel.ActiveDirectory_ManagerId;
                    user.ActiveDirectory_NameDisplay = masterUserProfileViewModel.ActiveDirectory_NameDisplay;
                    user.ActiveDirectory_ObjectId = masterUserProfileViewModel.ActiveDirectory_ObjectId;
                    user.ActiveDirectory_RoleDisplayName = masterUserProfileViewModel.ActiveDirectory_RoleDisplayName;
                    user.ActiveDirectory_UsageLocation = masterUserProfileViewModel.ActiveDirectory_UsageLocation;
                    result = MasterUserManagement.UpdateUser(masterUserSession, user,out userEntity, out exception);
                    if (exception != null) { throw (exception); }
                    masterUserProfileViewModel.Password = string.Empty;
                    masterUserProfileViewModel.PasswordConfirmation = string.Empty;
                    this.ViewBag.SuccessMessage = "User Successfully Updated.";
                    return RedirectToAction("Index", "MasterUser", new { id = user.Id });
                }
                else
                {
                    MasterUser user = new MasterUser();
                    user.Address1 = masterUserProfileViewModel.Address1;
                    user.Address2 = masterUserProfileViewModel.Address2;
                    user.City = masterUserProfileViewModel.City;
                    user.Country = masterUserProfileViewModel.Country;
                    user.Email = masterUserProfileViewModel.Email;
                    user.NameFamily = masterUserProfileViewModel.NameFamily;
                    user.NameGiven = masterUserProfileViewModel.NameGiven;
                    user.PasswordHash = masterUserProfileViewModel.Password;
                    user.PasswordSalt = "salt";
                    user.PhoneMobile = masterUserProfileViewModel.PhoneMobile;
                    user.PhoneWork = masterUserProfileViewModel.PhoneWork;
                    user.ActiveDirectoryId = masterUserProfileViewModel.ActiveDirectoryId;
                    user.UserName = masterUserProfileViewModel.UserName;
                    user.AuthenticationType = masterUserProfileViewModel.AuthenticationType;
                    user.ZipOrPostCode = masterUserProfileViewModel.ZipOrPostCode;
                    user.DateTimeCreated = DateTime.UtcNow;
                    user.ActiveDirectory_AuthenticationEmail = masterUserProfileViewModel.ActiveDirectory_AuthenticationEmail;
                    user.ActiveDirectory_AuthenticationPhone = masterUserProfileViewModel.ActiveDirectory_AuthenticationPhone;
                    user.ActiveDirectory_AuthenticationPhoneAlternate = masterUserProfileViewModel.ActiveDirectory_AuthenticationPhoneAlternate;
                    user.ActiveDirectory_Department = masterUserProfileViewModel.ActiveDirectory_Department;
                    user.ActiveDirectory_JobTitle = masterUserProfileViewModel.ActiveDirectory_JobTitle;
                    user.ActiveDirectory_ManagerId = masterUserProfileViewModel.ActiveDirectory_ManagerId;
                    user.ActiveDirectory_NameDisplay = masterUserProfileViewModel.ActiveDirectory_NameDisplay;
                    user.ActiveDirectory_ObjectId = masterUserProfileViewModel.ActiveDirectory_ObjectId;
                    user.ActiveDirectory_RoleDisplayName = masterUserProfileViewModel.ActiveDirectory_RoleDisplayName;
                    user.ActiveDirectory_UsageLocation = masterUserProfileViewModel.ActiveDirectory_UsageLocation;
                    result = MasterUserManagement.AddUser(masterUserSession, user, out exception);
                    if (exception != null) { throw exception; }
                    return RedirectToAction("Index", "MasterUsers", new { ErrorMessage = string.Empty, SuccessMessage = "User Successfully Added." });
                }

            }
            catch (Exception ex)
            {
                exception = ex;
                masterUserProfileViewModel.ActiveDirectory_ManagerId = Guid.Empty;
                masterUserProfileViewModel.ActiveDirectory_ObjectId = Guid.Empty;
            }
            List<SelectListItem> userauthtype = EnumUtilities.GetSelectItemListFromEnum<AuthenticationType>(out exception);
            foreach (var authtype in userauthtype)
            {
                if (authtype.Text == masterUserProfileViewModel.AuthenticationType.ToString())
                {
                    authtype.Selected = true;
                }
            }
            this.ViewBag.AuthenticationType = userauthtype;
            if (exception != null) { this.ViewBag.ErrorMessage = exception.Message; } else { this.ViewBag.ErrorMessage = string.Empty; }
            
            return (this.View("~/Views/Master/User.cshtml", masterUserProfileViewModel));
        }
    }
}