using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Triggers
{
	public interface ITrigger
	{
		Guid Key { get; }
		Guid JobKey { get; }
	}
}