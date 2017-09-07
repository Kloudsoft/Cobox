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

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
	public class MasterDashboardController:
		Controller
	{
		[HttpGet]
		public ActionResult Index ()
		{
			Exception exception = null;
			var masterUserSession = this.TempData [typeof(MasterUserSession).Name] as MasterUserSession;

			if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out masterUserSession, out exception)) { throw (exception); /*this.Response.RedirectToRoute("MasterSignIn");*/ }
			if (!MasterUserManagement.IsUserActionAllowed(masterUserSession.User, MasterActionType.DashboardView)) { throw (new UserNotAuthorizedException()); }

			return (this.View("~/Views/Master/Dashboard.cshtml"));
		}
	}
}