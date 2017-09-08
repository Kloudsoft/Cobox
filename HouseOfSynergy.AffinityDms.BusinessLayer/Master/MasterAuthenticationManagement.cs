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
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Master
{
	public static class MasterAuthenticationManagement
	{
		/// <summary>
		/// Authentication using: [Salted password hashing with PBKDF2-SHA1].
		/// Implementation available at: [HouseOfSynergy.PowerTools.Library.Security.Cryptography.PasswordHash].
		/// The password should never reach here in plain text. It should b encrypted using Sha512 in TypeScript or JavaScript.
		/// A C# implementation of Sha512 is available at: [HouseOfSynergy.PowerTools.Library.Security.Cryptography.Sha].
		/// </summary>
		/// <param name="sessionType">The origin of the request.</param>
		/// <param name="username">Username in plain text.</param>
		/// <param name="passwordHash">Password from client hashed using Sha512.</param>
		/// <param name="token">The user object fetched.</param>
		/// <param name="exception">Populates an exception where applicable.</param>
		/// <returns></returns>
		public static bool SignIn
		(
			SessionType sessionType,
			string username,
			string passwordHash,
			string clientIpAddress,
			string userAgent,
			long ticks,
			string sessionId,
			out MasterUserSession masterUserSession,
			out Exception exception
		)
		{
			var result = false;
			var now = DateTime.UtcNow;
			MasterUser userDatabase = null;
			MasterSession sessionDatabase = null;

			exception = null;
			masterUserSession = null;

			try
			{
				using (var context = new ContextMaster())
				{
					userDatabase = context.Users.SingleOrDefault(u => (u.UserName == username));
					if (userDatabase == null) { throw (new UserNotFoundException()); }

					if (!PasswordHash.ValidatePassword(passwordHash, userDatabase.PasswordHash)) { throw (new AuthenticationException()); }

					var token
							= userDatabase.Id.ToString()
							+ EntityConstants.TokenDelimiter
							+ userDatabase.UserName
							+ EntityConstants.TokenDelimiter
							+ userDatabase.AuthenticationType.ToString()
							+ EntityConstants.TokenDelimiter
							+ (userDatabase.ActiveDirectoryId ?? "").Trim()
							+ EntityConstants.TokenDelimiter
							+ ""
							+ EntityConstants.TokenDelimiter
							+ sessionType.ToString()
							+ EntityConstants.TokenDelimiter
							+ EntityConstants.TokenDelimiter
							+ EntityConstants.TokenDelimiter;

					// TODO: Remove for production.
					if (AffinityConfiguration.DeploymentLocation == DeploymentLocation.BtsSaleem) { now = now.Add(TimeSpan.FromHours(1)); }

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
					lines.Add($"SIGNIN");
					lines.Add($"Session Found: {sessionDatabase != null}");
					lines.Add($"now: {now}");
					lines.Add($"SessionId: {sessionId}");
					lines.Add($"token: {token}");
					lines.Add($"useragent: {userAgent}");
					lines.Add($"ipAddressString: {clientIpAddress}");
					lines.Add($"sessionType: {sessionType}");
					lines.Add($"--------------------------------------------------------------------------------------------------------------------------------------------------------------");
					AffinityConfiguration.Messages.Add(string.Join("<br />", lines));

					if (sessionDatabase == null)
					{
						var guid = Guid.NewGuid();
						var rijndaelKey = new byte [GlobalConstants.AlgorithmSymmetricKeySize];
						var rijndaelInitializationVector = new byte [GlobalConstants.AlgorithmSymmetricInitializationVectorSize];
						var rsaKeyPair = Rsa.GenerateKeyPair(GlobalConstants.AlgorithmAsymmetricKeySize);

						using (var randomNumberGenerator = RandomNumberGenerator.Create())
						{
							randomNumberGenerator.GetBytes(rijndaelKey);
							randomNumberGenerator.GetBytes(rijndaelInitializationVector);
						}

						do { guid = Guid.NewGuid(); } while (context.Sessions.Any(s => s.Guid == guid));

						sessionDatabase = new MasterSession();
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
						context.Sessions.Add(sessionDatabase);
						context.SaveChanges();
					}
					else
					{
						sessionDatabase.DateTimeExpiration = DateTime.UtcNow.Add(TimeSpan.FromDays(1));
						context.SaveChanges();
					}

					var sessions = userDatabase.Sessions.Where(s => s.DateTimeExpiration < DateTime.UtcNow.Subtract(TimeSpan.FromDays(30)));
					foreach (var s in sessions) { context.Sessions.Remove(s); }
					context.SaveChanges();

					sessionDatabase = context.Sessions.AsNoTracking().Single(s => s.Id == sessionDatabase.Id);
					userDatabase = context.Users.AsNoTracking().Include(p => p.Roles).AsNoTracking().Single(u => u.Id == userDatabase.Id);

					userDatabase.PasswordHash = "";
					userDatabase.PasswordSalt = "";
					sessionDatabase.RijndaelKey = "";
					sessionDatabase.RijndaelInitializationVector = "";
					sessionDatabase.RsaKeyPrivate = "";
					sessionDatabase.RsaKeyPublic = (sessionType == SessionType.Api) ? sessionDatabase.RsaKeyPublic : "";

					masterUserSession = new MasterUserSession(userDatabase, sessionDatabase);

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool SignOut (string token, out Exception exception)
		{
			var result = false;

			exception = null;

			try
			{
				using (var context = new ContextMaster())
				{
					var session = context.Sessions.SingleOrDefault(s => s.Token == token);
					if (session == null) { throw (new TokenInvalidException()); }

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
			string username,
			string clientIpAddress,
			string userAgent,
			long ticks,
			string sessionId,
			out MasterUserSession masterUserSession,
			out Exception exception
		)
		{
			var result = false;

			exception = null;
			masterUserSession = null;

			try
			{
				MasterAuthenticationManagement.ThrowOnInvalidToken(token, sessionType, username, clientIpAddress, userAgent, ticks, sessionId, out masterUserSession);

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
			string username,
			string clientIpAddress,
			string userAgent,
			long ticks,
			string sessionId,
			out MasterUserSession masterUserSession
		)
		{
			MasterUser userDatabase = null;
            MasterSession sessionDatabase = null;

            masterUserSession = null;

            if (clientIpAddress != null)
            {
                if (clientIpAddress.Contains(":"))
                {
                    clientIpAddress = clientIpAddress.Split(new string[] { ":" }, StringSplitOptions.None)[0];
                }
            }

			using (var context = new ContextMaster())
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
				lines.Add($"Session Found: {sessionsDatabase != null}");
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
                userDatabase = context.Users.AsNoTracking().Include(u => u.Roles).Single(u => u.Id == sessionDatabase.User.Id);

				masterUserSession = new MasterUserSession(userDatabase, sessionDatabase);
			}
		}
	}
}