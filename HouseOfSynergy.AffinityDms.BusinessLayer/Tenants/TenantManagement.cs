using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using System.Security.Cryptography;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
	public static class TenantManagement
	{
		public static bool GetPublicKey (TenantUserSession tenantUserSession, long tenandId, out string keyPublic, out Exception exception)
		{
			var result = false;

			keyPublic = "";
			exception = null;

			try
			{
				using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
				{
					var tenant = context.Tenants.SingleOrDefault(t => (t.Id == tenandId));

					if (tenant == null)
					{
						throw (new RowNotFoundException());
					}
					else
					{
						keyPublic = tenant.RsaKeyPublic;

						result = true;
					}
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		/// <summary>
		/// This function should not be called from the Master App.
		/// </summary>
		/// <param name="tenant"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		public static bool GetTenantSelf (TenantUserSession tenantUserSession, out Tenant tenant, out Exception exception)
		{
			var result = false;

			tenant = null;
			exception = null;

			try
			{
				using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
				{
					tenant = context.Tenants.Single();

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		//public static bool DeleteSubscription(int id)
		//{
		//    using (var context = new ContextTenant())
		//    {
		//        context.Subscription.Remove(context.Subscription.Find(s => s.Id == id).FirstOrDefault());
		//    }
		//    return subscription;
		//}
		public static Subscription GetSubscriptionById (TenantUserSession tenantUserSession, int id)
		{
			Subscription subscription = null;
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				subscription = context.Subscriptions.Where(r => r.Id == id).FirstOrDefault();

			}
			return subscription;
		}

		public static TenantSubscription GetSubscriptionByTenantId (TenantUserSession tenantUserSession, int id)
		{
			TenantSubscription subscription = null;
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				subscription = context.TenantSubscriptions.Where(r => r.TenantId == id && r.IsActive == true).FirstOrDefault();

			}
			return subscription;
		}

		public static Tenant GetTenantInfo (TenantUserSession tenantUserSession, int id)
		{
			Tenant Tenant = null;
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				Tenant = context.Tenants.Where(r => r.Id == id).FirstOrDefault();

			}
			return Tenant;
		}
		public static int SaveSubscription (TenantUserSession tenantUserSession, Subscription subscription)
		{

			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				if (subscription.Id < 1)
				{
					subscription = context.Subscriptions.Add(subscription);
					context.SaveChanges();
				}
				else
				{
					context.Subscriptions.Attach(subscription);
					context.Entry(subscription).State = System.Data.Entity.EntityState.Modified;
					context.SaveChanges();
				}
			}
			return Convert.ToInt32(subscription.Id);
		}

		public static int AddTenant (TenantUserSession tenantUserSession, Tenant tenant)
		{
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				tenant = context.Tenants.Add(tenant);
				context.SaveChanges();
			}
			return Convert.ToInt32(tenant.Id);
		}


		public static int SaveTenantSubscription (TenantUserSession tenantUserSession, TenantSubscription subscribtion)
		{
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				subscribtion.Subscription = context.Subscriptions.Where(s => s.Id == subscribtion.Subscription.Id).FirstOrDefault();
				subscribtion.NumberOfFormsAllowed = subscribtion.Subscription.NumberOfFormsAllowed;
				subscribtion.NumberOfFormsUsed = subscribtion.Subscription.NumberOfFormsUsed;
				subscribtion.NumberOfPagesAllowed = subscribtion.Subscription.NumberOfPagesAllowed;
				subscribtion.NumberOfPagesUsed = subscribtion.Subscription.NumberOfPagesUsed;
				subscribtion.NumberOfTemplatesAllowed = subscribtion.Subscription.NumberOfTemplatesAllowed;
				subscribtion.NumberOfUsersAllowed = subscribtion.Subscription.NumberOfUsersAllowed;
				subscribtion.NumberOfUsersUsed = subscribtion.Subscription.NumberOfUsersUsed;

				subscribtion = context.TenantSubscriptions.Add(subscribtion);
				context.SaveChanges();
			}
			return Convert.ToInt32(subscribtion.TenantId);
		}

		public static IList<Tenant> GetTenants (TenantUserSession tenantUserSession)
		{
			IList<Tenant> list = new List<Tenant>();
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				list = context.Tenants.ToList();
			}
			return list;
		}

		public static IList<Subscription> GetSubscriptions (TenantUserSession tenantUserSession)
		{
			IList<Subscription> list = new List<Subscription>();
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				list = context.Subscriptions.ToList();
			}
			return list;
		}

		public static IList<TenantSubscription> GetPreviousSubscriptions (TenantUserSession tenantUserSession, int tenantId)
		{
			var list = new List<TenantSubscription>();

			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				list = context.TenantSubscriptions.Where(t => ((t.TenantId == tenantId) && (t.IsActive == false))).ToList();
			}

			return list;
		}

		public static int UpdateTenant (TenantUserSession tenantUserSession, Tenant tenant)
		{
			using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
			{
				var tempTenant = new Tenant();

				tempTenant = context.Tenants.FirstOrDefault(t => t.Id == tenant.Id);

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
				context.Entry(tempTenant).State = System.Data.Entity.EntityState.Modified;
				context.SaveChanges();
			}
			return Convert.ToInt32(tenant.Id);
		}
	}
}