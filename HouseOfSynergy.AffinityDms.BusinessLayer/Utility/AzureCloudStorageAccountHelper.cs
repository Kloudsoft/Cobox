using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Utility;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Desktop;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.BusinessLayer
{
    public sealed class AzureCloudStorageAccountHelper :
        Disposable
    {
        private bool Disposed = false;

        public Tenant Tenant { get; private set; }
        public CloudStorageAccount CloudStorageAccount { get; private set; }

        public AzureCloudStorageAccountHelper(Tenant tenant)
        {
            this.Tenant = tenant;
            this.CloudStorageAccount = CloudStorageAccount.Parse(tenant.StorageConnectionStringPrimary);
        }

        public void Download(Template template, FileInfo fileBin, FileInfo fileXml, FileInfo fileTiff)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {

                try
                {


                    try
                    {
                        if (File.Exists(fileBin.FullName))
                        {
                            File.Delete(fileBin.FullName);
                        }
                        if (File.Exists(fileXml.FullName))
                        {
                            File.Delete(fileXml.FullName);
                        }
                        if (File.Exists(fileTiff.FullName))
                        {
                            File.Delete(fileTiff.FullName);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex = new Exception("Unable to update existing templates");
                    }
                    using (var streamBin = File.Open(fileBin.FullName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
                    using (var streamXml = File.Open(fileXml.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                    using (var streamTiff = File.Open(fileTiff.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                    {
                        //streamBin.Seek(0, SeekOrigin.Begin);
                        //streamXml.Seek(0, SeekOrigin.Begin);
                        //streamTiff.Seek(0, SeekOrigin.Begin);
                        this.GetTenantTemplateFileStream(template, streamBin, streamXml, streamTiff);
                        streamBin.Seek(0, SeekOrigin.Begin);
                        streamXml.Seek(0, SeekOrigin.Begin);
                        streamTiff.Seek(0, SeekOrigin.Begin);
                    }
                }
                finally
                {

                }
            }
        }

        public void GetTenantTemplateFileStream(Template template, Stream streamBin, Stream streamXml, Stream streamTiff)
        {
            CloudBlockBlob blob = null;

            var client = this.CloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference("templates");

            container.CreateIfNotExists();
            //await container.CreateIfNotExistsAsync(cancellationToken);
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

            var name = template.Id.ToString().PadLeft(long.MaxValue.ToString().Length, '0');
            blob = container.GetBlockBlobReference(name + ".bin");
            //await blob.DownloadToStreamAsync(streamBin, cancellationToken);
            blob.DownloadToStream(streamBin);

            blob = container.GetBlockBlobReference(name + ".xml");
            blob.DownloadToStream(streamXml);

            blob = container.GetBlockBlobReference(name + ".tif");
            blob.DownloadToStream(streamTiff);

        }
        public bool GetTenantDiscourseFileUrl(Tenant tenant, DiscoursePostVersionAttachment discoursePostVersionAttachment, out string url, out Exception exception)
        {
            var result = false;
            url = string.Empty;
            exception = null;

            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("discourseexternaldocuments");// tenant.UrlResourceGroup);

                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container, });

                var blob = container.GetBlockBlobReference(discoursePostVersionAttachment.FileNameServer);

                url = blob.Uri.AbsoluteUri;
                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public bool GetTenantDocumentFileUrl(Tenant tenant, Document document, out string url, out Exception exception)
        {
            var result = false;
            url = string.Empty;
            exception = null;

            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("documents");// tenant.UrlResourceGroup);

                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container, });

                var blob = container.GetBlockBlobReference(document.FileNameServer);

                url = blob.Uri.AbsoluteUri;
                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        public bool GetTenantDocumentFileStream(Tenant tenant, Document document, out Stream stream, out Exception exception)
        {
            var result = false;

            stream = null;
            exception = null;

            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("documents");// tenant.UrlResourceGroup);

                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

                var blob = container.GetBlockBlobReference(document.FileNameServer);

                stream = new MemoryStream();
                blob.DownloadToStream(stream);
                result = true;
            }
            catch (Exception e)
            {

                if (stream != null)
                {
                    try { stream.Dispose(); }
                    finally { stream = null; }
                }

                exception = e;
            }

            return (result);
        }
        public bool GetDiscourseDocumentFileStream(Tenant tenant, DiscoursePostVersionAttachment discoursePostVersionAttachment, out Stream stream, out Exception exception)
        {
            var result = false;

            stream = null;
            exception = null;

            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("discourseexternaldocuments");// tenant.UrlResourceGroup);

                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

                var blob = container.GetBlockBlobReference(discoursePostVersionAttachment.FileNameServer);

                stream = new MemoryStream();
                blob.DownloadToStream(stream);
                result = true;
            }
            catch (Exception e)
            {

                if (stream != null)
                {
                    try { stream.Dispose(); }
                    finally { stream = null; }
                }

                exception = e;
            }

            return (result);
        }

        //public bool DocumentUpload(TenantUserSession tenantUserSession, Document document, Stream stream, CancellationToken cancellationToken, out Exception exception)
        //{
        //    var result = false;

        //    exception = null;

        //    try
        //    {
        //        var connectionString = tenantUserSession.Tenant.StorageConnectionStringPrimary;
        //        var account = CloudStorageAccount.Parse(connectionString);
        //        var client = account.CreateCloudBlobClient();
        //        var container = client.GetContainerReference("documents");
        //        container.CreateIfNotExists();
        //        container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });
        //        var blob = container.GetBlockBlobReference(document.FileNameServer);
        //        var running = true;
        //        var watch = System.Diagnostics.Stopwatch.StartNew();

        //        blob.DeleteIfExists(DeleteSnapshotsOption.IncludeSnapshots);
        //        blob.UploadFromStream(stream);

        //        result = true;
        //    }
        //    catch (Exception e)
        //    {
        //        exception = e;
        //    }

        //    return (result);
        //}

        public bool DocumentUpload(TenantUserSession tenantUserSession, Document document, Stream stream, CancellationToken cancellationToken, out Exception exception)
        {
            var result = false;

            exception = null;

            try
            {
                var connectionString = tenantUserSession.Tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("documents");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });
                var blob = container.GetBlockBlobReference(document.FileNameServer);
                var running = true;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                stream.Seek(0, SeekOrigin.Begin);
                blob.DeleteIfExists(DeleteSnapshotsOption.IncludeSnapshots);
                var task = blob.UploadFromStreamAsync(stream, cancellationToken);

                do
                {
                    switch (task.Status)
                    {
                        case TaskStatus.Canceled:
                        case TaskStatus.Faulted:
                        case TaskStatus.RanToCompletion:
                            { running = false; break; }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    cancellationToken.ThrowIfCancellationRequested();
                }
                while (running);

                switch (task.Status)
                {
                    case TaskStatus.Faulted:
                    case TaskStatus.Canceled:
                        {
                            running = false;
                            throw (task.Exception ?? new Exception("The upload task encountered an unknown exception."));
                        }
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }


        public bool DiscourseDocumentUpload(TenantUserSession tenantUserSession, DiscoursePostVersionAttachment discourseDocument, Stream stream, CancellationToken cancellationToken, out Exception exception)
        {
            var result = false;

            exception = null;

            try
            {
                var connectionString = tenantUserSession.Tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("discourseexternaldocuments");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });
                var blob = container.GetBlockBlobReference(discourseDocument.Id.ToString() + "." + GlobalConstants.FileExtensionCloud);
                var running = true;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                stream.Seek(0, SeekOrigin.Begin);
                blob.DeleteIfExists(DeleteSnapshotsOption.IncludeSnapshots);
                var task = blob.UploadFromStreamAsync(stream, cancellationToken);

                do
                {
                    switch (task.Status)
                    {
                        case TaskStatus.Canceled:
                        case TaskStatus.Faulted:
                        case TaskStatus.RanToCompletion:
                            { running = false; break; }
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    cancellationToken.ThrowIfCancellationRequested();
                }
                while (running);

                switch (task.Status)
                {
                    case TaskStatus.Faulted:
                    case TaskStatus.Canceled:
                        {
                            running = false;
                            throw (task.Exception ?? new Exception("The upload task encountered an unknown exception."));
                        }
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }



        public bool DiscourseDocumentCopy(TenantUserSession tenantUserSession, long newDiscoursePostVersionAttachmentId, long oldDiscoursePostVersionAttachmentId, CancellationToken cancellationToken, out Exception exception)
        {
            var result = false;
            exception = null;
            try
            {
                var connectionString = tenantUserSession.Tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("discourseexternaldocuments");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });
                var oldBlob = container.GetBlockBlobReference(oldDiscoursePostVersionAttachmentId.ToString() + "." + GlobalConstants.FileExtensionCloud);
                var newBlob = container.GetBlockBlobReference(newDiscoursePostVersionAttachmentId.ToString() + "." + GlobalConstants.FileExtensionCloud);
                var running = true;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var task = newBlob.StartCopyAsync(oldBlob, cancellationToken);

                //stream.Seek(0, SeekOrigin.Begin);
                //blob.DeleteIfExists(DeleteSnapshotsOption.IncludeSnapshots);
                //var task = blob.UploadFromStreamAsync(stream, cancellationToken);

                do
                {
                    switch (task.Status)
                    {
                        case TaskStatus.Canceled:
                        case TaskStatus.Faulted:
                        case TaskStatus.RanToCompletion:
                            { running = false; break; }
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    cancellationToken.ThrowIfCancellationRequested();
                }
                while (running);

                switch (task.Status)
                {
                    case TaskStatus.Faulted:
                    case TaskStatus.Canceled:
                        {
                            running = false;
                            throw (task.Exception ?? new Exception("The copy task encountered an unknown exception."));
                        }
                }

                result = true;
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }

        private string GetContainerNameByUploadType(DiscussionPostAttachmentType DocumentTypeToRemove)
        {
            string result = string.Empty;
            switch (DocumentTypeToRemove)
            {
                case DiscussionPostAttachmentType.Template:
                    result = "templates";
                    break;
                case DiscussionPostAttachmentType.Document:
                    result = "documents";
                    break;
                case DiscussionPostAttachmentType.External:
                    result = "discourseexternaldocuments";
                    break;
                case DiscussionPostAttachmentType.Form:
                    result = "templates";
                    break;
                default:
                    break;
            }
            return result;
        }
        public bool RemoveFile(TenantUserSession tenantUserSession, long id, DiscussionPostAttachmentType DocumentTypeToRemove, CancellationToken cancellationToken, out Exception exception)
        {
            var result = false;
            exception = null;
            try
            {
                if (id <= 0) { throw (new Exception("Unable to find the following request")); }
                string containerName = GetContainerNameByUploadType(DocumentTypeToRemove);
                if (!string.IsNullOrEmpty(containerName))
                {
                    var connectionString = tenantUserSession.Tenant.StorageConnectionStringPrimary;
                    var account = CloudStorageAccount.Parse(connectionString);
                    var client = account.CreateCloudBlobClient();
                    var container = client.GetContainerReference(containerName);
                    container.CreateIfNotExists();
                    container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });
                    var Blob = container.GetBlockBlobReference(id.ToString() + "." + GlobalConstants.FileExtensionCloud);
                    var running = true;
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    var task = Blob.DeleteIfExistsAsync(cancellationToken);
                    do
                    {
                        switch (task.Status)
                        {
                            case TaskStatus.Canceled:
                            case TaskStatus.Faulted:
                            case TaskStatus.RanToCompletion:
                                { running = false; break; }
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    while (running);

                    switch (task.Status)
                    {
                        case TaskStatus.Faulted:
                        case TaskStatus.Canceled:
                            {
                                running = false;
                                throw (task.Exception ?? new Exception("The delete task encountered an unknown exception."));
                            }
                    }

                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
            return (result);
        }




        public bool UploadFilesToBlob(Tenant tenant, FileInfo filename, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(tenant.UrlResourceGroup);
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });
                try
                {
                    var blob = container.GetBlockBlobReference((Path.GetFileNameWithoutExtension(filename.FullName).PadLeft(long.MaxValue.ToString().Length, '0')) + Path.GetExtension(filename.FullName));
                    blob.UploadFromFile(filename.FullName);
                    result = true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }

        public bool UploadTemplateFilesToBlob(Tenant tenant, FileInfo filename, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                var connectionString = tenant.StorageConnectionStringPrimary;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("templates");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });
                try
                {
                    var blob = container.GetBlockBlobReference((Path.GetFileNameWithoutExtension(filename.FullName).PadLeft(long.MaxValue.ToString().Length, '0')) + Path.GetExtension(filename.FullName));
                    blob.UploadFromFile(filename.FullName);
                    result = true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                this.Disposed = true;

                if (disposing)
                {
                    // Managed.
                }

                // Unmanaged.
            }

            base.Dispose(disposing);
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using HouseOfSynergy.AffinityDms.Entities.Common;
//using HouseOfSynergy.AffinityDms.Entities.Tenants;
//using HouseOfSynergy.AffinityDms.Library;
//using HouseOfSynergy.PowerTools.Library.Utility;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;

//namespace HouseOfSynergy.AffinityDms.BusinessLayer
//{
//	public sealed class AzureCloudStorageAccountHelper:
//		Disposable
//	{
//		private bool Disposed = false;

//		public Tenant Tenant { get; private set; }
//		public CloudStorageAccount CloudStorageAccount { get; private set; }

//		public AzureCloudStorageAccountHelper (Tenant tenant)
//		{
//			this.CloudStorageAccount = CloudStorageAccount.Parse (tenant.StorageConnectionStringPrimary);
//		}

//		public void Download (Template template, FileInfo fileBin, FileInfo fileXml, FileInfo fileTiff)
//		{
//			using (var cancellationTokenSource = new CancellationTokenSource ())
//			{
//				this.DownloadAsync (template, fileBin, fileXml, fileTiff, cancellationTokenSource.Token).Wait (cancellationTokenSource.Token);
//			}
//		}

//		public void Download (Template template, Stream streamBin, Stream streamXml, Stream streamTiff)
//		{
//			using (var cancellationTokenSource = new CancellationTokenSource ())
//			{
//				this.DownloadAsync (template, streamBin, streamXml, streamTiff, cancellationTokenSource.Token).Wait (cancellationTokenSource.Token);
//			}
//		}

//		public async Task DownloadAsync (Template template, FileInfo fileBin, FileInfo fileXml, FileInfo fileTiff, CancellationToken cancellationToken)
//		{
//			using (var streamBin = File.Open (fileBin.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
//			using (var streamXml = File.Open (fileXml.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
//            using (var streamTiff = File.Open (fileTiff.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
//			{
//				await this.DownloadAsync(template, streamBin, streamXml, streamTiff, cancellationToken);
//			}
//		}

//		public async Task DownloadAsync (Template template, Stream streamBin, Stream streamXml, Stream streamTiff, CancellationToken cancellationToken)
//		{
//			CloudBlockBlob blob = null;

//			var client = this.CloudStorageAccount.CreateCloudBlobClient ();
//			var container = client.GetContainerReference ("Templates");

//			//container.CreateIfNotExists ();
//            await container.CreateIfNotExistsAsync(cancellationToken);
//			container.SetPermissions (new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

//			var name = template.Id.ToString ().PadLeft (long.MaxValue.ToString ().Length, '0');

//			blob = container.GetBlockBlobReference (name + ".bin");
//			await blob.DownloadToStreamAsync (streamBin, cancellationToken);

//			blob = container.GetBlockBlobReference (name + ".xml");
//			await blob.DownloadToStreamAsync (streamXml, cancellationToken);

//			blob = container.GetBlockBlobReference (name + ".tiff");
//			await blob.DownloadToStreamAsync (streamTiff, cancellationToken);
//		}

//        protected override void Dispose (bool disposing)
//		{
//			if (!this.Disposed)
//			{
//				this.Disposed = true;

//				if (disposing)
//				{
//					// Managed.
//				}

//				// Unmanaged.
//			}

//			base.Dispose (disposing);
//		}
//	}
//}
