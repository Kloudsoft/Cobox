using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantScanSessionsController : Controller
    {
        // GET: TenantScanSessions
        public ActionResult Index(string ErrorMessage,string SuccessMessage)
        {
            this.ViewBag.ErrorMessage = string.Empty;
            this.ViewBag.SuccessMessage = string.Empty;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            List<ScanSession> scanSessionList = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                scanSessionList = ScanSessionManagement.GetAllScanSessions(tenantUserSession);
                //if (scanSessionList.Count<=0)
                //{
                //    scanSessionList = GetDummyScanData(tenantUserSession, out exception);
                //}
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
            return View("~/Views/Tenants/ScanSessions/ScanSessions.cshtml", scanSessionList);
        }
        public static List<ScanSession> GetDummyScanData(TenantUserSession tenantUserSession,out Exception exception)
        {
            List<ScanSession> scanSessionList = new List<ScanSession>();
            List<User> userList = null;
            bool  result = TenantUserManagement.GetUsers(tenantUserSession, out userList, out exception);
            for (int i = 1; i < 10; i++)
            {
                ScanSession scanSession = new ScanSession();
                scanSession.Id = i;
                scanSession.DateTimeCreated = DateTime.UtcNow.AddMinutes(i);
                scanSession.Description = "Scan No: " + i;
                scanSession.Name = "Scan No: " + i;

                for (int j = 0; j < 10; j++)
                {

                    Document document = new Document();
                    document.Id = j;
                    document.Name = "Document: D" + i + "." + j;
                    document.CheckedOutByUser = userList.Where(x => x.Id == i).FirstOrDefault();
                    document.CheckedOutByUserId = i;
                    document.CheckedOutDateTime = DateTime.UtcNow;
                    document.Confidence = 10 * j;
                    document.DateTime = DateTime.UtcNow;
                    document.DocumentOriginalId = document.Id;
                    document.DocumentParent = document.Id;
                    document.DocumentQueueType = DocumentQueueType.Auto;
                    document.DocumentIndexType = DocumentIndexType.Auto;
                    document.DocumentType = DocumentType.Raster;
                    document.FileNameClient = "File_" + i + "." + j + ".jpg";
                    document.FileNameServer = "File_" + i + "." + j + ".cfe";
                    document.FullTextOCRXML = "This is some Dummy Xml Ocred Text";
                    document.IndexingIteration = 2;
                    document.IndexingLevel = (j % 2 == 0) ? (i) : (3);
                    document.IsFinalized = true;
                    document.Length = 500 * j;
                    document.ScanSessionId = i;
                    document.VersionMajor = j;
                    document.VersionMinor = j * 2;
                    document.User = userList.Where(x => x.Id == i).FirstOrDefault();
                    document.UserId = i;
                    document.Uploaded = true;
                    document.SourceType = SourceType.DesktopScanner;
                    if (document.Confidence < (int)OcrConfidence.MinimumRecognitionConfidence)
                    {
                        document.State = DocumentState.UnMatched;
                    }
                    else if (document.Confidence >= (int)OcrConfidence.MinimumRecognitionConfidence && document.Confidence < (int)OcrConfidence.MinimumOCRConfidence)
                    {
                        document.State = DocumentState.MatchedUnverified;
                    }
                    else if (document.Confidence >= (int)OcrConfidence.MinimumOCRConfidence)
                    {
                        document.State = DocumentState.Matched;
                    }
                    if (j == 3 || j == 6 || j == 9)
                    {
                        document.State = DocumentState.Verified;
                    }

                    Template template = new Template();
                    template.Id = j;
                    template.IsActive = true;
                    template.IsFinalized = true;
                    template.Title = "Template: Title" + i + "." + j;
                    template.Description = "Template: Description" + j + "." + i;
                    document.Template = template;
                    scanSession.Documents.Add(document);
                }
                scanSession.User = userList.Where(x => x.Id == i).FirstOrDefault();
                scanSession.UserId = i;
                scanSessionList.Add(scanSession);
            }
            return scanSessionList;
        }
    }
}