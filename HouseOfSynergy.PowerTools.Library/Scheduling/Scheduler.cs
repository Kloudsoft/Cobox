using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.EventArguments;
using HouseOfSynergy.PowerTools.Library.Scheduling.Jobs;
using HouseOfSynergy.PowerTools.Library.Scheduling.Schedules;
using HouseOfSynergy.PowerTools.Library.Scheduling.Triggers;
using HouseOfSynergy.PowerTools.Library.Threading;

namespace HouseOfSynergy.PowerTools.Library.Scheduling
{
	public class Scheduler:
		ThreadBase
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private List<Job> _Jobs { get; set; }
		public long Iteration { get; private set; }
		private List<Trigger> _Triggers { get; set; }
		public TimeSpan Resolution { get; private set; }
		public object SyncRootScheduler { get; private set; }
		public ReadOnlyCollection<Job> Jobs { get; private set; }
		public ReadOnlyCollection<Trigger> Triggers { get; private set; }
		private Dictionary<Job, List<Trigger>> MapJobTriggers { get; set; }
		private Dictionary<Trigger, Job> MapTriggerJob { get; set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public Scheduler (bool isBackground = true)
			: this(TimeSpan.TicksPerSecond, isBackground)
		{
		}

		public Scheduler (TimeSpan resolution, bool isBackground = true)
			: this(resolution.Ticks, isBackground)
		{
		}

		private Scheduler (long resolution, bool isBackground = true)
			: base(isBackground)
		{
			if (resolution < TimeSpan.TicksPerSecond) { throw (new ArgumentException("The argument [resolution] must be at least 1 second or more.", "resolution")); }

			this.Iteration = 0;
			this.SyncRootScheduler = new object();
			this.Resolution = TimeSpan.FromTicks(resolution);

			this._Jobs = new List<Job>();
			this._Triggers = new List<Trigger>();

			this.Jobs = new ReadOnlyCollection<Job>(this._Jobs);
			this.Triggers = new ReadOnlyCollection<Trigger>(this._Triggers);

			this.MapTriggerJob = new Dictionary<Trigger, Job>();
			this.MapJobTriggers = new Dictionary<Job, List<Trigger>>();
		}

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public bool Register (Job job, IEnumerable<Schedule> schedules, out Exception exception)
		{
			lock (this.SyncRootScheduler)
			{
				var index = 0;
				var result = false;
				var trigger = Trigger.Empty;
				var triggers = new List<Trigger>();

				exception = null;

				if (job == null) { throw (new ArgumentNullException("job")); }
				if (schedules == null) { throw (new ArgumentNullException("schedules")); }

				try
				{
					job.Key = this.GetKeyFreeJob();
					index = this._Jobs.BinarySearch(job, JobComparer.Default);
					this._Jobs.Insert(~index, job);

					foreach (var schedule in schedules)
					{
						trigger = new Trigger(this.GetKeyFreeTrigger(), schedule);

						index = this._Triggers.BinarySearch(trigger, TriggerComparer.Default);
						this._Triggers.Insert(~index, trigger);

						index = triggers.BinarySearch(trigger, TriggerComparer.Default);
						triggers.Insert(~index, trigger);
					}

					this.MapJobTriggers.Add(job, triggers);

					foreach (var t in triggers)
					{
						this.MapTriggerJob.Add(t, job);
					}

					result = true;
				}
				catch (Exception e)
				{
					exception = e;
					result = false;
					trigger = null;
				}

				return (result);
			}
		}

		public ReadOnlyCollection<Trigger> GetTriggers () { lock (this.SyncRootScheduler) { return (this._Triggers.ToList().AsReadOnly()); } }

		public Guid GetKeyFreeJob ()
		{
			lock (this.SyncRootScheduler)
			{
				var guid = Guid.Empty;

				do
				{
					guid = Guid.NewGuid();
				}
				while (this._Triggers.Any(t => t.Key == guid));

				return (guid);
			}
		}

		public Guid GetKeyFreeTrigger ()
		{
			lock (this.SyncRootScheduler)
			{
				var guid = Guid.Empty;

				do
				{
					guid = Guid.NewGuid();
				}
				while (this._Jobs.Any(t => t.Key == guid));

				return (guid);
			}
		}

		public void Sleep (TimeSpan interval, CancellationToken cancellationToken)
		{
			var minimum = TimeSpan.FromMilliseconds(10);
			var regular = TimeSpan.FromMilliseconds(100);

			if (interval <= minimum)
			{
				Thread.Sleep(minimum);
			}
			else if (interval < regular)
			{
				Thread.Sleep(interval);
			}
			else
			{
				var watch = Stopwatch.StartNew();

				do
				{
					Thread.Sleep(regular);
					if (cancellationToken.IsCancellationRequested) { break; }
				}
				while (watch.Elapsed < interval);

				watch.Stop();
			}
		}

		#endregion Methods.

		#region Methods: ThreadBase Overrides.

		//====================================================================================================
		// Methods: ThreadBase Overrides.
		//====================================================================================================

		protected override void OnStarting (CancelEventArgs e)
		{
			this.Iteration = 0;
		}

		protected override void OnStarted ()
		{
		}

		protected override void OnStopping (CancelEventArgs e)
		{
			lock (this.SyncRootScheduler)
			{
				for (int i = 0; i < this._Jobs.Count; i++)
				{
					try { this._Jobs [i].Stop(); }
					catch { }
				}
			}
		}

		protected override void OnStopped ()
		{
		}

		protected override void OnProcess (CancellationToken cancellationToken)
		{
			var watch = new Stopwatch();
			var interval = TimeSpan.Zero;

			Thread.CurrentThread.Priority = ThreadPriority.Highest;
			System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;

			do
			{
				this.Iteration++;
				if (this.Iteration == long.MaxValue) { this.Iteration = 0; }

				watch.Restart();

				// Trigger here.
				lock (this.SyncRootScheduler)
				{
					var now = DateTimeOffset.Now;
					var triggers = this._Triggers.Where(t => (t.Schedule != null));

					foreach (var trigger in triggers)
					{
						switch (trigger.Schedule.Type)
						{
							case ScheduleType.Discreet:
							{
								// TODO: Mark launched schedules to avoid repitition.
								var launch = (trigger.Schedule as ScheduleDiscreet)
									.Times
									.Any
									(
										time =>
										{
											return ((time <= now) && (now.Subtract(time) <= TimeSpan.FromSeconds(5)));
										}
									);

								if (launch)
								{
									this.MapTriggerJob [trigger].Start();
								}

								break;
							}
							case ScheduleType.Series:
							{
								break;
							}
						}
					}
				}

				interval = this.Resolution - watch.Elapsed;
				this.Sleep(interval, cancellationToken);
				watch.Restart();
			}
			while (!cancellationToken.IsCancellationRequested);
		}

		#endregion Methods: ThreadBase Overrides.

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

					this._Jobs.Clear();
					this._Triggers.Clear();
					this.MapTriggerJob.Clear();
					this.MapJobTriggers.Clear();
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

		public static readonly TimeSpan ResolutionMinimum = TimeSpan.FromSeconds(1);
		public static readonly TimeSpan ResolutionMaximum = TimeSpan.FromMinutes(1);

		#endregion Static.
	}
}