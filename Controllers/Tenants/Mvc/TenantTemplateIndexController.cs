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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantTemplateIndexController : Controller
    {
        #region Action Methods
        // GET: TenantTemplateDesign
        public ActionResult Index(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }

            try
            {
                List<SelectListItem> DataTypeSelectListItem = EnumUtilities.GetSelectItemListFromEnum<DocumentIndexDataType>(out exception);
                this.ViewBag.DataTypeSelectListItem = DataTypeSelectListItem;
                if (id > 0)
                {
                    long ID = long.Parse(id.ToString());
                    this.ViewBag.Id = ID;
                    bool result = false;
                    Template temp = null;
                    bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out temp, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((temp != null) && dbresult)
                    {


                        if (temp.TemplateImage != null)
                        {
                            byte[] Templateimagebytearr = temp.TemplateImage;
                            ImageConverter Imgconverter = new ImageConverter();
                            Image img = (Image)Imgconverter.ConvertFrom(Templateimagebytearr);
                            this.ViewBag.ModelTemplateImageByteArray = Templateimagebytearr;
                            this.ViewBag.ModelTemplateImage = img;
                        }
                        else
                        {
                            this.ViewBag.ModelTemplateImage = null;
                            this.ViewBag.ModelTemplateImageByteArray = null;
                        }
                        this.ViewBag.Finalized = temp.IsFinalized;
                        this.ViewBag.DesignStatus = temp.IsActive;
                        List<TemplateElement> elements = null;
                        dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        TemplateElementListAndElementDetailListViewModel elementandelementdetialviewmodel = null;
                        if ((elements != null) && dbresult)
                        {
                            List<TemplateElementDetail> elementdetaillist = null;
                            result = TemplateAndFormHelper.GetListOfTemplateElementDetails(tenantUserSession, elements, out elementdetaillist, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                            List<TemplateElement> elementlist = null;
                            result = TemplateAndFormHelper.GetListOfFormElements(tenantUserSession, elements, out elementlist, out exception);
                            elementandelementdetialviewmodel = new TemplateElementListAndElementDetailListViewModel();
                            elementandelementdetialviewmodel.elements = new List<TemplateElement>();
                            elementandelementdetialviewmodel.elementsdetails = new List<TemplateElementDetail>();
                            elementandelementdetialviewmodel.template = new Template();
                            if (elementlist != null)
                            {
                                elementandelementdetialviewmodel.elements = elementlist;
                            }
                            if (elementdetaillist != null)
                            {
                                elementandelementdetialviewmodel.elementsdetails = elementdetaillist;
                            }
                            if (temp != null)
                            {
                                elementandelementdetialviewmodel.template = temp;
                                
                            }
                        }
                        //return View("~/Views/Tenants/Templates/TemplateTestDesign.cshtml", elementandelementdetialviewmodel);
                        return View("~/Views/Tenants/Templates/TemplateIndex.cshtml", elementandelementdetialviewmodel);
                    }
                }
                else
                {
                    throw (new Exception("Invalid Template Id, Unable to load the following Template."));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = ExceptionUtilities.ExceptionToString(ex), SuccessMessage = string.Empty });
            }
            return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = string.Empty, SuccessMessage = string.Empty });
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
                    bool result = ElementManagement.CancelCheckout(tenantUserSession, (long)id, out exception);
                    if (exception != null) { throw exception; }
                    if (result)
                    {
                        return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = string.Empty, SuccessMessage = "Checkedout template was canceled" });
                    }
                    else
                    {
                        throw (new Exception("Unable to cancel checkedout template"));
                    }
                }
                else
                {
                    throw (new Exception("Unable to find the following template"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = ex.Message, SuccessMessage = string.Empty });
            }
        }
        #endregion
        #region Json Methods
        [System.Web.Http.HttpPost]
        public JsonResult UpdateTemplateSettings(bool? IsFinalized, bool? IsActive, int templateid)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.TemplatesEdit, TenantActionType.FormsEdit)) { throw (new UserNotAuthorizedException()); }

            bool result = false;
            try
            {
                result = TemplateAndFormHelper.UpdateSettings(tenantUserSession, IsFinalized, IsActive, templateid, out exception);
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


        [System.Web.Http.HttpPost]
        public JsonResult processindex(long? id,string desc,string val,int x,int y,int w,int h)

        {
            int idd = Convert.ToInt16(id); 
            var result = SaveTemplateIndex(idd,desc,val,x,y,w,h);
            //var json = new JavaScriptSerializer().Serialize(result);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Http.HttpPost]
        public JsonResult SaveTemplateIndex(int templateid,string desc, string val, int x, int y, int w, int h)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            TemplateElement sourceTemplateElement = new TemplateElement();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            
            bool dbresult = false;
            
            sourceTemplateElement.TemplateId = Convert.ToInt64(templateid);
            sourceTemplateElement.Name = desc;
            sourceTemplateElement.Value  = val;
            sourceTemplateElement.X  = x;
            sourceTemplateElement.Y = y;
            sourceTemplateElement.X2 = w;
            sourceTemplateElement.Y2 = h;
            sourceTemplateElement.ElementType = 0;
            sourceTemplateElement.ElementDataType = 0;
            sourceTemplateElement.Width = "200";
            sourceTemplateElement.Height = "200";
            sourceTemplateElement.SizeMode = 0;
            sourceTemplateElement.FontGraphicsUnit = 0;
            sourceTemplateElement.DocumentIndexDataType = 0;

            dbresult = ElementManagement.AddElement(tenantUserSession, sourceTemplateElement, out exception);
            if (exception != null)
            {
                throw exception;
            }
            var result =ListIndex(Convert.ToInt64(templateid));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListIndex(long templateid)
        {
            TempData["Listmode"] = null;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            TemplateElement sourceTemplateElement = new TemplateElement();
            
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            
            bool dbresult = false;

            List<TemplateElement> TemplateElements = new List<TemplateElement>();
            dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, templateid, out TemplateElements, out exception);
            TemplateElementListAndElementDetailListViewModel elementandelementdetialviewmodel = new TemplateElementListAndElementDetailListViewModel();
            elementandelementdetialviewmodel.elements = new List<TemplateElement>();

            if (TemplateElements != null)
            {
                elementandelementdetialviewmodel.elements = TemplateElements;
            }
            var json = new JavaScriptSerializer().Serialize(TemplateElements);
            return Json(json);
            //return View();

        }




        [System.Web.Mvc.HttpPost]
        public JsonResult SaveTemplateDesign(string[] Elements, string[] ElementDetails, int templateid)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.FormsAdd, TenantActionType.FormsEdit, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }

            bool result = false;
            try
            {

                JavaScriptSerializer JavaSerialize = new JavaScriptSerializer();

                List<TemplateElement> TemplateElementsList = JavaSerialize.Deserialize<List<TemplateElement>>(Elements[0]);
                List<TemplateElementDetailViewModel> TemplateElementDetailsViewModelList = JavaSerialize.Deserialize<List<TemplateElementDetailViewModel>>(ElementDetails[0]);
                result = TemplateAndFormHelper.SaveDesign(tenantUserSession, Server.MapPath("~"), TemplateElementsList, TemplateElementDetailsViewModelList, templateid, out exception);
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
        [System.Web.Http.HttpPost]
        public JsonResult Checkin(long templateId)
        {
            if (templateId > 0)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    var result = ElementManagement.Checkin(tenantUserSession, templateId, out exception);
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
                return Json("Unable to find the following Template");
            }

        }


        // AJAX CALL OCR JSON

       


        //AJAX CALL OCR JSON



        //public JsonResult process(long? id)

        //{
        //    var result = ProcessOcr(id);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult ProcessOcr(long? id)
        public async Task<ActionResult> ProcessOcr(long? id)
        {

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            string OCRText = "";
            long ID = Convert.ToInt64(Request.QueryString["id"])  ; 
            this.ViewBag.Id = ID;
            bool result = false;
            Template temp = null;
            bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out temp, out exception);
            if (exception != null)
            {
                throw exception;
            }
            if ((temp != null) && dbresult)
            {
                if (temp.TemplateImage != null)
                {
                    byte[] Templateimagebytearr = temp.TemplateImage;
                    ImageConverter Imgconverter = new ImageConverter();
                    Image img = (Image)Imgconverter.ConvertFrom(Templateimagebytearr);
                    this.ViewBag.ModelTemplateImageByteArray = Templateimagebytearr;
                    this.ViewBag.ModelTemplateImage = img;




                            // OCR The File
                            // Get SubscriptionKey

                            string SubscriptionKey = "5dc704a3098243dda51bb16d92c00d70";

                    //
                    // Create Project Oxford Vision API Service client
                    //
                    string apiroot = @"https://southeastasia.api.cognitive.microsoft.com/vision/v1.0/";
                    VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, apiroot);


                   
                    try
                    {
                        MemoryStream imageFileStream = new MemoryStream(Templateimagebytearr,0, Templateimagebytearr.Length);
                            //
                            // Upload an image and perform OCR
                            //
                        OcrResults ocrResult = await VisionServiceClient.RecognizeTextAsync(imageFileStream, "en");
                        OCRText = LogOcrResults(ocrResult);

                        var json = new JavaScriptSerializer().Serialize(ocrResult);

                        var jObj = (JObject)JsonConvert.DeserializeObject(json);

                        Sort(jObj);

                        string newJson = jObj.ToString();

                        // this.ViewBag.vbName = ocrResult.Regions[3].Lines[0].Words[0].Text + " " + ocrResult.Regions[3].Lines[0].Words[1].Text;
                        //  this.ViewBag.vbValue = ocrResult.Regions[3].Lines[0].Words[2].Text;

                        // TempData["vbName"]= ocrResult.Regions[3].Lines[0].Words[0].Text + " " + ocrResult.Regions[3].Lines[0].Words[1].Text;
                        //  TempData["vbValue"] = ocrResult.Regions[3].Lines[0].Words[2].Text;

                        

                        TempData["TDOcrresult"] = json;




                    }
                    catch (Exception e)
                    {
                       
                    }


                }
                else
                {
                    this.ViewBag.ModelTemplateImage = null;
                    this.ViewBag.ModelTemplateImageByteArray = null;
                }
                
            }

            return Redirect("~/TenantTemplateIndex/Index/" + Convert.ToInt64(Request.QueryString["id"]));
            //return View();

        }

        static void Sort(JObject jObj)
        {
            var props = jObj.Properties().ToList();
            foreach (var prop in props)
            {
                prop.Remove();
            }

            foreach (var prop in props.OrderBy(p => p.Name))
            {
                jObj.Add(prop);
                if (prop.Value is JObject)
                    Sort((JObject)prop.Value);
                if (prop.Value is JArray)
                {
                    Int32 iCount = prop.Value.Count();
                    for (Int32 iIterator = 0; iIterator < iCount; iIterator++)
                        if (prop.Value[iIterator] is JObject)
                            Sort((JObject)prop.Value[iIterator]);
                }
            }
        }

        //void Sort(JObject jObj)
        //{
        //    var props = jObj.Properties().ToList();
        //    foreach (var prop in props)
        //    {
        //        prop.Remove();
        //    }

        //    foreach (var prop in props.OrderBy(p => p.Name))
        //    {
        //        jObj.Add(prop);
        //        if (prop.Value is JObject)
        //            Sort((JObject)prop.Value);
        //    }
        //}


        private IHttpActionResult Ok(string oCRText)
        {
            throw new NotImplementedException();
        }

        protected string LogOcrResults(OcrResults results)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (results != null && results.Regions != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.AppendLine();
                foreach (var item in results.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
        [System.Web.Http.HttpPost]
        public JsonResult UpdateVersion(long templateId, int verMajor, int verMinor)
        {
            if (templateId > 0)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    var result = ElementManagement.UpdateVersion(tenantUserSession, templateId, verMajor, verMinor, out exception);
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
                return Json("Unable to find the following Template");
            }

        }

        [System.Web.Http.HttpPost]
        public JsonResult ActiveInactive(long templateId, bool activeInactive)
        {
            bool result = false;
            if (templateId > 0)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    result = ElementManagement.ActiveInactive(tenantUserSession, templateId, activeInactive, out exception);
                    if (exception != null) { throw exception; }
                }
                catch (Exception ex)
                {
                    return Json(ExceptionUtilities.ExceptionToString(ex));
                }
                return Json(result.ToString());
            }
            else
            {
                return Json("Unable to find the following Template");
            }
        }

        #endregion
    }
}