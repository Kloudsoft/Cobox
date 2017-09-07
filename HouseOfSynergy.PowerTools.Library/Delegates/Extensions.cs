using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Delegates
{
	public static class Extensions
	{
		public static void SafeInvoke (this System.EventHandler handler, object sender)
		{
			if (handler != null)
			{
				handler(sender, EventArgs.Empty);
			}
		}

		public static void SafeInvoke<TSender, TEventArgs> (this HouseOfSynergy.PowerTools.Library.Delegates.EventHandler<TSender, TEventArgs> handler, TSender sender, TEventArgs eventArgs)
			where TSender: class
			where TEventArgs: EventArgs
		{
			if (handler != null)
			{
				handler(sender, eventArgs);
			}
		}
	}
}