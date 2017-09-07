using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Utility
{
	public class TCacheInternal<T>:
		ITCache<T>
	{
		private static object SyncRoot = new object();

		public T Get(string cacheName, int cacheTimeoutInSeconds, Func<T> callback)
		{
			var entities = HttpContext.Current.Cache.Get(cacheName);

			if (entities == null)
			{
				lock (TCacheInternal<T>.SyncRoot)
				{
					if (entities == null)
					{
						entities = callback();

						HttpContext.Current.Cache.Insert(cacheName, entities, null, DateTime.UtcNow.Add(TimeSpan.FromSeconds(cacheTimeoutInSeconds)), TimeSpan.Zero);
					}
				}
			}

			return ((T) entities);
		}
	}
}