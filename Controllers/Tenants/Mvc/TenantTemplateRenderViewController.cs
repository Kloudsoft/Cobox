using HouseOfSynergy.AffinityDms.BusinessLayer;
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
    public class TenantTemplateRenderViewController : Controller
    {
        // GET: TenantTemplateRenderView
        public ActionResult Index(long? id)
        {
            Exception exception = null;
             TenantUserSession tenantUserSession = null;
            bool dbresult = false;
            TemplateElementListAndElementDetailListViewModel templaterenderViewModel = null;
             try
             {
				 if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                 if (id > 0)
                 {
                     Template template= null;
                     dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession,(long)id,out template, out exception);
                     if (exception != null)
                     {
                         throw exception;
                     }
                     if ((template != null) && dbresult)
                     {
                         List<TemplateElement> templateelementist = null;
                         dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, (long)id, out templateelementist, out exception);
                         if (exception != null)
                         {
                             throw exception;
                         }
                         List<TemplateElementDetail> templateelementdetaillist = null;
                         if ((templateelementist != null) && dbresult)
                         {
                             foreach (var element in templateelementist)
                             {
                                 if (element.ElementType == ElementType.Table)
                                 {
                                     dbresult = ElementManagement.GetElementDetailListByElementId(tenantUserSession, element.Id, out templateelementdetaillist, out exception);
                                     if (exception != null)
                                     {
                                         throw exception;
                                     }
                                 }
                             }
                         }
                         templaterenderViewModel = new TemplateElementListAndElementDetailListViewModel();
                         templaterenderViewModel.template = template;
                         templaterenderViewModel.elements = templateelementist;
                         templaterenderViewModel.elementsdetails = templateelementdetaillist;
                         return this.View("~/Views/Tenants/Templates/TamplateRenderView.cshtml", templaterenderViewModel);
                     }
                     
                 }
             }
             catch (Exception ex)
             {
                 exception = ex;
                 return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = exception.Message, SuccessMessage = string.Empty });
             }
             return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = "Unable to find the following Template", SuccessMessage = string.Empty });
            
        }

        public ActionResult MaxRenderView(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool dbresult = false;
            TemplateElementListAndElementDetailListViewModel templaterenderViewModel = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                if (id > 0)
                {
                    Template template = null;
                    dbresult = ElementManagement.GetMaxVersionTemplateByTemplateId(tenantUserSession, (long)id, out template, out exception);//GetTemplateByTemplateId
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((template != null) && dbresult)
                    {
                        List<TemplateElement> templateelementist = null;
                        dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, (long)template.Id, out templateelementist, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        List<TemplateElementDetail> templateelementdetaillist = null;
                        if ((templateelementist != null) && dbresult)
                        {
                            foreach (var element in templateelementist)
                            {
                                if (element.ElementType == ElementType.Table)
                                {
                                    dbresult = ElementManagement.GetElementDetailListByElementId(tenantUserSession, element.Id, out templateelementdetaillist, out exception);
                                    if (exception != null)
                                    {
                                        throw exception;
                                    }
                                }
                            }
                        }
                        templaterenderViewModel = new TemplateElementListAndElementDetailListViewModel();
                        templaterenderViewModel.template = template;
                        templaterenderViewModel.elements = templateelementist;
                        templaterenderViewModel.elementsdetails = templateelementdetaillist;
                        return this.View("~/Views/Tenants/Templates/TamplateRenderView.cshtml", templaterenderViewModel);
                    }

                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = exception.Message, SuccessMessage = string.Empty });
            }
            return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = "Unable to find the following Template", SuccessMessage = string.Empty });

        }



    }
}