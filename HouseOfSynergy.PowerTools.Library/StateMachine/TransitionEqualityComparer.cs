using System;
using System.Collections.Generic;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public sealed class TransitionEqualityComparer<TState, TCommand>:
		System.Collections.IEqualityComparer,
		IEqualityComparer<Transition<TState, TCommand>>
		where TState: struct, IComparable, IFormattable, IConvertible
		where TCommand: struct, IComparable, IFormattable, IConvertible
	{
		public new bool Equals (object x, object y)
		{
			if (x == null) { return (false); }
			if (y == null) { return (false); }
			if (!(x is Transition<TState, TCommand>)) { return (false); }
			if (!(y is Transition<TState, TCommand>)) { return (false); }

			return (this.Equals((Transition<TState, TCommand>) x, (Transition<TState, TCommand>) y));
		}

		public bool Equals (Transition<TState, TCommand> x, Transition<TState, TCommand> y)
		{
			if (x == null) { return (false); }
			if (y == null) { return (false); }
			if (!(x is Transition<TState, TCommand>)) { return (false); }
			if (!(y is Transition<TState, TCommand>)) { return (false); }

			return
			(
				x.From.Equals(y.From)
				&& x.To.Equals(y.To)
				&& x.Command.Equals(y.Command)
			);
		}

		public int GetHashCode (object value)
		{
			if (value == null)
			{
				throw (new ArgumentNullException("value"));
			}

			if (!(value is Transition<TState, TCommand>))
			{
				throw (new ArgumentException("The argument [value] must be of type [HouseOfSynergy.PowerTools.Library.StateMachine.Transition<TState, TCommand>].", "value"));
			}

			return (this.GetHashCode((Transition<TState, TCommand>) value));
		}

		public int GetHashCode (Transition<TState, TCommand> value)
		{
			int hash = 0;

			hash = 17;
			hash = hash * 31 + value.From.GetHashCode();
			hash = hash * 31 + value.To.GetHashCode();
			hash = hash * 31 + value.Command.GetHashCode();

			return (hash);
		}
	}
}