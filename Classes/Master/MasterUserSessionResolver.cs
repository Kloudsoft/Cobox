using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Master
{
	public sealed class MasterUserSessionResolver
	{
		public const string MasterUserSessionCookieName = "MasterUserSessionCookie";

		public static bool GetMasterUserSessionFromHttpRequest (HttpRequestBase request, out MasterUserSession masterUserSession, out Exception exception)
		{
			var result = false;

			exception = null;
			masterUserSession = null;

			try
			{
				masterUserSession = new MasterUserSession(new MasterUser(), new MasterSession());

				var cookie = request.Cookies [MasterUserSessionResolver.MasterUserSessionCookieName];

				masterUserSession.Session.Id = long.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_Id"] ?? "")));
				masterUserSession.Session.Guid = Guid.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_Guid"] ?? "")));
				masterUserSession.Session.SessionId = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_SessionId"] ?? ""));
				masterUserSession.Session.Token = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_Token"] ?? ""));
				masterUserSession.Session.CultureName = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_CultureName"] ?? ""));
				masterUserSession.Session.UserAgent = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_UserAgent"] ?? ""));
				masterUserSession.Session.IPAddressString = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_IPAddressString"] ?? ""));
				masterUserSession.Session.RijndaelKey = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_RijndaelKey"] ?? ""));
				masterUserSession.Session.RsaKeyPublic = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_Session_RsaKeyPublic"] ?? ""));
				masterUserSession.User.Id = long.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_User_Id"] ?? "")));
				masterUserSession.User.NameGiven = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_User_NameGiven"] ?? ""));
				masterUserSession.User.NameFamily = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_User_NameFamily"] ?? ""));
				masterUserSession.User.UserName = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_User_UserName"] ?? ""));
				masterUserSession.User.Email = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_User_Email"] ?? ""));
				masterUserSession.User.AuthenticationType = (AuthenticationType) Enum.Parse(typeof(AuthenticationType), GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_User_UserType"] ?? "")));
				masterUserSession.User.ActiveDirectoryId = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["MasterUserSession_User_ActiveDirectoryId"] ?? ""));

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
				masterUserSession = null;
			}

			return (result);
		}

		public static bool GetMasterUserSessionHttpCookieFromMasterUserSession (MasterUserSession masterUserSession, out HttpCookie cookie, out Exception exception)
		{
			var result = false;

			cookie = null;
			exception = null;

			try
			{
				cookie = new HttpCookie(MasterUserSessionResolver.MasterUserSessionCookieName);

				cookie.Values.Add("MasterUserSession_Session_Id", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.Id.ToString() ?? "")));
				cookie.Values.Add("MasterUserSession_Session_Guid", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.Guid.ToString() ?? "")));
				cookie.Values.Add("MasterUserSession_Session_SessionId", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.SessionId ?? "")));
				cookie.Values.Add("MasterUserSession_Session_Token", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.Token ?? "")));
				cookie.Values.Add("MasterUserSession_Session_CultureName", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.CultureName ?? "")));
				cookie.Values.Add("MasterUserSession_Session_UserAgent", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.UserAgent ?? "")));
				cookie.Values.Add("MasterUserSession_Session_IPAddressString", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.IPAddressString ?? "")));
				cookie.Values.Add("MasterUserSession_Session_RijndaelKey", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.RijndaelKey ?? "")));
				cookie.Values.Add("MasterUserSession_Session_RsaKeyPublic", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.Session.RsaKeyPublic ?? "")));
				cookie.Values.Add("MasterUserSession_User_Id", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.User.Id.ToString() ?? "")));
				cookie.Values.Add("MasterUserSession_User_NameGiven", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.User.NameGiven ?? "")));
				cookie.Values.Add("MasterUserSession_User_NameFamily", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.User.NameFamily ?? "")));
				cookie.Values.Add("MasterUserSession_User_UserName", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.User.UserName ?? "")));
				cookie.Values.Add("MasterUserSession_User_Email", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.User.Email ?? "")));
				cookie.Values.Add("MasterUserSession_User_UserType", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.User.AuthenticationType.ToString() ?? "")));
				cookie.Values.Add("MasterUserSession_User_ActiveDirectoryId", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(masterUserSession.User.ActiveDirectoryId ?? "")));

				result = true;
			}
			catch (Exception e)
			{
				cookie = null;
				exception = e;
			}

			return (result);
		}
	}
}