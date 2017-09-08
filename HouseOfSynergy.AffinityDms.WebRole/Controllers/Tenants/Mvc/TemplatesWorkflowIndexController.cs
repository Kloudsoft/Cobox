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
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TemplatesWorkflowIndexController : Controller
    {

        public class DocIndexes1
        {
            public string IndexName { get; set; }
            public string IndexValue { get; set; }
        }

        public class DocIndexes
        {
            public long NewindexId { get; set; }
            public string Newindexname { get; set; }
            public string Newindexvalue { get; set; }
            public int NewInfexLeft { get; set; }
            public int NewInfexTop { get; set; }
            public int NewInfexWidth { get; set; }
            public int NewInfexHeight { get; set; }

        }

        // GET: TenantTemplates
        //  [HttpGet]
        public ActionResult Index(long id)
        {
            List<DocIndexes> ObjDocIndexes = new List<DocIndexes>();
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            DocumentIndexViewModel documentIndexVM = new DocumentIndexViewModel();
            documentIndexVM.Document = new Document();
            documentIndexVM.DocumentIndexes = new List<DocumentIndex>();
            
            List<DocumentIndex> documentIndexList = null;
            this.ViewBag.ErrorMessage = "";
            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }


            this.ViewBag.CurrentLoggedinUser = tenantUserSession.User;
            try
            {
                if (id > 0)
                {
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {

                        long ID = long.Parse(id.ToString());
                        this.ViewBag.Id = ID;
                        //Document document = null;
                        List<Document> document = new List<Document>();

                        document = context.Documents.Where(x => x.Id == id).ToList();
                        if (exception != null) { throw exception; }
                        if (document == null) { throw (new Exception("Unable to find the document")); }
                        this.ViewBag.CurrentFolderId = document[0].FolderId;
                        this.ViewBag.hdndocId = document[0].Id;
                        this.ViewBag.PdfName = document[0].Name;
                        this.ViewBag.FirstNodeName = "Knowledge";
                        this.ViewBag.FirstNodeUrl = "../../TenantDocumentsFolderWise/Index";
                        this.ViewBag.lastNodeName = "Document Index";
                        this.ViewBag.LastNodeUrl = "../../TenantDocumentIndex/Index?id=" + id;
                        this.ViewBag.lastNodeIdentity = id.ToString();
                        this.ViewBag.LastNodeIdentityUrl = "#";
                        this.ViewBag.isShared = false;
                        this.ViewBag.isPrivate = false;
                        documentIndexVM.Document.WorkflowState = document[0].WorkflowState;




                   
                    // List<Document> parentDocuments = null;
                    //bool res = DocumentManagement.GetDocumentsRelated(tenantUserSession, document.Id, out parentDocuments, out exception);
                    //if (exception != null) { throw (exception); }
                    //this.ViewBag.ParentDocuments = parentDocuments;
                    //var documentVersions = new List<Document>();

                    //res = DocumentManagement.GetDocumentVersionsByDocumentId(tenantUserSession, ID, out documentVersions, out exception);
                    //if (exception != null) { throw exception; }
                    //this.ViewBag.DocumentVersions = documentVersions;

                    //dbresult = DocumentManagement.GetAllDocumentIndexByDocumentId(tenantUserSession, ID, out documentIndexList, out exception);
                    //if (exception != null) { throw exception; }

                    //List<SelectListItem> DataTypeSelectListItem = EnumUtilities.GetSelectItemListFromEnum<DocumentIndexDataType>(out exception);
                    //if (documentIndexList == null)
                    //{
                    //    documentIndexList = new List<DocumentIndex>();
                    //    documentIndexVM.Document = new Document();
                    //    documentIndexVM.DocumentIndexes = new List<DocumentIndex>();
                    //}
                    //else
                    //{
                    //    documentIndexVM.Document = document;
                    //    documentIndexVM.DocumentIndexes = documentIndexList;
                    //}
                    //this.ViewBag.DataTypeSelectListItem = DataTypeSelectListItem;
                    //  GetAllWorkflowScheme();//Bind all workflow scheme
                    

                    //Get Document Indexs  OLD
                   

                        var DocOriginalId = context.Documents.Where(x => ((x.Id == id))).Select(x => x.DocumentOriginalId).First();
                        
                        var documentIDs= context.Documents.Where(x => (x.DocumentOriginalId == DocOriginalId)).Select(x => x.Id).ToList();

                       

                        var DocumentIndexesList = context.ClassifiedFileIndexs.Where(x => documentIDs.Contains(x.documentid)).ToList();
                        if (DocumentIndexesList.Count() > 0)
                        {
                            var DocIndexess = context.ClassifiedFileIndexs.Where(x => documentIDs.Contains(x.documentid)).OrderBy(x=> x.indexvalue).ToList();

                            ///state.ToList()[i].StateName
                            ///
                            /// 
                                                        
                            string UpdatedIndexName = "";
                            string PreviousIdexValue = "";
                            foreach (var item in DocIndexess)
                            {
                                if (PreviousIdexValue != item.indexvalue)
                                {
                                    if (item.indexname.ToUpper().Contains("D/ONO")) { UpdatedIndexName = "DELIVERY ORDER NO"; }
                                    else if ((item.indexname.ToUpper().Contains("INVNO")) || (item.indexname.ToUpper().Contains("INVOICENO"))) { UpdatedIndexName = "INVOICE NO"; }
                                    else if (item.indexname.ToUpper().Contains("ACCOUNTNO")) { UpdatedIndexName = "ACCOUNT NO"; }
                                    else { UpdatedIndexName = item.indexname.ToUpper(); }

                                    //ObjDocIndexes.Add(new DocIndexes { IndexName = UpdatedIndexName, IndexValue = item.indexvalue });
                                    ObjDocIndexes.Add(new DocIndexes 
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

                                PreviousIdexValue = item.indexvalue;
                            }
                            ViewBag.ObjDocIndexes = ObjDocIndexes;
                        }

                        //VendorInfo
                        var DocFolderId = document[0].FolderId;
                        var VendorName = context.Folders.Where(x => (x.Id == DocFolderId)).Select(x => x.Name).First();

                        var VendorFullInfo = context.Vendors.Where(x => (x.VendorName == VendorName)).ToList();

                        string GetInfo = "";
                        foreach (var item in VendorFullInfo)
                        {
                            GetInfo += "<tr><td><b>Name&nbsp;:&nbsp;</b> </td><td>" + item.VendorName + "</td><td> <b> GST&nbsp;:&nbsp;</b>     </td><td>" + item.Gst + "</td></tr>";
                            GetInfo += "<tr><td><b>Address&nbsp;:&nbsp;</b></td><td>" + item.Address + "   </td><td> <b> Phone&nbsp;:&nbsp;</b>   </td><td>" + item.Phone + "</td></tr>";
                            GetInfo += "<tr><td><b>Email&nbsp;:&nbsp;</b></td><td>" + item.Email + "     </td><td> <b>Contact Person&nbsp;:&nbsp;</b> </td><td>" + item.ContactPerson + "</td></tr>";
                        }
                        ViewBag.VendorInfo = GetInfo;


                        //LOg Info


                        //VendorInfo

                        try { 
                        
                        var logFullInfo = context.Logs.Where(x => (x.documentid == id)).ToList();


                         //   var DocOriginalId = context.Documents.Where(x => ((x.Id == id))).Select(x => x.DocumentOriginalId).First();

                        //    var documentIDs = context.Documents.Where(x => (x.DocumentOriginalId == DocOriginalId)).Select(x => x.Id).ToList();



                      //      var DocumentIndexesList = context.ClassifiedFileIndexs.Where(x => documentIDs.Contains(x.documentid)).ToList();
                      //      if (DocumentIndexesList.Count() > 0)
                      //      {
                       //         var DocIndexess = context.ClassifiedFileIndexs.Where(x => documentIDs.Contains(x.documentid)).OrderBy(x => x.indexvalue).ToList();
//
                     //       }

                        string LoggInfo = "";
                        foreach (var item in logFullInfo)
                        {
                                var Username = context.Users.Where(x => (x.Id == item.userid)).Select(x=>x.UserName).First();

                                LoggInfo += "<tr><td>" + item.action + "</td><td>" + item.datetimecreated + "</td><td>" + Username + "</td><td>" + item.comments + "</td></tr>";
                        }
                        ViewBag.Loginfo = LoggInfo;
                        }
                        catch (Exception exx) { }


                    }


                }
                else
                {
                    return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = "Unable to find the document", SuccessMessage = "" });
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
            }
            return this.View("~/Views/Tenants/Templates/TemplateWorkflowIndex.cshtml", documentIndexVM);
        }



        public JsonResult Updateindex(long id, string val,string log, long docid)
        {
            int idd = Convert.ToInt16(id);
            var result = UpdateTemplateIndex(idd, val, log, docid);
            //var json = new JavaScriptSerializer().Serialize(result);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpPost]
        public JsonResult UpdateTemplateIndex(int indexid, string val,string log, long docid)
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

            //LOG
            //try
            //{
            //    if (log != "")
            //    {
            //        Log aln = new Log();
            //        aln.documentid = docid; //document.Id;
            //        aln.action = "Index Edit";
            //        aln.datetimecreated = DateTime.Now;
            //        aln.userid = tenantUserSession.User.Id;
            //        aln.comments = log;
            //        LogManagementcs.AddLog(tenantUserSession, aln, out exception);
            //    }
            //}
            //catch (Exception exx) { }


            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        

        public JsonResult AddLog(string action, string log, long docid)
        {
            var result = AddRemarksLog(action, log, docid);
            //var json = new JavaScriptSerializer().Serialize(result);
            return Json(result.Data.ToString(), JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpPost]
        public JsonResult AddRemarksLog(string action, string log, long docid)
        {

            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            ClassifiedFileIndexs sourceTemplateElement = new ClassifiedFileIndexs();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            try
            {
                if (log != "")
                {
                    Log aln = new Log();
                    aln.documentid = docid; //document.Id;
                    aln.action = action;
                    aln.datetimecreated = DateTime.Now;
                    aln.userid = tenantUserSession.User.Id;
                    aln.comments = log;
                    LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                }
            }
            catch (Exception exx) { }

            string LoggInfo = "";
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                  
                    var logFullInfo = context.Logs.Where(x => (x.documentid == docid)).ToList();
                    foreach (var item in logFullInfo)
                    {
                        var Username = context.Users.Where(x => (x.Id == item.userid)).Select(x => x.UserName).First();

                        LoggInfo += "<tr><td>" + item.action + "</td><td>" + item.datetimecreated + "</td><td>" + Username + "</td><td>" + item.comments + "</td></tr>";
                    }

                    ViewBag.Loginfo = LoggInfo;
                }
            }
            catch (Exception exx) { }
            

            return Json(LoggInfo, JsonRequestBehavior.AllowGet);

        }



        public JsonResult processdocument(long id, int mode, string comments)

        {
            int idd = Convert.ToInt16(id);
            var result = Savedoc(idd, mode, comments);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Http.HttpPost]
        public JsonResult Savedoc(int documentID, int mode, string comments)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Document sourceTemplateElement = new Document();
            Document sourceTemplateElement2 = new Document();
            string Action = "";

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            bool dbresult = false;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                sourceTemplateElement = context.Documents.Where(x => x.Id == documentID).FirstOrDefault();

                if (mode == 3) // Change to Draft
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.Draft); Action = "Draft";
                }
                if (mode == 4) // Change to Submitted
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.Submitted); Action = "Submitted"; }
                if (mode == 5) // Change to Approved
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.Approved); Action = "Approved"; }
                if (mode == 6) // Change to Rework
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.Draft); Action = "Rework"; }
                if (mode == 7) // Change to Closed
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.Closed); Action = "Completed"; }
                if (mode == 8) //  Change to Recoomend
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.Recommend); Action = "Recommend"; }
                if (mode == 9) //  Change to Adivse
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.Advised); Action = "Adivse"; }
                if (mode == 10) //  Change to  Process Payment
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.ProcessPayment); Action = "Process Payment"; }
                if (mode == 11) //  Change to ReworkSM
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.ReworkSM); Action = "Rework To SM"; }
                if (mode == 12) //  Change to ReworkPM
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.ReworkPM); Action = "Rework To PM"; }
                if (mode == 13) // Change to ReworkSSOAD
                { sourceTemplateElement = checkNmovefolder(sourceTemplateElement, DocumentWorkflowState.ReworkSSOAD); Action = "Rework To SSOAD"; }

                try
                {
                   // if (comments != "")
                  //  {
                        Log aln = new Log();
                        aln.documentid = documentID; //document.Id;
                        aln.action = Action;
                        aln.datetimecreated = DateTime.Now;
                        aln.userid = tenantUserSession.User.Id;
                        aln.comments = comments;
                        LogManagementcs.AddLog(tenantUserSession, aln, out exception);
                   // }
                }
                catch (Exception exx) { }


            }
            if (exception != null)
            {
                throw exception;
            }
            //var result = ListIndex(Convert.ToInt64(templateid));
            var data = new
            {
                rtnmode = mode
            };
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public Document checkNmovefolder(Document sourceTemplateElement, DocumentWorkflowState State)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            
            Folder folder = new Folder();
            Folder Isfolder = new Folder();

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            bool dbresult = false;
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {

                folder = context.Folders.Where(x => x.Id == sourceTemplateElement.FolderId).First();

                long CompletedFolderId = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderCompleted"]);

                Isfolder = context.Folders.Where(x => x.ParentId == CompletedFolderId && x.Name == folder.Name).First();
                if (Isfolder != null)
                {
                    //
                    var Docs = context.Documents.Where(x => x.DocumentOriginalId == sourceTemplateElement.DocumentOriginalId).ToList();

                    if (State == DocumentWorkflowState.Closed)
                    {
                        Docs.ForEach(a => a.FolderId = Isfolder.Id);
                        Docs.ForEach(a => a.WorkflowState = State);
                    }
                    else
                    {
                        Docs.ForEach(a => a.WorkflowState = State);
                    }
                    context.SaveChanges();
                }
                else
                {
                    //Create Folder. But folder creating when DO and INVOCIE automerging (TenantTemplateIndexListController - function Name createfolder)
                }

                //folder = context.Folders.Where(x => x.Id == sourceTemplateElement.FolderId).First();

                //long CompletedFolderId = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["FolderCompleted"]);

                //Isfolder = context.Folders.Where(x => x.ParentId == CompletedFolderId && x.Name == folder.Name).First();
                //if (Isfolder != null)
                //{
                //    sourceTemplateElement.FolderId = Isfolder.Id;
                //    sourceTemplateElement.WorkflowState = DocumentWorkflowState.Closed;
                //}
                //else
                //{
                //    //Create Folder. But folder creating when DO and INVOCIE automerging (TenantTemplateIndexListController - function Name createfolder)
                //}


            }

            return sourceTemplateElement;
        }


        public ActionResult CheckoutTemplate(long id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            Template template = null;
            List<TemplateElement> templateElement = null;
            List<TemplateElementDetail> templateElementDetail = null;
            bool result = false;
            TemplateElementListAndElementDetailListViewModel model = new TemplateElementListAndElementDetailListViewModel();

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                result = ElementManagement.CheckoutTemplateAndMakeNewVersion(tenantUserSession, id, out template, out templateElement, out templateElementDetail, out exception);
                if (exception != null) { throw exception; }

                model.template = new Template();
                model.elements = new List<TemplateElement>();
                model.elementsdetails = new List<TemplateElementDetail>();
                if (template != null)
                {
                    model.template = template;
                    if (templateElement != null)
                        model.elements = templateElement;
                    if (templateElementDetail != null)
                        model.elementsdetails = templateElementDetail;
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { id = -1, ErrorMessage = ExceptionUtilities.ExceptionToString(ex) });
            }
            return RedirectToAction("Index", "TenantTemplateTestDesign", new { id = template.Id });
            //return this.View("~/Views/Tenants/Templates/TemplateDesign.cshtml", model);
        }

        //[HttpPost]
        public ActionResult DeleteTemplate(Template template, string H_Templateid)
        {
            Exception exception = null;
            bool dbresult = false;
            long templateid = 0;
            try
            {
                H_Templateid = Request.QueryString["id"].ToString();   
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                if (H_Templateid != null)
                {
                    templateid = long.Parse(H_Templateid);
                    Template sourcetemplate = null;
                    dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateid, out sourcetemplate, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((sourcetemplate != null) && dbresult)
                    {
                        if (sourcetemplate.TemplateType == TemplateType.Template)
                        {

                            dbresult = ElementManagement.DeleteTemplate(tenantUserSession, sourcetemplate, out exception);

                            if (exception != null)
                            {
                                throw exception;
                            }
                            if (dbresult)
                            {
                                this.ViewBag.ErrorMessage = string.Empty;
                                return RedirectToAction("Index", "TenantTemplates", null);
                            }
                            else
                            {
                                Exception ex = new Exception("Unable to update the template");
                                throw ex;
                            }
                        }
                        else
                        {
                            throw (new Exception("The following template is not a form type."));
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
                return this.View("~/Views/Tenants/Templates/", template);
            }
            return RedirectToAction("Index");
        }



        public JsonResult SetUserRights(List<long> UserList, long Id)
        {
            bool result = false;
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (Id > 0)
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                    result = TenantUserManagement.UserRightsForTemplates(tenantUserSession, Id, UserList, out exception);
                    if (!result) { if (exception != null) { throw exception; } }
                }
                else
                {
                    throw (new Exception("Unable to proccess user rights"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return Json(exception);
            }
            return Json(result);
        }
        public ActionResult GetAllUsersForTemplate(long templateId)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;
            bool result = false;
            List<User> users = null;
            try
            {
                if (templateId > 0)
                {
                    if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                    result = TemplateAndFormHelper.GetUsersForSelection(tenantUserSession, templateId, TemplateType.Template, out users, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    throw (new Exception("Unable to find the following Template"));
                }
            }
            catch (Exception ex)
            {

                this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex);
                return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", null);
            }
            return PartialView("~/Views/Tenants/Users/_TenantUsersSelection.cshtml", users);
        }
        public JsonResult AddRemoveUser(long templateId, List<long> selectedUsers)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            List<User> users = new List<User>();

            try
            {
                if (!(TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception))) { if (exception != null) { throw exception; } }
                bool res = TemplateAndFormHelper.SetSelectedUsers(tenantUserSession, templateId, selectedUsers, TemplateType.Template, out users, out exception);
                if (exception != null) { throw exception; }
            }
            catch (Exception ex)
            {
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }

            var serialized = JsonConvert.SerializeObject(users, Formatting.Indented,
                                                            new JsonSerializerSettings
                                                            {
                                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                            });
            return Json(serialized);
        }
        public JsonResult InviteUser(long templateId, string email)
        {
            bool result = false;
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            var userObj = new User();
            var user = new User();
            try
            {

                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { if (exception != null) { throw exception; } }
                if ((templateId > 0))
                {
                    Template template = null;
                    ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateId, out template, out exception);
                    if (template == null) { throw (new Exception("Unable to find the following template")); }
                    if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrEmpty(email))
                    {
                        result = TenantUserManagement.GetUserByEmail(tenantUserSession, email, out user, out exception);
                        if (exception != null) { throw exception; }
                        if (user.Id > 0) { throw (new Exception("User already esists.")); }
                        // var userName = email;
                        var password = "";
                        result = TenantUserManagement.InviteUser(tenantUserSession, email, Url.Action("Index", "TenantTemplateProtectedView", new { id = templateId }), templateId, "Template", out password, out userObj, out exception);
                        if (exception != null) { throw exception; }
                        if (userObj.Id > 0)
                        {
                            var AppConfig = System.Configuration.ConfigurationManager.AppSettings;
                            // string recieverName = userObj.UserName;
                            string senderName = tenantUserSession.User.NameFull;
                            string url = "http://" + (new Uri(tenantUserSession.Tenant.UrlApi).Host) + "/TenantSignIn/AcceptInvite?key=" + userObj.InviteGuid.ToString() + "&domain=" + tenantUserSession.Tenant.Domain;
                            string emailBody = EmailTemplateConstants.InviteExternalUserByEmail.Replace("[{RecieverEmail}]", userObj.Email).Replace("[{SenderUsername}]", tenantUserSession.User.NameFull).Replace("[{SenderCompanyName}]", tenantUserSession.Tenant.CompanyName).Replace("[{EnitityType}]", EntityType.Template.ToString()).Replace("[{sharedEntityName}]", template.Title).Replace("[{LinkForInvite}]", url).Replace("[{RecieverEmail}]", userObj.Email).Replace("[{RecieverPassword}]", password);
                            //string emailBody = $"<strong>Hi {recieverName},</strong> <br>You have been invited by {senderName} for a discussion. Please click <a href='{ url }'><strong>here</strong></a> or copy-paste the link in your browser and login with the following credentials:<br>" + url + "<br>UserName:" + userName + "<br>Password:" + password;
                            string subject = "Invitation for Template";
                            EmailUtilities emailUtil = new EmailUtilities(userObj.Email.ToString(), AppConfig["EmailFrom"].ToString(), AppConfig["EmailPassword"].ToString(), emailBody, senderName, subject, int.Parse(AppConfig["EmailPort"]), AppConfig["EmailHost"], out exception, Boolean.Parse(AppConfig["IsBodyHtml"]), Boolean.Parse(AppConfig["IsBodyHtml"]));
                            if (exception != null) { throw exception; }
                            result = emailUtil.SendEmail(out exception);
                        }
                        else
                        { result = false; }
                    }
                    else
                    { throw (new Exception("UserName and Email is Required")); }
                }
                else
                { throw (new Exception("Unable to find the following Template")); }
            }
            catch (Exception ex)
            {
                result = false;
                return Json(ExceptionUtilities.ExceptionToString(ex));
            }
            return Json(result);
        }

        public static List<Template> GetAllTemplates(TenantUserSession tenantUserSession,out Exception exception)
        {
            exception = null; 
            List<Template> templates = new List<Template>();
            bool dbresult = ElementManagement.GetAllMaxVersionTemplates(tenantUserSession, out templates, out exception);
            if (exception != null)
            {
                throw exception;
            }
            if (templates != null)
            {
                if (templates.Count > 0)
                {
                    templates = templates.Where(x => x.TemplateType == TemplateType.Template).ToList();
                }
            }
            else
            {
                templates = new List<Template>();
            }

            return templates;
        }
    }
}