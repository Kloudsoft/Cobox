using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Jobs
{
	public interface IJob
	{
		Guid Key { get; }
		string Name { get; }
		bool AllowConcurrentExecution { get; }

		void Run ();
	}
}