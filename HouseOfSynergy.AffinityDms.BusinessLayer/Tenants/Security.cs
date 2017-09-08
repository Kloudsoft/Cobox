using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class Security
    {
		public static ICollection<Button> GetAvailableScreenButtons (TenantUserSession tenantUserSession, int UserId, int ScreenId)
        {
            List<Button> buttons = new List<Button>();
            List<UserRole> userRoles = new List<UserRole>();
        

            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                var query =
                    from ur in context.UserRoles
                    join rr in context.RoleRights
                    on ur.RoleId equals rr.RoleId
                    where ur.UserId == UserId && rr.Screen.Id == ScreenId
                    select new
                    {
                        buttonId = rr.Button.Id
                    };
                    foreach (var button in query)
                    {
                        buttons.Add(context.Buttons.Where(b=>b.Id == button.buttonId).First());
                    }
            }

            return buttons;
        }

		public static ICollection<Screen> GetAvailableMenus (TenantUserSession tenantUserSession, int UserId)
        {
            List<Screen> screens = new List<Screen>();
            List<UserRole> userRoles = new List<UserRole>();


            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                var query =
                    from ur in context.UserRoles
                    join rr in context.RoleRights
                    on ur.RoleId equals rr.RoleId
                    where ur.UserId == UserId 
                    select new
                    {
                        screenId = rr.Screen.Id
                    };
                foreach (var screen in query)
                {
                    screens.Add(context.Screens.Where(b => b.Id == screen.screenId).First());
                }
            }

            return screens;
        }
		public static bool GetScreenright (TenantUserSession tenantUserSession, int UserId, int ScreenId)
        {
            int count = 0;

            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                count =
                    (from ur in context.UserRoles
                     join rr in context.RoleRights
                     on ur.RoleId equals rr.RoleId
                     where ur.UserId == UserId && rr.Screen.Id == ScreenId
                     select rr).Count();
            }

            return (count > 0);
        }

		public static bool GetButtonRights (TenantUserSession tenantUserSession, int UserId, int ScreenId, int buttonId)
        {
            int count = 0;

            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                count =
                    (from ur in context.UserRoles
                     join rr in context.RoleRights
                     on ur.RoleId equals rr.RoleId
                     where ur.UserId == UserId && rr.Screen.Id == ScreenId && rr.Button.Id == buttonId
                     select rr).Count();
            }

            return (count > 0);
        }
    }
}
