using HouseOfSynergy.AffinityDms.Library.Workflow;
using OptimaJet.Workflow;
using OptimaJet.Workflow.Core.Builder;
using OptimaJet.Workflow.Core.Bus;
using OptimaJet.Workflow.Core.Runtime;
using OptimaJet.Workflow.Core.Parser;
using System.Collections.Generic;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using WorkflowRuntime = OptimaJet.Workflow.Core.Runtime.WorkflowRuntime;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System.Data;
//using System.Web.Http.Cors;

namespace WF.Sample.Controllers
{
  //  [EnableCors(origins: "http://affinity-ecm.azurewebsites.net/", headers: "*", methods: "*")]
    public class DesignerController : Controller
    {
       
        public ActionResult Index()
        {
            Exception exception = null;
            List<WorkflowViewModel> workflowViewModels = new List<WorkflowViewModel>();
            DataTable dtworkflow = new DataTable();
            try
            {
                WorkflowEngineHelper wfehelper = new WorkflowEngineHelper(ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString, "", null);
                WorkflowEngineHelper.GetAvailableSchemes(ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString,out dtworkflow, out exception);
                if (dtworkflow.Rows.Count > 0)
                {
                    foreach (DataRow item in dtworkflow.Rows)
                    {
                        WorkflowViewModel workflowmodel = new WorkflowViewModel();
                        workflowmodel.Code = item["Code"].ToString();
                        workflowmodel.Scheme = item["Scheme"].ToString();
                        workflowViewModels.Add(workflowmodel);
                    }
                }
                if (exception != null)
                    throw exception;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.ViewBag.Exception = ex.InnerException.Message;
                else
                    this.ViewBag.Exception = ex.Message;

            }

                return View("~/Views/Designer/Workflows.cshtml",workflowViewModels);
        }

        public ActionResult AddScheme()
        {
            return View("~/Views/Designer/index.cshtml");
        }
        public ActionResult EditScheme(string schemecode)
        {
            this.ViewBag.SchemeCode = schemecode;
            return View("~/Views/Designer/index.cshtml");
        }
        public ActionResult API(string schemecode)
        {
            Stream filestream = null;
            if (Request.Files.Count > 0)
                filestream = Request.Files[0].InputStream;

            var pars = new NameValueCollection();
            pars.Add(Request.Params);
            
            if(Request.HttpMethod.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
            {
                var parsKeys = pars.AllKeys;
                foreach (var key in Request.Form.AllKeys)
                {
                    if (!parsKeys.Contains(key))
                    {
                        pars.Add(Request.Form);
                    }
                }
            }
            WorkflowEngineHelper wfehelper = new WorkflowEngineHelper(ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString, schemecode, null);

            var res = wfehelper.getRuntime.DesignerAPI(pars, filestream, true);
            if (pars["operation"].ToLower() == "downloadscheme")
                return File(Encoding.UTF8.GetBytes(res), "text/xml", "scheme.xml");
            return Content(res);
        }

        //private static volatile WorkflowRuntime _runtime;
        //private static readonly object _sync = new object();
        //private WorkflowRuntime getRuntime
        //{
        //    get
        //    {
        //        if (_runtime == null)
        //        {
        //            lock (_sync)
        //            {
        //                if (_runtime == null)
        //                {
        //                    var connectionString = ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString;
        //                    var builder = new WorkflowBuilder<XElement>(
        //                        new OptimaJet.Workflow.DbPersistence.DbXmlWorkflowGenerator(connectionString),
        //                        new OptimaJet.Workflow.Core.Parser.XmlWorkflowParser(),
        //                        new OptimaJet.Workflow.DbPersistence.DbSchemePersistenceProvider(connectionString)
        //                        ).WithDefaultCache();
        //                    _runtime = new WorkflowRuntime(new Guid("{8D38DB8F-F3D5-4F26-A989-4FDD40F32D9D}"))
        //                        .WithBuilder(builder)
        //                        .WithPersistenceProvider(new OptimaJet.Workflow.DbPersistence.DbPersistenceProvider(connectionString))
        //                        .WithTimerManager(new TimerManager())
        //                        .WithBus(new NullBus())
        //                        .SwitchAutoUpdateSchemeBeforeGetAvailableCommandsOn()
        //                        .Start();
        //                }
        //            }
        //        }
        //        return _runtime;
        //    }
        //}
    }
}


 