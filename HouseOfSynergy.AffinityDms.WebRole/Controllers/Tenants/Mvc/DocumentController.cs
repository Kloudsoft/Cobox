using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class DocumentController : Controller
    {
		[HttpGet]
		public ActionResult DocumentListing ()
		{
            try
            {
                //List<Document> documents = DocumentManagement.GetAllDocuments();
                //List<DocumentsViewModel> documentsVMlist = documenetVMListFiller(documents);
                //ViewBag.Exception = null;
                //return View(documentsVMlist);

                Exception exception = null;
                List<Document> documEntentities = null;
                List<DocumentsViewModel> documentViewModels = null;

                TenantUserSession tenantUserSession = null;

				if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                // Define controller action types and validate against user roles.

                // TODO: Implement query string based paging to reduce server load.
                //	The business layer will be adjusted to take [PageNumber] and [PageRowCount].
                //	We will not be using the default paging of the grid views. We will do the filtering in the business layer ourselves.
                //	Example: DocumentManagement.GetDocumentsByUser(user.Id, int pageNumber, int pageRowCount, out documEntentities, out exception);
                //	Example: DocumentManagement.GetDocumentsByDocumentSearchCriteria(user.Id, documentSearchCriteria, int pageNumber, int pageRowCount, out documEntentities, out exception);

                // TODO: Depending on form input, get documents using [DocumentManagement.GetDocumentsByUser] or [DocumentManagement.GetDocumentsByDocumentSearchCriteria].

                var pageNumber = GlobalConstants.WebViewPageNumberMinimum;
                var pageRowCount = GlobalConstants.WebViewPageRowCountDefault;

                if (!int.TryParse(this.Request.QueryString["PageNumber"], out pageNumber)) { pageNumber = GlobalConstants.WebViewPageNumberDefault; }
                if (!int.TryParse(this.Request.QueryString["PageRowCount"], out pageRowCount)) { pageRowCount = GlobalConstants.WebViewPageRowCountDefault; }

                pageNumber = Math.Max(pageNumber, GlobalConstants.WebViewPageNumberMinimum);
                pageRowCount = Math.Min(Math.Max(pageRowCount, GlobalConstants.WebViewPageRowCountMinimum), GlobalConstants.WebViewPageRowCountMaximum);

                if (DocumentManagement.GetDocumentsByUser(tenantUserSession, tenantUserSession.User.Id, pageNumber, pageRowCount, out documEntentities, out exception))
                {
                    documentViewModels = documenetVMListFiller(tenantUserSession, documEntentities);
                }
                else
                {
                    documentViewModels = new List<DocumentsViewModel>();

                    this.ViewBag.Exception = exception;
                }

                documentViewModels = documenetVMListFiller(tenantUserSession, documEntentities);

				return (this.View("~/Views/Tenants/Documents/DocumentListing.cshtml", documentViewModels));
            }
            catch(Exception ex)
            {
                this.ViewBag.Exception = ex;
                return RedirectToAction("","");
            }
		}

        #region old code
        //[HttpPost]
        //public ActionResult DocumentListing(string DocumentName,string TemplateName,string FolderName,string TagsUser,string TagsGlobal, string Content, DateTime? StartDate,DateTime? EndDate)
        //{
        //    //string res = ElementManagement.UpdateTemplateFinalizeAndStatus(templateListarray);
        //    //Message = res;
        //    ////ViewBag.Message

        //    DocumentSearchCriteria documentsearchcriteria = new DocumentSearchCriteria();
        //    documentsearchcriteria.Name = DocumentName;
        //    documentsearchcriteria.TemplateName = TemplateName;
        //    documentsearchcriteria.FolderName = FolderName;
        //    documentsearchcriteria.DateTimeFrom = StartDate;
        //    documentsearchcriteria.DateTimeUpTo = EndDate;
        //    documentsearchcriteria.TagsUser = TagsUser;
        //    documentsearchcriteria.TagsGlobal = TagsGlobal;
        //    documentsearchcriteria.Content = Content;
        //    Exception exception;
        //    bool result = DocumentManagement.AddDocumentSearchCriteria(documentsearchcriteria,out exception);
        //    List<Document> documents = DocumentManagement.GetAllDocuments();
        //    List<DocumentsViewModel> documentsVMlist = documenetVMListFiller(documents);
        //    if ((exception == null) && result)
        //    {
        //        if (!(DocumentName == string.Empty || DocumentName == "" || DocumentName == null))
        //        {
        //            documentsVMlist = documentsVMlist.Where(x => x.FileNameClient.ToLower().Contains(DocumentName.ToLower())).Select(x => x).ToList();
        //        }
        //        if (!(TemplateName == string.Empty || TemplateName == "" || TemplateName == null))
        //        {
        //            documentsVMlist = documentsVMlist.Where(x => x.TemplateTitle.ToLower().Contains(TemplateName.ToLower())).Select(x => x).ToList();
        //        }
        //        if (!(FolderName == string.Empty || FolderName == "" || FolderName == null))
        //        {
        //            documentsVMlist = documentsVMlist.Where(x => x.FolderName.ToLower().Contains(FolderName.ToLower())).Select(x => x).ToList();
        //        }
        //        if (StartDate != null && EndDate != null)
        //        {
        //            if (StartDate < EndDate)
        //            {
        //                documentsVMlist = documentsVMlist.Where(x => (DateTime.Parse(x.DateTime) >= StartDate) && (DateTime.Parse(x.DateTime) <= EndDate)).Select(x => x).ToList();
        //            }
        //            else
        //            {
        //                documentsVMlist = documentsVMlist.Where(x => (DateTime.Parse(x.DateTime) >= EndDate) && (DateTime.Parse(x.DateTime) <= StartDate)).Select(x => x).ToList();
        //            }
        //        }
        //        else if (StartDate != null)
        //        {
        //            documentsVMlist = documentsVMlist.Where(x => DateTime.Parse(x.DateTime) >= StartDate).Select(x => x).ToList();
        //        }
        //        else if (EndDate != null)
        //        {
        //            documentsVMlist = documentsVMlist.Where(x => DateTime.Parse(x.DateTime) <= EndDate).Select(x => x).ToList();
        //        }

        //        return View(documentsVMlist);
        //    }
        //    if (exception != null)
        //    {
        //        ViewBag.Exception = exception;
        //    }
        //    else {
        //        ViewBag.Exception = null;
        //    }
        //    return View(documentsVMlist);


        //}
        #endregion

        [HttpGet]
        public ActionResult DocumentSearching()
        { 
            return View();
        }

        public JsonResult Folder_Read(TenantUserSession tenantUserSession, int? id)
        {
            List<Folder> folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: BusinessLayer.Lookup.FolderResultType.All);
			var result = folders.Where(x => x.ParentId == id).Select(x => x).ToList();
            var res2 = result.Select(x => new
            {
                ID = x.Id,
                Name = x.Name,
                HasChildren = (((folders.Where(y => y.ParentId == x.Id).Select(y => y).FirstOrDefault()) != null) ? (true) : (false)), //((x.ParentId != null) ? (false) : (true)),
                ParentId = x.ParentId
            }).ToList();
            //List<FolderTreeViewModel> res2 = GetTreeData(id);
            return Json(res2, JsonRequestBehavior.AllowGet);
        }
      

        public List<DocumentsViewModel> documenetVMListFiller(TenantUserSession tenantUserSession, List<Document> documents)
        {
            List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
            foreach (var document in documents)
            {
                Exception exception = null;
                DocumentsViewModel dvm = new DocumentsViewModel();
                dvm.MoveItem = false;
                dvm.Id = document.Id;
                if (document.TemplateId != null)
                {
                    long templateid = (long)document.TemplateId;
                    Template template = null;

                    bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateid, out template, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    dvm.TemplateTitle = template.Title;
                }
                else {
                    dvm.TemplateTitle = "Null";
                }
                
                int folderid = (int)document.FolderId;

				Folder folder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: folderid, folderIdType: FolderIdType.Id, documentId: null, folderResultType: FolderResultType.Exact).FirstOrDefault();
				if (folder == null) { throw (new Exception("Unable to find the following folder")); }
				dvm.FolderName = folder.Name;
				dvm.Name = document.Name;
				dvm.FileNameClient = document.FileNameClient;
                dvm.FileNameServer = document.FileNameServer;
                dvm.DateTime = document.DateTime.ToString("dd/MMM/yyyy");
                dvm.Cancelled = document.IsCancelled;
                dvm.Transit = document.IsInTransit;
                dvm.IsPrivate = document.IsPrivate;
                //commentout for transfer one source to another source  ,its need to be fixed
                //List<Models.DocumentTemplate> listofdoctemplates = new List<Models.DocumentTemplate>();
                //foreach (var documenttemplate in document.DocumentTemplates)
                //{
                //    listofdoctemplates.Add(TryConverting.EntiesDocumentTemplateToModelDocumentTemplate(documenttemplate));
                //}
                //dvm.DocumentTemplates = listofdoctemplates;
                documentsVMlist.Add(dvm);
            }
            return documentsVMlist;
        }
		public JsonResult GetModalTreeItems (TenantUserSession tenantUserSession)
		{
			List<Folder> folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: FolderResultType.All);
			//var result = folders;//folders.Where(x => x.ParentId == id).Select(x => x).ToList();
			var res2 = folders.Select(x => new
            {
                ID = x.Id,
                Name = x.Name,
                HasChildren = (((folders.Where(y => y.ParentId == x.Id).Select(y => y).FirstOrDefault()) != null) ? (true) : (false)), //((x.ParentId != null) ? (false) : (true)),
                ParentId = x.ParentId
            }).ToList();
            return Json(res2,JsonRequestBehavior.AllowGet);
        }
        public ActionResult DocumentDiscourseViewer(long? id)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Document document = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                if (id == null || id <= 0) { throw (new Exception("Unable to find the following document")); }
                DocumentManagement.GetDocumentDiscourseById(tenantUserSession, id,out document, out exception);
                if (exception != null) { throw exception; }
                if (document == null) { throw (new Exception("Unable to find the following document.")); }
                this.ViewBag.CurrentFolderId = document.FolderId;
                this.ViewBag.FirstNodeName = "Knowledge";
                this.ViewBag.FirstNodeUrl = "../../TenantDocumentsFolderWise/Index";
                this.ViewBag.lastNodeName = "Document Chat Viewer";
                this.ViewBag.LastNodeUrl = "../../Document/DocumentDiscourseViewer?id=" + id;
                this.ViewBag.lastNodeIdentity = id.ToString();
                this.ViewBag.LastNodeIdentityUrl = "#";
                this.ViewBag.isShared = false;
                this.ViewBag.isPrivate = false;
            }
            catch (Exception ex)
            {
                var exStr = (ex.InnerException!=null)?(ex.InnerException.Message):(ex.Message);
                this.ViewBag.ErrorMessage = exStr;
            }
            return View ("~/Views/Tenants/Documents/DocumentDiscourseViewer.cshtml", document);
        }

        
        public ActionResult GetDocumentStatus(long id)
        {
            //DocumentStatusViewModel
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<Document> documentVersions = new List<Document>();
            DocumentStatusViewModel documentStatusViewMOdel = new DocumentStatusViewModel();
            documentStatusViewMOdel.Indexes = new List<DocumentElement>();
            documentStatusViewMOdel.SharedWith = new List<Entities.Tenants.User>();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                bool result = DocumentManagement.GetDocumentVersionsByDocumentId(tenantUserSession,id,  out documentVersions, out exception);
                if (exception != null) { throw exception; }

                if (documentVersions != null) {
                    documentStatusViewMOdel.CreatedBy = documentVersions.First().User;
                    documentStatusViewMOdel.CreatedOn = documentVersions.First().DateTime;
                    foreach (var documentElement in documentVersions.Last().DocumentElements.ToList())
                    {
                        documentStatusViewMOdel.Indexes.Add(documentElement);
                    }
                    documentStatusViewMOdel.LastEdited = documentVersions.Last().CheckedOutDateTime;
                    foreach (var user in documentVersions.Last().DocumentUsers)
                    {
                        documentStatusViewMOdel.SharedWith.Add(user.User);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return PartialView("~/Views/Tenants/Documents/_DocumentStatus.cshtml", documentStatusViewMOdel);
        }


        #region Uzma Hashmi
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
		public JsonResult DocumentListing (string DocumentName, string TemplateName, string FolderName, string TagsUser, string TagsGlobal, string Content, DateTime? StartDate, DateTime? EndDate)
        {

            List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
            DocumentSearchCriteria documentsearchcriteria = new DocumentSearchCriteria();

			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            long _userId = 1;

            if (System.Web.HttpContext.Current.Request.Cookies["UserId"] != null)
            {
                _userId = long.Parse(System.Web.HttpContext.Current.Request.Cookies["UserId"].Value);
            
                List<Document> founddocuments = null;

                #region Set value to documentsearchcriteria object

                documentsearchcriteria.Filename = DocumentName;
                documentsearchcriteria.TemplateName = TemplateName;
                documentsearchcriteria.FolderName = FolderName;
                documentsearchcriteria.DateTimeFrom = StartDate;
                documentsearchcriteria.DateTimeUpTo = EndDate;
                documentsearchcriteria.TagsUser = TagsUser;
                documentsearchcriteria.TagsGlobal = TagsGlobal;
                documentsearchcriteria.Content = Content;
                documentsearchcriteria.UserId = _userId;

                #endregion
                try
                {
                    founddocuments = DocumentManagement.GetDocumentsByDocumentSearchCriteria(tenantUserSession, _userId, documentsearchcriteria);
                }
                catch (Exception ex)
                {
                    this.ViewBag.Exception = exception;
                }
                //bool rtnresult = DocumentManagement.GetDocumentsByDocumentSearchCriteria(tenantUserSession, _userId, documentsearchcriteria, out founddocuments, out exception);
                if (founddocuments != null)
                {

                    documentsVMlist = documenetVMListFiller(tenantUserSession, founddocuments);
                    //Models.DocumentTemplate dots = new Models.DocumentTemplate();
                    //dots.Confidence = 98;
                    //dots.DocumentId = 1;
                    //dots.Id = 1;
                    //dots.TemplateId = 1;
                    //documentsVMlist[0].DocumentTemplates.Add(dots);
                    //dots = new Models.DocumentTemplate();
                    //dots.Confidence = 98;
                    //dots.DocumentId = 2;
                    //dots.Id = 1;
                    //dots.TemplateId = 2;
                    //documentsVMlist[0].DocumentTemplates.Add(dots);

                }
                else
                {
                    documentsVMlist = new List<DocumentsViewModel>();
                }
                return Json(documentsVMlist);
            }
            else {
                return Json("Login Failed");
            }
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
        public JsonResult SaveSearchCriteria(string DocumentName, string TemplateName, string FolderName, string TagsUser, string TagsGlobal, string Content, DateTime? StartDate, DateTime? EndDate,string SearchName)
        {
            List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
            DocumentSearchCriteria documentsearchcriteria = new DocumentSearchCriteria();

			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            long _userId = 1;

            if (System.Web.HttpContext.Current.Request.Cookies["UserId"] != null)
            {
                _userId = long.Parse(System.Web.HttpContext.Current.Request.Cookies["UserId"].Value);

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
              documentsearchcriteria.UserId = _userId;

              #endregion

			  bool result = DocumentManagement.AddDocumentSearchCriteria(tenantUserSession, documentsearchcriteria, out exception);

              if (exception != null)
                  return Json(exception);

              return Json(result.ToString());
          }
          else
          {
              return Json("Login Failed");
          }
        }
        
        [HttpGet]
        public JsonResult BindPreviousSearch()
        {
            long _userId = 1;
            bool _result = false;

			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            if (System.Web.HttpContext.Current.Request.Cookies["UserId"] != null)
            {
                _userId = long.Parse(System.Web.HttpContext.Current.Request.Cookies["UserId"].Value);
                
                List<DocumentSearchCriteria> previousSearch = new List<DocumentSearchCriteria>();

				_result = DocumentManagement.GetPreviousSearchByUserId(tenantUserSession, _userId, out previousSearch, out exception);


                return Json(previousSearch, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Login Failed");
            }
        }

        [HttpGet]
        public JsonResult PopulatePreviousSearchById(long searchId)
        {
            long _userId = 1;
            bool _result = false;

			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            if (System.Web.HttpContext.Current.Request.Cookies["UserId"] != null)
            {
                _userId = long.Parse(System.Web.HttpContext.Current.Request.Cookies["UserId"].Value);
                DocumentSearchCriteria previousSearch = new DocumentSearchCriteria();


				_result = DocumentManagement.GetPreviousSearchById(tenantUserSession, _userId, searchId, out previousSearch, out exception);

                return Json(previousSearch, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Login Failed");
            }
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
		#endregion
	}
}












//[HttpGet]
//public ActionResult DocumentListing()
//{
//    List<Document> documents = DocumentManagement.GetAllDocuments();

//    List<DocumentsViewModel> documentsVMlist = documenetVMListFiller(documents);
//    List<Folder> folders = DocumentManagement.GetAllFolders();
//    var result = folders.Where(x => x.ParentId == null).Select(x => x).ToList();
//    List<FolderTreeViewModel> res2 = result.Select(x => new FolderTreeViewModel
//    {
//        ID = x.Id,
//        Name = x.Name,
//        HasChildren = (((folders.Where(y => y.ParentId == x.Id).Select(y => y).FirstOrDefault()) != null) ? (true) : (false)), //((x.ParentId != null) ? (false) : (true)),
//        ParentId = x.ParentId
//    }).ToList();
//    ViewBag.FoldersList = res2;
//    return View(documentsVMlist);
//}


//[HttpPost]
//public JsonResult DocumentListing(List<List<string>> documentMovingListarray)
//{
//    //string res = ElementManagement.UpdateTemplateFinalizeAndStatus(templateListarray);
//    //Message = res;
//     ////ViewBag.Message
//    string res = DocumentManagement.MoveDocumentsToFolder(documentMovingListarray);
//    return Json(res);
//}







//public JsonResult AddFolder(TenantUserSession tenantUserSession, string name, long id)
//{
//    List<Folder> folders = DocumentManagement.GetAllFolders(tenantUserSession);
//     Folder folder = new Folder();
//     folder.ParentId= id;
//     folder.Name = name;
//     folder.FolderType = 0;
//     string res = DocumentManagement.AddFolder(tenantUserSession, folder);

//     return Json(res); 
//}
//     public JsonResult GetDocumentsByFolderId(TenantUserSession tenantUserSession, string strfolderid)
//     {
//         long folderId = long.Parse(strfolderid);
//         List<Document> documents = DocumentManagement.GetAllDocumentsByFolderId(tenantUserSession, folderId);
//List<DocumentsViewModel> documentsVMlist = documenetVMListFiller(tenantUserSession, documents);
//         return Json(documentsVMlist);
//     }


//public JsonResult GetAllFoldersnameAndId (TenantUserSession tenantUserSession)
//{
//          List<Folder> folders = DocumentManagement.GetAllFolders(tenantUserSession);
//          var res2 = folders.Select(x => new
//          {
//              ID = x.Id,
//              Name = x.Name,
//          }).ToList();
//          return Json(res2, JsonRequestBehavior.AllowGet);
//      }




//List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
//foreach (var document in documents)
//{
//    DocumentsViewModel dvm = new DocumentsViewModel();
//    dvm.MoveItem = false;
//    dvm.Id = document.Id;
//    long templateid =(long) document.TemplateId;
//    dvm.TemplateTitle = ElementManagement.GetTemplateByTemplateId(templateid).Title;
//    int folderid = (int)document.FolderId;
//    dvm.FolderName = DocumentManagement.GetFolderById(folderid).Name;
//    dvm.FileNameClient = document.FileNameClient;
//    dvm.FileNameServer = document.FileNameServer;
//    dvm.DateTime = document.DateTime.ToLongDateString();
//    documentsVMlist.Add(dvm);
//}



//public void CreateTree(List<Folder> folders)
//{
//    //List<FolderTreeVM> FolderTreeVMTemp = new List<FolderTreeVM>();
//    //foreach (var folder in folders)
//    //{
//    //    FolderTreeVM foldertreevm = new FolderTreeVM();
//    //    foldertreevm.Name = folder.Name;
//    //    foldertreevm.ID = folder.Id;
//    //    foldertreevm.ParentId = folder.ParentId;
//    //    List<Folder> children = folders.Where(y => y.ParentId == folder.Id).Select(y => y).ToList();
//    //    if(children.Count>0)
//    //    {
//    //        foldertreevm.HasChildren = true;
//    //    }
//    //    else
//    //    {
//    //        foldertreevm.HasChildren = false;
//    //    }
//    //    List<FolderTreeVM> ListOfChildren = new List<FolderTreeVM>();
//    //    foreach (var child in children)
//    //    { 
//    //       FolderTreeVM foldertreevmchild = new FolderTreeVM();
//    //    }

//    //}
//}

//public List<FolderTreeViewModel> GetTreeData(long? id)
//{

//    return res2;
//}














//// GET: Document
//public ActionResult Index()
//{
//    return View();
//}

//// GET: Document/Details/5
//public ActionResult Details(int id)
//{
//    return View();
//}

//// GET: Document/Create
//public ActionResult Create()
//{
//    return View();
//}

//// POST: Document/Create
//[HttpPost]
//public ActionResult Create(FormCollection collection)
//{
//    try
//    {
//        // TODO: Add insert logic here

//        return RedirectToAction("Index");
//    }
//    catch
//    {
//        return View();
//    }
//}

//// GET: Document/Edit/5
//public ActionResult Edit(int id)
//{
//    return View();
//}

//// POST: Document/Edit/5
//[HttpPost]
//public ActionResult Edit(int id, FormCollection collection)
//{
//    try
//    {
//        // TODO: Add update logic here

//        return RedirectToAction("Index");
//    }
//    catch
//    {
//        return View();
//    }
//}

//// GET: Document/Delete/5
//public ActionResult Delete(int id)
//{
//    return View();
//}

//// POST: Document/Delete/5
//[HttpPost]
//public ActionResult Delete(int id, FormCollection collection)
//{
//    try
//    {
//        // TODO: Add delete logic here

//        return RedirectToAction("Index");
//    }
//    catch
//    {
//        return View();
//    }
//}