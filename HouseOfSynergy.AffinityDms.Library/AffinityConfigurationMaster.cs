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
    public static class AffinityConfigurationMaster
    {
        public static Exception exceptions = null;
        public static ConfigurationMode ConfigurationMode { get { return (AffinityConfiguration.IsConfigurationDebug ? ConfigurationMode.Debug : ConfigurationMode.Release); } }

        public static string DatabaseConnectionString
        {
            get
            {
                return (AffinityConfigurationMaster.DatabaseConnectionStringBuilder.ToString());
            }
        }

		public static SqlConnectionStringBuilder GetDatabaseConnectionStringBuilder (DeploymentLocation deploymentLocation)
		{
			var builder = new SqlConnectionStringBuilder();

			builder.IntegratedSecurity = true;
			builder.MultipleActiveResultSets = true;
			builder.InitialCatalog = "AffinityDmsMaster";

			switch (deploymentLocation)
			{
				case DeploymentLocation.W10EeVs2015CeU2: { builder.DataSource = @"W10EEVS2015CEU2\MSSQLEXPRESS2014"; break; }
				case DeploymentLocation.HosHp: { builder.DataSource = @"HOSLAPTOPHP\MSSQLS2016EXP"; break; }
				case DeploymentLocation.HosMonster: { builder.DataSource = @"MONSTER\MSSQLEXPRESS2014"; break; }
				case DeploymentLocation.HosRkPc: { builder.DataSource = @"RAHEELKHAN-PC\SQLEXPRESS"; break; }
				case DeploymentLocation.HosKausar: { builder.DataSource = @"KAUSARPC\SQLEXPRESSKAUSAR"; break; }
				case DeploymentLocation.BtsRizwan: { builder.DataSource = @"."; break; }
				case DeploymentLocation.BtsDanish: { builder.DataSource = @"."; break; }
				case DeploymentLocation.BtsUsmanJamal: { builder.DataSource = @"HOME-PC\SqlExpress2014"; break; }
				case DeploymentLocation.BtsFabehaMalik: { builder.DataSource = @"FABEHA-HDK\SQLEXPRESS"; break; }
				case DeploymentLocation.BtsUzma:
				{
					builder.DataSource = @"UZMA-PC\SQLEXPRESS";
					builder.UserID = @"sa";
					builder.Password = @"123456";
					builder.PersistSecurityInfo = true;
					builder.IntegratedSecurity = false;
					break;
				}
				case DeploymentLocation.BtsSaleem:
				{
					builder.DataSource = @"UzmaHashmi\SqlServer2014";
					builder.UserID = @"sa";
					builder.Password = @"123456";
					builder.PersistSecurityInfo = true;
					builder.IntegratedSecurity = false;

					break;
				}

                case DeploymentLocation.KsManjuLenovo:
                    {
                        builder.DataSource = @"LENOVO";
                        //builder.UserID = @"sa";
                        //builder.Password = @"123456";
                        builder.PersistSecurityInfo = true;
                        builder.IntegratedSecurity = true;

                        break;
                    }

                case DeploymentLocation.KsAdmintech:
                    {
                        builder.DataSource = @"ADMINNTECH";
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
                case DeploymentLocation.Live:
				{
					builder.ConnectionString
                       = @"Server=tcp:coboxdev.database.windows.net,1433;"
                        + @"Database=Affinity-GlobalAdmin-DB;"
                        + @"User ID=sysadmin;"
                        + @"Password=KLS@dm1nKLS@dm1n;"
                        + @"Encrypt=True;"
                        + @"TrustServerCertificate=False;"
                        + @"MultipleActiveResultSets=True;"
                        + @"Connection Timeout=60;";

                        break;
				}
				default: { throw (new NotImplementedException()); }
			}

			return (builder);
		}

		public static SqlConnectionStringBuilder DatabaseConnectionStringBuilder
		{
			get
			{
				return (AffinityConfigurationMaster.GetDatabaseConnectionStringBuilder(AffinityConfiguration.DeploymentLocation));
			}
		}

        //public static string MasterResourceGroup { get { return ("master-rg-affinity"); } }

        public static string MasterResourceGroup { get { return ("Affinity-ECM"); } }

        public static string UriMaster
        {
            get
            {
                //return ("http://dmsportalapp.azurewebsites.net/");
                switch (AffinityConfiguration.DeploymentLocation)
                {
                    //case DeploymentLocation.Live: { return ("http://dmsportalapp.azurewebsites.net/"); }
                    case DeploymentLocation.Live: { return ("http://affinity-ecm-globaladmin.azurewebsites.net/"); }
                    default: { return ("http://localhost:14688/"); }
                }
            }
        }

        public static string UriMasterWebApi
        {
            get
            {
                return (AffinityConfigurationMaster.UriMaster + "Api/");
            }
        }

        //public static string UriMasterApi
        //{
        //	get
        //	{
        //		//return ("http://kloudsoft-master-portal.azurewebsites.net/Pages/MasterApiWebForms.aspx");
        //		switch (AffinityConfiguration.DeploymentLocation)
        //		{
        //			case DeploymentLocation.Live: { return ("http://kloudsoft-master-portal.azurewebsites.net/Pages/MasterApiWebForms.aspx"); }
        //			default: { return ("http://localhost:14688/Pages/MasterApiWebForms.aspx"); }
        //		}
        //	}
        //}

        //public static string UrlStorage { get { return ("masteraffinitystorage"); } }
        public static string UrlStorage { get { return ("affinityecmstorage"); } }
        public static string UrlStorageBlob { get { return ("https://affinityecmstorage.blob.core.windows.net/"); } }
        public static string UrlStorageTable { get { return ("https://affinityecmstorage.table.core.windows.net/"); } }
        public static string UrlStorageQueue { get { return ("https://affinityecmstorage.queue.core.windows.net/"); } }
        public static string UrlStorageFile { get { return ("https://affinityecmstorage.file.core.windows.net/"); } }

        public static string StorageAccessKeyPrimary { get { return ("r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA=="); } }
        public static string StorageAccessKeySecondary { get { return ("Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w=="); } }
        public static string StorageConnectionStringPrimary { get { return ("DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA=="); } }
        public static string StorageConnectionStringSecondary { get { return ("DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w=="); } }
    }
}