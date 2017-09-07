using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using Newtonsoft.Json;
using System.IO;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
	public class TenantDocumentsController : Controller
	{
		// GET: TenantDocuments
		//public ActionResult Index(string ErrorMessage = "", string SuccessMessage = "")
		//{
		//	List<Document> documEntentities = null;
		//	//List<DocumentsViewModel> documentViewModels = null;
		//	Exception exception = null;

		//	try
		//	{

		//		TenantUserSession tenantUserSession = null;

		//		if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
		//		//DocumentManagement.DocumentSeeder(tenantUserSession, out exception);

		//		//  TODO: Implement query string based paging to reduce server load.
		//		//	The business layer will be adjusted to take [PageNumber] and [PageRowCount].
		//		//	We will not be using the default paging of the grid views. We will do the filtering in the business layer ourselves.
		//		//	Example: DocumentManagement.GetDocumentsByUser(user.Id, int pageNumber, int pageRowCount, out documEntentities, out exception);
		//		//	Example: DocumentManagement.GetDocumentsByDocumentSearchCriteria(user.Id, documentSearchCriteria, int pageNumber, int pageRowCount, out documEntentities, out exception);

		//		// TODO: Depending on form input, get documents using [DocumentManagement.GetDocumentsByUser] or [DocumentManagement.GetDocumentsByDocumentSearchCriteria].

		//		var pageNumber = GlobalConstants.WebViewPageNumberMinimum;
		//		var pageRowCount = GlobalConstants.WebViewPageRowCountDefault;

		//		if (!int.TryParse(this.Request.QueryString["PageNumber"], out pageNumber)) { pageNumber = GlobalConstants.WebViewPageNumberDefault; }
		//		if (!int.TryParse(this.Request.QueryString["PageRowCount"], out pageRowCount)) { pageRowCount = GlobalConstants.WebViewPageRowCountDefault; }

		//		pageNumber = Math.Max(pageNumber, GlobalConstants.WebViewPageNumberMinimum);
		//		pageRowCount = Math.Min(Math.Max(pageRowCount, GlobalConstants.WebViewPageRowCountMinimum), GlobalConstants.WebViewPageRowCountMaximum);
		//		bool result = DocumentManagement.GetDocumentsByUser(tenantUserSession, tenantUserSession.User.Id, pageNumber, pageRowCount, out documEntentities, out exception);
		//		if (exception != null)
		//		{
		//			throw exception;
		//		}
		//		documEntentities = GetFolderTreeViewData.CleanDocumentList(documEntentities, tenantUserSession);

		//		//if (result)
		//		//    documentViewModels = documenetVMListFiller(documEntentities);
		//		//else
		//		//    documentViewModels = new List<DocumentsViewModel>();
		//		//documentViewModels = documenetVMListFiller(documEntentities);
		//	}
		//	catch (Exception ex)
		//	{
		//		exception = ex;
		//	}
		//	if (!string.IsNullOrEmpty(ErrorMessage))
		//	{
		//		this.ViewBag.ErrorMessage = ErrorMessage;
		//	}
		//	if (exception != null)
		//	{
		//		this.ViewBag.ErrorMessage = exception.Message;
		//	}
		//	if (!string.IsNullOrEmpty(SuccessMessage))
		//	{
		//		this.ViewBag.SuccessMessage = SuccessMessage;
		//	}
		//	return (this.View("~/Views/Tenants/Documents/Documents.cshtml", documEntentities));
		//}

		[HttpGet]
		public string GetDocumentUri(long id)
		{
			return ("");
		}

		//[HttpGet]
		//public Stream GetDocumentAsStream(long id)
		//{

		//    AzureCloudStorageAccountHelper
		//    // Download from Azure.
		//    using (var stream = System.IO.File.Open("", FileMode.Open, FileAccess.Read, FileShare.Read))
		//    {
		//        this.Response.WriteToDownlodableStream(stream);
		//    }

		//    return (null);
		//}

		//[HttpGet]
		//public ActionResult DocumentSearching()
		//{
		//	return View();
		//}

		//     public List<DocumentsViewModel> documenetVMListFiller(List<Document> documents)
		//     {
		//         List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
		//         TenantUserSession tenantUserSession = null;
		//         Exception exception = null;
		//         Template template   = null;
		//         bool result = false;
		//if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }


		//         foreach (var document in documents)
		//         {
		//             DocumentsViewModel dvm = new DocumentsViewModel();

		//             dvm.MoveItem = false;
		//             dvm.Id = document.Id;
		//             if (document.TemplateId != null)
		//             {
		//                 long templateid = (long)document.TemplateId;

		//                 result=ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateid, out template, out exception);
		//                 dvm.TemplateTitle = template.Title;
		//             }
		//             else
		//             {
		//                 dvm.TemplateTitle = "Null";
		//             }

		//             int folderid = (int)document.FolderId;
		//             Folder folder;
		//             result = DocumentManagement.GetFolderById(tenantUserSession,folderid, out folder, out exception);
		//             dvm.FolderName = folder.Name;
		//             dvm.Name = document.Name;
		//             dvm.FileNameClient = document.FileNameClient;
		//             dvm.FileNameServer = document.FileNameServer;
		//             dvm.DateTime = document.DateTime.ToString("dd/MMM/yyyy");
		//             dvm.Cancelled = document.IsCancelled;
		//             dvm.Transit = document.IsInTransit;
		//             dvm.IsPrivate = document.IsPrivate;

		//             //List<DocumentTemplate> listofdoctemplates = new List<DocumentTemplate>();
		//             //foreach (var documenttemplate in document.DocumentTemplates)
		//             //{
		//             //    listofdoctemplates.Add(documenttemplate);
		//             //}
		//             dvm.DocumentTemplates =(List<DocumentTemplate>) document.DocumentTemplates;
		//             documentsVMlist.Add(dvm);
		//         }
		//         return documentsVMlist;
		//     }
		public JsonResult GetModalTreeItems()
		{
			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			bool result = false;
			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

			List<Folder> folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: BusinessLayer.Lookup.FolderResultType.All);
			//var result = folders;//folders.Where(x => x.ParentId == id).Select(x => x).ToList();
			var res2 = folders.Select(x => new
			{
				ID = x.Id,
				Name = x.Name,
				HasChildren = (((folders.Where(y => y.ParentId == x.Id).Select(y => y).FirstOrDefault()) != null) ? (true) : (false)), //((x.ParentId != null) ? (false) : (true)),
				ParentId = x.ParentId
			}).ToList();
			return Json(res2, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllFoldersnameAndId()
		{
			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			bool result = false;
			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

			List<Folder> folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: BusinessLayer.Lookup.FolderResultType.All);
			var res2 = folders.Select(x => new
			{
				ID = x.Id,
				Name = x.Name,
			}).ToList();
			return Json(res2, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Search document by following criteria
		/// </summary>
		/// <param name="DocumentName"></param>
		/// <param name="TemplateName"></param>
		/// <param name="FolderName"></param>
		/// <param name="TagsUser"></param>
		/// <param name="TagsGlobal"></param>
		/// <param name="Content"></param>
		/// <param name="StartDate"></param>
		/// <param name="EndDate"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult DocumentListing(string DocumentName, string TemplateName, string FolderName, string TagsUser, string TagsGlobal, string Content, DateTime? StartDate, DateTime? EndDate)
		{

			// List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
			DocumentSearchCriteria documentsearchcriteria = new DocumentSearchCriteria();
			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			List<Document> founddocuments = null;
			try
			{
				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }



				#region Set value to documentsearchcriteria object

				documentsearchcriteria.Filename = DocumentName;
				documentsearchcriteria.TemplateName = TemplateName;
				documentsearchcriteria.FolderName = FolderName;
				documentsearchcriteria.DateTimeFrom = StartDate;
				documentsearchcriteria.DateTimeUpTo = EndDate;
				documentsearchcriteria.TagsUser = TagsUser;
				documentsearchcriteria.TagsGlobal = TagsGlobal;
				documentsearchcriteria.Content = Content;
				documentsearchcriteria.UserId = tenantUserSession.User.Id;

                #endregion

                founddocuments = DocumentManagement.GetDocumentsByDocumentSearchCriteria(tenantUserSession, tenantUserSession.User.Id, documentsearchcriteria);
				if (founddocuments == null)
				{
					founddocuments = new List<Document>();
				}
				if (exception != null)
				{
					ViewBag.Exception = exception;
				}
				else
				{
					ViewBag.Exception = null;
				}
				founddocuments = GetFolderTreeViewData.CleanDocumentList(founddocuments, tenantUserSession);
			}
			catch (Exception ex)
			{
				ViewBag.Exception = ex.Message;
			}
			var JsonString = JsonConvert.SerializeObject(founddocuments, Formatting.Indented,
											new JsonSerializerSettings
											{
												ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
											});
			return Json(JsonString);
		}

		/// <summary>
		/// Save search criteria
		/// </summary>
		/// <param name="DocumentName"></param>
		/// <param name="TemplateName"></param>
		/// <param name="FolderName"></param>
		/// <param name="TagsUser"></param>
		/// <param name="TagsGlobal"></param>
		/// <param name="Content"></param>
		/// <param name="StartDate"></param>
		/// <param name="EndDate"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult SaveSearchCriteria(string DocumentName, string TemplateName, string FolderName, string TagsUser, string TagsGlobal, string Content, DateTime? StartDate, DateTime? EndDate, string SearchName)
		{
			// List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
			DocumentSearchCriteria documentsearchcriteria = new DocumentSearchCriteria();

			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			bool result = false;

			try
			{
				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

				#region Set value to documentsearchcriteria object
				documentsearchcriteria.Name = SearchName;
				documentsearchcriteria.Filename = DocumentName;
				documentsearchcriteria.TemplateName = TemplateName;
				documentsearchcriteria.FolderName = FolderName;
				documentsearchcriteria.DateTimeFrom = StartDate;
				documentsearchcriteria.DateTimeUpTo = EndDate;
				documentsearchcriteria.TagsUser = TagsUser;
				documentsearchcriteria.TagsGlobal = TagsGlobal;
				documentsearchcriteria.Content = Content;
				documentsearchcriteria.UserId = tenantUserSession.User.Id;

				#endregion

				result = DocumentManagement.AddDocumentSearchCriteria(tenantUserSession, documentsearchcriteria, out exception);

				if (exception != null)
					return Json(exception);
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
			return Json(result.ToString());
		}
		/// <summary>
		/// Bind Previous Search
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public JsonResult BindPreviousSearch()
		{
			bool _result = false;
			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			List<DocumentSearchCriteria> previousSearch = new List<DocumentSearchCriteria>();
			try
			{
				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

				_result = DocumentManagement.GetPreviousSearchByUserId(tenantUserSession, tenantUserSession.User.Id, out previousSearch, out exception);
				if (exception != null)
					return Json(exception);
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}

			return Json(previousSearch, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult PopulatePreviousSearchById(long searchId)
		{
			bool _result = false;
			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			DocumentSearchCriteria previousSearch = new DocumentSearchCriteria();

			try
			{
				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

				_result = DocumentManagement.GetPreviousSearchById(tenantUserSession, tenantUserSession.User.Id, searchId, out previousSearch, out exception);

				if (exception != null)
					return Json(exception);
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
			return Json(previousSearch, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Mark as public
		/// </summary>
		/// <param name="documentid"></param>
		/// <returns></returns>

		public JsonResult MarkAsPublic(long documentid)
		{

			Document document = new Document();

			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			bool result = false;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
			try
			{
				document.IsPrivate = false;
				DocumentManagement.MarkPublic(tenantUserSession, documentid);
			}
			catch (Exception ex)
			{
				return Json(ex.Message, JsonRequestBehavior.AllowGet);
			}

			return Json(result.ToString(), JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Mark as private
		/// </summary>
		/// <param name="documentid"></param>
		/// <returns></returns>
		public JsonResult MarkAsPrivate(long documentid)
		{

			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			bool result = false;
			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

			try
			{
				DocumentManagement.MarkPrivate(tenantUserSession, documentid);
				
			}
			catch (Exception ex)
			{
				return Json(ExceptionUtilities.ExceptionToString(ex), JsonRequestBehavior.AllowGet);
			}

			return Json("true", JsonRequestBehavior.AllowGet);
		}


		public ActionResult CheckoutDocument(long id)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;
			Document document = null;
			// DocumentsViewModel documentViewModel = new DocumentsViewModel();
			bool result = false;
			try
			{
				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
				result = DocumentManagement.CheckoutDoucmentAndMakeNewVersion(tenantUserSession, id, out document, out exception);
				if (exception != null) { throw exception; }
			}
			catch (Exception ex)
			{
				////////this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex); ////
				return RedirectToAction("Index", new { ErrorMessage = ExceptionUtilities.ExceptionToString(ex) });
			}
			return RedirectToAction("Index", "TenantDocumentIndex", new { id = document.Id });
			//////return this.View("~/Views/Tenants/Templates/TemplateDesign.cshtml", model);

			////=-=-=-=-=-=-=-=-=-=-=-=-=-
			////Where to redirect ?
			//return RedirectToAction("Index");
		}

		public JsonResult SetUserRights(List<long> UserList, long Id)
		{
			bool result = false;
			Exception exception = null;
			TenantUserSession tenantUserSession = null;
			try
			{
				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
				result = TenantUserManagement.UserRightsForDocuments(tenantUserSession, Id, UserList, out exception);
				if (!result) { if (exception != null) { throw exception; } }
			}
			catch (Exception ex)
			{
				exception = ex;
				return Json(exception);
			}
			return Json(result);
		}


		public ActionResult GetAllUsersForDocument(long documentId)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;
			bool result = false;
			List<User> users = null;
			try
			{
				if (documentId > 0)
				{
					if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
					result = TenantUserManagement.GetUsers(tenantUserSession, out users, out exception);
					if (exception != null) { throw exception; }
					if (users.Count > 0)
					{
						Document document = null;
						DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
						if (exception != null) { throw exception; }
						List<UserDocument> userDocuments = null;
                       
                        result = DocumentManagement.GetDocumentUsersByDocumentId(tenantUserSession, documentId, out userDocuments, out exception);
						if (exception != null) { throw exception; }
						if (userDocuments.Count > 0)
						{
							List<User> userList = users.ToList();
							users.Clear();

							foreach (var user in userList)
							{

								if (user.Id != document.CheckedOutByUserId)
								{
									foreach (var userTemplate in userDocuments)
									{
										if (userTemplate.User.Id == user.Id)
										{
											user.IsUserSelected = true;
										}
									}
									users.Add(user);
								}


							}
						}
						else
						{
							users = users.Where(x => x.Id != document.CheckedOutByUserId).ToList();
						}
					}
					else
					{
						users = new List<Entities.Tenants.User>();
					}
					if (exception != null) { throw exception; }
				}
				else
				{
					throw (new Exception("Unable to find the following Template"));
				}
			}
			catch (Exception ex)
			{

				this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex);
				return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", null);
			}
			return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", users);
		}

		public JsonResult AddRemoveUser(long documentId, List<long> selectedUsers)
		{
			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			List<User> users = new List<User>();
			if (selectedUsers == null) { selectedUsers = new List<long>(); }
			bool result = false;
			try
			{
				if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
				//if (selectedUsers.Count <= 0)
				//{
				//    throw (new Exception("No Users Selected"));
				//}
				if (documentId > 0)
				{

					foreach (var selectedUserId in selectedUsers)
					{
						User user = null;
						bool dbresult = TenantUserManagement.GetUserById(tenantUserSession, selectedUserId, out user, out exception);
						if (exception != null) { throw exception; }
						users.Add(user);
					}


                    result = DocumentManagement.AddRemoveDocumentUsers(tenantUserSession, documentId, users, out exception);
					if (exception != null) { throw exception; }
					//if (users.Count > 0)
					//{
					//    result = DocumentManagement.AddRemoveDocumentUsers(tenantUserSession, documentId, users, out exception);
					//    if (exception != null) { throw exception; }
					//}
					//else
					//{
					//    throw (new Exception("No Users Found"));
					//}
				}
				else
				{
					throw (new Exception("Unable to find the following discussion"));
				}
			}
			catch (Exception ex)
			{
				return Json(ExceptionUtilities.ExceptionToString(ex));
			}

			var serialized = JsonConvert.SerializeObject(users, Formatting.Indented,
															new JsonSerializerSettings
															{
																ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
															});
			return Json(serialized);
		}


		public JsonResult InviteUser(long documentId, string email)
		{
			bool result = false;
			TenantUserSession tenantUserSession = null;
			Exception exception = null;
			var userObj = new User();
			var user = new User();
			try
			{

				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
				if ((documentId > 0))
				{
					if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrEmpty(email))
					{
                        Document document = null;
                        DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
                        if (document == null) { throw (new Exception("Unable to find the following template")); }
                        if (document.UserId != tenantUserSession.User.Id) { throw (new Exception("You do not have permission to invite users")); }
						result = TenantUserManagement.GetUserByEmail(tenantUserSession, email, out user, out exception);
						if (exception != null) { throw exception; }
						if (user.Id > 0) { throw (new Exception("User already esists.")); }
						//var userName = email;
						var password = "";
						result = TenantUserManagement.InviteUser(tenantUserSession, email, Url.Action("Index", "TenantDocumentIndex", new { id = documentId }), documentId, "Document", out password, out userObj, out exception);
						if (exception != null) { throw exception; }
						if (userObj.Id > 0)
						{
							var AppConfig = System.Configuration.ConfigurationManager.AppSettings;
							string senderName = tenantUserSession.User.NameFull;
							string url = "http://" + (new Uri(tenantUserSession.Tenant.UrlApi).Host) + "/TenantSignIn/AcceptInvite?key=" + userObj.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;
                            string emailBody = EmailTemplateConstants.InviteExternalUserByEmail.Replace("[{RecieverEmail}]", userObj.Email).Replace("[{SenderUsername}]", tenantUserSession.User.NameFull).Replace("[{SenderCompanyName}]", tenantUserSession.Tenant.CompanyName).Replace("[{EnitityType}]", EntityType.Document.ToString()).Replace("[{sharedEntityName}]", document.Name).Replace("[{LinkForInvite}]", url).Replace("[{RecieverEmail}]", userObj.Email).Replace("[{RecieverPassword}]", password);
                            //string emailBody = $"<strong>Hi {recieverName},</strong> <br>You have been invited by {senderName} for a discussion. Please click <a href='{ url }'><strong>here</strong></a> or copy-paste the link in your browser and login with the following credentials:<br>" + url + "<br>UserName:" + userName + "<br>Password:" + password;
                            string subject = "Invitation for Document";
							EmailUtilities emailUtil = new EmailUtilities(userObj.Email.ToString(), AppConfig["EmailFrom"].ToString(), AppConfig["EmailPassword"].ToString(), emailBody, senderName, subject, int.Parse(AppConfig["EmailPort"]), AppConfig["EmailHost"], out exception, Boolean.Parse(AppConfig["IsBodyHtml"]), Boolean.Parse(AppConfig["IsBodyHtml"]));
							if (exception != null) { throw exception; }
							result = emailUtil.SendEmail(out exception);
						}
						else
						{ result = false; }
					}
					else
					{ throw (new Exception("UserName and Email is Required")); }
				}
				else
				{ throw (new Exception("Unable to find the following Template")); }
			}
			catch (Exception ex)
			{
				result = false;
				return Json(ExceptionUtilities.ExceptionToString(ex));
			}
			return Json(result);
		}


	}
}