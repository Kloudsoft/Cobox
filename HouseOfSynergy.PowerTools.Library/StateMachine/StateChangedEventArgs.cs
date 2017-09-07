using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public class StateChangedEventArgs<TState, TCommand>:
		EventArgs
		where TState: struct, IComparable, IFormattable, IConvertible
		where TCommand: struct, IComparable, IFormattable, IConvertible
	{
		public TransitionEvent<TState, TCommand> TransitionEvent { get; private set; }

		public StateChangedEventArgs (TransitionEvent<TState, TCommand> transitionEvent) { this.TransitionEvent = transitionEvent; }
	}
}