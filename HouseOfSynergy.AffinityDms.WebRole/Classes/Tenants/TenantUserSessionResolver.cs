using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
	public sealed class TenantUserSessionResolver
	{
		public const string TenantUserSessionCookieName = "TenantUserSessionCookie";

		public static bool GetTenantUserSessionFromHttpRequest (HttpRequestBase request, out TenantUserSession tenantUserSession, out Exception exception)
		{
			var result = false;

			exception = null;
			tenantUserSession = null;

			try
			{
				tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

				var cookie = request.Cookies [TenantUserSessionResolver.TenantUserSessionCookieName];

				tenantUserSession.Session.Id = long.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_Id"] ?? "")));
				tenantUserSession.Session.Guid = Guid.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_Guid"] ?? "")));
				tenantUserSession.Session.SessionId = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_SessionId"] ?? ""));
				tenantUserSession.Session.Token = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_Token"] ?? ""));
				tenantUserSession.Session.CultureName = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_CultureName"] ?? ""));
				tenantUserSession.Session.UserAgent = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_UserAgent"] ?? ""));
				tenantUserSession.Session.IPAddressString = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_IPAddressString"] ?? ""));
				tenantUserSession.Session.RijndaelKey = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_RijndaelKey"] ?? ""));
				tenantUserSession.Session.RsaKeyPublic = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Session_RsaKeyPublic"] ?? ""));
				tenantUserSession.User.Id = long.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_User_Id"] ?? "")));
				tenantUserSession.User.NameGiven = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_User_NameGiven"] ?? ""));
				tenantUserSession.User.NameFamily = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_User_NameFamily"] ?? ""));
				tenantUserSession.User.UserName = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_User_UserName"] ?? ""));
				tenantUserSession.User.Email = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_User_Email"] ?? ""));
				tenantUserSession.User.AuthenticationType = (AuthenticationType) Enum.Parse(typeof(AuthenticationType), GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_User_UserType"] ?? "")));
				tenantUserSession.User.ActiveDirectory_ObjectId = Guid.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_User_ActiveDirectory_ObjectId"] ?? "")));
				tenantUserSession.Tenant.Id = long.Parse(GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Tenant_Id"] ?? "")));
				tenantUserSession.Tenant.Domain = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Tenant_Domain"] ?? ""));
				tenantUserSession.Tenant.CompanyName = GlobalConstants.EncodingCryptography.GetString(Convert.FromBase64String(cookie.Values ["TenantUserSession_Tenant_CompanyName"] ?? ""));

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
				tenantUserSession = null;
			}

			return (result);
		}

		public static bool GetTenantUserSessionHttpCookieFromTenantUserSession (TenantUserSession tenantUserSession, out HttpCookie cookie, out Exception exception)
		{
			var result = false;

			cookie = null;
			exception = null;

			try
			{
				cookie = new HttpCookie(TenantUserSessionResolver.TenantUserSessionCookieName);

				cookie.Values.Add("TenantUserSession_Session_Id", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.Id.ToString() ?? "")));
				cookie.Values.Add("TenantUserSession_Session_Guid", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.Guid.ToString() ?? "")));
				cookie.Values.Add("TenantUserSession_Session_SessionId", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.SessionId ?? "")));
				cookie.Values.Add("TenantUserSession_Session_Token", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.Token ?? "")));
				cookie.Values.Add("TenantUserSession_Session_CultureName", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.CultureName ?? "")));
				cookie.Values.Add("TenantUserSession_Session_UserAgent", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.UserAgent ?? "")));
				cookie.Values.Add("TenantUserSession_Session_IPAddressString", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.IPAddressString ?? "")));
				cookie.Values.Add("TenantUserSession_Session_RijndaelKey", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.RijndaelKey ?? "")));
				cookie.Values.Add("TenantUserSession_Session_RsaKeyPublic", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Session.RsaKeyPublic ?? "")));
				cookie.Values.Add("TenantUserSession_User_Id", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.User.Id.ToString() ?? "")));
				cookie.Values.Add("TenantUserSession_User_NameGiven", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.User.NameGiven ?? "")));
				cookie.Values.Add("TenantUserSession_User_NameFamily", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.User.NameFamily ?? "")));
				cookie.Values.Add("TenantUserSession_User_UserName", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.User.UserName ?? "")));
				cookie.Values.Add("TenantUserSession_User_Email", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.User.Email ?? "")));
				cookie.Values.Add("TenantUserSession_User_UserType", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.User.AuthenticationType.ToString() ?? "")));
				cookie.Values.Add("TenantUserSession_User_ActiveDirectory_ObjectId", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.User.ActiveDirectory_ObjectId.ToString(GuidUtilities.EnumGuidFormat.N.ToString()))));
				cookie.Values.Add("TenantUserSession_Tenant_Id", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Tenant.Id.ToString() ?? "")));
				cookie.Values.Add("TenantUserSession_Tenant_Domain", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Tenant.Domain ?? "")));
				cookie.Values.Add("TenantUserSession_Tenant_CompanyName", Convert.ToBase64String(GlobalConstants.EncodingCryptography.GetBytes(tenantUserSession.Tenant.CompanyName ?? "")));

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