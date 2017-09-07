using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Jobs
{
	public sealed class JobAction:
		Job
	{
		public Action<CancellationToken> Action { get; private set; }

		public JobAction (Action<CancellationToken> action)
			: base(JobType.Action)
		{
			if (action == null) { throw (new ArgumentNullException("action")); }

			this.Action = action;
		}

		public override void Run (CancellationToken cancellationToken)
		{
			this.Action(cancellationToken);
		}

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
	}
}