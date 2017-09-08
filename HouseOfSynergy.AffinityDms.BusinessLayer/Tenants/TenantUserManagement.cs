using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Utilities;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class TenantUserManagement
    {
        private delegate List<TEntity> GetEntity<TEntity>(ContextTenant context)
            where TEntity : class, IEntity<TEntity>, new();
        private static GetEntity<User> GetExpandedUsers = (context) => { return (context.Users.AsNoTracking().Include(u => u.Roles).ToList()); };



        public static bool UpdateUserRolesAndMarkActiveInactive(TenantUserSession tenantUserSession, List<User> users, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (!(result = UpdateUserRolesAndMarkActiveInactive(tenantUserSession, null, users, out exception))) { if (exception != null) { throw (exception); } }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool UpdateUserRolesAndMarkActiveInactive(TenantUserSession tenantUserSession, ContextTenant context, List<User> users, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = UpdateUserRolesAndMarkActiveInactive(tenantUserSession, context, users);
                    }
                }
                else
                {
                    result = UpdateUserRolesAndMarkActiveInactive(tenantUserSession, context, users);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool UpdateUserRolesAndMarkActiveInactive(TenantUserSession tenantUserSession, ContextTenant context, List<User> users)
        {
            bool result = false;
            Exception exception = null;
            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var user in users.ToList())
                {
                    TenantUserManagement.UpdateUserRoles(tenantUserSession, context, user, out exception);
                    if (exception != null) { throw exception; }
                    TenantUserManagement.MarkUserActiveInActive(tenantUserSession, context, user, out exception);
                    if (exception != null) { throw exception; }
                }
                transaction.Commit();
                result = true;
            }
            return result;
        }

        public static bool MarkUserActiveInActive(TenantUserSession tenantUserSession, User user, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                if (!(result = MarkUserActiveInActive(tenantUserSession, null, user, out exception))) { if (exception != null) { throw exception; } }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool MarkUserActiveInActive(TenantUserSession tenantUserSession, ContextTenant context, User user, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = MarkUserActiveInActive(tenantUserSession, context, user);
                    }
                }
                else
                {
                    result = MarkUserActiveInActive(tenantUserSession, context, user);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool MarkUserActiveInActive(TenantUserSession tenantUserSession, ContextTenant context, User user)
        {
            bool result = false;
            var loggedinUser = context.Users.Include(x => x.UserRoles).Where(x => x.Id == tenantUserSession.User.Id).FirstOrDefault();
            if (!(loggedinUser.UserRoles.Any(x => x.RoleId == ((int)TenantRoleType.Administrator)))) { throw (new Exception("You dont have permissions to change user roles.")); }
            var userObj = context.Users.Include(x => x.UserRoles).Where(x => x.Id == user.Id).FirstOrDefault();
            if (userObj == null) { throw (new Exception("Unable to find the following user")); }
            if (userObj.UserName.Trim().ToLower() == "admin") { return true; }
            userObj.IsActive = user.IsActive;
            //context.Users.Attach(userObj);
            context.SaveChanges();
            result = true;
            return result;
        }


        public static bool UpdateUserRoles(TenantUserSession tenantUserSession, User user, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (!(result = UpdateUserRoles(tenantUserSession, null, user, out exception))) { if (exception != null) { throw (exception); } }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool UpdateUserRoles(TenantUserSession tenantUserSession, ContextTenant context, User user, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = UpdateUserRoles(tenantUserSession, context, user);
                    }
                }
                else
                {
                    result = UpdateUserRoles(tenantUserSession, context, user);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool UpdateUserRoles(TenantUserSession tenantUserSession, ContextTenant context, User user)
        {
            bool result = false;
            var loggedinUser = context.Users.Include(x => x.UserRoles).Where(x => x.Id == tenantUserSession.User.Id).FirstOrDefault();
            if (!(loggedinUser.UserRoles.Any(x => x.RoleId == ((int)TenantRoleType.Administrator)))) { throw (new Exception("You dont have permissions to change user roles.")); }
            if (user.Id == 1) { return false; }
            var userObj = context.Users.Include(x => x.UserRoles).Where(x => x.Id == user.Id).FirstOrDefault();
            if (userObj == null) { throw (new Exception("Unable to find the following user")); }
            if (userObj.UserName.Trim().ToLower() == "admin") { return true; }
            if (userObj.UserRoles != null)
            {
                foreach (var userrole in userObj.UserRoles.ToList())
                {
                    context.UserRoles.Remove(userrole);
                }
            }
            context.SaveChanges();
            foreach (var userrole in user.UserRoles)
            {
                if (string.IsNullOrEmpty(Enum.GetName(typeof(TenantRoleType), userrole.RoleId))) { throw (new Exception("Unable to find the following role")); }
                var newUserRole = new UserRole();
                newUserRole.UserId = user.Id;
                newUserRole.RoleId = userrole.RoleId;
                context.UserRoles.Add(newUserRole);
            }
            context.SaveChanges();
            result = true;
            return result;
        }




        /// <summary>
        /// Determines whether the user has the supplied rights.
        /// </summary>
        /// <param name="user">The user to test.</param>
        /// <param name="roleTypesMinimumRequired">The minimum combination of roles that is required. Users must have all these roles assigned.</param>
        /// <param name="roleTypesAllowedForBypass">A list of roles that allows the user to bypass the [roleTypesMinimumRequired] parameter even if a single match is found.</param>
        /// <returns></returns>
        public static bool IsUserActionAllowed(User user, TenantActionType action)
        {
            return (TenantUserManagement.IsUserActionAllowed(user, new TenantActionType[] { action }));
        }

        public static bool IsUserActionAllowed(User user, params TenantActionType[] actions)
        {
            return (TenantUserManagement.IsUserActionAllowed(user, actions.ToList()));
        }

        public static bool IsUserActionAllowed(User user, IEnumerable<TenantActionType> actions)
        {
            var allowed = false;
            var rolesAssigned = user.Roles.Select(role => role.RoleType).ToList();

            foreach (var action in actions)
            {
                allowed |= ((allowed) || (rolesAssigned.Any(roleType => RoleUtilities.TenantActionRolesAny[action].Contains(roleType))));
                allowed |= ((allowed) || (rolesAssigned.Any(roleType => RoleUtilities.TenantActionRolesAll[action].Contains(roleType))));
            }

            return (allowed);
        }

        public static List<RoleRight> RoleRights(TenantUserSession tenantUserSession, int screenId, int roleId)
        {
            List<RoleRight> roleRights = null;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                roleRights = context.RoleRights.Include(p => p.Button).Include(p => p.Screen).Where(r => r.Screen.Id == screenId).Where(r => r.Roles.Id == roleId).ToList();
            }
            return roleRights;
        }
        public static bool AddADUser(TenantUserSession tenantusersession, User user, out bool MatchedUserFromAD, out User founduser, out Exception exception)
        {
            MatchedUserFromAD = false;
            exception = null;
            founduser = null;
            bool result = false;
            System.Data.Common.DbTransaction Tran = null;
            ContextTenant context = new ContextTenant(tenantusersession.Tenant.DatabaseConnectionString);
            context.ObjectContext.Connection.Open();
            Tran = context.ObjectContext.Connection.BeginTransaction();
            try
            {
                var finduser = context.Users.Where(u => u.Email == user.Email).ToArray();
                User tenantuser = (finduser.Length > 0) ? (finduser.First()) : (null);
                if (tenantuser != null)
                {
                    founduser = tenantuser;
                    //update user
                    if ((founduser.ActiveDirectory_ObjectId == user.ActiveDirectory_ObjectId) && (!((founduser.AuthenticationType == AuthenticationType.Local) || ((founduser.AuthenticationType == AuthenticationType.Both)))))
                    {
                        user.Id = tenantuser.Id;
                        MatchedUserFromAD = false;
                        tenantuser = null;
                        result = UpdateUser(tenantusersession, user, out tenantuser, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (result)
                        {
                            Tran.Commit();
                            Tran.Dispose();
                        }
                        else
                        {
                            Tran.Rollback();
                            Tran.Dispose();
                        }
                    }
                    else
                    {
                        MatchedUserFromAD = true;
                        Tran.Rollback();
                        Tran.Dispose();
                    }
                }
                else
                {
                    //create user
                    result = AddUser(tenantusersession, user, out exception);
                    if (exception != null) { throw exception; }
                    if (result)
                    {
                        Tran.Commit();
                        Tran.Dispose();
                    }
                    else
                    {
                        Tran.Rollback();
                        Tran.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                exception = ex;
                Tran.Rollback();
                Tran.Dispose();
            }
            return result;
        }

      
        public static bool RemoveUser(TenantUserSession tenantUserSession, long id, ContextTenant context = null)
        {
            bool result = false;
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = RemoveUserPrivate( tenantUserSession,  id,context);
                    }
                }
                else
                {
                    result = RemoveUserPrivate(tenantUserSession, id, context);
                }
           
            return result;
        }
        private static bool RemoveUserPrivate(TenantUserSession tenantUserSession, long id, ContextTenant context = null)
        {
            bool result = false;

            using (var transaction = context.Database.BeginTransaction())
            {
                var user = context.Users
                              .Include(x => x.UserDocuments)
                              .Include(x => x.UserFolders)
                              .Include(x => x.UserRoles)
                              .Include(x => x.UserTemplates)
                              .Where(x => x.Id == id).FirstOrDefault();
                if (user == null) { throw (new Exception("Unable to find the following user")); }
                if (user.InviteGuid == null) { throw (new Exception("Fololowing user can not be deleted")); }
                foreach (var userrole in user.UserRoles?.ToList())
                {
                    context.UserRoles.Remove(userrole);
                    context.SaveChanges();
                }
                foreach (var userDoc in user.UserDocuments?.ToList())
                {
                    context.UserDocuments.Remove(userDoc);
                    context.SaveChanges();
                }
                foreach (var userFolder in user.UserFolders?.ToList())
                {
                    context.UserFolders.Remove(userFolder);
                    context.SaveChanges();
                }
                foreach (var userTemplate in user.UserTemplates?.ToList())
                {
                    context.UserTemplates.Remove(userTemplate);
                    context.SaveChanges();
                }
                var discourseUserList = context.DiscussionUsers.Where(x => x.UserId == id).ToList();
                foreach (var discourseUser in discourseUserList)
                {
                    context.DiscussionUsers.Remove(discourseUser);
                    context.SaveChanges();
                }
                context.Users.Remove(user);
                context.SaveChanges();
                transaction.Commit();
                result = true;
            }
          
            return result;
        }




        /**/
        //=========================
        #region AddUser
        //=========================
        //public static bool AddUser(TenantUserSession tenantUserSession, User user)
        //{
        //    var result = false;

        //    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //    {
        //        var tempUser = context.Users.Add(user);
        //        context.SaveChanges();
        //    }

        //    result = true;

        //    return (result);
        //}
        public static bool AddUser(TenantUserSession tenantUserSession, User user, out Exception exception)
        {
            var result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var tempUser = context.Users.Add(user);
                        context.SaveChanges();
                        var rootfolders = context.Folders.Where(x => (x.ParentId == 1));
                        user.UserFolders.Add(new UserFolder() { FolderId = 1, UserId = tempUser.Id, IsActive = true });
                        foreach (var folder in rootfolders)
                        {
                            if (user.AuthenticationType == AuthenticationType.External)
                            {
                                if (folder.Name == "Shared")
                                {
                                    user.UserFolders.Add(new UserFolder() { FolderId = folder.Id, UserId = tempUser.Id, IsActive = true });
                                }
                            }
                            else
                            {
                                user.UserFolders.Add(new UserFolder() { FolderId = folder.Id, UserId = tempUser.Id, IsActive = true });
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }

        public static bool UpdateUserByGuid(Tenant tenant, User user, out User userObj, out Exception exception)
        {
            userObj = new User();
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        userObj = context.Users.Include(x => x.Tenant).Where(x => (x.InviteGuid == user.InviteGuid)).SingleOrDefault();
                        var userNameAlreadyExists = context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
                        if (userNameAlreadyExists != null) { throw (new Exception("Username already exists. please select a unique username")); }
                        if (userObj == null) { throw new Exception("Unable to find the Invitation"); }
                        //if (!PasswordHash.ValidatePassword(user.PasswordHash, userObj.PasswordHash)) { throw (new AuthenticationException(isValidDomain: true, isValidUsername: true, isValidPassword: false)); }
                        userObj.UserName = user.UserName;
                        userObj.NameFamily = user.NameFamily;
                        userObj.NameGiven = user.NameGiven;
                        var passwordHashClient = Sha.GenerateHash(user.PasswordHash, Encoding.UTF8, Sha.EnumShaKind.Sha512);
                        var passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
                        userObj.PasswordHash = passwordHashServer;
                        userObj.PasswordSalt = passwordHashServer;
                        userObj.InviteGuid = null;
                        userObj.UserName = user.UserName;
                        context.SaveChanges();
                        contextTrans.Commit();
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

        //=========================
        #endregion AddUser
        //=========================
        /**/
        //=========================
        #region GetUser
        //=========================
        public static bool GetDepartments(TenantUserSession tenantUserSession, out List<Department> departments, out Exception exception)
        {
            departments = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    departments = context.Departments.Select(x => x).ToList();
                    context.Dispose();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static List<Screen> GetScreens(TenantUserSession tenantUserSession)
        {
            List<Screen> screens = null;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                screens = context.Screens.OrderBy(s => s.Id).ToList();
            }
            return screens;
        }
        public static bool GetUserById(TenantUserSession tenantUserSession, long userId, out User user, out Exception exception)
        {
            user = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    user = context.Users.Include(p => p.UserRoles).Include(p => p.Roles).FirstOrDefault(u => u.Id == userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        //public static bool GetUsers(TenantUserSession tenantUserSession, out List<User> userEntities, out Exception exception)
        //{
        //    throw new NotImplementedException();
        //}

        public static List<Button> GetScreenButtons(TenantUserSession tenantUserSession, int screenId)
        {
            List<Button> buttons = null;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                buttons = context.Buttons.Include(p => p.Screen).Where(r => r.Screen.Id == screenId).ToList();
            }
            return buttons;
        }
        public static List<Role> GetRoles(TenantUserSession tenantUserSession)
        {
            List<Role> roles = null;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                roles = context.Roles.Include(p => p.Users).Include(p => p.UserRoles).Include(p => p.RoleRights).ToList();
            }
            return roles;
        }
        public static bool GetUsers(TenantUserSession tenantUserSession, out List<User> users, out Exception exception)
        {
            var result = false;
            users = null;
            exception = null;

            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    //Ask raheel bhai whether to exclude administrators by name or by role ?
                    users = context.Users
                                   .Include(x => x.Roles)
                                   .Include(x => x.UserRoles)
                                   .Include(x => x.UserRoles.Select(y => y.Role))
                                   .Include(x => x.Department)
                                   .Include(x => x.UserDocuments)
                                   .Include(x => x.UserFolders)
                                   .Include(x => x.UserTemplates)
                                   .ToList();
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        public static bool GetUser(TenantUserSession tenantUserSession, out User user, out Exception exception)
        {
            var result = false;

            user = null;
            exception = null;

            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    user = context.Users.AsNoTracking().Include(p => p.UserRoles).FirstOrDefault(u => u.Id == tenantUserSession.User.Id);
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool GetUserByEmail(TenantUserSession tenantUserSession, string email, out User user, out Exception exception)
        {
            var result = false;

            user = null;
            exception = null;

            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var users = context.Users.Where(u => u.Email.ToLower() == email.ToLower()).ToArray();
                    if (users.Count() > 0)
                    {
                        user = users.First();
                        result = true;
                    }
                    else
                    {
                        user = new User();
                        result = false;
                    }
                }


            }
            catch (Exception e)
            {
                exception = e;
            }
            return result;
        }

        public static bool GetUsersByRoles(TenantUserSession tenantUserSession, TenantRoleType userRole, out List<User> users, out Exception exception)
        {
            bool result = false;
            exception = null;
            users = new List<User>();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var role = context.Roles.Where(x => x.RoleType == userRole).SingleOrDefault();
                    if (role != null)
                    {
                        var userroles = context.UserRoles
                                                .Include(x => x.User)
                                                .Where(x => x.RoleId == role.Id)
                                                .ToList();
                        foreach (var userrole in userroles.ToList())
                        {
                            users.Add(userrole.User.CleanDataGetIdAndName(userrole.User));
                        }
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

        public static bool AssignUser(TenantUserSession tenantUserSession, long docId, long userId, DiscussionPostAttachmentType docType, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    if (docType == DiscussionPostAttachmentType.Document)
                    {
                        var document = context.Documents.Where(x => x.Id == docId).SingleOrDefault();
                        if (document != null)
                        {
                            document.AssignedByUserId = tenantUserSession.User.Id;
                            document.AssignedToUserId = userId;
                            document.AssignedDate = DateTime.UtcNow;
                            document.AssignmentState = AssignmentState.Assigned;
                            context.SaveChanges();
                            result = true;
                        }
                    }
                    //else if (docType == DiscussionPostAttachmentType.Template)
                    //{
                    //    var template = context.Templates.Where(x => x.Id == docId).SingleOrDefault();
                    //    if (template != null)
                    //    {
                    //        template.AssignedByUserId = tenantUserSession.User.Id;
                    //        template.AssignedToUserId = userId;
                    //        template.AssignedDate = DateTime.Now;
                    //        template.AssignmentState = AssignmentState.Assigned;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        //public static IList<User> GetUsers(TenantUserSession tenantUserSession)
        //{
        //    IList<User> users = null;
        //    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //    {
        //        users = context.Users.AsNoTracking().ToList();
        //    }
        //    return users;
        //}
        public static User GetUserById(TenantUserSession tenantUserSession, int userId)
        {
            User userInfo = null;
            // User userInfo = new User();
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                userInfo = context.Users.Include(p => p.UserRoles).FirstOrDefault(u => u.Id == userId);
            }
            return userInfo;
        }
        //=========================
        #endregion  GetUser
        //=========================
        /**/
        //=========================
        #region UpdateUser
        //=========================

        public static bool UpdateUser(TenantUserSession tenantUserSession, User tenantuser, out User outtenantuser, out Exception exception)
        {
            bool result = false;
            outtenantuser = null;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    User user = new User();
                    user = context.Users.Where(x => x.Id == tenantuser.Id).FirstOrDefault();
                    user.Address1 = tenantuser.Address1;
                    user.Address2 = tenantuser.Address2;
                    user.City = tenantuser.City;
                    user.Country = tenantuser.Country;
                    user.Email = tenantuser.Email;
                    user.NameFamily = tenantuser.NameFamily;
                    user.NameGiven = tenantuser.NameGiven;
                    if (!string.IsNullOrEmpty(tenantuser.PasswordHash) && !string.IsNullOrWhiteSpace(tenantuser.PasswordHash))
                    {
                        var passwordHashClient = Sha.GenerateHash(tenantuser.PasswordHash, Encoding.UTF8, Sha.EnumShaKind.Sha512);
                        var passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
                        user.PasswordHash = passwordHashServer;
                        user.PasswordSalt = passwordHashServer;
                    }

                    user.PhoneMobile = tenantuser.PhoneMobile;
                    user.PhoneWork = tenantuser.PhoneWork;
                    user.ActiveDirectory_ManagerId = tenantuser.ActiveDirectory_ManagerId;
                    user.ActiveDirectory_ObjectId = tenantuser.ActiveDirectory_ObjectId;
                    user.AuthenticationType = tenantuser.AuthenticationType;
                    user.DepartmentId = tenantuser.DepartmentId;
                    user.ZipOrPostCode = tenantuser.ZipOrPostCode;
                    //context.Users.Attach(user);
                    //context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    context.Dispose();
                    outtenantuser = user;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }
        private static string GenerateRandomPassword()
        {
            var password = string.Empty;
            do
            {
                var random = new System.Random();
                var randomGenerated = random.Next();
                var random2Digit = randomGenerated.ToString().ToCharArray().Take(Convert.ToInt16(randomGenerated.ToString().Length / 2)).Reverse().Take(2).ToArray();
                var length = Convert.ToInt16(random2Digit[0].ToString() + random2Digit[1].ToString());
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                password = new string(
                    Enumerable.Repeat(chars, length)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
            } while (password.Length <= (EntityConstants.LengthPasswordMinimum * 2));

            if (password.Length > (EntityConstants.LengthPasswordMinimum * 2))
            {
                password = password.Substring(password.Length / 2, EntityConstants.LengthPasswordMinimum);
            }
            return password;
        }

        public static bool UpdateUserRandomPasswordAndInviteGuid(TenantUserSession tenantUserSession, User user, out string password, out Guid guid, out Exception exception)
        {
            var result = false;
            exception = null;
            password = string.Empty;
            guid = new Guid();
            try
            {

                password = GenerateRandomPassword();
                var passwordHashClient = Sha.GenerateHash(password, Encoding.UTF8, Sha.EnumShaKind.Sha512);
                var passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    user.PasswordHash = passwordHashServer;
                    user.PasswordSalt = passwordHashServer;
                    guid = Guid.NewGuid();
                    user.InviteGuid = guid;
                    //context.Entry(user).State = EntityState.Modified;
                    context.Users.Attach(user);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
            return result;
        }

        public static bool InviteUser(TenantUserSession tenantUserSession, string email, string inviteUrl, long id, string viewType, out string password, out User user, out Exception exception)
        {
            bool result = false;
            exception = null;
            user = new User();
            password = string.Empty;
            try
            {

                user.AuthenticationType = AuthenticationType.External;
                user.InviteUrl = inviteUrl;
                user.UserName = string.Empty;
                user.Email = email;
                user.DateTimeCreated = DateTime.UtcNow;
                user.TenantId = tenantUserSession.User.TenantId;

                user.NameGiven = string.Empty;
                user.NameFamily = string.Empty;
                user.Address1 = string.Empty;
                user.Address2 = string.Empty;
                user.City = string.Empty;
                user.ZipOrPostCode = string.Empty;
                user.Country = string.Empty;
                user.PhoneWork = string.Empty;
                user.PhoneMobile = string.Empty;
                user.DepartmentId = null; 

                //var random = new System.Random(10);
                password = GenerateRandomPassword();
                var passwordHashClient = Sha.GenerateHash(password, Encoding.UTF8, Sha.EnumShaKind.Sha512);
                var passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
                user.PasswordHash = passwordHashServer;
                user.PasswordSalt = passwordHashServer;
                Guid guid = Guid.NewGuid();
                user.InviteGuid = guid;




                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        if (context.Users.FirstOrDefault(x => x.Email == email) != null) { throw (new Exception("User Already Exists")); }
                        user = context.Users.Add(user);
                        context.SaveChanges();
                        if (viewType == "Discourse")
                        {
                            var discourseUser = new DiscourseUser();
                            discourseUser.DiscourseId = id;
                            discourseUser.UserId = user.Id;
                            discourseUser.IsActive = true;
                            context.DiscussionUsers.Add(discourseUser);
                        }
                        else if (viewType == "Document")
                        {
                            var userDocument = new UserDocument();
                            userDocument.DocumentId = id;
                            userDocument.UserId = user.Id;
                            userDocument.IsActive = true;
                            context.UserDocuments.Add(userDocument);
                        }
                        else if (viewType == "Template" || viewType == "Form")
                        {
                            var userTemplate = new UserTemplate();
                            userTemplate.TemplateId = id;
                            userTemplate.UserId = user.Id;
                            userTemplate.IsActive = true;
                            context.UserTemplates.Add(userTemplate);
                        }
                        var rootfolders = context.Folders.Where(x => (x.ParentId == 1));
                        user.UserFolders.Add(new UserFolder() { FolderId = 1, UserId = user.Id, IsActive = true });
                        foreach (var folder in rootfolders)
                        {
                            if (user.AuthenticationType == AuthenticationType.External)
                            {
                                if (folder.Name == "Shared")
                                {
                                    user.UserFolders.Add(new UserFolder() { FolderId = folder.Id, UserId = user.Id, IsActive = true });
                                }
                            }
                            else
                            {
                                user.UserFolders.Add(new UserFolder() { FolderId = folder.Id, UserId = user.Id, IsActive = true });
                            }
                        }
                        
                        context.SaveChanges();
                        contextTrans.Commit();
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

        //public static bool UpdateRights(TenantUserSession tenantUserSession, int screenId, int roleId, List<Button> checkedButtons)
        //{
        //    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //    {
        //        context.RoleRights.RemoveRange(context.RoleRights.Where(r => r.Screen.Id == screenId).Where(r => r.Roles.Id == roleId));
        //        context.SaveChanges();

        //        foreach (Button button in checkedButtons)
        //        {
        //            RoleRight newRoleRight = new RoleRight();
        //            newRoleRight.Screen = context.Screens.Where(s => s.Id == screenId).FirstOrDefault();
        //            newRoleRight.Roles = context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
        //            newRoleRight.Button = context.Buttons.Where(b => b.Id == button.Id).FirstOrDefault();
        //            context.RoleRights.Add(newRoleRight);
        //            context.SaveChanges();
        //        }
        //    }
        //    return true;
        //}
        public static bool UpdateRights(TenantUserSession tenantUserSession, int screenId, int roleId, List<Button> checkedButtons, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.RoleRights.RemoveRange(context.RoleRights.Where(r => r.Screen.Id == screenId).Where(r => r.Roles.Id == roleId));
                    context.SaveChanges();

                    foreach (Button button in checkedButtons)
                    {
                        RoleRight newRoleRight = new RoleRight();
                        newRoleRight.Screen = context.Screens.Where(s => s.Id == screenId).FirstOrDefault();
                        newRoleRight.Roles = context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
                        newRoleRight.Button = context.Buttons.Where(b => b.Id == button.Id).FirstOrDefault();
                        context.RoleRights.Add(newRoleRight);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool UpdateUserRoles(TenantUserSession tenantUserSession, long userid, List<TenantRoleType> roletypes, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        List<UserRole> userroles = context.UserRoles.Where(item => item.UserId == userid).ToList();//context.UserRoles.Where(item=>item.UserId==((long)Convert.ToInt16(roletypes.Any()))).ToList();
                        foreach (var item in userroles)
                        {
                            context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            context.SaveChanges();
                        }
                        try
                        {
                            foreach (var item in roletypes)
                            {
                                UserRole userrole = new UserRole();
                                userrole.UserId = userid;
                                userrole.RoleId = Convert.ToInt16(item);
                                context.UserRoles.Add(userrole);
                                context.SaveChanges();
                            }
                        }
                        catch (Exception exx)
                        {
                            Exception ex = new Exception("Unable to process roles further");
                            throw ex;
                        }
                        contextTrans.Commit();
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }
        public static bool UpdateUser(TenantUserSession tenantUserSession, User user)
        {
            try
            {


                User tempUser = null;
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    //using (transaction = context.Database.BeginTransaction())
                    //{
                    //    try
                    //    {
                    //    }
                    //}
                    context.Configuration.LazyLoadingEnabled = true;
                    // context.Configuration. = true;

                    tempUser = context.Users.Where(u => u.Id == user.Id).FirstOrDefault<User>();


                    tempUser.NameGiven = user.NameGiven;
                    tempUser.NameFamily = user.NameFamily;
                    tempUser.PhoneMobile = user.PhoneMobile;
                    tempUser.Address1 = user.Address1;
                    tempUser.Address2 = user.Address2;
                    tempUser.UserName = user.UserName;
                    tempUser.Email = user.Email;

                    foreach (Role role in user.Roles)
                    {
                        context.UserRoles.RemoveRange(context.UserRoles.Where(u => u.Id == user.Id));


                        context.UserRoles.AddRange(user.UserRoles);

                        if (tempUser.UserRoles.Where(u => u.RoleId == role.Id).Count() < 1)
                        {
                            UserRole userRole = new UserRole();
                            userRole.User = tempUser;
                            userRole.RoleId = role.Id;
                            tempUser.UserRoles.Add(userRole);
                        }
                    }

                    context.Users.Attach(tempUser);
                    context.Entry(tempUser).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public static bool UpdateInviteUser(TenantUserSession tenantUserSession, User user)
        {
            try
            {
                User tempUser = null;
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = true;
                    // context.Configuration. = true;
                    tempUser = context.Users.Where(u => u.Id == user.Id).FirstOrDefault<User>();
                    tempUser.NameGiven = user.NameGiven;
                    tempUser.NameFamily = user.NameFamily;
                    tempUser.PhoneMobile = user.PhoneMobile;
                    tempUser.Address1 = user.Address1;
                    tempUser.Address2 = user.Address2;
                    tempUser.UserName = user.UserName;
                    tempUser.NameFamily = user.NameFamily;
                    tempUser.NameGiven = user.NameGiven;
                    tempUser.PasswordHash = user.PasswordHash;
                    tempUser.PasswordSalt = user.PasswordSalt;
                    context.Users.Attach(tempUser);
                    context.Entry(tempUser).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        //=========================
        #endregion UpdateUser
        //=========================
        /**/
        //==========================
        #region FindUser
        //==========================

        public static bool FindUserByUsernameOrEmail(TenantUserSession tenantUserSession, string username, string email, out User tenantuser, out Exception exception)
        {
            tenantuser = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    tenantuser = context.Users.Where(x => (x.UserName == username) || (x.Email == email)).FirstOrDefault();
                }
                if (tenantuser != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool FindUserByUsername(TenantUserSession tenantUserSession, string username, out User tenantuser, out Exception exception)
        {
            tenantuser = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    tenantuser = context.Users.Where(x => (x.UserName == username)).FirstOrDefault();
                }
                if (tenantuser != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool FindUserByUsername(TenantUserSession tenantUserSession, string username, out Exception exception)
        {
            User tenantuser = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    tenantuser = context.Users.Where(x => (x.UserName == username)).FirstOrDefault();
                }
                if (tenantuser != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool FindUserByEmail(TenantUserSession tenantUserSession, string email, out User tenantuser, out Exception exception)
        {
            tenantuser = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    tenantuser = context.Users.Where(x => (x.Email == email)).FirstOrDefault();
                }
                if (tenantuser != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool FindUserByEmail(TenantUserSession tenantUserSession, string email, out Exception exception)
        {
            User tenantuser = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    tenantuser = context.Users.Where(x => (x.Email == email)).FirstOrDefault();
                }
                if (tenantuser != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool FindUserByUsernameOrEmail(TenantUserSession tenantUserSession, string username, string email, out Exception exception)
        {
            var tenantuser = new User();
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    tenantuser = context.Users.Where(x => (x.UserName == username) || (x.Email == email)).FirstOrDefault();
                }
                if (tenantuser != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        //==========================
        #endregion Find User
        //==========================
        /**/
        //==========================
        #region Document User Right  Management
        public static bool UserRightsForDocuments(TenantUserSession tenantUserSession, long documentId, List<long> usersId, out Exception exception)
        {
            bool result = false;
            exception = null;
            if (usersId == null) { usersId = new List<long>(); }
            try
            {
                using (var contextTenant = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {

                    using (var contextTrans = contextTenant.Database.BeginTransaction())
                    {
                        Document document = null;
                        DocumentManagement.GetDocumentById(tenantUserSession, contextTenant, documentId, out document, out exception);
                        if (document != null)
                        {
                            foreach (var documentUser in document.DocumentUsers.ToList())
                            {
                                if (document.UserId != documentUser.UserId)
                                {
                                    documentUser.IsActive = false;
                                    contextTenant.SaveChanges();
                                }
                            }
                            foreach (var uId in usersId)
                            {
                                if (document != null)
                                {

                                    var user = contextTenant.Users.Where(x => x.Id == uId).SingleOrDefault();
                                    if (user != null)
                                    {
                                        bool isUserAdded = false;
                                        foreach (var documentUser in document.DocumentUsers.ToList())
                                        {
                                            if (documentUser.UserId == user.Id)
                                            {
                                                isUserAdded = true;
                                                documentUser.IsActive = true;
                                                contextTenant.SaveChanges();
                                            }
                                        }
                                        if (!isUserAdded)
                                        {
                                            var userDocument = new UserDocument();
                                            userDocument.DocumentId = documentId;
                                            userDocument.UserId = user.Id;
                                            userDocument.IsActive = true;
                                            document.DocumentUsers.Add(userDocument);
                                            // contextTenant.Entry(userDocument).State = EntityState.Modified;
                                            contextTenant.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        result = true;
                        contextTrans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        #endregion
        //==========================
        /**/
        //==========================
        #region Folder User Management
        public static bool UserRightsForFolders(TenantUserSession tenantUserSession, long folderId, List<long> usersId, out Exception exception)
        {
            bool result = false;
            exception = null;
            if (usersId == null) { usersId = new List<long>(); }
            try
            {
                using (var contextTenant = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {

                    using (var contextTrans = contextTenant.Database.BeginTransaction())
                    {
                        Folder folder = null;
                        FolderManagement.GetFolderById(tenantUserSession, contextTenant, folderId, out folder, out exception);
                        if (folder != null)
                        {
                            foreach (var folderUser in folder.FolderUsers.ToList())
                            {
                                if (folder.UserCreatedById != folderUser.UserId)
                                {
                                    folderUser.IsActive = false;
                                    contextTenant.SaveChanges();
                                }
                            }
                            foreach (var uId in usersId)
                            {
                                if (folder != null)
                                {

                                    var user = contextTenant.Users.Where(x => x.Id == uId).SingleOrDefault();
                                    if (user != null)
                                    {
                                        bool isUserAdded = false;
                                        foreach (var folderUser in folder.FolderUsers.ToList())
                                        {
                                            if (folderUser.UserId == user.Id)
                                            {
                                                isUserAdded = true;
                                                folderUser.IsActive = true;
                                                contextTenant.SaveChanges();
                                            }
                                        }
                                        if (!isUserAdded)
                                        {
                                            var userFolder = new UserFolder();
                                            userFolder.FolderId = folderId;
                                            userFolder.UserId = user.Id;
                                            userFolder.IsActive = true;
                                            folder.FolderUsers.Add(userFolder);
                                            contextTenant.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        result = true;
                        contextTrans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        #endregion
        //==========================
        /**/
        ////==========================
        #region Template User Management
        public static bool UserRightsForTemplates(TenantUserSession tenantUserSession, long templateId, List<long> usersId, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var contextTenant = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {

                    using (var contextTrans = contextTenant.Database.BeginTransaction())
                    {


                        var template = contextTenant.Templates
                                                    .Include(x => x.TemplateUsers)
                                                    .Where(x => x.Id == templateId).SingleOrDefault();
                        if (template != null)
                        {
                            foreach (var templateuser in template.TemplateUsers.ToList())
                            {
                                if (template.UserId != templateuser.UserId)
                                {
                                    templateuser.IsActive = false;
                                    contextTenant.SaveChanges();
                                }
                            }
                            foreach (var uId in usersId)
                            {
                                if (template != null)
                                {

                                    var user = contextTenant.Users.Where(x => x.Id == uId).SingleOrDefault();
                                    if (user != null)
                                    {
                                        bool isUserAdded = false;
                                        foreach (var templateUser in template.TemplateUsers.ToList())
                                        {
                                            if (templateUser.UserId == user.Id)
                                            {
                                                isUserAdded = true;
                                                templateUser.IsActive = true;
                                                contextTenant.SaveChanges();
                                            }
                                        }
                                        if (!isUserAdded)
                                        {
                                            var userTemplate = new UserTemplate();
                                            userTemplate.TemplateId = template.Id;
                                            userTemplate.UserId = user.Id;
                                            userTemplate.IsActive = true;
                                            template.TemplateUsers.Add(userTemplate);
                                            contextTenant.Entry(template).State = EntityState.Modified;
                                            contextTenant.SaveChanges();
                                        }

                                    }
                                }
                            }
                            result = true;
                            contextTrans.Commit();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        #endregion
        ////==========================


    }
}