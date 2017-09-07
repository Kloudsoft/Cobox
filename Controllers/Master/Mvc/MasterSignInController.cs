using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.ResourceProvider;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.WebRole.Models.Common;
using HouseOfSynergy.AffinityDms.WebRole.Models.Master;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
	public class MasterSignInController:
		Controller
	{
		[HttpGet]
		public ActionResult Index ()
		{
			var model = new SignInViewModel();

			model.Domain = "houseofsynergy.com";
			model.UserName = "raheel.khan";
			model.Password = "audience";

			return (this.View("~/Views/Master/SignIn.cshtml", model));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SignIn (SignInViewModel signInViewModel)
		{
			var passwordHash = "";
			Exception exception = null;
			MasterUserSession masterUserSession = null;

			if (!Sha.GenerateHash((signInViewModel.Password ?? ""), GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind, out passwordHash, out exception)) { throw (exception); }

			if
			(
				MasterAuthenticationManagement.SignIn
				(
					SessionType.Mvc,
					signInViewModel.UserName,
					passwordHash,
					this.Request.UserHostAddress,
					this.Request.UserAgent,
					long.MaxValue,
					this.Session.SessionID,
					out masterUserSession,
					out exception
				)
			)
			{
				HttpCookie cookie = null;

				if (!MasterUserSessionResolver.GetMasterUserSessionHttpCookieFromMasterUserSession(masterUserSession, out cookie, out exception)) { throw (exception); }

				this.Response.Cookies.Remove(MasterUserSessionResolver.MasterUserSessionCookieName);
				this.Response.SetCookie(cookie);

				var userViewModel = new UserCurrentViewModel();
				userViewModel.NameGiven = masterUserSession.User.NameGiven;
				userViewModel.NameFamily = masterUserSession.User.NameGiven;
				userViewModel.Email = masterUserSession.User.Email;
				userViewModel.UserName = masterUserSession.User.UserName;
				userViewModel.UserType = masterUserSession.User.AuthenticationType;
				userViewModel.Company = "???";
				userViewModel.Domain = "???";

				this.ViewBag.UserSession = userViewModel;

				return (this.RedirectToRoute("MasterDashboard"));
			}
			else
			{
				throw (exception);
			}
		}
	}
}