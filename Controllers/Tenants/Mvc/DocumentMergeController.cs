using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using Newtonsoft.Json;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using System.IO;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
//using WebSupergoo.ABCpdf10;

using Leadtools;
using Leadtools.Pdf;
using Leadtools.Codecs;


namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class DocumentMergeController : Controller
    {
        // GET: DocumentMerge
        public ActionResult Index(long id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            List<Document> maxverdocuments = new List<Document>();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
            this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    maxverdocuments = context.Documents.Where(x => ((x.WorkflowState == DocumentWorkflowState.Draft))).ToList();
                    if (maxverdocuments.Count() > 0)
                    {
                        long FolderDraft = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);
                        //var Docs = context.Documents.Where(x => ((x.WorkflowState == DocumentWorkflowState.Draft && x.FolderId == FolderDraft && x.Id != id))).Select(x => new { DocId= x.Id,DocName=x.Name}).ToList();

                        maxverdocuments = DocumentManagement.GetMaxVersionDocuments(tenantUserSession, maxverdocuments);

                        var Docs = (
                            from doc in maxverdocuments
                            join fold in context.Folders
                            on doc.FolderId equals fold.Id
                            where fold.ParentId == FolderDraft && doc.WorkflowState == DocumentWorkflowState.Draft //&& doc.VersionMajor==1
                            select new
                            {
                                DocId = doc.Id,
                                DocName = doc.Name
                            }).ToList();

                        TempData["Docs"] = Docs;
                        ViewBag.Docs = new SelectList(Docs, "DocID", "DocName");
                        ViewBag.Id = id;

                    }
                }
            }
            catch (Exception ex) { }

            return View("~/Views/Tenants/Documents/DocumentMerge.cshtml");
        }

        public long CheckoutDocument(int Page1, int Page2)
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

                    id = Page1;
                    try
                    {
                        var thisdoc = context.Documents.Where(x => x.Id == id).FirstOrDefault();
                        thisdoc.LatestCheckedOutByUserId = 0;
                        context.SaveChanges();
                    }
                    catch (Exception ecc) { }


                    result = DocumentManagement.CheckoutDoucmentAndMakeNewVersion(tenantUserSession, id, out document, out exception);
                    if (exception != null) { throw exception; }
                    return document.Id;

                }


            }
            catch (Exception ex)
            {

                return 0;
            }
            return 0;
        }

        public JsonResult processdocumentmerge(long id, int Id2)
        {
            int Page1 = Convert.ToInt16(id);
            int Page2 = Convert.ToInt16(Id2);
            var result = Mergedocs(Page1, Page2);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpPost]
        public JsonResult Mergedocs(int Page1, int Page2)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Document DocuemtnPage1 = new Document();
            Document DocuemtnPage2 = new Document();
            UserDocument userdocument = new UserDocument();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            bool dbresult = false;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                //Create New version
                long NewVersiondocumentid = CheckoutDocument(Page1, Page2);
                if ((NewVersiondocumentid != 0) && (NewVersiondocumentid != null))
                {

                    DocuemtnPage1 = context.Documents.Where(x => x.Id == NewVersiondocumentid).FirstOrDefault();
                    DocuemtnPage2 = context.Documents.Where(x => x.Id == Page2).FirstOrDefault();

                    userdocument = context.UserDocuments.Where(x => x.DocumentId == Page2).FirstOrDefault();

                    //WebSupergoo.ABCpdf9.Doc theDoc = new WebSupergoo.ABCpdf9.Doc();

                    var pathTenantDoc = Server.MapPath("~/UploadedFiles/Tenants/" + tenantUserSession.Tenant.MasterTenantId + "/Documents/");

                    string GetExtn = Path.GetExtension(DocuemtnPage1.Name);
                    string GetwithnoExtn = Path.GetFileNameWithoutExtension(DocuemtnPage1.Name);
                    string Newfilname = "";
                    string filenamenew = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                    Newfilname = filenamenew + "-MM.pdf";
                    if (GetExtn.ToLower() == ".pdf")
                    {
                        


                        try
                        {
                            string strLIC = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic";
                            string strLICKey = Server.MapPath("~/App_Data/LeadToolsV2/") + "License/Kloud-Soft Pte Ltd-Document Imaging Suite v19 (DOCSTE19)_OEM-19.lic.key";

                            RasterSupport.SetLicense(strLIC, System.IO.File.ReadAllText(strLICKey));

                            RasterCodecs _codecs = new RasterCodecs();
                            //_codecs.Options.Pdf.InitialPath = @"C:\Temp\PDFEngine";
                            _codecs.Options.Pdf.InitialPath = Server.MapPath("~/PDFEngine/");

                            if (_codecs.Options.Pdf.IsEngineInstalled)
                            {
                                PDFFile firstFile = new PDFFile(pathTenantDoc + DocuemtnPage1.Name);
                                firstFile.MergeWith(new string[] { pathTenantDoc + DocuemtnPage2.Name }, pathTenantDoc + Newfilname);
                            }

                            // Load the input PDF document


                            try
                            {
                                Log aln = new Log();
                                aln.documentid = NewVersiondocumentid; //document.Id;
                                aln.action = "Manual Merge";
                                aln.datetimecreated = DateTime.Now;
                                aln.userid = tenantUserSession.User.Id;
                                LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                            }
                            catch (Exception exx) { }
                        }
                        catch (Exception exx) { }
                        
                        //theDoc.Read(pathTenantDoc + DocuemtnPage1.Name);
                        //int theCount = theDoc.PageCount;
                        //theDoc.Rect.Inset(0, 0);
                        //theDoc.Page = theDoc.AddPage();
                        //Image image = Image.FromFile(pathTenantDoc + DocuemtnPage2.Name, true);
                        //Bitmap bmp = new Bitmap(image);
                        //theDoc.AddImageBitmap(bmp, true);
                        //Newfilname = GetwithnoExtn + "-V." + (DocuemtnPage1.VersionMajor) + ".pdf";
                        //theDoc.Save(pathTenantDoc + Newfilname);
                        //theDoc.Clear();

                        //DocuemtnPage1.VersionMajor = DocuemtnPage1.VersionMajor + 1;
                        DocuemtnPage1.Name = Newfilname;
                        context.SaveChanges();

                        //DocuemtnPage2.FolderId = DocuemtnPage1.FolderId;
                        //context.SaveChanges();

                        if (DocuemtnPage2 != null)
                        {

                            context.Entry(userdocument).State = System.Data.Entity.EntityState.Deleted;
                            context.SaveChanges();

                            context.Entry(DocuemtnPage2).State = System.Data.Entity.EntityState.Deleted;
                            context.SaveChanges();

                        }

                    }
                    else
                    {
                        ////Page 1

                        //theDoc.Rect.Inset(0, 0);
                        //theDoc.Page = theDoc.AddPage();
                        //string thePath = pathTenantDoc + DocuemtnPage1.Name;
                        ////theDoc.AddImageFile(thePath, 1);
                        //Image image = Image.FromFile(thePath, true);
                        //Bitmap bmp = new Bitmap(image);
                        //theDoc.AddImageBitmap(bmp, true);

                        ////Page 2

                        //theDoc.Rect.Inset(0, 0);
                        //theDoc.Page = theDoc.AddPage();
                        //string thePath2 = pathTenantDoc + DocuemtnPage2.Name;
                        //Image image2 = Image.FromFile(thePath2, true);
                        //Bitmap bmp2 = new Bitmap(image2);
                        //theDoc.AddImageBitmap(bmp2, true);

                        ////theDoc.AddImageFile(thePath2, 2);

                        //string filewqiuthnoxtn = Path.GetFileNameWithoutExtension(DocuemtnPage1.Name);

                        //theDoc.Save(pathTenantDoc + filewqiuthnoxtn + ".pdf");
                        //theDoc.Clear();

                        //DocuemtnPage1.Name = filewqiuthnoxtn + ".pdf";
                        //// DocuemtnPage1.VersionMajor = DocuemtnPage1.VersionMajor + 1;
                        //context.SaveChanges();

                        ////DocuemtnPage2.FolderId = DocuemtnPage1.FolderId;
                        ////context.SaveChanges();

                        //if (DocuemtnPage2 != null)
                        //{

                        //    context.Entry(userdocument).State = System.Data.Entity.EntityState.Deleted;
                        //    context.SaveChanges();

                        //    context.Entry(DocuemtnPage2).State = System.Data.Entity.EntityState.Deleted;
                        //    context.SaveChanges();
                        //}

                    }



                    //DocuemtnPage1.FolderId= Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderDraft"]);


                }

            }

            if (exception != null)
            {

                throw exception;
                return Json(exception.InnerException, JsonRequestBehavior.AllowGet);
            }
            //var result = ListIndex(Convert.ToInt64(templateid));
            var data = new
            {
                rtnmode = 0
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}