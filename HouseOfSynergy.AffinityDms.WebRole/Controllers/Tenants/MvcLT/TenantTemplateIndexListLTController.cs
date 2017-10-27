using System;
using System.Data;
using System.Collections.Generic;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Common;

using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Cognitive.CustomVision;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using WebSupergoo.ABCpdf10;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using System.Configuration;
using System.Web.UI;

using Leadtools;
using Leadtools.Pdf;
using Leadtools.Codecs;

using System.IO.Compression;
using AzureSearchBackupRestore;
using Microsoft.WindowsAzure.ServiceRuntime;

using Microsoft.Azure;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.MvcLT
{
    public class LstIndexes
    {
        public long NewindexId { get; set; }
        public string Newindexname { get; set; }
        public string Newindexvalue { get; set; }
        public string Newindexdisplayname { get; set; }
        public int NewInfexLeft { get; set; }
        public int NewInfexTop { get; set; }
        public int NewInfexWidth { get; set; }
        public int NewInfexHeight { get; set; }


    }

    public class Bloblist
    {
        public string batchno { get; set; }
        public int nooffiles { get; set; }
        public int status { get; set; }
        public string inputblob { get; set; }
        public string outputblob { get; set; }
        public string filetype { get; set; }

    }

    public class LstAzureSearch
    {
        public string fieldname { get; set; }
        public string fieldvalue { get; set; }
        
    }

    public class ClsConnectStorage

    {
        //Calling from Wroker role

        public void ConnectStorage(DataTable Dt)
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {

            }
            //            return Dt;

        }
    }

    public class TenantTemplateIndexListLTController : Controller
    {
        List<long> IDs = new List<long>();

        private static string TargetSearchServiceName = ConfigurationManager.AppSettings["TargetSearchServiceName"];
        private static string TargetSearchServiceApiKey = ConfigurationManager.AppSettings["TargetSearchServiceApiKey"];
        private static HttpClient HttpClient;
        private static Uri ServiceUri;

        public string ServerSavePathOPDF_;

        public string AppendBlobBloplocalcopy = "";  // Append/MamgePDF

        public string PublicIndexName;
        public string PublicIndexValue;
        public bool IsNewFolder = false;
        string DeliveryOrderNumber = "NoDoNumber";
        string InvoiceNumber = "NoInvoiceNumber";
        string AccountNumber = "NoAccountNumber";
        string BillNumber = "NoBillNumber";
        string CreateFolderName = "NoName";
        string IndexDisplayName = "";
        int TotalZipCount = 0;
        int TotalFileCount = 0;
        string FileType = "";
        public string OCR_Search = "";
        public string FileName_Prefix = "";

        List<LstIndexes> ObjList = new List<LstIndexes>();
        List<LstIndexes> OutObjList = new List<LstIndexes>();
        List<Bloblist> OutBlobList = new List<Bloblist>();
        List<string> OcrFileNames = new List<string>();

        List<string> ExtractedOriginalFileNames = new List<string>();


        public static CloudBlobClient blobClient;
        //public static CloudQueue fileQueue;
        public static CloudQueue fileQueue { get; set; }
        public static CloudBlobContainer BlobContainer;

        private static string inputContainer = "inputcobox";
        private static string outputContainer = "outputcobox";
        public string blobbatch = "";

       

        // GET: TanentTemplateIndexListLT
        public ActionResult Index()
        {
            //Session["tempimg"] = null;
            //Session["tempimg"] = null;
            //Session["Indexs"] = null;

             


            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            ViewBag.ProjectName = tenantUserSession.Tenant.Domain;
            Session["ProjectName"] = tenantUserSession.Tenant.Domain;


            return this.View("~/Views/Tenants/Templates/TenantTemplateIndexListLT.cshtml");
        }
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase[] file)
        {
            List<azureblob> listazureblob = new List<azureblob>();
            listazureblob = Checkblobs();


            bool flag = await UploadFilesToBlob(file);

            return this.View("~/Views/Tenants/Templates/TenantTemplateIndexListLT.cshtml");
        }

       
        public List<azureblob> Checkblobs()
        {
            //Bloblist Objbloblist = new Bloblist();

            List<azureblob> listazureblob = new List<azureblob>();

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            bool dbresult = AzureblobManagement.GetAllBlobs(tenantUserSession, out listazureblob, out exception);

            return listazureblob;
        }

        public async Task<bool> UploadFilesToBlob(HttpPostedFileBase[] files)
        {
            blobbatch = Guid.NewGuid().ToString();

            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            var _pathTenantDoc = "";
            try
            {
                if (files != null)
                {
                   
               
                    foreach (var file in files)
                    {
                        if (file.ContentLength > 0)
                        {


                            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

                            // Get context object for working with blobs, and 
                            // set a default retry policy appropriate for a web user interface.
                            var blobClient = storageAccount.CreateCloudBlobClient();

                            BlobContainer = blobClient.GetContainerReference(inputContainer);
                            if (BlobContainer.CreateIfNotExists())
                            {
                                // Enable public access on the newly created "inputContainer" container.
                                BlobContainer.SetPermissions(
                                    new BlobContainerPermissions
                                    {
                                        PublicAccess = BlobContainerPublicAccessType.Blob
                                    });
                            }

                            blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

                            // Get a reference to the blob container.
                            BlobContainer = blobClient.GetContainerReference(inputContainer);

                            // Get context object for working with queues, and 
                            // set a default retry policy appropriate for a web user interface.
                            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                            queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

                            // Get a reference to the queue.
                            fileQueue = queueClient.GetQueueReference(inputContainer);
                            fileQueue.CreateIfNotExists();

                            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                          
                            CloudBlockBlob blockBlob = BlobContainer.GetBlockBlobReference(blobName);

                            blockBlob.UploadFromStream(file.InputStream);

                            var queueMessage = new CloudQueueMessage(blobName);
                            await fileQueue.AddMessageAsync(queueMessage);



                              //OUT PUT
                            BlobContainer = blobClient.GetContainerReference(outputContainer);
                            if (BlobContainer.CreateIfNotExists())
                            {
                                // Enable public access on the newly created "inputContainer" container.
                                BlobContainer.SetPermissions(
                                    new BlobContainerPermissions
                                    {
                                        PublicAccess = BlobContainerPublicAccessType.Blob
                                    });
                            }
                            
                            int totalfiles = 0;



                            OutBlobList.Add(new Bloblist
                            {
                                batchno = blobbatch,
                                nooffiles = files.Count(),
                                filetype = Path.GetExtension(file.FileName),
                                status = 0,
                                inputblob = blobName,
                                outputblob = blobName
                            });
                            
                            //}

                        }
                    }               
                    
                }
            }
            catch (Exception Exx) { }

            try
            {
                if (OutBlobList != null) {
                    if (OutBlobList.Count > 0)
                    {
                        azureblob azbb = new azureblob();
                        foreach (var item in OutBlobList)
                        {
                            azbb.batchno = item.batchno;
                            azbb.nooffiles = item.nooffiles;
                            azbb.filetype = item.filetype;
                            azbb.inputblob = item.inputblob;
                            azbb.outputblob = item.outputblob;
                            azbb.status = item.status;
                            azbb.createdon = DateTime.Now;
                            bool dbresult = AzureblobManagement.AddBlob(tenantUserSession, azbb, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }

                        }
                    }
                }
            }
            catch (Exception exx){ }



            return await Task.FromResult(true);
        }

        public async Task ExtractBlob(string file)
        {

            
        }


        [HttpPost]
        public async Task<bool> IndexV2(string Blobpath, CloudQueue fileQueue,CloudQueueMessage msg, CloudBlobContainer OutputBlobContainer)
        {

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            ServerSavePathOPDF_ = "";
            //if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            var pathTenantDoc = "";
            var InputFileName = "";
            var Fileextn = "";
            var Filewithnoextn = "";
            string filenamenew = "";
            var ServerSavePathO = "";
            
            try
            {
                Template temp = null;

                if (Blobpath != null)
                {
                    if (Blobpath!="")
                    {
                        int maxWidth = 900;
                        int maxHeight = 1273;

                        try
                        {
                            pathTenantDoc = ConfigurationSettings.AppSettings.Get("pathTenantDoc").ToString(); 

                         
                            using (WebClient webClient = new WebClient())
                            {
                                InputFileName = Blobpath;
                                Fileextn = Path.GetExtension(InputFileName);
                                Filewithnoextn = Path.GetFileNameWithoutExtension(InputFileName);
                                filenamenew = Blobpath; //DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                ServerSavePathO = pathTenantDoc + Filewithnoextn + Fileextn;

                                webClient.DownloadFile(ConfigurationSettings.AppSettings.Get("BlobRootUrl").ToString() + Blobpath, pathTenantDoc + Blobpath);
                                webClient.Dispose();

                            }

                            if (Fileextn.ToString().ToUpper() == ".PDF")
                            {

                               // ViewBag.FileType = "PDF";
                              ///  ViewBag.FileCount = 1;
                              ///  ViewBag.TotalPageCount = 0;

                                string strLIC = "";
                                string strLICKey = "";
                              
                                //PDF to image  using LEAD TOOLS
                                try
                                {
                                    //strLIC = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic";
                                    //strLICKey = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key";

                                    strLIC = ConfigurationSettings.AppSettings.Get("LICPATH").ToString();
                                    strLICKey = ConfigurationSettings.AppSettings.Get("LICKEYPATH").ToString();

                                    RasterSupport.SetLicense(strLIC, System.IO.File.ReadAllText(strLICKey));
                                    // Load the input PDF document
                                    PDFDocument document = new PDFDocument(pathTenantDoc + filenamenew);
                                    using (RasterCodecs codecs = new RasterCodecs())
                                    {
                                        // Loop through all the pages in the document
                                        for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
                                        {
                                            // Render the page into a raster image
                                            document.Resolution = 300;
                                            using (RasterImage imagenew = document.GetPageImage(codecs, pageNumber))
                                            {
                                                codecs.Save(imagenew, pathTenantDoc + Filewithnoextn + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 24, 1, 1, -1, CodecsSavePageMode.Append);
                                                //codecs.Save(imagenew, pathTenantDoc + filenamenew + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 64, 1, 1, -1, CodecsSavePageMode.Append);
                                                //Session["ServerSavePathO"] = filenamenew + "Page_" + pageNumber + ".png";
                                                OcrFileNames.Add(Filewithnoextn + "Page_" + pageNumber + ".png");
                                             //   ViewBag.TotalPageCount = ViewBag.TotalPageCount + 1;

                                            }

                                        }
                                        codecs.Dispose();
                                    }
                                    
                                    document.Dispose();
                                    ServerSavePathOPDF_ = Filewithnoextn + Fileextn;
                                    ExtractedOriginalFileNames.Add(ServerSavePathOPDF_);
                                    //Session["ServerSavePathOPDF"] = filenamenew + Fileextn;
                                    // TempData["OcrFileNames"] = OcrFileNames;
                                    //  Session["OcrFileNames"] = OcrFileNames;

                                    await ProcessOcr(filenamenew + Fileextn, ExtractedOriginalFileNames, OcrFileNames, ServerSavePathOPDF_, fileQueue,msg, pathTenantDoc + Blobpath, OutputBlobContainer);
                                }
                                catch (Exception exx)
                                {

                                }
                            }
                            if (Fileextn.ToString().ToUpper() == ".ZIP")
                            {
                                //ViewBag.FileType = "ZIP";
                                //ViewBag.TotalPageCount = 0;
                                ExtractedOriginalFileNames.Clear();
                                OcrFileNames.Clear();
                                
                                using (ZipArchive za = ZipFile.OpenRead(ServerSavePathO))
                                {
                                    
                                    //  ViewBag.FileCount = za.Entries.Count();

                                        int iCOUNT = 0;

                                        foreach (ZipArchiveEntry zaItem in za.Entries)
                                        {
                                            iCOUNT++;


                                            filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                        try
                                        {

                                            zaItem.ExtractToFile(Path.Combine(pathTenantDoc, filenamenew + Path.GetExtension(zaItem.FullName)));
                                            ExtractedOriginalFileNames.Add(filenamenew + Path.GetExtension(zaItem.FullName));
                                        }
                                        catch (Exception ee) { }


                                        string strLIC = "";
                                        string strLICKey = "";
                                        List<string> OcrFileNames = new List<string>();
                                        //PDF to image  using LEAD TOOLS
                                        try
                                        {
                                            // strLIC = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic";
                                            //strLICKey = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key";

                                            strLIC = ConfigurationSettings.AppSettings.Get("LICPATH").ToString();
                                            strLICKey = ConfigurationSettings.AppSettings.Get("LICKEYPATH").ToString();

                                            RasterSupport.SetLicense(strLIC, System.IO.File.ReadAllText(strLICKey));
                                            // Load the input PDF document
                                            PDFDocument document = new PDFDocument(pathTenantDoc + filenamenew + Path.GetExtension(zaItem.FullName));
                                            using (RasterCodecs codecs = new RasterCodecs())
                                            {
                                                // Loop through all the pages in the document
                                                for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
                                                {
                                                    // Render the page into a raster image
                                                    document.Resolution = 300;
                                                    using (RasterImage imagenew = document.GetPageImage(codecs, pageNumber))
                                                    {
                                                        codecs.Save(imagenew, pathTenantDoc + filenamenew + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 24, 1, 1, -1, CodecsSavePageMode.Append);
                                                        //codecs.Save(imagenew, pathTenantDoc + filenamenew + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 64, 1, 1, -1, CodecsSavePageMode.Append);
                                                        //Session["ServerSavePathO"] = filenamenew + "Page_" + pageNumber + ".png";
                                                        OcrFileNames.Add(filenamenew + "Page_" + pageNumber + ".png");
                                                        //        ViewBag.TotalPageCount = ViewBag.TotalPageCount + 1;
                                                        imagenew.Dispose();

                                                    }

                                                }
                                                
                                                codecs.Dispose();
                                            }
                                            document.Dispose();
                                            ServerSavePathOPDF_ = filenamenew + Path.GetExtension(zaItem.FullName);

                                            if (iCOUNT == za.Entries.Count)
                                            {
                                                ExtractedOriginalFileNames.Add(InputFileName);
                                                za.Dispose();
                                            }


                                            // ExtractedOriginalFileNames.Add(filenamenew + Path.GetExtension(zaItem.FullName));

                                            //   Session["ServerSavePathOPDF"] = filenamenew + Path.GetExtension(zaItem.FullName);
                                            //  TempData["OcrFileNames"] = OcrFileNames;
                                            //                                            Session["OcrFileNames"] = OcrFileNames;
                                        }
                                        catch (Exception exx)
                                        {

                                        }

                                        
                                        await ProcessOcr(zaItem.FullName, ExtractedOriginalFileNames, OcrFileNames, ServerSavePathOPDF_, fileQueue,msg, pathTenantDoc + Blobpath, OutputBlobContainer);

                                    }
                                }




                            }

                            try
                            {


                                //Grouping Indexes and Listing

                                //If vendor Name Exist then Add at the top of INDEX UL-LI list
                                int I = 1;

                                List<ClassifiedFileIndexs> Indexlist = new List<ClassifiedFileIndexs>();


                                List<string> NewObjList = new List<string>();

                                if (ObjList.Count > 0)
                                {
                                    foreach (LstIndexes iList in ObjList)
                                    {
                                        NewObjList.Add(iList.Newindexvalue);
                                    }

                                    bool dbresult = ElementManagement.GetClassifiedIndexesbyIndexValue(tenantUserSession, NewObjList, out Indexlist, out exception);
                                    if (exception != null)
                                    {
                                        throw exception;
                                    }
                                    //sorting

                                    string previousItem = "";
                                    foreach (var item in Indexlist)
                                    {
                                        if (item.indexvalue != previousItem)
                                        {
                                            OutObjList.Add(new LstIndexes
                                            {
                                                NewindexId = item.Id,
                                                Newindexname = item.indexname,
                                                Newindexdisplayname = item.indexdisplayname,
                                                Newindexvalue = item.indexvalue,
                                                NewInfexLeft = 0,
                                                NewInfexTop = 0,
                                                NewInfexWidth = 0,
                                                NewInfexHeight = 0
                                            });
                                        }
                                        previousItem = item.indexvalue;

                                    }
                                }
                                var CheckObj = ObjList;

                            }
                            catch (Exception ee) { }

                        }
                        catch (Exception ex) { }
                        //template.TemplatePath = file.FileName; 
                    }
                }
            }
            catch (Exception ex) { }

            //List upload Documents
            List<Document> document_ = new List<Document>();
            try
            {
                document_ = Getalldocuments();
            }
            catch (Exception ecc) { }

            return await Task.FromResult(true);

            //   return this.View("~/Views/Tenants/Templates/TenantTemplateIndexListLT.cshtml", document_);
        }  // Upload and PROCESS tasks from BLOB -  second version

        [HttpPost]
        public async Task<ActionResult> IndexV1(HttpPostedFileBase file)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            var pathTenantDoc = "";
            var InputFileName = "";
            var Fileextn = ""; 
            var Filewithnoextn = "";
            string filenamenew = "";
            var ServerSavePathO = "";

            try
            {


                Template temp = null;

                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                                               

                        int maxWidth = 900;
                        int maxHeight = 1273;

                        try
                        {
                            if (file.ContentType == "application/octet-stream") //Content type for .zip is application/octet-stream
                            {
                                //Upload PDF to into Documents Folder
                                pathTenantDoc = Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/");
                                InputFileName = Path.GetFileName(file.FileName);
                                Fileextn = Path.GetExtension(file.FileName);
                                Filewithnoextn = Path.GetFileNameWithoutExtension(file.FileName);
                                filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                ServerSavePathO = pathTenantDoc + filenamenew + Fileextn;
                                file.SaveAs(ServerSavePathO);
                            }
                            else {
                                pathTenantDoc = Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/");
                                InputFileName = Path.GetFileName(file.FileName);
                                Fileextn = Path.GetExtension(file.FileName);
                                Filewithnoextn = Path.GetFileNameWithoutExtension(file.FileName);
                                filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                ServerSavePathO = pathTenantDoc + filenamenew + Fileextn;
                                Session["ServerSavePathOPDF"] = filenamenew + Fileextn;
                                file.SaveAs(ServerSavePathO);
                            }


                            if (Fileextn.ToString().ToUpper() == ".PDF")
                            {

                                ViewBag.FileType = "PDF";
                                ViewBag.FileCount = 1;
                                ViewBag.TotalPageCount = 0;

                                string strLIC = "";
                                string strLICKey = "";
                                List<string> OcrFileNames = new List<string>();
                                //PDF to image  using LEAD TOOLS
                                try
                                {
                                    strLIC = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic";
                                    strLICKey = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key";

                                    RasterSupport.SetLicense(strLIC, System.IO.File.ReadAllText(strLICKey));
                                    // Load the input PDF document
                                    PDFDocument document = new PDFDocument(pathTenantDoc + filenamenew + Fileextn);
                                    using (RasterCodecs codecs = new RasterCodecs())
                                    {
                                        // Loop through all the pages in the document
                                        for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
                                        {
                                            // Render the page into a raster image
                                            document.Resolution = 300;
                                            using (RasterImage imagenew = document.GetPageImage(codecs, pageNumber))
                                            {
                                                codecs.Save(imagenew, pathTenantDoc + filenamenew + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 24, 1, 1, -1, CodecsSavePageMode.Append);
                                                //codecs.Save(imagenew, pathTenantDoc + filenamenew + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 64, 1, 1, -1, CodecsSavePageMode.Append);
                                                //Session["ServerSavePathO"] = filenamenew + "Page_" + pageNumber + ".png";
                                                OcrFileNames.Add(filenamenew + "Page_" + pageNumber + ".png");
                                                ViewBag.TotalPageCount = ViewBag.TotalPageCount + 1;

                                            }

                                        }
                                    }
                                    Session["ServerSavePathOPDF"] = filenamenew + Fileextn;
                                    TempData["OcrFileNames"] = OcrFileNames;
                                    Session["OcrFileNames"] = OcrFileNames;

                                  //  await ProcessOcr(filenamenew + Fileextn);
                                }
                                catch (Exception exx)
                                {

                                }
                            }
                            if (Fileextn.ToString().ToUpper() == ".ZIP")
                            {
                                ViewBag.FileType =  "ZIP";
                                ViewBag.TotalPageCount = 0;

                                using (ZipArchive za = ZipFile.OpenRead(ServerSavePathO))
                                {
                                    ViewBag.FileCount = za.Entries.Count(); 

                                    foreach (ZipArchiveEntry zaItem in za.Entries)
                                    {
                                        filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                        try
                                        {
                                            
                                            zaItem.ExtractToFile(Path.Combine(pathTenantDoc, filenamenew + Path.GetExtension(zaItem.FullName)));
                                        }
                                        catch (Exception ee) { }


                                        string strLIC = "";
                                        string strLICKey = "";
                                        List<string> OcrFileNames = new List<string>();
                                        //PDF to image  using LEAD TOOLS
                                        try
                                        {
                                            strLIC = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic";
                                            strLICKey = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key";

                                            RasterSupport.SetLicense(strLIC, System.IO.File.ReadAllText(strLICKey));
                                            // Load the input PDF document
                                            PDFDocument document = new PDFDocument(pathTenantDoc + filenamenew + Path.GetExtension(zaItem.FullName));
                                            using (RasterCodecs codecs = new RasterCodecs())
                                            {
                                                // Loop through all the pages in the document
                                                for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
                                                {
                                                    // Render the page into a raster image
                                                    document.Resolution = 300;
                                                    using (RasterImage imagenew = document.GetPageImage(codecs, pageNumber))
                                                    {
                                                        codecs.Save(imagenew, pathTenantDoc + filenamenew + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 24, 1, 1, -1, CodecsSavePageMode.Append);
                                                        //codecs.Save(imagenew, pathTenantDoc + filenamenew + "Page_" + pageNumber + ".png", RasterImageFormat.Png, 64, 1, 1, -1, CodecsSavePageMode.Append);
                                                        //Session["ServerSavePathO"] = filenamenew + "Page_" + pageNumber + ".png";
                                                        OcrFileNames.Add(filenamenew + "Page_" + pageNumber + ".png");
                                                        ViewBag.TotalPageCount = ViewBag.TotalPageCount + 1;

                                                    }

                                                }
                                            }
                                            Session["ServerSavePathOPDF"] = filenamenew + Path.GetExtension(zaItem.FullName);
                                            TempData["OcrFileNames"] = OcrFileNames;
                                            Session["OcrFileNames"] = OcrFileNames;
                                        }
                                        catch (Exception exx)
                                        {

                                        }
                                       // await ProcessOcr(zaItem.FullName);

                                    }
                                }


                               
                            }

                            try {


                                //Grouping Indexes and Listing

                                //If vendor Name Exist then Add at the top of INDEX UL-LI list
                                int I = 1;

                                List<ClassifiedFileIndexs> Indexlist = new List<ClassifiedFileIndexs>();


                                List<string> NewObjList = new List<string>();

                                if (ObjList.Count > 0)
                                {
                                    foreach (LstIndexes iList in ObjList)
                                    {
                                        NewObjList.Add(iList.Newindexvalue);
                                    }

                                    bool dbresult = ElementManagement.GetClassifiedIndexesbyIndexValue(tenantUserSession, NewObjList, out Indexlist, out exception);
                                    if (exception != null)
                                    {
                                        throw exception;
                                    }
                                    //sorting

                                    string previousItem = "";
                                    foreach (var item in Indexlist)
                                    {
                                        if (item.indexvalue != previousItem)
                                        {
                                            OutObjList.Add(new LstIndexes
                                            {
                                                NewindexId = item.Id,
                                                Newindexname = item.indexname,
                                                Newindexdisplayname = item.indexdisplayname,
                                                Newindexvalue = item.indexvalue,
                                                NewInfexLeft = 0,
                                                NewInfexTop = 0,
                                                NewInfexWidth = 0,
                                                NewInfexHeight = 0
                                            });
                                        }
                                        previousItem = item.indexvalue;

                                    }
                                }
                                ViewBag.LstIndexes = OutObjList;
                                ViewBag.CreateFolderName = CreateFolderName;



                                var CheckObj = ObjList;


                            } catch (Exception ee) { }

                        }
                        catch (Exception ex) { }
                        //template.TemplatePath = file.FileName; 
                    }
                }
            }
            catch (Exception ex) { }

            //List upload Documents
            List<Document> document_ = new List<Document>();
            try
            {
                document_ = Getalldocuments();
            }
            catch (Exception ecc) { }


            return this.View("~/Views/Tenants/Templates/TenantTemplateIndexListLT.cshtml", document_);
        }  // Upload and PROCESS tasks from LOCAL storage - First version


        public List<Document> Getalldocuments()
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<Document> document = new List<Document>();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            bool dbresult = false;
            if (tenantUserSession.User.UserRoles.Any(x => x.RoleId == (int)TenantRoleType.Administrator))  // Admin
            {
                dbresult = DocumentManagement.GetAllDocuments(tenantUserSession, out document, out exception);
                document = DocumentManagement.GetMaxVersionDocuments(tenantUserSession, document);
                if (exception != null) { throw exception; }
                if (document.Count > 0)
                {
                    document = document.Where(x => x.WorkflowState == DocumentWorkflowState.Draft
                                                     //|| x.WorkflowState == DocumentWorkflowState.Submitted
                                                     //|| x.WorkflowState == DocumentWorkflowState.Rework
                                                     //|| x.WorkflowState == DocumentWorkflowState.ReworkPM
                                                     //|| x.WorkflowState == DocumentWorkflowState.ReworkSM
                                                     //|| x.WorkflowState == DocumentWorkflowState.ReworkSSOAD
                                                     //|| x.WorkflowState == DocumentWorkflowState.Recommend
                                                     //|| x.WorkflowState == DocumentWorkflowState.Advised
                                                     //|| x.WorkflowState == DocumentWorkflowState.Approved
                                                     //|| x.WorkflowState == DocumentWorkflowState.ProcessPayment
                                                     //|| x.WorkflowState == DocumentWorkflowState.Closed
                                                     ).ToList();
                  
                    document = document.Where(s => IDs.Contains(s.Id)).ToList();
               
 

                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {

                    //    var Docs = from s in document
                    //               join sa in context.Folders on s.FolderId equals sa.Id
                    //               select new { s, sa };

                    //    return Docs;
                    }

                }
            }
            return document;
        }

        


        public static Bitmap CropWhiteSpace(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            int white = 0xffffff;

            Func<int, bool> allWhiteRow = r =>
            {
                for (int i = 0; i < w; ++i)
                    if ((bmp.GetPixel(i, r).ToArgb() & white) != white)
                        return false;
                return true;
            };

            Func<int, bool> allWhiteColumn = c =>
            {
                for (int i = 0; i < h; ++i)
                    if ((bmp.GetPixel(c, i).ToArgb() & white) != white)
                        return false;
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (!allWhiteRow(row))
                    break;
                topmost = row;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (!allWhiteRow(row))
                    break;
                bottommost = row;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (!allWhiteColumn(col))
                    break;
                leftmost = col;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (!allWhiteColumn(col))
                    break;
                rightmost = col;
            }

            if (rightmost == 0) rightmost = w; // As reached left
            if (bottommost == 0) bottommost = h; // As reached top.

            int croppedWidth = rightmost - leftmost;
            int croppedHeight = bottommost - topmost;

            if (croppedWidth == 0) // No border on left or right
            {
                leftmost = 0;
                croppedWidth = w;
            }

            if (croppedHeight == 0) // No border on top or bottom
            {
                topmost = 0;
                croppedHeight = h;
            }

            try
            {
                var target = new Bitmap(croppedWidth, croppedHeight);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(
                  string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", topmost, bottommost, leftmost, rightmost, croppedWidth, croppedHeight),
                  ex);
            }
        }

        public Bitmap Save(Bitmap image, int maxWidth, int maxHeight)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight);
            var destImage = new Bitmap(maxWidth, maxHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
                // graphics.Dispose(); 
            }

            return destImage;



            // Get the image's original width and height
            // int originalWidth = image.Width;
            // int originalHeight = image.Height;

            //// To preserve the aspect ratio
            //float ratioX = (float)maxWidth / (float)originalWidth;
            //float ratioY = (float)maxHeight / (float)originalHeight;
            //float ratio = Math.Min(ratioX, ratioY);

            //// New width and height based on aspect ratio
            //int newWidth = (int)(originalWidth * ratio);
            //int newHeight = (int)(originalHeight * ratio);

            //// Convert other formats (including CMYK) to RGB.
            //Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            //// Draws the image in the specified size with quality mode set to HighQuality
            //using (Graphics graphics = Graphics.FromImage(newImage))
            //{
            //    graphics.CompositingQuality = CompositingQuality.HighQuality;
            //    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    graphics.SmoothingMode = SmoothingMode.HighQuality;
            //    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            //}

            //// Get an ImageCodecInfo object that represents the JPEG codec.
            //ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Bmp);

            //// Create an Encoder object for the Quality parameter.
            //System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            //// Create an EncoderParameters object. 
            //EncoderParameters encoderParameters = new EncoderParameters(1);

            //// Save the image as a JPEG file with quality level.
            //EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            //encoderParameters.Param[0] = encoderParameter;
            //newImage.Save(filePath, imageCodecInfo, encoderParameters);

            ////Stream

            //return newImage;

        }

        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }



        public bool ThumbnailCallback() { return false; }

        public async Task<ActionResult> ProcessOcr(string file, List<string>  ExtractedOriginalFileNames, List<string> OcrFileNames,string ServerSavePathOPDF_, CloudQueue fileQueue,CloudQueueMessage msg,string Bloplocalcopy, CloudBlobContainer OutputBlobContainer)
        {

            DeliveryOrderNumber = "NoDoNumber";
            InvoiceNumber = "NoInvoiceNumber";
            AccountNumber = "NoAccountNumber";
            BillNumber = "NoBillNumber";
            PublicIndexName="";
            PublicIndexValue = ""; 
            

            ViewBag.OcrProcessed = "YES";
            string OCRText = "";
            string _OCRText = "";

            List<Vendor> vendor = new List<Vendor>();
            try
            {


            
                int ClassificationIDorTemplateIDorTagID = 0;
                string Indexx = "";
                StringBuilder stringBuilder = new StringBuilder();
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

              //  if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }  // 11-10-2017

                Template temp = null;

                if (OcrFileNames != null)
                {


                    string SubscriptionKey = "5dc704a3098243dda51bb16d92c00d70";

                    //
                    // Create Project Oxford Vision API Service client
                    //
                    string apiroot = @"https://southeastasia.api.cognitive.microsoft.com/vision/v1.0/";
                    VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, apiroot);
               

                    //var pathTenantDoc = Path.Combine(Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/")) + Session["ServerSavePathO"].ToString();
                    string pathTenantDoc = ""; 
                    bool Ismatched = false;
                    bool Isnewline = false;
                    bool IsvendorExist = false;
                    string currentline = "";
                    string previousline = "";
                    bool IsIndexSddedAlready = false;



                    // if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }  // 11-10-2017

                    tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();

                    bool masterindex_result = false;
                    List<MasterIndex> masterindexlist = new List<MasterIndex>();
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        masterindex_result = ElementManagement.GetAllMasterIndexes(tenantUserSession, out masterindexlist, out exception);
                        if (exception != null) { throw exception; }
                    }


                    //Get VendorNamees to List


                    //  if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); } // 11-10-2017



                    bool dbresult = VendorManagement.GetAllVendors(tenantUserSession, out vendor, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((vendor != null) && dbresult)
                    {
                    }

                    if (OcrFileNames.Count() > 0)
                    {
                      //   step2:
                        for (int Ipage = 0; Ipage < OcrFileNames.Count(); Ipage++)
                        {
                            //pathTenantDoc = Path.Combine(Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/")) + OcrFileNames[Ipage].ToString();

                            pathTenantDoc = ConfigurationSettings.AppSettings.Get("pathTenantDoc").ToString() + OcrFileNames[Ipage].ToString();

                            using (Stream imageFileStream = System.IO.File.OpenRead(pathTenantDoc))
                            {
                                //
                                // Upload an image and perform OCR
                                //
                                OcrResults ocrResult = await VisionServiceClient.RecognizeTextAsync(imageFileStream, "en");
                                _OCRText = LogOcrResults(ocrResult);
                                OCRText += RearrangeOCR(_OCRText, ocrResult, vendor);
                                imageFileStream.Dispose();
                            }
                        }





                        try
                        {

                            

                            try
                            {
                                OCR_Search = OCRText;
                                OCRText = OCRText.Replace("\r\n\r\n", "\r\n");
                                
                                using (var reader = new StringReader(OCRText))
                                {
                                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine().Replace(" ", ""))  //.Replace(" ","")
                                    {
                                        if (line.ToUpper().Contains("DELIVERY"))
                                        {
                                            string test = "OK";
                                        }
                                        //if (line.ToUpper().Contains("INVNO"))
                                        //{
                                        //    string test = "OK";
                                        //}

                                        currentline = line;

                                        if (Ismatched && (Isnewline == false))  //  INDEXES found on same line
                                        {
                                            Ismatched = false;
                                            try
                                            {
                                                foreach (LstIndexes iList in ObjList)   // check if same INDEX name added into list already
                                                {
                                                    // if (PublicIndexName.ToUpper().Contains(iList.Newindexname.ToUpper())) { IsIndexSddedAlready = true; }
                                                    if (PublicIndexValue.ToUpper().Contains(iList.Newindexvalue.ToUpper())) { IsIndexSddedAlready = true; }
                                                }

                                                if (IsIndexSddedAlready == false)
                                                {
                                                    string tempvar = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');

                                                    if (tempvar == "(SCD)nt" && tempvar == "DATE")
                                                    { }
                                                    else
                                                    {
                                                        if (masterindexlist != null)
                                                        {
                                                            if (masterindexlist.Count > 0)
                                                            {
                                                                foreach (var item in masterindexlist)
                                                                {
                                                                    if (PublicIndexName.ToUpper().Contains(item.master_index_name))
                                                                    {
                                                                        IndexDisplayName = item.display_name;
                                                                        //FileName_Prefix = item.prefix_file_name;
                                                                    }
                                                                }

                                                            }
                                                        }


                                                        ObjList.Add(new LstIndexes
                                                        {
                                                            Newindexdisplayname = IndexDisplayName,
                                                            Newindexname = PublicIndexName.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            Newindexvalue = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            NewInfexLeft = 0,
                                                            NewInfexTop = 0,
                                                            NewInfexWidth = 0,
                                                            NewInfexHeight = 0
                                                        });
                                                    }

                                                }

                                                //Get Master INdex From DB

                                                if (masterindexlist != null)
                                                {
                                                    if (masterindexlist.Count > 0)
                                                    {
                                                        foreach (var item in masterindexlist)
                                                        {
                                                            if (PublicIndexName.ToUpper().Contains(item.master_index_name))
                                                            {
                                                                DeliveryOrderNumber = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                            }
                                                        }

                                                    }
                                                }



                                            }
                                            catch (Exception e) { }
                                        }

                                        if (Ismatched && Isnewline)  //  INDEXES found on Next line
                                        {
                                            Ismatched = false;
                                            try
                                            {
                                                foreach (LstIndexes iList in ObjList)   // check if same INDEX name added into list already
                                                {
                                                    // if (PublicIndexName.ToUpper().Contains(iList.Newindexname.ToUpper())) { IsIndexSddedAlready = true; }
                                                    if (PublicIndexValue.ToUpper().Contains(iList.Newindexvalue.ToUpper())) { IsIndexSddedAlready = true; }
                                                }

                                                if (IsIndexSddedAlready == false)
                                                {
                                                   string tempvar = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');

                                                    if (tempvar == "(SCD)nt" && tempvar == "DATE")
                                                    {
                                                    }
                                                    else
                                                    {


                                                        // Get Index Display Name and assign below Obj LIST

                                                        if (masterindexlist != null)
                                                        {
                                                            if (masterindexlist.Count > 0)
                                                            {
                                                                foreach (var item in masterindexlist)
                                                                {
                                                                    if (PublicIndexName.ToUpper().Contains(item.master_index_name))
                                                                    {
                                                                        IndexDisplayName = item.display_name;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                        

                                                        ObjList.Add(new LstIndexes
                                                        {
                                                            Newindexdisplayname = IndexDisplayName,
                                                            Newindexname = PublicIndexName.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            Newindexvalue = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            NewInfexLeft = 0,
                                                            NewInfexTop = 0,
                                                            NewInfexWidth = 0,
                                                            NewInfexHeight = 0
                                                        });
                                                    }


                                                    if (masterindexlist != null)
                                                    {
                                                        if (masterindexlist.Count > 0)
                                                        {
                                                            foreach (var item in masterindexlist)
                                                            {
                                                                if (PublicIndexName.ToUpper().Contains(item.master_index_name))
                                                                {
                                                                    DeliveryOrderNumber = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                                }
                                                            }

                                                        }
                                                    }


                                                }
                                            }
                                            catch (Exception e) { }
                                        }

       try
                                        {

                                      
                                            if (masterindexlist != null)
                                            {
                                                if (masterindexlist.Count > 0)
                                                {
                                                    foreach (var item in masterindexlist)
                                                    {
                                                        if (currentline.ToUpper().Contains(item.master_index_name))
                                                        {
                                                            Isnewline = fncheckIsNewline(currentline);
                                                            Ismatched = true;
                                                        }
                                                    }

                                                }
                                            }


                                        }
                                        catch (Exception exx) { }

                                        //vendor

                                        try
                                        {


                                             if (IsvendorExist == false)
                                            {
                                                foreach (var item in vendor)
                                                {
                                                    if (currentline.ToUpper().Contains(item.VendorName.ToUpper()) || (currentline.ToUpper().Contains(item.VendorName.ToUpper().Replace("-",""))) )
                                                    {
                                                        IsvendorExist = true;
                                                        CreateFolderName = item.VendorName.ToUpper();

                                                    }
                                                }
                                            }

                                        }
                                        catch (Exception exx) { }

                                    }
                                }

                            }
                            catch (Exception exx) { }

                            //if(ObjList.Count==0)
                            //{
                            //    goto step2;
                            //}
                            

                            // Custom Vision Test
                            string NewGuidName = Guid.NewGuid().ToString().Substring(1, 8);

                            //bool NewResult = Checkdocumentindexmatching(AccountNumber,BillNumber,DeliveryOrderNumber, InvoiceNumber, ObjList);
                            bool NewResult = Checkdocumentindexmatching("", "", DeliveryOrderNumber, "", ObjList);
                            // ViewBag.ProjectResult = Result;

                            //UploadDocuments
                            int Dtype = -1;
                            long newFolderCreated = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);

                            if (NewResult == false)  // Uplaod to Support
                            {
                                Dtype = 2;
                                if (CreateFolderName != "" && CreateFolderName != "NoName")
                                {

                                    //try
                                    //{
                                    //    Log aln = new Log();
                                    //    aln.documentid = 1245;
                                    //    aln.action = "Upload";
                                    //    aln.datetimecreated = DateTime.Now;
                                    //    aln.userid = tenantUserSession.User.Id;
                                    //    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                                    //}
                                    //catch (Exception exx) { }

                                    try
                                    {
                                        newFolderCreated = fnCreateFolder(CreateFolderName, Dtype, NewGuidName);
                                    }
                                    catch (Exception eppp) { }
                                    var documentid = fnUploadDocsOthers(newFolderCreated, Dtype, NewGuidName, ObjList, CreateFolderName, "Supporting", ServerSavePathOPDF_);
                                }
                                else
                                {
                                    long FolderMiscellaneous = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderMiscellaneous"]);
                                    var documentid = fnUploadDocsOthers(FolderMiscellaneous, Dtype, NewGuidName, ObjList, CreateFolderName, "Miscellaneous", ServerSavePathOPDF_);
                                }


                            }

                            if (NewResult == true)  // Merge
                            {
                                //Create New version
                                Dtype = 1;
                                long documentid = CheckoutDocument();

                                //collect Document IDs
                                IDs.Add(documentid);

                                try
                                {
                                    Log aln = new Log();
                                    aln.documentid = documentid;
                                    aln.action = "Upload";
                                    aln.datetimecreated = DateTime.Now;
                                    aln.userid = 1; // tenantUserSession.User.Id;
                                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                                }
                                catch (Exception exx) { }


                                if ((documentid != 0) && (documentid != null))
                                {
                                    documentid = fnAppendDocs(documentid, Dtype, NewGuidName, ObjList, ServerSavePathOPDF_, OutputBlobContainer);
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }


                try
                {

                    // Remove Local File and Blob Queue 

                   
                  

                    CloudBlockBlob OutblockBlob;

                    for (int Ipage = 0; Ipage < ExtractedOriginalFileNames.Count(); Ipage++)
                    {
                        OutblockBlob = OutputBlobContainer.GetBlockBlobReference(ExtractedOriginalFileNames[Ipage].ToString());
                        Stream fs = System.IO.File.OpenRead(ConfigurationSettings.AppSettings.Get("pathTenantDoc").ToString() + ExtractedOriginalFileNames[Ipage].ToString());
                        OutblockBlob.UploadFromStream(fs);
                        fs.Dispose();
                    }


                    for (int Ipage = 0; Ipage < OcrFileNames.Count(); Ipage++)    // Delete OCR image files(PNG)
                    {
                        System.IO.File.Delete(ConfigurationSettings.AppSettings.Get("pathTenantDoc").ToString() + OcrFileNames[Ipage].ToString());
                    }


                    //if (System.IO.File.Exists(Bloplocalcopy))   // Delete Origina lcopy of BLOB (Pdf or zip)
                    //{
                    //    System.IO.File.Delete(Bloplocalcopy);
                    //}

                    //if (System.IO.File.Exists(AppendBlobBloplocalcopy))   // Delete Original copy of BLOB for append (Pdf or zip)
                    //{
                    //    System.IO.File.Delete(Bloplocalcopy);
                    //}

                    for (int Ipage = 0; Ipage < ExtractedOriginalFileNames.Count(); Ipage++)
                    {
                        System.IO.File.Delete(ConfigurationSettings.AppSettings.Get("pathTenantDoc").ToString() + ExtractedOriginalFileNames[Ipage].ToString());
                    }


                    //fileQueue.DeleteMessage(msg);

                    ExtractedOriginalFileNames.Clear();
                    OcrFileNames.Clear();

                    // Delete Queue

                } catch (Exception exx) {

                    ExtractedOriginalFileNames.Clear();
                    OcrFileNames.Clear();

                }

            }
            catch (Exception ex) { }

           return null;

           // return this.View("~/Views/Tenants/Templates/TenantTemplateIndexListLT.cshtml");
            
        }
        public class LstIndexesArray
        {
            public string Ocrline { get; set; }
            public int NewInfexLeft { get; set; }
            public int NewInfexTop { get; set; }
            public int NewInfexWidth { get; set; }
            public int NewInfexHeight { get; set; }
            public int groupwords { get; set; }
            public int orderlines { get; set; }

        }
        public long fncheckvendordocumentype(string Ocr_Text, List<Vendor> vendor)
        {

            bool _IsvendorExist = false;
            long RtnType = 1;  // 1 = Lines, 0 = Blocks
            //vendor
            Ocr_Text = Ocr_Text.Replace("\r\n\r\n", "\r\n");

            try
            {

                using (var reader = new StringReader(Ocr_Text))
                {
                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine().Replace(" ", ""))  //.Replace(" ","")
                    {
                        try
                        {
                            if (_IsvendorExist == false)
                            {
                                foreach (var item in vendor)
                                {
                                    if (line.ToUpper().Contains(item.VendorName.ToUpper()) || (line.ToUpper().Contains(item.VendorName.ToUpper().Replace("-", ""))))
                                    {
                                        _IsvendorExist = true;
                                        CreateFolderName = item.VendorName.ToUpper();
                                        RtnType = item.doc_type;
                                    }
                                }
                            }
                        }
                        catch (Exception exx) { }
                    }
                }
            }
            catch (Exception exx) { }
            return RtnType;
        }

        public string RearrangeOCR(string Ocr_Text,OcrResults ocrResult, List<Vendor> vendor)
        {
            long Type = fncheckvendordocumentype(Ocr_Text,vendor);

            
            string OCR_Text = "";
            string templine = "";
            int _groupwords = 1;
            int _orderlines = 1;
            List<LstIndexesArray> ObjListArray = new List<LstIndexesArray>();

            for (int i = 0; i < ocrResult.Regions.Length; i++)
            {
                for (int j = 0; j < ocrResult.Regions[i].Lines.Length; j++)
                {
                    templine = "";
                    for (int k = 0; k < ocrResult.Regions[i].Lines[j].Words.Length; k++)
                    {
                        var test = ocrResult.Regions[i].Lines[j].Words.ToString(); 
                        float Jsonleft = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Left;
                        float Jsontop = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Top;
                        float Jsonwidth = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Width;
                        float Jsonheight = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Height;
                        
                        OCR_Text += ocrResult.Regions[i].Lines[j].Words[k].Text;
                        OCR_Text += " ";
                        templine = ocrResult.Regions[i].Lines[j].Words[k].Text + " ";

                        ObjListArray.Add(new LstIndexesArray
                        {
                            Ocrline = templine,
                            NewInfexLeft = (int)Jsonleft,
                            NewInfexTop = (int)Jsontop,
                            NewInfexWidth = (int)Jsonwidth,
                            NewInfexHeight = (int)Jsonheight,
                            groupwords = _groupwords,
                            orderlines = _orderlines
                        });


                    }
                    _groupwords++;
                }
                
            }

            string result = "";
            
            StringBuilder builder = new StringBuilder();
            int previouslineorder_no = 1;
            if (Type == 0)   // Exmaple RICHPORT OCR
            {
              //  for blocks - repeat task

               var sortedList = ObjListArray.OrderBy(x => x.NewInfexTop).ThenBy(x => x.NewInfexLeft).ThenBy(x => x.groupwords).ToList();
                previouslineorder_no = 1;
                foreach (LstIndexesArray words in sortedList) // Loop through all strings
                    {
                        if (previouslineorder_no == words.groupwords)
                        {
                            builder.Append(words.Ocrline).Append(" "); // Append string to StringBuilder
                        }
                        else
                        {
                            builder.Append("\r\n").Append(words.Ocrline); // Append string to StringBuilder
                        }

                        previouslineorder_no = words.groupwords;
                    }
                result = builder.ToString(); // Get string from StringBuilder
            }
            else
            {   // Exmaple BEST TECH, COOL-LINK AND SINGTEL OCR
                var sortedList = ObjListArray.OrderBy(x => x.groupwords).ThenBy(x => x.NewInfexLeft).ThenBy(x => x.NewInfexTop).ToList();
               
                foreach (LstIndexesArray words in sortedList) // Loop through all strings
                {
                    if (previouslineorder_no == words.groupwords)
                    {
                        builder.Append(words.Ocrline).Append(" "); // Append string to StringBuilder
                    }
                    else
                    {
                        builder.Append("\r\n").Append(words.Ocrline); // Append string to StringBuilder
                    }

                    previouslineorder_no = words.groupwords;
                }
                result = builder.ToString(); // Get string from StringBuilder
            }

            return result;

        }


        public void CreateJson_AzureSearch(string filenamepdf,long document_id)
        {
            string value_vendor_name = CreateFolderName;
            string value_account_no = Guid.NewGuid().ToString().ToUpper().Substring(1,16);
            string value_delivery_order = Guid.NewGuid().ToString().ToUpper().Substring(1, 16); 
            string value_ocr = OCR_Search.Replace(@"\", "-").Replace("'", "");
            string value_link = ConfigurationManager.AppSettings["openfile"] + filenamepdf;

            List<LstAzureSearch> Objsearch = new List<LstAzureSearch>();
            List<string> Objsearch2 = new List<string>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            
            // Create JSON for AZURE search
            try
            {
                string jsonData = @"{ 'value':[{'account_no':'"+ value_account_no + "',  'delivery_order':'"+ value_delivery_order + "' ,  'vendorname':'" + value_vendor_name + "',  'ocr':'" + value_ocr + "',  'link':'"+ value_link + "' }]}";

                //dict.Add("vendorname", value_vendor_name);
                //dict.Add("ocr", value_ocr);
                //dict.Add("link", value_link);

                string jsondata = new JavaScriptSerializer().Serialize(jsonData);

                    int count = Directory.GetFiles(ConfigurationSettings.AppSettings.Get("Schema_and_Data").ToString(), "cobox" + "*.json").Count();
                    count = count + 1;
                    string path = ConfigurationSettings.AppSettings.Get("Schema_and_Data").ToString(); 
                    System.IO.File.WriteAllText(path + "cobox" + count + ".json", jsonData);

                    ServiceUri = new Uri("https://" + TargetSearchServiceName + ".search.windows.net");
                    HttpClient = new HttpClient();
                    HttpClient.DefaultRequestHeaders.Add("api-key", TargetSearchServiceApiKey);

                    string fileName = path + "cobox" + count + ".json";

                    Console.WriteLine("Uploading documents from file {0}", fileName);
                    string jsonupload = System.IO.File.ReadAllText(fileName);
                    Uri uri = new Uri(ServiceUri, "/indexes/" + "cobox" + "/docs/index");
                    HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(HttpClient, HttpMethod.Post, uri, jsonupload);
                    response.EnsureSuccessStatusCode();

                try
                {
                    Exception exception = null;
                    TenantUserSession tenantUserSession = null;
                    tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();

                    // if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                    Log aln = new Log();
                    aln.documentid = 1010;
                    aln.action = "AZURE SEARCH JSON" + response.StatusCode + response.ReasonPhrase;
                    aln.datetimecreated = DateTime.Now;
                    aln.userid = 1; // tenantUserSession.User.Id;
                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                }
                catch (Exception exx) { }


            }
            catch (Exception exx) { }

        }


        [System.Web.Http.HttpPost]
        public JsonResult SaveTemplateIndex(List<LstIndexes> ObjList)
        {

            List<LstIndexes> ObjList1 = new List<LstIndexes>();
            var result = 0; ;
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        public bool fncheckIsNewline(string line)
        {
            try
            { 
                string[] ArrayLine;
                ArrayLine = line.Split(':').ToArray();
                             
                if (ArrayLine.Length == 1)
                {
                    PublicIndexName = line; //.Replace(" ", "");
                    return true;   // Go next line
                }

                if (ArrayLine.Length == 2)   // Both Found in Same line
                {
                    PublicIndexName = ArrayLine[0].Replace(" ", "").TrimStart(':').TrimStart(':').TrimEnd(':').TrimEnd('.');
                    PublicIndexValue = ArrayLine[1].Replace(" ", "").TrimStart(':').TrimStart(':').TrimEnd(':').TrimEnd('.');

                    if (ArrayLine[1].Replace(" ", "").Length > 0)  // If array 2 lenght ZERO then No Index value in this line
                    {
                        return false;  
                    }
                    else
                    {
                        return true;   // Go next line
                    }
                }

                if (ArrayLine.Length > 2)    // Both Found in Same line
                {
                    PublicIndexName = ArrayLine[0].Replace(" ","").TrimStart(':').TrimStart(':').TrimEnd(':').TrimEnd('.');
                    PublicIndexValue = ArrayLine[ArrayLine.Length-1].Replace(" ", "").TrimStart(':').TrimStart(':').TrimEnd(':').TrimEnd('.');

                    return false;
                }
            }
            catch (Exception exx) {

                PublicIndexName = line.Replace(" ", "").TrimStart(':').TrimStart(':').TrimEnd(':').TrimEnd('.');
            }
            return true;
        }


        protected string LogOcrResults(OcrResults results)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (results != null && results.Regions != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.AppendLine();
                foreach (var item in results.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }


        public long CheckoutDocument()
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

            tenantUserSession.Tenant.Id = 1;
            tenantUserSession.User.Id = 1;

            tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();
            Document document = null;
            long id = 0; ;
            // DocumentsViewModel documentViewModel = new DocumentsViewModel();
            bool result = false;
            string SearchFieldDO = "";
            string SearchFieldIN = "";
            string SearchFieldOTHER = "";
            try
            {
                if (DeliveryOrderNumber != "" && DeliveryOrderNumber != "NoDoNumber")
                {
                    SearchFieldDO = DeliveryOrderNumber;
                }
                if (InvoiceNumber != "" && InvoiceNumber != "NoInvoiceNumber")
                {
                    SearchFieldDO = InvoiceNumber; 
                }
                if (AccountNumber != "" && AccountNumber != "NoAccountNumber")
                {
                    SearchFieldDO = AccountNumber;
                }
                if (BillNumber != "" && BillNumber != "NoBillNumber")
                {
                    SearchFieldDO = BillNumber;
                }



              //  if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }

                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var FoldersWithSameName = context.ClassifiedFileIndexs.Where(x => ((x.indexvalue == SearchFieldDO))).ToList();
                    if (FoldersWithSameName.Count() > 0)
                    {
                        var documentID = context.ClassifiedFileIndexs.Where(x => ((x.indexvalue == SearchFieldDO))).Select(x => x.documentid).First();
                        id = documentID;
                        
                        result = DocumentManagement.CheckoutDoucmentAndMakeNewVersion(tenantUserSession, id, out document, out exception);
                        if (exception != null) { throw exception; }
                        return document.Id;
                    }
                }


            }
            catch (Exception ex)
            {

                return 0;
            }
            return 0;
        }

        [System.Web.Http.HttpPost]
        public JsonResult Updateindex(long id, string val)

        {
            int idd = Convert.ToInt16(id);
            var result = UpdateTemplateIndex(idd, val);
            //var json = new JavaScriptSerializer().Serialize(result);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpPost]
        public JsonResult UpdateTemplateIndex(int indexid, string val)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            ClassifiedFileIndexs sourceTemplateElement = new ClassifiedFileIndexs();
            tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();

            //if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }


            bool dbresult = false;

            sourceTemplateElement.Id = Convert.ToInt64(indexid);
            sourceTemplateElement.indexvalue = val;
            
            dbresult = ElementManagement.UpdateIndexs(tenantUserSession, sourceTemplateElement, out exception);
            if (exception != null)
            {
                throw exception;
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }




        public long SaveIndexsNew(List<LstIndexes> Obj, long documentid)
        {
            long rtnValue = 0;
            foreach (LstIndexes iList in Obj)
            {
                SaveIndexs(iList.Newindexname, iList.Newindexvalue, iList.NewInfexLeft, iList.NewInfexTop, iList.NewInfexWidth, iList.NewInfexHeight, documentid, 0,iList.Newindexdisplayname);
            }

            return rtnValue;
        }

        public long fnCreateFolder(string folderName, int DocumentType, string DeliveryOrderNumber)
        {
            int rtnStatus = 0;
            long parentFolderId = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);
            long parentFolderId_Completed = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderCompleted"]);
            long parentFolderId_Supporting = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderSupporting"]);
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

            tenantUserSession.Tenant.Id = 1;
            tenantUserSession.User.Id = 1;

            Folder newFolderCreated = null;
            Folder newFolderCreated_Completed = null;
            Folder newFolderCreated_Supporting = null;
            try
            {

                if (parentFolderId_Supporting > 1)
                {

                    tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();
                    // if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                    Folder folder = new Folder();

                    folder.DateTimeCreated = DateTime.UtcNow;
                    folder.DepartmentId = null;
                    folder.ParentId = parentFolderId;
                    folder.Name = folderName;
                    folder.FolderType = FolderType.Child;


                    //Folder Copy of Draft for Completed
                    Folder folder_Completed = new Folder();
                    folder_Completed.DateTimeCreated = DateTime.UtcNow;
                    folder_Completed.DepartmentId = null;
                    folder_Completed.ParentId = parentFolderId_Completed;
                    folder_Completed.Name = folderName;
                    folder_Completed.FolderType = FolderType.Child;

                    //Folder Copy of Draft for Supporting
                    Folder folder_Supporting = new Folder();
                    folder_Supporting.DateTimeCreated = DateTime.UtcNow;
                    folder_Supporting.DepartmentId = null;
                    folder_Supporting.ParentId = parentFolderId_Supporting;
                    folder_Supporting.Name = folderName;
                    folder_Supporting.FolderType = FolderType.Child;


                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                       
                            var FoldersWithSameName = context.Folders.Where(x => ((x.ParentId == folder_Supporting.ParentId) && (x.Name == folderName))).ToList();
                            if (FoldersWithSameName.Count() > 0)
                            {
                                var newFolderCreatedIDDDD = context.Folders.Where(x => ((x.ParentId == folder_Supporting.ParentId) && (x.Name == folderName))).Select(x => x.Id).First();
                                IsNewFolder = false;
                                return newFolderCreatedIDDDD;
                            }
                            else
                            {
                                IsNewFolder = true;

                                if (!FolderManagement.AddFolder(tenantUserSession, folder, out newFolderCreated, out exception)) { if (exception != null) { throw exception; } }

                                //For Completed
                                if (!FolderManagement.AddFolder(tenantUserSession, folder_Completed, out newFolderCreated_Completed, out exception)) { if (exception != null) { throw exception; } }

                                //For Supporting
                                if (!FolderManagement.AddFolder(tenantUserSession, folder_Supporting, out newFolderCreated_Supporting, out exception)) { if (exception != null) { throw exception; } }

                                return newFolderCreated_Supporting.Id;
                            }

                    }


                }
                else
                {
                    throw (new Exception("No folder selected"));
                }
            }
            catch (Exception ex)
            {
                return -1;
            }


            return newFolderCreated.Id;
        }

        public long fnAppendDocs(long documentid, int ClassificationID, string DeliveryOrderNumber, List<LstIndexes> Obj,string ServerSavePathOPDF_, CloudBlobContainer OutputBlobContaine)
        {
            Document document = null;

            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
                tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

                // if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();

                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var CountDocs = context.Documents.Where(x => ((x.Id == documentid))).ToList();
                    if (CountDocs.Count() > 0)
                    {
                        var Pdfname = context.Documents.Where(x => ((x.Id == documentid))).Select(x => x.Name).First();

                        var pathTenantDoc = ConfigurationSettings.AppSettings.Get("pathTenantDoc").ToString();// Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/");

                        string filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                        string FileNamePDF = "";
                        
                        //Call PDF conversion and Append DO of the DO number
                        
                        FileNamePDF = ManagePDF(null, pathTenantDoc, Pdfname, pathTenantDoc, filenamenew + ".pdf", documentid, ServerSavePathOPDF_, OutputBlobContaine);

                        ExtractedOriginalFileNames.Add(FileNamePDF);

                        //Create Json for azure search

                        try
                        {
                            CreateJson_AzureSearch(FileNamePDF, documentid);
                        }
                        catch (Exception exx) { }


                        try
                        {
                            Log aln = new Log();
                            aln.documentid = documentid;
                            aln.action = "Merge";
                            aln.datetimecreated = DateTime.Now;
                            aln.userid = 1; //tenantUserSession.User.Id;
                            LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                        }
                        catch (Exception exx) { }


                        document = context.Documents.Where(x => x.Id == documentid).FirstOrDefault();
                        document.Name = FileNamePDF;
                        context.SaveChanges();

                        try
                        {

                            string SearchFieldDO = "";
                            string SearchFieldIN = "";
                            string SearchFieldOTHER = "";
                                if (DeliveryOrderNumber != "")
                                {
                                    SearchFieldDO = DeliveryOrderNumber;
                                }
                                if (InvoiceNumber == "")
                                {
                                    SearchFieldDO = InvoiceNumber;
                                }

                                var documentID = context.ClassifiedFileIndexs.Where(x => ((x.indexvalue == SearchFieldDO))).Select(x => x.documentid).First();

                           
                        }
                        catch (Exception exx) { }


                        //Move Supporting to Draft
                        try
                        {
                            Folder folder = new Folder();
                            Folder Isfolder = new Folder();

                            folder = context.Folders.Where(x => x.Id == document.FolderId).First();
                            long DraftFolderId = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);

                            Isfolder = context.Folders.Where(x => x.ParentId == DraftFolderId && x.Name == folder.Name).First();
                            if (Isfolder != null)
                            {
                                //
                                var Docs = context.Documents.Where(x => x.DocumentOriginalId == document.DocumentOriginalId).ToList();
                                Docs.ForEach(a => a.FolderId = Isfolder.Id);
                                Docs.ForEach(a => a.WorkflowState = DocumentWorkflowState.Draft);
                                context.SaveChanges();


                                try
                                {
                                    Log aln = new Log();
                                    aln.documentid = documentid;
                                    aln.action = "Draft";
                                    aln.datetimecreated = DateTime.Now;
                                    aln.userid = 1; // tenantUserSession.User.Id;
                                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                                }
                                catch (Exception exx) { }

                            }
                        }
                        catch (Exception exx) { }

                        foreach (LstIndexes iList in Obj)
                        {
                            SaveIndexs(iList.Newindexname, iList.Newindexvalue, iList.NewInfexLeft, iList.NewInfexTop, iList.NewInfexWidth, iList.NewInfexHeight, document.Id, ClassificationID,iList.Newindexdisplayname);
                        }

                       


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }
        public long fnUploadDocs(long folderId, int ClassificationID, string DeliveryOrderNumber, List<LstIndexes> Obj)
        {
            Document document = null;

            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
                tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();

                //if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                if (exception != null)
                    throw exception;

                var fileName = Session["InputFileName"].ToString();
                var DirectorRoot = Path.Combine(Server.MapPath("~/UploadedFiles/TemplateIndexList/"));
                var ServerSavePathR = DirectorRoot + "Resized/";
                Image image = Image.FromFile(ServerSavePathR + fileName, true);
                Bitmap bmp = new Bitmap(image);
                byte[] imageByte = (byte[])(new ImageConverter()).ConvertTo(bmp, typeof(byte[]));

                var pathTenantDoc = Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/");
                if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }

                string fileExty = Path.GetExtension(fileName);

                string filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                string Documenttype = Session["Dtype"].ToString();
                string FileNamePDF = "";
                string FilenameImg = DeliveryOrderNumber + "_" + filenamenew + fileExty;

                //Call PDF conversion and Append DO of the DO number
                var physicalPath = Path.Combine(pathTenantDoc, DeliveryOrderNumber + "_" + filenamenew + fileExty);
                bmp.Save(physicalPath);
                FileNamePDF = createPDF(bmp, ServerSavePathR, fileName, pathTenantDoc, DeliveryOrderNumber + "_" + filenamenew, ".pdf");
                bool result = CreateDocumentEntry(tenantUserSession, FilenameImg, FileNamePDF, imageByte.Length, folderId, ClassificationID, Obj,"Upload","");

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }
        public long fnUploadDocsOthers(long folderId, int ClassificationID, string DeliveryOrderNumber, List<LstIndexes> Obj, string CreateFolderName,string logtype,string ServerSavePathOPDF_)
        {
            Document document = null;

            TenantUserSession tenantUserSession = null;
            tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());
            Exception exception = null;

            try
            {
                tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();

                //if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                if (exception != null)
                    throw exception;

                var FileNamePDF = ServerSavePathOPDF_;


                var pathTenantDoc = ConfigurationSettings.AppSettings.Get("pathTenantDoc").ToString();
                if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }

               bool result = CreateDocumentEntry(tenantUserSession, FileNamePDF, FileNamePDF, 100, folderId, ClassificationID, Obj,logtype, CreateFolderName);
              

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        public string createPDF(Bitmap bmp, string Readpath, string OriginalFilename, string Savepath, string FileName, string extn)
        {


            //PDF to image  using LEAD TOOLS

            //try
            //{
            //    //RasterSupport.SetLicense(@"D:\DEVMVC\LEADTOOLSSAMPLES\LEADTOOLSSAMPLES\App_Data\LeadTools\License\eval-license-files.lic", System.IO.File.ReadAllText(@"D:\DEVMVC\LEADTOOLSSAMPLES\LEADTOOLSSAMPLES\App_Data\LeadTools\License\eval-license-files.lic.key"));
            //    RasterSupport.SetLicense(@"D:\DEVMVC\TFS-Current\Github Cobox Prototype\HouseOfSynergy.AffinityDms.WebRole\App_Data\LeadToolsV2\License\Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic", System.IO.File.ReadAllText(@"D:\DEVMVC\TFS-Current\Github Cobox Prototype\HouseOfSynergy.AffinityDms.WebRole\App_Data\LeadToolsV2\License\Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key"));
            //    // Load the input PDF document
            //    PDFDocument document = new PDFDocument(Readpath + "/" + OriginalFilename);
            //    using (RasterCodecs codecs = new RasterCodecs())
            //    {
            //        // Loop through all the pages in the document
            //        for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
            //        {
            //            // Render the page into a raster image
            //            using (RasterImage imagenew = document.GetPageImage(codecs, pageNumber))
            //            {
            //                // Append to (or create if it does not exist) a TIFF file
            //                codecs.Save(imagenew, DirectorRoot + "Original/" + Filewithnoextn + Fileextn, RasterImageFormat.TifJpeg, 24, 1, 1, -1, CodecsSavePageMode.Append);
            //            }
            //        }
            //    }
                

            //}
            //catch (Exception exx) { }

            return FileName + extn;

            //PDF to image  using LEAD TOOLS END


            //  //string license = "4AEA30F4DBB8171DE7CF1166516E03";
            //  string license = "XeJREBodo/8B5XUBbv2MatilIrQdPdtypqn67/2pRFUAjFi+KCb6N+owejUx5nQFBw==";
            ////  XSettings.InstallTrialLicense(license);

            //  Doc theDoc = new Doc();
            //  theDoc.Rect.Inset(0, 0);
            //  string thePath = Readpath + OriginalFilename;
            //  theDoc.AddImageBitmap(bmp, true);
            //  bmp.Dispose();
            //  theDoc.Save(Savepath + FileName + extn);
            //  theDoc.Clear();
            //  return FileName + extn;

            //-------------------------------------

            //MemoryStream stream = new MemoryStream(imageByte);
            //PDFDocument document = new PDFDocument(stream);

            // PDFFile pdfdoc = new PDFFile(Readpath + OriginalFilename);

            //using (RasterCodecs codecs = new RasterCodecs())
            //{
            //    // Loop through all the pages in the document 
            //    for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
            //    {
            //        // Render the page into a raster image 
            //        using (RasterImage image = document.GetPageImage(codecs, pageNumber))
            //        {
            //            // Append to (or create if it does not exist) a TIFF file 
            //            codecs.Save(image, "Output.tif", RasterImageFormat.TifJpeg, 24, 1, 1, -1, CodecsSavePageMode.Append);
            //        }
            //    }
            //}

            //return FileName + extn;




        }

        public string ManagePDF(Bitmap bmp, string pathTenantDoc, string Pdfname, string pathTenantDoc1, string FileName, long documentid, string ServerSavePathOPDF_, CloudBlobContainer OutputBlobContaine)
        {
            Document document = null;
            string PDFFilenametoAppend = "";
            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
               // if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                if (exception != null)
                    throw exception;


                try
                {
                    //RasterSupport.SetLicense(@"D:\DEVMVC\LEADTOOLSSAMPLES\LEADTOOLSSAMPLES\App_Data\LeadTools\License\eval-license-files.lic", System.IO.File.ReadAllText(@"D:\DEVMVC\LEADTOOLSSAMPLES\LEADTOOLSSAMPLES\App_Data\LeadTools\License\eval-license-files.lic.key"));
                    //RasterSupport.SetLicense(@"D:\DEVMVC\TFS-Current\Github Cobox Prototype\HouseOfSynergy.AffinityDms.WebRole\App_Data\LeadToolsV2\License\Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic", System.IO.File.ReadAllText(@"D:\DEVMVC\TFS-Current\Github Cobox Prototype\HouseOfSynergy.AffinityDms.WebRole\App_Data\LeadToolsV2\License\Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key"));

                    string strLIC = ConfigurationSettings.AppSettings.Get("LICPATH").ToString();
                    string strLICKey = ConfigurationSettings.AppSettings.Get("LICKEYPATH").ToString();
                    
                    RasterSupport.SetLicense(strLIC, System.IO.File.ReadAllText(strLICKey));

                    RasterCodecs _codecs = new RasterCodecs();
                    //_codecs.Options.Pdf.InitialPath = @"C:\Temp\PDFEngine";
                    _codecs.Options.Pdf.InitialPath = ConfigurationSettings.AppSettings.Get("PDFEngine").ToString();

                    string blobname = Pdfname;
                    using (WebClient webClient = new WebClient())
                    {

                        CloudBlockBlob blockBlob = OutputBlobContaine.GetBlockBlobReference(blobname);

                        if(blockBlob.Exists())
                        {
                            webClient.DownloadFile(ConfigurationSettings.AppSettings.Get("BlobRootUrlOP").ToString() + blobname, pathTenantDoc + blobname);
                            webClient.Dispose();
                        }
                        else
                        {
                            webClient.DownloadFile(ConfigurationSettings.AppSettings.Get("BlobRootUrl").ToString() + blobname, pathTenantDoc + blobname);
                            webClient.Dispose();
                        }
                        
                        ExtractedOriginalFileNames.Add(blobname);
                        
                    }

                    Thread.Sleep(5000);

                    if (_codecs.Options.Pdf.IsEngineInstalled)
                    {

                        PDFFile firstFile = new PDFFile(pathTenantDoc + Pdfname);

                        firstFile.MergeWith(new string[] { pathTenantDoc + ServerSavePathOPDF_ }, pathTenantDoc + FileName);
                        
                    }
                    
                    _codecs.Dispose();

                }
                catch (Exception exx) {
                    

                }

            }
            catch (Exception ex) { }

            return FileName;
        }


        public bool SaveIndexs(string desc, string val, int x, int y, int w, int h, long documentid, int classification,string dispalyname)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            ClassifiedFileIndexs sourceTemplateElement = new ClassifiedFileIndexs();
            tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

            tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();

            // if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }


            bool dbresult = false;
            sourceTemplateElement.userid = 1; // tenantUserSession.Tenant.Id;
            sourceTemplateElement.userdomain = "kloud-soft.com";  // tenantUserSession.Tenant.Domain;
            sourceTemplateElement.indexdisplayname = dispalyname;
            sourceTemplateElement.indexname = desc;
            sourceTemplateElement.indexvalue = val;
            sourceTemplateElement.indexbounding = x + "^" + y + "^" + w + "^" + h;
            sourceTemplateElement.classification = classification.ToString();
           // sourceTemplateElement.userfilepathO = Session["ServerSavePathO"].ToString();
          //  sourceTemplateElement.userfilepathR = Session["ServerSavePathR"].ToString();
            sourceTemplateElement.documentid = documentid;


            dbresult = ElementManagement.AddFileIndexs(tenantUserSession, sourceTemplateElement, out exception);
            if (exception != null)
            {
                throw exception;
            }
            return dbresult;
        }


        public bool Checkdocumentindexmatching(string AN,string BI, string DO, string IN, List<LstIndexes> Obj)
        {
            string Str = "";
            int IDx = 0;
            string Strstatus = "";
            Document document = null;

            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
                //if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

                tenantUserSession.Tenant.DatabaseConnectionString = ConfigurationSettings.AppSettings.Get("DatabaseConnectionString_Local").ToString();


                try
                {
                    
                        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                        {
                        if ((DO != "") || (IN != "") || (AN != "") || (BI != ""))
                        {
                            var IsExist = context.ClassifiedFileIndexs.Where(x => ((x.indexvalue == DO) || (x.indexvalue == IN) || (x.indexvalue == AN) || (x.indexvalue == BI))).ToList();
                            if (IsExist.Count() > 0) { return true; }
                        }
                        if ((DO != "") && (IN != ""))
                        {
                           var IsExist = context.ClassifiedFileIndexs.Where(x => ((x.indexvalue == DO) || (x.indexvalue == IN))).ToList();
                           if (IsExist.Count() > 0) { return true; }
                        }
                        if ((DO != "") && (IN == ""))
                        {
                            var IsExist = context.ClassifiedFileIndexs.Where(x => (x.indexvalue == DO)).ToList();
                            if (IsExist.Count() > 0) { return true; }
                        }
                        if ((DO == "") && (IN != ""))
                        {
                            var IsExist = context.ClassifiedFileIndexs.Where(x => (x.indexvalue == IN)).ToList();
                            if (IsExist.Count() > 0) { return true; }
                        }
                        if ((DO == "") && (IN == "") && (AN == "") && (BI == ""))
                        {
                            return false;
                        }
                    }
                  }
                    catch (Exception exx) { }
                
            }
            catch (Exception exx) { }

            return false;
        }


        public string CVTestProcess()
        {
            string Str = "";
            //Get Project Name and ID
            try
            {
                Template tm = new Template();

                ClassificationImagelist Templist = new ClassificationImagelist();


                //string trainingKey = "0596089aa569488e9b827616d9dfee41";
                string trainingKey = System.Configuration.ConfigurationManager.AppSettings["trainingKey"].ToString();

                // Create the Api, passing in a credentials object that contains the training key
                TrainingApiCredentials trainingCredentials = new TrainingApiCredentials(trainingKey);
                TrainingApi trainingApi = new TrainingApi(trainingCredentials);

                var account = trainingApi.GetAccountInfo();

                Guid ProjectIdGuid = Guid.Empty;
                //ProjectIdGuid = new Guid(Session["projectId"].ToString());

                if (System.Configuration.ConfigurationManager.AppSettings["KloudProjectID"] == "")
                {
                    ProjectIdGuid = new Guid(Session["projectId"].ToString());
                }
                else
                {
                    ProjectIdGuid = new Guid(System.Configuration.ConfigurationManager.AppSettings["KloudProjectID"]);
                }



                var predictionKey = account.Keys.PredictionKeys.PrimaryKey;

                PredictionEndpointCredentials predictionEndpointCredentials = new PredictionEndpointCredentials(predictionKey);
                PredictionEndpoint endpoint = new PredictionEndpoint(predictionEndpointCredentials);

                byte[] imgbytearray = (byte[])Session["imgbyte"];
                Stream stream = new MemoryStream(imgbytearray);

                var result = endpoint.PredictImage(ProjectIdGuid, stream);

                var resultlist = result.Predictions.OrderByDescending(x => x.Probability);

                if (resultlist.Count() == 0)
                    Session["Dtype"] = "2";  // Default to Supporting FOlder

                int IDx = 0;
                string Strstatus = "";
                foreach (var c in resultlist)
                {
                    if (IDx == 0)
                    {
                        Strstatus = "<b style='color:maroon;font-size:18px;font-weight:bold'>" + c.Tag + "</b>";
                        if (c.Tag.Contains("INVOICE"))
                            Session["Dtype"] = "0";
                        else if (c.Tag.Contains("DO"))
                            Session["Dtype"] = "1";
                        else if (c.Tag.Contains("DELIVERY ORDER"))
                            Session["Dtype"] = "1";
                        else
                            Session["Dtype"] = "2";  //  Supporting FOlder
                    }
                    //Console.WriteLine($"\t{c.Tag}: {c.Probability:P1}");

                    Str += c.Tag + "  :  <b>" + c.Probability + "</b>&nbsp;&nbsp;";
                    IDx++;
                }
                Str += Strstatus;



                return Str;

            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
            return "Done!";
        }

        private bool CreateDocumentEntry(TenantUserSession tenantUserSession, string filename, string filenamepdf, long size, long folderId, int ClassificationID, List<LstIndexes> Obj,string logtype, string vendorname)
        {
            var result = false;
            Document document = null;
            if (size <= 0) { throw (new ArgumentException("The argument [length] is zero.", "length")); }
            if (string.IsNullOrWhiteSpace(filename)) { throw (new ArgumentException("The argument [filename] is empty.", "filename")); }
            Exception exception = null;
            result = false;
            if (document == null)
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    try
                    {
                        var file = new FileInfo(filename);

                        document = new Document();

                        document.Name = file.Name;
                        document.Hash = string.Empty;
                        document.Length = size;
                        document.DateTime = DateTime.UtcNow;
                        document.SourceType = SourceType.DesktopFileSystem;
                        document.DeviceName = "";
                        document.FullTextOCRXML = "";
                        document.FileNameServer = file.Name;
                        document.FileNameClient = file.FullName;
                        document.State = DocumentState.Uploading;
                        document.IsFinalized = true;
                        document.CheckedOutDateTime = DateTime.UtcNow;
                        document.CheckedOutByUserId = 1;// tenantUserSession.User.Id;
                        document.VersionCount = 0;
                        document.VersionMajor = 1;
                        document.VersionMinor = 0;
                        document.UserId = 1; // tenantUserSession.User.Id;
                        document.ClassificationID = ClassificationID;  //INVOICE=0,DO-1,Support=2
                        document.IsPrivate = true;
                        document.FolderId = folderId;
                        document.LatestCheckedOutByUserId = 0;
                        document.Namepdf = filenamepdf;
                        document.vendorname = vendorname;
                        document.WorkflowState = HouseOfSynergy.AffinityDms.Entities.Lookup.DocumentWorkflowState.Draft;
                        context.Documents.Add(document);
                        context.SaveChanges();

                        document.DocumentOriginalId = document.Id;
                        document.DocumentParent = document.Id;

                        //collect Document IDs
                        IDs.Add(document.Id);


                        document.FileNameServer = document.Id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                        context.SaveChanges();

                        //User Documents
                        //var userDocument = new UserDocument() { UserId = tenantUserSession.User.Id, DocumentId = document.DocumentOriginalId, IsActive = true };
                        var userDocument = new UserDocument() { UserId = 1, DocumentId = document.DocumentOriginalId, IsActive = true };
                        context.UserDocuments.Add(userDocument);
                        context.SaveChanges();

                        
                    
                        ///Save Indexes
                        ///
                        foreach (LstIndexes iList in Obj)
                        {
                            SaveIndexs(iList.Newindexname, iList.Newindexvalue, iList.NewInfexLeft, iList.NewInfexTop, iList.NewInfexWidth, iList.NewInfexHeight, document.Id, ClassificationID, iList.Newindexdisplayname);
                        }

                        try
                        {
                            CreateJson_AzureSearch(file.Name, document.Id);
                        }
                        catch (Exception exx) { }


                        try
                                {
                                    Log aln = new Log();
                                    aln.documentid = document.Id;
                                    aln.action = "Upload";
                                    aln.datetimecreated = DateTime.Now;
                                    aln.userid = 1;
                                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                               

                        if(logtype=="Supporting")
                        {
                            
                            Log alnS = new Log();
                            alnS.documentid = document.Id;
                            alnS.action = "Supporting";
                            alnS.datetimecreated = DateTime.Now;
                            alnS.userid = 1;
                            LogManagementcs.AddLog(tenantUserSession, alnS, out exception);
                        }

                        if (logtype == "Miscellaneous")
                        {
                            
                            Log alnM = new Log();
                                alnM.documentid = document.Id;
                                alnM.action = "Miscellaneous";
                                alnM.datetimecreated = DateTime.Now;
                                alnM.userid = 1;
                            LogManagementcs.AddLog(tenantUserSession, alnM, out exception);
                        }

                        }
                        catch (Exception exx) { }

                    }
                    catch (Exception ex)
                    {
                        document = null;
                        exception = ex;
                    }
                }
            }
            else
            {
                throw (new DocumentAlreadyExistsException(document, "A document with the same hash value already exists in the system."));
            }


            return (result);
        }




    }

}