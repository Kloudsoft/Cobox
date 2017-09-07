using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
//using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantScanSessionController : Controller
    {
        // GET: TenantScanSession
        public ActionResult Index(long id,bool IsClassifiedActive = true)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            ScanSession scanSession = null;
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(HttpContext.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                scanSession = GetScanSession(tenantUserSession, id, IsClassifiedActive);
                if (scanSession.Id<=0) { return RedirectToAction("Index", "TenantScanSessions"); }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = ex.Message;//ExceptionUtilities.ExceptionToString(ex);
            }
            return View("~/Views/Tenants/ScanSessions/ScanSession.cshtml", scanSession);
        }
        private ScanSession GetScanSession(TenantUserSession tenantUserSession,long scanSessionId, bool IsClassifiedActive)
        {
            this.ViewBag.IsClassifiedActive = IsClassifiedActive;
            Exception exception = null;
            ScanSession scanSession = null;
            if (scanSessionId > 0)
            {
                try
                {
                    scanSession = ScanSessionManagement.GetScanSessionById(tenantUserSession, scanSessionId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            //if (scanSession == null)
            //{
            //    var scanSessionList = TenantScanSessionsController.GetDummyScanData(tenantUserSession, out exception);
            //    if (exception != null) { throw exception; }
            //    scanSession = scanSessionList.Where(x => x.Id == scanSessionId).FirstOrDefault();
            //}
            scanSession = filterClassifiedUnclassifiedDocuments(scanSession, IsClassifiedActive);
            return scanSession;
        }
        public ActionResult GetPartialScanSession(long id, bool IsClassifiedActive = true)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            ScanSession scanSession = null;
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(HttpContext.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                scanSession = GetScanSession(tenantUserSession, id, IsClassifiedActive);
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = ex.Message;//ExceptionUtilities.ExceptionToString(ex);
            }
            return PartialView("~/Views/Tenants/ScanSessions/_ScanSession.cshtml", scanSession);
        }
        public ScanSession filterClassifiedUnclassifiedDocuments(ScanSession scanSession,bool classified)
        {
            if (scanSession != null)
            {
                if (classified)
                {
                    var documents = scanSession.Documents.Where(x => (x.State == DocumentState.Verified) || (x.State == DocumentState.Matched) || (x.State == DocumentState.MatchedMultiple) || (x.State == DocumentState.MatchedUnverified)).ToList();
                    scanSession.Documents.Clear();
                    foreach (var document in documents)
                    {
                        scanSession.Documents.Add(document);
                    }
                }
                else
                {
                    var documents = scanSession.Documents.Where(x => (!(x.State == DocumentState.Verified) || (x.State == DocumentState.Matched) || (x.State == DocumentState.MatchedMultiple) || (x.State == DocumentState.MatchedUnverified))).ToList();
                    scanSession.Documents.Clear();
                    foreach (var document in documents)
                    {
                        scanSession.Documents.Add(document);
                    }
                }
            }
            else
            {
                scanSession = new ScanSession();
            }
            return scanSession;
        }
        public ActionResult GetTemplates()
        {
            TenantUserSession tenantUserSession = null;
            Exception exception= null;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            var templates = TenantTemplatesController.GetAllTemplates(tenantUserSession, out exception);
            //var scanSession = TenantScanSessionsController.GetDummyScanData(tenantUserSession, out exception);
            //templates = scanSession.SelectMany(x => x.Documents).ToList().Select(x => x.Template).ToList().Take(10).ToList();
            return PartialView("~/Views/Tenants/ScanSessions/_ScanSessionTemplates.cshtml", templates);

        }
        public ActionResult ScanSessionDocumentTemplateViewer(long documentId)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            Document document = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                bool result = DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return PartialView("~/Views/Tenants/ScanSessions/ScanSessionDocumentTemplate.cshtml", document);

        }

    }
}