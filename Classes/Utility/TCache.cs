using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Utility
{
	public class TCache<T>:
		ITCache<T>
	{
		private static object SyncRoot = new object();

		public T Get(string cacheName, int cacheTimeoutInSeconds, Func<T> callback)
		{
			return (new TCacheInternal<T>().Get(cacheName, cacheTimeoutInSeconds, callback));
		}
	}
}