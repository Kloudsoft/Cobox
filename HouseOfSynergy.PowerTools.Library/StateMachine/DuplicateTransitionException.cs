using System;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public class DuplicateTransitionException:
		ArgumentException
	{
		public DuplicateTransitionException () : base() { }

		public DuplicateTransitionException (string message) : base(message) { }

		public DuplicateTransitionException (string message, Exception innerException) : base(message, innerException) { }

		public DuplicateTransitionException (string message, string paramName) : base(message, paramName) { }

		public DuplicateTransitionException (string message, string paramName, Exception innerException) : base(message, paramName, innerException) { }
	}
}