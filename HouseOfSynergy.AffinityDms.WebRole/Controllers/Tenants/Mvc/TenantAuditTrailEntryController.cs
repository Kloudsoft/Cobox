using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;


namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantAuditTrailEntryController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {

            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            try
            {

                {
                    ContextTenant context = null;
                    List<AuditTrailEntry> audittrailentry = null;
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        AuditTrailManagement.GetAllAuditTrail(tenantUserSession, context, out audittrailentry);

                    }
                    var model = audittrailentry;

                    return (this.View("~/Views/Tenants/AuditTrailEntry/TenantAuditTrailEntries.cshtml", model));
                }



            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        [HttpPost]
        public ActionResult Index(DateTime DateTo, DateTime DateFrom)
        {

            if ((DateFrom > DateTo))
            {
                throw new Exception("DateTimeFrom cannot be less than DateTimeTo ");

            }
            else if ((DateTo < DateFrom))
            {
                throw new Exception("DateTimeTo cannot be less than DateTimeFrom ");

            }


            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            try
            {

                {
                    ContextTenant context = null;
                    List<AuditTrailEntry> audittrailentry = null;
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        AuditTrailManagement.GetAuditTrailByDateRange(tenantUserSession, context, DateTo, DateFrom, out audittrailentry);

                    }
                    var model = audittrailentry;

                    return (this.View("~/Views/Tenants/AuditTrailEntry/TenantAuditTrailEntries.cshtml", model));
                }



            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }



    }
}