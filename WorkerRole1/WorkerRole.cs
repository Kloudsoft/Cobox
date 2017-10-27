using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.MvcLT;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Leadtools;
using Leadtools.Pdf;
using Leadtools.Codecs;
using System.IO;
using System.IO.Compression;

using System.Data.Entity;
using System.Text;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using System.Text.RegularExpressions;



using Microsoft.WindowsAzure.Storage.RetryPolicies;


namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private CloudQueue fileQueue;
        private CloudBlobContainer inputBlobContainer;
        private CloudBlobContainer outputBlobContainer;

        private string outputContainer = "outputcobox";
        private string inputContainer = "inputcobox";

        public class LstIndexes
        {
            public long NewindexId { get; set; }
            public string Newindexname { get; set; }
            public string Newindexvalue { get; set; }
            public string Newindexdisplayname { get; set; }
            public int NewInfexLeft { get; set; }
            public int NewInfexTop { get; set; }
            public int NewInfexWidth { get; set; }
            public int NewInfexHeight { get; set; }


        }

        public class Bloblist
        {
            public string batchno { get; set; }
            public int nooffiles { get; set; }
            public int status { get; set; }
            public string inputblob { get; set; }
            public string outputblob { get; set; }
            public string filetype { get; set; }

        }

        public class LstAzureSearch
        {
            public string fieldname { get; set; }
            public string fieldvalue { get; set; }

        }


        public override void Run()
        {
            Trace.TraceInformation("CanonWorker is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections.
            ServicePointManager.DefaultConnectionLimit = 12;
            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

            // Get context object for working with blobs, and 
            // set a default retry policy appropriate for a web user interface.
            var blobClient = storageAccount.CreateCloudBlobClient();

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            // queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            //INPUT
            inputBlobContainer = blobClient.GetContainerReference(inputContainer);
            if (inputBlobContainer.CreateIfNotExists())
            {
                // Enable public access on the newly created "inputContainer" container.
                inputBlobContainer.SetPermissions(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });
            }

            ////OUPUT
            //outputBlobContainer = blobClient.GetContainerReference(outputContainer);
            //if (outputBlobContainer.CreateIfNotExists())
            //{
            //    // Enable public access on the newly created "inputContainer" container.
            //    outputBlobContainer.SetPermissions(
            //        new BlobContainerPermissions
            //        {
            //            PublicAccess = BlobContainerPublicAccessType.Blob
            //        });
            //}

            // blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            // Get a reference to the blob container.
            outputBlobContainer = blobClient.GetContainerReference(outputContainer);
            outputBlobContainer.CreateIfNotExists();
            

            // Get a reference to the queue.
            fileQueue = queueClient.GetQueueReference(inputContainer);
            fileQueue.CreateIfNotExists();

            // fileQueue = TenantTemplateIndexListLTController.fileQueue;
            //  TenantTemplateIndexListLTController.InitializeStorage(inputContainer);
            // TenantTemplateIndexListLTController.InitializeStorage(outputContainer);
            // fileQueue = TenantTemplateIndexListLTController.fileQueue;

            return base.OnStart();
        }


      

        public override void OnStop()
        {
            Trace.TraceInformation("CanonWorker is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("CanonWorker has stopped");
        }
        private void ProcessQueueMessage(CloudQueueMessage msg)
            {
            try
            {
                CloudBlockBlob outputBlob;
                Trace.TraceInformation("Processing queue message {0}", msg);

                TenantTemplateIndexListLTController Obj = new TenantTemplateIndexListLTController();

                //http://127.0.0.1:10000/devstoreaccount1/inputcobox/6c4fa6f9-5d74-4abb-a790-a5caa64511ec.pdf

                //Obj.ProcessOcr(msg.AsString);

                // string test = ConfigurationSettings.AppSettings.GetValues("LICPATH").ToString();   

                //msg = this.fileQueue.GetMessage();
                if (msg != null)
                {
                    Obj.IndexV2(msg.AsString, fileQueue, msg, outputBlobContainer);

                    fileQueue.DeleteMessage(msg);
                }
                



            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        //private async void SendEmail(string fileName)
        //{
        //    SendemailHelper sendmail = new SendemailHelper();
        //    string mess = await sendmail.SendEmail();
        //    //Remove OCR'ed PDF files from SFTP
        //    ShellScriptService.RemoveOCRedFiles(fileName);
        //}
               
        

        private async Task RunAsync()
        {
            Trace.TraceInformation("Working");
            CloudQueueMessage msg = null;
            

                try
                {
                    // Retrieve a new message from the queue.
                    // A production app could be more efficient and scalable and conserve
                    // on transaction costs by using the GetMessages method to get
                    // multiple queue messages at a time. See:
                    // http://azure.microsoft.com/en-us/documentation/articles/cloud-services-dotnet-multi-tier-app-storage-5-worker-role-b/#addcode
                    msg = this.fileQueue.GetMessage();
                    if (msg != null)
                    {
                        ProcessQueueMessage(msg);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(300000);
                    }
                }
                catch (StorageException e)
                {
                 
                }
                await Task.Delay(300000);
            
        }
        private async Task RunAsync(CancellationToken cancellationToken)
        {
            Trace.TraceInformation("Working");
            CloudQueueMessage msg = null;
            while (!cancellationToken.IsCancellationRequested)
            {

                try
                {
                    // Retrieve a new message from the queue.
                    // A production app could be more efficient and scalable and conserve
                    // on transaction costs by using the GetMessages method to get
                    // multiple queue messages at a time. See:
                    // http://azure.microsoft.com/en-us/documentation/articles/cloud-services-dotnet-multi-tier-app-storage-5-worker-role-b/#addcode
                    msg = this.fileQueue.GetMessage();
                    if (msg != null)
                    {
                        ProcessQueueMessage(msg);
                    }
                    else
                    {

                        System.Threading.Thread.Sleep(120000);
                    }
                }
                catch (StorageException e)
                {
                    if (msg != null && msg.DequeueCount > 5)
                    {
                        this.fileQueue.DeleteMessage(msg);
                        Trace.TraceError("Deleting poison queue item: '{0}'", msg.AsString);
                    }
                    Trace.TraceError("Exception in CanonWorker: '{0}'", e.Message);
                    System.Threading.Thread.Sleep(300000);
                }
                await Task.Delay(120000);
            }
        }
    }
}
