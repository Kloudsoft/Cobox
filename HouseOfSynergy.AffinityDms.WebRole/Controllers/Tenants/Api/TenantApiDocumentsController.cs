using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System.Configuration;
using OptimaJet.Workflow.Core.Runtime;
using HouseOfSynergy.AffinityDms.Library.Workflow;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers.Api
{
	public class TenantApiDocumentsController:
		ApiController
	{
		[HttpGet]
		public JsonResult<List<Document>> GetAll (string domain, string username, string token)
		{
			Exception exception = null;
			List<Document> documents = null;
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
            //AuditTrailEntry audittrail = new AuditTrailEntry();
            //audittrail.EntityType = EntityType.Document;
            //audittrail.AuditTrailActionType = AuditTrailActionType.Rename;
           
            //audittrail.UserId = tenantUserSession.User.Id;
            if (!DocumentManagement.GetAllDocuments(tenantUserSession, out documents, out exception)) { throw (exception); }

			return (this.Json(documents, JsonNetFormatter.JsonSerializerSettings));
		}

		[HttpGet]
		public JsonResult<Document> Get (string domain, string username, string token, string hash)
		{
			Document document = null;
			Exception exception = null;
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

			if (!DocumentManagement.GetDocumentByHash(tenantUserSession, hash, out document, out exception)) { throw (exception); }

			return (this.Json(document, JsonNetFormatter.JsonSerializerSettings));
		}

		[HttpGet]
		public JsonResult<Document> Get (string domain, string username, string token, string filename, string hash, long size, long? folderId, long? scanSessionId)
		{
			Document document = null;
			Exception exception = null;
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

			if (!DocumentManagement.CreateDocumentEntry(tenantUserSession, filename, hash, size, folderId, scanSessionId, tenantUserSession.User, out document, out exception))
			{ throw (exception); }

			return (this.Json(document, JsonNetFormatter.JsonSerializerSettings));
		}
        //[HttpGet]
        //public JsonResult<Document> UploadDocument(string domain, string username, string token, string filename, long? folderId, byte[] byteArray)
        //{
        //    Document document = null;
        //    Exception exception = null;
        //    TenantUserSession tenantUserSession = null;

        //    AuthenticationManagement.ThrowOnInvalidToken
        //    (
        //        token,
        //        SessionType.Api,
        //        domain,
        //        username,
        //        HttpRequestUtilities.GetClientIpAddressFromHttpRequestMessage(this.Request),
        //        this.Request.Headers.UserAgent.ToString(),
        //        0,
        //        "",
        //        out tenantUserSession
        //    );

        //    if (!DocumentManagement.CreateDocumentEntry(tenantUserSession, filename, hash, size, folderId, tenantUserSession.User, out document, out exception))
        //    { throw (exception); }

        //    return (this.Json(document, JsonNetFormatter.JsonSerializerSettings));
        //}

        [HttpGet]
		public JsonResult<Document> DocumentEntryFinalize (string domain, string username, string token, long documentId)
		{
			Document document = null;
			Exception exception = null;
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

			if (!DocumentManagement.DocumentEntryFinalize(tenantUserSession, documentId, out document, out exception)) { throw (exception); }

            //WorkflowEngineHelper wfehelper = new WorkflowEngineHelper(ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString, "SimpleWF", null);
            //wfehelper.CreateInstance(out exception);
            //if (exception != null) { throw exception; }
            //var processId = wfehelper.processId;
            //if (processId != null)
            //{
            //    if (processId != Guid.Empty)
            //    {
            //        document.ProcessInstanceId = processId;
            //        document.SchemeCode = "SimpleWF";
            //        bool result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
            //        if (exception != null) { throw exception; }
            //        List<WorkflowCommand> commands = null;
            //        wfehelper.GetAvailableCommands(out commands, out exception);
            //        if (exception != null) { throw exception; }
            //        var command = commands.Where(x => x.CommandName.ToLower() == "uploadandfinalized").SingleOrDefault();
            //        if (command != null)
            //        {
            //            wfehelper.ExecuteCommand(command, out exception);
            //            if (exception != null) { throw exception; }
            //        }
            //    }
            //}
            return (this.Json(document, JsonNetFormatter.JsonSerializerSettings));
		}
	}
}