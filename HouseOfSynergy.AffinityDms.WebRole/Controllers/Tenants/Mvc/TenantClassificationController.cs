using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using Microsoft.Cognitive.CustomVision;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;



namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{

    public class TenantClassificationController : Controller
    {
        private static List<MemoryStream> Imagelist;



        // GET: TenantClassification
        public ActionResult Index()
        {

            List<Template> templates = null;
            try
            {
                bool dbresult = false;
                Template sourcetemplate = null;
                ClassificationImagelist Templist = new ClassificationImagelist();

                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                ViewBag.ProjectName = tenantUserSession.Tenant.Domain;
                TempData["ProjectName"] = tenantUserSession.Tenant.Domain;

                dbresult = ElementManagement.GetAllTemplates(tenantUserSession, out templates, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                var templateslist = templates.Select(l => l.Title).ToList();

                ViewBag.Templates = templateslist.ToList();
                TempData["Templates"] = templateslist.ToList();


            }
            catch (Exception ex)
            {

                return this.View("~/Views/Tenants/Templates/Classification.cshtml");
            }

            return View("~/Views/Tenants/Templates/Classification.cshtml");
        }

        [HttpPost]
        public ActionResult Index(string projectname)
        {
            try
            {

                var FolderPath = "";
                if (Request.Files.Count > 0)
                {


                    HttpFileCollectionBase files = Request.Files;
                    long ID = 0; //long.Parse(id.ToString());
                    string ProjectName = Request.Form["ProjectName"];
                    string ProjectTagName = Request.Form["TemplateNames"];
                    //Ensure model state is valid  
                    if (ModelState.IsValid)
                    {   //iterating through multiple file collection   
                        for (int i = 0; i < files.Count; i++)
                        {

                            HttpPostedFileBase file = files[i];
                            string fname;
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            //fname = Path.Combine(Server.MapPath("~/UploadedFiles/"), fname);
                            //file.SaveAs(fname);


                            //Checking file is available to save.  
                            //if (file != null)
                            // {
                            //check Directory
                            var DirectorRoot = Path.Combine(Server.MapPath("~/UploadedFiles/"));
                            FolderPath = DirectorRoot + ProjectName + "/" + ProjectTagName;
                            //check existing
                            bool exists = Directory.Exists(FolderPath);

                            if (!exists)
                            {
                                Directory.CreateDirectory(FolderPath);
                            }
                            if (Directory.Exists(FolderPath))
                            {
                                var InputFileName = Path.GetFileName(file.FileName);
                                var ServerSavePath = FolderPath + "\\" + Guid.NewGuid().ToString().Substring(0, 8) + InputFileName;
                                //Save file to server folder  
                                file.SaveAs(ServerSavePath);
                                //assigning file uploaded status to ViewBag for showing message to user.  
                                ViewBag.UploadStatus = files.Count + " files uploaded successfully.";
                            }


                            //}

                        }
                        string Getallimages = "";
                        //Get subdirectories
                        string[] subdirectoryEntries = Directory.GetDirectories(Path.Combine(Server.MapPath("~/UploadedFiles/" + ProjectName + "/")));
                        foreach (string subdirectory in subdirectoryEntries)
                        {
                            Getallimages += "<br/>Template Name : " + Path.GetFileName(subdirectory) + "";
                            Getallimages += "<div class='container'>";

                            string[] fileEntries = Directory.GetFiles(subdirectory);
                            foreach (string fileName in fileEntries)
                            {
                                Getallimages += "<div class='block'>";
                                Getallimages += "<img src='" + "UploadedFiles/" + ProjectName + "/" + Path.GetFileName(subdirectory) + "/" + Path.GetFileName(fileName) + "' alt='' width='150' height='150' />";
                                Getallimages += "</div>";

                            }
                            if ((fileEntries.Length > 5) || (fileEntries.Length < 5))
                            {
                                Getallimages += "</div><br/><br/><br/><br/><br/><br/><br/><br/>";
                            }
                            else
                            {
                                Getallimages += "</div><br/>"; 
                            }
                        }


                        // List Images
                        //var model = new ClassificationImagelist
                        //{
                        //    Images = Directory.EnumerateFiles(FolderPath + "/")
                        //      .Select(fn => FolderPath + "/" + Path.GetFileName(fn))
                        //};




                        //return View("~/Views/Tenants/Templates/Classification.cshtml", model);
                        //return Json("File Uploaded Successfully!");
                        return Json(Getallimages, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("No files selected.");
                }
                //return View("~/Views/Tenants/Templates/Classification.cshtml", TempData);
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
            return Json("File Uploaded Successfully!");
        }



        
        public ActionResult Train()
        {
            
            Exception exception = null;
            bool dbresult = false;
            Template sourcetemplate = null;
            
            try
            {
                string ProjectName = Request.Form["ProjectName"];
                var result=Process(ProjectName);
                return Json(result.ToString());

            }
            catch (Exception ex)
            {

                return Json("Error occurred. Error details: " + ex.Message);
            }
            
        }

        private JsonResult Process(string ProjectName)
          {
            string trainingKey = System.Configuration.ConfigurationManager.AppSettings["trainingKey"].ToString(); 

            // Create the Api, passing in a credentials object that contains the training key
            TrainingApiCredentials trainingCredentials = new TrainingApiCredentials(trainingKey);
            TrainingApi trainingApi = new TrainingApi(trainingCredentials);


            // Create a new project
            string projectid = "";
            if (System.Configuration.ConfigurationManager.AppSettings["KloudProjectID"].ToString() != "")
            {
                //var project = trainingApi.CreateProject(ProjectName);
                projectid = System.Configuration.ConfigurationManager.AppSettings["KloudProjectID"].ToString();
                Session["projectId"] = projectid;
            }
            else
            {
                var project = trainingApi.CreateProject(ProjectName);
                projectid = project.Id.ToString();
                Session["projectId"] = projectid;

            }
            
            
            string[] subdirectoryEntries = Directory.GetDirectories(Path.Combine(Server.MapPath("~/UploadedFiles/" + ProjectName + "/")));
            foreach (string subdirectory in subdirectoryEntries)
            {
                var Tagging = trainingApi.CreateTag(new Guid(projectid), Path.GetFileName(subdirectory));

                Imagelist = Directory.GetFiles(Path.Combine(Server.MapPath("~/UploadedFiles/" + ProjectName + "/" + Path.GetFileName(subdirectory)))).Select(f => new MemoryStream(System.IO.File.ReadAllBytes(f))).ToList();

                foreach (var image in Imagelist)
                {
                    trainingApi.CreateImagesFromData(new Guid(projectid), image, new List<string>() { Tagging.Id.ToString() });

                }
            }

            // Now there are images with tags start training the project
            var iteration = trainingApi.TrainProject(new Guid(projectid));
            // The returned iteration will be in progress, and can be queried periodically to see when it has completed
            while (iteration.Status == "Training")
            {
                Thread.Sleep(1000);

                // Re-query the iteration to get it's updated status
                iteration = trainingApi.GetIteration(new Guid(projectid), iteration.Id);
            }

            // The iteration is now trained. Make it the default project endpoint
            iteration.IsDefault = true;
            trainingApi.UpdateIteration(new Guid(projectid), iteration.Id, iteration);

            ViewBag.UploadStatus = "Invoices Trained Successfully!";
            return Json("Success");

            

        }
        private void LoadImagesFromDisk(string tempid)
        {
            // this loads the images to be uploaded from disk into memory

            // List Images

            var DirectorRoot = Path.Combine(Server.MapPath("~/UploadedFiles/"));
            var FolderPath = DirectorRoot + tempid;
            //check existing
            bool exists = Directory.Exists(FolderPath);

            if (exists)
            {
                Imagelist = Directory.GetFiles(FolderPath).Select(f => new MemoryStream(System.IO.File.ReadAllBytes(f))).ToList();
            }

        }







    }
}