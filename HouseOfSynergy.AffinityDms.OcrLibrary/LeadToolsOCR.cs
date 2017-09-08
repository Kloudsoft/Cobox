using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using Leadtools;
using Leadtools.Barcode;
using Leadtools.Codecs;
//using Leadtools.Codecs.Cmp;
using Leadtools.Codecs.Png;
using Leadtools.Codecs.Tif;
using Leadtools.Documents;
using Leadtools.Documents.Converters;
using Leadtools.Documents.Pdf;
using Leadtools.Documents.Raster;
using Leadtools.Drawing;
using Leadtools.Forms;
using Leadtools.Forms.Auto;
using Leadtools.Forms.DocumentReaders;
using Leadtools.Forms.DocumentWriters;
using Leadtools.Forms.Ocr;
using Leadtools.Forms.Processing;
using Leadtools.Forms.Recognition;
using Leadtools.Forms.Recognition.Ocr;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Core;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class LeadToolsOCR
    {
        //static string LicenceFile = string.Empty;
        //static string LicenceKey = string.Empty;


        //string LICENSE_FILE;
        //string LICENSE_KEY;
        public string LeadToolsTemplatesDir = string.Empty;
        public string LeadtoolsTempDir = string.Empty;
        public string LeadtoolsAdvantageRuntimeDir = string.Empty;
        public RasterImage RasterImage { get; set; }

        //AutoFormsEngine autoEngine;
        RasterCodecs formsCodec;
        // Create the OCR Engine
        IOcrEngine ocrengine;
        IOcrPage ocrpage;
        IOcrDocument ocrDocument;
        // Create the repository of master forms
        //DiskMasterFormsRepository formsRepository;
        // RasterImage rasterImage;
        string text;
        //private IOcrZoneCharacters _zoneCharacters;
        //string MY_LICENSE_FILE;
        //string MY_LICENSE_KEY;
        //string ssss = "";
        FormRecognitionEngine formrecogengine;
        DocumentWriter docwriter;
        OcrObjectsManager ocrobjmanager;
        FormRecognitionAttributes formattributes;
        FormProcessingEngine formproccessingengine;
        FormPage formpage;
        BarcodeEngine barcodeengine;
        //AutoFormsRecognitionManager autoformrecogmanager;
        List<string> tablelist = new List<string>();
        List<string> distinctcolnames = new List<string>();
        List<Image> imglist = new List<Image>();
        string createtext = "";
        public Exception exception = null;
        //public LeadToolsOCR(string ServerMapPath)
        //{
        //    bool result = LeadToolsLicense.SetLicense(ServerMapPath, out LeadtoolsTempDir, out LeadToolsTemplatesDir, out LeadtoolsAdvantageRuntimeDir, out exception);
        //}
        public LeadToolsOCR(string ServerMapPath, string tenantid, string UniqueTempFolderValue, out Exception exception)
        {
            bool result = LeadToolsLicense.SetLicense(ServerMapPath, tenantid, UniqueTempFolderValue, out LeadtoolsTempDir, out LeadToolsTemplatesDir, out LeadtoolsAdvantageRuntimeDir, out exception);
        }
        public void LoadEngine(RasterCodecs formcodec, DocumentWriter docwrite, string tempdir, out IOcrEngine OCREngine, out Exception exception)
        {
            exception = null;
            OCREngine = null;
            try
            {

                text = "";
                OCREngine = OcrEngineManager.CreateEngine(LeadToolsSettings.OcrEngineType, false);
                if (!OCREngine.IsStarted)
                {
                    OCREngine.Startup(formcodec, docwrite, tempdir, LeadtoolsAdvantageRuntimeDir);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

        }
        public void LoadEngine(RasterCodecs formcodec, DocumentWriter docwrite, string tempdir)
        {
            text = "";
            ocrengine = OcrEngineManager.CreateEngine(LeadToolsSettings.OcrEngineType, false);
            if (!ocrengine.IsStarted)
            {
                ocrengine.Startup(formcodec, docwrite, tempdir, LeadtoolsAdvantageRuntimeDir);
            }
        }
        public void LoadEngine(RasterCodecs formcodec, DocumentWriter docwrite, string tempdir, Image templateimage)
        {
            text = "";
            try
            {
                ocrengine = OcrEngineManager.CreateEngine(LeadToolsSettings.OcrEngineType, false);
                if (!ocrengine.IsStarted)
                {
                    ocrengine.Startup(formcodec, docwrite, tempdir, LeadtoolsAdvantageRuntimeDir);
                }
                if (templateimage != null)
                {
                    RasterImage = RasterImageConverter.ConvertFromImage(templateimage, ConvertFromImageOptions.None);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public void LoadEngine(string imagepath, RasterCodecs formcodec, DocumentWriter docwrite, string tempdir)
        {
            text = "";
            ocrengine = OcrEngineManager.CreateEngine(LeadToolsSettings.OcrEngineType, false);
            if (!ocrengine.IsStarted)
            {
                ocrengine.Startup(formcodec, docwrite, tempdir, LeadtoolsAdvantageRuntimeDir);
                //ocrengine.SettingManager.SetEnumValue("Recognition.Fonts.DetectFontStyles", 0);
                //ocrengine.SettingManager.SetBooleanValue("Recognition.Fonts.RecognizeFontAttributes", false);
            }

            try
            {
                if (!(string.IsNullOrEmpty(imagepath)))
                {
                    string imgFileName = Path.Combine(imagepath);//ImagesDir, "A4SKEW.TIF");
                    RasterImage = ocrengine.RasterCodecsInstance.Load(imgFileName, 1);
                }
            }
            catch (RasterException ex)
            {
                GetExceptions(null, ex);
            }
        }
        public bool GetFullTextOCR(RasterImage rasterimage, out Exception exception, out string ocrtext)
        {
            bool result = false;
            exception = null;
            ocrtext = "";
            try
            {
                formsCodec = new RasterCodecs();
                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {
                    LoadEngine(null, null, null);
                    ocrpage = ocrengine.CreatePage(rasterimage, OcrImageSharingMode.AutoDispose);
                    IOcrDocument ocrDocument = ocrengine.DocumentManager.CreateDocument();
                    ocrpage = null;
                    ocrpage = ocrDocument.Pages.AddPage(rasterimage, null);
                    ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
                    ocrpage.AutoZone(null);
                    OcrZone ocrzone = new OcrZone();
                    ocrzone.Name = "ZoneName";
                    ocrzone.ZoneType = OcrZoneType.Text;
                    int aaaaa = ocrpage.Width;
                    ocrzone.Bounds = new LogicalRectangle(10, 10, ocrpage.Width - 20, 100, LogicalUnit.Pixel);
                    ocrpage.Zones.Add(ocrzone);
                    ocrpage.Recognize(null);
                    //  IOcrPageCharacters pageCharacters = ocrpage.GetRecognizedCharacters();
                    //  _zoneCharacters = pageCharacters[0];

                    int count = 0;
                    while (count < ocrpage.Zones.Count())
                    {
                        ocrtext += ocrpage.GetText(count);
                        count++;
                        continue;
                    }
                    ocrengine.Dispose();
                    ocrengine.Shutdown();
                    ocrengine.Dispose();
                    if (!(rasterimage.IsDisposed))
                    {
                        rasterimage.Dispose();
                    }
                    if (!(ocrDocument.IsInMemory))
                    {
                        ocrDocument.Dispose();
                    }
                    if (ocrpage != null)
                    {
                        ocrpage.Dispose();
                    }
                    result = true;

                }
                else
                {
                    Exception inex = new Exception("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
                    Exception ex = new Exception("RasterSupport Is Locked", inex);
                    throw ex;
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ocrengine.IsStarted)
                {
                    ocrengine.Shutdown();
                    ocrengine.Dispose();
                }
                exception = ex;
                return result;
            }
        }
        public bool GetFullTextXMLOCR(RasterImage rasterimage, out Exception exception, out string xmldata)
        {
            exception = null;
            xmldata = "";
            bool result = false;
            try
            {
                IOcrPage ocrpage = null;
                if (exception != null)
                {
                    throw exception;
                }
                formsCodec = new RasterCodecs();
                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {

                    LoadEngine(null, null, null);
                    IOcrDocument ocrDocument = ocrengine.DocumentManager.CreateDocument();
                    ocrpage = null;
                    ocrpage = ocrDocument.Pages.AddPage(rasterimage, null);
                    ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
                    ocrpage.Recognize(null);
                    string xml = ocrDocument.SaveXml(OcrXmlOutputOptions.None);
                    StringReader reader = new System.IO.StringReader(xml);
                    System.Xml.XPath.XPathDocument doc = new System.Xml.XPath.XPathDocument(reader);
                    System.Xml.XPath.XPathNavigator nav = doc.CreateNavigator();
                    System.Xml.XPath.XPathNodeIterator iter = nav.Select(@"//word");

                    while (iter.MoveNext())
                    {
                        xmldata += iter.Current.Value;
                    }
                    xmldata = nav.InnerXml.ToString();
                    ocrengine.Shutdown();
                    ocrengine.Dispose();
                    if (!(rasterimage.IsDisposed))
                    {
                        rasterimage.Dispose();
                    }
                    if (!(ocrDocument.IsInMemory))
                    {
                        ocrDocument.Dispose();
                    }
                    if (ocrpage != null)
                    {
                        ocrpage.Dispose();
                    }
                    result = true;

                }
                else
                {
                    Exception inex = new Exception("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
                    Exception ex = new Exception("RasterSupport Is Locked", inex);
                    throw ex;
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ocrengine.IsStarted)
                {
                    ocrengine.Shutdown();
                    ocrengine.Dispose();
                }
                exception = ex;
                return result;
            }
        }
        private void GetExceptions(Exception ex, RasterException rasterexception)
        {
            if (ex != null)
            {
                string excombo = ", HResult:" + ex.HResult + ", Message:" + ex.Message + ", InnerException:" + ex.InnerException + "\n";
                //ExceptionLogger.allExceptions += excombo;
                //ExceptionLogger.lastException = excombo;
            }
            if (rasterexception != null)
            {
                string excombo = "Code:" + rasterexception.Code + ", HResult:" + rasterexception.HResult + ", Message:" + rasterexception.Message + ", InnerException:" + rasterexception.InnerException + "\n";
                //ExceptionLogger.allExceptions += excombo;
                //ExceptionLogger.lastException = excombo;
            }
        }
        public RasterImage getrasterImage()
        {
            return RasterImage;
        }


        public string GetZoneText(double X, double Y, double Width, double Height, Image image)
        {
            formsCodec = new RasterCodecs();
            if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
            {
                try
                {
                    RasterImage = RasterImageConverter.ConvertFromImage(image, ConvertFromImageOptions.None);
                    LoadEngine(string.Empty, null, null, null);
                    ocrpage = ocrengine.CreatePage(RasterImage, OcrImageSharingMode.AutoDispose);
                    DoAutoPreprocessing(RasterImage);

                    //entiesElement.DivY, entiesElement.DivWidth, entiesElement.DivHeight
                    // LeadRect leadrect = LeadRect.FromLTRB(X, Y, Width, Height);

                    OcrZone zone = new Leadtools.Forms.Ocr.OcrZone();
                    zone.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    zone.ZoneType = OcrZoneType.Text;
                    zone.CharacterFilters = OcrZoneCharacterFilters.None;
                    ocrpage.Zones.Clear();
                    ocrpage.Zones.Add(zone);

                    ocrpage.Recognize(null);
                    string result = ocrpage.GetText(0);

                    if (result != "\n" || result != "" || result != "<br>")
                    {
                        ocrengine.Dispose();
                        return result;
                    }
                    else
                    {
                        ocrengine.Dispose();
                        return "No Result Found";
                    }
                }
                catch (Exception ex)
                {
                    return ("Error: " + ex.Message);
                }
            }
            return text;
        }

        public List<ComputeCoordinates> GetAllZoneData(List<TemplateElement> elements, double _computeddifference, Image image)
        {
            formsCodec = new RasterCodecs();
            if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
            {
                try
                {
                    RasterImage = RasterImageConverter.ConvertFromImage(image, ConvertFromImageOptions.None);
                    LoadEngine(string.Empty, null, null, null);
                    ocrpage = ocrengine.CreatePage(RasterImage, OcrImageSharingMode.AutoDispose);
                    DoAutoPreprocessing(RasterImage);

                    //entiesElement.DivY, entiesElement.DivWidth, entiesElement.DivHeight
                    // LeadRect leadrect = LeadRect.FromLTRB(X, Y, Width, Height);
                    List<ComputeCoordinates> cordlistlist = new List<ComputeCoordinates>();
                    ocrpage.Zones.Clear();
                    foreach (var element in elements)
                    {
                        ComputeCoordinates computecord = new ComputeCoordinates();
                        computecord.ComputeEleemnts(element, _computeddifference);
                        cordlistlist.Add(computecord);
                        OcrZone zone = new Leadtools.Forms.Ocr.OcrZone();
                        zone.Bounds = new LogicalRectangle(computecord.ComputedX, computecord.ComputedY, computecord.ComputedWidth, computecord.ComputedHeight, LogicalUnit.Pixel);
                        zone.ZoneType = OcrZoneType.Text;
                        zone.CharacterFilters = OcrZoneCharacterFilters.None;
                        ocrpage.Zones.Add(zone);
                    }


                    ocrpage.Recognize(null);
                    string result = ocrpage.GetText(0);
                    for (int i = 0; i < cordlistlist.Count; i++)
                    {
                        cordlistlist[i].Text = ocrpage.GetText(i);
                    }


                    if (result != "\n" || result != "" || result != "<br>")
                    {
                        ocrengine.Dispose();
                        return cordlistlist;
                    }
                    else
                    {
                        ocrengine.Dispose();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                    return (null);
                }
            }
            return null;
        }


        public void DoAutoPreprocessing(RasterImage rasterimage)
        {
            ocrDocument = ocrengine.DocumentManager.CreateDocument();
            ocrpage = ocrDocument.Pages.AddPage(rasterimage, null);
            //ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.Deskew, null);
            //ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.Invert, null);
            //ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.Rotate, null);
            ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
        }

        public int GetDeskewAngle(IOcrPage ocrpage)
        {
            return (ocrpage.GetDeskewAngle());
        }
        public RasterImage DeskewImage(RasterImage rasterimg)
        {
            DeskewCommand deskewcom = new DeskewCommand();
            deskewcom.Flags = DeskewCommandFlags.DeskewImage | DeskewCommandFlags.DoNotFillExposedArea;
            deskewcom.Run(rasterimg);
            return rasterimg;
        }
        public RasterImage RotateImage(int rotation, RasterImage rasterImageToMatch)
        {
            if (!(rotation == 0))
            {
                RotateCommand rotate = new RotateCommand();
                rotate.Angle = rotation * 100;
                rotate.FillColor = RasterColor.FromKnownColor(RasterKnownColor.White);
                rotate.Flags = RotateCommandFlags.Resize;
                rotate.Run(rasterImageToMatch);
            }
            return rasterImageToMatch;
        }
        public int GetRotationAngle(IOcrPage ocrpage)
        {
            return (ocrpage.GetRotateAngle());
        }




        public void AddMasterForm(List<TemplateElement> entieselements, List<List<TemplateElementDetail>> entieselementsdetails, string templatename, Image templateimage)
        {
            barcodeengine = new BarcodeEngine();
            formrecogengine = new FormRecognitionEngine();

            formsCodec = new RasterCodecs();
            IMasterFormsCategory parentCategory;
            DiskMasterFormsRepository workingRepository = new DiskMasterFormsRepository(formsCodec, LeadtoolsTempDir);
            parentCategory = workingRepository.RootCategory;
            FormRecognitionOptions options = new FormRecognitionOptions();
            formattributes = formrecogengine.CreateMasterForm(templatename, new Guid(), options);
            formrecogengine.CloseMasterForm(formattributes);
            parentCategory.AddMasterForm(formattributes, null, (RasterImage)null);
            DiskMasterForm currentMasterForm = parentCategory.MasterForms[0] as DiskMasterForm;

            FormPages formPages = currentMasterForm.ReadFields();
            RasterImage formImage = currentMasterForm.ReadForm();
            PageRecognitionOptions optionssss = new PageRecognitionOptions();

            docwriter = null;
            //ocrengine = OcrEngineManager.CreateEngine(OcrEngineType.Advantage, false);
            //ocrengine.Startup(formsCodec, docwriter, MasterFormsTemp, MasterFormsForms);

            LoadEngine(formsCodec, docwriter, LeadtoolsTempDir, templateimage);
            ocrobjmanager = new OcrObjectsManager(ocrengine);
            formrecogengine.ObjectsManagers.Add(ocrobjmanager);

            //formattributes = new FormRecognitionAttributes();
            //formattributes = formrecogengine.CreateMasterForm(formname, Guid.Empty, null);
            for (int i = 1; i <= RasterImage.PageCount; i++)
            {
                RasterImage.Page = i;
                formrecogengine.OpenMasterForm(formattributes);
                formrecogengine.InsertMasterFormPage(-1, formattributes, RasterImage.Clone(), optionssss, null);
                formrecogengine.CloseMasterForm(formattributes);
                //AddPageToMasterForm(rasterImage, formattributes);
            }
            if (formImage != null)
                formImage.AddPages(RasterImage.CloneAll(), 1, RasterImage.PageCount);
            else
                formImage = RasterImage.CloneAll();

            if (formPages != null)
            {
                for (int i = 0; i < RasterImage.PageCount; i++)
                    formPages.Add(new FormPage(formPages.Count + 1, RasterImage.XResolution, RasterImage.YResolution));
            }
            else
            {
                //No processing pages exist so we must create them
                FormProcessingEngine tempProcessingEngine = new FormProcessingEngine();
                tempProcessingEngine.OcrEngine = ocrengine;
                tempProcessingEngine.BarcodeEngine = barcodeengine;

                for (int i = 0; i < formrecogengine.GetFormProperties(formattributes).Pages; i++)
                    tempProcessingEngine.Pages.Add(ProccessMasterFormField(entieselements, entieselementsdetails));

                formPages = tempProcessingEngine.Pages;
            }

            currentMasterForm.WriteForm(formImage);
            currentMasterForm.WriteAttributes(formattributes);
            currentMasterForm.WriteFields(formPages);

            // formrecogengine.CloseMasterForm(formattributes);
            //  ProccessMasterFormField(zonedetails);

        }

        public FormPage ProccessMasterFormField(List<TemplateElement> entieselement, List<List<TemplateElementDetail>> entieselementdetails)
        {
            formpage = new FormPage(RasterImage.Page, RasterImage.XResolution, RasterImage.YResolution);
            formproccessingengine = new FormProcessingEngine();
            LoadEngine(string.Empty, null, null, null);

            formproccessingengine.OcrEngine = ocrengine;

            //Enable Disable BarcodeEngine Based On Usage
            barcodeengine = new BarcodeEngine();
            formproccessingengine.BarcodeEngine = barcodeengine;

            List<FormField> fields = new List<FormField>();
            foreach (var element in entieselement)
            {
                double X = 0.0;
                double Y = 0.0;
                double Width = 0.0;
                double Height = 0.0;
                if (element.ElementType != ElementType.Table)
                {
                    X = Convert.ToDouble(element.DivX);
                    Y = Convert.ToDouble(element.DivY);
                    Width = double.Parse(element.DivWidth.Replace("px", ""));
                    Height = double.Parse(element.DivHeight.Replace("px", ""));
                }
                else
                {
                    X = Convert.ToDouble(element.X);
                    Y = Convert.ToDouble(element.Y);
                    Width = double.Parse(element.Width);
                    Height = double.Parse(element.Height);
                }



                if (element.ElementType == ElementType.Rectangle)
                {
                    TextFormField text = new TextFormField();
                    text.Name = element.Id.ToString();
                    text.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    formpage.Add(text);
                }
                //else if (element.ElementType.ToLower() == "omr")
                //{
                //    OmrFormField omr = new OmrFormField();
                //    omr.Name = element.Id.ToString();
                //    omr.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                //    formpage.Add(omr);
                //}
                else if (element.ElementType == ElementType.Image)
                {
                    ImageFormField image = new ImageFormField();
                    image.Name = element.Id.ToString();
                    image.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    formpage.Add(image);
                }
                else if (element.ElementType == ElementType.Barcode2D)
                {
                    BarcodeFormField barcode = new BarcodeFormField();
                    barcode.Name = element.Id.ToString();
                    barcode.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    formpage.Add(barcode);
                }

                else if (element.ElementType ==ElementType.Table)
                {
                    TableFormField tableField = new TableFormField();
                    tableField.Name = element.Id.ToString();
                    tableField.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    tableField.Rules = TableRules.NoRules;
                    foreach (List<TemplateElementDetail> elementdetails in entieselementdetails)
                    {
                        if (elementdetails != null)
                        {
                            foreach (TemplateElementDetail elementdetail in elementdetails)
                            {
                                if (elementdetail.ElementId == element.Id)
                                {
                                    double ColX = Convert.ToDouble(elementdetail.X);
                                    double ColY = Convert.ToDouble(elementdetail.Y);
                                    double ColWidth = double.Parse(elementdetail.Width);
                                    double ColHeight = double.Parse(elementdetail.Height);
                                    TextFormField column = new TextFormField();
                                    column.Name = elementdetail.Id.ToString();
                                    column.Bounds = new LogicalRectangle(ColX, ColY, ColWidth, ColHeight, LogicalUnit.Pixel);
                                    tableField.Columns.Add(new TableColumn(column));
                                }
                            }
                        }


                    }
                    formpage.Add(tableField);
                }
            }
            return formpage;
        }

        public List<string> GetMasterfileName(List<Template> entiesTemplates, string filetype)
        {
            List<string> masterFileNames = null;
            foreach (var entiesTemplate in entiesTemplates)
            {
                string filepath = Path.Combine(LeadToolsTemplatesDir, entiesTemplate.Title, ".", filetype);
                if (File.Exists(filepath))
                {
                    masterFileNames.Add(filepath);
                }
            }
            return masterFileNames;
        }

        #region WORKING TEMPLATE MATCHING OLD

        public string[] GetMasterfileName(string filetype)
        {
            string[] masterFileNames = Directory.GetFiles(LeadToolsTemplatesDir, "*." + filetype);//, SearchOption.AllDirectories);
            return masterFileNames;
        }


        //TemplateMatachingData tempdata;
        public string DoTemplateMatching(Image imagetomatch, string fullfilenamewidthExtention)
        {
            //  tempdata = new TemplateMatachingData();
            try
            {
                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {
                    //formrecogengine = new FormRecognitionEngine();
                    //formsCodec = new RasterCodecs();
                    //LoadEngine(null, formsCodec, null, null);
                    //ocrobjmanager = new OcrObjectsManager(ocrengine);
                    //ocrobjmanager.Engine = ocrengine;
                    //formrecogengine.ObjectsManagers.Add(ocrobjmanager);
                    //RasterImage rasterImageToMatch = RasterImageConverter.ConvertFromImage(imagetomatch, ConvertFromImageOptions.None);// formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                    //formattributes = formrecogengine.CreateForm(null);
                    //IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
                    //ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                    //// int deskew = GetDeskewAngle(ocrpage);
                    //int rotation = GetRotationAngle(ocrpage);
                    //rasterImageToMatch = RotateImage(rotation, rasterImageToMatch);
                    //rasterImageToMatch = DeskewImage(rasterImageToMatch);
                    ////cleanimage(1, rasterImage.PageCount);
                    ////RotateCommand command = new RotateCommand();
                    ////command.Angle = 45 * 100;
                    ////command.FillColor = new RasterColor(255, 255, 255);
                    ////command.Flags = RotateCommandFlags.Bicubic;
                    ////command.Run(rasterImage);


                    string[] masterFileNames = GetMasterfileName("bin");
                    RasterImage rasterImageToMatch = null;
                    foreach (string masterFileName in masterFileNames)
                    {
                        //RasterSupport.SetLicense(LICENSE_FILE, LICENSE_KEY);

                        formrecogengine = new FormRecognitionEngine();
                        formsCodec = new RasterCodecs();
                        LoadEngine(null, formsCodec, null, null);
                        ocrobjmanager = new OcrObjectsManager(ocrengine);
                        ocrobjmanager.Engine = ocrengine;
                        formrecogengine.ObjectsManagers.Add(ocrobjmanager);
                        if (rasterImageToMatch == null)
                        {
                            rasterImageToMatch = RasterImageConverter.ConvertFromImage(imagetomatch, ConvertFromImageOptions.None);// formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                            formattributes = formrecogengine.CreateForm(null);
                            IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
                            ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                            // int deskew = GetDeskewAngle(ocrpage);
                            int rotation = GetRotationAngle(ocrpage);
                            rasterImageToMatch = RotateImage(rotation, rasterImageToMatch);
                            rasterImageToMatch = DeskewImage(rasterImageToMatch);
                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                            {
                                rasterImageToMatch.Page = i + 1;
                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                            }
                            formrecogengine.CloseForm(formattributes);
                        }
                        else
                        {
                            formattributes = formrecogengine.CreateForm(null);
                            IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
                            ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                            {
                                rasterImageToMatch.Page = i + 1;
                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                            }
                            formrecogengine.CloseForm(formattributes);
                        }

                        string resultMessage = "The form could not be recognized";
                        FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();
                        masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
                        FormRecognitionResult recognitionResult = formrecogengine.CompareForm(masterFormAttributes, formattributes, null);
                        if (recognitionResult.Confidence >= 80)
                        {
                            if (!(createtext == ""))
                            {
                                createtext += "<br><br>";
                            }
                            createtext += "Results Matched With Template: " + Path.GetFileNameWithoutExtension(masterFileName) + "<br>Template Confidence: " + recognitionResult.Confidence + "<br>Recognition Reason: " + recognitionResult.Reason.ToString() + "<br><br>";
                            string MasterFormXmlFileWithFields = masterFileName.Replace(".bin", ".xml");
                            StartRecogniction(rasterImageToMatch, masterFormAttributes, formattributes, MasterFormXmlFileWithFields);
                            resultMessage = String.Format("This form has been recognized as a {0}  : {1} ", Path.GetFileNameWithoutExtension(masterFileName), recognitionResult.Reason.ToString());
                            if (ocrengine != null && ocrengine.IsStarted)
                                ocrengine.Shutdown();
                        }
                        else
                        {
                            createtext += "Template Not matched ( " + Path.GetFileNameWithoutExtension(masterFileName) + ") : Template Confidence was " + recognitionResult.Confidence + "<br> ";
                        }
                    }
                }
                return createtext;
            }
            catch (Exception ex)
            {
                return ("Error: " + ex.Message);

            }
        }
        private void StartRecogniction(RasterImage rasterImageToMatch, FormRecognitionAttributes masterFormAttributes, FormRecognitionAttributes formattributestomatch, string MasterFormXmlFileWithFields)
        {

            formproccessingengine = new FormProcessingEngine();
            LoadEngine(string.Empty, null, null, null);
            formproccessingengine.OcrEngine = ocrengine;
            formproccessingengine.OcrEngine.Startup(formsCodec, null, null, LeadtoolsAdvantageRuntimeDir);
            formproccessingengine.LoadFields(MasterFormXmlFileWithFields);
            IList<PageAlignment> pagealignmentlist = formrecogengine.GetFormAlignment(masterFormAttributes, formattributestomatch, null, null);
            formproccessingengine.Process(rasterImageToMatch, pagealignmentlist);
            FormPages ProcessingPages = formproccessingengine.Pages;
            if (ProcessingPages != null)
            {
                string coltext = "";
                string tabletext = "";
                string rowtext = "";
                for (int i = 0; i < ProcessingPages.Count; i++)
                {
                    for (int j = 0; j < ProcessingPages[i].Count; j++)
                    {
                        FormField formfield = ProcessingPages[i][j];
                        if (formfield is TableFormField)
                        {
                            TableFormField tableformfield = ProcessingPages[i][j] as TableFormField;
                            tabletext += "<table>" + tableformfield.Name;
                            TableFormFieldResult results = tableformfield.Result as TableFormFieldResult;
                            if (results != null && results.Rows != null)
                            {

                                for (int x = 0; x < results.Rows.Count; x++)
                                {
                                    TableFormRow row = results.Rows[x];
                                    rowtext += "<tr>";
                                    //int lineCounter = 1;
                                    for (int y = 0; y < row.Fields.Count; y++)
                                    {
                                        OcrFormField ocrField = row.Fields[y];
                                        if (ocrField is TextFormField)
                                        {
                                            TextFormFieldResult txtResults = ocrField.Result as TextFormFieldResult;
                                            if (coltext == "")
                                            {
                                                distinctcolnames.Add(ocrField.Name);
                                                coltext = "<tr><th>" + ocrField.Name + "</th>";
                                            }
                                            else
                                            {
                                                if (!(distinctcolnames.Contains(ocrField.Name)))
                                                {
                                                    distinctcolnames.Add(ocrField.Name);
                                                    coltext += "<th>" + ocrField.Name + "</th>";
                                                }
                                            }

                                            //tabledetails.Add(ocrField.Name);
                                            rowtext += "<td>" + txtResults.Text + "</td>";
                                            //tabledetails.Add(txtResults.Text);
                                        }
                                        else if (ocrField is OmrFormField)
                                        {
                                            OmrFormFieldResult omrResults = ocrField.Result as OmrFormFieldResult;
                                            //tabledetails.Add(omrResults.Text);
                                            //tabledetails.Add(ocrField.Name);
                                        }
                                    }
                                    rowtext += "</tr>";
                                    coltext += "</tr>";
                                }
                            }

                            tabletext += coltext + rowtext + "</table>";
                            tablelist.Add(tabletext);
                            coltext = "";
                            rowtext = "";
                            distinctcolnames.Clear();
                        }
                        else if (formfield is TextFormField)
                        {
                            var fieldtext = ((formfield as TextFormField).Result as TextFormFieldResult).Text;
                            createtext += "Field Name: " + formfield.Name + "<br>";
                            createtext += "Text: " + fieldtext;
                            var fieldtextconfi = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence.ToString();
                            createtext += "Field Confidence: " + fieldtextconfi + "<br>";
                            createtext += "TOP: " + formfield.Bounds.Top + "<br>";
                            createtext += "LEFT: " + formfield.Bounds.Left + "<br>";
                            createtext += "Width: " + formfield.Bounds.Width + "<br>";
                            createtext += "Height: " + formfield.Bounds.Height + "<br><br>";


                        }
                        else if (formfield is ImageFormField)
                        {
                            var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
                            createtext += "Field Name: " + formfield.Name + "<br>";
                            createtext += "Imagee:<br>";
                            var img = ((formfield as ImageFormField).Result as ImageFormFieldResult).Image;
                            Image destImage = RasterImageConverter.ChangeToImage(img, ChangeToImageOptions.ForceChange);
                            imglist.Add(destImage);
                            var fieldtextw = ((formfield as ImageFormField).Result as ImageFormFieldResult).Image.Width;
                            createtext += "Width: " + fieldtextw + "<br>";
                            var fieldtexth = ((formfield as ImageFormField).Result as ImageFormFieldResult).Image.Height;
                            createtext += "Height: " + fieldtexth + "<br>";
                        }
                    }
                }
            }
        }
        #endregion WORKING TEMPLATE MATCHING OLD


        //========================================
        #region Template Matching WorkerRole
        //========================================

        public bool DoTemplateMatching(Image imagetomatch, HouseOfSynergy.AffinityDms.Entities.Tenants.Document inputdocument, List<Template> AllTemplates, List<TemplateElement> AllELements, List<TemplateElement> filterdTemplateElements, out HouseOfSynergy.AffinityDms.Entities.Tenants.Document matcheddocument, out List<DocumentTemplate> documenttemplate, out Exception exception)
        {

            bool result = false;
            exception = null;
            matcheddocument = null;
            documenttemplate = null;
            try
            {
                if (RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {
                    Exception inex = new Exception("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing/Expiried");
                    Exception ex = new Exception("RasterSupport Is Locked", inex);
                    throw ex;
                }
                List<long> distinctFilteredTemplatesElementsTemplateId = filterdTemplateElements.Select(x => x.TemplateId).ToList().Distinct().ToList();
                List<Template> entiesTemplates = new List<Template>();
                foreach (var distincttemplateid in distinctFilteredTemplatesElementsTemplateId)
                {
                    Template template = AllTemplates.Where(e => e.Id == distincttemplateid).Select(x => x).FirstOrDefault();
                    if (template != null)
                    {
                        entiesTemplates.Add(template);
                    }
                }
                // List<string> masterFileNames = GetMasterfileName(entiesTemplates,"bin");
                RasterImage rasterImageToMatch = null;
                List<ConfidenceMatchedTemplates> ConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
                // List<ConfidenceMatchedTemplates> LowConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
                foreach (var masterFileNametemplate in entiesTemplates)
                {
                    formrecogengine = new FormRecognitionEngine();
                    formsCodec = new RasterCodecs();
                    LoadEngine(null, formsCodec, null, null);
                    ocrobjmanager = new OcrObjectsManager(ocrengine);
                    ocrobjmanager.Engine = ocrengine;
                    formrecogengine.ObjectsManagers.Add(ocrobjmanager);
                    formattributes = formrecogengine.CreateForm(null);
                    IOcrDocument doc = null;
                    doc = ocrengine.DocumentManager.CreateDocument();
                    ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                    if (rasterImageToMatch == null)
                    {
                        rasterImageToMatch = RasterImageConverter.ConvertFromImage(((Image)imagetomatch.Clone()), ConvertFromImageOptions.None);// formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                        int rotation = GetRotationAngle(ocrpage);
                        rasterImageToMatch = RotateImage(rotation, rasterImageToMatch);
                        rasterImageToMatch = DeskewImage(rasterImageToMatch);
                    }
                    for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                    {
                        rasterImageToMatch.Page = i + 1;
                        formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                    }
                    formrecogengine.CloseForm(formattributes);

                    //string resultMessage = "The form could not be recognized";
                    FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();
                    string masterFileName = Path.Combine(LeadToolsTemplatesDir, masterFileNametemplate.Id + ".bin");
                    if (!(File.Exists(masterFileName)))
                    {
                        //exception 
                        Exception inex = new Exception("The following file does not exist in the directory: " + masterFileName.ToString());
                        Exception ex = new Exception("File does not Exist in the directory");
                        throw ex;
                    }
                    masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
                    FormRecognitionResult recognitionResult = formrecogengine.CompareForm(masterFormAttributes, formattributes, null);
                    if (recognitionResult.Confidence >= ((int)OcrConfidence.MinimumOCRConfidence))
                    {
                        if (!(createtext == ""))
                        {
                            createtext += "<br><br>";
                        }
                        string MasterFormXmlFileWithFields = masterFileName.Replace(".bin", ".xml");
                        ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                        confiedenceObj.Template = masterFileNametemplate;
                        confiedenceObj.MasterFormAttributes = masterFormAttributes;
                        confiedenceObj.FormAttributes = formattributes;
                        confiedenceObj.Confidence = recognitionResult.Confidence;
                        ConfidenceMatchedtemplates.Add(confiedenceObj);
                    }
                    //else if ((recognitionResult.Confidence >= ((int)Enties.OcrConfidence.MinimumRecognitionConfidence))&&(recognitionResult.Confidence < ((int)Enties.OcrConfidence.MinimumOCRConfidence)))
                    //{ 
                    //    ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                    //    confiedenceObj.Template = masterFileNametemplate;
                    //    confiedenceObj.MasterFormAttributes = masterFormAttributes;
                    //    confiedenceObj.FormAttributes = formattributes;
                    //    confiedenceObj.Confidence = recognitionResult.Confidence;
                    //    LowConfidenceMatchedtemplates.Add(confiedenceObj);
                    //}
                }
                List<ConfidenceMatchedTemplates> Matchedtemplates = null;
                if (ConfidenceMatchedtemplates.Count > 0)
                {
                    result = TemplatesConfidenceMatched(inputdocument, AllTemplates, AllELements, ConfidenceMatchedtemplates, rasterImageToMatch, out Matchedtemplates, out matcheddocument, out documenttemplate, out exception);
                }

                if (exception != null)
                {
                    throw exception;
                }
                if (matcheddocument == null)
                {
                    matcheddocument = new Entities.Tenants.Document();
                }
                //===========================
                #region Update DocumentState
                //===========================

                ///<summary>
                ///Condition: Definition
                ///1) Checks if matched template is less than or equals to 0 the document state will be unmatched
                ///2) Checks if matched template is greater than 0  and result returned is false. False is return when multiple templates are matched. Document state will be matched multiple
                ///3) Checks if result returned is true.True is return when a single template is matched.  Document state will be matched.
                /// </summary>
                if ((ConfidenceMatchedtemplates.Count <= 0)) //((LowConfidenceMatchedtemplates.Count > 0) && (result==false))
                {
                    matcheddocument.State = DocumentState.UnMatched;
                }
                else if ((ConfidenceMatchedtemplates.Count > 0) && (result == false))
                {
                    matcheddocument.State = DocumentState.MatchedMultiple;
                }
                else if (result)
                {
                    matcheddocument.State = DocumentState.Matched;
                }
                //===========================
                #endregion Update DocumentState
                //===========================
                matcheddocument.Id = inputdocument.Id;
                return result;
            }
            catch (Exception ex)
            {
                if (ocrengine.IsStarted)
                {
                    ocrengine.Shutdown();
                    ocrengine.Dispose();
                }

                if (imagetomatch != null)
                {
                    imagetomatch.Dispose();
                }
                if (!(ocrDocument.IsInMemory))
                {
                    ocrDocument.Dispose();
                }
                if (ocrpage != null)
                {
                    ocrpage.Dispose();
                }
                result = true;
                exception = ex;
                return result;
            }
        }


        public bool TemplatesConfidenceMatched(HouseOfSynergy.AffinityDms.Entities.Tenants.Document inputdocument, List<Template> AllTemplates, List<TemplateElement> AllELements, List<ConfidenceMatchedTemplates> confidencematchedtemplates, RasterImage rasterimagetomatch, out List<ConfidenceMatchedTemplates> matchedtemplates, out HouseOfSynergy.AffinityDms.Entities.Tenants.Document document, out List<DocumentTemplate> documentTemplates, out Exception exception)
        {
            exception = null;
            matchedtemplates = null;
            document = new Entities.Tenants.Document();
            documentTemplates = new List<DocumentTemplate>();
            bool result = false;
            try
            {
                if (confidencematchedtemplates.Count == 1)
                {
                    result = ProccessSingleMatchedDocument(inputdocument, AllTemplates, AllELements, confidencematchedtemplates.First(), rasterimagetomatch, out document, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                else if (confidencematchedtemplates.Count > 1)
                {
                    ConfidenceMatchedTemplates maxconfidencetemplate = null;
                    bool foundmaxconfidencetemplate = false;
                    foreach (var confidencematchedtemplate in confidencematchedtemplates)
                    {
                        if (maxconfidencetemplate == null)
                        {
                            maxconfidencetemplate = new ConfidenceMatchedTemplates();
                            maxconfidencetemplate = confidencematchedtemplate;
                            foundmaxconfidencetemplate = true;
                        }
                        else
                        {
                            if (maxconfidencetemplate.Confidence < confidencematchedtemplate.Confidence)
                            {
                                maxconfidencetemplate = confidencematchedtemplate;
                                foundmaxconfidencetemplate = true;
                            }
                            else if (maxconfidencetemplate.Confidence == confidencematchedtemplate.Confidence)
                            {
                                foundmaxconfidencetemplate = false;
                            }
                        }
                    }
                    if (foundmaxconfidencetemplate)
                    {

                        bool dbresult = ProccessSingleMatchedDocument(inputdocument, AllTemplates, AllELements, confidencematchedtemplates.First(), rasterimagetomatch, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                    }
                    else
                    {
                        foreach (var confidencematchedtemplate in confidencematchedtemplates)
                        {
                            var documenttemplate = new DocumentTemplate();
                            documenttemplate.DocumentId = inputdocument.Id;
                            documenttemplate.TemplateId = confidencematchedtemplate.Template.Id;
                            documenttemplate.Confidence = confidencematchedtemplate.Confidence;
                            documentTemplates.Add(documenttemplate);
                        }
                    }
                    result = foundmaxconfidencetemplate;
                }
                else
                {
                    result = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                return result;
            }
        }


        public bool ProccessSingleMatchedDocument(HouseOfSynergy.AffinityDms.Entities.Tenants.Document inputdocument, List<Template> AllTemplates, List<TemplateElement> AllELements, ConfidenceMatchedTemplates matchedtemplate, RasterImage rasterimagetomatch, out HouseOfSynergy.AffinityDms.Entities.Tenants.Document document, out Exception exception)
        {
            exception = null;
            document = new Entities.Tenants.Document();
            bool result = false;
            List<DocumentElement> documentElements = null;
            try
            {

                bool returnedreslt = StartRecogniction(inputdocument, AllTemplates, AllELements, matchedtemplate, rasterimagetomatch, out documentElements, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (returnedreslt)
                {
                    //BusinessLayer.DocumentManagement.UpdateDocument()
                    document.TemplateId = matchedtemplate.Template.Id;
                    document.Confidence = matchedtemplate.Confidence;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }


        private bool StartRecogniction(HouseOfSynergy.AffinityDms.Entities.Tenants.Document inputdocument, List<Template> AllTemplates, List<TemplateElement> AllELements, ConfidenceMatchedTemplates confidencematchedtemplates, RasterImage rasterimagetomatch, out List<DocumentElement> documentelements, out Exception exception)
        {
            List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates>();
            exception = null;
            documentelements = new List<DocumentElement>();
            bool result = false;
            try
            {
                string MasterFormXmlFileWithFields = Path.Combine(LeadToolsTemplatesDir, confidencematchedtemplates.Template.Id + ".xml");
                if (!(File.Exists(MasterFormXmlFileWithFields)))
                {
                    Exception inex = new Exception("Unable to find the Template: " + MasterFormXmlFileWithFields);
                    Exception ex = new Exception("Template Not Found", inex);
                    throw ex;
                }
                formproccessingengine = new FormProcessingEngine();
                LoadEngine(string.Empty, null, null, null);
                formproccessingengine.OcrEngine = ocrengine;
                formproccessingengine.OcrEngine.Startup(formsCodec, null, null, LeadtoolsAdvantageRuntimeDir);
                formproccessingengine.LoadFields(MasterFormXmlFileWithFields);
                IList<PageAlignment> pagealignmentlist = formrecogengine.GetFormAlignment(confidencematchedtemplates.MasterFormAttributes, confidencematchedtemplates.FormAttributes, null, null);
                formproccessingengine.Process(rasterimagetomatch, pagealignmentlist);
                FormPages ProcessingPages = formproccessingengine.Pages;
                List<TemplateElement> templateelements = AllELements.Where(e => e.TemplateId == confidencematchedtemplates.Template.Id).ToList(); ;
                if (ProcessingPages != null)
                {

                    for (int i = 0; i < ProcessingPages.Count; i++)
                    {
                        for (int j = 0; j < ProcessingPages[i].Count; j++)
                        {
                            FormField formfield = ProcessingPages[i][j];
                            if (formfield is TableFormField)
                            {
                                TableFormField tableformfield = ProcessingPages[i][j] as TableFormField;
                                long tableid = long.Parse(tableformfield.Name);
                                TableFormFieldResult results = tableformfield.Result as TableFormFieldResult;
                                if (results != null && results.Rows != null)
                                {
                                    for (int x = 0; x < results.Rows.Count; x++)
                                    {
                                        TableFormRow row = results.Rows[x];
                                        for (int y = 0; y < row.Fields.Count; y++)
                                        {
                                            OcrFormField ocrField = row.Fields[y];
                                            if (ocrField is TextFormField)
                                            {
                                                TextFormFieldResult txtResults = ocrField.Result as TextFormFieldResult;
                                                var documentelement = new DocumentElement();
                                                documentelement.TemplateElementId = tableid;
                                                documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
                                                documentelement.OcrText = txtResults.Text;
                                                documentelement.DocumentId = inputdocument.Id;
                                                documentelements.Add(documentelement);

                                            }
                                            else if (ocrField is OmrFormField)
                                            {
                                                OmrFormFieldResult omrResults = ocrField.Result as OmrFormFieldResult;
                                                var documentelement = new DocumentElement();
                                                documentelement.TemplateElementId = tableid;
                                                documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
                                                documentelement.OcrText = omrResults.Text;
                                                documentelement.DocumentId = inputdocument.Id;
                                                documentelements.Add(documentelement);
                                            }
                                        }
                                    }
                                }
                            }
                            else if (formfield is TextFormField)
                            {
                                var fieldtext = ((formfield as TextFormField).Result as TextFormFieldResult).Text;
                                var documentelement = new DocumentElement();
                                documentelement.TemplateElementId = long.Parse(formfield.Name);
                                documentelement.OcrText = fieldtext;
                                documentelement.DocumentId = inputdocument.Id;
                                documentelements.Add(documentelement);
                            }
                            else if (formfield is ImageFormField)
                            {
                                var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
                                var documentelement = new DocumentElement();
                                documentelement.TemplateElementId = long.Parse(formfield.Name);
                                documentelement.OcrText = fieldtext;
                                documentelement.DocumentId = inputdocument.Id;
                                documentelements.Add(documentelement);
                            }
                        }
                    }
                }
                result = true;


            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        //========================================
        #endregion Template Matching WorkerRole
        //========================================


        #region WORKING TEMPLATE MATCHING NEW
        public bool DoTemplateMatching(TenantUserSession tenantUserSession, Image imagetomatch, long documentid, List<TemplateElement> filterdTemplateElements, out Exception exception)
        {

            bool result = false;
            exception = null;
            try
            {
                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {
                    List<long> distinctFilteredTemplatesElementsTemplateId = filterdTemplateElements.Select(x => x.TemplateId).ToList().Distinct().ToList();
                    List<Template> entiesTemplates = new List<Template>();
                    foreach (var distincttemplateids in distinctFilteredTemplatesElementsTemplateId)
                    {
                        Template template = new Template();
                        bool rtnresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, distincttemplateids, out template, out exception);

                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (rtnresult)
                        {
                            entiesTemplates.Add(template);
                        }
                    }
                    // List<string> masterFileNames = GetMasterfileName(entiesTemplates,"bin");
                    RasterImage rasterImageToMatch = null;
                    List<ConfidenceMatchedTemplates> ConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
                    // List<ConfidenceMatchedTemplates> LowConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
                    foreach (var masterFileNametemplate in entiesTemplates)
                    {
                        formrecogengine = new FormRecognitionEngine();
                        formsCodec = new RasterCodecs();
                        LoadEngine(null, formsCodec, null, null);
                        ocrobjmanager = new OcrObjectsManager(ocrengine);
                        ocrobjmanager.Engine = ocrengine;
                        formrecogengine.ObjectsManagers.Add(ocrobjmanager);
                        if (rasterImageToMatch == null)
                        {
                            rasterImageToMatch = RasterImageConverter.ConvertFromImage(imagetomatch, ConvertFromImageOptions.None);// formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                            formattributes = formrecogengine.CreateForm(null);
                            IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
                            ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                            // int deskew = GetDeskewAngle(ocrpage);
                            int rotation = GetRotationAngle(ocrpage);
                            rasterImageToMatch = RotateImage(rotation, rasterImageToMatch);
                            rasterImageToMatch = DeskewImage(rasterImageToMatch);
                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                            {
                                rasterImageToMatch.Page = i + 1;
                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                            }
                            formrecogengine.CloseForm(formattributes);
                        }
                        else
                        {
                            formattributes = formrecogengine.CreateForm(null);
                            IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
                            ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                            {
                                rasterImageToMatch.Page = i + 1;
                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                            }
                            formrecogengine.CloseForm(formattributes);
                        }

                        //string resultMessage = "The form could not be recognized";
                        FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();
                        string masterFileName = Path.Combine(LeadToolsTemplatesDir, masterFileNametemplate.Id + ".bin");
                        if (!(File.Exists(masterFileName)))
                        {
                            //exception 
                            Exception inex = new Exception("The following file does not exist in the directory: " + masterFileName.ToString());
                            Exception ex = new Exception("File does not Exist in the directory");
                            throw ex;
                        }
                        masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
                        FormRecognitionResult recognitionResult = formrecogengine.CompareForm(masterFormAttributes, formattributes, null);
                        if (recognitionResult.Confidence >= ((int)OcrConfidence.MinimumOCRConfidence))
                        {
                            if (!(createtext == ""))
                            {
                                createtext += "<br><br>";
                            }
                            string MasterFormXmlFileWithFields = masterFileName.Replace(".bin", ".xml");
                            ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                            confiedenceObj.Template = masterFileNametemplate;
                            confiedenceObj.MasterFormAttributes = masterFormAttributes;
                            confiedenceObj.FormAttributes = formattributes;
                            confiedenceObj.Confidence = recognitionResult.Confidence;
                            ConfidenceMatchedtemplates.Add(confiedenceObj);
                        }
                        //else if ((recognitionResult.Confidence >= ((int)Enties.OcrConfidence.MinimumRecognitionConfidence))&&(recognitionResult.Confidence < ((int)Enties.OcrConfidence.MinimumOCRConfidence)))
                        //{ 
                        //    ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                        //    confiedenceObj.Template = masterFileNametemplate;
                        //    confiedenceObj.MasterFormAttributes = masterFormAttributes;
                        //    confiedenceObj.FormAttributes = formattributes;
                        //    confiedenceObj.Confidence = recognitionResult.Confidence;
                        //    LowConfidenceMatchedtemplates.Add(confiedenceObj);
                        //}
                    }
                    List<ConfidenceMatchedTemplates> Matchedtemplates = null;
                    if (ConfidenceMatchedtemplates.Count > 0)
                    {
                        result = TemplatesConfidenceMatched(tenantUserSession, ConfidenceMatchedtemplates, documentid, rasterImageToMatch, out Matchedtemplates, out exception);
                    }

                    if (exception != null)
                    {
                        throw exception;
                    }
                    //===========================
                    #region Update DocumentState
                    //===========================

                    ///<summary>
                    ///Condition: Definition
                    ///1) Checks if matched template is less than or equals to 0 the document state will be unmatched
                    ///2) Checks if matched template is greater than 0  and result returned is false. False is return when multiple templates are matched. Document state will be matched multiple
                    ///3) Checks if result returned is true.True is return when a single template is matched.  Document state will be matched.
                    /// </summary>
                    if ((ConfidenceMatchedtemplates.Count <= 0)) //((LowConfidenceMatchedtemplates.Count > 0) && (result==false))
                    {
                        var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                        bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult)
                        {
                            document.State = DocumentState.UnMatched;
                            result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }
                    else if ((ConfidenceMatchedtemplates.Count > 0) && (result == false))
                    {
                        var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                        bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult)
                        {
                            document.State = DocumentState.MatchedMultiple;
                            result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }
                    else if (result)
                    {
                        var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                        bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult)
                        {
                            document.State = DocumentState.Matched;
                            result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }
                    //===========================
                    #endregion Update DocumentState
                    //===========================

                }
                else
                {
                    Exception inex = new Exception("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
                    Exception ex = new Exception("RasterSupport Is Locked", inex);
                    throw ex;
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ocrengine.IsStarted)
                {
                    ocrengine.Shutdown();
                    ocrengine.Dispose();
                }

                if (imagetomatch != null)
                {
                    imagetomatch.Dispose();
                }
                if (!(ocrDocument.IsInMemory))
                {
                    ocrDocument.Dispose();
                }
                if (ocrpage != null)
                {
                    ocrpage.Dispose();
                }
                result = true;
                exception = ex;
                return result;
            }
        }
        public bool DoTemplateMatching(TenantUserSession tenantUserSession, Image imagetomatch, long documentid, List<TemplateElement> filterdTemplateElements, ref OcrResultInfo ocrresultinfo, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {
                    List<long> distinctFilteredTemplatesElementsTemplateId = filterdTemplateElements.Select(x => x.TemplateId).ToList().Distinct().ToList();
                    List<Template> entiesTemplates = new List<Template>();
                    foreach (var distincttemplateids in distinctFilteredTemplatesElementsTemplateId)
                    {
                        Template template = new Template();
                        bool rtnresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, distincttemplateids, out template, out exception);

                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (rtnresult)
                        {
                            entiesTemplates.Add(template);
                        }
                    }
                    // List<string> masterFileNames = GetMasterfileName(entiesTemplates,"bin");
                    RasterImage rasterImageToMatch = null;
                    List<ConfidenceMatchedTemplates> ConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
                    // List<ConfidenceMatchedTemplates> LowConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
                    foreach (var masterFileNametemplate in entiesTemplates)
                    {
                        formrecogengine = new FormRecognitionEngine();
                        formsCodec = new RasterCodecs();
                        LoadEngine(null, formsCodec, null, null);
                        ocrobjmanager = new OcrObjectsManager(ocrengine);
                        ocrobjmanager.Engine = ocrengine;
                        formrecogengine.ObjectsManagers.Add(ocrobjmanager);
                        if (rasterImageToMatch == null)
                        {
                            rasterImageToMatch = RasterImageConverter.ConvertFromImage(imagetomatch, ConvertFromImageOptions.None);// formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                            formattributes = formrecogengine.CreateForm(null);
                            IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
                            ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                            // int deskew = GetDeskewAngle(ocrpage);
                            int rotation = GetRotationAngle(ocrpage);
                            rasterImageToMatch = RotateImage(rotation, rasterImageToMatch);
                            rasterImageToMatch = DeskewImage(rasterImageToMatch);
                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                            {
                                rasterImageToMatch.Page = i + 1;
                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                            }
                            formrecogengine.CloseForm(formattributes);
                        }
                        else
                        {
                            formattributes = formrecogengine.CreateForm(null);
                            IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
                            ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                            {
                                rasterImageToMatch.Page = i + 1;
                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                            }
                            formrecogengine.CloseForm(formattributes);
                        }

                        //string resultMessage = "The form could not be recognized";
                        FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();
                        string masterFileName = Path.Combine(LeadToolsTemplatesDir, masterFileNametemplate.Id + ".bin");
                        if (!(File.Exists(masterFileName)))
                        {
                            //exception 
                            Exception inex = new Exception("The following file does not exist in the directory: " + masterFileName.ToString());
                            Exception ex = new Exception("File does not Exist in the directory");
                            throw ex;
                        }
                        masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
                        FormRecognitionResult recognitionResult = formrecogengine.CompareForm(masterFormAttributes, formattributes, null);
                        if (recognitionResult.Confidence >= ((int)OcrConfidence.MinimumOCRConfidence))
                        {
                            if (!(createtext == ""))
                            {
                                createtext += "<br><br>";
                            }
                            string MasterFormXmlFileWithFields = masterFileName.Replace(".bin", ".xml");
                            ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                            confiedenceObj.Template = masterFileNametemplate;
                            confiedenceObj.MasterFormAttributes = masterFormAttributes;
                            confiedenceObj.FormAttributes = formattributes;
                            confiedenceObj.Confidence = recognitionResult.Confidence;
                            ConfidenceMatchedtemplates.Add(confiedenceObj);
                        }
                        //else if ((recognitionResult.Confidence >= ((int)Enties.OcrConfidence.MinimumRecognitionConfidence))&&(recognitionResult.Confidence < ((int)Enties.OcrConfidence.MinimumOCRConfidence)))
                        //{ 
                        //    ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                        //    confiedenceObj.Template = masterFileNametemplate;
                        //    confiedenceObj.MasterFormAttributes = masterFormAttributes;
                        //    confiedenceObj.FormAttributes = formattributes;
                        //    confiedenceObj.Confidence = recognitionResult.Confidence;
                        //    LowConfidenceMatchedtemplates.Add(confiedenceObj);
                        //}
                    }
                    List<ConfidenceMatchedTemplates> Matchedtemplates = null;
                    if (ConfidenceMatchedtemplates.Count > 0)
                    {
                        result = TemplatesConfidenceMatched(tenantUserSession, ConfidenceMatchedtemplates, documentid, rasterImageToMatch, out Matchedtemplates, ref ocrresultinfo, out exception);
                    }

                    if (exception != null)
                    {
                        throw exception;
                    }

                    //===========================
                    #region Update DocumentState
                    //===========================

                    ///<summary>
                    ///Condition: Definition
                    ///1) Checks if matched template is less than or equals to 0 the document state will be unmatched
                    ///2) Checks if matched template is greater than 0  and result returned is false. False is return when multiple templates are matched. Document state will be matched multiple
                    ///3) Checks if result returned is true.True is return when a single template is matched.  Document state will be matched.
                    /// </summary>
                    if ((ConfidenceMatchedtemplates.Count <= 0)) //((LowConfidenceMatchedtemplates.Count > 0) && (result==false))
                    {
                        var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                        bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult)
                        {
                            ocrresultinfo.DoucmentState = DocumentState.UnMatched;
                            document.State = DocumentState.UnMatched;
                            result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }
                    else if ((ConfidenceMatchedtemplates.Count > 0) && (result == false))
                    {
                        var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                        bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult)
                        {
                            ocrresultinfo.DoucmentState = DocumentState.MatchedMultiple;
                            document.State = DocumentState.MatchedMultiple;
                            result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }
                    else if (result)
                    {
                        var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                        bool dbresult = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (dbresult)
                        {
                            ocrresultinfo.DoucmentState = DocumentState.Matched;
                            document.State = DocumentState.Matched;
                            result = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }
                    //==============================
                    #endregion Update DocumentState
                    //==============================
                }
                else
                {
                    Exception inex = new Exception("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
                    Exception ex = new Exception("RasterSupport Is Locked", inex);
                    throw ex;
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ocrengine.IsStarted)
                {
                    ocrengine.Shutdown();
                    ocrengine.Dispose();
                }

                if (imagetomatch != null)
                {
                    imagetomatch.Dispose();
                }
                if (!(ocrDocument.IsInMemory))
                {
                    ocrDocument.Dispose();
                }
                if (ocrpage != null)
                {
                    ocrpage.Dispose();
                }
                result = true;
                exception = ex;
                return result;
            }
        }
        public bool ProccessSingleMatchedDocument(TenantUserSession tenantUserSession, ConfidenceMatchedTemplates matchedtemplate, RasterImage rasterimagetomatch, long documentid, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                bool returnedreslt = StartRecogniction(tenantUserSession, matchedtemplate, rasterimagetomatch, documentid, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (returnedreslt)
                {
                    //BusinessLayer.DocumentManagement.UpdateDocument()
                    var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                    returnedreslt = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (returnedreslt)
                    {
                        document.TemplateId = matchedtemplate.Template.Id;
                        document.Confidence = matchedtemplate.Confidence;
                        returnedreslt = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (returnedreslt)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public bool ProccessSingleMatchedDocument(TenantUserSession tenantUserSession, ConfidenceMatchedTemplates matchedtemplate, RasterImage rasterimagetomatch, long documentid, ref OcrResultInfo ocrresultinfo, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                bool returnedreslt = StartRecogniction(tenantUserSession, matchedtemplate, rasterimagetomatch, documentid, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (returnedreslt)
                {
                    //BusinessLayer.DocumentManagement.UpdateDocument()
                    var document = new HouseOfSynergy.AffinityDms.Entities.Tenants.Document();
                    returnedreslt = DocumentManagement.GetDocumentById(tenantUserSession, documentid, out document, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (returnedreslt)
                    {
                        document.TemplateId = matchedtemplate.Template.Id;
                        document.Confidence = matchedtemplate.Confidence;
                        returnedreslt = DocumentManagement.UpdateDocument(tenantUserSession, document, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (returnedreslt)
                        {
                            MatchedTemplates item = new MatchedTemplates();
                            item.TemplateId = matchedtemplate.Template.Id;
                            item.Confidence = matchedtemplate.Confidence;
                            ocrresultinfo.MatchedTemplates = new List<MatchedTemplates>();
                            ocrresultinfo.MatchedTemplates.Add(item);
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public bool TemplatesConfidenceMatched(TenantUserSession tenantUserSession, List<ConfidenceMatchedTemplates> confidencematchedtemplates, long documentid, RasterImage rasterimagetomatch, out List<ConfidenceMatchedTemplates> matchedtemplates, out Exception exception)
        {
            exception = null;
            matchedtemplates = null;
            bool result = false;
            try
            {
                if (confidencematchedtemplates.Count == 1)
                {
                    result = ProccessSingleMatchedDocument(tenantUserSession, confidencematchedtemplates.First(), rasterimagetomatch, documentid, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                else if (confidencematchedtemplates.Count > 1)
                {
                    ConfidenceMatchedTemplates maxconfidencetemplate = null;
                    bool foundmaxconfidencetemplate = false;
                    foreach (var confidencematchedtemplate in confidencematchedtemplates)
                    {
                        if (maxconfidencetemplate == null)
                        {
                            maxconfidencetemplate = new ConfidenceMatchedTemplates();
                            maxconfidencetemplate = confidencematchedtemplate;
                            foundmaxconfidencetemplate = true;
                        }
                        else
                        {
                            if (maxconfidencetemplate.Confidence < confidencematchedtemplate.Confidence)
                            {
                                maxconfidencetemplate = confidencematchedtemplate;
                                foundmaxconfidencetemplate = true;
                            }
                            else if (maxconfidencetemplate.Confidence == confidencematchedtemplate.Confidence)
                            {
                                foundmaxconfidencetemplate = false;
                            }
                        }
                    }
                    if (foundmaxconfidencetemplate)
                    {

                        bool dbresult = ProccessSingleMatchedDocument(tenantUserSession, maxconfidencetemplate, rasterimagetomatch, documentid, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                    }
                    else
                    {
                        foreach (var confidencematchedtemplate in confidencematchedtemplates)
                        {
                            var documenttemplate = new DocumentTemplate();
                            documenttemplate.DocumentId = documentid;
                            documenttemplate.TemplateId = confidencematchedtemplate.Template.Id;
                            documenttemplate.Confidence = confidencematchedtemplate.Confidence;
                            bool dbresult = DocumentManagement.AddDocumentTemplate(tenantUserSession, documenttemplate, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }
                    result = foundmaxconfidencetemplate;
                }
                else
                {
                    result = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                return result;
            }
        }
        public bool TemplatesConfidenceMatched(TenantUserSession tenantUserSession, List<ConfidenceMatchedTemplates> confidencematchedtemplates, long documentid, RasterImage rasterimagetomatch, out List<ConfidenceMatchedTemplates> matchedtemplates, ref OcrResultInfo ocrresultinfo, out Exception exception)
        {
            exception = null;
            matchedtemplates = null;
            bool result = false;
            try
            {
                if (confidencematchedtemplates.Count == 1)
                {
                    result = ProccessSingleMatchedDocument(tenantUserSession, confidencematchedtemplates.First(), rasterimagetomatch, documentid, ref ocrresultinfo, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                else if (confidencematchedtemplates.Count > 1)
                {
                    ConfidenceMatchedTemplates maxconfidencetemplate = null;
                    bool foundmaxconfidencetemplate = false;
                    foreach (var confidencematchedtemplate in confidencematchedtemplates)
                    {
                        if (maxconfidencetemplate == null)
                        {
                            maxconfidencetemplate = new ConfidenceMatchedTemplates();
                            maxconfidencetemplate = confidencematchedtemplate;
                            foundmaxconfidencetemplate = true;
                        }
                        else
                        {
                            if (maxconfidencetemplate.Confidence < confidencematchedtemplate.Confidence)
                            {
                                maxconfidencetemplate = confidencematchedtemplate;
                                foundmaxconfidencetemplate = true;
                            }
                            else if (maxconfidencetemplate.Confidence == confidencematchedtemplate.Confidence)
                            {
                                foundmaxconfidencetemplate = false;
                            }
                        }
                    }
                    if (foundmaxconfidencetemplate)
                    {

                        bool dbresult = ProccessSingleMatchedDocument(tenantUserSession, maxconfidencetemplate, rasterimagetomatch, documentid, ref ocrresultinfo, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                    }
                    else
                    {
                        foreach (var confidencematchedtemplate in confidencematchedtemplates)
                        {
                            var documenttemplate = new DocumentTemplate();
                            documenttemplate.DocumentId = documentid;
                            documenttemplate.TemplateId = confidencematchedtemplate.Template.Id;
                            documenttemplate.Confidence = confidencematchedtemplate.Confidence;
                            bool dbresult = DocumentManagement.AddDocumentTemplate(tenantUserSession, documenttemplate, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                            if (dbresult)
                            {
                                MatchedTemplates item = new MatchedTemplates();
                                item.TemplateId = confidencematchedtemplate.Template.Id;
                                item.Confidence = confidencematchedtemplate.Confidence;
                                ocrresultinfo.MatchedTemplates = new List<MatchedTemplates>();
                                ocrresultinfo.MatchedTemplates.Add(item);
                            }
                        }
                    }
                    result = foundmaxconfidencetemplate;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                return result;
            }
        }

        private bool StartRecogniction(TenantUserSession tenantUserSession, ConfidenceMatchedTemplates confidencematchedtemplates, RasterImage rasterimagetomatch, long documentid, out Exception exception)
        {
            List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates>();
            exception = null;
            bool result = false;
            bool returnedresult = false;
            try
            {
                string MasterFormXmlFileWithFields = Path.Combine(LeadToolsTemplatesDir, confidencematchedtemplates.Template.Id + ".xml");
                if (!(File.Exists(MasterFormXmlFileWithFields)))
                {
                    Exception inex = new Exception("Unable to find the Template: " + MasterFormXmlFileWithFields);
                    Exception ex = new Exception("Template Not Found", inex);
                    throw ex;
                }
                formproccessingengine = new FormProcessingEngine();
                LoadEngine(string.Empty, null, null, null);
                formproccessingengine.OcrEngine = ocrengine;
                formproccessingengine.OcrEngine.Startup(formsCodec, null, null, LeadtoolsAdvantageRuntimeDir);
                formproccessingengine.LoadFields(MasterFormXmlFileWithFields);
                IList<PageAlignment> pagealignmentlist = formrecogengine.GetFormAlignment(confidencematchedtemplates.MasterFormAttributes, confidencematchedtemplates.FormAttributes, null, null);
                formproccessingengine.Process(rasterimagetomatch, pagealignmentlist);
                FormPages ProcessingPages = formproccessingengine.Pages;
                List<TemplateElement> templateelements;
                returnedresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, confidencematchedtemplates.Template.Id, out templateelements, out exception);
                //                    List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates>();
                // ConfidenceMatchedTemplates elementocrtemplate;
                if (exception != null)
                {
                    throw exception;
                }
                if (ProcessingPages != null)
                {
                    //string coltext = "";
                    //string rowtext = "";

                    for (int i = 0; i < ProcessingPages.Count; i++)
                    {
                        for (int j = 0; j < ProcessingPages[i].Count; j++)
                        {
                            FormField formfield = ProcessingPages[i][j];
                            if (formfield is TableFormField)
                            {
                                TableFormField tableformfield = ProcessingPages[i][j] as TableFormField;
                                long tableid = long.Parse(tableformfield.Name);
                                TableFormFieldResult results = tableformfield.Result as TableFormFieldResult;
                                if (results != null && results.Rows != null)
                                {
                                    for (int x = 0; x < results.Rows.Count; x++)
                                    {
                                        TableFormRow row = results.Rows[x];
                                        for (int y = 0; y < row.Fields.Count; y++)
                                        {
                                            OcrFormField ocrField = row.Fields[y];
                                            if (ocrField is TextFormField)
                                            {
                                                TextFormFieldResult txtResults = ocrField.Result as TextFormFieldResult;
                                                var documentelement = new DocumentElement();
                                                documentelement.TemplateElementId = tableid;
                                                documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
                                                documentelement.OcrText = txtResults.Text;
                                                documentelement.DocumentId = documentid;
                                                bool returnresult = DocumentManagement.AddDocumentElement(tenantUserSession, documentelement, out exception);
                                                if (exception != null)
                                                {
                                                    throw exception;
                                                }
                                            }
                                            else if (ocrField is OmrFormField)
                                            {
                                                OmrFormFieldResult omrResults = ocrField.Result as OmrFormFieldResult;
                                                var documentelement = new DocumentElement();
                                                documentelement.TemplateElementId = tableid;
                                                documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
                                                documentelement.OcrText = omrResults.Text;
                                                documentelement.DocumentId = documentid;
                                                bool returnresult = DocumentManagement.AddDocumentElement(tenantUserSession, documentelement, out exception);
                                                if (exception != null)
                                                {
                                                    throw exception;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (formfield is TextFormField)
                            {
                                var fieldtext = ((formfield as TextFormField).Result as TextFormFieldResult).Text;
                                var documentelement = new DocumentElement();
                                documentelement.TemplateElementId = long.Parse(formfield.Name);
                                documentelement.OcrText = fieldtext;
                                documentelement.DocumentId = documentid;
                                bool returnresult = DocumentManagement.AddDocumentElement(tenantUserSession, documentelement, out exception);
                                if (exception != null)
                                {
                                    throw exception;
                                }


                            }
                            else if (formfield is ImageFormField)
                            {
                                var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
                                var documentelement = new DocumentElement();
                                documentelement.TemplateElementId = long.Parse(formfield.Name);
                                documentelement.OcrText = fieldtext;
                                documentelement.DocumentId = documentid;
                                bool returnresult = DocumentManagement.AddDocumentElement(tenantUserSession, documentelement, out exception);
                                if (exception != null)
                                {
                                    throw exception;
                                }
                            }
                        }
                    }
                }
                result = true;
                return result;

            }
            catch (Exception ex)
            {
                exception = ex;
                return result;
            }
        }
        #endregion WORKING TEMPLATE MATCHING NEW







        //===============
        #region Barcode
        //===============
        public bool ReadBarcode(Image img, out List<LeadtoolsBarcodeData> barcodedata, out Exception exception)
        {
            bool result = false;
            barcodedata = null;
            exception = null;
            try
            {
                BarcodeEngine barcodeengine = new BarcodeEngine();
                BarcodeReader barcodereader = barcodeengine.Reader;
                RasterCodecs rastercodecs = new RasterCodecs();
                RasterImage rasterimge = Leadtools.Drawing.RasterImageConverter.ConvertFromImage(img, ConvertFromImageOptions.None);
                BarcodeData[] bdata = barcodereader.ReadBarcodes(rasterimge, LogicalRectangle.Empty, 0, null);

                foreach (var item in bdata)
                {
                    LeadtoolsBarcodeData leadtoolsbarcodedata = new LeadtoolsBarcodeData();
                    Rectangle rect = new Rectangle((int)item.Bounds.X, (int)item.Bounds.Y, (int)item.Bounds.Width, (int)item.Bounds.Height);
                    leadtoolsbarcodedata.Bounds = rect;
                    leadtoolsbarcodedata.RotationAngle = item.RotationAngle;
                    leadtoolsbarcodedata.Symbology = item.Symbology.ToString();
                    leadtoolsbarcodedata.Tag = item.Tag;
                    leadtoolsbarcodedata.Value = item.Value;
                    barcodedata.Add(leadtoolsbarcodedata);
                }
                if (barcodedata != null && barcodedata.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public bool WriteBarcode(Image img, string value, out Image barcodeimage, out Exception exception)
        {
            exception = null;
            bool result = false;
            barcodeimage = null;
            try
            {
                BarcodeEngine barcodeengine = new BarcodeEngine();
                BarcodeWriter barcodeWriter = barcodeengine.Writer;
                int Width = 0, Height = 0;
                Image BarcodeImage = new Bitmap(Width, Height);
                QRBarcodeData data = new QRBarcodeData();
                data.Symbology = BarcodeSymbology.QR;
                data.Value = value;
                data.Bounds = new LogicalRectangle(0, 0, 100, 100, LogicalUnit.Pixel);
                QRBarcodeWriteOptions writeOptions = new QRBarcodeWriteOptions();
                writeOptions.BackColor = RasterColor.White;
                writeOptions.ForeColor = RasterColor.Black;
                // Set X Module
                //writeOptions.XModule = 30;
                RasterImage barcodeRasterImage = RasterImageConverter.ConvertFromImage(BarcodeImage, ConvertFromImageOptions.None);
                barcodeWriter.WriteBarcode(barcodeRasterImage, data, writeOptions);
                barcodeimage = RasterImageConverter.ChangeToImage(barcodeRasterImage, ChangeToImageOptions.None);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            return result;
        }

        //===============
        #endregion Barcode
        //===============


        /**/




















        ////TemplateMatachingData tempdata;
        //public string DoTemplateMatching(Image imagetomatch, string fullfilenamewidthExtention)
        //{
        //    //  tempdata = new TemplateMatachingData();
        //    try
        //    {
        //        if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
        //        {
        //            //formrecogengine = new FormRecognitionEngine();
        //            //formsCodec = new RasterCodecs();
        //            //LoadEngine(null, formsCodec, null, null);
        //            //ocrobjmanager = new OcrObjectsManager(ocrengine);
        //            //ocrobjmanager.Engine = ocrengine;
        //            //formrecogengine.ObjectsManagers.Add(ocrobjmanager);
        //            //RasterImage rasterImageToMatch = RasterImageConverter.ConvertFromImage(imagetomatch, ConvertFromImageOptions.None);// formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
        //            //formattributes = formrecogengine.CreateForm(null);
        //            //IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
        //            //ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
        //            //// int deskew = GetDeskewAngle(ocrpage);
        //            //int rotation = GetRotationAngle(ocrpage);
        //            //rasterImageToMatch = RotateImage(rotation, rasterImageToMatch);
        //            //rasterImageToMatch = DeskewImage(rasterImageToMatch);
        //                    ////cleanimage(1, rasterImage.PageCount);
        //                    ////RotateCommand command = new RotateCommand();
        //                    ////command.Angle = 45 * 100;
        //                    ////command.FillColor = new RasterColor(255, 255, 255);
        //                    ////command.Flags = RotateCommandFlags.Bicubic;
        //                    ////command.Run(rasterImage);


        //            string[] masterFileNames = GetMasterfileName("bin");
        //             RasterImage rasterImageToMatch = null;
        //            foreach (string masterFileName in masterFileNames)
        //            {
        //                RasterSupport.SetLicense(LICENSE_FILE, LICENSE_KEY);
        //                formrecogengine = new FormRecognitionEngine();
        //                formsCodec = new RasterCodecs();
        //                LoadEngine(null, formsCodec, null, null);
        //                ocrobjmanager = new OcrObjectsManager(ocrengine);
        //                ocrobjmanager.Engine = ocrengine;
        //                formrecogengine.ObjectsManagers.Add(ocrobjmanager);
        //                if (rasterImageToMatch == null)
        //                {
        //                    rasterImageToMatch = RasterImageConverter.ConvertFromImage(imagetomatch, ConvertFromImageOptions.None);// formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
        //                    formattributes = formrecogengine.CreateForm(null);
        //                    IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
        //                    ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
        //                    // int deskew = GetDeskewAngle(ocrpage);
        //                    int rotation = GetRotationAngle(ocrpage);
        //                    rasterImageToMatch = RotateImage(rotation, rasterImageToMatch);
        //                    rasterImageToMatch = DeskewImage(rasterImageToMatch);
        //                    for (int i = 0; i < rasterImageToMatch.PageCount; i++)
        //                    {
        //                        rasterImageToMatch.Page = i + 1;
        //                        formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
        //                    }
        //                    formrecogengine.CloseForm(formattributes);
        //                }
        //                else
        //                {
        //                    formattributes = formrecogengine.CreateForm(null);
        //                    IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
        //                    ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
        //                    for (int i = 0; i < rasterImageToMatch.PageCount; i++)
        //                    {
        //                        rasterImageToMatch.Page = i + 1;
        //                        formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
        //                    }
        //                    formrecogengine.CloseForm(formattributes);
        //                }

        //                string resultMessage = "The form could not be recognized";
        //                FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();
        //                masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
        //                FormRecognitionResult recognitionResult = formrecogengine.CompareForm(masterFormAttributes, formattributes, null);
        //                if (recognitionResult.Confidence >= 80)
        //                {
        //                    if (!(createtext == ""))
        //                    {
        //                        createtext += "<br><br>";
        //                    }
        //                    createtext += "Results Matched With Template: " + Path.GetFileNameWithoutExtension(masterFileName) + "<br>Template Confidence: " + recognitionResult.Confidence + "<br>Recognition Reason: " + recognitionResult.Reason.ToString() + "<br><br>";
        //                    StartRecogniction(rasterImageToMatch, fullfilenamewidthExtention);
        //                    resultMessage = String.Format("This form has been recognized as a {0}  : {1} ", Path.GetFileNameWithoutExtension(masterFileName), recognitionResult.Reason.ToString());
        //                    if (ocrengine != null && ocrengine.IsStarted)
        //                        ocrengine.Shutdown();
        //                    //break;
        //                }
        //                else
        //                {
        //                    createtext += "Template Not matched ( " + Path.GetFileNameWithoutExtension(masterFileName) + ") : Template Confidence was "+recognitionResult.Confidence+"<br> ";
        //                }
        //            }
        //        }
        //        //if (ocrengine != null && ocrengine.IsStarted)
        //        //{
        //        //    if (createtext != null || tablelist != null)
        //        //    {
        //        //        tempdata.rasterimage = rasterImageToMatch;
        //        //        tempdata.Text = createtext;
        //        //        tempdata.Tables = tablelist;
        //        //        tempdata.distinctcolumns = distinctcolnames;
        //        //        tempdata.imagelist = imglist;
        //        //    }
        //        //    ocrengine.Shutdown();
        //        //    return tempdata;
        //        //}
        //        //else
        //        //{
        //        //    return null;
        //        //}
        //        return createtext;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ("Error: "+ex.Message);
        //        // GetExceptions(ex, null);
        //        // return null;
        //    }
        //}






        //private void StartRecogniction(RasterImage rasterImageToMatch, string fullfilename)
        //{

        //    formsCodec = new RasterCodecs();
        //    barcodeengine = new BarcodeEngine();
        //    formsRepository = new DiskMasterFormsRepository(formsCodec, LeadToolsTemplatesDir);
        //    LoadEngine(null, formsCodec, null, null);
        //    // IOcrDocument doc = ocrengine.DocumentManager.CreateDocument();
        //    // ocrpage = doc.Pages.AddPage(rasterImage, null);
        //    //// int deskew = GetDeskewAngle(ocrpage);
        //    // int rotation = GetRotationAngle(ocrpage);
        //    // RotateImage(rotation);
        //    // rasterImage= DeskewImage(rasterImage);
        //    //autoformrecogmanager = AutoFormsRecognitionManager.Ocr;
        //    autoEngine = new AutoFormsEngine(formsRepository, ocrengine, barcodeengine);
        //    FilledForm form = new FilledForm();
        //    form.FileName = Path.GetFileName(fullfilename);
        //    form.Name = Path.GetFileNameWithoutExtension(fullfilename);
        //    form.Image = rasterImageToMatch; //formsCodec.Load(imagepath, 0, CodecsLoadByteOrder.Bgr, 1, 1);
        //    AutoFormsRunResult autoformrunres = autoEngine.Run(rasterImageToMatch, null, form, null);
        //    //FormPages formpages;
        //    if (autoformrunres != null)
        //    {
        //        form.Properties = autoformrunres.RecognitionResult.Properties;
        //        form.Result = autoformrunres.RecognitionResult.Result;
        //        form.Alignment = autoformrunres.RecognitionResult.Alignments;
        //        form.ProcessingPages = autoformrunres.RecognitionResult.FormPages;
        //        form.MasterForm = autoformrunres.RecognitionResult.MasterForm;
        //        //var tableResults = "";
        //        string coltext = "";
        //        string tabletext = "";
        //        string rowtext = "";
        //        for (int i = 0; i < form.ProcessingPages.Count; i++)
        //        {
        //            for (int j = 0; j < form.ProcessingPages[i].Count; j++)
        //            {
        //                FormField formfield = form.ProcessingPages[i][j];
        //                if (formfield is TableFormField)
        //                {
        //                    TableFormField tableformfield = form.ProcessingPages[i][j] as TableFormField;
        //                    tabletext += "<table>" + tableformfield.Name;
        //                    TableFormFieldResult results = tableformfield.Result as TableFormFieldResult;
        //                    if (results != null && results.Rows != null)
        //                    {

        //                        for (int x = 0; x < results.Rows.Count; x++)
        //                        {
        //                            TableFormRow row = results.Rows[x];
        //                            rowtext += "<tr>";
        //                            //int lineCounter = 1;
        //                            for (int y = 0; y < row.Fields.Count; y++)
        //                            {
        //                                OcrFormField ocrField = row.Fields[y];
        //                                if (ocrField is TextFormField)
        //                                {
        //                                    TextFormFieldResult txtResults = ocrField.Result as TextFormFieldResult;
        //                                    if (coltext == "")
        //                                    {
        //                                        distinctcolnames.Add(ocrField.Name);
        //                                        coltext = "<tr><th>" + ocrField.Name + "</th>";
        //                                    }
        //                                    else
        //                                    {
        //                                        if (!(distinctcolnames.Contains(ocrField.Name)))
        //                                        {
        //                                            distinctcolnames.Add(ocrField.Name);
        //                                            coltext += "<th>" + ocrField.Name + "</th>";
        //                                        }
        //                                    }

        //                                    //tabledetails.Add(ocrField.Name);
        //                                    rowtext += "<td>" + txtResults.Text + "</td>";
        //                                    //tabledetails.Add(txtResults.Text);
        //                                }
        //                                else if (ocrField is OmrFormField)
        //                                {
        //                                    OmrFormFieldResult omrResults = ocrField.Result as OmrFormFieldResult;
        //                                    //tabledetails.Add(omrResults.Text);
        //                                    //tabledetails.Add(ocrField.Name);
        //                                }
        //                            }
        //                            rowtext += "</tr>";
        //                            coltext += "</tr>";
        //                        }
        //                    }

        //                    tabletext += coltext + rowtext + "</table>";
        //                    tablelist.Add(tabletext);
        //                    coltext = "";
        //                    rowtext = "";
        //                    distinctcolnames.Clear();

        //                    //if (tableformfield.Result.Status == FormFieldStatus.Success)
        //                    //{
        //                    //    foreach (TableColumn tablecolumn in tableformfield.Columns)
        //                    //    {
        //                    //        colfieldsname.Add("colfieldsname "+tablecolumn.OcrField.Name);
        //                    //        var aa = tablecolumn.OcrField.Bounds.Top;
        //                    //        var aaa = tablecolumn.OcrField.Bounds.Left;
        //                    //        var aaaa = tablecolumn.OcrField.Bounds.Width;
        //                    //        var aaaaa = tablecolumn.OcrField.Bounds.Height;
        //                    //        TableFormFieldResult tableformfieldres = tableformfield.Result as TableFormFieldResult;
        //                    //        for (int x = 0; x < tableformfieldres.Rows.Count; x++)
        //                    //        {
        //                    //            TableFormRow tableformrow = tableformfieldres.Rows[i];
        //                    //            colfieldsname.Add("tableformrow I "+i+" : "+tableformrow.Fields[i].Name.ToString());
        //                    //            for (int y = 0; y < tableformrow.Fields.Count; y++)
        //                    //            {
        //                    //                OcrFormField ocrformfield = tableformrow.Fields[j];
        //                    //                colfieldsname.Add("tableformrow J "+i+" : "+ j +": "+tableformrow.Fields[j].Name.ToString());
        //                    //               // ((tableformrow.Fields[j] as TextFormFieldResult).Result as TextFormFieldResult).Text;
        //                    //                var a = ocrformfield.Result.ToString();

        //                    //                TextFormFieldResult textformfieldresult = ocrformfield.Result as TextFormFieldResult;
        //                    //                //var aaaaaaa = ocrformfield.Bounds.Top;
        //                    //                //var aaaaaaaa = ocrformfield.Bounds.Left;
        //                    //                //var aaaaaaaaa = ocrformfield.Bounds.Width;
        //                    //                //var aaaaaaaaaa = ocrformfield.Bounds.Height;

        //                    //                colfieldsname.Add("textformfieldresult "+i+" : "+ j +": "+textformfieldresult.Text);
        //                    //            }
        //                    //        }

        //                    //    }
        //                    //}
        //                    // tablelist.Add(tabledetails);
        //                }
        //                else if (formfield is TextFormField)
        //                {
        //                    var fieldtext = ((formfield as TextFormField).Result as TextFormFieldResult).Text;
        //                    createtext += "Field Name: " + formfield.Name + "<br>";
        //                    createtext += "Text: " + fieldtext;
        //                    var fieldtextconfi = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence.ToString();
        //                    createtext += "Field Confidence: " + fieldtextconfi + "<br>";
        //                    createtext += "TOP: " + formfield.Bounds.Top + "<br>";
        //                    createtext += "LEFT: " + formfield.Bounds.Left + "<br>";
        //                    createtext += "Width: " + formfield.Bounds.Width + "<br>";
        //                    createtext += "Height: " + formfield.Bounds.Height + "<br><br>";


        //                }
        //                else if (formfield is ImageFormField)
        //                {
        //                    var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
        //                    createtext += "Field Name: " + formfield.Name + "<br>";
        //                    createtext += "Imagee:<br>";
        //                    var img = ((formfield as ImageFormField).Result as ImageFormFieldResult).Image;
        //                    Image destImage = RasterImageConverter.ChangeToImage(img, ChangeToImageOptions.ForceChange);
        //                    imglist.Add(destImage);
        //                    var fieldtextw = ((formfield as ImageFormField).Result as ImageFormFieldResult).Image.Width;
        //                    createtext += "Width: " + fieldtextw + "<br>";
        //                    var fieldtexth = ((formfield as ImageFormField).Result as ImageFormFieldResult).Image.Height;
        //                    createtext += "Height: " + fieldtexth + "<br>";
        //                }
        //            }
        //        }
        //    }
        //}

    }
    //public class ConfidenceMatchedTemplates
    //{
    //    public Template Template { get; set; }
    //    public FormRecognitionAttributes MasterFormAttributes { get; set; }
    //    public FormRecognitionAttributes FormAttributes { get; set; }
    //    public int? Confidence { get; set; }
    //    public string XMLOCR { get; set; }
    //    public string TEXTOCR { get; set; }
    //    public long? TemplateElementID { get; set; }
    //    public long? TemplateElementDetailId { get; set; }
    //    //public Enties.TemplateElement? TemplateElement { get; set; }
    //    //public Enties.TemplateElementDetail? TemplateElementDetail { get; set; }
    //}
}