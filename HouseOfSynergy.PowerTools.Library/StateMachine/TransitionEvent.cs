using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public class TransitionEvent<TState, TCommand>
		where TState: struct, IComparable, IFormattable, IConvertible
		where TCommand: struct, IComparable, IFormattable, IConvertible
	{
		#region Enumerations.

		//====================================================================================================
		// Enumerations.
		//====================================================================================================

		public enum EnumType
		{
			None,
			State,
			Command,
		}

		#endregion Enumerations.

		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public long Id { get; private set; }

		public TransitionEvent<TState, TCommand>.EnumType Type { get; private set; }

		public StateMachine<TState, TCommand> StateMachine { get; private set; }

		public DateTimeOffset DateTime { get; private set; }

		public Nullable<TCommand> RequestedCommand { get; private set; }

		public Nullable<TState> RequestedState { get; private set; }

		public Nullable<TState> OldState { get; private set; }

		public Nullable<TState> NewState { get; private set; }

		public string Message { get; private set; }

		/// <summary>
		/// Determines if the the requested operation was well-formed.
		/// </summary>
		public bool HasErrors { get; private set; }

		/// <summary>
		/// Determines if the the requested operation was legal and if it has been applied.
		/// </summary>
		public bool Succeeded { get; private set; }

		#endregion Members.

		#region Constructors, Destructor and Initializers.

		//====================================================================================================
		// Constructors, Destructor and Initializers.
		//====================================================================================================

		private TransitionEvent
		(
			long id,
			TransitionEvent<TState, TCommand>.EnumType type,
			StateMachine<TState, TCommand> machine,
			DateTimeOffset dateTime,
			Nullable<TCommand> requestedCommand,
			Nullable<TState> requestedState,
			TState oldState,
			TState newState,
			bool hasErrors,
			bool succeeded,
			string message
		)
		{
			this.Id = id;
			this.Type = type;
			this.StateMachine = machine;
			this.DateTime = dateTime;
			this.RequestedCommand = requestedCommand;
			this.RequestedState = requestedState;
			this.OldState = oldState;
			this.NewState = newState;
			this.HasErrors = HasErrors;
			this.Succeeded = succeeded;
			this.Message = message;
		}

		public TransitionEvent
		(
			long id,
			StateMachine<TState, TCommand> machine,
			DateTimeOffset dateTime,
			TCommand requestedCommand,
			TState oldState,
			TState newState,
			bool hasErrors,
			bool succeeded,
			string message
		)
			: this
			(
				id,
				TransitionEvent<TState, TCommand>.EnumType.Command,
				machine,
				dateTime,
				requestedCommand,
				null,
				oldState,
				newState,
				hasErrors,
				succeeded,
				message
			)
		{
		}

		public TransitionEvent
		(
			long id,
			StateMachine<TState, TCommand> machine,
			System.DateTimeOffset dateTime,
			TState requestedState,
			TState oldState,
			TState newState,
			bool hasErrors,
			bool succeeded,
			string message
		)
			: this
			(
				id,
				TransitionEvent<TState, TCommand>.EnumType.State,
				machine,
				dateTime,
				null,
				requestedState,
				oldState,
				newState,
				hasErrors,
				succeeded,
				message
			)
		{
		}

		#endregion Constructors, Destructor and Initializers.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		#endregion Methods.
	}
}