using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDocumentViewerController : Controller
    {
        // GET: TenantDocumentViewer
        public ActionResult Index(long? id, DiscussionPostAttachmentType type = DiscussionPostAttachmentType.None)
        {
            try
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;
                Document document = null;
                DiscoursePostVersionAttachment attachment = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                if (id != null)
                {
                    if (id > 0)
                    {
                        bool result = getDataByType(tenantUserSession,(long) id, out document, out attachment, out exception, type);
                        if (result)
                        {
                            this.ViewBag.Id = id;
                            this.ViewBag.Type = type;
                            if (type == DiscussionPostAttachmentType.Document)
                            {
                                this.ViewBag.Model = document;
                            }
                            else if(type == DiscussionPostAttachmentType.External)
                            {
                                this.ViewBag.Model = attachment;
                            }
                        }
                    }
                    else { throw new Exception("Unable to Find the following Document"); }
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ex.Message;
            }
            return View("~/Views/Tenants/DocumentViewer/DocumentViewer.cshtml");
        }
        private bool getDataByType(TenantUserSession tenantUserSession, long id, out Document document, out DiscoursePostVersionAttachment attachment, out Exception exception, DiscussionPostAttachmentType type = DiscussionPostAttachmentType.None)
        {
            bool result = false;
            document = null;
            exception = null;
            attachment = null;
            switch (type)
            {
                case DiscussionPostAttachmentType.None:
                    break;
                //case DiscussionPostAttachmentType.Template:
                //    break;
                case DiscussionPostAttachmentType.Document:
                    
                    DocumentManagement.GetDocumentById(tenantUserSession, (long)id, out document, out exception);
                    if (exception != null) { throw exception; }
                    if (document != null) { result = true; }

                    break;
                case DiscussionPostAttachmentType.External:
                 
                    DiscourseManagement.GetDiscoursePostVersionAttachmentById(tenantUserSession, (long)id, out attachment, out exception);
                    if (exception != null) { throw exception; }
                    if (attachment != null) { result = true; }
                    break;
                //case DiscussionPostAttachmentType.Live:
                //    break;
                //case DiscussionPostAttachmentType.Form:
                //    break;
                default:
                    break;
            }
            return result;
        }
        public JsonResult GetViewerDocument(long id, DiscussionPostAttachmentType type)
        {
           // FileStreamResult fileStreamResult = null;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            //DocumentViewer.ltDocument ltdoscument= null;
            string url = "";
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                if (id > 0)
                {
                    switch (type)
                    {
                        //case DiscussionPostAttachmentType.None:
                        //    break;
                        //case DiscussionPostAttachmentType.Template:
                        //    break;
                        case DiscussionPostAttachmentType.Document:
                            {
                                AzureCloudStorageAccountHelper azure = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                                Document document = null;
                                DocumentManagement.GetDocumentById(tenantUserSession, id, out document, out exception);
                                if (exception != null) { throw exception; }
                                bool result = azure.GetTenantDocumentFileUrl(tenantUserSession.Tenant, document, out url, out exception);
                                if (exception != null) { throw exception; }
                               
                                break;
                            }
                        case DiscussionPostAttachmentType.External:
                            {
                                AzureCloudStorageAccountHelper azure = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                                DiscoursePostVersionAttachment externalFile = null;
                                DiscourseManagement.GetDiscoursePostVersionAttachmentById(tenantUserSession, id, out externalFile, out exception);
                                if (exception != null) { throw exception; }
                                bool result = azure.GetTenantDiscourseFileUrl(tenantUserSession.Tenant, externalFile, out url, out exception);
                                if (exception != null) { throw exception; }
                                //var tenantDiscourseController = new TenantDiscourseController();
                                break;
                            }
                            
                        //case DiscussionPostAttachmentType.Live:
                        //    break;
                        //case DiscussionPostAttachmentType.Form:
                        //    break;
                        default:
                            break;
                    }
                }
                else {

                }
            }
            catch (Exception) { }

            return Json(url,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPartialDocumentView(long? id, DiscussionPostAttachmentType type = DiscussionPostAttachmentType.None) {
            Document document = null;
            DiscoursePostVersionAttachment attachment = null;
            try
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                if (id != null)
                {
                    if (id > 0)
                    {
                       getDataByType(tenantUserSession, (long)id, out document, out attachment, out exception, type);
                        this.ViewBag.Type = "Document";
                    }
                    else { throw new Exception("Unable to Find the following Document"); }
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = (ex.InnerException != null) ? (ex.InnerException.Message) : (ex.Message);
            }
            return PartialView("~/Views/Tenants/DocumentViewer/_DocumentViewer.cshtml", document);
        }
        public ActionResult TemplateDesigner(long? id, DiscussionPostAttachmentType type = DiscussionPostAttachmentType.None)
        {
            try
            {
                TenantUserSession tenantUserSession = null;
                Exception exception = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                if (id != null)
                {
                    if (id > 0)
                    {
                        bool result = false;
                        switch (type)
                        {
                            case DiscussionPostAttachmentType.None:
                                break;
                            //case DiscussionPostAttachmentType.Template:
                            //    break;
                            case DiscussionPostAttachmentType.Document:
                                Document document = null;
                                DocumentManagement.GetDocumentById(tenantUserSession, (long)id, out document, out exception);
                                if (exception != null) { throw exception; }
                                if (document != null) { result = true; }

                                break;
                            case DiscussionPostAttachmentType.External:
                                DiscoursePostVersionAttachment attachment = null;
                                DiscourseManagement.GetDiscoursePostVersionAttachmentById(tenantUserSession, (long)id, out attachment, out exception);
                                if (exception != null) { throw exception; }
                                if (attachment != null) { result = true; }
                                break;
                            //case DiscussionPostAttachmentType.Live:
                            //    break;
                            //case DiscussionPostAttachmentType.Form:
                            //    break;
                            default:
                                break;
                        }
                        if (result)
                        {
                            this.ViewBag.Id = id;
                            this.ViewBag.Type = type;
                        }
                    }
                    else { throw new Exception("Unable to Find the following Document"); }
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ex.Message;
            }
            return View("~/Views/Tenants/DocumentViewer/DocumentViewer.cshtml");
        }
        public JsonResult GetTemplateDesigner(long id, DiscussionPostAttachmentType type)
        {
            // FileStreamResult fileStreamResult = null;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            //DocumentViewer.ltDocument ltdoscument= null;
            string url = "";
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                if (id > 0)
                {
                    switch (type)
                    {
                        //case DiscussionPostAttachmentType.None:
                        //    break;
                        //case DiscussionPostAttachmentType.Template:
                        //    break;
                        case DiscussionPostAttachmentType.Document:
                            {
                                AzureCloudStorageAccountHelper azure = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                                Document document = null;
                                DocumentManagement.GetDocumentById(tenantUserSession, id, out document, out exception);
                                if (exception != null) { throw exception; }
                                bool result = azure.GetTenantDocumentFileUrl(tenantUserSession.Tenant, document, out url, out exception);
                                if (exception != null) { throw exception; }

                                break;
                            }
                        case DiscussionPostAttachmentType.External:
                            {
                                AzureCloudStorageAccountHelper azure = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                                DiscoursePostVersionAttachment externalFile = null;
                                DiscourseManagement.GetDiscoursePostVersionAttachmentById(tenantUserSession, id, out externalFile, out exception);
                                if (exception != null) { throw exception; }
                                bool result = azure.GetTenantDiscourseFileUrl(tenantUserSession.Tenant, externalFile, out url, out exception);
                                if (exception != null) { throw exception; }
                                //var tenantDiscourseController = new TenantDiscourseController();
                                break;
                            }

                        //case DiscussionPostAttachmentType.Live:
                        //    break;
                        //case DiscussionPostAttachmentType.Form:
                        //    break;
                        default:
                            break;
                    }
                }
                else
                {

                }
            }
            catch (Exception) { }

            return Json(url, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult DiscourseExternalDocument(long? id,string type)
        //{

        //}

    }
}