using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.CustomKendo;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Utility;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using Newtonsoft.Json;
using OptimaJet.Workflow.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.WebRole.Classes.CustomHelpers;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDocumentsFolderWiseController : Controller
    {
        //    // GET: TenantDocumentsFolderWise
        //    public ActionResult Index(string ErrorMessage, string SuccessMessage)
        //    {
        //        Exception exception = null;
        //        this.ViewBag.ErrorMessage = string.Empty;
        //        this.ViewBag.SuccessMessage = string.Empty;
        //        // List<Folder> folders = null;
        //        Folder parentFolder = null;
        //        TenantUserSession tenantUserSession = null;
        //        DocumentFolderTreeViewModel documentFolderTreeViewModel = new DocumentFolderTreeViewModel();
        //        try
        //        {
        //            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
        //            List<FolderTreeViewModel> FolderTreeVMListWhenParentIdIsNull = GetFolderTreeViewData.GetFolderList(tenantUserSession, null, out parentFolder, out exception);
        //if (exception != null) { throw exception; }
        //            if (parentFolder != null)
        //            {
        //                documentFolderTreeViewModel.Parentfolder = parentFolder;
        //                this.ViewBag.FolderTreeVM = FolderTreeVMListWhenParentIdIsNull;
        //                if (parentFolder.Folders.Count > 0)
        //                {
        //                    documentFolderTreeViewModel.ChildFolders = parentFolder.Folders.ToList();
        //                }
        //                else
        //                {
        //                    documentFolderTreeViewModel.ChildFolders = new List<Folder>();
        //                }
        //                if (parentFolder.Documents.Count > 0)
        //                {
        //                    documentFolderTreeViewModel.ParentFolderDocuments = parentFolder.Documents.ToList();
        //                }
        //                else
        //                {
        //                    documentFolderTreeViewModel.ParentFolderDocuments = new List<Document>();
        //                }
        //            }
        //            else
        //            {
        //                documentFolderTreeViewModel.ChildFolders = new List<Folder>();
        //                documentFolderTreeViewModel.Parentfolder = new Folder();
        //                documentFolderTreeViewModel.ParentFolderDocuments = new List<Document>();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            exception = ex;
        //            this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
        //        }

        //        if (!(string.IsNullOrEmpty(ErrorMessage)))
        //        {
        //            this.ViewBag.ErrorMessage = ErrorMessage;
        //        }

        //        if (!(string.IsNullOrEmpty(SuccessMessage)))
        //        {
        //            this.ViewBag.SuccessMessage = SuccessMessage;
        //        }


        //        documentFolderTreeViewModel = GetFolderTreeViewData.CleanDocumentTreeViewModalData(documentFolderTreeViewModel, tenantUserSession);
        //        return View("~/Views/Tenants/Documents/DocumentsFolderWiseNew.cshtml", documentFolderTreeViewModel);
        //    }

        // GET: TenantDocumentsFolderWise
        public ActionResult Index(string ErrorMessage, string SuccessMessage, long? parentFolderId = null, long? subfolderId = null)
        {
            Exception exception = null;
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            TenantUserSession tenantUserSession = null;
            DocumentFolderTreeViewModel documentFolderTreeViewModel = new DocumentFolderTreeViewModel();
            if (parentFolderId != null)
            {
                this.ViewBag.ParentFolderId = parentFolderId;
                if (subfolderId != null)
                {
                    this.ViewBag.SubfolderId = subfolderId;
                }
            }
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
                List<Folder> folders = null;
                FolderManagement.GetRootFolderChildrens(tenantUserSession: tenantUserSession, folders: out folders, exception: out exception);
                if (exception != null) { throw exception; }
                //List<FolderTreeViewModel> folderTreeViewModel = new List<FolderTreeViewModel>();// = GetFolderTreeViewData.GetFolderList(tenantUserSession, null, out parentFolder, out exception);
                documentFolderTreeViewModel.Parentfolder = new Folder();
                documentFolderTreeViewModel.ChildFolders = folders;
                documentFolderTreeViewModel.ParentFolderDocuments = new List<Document>();

            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }

            if (!(string.IsNullOrEmpty(ErrorMessage)))
            {
                this.ViewBag.ErrorMessage = ErrorMessage;
            }

            if (!(string.IsNullOrEmpty(SuccessMessage)))
            {
                this.ViewBag.SuccessMessage = SuccessMessage;
            }


            //documentFolderTreeViewModel = GetFolderTreeViewData.CleanDocumentTreeViewModalData(documentFolderTreeViewModel, tenantUserSession);
            return View("~/Views/Tenants/Documents/DocumentsFolderWiseNew.cshtml", documentFolderTreeViewModel);
        }


        public JsonResult GetRootFolder(TenantUserSession tenantUserSession)
        {
            FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: FolderIdType.ParentId, documentId: null, folderResultType: FolderResultType.Root);
            return Json(null);
        }

        //public ActionResult GetDynamicTreeview(long? folderId, TreeViewInformation kendoTreeViewInformation)
        //{
        //	Kendo.Mvc.UI.TreeViewItem tvi = new Kendo.Mvc.UI.TreeViewItem();
        //	//kendoTreeViewInformation = new TreeViewInformation();
        //	TenantUserSession tenantUserSession = null;
        //	Exception exception = null;
        //	if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
        //	kendoTreeViewInformation.Data = GetFolderTreeViewData.GetFolderTreeviewHieraricalList(tenantUserSession: tenantUserSession, folderId: folderId, folderIdType: FolderIdType.ParentId, folderResultType: FolderResultType.All, exception: out exception);

        //	return PartialView("~/Views/Tenants/Folders/_DynamicKendoTreeview.cshtml", kendoTreeViewInformation);
        //}
        public ActionResult GetHieraricalTreeViewData(long? folderId)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
            List<FolderTreeViewModel> FolderTreeVMList = GetFolderTreeViewData.GetFolderTreeviewHieraricalList(tenantUserSession: tenantUserSession, folderId: folderId, folderIdType: FolderIdType.ParentId, folderResultType: FolderResultType.All, exception: out exception);
            return Json(FolderTreeVMList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetmovedocumenttoDraftmanually(long id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            long DraftFolderId = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);
            long SupportingFolder = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderSupporting"]);

            Folder folder = new Folder();
            Folder Isfolder = new Folder();
            Document sourcedocument = new Document();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
            bool dbresult = false;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {

                sourcedocument = context.Documents.Where(x => x.Id == id).First(); 

                folder = context.Folders.Where(x => x.Id == sourcedocument.FolderId).First();

                Isfolder = context.Folders.Where(x => x.ParentId == DraftFolderId && x.Name == folder.Name).First();
                if (Isfolder != null)
                {
                    //
                    var Docs = context.Documents.Where(x => x.DocumentOriginalId == sourcedocument.DocumentOriginalId).ToList();
                    
                        Docs.ForEach(a => a.FolderId = Isfolder.Id);
                        Docs.ForEach(a => a.WorkflowState = DocumentWorkflowState.Draft);
                        context.SaveChanges();
                }
            }


            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            DocumentFolderTreeViewModel documentFolderTreeViewModel = new DocumentFolderTreeViewModel();
           
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
                List<Folder> folders = null;
                FolderManagement.GetRootFolderChildrens(tenantUserSession: tenantUserSession, folders: out folders, exception: out exception);
                if (exception != null) { throw exception; }
                //List<FolderTreeViewModel> folderTreeViewModel = new List<FolderTreeViewModel>();// = GetFolderTreeViewData.GetFolderList(tenantUserSession, null, out parentFolder, out exception);
                documentFolderTreeViewModel.Parentfolder = new Folder();
                documentFolderTreeViewModel.ChildFolders = folders;
                documentFolderTreeViewModel.ParentFolderDocuments = new List<Document>();

            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            
            //documentFolderTreeViewModel = GetFolderTreeViewData.CleanDocumentTreeViewModalData(documentFolderTreeViewModel, tenantUserSession);
            return View("~/Views/Tenants/Documents/DocumentsFolderWiseNew.cshtml", documentFolderTreeViewModel);
            
        }






        public ActionResult GetMoveFolderHieraricalTreeViewData(long? folderId)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
            //List<FolderTreeViewModel> FolderTreeVMList = GetFolderTreeViewData.GetFolderTreeviewHieraricalList(tenantUserSession: tenantUserSession, folderId: folderId, folderIdType: FolderIdType.ParentId, folderResultType: FolderResultType.All, exception: out exception);
            List<FolderTreeViewModel> FolderTreeVMList = GetFolderTreeViewData.GetFolderTreeviewHieraricalList(tenantUserSession: tenantUserSession, folderId: folderId, folderIdType: FolderIdType.ParentId, folderResultType: FolderResultType.All, exception: out exception, skipSharedFolder: true, skipPrivateFolder: true);
            return Json(FolderTreeVMList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Folder_Read(long? id)
        {
            Exception exception = null;
            Folder parentFolder = null;

            TenantUserSession tenantUserSession = null;
            List<FolderTreeViewModel> FolderTreeVMList = null;

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }


                FolderTreeVMList = GetFolderTreeViewData.GetFolderList(tenantUserSession, id, out parentFolder, out exception);

                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                return Json($"Exception: {ExceptionUtilities.ExceptionToString(ex)}", JsonRequestBehavior.AllowGet);
            }
            return Json(FolderTreeVMList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocumentsFoldersSearchResult(string QueryDocument, long? folderId)
        {
            List<DocumentsViewModel> documentsVMlist = new List<DocumentsViewModel>();
            
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            if (folderId <= 0) { folderId = null; }
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
            DocumentSearchCriteria documentsearchcriteria = new DocumentSearchCriteria();
            if (folderId.HasValue)
            {
                Folder folder = null;
                FolderManagement.GetFolderById(tenantUserSession, (long)folderId, out folder, out exception);
                if (exception != null) { throw (exception); }
                documentsearchcriteria.FolderName = folder.Name;
                this.ViewBag.FolderId = folder.Id;
            }
            documentsearchcriteria.Filename = QueryDocument;
            List<Document> founddocuments = new List<Document>();
            founddocuments = DocumentManagement.GetDocumentsByDocumentSearchCriteria(tenantUserSession, tenantUserSession.User.Id, documentsearchcriteria);
            return View("~/Views/Tenants/Documents/DocumentsFoldersSearchResult.cshtml", founddocuments);
        }

        public ActionResult GetFolderBreadcrumb(long? currentFolderId, string firstNodeName, string firstNodeUrl, string lastNodeName, string lastNodeUrl, string lastNodeIdentity, string lastNodeIdentityUrl, bool isSharedFolder = false, bool isPrivateFolder = false)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                var folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: FolderResultType.All);
                string breadcrumbStr = CustomHtmlHelpers.GetFolderBreadcrumb(folders, "../../TenantDocumentsFolderWise/Index", currentFolderId, firstNodeName, firstNodeUrl, lastNodeName, lastNodeUrl, lastNodeIdentity, lastNodeIdentityUrl, isSharedFolder, isPrivateFolder);
                return (this.PartialView("~/Views/Tenants/_Breadcrumbs.cshtml", breadcrumbStr));
            }
            catch (Exception ex)
            {
                return (this.PartialView("~/Views/Tenants/_Breadcrumbs.cshtml", ExceptionUtilities.ExceptionToString(ex)));

            }
        }

        public ActionResult GetFolderTreeView(long? id)
        {
            Exception exception = null;
            Folder parentFolder = null;
            TenantUserSession tenantUserSession = null;
            List<FolderTreeViewModel> FolderTreeVMList = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }


                FolderTreeVMList = GetFolderTreeViewData.GetFolderList(tenantUserSession, id, out parentFolder, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                return Json("An Exception Occured", JsonRequestBehavior.AllowGet);
            }
            return PartialView("~/Views/Tenants/Documents/_FolderTreeViewModal.cshtml", FolderTreeVMList);
        }

        [HttpPost]
        public JsonResult GetSelectedFolderDetails(long folderid)
        {
            Exception exception = null;
            Folder parentFolder = null;
            TenantUserSession tenantUserSession = null;
            DocumentFolderTreeViewModel documentFolderTreeViewModel = new DocumentFolderTreeViewModel();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }


                List<FolderTreeViewModel> FolderTreeVMListWhenParentIdIsNull = GetFolderTreeViewData.GetFolderList(tenantUserSession, folderid, out parentFolder, out exception);
                if (exception != null) { throw exception; }
                if (parentFolder != null)
                {
                    documentFolderTreeViewModel.Parentfolder = parentFolder;
                    this.ViewBag.FolderTreeVM = FolderTreeVMListWhenParentIdIsNull;
                    if (parentFolder.Folders.Count > 0)
                    {
                        documentFolderTreeViewModel.ChildFolders = parentFolder.Folders.ToList();
                    }
                    else
                    {
                        documentFolderTreeViewModel.ChildFolders = new List<Folder>();
                    }
                    if (parentFolder.Documents.Count > 0)
                    {
                        documentFolderTreeViewModel.ParentFolderDocuments = parentFolder.Documents.ToList();
                    }
                    else
                    {
                        documentFolderTreeViewModel.ParentFolderDocuments = new List<Document>();
                    }
                }
                else
                {
                    documentFolderTreeViewModel.ChildFolders = new List<Folder>();
                    documentFolderTreeViewModel.Parentfolder = new Folder();
                    documentFolderTreeViewModel.ParentFolderDocuments = new List<Document>();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            documentFolderTreeViewModel = GetFolderTreeViewData.CleanDocumentTreeViewModalData(documentFolderTreeViewModel, tenantUserSession);

            string jsonDocumentFolderTreeViewModel = JsonConvert.SerializeObject(documentFolderTreeViewModel, Formatting.Indented,
                                                                new JsonSerializerSettings
                                                                {
                                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                                });
            return Json(jsonDocumentFolderTreeViewModel);
        }
        public PartialViewResult GetSelectedFolderDocuments(long folderid)
        {
            Exception exception = null;
            Folder parentFolder = null;
            TenantUserSession tenantUserSession = null;
            DocumentFolderTreeViewModel documentFolderTreeViewModel = new DocumentFolderTreeViewModel();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
                List<FolderTreeViewModel> FolderTreeVMListWhenParentIdIsNull = GetFolderTreeViewData.GetFolderList(tenantUserSession, folderid, out parentFolder, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return PartialView("~/Views/Tenants/Documents/_DocumentListGridView.cshtml", parentFolder.Documents.ToList());
        }
        [HttpPost]
        public JsonResult AddFolder(long parentFolderId, string folderName)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Folder newFolderCreated = null;
            try
            {

                if (parentFolderId > 1)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    Folder folder = new Folder();
                    folder.DateTimeCreated = DateTime.UtcNow;
                    folder.DepartmentId = null;
                    folder.ParentId = parentFolderId;
                    folder.Name = folderName;
                    folder.FolderType = FolderType.Child;
                    if (!FolderManagement.AddFolder(tenantUserSession, folder, out newFolderCreated, out exception)) { if (exception != null) { throw exception; } }
                }
                else
                {
                    throw (new Exception("No folder selected"));
                }
            }
            catch (Exception ex)
            {
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }
            return Json(newFolderCreated.Id);
        }

        public JsonResult GetDocumentByFolderId(long folderId)
        {

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<Document> documentListOut = null;
            List<Document> documentList = null;
            Folder parentFolder = null;
            bool result = false;
            string jsonDocumentFolderTreeViewModel = string.Empty;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                result = FolderManagement.GetDocumentbyFolderId(tenantUserSession, folderId, out parentFolder, out exception);
                if (exception != null)
                    throw exception;

                if (parentFolder != null)
                    documentListOut = parentFolder.Documents.ToList();

                documentList = GetFolderTreeViewData.CleanDocumentList(documentListOut, tenantUserSession);

                jsonDocumentFolderTreeViewModel = JsonConvert.SerializeObject(documentList, Formatting.Indented,
                                                                    new JsonSerializerSettings
                                                                    {
                                                                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                                    });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return Json(jsonDocumentFolderTreeViewModel);
        }

        //public class FileUploadStatus
        //{
        //    public string FileName { get; set; }
        //    public string File { get; set; }
        //    public bool Finalized { get; set; }
        //    public string StatusMessage { get; set; }
        //}

        public JsonResult UploadDocuemntsToAzure(long folderId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            //bool result = false;
            var filePath = string.Empty;
            var fileEntries = new List<FileUploadStatus>();
            FileInfo fileInfo = null;
            Document document = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                filePath = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents");
                string[] filesArray = Directory.GetFiles(filePath);

                //var files = filesArray.ToList().ConvertAll(f => new FileInfo(f));
                //if (files.Any(f => f.Length > Math.Pow(2, 14))) { throw (new Exception($"One or more files exceeds the {Math.Pow(2, 14)} MB size limit.")); }

                fileEntries = filesArray.ToList().ConvertAll<FileUploadStatus>(f => new FileUploadStatus() { File = f, FileName = null, StatusMessage = null, Finalized = false, });

                foreach (var fileEntry in fileEntries)
                {
                    fileInfo = new FileInfo(fileEntry.File.ToString());
                    fileEntry.FileName = fileInfo.Name;

                    try
                    {
                        #region Process files

                        using (var algorithm = HashAlgorithm.Create(GlobalConstants.AlgorithmHashName))
                        {
                            using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                            {

                                var hash = Convert.ToBase64String(algorithm.ComputeHash(stream));

                                if (DocumentManagement.GetDocumentByHash(tenantUserSession, hash, out document, out exception))
                                {
                                    if (document == null)
                                    {
                                        var documentIdStr = fileInfo.Name.Substring(fileInfo.Name.IndexOf("._.") + 3);
                                        long documentId = 0;
                                        if(!(long.TryParse(documentIdStr, out documentId))) { throw (new Exception("Invalid Document.")); }
                                        var result = DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
                                        if (exception != null) { throw (exception); }
                                        document =  DocumentManagement.UpdateDocumentEntry(tenantUserSession, documentId,folderId,hash,stream.Length);
                                        // Upload to azure.
                                        //if (!DocumentManagement.UpdateDocumentFolder(tenantUserSession, document.Id, folderId, out exception)) { if (exception != null) { throw exception; } };
                                        using (var azureHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant))
                                        {
                                            using (var cancellationTokenSource = new CancellationTokenSource())
                                            {
                                                if (azureHelper.DocumentUpload(tenantUserSession, document, stream, cancellationTokenSource.Token, out exception))
                                                {
                                                    if (DocumentManagement.DocumentEntryFinalize(tenantUserSession, document.Id, out document, out exception))
                                                    {
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




                                                        // Done.
                                                        // User can no longer cancel this upload.
                                                        fileEntry.Finalized = true;
                                                        fileEntry.StatusMessage = "Document uploaded and finalized successfully";
                                                        document = null;
                                                        fileInfo = null;
                                                    }
                                                    else
                                                    {
                                                        // Notify user of failed upload/finalization.
                                                        throw new Exception("Document failed to finalized");
                                                    }
                                                }
                                                else
                                                {
                                                    // Upload to azure failed. Notify user.
                                                    throw new Exception("Document failed to upload");
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        // The document already exists. Just notify the user.
                                        fileEntry.Finalized = false;
                                        throw new Exception("Document already exists");
                                    }
                                }
                                else
                                {
                                    // The document already exists. Just notify the user.
                                    fileEntry.Finalized = false;
                                    throw new Exception("Document already exists");
                                }
                            }
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        fileEntry.Finalized = false;
                        fileEntry.StatusMessage = ExceptionUtilities.ExceptionToString(ex);
                        if (document != null)
                        {
                            DocumentManagement.DeleteDocument(tenantUserSession, document, out exception);
                            document = null;
                        }
                    }
                }
                if (exception != null)
                {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                if (document != null)
                {
                    DocumentManagement.DeleteDocument(tenantUserSession, document, out exception);
                    document = null;
                }
            }
            finally
            {
                #region Delete local file
                DirectoryHelper.DeleteAllFiles(filePath, out exception);
                #endregion
            }

            return Json(fileEntries);
        }
        public ActionResult UploadDocumentsToFolder(IEnumerable<HttpPostedFileBase> files)
        {
            Document document = null;
            if (files != null)
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;
                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                    if (exception != null)
                        throw exception;
                    var filesList = files.ToList();
                    foreach (var file in files)
                    {
                        var containsDuplicateFiles = files.Where(x => ((Path.GetFileName(x.FileName) == Path.GetFileName(file.FileName)) && (x.ContentLength == file.ContentLength))).ToList();
                        if (containsDuplicateFiles.Count <= 1)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            if (DocumentManagement.CreateDocumentEntry(tenantUserSession,fileName, file.ContentLength, null, tenantUserSession.User, out document, out exception))
                            {

                                var pathTenantDoc = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents" + "");
                                if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }
                                var physicalPath = Path.Combine(pathTenantDoc, fileName+"._."+document.Id.ToString());
                                file.SaveAs(physicalPath);
                            }
                            else
                            {
                                throw (exception);
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public FileStreamResult DownloadDocument(long id)
        {
            Document document = null;
            Stream documentStream = null;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                DocumentManagement.GetDocumentById(tenantUserSession, id, out document, out exception);
                if (exception != null) { throw exception; }
                AzureCloudStorageAccountHelper azureCloudHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                azureCloudHelper.GetTenantDocumentFileStream(tenantUserSession.Tenant, document, out documentStream, out exception);
                if (exception != null) { throw exception; }
                var fileName = Path.GetFileNameWithoutExtension(document.Name) + Path.GetExtension(document.FileNameClient);
                documentStream.Seek(0, SeekOrigin.Begin);
                this.Response.BufferOutput = false;
                return File(documentStream, System.Net.Mime.MediaTypeNames.Application.Octet.ToString(), fileName);
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(Server.MapPath(@"~/App_Data/Downloads/Temp.txt"), ExceptionUtilities.ExceptionToString(ex));
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@"~/App_Data/Downloads/Temp.txt"));
                var fileName = "Error.txt";
                Stream errorStream = new MemoryStream(fileBytes);
                errorStream.Seek(0, SeekOrigin.Begin);
                return File(errorStream, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }

        }

        public JsonResult RenameDocument(long id, string documentName)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            try
            {
                if (id <= 0) { throw (new Exception("Unable to find the requested document")); }
                if (string.IsNullOrEmpty(documentName.Trim())) { throw (new Exception("Document Name is Required")); }
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                result = DocumentManagement.RenameDocumentById(tenantUserSession, id, documentName, out exception);
                if (exception != null) { throw exception; }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }

        }

        public ActionResult RemoveDocumentsToFolder(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;

                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                    if (exception != null)
                        throw exception;

                    foreach (var fullName in fileNames)
                    {
                        var fileName = Path.GetFileName(fullName);

                        var pathTenantDoc = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents" + "");
                        if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }

                        var physicalPath = Path.Combine(pathTenantDoc, fileName);

                        // TODO: Verify user permissions

                        if (System.IO.File.Exists(physicalPath))
                        {
                            // The files are not actually removed in this demo
                            System.IO.File.Delete(physicalPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult GetRelatedDocuments(long id)
        {

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<Document> documentList = new List<Document>();
            this.ViewBag.HeaderHtmlAttributesTitle = "text-align: center;display:none";
            this.ViewBag.HeaderHtmlAttributesItems = "display:none";
            this.ViewBag.EnablePrintOption = true;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                bool result = DocumentManagement.GetDocumentsRelated(tenantUserSession, id, out documentList, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                documentList = new List<Document>();
                this.ViewBag.RelatedDocumentsErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return PartialView("~/Views/Tenants/Documents/_RelatedDocuments.cshtml", documentList);
        }
        private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }
        public ActionResult GetDocumentVersions(long id)
        {
            var documentVersions = new List<Document>();
            try
            {
                if (id <= 0) { throw new Exception("Unable to find the following document"); }
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                DocumentManagement.GetDocumentVersionsByDocumentId(tenantUserSession, id, out documentVersions, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return PartialView("~/Views/Tenants/Documents/_DocumentHistory.cshtml", documentVersions);
        }
        public ActionResult CleanUploadedDocuemnts()
        {
            try
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                var filePath = Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents");
                DirectoryHelper.DeleteAllFiles(filePath, out exception);
            }
            catch (Exception ex)
            {
                return Json(ExceptionUtilities.ExceptionToString(ex), JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RenameFolder(long folderId, string folderName)
        {
            bool result = false;
            try
            {
                if (folderId <= 0) { throw (new Exception("No Folder Selected")); }
                if (string.IsNullOrEmpty((folderName.Trim() ?? string.Empty))) { throw (new Exception("Folder name is required")); }
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                result = FolderManagement.RenameFolderByFolderId(tenantUserSession, folderId, folderName, out exception);
                if (exception != null) { throw exception; }

            }
            catch (Exception ex)
            {
                return Json(ExceptionUtilities.ExceptionToString(ex), JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}