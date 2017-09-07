using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Scheduling
{
	public sealed class JobEventArgs:
		EventArgs
	{
		/// <summary>
		/// Specifies whether the scheduler should try to run the job again for the same scheduled instance.
		/// </summary>
		public bool Retry { get; set; }

		/// <summary>
		/// Specifies whether to delete all scheduled instances of this job.
		/// </summary>
		public bool DeleteSchedule { get; set; }

		public DateTimeOffset DateTime { get; private set; }

		public JobEventArgs ()
		{
			this.Retry = false;
			this.DeleteSchedule = false;
			;
			this.DateTime = DateTimeOffset.Now;
		}

		public JobEventArgs (DateTimeOffset dateTime)
		{
			this.Retry = false;
			this.DateTime = dateTime;
			this.DeleteSchedule = false;
		}
	}
}