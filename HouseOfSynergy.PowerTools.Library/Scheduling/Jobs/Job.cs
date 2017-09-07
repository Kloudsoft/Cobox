using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.Threading;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Jobs
{
	public abstract class Job:
		ThreadBase
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public Guid Key { get; internal set; }
		public string Name { get; private set; }
		public JobType Type { get; private set; }
		public bool AllowConcurrentExecution { get; private set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		protected Job (JobType type)
		{
			this.Type = type;
		}

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		private Action Action { get; set; }

		#endregion Properties.

		#region Methods: ThreadBase.

		//====================================================================================================
		// Methods: ThreadBase.
		//====================================================================================================

		protected sealed override void OnStarting (CancelEventArgs e)
		{
		}

		protected sealed override void OnStarted ()
		{
		}

		protected sealed override void OnStopping (CancelEventArgs e)
		{
		}

		protected sealed override void OnStopped ()
		{
		}

		protected sealed override void OnProcess (CancellationToken cancellationToken)
		{
			switch (this.Type)
			{
				case JobType.Action:
				case JobType.Class:
				{
					this.Run(cancellationToken);

					break;
				}
				default:
				{
					throw (new NotImplementedException());
				}
			}
		}

		public abstract void Run (CancellationToken cancellationToken);

		#endregion Methods: ThreadBase.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		#endregion Methods.

		#region Interface: IDisposable.

		//====================================================================================================
		// Interface: IDisposable.
		//====================================================================================================

		private bool Disposed { get; set; }

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

		#endregion Interface: IDisposable.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		#endregion Static.
	}
}