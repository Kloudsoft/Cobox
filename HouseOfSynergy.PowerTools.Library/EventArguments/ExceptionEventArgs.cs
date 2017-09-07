using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.EventArguments
{
	public class ExceptionEventArgs:
		CustomEventArgs
	{
		public Exception Exception { get; private set; }

		public ExceptionEventArgs (Exception exception) { this.Exception = exception; }
	}
}