using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.Library;
using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace HouseOfSynergy.AffinityDms.WebRole
{
	public class WebRole: RoleEntryPoint
	{
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        ////////////////////////////////////////////////////////////private RoleDiagnostics roleDiagnostics;
        ////////////////////////////////////////////////////////////public TelemetryClient telemetryClient;
        //      public override bool OnStart ()
        //{
        //          // For information on handling configuration changes
        //          // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

        //          ////////////////////////////////////////////////////////Exception exception = null;
        //          ////////////////////////////////////////////////////////startDiagnostics();

        //         // Trace.AutoFlush = true;
        //          //Trace.TraceInformation("TraceInfo:: HouseOfSynergy.AffinityDms.WebRole is Starting");
        //          //Trace.Refresh();
        //          //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WebTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: HouseOfSynergy.AffinityDms.WebRole is running");

        //          ///////////////////////////////////////////////////////telemetryClient.TrackEvent("TelemetryWeb:: HouseOfSynergy.AffinityDms.WebRole is running");

        //          //IISCertificateHelper.IISCertificateConfiguration(telemetryClient, IISConstants.SiteName, IISConstants.clientCertIssuer, IISConstants.clientCertName, IISConstants.Username, IISConstants.Password, IISConstants.base64CertClient, out exception);
        //          //if (exception != null)
        //          //{
        //          //    //Trace.WriteLine("TraceInfo:: " + exception.ToString());
        //          //   // roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WebTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: " + exception.ToString());
        //          //    telemetryClient.TrackEvent("TelemetryWeb:: Exception: " + exception.ToString());
        //          //}
        //          //else
        //          //{
        //          //    //Trace.WriteLine("TraceInfo:: Running");
        //          //   // roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WebTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: Running");
        //          //    telemetryClient.TrackEvent("TelemetryWeb:: Running");
        //          //}
        //          return base.OnStart();
        //      }
        //      private void startDiagnostics()
        //      {
        //          //if (roleDiagnostics == null)
        //          //{
        //          //    roleDiagnostics = new RoleDiagnostics(RoleDiagnostics.Role.Web);
        //          //}
        //          //////////////////////////////////////if (telemetryClient == null)
        //          //////////////////////////////////////{
        //          //////////////////////////////////////    telemetryClient = new TelemetryClient();
        //          //////////////////////////////////////    telemetryClient.Context.InstrumentationKey = "ed590912-a295-455a-a757-9416c939d5fa";
        //          //////////////////////////////////////}
        //      }
    }
}
