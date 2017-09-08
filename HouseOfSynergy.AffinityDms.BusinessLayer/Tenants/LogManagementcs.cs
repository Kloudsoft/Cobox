using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Utility;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Utilities;
using System.Text.RegularExpressions;
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class LogManagementcs
    {
        public static bool AddLog(TenantUserSession tenantUserSession, Log auditlog, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {

                    if (auditlog != null)
                    {
                        context.Logs.Add(auditlog);
                        context.SaveChanges();

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool GetVendorById(TenantUserSession tenantUserSession, int DocumentId, out Log auditlog, out Exception exception)
        {
            exception = null;
            auditlog = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    //context.Configuration.LazyLoadingEnabled = true;
                    auditlog = context.Logs.Where(e => e.Id == DocumentId).FirstOrDefault<Log>();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

    }
}
