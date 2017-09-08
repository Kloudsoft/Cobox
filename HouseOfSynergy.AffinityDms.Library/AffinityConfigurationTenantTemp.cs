//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace HouseOfSynergy.AffinityDms.Library
//{
//	public static class AffinityConfigurationTenantTemp
//	{
//		private static readonly string DnsName = Dns.GetHostName().ToLower();
//		public static ConfigurationMode ConfigurationMode { get { return (AffinityConfiguration.IsConfigurationDebug ? ConfigurationMode.Debug : ConfigurationMode.Release); } }
//		public static Exception exceptions { get; set; }

//		public static DeploymentLocation DeploymentLocation
//		{
//			get
//			{
//				try
//				{
//					switch (AffinityConfigurationTenantTemp.DnsName)
//					{
//						case @"admin": { return (DeploymentLocation.HosHp); }
//						case @"monster": { return (DeploymentLocation.HosMonster); }
//						case @"raheelkhan-pc": { return (DeploymentLocation.HosRkPc); }
//						case @"kausarpc": { return (DeploymentLocation.HosKausar); }
//						case @"benztech-rizwan": { return (DeploymentLocation.BtsRizwan); }
//						case @"uzmahashmi": { return (DeploymentLocation.BtsUzma); }
//						case @"benztech-danish": { return (DeploymentLocation.BtsDanish); }
//						case @"benztech-pc": { return (DeploymentLocation.BtsSaleem); }
//						default:
//						{
//							return (DeploymentLocation.Live);
//							//throw (new NotImplementedException()); 
//						}
//					}
//				}
//				catch (Exception ex)
//				{
//					exceptions = ex;
//					return DeploymentLocation.Live;
//				}
//			}
//		}

//		public static string DatabaseConnectionString
//		{

//			get
//			{
//				try
//				{
//					return (AffinityConfigurationTenantTemp.DatabaseConnectionStringBuilder.ToString());
//				}
//				catch (Exception ex)
//				{
//					exceptions = ex;
//					return "";
//				}
//			}
//		}

//		public static SqlConnectionStringBuilder DatabaseConnectionStringBuilder
//		{
//			get
//			{
//				var builder = new SqlConnectionStringBuilder();

//				try
//				{
//					if (System.Configuration.ConfigurationManager.AppSettings ["DataBaseName"] != null) { builder.InitialCatalog = System.Configuration.ConfigurationManager.AppSettings ["DataBaseName"].ToString(); }

//					//builder.InitialCatalog = "AffinityDmsTenant";
//					builder.IntegratedSecurity = true;
//					builder.MultipleActiveResultSets = true;

//					switch (AffinityConfigurationTenantTemp.DeploymentLocation)
//					{
//						case DeploymentLocation.Live:
//						{
//							if (System.Configuration.ConfigurationManager.AppSettings ["DataSource"] != null) { builder.DataSource = System.Configuration.ConfigurationManager.AppSettings ["DataSource"].ToString(); }
//							if (System.Configuration.ConfigurationManager.AppSettings ["UserID"] != null) { builder.UserID = System.Configuration.ConfigurationManager.AppSettings ["UserID"].ToString(); }
//							if (System.Configuration.ConfigurationManager.AppSettings ["Password"] != null) { builder.Password = System.Configuration.ConfigurationManager.AppSettings ["Password"].ToString(); }

//							builder.IntegratedSecurity = false;
//							builder.TrustServerCertificate = false;

//							break;
//						}
//						case DeploymentLocation.HosHp: { builder.DataSource = @"ADMIN\SQLEXPRESS"; break; }
//						case DeploymentLocation.HosMonster: { builder.DataSource = @"MONSTER\MSSQLEXPRESS2014"; break; }
//						case DeploymentLocation.HosRkPc: { builder.DataSource = @"RAHEELKHAN-PC\SQLEXPRESS"; break; }
//						case DeploymentLocation.HosKausar: { builder.DataSource = @"KAUSARPC\SQLEXPRESSKAUSAR"; break; }
//						case DeploymentLocation.BtsRizwan: { builder.DataSource = "."; break; }
//						case DeploymentLocation.BtsDanish: { builder.DataSource = "."; break; }
//						case DeploymentLocation.BtsUzma: { builder.DataSource = "."; break; }
//						case DeploymentLocation.BtsSaleem: { builder.DataSource = "benztech-pc"; break; }
//						default: { throw (new NotImplementedException()); }
//					}

//					return (builder);
//				}
//				catch (Exception ex)
//				{
//					exceptions = ex;
//					return builder;
//				}
//			}
//		}
//	}
//}