using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Schedules
{
	public sealed class ScheduleEmpty:
		Schedule
	{
		public ScheduleEmpty ()
			: base(ScheduleType.None)
		{
		}
	}
}