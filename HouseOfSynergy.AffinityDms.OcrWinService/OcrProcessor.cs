using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.BusinessLayer.Ocr;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.PowerTools.Library.Threading;

namespace HouseOfSynergy.AffinityDms.OcrWinService
{
	public sealed class OcrProcessor:
		ThreadBase
	{
		private Global Global = null;
		private bool Disposed = false;
		private ServiceBase ServiceBase = null;
		private OcrEngineSettings OcrEngineSettings = null;

		public OcrProcessor (ServiceBase serviceBase, Global global, OcrEngineSettings ocrEngineSettings)
			: base(isBackground: false, apartmentState: ApartmentState.MTA, threadPriority: ThreadPriority.Normal)
		{
			if (global == null) { throw (new ArgumentNullException(nameof(global))); }
			if (serviceBase == null) { throw (new ArgumentNullException(nameof(serviceBase))); }
			if (ocrEngineSettings == null) { throw (new ArgumentNullException(nameof(ocrEngineSettings))); }

			this.Global = global;
			this.ServiceBase = serviceBase;
			this.OcrEngineSettings = ocrEngineSettings;
		}

		protected override void OnStarted () { }
		protected override void OnStopped () { }
		protected override void OnStarting (CancelEventArgs e) { }
		protected override void OnStopping (CancelEventArgs e) { }

		protected override void OnProcess (CancellationToken cancellationToken)
		{
            var stopService = true;
			Exception exception = null;

			try
			{
				do
				{
                    this.WaitWhilePausedAndThrowOnCancellation(cancellationToken);

					try
					{
						Document document = null;
						List<Tenant> tenants = null;

						cancellationToken.ThrowIfCancellationRequested();

						this.Global.Logger.Write("Getting Tenants");
						if (Business.GetTenants(out tenants, out exception))
						{
							cancellationToken.ThrowIfCancellationRequested();

							this.Global.Logger.Write($"Found {tenants.Count:N0} tenants.");
							foreach (var tenant in tenants)
							{
								cancellationToken.ThrowIfCancellationRequested();
                                this.WaitWhilePausedAndThrowOnCancellation(cancellationToken);
                                this.Global.Logger.Write($"Processing tenant: [{tenant.Id}]: [{tenant.Domain}].");
								this.Global.Logger.Write($"Getting queued document.");

								if (Business.TenantDocumentGet(tenant, out document, out exception))
								{
									cancellationToken.ThrowIfCancellationRequested();
                                    var currentDocument = document;
									if (document == null)
									{
										this.Global.Logger.Write("None found.");
									}
									else
									{
										this.Global.Logger.Write($"Found document: Id: {document.Id}, Name: {document.Name}, Folder: {document.Folder?.Name}, User: {document.User?.UserName}.");
                                        var documentState = document.State;
										try
										{
                                            document.State = Entities.Lookup.DocumentState.Processing;
                                            Business.TenantDocumentUpdate(tenant, document, out exception);
                                            if (exception != null) { throw exception; }
                                            this.Global.Logger.Write($"Processing OCR on the Document");
                                            // Download image from Azure Blobl Storage.
                                            //document.AttemptCountOcr++;
                                            if (!OCR.TenantDocumentPerformOcr(this.Global, tenant, document, this.OcrEngineSettings, out exception))
											{
												this.Global.Logger.Write($"Exception: {exception.ToString()}");
											}
										}
										catch (Exception e)
										{
                                            currentDocument.State = documentState;
                                            Business.TenantDocumentUpdate(tenant, currentDocument, out exception);
                                            this.Global.Logger.Write($"Exception: {e.ToString()}");
                                            
                                        }
									}
								}
								else
								{
									// TODO: Requires an exit strategy.
									if (exception != null)
									{
										this.Global.Logger.Write($"Exception: {exception.ToString()}");
									}
								}

								cancellationToken.ThrowIfCancellationRequested();

								Thread.Sleep(TimeSpan.FromSeconds(1));
							}
						}
						else
						{
							// TODO: Requires an exit strategy.
							if (exception != null)
							{
								this.Global.Logger.Write($"Exception: {exception.ToString()}");
							}
						}

						cancellationToken.ThrowIfCancellationRequested();
                        this.WaitWhilePausedAndThrowOnCancellation(cancellationToken);

                        Thread.Sleep(TimeSpan.FromSeconds(1));
					}
					catch (OperationCanceledException)
					{
						throw;
					}
					catch (Exception e)
					{
						// TODO: Log.
						// TODO: Decide whether to exit loop.
						this.Global.Logger.Write("Exception: " + e.ToString());

						throw;
					}
				}
				while (!cancellationToken.IsCancellationRequested);
			}
			catch (OperationCanceledException e)
			{
                stopService = false;
                // Ignore.
                this.Global.Logger.Write("Exception: " + e.ToString());
			}
			catch (Exception e)
			{
				// TODO: Log.
				this.Global.Logger.Write("Exception: " + e.ToString());

				throw;
			}
			finally
			{
                if (stopService)
                {
                    this.Global.Logger.Write("Calling [ServiceBase.Stop] from within [OcrProcessor.OnProcessed].");
                    Task.Run(new Action(() => { this.ServiceBase.Stop(); }));
                    this.Global.Logger.Write("Called [ServiceBase.Stop] from within [OcrProcessor.OnProcessed].");
                }
            }
        }

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}
	}
}