using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Generics;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	/// <summary>
	/// The Transition class represents an instance of a unique state machine transition.
	/// A state machine transition consists of state and command enumerations that can be configured with flexibility.
	/// </summary>
	/// <typeparam name="TState">The state enumeration of the state machine. Multiple constraints added for lack of an enum constraint.</typeparam>
	/// <typeparam name="TCommand">The command enumeration of the state machine. Multiple constraints added for lack of an enum constraint.</typeparam>
	public class Transition<TState, TCommand>:
		System.IEquatable<Transition<TState, TCommand>>
		where TState: struct, IComparable, IFormattable, IConvertible
		where TCommand: struct, IComparable, IFormattable, IConvertible
	{
		/// <summary>
		/// The command used to make a transition from [TState From] to [TState To].
		/// </summary>
		public TCommand Command { get; private set; }

		/// <summary>
		/// The state to change from.
		/// </summary>
		public TState From { get; private set; }

		/// <summary>
		/// The state to change to.
		/// </summary>
		public TState To { get; private set; }

		public int MaxLengthState { get; private set; }

		public int MaxLengthCommand { get; private set; }

		public Transition (TCommand command, TState from, TState to)
		{
			if (!typeof(TState).IsEnum)
				throw (new GenericArgumentTypeException<TState>("The generic parameter [TState] has to be of type [System.Enum] instead of [" + typeof(TState).Name + "].", "TState"));
			if (!typeof(TCommand).IsEnum)
				throw (new GenericArgumentTypeException<TCommand>("The generic parameter [TCommand] has to be of type [System.Enum] instead of [" + typeof(TState).Name + "].", "TCommand"));

			this.MaxLengthState = System.Enum.GetNames(typeof(TState)).ToList().ConvertAll<int>(name => name.Length).Max();
			this.MaxLengthCommand = System.Enum.GetNames(typeof(TCommand)).ToList().ConvertAll<int>(name => name.Length).Max();

			this.Command = command;
			this.From = from;
			this.To = to;
		}

		public Transition (Transition<TState, TCommand> transition) : this(transition.Command, transition.From, transition.To) { }

		public override bool Equals (object other) { return Equals(other as Transition<TState, TCommand>); }

		public bool Equals (Transition<TState, TCommand> other)
		{
			if (other == null)
			{
				return false;
			}

			return
			(
				this.From.Equals(other.From)
				&& this.To.Equals(other.To)
				&& this.Command.Equals(other.Command)
			);
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			hash = 17;
			hash = hash * 31 + this.From.GetHashCode();
			hash = hash * 31 + this.To.GetHashCode();
			hash = hash * 31 + this.Command.GetHashCode();

			return (hash);
		}

		public override string ToString () { return (this.Command.ToString().PadRight(this.MaxLengthCommand, ' ') + ": " + this.From.ToString().PadRight(this.MaxLengthState, ' ') + " to " + this.To.ToString()); }
	}
}