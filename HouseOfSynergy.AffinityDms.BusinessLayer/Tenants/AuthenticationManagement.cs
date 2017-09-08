using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Library;
using System.Security.Cryptography;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class AuthenticationManagement
    {
        /// <summary>
        /// Authentication using: [Salted password hashing with PBKDF2-SHA1].
        /// Implementation available at: [HouseOfSynergy.PowerTools.Library.Security.Cryptography.PasswordHash].
        /// The password should never reach here in plain text. It should be hashed using Sha512 in TypeScript or JavaScript.
        /// A C# implementation of Sha512 is available at: [HouseOfSynergy.PowerTools.Library.Security.Cryptography.Sha].
        /// </summary>
        /// <param name="sessionType">The origin of the request.</param>
        /// <param name="domain">The domain of the requested tenant.</param>
        /// <param name="username">Username in plain text.</param>
        /// <param name="passwordHash">Password from client hashed using Sha512.</param>
        /// <param name="clientIpAddress">The IP Address the request originated from.</param>
        /// <param name="userAgent">The User Agent of the request.</param>
        /// <param name="ticks">The user's timestamp in ticks.</param>
        /// <param name="ticks">The requests server Session Id.</param>
        /// <param name="tenantUserSession">Populates the out tenantUserSession if the function succeeds.</param>
        /// <param name="exception">Populates the out exception where applicable.</param>
        /// <returns>Returns true if sign-in succeeds. Otherwise, populates the out exception parameter.</returns>
        public static bool SignIn
        (
            SessionType sessionType,
            string domain,
            string username,
            string passwordHash,
            string clientIpAddress,
            string userAgent,
            long ticks,
            string sessionId,
            out TenantUserSession tenantUserSession,
            out Exception exception
        )
        {
            var result = false;
            User userDatabase = null;
            var now = DateTime.UtcNow;
            Tenant tenantDatabase = null;
            Session sessionDatabase = null;

            exception = null;
            tenantUserSession = null;

            try
            {
                if (!MasterTenantManagement.GetTenantByDomain(domain, out tenantDatabase, out exception)) { throw (exception); }
                if (tenantDatabase == null) { throw (new DomainNotFoundException()); }

               // ContextTenant.Initialize(tenantDatabase.DatabaseConnectionString);

                using (var context = new ContextTenant(tenantDatabase.DatabaseConnectionString))
                {
                    tenantDatabase = context.Tenants.SingleOrDefault();
                    if (tenantDatabase == null) { throw (new DomainNotFoundException()); }

                    // TODO: Use a single exception for unmatched username/password scenarios.
                    userDatabase = context.Users.SingleOrDefault(u => (u.UserName == username));
                    if (userDatabase == null) { throw (new AuthenticationException(isValidDomain: true, isValidUsername: false, isValidPassword: null)); }

                    if (!PasswordHash.ValidatePassword(passwordHash, userDatabase.PasswordHash)) { throw (new AuthenticationException(isValidDomain: true, isValidUsername: true, isValidPassword: false)); }

                    var token
                            = tenantDatabase.Id.ToString()
                            + EntityConstants.TokenDelimiter
                            + tenantDatabase.Domain
                            + EntityConstants.TokenDelimiter
                            + userDatabase.Id.ToString()
                            + EntityConstants.TokenDelimiter
                            + userDatabase.UserName
                            + EntityConstants.TokenDelimiter
                            + userDatabase.AuthenticationType.ToString()
                            + EntityConstants.TokenDelimiter
                            + (userDatabase.ActiveDirectory_ObjectId.ToString(GuidUtilities.EnumGuidFormat.N.ToString())).Trim()
                            + EntityConstants.TokenDelimiter
                            + ""
                            + EntityConstants.TokenDelimiter
                            + sessionType.ToString()
                            + EntityConstants.TokenDelimiter
                            + EntityConstants.TokenDelimiter
                            + EntityConstants.TokenDelimiter;

                    // TODO: Remove for production.
                    if (AffinityConfiguration.DeploymentLocation == DeploymentLocation.BtsSaleem) { now = now.Add(TimeSpan.FromHours(1)); }

                    //clientIpAddress.r

                    sessionDatabase = userDatabase.Sessions.SingleOrDefault
                    (
                        s =>
                        (
                            (s.DateTimeCreated < now)
                            && (s.DateTimeExpiration > now)
                            && (s.SessionId == sessionId)
                            && (s.Token == token)
                            && (s.UserAgent == userAgent)
                            && (s.IPAddressString == clientIpAddress)
                            && (s.SessionType == sessionType)
                        )
                    );

                    var lines = new List<string>();
                    lines.Add($"--------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    lines.Add($"SIGNIN TOKEN");
                    lines.Add($"Session Found: {sessionDatabase != null}");
                    lines.Add($"now: {now}");
                    lines.Add($"SessionId: {sessionId}");
                    lines.Add($"token: {token}");
                    lines.Add($"useragent: {userAgent}");
                    lines.Add($"ipAddressString: {clientIpAddress}");
                    lines.Add($"sessionType: {sessionType}");
                    lines.Add($"--------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    AffinityConfiguration.Messages.Add(string.Join("<br />", lines));

                    // TODO: Remove for production.
                    if (AffinityConfiguration.DeploymentLocation == DeploymentLocation.BtsSaleem) { now = now.Subtract(TimeSpan.FromHours(2)); }

                    if (sessionDatabase == null)
                    {
                        var guid = Guid.NewGuid();
                        var rijndaelKey = new byte[GlobalConstants.AlgorithmSymmetricKeySize];
                        var rijndaelInitializationVector = new byte[GlobalConstants.AlgorithmSymmetricInitializationVectorSize];
                        var rsaKeyPair = Rsa.GenerateKeyPair(GlobalConstants.AlgorithmAsymmetricKeySize);

                        using (var randomNumberGenerator = RandomNumberGenerator.Create())
                        {
                            randomNumberGenerator.GetBytes(rijndaelKey);
                            randomNumberGenerator.GetBytes(rijndaelInitializationVector);
                        }

                        do { guid = Guid.NewGuid(); } while (context.Sessions.Any(s => s.Guid == guid));

                        sessionDatabase = new Session();
                        sessionDatabase.Guid = guid;
                        sessionDatabase.CultureName = "en";
                        sessionDatabase.Token = token;
                        sessionDatabase.SessionId = sessionId;
                        sessionDatabase.SessionType = sessionType;
                        sessionDatabase.UserAgent = userAgent;
                        sessionDatabase.IPAddressString = clientIpAddress;
                        sessionDatabase.DeviceType = DeviceType.Unknown;
                        sessionDatabase.DateTimeCreated = now;
                        sessionDatabase.DateTimeExpiration = sessionDatabase.DateTimeCreated.Add(TimeSpan.FromDays(1));
                        sessionDatabase.RijndaelKey = Convert.ToBase64String(rijndaelKey);
                        sessionDatabase.RijndaelInitializationVector = Convert.ToBase64String(rijndaelInitializationVector);
                        sessionDatabase.RsaKeyPublic = rsaKeyPair.KeyPublic.KeyToString();
                        sessionDatabase.RsaKeyPrivate = rsaKeyPair.KeyPrivate.KeyToString();
                        sessionDatabase.User = userDatabase;
                        sessionDatabase.UserId = userDatabase.Id;
                        sessionDatabase.Tenant = tenantDatabase;
                        sessionDatabase.TenantId = tenantDatabase.Id;
                        context.Sessions.Add(sessionDatabase);
                        context.SaveChanges();
                    }
                    else
                    {
                        sessionDatabase.DateTimeExpiration = now.Add(TimeSpan.FromDays(1));
                        context.SaveChanges();
                    }

                    var sessions = userDatabase.Sessions.Where(s => s.DateTimeExpiration < now.Subtract(TimeSpan.FromDays(30)));
                    foreach (var s in sessions) { context.Sessions.Remove(s); }
                    context.SaveChanges();

                    sessionDatabase = context.Sessions.AsNoTracking().Single(s => s.Id == sessionDatabase.Id);
                    userDatabase = context.Users.AsNoTracking().Include(p => p.Roles).AsNoTracking().Single(u => u.Id == userDatabase.Id);
                    tenantDatabase = context.Tenants.AsNoTracking().Include(t => t.TenantSubscriptions).Single(t => t.Id == tenantDatabase.Id);

                    userDatabase.PasswordHash = "";
                    userDatabase.PasswordSalt = "";
                    sessionDatabase.RijndaelKey = "";
                    sessionDatabase.RijndaelInitializationVector = "";
                    sessionDatabase.RsaKeyPrivate = "";
                    sessionDatabase.RsaKeyPublic = (sessionType == SessionType.Api) ? sessionDatabase.RsaKeyPublic : "";
                    tenantDatabase.RsaKeyPrivate = "";
                    tenantDatabase.RsaKeyPublic = (sessionType == SessionType.Api) ? tenantDatabase.RsaKeyPublic : "";
                    tenantDatabase.StorageAccessKeyPrimary = "";
                    tenantDatabase.StorageAccessKeySecondary = "";
                    tenantDatabase.StorageConnectionStringPrimary = (sessionType == SessionType.Mvc) ? tenantDatabase.StorageConnectionStringPrimary : "";
                    tenantDatabase.StorageConnectionStringSecondary = (sessionType == SessionType.Mvc) ? tenantDatabase.StorageConnectionStringSecondary : "";

                    if (sessionType == SessionType.Api)
                    {
                        foreach (var ts in tenantDatabase.TenantSubscriptions) { ts.Tenant = null; }
                        foreach (var r in userDatabase.Roles) { r.Users.Clear(); r.UserRoles.Clear(); }
                    }

                    // The out parameter [tenantUserSession] should not be assigned until
                    //	the objects passed to its constructor are fully populated and scrubbed.
                    tenantUserSession = new TenantUserSession(tenantDatabase, userDatabase, sessionDatabase);

                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool SignOut(string domain, string username, string token, out Exception exception)
        {
            var result = false;
            Tenant tenant = null;

            exception = null;

            try
            {
                if (!MasterTenantManagement.GetTenantByDomain(domain, out tenant, out exception)) { throw (exception); }
                if (tenant == null) { throw (new DomainNotFoundException()); }

                using (var context = new ContextTenant(tenant.DatabaseConnectionString))
                {
                    var user = context.Users.SingleOrDefault(s => s.UserName == username);
                    var session = context.Sessions.SingleOrDefault(s => s.Token == token);

                    if (user == null) { throw (new TokenInvalidException()); }
                    if (session == null) { throw (new TokenInvalidException()); }
                    if (session.UserId != user.Id) { throw (new TokenInvalidException()); }
                    if (session.TenantId != tenant.Id) { throw (new TokenInvalidException()); }

                    session.DateTimeExpiration = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(1));

                    context.SaveChanges();

                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static bool ValidateToken
        (
            string token,
            SessionType sessionType,
            string domain,
            string username,
            string clientIpAddress,
            string userAgent,
            long ticks,
            string sessionId,
            out TenantUserSession tenantUserSession,
            out Exception exception
        )
        {
            var result = false;

            exception = null;
            tenantUserSession = null;

            try
            {
                AuthenticationManagement.ThrowOnInvalidToken(token, sessionType, domain, username, clientIpAddress, userAgent, ticks, sessionId, out tenantUserSession);

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public static void ThrowOnInvalidToken
        (
            string token,
            SessionType sessionType,
            string domain,
            string username,
            string clientIpAddress,
            string userAgent,
            long ticks,
            string sessionId,
            out TenantUserSession tenantUserSession
        )
        {
            User userDatabase = null;
            Exception exception = null;
            Tenant tenantDatabase = null;
            Session sessionDatabase = null;

            tenantUserSession = null;
            if ((clientIpAddress != null) && (!clientIpAddress.Contains("::1")))
            {
                if (clientIpAddress.Contains(":"))
                {
                    clientIpAddress = clientIpAddress.Split(new string[] { ":" }, StringSplitOptions.None)[0];
                }
            }
            if (!MasterTenantManagement.GetTenantByDomain(domain, out tenantDatabase, out exception)) { throw (exception); }
            if (tenantDatabase == null) { throw (new DomainNotFoundException()); }

            if (!tenantDatabase.TenantSubscriptions.Any(ts => ts.IsActive)) { throw (new TenantNotAuthorizedException()); }

            using (var context = new ContextTenant(tenantDatabase.DatabaseConnectionString))
            {
                var now = DateTime.UtcNow;
                var sessionsDatabase = context
                    .Sessions
                    .Include(p => p.User)
                    .Where
                    (
                        s =>
                        (
                            // TODO: Add checks.
                            (s.DateTimeCreated < now)
                            && (s.DateTimeExpiration > now)
                            && (s.SessionId == sessionId)
                            && (s.Token == token)
                            && (s.UserAgent == userAgent)
                            && (s.IPAddressString == clientIpAddress)
                            && (s.SessionType == sessionType)
                        )
                    )
                    .ToList();

                var lines = new List<string>();
                lines.Add($"--------------------------------------------------------------------------------------------------------------------------------------------------------------");
                lines.Add($"VALIDATE TOKEN");
                lines.Add($"Session Found: {sessionDatabase != null}");
                lines.Add($"now: {now}");
                lines.Add($"SessionId: {sessionId}");
                lines.Add($"token: {token}");
                lines.Add($"useragent: {userAgent}");
                lines.Add($"ipAddressString: {clientIpAddress}");
                lines.Add($"sessionType: {sessionType}");
                lines.Add($"--------------------------------------------------------------------------------------------------------------------------------------------------------------");
                AffinityConfiguration.Messages.Add(string.Join("<br />", lines));

                if (sessionsDatabase.Count > 1)
                {
                    // Order by Id (descending).
                    sessionsDatabase.Sort((x, y) => x.Id.CompareTo(y.Id));

                    do
                    {
                        context.Sessions.Remove(sessionsDatabase.First());
                        context.SaveChanges();
                        sessionsDatabase.Remove(sessionsDatabase.First());
                    }
                    while (sessionsDatabase.Count > 1);

                    context.SaveChanges();
                }

                if (sessionsDatabase.Count < 1) { throw (new TokenInvalidException()); }
                else if (sessionsDatabase.Count > 1) { throw (new Exception("Multiple session matches were found. This is a dev bug!")); }

                sessionDatabase = sessionsDatabase.Single();
                sessionDatabase = context.Sessions.AsNoTracking().Include(s => s.User).Single(s => s.Id == sessionDatabase.Id);
                userDatabase = context.Users.AsNoTracking().Include(u => u.Roles).Include(u => u.UserRoles).Single(u => u.Id == sessionDatabase.User.Id);

                userDatabase.PasswordHash = "";
                userDatabase.PasswordSalt = "";
                sessionDatabase.RijndaelKey = "";
                sessionDatabase.RijndaelInitializationVector = "";
                sessionDatabase.RsaKeyPublic = (sessionType == SessionType.Api) ? sessionDatabase.RsaKeyPublic : "";
                sessionDatabase.RsaKeyPrivate = "";
                tenantDatabase.RsaKeyPublic = (sessionType == SessionType.Api) ? tenantDatabase.RsaKeyPublic : "";
                tenantDatabase.RsaKeyPrivate = "";
                tenantDatabase.StorageAccessKeyPrimary = "";
                tenantDatabase.StorageAccessKeySecondary = "";
                //tenantDatabase.StorageConnectionStringPrimary = "";
                //tenantDatabase.StorageConnectionStringSecondary = "";

                if (sessionType == SessionType.Api)
                {
                    // Remove circular references for DTO Serialization.
                    foreach (var ts in tenantDatabase.TenantSubscriptions) { ts.Tenant = null; }
                    foreach (var r in userDatabase.Roles) { r.Users.Clear(); r.UserRoles.Clear(); }
                }

                // The out parameter [tenantUserSession] should not be assigned until
                //	the objects passed to its constructor are fully populated and scrubbed.
                tenantUserSession = new TenantUserSession(tenantDatabase, userDatabase, sessionDatabase);
            }
        }


        public static bool ValidateInviteKey
        (
            SessionType sessionType,
            Guid inviteGuid,
            string domain,
            double InviteTimeOutHrs,
            out Tenant tenantDatabase,
            out Exception exception
        )
        {
            var result = false;
            User userDatabase = null;
            var now = DateTime.UtcNow;
            tenantDatabase = null;
            exception = null;
            try
            {
                if (!MasterTenantManagement.GetTenantByDomain(domain, out tenantDatabase, out exception)) { throw (exception); }
                if (tenantDatabase == null) { throw (new DomainNotFoundException()); }
                using (var context = new ContextTenant(tenantDatabase.DatabaseConnectionString))
                {
                    tenantDatabase = context.Tenants.SingleOrDefault();
                    userDatabase = context.Users.Where(x => x.InviteGuid == inviteGuid).SingleOrDefault();
                    if (userDatabase == null) { throw new Exception("Unable to find the invitation. This might happen for the follwoing reason:"+Environment.NewLine+ "1) If the user is already a member." + Environment.NewLine + "2)Invalid key or domain"); }
                    var expiryDate = userDatabase.DateTimeCreated.AddHours(InviteTimeOutHrs);
                    if (DateTime.UtcNow >= expiryDate) { throw new Exception("The invitation was expired, please contact the sender for a new invitation."); }
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
            return (result);
        }
        //TOBE DELETED
        public static void GetAllUsers()
        {
            using (var context = new ContextMaster())
            {

                var user = context.Users.First();
                user.Email = user.Email + " LOL";
                context.SaveChanges();
                //return context.Users.First().Email;
            }
        }

        public static List<string> GetAllRole()
        {
            List<string> roles = new List<string>();
            using (var context =new ContextMaster())
            {
                var role = context.Roles.ToList();
            }
            return roles;
        }

        public static bool ValidateInviteKeyAndCredentials
        (
            SessionType sessionType,
            Guid inviteGuid,
            string domain,
            string email,
            string currentPassword,
            double InviteTimeOutHrs,
            out Tenant tenantDatabase,
            out Exception exception
        )
        {
            var result = false;
            User user = null;
            var now = DateTime.UtcNow;
            tenantDatabase = null;
            exception = null;
            try
            {
                if (!MasterTenantManagement.GetTenantByDomain(domain, out tenantDatabase, out exception)) { throw (exception); }
                if (tenantDatabase == null) { throw (new DomainNotFoundException()); }
                using (var context = new ContextTenant(tenantDatabase.DatabaseConnectionString))
                {
                    user = context.Users.Where(x => x.InviteGuid == inviteGuid && x.Email == email).SingleOrDefault();
                    if (user == null) { throw new Exception(@"Unable to find the invitation. This might happen for the follwoing reason:" + Environment.NewLine + "1) If the user is already a member." + Environment.NewLine + "2)Invalid key or domain." + Environment.NewLine + "3)Invalid credentials."); }
                    var expiryDate = user.DateTimeCreated.AddHours(InviteTimeOutHrs);
                    if ( DateTime.UtcNow >= expiryDate) { throw new Exception("The invitation was expired, please contact the sender for a new invitation."); }
                    string passwordHash = "";
                    if (!Sha.GenerateHash((currentPassword ?? ""), GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind, out passwordHash, out exception)) { throw (exception); }
                    if (!PasswordHash.ValidatePassword(passwordHash, user.PasswordHash)) { throw (new AuthenticationException(isValidDomain: true, isValidUsername: true, isValidPassword: false)); }
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
            return (result);
        }
    }
}