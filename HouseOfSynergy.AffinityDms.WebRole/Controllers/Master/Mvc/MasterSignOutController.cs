using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
	public class MasterSignOutController:
		Controller
	{
		public ActionResult Index ()
		{
			HttpCookie cookie = null;
			Exception exception = null;
			MasterUserSession masterUserSession = null;

			masterUserSession = new MasterUserSession(new MasterUser(), new MasterSession());

			if (!MasterUserSessionResolver.GetMasterUserSessionHttpCookieFromMasterUserSession(masterUserSession, out cookie, out exception)) { throw (exception); }

			this.Response.Cookies.Remove(MasterUserSessionResolver.MasterUserSessionCookieName);
			this.Response.SetCookie(cookie);

			// TODO: See which method allows the cookie to be deleted before redirection.
			return (this.View("~/Views/Master/Home.cshtml"));
			//return (this.RedirectToAction("Index", "TenantSignIn"));
		}
	}
}