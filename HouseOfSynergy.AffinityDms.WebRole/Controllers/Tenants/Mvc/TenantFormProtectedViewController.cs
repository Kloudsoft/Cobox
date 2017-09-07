using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantFormProtectedViewController : Controller
    {
        // GET: TenantFormProtectedView
        [HttpGet]
        public ActionResult Index(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool dbresult = false;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
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
                                    return this.View("~/Views/Tenants/Forms/FormProtectedView.cshtml", elementandelementdetialviewmodel);
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
                                List<TemplateElement> elementslist = null;
                                return this.View("~/Views/Tenants/Forms/FormProtectedView.cshtml", elementandelementdetialviewmodel);
                            }
                        }
                    }
                }
                else
                {
                    throw (new Exception("Invalid Form Id, Unable to load the following Form."));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return RedirectToAction("Index", "TenantForms", new { ErrorMessage = exception.Message, SuccessMessage = string.Empty });

            }
            return RedirectToAction("Index", "TenantForms", new { ErrorMessage = string.Empty, SuccessMessage = string.Empty });
        }

        public ActionResult MaxProtectedView(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool dbresult = false;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
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
                    Template template = null;
                    dbresult = ElementManagement.GetMaxVersionTemplateByTemplateId(tenantUserSession, (long)id, out template, out exception);
                    if (exception != null) { throw exception; }
                    if (dbresult && (template != null))
                    {
                        this.ViewBag.Finalized = template.IsFinalized;
                        this.ViewBag.DesignStatus = template.IsActive;
                        dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, template.Id, out elements, out exception);
                        if (elements != null)
                        {
                            this.ViewBag.Id = template.Id;
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
                                return this.View("~/Views/Tenants/Forms/FormProtectedView.cshtml", elementandelementdetialviewmodel);
                            }
                        }
                    }



                    //var elements = new List<TemplateElement>();
                    //dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                    /////
                    ///// If Element Count is Greater than 0 than the form will eventually be redirected for editing else creating
                    /////
                    //if (elements.Count > 0)
                    //{

                    //}
                    //else
                    //{
                    //    Template template = null;
                    //    dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
                    //    if (exception != null)
                    //    {
                    //        throw exception;
                    //    }
                    //    if (dbresult)
                    //    {
                    //        if (template != null)
                    //        {
                    //            this.ViewBag.Id = id;
                    //            List<TemplateElement> elementslist = null;
                    //            return this.View("~/Views/Tenants/Forms/FormProtectedView.cshtml", elementandelementdetialviewmodel);
                    //        }
                    //    }
                    //}
                }
                else
                {
                    throw (new Exception("Invalid Form Id, Unable to load the following Form."));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return RedirectToAction("Index", "TenantForms", new { ErrorMessage = exception.Message, SuccessMessage = string.Empty });

            }
            return RedirectToAction("Index", "TenantForms", new { ErrorMessage = string.Empty, SuccessMessage = string.Empty });
        }


    }
}