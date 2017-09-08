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
    
    public static class VendorManagement
    {
        public static bool AddVendor(TenantUserSession tenantUserSession, Vendor vendor, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {

                    if (vendor != null)
                    {
                        context.Vendors.Add(vendor);
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


        public static bool GetVendorById(TenantUserSession tenantUserSession, int VendorId, out Vendor vendor, out Exception exception)
        {
            exception = null;
            vendor = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    //context.Configuration.LazyLoadingEnabled = true;
                    vendor = context.Vendors.Where(e => e.Id == VendorId).FirstOrDefault<Vendor>();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool GetAllVendors(TenantUserSession tenantUserSession, out List<Vendor> VendorList, out Exception exception)
        {
            bool result = false;
            VendorList = null;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    //VendorList = context.Vendors.Select(a => a).ToList();
                    VendorList = context.Vendors.AsNoTracking().ToList();
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

        public static bool UpdateVendor(TenantUserSession tenantUserSession, Vendor vendor, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                Vendor temp = context.Vendors.Where(x => x.Id == vendor.Id).Select(x => x).FirstOrDefault();
                temp.VendorName = vendor.VendorName;
                temp.Gst = vendor.Gst;
                temp.Address = vendor.Address;
                temp.Phone = vendor.Phone;
                temp.Email = vendor.Email;
                temp.ContactPerson = vendor.ContactPerson;
                context.Vendors.Add(temp);
                context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
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


        public static bool DeleteVendor(TenantUserSession tenantUserSession, Vendor vendor, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                Vendor temp = context.Vendors.Where(x => x.Id == vendor.Id).Select(x => x).FirstOrDefault();
                context.Vendors.Remove(temp);
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
