using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System.Web.Script.Serialization;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantFormDesignController : Controller
    {
        #region Action Methods
        [HttpGet]
        public ActionResult Index(long?id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool dbresult = false;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            List<SelectListItem> DataTypeSelectListItem = EnumUtilities.GetSelectItemListFromEnum<DocumentIndexDataType>(out exception);
            this.ViewBag.DataTypeSelectListItem = DataTypeSelectListItem;
            var elementandelementdetialviewmodel = new TemplateElementListAndElementDetailListViewModel();
            elementandelementdetialviewmodel.elements = new List<TemplateElement>();
            elementandelementdetialviewmodel.elementsdetails = new List<TemplateElementDetail>();
            elementandelementdetialviewmodel.template = new Template();
            try
            {
                long ID = (long)id;
                if (ID > 0)
                {
                    var elements = new List<TemplateElement>();
                    dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                    ///
                    /// If Element Count is Greater than 0 than the form will eventually be redirected for editing else creating
                    ///
                    if (elements.Count > 0)
                    {
                        Template template = null;
                        dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult && (template != null))
                        {
                            this.ViewBag.Finalized = template.IsFinalized;
                            this.ViewBag.DesignStatus = template.IsActive;
                            if (elements != null)
                            {
                                this.ViewBag.Id = ID;
                                List<TemplateElement> elementslist = null;
                                
                                bool result = TemplateAndFormHelper.GetListOfFormElements(tenantUserSession, elements, out elementslist, out exception);
                                if (exception != null)
                                {
                                    throw exception;
                                }
                                if ((elementslist != null) && result)
                                {
                                    if (elementslist != null)
                                    {
                                        elementandelementdetialviewmodel.elements = elementslist;
                                    }
                                    if (template != null)
                                    {
                                        elementandelementdetialviewmodel.template = template;
                                    }
                                    return this.View("~/Views/Tenants/Forms/FormDesign.cshtml", elementandelementdetialviewmodel);
                                }
                            }
                        }
                    }
                    else
                    {
                        Template template = null;
                        dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult)
                        {
                            if (template != null)
                            {
                                this.ViewBag.Id = id;
                                elementandelementdetialviewmodel.template = template;
                                return this.View("~/Views/Tenants/Forms/FormDesign.cshtml", elementandelementdetialviewmodel);
                            }
                        }
                    }
                }
                else
                {
                    throw (new Exception("Invalid Form Id, Unable to load the following Form."));
                }
            }
            catch(Exception ex)
            {
                exception = ex;
                return RedirectToAction("Index", "TenantForms", new { ErrorMessage = exception.Message, SuccessMessage = string.Empty });

            }
            return RedirectToAction("Index", "TenantForms", new { ErrorMessage = string.Empty, SuccessMessage=string.Empty });
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
                        return RedirectToAction("Index", "TenantForms", new { ErrorMessage = string.Empty, SuccessMessage = "Checkedout form was canceled" });
                    }
                    else
                    {
                        throw (new Exception("Unable to cancel checkedout form"));
                    }
                }
                else
                {
                    throw (new Exception("Unable to find the following form"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return RedirectToAction("Index", "TenantForms", new { ErrorMessage = ex.Message, SuccessMessage = string.Empty });
            }
        }
        #endregion 
        #region Json Methods
        [HttpPost]
        public JsonResult UpdateFormSettings(bool? IsFinalized, bool? IsActive, int templateid)
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
        public JsonResult SaveFormDesign(string[] Elements, string[] ElementDetails, int templateid)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
           // if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.FormsAdd, TenantActionType.FormsEdit, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }

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
                    return Json(ex.ToString());
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