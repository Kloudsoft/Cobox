using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDiscoursesController : Controller
    {
        // GET: TenantDiscussions
        public ActionResult Index(string ErrorMessage,string SuccessMessage)
        {
            Exception exception = null;
            bool result = false;
            List<Discourse> discourses = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)){ if (exception != null) { throw exception; } }
                this.ViewBag.CurrentLoggedInUser = tenantUserSession.User;
                result = DiscourseManagement.GetAllDiscourses(tenantUserSession, out discourses, out exception);
                if (exception != null)
                {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            ViewBag.ErrorMessage = (!(string.IsNullOrEmpty(ErrorMessage))) ? ErrorMessage : (exception != null) ? ExceptionUtilities.ExceptionToString(exception) : string.Empty;
            ViewBag.SuccessMessage = (!(string.IsNullOrEmpty(SuccessMessage))) ? SuccessMessage : string.Empty;
            return View("~/Views/Tenants/Discourses/Discourses.cshtml", discourses);
        }
    }
}