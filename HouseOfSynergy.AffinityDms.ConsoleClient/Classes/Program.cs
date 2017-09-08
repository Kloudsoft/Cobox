using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;
using HouseOfSynergy.AffinityDms.BusinessLayer.Ocr;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.ConsoleClient.Forms;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.AffinityDms.ResourceProvider.Classes;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using HouseOfSynergy.PowerTools.Library.Utility;
using HouseOfSynergy.PowerTools.Library.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
			var global = new Global();

            global.Initialize();

            Console.Title = global.ApplicationInfo.ProductName;
            Console.Write(global.ApplicationInfo.ProductName);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Running selected utilities...");

            //Program.TestDocumentAcl();
            //Program.GenerateDocuments();
            Program.InitializeLocalDatabases();
            //Program.UpdateLiveTenantDatabases("cobox.com");
           Program.UpdateLiveTenantDatabases("kloud-soft.com");
            
            
            //Program.FarazTestFunction1();

            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"All utilities run.");
            Console.WriteLine();
            Console.Write($"Press any key to exit...");
            Console.ReadKey(intercept: true);

            #region Reference Code.

            //var services = ServiceController
            //	.GetServices()
            //	.Where(s => s.ServiceName == "Affinity ECM - DMS - Recognition & Classification Service");
            //var service = services.SingleOrDefault();
            //service.ExecuteCommand((int) ServiceCommand.RequestTerminate);
            //return;

            //using
            //(
            //	var ocrEngineSettings = new OcrEngineSettings
            //	(
            //                 //global.lo
            //		File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LT", "License", "eval-license-files.lic.key")),
            //		Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), "LT", "License", "eval-license-files.lic"),
            //		Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), "Data"),
            //		Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), "LT", "Runtime")
            //	)
            //)
            //{
            //	WorkerRoleSimulation.SimulateWorkerRole (ocrEngineSettings);
            //}

            #endregion Reference Code.
        }

		private static void TestFingerprintVerificationServer ()
		{
			using (var verificationServer = new VerificationServer(new Uri("http://localhost:55487/"), Encoding.UTF8))
			{
				try
				{
					Console.Write("Server starting...");
					verificationServer.Start();
					Console.WriteLine();
					Console.Write("Server started...");
					Console.WriteLine();

					{
						// ASP .NET code.
						Console.WriteLine();
						Console.Write("Sending request...");

						Parallel.For
						(
							0,
							10,
							(i) =>
							{
								Exception exception = null;
								ICustomData data = new Data();
								var algorithmSymmetricName = typeof(RijndaelManaged).FullName;

								var request = new Request<VerificationCommand>(VerificationCommand.Verify, data);

								request.CustomDictionary.Add("Type", VerificationCommand.Enroll.ToString());
								request.CustomDictionary.Add("Type", VerificationCommand.Verify.ToString());

								using (var algorithm = SymmetricAlgorithm.Create(algorithmSymmetricName))
								{
									if (request.Process(new Uri("http://localhost:55487/"), HttpRequestMethod.Post, algorithm, out exception))
									{
										Console.WriteLine();
										Console.Write($"Result {i}: {request.Response.Result}");
										Console.WriteLine();
										Console.Write($"Response {i}: {request.Response.Data.Elements.First().OuterXml}");
									}
									else
									{
										Console.WriteLine();
										Console.Write(exception);
									}
								}
							}
						);
					}

					Console.WriteLine();
					Console.WriteLine();
					Console.Write("Press any key...");

					verificationServer.Stop();

					Console.WriteLine();
					Console.WriteLine();
					Console.Write("Server stopping...");
				}
				catch (Exception exception)
				{
					Console.WriteLine();
					Console.WriteLine();
					Console.Write(exception.ToString());
				}
				finally
				{
					try { verificationServer.Stop(); }
					finally { }
				}
			}
		}

        private static void FarazTestFunction1()
        {
            var toContinue = false;
            do
            {
                toContinue = false;
                Console.WriteLine(AffinityConfigurationMaster.GetDatabaseConnectionStringBuilder(DeploymentLocation.BtsFaraz).ToString());
                Exception exception = null;
                TenantUserSession tenantUserSessionRaheel = null;
                TenantUserSession tenantUserSessionJunaid = null;
                AuthenticationManagement.SignIn(SessionType.Mvc, "kloud-soft.com", "raheel.khan", Sha.GenerateHash("audience", GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind), IPAddress.Loopback.ToString(), "User Agent", 0, "Session Id", out tenantUserSessionRaheel, out exception);
                AuthenticationManagement.SignIn(SessionType.Mvc, "kloud-soft.com", "junaid.sayed", Sha.GenerateHash("audience", GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind), IPAddress.Loopback.ToString(), "User Agent", 0, "Session Id", out tenantUserSessionJunaid, out exception);
                List<Folder> folders = null;
                using (var context = new ContextTenant(tenantUserSessionRaheel.Tenant.DatabaseConnectionString))
                {
                    folders = context.Folders.Include(x => x.FolderUsers).ToList();
                    Console.WriteLine(FolderManagement.ValidateUserFolderRightsHirarchy(15, folders, 1).ToString());
                }
                Console.WriteLine("press 'y' to continue");
                var read = Console.ReadKey();
                if (read.KeyChar.ToString().ToLower() == "y")
                {
                    toContinue = true;
                    Console.Clear();
                }
            } while (toContinue);
        }

        private static void UpdateLiveTenantDatabases(params string[] productionDomainsToBeUpdated)
        {
            Program.UpdateLiveTenantDatabases(productionDomainsToBeUpdated.AsEnumerable());
        }

        private static void UpdateLiveTenantDatabases(IEnumerable<string> productionDomainsToBeUpdated)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"----------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"UpdateLiveTenantDatabases.");

            Uri uri;
            var encoding = Encoding.UTF8;
            var tenantsMasterLive = new List<Tenant>();
            var tenantsMasterLocal = new List<Tenant>();
           // var builderTenantLive = new SqlConnectionStringBuilder();
            var builderTenantLocal = new SqlConnectionStringBuilder();
            //var builderMasterLive = AffinityConfigurationMaster.GetDatabaseConnectionStringBuilder(DeploymentLocation.Live);
            var builderMasterLocal = AffinityConfigurationMaster.GetDatabaseConnectionStringBuilder(AffinityConfiguration.DeploymentLocation);

            if
            (
                (productionDomainsToBeUpdated.Any())
                &&
                (
                    productionDomainsToBeUpdated.All
                    (
                        d =>
                        (!string.IsNullOrWhiteSpace(d))
                        && (Uri.CheckHostName(d) == UriHostNameType.Dns)
                        && (Uri.TryCreate(d, UriKind.RelativeOrAbsolute, out uri))
                        && (!uri.IsAbsoluteUri)
                        && (!uri.UserEscaped)
                    )
                )
            )
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"Domains selected to be updated:");
                Console.WriteLine();
                Console.Write(string.Join(Environment.NewLine, productionDomainsToBeUpdated.Select(d => $" - {d}")));
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"Either no domains were passed in, or at least one of the domains");
                Console.WriteLine();
                Console.Write($"was empty, null or invalid. Domains should be in the form of: [xyz.com].");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"The utility will now return.");

                return;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"WARNING: This action cannot be undone!");
            Console.WriteLine();
            Console.Write($"Press [Y] to run the action, any other key to cancel: ");

            var consoleKeyInfo = Console.ReadKey(intercept: true);

            if (consoleKeyInfo.Key == ConsoleKey.Y)
            {
                Console.Write("Y");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"WARNING: Once again, this action cannot be undone!");
                Console.WriteLine();
                Console.Write($"Press [Y] to run the action, any other key to cancel: ");

                consoleKeyInfo = Console.ReadKey(intercept: true);

                if (consoleKeyInfo.Key == ConsoleKey.Y)
                {
                    Console.Write("Y");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write($"Well, you WERE warned! Continuing script execution...");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write($"Script execution cancelled. Phew!!!");

                    return;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"Script execution cancelled. Phew!!!");

                return;
            }

            Program.InitializeLocalDatabases();

            var fileScript = new FileInfo(Assembly.GetExecutingAssembly().Location);
            fileScript = new FileInfo(Path.Combine(fileScript.Directory.FullName, @"..\..\..\HouseOfSynergy.AffinityDms.Library\Database\Script Schema Drop Create Data Insert.sql"));
            if (!fileScript.Exists) { throw (new FileNotFoundException($"File not found: {fileScript.FullName}.", fileScript.FullName)); }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"Getting live tenants from master...");
            using (var contextMasterLive = new ContextMaster(deploymentLocation: DeploymentLocation.Live))
            {
                tenantsMasterLive = contextMasterLive.Tenants.ToList();
            }

            var validDomains = tenantsMasterLive.ConvertAll(t => t.Domain);
            var invalidDomains = productionDomainsToBeUpdated.Where(d => !validDomains.Contains(d));

            Console.WriteLine();
            Console.Write($"Domains found in the master database:");
            Console.WriteLine();
            Console.Write(string.Join(Environment.NewLine, validDomains.Select(d => $" - {d}")));

            if (invalidDomains.Any())
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"The following domains do not exist in the master database:");
                Console.WriteLine();
                Console.Write(string.Join(Environment.NewLine, invalidDomains.Select(d => $" - {d}")));
                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"The utility will now return.");

                return;
            }

            tenantsMasterLive = tenantsMasterLive.Where(t => productionDomainsToBeUpdated.Contains(t.Domain)).ToList();

            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"Updating live tenants...");
            foreach (var tenantMasterLive in tenantsMasterLive)
            {
                var tenantTenantLive = tenantMasterLive.Clone();
                var databaseNameTenantLive = new SqlConnectionStringBuilder(tenantMasterLive.DatabaseConnectionString).InitialCatalog;

                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"Tenant: [{tenantMasterLive.Domain}].");

				using (var contextMasterLocal = new ContextMaster())
				{
					var tenantMasterLocal = contextMasterLocal.Tenants.SingleOrDefault(t => t.Domain.ToLower() == tenantMasterLive.Domain.ToLower());

					builderTenantLocal = new SqlConnectionStringBuilder(tenantMasterLocal.DatabaseConnectionString);
				}

				// Generate Drop, Create & Seed Script.
				//builderTenantLocal = new SqlConnectionStringBuilder()
				//{
				//    DataSource = @"BENZFARAZ-PC\SQLEXPRESS",
				//    InitialCatalog = "AffinityDmsTenant_0000000000000000002",
				//    IntegratedSecurity = true,
				//    UserID = @"sa",
				//    Password = @"123456",
				//    MultipleActiveResultSets = false,
				//    PersistSecurityInfo = true
				//};

				// Generate Drop, Create & Seed Script.
				builderTenantLocal = new SqlConnectionStringBuilder()
				{
					DataSource = @"Lenovo",
					//DataSource = @"HOSLAPTOPHP\MSSQLS2016EXP",
					InitialCatalog = "AffinityDmsTenant_0000000000000000001",
					IntegratedSecurity = true,
					//UserID = @"",
					//Password = @"",
					//MultipleActiveResultSets = false,
					PersistSecurityInfo = true
				};

				Program.GenerateScript(builderTenantLocal, databaseNameTenantLive, fileScript, encoding);

                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"Execute script against the respective live tenant.");
                using (var contextTenantLive = new ContextTenant(tenantTenantLive.DatabaseConnectionString))
                {
                    var script = File.ReadAllText(fileScript.FullName, encoding);

					var t = contextTenantLive.Tenants.ToList();

					contextTenantLive.Database.CommandTimeout = 30 * 60;
					contextTenantLive.Database.ExecuteSqlCommand(script);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.Write($"Correcting script parameters.");
                using (var contextTenantLive = new ContextTenant(tenantTenantLive.DatabaseConnectionString))
                {
                    tenantTenantLive.Id = 1; // To update the single tenant row.
                    tenantTenantLive.TenantType = EntityMasterTenantType.Tenant;

                    contextTenantLive.Tenants.Attach(tenantTenantLive);
                    contextTenantLive.SaveChanges();
                }
            }
        }

        private static void GenerateScript(SqlConnectionStringBuilder builderTenantLocal, string databaseTenantLiveName, FileInfo fileScript, Encoding encoding)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"GenerateScript.");

            var tableNames = new string[] { "__MigrationHistory", "Culture", "Department", "Folder", "Subscription", "Tenant", "TenantSubscription", "Role", "User", "UserFolder", "UserRole", "RoleUsers", };

            using (var connection = new SqlConnection(builderTenantLocal.ConnectionString))
            {
                var scriptDataInsert = "";
                var scriptSchemaDrop = "";
                var scriptSchemaCreate = "";
                var serverConnection = new Microsoft.SqlServer.Management.Common.ServerConnection(connection);
                var server = new Microsoft.SqlServer.Management.Smo.Server(serverConnection);
                var database = server.Databases[builderTenantLocal.InitialCatalog];
                var scripter = new Microsoft.SqlServer.Management.Smo.Scripter(server);
                var tables = new Microsoft.SqlServer.Management.Smo.UrnCollection();

                File.WriteAllText(fileScript.FullName, $"USE [{databaseTenantLiveName}]{Environment.NewLine}{Environment.NewLine}", encoding);

                tables.Clear();
                scripter.Options.ScriptDrops = true;
                scripter.Options.ScriptSchema = true;
                scripter.Options.ScriptData = false;
                scripter.Options.WithDependencies = true;
                scripter.Options.DriAllConstraints = true;
                scripter.Options.NoCommandTerminator = true;
                scripter.Options.IncludeDatabaseContext = false;
                scripter.Options.FileName = fileScript.FullName;
                scripter.Options.AppendToFile = true;
                scripter.Options.ToFileOnly = false;
                scripter.Options.IncludeHeaders = true;
                scripter.Options.IncludeIfNotExists = true;
                scripter.Options.PrimaryObject = true;
                scripter.Options.ExtendedProperties = true;
                scripter.Options.Encoding = encoding;
                foreach (Microsoft.SqlServer.Management.Smo.Table table in database.Tables) { tables.Add(table.Urn); }
                scriptSchemaDrop = string.Join(Environment.NewLine, scripter.EnumScript(tables));

                tables.Clear();
                scripter.Options.ScriptDrops = false;
                scripter.Options.ScriptSchema = true;
                scripter.Options.ScriptData = false;
                scripter.Options.WithDependencies = true;
                scripter.Options.DriAllConstraints = true;
                scripter.Options.NoCommandTerminator = true;
                scripter.Options.IncludeDatabaseContext = false;
                scripter.Options.FileName = fileScript.FullName;
                scripter.Options.AppendToFile = true;
                scripter.Options.ToFileOnly = false;
                scripter.Options.IncludeHeaders = true;
                scripter.Options.IncludeIfNotExists = true;
                scripter.Options.PrimaryObject = true;
                scripter.Options.ExtendedProperties = true;
                scripter.Options.Encoding = encoding;
                foreach (Microsoft.SqlServer.Management.Smo.Table table in database.Tables) { tables.Add(table.Urn); }
                scriptSchemaCreate = string.Join(Environment.NewLine, scripter.EnumScript(tables));

                tables.Clear();
                scripter.Options.ScriptDrops = false;
                scripter.Options.ScriptSchema = false;
                scripter.Options.ScriptData = true;
                scripter.Options.WithDependencies = true;
                scripter.Options.DriAllConstraints = true;
                scripter.Options.NoCommandTerminator = true;
                scripter.Options.IncludeDatabaseContext = false;
                scripter.Options.FileName = fileScript.FullName;
                scripter.Options.AppendToFile = true;
                scripter.Options.ToFileOnly = false;
                scripter.Options.IncludeHeaders = true;
                scripter.Options.IncludeIfNotExists = true;
                scripter.Options.PrimaryObject = true;
                scripter.Options.ExtendedProperties = true;
                scripter.Options.Encoding = encoding;
                foreach (var tableName in tableNames) { tables.Add(database.Tables[tableName].Urn); }
                scriptDataInsert = string.Join(Environment.NewLine, scripter.EnumScript(tables));
            }
        }

        private static void InitializeLocalDatabases()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"InitializeLocalDatabases.");

            ContextMaster.Initialize();
            ContextDesktop.Initialize(AffinityConfigurationDesktop.GetDatabaseConnectionStringBuilder(AffinityConfiguration.DeploymentLocation).ConnectionString);

            using (var contextDesktop = new ContextDesktop(AffinityConfigurationDesktop.GetDatabaseConnectionStringBuilder(AffinityConfiguration.DeploymentLocation).ConnectionString))
            {
            }

            using (var contextMaster = new ContextMaster())
            {
                var tenants = contextMaster.Tenants.ToList();

                foreach (var tenant in tenants)
                {
                    ContextTenant.Initialize(tenant.DatabaseConnectionString, true);

                    using (var contextTenant = new ContextTenant(tenant.DatabaseConnectionString))
                    {
                        contextTenant.Cultures.ToString();
                    }
                }
            }
        }

        private static void TestDocumentAcl()
        {
            Exception exception = null;
            List<Document> documentsRaheel = null;
            List<Document> documentsJunaid = null;
            TenantUserSession tenantUserSessionRaheel = null;
            TenantUserSession tenantUserSessionJunaid = null;

            ContextMaster.Initialize();
            using (var context = new ContextMaster())
            {
                context.Tenants.ToList();
            }

            AuthenticationManagement.SignIn(SessionType.Mvc, "kloud-soft.com", "raheel.khan", Sha.GenerateHash("audience", GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind), IPAddress.Loopback.ToString(), "User Agent", 0, "Session Id", out tenantUserSessionRaheel, out exception);
            AuthenticationManagement.SignIn(SessionType.Mvc, "kloud-soft.com", "junaid.sayed", Sha.GenerateHash("audience", GlobalConstants.EncodingCryptography, GlobalConstants.AlgorithmHashShaKind), IPAddress.Loopback.ToString(), "User Agent", 0, "Session Id", out tenantUserSessionJunaid, out exception);

            documentsRaheel = DocumentManagement.GetDocuments
            (
                tenantUserSession: tenantUserSessionRaheel,
                documentId: 23,
                documentIdType: DocumentIdType.Id,
                folderId: 3,
                documentResultVersionType: DocumentResultVersionType.All,
                includeDiscourse: false,
                includeDocumentElements: false,
                includeDocumentFragments: false,
                includeCreatorUser: false,
                includeCheckedOutUser: false,
                includeDocumentUsers: false,
                includeDocumentIndexes: false,
                includeDocumentTags: false,
                includeDocumentTagUsers: false,
                includeDocumentTemplates: false,
                includeDocumentCorrectiveIndexValues: false,
                isFinalized: null,
                skipRows: null,
                takeRows: null
            );

            documentsJunaid = DocumentManagement.GetDocuments
            (
                tenantUserSession: tenantUserSessionJunaid,
                documentId: 23,
                documentIdType: DocumentIdType.Id,
                folderId: 3,
                documentResultVersionType: DocumentResultVersionType.All,
                includeDiscourse: false,
                includeDocumentElements: false,
                includeDocumentFragments: false,
                includeCreatorUser: false,
                includeCheckedOutUser: false,
                includeDocumentUsers: false,
                includeDocumentIndexes: false,
                includeDocumentTags: false,
                includeDocumentTagUsers: false,
                includeDocumentTemplates: false,
                includeDocumentCorrectiveIndexValues: false,
                isFinalized: null,
                skipRows: null,
                takeRows: null
            );

            Console.Write($"User: {tenantUserSessionRaheel.User.NameFull}:");
            foreach (var document in documentsRaheel)
            {
                Console.WriteLine();
                Console.Write($" - Name: {document.Name}");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"User: {tenantUserSessionJunaid.User.NameFull}:");
            foreach (var document in documentsJunaid)
            {
                Console.WriteLine();
                Console.Write($" - Name: {document.Name}");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void GenerateDocuments()
        {
            Tenant tenant = null;
            var random = new System.Random();

            try
            {
                using (var contextMaster = new ContextMaster())
                {
                    tenant = contextMaster
                        .Tenants
                        .AsNoTracking()
                        .Include(t => t.Users)
                        .First();
                }

                using (var context = new ContextTenant(tenant.DatabaseConnectionString))
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //var countTemplates = 10;

                            //for (int i = 0; i < countTemplates; i++)
                            //{
                            //	var elements = new List<TemplateElement> ();
                            //	var template = new Template () { Title = "", Description = "", };
                            //	context.Templates.Add (template);
                            //	context.SaveChanges ();
                            //	template.Title = "Template " + template.Id.ToString ().PadLeft (long.MaxValue.ToString ().Length, '0');
                            //	context.SaveChanges ();

                            //	var file = PathUtilities.GetTempFile (".png", false);
                            //	using (var bitmap = new Bitmap (random.Next (500, 1000), random.Next (500, 1000), PixelFormat.Format32bppArgb))
                            //	{
                            //		using (var graphics = Graphics.FromImage (bitmap))
                            //		{
                            //			graphics.SetQualityHighest ();
                            //			graphics.Clear (Color.White);

                            //			var size = SizeF.Empty;
                            //			TemplateElement element = null;
                            //			var font = new Font (FontFamily.GenericMonospace, 12F);

                            //			element = new TemplateElement () { Name = "", Description = "", X = 0, Y = 0, X2 = 0, Y2 = 0, Width = "", Height = "", TemplateId = template.Id, };
                            //			template.Elements.Add (element);
                            //			context.SaveChanges ();
                            //			element.Name = "Element " + element.Id.ToString ().PadLeft (long.MaxValue.ToString ().Length, '0');
                            //			context.SaveChanges ();
                            //			element.Value = "INVOICE";
                            //			element.ElementIndexType = 1;
                            //			element.ElementType = (int) ElementType.Label;
                            //			size = graphics.MeasureString (element.Value, font);
                            //			element.X = 10;
                            //			element.Y = 10;
                            //			element.X2 = element.X + size.Width;
                            //			element.Y2 = element.X + size.Height;
                            //			//element.Width
                            //			context.SaveChanges ();
                            //			elements.Add (element);
                            //			graphics.DrawRectangle (Pens.Black, RectangleF.FromLTRB (element.X, element.Y, element.X2, element.Y2));
                            //			graphics.DrawString (element.Value, font, Brushes.Black, element.X, element.Y);

                            //			element = new TemplateElement () { Name = "", Description = "", X = 0, Y = 0, X2 = 0, Y2 = 0, Width = "", Height = "", TemplateId = template.Id, };
                            //			template.Elements.Add (element);
                            //			context.SaveChanges ();
                            //			element.Name = "Element " + element.Id.ToString ().PadLeft (long.MaxValue.ToString ().Length, '0');
                            //			context.SaveChanges ();
                            //			element.Value = "INV #:";
                            //			element.ElementIndexType = 1;
                            //			element.ElementType = (int) ElementType.Label;
                            //			size = graphics.MeasureString (element.Value, font);
                            //			element.X = 100;
                            //			element.Y = 50;
                            //			element.X2 = element.X + size.Width;
                            //			element.Y2 = element.X + size.Height;
                            //			//element.Width
                            //			context.SaveChanges ();
                            //			elements.Add (element);
                            //			graphics.DrawRectangle (Pens.Black, RectangleF.FromLTRB (element.X, element.Y, element.X2, element.Y2));
                            //			graphics.DrawString (element.Value, font, Brushes.Black, element.X, element.Y);
                            //		}

                            //		bitmap.Save (file.FullName, ImageFormat.Png);
                            //		Program.TemplateUpload (tenant, template, file);
                            //		//OcrTest.CreateTemplateFiles (tenant, template, file, elements);
                            //	}
                            //}

                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception exception)
                        {
                            transaction.Rollback();
                            Debug.Write(exception);
                            Debugger.Break();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.Write(exception);
                Debugger.Break();
            }
        }

        private static void TemplateUpload(Tenant tenant, Template template, FileInfo file)
        {
            using (var fileStream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Program.TemplateUpload(tenant, template, fileStream);
            }
        }

        private static void TemplateUpload(Tenant tenant, Template template, FileStream fileStream)
        {
            var connectionString = tenant.StorageConnectionStringPrimary;
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("Templates");

            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

            var blob = container.GetBlockBlobReference(template.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".png");

            blob.UploadFromStream(fileStream);
        }

        private static bool GetTenantDocumentFileStream(Tenant tenant, Document document, out Stream stream, out Exception exception)
        {
            var result = false;

            stream = null;
            exception = null;

            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(tenant.UrlResourceGroup);

                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

                var blob = container.GetBlockBlobReference(document.FileNameServer);

                stream = new MemoryStream();
                blob.DownloadToStream(stream);

                result = true;
            }
            catch (Exception e)
            {
                if (stream != null)
                {
                    try { stream.Dispose(); }
                    finally { stream = null; }
                }

                exception = e;
            }

            return (result);
        }
    }
}