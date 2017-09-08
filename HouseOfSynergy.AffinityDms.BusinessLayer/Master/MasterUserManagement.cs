using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Entities.Utilities;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Master
{
	public static class MasterUserManagement
	{
		/// <summary>
		/// Determines whether the user has the supplied rights.
		/// </summary>
		/// <param name="user">The user to test.</param>
		/// <param name="roleTypesMinimumRequired">The minimum combination of roles that is required. Users must have all these roles assigned.</param>
		/// <param name="roleTypesAllowedForBypass">A list of roles that allows the user to bypass the [roleTypesMinimumRequired] parameter even if a single match is found.</param>
		/// <returns></returns>
		public static bool IsUserActionAllowed (MasterUser user, MasterActionType action)
		{
			return (MasterUserManagement.IsUserActionAllowed(user, new MasterActionType [] { action }));
		}
		public static bool IsUserActionAllowed (MasterUser user, params MasterActionType [] actions)
		{
			return (MasterUserManagement.IsUserActionAllowed(user, actions.ToList()));
		}
		public static bool IsUserActionAllowed (MasterUser user, IEnumerable<MasterActionType> actions)
		{
			var allowed = true;
			var rolesAssigned = user.Roles.Select(role => role.RoleType).ToList();
            return allowed;
            //foreach (var action in actions)
            //{
            //    allowed |= ((allowed) || (rolesAssigned.Any(roleType => RoleUtilities.MasterActionRolesAny [action].Contains(roleType))));
            //    allowed |= ((allowed) || (rolesAssigned.Any(roleType => RoleUtilities.MasterActionRolesAll [action].Contains(roleType))));
            //}

            //return (allowed);
		}
        //public static bool UpdateUser(MasterUserSession masterUserSession, MasterUser masteruser, out Exception exception)
        //{
        //    bool result = false;
        //    exception = null;
        //    try
        //    {
        //        using (var context = new ContextMaster())
        //        {
        //            context.Users.Attach(masteruser);
        //            context.Entry(masteruser).State = System.Data.Entity.EntityState.Modified;
        //            context.SaveChanges();
        //            context.Dispose();
        //            result = true;
        //        }
        //    }
        //    catch (Exception ex) {
        //        exception = ex;
        //    }
        //    return (result);

        //}
        public static bool AddUser(MasterUserSession masterUserSession, MasterUser masteruser, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextMaster())
                {
                    if (masteruser.DateTimeCreated != null) { masteruser.DateTimeCreated = DateTime.UtcNow; }
                    context.Users.Add(masteruser);
                    context.SaveChanges();
                    context.Dispose();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);

        }
        public static bool GetAllUsers(MasterUserSession masterUserSession, out List<MasterUser> masteruser, out Exception exception)
        {
            masteruser = null;
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextMaster())
                {
                    masteruser = context.Users.Include("UserRoles").Include("Roles").Select(x => x).ToList();
                    context.Dispose();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);

        }
        public static bool GetUserUserById(MasterUserSession masterUserSession, long id, out MasterUser masteruser, out Exception exception)
        {
            masteruser = null;
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextMaster())
                {
                    masteruser =  context.Users.Where(x=>x.Id == id).Select(x => x).FirstOrDefault();
                    context.Dispose();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }
        public static bool UpdateUser(MasterUserSession masterUserSession, MasterUser masteruser,out MasterUser outmasteruser,  out Exception exception)
        {
            bool result = false;
            outmasteruser = null;
            exception = null;
            try
            {
                using (var context = new ContextMaster())
                {
                    MasterUser user = new MasterUser();
                    user = context.Users.Where(x => x.Id == masteruser.Id).FirstOrDefault();
                    user.Address1 = masteruser.Address1; 
                    user.Address2 = masteruser.Address2;
                    user.City = masteruser.City;
                    user.Country = masteruser.Country;
                    user.Email = masteruser.Email;
                    user.NameFamily = masteruser.NameFamily;
                    user.NameGiven = masteruser.NameGiven;
                    user.PasswordHash = masteruser.PasswordHash;
                    user.PasswordSalt = masteruser.PasswordSalt;
                    user.PhoneMobile = masteruser.PhoneMobile;
                    user.PhoneWork = masteruser.PhoneWork;
                    user.ActiveDirectoryId = masteruser.ActiveDirectoryId;
                    // user.UserName = masteruser.UserName;
                    user.AuthenticationType = masteruser.AuthenticationType;
                    user.ZipOrPostCode = masteruser.ZipOrPostCode;
                    context.Users.Attach(user);
                    context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    context.Dispose();
                    outmasteruser = user;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }
        public static bool UpdateUserRoles(MasterUserSession masterUserSession, long userid, List<MasterRoleType> roletypes, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                var context = new ContextMaster();
                List<MasterUserRole> userroles = context.UserRoles.Where(item=> item.UserId == userid).ToList();//context.UserRoles.Where(item=>item.UserId==((long)Convert.ToInt16(roletypes.Any()))).ToList();
                foreach (var item in userroles)
                {
                    //context.UserRoles.Remove(item);
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                context.Dispose();
                context = new ContextMaster();
                foreach (var item in roletypes)
                {
                    MasterUserRole userrole = new MasterUserRole();
                    userrole.UserId = userid;
                    userrole.RoleId = Convert.ToInt16(item);
                    context.UserRoles.Add(userrole);
                    context.SaveChanges();
                }
                context.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }

	}
}