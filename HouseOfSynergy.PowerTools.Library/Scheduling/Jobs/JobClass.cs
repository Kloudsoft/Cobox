using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Jobs
{
	public abstract class JobClass:
		Job
	{
		public Type ClassType { get; private set; }

		public JobClass ()
			: base(JobType.Class)
		{
			this.ClassType = this.GetType();
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