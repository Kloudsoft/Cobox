using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Utility
{
	public interface ITCache<T>
	{
		T Get (string cacheName, int cacheTimeoutInSeconds, Func<T> callback);
	}
}