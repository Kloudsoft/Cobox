using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Threading
{
	internal class ThreadBaseStopParameters
	{
		public TimeSpan? Timeout { get; private set; }
		public bool ThrowOnDisposed { get; private set; }

		public ThreadBaseStopParameters (bool throwOnDisposed = true, TimeSpan? timeout = null)
		{
			this.Timeout = timeout;
			this.ThrowOnDisposed = throwOnDisposed;
		}
	}
}