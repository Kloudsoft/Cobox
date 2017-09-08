using System;
using System.Collections.Generic;
using System.Linq;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System.Data.Entity;
using System.IO;
using System.Drawing;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.ApplicationInsights;
using HouseOfSynergy.PowerTools.Library.Log;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Library.Workflow;
using System.Configuration;
using OptimaJet.Workflow.Core.Runtime;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class OCR
    {
        public static bool TenantDocumentPerformOcr(Global global, Tenant tenant, Document document, OcrEngineSettings ocrEngineSettings, out Exception exception)
        {
            var result = false;
            Document ocrDocument = null;
            List<DocumentTemplate> ocrDocumentTemplate = null;
            List<DocumentFragment> ocrDocumentFragment = null;
            List<DocumentElement> documentElements = null;

            exception = null;

            try
            {
                List<Template> allTemplates = null;
                List<TemplateElement> allElements = null;

                using (var context = new ContextTenant(tenant.DatabaseConnectionString))
                {
                    if (document.DocumentQueueType == DocumentQueueType.Manual)
                    {
                        if (document.TemplateId == null)
                        {
                            throw (new Exception("Template can  not be null. Template should be provided for manual classification"));
                        }
                        else
                        {
                            allTemplates = context.Templates
                                .AsNoTracking()
                                .Include(t => t.Elements)
                                .Include(t => t.Elements.Select(x => x.ElementDetails))
                                .Where(t => (t.IsActive == true) && (t.IsFinalized == true) && (t.Id == document.TemplateId))
                                .ToList();
                        }
                    }
                    else
                    {
                        allTemplates = new List<Template>();
                        var parentTemplates = context.Templates
                                                     .AsNoTracking()
                                                     .Where(x => x.TemplateParent == null)
                                                     .ToList();
                        foreach (var parent in parentTemplates)
                        {
                            if (parent.IsFinalized)
                            {
                                var childtemplates = context.Templates
                                                            .AsNoTracking()
                                                            .Include(t => t.Elements)
                                                            .Include(t => t.Elements.Select(x => x.ElementDetails))
                                                            .Where(x => x.TemplateOriginalId == parent.Id).ToList();
                                childtemplates = childtemplates.Where(x => (x.IsActive) && (x.IsFinalized)).ToList();
                                if (childtemplates.Count > 0)
                                {
                                    var maxChild = childtemplates.Where(x => x.Id == childtemplates.Max(y => y.Id)).SingleOrDefault();
                                    if (maxChild != null)
                                    {
                                        allTemplates.Add(maxChild);
                                    }
                                }
                            }
                        }





                        //allTemplates = context
                        //.Templates
                        //.AsNoTracking()
                        //.Include(t => t.Elements)
                        //.Include(t => t.Elements.Select(x => x.ElementDetails))
                        //.Where(t => (t.IsActive == true) && (t.IsFinalized == true))
                        //.ToList();
                    }
                    //if (allTemplates.Count > 0)
                    //{
                    //    var parents = allTemplates.Where(x => x.TemplateParent == null).ToList();
                    //    var maxchilds = new List<Template>();
                    //    List<Template> alltemplatecopy = new List<Template>();
                    //    foreach (var template in allTemplates)
                    //    {
                    //        alltemplatecopy.Add(template);
                    //    }
                    //    allTemplates.Clear();
                    //    foreach (var parent in parents)
                    //    {
                    //        maxchilds = alltemplatecopy.Where(x => x.TemplateOriginalId == parent.Id).ToList();
                    //        if (maxchilds != null)
                    //        {
                    //            if (maxchilds.Count > 0)
                    //            {
                    //                var maxchild = maxchilds.Where(x => x.Id == maxchilds.Max(y => y.Id)).SingleOrDefault();
                    //                allTemplates.Add(maxchild);
                    //            }
                    //            else
                    //            {
                    //                allTemplates.Add(parent);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            allTemplates.Add(parent);
                    //        }
                    //    }
                    //}

                    if (allTemplates.Count <= 0) { throw (new Exception("No templates found.")); }
                }

                // workerRoleParameters???

                // Download file from azure.
                Stream stream = null;
                List<OcrResultInfo> ocrresultinfos = new List<OcrResultInfo>();

                using (var azureCloudServiceHelper = new AzureCloudStorageAccountHelper(tenant))
                {
                    if (azureCloudServiceHelper.GetTenantDocumentFileStream(tenant, document, out stream, out exception))
                    {
                        Image documentImage = null;

                        using (var image = Image.FromStream(stream))
                        {
                            documentImage = (Image)image.Clone();
                            // LeadTools code.
                            // Stream and image available.
                        }

                        // TODO: Replace with Server.MapPath.
                        bool foundbarcode = false;
                        List<LeadtoolsBarcodeData> barcodedata = null;

                        foundbarcode = Barcode.ReadBarcode(documentImage, out barcodedata, out exception);
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
                            result = AutoOCR(global, ocrEngineSettings, tenant, documentImage, document, allTemplates, out ocrDocument, out documentElements, out ocrDocumentFragment, out ocrDocumentTemplate, out exception);
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
                }
                Guid? processId = Guid.Empty;
                WorkflowEngineHelper wfehelper = null;
                using (var context = new ContextTenant(tenant.DatabaseConnectionString))
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (ocrDocument.State == DocumentState.UnMatched)
                            {
                                ocrDocument.WorkflowState = DocumentWorkflowState.None;
                            }
                            else if (ocrDocument.State == DocumentState.MatchedUnverified)
                            {
                                ocrDocument.WorkflowState = DocumentWorkflowState.None;
                                ///////////////////////////////////////////WorkFLow////////////////////////////////////////////////////
                                //////////////{
                                //////////////    List<string> schemes = new List<string>();
                                //////////////    var connectionString = "";
                                //////////////    try
                                //////////////    {
                                //////////////        connectionString = ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString;
                                //////////////    }
                                //////////////    catch (Exception ex) { throw (new Exception("Unable to find workflow connecgtion string." + ex.Message)); }
                                //////////////    WorkflowEngineHelper.GetAvailableSchemeNames(connectionString, out schemes, out exception);
                                //////////////    if (exception != null) { throw exception; }
                                //////////////    processId = WorkflowEngineHelper.GenerateUniqueProccessId(connectionString, out exception);
                                //////////////    if (exception != null) { throw exception; }
                                //////////////    wfehelper = new WorkflowEngineHelper(connectionString, schemes.First(), processId);
                                //////////////    wfehelper.CreateInstance(out exception);
                                //////////////    if (exception != null) { throw exception; }
                                //////////////    if (processId != null)
                                //////////////    {
                                //////////////        if (processId != Guid.Empty)
                                //////////////        {
                                //////////////            ocrDocument.ProcessInstanceId = processId;
                                //////////////            ocrDocument.SchemeCode = schemes.First();
                                //////////////        }
                                //////////////        else { throw (new Exception("processId can not be empty")); }
                                //////////////    }
                                //////////////    else { throw (new Exception("Unable to generate Workflow process id")); }
                                //////////////}
                                
                            }
                            else if (ocrDocument.State == DocumentState.Verified)
                            {
                                ocrDocument.WorkflowState = DocumentWorkflowState.Verified;
                            }

                            if (ocrDocument != null)
                            {
                                context.Documents.Attach(ocrDocument);
                                context.Entry(ocrDocument).State = EntityState.Modified;
                                context.SaveChanges();

                            }
                            if (documentElements != null)
                            {
                                foreach (var docElement in documentElements)
                                {
                                    context.DocumentElements.Add(docElement);
                                    context.SaveChanges();
                                }
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
                            if (ocrDocument.State == DocumentState.MatchedUnverified)
                            {
                                if (documentElements != null)
                                {
                                    var role = context.Roles.Where(x => x.RoleType == TenantRoleType.Indexer).Include(x=>x.Users).FirstOrDefault();
                                    
                                    if (role != null)
                                    {
                                        var usersRoles = context.UserRoles
                                                            .Include(x => x.User)
                                                            .Where(x => x.RoleId == role.Id)
                                                            .ToList();
                                        if (usersRoles.Count > 0)
                                        {
                                            usersRoles = usersRoles.Take(2).ToList();
                                            for (int i = 0; i < 2; i++)
                                            {
                                                for (int j = 0; j < documentElements.Count(); j++)
                                                {
                                                    DocumentCorrectiveIndexValue docCorrect = new DocumentCorrectiveIndexValue();
                                                    docCorrect.DocumentId = ocrDocument.Id;
                                                    docCorrect.IndexElementId = documentElements[j].TemplateElementId;
                                                    docCorrect.IndexerId = usersRoles[i].UserId;
                                                    docCorrect.IndexValue = documentElements[j].OcrText;
                                                    docCorrect.Status = DocumentCorrectiveIndexValueStatus.None;
                                                    context.DocumentCorrectiveIndexValues.Add(docCorrect);
                                                    context.SaveChanges();
                                                }
                                            }
                                            ocrDocument.WorkflowState = DocumentWorkflowState.WaitingToBeIndexed;
                                            context.Entry(ocrDocument).State = EntityState.Modified;
                                            context.SaveChanges();
                                        }
                                        else { throw (new Exception("Unable to find any indexers")); }
                                    }
                                    else { throw (new Exception("Unable to find any indexers")); }
                                }
                                else { throw (new Exception("Unable to find any elements")); }
                                //var documentCorrectiveIndexValues
                            }




                            //context.Templates.Attach(templates[0]);
                            // context.SaveChanges();
                            // Do not call this line from anywhere else.
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            global.Logger.Write(e.ToString());
                        }
                    }
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
                global.Logger.Write(e.ToString());
            }

            return (result);
        }
        private static bool ExecuteFirstCommand(WorkflowEngineHelper wfehelper, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                List<WorkflowCommand> commands = new List<WorkflowCommand>();
                wfehelper.GetAvailableCommands(out commands, out exception);
                if (exception != null) { throw exception; }
                if (commands.Count > 0)
                {
                    wfehelper.ExecuteCommand(commands.First(), out exception);
                    if (exception != null) { throw exception; }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool AutoOCR(Global global, OcrEngineSettings ocrEngineSettings, Tenant tenant, Image image, Document document, List<Template> alltemplates, out Document documentObj, out List<DocumentElement> documentElements, out List<DocumentFragment> documentfragments, out List<DocumentTemplate> documentTemplate, out Exception exception)
        {
            bool result = false;
            exception = null;
            documentObj = null;
            documentfragments = null;
            documentElements = null;
            documentTemplate = null;
            try
            {
                OcrClassification ocrclassification = new OcrClassification();
                result = ocrclassification.BeginOcrClassification(global, ocrEngineSettings, tenant, image, document, alltemplates, out documentObj, out documentElements, out documentfragments, out documentTemplate, out exception);
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
    }
}

