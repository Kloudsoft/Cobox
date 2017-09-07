using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.Library.Workflow;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HouseOfSynergy.AffinityDms.WebRole.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDocumentIndexController : Controller
    {
        // GET: TenantDocumentIndex
        public ActionResult Index(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            DocumentIndexViewModel documentIndexVM = new DocumentIndexViewModel();
            documentIndexVM.Document = new Document();
            documentIndexVM.DocumentIndexes = new List<DocumentIndex>();
            List<DocumentIndex> documentIndexList = null;
            this.ViewBag.ErrorMessage = "";
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
            try
            {
                if (id > 0)
                {
                    long ID = long.Parse(id.ToString());
                    this.ViewBag.Id = ID;
                    Document document = null;
                    bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, ID, out document, out exception);
                    if (exception != null) { throw exception; }
                    if (document == null) { throw (new Exception("Unable to find the document")); }
                    this.ViewBag.CurrentFolderId = document.FolderId;
                    this.ViewBag.FirstNodeName = "Knowledge";
                    this.ViewBag.FirstNodeUrl = "../../TenantDocumentsFolderWise/Index";
                    this.ViewBag.lastNodeName = "Document Index";
                    this.ViewBag.LastNodeUrl = "../../TenantDocumentIndex/Index?id=" + id;
                    this.ViewBag.lastNodeIdentity = id.ToString();
                    this.ViewBag.LastNodeIdentityUrl = "#";
                    this.ViewBag.isShared = false;
                    this.ViewBag.isPrivate = false;


                    List<Document> parentDocuments = null;
                    bool res = DocumentManagement.GetDocumentsRelated(tenantUserSession, document.Id, out parentDocuments, out exception);
                    if (exception != null) { throw (exception); }
                    this.ViewBag.ParentDocuments = parentDocuments;
                    var documentVersions = new List<Document>();

                    res = DocumentManagement.GetDocumentVersionsByDocumentId(tenantUserSession, ID, out documentVersions, out exception);
                    if (exception != null) { throw exception; }
                    this.ViewBag.DocumentVersions = documentVersions;

                    dbresult = DocumentManagement.GetAllDocumentIndexByDocumentId(tenantUserSession, ID, out documentIndexList, out exception);
                    if (exception != null) { throw exception; }

                    List<SelectListItem> DataTypeSelectListItem = EnumUtilities.GetSelectItemListFromEnum<DocumentIndexDataType>(out exception);
                    if (documentIndexList == null)
                    {
                        documentIndexList = new List<DocumentIndex>();
                        documentIndexVM.Document = new Document();
                        documentIndexVM.DocumentIndexes = new List<DocumentIndex>();
                    }
                    else
                    {
                        documentIndexVM.Document = document;
                        documentIndexVM.DocumentIndexes = documentIndexList;
                    }
                    this.ViewBag.DataTypeSelectListItem = DataTypeSelectListItem;
                    //  GetAllWorkflowScheme();//Bind all workflow scheme
                }
                else
                {
                    return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = "Unable to find the document", SuccessMessage = "" });
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return this.View("~/Views/Tenants/Documents/DocumentIndex.cshtml", documentIndexVM);
        }
        private byte[] ConvertImageToByteArray(Image img)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
            return xByte;
        }
        [HttpPost]
        public JsonResult UpdateVersion(long documentId, string version)
        {
            if (documentId > 0)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                try
                {
                    //if (!Regex.Match(version, @"\d+\.\d+").Success) { throw (new Exception("Version No is Invalid")); }
                    //var versions = version.Split('.');
                    //int VersionMajor =Convert.ToInt16(versions[0]), VersionMinor = Convert.ToInt16(versions[1]);
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    //AuditTrailEntry audittrail = new AuditTrailEntry();
                    //audittrail.EntityType = EntityType.Document;
                    //audittrail.AuditTrailActionType = AuditTrailActionType.UpdateVersion;
                    //audittrail.UserId = tenantUserSession.User.Id;
                    var result = DocumentManagement.UpdateVersion(tenantUserSession, documentId, version, out exception);

                    if (exception != null) { throw exception; }
                }
                catch (Exception ex)
                {
                    return Json(ExceptionUtilities.ExceptionToString(ex));
                }
                return Json("true");
            }
            else
            {
                return Json("Unable to find the following Document");
            }

        }

        [HttpPost]
        public JsonResult Checkin(long documentId)
        {
            if (documentId > 0)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    Document document = null;
                    DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
                    //if (document != null)
                    //{
                    //    //document
                    //    WorkflowEngineHelper wfeHelper = new WorkflowEngineHelper(System.Configuration.ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString, "SimpleWF", document.ProcessInstanceId);

                    //   // wfeHelper.GetAvailableStateNames()
                    //}
                    //AuditTrailEntry audittrail = new AuditTrailEntry();
                    //audittrail.EntityType = EntityType.Document;
                    //audittrail.AuditTrailActionType = AuditTrailActionType.CheckIn;

                    //audittrail.UserId = tenantUserSession.User.Id;

                    DocumentManagement.Checkin(tenantUserSession, documentId);
                }
                catch (Exception ex)
                {
                    return Json(ExceptionUtilities.ExceptionToString(ex));
                }

                return Json("true");
            }
            else
            {
                return Json("Unable to find the following Template");
            }

        }

        [HttpPost]
        public JsonResult SaveDocumentIndexes(string[] documentIndexes, int documentId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            bool result = false;
            try
            {
                JavaScriptSerializer JavaSerialize = new JavaScriptSerializer();
                List<DocumentIndex> documentIndexList = JavaSerialize.Deserialize<List<DocumentIndex>>(documentIndexes[0]);

                DocumentManagement.SaveNewAndDeleteOldDocumentIndex(tenantUserSession, documentId, documentIndexList);

                if (exception != null)
                {
                    throw exception;
                }
                return Json("true");
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return Json(exception.Message);
        }

        public ActionResult CancelCheckout(long? id)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            try
            {
                if (id > 0)
                {

                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }


                    bool result = DocumentManagement.CancelCheckoutDocument(tenantUserSession, (long)id, out exception);
                    if (exception != null) { throw exception; }
                    if (!result)
                    {
                        throw (new Exception("Unable to cancel checkedout document"));
                    }
                }
                else
                {
                    throw (new Exception("Unable to find the following document"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = ex.Message, SuccessMessage = string.Empty });
            }
            return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = string.Empty, SuccessMessage = "Checkedout document was canceled" });
        }
        public ActionResult DocumentIndexReadOnlyView(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            DocumentIndexViewModel documentIndexVM = new DocumentIndexViewModel();
            documentIndexVM.Document = new Document();
            documentIndexVM.DocumentIndexes = new List<DocumentIndex>();
            this.ViewBag.ErrorMessage = "";
            this.ViewBag.IsDocumentReadOnly = true;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
            try
            {
                if (id > 0)
                {
                    Document document = null;
                    bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, (long)id, out document, out exception);
                    if (exception != null) { throw exception; }
                    if (document == null) { throw (new Exception("Unable to find the document")); }
                    documentIndexVM = FillDocumentIndexViewModel(tenantUserSession, document);
                    this.ViewBag.CurrentFolderId = document.FolderId;
                    this.ViewBag.FirstNodeName = "Knowledge";
                    this.ViewBag.FirstNodeUrl = "../../TenantDocumentsFolderWise/Index";
                    this.ViewBag.lastNodeName = "Document Index Readonly";
                    this.ViewBag.LastNodeUrl = "../../TenantDocumentIndex/DocumentIndexReadOnlyView?id=" + id;
                    this.ViewBag.lastNodeIdentity = id.ToString();
                    this.ViewBag.LastNodeIdentityUrl = "#";
                    this.ViewBag.isShared = false;
                    this.ViewBag.isPrivate = false;

                }
                else
                {
                    return RedirectToAction("Index", "TenantDocuments", new { ErrorMessage = "Unable to find the document" });
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return this.View("~/Views/Tenants/Documents/DocumentIndex.cshtml", documentIndexVM);
        }
        public ActionResult DocumentIndexMaxReadOnlyView(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            DocumentIndexViewModel documentIndexVM = new DocumentIndexViewModel();
            documentIndexVM.Document = new Document();
            documentIndexVM.DocumentIndexes = new List<DocumentIndex>();
            this.ViewBag.ErrorMessage = "";
            this.ViewBag.IsDocumentReadOnly = true;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
            try
            {
                if (id > 0)
                {
                    Document document = null;
                    bool dbresult = DocumentManagement.GetMaxVersionDocumentByDocumentId(tenantUserSession, (long)id, out document, out exception);
                    if (exception != null) { throw exception; }
                    if (document == null) { throw (new Exception("Unable to find the document")); }
                    documentIndexVM = FillDocumentIndexViewModel(tenantUserSession, document);
                    this.ViewBag.CurrentFolderId = document.FolderId;
                    this.ViewBag.FirstNodeName = "Knowledge";
                    this.ViewBag.FirstNodeUrl = "../../TenantDocumentsFolderWise/Index";
                    this.ViewBag.lastNodeName = "Document Index Readonly";
                    this.ViewBag.LastNodeUrl = "../../TenantDocumentIndex/DocumentIndexMaxReadOnlyView?id=" + id;
                    this.ViewBag.lastNodeIdentity = id.ToString();
                    this.ViewBag.LastNodeIdentityUrl = "#";
                    this.ViewBag.isShared = false;
                    this.ViewBag.isPrivate = false;

                }
                else
                {
                    return RedirectToAction("Index", "TenantDocuments", new { ErrorMessage = "Unable to find the document" });
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }



            return this.View("~/Views/Tenants/Documents/DocumentIndex.cshtml", documentIndexVM);
        }


        [HttpPost]
        public JsonResult MoveDocumentFolder(long folderId, long documentId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            try
            {
                if (documentId > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    if (!DocumentManagement.MoveDocumentIntoFolder(tenantUserSession, documentId, folderId, out exception)) { if (exception != null) { throw exception; } }
                    return Json(true);

                }
                else
                    throw (new Exception("Unable to find document"));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return Json(ExceptionUtilities.ExceptionToString(exception));
        }



        public JsonResult UpdateDocumentElement(string DocumentElementVM)
        {
            var documentElementViewModels = new List<DocumentElementViewModel>();
            JavaScriptSerializer JavaSerialize = new JavaScriptSerializer();

            documentElementViewModels = JavaSerialize.Deserialize<List<DocumentElementViewModel>>(DocumentElementVM);
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            try
            {
                if (documentElementViewModels.Count > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    foreach (var documentElementViewModel in documentElementViewModels)
                    {
                        if (!DocumentManagement.UpdateDocumentElementText(tenantUserSession, documentElementViewModel.DocumentElementId, documentElementViewModel.DocuemntOcrText, out exception)) { if (exception != null) { throw exception; } }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }
            return Json(result);
        }

        private DocumentIndexViewModel FillDocumentIndexViewModel(TenantUserSession tenantUserSession, Document document)
        {
            DocumentIndexViewModel documentIndexVM = new DocumentIndexViewModel();
            documentIndexVM.Document = new Document();
            documentIndexVM.DocumentIndexes = new List<DocumentIndex>();
            List<DocumentIndex> documentIndexList = null;

            Exception exception = null;
            this.ViewBag.Id = document.Id;
            List<Document> parentDocuments = null;
            Folder folder = null;

            this.ViewBag.FolderTreeViewModel = GetFolderTreeViewData.GetFolderList(tenantUserSession, null, out folder, out exception);
            if (exception != null) { throw (exception); }
            bool res = DocumentManagement.GetDocumentsRelated(tenantUserSession, document.Id, out parentDocuments, out exception);
            if (exception != null) { throw (exception); }
            this.ViewBag.ParentDocuments = parentDocuments;
            var documentVersions = new List<Document>();

            res = DocumentManagement.GetDocumentVersionsByDocumentId(tenantUserSession, document.Id, out documentVersions, out exception);
            if (exception != null) { throw exception; }
            this.ViewBag.DocumentVersions = documentVersions;
            var dbresult = DocumentManagement.GetAllDocumentIndexByDocumentId(tenantUserSession, document.Id, out documentIndexList, out exception);
            if (exception != null) { throw exception; }
            List<SelectListItem> DataTypeSelectListItem = EnumUtilities.GetSelectItemListFromEnum<DocumentIndexDataType>(out exception);
            documentIndexVM.Document = document;
            documentIndexVM.DocumentIndexes = documentIndexList;
            this.ViewBag.DataTypeSelectListItem = DataTypeSelectListItem;
            if (documentIndexList == null)
            {
                documentIndexList = new List<DocumentIndex>();
            }
            var documentElements = new List<DocumentElement>();
            if (!DocumentManagement.GetAllDocumentElementsById(tenantUserSession, document.Id, out documentElements, out exception)) { if (exception != null) { throw exception; } }
            this.ViewBag.DocumentElements = documentElements;
            return documentIndexVM;
        }


        #region Wokflow Implementation
        public void GetAllWorkflowScheme()
        {
            List<string> listworkflowSchemeNames = new List<string>();
            Exception exception = null;
            WorkflowEngineHelper.GetAvailableSchemeNames(ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString, out listworkflowSchemeNames, out exception);
            this.ViewBag.WorkflowList = listworkflowSchemeNames;
        }
        #endregion
    }

}