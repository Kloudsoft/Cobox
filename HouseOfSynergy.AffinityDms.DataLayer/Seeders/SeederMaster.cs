using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.DataLayer.Seeders
{
	public static class SeederMaster
	{
		public static void Seed (ContextMaster contextMaster)
		{
			var random = new System.Random();

			try
			{
				//====================================================================================================
				#region Master: Culture Creation.
				//====================================================================================================

				{
					var cultureInfos = CultureInfo
						.GetCultures(CultureTypes.AllCultures)
						.Where
						(
							c =>
							(
								(c.Name == "en")
								|| (c.Name == "en-US")
								|| (c.Name == "en-GB")
								|| (c.Name == "zh-Hans")
								|| (c.Name == "zh-Hant")
								|| (c.Name == "zh-SG")
								|| (c.Name == "zh-TW")
								|| (c.Name == "ur")
								|| (c.Name == "ur-PK")
							)
						)
						.OrderBy(c => (c.Name != "en"))
						.ThenBy(c => (c.Name != "en-US"))
						.ThenBy(c => (c.Name != "en-GB"))
						.ThenBy(c => (c.Name != "zh-Hans"))
						.ThenBy(c => (c.Name != "zh-Hant"))
						.ThenBy(c => (c.Name != "zh-SG"))
						.ThenBy(c => (c.Name != "zh-TW"))
						.ThenBy(c => (c.Name != "ur"))
						.ThenBy(c => (c.Name != "ur-PK"))
						.ToList();

					foreach (var cultureInfo in cultureInfos)
					{
						var culture = new Culture();

						culture.Name = cultureInfo.Name;
						culture.LocaleId = cultureInfo.LCID;
						culture.NameNative = cultureInfo.NativeName;
						culture.NameDisplay = cultureInfo.DisplayName;
						culture.NameEnglish = cultureInfo.EnglishName;
						culture.NameIsoTwoLetter = cultureInfo.TwoLetterISOLanguageName;
						culture.NameIsoThreeLetter = cultureInfo.ThreeLetterISOLanguageName;
						culture.NameWindowsThreeLetter = cultureInfo.ThreeLetterWindowsLanguageName;

						contextMaster.Cultures.Add(culture);
					}

					contextMaster.SaveChanges();
				}

				//====================================================================================================
				#endregion Master: Culture Creation.
				//====================================================================================================

				//====================================================================================================
				#region Master: Role & User Creation.
				//====================================================================================================

				{
					var password = "";
					var passwordHashClient = "";
					var passwordHashServer = "";
					List<MasterRoleType> roles = null;
					var keyPair = Rsa.GenerateKeyPair(GlobalConstants.AlgorithmAsymmetricKeySize);

					roles = EnumUtilities.GetValues<MasterRoleType>().Where(e => e != MasterRoleType.None).ToList();
					foreach (var role in roles) { contextMaster.Roles.Add(new MasterRole() { RoleType = role, Name = role.ToString(), Description = "", }); }

					contextMaster.SaveChanges();

					password = "audience";
					// Do not centralize the [passwordHashServer = PasswordHash.CreateHash(passwordHashClient)]!
					passwordHashClient = Sha.GenerateHash(password, GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind);

					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "admin", Email = "raheel.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Administrator", NameFamily = "Global", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "raheel.khan", Email = "raheel.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Raheel", NameFamily = "Khan", Address1 = "E-11/4", Address2 = "", City = "Islamabad", ZipOrPostCode = "44000", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 517-3303", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "junaid.sayed", Email = "sjunaid@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Junaid", NameFamily = "Sayed", Address1 = "G-13", Address2 = "", City = "Islamabad", ZipOrPostCode = "44000", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 245-2112", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "rizwan.khan", Email = "rizwan.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Rizwan", NameFamily = "Khan", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 280-7325", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "danish.muhammad", Email = "danish.muhammad@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Danish", NameFamily = "Muhammad", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (345) 243-5474", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "uzma.hashmi", Email = "uzma.hashmi@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Uzma", NameFamily = "Hashmi", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (303) 289-8969", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "kausar.khan", Email = "kausar.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Kausar", NameFamily = "Khan", Address1 = "?", Address2 = "", City = "Islamabad", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (334) 500-4781", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });

					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "lawrence", Email = "lawrence@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Lawrence", NameFamily = "", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "richard", Email = "richard@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Richard", NameFamily = "Chong", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "jeff", Email = "defang@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Jeff", NameFamily = "", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });
					passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
					contextMaster.Users.Add(new MasterUser() { UserName = "ben", Email = "ben@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Ben", NameFamily = "Tan", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, ActiveDirectoryId = "", });

					contextMaster.SaveChanges();

					foreach (var user in contextMaster.Users)
					{
						roles = EnumUtilities.GetValues<MasterRoleType>().Where(e => e != MasterRoleType.None).ToList();
						foreach (var role in roles) { user.Roles.Add(contextMaster.Roles.Single(e => e.RoleType == role)); }
					}

					contextMaster.SaveChanges();
				}

				//====================================================================================================
				#endregion Master: Role & User Creation.
				//====================================================================================================

				if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
				{
					//====================================================================================================
					#region Master: Tenant Creation.
					//====================================================================================================

					{
						{
							var tenant = new Tenant();
							var keyPair = Rsa.GenerateKeyPair(GlobalConstants.AlgorithmAsymmetricKeySize);

							tenant.Domain = "kloud-soft.com";
							tenant.CompanyName = "Kloud-Soft Private Limited";

							tenant.MasterTenantId = 1;
							tenant.TenantType = EntityMasterTenantType.Master;
							tenant.AuthenticationType = AuthenticationType.Local;
							tenant.MasterTenantToken = Guid.NewGuid().ToString("B");

							tenant.SetAllContacts("Raheel", "Khan", "E-11/4", "Islamabad", "ICT", "44000", "Pakistan", "+92 (321) 517-3303", "+92 (321) 517-3303", "raheel.khan@houseofsynergy.com");

							tenant.RsaKeyPublic = keyPair.KeyPublic.KeyToString();
							tenant.RsaKeyPrivate = keyPair.KeyPrivate.KeyToString();

							tenant.UrlResourceGroup = "kloudsoft-rg-affinity";
							tenant.UrlApi = "http://affinity-ecm-tenantportal.azurewebsites.net/Api/";

							tenant.UrlStorage = "kloudsoftstorage";
							tenant.UrlStorageBlob = "https://kloudsoftstorage.blob.core.windows.net/";
							tenant.UrlStorageTable = "https://kloudsoftstorage.table.core.windows.net/";
							tenant.UrlStorageQueue = "https://kloudsoftstorage.queue.core.windows.net/";
							tenant.UrlStorageFile = "https://kloudsoftstorage.file.core.windows.net/";

							tenant.StorageAccessKeyPrimary = "r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==";
							tenant.StorageAccessKeySecondary = "Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==";
							tenant.StorageConnectionStringPrimary = "DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==;";
							tenant.StorageConnectionStringSecondary = "DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==;";

							tenant.DatabaseConnectionString = AffinityConfigurationTenant.GetDatabaseConnectionString(tenant.MasterTenantId);

							contextMaster.Tenants.Add(tenant);
							contextMaster.SaveChanges();
						}

						{
							var tenant = new Tenant();
							var keyPair = Rsa.GenerateKeyPair(GlobalConstants.AlgorithmAsymmetricKeySize);

							tenant.Domain = "cobox.com";
							tenant.CompanyName = "Cobox";

							tenant.MasterTenantId = 2;
							tenant.TenantType = EntityMasterTenantType.Master;
							tenant.MasterTenantToken = Guid.NewGuid().ToString("B");

							tenant.SetAllContacts("Richard", "Chong", "", "", "", "", "Singapore", "9655 7994", "9655 7994", "richard@kloud-soft.com ");

							tenant.RsaKeyPublic = keyPair.KeyPublic.KeyToString();
							tenant.RsaKeyPrivate = keyPair.KeyPrivate.KeyToString();

							tenant.UrlResourceGroup = "affinity-ecm";
							tenant.UrlApi = "http://cobox-demo.azurewebsites.net/Api/";

							tenant.UrlStorage = "affinityecmstorage";
							tenant.UrlStorageBlob = "https://affinityecmstorage.blob.core.windows.net/";
							tenant.UrlStorageTable = "https://affinityecmstorage.table.core.windows.net/";
							tenant.UrlStorageQueue = "https://affinityecmstorage.queue.core.windows.net/";
							tenant.UrlStorageFile = "https://affinityecmstorage.file.core.windows.net/";

							tenant.StorageAccessKeyPrimary = "5xuKR9BvlL2xS251eAI0cuPwYjkt2Mhru2xCqx+KiEcPsqXZgmILe6N0FPHuI9g8lNeeyRkOJcDrrhFGsOab/g==";
							tenant.StorageAccessKeySecondary = "BQFfUpo/G7BudTk6nac0b4I7YjhcBf/ygfRat/QY7ndis7y4dcxVt2guDV0s816/o9Qu6oN7IfKPjMnfOLSAcg==";
							tenant.StorageConnectionStringPrimary = "DefaultEndpointsProtocol=https;AccountName=coboxstorage;AccountKey=5xuKR9BvlL2xS251eAI0cuPwYjkt2Mhru2xCqx+KiEcPsqXZgmILe6N0FPHuI9g8lNeeyRkOJcDrrhFGsOab/g==;";
							tenant.StorageConnectionStringSecondary = "DefaultEndpointsProtocol=https;AccountName=coboxstorage;AccountKey=BQFfUpo/G7BudTk6nac0b4I7YjhcBf/ygfRat/QY7ndis7y4dcxVt2guDV0s816/o9Qu6oN7IfKPjMnfOLSAcg==;";

							tenant.DatabaseConnectionString = AffinityConfigurationTenant.GetDatabaseConnectionString(tenant.MasterTenantId);

							contextMaster.Tenants.Add(tenant);
							contextMaster.SaveChanges();
						}
					}

					//====================================================================================================
					#endregion Master: Tenant Creation.
					//====================================================================================================

					//====================================================================================================
					#region Master: Tenant Subscription Creation.
					//====================================================================================================

					{
						var id = 0;
						contextMaster.Subscriptions.Add(new Subscription() { IsDemo = true, MasterSubscriptionId = (++id), IsActive = true, NumberOfFormsAllowed = random.Next(0, 1000), NumberOfPagesAllowed = random.Next(0, 1000), NumberOfTemplatesAllowed = random.Next(0, 1000), NumberOfUsersAllowed = random.Next(0, 1000), AllowScanning = true, AllowBranding = true, AllowTemplateWorkflows = true, Description = "Text subscription 1." });
						contextMaster.Subscriptions.Add(new Subscription() { IsDemo = false, MasterSubscriptionId = (++id), IsActive = true, NumberOfFormsAllowed = random.Next(0, 1000), NumberOfPagesAllowed = random.Next(0, 1000), NumberOfTemplatesAllowed = random.Next(0, 1000), NumberOfUsersAllowed = random.Next(0, 1000), AllowScanning = true, AllowBranding = true, AllowTemplateWorkflows = true, Description = "Text subscription 2." });
						contextMaster.Subscriptions.Add(new Subscription() { IsDemo = false, MasterSubscriptionId = (++id), IsActive = true, NumberOfFormsAllowed = random.Next(0, 1000), NumberOfPagesAllowed = random.Next(0, 1000), NumberOfTemplatesAllowed = random.Next(0, 1000), NumberOfUsersAllowed = random.Next(0, 1000), AllowScanning = true, AllowBranding = true, AllowTemplateWorkflows = true, Description = "Text subscription 3." });
						contextMaster.Subscriptions.Add(new Subscription() { IsDemo = false, MasterSubscriptionId = (++id), IsActive = true, NumberOfFormsAllowed = random.Next(0, 1000), NumberOfPagesAllowed = random.Next(0, 1000), NumberOfTemplatesAllowed = random.Next(0, 1000), NumberOfUsersAllowed = random.Next(0, 1000), AllowScanning = true, AllowBranding = true, AllowTemplateWorkflows = true, Description = "Text subscription 4." });
						contextMaster.SaveChanges();
						foreach (var s in contextMaster.Subscriptions.ToList())
						{
							s.MasterSubscriptionId = s.Id;
						}
						contextMaster.SaveChanges();

						var tenants = contextMaster.Tenants.ToList();
						var subscriptions = contextMaster.Subscriptions.Where(s => s.IsActive).ToList();

						foreach (var tenant in tenants)
						{
							var subscription = subscriptions [random.Next(0, subscriptions.Count)];

							var tenantSubscription = new TenantSubscription();

							tenantSubscription.IsDemo = subscription.IsDemo;
							tenantSubscription.IsActive = subscription.IsActive;

							tenantSubscription.NumberOfPagesAllowed = subscription.NumberOfPagesAllowed;
							tenantSubscription.NumberOfTemplatesAllowed = subscription.NumberOfTemplatesAllowed;
							tenantSubscription.NumberOfFormsAllowed = subscription.NumberOfFormsAllowed;
							tenantSubscription.NumberOfUsersAllowed = subscription.NumberOfUsersAllowed;

							tenantSubscription.AllowScanning = subscription.AllowScanning;
							tenantSubscription.AllowBranding = subscription.AllowBranding;
							tenantSubscription.AllowTemplateWorkflows = subscription.AllowTemplateWorkflows;

							tenantSubscription.Time = DateTime.UtcNow;
							tenantSubscription.DateTimeStart = DateTime.UtcNow;
							tenantSubscription.DateTimeExpires = DateTime.UtcNow.Add(TimeSpan.FromDays(365));

							tenantSubscription.TenantId = tenant.Id;
							tenantSubscription.SubscriptionId = subscription.Id;

							contextMaster.TenantSubscriptions.Add(tenantSubscription);

							contextMaster.SaveChanges();

							tenantSubscription.MasterTenantSubscriptionId = tenantSubscription.Id;

						}
					}

					//====================================================================================================
					#endregion Master: Tenant Subscription Creation.
					//====================================================================================================

					//====================================================================================================
					#region Tenant Database Creation & Seeding.
					//====================================================================================================

					{
						foreach (var tenantMasterDatabase in contextMaster.Tenants.AsNoTracking().ToList())
						{
							var tenantMaster = contextMaster.Tenants.AsNoTracking().Single(t => t.Id == tenantMasterDatabase.Id);

							tenantMaster.TenantType = EntityMasterTenantType.Tenant;

							ContextTenant.Initialize(tenantMaster.DatabaseConnectionString);

							using (var contextTenant = new ContextTenant(tenantMaster.DatabaseConnectionString))
							{
								//====================================================================================================
								#region Tenant: Culture Creation.
								//====================================================================================================

								{
									var cultureInfos = CultureInfo
										.GetCultures(CultureTypes.AllCultures)
										.Where
										(
											c =>
											(
												(c.Name == "en")
												|| (c.Name == "en-US")
												|| (c.Name == "en-GB")
												|| (c.Name == "zh-Hans")
												|| (c.Name == "zh-Hant")
												|| (c.Name == "zh-SG")
												|| (c.Name == "zh-TW")
												|| (c.Name == "ur")
												|| (c.Name == "ur-PK")
											)
										)
										.OrderBy(c => (c.Name != "en"))
										.ThenBy(c => (c.Name != "en-US"))
										.ThenBy(c => (c.Name != "en-GB"))
										.ThenBy(c => (c.Name != "zh-Hans"))
										.ThenBy(c => (c.Name != "zh-Hant"))
										.ThenBy(c => (c.Name != "zh-SG"))
										.ThenBy(c => (c.Name != "zh-TW"))
										.ThenBy(c => (c.Name != "ur"))
										.ThenBy(c => (c.Name != "ur-PK"))
										.ToList();

									foreach (var cultureInfo in cultureInfos)
									{
										contextTenant.Cultures.Add(new Culture().FromCultureInfo(cultureInfo));
									}

									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Culture Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Tenant Creation.
								//====================================================================================================

								{
									contextTenant.Tenants.Add(tenantMaster);
									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Tenant Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Subscription Creation.
								//====================================================================================================

								{
									var subscriptionsMaster = contextMaster.Subscriptions.AsNoTracking().ToList();
									contextTenant.Subscriptions.AddRange(subscriptionsMaster);
									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Subscription Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Tenant Subscription Creation.
								//====================================================================================================

								{
									var tenantSubscriptionsMaster = contextMaster.TenantSubscriptions.AsNoTracking().Where(ts => ts.TenantId == tenantMaster.MasterTenantId).ToList();

									foreach (var tenantSubscriptionMaster in tenantSubscriptionsMaster)
									{
										tenantSubscriptionMaster.Id = 0;
										tenantSubscriptionMaster.Tenant = null;
										tenantSubscriptionMaster.TenantId = tenantMaster.Id;
									}

									contextTenant.TenantSubscriptions.AddRange(tenantSubscriptionsMaster);
									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Tenant Subscription Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Department Creation.
								//====================================================================================================

								{
									contextTenant.Departments.Add(new Department() { Name = "CEO", });
									contextTenant.Departments.Add(new Department() { Name = "Board of Directors", });
									contextTenant.Departments.Add(new Department() { Name = "Finance", });
									contextTenant.Departments.Add(new Department() { Name = "Accounting", });
									contextTenant.Departments.Add(new Department() { Name = "Human Resource", });
									contextTenant.Departments.Add(new Department() { Name = "Information Technology", });
									contextTenant.Departments.Add(new Department() { Name = "Sales", });
									contextTenant.Departments.Add(new Department() { Name = "Marketing", });
									contextTenant.Departments.Add(new Department() { Name = "Operations", });
									contextTenant.Departments.Add(new Department() { Name = "Manufacturing", });

									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Department Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Role & User Creation.
								//====================================================================================================

								{
									var password = "";
									var passwordHashClient = "";
									var passwordHashServer = "";
									List<TenantRoleType> roles = null;
									var departments = contextTenant.Departments.ToList();
									var keyPair = Rsa.GenerateKeyPair(GlobalConstants.AlgorithmAsymmetricKeySize);

									roles = EnumUtilities.GetValues<TenantRoleType>().Where(e => e != TenantRoleType.None).ToList();
									foreach (var role in roles) { contextTenant.Roles.Add(new Role() { RoleType = role, Name = role.ToString(), Description = "", }); }

									contextTenant.SaveChanges();

									password = "audience";
									// Do not centralize the [passwordHashServer = PasswordHash.CreateHash(passwordHashClient)]!
									passwordHashClient = Sha.GenerateHash(password, GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind);

									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "admin", Email = "raheel.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Administrator", NameFamily = "Global", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "raheel.khan", Email = "raheel.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Raheel", NameFamily = "Khan", Address1 = "E-11/4", Address2 = "", City = "Islamabad", ZipOrPostCode = "44000", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 517-3303", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "junaid.sayed", Email = "sjunaid@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Junaid", NameFamily = "Sayed", Address1 = "G-13", Address2 = "", City = "Islamabad", ZipOrPostCode = "44000", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 245-2112", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "rizwan.khan", Email = "rizwan.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Rizwan", NameFamily = "Khan", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 280-7325", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "danish.muhammad", Email = "danish.muhammad@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Danish", NameFamily = "Muhammad", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (345) 243-5474", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "uzma.hashmi", Email = "uzma.hashmi@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Uzma", NameFamily = "Hashmi", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (303) 289-8969", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "kausar.khan", Email = "kausar.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Kausar", NameFamily = "Khan", Address1 = "?", Address2 = "", City = "Islamabad", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (334) 500-4781", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });

									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "lawrence", Email = "lawrence@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Lawrence", NameFamily = "", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "richard", Email = "richard@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Richard", NameFamily = "Chong", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "jeff", Email = "defang@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Jeff", NameFamily = "", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });
									passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
									contextTenant.Users.Add(new User() { UserName = "ben", Email = "ben@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Ben", NameFamily = "Tan", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, IsActive = true, });

									contextTenant.SaveChanges();

									foreach (var user in contextTenant.Users)
									{
										roles = EnumUtilities.GetValues<TenantRoleType>().Where(e => e != TenantRoleType.None).ToList();
										foreach (var role in roles) { user.Roles.Add(contextTenant.Roles.Single(e => e.RoleType == role)); }
                                        if (user.Id == 1)
                                        {
                                            foreach (var role in roles) { user.UserRoles.Add(new UserRole { RoleId = (int)role, UserId = user.Id }); }
                                        }
									}

									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Role & User Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Folder Creation.
								//====================================================================================================

								{
									var departments = contextTenant.Departments.ToList();

									var folderRoot = new Folder() { Name = tenantMaster.CompanyName, FolderType = FolderType.Root, DateTimeCreated = DateTime.UtcNow, DateTimeModified = DateTime.UtcNow, UserCreatedById = 1, };
									contextTenant.Folders.Add(folderRoot);
									contextTenant.SaveChanges();

									var folderEnterpriseRoot = new Folder() { Name = "Enterprise", FolderType = FolderType.EnterpriseRoot, DateTimeCreated = DateTime.UtcNow, DateTimeModified = DateTime.UtcNow, UserCreatedById = 1, };
									folderRoot.Folders.Add(folderEnterpriseRoot);
									contextTenant.SaveChanges();

									foreach (var department in departments)
									{
										var folderEnterpriseChild = new Folder() { Name = department.Name, FolderType = FolderType.EnterpriseChild, DateTimeCreated = DateTime.UtcNow, DateTimeModified = DateTime.UtcNow, UserCreatedById = 1, };
										folderEnterpriseRoot.Folders.Add(folderEnterpriseChild);
										contextTenant.SaveChanges();
									}

									//var folderProjectRoot = new Folder() { Name = "Projects", FolderType = FolderType.ProjectRoot, DateTimeCreated = DateTime.Now, DateTimeModified = DateTime.Now, UserCreatedById = 1, };
									//folderRoot.Folders.Add(folderProjectRoot);
									//contextTenant.SaveChanges();

									//for (int i = 0; i < 10; i++)
									//{
									//	var folderProjectChild = new Folder() { Name = $"Project {(i + 1).ToString().PadLeft(4, '0')}", FolderType = FolderType.ProjectChild, DateTimeCreated = DateTime.Now, DateTimeModified = DateTime.Now, UserCreatedById = 1, };
									//	folderProjectRoot.Folders.Add(folderProjectChild);
									//	contextTenant.SaveChanges();
									//}

									var folderSharedRoot = new Folder() { Name = "Shared", FolderType = FolderType.SharedRoot, DateTimeCreated = DateTime.UtcNow, DateTimeModified = DateTime.UtcNow, UserCreatedById = 1, };
									folderRoot.Folders.Add(folderSharedRoot);
									contextTenant.SaveChanges();

									var folderUserRoot = new Folder() { Name = "Private", FolderType = FolderType.UserRoot, DateTimeCreated = DateTime.UtcNow, DateTimeModified = DateTime.UtcNow, UserCreatedById = 1, };
									folderRoot.Folders.Add(folderUserRoot);
									contextTenant.SaveChanges();

									var users = contextTenant.Users.ToList();
									foreach (var folder in contextTenant.Folders)
									{
										var department = contextTenant.Departments.SingleOrDefault(d => d.Name == folder.Name);
										if (department != null) { folder.DepartmentId = department.Id; }

										foreach (var user in users)
										{
											contextTenant.UserFolders.Add(new UserFolder() { UserId = user.Id, FolderId = folder.Id, IsActive = true, });
										}
									}
									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Folder Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Document Creation.
								//====================================================================================================

								{
									//foreach (var user in contextTenant.Users.ToList())
									//{
									//	var documentCount = 2;
									//	var wordCountMinimum = 10;
									//	var wordCountMaximum = 21;
									//	var wordLengthMinimum = 1;
									//	var wordLengthMaximum = 11;
									//	random = new System.Random();
									//	var randomm = new System.Random();

									//	for (int i = 0; i < documentCount; i++)
									//	{
									//		var document = new Document();
									//		var words = new List<string>();
									//		var wordCount = randomm.Next(wordCountMinimum, wordCountMaximum);
									//		var folder = contextTenant.Folders.Single(f => (f.FolderType == FolderType.Root));

									//		for (int j = 0; j < wordCount; j++)
									//		{
									//			var word = "";
									//			var wordLength = randomm.Next(wordLengthMinimum, wordLengthMaximum);

									//			for (int k = 0; k < wordLength; k++)
									//			{
									//				word += (char) randomm.Next((int) 'a', ((int) 'z') + 1);
									//			}

									//			words.Add(word);
									//		}

									//		var content = string.Join(" ", words);
									//		var directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sample Documents"));
									//		var filename = words.OrderByDescending(s => s.Length).First().ToString() + ".txt";
									//		var file = new FileInfo(Path.Combine(directory.FullName, filename));

									//		document.Uploaded = false;
									//		document.Name = file.Name;
									//		document.SourceType = SourceType.DesktopFileSystem;
									//		document.FullTextOCRXML = "";
									//		document.IsDigital = true;
									//		document.DeviceName = "";
									//		document.Uploaded = true;
									//		document.FullTextOCRXML = null;
									//		document.DocumentType = DocumentType.Digital;
									//		document.State = DocumentState.UnMatched;
									//		document.Confidence = null;
									//		document.CountAttemptOcr = 0;
									//		document.IsFinalized = false;

									//		document.VersionCount = 1;
									//		document.LatestCheckedOutByUserId = user.Id;
									//		document.VersionMajor = 1;
									//		document.VersionMinor = 0;
									//		document.CheckedOutByUser = user;
									//		document.CheckedOutDateTime = DateTime.UtcNow;
									//		document.DateTime = DateTime.UtcNow;
									//		document.TemplateId = null;
									//		document.FileNameClient = file.FullName;
									//		document.FileNameServer = file.Name;
									//		document.ScanSessionId = null;
									//		document.IsCancelled = false;
									//		document.IsInTransit = true;
									//		document.Length = content.Length;
									//		document.Hash = Sha.GenerateHash(content, Encoding.UTF8, PowerTools.Library.Security.Cryptography.Sha.EnumShaKind.Sha512);
									//		//document.User = user;
									//		document.UserId = user.Id;
									//		document.IsPrivate = true;

									//		if (folder.Documents.Any())
									//		{
									//			var folderIds = contextTenant.Folders.Select(f => f.Id).ToList();
									//			var index = random.Next(0, folderIds.Count);
									//			var folderId = folderIds [index];

									//			folder = contextTenant.Folders.Single(f => (f.Id == folderId));
									//		}

									//		//document.Folder = folder;
									//		document.FolderId = folder.Id;

									//		contextTenant.Documents.Add(document);

									//		foreach (var w in words)
									//		{
									//			document.DocumentFragments.Add(new DocumentFragment() { FullTextOcr = w, });
									//		}

									//		contextTenant.SaveChanges();
									//		document.DocumentOriginalId = document.Id;
									//		document.DocumentParent = document.Id;
									//		contextTenant.Entry(document).State = EntityState.Modified;
									//		foreach (var item in contextTenant.Documents) { item.FileNameServer = item.Id.ToString() + "." + GlobalConstants.FileExtensionCloud; }

									//		contextTenant.SaveChanges();
									//	}
									//}
								}

								//====================================================================================================
								#endregion Tenant: Document Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Search Criteria Creation.
								//====================================================================================================

								{
									var criteria = new DocumentSearchCriteria();

									criteria.Name = "My Search 01";
									criteria.DateTimeFrom = new DateTime(2000, 01, 01);
									criteria.DateTimeUpTo = new DateTime(2020, 12, DateTime.DaysInMonth(2020, 12));
									criteria.TagsUser = null;
									criteria.TagsGlobal = null;
									criteria.Filename = null;
									criteria.FolderName = null;
									criteria.TemplateName = null;
									criteria.User = contextTenant.Users.First();
									criteria.UserId = criteria.User.Id;

									contextTenant.DocumentSearchCriteria.Add(criteria);
									contextTenant.SaveChanges();
								}

								//====================================================================================================
								#endregion Tenant: Search Criteria Creation.
								//====================================================================================================

								//====================================================================================================
								#region Tenant: Unknown.
								//====================================================================================================

								{
								}

								//====================================================================================================
								#endregion Tenant: Unknown.
								//====================================================================================================
							}
						}
					}

					//====================================================================================================
					#endregion Tenant Database Creation & Seeding.
					//====================================================================================================
				}
			}
			catch (Exception exception)
			{
				if (AffinityConfiguration.IsConfigurationDebug)
				{
					Debugger.Break();

					if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
					{
						Debug.Print(exception.ToString());
					}
				}
				else
				{
					throw;
				}
			}
		}
	}
}