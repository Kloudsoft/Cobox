using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
    public class AzureService
    {
        public static CloudBlobContainer fileBlobContainer;
        public static string inputContainer = "input";
        public static string outputContainer = "output";
        public static CloudQueue fileQueue { get; set; }
        public static void InitializeStorage(string containerName)
        {
            // Open storage account using credentials from .cscfg file.
            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

            // Get context object for working with blobs, and 
            // set a default retry policy appropriate for a web user interface.
            var blobClient = storageAccount.CreateCloudBlobClient();

            fileBlobContainer = blobClient.GetContainerReference(containerName);
            if (fileBlobContainer.CreateIfNotExists())
            {
                // Enable public access on the newly created "inputContainer" container.
                fileBlobContainer.SetPermissions(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });
            }

            blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            // Get a reference to the blob container.
            fileBlobContainer = blobClient.GetContainerReference(containerName);

            // Get context object for working with queues, and 
            // set a default retry policy appropriate for a web user interface.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            fileQueue = queueClient.GetQueueReference("output");
            fileQueue.CreateIfNotExists();
        }

        public static CloudBlockBlob MoveTOutputCotainer(string fileName)
        {
            string blobName = fileName;
            //blobName = blobName.Replace(".tiff.pdf","_ocr.pdf");
            CloudBlockBlob fileBlob = fileBlobContainer.GetBlockBlobReference(blobName);
            try
            {

                //using (SftpClient client = new SftpClient("52.163.94.105", 22, "kloudsoft-admin", "KLS@dm1nKLS@dm1n"))
                //{
                //    client.Connect();
                //    client.ChangeDirectory("/home/kloudsoft-admin/PDFOCr/");

                //    if ((!fileName.StartsWith(".")))

                //        using (Stream fileStream = new MemoryStream())
                //        {
                //            client.DownloadFile(fileName, fileStream);
                //            if (fileStream != null)
                //            {
                //                if (!fileStream.CanSeek)
                //                {
                //                    throw new Exception("Won't work :(");
                //                }

                //                fileStream.Seek(0, SeekOrigin.Begin);


                //                // Create the blob by uploading a local file.
                //                fileBlob.UploadFromStream(fileStream);

                //            }
                //        }
                //    client.Disconnect();
                //}
                return fileBlob;
            }
            catch (Exception ex)
            {
                return fileBlob;

            }
        }

    }
}
