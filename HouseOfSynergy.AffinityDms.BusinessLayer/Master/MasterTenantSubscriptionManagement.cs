using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.BusinessLayer
{
    public static class MasterTenantSubscriptionManagement
    {
        public static bool GetTenantSubscriptionById(MasterUserSession MasterUserSession, long id, out TenantSubscription tenantsubscription, out Exception exception)
        {
            var result = false;

            tenantsubscription = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    tenantsubscription = context.TenantSubscriptions.AsNoTracking().SingleOrDefault(t => t.Id == id);

                    if (tenantsubscription == null) { throw (new RowNotFoundException()); }

                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        public static bool GetTenantSubscriptionById(MasterUserSession MasterUserSession, long id, out List<TenantSubscription> tenantsubscription, out Exception exception)
        {
            var result = false;

            tenantsubscription = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    tenantsubscription = context.TenantSubscriptions.AsNoTracking().Where(x => x.Id == id).OrderByDescending(x => x.Id).ToList();
                    if (tenantsubscription == null) { throw (new RowNotFoundException()); }
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool GetActiveTenantSubscriptionById(MasterUserSession MasterUserSession, long id, out TenantSubscription tenantsubscription, out Exception exception)
        {
            var result = false;

            tenantsubscription = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    tenantsubscription = context.TenantSubscriptions.AsNoTracking().Where(x => ((x.TenantId == id) && (x.IsActive == true))).FirstOrDefault();
                    if (tenantsubscription == null) { tenantsubscription = new TenantSubscription(); }//throw (new RowNotFoundException()); }
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool GetTenantSubcriptionsByTenantId(MasterUserSession MasterUserSession, long tenantid, out List<TenantSubscription> tenantsubscriptions, out Exception exception)
        {
            var result = false;

            tenantsubscriptions = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    //todo
                    tenantsubscriptions = context.TenantSubscriptions.AsNoTracking().ToArray().Where(t => t.TenantId == tenantid).OrderByDescending(x => x.Id).ToList();

                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        public static bool GetTenantSubcriptions(MasterUserSession MasterUserSession, out List<TenantSubscription> tenantsubscriptions, out Exception exception)
        {
            var result = false;

            tenantsubscriptions = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    //todo
                    tenantsubscriptions = context.TenantSubscriptions.AsNoTracking().ToList();

                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool CreateTenantSubscription(MasterUserSession MasterUserSession, TenantSubscription tenantsubscription, out TenantSubscription outtenantsubscription, out Exception exception)
        {
            var result = false;

            //tenant = null;
            outtenantsubscription = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    DbContextTransaction mastertrans = null;
                    DbContextTransaction tenanttrans = null;
                    try
                    {
                        using (mastertrans = context.Database.BeginTransaction())
                        {
                            outtenantsubscription = context.TenantSubscriptions.Add(tenantsubscription);
                            context.SaveChanges();
                            var tenant = context.Tenants.Where(x => x.Id == tenantsubscription.TenantId).FirstOrDefault();
                            var subscription = context.Subscriptions.Where(x => x.Id == tenantsubscription.SubscriptionId).FirstOrDefault();
                            using (var contexttenant = new ContextTenant(tenant.DatabaseConnectionString))
                            {
                                try
                                {
                                    using (tenanttrans = contexttenant.Database.BeginTransaction())
                                    {
                                        var mastersubscriptionid = subscription.Id;
                                        subscription.Id = 0;
                                        contexttenant.Subscriptions.Add(subscription);
                                        contexttenant.SaveChanges();
                                        tenantsubscription.SubscriptionId = subscription.Id;
                                        contexttenant.TenantSubscriptions.Add(tenantsubscription);
                                        contexttenant.SaveChanges();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    exception = ex;
                                    throw ex;
                                }
                            }
                            tenanttrans.Commit();
                            mastertrans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (mastertrans != null)
                        {
                            mastertrans.Rollback();
                            mastertrans.Dispose();
                        }
                        if (tenanttrans != null)
                        {
                            tenanttrans.Rollback();
                            tenanttrans.Dispose();
                        }
                        exception = ex;
                        throw ex;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return (result);
        }

        public static bool UpdateTenantSubscription(MasterUserSession MasterUserSession, TenantSubscription tenantsubscription, out TenantSubscription outtenantsubscription, out Exception exception)
        {
            var result = false;
            outtenantsubscription = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    var temp = new TenantSubscription();

                    temp = context.TenantSubscriptions.FirstOrDefault(t => t.Id == tenantsubscription.Id);

                    temp.IsDemo = tenantsubscription.IsDemo;
                    temp.IsActive = tenantsubscription.IsActive;

                    temp.DateTimeStart = tenantsubscription.DateTimeStart;
                    temp.DateTimeExpires = tenantsubscription.DateTimeExpires;

                    temp.NumberOfFormsAllowed = tenantsubscription.NumberOfFormsAllowed;
                    temp.NumberOfUsersAllowed = tenantsubscription.NumberOfUsersAllowed;
                    temp.NumberOfPagesAllowed = tenantsubscription.NumberOfPagesAllowed;
                    temp.NumberOfTemplatesAllowed = tenantsubscription.NumberOfTemplatesAllowed;

                    temp.NumberOfFormsUsed = tenantsubscription.NumberOfFormsUsed;
                    temp.NumberOfPagesUsed = tenantsubscription.NumberOfPagesUsed;
                    temp.NumberOfUsersUsed = tenantsubscription.NumberOfUsersUsed;
                    temp.NumberOfTemplatesUsed = tenantsubscription.NumberOfTemplatesUsed;

                    temp.AllowScanning = tenantsubscription.AllowScanning;
                    temp.AllowBranding = tenantsubscription.AllowBranding;
                    temp.AllowTemplateWorkflows = tenantsubscription.AllowTemplateWorkflows;

                    context.TenantSubscriptions.Attach(temp);
                    context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    outtenantsubscription = temp;
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool CreateTenantSubscriptionAndDeactivateExistingTenantSubscription(MasterUserSession MasterUserSession, long existingtenantsubscriptionid, Subscription subscriptionMaster, TenantSubscription tenantsubscriptionMaster, out Exception exception)
        {
            var result = false;
            exception = null;
            //System.Data.Common.DbTransaction Tran = null;
            //ContextMaster context = new ContextMaster();
            //context.ObjectContext.Connection.Open();
            //Tran = context.ObjectContext.Connection.BeginTransaction();
            DbContextTransaction masterTrans = null;
            DbContextTransaction tenantTrans = null;
            using ( var contextMaster = new ContextMaster())
            {
                try
                {
                    using (masterTrans = contextMaster.Database.BeginTransaction())
                    {

                        try
                        {
                            var existingSubscriptions = contextMaster.TenantSubscriptions.Where(x => (x.TenantId == existingtenantsubscriptionid) && (x.IsActive == true)).ToList();

                            foreach (var existingSubnscription in existingSubscriptions)
                            {
                                existingSubnscription.IsActive = false;
                                contextMaster.SaveChanges();
                            }

                            contextMaster.TenantSubscriptions.Add(tenantsubscriptionMaster);
                            contextMaster.SaveChanges();
                            tenantsubscriptionMaster.MasterTenantSubscriptionId = tenantsubscriptionMaster.Id;
                            contextMaster.Entry(tenantsubscriptionMaster).State = EntityState.Modified;
                            contextMaster.SaveChanges();

                            var tenant = contextMaster.Tenants.SingleOrDefault(x => x.Id == tenantsubscriptionMaster.TenantId);

                            if (tenant == null) { throw (new Exception("Unable to Find Tenant in the Master DB.")); }

                            using (var contextTenant = new ContextTenant(tenant.DatabaseConnectionString))
                            {
                                try
                                {
                                    using (tenantTrans = contextTenant.Database.BeginTransaction())
                                    {
                                        var existingtenantsubscriptionstenant = contextTenant.TenantSubscriptions.Where(x => (x.IsActive == true)).ToList();

                                        foreach (var existingSubscriptionTenant in existingtenantsubscriptionstenant)
                                        {
                                            existingSubscriptionTenant.IsActive = false;
                                            contextTenant.SaveChanges();
                                        }

                                        var subscriptionTenant = contextTenant.Subscriptions.SingleOrDefault(s => s.MasterSubscriptionId == subscriptionMaster.Id);

                                        if (subscriptionTenant == null)
                                        {
                                            subscriptionTenant = subscriptionMaster.Clone();
                                            subscriptionTenant.Id = 0;
                                            subscriptionTenant.MasterSubscriptionId = subscriptionMaster.Id;
                                            subscriptionTenant.SubscriptionType = EntityMasterTenantType.Tenant;
                                            contextTenant.Subscriptions.Add(subscriptionTenant);
                                            contextTenant.SaveChanges();
                                        }
                                        else
                                        {
                                            //subscriptionTenant = subscription.Clone();
                                            //subscriptionTenant.MasterTenantId = subscription.Id;//Yeh Maam Nay kerwaya hai.
                                            //contextTenant.Entry(subscriptionTenant).State = EntityState.Modified;
                                            //contextTenant.SaveChanges();
                                        }

                                        var count = contextTenant.Tenants.Count();
                                        if (count != 1) { throw (new Exception($"The number of tenant entries found in the Tenant database was {count} instead of 1.")); }
                                        var tenantTenant = contextTenant.Tenants.Single();

                                            var tenantsubscriptionTenant = tenantsubscriptionMaster;
                                        var masterTenantSubscriptionId = contextMaster.TenantSubscriptions.AsNoTracking().Where(x => x.IsActive == true).FirstOrDefault().Id;
                                        tenantsubscriptionTenant.MasterTenantSubscriptionId = tenantsubscriptionMaster.Id;
                                        tenantsubscriptionTenant.Id = 0;
                                        tenantsubscriptionTenant.SubscriptionId = subscriptionTenant.Id;
                                        tenantsubscriptionTenant.Tenant = null;
                                        tenantsubscriptionTenant.TenantId = tenantTenant.Id;
                                        tenantsubscriptionTenant.TenantSubscriptionType = EntityMasterTenantType.Tenant;
                                        contextTenant.TenantSubscriptions.Add(tenantsubscriptionTenant);
                                        contextTenant.SaveChanges();


                                        

                                        try

                                        {
                                            tenantTrans.Commit();
                                            masterTrans.Commit();
                                        }
                                        catch
                                        {
                                            tenantTrans.Rollback();
                                            masterTrans.Rollback();
                                        }

                                        result = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    exception = ex;
                                    throw;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            exception = ex;
                            throw;
                            //Tran.Rollback();
                            //Tran.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    masterTrans.Rollback();
                    tenantTrans.Rollback();
                    masterTrans.Dispose();
                    tenantTrans.Dispose();
                }
            }

            return (result);
        }

    }
}