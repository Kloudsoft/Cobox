using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantMoveFolderController : Controller
    {
        // GET: TenantMoveFolder
        public ActionResult Index(long id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<FolderTreeViewModel> folderTVM = new List<FolderTreeViewModel>();
            List<Folder> folderList = new List<Folder>();
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            try
            {
                if (id > 0)
                {
                    this.ViewBag.Id = id;
                    Document document = null;
                    Stream documentImageStream = null;
                    bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, id, out document, out exception);
                    if (exception != null) { throw exception; }
                    if (document == null) { throw (new Exception("Unable to find the document")); }
            
                    using (var azureCloudServiceHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant))
                    {
                        azureCloudServiceHelper.GetTenantDocumentFileStream(tenantUserSession.Tenant, document, out documentImageStream, out exception);
                        if (exception != null || documentImageStream == null) { this.ViewBag.ErrorMessage = "Document Image Not Found"; }//throw exception; }
                        else
                        {
                            if (documentImageStream.Length <= 0) { throw (new Exception("Unable to find document Image")); }
                            Image documentImg = Image.FromStream(documentImageStream);
                            this.ViewBag.ModelDocuemntImage = documentImg;
                            byte[] documentImageByteArray = ConvertImageToByteArray(documentImg);
                            this.ViewBag.ModelDocuemntImageByteArray = documentImageByteArray;
                        }

                    }

                    // Bind Folder Tree view
                    //folderTVM = GetFolderTreeViewData.GetFolderList(tenantUserSession, null, out folderList, out exception);
                    //if (exception != null) { throw exception; }
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
            return this.View("~/Views/Tenants/Documents/FolderMove.cshtml", folderTVM);
            
        }

        private byte[] ConvertImageToByteArray(Image img)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
            return xByte;
        }

        public JsonResult GetSelectedFolderDetails(long? folderid)
        {
            Exception exception = null;
            Folder parentFolder = null;
            TenantUserSession tenantUserSession = null;
            DocumentFolderTreeViewModel documentFolderTreeViewModel = new DocumentFolderTreeViewModel();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

               
                if (!FolderManagement.GetDocumentFolderWiseData(tenantUserSession, folderid, out parentFolder, out exception)) { if (exception != null) { throw exception; } }
                if (parentFolder != null)
                {
                    documentFolderTreeViewModel.Parentfolder = parentFolder;
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
            return Json(documentFolderTreeViewModel);
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
                    return Json("Document moved successfully.");

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
    }
}