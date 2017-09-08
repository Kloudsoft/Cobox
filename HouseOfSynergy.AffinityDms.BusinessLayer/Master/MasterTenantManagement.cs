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
using System.Data.SqlClient;

namespace HouseOfSynergy.AffinityDms.BusinessLayer
{
	public static class MasterTenantManagement
	{
		public static bool GetTenantById (long id, out Tenant tenant, out Exception exception)
		{
			var result = false;

			tenant = null;
			exception = null;

			try
			{
				using (var context = new ContextMaster())
				{
					tenant = context.Tenants.AsNoTracking().SingleOrDefault(t => t.Id == id);

					if (tenant == null) { throw (new RowNotFoundException()); }

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool GetTenantByDomain (string domain, out Tenant tenant, out Exception exception)
		{
			var result = false;

			tenant = null;
			exception = null;

			try
			{
				using (var context = new ContextMaster())
				{
					tenant = context.Tenants.AsNoTracking().Include(t => t.TenantSubscriptions).SingleOrDefault(t => t.Domain == domain);

					if (tenant == null) { throw (new DomainNotFoundException()); }

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool GetTenants (out List<Tenant> tenants, out Exception exception)
		{
			var result = false;

			tenants = null;
			exception = null;

			try
			{
				using (var context = new ContextMaster())
				{
					tenants = context.Tenants.AsNoTracking().Include(t => t.TenantSubscriptions).ToList();

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

        public static bool CreateTenant(MasterUserSession tenantUserSession,Tenant tenant, out Tenant tenantMaster, out Exception exception)
        {
            var result = false;
			Tenant tenantMasterTemp = null;

            exception = null;
            tenantMaster = null;

            try
            {

                using (var context = new ContextMaster())
                {
					using (var transaction = context.Database.BeginTransaction())
					{
						try
						{
                            var count = context.Tenants.Count(t => t.Domain.ToLower() == tenant.Domain.ToLower());
                            if (count == 1) { throw (new Exception("The provided domain already exists in the system.")); }
                            else if (count > 1) { throw (new Exception("The provided domain exists multiple times in the system.")); }

                            tenant.DatabaseConnectionString = AzureDBConnectionStringBuilder(tenant.DatabaseConnectionString);
                            tenantMasterTemp = context.Tenants.Add(tenant);
                            context.SaveChanges();
                            tenantMasterTemp.MasterTenantId = tenantMasterTemp.Id;
                            context.Entry(tenantMasterTemp).State = EntityState.Modified;
                            context.SaveChanges();
                            if (tenantMasterTemp != null)
							{
								var tenantTenant = tenantMasterTemp.Clone ();

								tenantTenant.Id = 0;
								tenantTenant.MasterTenantId = tenant.Id;
								tenantTenant.TenantType = EntityMasterTenantType.Tenant;
                                tenantTenant.AuthenticationType = AuthenticationType.None;
                                ContextTenant.Initialize(tenantTenant.DatabaseConnectionString);

								// Commented out by Raheel to find a better way to automate the process.
								//using (var contexttenant = new ContextTenant (tenantTenant.DatabaseConnectionString))
								//{
								//	bool seederresult = DataLayer.Seeders.SeederTenant.Seed (contexttenant, tenantTenant, out exception);

								//	if (seederresult)
								//	{
								//		transaction.Commit ();

								//		tenantMaster = tenantMasterTemp;
								//	}
								//	else
								//	{
								//		transaction.Rollback ();
								//	}
								//}
							}
						}
						catch (Exception ex)
						{
							exception = ex;
							transaction.Rollback();
                            tenantMaster = tenant;
                        }
					}
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
                tenantMaster = tenant;
            }

            return (result);
        }

        public static bool UpdateTenant(MasterUserSession tenantUserSession, Tenant tenant, out Tenant outtenant, out Exception exception)
        {
            var result = false;
            outtenant = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    var count = context.Tenants.Count(t => t.Id == tenant.Id);
                    if (count == 0) { throw (new Exception("The provided tenant Id was not found.")); }

                    count = context.Tenants.Count(t => ((t.Id != tenant.Id) && (t.Domain.ToLower() == tenant.Domain.ToLower())));
                    if (count == 1) { throw (new Exception("The provided domain already exists in the system.")); }
                    else if (count > 1) { throw (new Exception("The provided domain exists multiple times in the system.")); }

                    var tempTenant = context.Tenants.Single(t => t.Id == tenant.Id);

                    tempTenant.CompanyName = tenant.CompanyName;
                    tempTenant.Domain = tenant.Domain;
                    tempTenant.ContactOwnerNameGiven = tenant.ContactOwnerNameGiven;
                    tempTenant.ContactOwnerNameFamily = tenant.ContactOwnerNameFamily;
                    tempTenant.ContactOwnerAddress = tenant.ContactOwnerAddress;
                    tempTenant.ContactOwnerCity = tenant.ContactOwnerCity;
                    tempTenant.ContactOwnerState = tenant.ContactOwnerState;
                    tempTenant.ContactOwnerZipCode = tenant.ContactOwnerZipCode;
                    tempTenant.ContactOwnerCountry = tenant.ContactOwnerCountry;
                    tempTenant.ContactOwnerPhone = tenant.ContactOwnerPhone;
                    tempTenant.ContactOwnerFax = tenant.ContactOwnerFax;
                    tempTenant.ContactOwnerEmail = tenant.ContactOwnerEmail;
                    tempTenant.ContactAdministratorNameGiven = tenant.ContactAdministratorNameGiven;
                    tempTenant.ContactAdministratorNameFamily = tenant.ContactAdministratorNameFamily;
                    tempTenant.ContactAdministratorAddress = tenant.ContactAdministratorAddress;
                    tempTenant.ContactAdministratorCity = tenant.ContactAdministratorCity;
                    tempTenant.ContactAdministratorState = tenant.ContactAdministratorState;
                    tempTenant.ContactAdministratorZipCode = tenant.ContactAdministratorZipCode;
                    tempTenant.ContactAdministratorCountry = tenant.ContactAdministratorCountry;
                    tempTenant.ContactAdministratorPhone = tenant.ContactAdministratorPhone;
                    tempTenant.ContactAdministratorFax = tenant.ContactAdministratorFax;
                    tempTenant.ContactAdministratorEmail = tenant.ContactAdministratorEmail;
                    tempTenant.ContactBillingNameGiven = tenant.ContactBillingNameGiven;
                    tempTenant.ContactBillingNameFamily = tenant.ContactBillingNameFamily;
                    tempTenant.ContactBillingAddress = tenant.ContactBillingAddress;
                    tempTenant.ContactBillingCity = tenant.ContactBillingCity;
                    tempTenant.ContactBillingState = tenant.ContactBillingState;
                    tempTenant.ContactBillingZipCode = tenant.ContactBillingZipCode;
                    tempTenant.ContactBillingCountry = tenant.ContactBillingCountry;
                    tempTenant.ContactBillingPhone = tenant.ContactBillingPhone;
                    tempTenant.ContactBillingFax = tenant.ContactBillingFax;
                    tempTenant.ContactBillingEmail = tenant.ContactBillingEmail;
                    tempTenant.ContactTechnicalNameGiven = tenant.ContactTechnicalNameGiven;
                    tempTenant.ContactTechnicalNameFamily = tenant.ContactTechnicalNameFamily;
                    tempTenant.ContactTechnicalAddress = tenant.ContactTechnicalAddress;
                    tempTenant.ContactTechnicalCity = tenant.ContactTechnicalCity;
                    tempTenant.ContactTechnicalState = tenant.ContactTechnicalState;
                    tempTenant.ContactTechnicalZipCode = tenant.ContactTechnicalZipCode;
                    tempTenant.ContactTechnicalCountry = tenant.ContactTechnicalCountry;
                    tempTenant.ContactTechnicalPhone = tenant.ContactTechnicalPhone;
                    tempTenant.ContactTechnicalFax = tenant.ContactTechnicalFax;
                    tempTenant.ContactTechnicalEmail = tenant.ContactTechnicalEmail;

                    context.Tenants.Attach(tempTenant);
                    //context.Entry(tempTenant).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    outtenant = tempTenant;
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
                outtenant = tenant;
            }

            return (result);
        }

        private static string AzureDBConnectionStringBuilder(string dbName)
        {
            var builder = new SqlConnectionStringBuilder();

            //builder.ConnectionString
            //                = $@"Server=tcp:kloudsoft-master-portal-dbserver.database.windows.net,1433;
            //                Database={dbName};
            //                User ID=kmp-admin@kloudsoft-master-portal-dbserver;
            //                Password=Audience123;
            //                Encrypt=True;
            //                TrustServerCertificate=False;
            //                MultipleActiveResultSets=True;
            //                Connection Timeout=30;";

            builder.ConnectionString
                = @"Server=tcp:affinity-ecm-dbserver.database.windows.net,1433;"//tcp:kloudsoft-master-portal-dbserver.database.windows.net,1433;"
                + @"Database="+ dbName + ";"
                + @"User ID=kloudsoft-admin;"//kmp-admin;"
                + @"Password=KLS@dm1nKLS@dm1n;"//Audience123;"
                + @"Encrypt=True;"
                + @"TrustServerCertificate=False;"
                + @"MultipleActiveResultSets=True;"
                + @"Connection Timeout=300;";


            return builder.ToString();
        }

	}
}