using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantOcrClassificationsController : Controller
    {
        // GET: TenantOcrClassifications
        public ActionResult Index(string ErrorMessage, string SuccessMessage)
        {
            List<OcrResultInfo> OcrResultInfos = TempData["OcrResultInfos"] as List<OcrResultInfo>;
            if (!(string.IsNullOrEmpty(ErrorMessage)))
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            if (!(string.IsNullOrEmpty(SuccessMessage)))
            {
                ViewBag.SuccessMessage = SuccessMessage;
            }

            if (OcrResultInfos != null)
            {
                return this.View("~/Views/Tenants/OcrClassifications/OcrClassifications.cshtml", OcrResultInfos);
            }
            else
            {
                #region OcrResultInfosSeeder
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                Document document = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request,SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.FormsAdd, TenantActionType.FormsEdit, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }
                OcrResultInfos = new List<OcrResultInfo>();
                bool result = DocumentManagement.GetDocumentById(tenantUserSession, 5, out document, out exception);

                
                //===================================================================
                OcrResultInfo OcrResultInfo = new OcrResultInfo();
                document.FileNameClient = "Default_QR_Image.png";
                OcrResultInfo.Document = document;
                OcrResultInfo.DocumentId = 5;
                OcrResultInfo.DoucmentState = DocumentState.UnMatched;
                MatchedTemplates mt = new MatchedTemplates();
                mt.Confidence=0;
                mt.DocumentTemplates= null;
                mt.TemplateName="";
                OcrResultInfo.MatchedTemplates = new List<MatchedTemplates>();
                OcrResultInfo.MatchedTemplates.Add(mt);
                OcrResultInfo.MatchedTemplatesCount = 0;
                OcrResultInfo.OcrFullTextData = "OCR TEXT1\n\nsadasdasdasdsd";
                OcrResultInfo.OcrFullXmlData = "OCR XML1\rasd\nasdsadsa";
                OcrResultInfo.TemplateType = TemplateType.Template;
                OcrResultInfos.Add(OcrResultInfo);
                


                OcrResultInfo = new OcrResultInfo();
                document.FileNameClient = "Default_QR_Image.png";
                OcrResultInfo.Document = document;
                OcrResultInfo.DocumentId = 6;
                OcrResultInfo.DoucmentState = DocumentState.MatchedMultiple;
                mt = new MatchedTemplates();
                mt.Confidence = 90;
                mt.DocumentTemplates = null;
                mt.TemplateId = 1;
                mt.TemplateName = "Templatename11";
                OcrResultInfo.MatchedTemplates = new List<MatchedTemplates>();
                OcrResultInfo.MatchedTemplates.Add(mt);
                mt = new MatchedTemplates();
                mt.Confidence = 90;
                mt.DocumentTemplates = null;
                mt.TemplateId = 2;
                mt.TemplateName = "Templatename12";
                mt = new MatchedTemplates();
                mt.Confidence = 90;
                mt.DocumentTemplates = null;
                mt.TemplateId = 3;
                mt.TemplateName = "Templatename13";
                OcrResultInfo.MatchedTemplates.Add(mt);
                OcrResultInfo.MatchedTemplatesCount = 2;
                OcrResultInfo.OcrFullTextData = "OCR TEXT2\n\nsadasdasdasdsdTEXT";
                OcrResultInfo.OcrFullXmlData = "OCR XMl2\rasd\nasdsadsa";
                OcrResultInfo.TemplateType = TemplateType.Template;
                OcrResultInfos.Add(OcrResultInfo);


                 OcrResultInfo = new OcrResultInfo();
                document.FileNameClient = "Default_QR_Image.png";
                OcrResultInfo.Document = document;
                OcrResultInfo.DocumentId = 7;
                OcrResultInfo.DoucmentState = DocumentState.Matched;
                 mt = new MatchedTemplates();
                mt.Confidence = 90;
                mt.DocumentTemplates = null;
                mt.TemplateId = 3;
                mt.TemplateName = "Templatename3";
                OcrResultInfo.MatchedTemplates = new List<MatchedTemplates>();
                OcrResultInfo.MatchedTemplates.Add(mt);
                OcrResultInfo.MatchedTemplatesCount = 1;
                OcrResultInfo.OcrFullTextData = "OCR TEXT3\n\nsadasd\r\rr\\r\n\nasdasdsdTEXT";
                OcrResultInfo.OcrFullXmlData = "OCR XML3\rasd\nasd\n\nsadasd\r\rr\\r\n\nsadsa";
                OcrResultInfo.TemplateType = TemplateType.Template;
                OcrResultInfos.Add(OcrResultInfo);
                //===========================================================
                //foreach (var ocrResultInfo in OcrResultInfos)
                //{
                //    ocrResultInfo.OcrFullTextData.Replace("\n", "<br><br>");
                //    ocrResultInfo.OcrFullTextData.Replace("\r", "<br><br>");
                //    ocrResultInfo.OcrFullXmlData.Replace("\n", "<br><br>");
                //    ocrResultInfo.OcrFullXmlData.Replace("\r", "<br><br>");
                //}
                #endregion OcrResultInfosSeeder
                return this.View("~/Views/Tenants/OcrClassifications/OcrClassifications.cshtml", OcrResultInfos);
            }
            
        }
    }
}