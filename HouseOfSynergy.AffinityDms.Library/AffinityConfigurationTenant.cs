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
	public static class AffinityConfigurationTenant
	{
		public static Exception exceptions = null;
		public static ConfigurationMode ConfigurationMode { get { return (AffinityConfiguration.IsConfigurationDebug ? ConfigurationMode.Debug : ConfigurationMode.Release); } }

		public static string GetDatabaseConnectionString (long tenantId)
		{
			return (AffinityConfigurationTenant.GetDatabaseConnectionStringBuilder(tenantId).ToString());
		}

		public static SqlConnectionStringBuilder GetDatabaseConnectionStringBuilder (long tenantId)
		{
			var builder = new SqlConnectionStringBuilder();

			builder.IntegratedSecurity = true;
			builder.MultipleActiveResultSets = true;
			builder.InitialCatalog = "AffinityDmsTenant_" + tenantId.ToString().PadLeft(long.MaxValue.ToString().Length, '0');

			switch (AffinityConfiguration.DeploymentLocation)
			{
				case DeploymentLocation.W10EeVs2015CeU2: { builder.DataSource = @"W10EEVS2015CEU2\MSSQLEXPRESS2014"; break; }
				case DeploymentLocation.HosHp: { builder.DataSource = @"HOSLAPTOPHP\MSSQLS2016EXP"; break; }
				case DeploymentLocation.HosMonster: { builder.DataSource = @"MONSTER\MSSQLEXPRESS2014"; break; }
				case DeploymentLocation.HosRkPc: { builder.DataSource = @"RAHEELKHAN-PC\SQLEXPRESS"; break; }
				case DeploymentLocation.HosKausar: { builder.DataSource = @"KAUSARPC\SQLEXPRESSKAUSAR"; break; }
				case DeploymentLocation.BtsRizwan: { builder.DataSource = @"."; break; }
				case DeploymentLocation.BtsDanish: { builder.DataSource = @"."; break; }
				case DeploymentLocation.BtsUzma: { builder.DataSource = @"UzmaHashmi\SqlServer2014"; break; }
				case DeploymentLocation.BtsUsmanJamal: { builder.DataSource = @"HOME-PC\SqlExpress2014"; break; }
				case DeploymentLocation.BtsFabehaMalik: { builder.DataSource = @"FABEHA-HDK\SQLEXPRESS"; break; }
				case DeploymentLocation.BtsSaleem:
				{
					builder.DataSource = @"UzmaHashmi\SqlServer2014";
					builder.UserID = @"sa";
					builder.Password = @"123456";
					builder.PersistSecurityInfo = true;
					builder.IntegratedSecurity = false;

					break;
				}
                case DeploymentLocation.BtsFaraz:
                    {
                        builder.DataSource = @"BENZFARAZ-PC\SQLEXPRESS";
                        builder.UserID = @"sa";
                        builder.Password = @"123456";
                        builder.PersistSecurityInfo = true;
                        builder.IntegratedSecurity = false;

                        break;
                    }
                case DeploymentLocation.KsManjuLenovo:
                    {
                        builder.DataSource = @"Lenovo";
                        //builder.UserID = @"sa";
                        //builder.Password = @"123456";
                        builder.PersistSecurityInfo = true;
                        builder.IntegratedSecurity = true;

                        break;
                    }
                case DeploymentLocation.KsThiya:
                    {
                        builder.DataSource = @"VAIO_HFTEH";
                        //builder.UserID = @"sa";
                        //builder.Password = @"123456";
                        builder.PersistSecurityInfo = true;
                        builder.IntegratedSecurity = true;

                        break;
                    }

                case DeploymentLocation.KsAdmintech:
                    {
                        builder.DataSource = @"ADMINNTECH\SQLEXPRESS";
                        //builder.UserID = @"sa";
                        //builder.Password = @"123456";
                        builder.PersistSecurityInfo = true;
                        builder.IntegratedSecurity = true;

                        break;
                    }
                case DeploymentLocation.Live:
				{
					if (System.Configuration.ConfigurationManager.AppSettings ["DataSource"] != null)
					{
						builder.DataSource = System.Configuration.ConfigurationManager.AppSettings ["DataSource"].ToString();
					}

                        if (System.Configuration.ConfigurationManager.AppSettings["UserID"] != null)
                        {
                            builder.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString();
                        }

                        if (System.Configuration.ConfigurationManager.AppSettings["Password"] != null)
                        {
                            builder.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
                        }

                        builder.IntegratedSecurity = false;
					builder.TrustServerCertificate = false;

					break;
				}
				default: { throw (new NotImplementedException()); }
			}

			return (builder);
		}
	}
}