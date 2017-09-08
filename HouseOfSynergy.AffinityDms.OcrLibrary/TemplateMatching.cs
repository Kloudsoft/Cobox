using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.PowerTools.Library.Log;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Drawing;
using Leadtools.Forms;
using Leadtools.Forms.Auto;
using Leadtools.Forms.Ocr;
using Leadtools.Forms.Processing;
using Leadtools.Forms.Recognition;
using Leadtools.Forms.Recognition.Ocr;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public sealed class TemplateMatching
    {
        //========================================
        #region Template Matching WorkerRole
        //========================================

        public bool DoTemplateMatching(Global global, OcrEngineSettings ocrEngineSettings, Tenant tenant, Image imagetomatch, Document inputdocument, List<Template> AllTemplates, List<TemplateElement> filterdTemplateElements, out Document matcheddocument, out List<DocumentElement> documentElements, out List<DocumentTemplate> documenttemplate, out Exception exception)
        {

            bool result = false;
            exception = null;
            matcheddocument = null;
            documenttemplate = null;
            documentElements = null;
            IOcrDocument doc = null;
            IOcrPage ocrpage = null;
            FormRecognitionEngine formrecogengine = null;
            try
            {
                global.Logger.Write("Start Template Matching");
                List<long> distinctFilteredTemplatesElementsTemplateId = filterdTemplateElements.Select(x => x.TemplateId).ToList().Distinct().ToList();
                List<Template> entiesTemplates = new List<Template>();
                List<ConfidenceMatchedTemplates> ConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
                RasterImage rasterImageToMatch = null;
                global.Logger.Write($"Found {distinctFilteredTemplatesElementsTemplateId.Count()} Distinct Template");
                using (var formsCodec = new RasterCodecs())
                {
                    using (var ocrengine = Engines.LoadEngine(formsCodec, null, null, ocrEngineSettings.PathRuntime.FullName))
                    {
                        foreach (var distincttemplateid in distinctFilteredTemplatesElementsTemplateId)
                        {
                            Template template = AllTemplates.Where(e => e.Id == distincttemplateid).Select(x => x).FirstOrDefault();
                            if (template != null)
                            {
                                entiesTemplates.Add(template);
                            }
                        }
                        foreach (var masterFileNametemplate in entiesTemplates)
                        {

                            formrecogengine = new FormRecognitionEngine();
                            var ocrobjmanager = new OcrObjectsManager(ocrengine);
                            ocrobjmanager.Engine = ocrengine;
                            formrecogengine.ObjectsManagers.Add(ocrobjmanager);
                            var formattributes = formrecogengine.CreateForm(null);
                            doc = ocrengine.DocumentManager.CreateDocument();
                            if (rasterImageToMatch == null)
                            {
                                // formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                                rasterImageToMatch = RasterImageConverter.ConvertFromImage(((Image)imagetomatch.Clone()), ConvertFromImageOptions.None);
                                ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                                int rotation = HelperMethods.GetRotationAngle(ocrpage);
                                rasterImageToMatch = HelperMethods.RotateImage(rotation, rasterImageToMatch);
                                rasterImageToMatch = HelperMethods.DeskewImage(rasterImageToMatch);
                            }
                            else
                            {
                                ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
                            }

                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
                            {
                                rasterImageToMatch.Page = i + 1;
                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
                            }
                            formrecogengine.CloseForm(formattributes);

                            //string resultMessage = "The form could not be recognized";
                            FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();

                            //CHANGEPATH :  Need Path To The Template Files.   Line 100 Can take ByteArray too. (masterFileName) 
                            using (var azureCloudStorageAccountHelper = new AzureCloudStorageAccountHelper(tenant))
                            {
                                try
                                {
                                    var fileBin = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".bin"));
                                    var fileXml = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml"));
                                    var fileTiff = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".tif"));
                                    azureCloudStorageAccountHelper.Download(masterFileNametemplate, fileBin, fileXml, fileTiff);
                                }
                                catch (Exception ex)
                                {
                                    global.Logger.Write($"Exception :: unable to find (BIN XML TIFF) Files: {ex.ToString()}");
                                    throw;
                                }
                            }
                            string masterFileName = Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".bin");
                            if (!(File.Exists(masterFileName)))
                            {
                                //exception 
                                Exception inex = new Exception("The following file does not exist in the directory: " + masterFileName.ToString());
                                Exception ex = new Exception("File does not Exist in the directory", inex);
                                global.Logger.Write($"Exception :: Unable to find (BIN/XML/TIFF) Files: {ex.ToString()}");

                                throw ex;
                            }
                            masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
                            FormRecognitionResult recognitionResult = formrecogengine.CompareForm(masterFormAttributes, formattributes, null);
                            if (inputdocument.DocumentQueueType == DocumentQueueType.Manual)
                            {
                                ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                                confiedenceObj.Template = masterFileNametemplate;
                                confiedenceObj.MasterFormAttributes = masterFormAttributes;
                                confiedenceObj.FormAttributes = formattributes;
                                confiedenceObj.Confidence = recognitionResult.Confidence;
                                ConfidenceMatchedtemplates.Add(confiedenceObj);
                            }
                            else
                            {
                                if (recognitionResult.Confidence >= ((int)OcrConfidence.MinimumRecognitionConfidence))
                                {
                                    //string MasterFormXmlFileWithFields = masterFileName.Replace (".bin", ".xml");
                                    ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
                                    confiedenceObj.Template = masterFileNametemplate;
                                    confiedenceObj.MasterFormAttributes = masterFormAttributes;
                                    confiedenceObj.FormAttributes = formattributes;
                                    confiedenceObj.Confidence = recognitionResult.Confidence;
                                    ConfidenceMatchedtemplates.Add(confiedenceObj);
                                }
                            }

                        }
                    }
                }
                try
                {
                    ocrpage.Dispose();
                    formrecogengine = null;
                    doc.Dispose();
                }
                finally { }
                
                //ImageConverter ic = new ImageConverter();
                //var imgbyte = ic.ConvertTo(imagetomatch, typeof(byte[]));
                //Stream ImageStream = new MemoryStream((byte[])imgbyte);
                var ImageStream = new System.IO.MemoryStream();
                imagetomatch.Save(ImageStream, imagetomatch.RawFormat);
                ImageStream.Position = 0;
                List<ConfidenceMatchedTemplates> Matchedtemplates = null;
                if (ConfidenceMatchedtemplates.Count > 0)
                {
                    result = TemplatesConfidenceMatched(ocrEngineSettings, tenant, inputdocument, AllTemplates, ConfidenceMatchedtemplates, rasterImageToMatch, ImageStream, out Matchedtemplates, out matcheddocument, out documentElements, out documenttemplate, out exception);
                }

                if (exception != null)
                {
                    throw exception;
                }
                if (matcheddocument == null)
                {
                    matcheddocument = new Document();
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
                else if ((ConfidenceMatchedtemplates.Count > 0) && (!result))
                {
                    matcheddocument.State = DocumentState.MatchedMultiple;
                }
                else if (result)
                {
                    matcheddocument.State = DocumentState.Matched;
                }



                if ((matcheddocument.Confidence >= (int)OcrConfidence.MinimumRecognitionConfidence) && (matcheddocument.Confidence <= (int)OcrConfidence.MinimumOCRConfidence))
                {
                    matcheddocument.State = DocumentState.MatchedUnverified;
                }
                else if ((result && (matcheddocument.Confidence >= (int)OcrConfidence.MinimumOCRConfidence))|| (inputdocument.DocumentQueueType == DocumentQueueType.Manual))
                {
                    matcheddocument.State = DocumentState.Verified;
                }
                else //if (matcheddocument.Confidence < (int)OcrConfidence.MinimumRecognitionConfidence)
                {
                    matcheddocument.State = DocumentState.UnMatched;
                }


                //===========================
                #endregion Update DocumentState
                //===========================
                matcheddocument.Id = inputdocument.Id;
                global.Logger.Write($"End Template Matching");
                return result;


            }
            catch (Exception ex)
            {
                if (imagetomatch != null)
                {
                    imagetomatch.Dispose();
                }
                //if (!(ocrDocument.IsInMemory))
                //{
                //    ocrDocument.Dispose();
                //}
                if (ocrpage != null)
                {
                    ocrpage.Dispose();
                }
                result = true;
                exception = ex;
                global.Logger.Write($"Exception :: {ex.ToString()}");

                return result;
            }
        }


        public bool TemplatesConfidenceMatched(OcrEngineSettings ocrEngineSettings, Tenant tenant, Document inputdocument, List<Template> AllTemplates, List<ConfidenceMatchedTemplates> confidencematchedtemplates, RasterImage rasterimagetomatch, Stream ImageStream, out List<ConfidenceMatchedTemplates> matchedtemplates, out HouseOfSynergy.AffinityDms.Entities.Tenants.Document document, out List<DocumentElement> documentElements, out List<DocumentTemplate> documentTemplates, out Exception exception)
        {
            exception = null;
            matchedtemplates = null;
            document = new Document();
            documentElements = null;
            documentTemplates = new List<DocumentTemplate>();
            bool result = false;
            try
            {
                if (confidencematchedtemplates.Count == 1)
                {
                    result = ProccessSingleMatchedDocument(ocrEngineSettings, tenant, inputdocument, AllTemplates, confidencematchedtemplates.First(), rasterimagetomatch, ImageStream, out document, out documentElements, out exception);
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

                        bool dbresult = ProccessSingleMatchedDocument(ocrEngineSettings, tenant,  inputdocument, AllTemplates, confidencematchedtemplates.First(), rasterimagetomatch, ImageStream, out document, out documentElements, out exception);
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


        public bool ProccessSingleMatchedDocument(OcrEngineSettings ocrEngineSettings, Tenant tenant, Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates matchedtemplate, RasterImage rasterimagetomatch, Stream ImageStream, out Document document, out List<DocumentElement> documentElements, out Exception exception)
        {
            exception = null;
            document = new Entities.Tenants.Document();
            bool result = false;
            documentElements = null;
            try
            {

                result = StartRecogniction(ocrEngineSettings, tenant, inputdocument, AllTemplates, matchedtemplate, rasterimagetomatch, ImageStream, out documentElements, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (result)
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


        private bool StartRecognictionOLD(OcrEngineSettings ocrEngineSettings, Tenant tenant, IOcrEngine ocrengine, FormRecognitionEngine formrecogengine, Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates confidencematchedtemplates, RasterImage rasterimagetomatch, out List<DocumentElement> documentelements, out Exception exception)
        {
            List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates>();
            exception = null;
            documentelements = new List<DocumentElement>();
            bool result = false;
            // IOcrEngine engine = null;
            try
            {
                //CHANGEPATH :  Need Path To The Template Files.   Line 306 Can take Stream too. (MasterFormXmlFileWithFields) 
                string MasterFormXmlFileWithFields = Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, confidencematchedtemplates.Template.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml");
                if (!(File.Exists(MasterFormXmlFileWithFields)))
                {
                    Exception inex = new Exception("Unable to find the Template: " + MasterFormXmlFileWithFields);
                    Exception ex = new Exception("Template Not Found", inex);
                    throw ex;
                }
                var formproccessingengine = new FormProcessingEngine();
                //LoadEngine(string.Empty, null, null, null);
                if (!ocrengine.IsStarted)
                {
                    //var formsCodec = new RasterCodecs();
                    //ocrengine = Engines.LoadEngine(formsCodec, null, null, ocrEngineSettings.PathRuntime.FullName);
                    ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName);
                }
                //Engines.LoadEngine(null, null, ocrEngineSettings.WorkDirectory.FullName, ocrEngineSettings.PathRuntime.FullName, out ocrengine, out exception);
                formproccessingengine.OcrEngine = ocrengine;
                formproccessingengine.OcrEngine.Startup(ocrengine.RasterCodecsInstance, null, null, ocrEngineSettings.PathRuntime.FullName);
                formproccessingengine.LoadFields(MasterFormXmlFileWithFields);
                IList<PageAlignment> pagealignmentlist = formrecogengine.GetFormAlignment(confidencematchedtemplates.MasterFormAttributes, confidencematchedtemplates.FormAttributes, null, null);
                formproccessingengine.Process(rasterimagetomatch, pagealignmentlist);
                FormPages ProcessingPages = formproccessingengine.Pages;
                //  List<TemplateElement> templateelements = AllELements.Where(e => e.TemplateId == confidencematchedtemplates.Template.Id).ToList(); ;
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
                                                documentelement.Confidience = (ocrField.Result as TextFormFieldResult).AverageConfidence;
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
                                                documentelement.Confidience = (ocrField.Result as OmrFormFieldResult).AverageConfidence;
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
                                documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
                                documentelement.TemplateElementId = long.Parse(formfield.Name);
                                documentelement.OcrText = fieldtext;
                                documentelement.DocumentId = inputdocument.Id;
                                documentelements.Add(documentelement);
                            }
                            else if (formfield is ImageFormField)
                            {
                                var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
                                var documentelement = new DocumentElement();
                                documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
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


        private bool StartRecogniction(OcrEngineSettings ocrEngineSettings, Tenant tenant,  Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates confidencematchedtemplates, RasterImage rasterimagetomatch, Stream ImageStream, out List<DocumentElement> documentelements, out Exception exception)
        {
            List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates>();
            exception = null;
            documentelements = new List<DocumentElement>();
            bool result = false;
            try
            {
                //CHANGEPATH :  Need Path To The Template Files.   Line 306 Can take Stream too. (MasterFormXmlFileWithFields) 
                string MasterFormXmlFileWithFields = Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, confidencematchedtemplates.Template.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml");
                if (!(File.Exists(MasterFormXmlFileWithFields)))
                {
                    Exception inex = new Exception("Unable to find the Template: " + MasterFormXmlFileWithFields);
                    Exception ex = new Exception("Template Not Found", inex);
                    throw ex;
                }
                using (var codecs = new RasterCodecs())
                {
                    IOcrEngine ocrengine = null;
                    AutoFormsEngine _autoEngine;
                    AutoFormsRecognitionManager _manager = AutoFormsRecognitionManager.Ocr;
                    //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Temp")
                    ocrengine = Engines.LoadEngine(codecs, null, null , ocrEngineSettings.PathRuntime.FullName);
                    DiskMasterFormsRepository _Repository = new DiskMasterFormsRepository(codecs, Path.GetDirectoryName(MasterFormXmlFileWithFields));
                    _autoEngine = new AutoFormsEngine(_Repository, ocrengine, null, _manager);
                    List<IMasterFormsCategory> MasterCat = new List<IMasterFormsCategory>();
                    _autoEngine.RecognizeFirstPageOnly = true;
                    if (_Repository.RootCategory.MasterFormsCount > 0)
                    {
                        foreach (var item in _Repository.RootCategory.MasterForms.ToList())
                        {
                            if (item.Name != Path.GetFileNameWithoutExtension(MasterFormXmlFileWithFields))
                            {
                                item.Parent.MasterForms.Remove(item);
                            }
                        }
                    }
                    if (_Repository.RootCategory.MasterFormsCount > 0)
                    {
                        MasterCat.Add(_Repository.RootCategory);
                    }
                    AutoFormsRunResult _Results = new AutoFormsRunResult();
                    _Results = _autoEngine.Run(ImageStream, MasterCat);
                    if (_Results != null)
                    {
                        FormPages ProcessingPages = _Results.FormFields;
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
                                                        documentelement.Confidience = (ocrField.Result as TextFormFieldResult).AverageConfidence;
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
                                                        documentelement.Confidience = (ocrField.Result as OmrFormFieldResult).AverageConfidence;
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
                                        documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
                                        documentelement.TemplateElementId = long.Parse(formfield.Name);
                                        documentelement.OcrText = fieldtext;
                                        documentelement.DocumentId = inputdocument.Id;
                                        documentelements.Add(documentelement);
                                    }
                                    else if (formfield is ImageFormField)
                                    {
                                        var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
                                        var documentelement = new DocumentElement();
                                        documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
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
                }
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

        /*_*/

        //========================================
        #region Other Functions
        //========================================
        public bool GetFullTextOCR(OcrEngineSettings ocrEngineSettings, RasterImage rasterimage, out Exception exception, out string ocrtext)
        {
            var result = false;

            ocrtext = "";
            exception = null;

            try
            {
                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {
                    using (var ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName))
                    {
                        ocrengine.CreatePage(rasterimage, OcrImageSharingMode.AutoDispose);

                        using (var ocrDocument = ocrengine.DocumentManager.CreateDocument())
                        {
                            using (var ocrpage = ocrDocument.Pages.AddPage(rasterimage, null))
                            {
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

                                int zoneIndex = 0;

                                while (zoneIndex < ocrpage.Zones.Count())
                                {
                                    ocrtext += " " + ocrpage.GetText(zoneIndex++);
                                }
                            }
                        }
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
                exception = ex;
                return result;
            }
        }

        public bool GetFullTextXMLOCR(OcrEngineSettings ocrEngineSettings, RasterImage rasterimage, out Exception exception, out string xmldata)
        {
            var xml = "";
            var result = false;

            xmldata = "";
            exception = null;

            try
            {
                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
                {
                    using (var ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName))
                    {
                        using (var ocrDocument = ocrengine.DocumentManager.CreateDocument())
                        {
                            using (var ocrpage = ocrDocument.Pages.AddPage(rasterimage, null))
                            {
                                ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
                                ocrpage.Recognize(null);
                            }

                            xml = ocrDocument.SaveXml(OcrXmlOutputOptions.None);
                        }
                    }

                    using (var reader = new System.IO.StringReader(xml))
                    {
                        var doc = new System.Xml.XPath.XPathDocument(reader);
                        var nav = doc.CreateNavigator();
                        var iter = nav.Select(@"//word");

                        while (iter.MoveNext())
                        {
                            xmldata += iter.Current.Value;
                        }

                        xmldata = nav.InnerXml.ToString();
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

                exception = ex;
                return result;
            }
        }
        //========================================
        #endregion Other Functions
        //========================================
    }
}











































//using HouseOfSynergy.AffinityDms.BusinessLayer;
//using HouseOfSynergy.AffinityDms.Entities.Common;
//using HouseOfSynergy.AffinityDms.Entities.Lookup;
//using HouseOfSynergy.AffinityDms.Entities.Tenants;
//using HouseOfSynergy.PowerTools.Library.Log;
//using Leadtools;
//using Leadtools.Codecs;
//using Leadtools.Drawing;
//using Leadtools.Forms;
//using Leadtools.Forms.Auto;
//using Leadtools.Forms.Ocr;
//using Leadtools.Forms.Processing;
//using Leadtools.Forms.Recognition;
//using Leadtools.Forms.Recognition.Ocr;
//using Microsoft.ApplicationInsights;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HouseOfSynergy.AffinityDms.OcrLibrary
//{
//    public sealed class TemplateMatching
//    {
//        //========================================
//        #region Template Matching WorkerRole
//        //========================================

//        public bool DoTemplateMatching(Global global, OcrEngineSettings ocrEngineSettings, Tenant tenant, Image imagetomatch, Document inputdocument, List<Template> AllTemplates, List<TemplateElement> filterdTemplateElements, out Document matcheddocument, out List<DocumentElement> documentElements, out List<DocumentTemplate> documenttemplate, out Exception exception)
//        {

//            bool result = false;
//            exception = null;
//            matcheddocument = null;
//            documenttemplate = null;
//            documentElements = null;
//            IOcrDocument doc = null;
//            IOcrPage ocrpage = null;
//            FormRecognitionEngine formrecogengine = null;
//            try
//            {
//                using (var formsCodec = new RasterCodecs())
//                {
//                    global.Logger.Write("Start Template Matching");

//                    using (var ocrengine = Engines.LoadEngine(formsCodec, null, null, ocrEngineSettings.PathRuntime.FullName))
//                    {
//                        List<long> distinctFilteredTemplatesElementsTemplateId = filterdTemplateElements.Select(x => x.TemplateId).ToList().Distinct().ToList();
//                        List<Template> entiesTemplates = new List<Template>();
//                        global.Logger.Write($"Found {distinctFilteredTemplatesElementsTemplateId.Count()} Distinct Template");

//                        foreach (var distincttemplateid in distinctFilteredTemplatesElementsTemplateId)
//                        {
//                            Template template = AllTemplates.Where(e => e.Id == distincttemplateid).Select(x => x).FirstOrDefault();
//                            if (template != null)
//                            {
//                                entiesTemplates.Add(template);
//                            }
//                        }
//                        RasterImage rasterImageToMatch = null;
//                        List<ConfidenceMatchedTemplates> ConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates>();
//                        foreach (var masterFileNametemplate in entiesTemplates)
//                        {

//                            formrecogengine = new FormRecognitionEngine();


//                            var ocrobjmanager = new OcrObjectsManager(ocrengine);
//                            ocrobjmanager.Engine = ocrengine;
//                            formrecogengine.ObjectsManagers.Add(ocrobjmanager);
//                            var formattributes = formrecogengine.CreateForm(null);
//                            doc = ocrengine.DocumentManager.CreateDocument();
//                            if (rasterImageToMatch == null)
//                            {
//                                // formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
//                                rasterImageToMatch = RasterImageConverter.ConvertFromImage(((Image)imagetomatch.Clone()), ConvertFromImageOptions.None);
//                                ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
//                                int rotation = HelperMethods.GetRotationAngle(ocrpage);
//                                rasterImageToMatch = HelperMethods.RotateImage(rotation, rasterImageToMatch);
//                                rasterImageToMatch = HelperMethods.DeskewImage(rasterImageToMatch);
//                            }
//                            else
//                            {
//                                ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
//                            }

//                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
//                            {
//                                rasterImageToMatch.Page = i + 1;
//                                formrecogengine.AddFormPage(formattributes, rasterImageToMatch, null);
//                            }
//                            formrecogengine.CloseForm(formattributes);

//                            //string resultMessage = "The form could not be recognized";
//                            FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();

//                            //CHANGEPATH :  Need Path To The Template Files.   Line 100 Can take ByteArray too. (masterFileName) 
//                            using (var azureCloudStorageAccountHelper = new AzureCloudStorageAccountHelper(tenant))
//                            {
//                                try
//                                {
//                                    var fileBin = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".bin"));
//                                    var fileXml = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml"));
//                                    var fileTiff = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".tif"));
//                                    azureCloudStorageAccountHelper.Download(masterFileNametemplate, fileBin, fileXml, fileTiff);
//                                }
//                                catch (Exception ex)
//                                {
//                                    global.Logger.Write($"Exception :: unable to find (BIN XML TIFF) Files: {ex.ToString()}");
//                                    throw;
//                                }
//                            }
//                            string masterFileName = Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".bin");
//                            if (!(File.Exists(masterFileName)))
//                            {
//                                //exception 
//                                Exception inex = new Exception("The following file does not exist in the directory: " + masterFileName.ToString());
//                                Exception ex = new Exception("File does not Exist in the directory", inex);
//                                global.Logger.Write($"Exception :: Unable to find (BIN/XML/TIFF) Files: {ex.ToString()}");

//                                throw ex;
//                            }
//                            masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
//                            FormRecognitionResult recognitionResult = formrecogengine.CompareForm(masterFormAttributes, formattributes, null);
//                            if (inputdocument.DocumentQueueType == DocumentQueueType.Manual)
//                            {
//                                ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
//                                confiedenceObj.Template = masterFileNametemplate;
//                                confiedenceObj.MasterFormAttributes = masterFormAttributes;
//                                confiedenceObj.FormAttributes = formattributes;
//                                confiedenceObj.Confidence = recognitionResult.Confidence;
//                                ConfidenceMatchedtemplates.Add(confiedenceObj);
//                            }
//                            else
//                            {
//                                if (recognitionResult.Confidence >= ((int)OcrConfidence.MinimumOCRConfidence))
//                                {
//                                    //string MasterFormXmlFileWithFields = masterFileName.Replace (".bin", ".xml");
//                                    ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
//                                    confiedenceObj.Template = masterFileNametemplate;
//                                    confiedenceObj.MasterFormAttributes = masterFormAttributes;
//                                    confiedenceObj.FormAttributes = formattributes;
//                                    confiedenceObj.Confidence = recognitionResult.Confidence;
//                                    ConfidenceMatchedtemplates.Add(confiedenceObj);
//                                }
//                            }

//                        }
//                        List<ConfidenceMatchedTemplates> Matchedtemplates = null;
//                        if (ConfidenceMatchedtemplates.Count > 0)
//                        {
//                            result = TemplatesConfidenceMatched(ocrEngineSettings, tenant, ocrengine, inputdocument, AllTemplates, ConfidenceMatchedtemplates, rasterImageToMatch, out Matchedtemplates, out matcheddocument, out documentElements, out documenttemplate, out exception);
//                        }

//                        if (exception != null)
//                        {
//                            throw exception;
//                        }
//                        if (matcheddocument == null)
//                        {
//                            matcheddocument = new Document();
//                        }
//                        //===========================
//                        #region Update DocumentState
//                        //===========================

//                        ///<summary>
//                        ///Condition: Definition
//                        ///1) Checks if matched template is less than or equals to 0 the document state will be unmatched
//                        ///2) Checks if matched template is greater than 0  and result returned is false. False is return when multiple templates are matched. Document state will be matched multiple
//                        ///3) Checks if result returned is true.True is return when a single template is matched.  Document state will be matched.
//                        /// </summary>
//                        if ((ConfidenceMatchedtemplates.Count <= 0)) //((LowConfidenceMatchedtemplates.Count > 0) && (result==false))
//                        {
//                            matcheddocument.State = DocumentState.UnMatched;
//                        }
//                        else if ((ConfidenceMatchedtemplates.Count > 0) && (!result))
//                        {
//                            matcheddocument.State = DocumentState.MatchedMultiple;
//                        }
//                        else if (result)
//                        {
//                            matcheddocument.State = DocumentState.Matched;
//                        }
//                        //===========================
//                        #endregion Update DocumentState
//                        //===========================
//                        matcheddocument.Id = inputdocument.Id;
//                        global.Logger.Write($"End Template Matching");
//                        return result;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                if (imagetomatch != null)
//                {
//                    imagetomatch.Dispose();
//                }
//                //if (!(ocrDocument.IsInMemory))
//                //{
//                //    ocrDocument.Dispose();
//                //}
//                if (ocrpage != null)
//                {
//                    ocrpage.Dispose();
//                }
//                result = true;
//                exception = ex;
//                global.Logger.Write($"Exception :: {ex.ToString()}");

//                return result;
//            }
//        }


//        public bool TemplatesConfidenceMatched(OcrEngineSettings ocrEngineSettings, Tenant tenant, IOcrEngine ocrengine,  Document inputdocument, List<Template> AllTemplates, List<ConfidenceMatchedTemplates> confidencematchedtemplates, RasterImage rasterimagetomatch, out List<ConfidenceMatchedTemplates> matchedtemplates, out HouseOfSynergy.AffinityDms.Entities.Tenants.Document document, out List<DocumentElement> documentElements, out List<DocumentTemplate> documentTemplates, out Exception exception)
//        {
//            exception = null;
//            matchedtemplates = null;
//            document = new Document();
//            documentElements = null;
//            documentTemplates = new List<DocumentTemplate>();
//            bool result = false;
//            try
//            {
//                if (confidencematchedtemplates.Count == 1)
//                {
//                    result = ProccessSingleMatchedDocument(ocrEngineSettings, ocrengine, tenant, inputdocument, AllTemplates, confidencematchedtemplates.First(), rasterimagetomatch, out document, out documentElements, out exception);
//                    if (exception != null)
//                    {
//                        throw exception;
//                    }
//                }
//                else if (confidencematchedtemplates.Count > 1)
//                {
//                    ConfidenceMatchedTemplates maxconfidencetemplate = null;
//                    bool foundmaxconfidencetemplate = false;
//                    foreach (var confidencematchedtemplate in confidencematchedtemplates)
//                    {
//                        if (maxconfidencetemplate == null)
//                        {
//                            maxconfidencetemplate = new ConfidenceMatchedTemplates();
//                            maxconfidencetemplate = confidencematchedtemplate;
//                            foundmaxconfidencetemplate = true;
//                        }
//                        else
//                        {
//                            if (maxconfidencetemplate.Confidence < confidencematchedtemplate.Confidence)
//                            {
//                                maxconfidencetemplate = confidencematchedtemplate;
//                                foundmaxconfidencetemplate = true;
//                            }
//                            else if (maxconfidencetemplate.Confidence == confidencematchedtemplate.Confidence)
//                            {
//                                foundmaxconfidencetemplate = false;
//                            }
//                        }
//                    }
//                    if (foundmaxconfidencetemplate)
//                    {

//                        bool dbresult = ProccessSingleMatchedDocument(ocrEngineSettings, ocrengine, tenant, inputdocument, AllTemplates, confidencematchedtemplates.First(), rasterimagetomatch, out document, out documentElements, out exception);
//                        if (exception != null)
//                        {
//                            throw exception;
//                        }
//                    }
//                    else
//                    {
//                        foreach (var confidencematchedtemplate in confidencematchedtemplates)
//                        {
//                            var documenttemplate = new DocumentTemplate();
//                            documenttemplate.DocumentId = inputdocument.Id;
//                            documenttemplate.TemplateId = confidencematchedtemplate.Template.Id;
//                            documenttemplate.Confidence = confidencematchedtemplate.Confidence;
//                            documentTemplates.Add(documenttemplate);
//                        }
//                    }
//                    result = foundmaxconfidencetemplate;
//                }
//                else
//                {
//                    result = false;
//                }

//                return result;
//            }
//            catch (Exception ex)
//            {
//                exception = ex;
//                return result;
//            }
//        }


//        public bool ProccessSingleMatchedDocument(OcrEngineSettings ocrEngineSettings, IOcrEngine ocrengine, Tenant tenant, Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates matchedtemplate, RasterImage rasterimagetomatch, out Document document, out List<DocumentElement> documentElements, out Exception exception)
//        {
//            exception = null;
//            document = new Entities.Tenants.Document();
//            bool result = false;
//            documentElements = null;
//            try
//            {

//                result = StartRecogniction(ocrEngineSettings, tenant, ocrengine, inputdocument, AllTemplates, matchedtemplate, rasterimagetomatch, out documentElements, out exception);
//                if (exception != null)
//                {
//                    throw exception;
//                }
//                if (result)
//                {
//                    //BusinessLayer.DocumentManagement.UpdateDocument()
//                    document.TemplateId = matchedtemplate.Template.Id;
//                    document.Confidence = matchedtemplate.Confidence;
//                }

//            }
//            catch (Exception ex)
//            {
//                exception = ex;
//            }
//            return result;
//        }


//        private bool StartRecognictionOLD(OcrEngineSettings ocrEngineSettings, Tenant tenant, IOcrEngine ocrengine, FormRecognitionEngine formrecogengine, Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates confidencematchedtemplates, RasterImage rasterimagetomatch, out List<DocumentElement> documentelements, out Exception exception)
//        {
//            List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates>();
//            exception = null;
//            documentelements = new List<DocumentElement>();
//            bool result = false;
//            // IOcrEngine engine = null;
//            try
//            {
//                //CHANGEPATH :  Need Path To The Template Files.   Line 306 Can take Stream too. (MasterFormXmlFileWithFields) 
//                string MasterFormXmlFileWithFields = Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, confidencematchedtemplates.Template.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml");
//                if (!(File.Exists(MasterFormXmlFileWithFields)))
//                {
//                    Exception inex = new Exception("Unable to find the Template: " + MasterFormXmlFileWithFields);
//                    Exception ex = new Exception("Template Not Found", inex);
//                    throw ex;
//                }
//                var formproccessingengine = new FormProcessingEngine();
//                //LoadEngine(string.Empty, null, null, null);
//                if (!ocrengine.IsStarted)
//                {
//                    //var formsCodec = new RasterCodecs();
//                    //ocrengine = Engines.LoadEngine(formsCodec, null, null, ocrEngineSettings.PathRuntime.FullName);
//                    ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName);
//                }
//                //Engines.LoadEngine(null, null, ocrEngineSettings.WorkDirectory.FullName, ocrEngineSettings.PathRuntime.FullName, out ocrengine, out exception);
//                formproccessingengine.OcrEngine = ocrengine;
//                formproccessingengine.OcrEngine.Startup(ocrengine.RasterCodecsInstance, null, null, ocrEngineSettings.PathRuntime.FullName);
//                formproccessingengine.LoadFields(MasterFormXmlFileWithFields);
//                IList<PageAlignment> pagealignmentlist = formrecogengine.GetFormAlignment(confidencematchedtemplates.MasterFormAttributes, confidencematchedtemplates.FormAttributes, null, null);
//                formproccessingengine.Process(rasterimagetomatch, pagealignmentlist);
//                FormPages ProcessingPages = formproccessingengine.Pages;
//                //  List<TemplateElement> templateelements = AllELements.Where(e => e.TemplateId == confidencematchedtemplates.Template.Id).ToList(); ;
//                if (ProcessingPages != null)
//                {

//                    for (int i = 0; i < ProcessingPages.Count; i++)
//                    {
//                        for (int j = 0; j < ProcessingPages[i].Count; j++)
//                        {
//                            FormField formfield = ProcessingPages[i][j];
//                            if (formfield is TableFormField)
//                            {
//                                TableFormField tableformfield = ProcessingPages[i][j] as TableFormField;
//                                long tableid = long.Parse(tableformfield.Name);
//                                TableFormFieldResult results = tableformfield.Result as TableFormFieldResult;
//                                if (results != null && results.Rows != null)
//                                {
//                                    for (int x = 0; x < results.Rows.Count; x++)
//                                    {
//                                        TableFormRow row = results.Rows[x];
//                                        for (int y = 0; y < row.Fields.Count; y++)
//                                        {
//                                            OcrFormField ocrField = row.Fields[y];
//                                            if (ocrField is TextFormField)
//                                            {
//                                                TextFormFieldResult txtResults = ocrField.Result as TextFormFieldResult;
//                                                var documentelement = new DocumentElement();
//                                                documentelement.Confidience = (ocrField.Result as TextFormFieldResult).AverageConfidence;
//                                                documentelement.TemplateElementId = tableid;
//                                                documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
//                                                documentelement.OcrText = txtResults.Text;
//                                                documentelement.DocumentId = inputdocument.Id;
//                                                documentelements.Add(documentelement);

//                                            }
//                                            else if (ocrField is OmrFormField)
//                                            {
//                                                OmrFormFieldResult omrResults = ocrField.Result as OmrFormFieldResult;
//                                                var documentelement = new DocumentElement();
//                                                documentelement.Confidience = (ocrField.Result as OmrFormFieldResult).AverageConfidence;
//                                                documentelement.TemplateElementId = tableid;
//                                                documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
//                                                documentelement.OcrText = omrResults.Text;
//                                                documentelement.DocumentId = inputdocument.Id;
//                                                documentelements.Add(documentelement);
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                            else if (formfield is TextFormField)
//                            {
//                                var fieldtext = ((formfield as TextFormField).Result as TextFormFieldResult).Text;
//                                var documentelement = new DocumentElement();
//                                documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
//                                documentelement.TemplateElementId = long.Parse(formfield.Name);
//                                documentelement.OcrText = fieldtext;
//                                documentelement.DocumentId = inputdocument.Id;
//                                documentelements.Add(documentelement);
//                            }
//                            else if (formfield is ImageFormField)
//                            {
//                                var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
//                                var documentelement = new DocumentElement();
//                                documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
//                                documentelement.TemplateElementId = long.Parse(formfield.Name);
//                                documentelement.OcrText = fieldtext;
//                                documentelement.DocumentId = inputdocument.Id;
//                                documentelements.Add(documentelement);
//                            }
//                        }
//                    }
//                }
//                result = true;


//            }
//            catch (Exception ex)
//            {
//                exception = ex;
//            }
//            return result;
//        }


//        private bool StartRecogniction(OcrEngineSettings ocrEngineSettings, Tenant tenant, IOcrEngine ocrengine, Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates confidencematchedtemplates, RasterImage rasterimagetomatch, out List<DocumentElement> documentelements, out Exception exception)
//        {
//            List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates>();
//            exception = null;
//            documentelements = new List<DocumentElement>();
//            bool result = false;
//            // IOcrEngine engine = null;
//            try
//            {
//                //CHANGEPATH :  Need Path To The Template Files.   Line 306 Can take Stream too. (MasterFormXmlFileWithFields) 
//                string MasterFormXmlFileWithFields = Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, confidencematchedtemplates.Template.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml");
//                if (!(File.Exists(MasterFormXmlFileWithFields)))
//                {
//                    Exception inex = new Exception("Unable to find the Template: " + MasterFormXmlFileWithFields);
//                    Exception ex = new Exception("Template Not Found", inex);
//                    throw ex;
//                }

//                //////////////////var formproccessingengine = new FormProcessingEngine();
//                ////////////////////LoadEngine(string.Empty, null, null, null);
//                //////////////////if (!ocrengine.IsStarted)
//                //////////////////{
//                //////////////////    //var formsCodec = new RasterCodecs();
//                //////////////////    //ocrengine = Engines.LoadEngine(formsCodec, null, null, ocrEngineSettings.PathRuntime.FullName);
//                //////////////////    ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName);
//                //////////////////}
//                ////////////////////Engines.LoadEngine(null, null, ocrEngineSettings.WorkDirectory.FullName, ocrEngineSettings.PathRuntime.FullName, out ocrengine, out exception);
//                //////////////////formproccessingengine.OcrEngine = ocrengine;
//                //////////////////formproccessingengine.OcrEngine.Startup(ocrengine.RasterCodecsInstance, null, null, ocrEngineSettings.PathRuntime.FullName);
//                //////////////////formproccessingengine.LoadFields(MasterFormXmlFileWithFields);
//                //////////////////IList<PageAlignment> pagealignmentlist = formrecogengine.GetFormAlignment(confidencematchedtemplates.MasterFormAttributes, confidencematchedtemplates.FormAttributes, null, null);
//                //////////////////formproccessingengine.Process(rasterimagetomatch, pagealignmentlist);
//                //////////////////FormPages ProcessingPages = formproccessingengine.Pages;
//                ////////////////////  List<TemplateElement> templateelements = AllELements.Where(e => e.TemplateId == confidencematchedtemplates.Template.Id).ToList(); ;

//                using (var codecs = new RasterCodecs())
//                {
//                    AutoFormsEngine _autoEngine;
//                    AutoFormsRecognitionManager _manager = AutoFormsRecognitionManager.Ocr;
//                    DiskMasterFormsRepository _Repository = new DiskMasterFormsRepository(codecs, ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName);
//                    _autoEngine = new AutoFormsEngine(_Repository, ocrengine, null, _manager);
//                    List<IMasterFormsCategory> MasterCat = new List<IMasterFormsCategory>();
//                    _autoEngine.RecognizeFirstPageOnly = true;
//                    if (_Repository.RootCategory.MasterFormsCount > 0)
//                    {
//                        foreach (var item in _Repository.RootCategory.MasterForms.ToList())
//                        {
//                            if (item.Name != Path.GetFileNameWithoutExtension(MasterFormXmlFileWithFields))
//                            {
//                                item.Parent.MasterForms.Remove(item);
//                            }
//                        }
//                    }
//                    if (_Repository.RootCategory.MasterFormsCount > 0)
//                    {
//                        MasterCat.Add(_Repository.RootCategory);
//                    }
//                    AutoFormsRunResult _Results = new AutoFormsRunResult();
//                    _Results = _autoEngine.Run(rasterimagetomatch, null, null, MasterCat);
//                    FormPages ProcessingPages = _Results.FormFields;
//                    if (ProcessingPages != null)
//                    {
//                        for (int i = 0; i < ProcessingPages.Count; i++)
//                        {
//                            for (int j = 0; j < ProcessingPages[i].Count; j++)
//                            {
//                                FormField formfield = ProcessingPages[i][j];
//                                if (formfield is TableFormField)
//                                {
//                                    TableFormField tableformfield = ProcessingPages[i][j] as TableFormField;
//                                    long tableid = long.Parse(tableformfield.Name);
//                                    TableFormFieldResult results = tableformfield.Result as TableFormFieldResult;
//                                    if (results != null && results.Rows != null)
//                                    {
//                                        for (int x = 0; x < results.Rows.Count; x++)
//                                        {
//                                            TableFormRow row = results.Rows[x];
//                                            for (int y = 0; y < row.Fields.Count; y++)
//                                            {
//                                                OcrFormField ocrField = row.Fields[y];
//                                                if (ocrField is TextFormField)
//                                                {
//                                                    TextFormFieldResult txtResults = ocrField.Result as TextFormFieldResult;
//                                                    var documentelement = new DocumentElement();
//                                                    documentelement.Confidience = (ocrField.Result as TextFormFieldResult).AverageConfidence;
//                                                    documentelement.TemplateElementId = tableid;
//                                                    documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
//                                                    documentelement.OcrText = txtResults.Text;
//                                                    documentelement.DocumentId = inputdocument.Id;
//                                                    documentelements.Add(documentelement);

//                                                }
//                                                else if (ocrField is OmrFormField)
//                                                {
//                                                    OmrFormFieldResult omrResults = ocrField.Result as OmrFormFieldResult;
//                                                    var documentelement = new DocumentElement();
//                                                    documentelement.Confidience = (ocrField.Result as OmrFormFieldResult).AverageConfidence;
//                                                    documentelement.TemplateElementId = tableid;
//                                                    documentelement.TemplateElementDetailId = long.Parse(ocrField.Name);
//                                                    documentelement.OcrText = omrResults.Text;
//                                                    documentelement.DocumentId = inputdocument.Id;
//                                                    documentelements.Add(documentelement);
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                                else if (formfield is TextFormField)
//                                {
//                                    var fieldtext = ((formfield as TextFormField).Result as TextFormFieldResult).Text;
//                                    var documentelement = new DocumentElement();
//                                    documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
//                                    documentelement.TemplateElementId = long.Parse(formfield.Name);
//                                    documentelement.OcrText = fieldtext;
//                                    documentelement.DocumentId = inputdocument.Id;
//                                    documentelements.Add(documentelement);
//                                }
//                                else if (formfield is ImageFormField)
//                                {
//                                    var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString();
//                                    var documentelement = new DocumentElement();
//                                    documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
//                                    documentelement.TemplateElementId = long.Parse(formfield.Name);
//                                    documentelement.OcrText = fieldtext;
//                                    documentelement.DocumentId = inputdocument.Id;
//                                    documentelements.Add(documentelement);
//                                }
//                            }
//                        }
//                    }
//                    result = true;
//                }






//            }
//            catch (Exception ex)
//            {
//                exception = ex;
//            }
//            return result;
//        }
//        //========================================
//        #endregion Template Matching WorkerRole
//        //========================================

//        /*_*/

//        //========================================
//        #region Other Functions
//        //========================================
//        public bool GetFullTextOCR(OcrEngineSettings ocrEngineSettings, RasterImage rasterimage, out Exception exception, out string ocrtext)
//        {
//            var result = false;

//            ocrtext = "";
//            exception = null;

//            try
//            {
//                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
//                {
//                    using (var ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName))
//                    {
//                        ocrengine.CreatePage(rasterimage, OcrImageSharingMode.AutoDispose);

//                        using (var ocrDocument = ocrengine.DocumentManager.CreateDocument())
//                        {
//                            using (var ocrpage = ocrDocument.Pages.AddPage(rasterimage, null))
//                            {
//                                ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
//                                ocrpage.AutoZone(null);
//                                OcrZone ocrzone = new OcrZone();
//                                ocrzone.Name = "ZoneName";
//                                ocrzone.ZoneType = OcrZoneType.Text;
//                                int aaaaa = ocrpage.Width;
//                                ocrzone.Bounds = new LogicalRectangle(10, 10, ocrpage.Width - 20, 100, LogicalUnit.Pixel);
//                                ocrpage.Zones.Add(ocrzone);
//                                ocrpage.Recognize(null);
//                                //  IOcrPageCharacters pageCharacters = ocrpage.GetRecognizedCharacters();
//                                //  _zoneCharacters = pageCharacters[0];

//                                int zoneIndex = 0;

//                                while (zoneIndex < ocrpage.Zones.Count())
//                                {
//                                    ocrtext += " " + ocrpage.GetText(zoneIndex++);
//                                }
//                            }
//                        }
//                    }

//                    result = true;
//                }
//                else
//                {
//                    Exception inex = new Exception("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
//                    Exception ex = new Exception("RasterSupport Is Locked", inex);
//                    throw ex;
//                }
//                return result;
//            }
//            catch (Exception ex)
//            {
//                exception = ex;
//                return result;
//            }
//        }

//        public bool GetFullTextXMLOCR(OcrEngineSettings ocrEngineSettings, RasterImage rasterimage, out Exception exception, out string xmldata)
//        {
//            var xml = "";
//            var result = false;

//            xmldata = "";
//            exception = null;

//            try
//            {
//                if (!RasterSupport.IsLocked(RasterSupportType.OcrAdvantage))
//                {
//                    using (var ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName))
//                    {
//                        using (var ocrDocument = ocrengine.DocumentManager.CreateDocument())
//                        {
//                            using (var ocrpage = ocrDocument.Pages.AddPage(rasterimage, null))
//                            {
//                                ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
//                                ocrpage.Recognize(null);
//                            }

//                            xml = ocrDocument.SaveXml(OcrXmlOutputOptions.None);
//                        }
//                    }

//                    using (var reader = new System.IO.StringReader(xml))
//                    {
//                        var doc = new System.Xml.XPath.XPathDocument(reader);
//                        var nav = doc.CreateNavigator();
//                        var iter = nav.Select(@"//word");

//                        while (iter.MoveNext())
//                        {
//                            xmldata += iter.Current.Value;
//                        }

//                        xmldata = nav.InnerXml.ToString();
//                    }

//                    result = true;
//                }
//                else
//                {
//                    Exception inex = new Exception("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
//                    Exception ex = new Exception("RasterSupport Is Locked", inex);
//                    throw ex;
//                }

//                return result;
//            }
//            catch (Exception ex)
//            {

//                exception = ex;
//                return result;
//            }
//        }
//        //========================================
//        #endregion Other Functions
//        //========================================
//    }
//}




















































































//using HouseOfSynergy.AffinityDms.BusinessLayer;
//using HouseOfSynergy.AffinityDms.Entities.Common;
//using HouseOfSynergy.AffinityDms.Entities.Lookup;
//using HouseOfSynergy.AffinityDms.Entities.Tenants;
//using HouseOfSynergy.PowerTools.Library.Log;
//using Leadtools;
//using Leadtools.Codecs;
//using Leadtools.Drawing;
//using Leadtools.Forms;
//using Leadtools.Forms.Ocr;
//using Leadtools.Forms.Processing;
//using Leadtools.Forms.Recognition;
//using Leadtools.Forms.Recognition.Ocr;
//using Microsoft.ApplicationInsights;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HouseOfSynergy.AffinityDms.OcrLibrary
//{
//	public sealed class TemplateMatching
//	{
//		//========================================
//		#region Template Matching WorkerRole
//		//========================================

//		public bool DoTemplateMatching (Global global, OcrEngineSettings ocrEngineSettings, Tenant tenant, Image imagetomatch, Document inputdocument, List<Template> AllTemplates, List<TemplateElement> filterdTemplateElements, out Document matcheddocument, out List<DocumentElement> documentElements,  out List<DocumentTemplate> documenttemplate, out Exception exception)
//		{

//			bool result = false;
//			exception = null;
//			matcheddocument = null;
//			documenttemplate = null;
//            documentElements = null;
//			IOcrDocument doc = null;
//			IOcrPage ocrpage = null;
//			FormRecognitionEngine formrecogengine = null;
//			try
//			{
//				using (var formsCodec = new RasterCodecs ())
//				{
//                    global.Logger.Write("Start Template Matching");

//                    using (var ocrengine = Engines.LoadEngine (formsCodec, null, null, ocrEngineSettings.PathRuntime.FullName))
//					{
//						List<long> distinctFilteredTemplatesElementsTemplateId = filterdTemplateElements.Select (x => x.TemplateId).ToList ().Distinct ().ToList ();
//						List<Template> entiesTemplates = new List<Template> ();
//                        global.Logger.Write($"Found {distinctFilteredTemplatesElementsTemplateId.Count()} Distinct Template");

//                        foreach (var distincttemplateid in distinctFilteredTemplatesElementsTemplateId)
//						{
//							Template template = AllTemplates.Where (e => e.Id == distincttemplateid).Select (x => x).FirstOrDefault ();
//							if (template != null)
//							{
//								entiesTemplates.Add (template);
//							}
//						}
//						RasterImage rasterImageToMatch = null;
//						List<ConfidenceMatchedTemplates> ConfidenceMatchedtemplates = new List<ConfidenceMatchedTemplates> ();
//						foreach (var masterFileNametemplate in entiesTemplates)
//						{

//							formrecogengine = new FormRecognitionEngine ();


//							var ocrobjmanager = new OcrObjectsManager (ocrengine);
//							ocrobjmanager.Engine = ocrengine;
//							formrecogengine.ObjectsManagers.Add (ocrobjmanager);
//							var formattributes = formrecogengine.CreateForm (null);
//                            doc = ocrengine.DocumentManager.CreateDocument();
//                            if (rasterImageToMatch == null)
//                            {
//                                // formsCodec.Load(paths, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
//                                rasterImageToMatch = RasterImageConverter.ConvertFromImage(((Image)imagetomatch.Clone()), ConvertFromImageOptions.None);
//                                ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
//                                int rotation = HelperMethods.GetRotationAngle(ocrpage);
//                                rasterImageToMatch = HelperMethods.RotateImage(rotation, rasterImageToMatch);
//                                rasterImageToMatch = HelperMethods.DeskewImage(rasterImageToMatch);
//                            }
//                            else
//                            {
//                                ocrpage = doc.Pages.AddPage(rasterImageToMatch, null);
//                            }

//                            for (int i = 0; i < rasterImageToMatch.PageCount; i++)
//							{
//								rasterImageToMatch.Page = i + 1;
//								formrecogengine.AddFormPage (formattributes, rasterImageToMatch, null);
//							}
//							formrecogengine.CloseForm (formattributes);

//							//string resultMessage = "The form could not be recognized";
//							FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes ();

//                            //CHANGEPATH :  Need Path To The Template Files.   Line 100 Can take ByteArray too. (masterFileName) 
//                            using (var azureCloudStorageAccountHelper = new AzureCloudStorageAccountHelper(tenant))
//                            {
//                                try
//                                {
//                                    var fileBin = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName,  masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".bin"));
//                                    var fileXml = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml"));
//                                    var fileTiff = new FileInfo(Path.Combine(ocrEngineSettings.GetPathDataTenantTemplates(tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".tif"));
//                                    azureCloudStorageAccountHelper.Download(masterFileNametemplate,fileBin,fileXml,fileTiff); 
//                                }
//                                catch (Exception ex)
//                                {
//                                    global.Logger.Write($"Exception :: unable to find (BIN XML TIFF) Files: {ex.ToString()}");
//                                    throw;
//                                }
//                            }
//                            string masterFileName = Path.Combine (ocrEngineSettings.GetPathDataTenantTemplates (tenant).FullName, masterFileNametemplate.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".bin");
//							if (!(File.Exists (masterFileName)))
//							{
//								//exception 
//								Exception inex = new Exception ("The following file does not exist in the directory: " + masterFileName.ToString ());
//								Exception ex = new Exception ("File does not Exist in the directory",inex);
//                                global.Logger.Write($"Exception :: Unable to find (BIN/XML/TIFF) Files: {ex.ToString()}");

//                                throw ex;
//							}
//							masterFormAttributes.SetData (File.ReadAllBytes (masterFileName));
//							FormRecognitionResult recognitionResult = formrecogengine.CompareForm (masterFormAttributes, formattributes, null);
//                            if (inputdocument.DocumentQueueType == DocumentQueueType.Manual)
//                            {
//                                ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
//                                confiedenceObj.Template = masterFileNametemplate;
//                                confiedenceObj.MasterFormAttributes = masterFormAttributes;
//                                confiedenceObj.FormAttributes = formattributes;
//                                confiedenceObj.Confidence = recognitionResult.Confidence;
//                                ConfidenceMatchedtemplates.Add(confiedenceObj);
//                            }
//                            else
//                            {
//                                if (recognitionResult.Confidence >= ((int)OcrConfidence.MinimumOCRConfidence))
//                                {
//                                    //string MasterFormXmlFileWithFields = masterFileName.Replace (".bin", ".xml");
//                                    ConfidenceMatchedTemplates confiedenceObj = new ConfidenceMatchedTemplates();
//                                    confiedenceObj.Template = masterFileNametemplate;
//                                    confiedenceObj.MasterFormAttributes = masterFormAttributes;
//                                    confiedenceObj.FormAttributes = formattributes;
//                                    confiedenceObj.Confidence = recognitionResult.Confidence;
//                                    ConfidenceMatchedtemplates.Add(confiedenceObj);
//                                }
//                            }

//						}
//						List<ConfidenceMatchedTemplates> Matchedtemplates = null;
//						if (ConfidenceMatchedtemplates.Count > 0)
//						{
//							result = TemplatesConfidenceMatched (ocrEngineSettings, tenant, ocrengine, formrecogengine, inputdocument, AllTemplates, ConfidenceMatchedtemplates, rasterImageToMatch, out Matchedtemplates, out matcheddocument, out documentElements, out documenttemplate, out exception);
//						}

//						if (exception != null)
//						{
//							throw exception;
//						}
//						if (matcheddocument == null)
//						{
//							matcheddocument = new Document ();
//						}
//						//===========================
//						#region Update DocumentState
//						//===========================

//						///<summary>
//						///Condition: Definition
//						///1) Checks if matched template is less than or equals to 0 the document state will be unmatched
//						///2) Checks if matched template is greater than 0  and result returned is false. False is return when multiple templates are matched. Document state will be matched multiple
//						///3) Checks if result returned is true.True is return when a single template is matched.  Document state will be matched.
//						/// </summary>
//						if ((ConfidenceMatchedtemplates.Count <= 0)) //((LowConfidenceMatchedtemplates.Count > 0) && (result==false))
//						{
//							matcheddocument.State = DocumentState.UnMatched;
//						}
//						else if ((ConfidenceMatchedtemplates.Count > 0) && (!result))
//						{
//							matcheddocument.State = DocumentState.MatchedMultiple;
//						}
//						else if (result)
//						{
//							matcheddocument.State = DocumentState.Matched;
//						}
//						//===========================
//						#endregion Update DocumentState
//						//===========================
//						matcheddocument.Id = inputdocument.Id;
//                        global.Logger.Write($"End Template Matching");
//                        return result;
//                    }
//                }
//			}
//			catch (Exception ex)
//			{
//				if (imagetomatch != null)
//				{
//					imagetomatch.Dispose ();
//				}
//				//if (!(ocrDocument.IsInMemory))
//				//{
//				//    ocrDocument.Dispose();
//				//}
//				if (ocrpage != null)
//				{
//					ocrpage.Dispose ();
//				}
//				result = true;
//				exception = ex;
//                global.Logger.Write($"Exception :: {ex.ToString()}");

//                return result;
//			}
//		}


//		public bool TemplatesConfidenceMatched (OcrEngineSettings ocrEngineSettings, Tenant tenant, IOcrEngine ocrengine, FormRecognitionEngine formrecogengine, Document inputdocument, List<Template> AllTemplates, List<ConfidenceMatchedTemplates> confidencematchedtemplates, RasterImage rasterimagetomatch, out List<ConfidenceMatchedTemplates> matchedtemplates, out HouseOfSynergy.AffinityDms.Entities.Tenants.Document document,out List<DocumentElement> documentElements, out List<DocumentTemplate> documentTemplates, out Exception exception)
//		{
//			exception = null;
//			matchedtemplates = null;
//			document = new Document ();
//            documentElements = null;
//            documentTemplates = new List<DocumentTemplate> ();
//			bool result = false;
//			try
//			{
//				if (confidencematchedtemplates.Count == 1)
//				{
//					result = ProccessSingleMatchedDocument (ocrEngineSettings, ocrengine, tenant, formrecogengine, inputdocument, AllTemplates, confidencematchedtemplates.First (), rasterimagetomatch, out document,out documentElements, out exception);
//					if (exception != null)
//					{
//						throw exception;
//					}
//				}
//				else if (confidencematchedtemplates.Count > 1)
//				{
//					ConfidenceMatchedTemplates maxconfidencetemplate = null;
//					bool foundmaxconfidencetemplate = false;
//					foreach (var confidencematchedtemplate in confidencematchedtemplates)
//					{
//						if (maxconfidencetemplate == null)
//						{
//							maxconfidencetemplate = new ConfidenceMatchedTemplates ();
//							maxconfidencetemplate = confidencematchedtemplate;
//							foundmaxconfidencetemplate = true;
//						}
//						else
//						{
//							if (maxconfidencetemplate.Confidence < confidencematchedtemplate.Confidence)
//							{
//								maxconfidencetemplate = confidencematchedtemplate;
//								foundmaxconfidencetemplate = true;
//							}
//							else if (maxconfidencetemplate.Confidence == confidencematchedtemplate.Confidence)
//							{
//								foundmaxconfidencetemplate = false;
//							}
//						}
//					}
//					if (foundmaxconfidencetemplate)
//					{

//						bool dbresult = ProccessSingleMatchedDocument (ocrEngineSettings, ocrengine, tenant, formrecogengine, inputdocument, AllTemplates, confidencematchedtemplates.First (), rasterimagetomatch, out document, out documentElements, out exception);
//						if (exception != null)
//						{
//							throw exception;
//						}
//					}
//					else
//					{
//						foreach (var confidencematchedtemplate in confidencematchedtemplates)
//						{
//							var documenttemplate = new DocumentTemplate ();
//							documenttemplate.DocumentId = inputdocument.Id;
//							documenttemplate.TemplateId = confidencematchedtemplate.Template.Id;
//							documenttemplate.Confidence = confidencematchedtemplate.Confidence;
//							documentTemplates.Add (documenttemplate);
//						}
//					}
//					result = foundmaxconfidencetemplate;
//				}
//				else
//				{
//					result = false;
//				}

//				return result;
//			}
//			catch (Exception ex)
//			{
//				exception = ex;
//				return result;
//			}
//		}


//		public bool ProccessSingleMatchedDocument (OcrEngineSettings ocrEngineSettings, IOcrEngine ocrengine, Tenant tenant, FormRecognitionEngine formrecogengine, Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates matchedtemplate, RasterImage rasterimagetomatch, out Document document, out List<DocumentElement> documentElements, out Exception exception)
//		{
//			exception = null;
//			document = new Entities.Tenants.Document ();
//			bool result = false;
//            documentElements = null;
//			try
//			{

//				result = StartRecogniction (ocrEngineSettings, tenant, ocrengine, formrecogengine, inputdocument, AllTemplates, matchedtemplate, rasterimagetomatch, out documentElements, out exception);
//				if (exception != null)
//				{
//					throw exception;
//				}
//				if (result)
//				{
//					//BusinessLayer.DocumentManagement.UpdateDocument()
//					document.TemplateId = matchedtemplate.Template.Id;
//					document.Confidence = matchedtemplate.Confidence;
//                }

//            }
//			catch (Exception ex)
//			{
//				exception = ex;
//			}
//			return result;
//		}


//		private bool StartRecogniction (OcrEngineSettings ocrEngineSettings, Tenant tenant, IOcrEngine ocrengine, FormRecognitionEngine formrecogengine, Document inputdocument, List<Template> AllTemplates, ConfidenceMatchedTemplates confidencematchedtemplates, RasterImage rasterimagetomatch, out List<DocumentElement> documentelements, out Exception exception)
//		{
//			List<ConfidenceMatchedTemplates> ListOfElementOcrDetails = new List<ConfidenceMatchedTemplates> ();
//			exception = null;
//			documentelements = new List<DocumentElement> ();
//			bool result = false;
//			// IOcrEngine engine = null;
//			try
//			{
//                //CHANGEPATH :  Need Path To The Template Files.   Line 306 Can take Stream too. (MasterFormXmlFileWithFields) 
//                string MasterFormXmlFileWithFields = Path.Combine (ocrEngineSettings.GetPathDataTenantTemplates (tenant).FullName,confidencematchedtemplates.Template.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + ".xml");
//				if (!(File.Exists (MasterFormXmlFileWithFields)))
//				{
//					Exception inex = new Exception ("Unable to find the Template: " + MasterFormXmlFileWithFields);
//					Exception ex = new Exception ("Template Not Found", inex);
//					throw ex;
//				}
//				var formproccessingengine = new FormProcessingEngine ();
//                //LoadEngine(string.Empty, null, null, null);
//                if (!ocrengine.IsStarted)
//                {
//                    //var formsCodec = new RasterCodecs();
//                    //ocrengine = Engines.LoadEngine(formsCodec, null, null, ocrEngineSettings.PathRuntime.FullName);
//                    ocrengine = Engines.LoadEngine(null, null, null, ocrEngineSettings.PathRuntime.FullName);
//                }
//                //Engines.LoadEngine(null, null, ocrEngineSettings.WorkDirectory.FullName, ocrEngineSettings.PathRuntime.FullName, out ocrengine, out exception);
//                formproccessingengine.OcrEngine = ocrengine;
//				formproccessingengine.OcrEngine.Startup (ocrengine.RasterCodecsInstance, null, null, ocrEngineSettings.PathRuntime.FullName);
//				formproccessingengine.LoadFields (MasterFormXmlFileWithFields);
//				IList<PageAlignment> pagealignmentlist = formrecogengine.GetFormAlignment (confidencematchedtemplates.MasterFormAttributes, confidencematchedtemplates.FormAttributes, null, null);
//				formproccessingengine.Process (rasterimagetomatch, pagealignmentlist);
//                FormPages ProcessingPages = formproccessingengine.Pages;
//				//  List<TemplateElement> templateelements = AllELements.Where(e => e.TemplateId == confidencematchedtemplates.Template.Id).ToList(); ;
//				if (ProcessingPages != null)
//				{

//					for (int i = 0; i < ProcessingPages.Count; i++)
//					{
//						for (int j = 0; j < ProcessingPages [i].Count; j++)
//						{
//							FormField formfield = ProcessingPages [i] [j];
//							if (formfield is TableFormField)
//							{
//								TableFormField tableformfield = ProcessingPages [i] [j] as TableFormField;
//								long tableid = long.Parse (tableformfield.Name);
//								TableFormFieldResult results = tableformfield.Result as TableFormFieldResult;
//								if (results != null && results.Rows != null)
//								{
//									for (int x = 0; x < results.Rows.Count; x++)
//									{
//										TableFormRow row = results.Rows [x];
//										for (int y = 0; y < row.Fields.Count; y++)
//										{
//											OcrFormField ocrField = row.Fields [y];
//											if (ocrField is TextFormField)
//											{
//												TextFormFieldResult txtResults = ocrField.Result as TextFormFieldResult;
//												var documentelement = new DocumentElement ();
//                                                documentelement.Confidience = (ocrField.Result as TextFormFieldResult).AverageConfidence;
//                                                documentelement.TemplateElementId = tableid;
//												documentelement.TemplateElementDetailId = long.Parse (ocrField.Name);
//												documentelement.OcrText = txtResults.Text;
//												documentelement.DocumentId = inputdocument.Id;
//												documentelements.Add (documentelement);

//											}
//											else if (ocrField is OmrFormField)
//											{
//												OmrFormFieldResult omrResults = ocrField.Result as OmrFormFieldResult;
//												var documentelement = new DocumentElement ();
//                                                documentelement.Confidience = (ocrField.Result as OmrFormFieldResult).AverageConfidence;
//                                                documentelement.TemplateElementId = tableid;
//												documentelement.TemplateElementDetailId = long.Parse (ocrField.Name);
//												documentelement.OcrText = omrResults.Text;
//												documentelement.DocumentId = inputdocument.Id;
//												documentelements.Add (documentelement);
//											}
//										}
//									}
//								}
//							}
//							else if (formfield is TextFormField)
//							{
//								var fieldtext = ((formfield as TextFormField).Result as TextFormFieldResult).Text;
//								var documentelement = new DocumentElement ();
//                                documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
//                                documentelement.TemplateElementId = long.Parse (formfield.Name);
//								documentelement.OcrText = fieldtext;
//								documentelement.DocumentId = inputdocument.Id;
//								documentelements.Add (documentelement);
//							}
//							else if (formfield is ImageFormField)
//							{
//								var fieldtext = ((formfield as ImageFormField).Result as ImageFormFieldResult).Status.ToString ();
//								var documentelement = new DocumentElement ();
//                                documentelement.Confidience = ((formfield as TextFormField).Result as TextFormFieldResult).AverageConfidence;
//                                documentelement.TemplateElementId = long.Parse (formfield.Name);
//								documentelement.OcrText = fieldtext;
//								documentelement.DocumentId = inputdocument.Id;
//								documentelements.Add (documentelement);
//							}
//						}
//					}
//				}
//				result = true;


//			}
//			catch (Exception ex)
//			{
//				exception = ex;
//			}
//			return result;
//		}

//		//========================================
//		#endregion Template Matching WorkerRole
//		//========================================

//		/*_*/

//		//========================================
//		#region Other Functions
//		//========================================
//		public bool GetFullTextOCR (OcrEngineSettings ocrEngineSettings, RasterImage rasterimage, out Exception exception, out string ocrtext)
//		{
//			var result = false;

//			ocrtext = "";
//			exception = null;

//			try
//			{
//				if (!RasterSupport.IsLocked (RasterSupportType.OcrAdvantage))
//				{
//					using (var ocrengine = Engines.LoadEngine (null, null, null, ocrEngineSettings.PathRuntime.FullName))
//					{
//						ocrengine.CreatePage (rasterimage, OcrImageSharingMode.AutoDispose);

//						using (var ocrDocument = ocrengine.DocumentManager.CreateDocument ())
//						{
//							using (var ocrpage = ocrDocument.Pages.AddPage (rasterimage, null))
//							{
//								ocrpage.AutoPreprocess (OcrAutoPreprocessPageCommand.All, null);
//								ocrpage.AutoZone (null);
//								OcrZone ocrzone = new OcrZone ();
//								ocrzone.Name = "ZoneName";
//								ocrzone.ZoneType = OcrZoneType.Text;
//								int aaaaa = ocrpage.Width;
//								ocrzone.Bounds = new LogicalRectangle (10, 10, ocrpage.Width - 20, 100, LogicalUnit.Pixel);
//								ocrpage.Zones.Add (ocrzone);
//								ocrpage.Recognize (null);
//								//  IOcrPageCharacters pageCharacters = ocrpage.GetRecognizedCharacters();
//								//  _zoneCharacters = pageCharacters[0];

//								int zoneIndex = 0;

//								while (zoneIndex < ocrpage.Zones.Count ())
//								{
//									ocrtext += " " + ocrpage.GetText (zoneIndex++);
//								}
//							}
//						}
//					}

//					result = true;
//				}
//				else
//				{
//					Exception inex = new Exception ("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
//					Exception ex = new Exception ("RasterSupport Is Locked", inex);
//					throw ex;
//				}
//				return result;
//			}
//			catch (Exception ex)
//			{
//				exception = ex;
//				return result;
//			}
//		}

//		public bool GetFullTextXMLOCR (OcrEngineSettings ocrEngineSettings, RasterImage rasterimage, out Exception exception, out string xmldata)
//		{
//			var xml = "";
//			var result = false;

//			xmldata = "";
//			exception = null;

//			try
//			{
//				if (!RasterSupport.IsLocked (RasterSupportType.OcrAdvantage))
//				{
//					using (var ocrengine = Engines.LoadEngine (null, null, null, ocrEngineSettings.PathRuntime.FullName))
//					{
//						using (var ocrDocument = ocrengine.DocumentManager.CreateDocument ())
//						{
//							using (var ocrpage = ocrDocument.Pages.AddPage (rasterimage, null))
//							{
//								ocrpage.AutoPreprocess (OcrAutoPreprocessPageCommand.All, null);
//								ocrpage.Recognize (null);
//							}

//							xml = ocrDocument.SaveXml (OcrXmlOutputOptions.None);
//						}
//					}

//					using (var reader = new System.IO.StringReader (xml))
//					{
//						var doc = new System.Xml.XPath.XPathDocument (reader);
//						var nav = doc.CreateNavigator ();
//						var iter = nav.Select (@"//word");

//						while (iter.MoveNext ())
//						{
//							xmldata += iter.Current.Value;
//						}

//						xmldata = nav.InnerXml.ToString ();
//					}

//					result = true;
//				}
//				else
//				{
//					Exception inex = new Exception ("Unable to Perform XML OCR. Raster Support is Locked. Either Proper Runtime Files/Licence is Missing or Expiried");
//					Exception ex = new Exception ("RasterSupport Is Locked", inex);
//					throw ex;
//				}

//				return result;
//			}
//			catch (Exception ex)
//			{

//				exception = ex;
//				return result;
//			}
//		}
//		//========================================
//		#endregion Other Functions
//		//========================================
//	}
//}
