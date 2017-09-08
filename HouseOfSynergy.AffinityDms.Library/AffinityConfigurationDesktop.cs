using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
	public static class AffinityConfigurationDesktop
	{
		public static Exception exceptions = null;
		public static ConfigurationMode ConfigurationMode { get { return (AffinityConfiguration.IsConfigurationDebug ? ConfigurationMode.Debug : ConfigurationMode.Release); } }

		public static string DatabaseConnectionString
		{
			get
			{
				var file = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AffinityDmsDesktop.txt"));

				if (!file.Exists) { throw (new FileNotFoundException($"The connection string file was not found: '{file.FullName}'")); }

				var text = File.ReadAllText(file.FullName);

				return (text);

				//switch (AffinityConfiguration.DeploymentLocation)
				//{
				//	case DeploymentLocation.W10EeVs2015CeU2:
				//	case DeploymentLocation.HosHp:
				//	case DeploymentLocation.HosMonster:
				//	case DeploymentLocation.HosRkPc:
				//	case DeploymentLocation.HosKausar:
				//	case DeploymentLocation.BtsRizwan:
				//	case DeploymentLocation.BtsDanish:
				//	case DeploymentLocation.BtsUzma:
				//	case DeploymentLocation.BtsSaleem:
				//	case DeploymentLocation.BtsUsmanJamal:
				//	case DeploymentLocation.BtsFaraz: { return (AffinityConfigurationDesktop.DatabaseConnectionStringBuilder.ToString()); }
				//	default: { return ("AffinityDmsDesktop"); }
				//}
			}
		}

		public static SqlConnectionStringBuilder DatabaseConnectionStringBuilder { get { return (AffinityConfigurationDesktop.GetDatabaseConnectionStringBuilder(AffinityConfiguration.DeploymentLocation)); } }

		public static SqlConnectionStringBuilder GetDatabaseConnectionStringBuilder (DeploymentLocation deploymentLocation)
		{
			var builder = new SqlConnectionStringBuilder();

			builder.InitialCatalog = "AffinityDmsDesktop";
			builder.IntegratedSecurity = true;
			builder.MultipleActiveResultSets = true;

			switch (deploymentLocation)
			{
				case DeploymentLocation.W10EeVs2015CeU2: { builder.DataSource = @"W10EEVS2015CEU2\MSSQLEXPRESS2014"; break; }
				case DeploymentLocation.HosHp: { builder.DataSource = @"HOSLAPTOPHP\MSSQLS2016EXP"; break; }
				case DeploymentLocation.HosMonster: { builder.DataSource = @"MONSTER\MSSQLEXPRESS2014"; break; }
				case DeploymentLocation.HosRkPc: { builder.DataSource = @"RAHEELKHAN-PC\SQLEXPRESS"; break; }
				case DeploymentLocation.HosKausar: { builder.DataSource = @"KAUSARPC\SQLEXPRESSKAUSAR"; break; }
				case DeploymentLocation.BtsRizwan: { builder.DataSource = "."; break; }
				case DeploymentLocation.BtsDanish: { builder.DataSource = "."; break; }
				case DeploymentLocation.BtsUzma: { builder.DataSource = "."; break; }
				case DeploymentLocation.BtsSaleem: { builder.DataSource = "benztech-pc"; break; }
				case DeploymentLocation.BtsUsmanJamal: { builder.DataSource = @"HOME-PC\SqlExpress2014"; break; }
				case DeploymentLocation.BtsFabehaMalik: { builder.DataSource = @"FABEHA-HDK\SQLEXPRESS"; break; }
				case DeploymentLocation.BtsFaraz:
				{
					builder.DataSource = @"BENZFARAZ-PC\SQLEXPRESS";
					builder.UserID = @"sa";
					builder.Password = @"123456";
					builder.PersistSecurityInfo = true;
					builder.IntegratedSecurity = false;

					break;
				}
				default: { builder.DataSource = ""; break; }
			}

			//builder.InitialCatalog = @"";
			//builder.DataSource = @"(LocalDb)\v11.0";
			//builder.IntegratedSecurity = true;
			//builder.MultipleActiveResultSets = true;
			//builder.AttachDBFilename = AffinityConfigurationDesktop.FileApplicationData.FullName;

			return (builder);
		}

		public static DirectoryInfo DirectoryApplicationData
		{
			get
			{
				var directory = "";

				directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.ManufacturerName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.CompanyName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.ProductName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.ProductVersion.ToString());
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.AssemblyName);
				directory = Path.Combine(directory, AffinityConfiguration.ApplicationInfo.AssemblyVersion.ToString());

				if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

				return (new DirectoryInfo(directory));
			}
		}

		public static DirectoryInfo DirectoryApplicationDataDatabase
		{
			get
			{
				var directory = "";

				directory = DirectoryApplicationData.FullName;
				directory = Path.Combine(directory, "Database");

				if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

				return (new DirectoryInfo(directory));
			}
		}

		public static FileInfo FileApplicationData
		{
			get
			{
				var filename = "";

				filename = AffinityConfigurationDesktop.DirectoryApplicationDataDatabase.FullName;
				filename = Path.Combine(filename, "AffinityDms.mdf");

				return (new FileInfo(filename));
			}
		}
	}
}