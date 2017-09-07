using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Utilities;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.PowerTools.Library.Extensions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Pages
{
	public partial class TenantApiWebForms:
		System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			var type = ApiTenantType.None;
			var document = Api.CreateRequest(type);
			var source = Api.GetApiHttpRequestNameValueCollection(this.Request);

			if (!string.IsNullOrWhiteSpace(source ["Type"]))
			{
				type = (ApiTenantType) Enum.Parse(typeof(ApiTenantType), source ["Type"]);

				try
				{
					if (Enum.IsDefined(typeof(ApiTenantType), type))
					{
						document = Api.CreateRequest(type);

						switch (type)
						{
							case ApiTenantType.Ping: { this.ProcessPing(source, document); break; }
							case ApiTenantType.SignIn: { this.ProcessSignIn(source, document); break; }
							case ApiTenantType.SignOut: { this.ProcessSignOut(source, document); break; }
							case ApiTenantType.GetFolders: { this.ProcessGetFolders(source, document); break; }
							case ApiTenantType.GetPublicKey: { this.ProcessGetPublicKey(source, document); break; }
							case ApiTenantType.ValidateToken: { this.ProcessValidateToken(source, document); break; }
							case ApiTenantType.GetDocumentByHash: { this.ProcessGetDocumentByHash(source, document); break; }
							case ApiTenantType.DocumentEntryCreate: { this.ProcessDocumentEntryCreate(source, document); break; }
							case ApiTenantType.DocumentEntryFinalize: { this.ProcessDocumentEntryFinalize(source, document); break; }
							case ApiTenantType.DocumentEntryPerformOcr: { this.ProcessDocumentEntryPerformOcr(source, document); break; }
							default: { this.ProcessBadRequest(source, document); break; }
						}
					}
					else
					{
						this.ProcessBadRequest(source, document);
					}
				}
				catch (Exception exception)
				{
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringFailure;
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Message"].Value = exception.Message;
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Exception"].Value = exception.GetType().FullName;

					if (AffinityConfiguration.IsConfigurationDebug)
					{
						document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["StackTrace"].Value = exception.ToString();
					}
				}
				finally
				{
					// TODO: Encrypt.
					this.Response.Clear();
					this.Response.Write(document.OuterXml);
				}
			}
		}

		private void ProcessBadRequest (NameValueCollection source, XmlDocument document)
		{
			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringFailure;
			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Exception"].Value = "Bad request type.";
		}

		private void ProcessPing (NameValueCollection source, XmlDocument document)
		{
			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
		}

		private void ProcessSignIn (NameValueCollection source, XmlDocument document)
		{
			User user = null;
			Tenant tenant = null;
			Session session = null;
			Exception exception = null;
			var domain = source ["Domain"];
			var username = source ["Username"];
			var passwordHash = source ["PasswordHash"];
			TenantUserSession tenantUserSession = null;

			if (string.IsNullOrWhiteSpace(username)) { throw (new Exception("Request parameters missing.")); }
			if (string.IsNullOrWhiteSpace(passwordHash)) { throw (new Exception("Request parameters missing.")); }
			if (username.Length == 0) { throw (new Exception("Request parameters invalid.")); }
			if (passwordHash.Length == 0) { throw (new Exception("Request parameters invalid.")); }

			if (AuthenticationManagement.SignIn(SessionType.Api, domain, username, passwordHash, "", "", 0L, "", out tenantUserSession, out exception))
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(tenant.ToXmlElement(document));
				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(user.ToXmlElement(document));
				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(session.ToXmlElement(document));
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
			}
			else
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringFailure;
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Exception"].Value = exception.Message;

				if (AffinityConfiguration.IsConfigurationDebug)
				{
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["StackTrace"].Value = exception.StackTrace;
				}
			}
		}

		private void ProcessSignOut (NameValueCollection source, XmlDocument document)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			var user = this.ThrowOnInValidToken(source);

			if (AuthenticationManagement.SignOut(tenantUserSession.Tenant.Domain, tenantUserSession.User.UserName, tenantUserSession.Session.Token, out exception))
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
			}
			else
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringFailure;
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Exception"].Value = exception.Message;

				if (AffinityConfiguration.IsConfigurationDebug)
				{
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["StackTrace"].Value = exception.StackTrace;
				}
			}
		}

		private void ProcessGetPublicKey (NameValueCollection source, XmlDocument document)
		{
			var id = 0L;
			var key = "";
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (string.IsNullOrWhiteSpace(source ["Id"])) { throw (new Exception("Request parameters missing.")); }
			if (!long.TryParse(source ["Id"], out id)) { throw (new Exception("Request parameters invalid.")); }
			if (id <= 0) { throw (new Exception("Request parameters invalid.")); }

			tenantUserSession = new TenantUserSession(new Tenant() { Id = id, });

			if (TenantManagement.GetPublicKey(tenantUserSession, id, out key, out exception))
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].InnerText = key;
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
			}
			else
			{
				throw (exception);
			}
		}

		private void ProcessGetFolders (NameValueCollection source, XmlDocument document)
		{
			Exception exception = null;
			List<Folder> folders = null;

			var tenantUserSession = this.ThrowOnInValidToken(source);

			folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: BusinessLayer.Lookup.FolderResultType.All, includeFolderUsers: true);
			if (folders != null)
			{
				//Faraz Get Approval
				foreach (var folder in folders)
				{
					var documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, documentId: null, documentIdType: null, folderId: folder.Id, documentResultVersionType: DocumentResultVersionType.All, includeCreatorUser: true, includeCheckedOutUser: true).ToList();
					foreach (var doc in documents) { folder.Documents.Add(doc); }
				}
			}
			if (folders != null)
			{
				document.DocumentElement["DMS"]["API"]["Response"]["Data"].AppendChild(folders.ToXmlElement(document));
				document.DocumentElement["DMS"]["API"]["Response"].Attributes["Result"].Value = Api.StringSuccess;
			}
			else { throw (new Exception("Unable to find the following folder")); }
		}

		/// <param name="hash">The SHA512 hash value to find.</param>
		private void ProcessGetDocumentByHash (NameValueCollection source, XmlDocument document)
		{
			Document doc = null;
			Exception exception = null;
			string hash = source ["Hash"];

			if (string.IsNullOrWhiteSpace(hash)) { throw (new Exception("Request parameters missing.")); }

			var tenantUserSession = this.ThrowOnInValidToken(source);

			if (DocumentManagement.GetDocumentByHash(tenantUserSession, hash, out doc, out exception))
			{
				var element = document.CreateElement("Document");

				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(element);

				// TODO: Fill doc object.
				element.Attributes.Append(document, "Id", doc.Id.ToString());

				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
			}
			else
			{
				throw (exception);
			}
		}

		private void ProcessDocumentEntryCreate (NameValueCollection source, XmlDocument document)
		{
			var sizeValue = 0L;
			Document doc = null;
			var folderIdValue = 0L;
			Exception exception = null;

			var tenantUserSession = this.ThrowOnInValidToken(source);

			if (!tenantUserSession.User.Roles.Any(r => r.RoleType == TenantRoleType.Uploader)) { throw (new UserNotAuthorizedException()); }

			var hash = source ["Hash"];
			var size = source ["Size"];
			var filename = source ["Filename"];
			var folderId = source ["FolderId"];

			if (string.IsNullOrWhiteSpace(hash)) { throw (new Exception("Request parameters missing.")); }
			if (string.IsNullOrWhiteSpace(size)) { throw (new Exception("Request parameters missing.")); }
			if (string.IsNullOrWhiteSpace(filename)) { throw (new Exception("Request parameters missing.")); }

			if (!long.TryParse(size, out sizeValue)) { throw (new Exception("Request parameters invalid.")); }
			if (sizeValue <= 0) { throw (new Exception("Request parameters invalid.")); }
			try { new FileInfo(filename); }
			catch { throw (new Exception("Request parameters invalid.")); }

			if (!string.IsNullOrWhiteSpace(folderId))
			{
				if (!long.TryParse(folderId, out folderIdValue)) { throw (new Exception("Request parameters invalid.")); }
				if (folderIdValue <= 0) { throw (new Exception("Request parameters invalid.")); }
			}

			if (DocumentManagement.GetDocumentByHash(tenantUserSession, hash, out doc, out exception))
			{
				if (doc == null)
				{
                    //Faraz ASK RAHEEK BHAI
					if (DocumentManagement.CreateDocumentEntry(tenantUserSession, filename, hash, long.Parse(size), folderIdValue, null, tenantUserSession.User, out doc, out exception))
					{
						var element = doc.ToXmlElement(document);

						document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(element);

						document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
					}
					else
					{
						throw (exception);
					}
				}
				else
				{
					var element = doc.ToXmlElement(document);

					document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(element);

					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
				}
			}
			else
			{
				throw (exception);
			}
		}

		private void ProcessDocumentEntryFinalize (NameValueCollection source, XmlDocument document)
		{
			var id = 0L;
			Document doc = null;
			var textId = source ["Id"];
			Exception exception = null;

			var tenantUserSession = this.ThrowOnInValidToken(source);

			if (!tenantUserSession.User.Roles.Any(r => r.RoleType == TenantRoleType.Uploader)) { throw (new UserNotAuthorizedException()); }

			if (string.IsNullOrWhiteSpace(textId)) { throw (new Exception("Request parameters missing.")); }
			if (!long.TryParse(textId, out id)) { throw (new Exception("Request parameters invalid.")); }
			if (id <= 0) { throw (new Exception("Request parameters invalid.")); }

			if (DocumentManagement.DocumentEntryFinalize(tenantUserSession, id, out doc, out exception))
			{
				var element = doc.ToXmlElement(document);

				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(element);

				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
			}
			else
			{
				throw (exception);
			}
		}

		/// <summary>
		/// Performs Full Text OCR.
		/// The caller of this API should expect a timeout.
		/// Any errors should be properly logged in verbose mode.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="document"></param>
		private void ProcessDocumentEntryPerformOcr (NameValueCollection source, XmlDocument document)
		{
			try
			{
				var id = 0L;
				var result = false;
				Exception exception = null;
				Tenant tenantObject = null;
				Document documentObject = null;

				var tenantUserSession = this.ThrowOnInValidToken(source);

				if (!tenantUserSession.User.Roles.Any(r => r.RoleType == TenantRoleType.Uploader)) { throw (new UserNotAuthorizedException()); }

				if (string.IsNullOrWhiteSpace(source ["Id"])) { throw (new Exception("Request parameters missing.")); }
				if (!long.TryParse(source ["Id"], out id)) { throw (new Exception("Request parameters invalid.")); }
				if (id <= 0) { throw (new Exception("Request parameters invalid.")); }

				result = DocumentManagement.GetDocumentById(tenantUserSession, id, out documentObject, out exception);
				if (!result) { throw (exception); }
				if (document == null) { throw (new RowNotFoundException()); }
				if (documentObject.DocumentType != DocumentType.Raster) { throw (new DocumentTypeException()); }
				if (documentObject.State != DocumentState.QueuedAuto) { throw (new DocumentStateException()); }

				var type = DocumentUtilities.GetFileType(documentObject.FileNameClient);
				var format = DocumentUtilities.GetFileFormatType(documentObject.FileNameClient);

				if (type == FileType.Document)
				{
					if (format != FileFormatType.Pdf) { throw (new DocumentTypeException()); }
				}
				else if (type == FileType.Image)
				{
					if ((format != FileFormatType.Bmp) && (format != FileFormatType.Jpg) && (format != FileFormatType.Png) && (format != FileFormatType.Tiff))
					{
						throw (new DocumentTypeException());
					}
				}
				else
				{
					throw (new DocumentTypeException());
				}

				result = TenantManagement.GetTenantSelf(tenantUserSession, out tenantObject, out exception);
				if (!result) { throw (exception); }
				if (document == null) { throw (new RowNotFoundException()); }

				var account = CloudStorageAccount.Parse(tenantObject.StorageConnectionStringPrimary);
				var client = account.CreateCloudBlobClient();
				var container = client.GetContainerReference(tenantObject.UrlResourceGroup);

				container.CreateIfNotExists();
				container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

				var blobName = documentObject.Id.ToString() + "." + GlobalConstants.FileExtensionCloud;
				var blob = container.GetBlockBlobReference(blobName);

				if (blob.BlobType != BlobType.BlockBlob) { throw (new BlobTypeException()); }

				var file = new FileInfo(this.MapPath("../App_Data/Documents/") + id.ToString() + "." + Path.GetExtension(documentObject.FileNameClient).TrimStart('.'));

				blob.DownloadToFile(file.FullName, AffinityConfiguration.IsConfigurationDebug ? FileMode.Create : FileMode.CreateNew);

				using (var fileStream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.None))
				{
					using (var algorithm = HashAlgorithm.Create(GlobalConstants.AlgorithmHashName))
					{
						var hash = Convert.ToBase64String(algorithm.ComputeHash(fileStream));

						if (string.Compare(documentObject.Hash, hash, StringComparison.OrdinalIgnoreCase) != 0) { throw (new DocumentHashMismatchException()); }
					}
				}

				if (type == FileType.Document)
				{
					if (format == FileFormatType.Pdf)
					{
						// Process PDF.
						// You can use the [stream] and [file] objects now.
						// Use [using] statements for all LeadTools objects that implement IDisposable.

						// Do not execute the following lines in case of errors.
						var element = documentObject.ToXmlElement(document);
						document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(element);
						document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
					}
					else
					{
						throw (new DocumentTypeException());
					}
				}
				else if (type == FileType.Image)
				{
					System.Drawing.Bitmap bitmap = null;

					using (var image = System.Drawing.Image.FromFile(file.FullName))
					{
						switch (format)
						{
							case FileFormatType.Bmp:
							case FileFormatType.Jpg:
							case FileFormatType.Png:
							case FileFormatType.Tiff: { bitmap = new System.Drawing.Bitmap(image); break; }
							default: { throw (new DocumentTypeException()); }
						}
					}

					file = new FileInfo(Path.ChangeExtension(file.FullName, ".png"));
					bitmap.Save(file.FullName, System.Drawing.Imaging.ImageFormat.Png);

					// Process Image.
					// You can use the [bitmap] and [file] objects now.
					// Use [using] statements for all LeadTools objects that implement IDisposable.
					//OCRClassification ocrclassification = new OCRClassification();
					//Document returningdocument = null;
					//result = ocrclassification.PerformOCR(bitmap, documentObject, out returningdocument, out exception);
					if (exception != null)
					{
						throw exception;
					}
					bitmap.Dispose();
					// Do not execute the following line in case of errors.
					var element = documentObject.ToXmlElement(document);
					document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(element);
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
				}
				else
				{
					throw (new DocumentTypeException());
				}
			}
			catch (Exception exception)
			{
				// Log error using the Semantic Logging Application Block from Microsoft Enterprise Library.
				// This library needs to configured for use throughout the application.
				//Debug.Print(exception.ToString());

				throw;
			}
			finally
			{
			}
		}

		private void ProcessValidateToken (NameValueCollection source, XmlDocument document)
		{
			Exception exception = null;
			string token = source ["Token"];
			TenantUserSession tenantUserSession = null;

			if (string.IsNullOrWhiteSpace(token)) { throw (new Exception("Request parameters missing.")); }

			if (AuthenticationManagement.ValidateToken(token, SessionType.Api, "", "", "", "", 0, "", out tenantUserSession, out exception))
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].Attributes.Append(document, "Validated", true.ToString());
			}
			else
			{
				if (exception is TokenExpiredException)
				{
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
					document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].Attributes.Append(document, "Validated", false.ToString());
				}

				throw (exception);
			}
		}

		private TenantUserSession ThrowOnInValidToken (NameValueCollection source)
		{
			string token = source ["Token"];
			TenantUserSession tenantUserSession = null;

			if (string.IsNullOrWhiteSpace(token)) { throw (new Exception("Request parameters missing.")); }

			AuthenticationManagement.ThrowOnInvalidToken(token, SessionType.Api, "", "", "", "", 0, "", out tenantUserSession);

			return (tenantUserSession);
		}
	}
}