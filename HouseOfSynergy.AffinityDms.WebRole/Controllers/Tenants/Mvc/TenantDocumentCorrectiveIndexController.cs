using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library.Workflow;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using OptimaJet.Workflow.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantDocumentCorrectiveIndexController : Controller
    {
        // GET: DocumentCorrectiveIndex
        [HttpGet]
        public ActionResult Index(long? Id)
        {
            TenantUserSession tenantUserSession = null;
            Exception exception = null;
            List<DocumentCorrectiveIndexValue> documentCorrectiveIndexList = new List<DocumentCorrectiveIndexValue>();
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { return (this.RedirectToRoute("TenantSignIn")); }
                bool result = DocumentManagement.GetDocumentCorrectiveIndexValuesByDocumentId(tenantUserSession, (long)Id, out documentCorrectiveIndexList, out exception);
                if (exception != null) { throw exception; }
                if (documentCorrectiveIndexList.Count > 0)
                {
                    documentCorrectiveIndexList = documentCorrectiveIndexList.Where(x => x.IndexerId == tenantUserSession.User.Id).ToList();
                    if (documentCorrectiveIndexList.Count <= 0) { throw (new Exception("You are assigned to index the following document.")); }
                }
                else
                {
                    throw (new Exception("Unable to find the following document."));
                }
            }
            catch (Exception ex)
            {
                var message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = message, SuccessMessage = string.Empty });
            }
            return View("~/Views/Tenants/Documents/DocumentCorrectiveIndexValues.cshtml",documentCorrectiveIndexList);
        }
        [HttpPost]
        public ActionResult Update(List<DocumentCorrectiveIndexValue> item)
        {
            Exception exception;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                var result = DocumentManagement.UpdateDocumentCorrectiveIndexValues(tenantUserSession, item, out exception);
                if (exception != null) { throw exception; }
                if (result)
                {
                    item.ForEach(x => x.Status = DocumentCorrectiveIndexValueStatus.Updated);
                    this.ViewBag.Success = "Successfully indexed the following document.";
                }
            }
            catch (UnauthorizedAccessException authEx)
            {
                var ExStr = (authEx.InnerException != null) ? (authEx.InnerException.Message) : (authEx.Message.ToString());
                return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = ExStr, SuccessMessage = "Successfully indexed" });
            }
            catch (Exception ex)
            {
                var ExStr = (ex.InnerException != null) ? (ex.InnerException.Message) : (ex.Message.ToString());
                ModelState.AddModelError("Exception", ExStr);
            }
            return View("~/Views/Tenants/Documents/DocumentCorrectiveIndexValues.cshtml", item);
        }

        public JsonResult ConfirmIndexing(long id,long documentId)
        {
            Exception exception;
            TenantUserSession tenantUserSession = null;
            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw exception; }
                var result = DocumentManagement.ConfirmDocumentCorrectiveIndexValues(tenantUserSession, id,documentId, out exception);
                if (exception != null) { throw exception; }
               // bool rresult = ExecuteCorrectiveIndexingWorkflow(tenantUserSession, id, documentId,out exception);
            }
            catch (Exception ex)
            {
                var ExStr = (ex.InnerException != null) ? (ex.InnerException.Message) : (ex.Message.ToString());
                // ModelState.AddModelError("Exception", ExStr);
                return Json(ExStr, JsonRequestBehavior.AllowGet); //RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = ExStr, SuccessMessage = string.Empty });
            }
            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "TenantDocumentsFolderWise", new { ErrorMessage = string.Empty, SuccessMessage = "Document indexing successfully submitted" });
            //return View("~/Views/Tenants/Documents/DocumentCorrectiveIndexValues.cshtml", item);
        }
        //private bool ExecuteCorrectiveIndexingWorkflow(TenantUserSession tenantUserSession, long id, long documentId,out Exception exception)
        //{
        //    bool result = false;
        //    exception = null;
        //    var connectionString = "";
        //    List<string> schemes = new List<string>();
        //    var currentScheme = "CorrectiveIndexWF";
        //    Document document = null;
        //    try
        //    {

        //        ///////////////////////////////////////////////////////////////////////Workflow/////////////////////////////////////////////////////////////////
        //        //////////////////{
        //        //////////////////    connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString; ;
        //        //////////////////    result = DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
        //        //////////////////    if (exception != null) { throw exception; }
        //        //////////////////    if (document.ProcessInstanceId == null) { throw (new Exception("No workflow initiated for the following document")); }
        //        //////////////////    if (document.ProcessInstanceId == Guid.Empty) { throw (new Exception("Workflow instance can not be empty")); }
        //        //////////////////    WorkflowEngineHelper wfe = new WorkflowEngineHelper(connectionString, currentScheme, document.ProcessInstanceId);
        //        //////////////////    List<WorkflowCommand> workflowCommand = new List<WorkflowCommand>();
        //        //////////////////    bool wfresult = wfe.GetAvailableCommands(out workflowCommand, out exception);
        //        //////////////////    if (exception != null) { throw exception; }
        //        //////////////////    WorkflowState currentState = wfe.getRuntime.GetCurrentState((Guid)wfe.processId);
        //        //////////////////    try
        //        //////////////////    {
        //        //////////////////        if (currentState.Name == "")
        //        //////////////////        {

        //        //////////////////        }
        //        //////////////////    }
        //        //////////////////    catch (Exception)
        //        //////////////////    {
        //        //////////////////    }
        //        //////////////////}
                
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return result;
        //}
    }
}