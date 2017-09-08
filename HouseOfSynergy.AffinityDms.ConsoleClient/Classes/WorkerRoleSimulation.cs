using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.BusinessLayer.Ocr;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
	public static class WorkerRoleSimulation
	{
		public static void SimulateWorkerRole (OcrEngineSettings ocrEngineSettings)
		{
			try
			{
				var thread = new Thread (new ParameterizedThreadStart (WorkerRoleSimulation.SimulateWorkerRole));

				using (var cancellationTokenSource = new CancellationTokenSource ())
				{
					Console.Title = "OCR Simulation";
					Console.Write ("OCR Simulation.");
					Console.WriteLine ();
					Console.WriteLine ();
					Console.Write ("Press [Escape] at any time to quit.");
					Console.WriteLine ();
					Console.WriteLine ();
					Console.Write ("Starting OCR Thread.");

					thread.Start (new WorkerRoleParameters(ocrEngineSettings, cancellationTokenSource.Token));

					do { Thread.Sleep (TimeSpan.FromSeconds (0.1)); } while (thread.ThreadState != System.Threading.ThreadState.Running);

					do
					{
						if (Console.KeyAvailable)
						{
							var consoleKeyInfo = Console.ReadKey (true);

							if (consoleKeyInfo.Key == ConsoleKey.Escape)
							{
								Console.WriteLine ();
								Console.WriteLine ();
								Console.Write ("Requesting cancellation.");

								// Allow exceptions to accumulate into an AggregateException.
								cancellationTokenSource.Cancel (throwOnFirstException: true);

								break;
							}
						}

						Thread.Sleep (TimeSpan.FromSeconds (0.1));
					}
					while (true);

					thread.Join ();

					Console.WriteLine ();
					Console.WriteLine ();
					Console.Write ("Thread stopped.");
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine ();
				Console.WriteLine ();
				Console.Write (exception.ToString ());

				Debugger.Break ();
			}

			Console.WriteLine ();
			Console.WriteLine ();
			Console.WriteLine ("Press any key to exit: ");
			Console.ReadKey (true);
		}

		private static void SimulateWorkerRole (object workerRoleParameters)
		{
			if (workerRoleParameters == null) { throw (new ArgumentNullException ("workerRoleParameters")); }

			if (workerRoleParameters is WorkerRoleParameters)
			{
				WorkerRoleSimulation.SimulateWorkerRole ((WorkerRoleParameters) workerRoleParameters);
			}
			else
			{
				throw (new ArgumentException ("The argument [workerRoleParameters] must be of type [WorkerRoleParameters].", "workerRoleParameters"));
			}
		}

		private static void SimulateWorkerRole (WorkerRoleParameters workerRoleParameters)
		{
			Exception exception = null;

			Console.WriteLine ();
			Console.WriteLine ();
			Console.Write ("OCR Thread Started.");

			try
			{
				workerRoleParameters.CancellationToken.ThrowIfCancellationRequested ();

				do
				{
					try
					{
						Document document = null;
						List<Tenant> tenants = null;

						workerRoleParameters.CancellationToken.ThrowIfCancellationRequested ();

						Console.WriteLine ();
						Console.WriteLine ();
						Console.Write ("Getting tenants.");

						if (Business.GetTenants (out tenants, out exception))
						{
							Console.WriteLine ();
							Console.Write ($"Found {tenants.Count:N0} tenants.");

							foreach (var tenant in tenants)
							{
								workerRoleParameters.CancellationToken.ThrowIfCancellationRequested ();

								Console.WriteLine ();
								Console.Write ($"Processing tenant: [{tenant.Id}]: [{tenant.Domain}].");

								Console.WriteLine ();
								Console.Write ($"Getting queued document.");

								if (Business.TenantDocumentGet (tenant, out document, out exception))
								{
									if (document == null)
									{
										Console.WriteLine ();
										Console.Write ($"None found.");
									}
									else
									{
										Console.WriteLine ();
										Console.Write ($"Found document: Id: {document.Id}, Name: {document.Name}, Folder: {document.Folder?.Name}, User: {document.User.UserName}.");

										try
										{
											workerRoleParameters.CancellationToken.ThrowIfCancellationRequested ();

											if (!WorkerRoleSimulation.TenantDocumentPerformOcr (tenant, document, workerRoleParameters, out exception))
											{
												Console.WriteLine ();
												Console.Write ($"Exception: {exception.Message}");
											}
										}
										catch (OperationCanceledException)
										{
											// Ignore.
										}
										catch (Exception e)
										{
											Console.WriteLine ();
											Console.Write ($"Exception: {e.Message}");
										}
									}
								}
								else
								{
									// TODO: Requires an exit strategy.
									Console.WriteLine ();
									Console.Write ($"Exception: {exception.Message}");
								}

								Thread.Sleep (1000);
							}
						}
						else
						{
							// TODO: Requires an exit strategy.
							Console.WriteLine ();
							Console.Write ($"Exception: {exception.Message}");
						}

						Thread.Sleep (1000);
						Console.WriteLine ();
						Console.Write ("Working");
					}
					catch (OperationCanceledException)
					{
						throw;
					}
					catch (Exception e)
					{
						// TODO: Log.
						// TODO: Decide whether to exit loop.
						Trace.TraceError (e.ToString ());

						throw;
					}
				}
				while (!workerRoleParameters.CancellationToken.IsCancellationRequested);
			}
			catch (OperationCanceledException)
			{
				// Ignore.
			}
			catch
			{
				// TODO: Log.
				throw;
			}
		}

		public static bool TenantDocumentPerformOcr (Tenant tenant, Document document, WorkerRoleParameters workerRoleParameters, out Exception exception)
		{
			var result = false;
			Document ocrDocument = null;
			List<DocumentTemplate> ocrDocumentTemplate = null;
			List<DocumentFragment> ocrDocumentFragment = null;

			exception = null;

			try
			{
				List<Template> allTemplates = null;
				List<TemplateElement> allElements = null;

				using (var context = new ContextTenant (tenant.DatabaseConnectionString))
				{
					allTemplates = context
						.Templates
						.AsNoTracking ()
						.Include (t => t.Elements)
						.Include (t => t.Elements.Select (x => x.ElementDetails))
						.Where (t => (t.IsActive == true) && (t.IsFinalized == true))
						.ToList ();

					if (allTemplates.Count <= 0) { throw (new Exception ("No templates found.")); }
				}

				// workerRoleParameters???

				// Download file from azure.
				Stream stream = null;
				List<OcrResultInfo> ocrresultinfos = new List<OcrResultInfo> ();

				if (WorkerRoleSimulation.GetTenantDocumentFileStream (tenant, document, out stream, out exception))
				{
					Image documentImage = null;

					using (var image = Image.FromStream (stream))
					{
						documentImage = (Image) image.Clone ();
						// LeadTools code.
						// Stream and image available.
					}

					// TODO: Replace with Server.MapPath.
					//LeadToolsOCR leadtoolsocr = new LeadToolsOCR (workerRoleParameters.OcrEngineSettings.GetPathData().FullName, tenant.Id.ToString (), string.Empty, out exception);
					//if (exception != null) { throw exception; }

					List<LeadtoolsBarcodeData> barcodedata = null;
					bool foundbarcode = false; // leadtoolsocr.ReadBarcode (documentImage, out barcodedata, out exception);
					//if (exception != null) { throw exception; }

					if (foundbarcode)
					{
						//What to do if multiple barcodes are found.
						int templateid = 0;
						bool foundId = int.TryParse (barcodedata.First ().Value, out templateid);
						if (!(foundId) || templateid <= 0)
						{ }//What to do if Form Id is Not Found by the barcode
						Template template = allTemplates.Where (t => t.Id == ((long) templateid)).ToArray ().FirstOrDefault ();
						if (template.TemplateType == TemplateType.Form)
						{
							double _computeddifference = 0.0;
							if ((documentImage.Width <= documentImage.Height) && (!(documentImage.Height <= 0)))
							{
								_computeddifference = ((Convert.ToDouble (documentImage.Width)) / (Convert.ToDouble (documentImage.Height)));
							}
							else
							{
								if (!(documentImage.Width <= 0))
								{
									_computeddifference = documentImage.Height / documentImage.Width;
								}
							}
							var elements = allElements.Where (e => e.TemplateId == (long) templateid).ToList ();
							List<ComputeCoordinates> cordlistlist = new List<ComputeCoordinates> ();
							//cordlistlist = leadtoolsocr.GetAllZoneData (elements, _computeddifference, documentImage);

							//what to do with form matched results


							//foreach (ComputeCoordinates cordata in cordlistlist)
							//{
							//    returnedresult += cordata.Text;
							//}
						}
					}
					else
					{
						result = AutoOCR (workerRoleParameters.OcrEngineSettings, tenant, documentImage, document, allTemplates, out ocrDocument, out ocrDocumentFragment, out ocrDocumentTemplate, out exception);
						if (exception != null)
						{
							throw exception;
						}

					}

					// Perform OCR and time-consuming tasks.
				}
				else
				{
					throw (exception);
				}

				using (var context = new ContextTenant (tenant.DatabaseConnectionString))
				{
					using (var transaction = context.Database.BeginTransaction ())
					{
						try
						{
							if (ocrDocument != null)
							{
								context.Documents.Attach (ocrDocument);
								context.Entry (ocrDocument).State = EntityState.Modified;
								context.SaveChanges ();
							}
							if (ocrDocumentFragment != null)
							{
								foreach (var documentfragment in ocrDocumentFragment)
								{
									context.DocumentFragments.Add (documentfragment);
									context.SaveChanges ();
								}
							}
							if (ocrDocumentTemplate != null)
							{
								foreach (var documenttemplate in ocrDocumentTemplate)
								{
									context.DocumentTemplate.Add (documenttemplate);
									context.SaveChanges ();
								}
							}
							//context.Templates.Attach(templates[0]);
							context.SaveChanges ();

							// Do not call this line from anywhere else.
							transaction.Commit ();

						}
						catch (Exception e)
						{
							transaction.Rollback ();
						}
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

		public static bool AutoOCR (OcrEngineSettings ocrEngineSettings, Tenant tenant, Image image, Document document, List<Template> alltemplates, out Document documentObj, out List<DocumentFragment> documentfragments, out List<DocumentTemplate> documentTemplate, out Exception exception)
		{
			bool result = false;
			exception = null;
			documentObj = null;
			documentfragments = null;
			documentTemplate = null;

			try
			{
				OcrClassification ocrclassification = new OcrClassification ();
                
				//result = ocrclassification.BeginOcrClassification (ocrEngineSettings, tenant, image, document, alltemplates, out documentObj, out documentfragments, out documentTemplate, out exception);
				if (exception != null)
				{
					throw exception;
				}
			}
			catch (Exception ex)
			{
				exception = ex;

			}
			return result;
		}

		private static bool GetTenantDocumentFileStream (Tenant tenant, Document document, out Stream stream, out Exception exception)
		{
			var result = false;

			stream = null;
			exception = null;

			try
			{
				var connectionString = tenant.StorageConnectionStringPrimary;
				var account = CloudStorageAccount.Parse (connectionString);
				var client = account.CreateCloudBlobClient ();
				var container = client.GetContainerReference (tenant.UrlResourceGroup);

				container.CreateIfNotExists ();
				container.SetPermissions (new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off, });

				var blob = container.GetBlockBlobReference (document.FileNameServer);

				stream = new MemoryStream ();
				blob.DownloadToStream (stream);

				result = true;
			}
			catch (Exception e)
			{
				if (stream != null)
				{
					try { stream.Dispose (); }
					finally { stream = null; }
				}

				exception = e;
			}

			return (result);
		}
	}
}
