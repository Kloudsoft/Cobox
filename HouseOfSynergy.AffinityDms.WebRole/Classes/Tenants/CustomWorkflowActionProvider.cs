using OptimaJet.Workflow.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OptimaJet.Workflow.Core.Model;
using System.Diagnostics;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
    public class CustomWorkflowActionProvider :
        IWorkflowActionProvider
    {
        public void ExecuteAction(string name, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            Debugger.Break();
            //WF.Sample.Controllers.DesignerController dc = new WF.Sample.Controllers.DesignerController();
            //dc.getRuntime.
        }

        public bool ExecuteCondition(string name, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            Debugger.Break();

            return (true);
        }

        public List<string> GetActions()
        {
            Debugger.Break();

            return (new string[] { "UpdateEmail", }.ToList());
        }
    }
}