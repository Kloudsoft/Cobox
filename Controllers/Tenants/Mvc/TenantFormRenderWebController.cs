using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantFormRenderWebController : Controller
    {
        // GET: TenantFormRenderWeb
        public ActionResult Index(long? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<TemplateElement> elementslist = new List<TemplateElement>();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            try
            {
                if (id > 0)
                {
                    long ID = (long)id;
                    List<TemplateElement> elements = null;
                    bool dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                    if (elements.Count > 0 && dbresult)
                    {
                        TemplateAndFormHelper.GetListOfFormElements(tenantUserSession, elements, out elementslist, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        return View("~/Views/Tenants/Forms/FormRenderWeb.cshtml", elementslist);
                    }
                    else
                    {
                        elementslist = new List<TemplateElement>();
                        return View("~/Views/Tenants/Forms/FormRenderWeb.cshtml", elementslist);
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
            }
            return RedirectToAction("Index", "TenantForms", new { ErrorMessage = ((exception!=null)? (exception.Message):(string.Empty)), SuccessMessage = string.Empty });
        }
       
    }
}