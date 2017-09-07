using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public class ProcessRestarterException:
		Exception
	{
		public ProcessRestarterException () : base() { }
		public ProcessRestarterException (string message) : base(message) { }
		protected ProcessRestarterException (SerializationInfo info, StreamingContext context) : base(info, context) { }
		public ProcessRestarterException (string message, Exception innerException) : base(message, innerException) { }
	}

	public class ProcessRestarterTimeoutException:
		ProcessRestarterException
	{
		public ProcessRestarterTimeoutException () : base() { }
		public ProcessRestarterTimeoutException (string message) : base(message) { }
		protected ProcessRestarterTimeoutException (SerializationInfo info, StreamingContext context) : base(info, context) { }
		public ProcessRestarterTimeoutException (string message, Exception innerException) : base(message, innerException) { }
	}

	public class ProcessRestarterExitCodeException:
		ProcessRestarterException
	{
		public ProcessRestarterExitCode ExitCode { get; private set; }

		public ProcessRestarterExitCodeException (ProcessRestarterExitCode code) : base() { }
		public ProcessRestarterExitCodeException (ProcessRestarterExitCode code, string message) : base(message) { }
		protected ProcessRestarterExitCodeException (SerializationInfo info, StreamingContext context) : base(info, context) { }
		public ProcessRestarterExitCodeException (ProcessRestarterExitCode code, string message, Exception innerException) : base(message, innerException) { }
	}
}