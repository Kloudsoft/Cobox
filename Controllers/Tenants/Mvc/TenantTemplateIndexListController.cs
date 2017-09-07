using System;
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
//using Leadtools;
//using Leadtools.Pdf;
//using Leadtools.Codecs;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class LstIndexes
    {
        public string Newindexname { get; set; }
        public string Newindexvalue { get; set; }
        public int NewInfexLeft { get; set; }
        public int NewInfexTop { get; set; }
        public int NewInfexWidth { get; set; }
        public int NewInfexHeight { get; set; }

    }

    public class TenantTemplateIndexListController : Controller
    {

        public bool IsNewFolder = false;
        string DeliveryOrderNumber = "NoNumber";
        string CreateFolderName = "NoName";
        // GET: TanentTemplateIndexList
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


            return this.View("~/Views/Tenants/Templates/TenantTemplateIndexList.cshtml");
        }





        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {

                Exception exception = null;
                TenantUserSession tenantUserSession = null;

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                Template temp = null;

                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {


                        int maxWidth = 900;
                        int maxHeight = 1273;

                        try
                        {
                            var DirectorRoot = Path.Combine(Server.MapPath("~/UploadedFiles/TemplateIndexList/"));
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePathO = DirectorRoot + "Original/" + InputFileName;
                            Session["ServerSavePathO"] = "Original/" + InputFileName;
                            file.SaveAs(ServerSavePathO);


                            Image image = Image.FromFile(ServerSavePathO, true);
                            Bitmap bmp = new Bitmap(image);

                            Bitmap cropbmp = CropWhiteSpace(bmp);

                            //Image newImage = Save(bmp, maxWidth, maxHeight, 50, ServerSavePath);
                            Bitmap newbmp = Save(cropbmp, maxWidth, maxHeight);



                            var ServerSavePathR = DirectorRoot + "Resized/" + InputFileName;

                            Session["ServerSavePathR"] = "Resized/" + InputFileName;
                            Session["InputFileName"] = InputFileName;
                            newbmp.Save(ServerSavePathR);


                            byte[] imageByte = (byte[])(new ImageConverter()).ConvertTo(newbmp, typeof(byte[]));

                            this.ViewBag.ModelTemplateImageByteArray = imageByte;
                            Session["imgbyte"] = imageByte;
                            this.ViewBag.ModelTemplateImage = newbmp;
                            Session["tempimg"] = newbmp;

                            //newbmp.Dispose();


                        }
                        catch (Exception ex) { }


                        //template.TemplatePath = file.FileName; 
                    }
                }
            }
            catch (Exception ex) { }

            return this.View("~/Views/Tenants/Templates/TenantTemplateIndexList.cshtml");
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

        public async Task<ActionResult> ProcessOcr(HttpPostedFileBase file)
        {
            try
            {




                int ClassificationIDorTemplateIDorTagID = 0;
                string Indexx = "";
                StringBuilder stringBuilder = new StringBuilder();
                Exception exception = null;
                TenantUserSession tenantUserSession = null;

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                Template temp = null;

                if (Session["imgbyte"] != null)
                {


                    byte[] Templateimagebytearr = (byte[])Session["imgbyte"];

                    ImageConverter Imgconverter = new ImageConverter();
                    Image img = (Image)Imgconverter.ConvertFrom(Templateimagebytearr);
                    this.ViewBag.ModelTemplateImageByteArray = Templateimagebytearr;
                    Session["imgbyte"] = Templateimagebytearr;
                    this.ViewBag.ModelTemplateImage = img;
                    Session["tempimg"] = img;

                    // OCR The File
                    // Get SubscriptionKey

                    string SubscriptionKey = "5dc704a3098243dda51bb16d92c00d70";

                    //
                    // Create Project Oxford Vision API Service client
                    //
                    string apiroot = @"https://southeastasia.api.cognitive.microsoft.com/vision/v1.0/";
                    VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, apiroot);




                    MemoryStream imageFileStream = new MemoryStream(Templateimagebytearr, 0, Templateimagebytearr.Length);
                    //
                    // Upload an image and perform OCR
                    //
                    OcrResults ocrResult = await VisionServiceClient.RecognizeTextAsync(imageFileStream, "en");

                    var json = new JavaScriptSerializer().Serialize(ocrResult);



                    //-------------------DB PROCESS


                    try
                    {
                        if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                        List<TemplateElement> templateElement = null;

                        bool dbresult = ElementManagement.GetAllElements(tenantUserSession, out templateElement, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if ((templateElement != null) && dbresult)
                        {

                            Indexx += "";

                            int ijk = 1;

                            List<LstIndexes> ObjList = new List<LstIndexes>();

                            for (int i = 0; i < ocrResult.Regions.Length; i++)
                            {
                                for (int j = 0; j < ocrResult.Regions[i].Lines.Length; j++)
                                {
                                    for (int k = 0; k < ocrResult.Regions[i].Lines[j].Words.Length; k++)
                                    {

                                        float Jsonleft = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Left;
                                        float Jsontop = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Top;
                                        float Jsonwidth = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Width;
                                        float Jsonheight = ocrResult.Regions[i].Lines[j].Words[k].Rectangle.Height;


                                        foreach (var item in templateElement)
                                        {
                                            float IndexLeft = item.X;
                                            float IndexTop = item.Y;
                                            float IndexWidth = item.X2;
                                            float IndexHeight = item.Y2;


                                            if ((Jsonleft == IndexLeft) && Jsontop == IndexTop)
                                            {


                                                Indexx += "<div class='col-md-1'>";
                                                Indexx += "<span class='num-bg'>" + ijk + "</span>";
                                                Indexx += "</div>";
                                                Indexx += "<div class='col-md-11'>";
                                                Indexx += "<p>" + item.Name + "</p>";
                                                Indexx += "<p>•" + ocrResult.Regions[i].Lines[j].Words[k].Text.ToString() + " </p>";
                                                Indexx += "</div>";
                                                Indexx += "<div class='clearfix'> </div>";
                                                Indexx += "<hr size ='1'>";
                                                ijk++;

                                                try
                                                {
                                                    //Mactching Indexes
                                                    // SaveIndexs(item.Name, ocrResult.Regions[i].Lines[j].Words[k].Text.ToString(), (int)Jsonleft, (int)Jsontop, (int)Jsonwidth, (int)Jsonheight);

                                                    try
                                                    {
                                                        ObjList.Add(new LstIndexes
                                                        {
                                                            Newindexname = item.Name,
                                                            Newindexvalue = ocrResult.Regions[i].Lines[j].Words[k].Text.ToString(),
                                                            NewInfexLeft = (int)Jsonleft,
                                                            NewInfexTop = (int)Jsontop,
                                                            NewInfexWidth = (int)Jsonwidth,
                                                            NewInfexHeight = (int)Jsonheight
                                                        });
                                                    }
                                                    catch (Exception EEE) { }

                                                    if ((item.Name.ToUpper() == "VENDORNAME") || (item.Name.ToUpper() == "VENDOR NAME") || (item.Name.ToUpper() == "VENDOR") || (item.Name.ToUpper() == "NAME"))
                                                    {
                                                        CreateFolderName = ocrResult.Regions[i].Lines[j].Words[k].Text.ToString();
                                                    }
                                                    if ((item.Name.ToUpper().Contains("DO NUMBER")) || (item.Name.ToUpper().Contains("DELIVERY ORDER")) || (item.Name.ToUpper().Contains("DELIVERY ORDER NUMBER")) || (item.Name.ToUpper().Contains("DO NUM")))
                                                    {
                                                        DeliveryOrderNumber = ocrResult.Regions[i].Lines[j].Words[k].Text.ToString();
                                                    }
                                                }
                                                catch (Exception ex) { }

                                            }

                                        }

                                    }
                                }
                            }

                            // Custom Vision Test

                            string Resultt = CVTestProcess();
                            ViewBag.ProjectResult = Resultt;


                            //UploadDocuments
                            int Dtype = -1;
                            if (Session["Dtype"] != null)
                            {
                                Dtype = Convert.ToInt32(Session["Dtype"]);
                            }

                            long newFolderCreated = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);
                            
                            if (Dtype == 1) //Only if it DO
                            {
                                // Creating Folder
                                newFolderCreated = fnCreateFolder(CreateFolderName, Dtype, DeliveryOrderNumber);
                                long documentid = fnUploadDocs(newFolderCreated, Dtype, DeliveryOrderNumber, ObjList);
                            }
                            if (Dtype == 0)  // Invoice
                            {
                                //Create New version
                                long documentid = CheckoutDocument();
                                if ((documentid != 0) && (documentid != null))
                                    documentid = fnAppendDocs(documentid, Dtype, DeliveryOrderNumber, ObjList);
                                else
                                    Dtype = 2;

                            }
                            if ((Dtype == 0) || (Dtype == 1))  // Others
                            {

                            }
                            else
                            {
                                
                                newFolderCreated = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderSupporting"]);
                                var documentid = fnUploadDocsOthers(newFolderCreated, Dtype, DeliveryOrderNumber, ObjList);
                            }


                            //SaveIndexsNew(ObjList, documentid);

                        }
                        else
                        {
                            exception = new Exception("Unable to find the template Index");
                            throw exception;
                        }
                    }

                    catch (Exception ex)
                    {
                        exception = ex;
                        this.ViewBag.ErrorMessage = exception.Message;
                    }

                    Session["Indexs"] = Indexx;

                }
                else
                {
                    Exception ex = new Exception("Unable to find an image");
                    throw ex;
                }


            }
            catch (Exception e)
            {

            }

            return this.View("~/Views/Tenants/Templates/TenantTemplateIndexList.cshtml");




        }


        public long CheckoutDocument()
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Document document = null;
            long id = 0; ;
            // DocumentsViewModel documentViewModel = new DocumentsViewModel();
            bool result = false;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }

                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var FoldersWithSameName = context.ClassifiedFileIndexs.Where(x => ((x.indexvalue == DeliveryOrderNumber))).ToList();
                    if (FoldersWithSameName.Count() > 0)
                    {
                       var documentID = context.ClassifiedFileIndexs.Where(x => ((x.indexvalue == DeliveryOrderNumber))).Select(x => x.documentid).First();
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


        public long SaveIndexsNew(List<LstIndexes> Obj, long documentid)
        {
            long rtnValue = 0;
            foreach (LstIndexes iList in Obj)
            {
                SaveIndexs(iList.Newindexname, iList.Newindexvalue, iList.NewInfexLeft, iList.NewInfexTop, iList.NewInfexWidth, iList.NewInfexHeight, documentid,0);
            }

            return rtnValue;
        }

        public long fnCreateFolder(string folderName,int DocumentType,string DeliveryOrderNumber)
        {
            int rtnStatus = 0;
            long parentFolderId = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);
            long parentFolderId_Completed = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderCompleted"]);
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Folder newFolderCreated = null;
            Folder newFolderCreated_Completed = null;
            try
            {

                if (parentFolderId > 1)
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


                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        if (DocumentType == 1)
                        {
                            var FoldersWithSameName = context.Folders.Where(x => ((x.ParentId == folder.ParentId) && (x.Name == folderName))).ToList();
                            if (FoldersWithSameName.Count() > 0)
                            {
                                var newFolderCreatedIDDDD = context.Folders.Where(x => ((x.ParentId == folder.ParentId) && (x.Name == folderName))).Select(x => x.Id).First();
                                IsNewFolder = false;
                                return newFolderCreatedIDDDD;
                            }
                            else
                            {
                                IsNewFolder = true;
                                if (!FolderManagement.AddFolder(tenantUserSession, folder, out newFolderCreated, out exception)) { if (exception != null) { throw exception; } }

                                //For Completed
                                if (!FolderManagement.AddFolder(tenantUserSession, folder_Completed, out newFolderCreated_Completed, out exception)) { if (exception != null) { throw exception; } }

                                return newFolderCreated.Id;
                            }
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
                        var Pdfname = context.Documents.Where(x => ((x.Id == documentid))).Select(x => x.Namepdf).First();


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
                        FileNamePDF = ManagePDF(bmp, pathTenantDoc, Pdfname, pathTenantDoc, DeliveryOrderNumber + "_" + filenamenew, documentid);

                        document = context.Documents.Where(x => x.Id == documentid).FirstOrDefault();
                        document.Name = DeliveryOrderNumber + "_" + filenamenew + ".pdf";
                        context.SaveChanges();

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
                    bool result = CreateDocumentEntry(tenantUserSession, FilenameImg, FileNamePDF, imageByte.Length, folderId, ClassificationID, Obj);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }
        public long fnUploadDocsOthers(long folderId, int ClassificationID, string DeliveryOrderNumber, List<LstIndexes> Obj)
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
                bool result = CreateDocumentEntry(tenantUserSession, FilenameImg, "", imageByte.Length, folderId, ClassificationID, Obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }

        public string createPDF(Bitmap bmp, string Readpath, string OriginalFilename, string Savepath, string FileName, string extn)
        {
            //string license = "4AEA30F4DBB8171DE7CF1166516E03";
            string license = "XeJREBodo/8B5XUBbv2MatilIrQdPdtypqn67/2pRFUAjFi+KCb6N+owejUx5nQFBw==";
            XSettings.InstallTrialLicense(license);

            Doc theDoc = new Doc();
            theDoc.Rect.Inset(0, 0);
            string thePath = Readpath + OriginalFilename;
            theDoc.AddImageBitmap(bmp, true);
            bmp.Dispose();
            theDoc.Save(Savepath + FileName + extn);
            theDoc.Clear();
            return FileName + extn;

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

        public string ManagePDF(Bitmap bmp, string Readpath,string Pdfname, string Savepath, string FileName, long documentid)
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

                //License
                //string license = "4AEA30F4DBB8171DE7CF1166516E03";
                string license = "XeJREBodo/8B5XUBbv2MatilIrQdPdtypqn67/2pRFUAjFi+KCb6N+owejUx5nQFBw==";
               


                Doc theDoc = new Doc();

                theDoc.Read(Readpath + Pdfname);
                int theCount = theDoc.PageCount;
                theDoc.Rect.Inset(0, 0);
                theDoc.Page = theDoc.AddPage();
                theDoc.AddImageBitmap(bmp, true);
                bmp.Dispose();
                theDoc.Save(Savepath + FileName + ".pdf");
                theDoc.Clear();
                

            }
            catch (Exception ex) { }

            return FileName + ".pdf";

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
            sourceTemplateElement.userfilepathO = Session["ServerSavePathO"].ToString();
            sourceTemplateElement.userfilepathR = Session["ServerSavePathR"].ToString();
            sourceTemplateElement.documentid = documentid;


            dbresult = ElementManagement.AddFileIndexs(tenantUserSession, sourceTemplateElement, out exception);
            if (exception != null)
            {
                throw exception;
            }
            return dbresult;
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

                if(resultlist.Count()==0)
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

        private bool CreateDocumentEntry(TenantUserSession tenantUserSession, string filename, string filenamepdf, long size, long folderId, int ClassificationID, List<LstIndexes> Obj)
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