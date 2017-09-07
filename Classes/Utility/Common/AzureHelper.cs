using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
    public class AzureHelper
    {
        /// <summary>
        /// Setting up Active Directory Client
        /// </summary>
        /// <param name="activeDirectoryClient">Returns Active Directory Client Object</param>
        /// <param name="exception">Returns any exception occurred</param>
        /// <returns>Returns true if successfully executed.</returns>
        public static bool SetActiveDirectoryClient(out ActiveDirectoryClient activeDirectoryClient, out Exception exception)
        {
            exception = null;
            activeDirectoryClient = null;
            bool result = false;
            try
            {
                activeDirectoryClient = AzureAuthenticationHelper.GetActiveDirectoryClientAsApplication();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        /// <summary>
        /// Get User by User Principle Name
        /// </summary>
        /// <param name="activeDirectoryClient">Instance of Active Directory Client</param>
        /// <param name="UPN">User Principle Name to search</param>
        /// <param name="founduser">User found by UPN</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if user is found</returns>
        public static bool GetUserByUPN(ActiveDirectoryClient activeDirectoryClient, string UPN, out User founduser, out Exception exception)
        {
            exception = null;
            bool result = false;
            founduser = null;
            try
            {
                List<IUser> userslist = activeDirectoryClient.Users.Where(user => user.UserPrincipalName.Equals(UPN)).ExecuteAsync().Result.CurrentPage.ToList();
                if (userslist != null && userslist.Count > 0)
                {
                    founduser = (User)userslist.First();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Gets all user in azure active directory
        /// </summary>
        /// <param name="activeDirectoryClient">Instance of Active Directory Client</param>
        /// <param name="foundusers">List of users found</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>returns true if users are found</returns>
        public static bool GetUsers(ActiveDirectoryClient activeDirectoryClient, out List<IUser> foundusers, out Exception exception)
        {
            exception = null;
            bool result = false;
            foundusers = null;
            try
            {
                List<IUser> userslist = activeDirectoryClient.Users.ExecuteAsync().Result.CurrentPage.ToList();
                if (userslist != null && userslist.Count > 0)
                {
                    foundusers = userslist;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
    }
}