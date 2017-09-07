using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.ApplicationInsights;

namespace HouseOfSynergy.AffinityDms.OcrWorkerRole
{
    public class OcrOldBackup
    {
        public static bool TenantDocumentPerformOcr(TelemetryClient telemetryClient, Tenant tenant, Document document, OcrEngineSettings ocrEngineSettings, out Exception exception, long templateId = 0)
        {
            var result = false;
            Document ocrDocument = null;
            List<DocumentTemplate> ocrDocumentTemplate = null;
            List<DocumentFragment> ocrDocumentFragment = null;

            exception = null;

            try
            {
                List<Template> allTemplates = null;
                List<TemplateElement> allElements = null;

                using (var context = new ContextTenant(tenant.DatabaseConnectionString))
                {
                    if (document.DocumentQueueType == DocumentQueueType.Manual)
                    {
                        if (templateId > 0)
                        {
                            allTemplates = context.Templates
                                                  .AsNoTracking()
                                                  .Include(t => t.Elements)
                                                  .Include(t => t.Elements.Select(x => x.ElementDetails))
                                                  .Where(t => (t.IsActive == true) && (t.IsFinalized == true) && (t.Id == templateId))
                                                  .ToList();
                        }
                        else
                        {
                            throw (new Exception("Template can  not be null. Template should be provided for manual classification"));
                        }
                        
                    }
                    else
                    {
                        allTemplates = context
                        .Templates
                        .AsNoTracking()
                        .Include(t => t.Elements)
                        .Include(t => t.Elements.Select(x => x.ElementDetails))
                        .Where(t => (t.IsActive == true) && (t.IsFinalized == true))
                        .ToList();
                    }
                    

                    if (allTemplates.Count <= 0) { throw (new Exception("No templates found.")); }
                }

                // workerRoleParameters???

                // Download file from azure.
                Stream stream = null;
                List<OcrResultInfo> ocrresultinfos = new List<OcrResultInfo>();

                if (GetTenantDocumentFileStream(tenant, document, out stream, out exception))
                {
                    Image documentImage = null;

                    using (var image = Image.FromStream(stream))
                    {
                        documentImage = (Image)image.Clone();
                        // LeadTools code.
                        // Stream and image available.
                    }

                    // TODO: Replace with Server.MapPath.
                    //LeadToolsOCR leadtoolsocr = new LeadToolsOCR (workerRoleParameters.OcrEngineSettings.GetPathData().FullName, tenant.Id.ToString (), string.Empty, out exception);
                    //if (exception != null) { throw exception; }

                    List<LeadtoolsBarcodeData> barcodedata = null;
                    bool foundbarcode = false;
                    foundbarcode =  Barcode.ReadBarcode (documentImage, out barcodedata, out exception);
                    if (exception != null) { throw exception; }
                    if (foundbarcode)
                    {
                        //What to do if multiple barcodes are found.
                        int templateid = 0;
                        bool foundId = int.TryParse(barcodedata.First().Value, out templateid);
                        if (!(foundId) || templateid <= 0)
                        { }//What to do if Form Id is Not Found by the barcode
                        Template template = allTemplates.Where(t => t.Id == ((long)templateid)).ToArray().FirstOrDefault();
                        if (template.TemplateType == TemplateType.Form)
                        {
                            double _computeddifference = 0.0;
                            if ((documentImage.Width <= documentImage.Height) && (!(documentImage.Height <= 0)))
                            {
                                _computeddifference = ((Convert.ToDouble(documentImage.Width)) / (Convert.ToDouble(documentImage.Height)));
                            }
                            else
                            {
                                if (!(documentImage.Width <= 0))
                                {
                                    _computeddifference = documentImage.Height / documentImage.Width;
                                }
                            }
                            var elements = allElements.Where(e => e.TemplateId == (long)templateid).ToList();
                            List<ComputeCoordinates> cordlistlist = new List<ComputeCoordinates>();
                            //cordlistlist = leadtoolsocr.GetAllZoneData (elements, _computeddifference, documentImage);

                            //what to do with form matched results


                            //foreach (ComputeCoordinates cordata in cordlistlist)
                            //{
                            //    returnedresult += cordata.Text;
                            //}
                        }
                    }
                    else
                    {
                        result = AutoOCR(telemetryClient, ocrEngineSettings, tenant, documentImage, document, allTemplates, out ocrDocument, out ocrDocumentFragment, out ocrDocumentTemplate, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }

                    }

                    // Perform OCR and time-consuming tasks.
                }
                else
                {
                    throw (exception);
                }

                using (var context = new ContextTenant(tenant.DatabaseConnectionString))
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (ocrDocument != null)
                            {
                                context.Documents.Attach(ocrDocument);
                                context.Entry(ocrDocument).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            if (ocrDocumentFragment != null)
                            {
                                foreach (var documentfragment in ocrDocumentFragment)
                                {
                                    context.DocumentFragments.Add(documentfragment);
                                    context.SaveChanges();
                                }
                            }
                            if (ocrDocumentTemplate != null)
                            {
                                foreach (var documenttemplate in ocrDocumentTemplate)
                                {
                                    context.DocumentTemplate.Add(documenttemplate);
                                    context.SaveChanges();
                                }
                            }
                            //context.Templates.Attach(templates[0]);
                            context.SaveChanges();

                            // Do not call this line from anywhere else.
                            transaction.Commit();

                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            telemetryClient.TrackEvent(e.ToString());
                        }
                    }
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
                telemetryClient.TrackEvent($"Telemetry:: {e.ToString()}");
            }

            return (result);
        }

        public static bool AutoOCR(TelemetryClient telemetryClient, OcrEngineSettings ocrEngineSettings, Tenant tenant, Image image, Document document, List<Template> alltemplates, out Document documentObj, out List<DocumentFragment> documentfragments, out List<DocumentTemplate> documentTemplate, out Exception exception)
        {
            bool result = false;
            exception = null;
            documentObj = null;
            documentfragments = null;
            documentTemplate = null;

            try
            {
                OcrClassification ocrclassification = new OcrClassification();
               // result = ocrclassification.BeginOcrClassification(telemetryClient, ocrEngineSettings, tenant, image, document, alltemplates, out documentObj, out documentfragments, out documentTemplate, out exception);
                if (exception != null)
                {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                exception = ex;

            }
            return result;
        }
      
        private static bool GetTenantDocumentFileStream(Tenant tenant, Document document, out Stream stream, out Exception exception)
        {
            var result = false;

            stream = null;
            exception = null;

            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(tenant.UrlResourceGroup);

                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

                var blob = container.GetBlockBlobReference(document.FileNameServer);

                stream = new MemoryStream();
                blob.DownloadToStream(stream);

                result = true;
            }
            catch (Exception e)
            {
                if (stream != null)
                {
                    try { stream.Dispose(); }
                    finally { stream = null; }
                }

                exception = e;
            }

            return (result);
        }
    }
}
