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
    public class TenantFormRenderPrintController : Controller
    {
        // GET: TenantFormRenderPrint
        public ActionResult Index(long?id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            List<TemplateElement> elements = null;

            try
            {
                if (id > 0)
                {
                    long ID = long.Parse(id.ToString());
                    bool dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (elements.Count > 0 && dbresult)
                    {
                        List<TemplateElement> elementslist = null;
                        TemplateAndFormHelper.GetListOfFormElements(tenantUserSession,elements, out elementslist, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        return View("~/Views/Tenants/Forms/FormRenderPrint.cshtml", elementslist);
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
            return RedirectToAction("Index", "TenantForms", new { ErrorMessage = ((exception != null) ? (exception.Message) : (string.Empty)), SuccessMessage = string.Empty });
        }
        
    }
}