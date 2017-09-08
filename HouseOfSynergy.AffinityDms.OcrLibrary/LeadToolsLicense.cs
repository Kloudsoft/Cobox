using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Leadtools;
using System.IO;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public static class LeadToolsLicense
	{
		
		/// <summary>
		/// Sets Up LeadTools License
		/// </summary>
		/// <param name="ServerMapPath">ServerPath</param>
		/// <param name="LeadtoolsTempDir">Return Leadtools Temporary Templates Directory </param>
		/// <param name="LeadToolsTemplatesDir">Return Leadtools Templates Directory</param>
		/// <param name="LeadtoolsAdvantageRuntimeDir">Return Leadtools Runtime Directory</param>
		/// <param name="exception">Returns Exception Occured</param>
		/// <returns>Returns true if successfull</returns>
		public static bool SetLicense (string ServerMapPath, out string LeadtoolsTempDir, out string LeadToolsTemplatesDir, out string LeadtoolsAdvantageRuntimeDir, out Exception exception)
		{
			bool result = false;
			exception = null;
			LeadToolsTemplatesDir = "";
			LeadtoolsTempDir = "";
			LeadtoolsAdvantageRuntimeDir = "";
			try
			{
				//LeadtoolsTempDir = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings ["LeadtoolsTempDir"]);
				//LeadToolsTemplatesDir = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings ["LeadToolsTemplatesDir"]);
				//LeadtoolsAdvantageRuntimeDir = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings ["LeadtoolsAdvantageRuntimeDir"]);
				//string LICENSE_FILE = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings ["LICENSE_FILE"]);
                LeadtoolsTempDir = Path.Combine(ServerMapPath,System.Configuration.ConfigurationManager.AppSettings["LeadtoolsTempDir"]);
                LeadToolsTemplatesDir = Path.Combine(ServerMapPath, System.Configuration.ConfigurationManager.AppSettings["LeadToolsTemplatesDir"]);
                LeadtoolsAdvantageRuntimeDir = Path.Combine(ServerMapPath, System.Configuration.ConfigurationManager.AppSettings["LeadtoolsAdvantageRuntimeDir"]);
                string LICENSE_FILE = Path.Combine(ServerMapPath, System.Configuration.ConfigurationManager.AppSettings["LICENSE_FILE"]);
				string LICENSE_KEY = System.Configuration.ConfigurationManager.AppSettings ["LICENSE_KEY"]; //i8xOXppT35aabV3DQdE7k4CRXMBKLm0IvN383qJp5zPooMDYamPE1yiYzXr5CmFEaZzEcrKaaiSHpLVG9+GblxojHaF6nx/u";
				RasterSupport.SetLicense(LICENSE_FILE, LICENSE_KEY);
				result = true;

			}
			catch (Exception ex)
			{
				exception = ex;
			}
			return result;
		}

		
		/// <summary>
		/// Sets Up LeadTools License
		/// </summary>
		/// <param name="DirPath">Main Directory Containing Leadtools and templates folders</param>
		/// <param name="tenantid">Current Tenant Id</param>
		/// <param name="UniqueTempFolderValue">Unique name used to create temporary folder</param>
		/// <param name="LeadtoolsTempDir">Return Leadtools Temporary Templates Directory</param>
		/// <param name="LeadToolsTemplatesDir">Return Leadtools Templates Directory</param>
		/// <param name="LeadtoolsAdvantageRuntimeDir">Return Leadtools Runtime Directory</param>
		/// <param name="exception">Returns Exception Occured</param>
		/// <returns>Returns true if successfull</returns>
		public static bool SetLicense(string DirPath,string tenantid, string UniqueTempFolderValue, out string LeadtoolsTempDir, out string LeadToolsTemplatesDir, out string LeadtoolsAdvantageRuntimeDir, out Exception exception)
		{
			bool result = false;
			exception = null;
			LeadToolsTemplatesDir = "";
			LeadtoolsTempDir = "";
			LeadtoolsAdvantageRuntimeDir = "";
			try
			{
                //DirPath = @"D:\Projects\Affinity\Source\HouseOfSynergy.AffinityDms.ConsoleClient\";
                string datafolder = Path.Combine(DirPath, "App_Data");
				if (!Directory.Exists(datafolder)){ Directory.CreateDirectory(datafolder); }
				string alltemplates = Path.Combine(DirPath, "App_Data", "AllTemplates");
				if (!Directory.Exists(alltemplates)) { Directory.CreateDirectory(alltemplates); }
				if (!string.IsNullOrEmpty(tenantid))
				{
					string tenantfolder = Path.Combine(DirPath, "App_Data", "AllTemplates", tenantid);
					if (!Directory.Exists(tenantfolder)) { Directory.CreateDirectory(tenantfolder); }
					string templatesfolder = Path.Combine(DirPath, "App_Data", "AllTemplates", tenantid, "Templates");
					if (!Directory.Exists(templatesfolder)) { Directory.CreateDirectory(templatesfolder); }
					LeadToolsTemplatesDir = templatesfolder;
					if (!(string.IsNullOrEmpty(UniqueTempFolderValue)))
					{
						string tempfolder = Path.Combine(DirPath, "App_Data", "AllTemplates", tenantid, "Temp_" + UniqueTempFolderValue);
						if (!Directory.Exists(tempfolder)) { Directory.CreateDirectory(tempfolder); } else { throw (new Exception("Temporary folder already in use.")); }
						LeadtoolsTempDir = tempfolder;
					}
				}
				string runtimefolder = Path.Combine(DirPath, "App_Data", "LeadTools", "Runtime");
				if (!Directory.Exists(runtimefolder)) { Directory.CreateDirectory(runtimefolder); }
                string licensefolder = Path.Combine(DirPath, "App_Data", "LeadTools", "License");
                if (!Directory.Exists(licensefolder)) { Directory.CreateDirectory(licensefolder); }
                if (Directory.GetFiles(runtimefolder).Length == 0) { throw (new Exception("Unable to find any runtime file")); }
				LeadtoolsAdvantageRuntimeDir = runtimefolder;
				string LICENSE_FILE = Path.Combine(DirPath, "App_Data", "LeadTools", "License", "eval-license-files.lic");
				if (!File.Exists(LICENSE_FILE)) { throw (new Exception("License File Not Found")); }
				string LICENSE_KEY_PATH = Path.Combine(Path.Combine(DirPath, "App_Data", "LeadTools", "License"), "eval-license-files.lic.key");
				if (!File.Exists(LICENSE_KEY_PATH)) { throw (new Exception("License Key Not Found")); }
				string LICENSE_KEY = File.ReadAllText(LICENSE_KEY_PATH);
				RasterSupport.SetLicense(LICENSE_FILE, LICENSE_KEY);
				result = true;

			}
			catch (Exception ex)
			{
				exception = ex;
				if (!(string.IsNullOrEmpty(UniqueTempFolderValue)))
				{
					string tempfolder = Path.Combine(DirPath, "Data", "AllTemplates", tenantid, "Temp_" + UniqueTempFolderValue);
					if (Directory.Exists(tempfolder))
					{
						Directory.Delete(tempfolder);
					}
				}
			}
			return result;
		}
	}
}