using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Api
{
    public class TenantApiScanSessionsController : ApiController
    {
        // GET: TenantApiScanSessions
        [HttpGet]
        public JsonResult<ScanSession> ScanSessionEntry(string domain, string username, string token, string name, string description)
        {
            TenantUserSession tenantUserSession = null;
            AuthenticationManagement.ThrowOnInvalidToken
            (
                token,
                SessionType.Api,
                domain,
                username,
                HttpRequestUtilities.GetClientIpAddressFromHttpRequestMessage(this.Request),
                this.Request.Headers.UserAgent.ToString(),
                0,
                "",
                out tenantUserSession
            );
            ScanSession scanSession = ScanSessionManagement.CreateScanSessionEntry(tenantUserSession, name, description);
            return (this.Json(scanSession, JsonNetFormatter.JsonSerializerSettings));
        }

        [HttpGet]
        public JsonResult<ScanSession> ScanSessionEntryFinalize(string domain, string username, string token,long scanSessionId)
        {
            TenantUserSession tenantUserSession = null;
            AuthenticationManagement.ThrowOnInvalidToken
            (
                token,
                SessionType.Api,
                domain,
                username,
                HttpRequestUtilities.GetClientIpAddressFromHttpRequestMessage(this.Request),
                this.Request.Headers.UserAgent.ToString(),
                0,
                "",
                out tenantUserSession
            );
            ScanSession scanSession = ScanSessionManagement.ScanSessionEntryFinalize(tenantUserSession,scanSessionId);
            return (this.Json(scanSession, JsonNetFormatter.JsonSerializerSettings));
        }

        [HttpGet]
        public JsonResult<ScanSession> ScanSessionEdit(string domain, string username, string token, long id, string name, string description)
        {
            TenantUserSession tenantUserSession = null;
            AuthenticationManagement.ThrowOnInvalidToken
            (
                token,
                SessionType.Api,
                domain,
                username,
                HttpRequestUtilities.GetClientIpAddressFromHttpRequestMessage(this.Request),
                this.Request.Headers.UserAgent.ToString(),
                0,
                "",
                out tenantUserSession
            );
            ScanSession tenantScanSession = ScanSessionManagement.EditScanSession(tenantUserSession,id, name, description);
            return (this.Json(tenantScanSession, JsonNetFormatter.JsonSerializerSettings));
        }
    }
}