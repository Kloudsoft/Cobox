using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.BusinessLayer.Ocr;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using System.IO;
using System.Reflection;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Library;
using Microsoft.ApplicationInsights;

namespace HouseOfSynergy.AffinityDms.OcrWorkerRole
{
    public class WorkerRole :
        RoleEntryPoint
    {
        private readonly ManualResetEvent RunCompleteManualResetEvent = new ManualResetEvent(false);
        private readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
       // private static RoleDiagnostics roleDiagnostics;
        public TelemetryClient telemetryClient;
        private void simulateTemplateCreation(OcrEngineSettings ocrEngineSettings,Tenant tenant)
        {

        }
        private void startDiagnostics()
        {
            //if (roleDiagnostics == null)
            //{
            //    roleDiagnostics = new RoleDiagnostics(RoleDiagnostics.Role.Worker);
            //}
            if(telemetryClient ==null)
            {
                telemetryClient = new TelemetryClient();
                telemetryClient.Context.InstrumentationKey = "ed590912-a295-455a-a757-9416c939d5fa";
            }
        }
        public override void Run()
        {
            //if (roleDiagnostics == null)
            //{
            //    startDiagnostics();
            //}
            //Trace.AutoFlush = true;
            //Trace.TraceInformation("TraceInfo:: HouseOfSynergy.AffinityDms.OcrWorkerRole is running");
            //Trace.Refresh();
            //Trace.WriteLine("TraceInfo:: WorkerRole::Run - " + roleDiagnostics.WorkerTrace.Switch.Level.ToString());
            //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information,RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: Diagnostics:: HouseOfSynergy.AffinityDms.OcrWorkerRole is running");
            telemetryClient.TrackEvent("TelemetryWorker:: HouseOfSynergy.AffinityDms.OcrWorkerRole is running");
            ////////try
            ////////{
            ////////    using
            ////////    (
            ////////         var ocrEngineSettings = new OcrEngineSettings
            ////////         (
            ////////             File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LeadTools", "License", "eval-license-files.lic.key")),
            ////////             Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LeadTools", "License", "eval-license-files.lic"),
            ////////             Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data"),
            ////////             Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))//, "LeadTools", "Runtime")
            ////////         )
            ////////     )
            ////////    {

            ////////        // \\Root\Data\Tenants\00001\Templates\0001
            ////////        //ocrEngineSettings.GetPathDataTenantTemplate(tenant, template).FullName + ".bin";
            ////////        //ocrEngineSettings.GetPathDataTenantTemplate(tenant, template).FullName + ".xml";
            ////////        //ocrEngineSettings.GetPathDataTenantTemplate(tenant, template).FullName + ".tiff";

            ////////        if (AffinityConfiguration.IsConfigurationDebug)
            ////////        {
            ////////            // file
            ////////            //string sourcePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"LeadTools");
            ////////            //string rootFolder = Assembly.GetExecutingAssembly().Location;
            ////////            //foreach (var file in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            ////////            //{
            ////////            //    string destPath = Path.Combine(Path.GetDirectoryName(rootFolder),Path.GetFileName(file));
            ////////            //    if (!File.Exists(destPath))
            ////////            //    {
            ////////            //        File.Copy(file, destPath);
            ////////            //    }
            ////////            //}
            ////////        }
            ////////        //this.RunAsync1(ocrEngineSettings, this.CancellationTokenSource.Token).Wait();
            ////////    }
            ////////}
            ////////finally
            ////////{
            ////////    this.RunCompleteManualResetEvent.Set();
            ////////}
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            //if (roleDiagnostics == null)
            //{
            //    startDiagnostics();
            //}
            startDiagnostics();
            bool result = base.OnStart();

            //Trace.TraceInformation("TraceInfo:: HouseOfSynergy.AffinityDms.OcrWorkerRole has been started");
            //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: Diagnostics:: HouseOfSynergy.AffinityDms.OcrWorkerRole has been started");
            telemetryClient.TrackEvent("TelemetryWorker:: HouseOfSynergy.AffinityDms.OcrWorkerRole has been started");

            return (result);
        }

        public override void OnStop()
        {
            //if (roleDiagnostics == null)
            //{
            //    startDiagnostics();
            //}
            //Trace.TraceInformation("TraceInfo:: HouseOfSynergy.AffinityDms.OcrWorkerRole is stopping");
            //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: HouseOfSynergy.AffinityDms.OcrWorkerRole is stopping");
            telemetryClient.TrackEvent("TelemetryWorker:: HouseOfSynergy.AffinityDms.OcrWorkerRole is stopping");


            this.CancellationTokenSource.Cancel();
            this.RunCompleteManualResetEvent.WaitOne();

            base.OnStop();

			//Trace.TraceInformation("TraceInfo:: HouseOfSynergy.AffinityDms.OcrWorkerRole has stopped");

            //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: HouseOfSynergy.AffinityDms.OcrWorkerRole has stopped");
            telemetryClient.TrackEvent("TelemetryWorker:: HouseOfSynergy.AffinityDms.OcrWorkerRole has stopped");

        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
        }

        //private async Task RunAsync1(OcrEngineSettings ocrEngineSettings, CancellationToken cancellationToken)
        //{
        //    Exception exception = null;

        //    //Trace.TraceInformation("TraceInfo:: Working");
        //    //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: Working");
        //    telemetryClient.TrackEvent("TelemetryWorker:: Working");

        //    //Trace.TraceInformation("TraceInfo:: Sleep Mode Activate");
        //    //Thread.Sleep(40000);
        //    //Trace.TraceInformation("TraceInfo:: Sleep Mode Deactivated");

        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        do
        //        {
        //            try
        //            {
        //                Document document = null;
        //                List<Tenant> tenants = null;

        //                cancellationToken.ThrowIfCancellationRequested();

        //                //Trace.TraceInformation("TraceInfo:: Getting tenants.");
        //                //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, "Diagnostics:: Getting tenants.");
        //                telemetryClient.TrackEvent("TelemetryWorker:: Getting tenants.");

        //                if (Business.GetTenants(out tenants, out exception))
        //                {
        //                    //Trace.TraceInformation($"TraceInfo:: Found {tenants.Count:N0} tenants.");
        //                    //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Found {tenants.Count:N0} tenants.");
        //                    telemetryClient.TrackEvent($"TelemetryWorker:: Found {tenants.Count:N0} tenants.");
        //                    foreach (var tenant in tenants)
        //                    {
        //                        cancellationToken.ThrowIfCancellationRequested();

        //                        //Trace.TraceInformation($"TraceInfo:: Processing tenant: [{tenant.Id}]: [{tenant.Domain}].");
        //                        //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Processing tenant: [{tenant.Id}]: [{tenant.Domain}].");
        //                        telemetryClient.TrackEvent($"TelemetryWorker:: Processing tenant: [{tenant.Id}]: [{tenant.Domain}].");

        //                        //Trace.TraceInformation($"TraceInfo:: Getting queued document.");
        //                        //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Getting queued document.");
        //                        telemetryClient.TrackEvent($"TelemetryWorker:: Getting queued document.");

        //                        if (Business.TenantDocumentGet(tenant, out document, out exception))
        //                        {
        //                            simulateTemplateCreation(ocrEngineSettings,tenant);
        //                            if (document == null)
        //                            {
        //                                //Trace.TraceInformation($"TraceInfo:: None found.");
        //                                //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: None found.");
        //                                telemetryClient.TrackEvent("TelemetryWorker:: None found.");

        //                            }
        //                            else
        //                            {
        //                                //Trace.TraceInformation($"TraceInfo:: Found document: Id: {document.Id}, Name: {document.Name}, Folder: {document.Folder?.Name}, User: {document.User?.UserName}.");
        //                                //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Found document: Id: {document.Id}, Name: {document.Name}, Folder: {document.Folder?.Name}, User: {document.User?.UserName}.");
        //                                telemetryClient.TrackEvent($"TelemetryWorker:: Found document: Id: {document.Id}, Name: {document.Name}, Folder: {document.Folder?.Name}, User: {document.User?.UserName}.");


        //                                try
        //                                {
        //                                    // Download image from Azure Blobl Storage.
                                           

        //                                    //document.AttemptCountOcr++;
        //                                    if (!OCR.TenantDocumentPerformOcr(telemetryClient,tenant, document, ocrEngineSettings, out exception))
        //                                    {
        //                                        //Trace.TraceInformation($"TraceInfo:: Exception: {exception.Message}");
        //                                        //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Exception: {exception.Message}");
        //                                        telemetryClient.TrackEvent($"TelemetryWorker:: Exception: {exception.ToString()}");

        //                                    }
        //                                }
        //                                catch (Exception e)
        //                                {
        //                                    //Trace.TraceInformation($"TraceInfo:: Exception: {e.Message}");
        //                                    //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Exception: {e.Message}");
        //                                    telemetryClient.TrackEvent($"TelemetryWorker:: Exception: {e.ToString()}");

        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            // TODO: Requires an exit strategy.
        //                            if (exception != null)
        //                            {
        //                                //Trace.TraceInformation($"TraceInfo:: Exception: {exception.Message}");
        //                                //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Exception: {exception.Message}");
        //                                telemetryClient.TrackEvent($"TelemetryWorker:: Exception: {exception.ToString()}");
        //                            }
        //                        }

        //                        await Task.Delay(1000);
        //                    }
        //                }
        //                else
        //                {
        //                    // TODO: Requires an exit strategy.
        //                    if (exception != null)
        //                    {
        //                        //Trace.TraceInformation($"TraceInfo:: Exception: {exception.Message}");
        //                        //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral, $"Diagnostics:: Exception: {exception.Message}");
        //                        telemetryClient.TrackEvent($"TelemetryWorker:: Exception: {exception.ToString()}");
        //                    }
        //                }

        //                await Task.Delay(1000);
        //                //Trace.TraceInformation("TraceInfo:: Working");
        //                //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Information, RoleDiagnostics.TraceEventID.traceGeneral,"Working");
        //                telemetryClient.TrackEvent("TelemetryWorker:: Exception: Working");

        //            }
        //            catch (OperationCanceledException)
        //            {
        //                throw;
        //            }
        //            catch (Exception e)
        //            {
        //                // TODO: Log.
        //                // TODO: Decide whether to exit loop.
        //                //Trace.TraceError(e.ToString());
        //                //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Error, RoleDiagnostics.TraceEventID.traceGeneral, e.ToString());
        //                telemetryClient.TrackEvent("TelemetryWorker:: Exception: "+e.ToString());


        //                throw;
        //            }
        //        }
        //        while (!cancellationToken.IsCancellationRequested);
        //    }
        //    catch (OperationCanceledException e)
        //    {
        //        // Ignore.
        //        //Trace.TraceError(e.ToString());
        //        //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Error, RoleDiagnostics.TraceEventID.traceGeneral, e.ToString());
        //        telemetryClient.TrackEvent("TelemetryWorker:: Exception: " + e.ToString());
        //    }
        //    catch(Exception e)
        //    {
        //        // TODO: Log.
        //        //Trace.TraceError(e.ToString());
        //        //roleDiagnostics.WriteDiagnosticInfo(roleDiagnostics.WorkerTrace, TraceEventType.Error, RoleDiagnostics.TraceEventID.traceGeneral, e.ToString());
        //        telemetryClient.TrackEvent("TelemetryWorker:: Exception: " + e.ToString());
        //        throw;
        //    }
        //}
    }
}