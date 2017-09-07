using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDepartmentController : Controller
    {
        // GET: TenantDepartent
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AddDepartment(string departmentName)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            Department department = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                if (!DepartmentManagement.AddDepartment(tenantUserSession, departmentName, out department, out exception)) { if (exception != null) { throw exception; } }
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(ex.Message.ToString(),JsonRequestBehavior.AllowGet);
            }
            return Json(department,JsonRequestBehavior.AllowGet);
        }

    }
}