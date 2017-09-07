using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Delegates
{
	public delegate void EventHandler<TSender, TEventArgs> (TSender sender, TEventArgs e)
		where TSender: class
		where TEventArgs: EventArgs
		;
}