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

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantTemplateDesignController : Controller
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
                            if (temp!=null)
                            {
                                elementandelementdetialviewmodel.template = temp;
                            }
                        }
                        return View("~/Views/Tenants/Templates/TemplateTestDesign.cshtml", elementandelementdetialviewmodel);
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
            return  RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = string.Empty, SuccessMessage = string.Empty });
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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
        public JsonResult Checkin(long templateId) {
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

        [HttpPost]
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

        [HttpPost]
        public JsonResult ActiveInactive(long templateId,bool activeInactive)
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