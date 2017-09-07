using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers
{
    public class HomeController : Controller
    {
		[HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            // Trace.TraceInformation("WebRole Home Index");
            return (this.View("~/Views/Home/Index.cshtml"));
        }
        public FileResult DownloadFiles(string file)
        {
            string fileName = string.Empty;
             
            try
            {
                if (file == "1")
                {
                    byte[] fileBytes = fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@"~/App_Data/Downloads/Leadtools.WebScanning.Setup.msi"));
                    fileName = "Leadtools.WebScanning.Setup.msi";
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                else if(file == "2")
                {
                    byte[] fileBytes = fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@"~/App_Data/Downloads/Affinity.Ecm.Client.Setup.exe"));
                    fileName = "Affinity.Ecm.Client.Setup.exe";
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                else
                    return File(new byte[10], System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            }
            catch(Exception ex)
            {
                System.IO.File.WriteAllText(Server.MapPath(@"~/App_Data/Downloads/Temp.txt"), ex.ToString());
                byte[] fileBytes = fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@"~/App_Data/Downloads/Temp.txt"));
                fileName = "Error.txt";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }

            
        }

    }
}