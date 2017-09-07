using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Schedules
{
	public enum ScheduleType
	{
		/// <summary>
		/// Represents a schedule with no entries.
		/// </summary>
		None,

		/// <summary>
		/// Represents a schedule that stores recurrence information only.
		/// </summary>
		Series,

		/// <summary>
		/// Represents a schedule that stores distinct future dates.
		/// </summary>
		Discreet,
	}
}