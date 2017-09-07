using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantFoldersController : Controller
    {
        // GET: TenantFolder
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SetUserRights(List<long> UserList, long Id)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                result = TenantUserManagement.UserRightsForFolders(tenantUserSession, Id, UserList, out exception);
                if (!result) { if (exception != null) { throw exception; } }
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(exception);
            }
            return Json(result);
        }

    }

}