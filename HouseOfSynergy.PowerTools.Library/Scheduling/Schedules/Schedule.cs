using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Schedules
{
	public abstract class Schedule
	{
		public ScheduleType Type { get; private set; }

		protected Schedule (ScheduleType type)
		{
			this.Type = type;
		}
	}
}