using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.WebRole.Models.Common;
using HouseOfSynergy.AffinityDms.WebRole.Models.Master;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
	public class MasterUserCurrentController:
		Controller
    {
        [MasterMvcTokenAuthorize]
        public ActionResult Index()
        {
			Exception exception = null;
			MasterUserSession masterUserSession = null;
			var userViewModel = new UserCurrentViewModel();

			if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out masterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }

			userViewModel.NameGiven = masterUserSession.User.NameGiven;
			userViewModel.NameFamily = masterUserSession.User.NameFamily;
			userViewModel.Email = masterUserSession.User.Email;
			userViewModel.UserName = masterUserSession.User.UserName;
			userViewModel.UserType = masterUserSession.User.AuthenticationType;
			userViewModel.Company = "???";
			userViewModel.Domain = "???";

			this.TempData.Remove(typeof(MasterUserSession).Name);
			this.TempData.Add(typeof(MasterUserSession).Name, masterUserSession);

			return (this.PartialView("~/Views/Master/UserCurrent.cshtml", masterUserSession));
        }
    }
}