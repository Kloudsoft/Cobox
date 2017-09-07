using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Schedules
{
	public sealed class ScheduleDiscreet:
		Schedule
	{
		public ReadOnlyCollection<DateTimeOffset> Times { get; private set; }

		public ScheduleDiscreet (IEnumerable<DateTimeOffset> dateTimeOffsets)
			: base(ScheduleType.Discreet)
		{
			// TODO: Check for duplicates and values that are too close together.
			var list = dateTimeOffsets.ToList().GetSorted();

			this.Times = new ReadOnlyCollection<DateTimeOffset>(list);
		}
	}
}