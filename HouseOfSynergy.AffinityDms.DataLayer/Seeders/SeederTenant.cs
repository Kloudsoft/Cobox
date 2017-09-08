using System;
using System.Collections.Generic;
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
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.DataLayer.Seeders
{
	public static class SeederTenant
	{
		public static void Seed(ContextTenant contextTenant)
		{
			try
			{
				contextTenant.SaveChanges();
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

		//Created by Uzma Hashmi for creating tenant DB and seeding when tenant enter in master DB
		//public static bool Seed (ContextTenant contextTenant, Tenant tenant, out Exception exception)
		//{
		//	var random = new System.Random();
		//	exception = null;
		//	var result = false;

		//	try
		//	{

		//		//====================================================================================================
		//		#region Tenant: Culture Creation.
		//		//====================================================================================================

		//		{
		//			var cultureInfos = CultureInfo
		//			.GetCultures(CultureTypes.AllCultures)
		//			.Where
		//			(
		//				c =>
		//				(
		//					(c.Name == "en")
		//					|| (c.Name == "en-US")
		//					|| (c.Name == "en-GB")
		//					|| (c.Name == "zh-Hans")
		//					|| (c.Name == "zh-Hant")
		//					|| (c.Name == "zh-SG")
		//					|| (c.Name == "zh-TW")
		//					|| (c.Name == "ur")
		//					|| (c.Name == "ur-PK")
		//				)
		//			)
		//			.OrderBy(c => (c.Name != "en"))
		//			.ThenBy(c => (c.Name != "en-US"))
		//			.ThenBy(c => (c.Name != "en-GB"))
		//			.ThenBy(c => (c.Name != "zh-Hans"))
		//			.ThenBy(c => (c.Name != "zh-Hant"))
		//			.ThenBy(c => (c.Name != "zh-SG"))
		//			.ThenBy(c => (c.Name != "zh-TW"))
		//			.ThenBy(c => (c.Name != "ur"))
		//			.ThenBy(c => (c.Name != "ur-PK"))
		//			.ToList();

		//			foreach (var cultureInfo in cultureInfos)
		//			{
		//				contextTenant.Cultures.Add(new Culture().FromCultureInfo(cultureInfo));
		//				contextTenant.SaveChanges();
		//			}


		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Culture Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant Creation.
		//		{
		//			contextTenant.Tenants.Add(tenant);
		//			contextTenant.SaveChanges();
		//		}
		//		#endregion Tenant Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Subscription Creation.
		//		//====================================================================================================

		//		{
		//			//var subscriptionsMaster = contextTenant.Subscriptions.AsNoTracking().ToList();
		//			//contextTenant.Subscriptions.AddRange(subscriptionsMaster);
		//			//contextTenant.SaveChanges();
		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Subscription Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Tenant Subscription Creation.
		//		//====================================================================================================

		//		{
		//			//var tenantSubscriptionsMaster = contextTenant.TenantSubscriptions.AsNoTracking().Where(ts => ts.TenantId == tenant.MasterTenantId).ToList();

		//			//foreach (var tenantSubscriptionMaster in tenantSubscriptionsMaster)
		//			//{
		//			//	tenantSubscriptionMaster.Id = 0;
		//			//	tenantSubscriptionMaster.Tenant = null;
		//			//	tenantSubscriptionMaster.TenantId = tenant.Id;
		//			//}

		//			//contextTenant.TenantSubscriptions.AddRange(tenantSubscriptionsMaster);
		//			//contextTenant.SaveChanges();
		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Tenant Subscription Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Department Creation.
		//		//====================================================================================================

		//		{
		//			contextTenant.Departments.Add(new Department() { Name = "CEO", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Board of Directors", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Finance", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Accounting", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Human Resource", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Information Technology", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Sales", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Marketing", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Operations", });
		//			contextTenant.SaveChanges();
		//			contextTenant.Departments.Add(new Department() { Name = "Manufacturing", });
		//			contextTenant.SaveChanges();
		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Department Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Role & User Creation.
		//		//====================================================================================================

		//		{
		//			var password = "";
		//			var passwordHashClient = "";
		//			var passwordHashServer = "";
		//			List<TenantRoleType> roles = null;
		//			var departments = contextTenant.Departments.ToList();
		//			var keyPair = Rsa.GenerateKeyPair(GlobalConstants.AlgorithmAsymmetricKeySize);

		//			roles = EnumUtilities.GetValues<TenantRoleType>().Where(e => e != TenantRoleType.None).ToList();
		//			foreach (var role in roles) { contextTenant.Roles.Add(new Role() { RoleType = role, Name = role.ToString(), Description = "", }); }

		//			contextTenant.SaveChanges();

		//			password = "audience";
		//			// Do not centralize the [passwordHashServer = PasswordHash.CreateHash(passwordHashClient)]!
		//			passwordHashClient = Sha.GenerateHash(password, Encoding.UTF8, Sha.EnumShaKind.Sha512);

		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "raheel.khan", Email = "raheel.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Raheel", NameFamily = "Khan", Address1 = "E-11/4", Address2 = "", City = "Islamabad", ZipOrPostCode = "44000", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 517-3303", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "junaid.sayed", Email = "sjunaid@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Junaid", NameFamily = "Sayed", Address1 = "G-13", Address2 = "", City = "Islamabad", ZipOrPostCode = "44000", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 245-2112", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "rizwan.khan", Email = "rizwan.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Rizwan", NameFamily = "Khan", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (321) 280-7325", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "danish.muhammad", Email = "danish.muhammad@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Danish", NameFamily = "Muhammad", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (345) 243-5474", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "uzma.hashmi", Email = "uzma.hashmi@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Uzma", NameFamily = "Hashmi", Address1 = "?", Address2 = "", City = "Karachi", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (303) 289-8969", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "kausar.khan", Email = "kausar.khan@houseofsynergy.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Kausar", NameFamily = "Khan", Address1 = "?", Address2 = "", City = "Islamabad", ZipOrPostCode = "75500", Country = "Pakistan", PhoneWork = "", PhoneMobile = "+92 (334) 500-4781", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });

		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "lawrence", Email = "lawrence@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Lawrence", NameFamily = "", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "richard", Email = "richard@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Richard", NameFamily = "Chong", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "jeff", Email = "defang@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Jeff", NameFamily = "", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });
		//			passwordHashServer = PasswordHash.CreateHash(passwordHashClient);
		//			contextTenant.Users.Add(new User() { UserName = "ben", Email = "ben@kloud-soft.com", PasswordHash = passwordHashServer, PasswordSalt = passwordHashServer, NameGiven = "Ben", NameFamily = "Tan", Address1 = "", Address2 = "", City = "", ZipOrPostCode = "", Country = "Singapore", PhoneWork = "", PhoneMobile = "", DateTimeCreated = DateTime.UtcNow, AuthenticationType = AuthenticationType.Local, TenantId = 1, DepartmentId = departments [random.Next(0, departments.Count)].Id, });

		//			contextTenant.SaveChanges();

		//			foreach (var user in contextTenant.Users)
		//			{
		//				roles = EnumUtilities.GetValues<TenantRoleType>().Where(e => e != TenantRoleType.None).ToList();
		//				foreach (var role in roles) { user.Roles.Add(contextTenant.Roles.Single(e => e.RoleType == role)); }
		//			}

		//			contextTenant.SaveChanges();
		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Role & User Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Folder Creation.
		//		//====================================================================================================

		//		{
		//			var folderRoot = new Folder() { Name = tenant.CompanyName, FolderType = FolderType.Root, DateTimeCreated = DateTime.UtcNow, };
		//			contextTenant.Folders.Add(folderRoot);
		//			contextTenant.SaveChanges();

		//			var folderDepartments = new Folder() { Name = "Departments", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, };
		//			folderRoot.Folders.Add(folderDepartments);
		//			contextTenant.SaveChanges();

		//			var folderPersonal = new Folder() { Name = "My Folders", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, };
		//			folderRoot.Folders.Add(folderDepartments);
		//			contextTenant.SaveChanges();

		//			folderDepartments.Folders.Add(new Folder() { Name = "CEO", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Board of Directors", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Finance", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Accounting", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Human Resource", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Information Technology", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Sales", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Marketing", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Operations", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });
		//			folderDepartments.Folders.Add(new Folder() { Name = "Manufacturing", FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, });

		//			foreach (var folder in contextTenant.Folders)
		//			{
		//				var department = contextTenant.Departments.SingleOrDefault(d => d.Name == folder.Name);
		//				if (department != null) { folder.DepartmentId = department.Id; }
		//			}
		//			contextTenant.SaveChanges();

		//			for (int year = (DateTime.UtcNow.Year - 1); year <= DateTime.UtcNow.Year; year++)
		//			{
		//				var folderYear = new Folder() { Name = "Year - " + year.ToString().PadLeft(4, '0'), FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, };
		//				folderPersonal.Folders.Add(folderYear);
		//				contextTenant.SaveChanges();

		//				for (int month = 1; month <= 12; month++)
		//				{
		//					var date = new DateTime(year, month, 1);
		//					var folderMonth = new Folder() { Name = "Month - " + month.ToString().PadLeft(2, '0') + " - " + date.ToString("MMMM"), FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, };
		//					folderYear.Folders.Add(folderMonth);
		//					contextTenant.SaveChanges();

		//					for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
		//					{
		//						var week = 0;

		//						date = new DateTime(year, month, day);

		//						if (day <= 7) { week = 1; }
		//						else if (day <= 14) { week = 2; }
		//						else if (day <= 21) { week = 3; }
		//						else if (day <= 28) { week = 4; }
		//						else { week = 5; }

		//						var folderWeek = folderMonth.Folders.SingleOrDefault(f => f.Name == "Week - " + week.ToString().PadLeft(2, '0'));

		//						if (folderWeek == null)
		//						{
		//							folderWeek = new Folder() { Name = "Week - " + week.ToString().PadLeft(2, '0'), FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, };
		//							folderMonth.Folders.Add(folderWeek);
		//							contextTenant.SaveChanges();
		//						}

		//						var folderDay = new Folder() { Name = "Day - " + day.ToString().PadLeft(2, '0') + " - " + date.ToString("dddd"), FolderType = FolderType.Child, DateTimeCreated = DateTime.UtcNow, };
		//						folderWeek.Folders.Add(folderDay);
		//						contextTenant.SaveChanges();
		//					}
		//				}
		//			}
		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Folder Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Document Creation.
		//		//====================================================================================================

		//		{
		//			#region Old Code
		//			//foreach (var user in contextTenant.Users.ToList())
		//			//{
		//			//    var documentCount = 2;
		//			//    var wordCountMinimum = 10;
		//			//    var wordCountMaximum = 21;
		//			//    var wordLengthMinimum = 1;
		//			//    var wordLengthMaximum = 11;
		//			//    var randomm = new System.Random();

		//			//    for (int i = 0; i < documentCount; i++)
		//			//    {
		//			//        var document = new Document();
		//			//        var words = new List<string>();
		//			//        var wordCount = randomm.Next(wordCountMinimum, wordCountMaximum);
		//			//        var folder = contextTenant.Folders.Single(f => (f.FolderType == FolderType.Root));

		//			//        for (int j = 0; j < wordCount; j++)
		//			//        {
		//			//            var word = "";
		//			//            var wordLength = randomm.Next(wordLengthMinimum, wordLengthMaximum);

		//			//            for (int k = 0; k < wordLength; k++)
		//			//            {
		//			//                word += (char)randomm.Next((int)'a', ((int)'z') + 1);
		//			//            }

		//			//            words.Add(word);
		//			//        }

		//			//        var content = string.Join(" ", words);
		//			//        var directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sample Documents"));
		//			//        var filename = words.OrderByDescending(s => s.Length).First().ToSentenceCase() + ".txt";
		//			//        var file = new FileInfo(Path.Combine(directory.FullName, filename));

		//			//        document.Uploaded = false;
		//			//        document.Name = file.Name;
		//			//        document.SourceType = SourceType.DesktopFileSystem;
		//			//        document.FullTextOCRXML = "";
		//			//        document.IsDigital = true;
		//			//        document.DeviceName = "";
		//			//        document.DateTime = DateTime.Now;
		//			//        document.FileNameClient = file.FullName;
		//			//        document.FileNameServer = file.Name;
		//			//        document.ScannerSessionId = 0;
		//			//        document.IsCancelled = false;
		//			//        document.IsInTransit = true;
		//			//        document.Length = content.Length;
		//			//        document.Hash = Sha.GenerateHash(content, Encoding.UTF8, Sha.EnumShaKind.Sha512);
		//			//        document.User = user;
		//			//        document.UserId = user.Id;
		//			//        document.IsPrivate = true;

		//			//        if (folder.Documents.Any())
		//			//        {
		//			//            var folderIds = contextTenant.Folders.Select(f => f.Id).ToList();
		//			//            var index = random.Next(0, folderIds.Count);
		//			//            var folderId = folderIds[index];

		//			//            folder = contextTenant.Folders.Single(f => (f.Id == folderId));
		//			//        }

		//			//        document.Folder = folder;
		//			//        document.FolderId = folder.Id;

		//			//        contextTenant.Documents.Add(document);

		//			//        foreach (var w in words)
		//			//        {
		//			//            document.DocumentFragments.Add(new DocumentFragment() { FullTextOcr = w, });
		//			//        }

		//			//        contextTenant.SaveChanges();

		//			//        foreach (var item in contextTenant.Documents) { item.FileNameServer = item.Id.ToString() + "." + GlobalConstants.FileExtensionCloud; }

		//			//        contextTenant.SaveChanges();
		//			//    }
		//			//}
		//			#endregion

		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Document Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Search Criteria Creation.
		//		//====================================================================================================

		//		{
		//			var criteria = new DocumentSearchCriteria();

		//			criteria.Name = "My Search 01";
		//			criteria.DateTimeFrom = new DateTime(2000, 01, 01);
		//			criteria.DateTimeUpTo = new DateTime(2020, 12, DateTime.DaysInMonth(2020, 12));
		//			criteria.TagsUser = null;
		//			criteria.TagsGlobal = null;
		//			criteria.Filename = null;
		//			criteria.FolderName = null;
		//			criteria.TemplateName = null;
		//			criteria.User = contextTenant.Users.First();
		//			criteria.UserId = criteria.User.Id;

		//			contextTenant.DocumentSearchCriteria.Add(criteria);
		//			contextTenant.SaveChanges();
		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Search Criteria Creation.
		//		//====================================================================================================

		//		//====================================================================================================
		//		#region Tenant: Unknown.
		//		//====================================================================================================

		//		{
		//		}

		//		//====================================================================================================
		//		#endregion Tenant: Unknown.
		//		//====================================================================================================
		//		result = true;

		//	}
		//	catch (Exception ex)
		//	{
		//		exception = ex;
		//		//Debug.Print(ex.ToString());
		//		if (AffinityConfiguration.IsConfigurationDebug) { Debugger.Break(); }

		//		throw;
		//	}
		//	return result;
		//}
	}
}