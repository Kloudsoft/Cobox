using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.EventArguments
{
	public class CancelExceptionEventArgs:
		ExceptionEventArgs
	{
		public bool Cancel { get; set; }

		public CancelExceptionEventArgs (bool cancel = false, Exception exception = null) : base(exception) { this.Cancel = cancel; }
	}
}