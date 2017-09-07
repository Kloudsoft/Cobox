using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public class FiniteStateMachine<TState, TCommand>:
		StateMachine<TState, TCommand>
		where TState: struct, IComparable, IFormattable, IConvertible
		where TCommand: struct, IComparable, IFormattable, IConvertible
	{
		public FiniteStateMachine (TState initialState)
			: base(initialState)
		{
		}
	}
}