using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HouseOfSynergy.AffinityDms.WebRole.Pages
{
	public partial class Test: System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
            //try {
            //    Debugger.Break();
            //    string schemeCode = "SimpleWF";
            //    Guid? processId = null;
            //    processId = Guid.NewGuid();
            //    var runtime = new WF.Sample.Controllers.DesignerController().getRuntime;
            //    runtime.CreateInstance(schemeCode, processId.Value);

            //    var command = runtime.GetAvailableCommands(processId.Value, string.Empty)
            //        .Where(c => c.CommandName.Trim().ToLower() == "starttoroute").FirstOrDefault();

            //    if (command != null)
            //    {
            //        runtime.ExecuteCommand(processId.Value, string.Empty, string.Empty, command);
            //    }

            //    //runtime.ActionProvider.ExecuteAction("UpdateEmail", runtime.Builder.GetProcessInstance(processId.Value), runtime, string.Empty);

            //    command = runtime.GetAvailableCommands(processId.Value, string.Empty)
            //        .Where(c => c.CommandName.Trim().ToLower() == "approve").FirstOrDefault();

            //    if (command != null)
            //    {
            //        runtime.ExecuteCommand(processId.Value, string.Empty, string.Empty, command);
            //    }

            //    runtime.ActionProvider.ExecuteAction("UpdateEmail", runtime.Builder.GetProcessInstance(processId.Value), runtime, string.Empty);

            //}
            //catch (Exception ex) {
            //    Debugger.Break();
            //}
            
        }
	}

}