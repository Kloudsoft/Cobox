using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System.Data.Entity;
namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class ScanSessionManagement
    {
        //public static bool GetAllScanSessions(TenantUserSession tenantUserSession, out List<ScanSession> scanSessionList, out Exception exception)
        //{
        //    var result = false;
        //    scanSessionList = new List<ScanSession>();
        //    exception = null;
        //    try
        //    {
        //        if (!(ScanSessionManagement.GetAllScanSessions(tenantUserSession, null, out scanSessionList,out exception))) { if (exception != null) { throw (exception); } }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //    }
        //    return result;
        //}
        //internal static bool GetAllScanSessions(TenantUserSession tenantUserSession, ContextTenant context, out List<ScanSession> scanSessionList, out Exception exception)
        //{
        //    var result = false;
        //    scanSessionList = new List<ScanSession>();
        //    exception = null;
        //    try
        //    {
        //        if (context == null)
        //        {
        //            using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //            {
        //                if (!(ScanSessionManagement.GetAllScanSessions(tenantUserSession, context, out scanSessionList))) { if (exception != null) { throw (exception); } }
        //            }
        //        }
        //        else
        //        {
        //            if (!(ScanSessionManagement.GetAllScanSessions(tenantUserSession, context, out scanSessionList))) { if (exception != null) { throw (exception); } }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //    }
        //    return result;
        //}
        //private static bool GetAllScanSessions(TenantUserSession tenantUserSession,ContextTenant context, out List<ScanSession> scanSessionList)
        //{
        //    var result = false;
        //    scanSessionList = context.ScanSessions
        //                                .Include(x => x.Documents)
        //                                .Include(x => x.Documents.Select(y => y.User))
        //                                .Include(x => x.User)
        //                                .ToList();
        //    return result;
        //}

        public static List<ScanSession> GetAllScanSessions(TenantUserSession tenantUserSession, ContextTenant context = null)
        {
            if (context == null)
            {
                using (context= new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    return (ScanSessionManagement.GetAllScanSessionsPrivate(tenantUserSession, context));
                    // Audit Trail.
                }
            }
            else
            {
                return (ScanSessionManagement.GetAllScanSessionsPrivate(tenantUserSession, context));
                // Audit Trail.
            }
        }

      

        private static List<ScanSession> GetAllScanSessionsPrivate(TenantUserSession tenantUserSession, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }
            return
            (
                context.ScanSessions
                    .Include(x => x.Documents)
                    .Include(x => x.Documents.Select(y => y.User))
                    .Include(x => x.Documents.Select(y => y.Template))
                    .Include(x => x.User)
                    .ToList()
            );
        }

        public static ScanSession EditScanSession(TenantUserSession tenantUserSession, long id, string name, string description, ContextTenant context = null)
        {
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    return (ScanSessionManagement.EditScanSessionPrivate(tenantUserSession, id, name, description, context));
                    // Audit Trail.
                }
            }
            else
            {
                return (ScanSessionManagement.EditScanSessionPrivate(tenantUserSession, id, name, description, context));
                // Audit Trail.
            }
        }

        private static ScanSession EditScanSessionPrivate(TenantUserSession tenantUserSession, long id, string name, string description, ContextTenant context)
        {
            var scanSession = context.ScanSessions.Where(x => x.Id == id).FirstOrDefault();
            if (scanSession == null) { throw (new Exception("Unable to find the following scan session")); }
            scanSession.Name = name;
            scanSession.Description = description;
            context.SaveChanges();
            return scanSession;

        }

        //public static bool GetScanSessionById(TenantUserSession tenantUserSession,  long id, out ScanSession scanSession, out Exception exception)
        //{
        //    var result = false;
        //    scanSession = new ScanSession();
        //    exception = null;
        //    try
        //    {
        //        if (!(ScanSessionManagement.GetScanSessionById(tenantUserSession, null, id, out scanSession, out exception))) { if (exception != null) { throw (exception); } }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //    }
        //    return result;
        //}
        public static ScanSession GetScanSessionById(TenantUserSession tenantUserSession, long id, ContextTenant context = null)
        {
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    return (ScanSessionManagement.GetScanSessionByIdPrivate(tenantUserSession, id, context));
                }
            }
            else
            {
                return (ScanSessionManagement.GetScanSessionByIdPrivate(tenantUserSession, id, context));
            }
        }
        private static ScanSession GetScanSessionByIdPrivate(TenantUserSession tenantUserSession, long id, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }

            return
            (
                context.ScanSessions
                    .Include(x => x.Documents)
                    .Include(x => x.Documents.Select(y => y.User))
                    .Include(x => x.Documents.Select(y => y.Template))
                    .Include(x => x.User)
                    .Where(x => x.Id == id)
                    .FirstOrDefault()
            );
        }

        public static ScanSession CreateScanSessionEntry(TenantUserSession tenantUserSession, string name, string description, ContextTenant context = null)
        {
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    return (ScanSessionManagement.CreateScanSessionEntryPrivate(tenantUserSession, name, description, context));
                }
            }
            else
            {
                return (ScanSessionManagement.CreateScanSessionEntryPrivate(tenantUserSession, name, description, context));
            }
        }
        private static ScanSession CreateScanSessionEntryPrivate(TenantUserSession tenantUserSession, string name, string description, ContextTenant context )
        {
            if (context == null) { throw (new ArgumentNullException("context")); }
            if (string.IsNullOrEmpty(name.Trim())) { throw (new Exception("Name is required")); }
            if (context.ScanSessions.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim())) { throw new Exception("Scan Session with same name already exists. Please select a unique name."); }

            ScanSession scanSession = new ScanSession();
            scanSession.DateTimeCreated = DateTime.UtcNow;
            scanSession.Description = description;
            scanSession.Guid = Guid.NewGuid();
            scanSession.Name = name;
            scanSession.UserId = tenantUserSession.User.Id;
            var scan = context.ScanSessions.Add(scanSession);
            context.SaveChanges();
            return scan;
        }

        public static ScanSession ScanSessionEntryFinalize(TenantUserSession tenantUserSession, long scanSessionId,ContextTenant context= null)
        {
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    return (ScanSessionManagement.ScanSessionEntryFinalizePrivate(tenantUserSession, scanSessionId,context));
                }
            }
            else
            {
                return (ScanSessionManagement.ScanSessionEntryFinalizePrivate(tenantUserSession, scanSessionId, context));
            }
        }

        private static ScanSession ScanSessionEntryFinalizePrivate(TenantUserSession tenantUserSession, long scanSessionId, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }
            if (scanSessionId<=0) { throw (new Exception("Unable to find the following scan session")); }

            ScanSession scanSession = context.ScanSessions.Where(x => x.Id == scanSessionId).FirstOrDefault();
            if (scanSession != null) { throw (new Exception("Unable to find the following scan session")); }
            scanSession.Finalized = true;
            scanSession = context.ScanSessions.Add(scanSession);
            context.SaveChanges();
            return scanSession;
        }
    }
}
