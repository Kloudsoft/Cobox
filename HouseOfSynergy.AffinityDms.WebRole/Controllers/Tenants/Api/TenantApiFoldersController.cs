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
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers.Api
{
	public class TenantApiFoldersController:
		ApiController
	{
		[HttpGet]
		public JsonResult<List<Folder>> Get (string domain, string username, string token)
		{
			List<Folder> folders = null;
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
			folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: BusinessLayer.Lookup.FolderResultType.All, includeFolderUsers: true);
			if (folders != null)
			{
				//Faraz Get Approval
				foreach (var folder in folders)
				{
					var documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, documentId: null, documentIdType: null, folderId: folder.Id, documentResultVersionType: DocumentResultVersionType.All, includeCreatorUser: true, includeCheckedOutUser: true).ToList();
					foreach (var document in documents) { folder.Documents.Add(document); }
				}
			}
			if (folders == null) { throw (new Exception("Unable to find the following folder")); }
			return (this.Json(folders, JsonNetFormatter.JsonSerializerSettings));
		}
	}
}