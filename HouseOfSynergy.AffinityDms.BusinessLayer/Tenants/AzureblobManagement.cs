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
    public static class AzureblobManagement
    {
        public static bool AddBlob(TenantUserSession tenantUserSession, azureblob azureblob, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {

                    if (azureblob != null)
                    {
                        context.azureblobs.Add(azureblob);
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

        public static bool GetBlobById(TenantUserSession tenantUserSession, long Id, out azureblob azureblob, out Exception exception)
        {

            exception = null;
            azureblob = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    //context.Configuration.LazyLoadingEnabled = true;
                    azureblob = context.azureblobs.Where(e => e.Id == Id).FirstOrDefault<azureblob>();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool GetBlobByBatchId(TenantUserSession tenantUserSession, string BlobId, out azureblob azureblob, out Exception exception)
        {

            exception = null;
            azureblob = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    //context.Configuration.LazyLoadingEnabled = true;
                    azureblob = context.azureblobs.Where(e => e.batchno == BlobId).FirstOrDefault<azureblob>();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool GetAllBlobs(TenantUserSession tenantUserSession, out List<azureblob> azurebloblist, out Exception exception)
        {

            bool result = false;
            azurebloblist = null;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    //VendorList = context.Vendors.Select(a => a).ToList();
                    azurebloblist = context.azureblobs.Where(x=>x.status==0).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        public static bool UpdateBlobbatchstatus(TenantUserSession tenantUserSession, azureblob azureblob, out Exception exception)
        {

            exception = null;
            bool result = false;
            try
            {
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                azureblob temp = context.azureblobs.Where(x => x.batchno == azureblob.batchno).Select(x => x).FirstOrDefault();
                temp.status = azureblob.status;
                context.azureblobs.Add(temp);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }


    }
}
