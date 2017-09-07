using System;
using System.Collections.Generic;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Scheduling.Schedules;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Triggers
{
	public sealed class Trigger
	{
		public Guid Key { get; private set; }
		public Schedule Schedule { get; private set; }

		public Trigger (Guid key, Schedule schedule)
		{
			this.Key = key;
			this.Schedule = schedule;
		}

		public static readonly Trigger Empty = new Trigger(Guid.Empty, new ScheduleEmpty());
	}
}