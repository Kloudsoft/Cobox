using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Log;
using System.Threading;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.BusinessLayer.Ocr;
using System.Net;
using System.Reflection;
using System.Runtime;

namespace HouseOfSynergy.AffinityDms.OcrWinService
{
	public partial class ServiceMain:
		ServiceBase
	{
		private readonly object SyncRoot = new object();

		private Global Global = null;
		private OcrProcessor OcrProcessor = null;
		private OcrEngineSettings OcrEngineSettings = null;

		public ServiceMain (Global global)
		{
			if (global == null) { throw (new ArgumentNullException(nameof(global))); }

			this.InitializeComponent();

			this.Global = global;
		}

		protected override bool CanRaiseEvents { get { return (true); } }

		protected override void OnStart (string [] args)
		{
			try
			{
				lock (this.SyncRoot)
				{
					if (this.OcrProcessor == null)
					{
						if (this.OcrEngineSettings == null)
						{
							this.OcrEngineSettings = new OcrEngineSettings
							(
                                this.Global.Logger,
								File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LeadTools", "License", "eval-license-files.lic.key")),
								Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LeadTools", "License", "eval-license-files.lic"),
								Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data"),
								Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))//, "LeadTools", "Runtime")
							);
						}

						this.OcrProcessor = new OcrProcessor(this, this.Global, this.OcrEngineSettings);
					}

					this.OcrProcessor.Start();
				}
			}
			catch (Exception exception)
			{
				this.Global.Logger.Write(exception);
			}
		}

		protected override void OnPause ()
		{
			lock (this.SyncRoot)
			{
				this.Global.Logger.Write("Service is pausing.");

				if (this.OcrProcessor != null)
				{
					if (this.OcrProcessor.Running)
					{
						if (!this.OcrProcessor.Paused)
						{
							this.OcrProcessor.Pause();
						}
					}
				}

				this.Global.Logger.Write("Service has been paused.");

				base.OnPause();
			}
		}

		protected override void OnContinue ()
		{
			lock (this.SyncRoot)
			{
				this.Global.Logger.Write("Service is resuming.");

				if (this.OcrProcessor != null)
				{
					if (this.OcrProcessor.Running)
					{
						if (this.OcrProcessor.Paused)
						{
							this.OcrProcessor.Resume();
						}
					}
				}

				this.Global.Logger.Write("Service has been resumed.");

				base.OnContinue();
			}
		}

		protected override void OnStop ()
		{
			lock (this.SyncRoot)
			{
				this.OcrProcessor.RequestCancellation();
				this.Global.Logger.Write("Service is stopping.");

				do
				{
					this.RequestAdditionalTime(100);
					Thread.Sleep(TimeSpan.FromSeconds(100));
				}
				while (this.OcrProcessor.Running);

				this.Global.Logger.Write("Service has stopped.");

				base.OnStop();
			}
		}

		protected override bool OnPowerEvent (PowerBroadcastStatus powerStatus)
		{
			return base.OnPowerEvent(powerStatus);
		}

		protected override void OnSessionChange (SessionChangeDescription changeDescription)
		{
			this.OnStop();

			base.OnSessionChange(changeDescription);
		}

		protected override void OnShutdown ()
		{
			base.OnShutdown();
		}

		protected override void OnCustomCommand (int command)
		{
			var serviceCommand = (ServiceCommand) command;

			switch (serviceCommand)
			{
				case ServiceCommand.RequestTerminate:
				{
					this.OcrProcessor.RequestCancellation();

					break;
				}
			}

			base.OnCustomCommand(command);
		}
	}
}