using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public class StateChangeRequestedEventArgs<TState, TCommand>:
		EventArgs
		where TState: struct, IComparable, IFormattable, IConvertible
		where TCommand: struct, IComparable, IFormattable, IConvertible
	{
		public TransitionEvent<TState, TCommand> TransitionEvent { get; private set; }

		public StateChangeRequestedEventArgs (TransitionEvent<TState, TCommand> transitionEvent) { this.TransitionEvent = transitionEvent; }
	}
}