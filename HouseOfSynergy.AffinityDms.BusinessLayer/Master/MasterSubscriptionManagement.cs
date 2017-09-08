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

namespace HouseOfSynergy.AffinityDms.BusinessLayer
{
    public static class MasterSubscriptionManagement
	{
        public static bool GetSubscriptionById(MasterUserSession MasterUserSession, long id, out Subscription subscription, out Exception exception)
		{
			var result = false;

            subscription = null;
			exception = null;

			try
			{
				using (var context = new ContextMaster())
				{
                    subscription = context.Subscriptions.AsNoTracking().SingleOrDefault(t => t.Id == id);

                    if (subscription == null) { throw (new RowNotFoundException()); }

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

        public static bool GetSubcriptions(MasterUserSession MasterUserSession, out List<Subscription> subscriptions, out Exception exception)
		{
			var result = false;

            subscriptions = null;
			exception = null;

			try
			{
				using (var context = new ContextMaster())
				{
                    subscriptions = context.Subscriptions.AsNoTracking().ToList();

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

        public static bool CreateSubscription(MasterUserSession MasterUserSession, Subscription subscription, out Subscription outsubscription, out Exception exception)
        {
            var result = false;

            //tenant = null;
            outsubscription = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            outsubscription = context.Subscriptions.Add(subscription);
                            context.SaveChanges();
                            subscription.MasterSubscriptionId = subscription.Id;
                            context.SaveChanges();

                            transaction.Commit();

                            result = true;
                        }
                        catch (Exception e)
                        {
                            exception = e;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool UpdateSubscription(MasterUserSession MasterUserSession, Subscription subscription, out Subscription outsubscription, out Exception exception)
        {
            var result = false;
            outsubscription = null;
            exception = null;

            try
            {
                using (var context = new ContextMaster())
                {
                    var temp = new Subscription();

                    temp = context.Subscriptions.FirstOrDefault(t => t.Id == subscription.Id);

                    temp.Description = subscription.Description;

                    temp.IsActive = subscription.IsActive;
                    temp.IsDemo = subscription.IsDemo;

                    temp.NumberOfFormsAllowed = subscription.NumberOfFormsAllowed;
                    temp.NumberOfPagesAllowed = subscription.NumberOfPagesAllowed;
                    temp.NumberOfUsersAllowed = subscription.NumberOfUsersAllowed;
                    temp.NumberOfTemplatesAllowed = subscription.NumberOfTemplatesAllowed;

                    temp.NumberOfFormsUsed = subscription.NumberOfFormsUsed;
                    temp.NumberOfPagesUsed = subscription.NumberOfPagesUsed;
                    temp.NumberOfUsersUsed = subscription.NumberOfUsersUsed; 
                    temp.NumberOfTemplatesUsed = subscription.NumberOfTemplatesUsed;

                    temp.AllowScanning = subscription.AllowScanning;
                    temp.AllowBranding = subscription.AllowBranding;
                    temp.AllowTemplateWorkflows = subscription.AllowTemplateWorkflows;

                    context.Subscriptions.Attach(temp);
                    context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    outsubscription = temp;
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool GetMasterUserByIdTemporaryFunction(out HouseOfSynergy.AffinityDms.Entities.Master.MasterUser user, out Exception exception)
        {
            user = null;
            exception = null;
            bool result = false;
            try
            {
                var context = new ContextMaster();
                user = context.Users.First();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
	}
}