using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
//using HouseOfSynergy.PowerTools.Library.Collections;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Schedules
{
	public sealed class ScheduleSeries:
		Schedule
	{
		public ReadOnlyCollection<TimeSpan> TimesOfDay { get; private set; }
		public ReadOnlyDictionary<int, bool> DaysOfYear { get; private set; }
		public ReadOnlyDictionary<int, bool> DaysOfMonth { get; private set; }
		public ReadOnlyDictionary<DayOfWeek, bool> DaysOfWeek { get; private set; }

		public ScheduleSeries (IEnumerable<TimeSpan> timesOfDay)
			: base(ScheduleType.Series)
		{
			var daysOfYear = new Dictionary<int, bool>();
			var daysOfMonth = new Dictionary<int, bool>();
			var daysOfWeek = new Dictionary<DayOfWeek, bool>();

			Enumerable.Range(1, 31).ToList().ForEach(d => daysOfMonth.Add(d, false));
			Enumerable.Range(1, 366).ToList().ForEach(d => daysOfYear.Add(d, false));
			EnumUtilities.GetValues<DayOfWeek>().ForEach(d => daysOfWeek.Add(d, false));

			// TODO: Check for duplicates and values that are too close together.
			var list = timesOfDay.ToList().GetSorted();

			this.TimesOfDay = new ReadOnlyCollection<TimeSpan>(list);
			this.DaysOfYear = new ReadOnlyDictionary<int, bool>(daysOfYear);
			this.DaysOfMonth = new ReadOnlyDictionary<int, bool>(daysOfMonth);
			this.DaysOfWeek = new ReadOnlyDictionary<DayOfWeek, bool>(daysOfWeek);
		}
	}
}