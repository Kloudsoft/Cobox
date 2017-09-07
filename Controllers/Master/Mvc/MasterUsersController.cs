using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.WebRole.Models.Master;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class MasterUsersController :
        Controller
    {
        [HttpGet]
        public ActionResult Index(string ErrorMessage,string SuccessMessage)
        {
            Exception exception = null;
            var userEntities= new List<MasterUser>();
            MasterUserSession masterUserSession = null;
            var userViewModels = new List<MasterUserViewModel>();
            try
            {
                
                if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out masterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                bool result = MasterUserManagement.GetAllUsers(masterUserSession, out userEntities, out exception);
                if (exception != null) { throw (exception); }
                foreach (var userEntity in userEntities)
                {
                    userViewModels.Add(new MasterUserViewModel().FromEntity(userEntity));
                }
                if (!(string.IsNullOrEmpty(ErrorMessage))) { ViewBag.ErrorMessage = ErrorMessage; } else { ViewBag.ErrorMessage = string.Empty; }
                if (!(string.IsNullOrEmpty(SuccessMessage))) { ViewBag.SuccessMessage = SuccessMessage; } else { ViewBag.SuccessMessage = string.Empty; }
                return (this.View("~/Views/Master/Users.cshtml", userViewModels));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (this.View("~/Views/Master/Users.cshtml", userViewModels));
        }

        [HttpPost]
        public ActionResult Index(MasterUserViewModel[] item)
        {
            var userEntities = new List<MasterUser>();
            var userViewModels = new List<MasterUserViewModel>();
            Exception exception =null;
            bool result = false;
            MasterUserSession masterUserSession =null;
            try
            {
                if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out masterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
                for (int i = 0; i < item.Length; i++)
                {
                    var admin = item[i].RoleAdministrator;
                    var custom = item[i].RoleCustom;
                  //  var none = item[i].RoleNone;
                    var reporting = item[i].RoleReporting;
                    long userid = item[i].Id;
                    List<MasterRoleType> userroles = new List<MasterRoleType>();
                    if (admin)
                    {
                        userroles.Add(MasterRoleType.Administrator);
                    }
                    if (custom)
                    {
                        userroles.Add(MasterRoleType.Custom);
                    }
                    //if (none)
                    //{
                    //    userroles.Add(MasterRoleType.None);
                    //}
                    if (reporting)
                    {
                        userroles.Add(MasterRoleType.Reporting);
                    }
                    result =  MasterUserManagement.UpdateUserRoles(masterUserSession, userid, userroles, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                result = MasterUserManagement.GetAllUsers(masterUserSession, out userEntities, out exception);
                if (exception != null) { throw (exception); }
                foreach (var userEntity in userEntities)
                {
                    userViewModels.Add(new MasterUserViewModel().FromEntity(userEntity));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            // TODO: Update DB.
            return (this.View("~/Views/Master/Users.cshtml", userViewModels));
        }
    }
}