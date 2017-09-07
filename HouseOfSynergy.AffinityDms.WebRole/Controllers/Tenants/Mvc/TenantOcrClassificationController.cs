using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Desktop;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    // Replace VintaSoft with LT.
    // Add post filter signatures and calls at the point after LT processing.
    // There may be forms without barcodes. We need to come up with a strategy and conditioning for that.
    public class TenantOcrClassificationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Tenants/OcrClassifications/OcrClassificationManually.cshtml");
        }
        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files)
        {



            // Structure:
            try
            {
                // Get file.

                try
                {
                    // CreateDocumentEntry.

                    try
                    {
                        // Upload to Azure.

                        try
                        {
                            // Pre Filter.
                            // Classification.
                            // Post Filters.

                            // Mark document as Unmatched/Matched/MatchedMultiple.

                            // Redirect to /Tenants/Documents.
                        }
                        catch (Exception e)
                        {
                            // TODO: Log.
                        }
                        finally
                        {
                            // Cleanup.
                        }
                    }
                    catch (Exception e)
                    {
                        // TODO: Log.
                    }
                    finally
                    {
                        // Cleanup.
                    }
                }
                catch (Exception e)
                {
                    // TODO: Log.
                }
                finally
                {
                    // Cleanup.
                }
            }
            catch (Exception e)
            {
                // TODO: Log.
            }
            finally
            {
                // Cleanup.
            }


             

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Document document = null;
            if (!TenantAuthenticationHelper.ValidateToken(this.Request,SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.FormsAdd, TenantActionType.FormsEdit, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }
            VintasoftBarcodeInfo[] barcodeinfos = null;
            List<OcrResultInfo> ocrresultinfos = new List<OcrResultInfo>();
            bool result = false;
            try
            {
                foreach (var file in files)
                {
                    if (file.ContentLength > 0)
                    {

                        
                        //document.FolderId = 1;
                        //document.UserId = 1;
                        //document.Length = file.ContentLength;
                        //document.Hash = "/wKqpA27W5lQbQ5g+5F8ZWqCvpZylC4/tDtTq17hyJE8LEGQOc48oexzqMzmLCvP9Nb/xx1LVkLvLINXhWJASQ==";
                        //document.Uploaded = true;
                        //document.Name = file.FileName;
                        //document.IsPrivate = true;
                        //document.SourceType = SourceType.BrowserFileSystem;
                        //document.IsDigital = false;
                        //document.FileNameClient = file.FileName;
                        //document.FileNameServer = file.FileName;
                        //document.ScannerSessionId = 0;
                        //document.IsCancelled = false;
                        //document.IsInTransit = true;
                        //document.DocumentType = DocumentType.Digital;
                        //document.State = DocumentState.None;
                        //document.DateTime = DateTime.Now;
                        Folder folder = new Folder();
                        result = DocumentManagement.CreateDocumentEntry(tenantUserSession, file.FileName, DateTime.UtcNow.ToString(), file.ContentLength, null, null, tenantUserSession.User, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        string returnedresult = "";
                        //string barcodetext = "";
                        Stream s = file.InputStream;
                        MemoryStream ms = new MemoryStream();
                        s.CopyTo(ms);
                        byte[] imgbytes = ms.ToArray();
                        Image bitmapimg = new Bitmap(s);
                        FileInfo f = new FileInfo(file.FileName);
                        
                        string filepath = document.Id+f.Extension.ToString();
                        string dirpath = Server.MapPath("/Images/");//"App_data/Tenants/Documents/");
                        if (!(Directory.Exists(dirpath)))
                        {
                            Directory.CreateDirectory(dirpath);
                        }
                        FileStream fs = new FileStream((dirpath + filepath), FileMode.CreateNew);
                        fs.Write(imgbytes,0,imgbytes.Length);
                        fs.Close();
                        fs.Dispose();
                        
                        VintasoftBarcode barcode = new VintasoftBarcode();
                        barcodeinfos = barcode.ReadBarcodeFromImage(bitmapimg);


                        #region Azure upload.
                        {
                            long? folderId = null;
                            Tenant tenantObject = null;
                            User userObject = null;
                            Document documentObject = null;
                            DocumentEntry documentEntryObject = null;

                            var fileInfo = new FileInfo(dirpath + filepath); // Get server filename.

                            documentEntryObject = new DocumentEntry(fileInfo);
                            using (var algorithm = HashAlgorithm.Create(GlobalConstants.AlgorithmHashName)) { documentEntryObject.CalculateHash(algorithm); }

                            {
                                // Upload to blob storage.
                                var connectionString = tenantObject.StorageConnectionStringPrimary;
                                var account = CloudStorageAccount.Parse(connectionString);
                                var client = account.CreateCloudBlobClient();
                                var container = client.GetContainerReference(tenantObject.UrlResourceGroup);

                                container.CreateIfNotExists();
                                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

                                var blob = container.GetBlockBlobReference(documentObject.FileNameServer);

                                using (var fileStream = System.IO.File.Open(documentEntryObject.Filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    blob.UploadFromStream(fileStream);
                                }
                            }

                            if (!DocumentManagement.DocumentEntryFinalize(tenantUserSession, documentObject.Id, out documentObject, out exception)) { throw (exception); }
                        }
                        #endregion Azure upload.

                        string serverpath = Server.MapPath("~");
                        if (barcodeinfos != null)
                        {

                            //if not null then perform ocr on form designs
                            var confidence = barcodeinfos[0].Confidence;
                            long resultId = 4;
                            Template template = null;
                            bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, resultId, out template, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                            if ((template != null) && dbresult)
                            {
                                if (template.TemplateType == TemplateType.Form)
                                {

                                    LeadToolsOCR leadtoolsocr = new LeadToolsOCR(serverpath,tenantUserSession.Tenant.Id.ToString(),string.Empty,out exception);
									if (exception != null) { throw exception; }
									double _computeddifference = 0.0;
                                    if ((bitmapimg.Width <= bitmapimg.Height) && (!(bitmapimg.Height <= 0)))
                                    {
                                        _computeddifference = ((Convert.ToDouble(bitmapimg.Width)) / (Convert.ToDouble(bitmapimg.Height)));
                                    }
                                    else
                                    {
                                        if (!(bitmapimg.Width <= 0))
                                        {
                                            _computeddifference = bitmapimg.Height / bitmapimg.Width;
                                        }

                                    }
                                    List<TemplateElement> elements = null;
                                    dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, resultId, out elements, out exception);
                                    if (exception != null)
                                    {
                                        throw exception;
                                    }
                                    List<ComputeCoordinates> cordlistlist = new List<ComputeCoordinates>();
                                    //foreach (var element in elements)
                                    //{                                                      
                                    //    ComputeCoordinates computecord = new ComputeCoordinates();
                                    //    computecord.ComputeEleemnts(element, _computeddifference);
                                    //    cordlistlist.Add(computecord);
                                    //    string result = leadtoolsocr.GetZoneText(computecord.ComputedX, computecord.ComputedY, computecord.ComputedWidth, computecord.ComputedHeight, bitmapimg);
                                    //}

                                    cordlistlist = leadtoolsocr.GetAllZoneData(elements, _computeddifference, bitmapimg);

                                    foreach (ComputeCoordinates cordata in cordlistlist)
                                    {
                                        returnedresult += cordata.Text;
                                    }
                                    result = true;
                                }
                            }
                            //else
                            //{
                            //    LeadToolsOCR leadtoolsocr = new LeadToolsOCR(serverpath);
                            //    returnedresult = leadtoolsocr.DoTemplateMatching(bitmapimg, file.FileName);
                            //}
                        }
                        else
                        {
                            {
                                //OCRClassification ocrclassification = new OCRClassification();
                                //Document document = null;
                                long documentId = document.Id;//use as tamporay value
                                //BusinessLayer.DocumentManagement.GetDocumentById(documentId, out document, out exception);
                                //Enties.Document documentt = new Enties.Document();
                                // ocrclassification.PerformOCR(bitmapimg, document, out documentt, out exception);
                                OcrResultInfo ocrresultinfo = null;
                                result = AutoOCR(tenantUserSession, bitmapimg, documentId,out ocrresultinfo, out exception);
                                if (exception != null)
                                {
                                    throw exception;
                                }
                                if (result)
                                {
                                    ocrresultinfo.Document = document;
                                    ocrresultinfo.ImagePath = "/Images/" + filepath;
                                    ocrresultinfos.Add(ocrresultinfo);
                                    returnedresult = "Successfull";
                                }
                                //LeadToolsOCR leadtoolsocr = new LeadToolsOCR();
                                // string result = leadtoolsocr.DoTemplateMatching(bitmapimg, file.FileName);
                                // ViewBag.Data = result;
                                // return View("~/Views/OCR/DisplayOCRResult.cshtml", null, (string)result);
                                // //if null then perform ocr on Template designs
                            }
                        }


                        //Image bitmapimage = new Bitmap(new MemoryStream(xByte));
                        //BinaryReader rdr = new BinaryReader(file.InputStream);
                        //imageByte = rdr.ReadBytes((int)file.ContentLength);
                        //template.TemplateImage = imageByte;
                        //return View();
                    }
                }
                foreach (var ocrresultinfo in ocrresultinfos)
                {
                    int count = 0;
                    if (ocrresultinfo.MatchedTemplates != null)
                    {
                        foreach (var MatchedTemplate in ocrresultinfo.MatchedTemplates)
                        {
                            count++;
                            Template template = null;
                            bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, (long)MatchedTemplate.TemplateId, out template, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                            if (dbresult)
                            {
                                MatchedTemplate.TemplateName = template.Title;
                            }
                        }
                    }
                    ocrresultinfo.MatchedTemplatesCount = count;
                }
                TempData["OcrResultInfos"] = ocrresultinfos;
                return RedirectToAction("Index", "TenantOcrClassifications", new { ErrorMessage = string.Empty, SuccessMessage = string.Empty });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            ViewBag.ErrorMessage = exception.Message;
            return View("~/Views/Tenants/OcrClassifications/OcrClassificationManually.cshtml");
            //if (exception != null)
            //{
            //    return View();
            //}
            //return View();
        }
        public bool AutoOCR(TenantUserSession tenantUserSession, Image image, long documentid, out Document document, out Exception exception)
        {
            bool result = false;
            exception = null;
            document = null;
            try
            {
                OcrClassification ocrclassification = new OcrClassification();
                Document entiesdocument = new Document();
                //long documentId = documentid;//use as tamporay value
                result = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out entiesdocument, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (result)
                {
                    result = ocrclassification.PerformOCR(tenantUserSession,image, entiesdocument, out document, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                return result;
            }

        }
        public bool AutoOCR(TenantUserSession tenantUserSession, Image image, long documentid, out OcrResultInfo ocrresultinfo, out Exception exception)
        {
            bool result = false;
            exception = null;
            ocrresultinfo = null;
            try
            {
                OcrClassification ocrclassification = new OcrClassification();
                Document entiesdocument = new Document();
                //long documentId = documentid;//use as tamporay value
                result = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out entiesdocument, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (result)
                {
                    result = ocrclassification.PerformOCR(tenantUserSession,image,entiesdocument, out ocrresultinfo, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                return result;
            }

        }
        public JsonResult CallOCR(long documentid)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            Document document = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request,SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.FormsAdd, TenantActionType.FormsEdit, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }

                OcrClassification ocrclassification = new OcrClassification();
                Document entiesdocument = null;
                //long documentId = documentid;//use as tamporay value
                result = DocumentManagement.GetDocumentById(tenantUserSession,documentid, out entiesdocument, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (result && (entiesdocument != null))
                {
                    string DocumentsDirectory = Server.MapPath("../App_Data/Tenants/Documents/");
                    DirectoryInfo directoryinfo = new DirectoryInfo(DocumentsDirectory);
                    FileInfo[] files = directoryinfo.GetFiles(documentid.ToString() + ".*");
                    if (files.Count() == 1)
                    {
                        string filepath = Path.Combine(files[0].DirectoryName, files[0].FullName);
                        Stream stream = new FileStream(Path.Combine(files[0].DirectoryName, files[0].FullName), FileMode.OpenOrCreate);
                        Image image = new Bitmap(stream);
                        stream.Close();
                        stream.Dispose();
                        //Directory.GetFiles()
                        result = ocrclassification.PerformOCR(tenantUserSession, image, entiesdocument, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                    }
                    else
                    {
                        result = false;
                        return Json("Document Image not found for performing OCR");
                    }
                }
                return Json(result.ToString());
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(result.ToString());
            }

        }
    }
}