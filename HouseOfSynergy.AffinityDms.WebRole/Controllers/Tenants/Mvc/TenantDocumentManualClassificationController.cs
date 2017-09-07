using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDocumentManualClassificationController : Controller
    {
        // GET: TenantDocumentManualClassification
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TemplateListing(long id)
        {
            this.ViewBag.DocumentId = -1;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<Template> templates = new List<Template>();
            try
            {
                if (id > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                    bool dbresult = ElementManagement.GetAllMaxVersionTemplates(tenantUserSession, out templates, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (templates != null)
                    {
                        if (templates.Count > 0)
                        {
                            templates = templates.Where(x => x.TemplateType == TemplateType.Template).ToList();
                        }
                    }
                    else
                    {
                        templates = new List<Template>();
                    }
                    this.ViewBag.DocumentId = id;
                }
                else
                {
                    throw (new Exception("Unable to perform the following operation. Following document was not found"));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = ExceptionUtilities.ExceptionToString(ex), SuccessMessage = string.Empty });
            }
            return View("~/Views/Tenants/Documents/DocumentTemplateListings.cshtml", templates);
        }
        [HttpPost]
        public JsonResult ProcessDocument(long documentId, long templateId) {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            try
            {
                if (documentId > 0 && templateId >0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    result = DocumentManagement.UpdateDocumentForManualClassifcation(tenantUserSession, documentId, templateId, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    string exceptionstr = "";
                    if (documentId <= 0)
                    {
                        exceptionstr = "Unable to find the following document.";
                    }
                    if (templateId <= 0)
                    {
                        exceptionstr += "Unable to find the following template.";
                    }
                    throw (new Exception(exceptionstr));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(ExceptionUtilities.ExceptionToString(exception));
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult ProcessDocuments(List<long> documentIds, long templateId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                foreach (var documentId in documentIds)
                {
                    if (documentId > 0 && templateId > 0)
                    {

                        result = DocumentManagement.UpdateDocumentForManualClassifcation(tenantUserSession, documentId, templateId, out exception);
                        if (exception != null) { throw exception; }
                    }
                    else
                    {
                        string exceptionstr = "";
                        if (documentId <= 0)
                        {
                            exceptionstr = "Unable to find the following document.";
                        }
                        if (templateId <= 0)
                        {
                            exceptionstr += "Unable to find the following template.";
                        }
                        throw (new Exception(exceptionstr));
                    }
                }
                
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(ExceptionUtilities.ExceptionToString(exception));
            }
            return Json(result);
        }

    }
}