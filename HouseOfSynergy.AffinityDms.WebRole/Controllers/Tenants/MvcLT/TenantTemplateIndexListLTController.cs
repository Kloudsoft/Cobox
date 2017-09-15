﻿using System;
using System.Collections.Generic;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

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




namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.MvcLT
{
    public class LstIndexes
    {
        public long NewindexId { get; set; }
        public string Newindexname { get; set; }
        public string Newindexvalue { get; set; }
        public int NewInfexLeft { get; set; }
        public int NewInfexTop { get; set; }
        public int NewInfexWidth { get; set; }
        public int NewInfexHeight { get; set; }


    }

    public class LstAzureSearch
    {
        public string fieldname { get; set; }
        public string fieldvalue { get; set; }
        
    }




    public class TenantTemplateIndexListLTController : Controller
    {

        private static string TargetSearchServiceName = ConfigurationManager.AppSettings["TargetSearchServiceName"];
        private static string TargetSearchServiceApiKey = ConfigurationManager.AppSettings["TargetSearchServiceApiKey"];
        private static HttpClient HttpClient;
        private static Uri ServiceUri;

        public string PublicIndexName;
        public string PublicIndexValue;
        public bool IsNewFolder = false;
        string DeliveryOrderNumber = "NoDoNumber";
        string InvoiceNumber = "NoInvoiceNumber";
        string AccountNumber = "NoAccountNumber";
        string BillNumber = "NoBillNumber";
        string CreateFolderName = "NoName";
        int TotalZipCount = 0;
        int TotalFileCount = 0;
        string FileType = "";
        public string OCR_Search = "";

        List<LstIndexes> ObjList = new List<LstIndexes>();
        List<LstIndexes> OutObjList = new List<LstIndexes>();

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
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            var pathTenantDoc = "";
            var InputFileName = "";
            var Fileextn = ""; ;
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
                        //if (file.ContentLength > 2100000)    // Exactly 2097152 bytes  = 2MB
                        //{

                        //}


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

                                    await ProcessOcr(filenamenew + Fileextn);
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
                                        await ProcessOcr(zaItem.FullName);

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

            return this.View("~/Views/Tenants/Templates/TenantTemplateIndexListLT.cshtml");
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

        public async Task<ActionResult> ProcessOcr(string file)
        {
             DeliveryOrderNumber = "NoDoNumber";
             InvoiceNumber = "NoInvoiceNumber";
             AccountNumber = "NoAccountNumber";
             BillNumber = "NoBillNumber";
              PublicIndexName="";
            PublicIndexValue = ""; 

        ViewBag.OcrProcessed = "YES";
            string OCRText = "";
            List<Vendor> vendor = new List<Vendor>();
            try
            {


            
                int ClassificationIDorTemplateIDorTagID = 0;
                string Indexx = "";
                StringBuilder stringBuilder = new StringBuilder();
                Exception exception = null;
                TenantUserSession tenantUserSession = null;

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                Template temp = null;

                if (Session["OcrFileNames"] != null)
                {


                    //byte[] Templateimagebytearr = (byte[])Session["imgbyte"];

                    //ImageConverter Imgconverter = new ImageConverter();
                    //Image img = (Image)Imgconverter.ConvertFrom(Templateimagebytearr);
                    //this.ViewBag.ModelTemplateImageByteArray = Templateimagebytearr;
                    //Session["imgbyte"] = Templateimagebytearr;
                    //this.ViewBag.ModelTemplateImage = img;
                    //Session["tempimg"] = img;

                    // OCR The File
                    // Get SubscriptionKey

                    string SubscriptionKey = "5dc704a3098243dda51bb16d92c00d70";

                    //
                    // Create Project Oxford Vision API Service client
                    //
                    string apiroot = @"https://southeastasia.api.cognitive.microsoft.com/vision/v1.0/";
                    VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, apiroot);

                    //string imageFilePath = Path.Combine(Server.MapPath("~/UploadedFiles/TemplateIndexList/")) + Session["ServerSavePathO"].ToString();



                    List<string> OcrFileNames = new List<string>();
                    //OcrFileNames = (List<string>)TempData["OcrFileNames"];
                    OcrFileNames = (List<string>)Session["OcrFileNames"];
                    

                    //var pathTenantDoc = Path.Combine(Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/")) + Session["ServerSavePathO"].ToString();
                    string pathTenantDoc = "";

                    if (OcrFileNames.Count() > 0)
                    {
                      //   step2:
                        for (int Ipage = 0; Ipage < OcrFileNames.Count(); Ipage++)
                        {
                            pathTenantDoc = Path.Combine(Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/")) + OcrFileNames[Ipage].ToString();

                            using (Stream imageFileStream = System.IO.File.OpenRead(pathTenantDoc))
                            {
                                //
                                // Upload an image and perform OCR
                                //
                                OcrResults ocrResult = await VisionServiceClient.RecognizeTextAsync(imageFileStream, "en");
                                OCRText += RearrangeOCR(ocrResult);

                               
                            //    OCRText += LogOcrResults(ocrResult);

                                //try
                                //{
                                //    RearrangeOCR(ocrResult);

                                //}
                                //catch (Exception ecc) { }

                                //Response.End();

                                //try
                                //{
                                //    Log aln = new Log();
                                //    aln.documentid = 0; //document.Id;
                                //    aln.action = "OCR-" + OCRText.ToString().Substring(1,500);
                                //    aln.datetimecreated = DateTime.Now;
                                //    aln.userid = tenantUserSession.User.Id;
                                //    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                                //}
                                //catch (Exception exx) { }

                            }
                        }

                        //Get VendorNamees to List


                        if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }



                        bool dbresult = VendorManagement.GetAllVendors(tenantUserSession, out vendor, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if ((vendor != null) && dbresult)
                        {
                        }



                        try
                        {

                            bool Ismatched = false;
                            bool Isnewline = false;
                            bool IsvendorExist = false;
                            string currentline = "";
                            string previousline = "";
                            bool IsIndexSddedAlready = false;

                            try
                            {
                                OCR_Search = OCRText;
                                OCRText = OCRText.Replace("\r\n\r\n", "\r\n");
                                
                                using (var reader = new StringReader(OCRText))
                                {
                                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine().Replace(" ", ""))  //.Replace(" ","")
                                    {
                                        if (line.ToUpper().Contains("ACCOUNTNO"))
                                        {
                                            string test = "OK";
                                        }
                                        if (line.ToUpper().Contains("INVNO"))
                                        {
                                            string test = "OK";
                                        }

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
                                                        ObjList.Add(new LstIndexes
                                                        {
                                                            Newindexname = PublicIndexName.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            Newindexvalue = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            NewInfexLeft = 0,
                                                            NewInfexTop = 0,
                                                            NewInfexWidth = 0,
                                                            NewInfexHeight = 0
                                                        });
                                                    }

                                                }

                                                if (PublicIndexName.ToUpper().Contains("D/ONO"))
                                                    {
                                                        DeliveryOrderNumber = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                if (PublicIndexName.ToUpper().Contains("DELIVERYORDERNO"))
                                                {
                                                    DeliveryOrderNumber = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                }
                                                if (PublicIndexName.ToUpper().Contains("INVNO"))
                                                    {
                                                        InvoiceNumber = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("INVOICENO"))
                                                    {
                                                        InvoiceNumber = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("ACCOUNTNO"))
                                                    {
                                                        AccountNumber = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("BILL-ID") || PublicIndexName.ToUpper().Contains("BILL-10"))
                                                    {
                                                        BillNumber = PublicIndexValue.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
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
                                                        ObjList.Add(new LstIndexes
                                                        {
                                                            Newindexname = PublicIndexName.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            Newindexvalue = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':'),
                                                            NewInfexLeft = 0,
                                                            NewInfexTop = 0,
                                                            NewInfexWidth = 0,
                                                            NewInfexHeight = 0
                                                        });
                                                    }

                                                    if (PublicIndexName.ToUpper().Contains("D/ONO"))
                                                    {
                                                        DeliveryOrderNumber = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("DELIVERYORDERNO"))
                                                    {
                                                        DeliveryOrderNumber = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("INVNO"))
                                                    {
                                                        InvoiceNumber = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("INVOICENO"))
                                                    {
                                                        InvoiceNumber = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("ACCOUNTNO"))
                                                    {
                                                        AccountNumber = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                    if (PublicIndexName.ToUpper().Contains("BILL-ID") || PublicIndexName.ToUpper().Contains("BILL-10"))
                                                    {
                                                        BillNumber = currentline.TrimStart('•').TrimStart('.').TrimStart(':').TrimEnd('•').TrimEnd('.').TrimEnd(':');
                                                    }
                                                }
                                            }
                                            catch (Exception e) { }
                                        }


                                        //if (currentline.ToUpper().Contains("INVOICENO") || currentline.ToUpper().Contains("INVOICE NO") || currentline.ToUpper().Contains("INVOICE NO.") || currentline.ToUpper().Contains("INVNO") || currentline.ToUpper().Contains("INV NO") || currentline.ToUpper().Contains("INV NO."))
                                        //{
                                        //    Isnewline =fncheckIsNewline(currentline);
                                        //    Ismatched = true;
                                        //}

                                        try
                                        {

                                            if (currentline.ToUpper().Contains("ACCOUNTNO") || currentline.ToUpper().Contains("BILL-ID") || currentline.ToUpper().Contains("BILL-10") || currentline.ToUpper().Contains("D/ONO") || currentline.ToUpper().Contains("DELIVERYORDERNO") || currentline.ToUpper().Contains("INVNO") || currentline.ToUpper().Contains("INVOICENO"))
                                            {
                                                Isnewline = fncheckIsNewline(currentline);
                                                Ismatched = true;

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

                            bool NewResult = Checkdocumentindexmatching(AccountNumber,BillNumber,DeliveryOrderNumber, InvoiceNumber, ObjList);
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
                                    var documentid = fnUploadDocsOthers(newFolderCreated, Dtype, NewGuidName, ObjList, CreateFolderName, "Supporting");
                                }
                                else
                                {
                                    long FolderMiscellaneous = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderMiscellaneous"]);
                                    var documentid = fnUploadDocsOthers(FolderMiscellaneous, Dtype, NewGuidName, ObjList, CreateFolderName, "Miscellaneous");
                                }


                            }

                            if (NewResult == true)  // Merge
                            {
                                //Create New version
                                Dtype = 1;
                                long documentid = CheckoutDocument();

                                try
                                {
                                    Log aln = new Log();
                                    aln.documentid = documentid;
                                    aln.action = "Upload";
                                    aln.datetimecreated = DateTime.Now;
                                    aln.userid = tenantUserSession.User.Id;
                                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                                }
                                catch (Exception exx) { }


                                if ((documentid != 0) && (documentid != null))
                                {
                                    documentid = fnAppendDocs(documentid, Dtype, NewGuidName, ObjList);
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }

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

        public string RearrangeOCR(OcrResults ocrResult)
        {
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


                        ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Left=510;

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
                    //OCR_Text += "\r\n";

                    //ObjListArray.Add(new LstIndexesArray
                    //{
                    //    Ocrline = "\r\n",
                    //    NewInfexLeft = 0,
                    //    NewInfexTop = 0,
                    //    NewInfexWidth = 0,
                    //    NewInfexHeight = 0
                    //});
                }
                
            }
            var sortedList = ObjListArray.OrderBy(x => x.groupwords).ThenBy(x => x.NewInfexLeft).ThenBy(x => x.NewInfexTop).ToList();
            string result = "";

            StringBuilder builder = new StringBuilder();
            int previouslineorder_no = 1;
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

            //for blocks - repeat task

            //sortedList = ObjListArray.OrderBy(x => x.NewInfexTop).ThenBy(x => x.NewInfexLeft).ThenBy(x => x.groupwords).ToList();
            //previouslineorder_no = 1;
            //foreach (LstIndexesArray words in sortedList) // Loop through all strings
            //{
            //    if (previouslineorder_no == words.groupwords)
            //    {
            //        builder.Append(words.Ocrline).Append(" "); // Append string to StringBuilder
            //    }
            //    else
            //    {
            //        builder.Append("\r\n").Append(words.Ocrline); // Append string to StringBuilder
            //    }

            //    previouslineorder_no = words.groupwords;
            //}
            //result = builder.ToString(); // Get string from StringBuilder




            return result;

        }


        public void CreateJson_AzureSearch(string filenamepdf)
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

                    int count = Directory.GetFiles(Server.MapPath("~/Schema_and_Data/"), "cobox" + "*.json").Count();
                    count = count + 1;
                    string path = Server.MapPath("~/Schema_and_Data/");
                    System.IO.File.WriteAllText(path + "cobox" + count + ".json", jsonData);

                    ServiceUri = new Uri("https://" + TargetSearchServiceName + ".search.windows.net");
                    HttpClient = new HttpClient();
                    HttpClient.DefaultRequestHeaders.Add("api-key", TargetSearchServiceApiKey);

                    string fileName = Server.MapPath("~/Schema_and_Data/") + "cobox" + count + ".json";

                    Console.WriteLine("Uploading documents from file {0}", fileName);
                    string jsonupload = System.IO.File.ReadAllText(fileName);
                    Uri uri = new Uri(ServiceUri, "/indexes/" + "cobox" + "/docs/index");
                    HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(HttpClient, HttpMethod.Post, uri, jsonupload);
                    response.EnsureSuccessStatusCode();

                try
                {
                    Exception exception = null;
                    TenantUserSession tenantUserSession = null;

                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                    Log aln = new Log();
                    aln.documentid = 1010;
                    aln.action = "AZURE SEARCH JSON" + response.StatusCode + response.ReasonPhrase;
                    aln.datetimecreated = DateTime.Now;
                    aln.userid = tenantUserSession.User.Id;
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



                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }

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

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }


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
                SaveIndexs(iList.Newindexname, iList.Newindexvalue, iList.NewInfexLeft, iList.NewInfexTop, iList.NewInfexWidth, iList.NewInfexHeight, documentid, 0);
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
            Folder newFolderCreated = null;
            Folder newFolderCreated_Completed = null;
            Folder newFolderCreated_Supporting = null;
            try
            {

                if (parentFolderId_Supporting > 1)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
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

        public long fnAppendDocs(long documentid, int ClassificationID, string DeliveryOrderNumber, List<LstIndexes> Obj)
        {
            Document document = null;

            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var CountDocs = context.Documents.Where(x => ((x.Id == documentid))).ToList();
                    if (CountDocs.Count() > 0)
                    {
                        var Pdfname = context.Documents.Where(x => ((x.Id == documentid))).Select(x => x.Name).First();

                        var pathTenantDoc = Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/");

                        string filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                        string FileNamePDF = "";
                        
                        //Call PDF conversion and Append DO of the DO number
                        
                        FileNamePDF = ManagePDF(null, pathTenantDoc, Pdfname, pathTenantDoc, filenamenew + ".pdf", documentid);

                        //Create Json for azure search

                        try
                        {
                            CreateJson_AzureSearch(FileNamePDF);
                        }
                        catch (Exception exx) { }


                        try
                        {
                            Log aln = new Log();
                            aln.documentid = documentid;
                            aln.action = "Merge";
                            aln.datetimecreated = DateTime.Now;
                            aln.userid = tenantUserSession.User.Id;
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
                                    aln.userid = tenantUserSession.User.Id;
                                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                                }
                                catch (Exception exx) { }

                            }
                        }
                        catch (Exception exx) { }

                        foreach (LstIndexes iList in Obj)
                        {
                            SaveIndexs(iList.Newindexname, iList.Newindexvalue, iList.NewInfexLeft, iList.NewInfexTop, iList.NewInfexWidth, iList.NewInfexHeight, document.Id, ClassificationID);
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
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

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
        public long fnUploadDocsOthers(long folderId, int ClassificationID, string DeliveryOrderNumber, List<LstIndexes> Obj, string CreateFolderName,string logtype)
        {
            Document document = null;

            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                    if (exception != null)
                    throw exception;

                //var fileName = Session["InputFileName"].ToString();
                var FileNamePDF = Session["ServerSavePathOPDF"].ToString();
                
                //var DirectorRoot = Path.Combine(Server.MapPath("~/UploadedFiles/TemplateIndexList/"));
                //var ServerSavePathR = DirectorRoot + "Resized/";
                //Image image = Image.FromFile(ServerSavePathR + fileName, true);
                //Bitmap bmp = new Bitmap(image);
                //byte[] imageByte = (byte[])(new ImageConverter()).ConvertTo(bmp, typeof(byte[]));

                var pathTenantDoc = Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/");
                if (!Directory.Exists(pathTenantDoc)) { Directory.CreateDirectory(pathTenantDoc); }

                //string fileExty = Path.GetExtension(fileName);

               // string filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

               //// string Documenttype = Session["Dtype"].ToString();
               // string FileNamePDF = "";
                //string FilenameImg = DeliveryOrderNumber + "_" + filenamenew + fileExty;

                //Call PDF conversion and Append DO of the DO number
              //  var physicalPath = Path.Combine(pathTenantDoc, DeliveryOrderNumber + "_" + filenamenew + fileExty);
                //bmp.Save(physicalPath);
                //FileNamePDF = createPDF(bmp, ServerSavePathR, fileName, pathTenantDoc, DeliveryOrderNumber + "_" + filenamenew, ".pdf");
                bool result = CreateDocumentEntry(tenantUserSession, FileNamePDF, FileNamePDF, 100, folderId, ClassificationID, Obj,logtype, CreateFolderName);
                try
                {
                    CreateJson_AzureSearch(FileNamePDF);
                }
                catch (Exception exx) { }



                //imageByte.Length = 0;


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

        public string ManagePDF(Bitmap bmp, string pathTenantDoc, string Pdfname, string pathTenantDoc1, string FileName, long documentid)
        {
            Document document = null;
            string PDFFilenametoAppend = "";
            TenantUserSession tenantUserSession = null;
            Exception exception = null;

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

                if (exception != null)
                    throw exception;


                try
                {
                    //RasterSupport.SetLicense(@"D:\DEVMVC\LEADTOOLSSAMPLES\LEADTOOLSSAMPLES\App_Data\LeadTools\License\eval-license-files.lic", System.IO.File.ReadAllText(@"D:\DEVMVC\LEADTOOLSSAMPLES\LEADTOOLSSAMPLES\App_Data\LeadTools\License\eval-license-files.lic.key"));
                    //RasterSupport.SetLicense(@"D:\DEVMVC\TFS-Current\Github Cobox Prototype\HouseOfSynergy.AffinityDms.WebRole\App_Data\LeadToolsV2\License\Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic", System.IO.File.ReadAllText(@"D:\DEVMVC\TFS-Current\Github Cobox Prototype\HouseOfSynergy.AffinityDms.WebRole\App_Data\LeadToolsV2\License\Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key"));

                    string strLIC = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic";
                    string strLICKey = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key";

                    RasterSupport.SetLicense(strLIC, System.IO.File.ReadAllText(strLICKey));

                    RasterCodecs _codecs = new RasterCodecs();
                    //_codecs.Options.Pdf.InitialPath = @"C:\Temp\PDFEngine";
                    _codecs.Options.Pdf.InitialPath = Server.MapPath("~/PDFEngine/");
                    
                    if (_codecs.Options.Pdf.IsEngineInstalled)
                    {
                        PDFFile firstFile = new PDFFile(pathTenantDoc + Pdfname);


                        //try
                        //{
                        //    Log aln = new Log();
                        //    aln.documentid = documentid; //document.Id;
                        //    aln.action = "Source Merge-" + pathTenantDoc + Pdfname;
                        //    aln.datetimecreated = DateTime.Now;
                        //    aln.userid = tenantUserSession.User.Id;
                        //    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                        //}
                        //catch (Exception exx) { }


                        firstFile.MergeWith(new string[] { pathTenantDoc + Session["ServerSavePathOPDF"] }, pathTenantDoc + FileName);

                        //try
                        //{
                        //    Log aln = new Log();
                        //    aln.documentid = documentid; //document.Id;
                        //    aln.action = "Merge with-" + pathTenantDoc + Session["ServerSavePathOPDF"];
                        //    aln.datetimecreated = DateTime.Now;
                        //    aln.userid = tenantUserSession.User.Id;
                        //    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                        //}
                        //catch (Exception exx) { }

                        //try
                        //{
                        //    Log aln = new Log();
                        //    aln.documentid = documentid; //document.Id;
                        //    aln.action = "Dest-Merge-" + pathTenantDoc + FileName;
                        //    aln.datetimecreated = DateTime.Now;
                        //    aln.userid = tenantUserSession.User.Id;
                        //    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                        //}
                        //catch (Exception exx) { }

                    }

                    // Load the input PDF document


                    //try
                    //{
                    //    Log aln = new Log();
                    //    aln.documentid = documentid; //document.Id;
                    //    aln.action = "Merge";
                    //    aln.datetimecreated = DateTime.Now;
                    //    aln.userid = tenantUserSession.User.Id;
                    //    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                    //}
                    //catch (Exception exx) { }
                }
                catch (Exception exx) { }



                //License
                //string license = "4AEA30F4DBB8171DE7CF1166516E03";
                //string license = "XeJREBodo/8B5XUBbv2MatilIrQdPdtypqn67/2pRFUAjFi+KCb6N+owejUx5nQFBw==";
                //XSettings.InstallTrialLicense(license);


                //Doc theDoc = new Doc();

                //theDoc.Read(Readpath + Pdfname);
                //int theCount = theDoc.PageCount;
                //theDoc.Rect.Inset(0, 0);
                //theDoc.Page = theDoc.AddPage();
                //theDoc.AddImageBitmap(bmp, true);
                //bmp.Dispose();
                //theDoc.Save(Savepath + FileName + ".pdf");
                //theDoc.Clear();


            }
            catch (Exception ex) { }

            return FileName;
        }


        public bool SaveIndexs(string desc, string val, int x, int y, int w, int h, long documentid, int classification)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            ClassifiedFileIndexs sourceTemplateElement = new ClassifiedFileIndexs();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }


            bool dbresult = false;
            sourceTemplateElement.userid = tenantUserSession.Tenant.Id;
            sourceTemplateElement.userdomain = tenantUserSession.Tenant.Domain;
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
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

               
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
                        document.CheckedOutByUserId = tenantUserSession.User.Id;
                        document.VersionCount = 0;
                        document.VersionMajor = 1;
                        document.VersionMinor = 0;
                        document.UserId = tenantUserSession.User.Id;
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

                        document.FileNameServer = document.Id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                        context.SaveChanges();

                        //User Documents

                        var userDocument = new UserDocument() { UserId = tenantUserSession.User.Id, DocumentId = document.DocumentOriginalId, IsActive = true };
                        context.UserDocuments.Add(userDocument);
                        context.SaveChanges();

                        
                    
                        ///Save Indexes
                        ///
                        foreach (LstIndexes iList in Obj)
                        {
                            SaveIndexs(iList.Newindexname, iList.Newindexvalue, iList.NewInfexLeft, iList.NewInfexTop, iList.NewInfexWidth, iList.NewInfexHeight, document.Id, ClassificationID);

                           

                        }

                    
                         try
                                {
                                    Log aln = new Log();
                                    aln.documentid = document.Id;
                                    aln.action = "Upload";
                                    aln.datetimecreated = DateTime.Now;
                                    aln.userid = tenantUserSession.User.Id;
                                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                               

                        if(logtype=="Supporting")
                        {
                            
                            Log alnS = new Log();
                            alnS.documentid = document.Id;
                            alnS.action = "Supporting";
                            alnS.datetimecreated = DateTime.Now;
                            alnS.userid = tenantUserSession.User.Id;
                            LogManagementcs.AddLog(tenantUserSession, alnS, out exception);
                        }

                        if (logtype == "Miscellaneous")
                        {
                            
                            Log alnM = new Log();
                                alnM.documentid = document.Id;
                                alnM.action = "Miscellaneous";
                                alnM.datetimecreated = DateTime.Now;
                                alnM.userid = tenantUserSession.User.Id;
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