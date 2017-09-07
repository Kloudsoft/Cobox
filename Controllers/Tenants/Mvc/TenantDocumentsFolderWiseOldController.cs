using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
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

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDocumentsFolderWiseOldController : Controller
    {
        // GET: TenantDocumentsFolderWise
        public ActionResult Index(string ErrorMessage, string SuccessMessage)
        {
            Exception exception = null;
            // List<Folder> folders = null;
            Folder parentFolder = null;
            TenantUserSession tenantUserSession = null;
            DocumentFolderTreeViewModel documentFolderTreeViewModel = new DocumentFolderTreeViewModel();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                List<FolderTreeViewModel> FolderTreeVMListWhenParentIdIsNull = GetFolderTreeViewData.GetFolderList(tenantUserSession, null, out parentFolder, out exception);
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
            if (!(string.IsNullOrEmpty(ErrorMessage)))
            {
                this.ViewBag.ErrorMessage = ErrorMessage;
            }
            else
            {
                this.ViewBag.ErrorMessage = string.Empty;
            }
            if (!(string.IsNullOrEmpty(SuccessMessage)))
            {
                this.ViewBag.SuccessMessage = SuccessMessage;
            }
            else
            {
                this.ViewBag.SuccessMessage = string.Empty;
            }
            documentFolderTreeViewModel = GetFolderTreeViewData.CleanDocumentTreeViewModalData(documentFolderTreeViewModel, tenantUserSession);
            return View("~/Views/Tenants/Documents/DocumentsFolderWise.cshtml", documentFolderTreeViewModel);
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

                FolderTreeVMList = GetFolderTreeViewData.GetFolderList(tenantUserSession, id,  out parentFolder, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                return Json($"Exception: {ExceptionUtilities.ExceptionToString(ex)}", JsonRequestBehavior.AllowGet);
            }
            return Json(FolderTreeVMList, JsonRequestBehavior.AllowGet);
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
            documentFolderTreeViewModel = GetFolderTreeViewData.CleanDocumentTreeViewModalData(documentFolderTreeViewModel,tenantUserSession);

            string jsonDocumentFolderTreeViewModel = JsonConvert.SerializeObject(documentFolderTreeViewModel, Formatting.Indented,
                                                                new JsonSerializerSettings
                                                                {
                                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                                });
            return Json(jsonDocumentFolderTreeViewModel);
        }

        [HttpPost]
        public JsonResult AddFolder(long parentFolderId, string folderName)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Folder newFolderCreated = null;
            try
            {
                if (parentFolderId > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    Folder folder = new Folder();
                    folder.DateTimeCreated = DateTime.UtcNow;
                    folder.DepartmentId = null;
                    folder.ParentId = parentFolderId;
                    folder.Name = folderName;
                    folder.FolderType = FolderType.Child;
                    if (!FolderManagement.AddFolder(tenantUserSession, folder, out newFolderCreated, out exception)) { if (exception != null) { throw exception; } }
                    return Json(newFolderCreated.Id);
                }
                else
                {
                    throw (new Exception("Unable to find the following folder"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return Json(ExceptionUtilities.ExceptionToString(exception));
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

                result = FolderManagement.GetDocumentbyFolderId(tenantUserSession, folderId, out parentFolder,out exception);
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
                filePath = Server.MapPath("~/App_Data/Tenants/"+tenantUserSession.Tenant.MasterTenantId+"/Documents");
                string[] filesArray=  Directory.GetFiles(filePath);

                fileEntries = filesArray.ToList().ConvertAll<FileUploadStatus>(f => new FileUploadStatus() { File = f, FileName=null, StatusMessage = null, Finalized = false, });

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
                                        if (DocumentManagement.CreateDocumentEntry(tenantUserSession, fileEntry.File, hash, fileInfo.Length, folderId,null, tenantUserSession.User, out document, out exception))
                                        {
                                            //Upload to azure.
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
                                                            //bool result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                                                            //if (exception != null) { throw exception; }
                                                            //    List<WorkflowCommand> commands = null;
                                                            //    wfehelper.GetAvailableCommands(out commands, out exception);
                                                            //    if (exception != null) { throw exception; }
                                                            //    var command = commands.Where(x => x.CommandName.ToLower() == "uploadandfinalized").SingleOrDefault();
                                                            //    if (command != null)
                                                            //    {
                                                            //        wfehelper.ExecuteCommand(command, out exception);
                                                            //        if (exception != null) { throw exception; }
                                                            //    }
                                                            //}
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
                                            throw (exception);
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
                        //fileEntry.Exception = ex.ToString();
                        fileEntry.StatusMessage = ExceptionUtilities.ExceptionToString(ex);
                        #region Delete local file
                        if (fileInfo != null)
                        {
                            fileInfo.Delete();
                            fileInfo = null;
                        }
                        if (document != null)
                        {
                            DocumentManagement.DeleteDocument(tenantUserSession, document, out exception);
                            document = null;
                        }
                        #endregion
                    }


                }

                if (exception != null) {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                #region Delete local file
                if (fileInfo != null)
                {
                    fileInfo.Delete();
                    fileInfo = null;
                }
                if (document !=null)
                {
                    DocumentManagement.DeleteDocument(tenantUserSession, document, out exception);
                    document = null;
                }
                #endregion
            }

            return Json(fileEntries);
        }
        public ActionResult UploadDocumentsToFolder(IEnumerable<HttpPostedFileBase> files)
        {
            if(files!=null)
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;
                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                    if (exception != null)
                        throw exception;

                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        var pathTenantDoc= Server.MapPath("~/App_Data/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents" + "");
                        if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }

                        var physicalPath = Path.Combine(pathTenantDoc, fileName);
                        file.SaveAs(physicalPath);
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

        private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }
    }
}