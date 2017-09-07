using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Delegates;
using HouseOfSynergy.PowerTools.Library.Generics;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public abstract class StateMachine<TState, TCommand>
		where TState: struct, System.IComparable, System.IFormattable, System.IConvertible
		where TCommand: struct, System.IComparable, System.IFormattable, System.IConvertible
	{
		#region Delegates and Events.

		//====================================================================================================
		// Delegates and Events.
		//====================================================================================================

		public event EventHandler<StateMachine<TState, TCommand>, StateChangedEventArgs<TState, TCommand>> StateChangedEvent = null;
		//public event EventHandler<HouseOfSynergy.PowerTools.Library.StateMachine.StateMachine<TState, TCommand>, StateChangeRequestedEventArgs<TState, TCommand>> StateChangeRequestedEvent = null;

		#endregion Delegates and Events.

		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private object _SyncRoot = new object();
		private List<TState> _StateHistory { get; set; }
		private List<TState> _AllowedStates { get; set; }
		private List<TCommand> _CommandHistory { get; set; }
		private List<TCommand> _AllowedCommands { get; set; }
		private List<Transition<TState, TCommand>> _AllTransitions { get; set; }
		private List<Transition<TState, TCommand>> _AllowedTransitions { get; set; }
		private List<TransitionEvent<TState, TCommand>> _TransitionHistory { get; set; }

		public bool Locked { get; private set; }

		/// <summary>
		/// The [Initial] property represents the initial state of this state machine.
		/// </summary>
		public TState InitialState { get; private set; }

		/// <summary>
		/// The [State] property represents the current state of this state machine.
		/// </summary>
		public TState State { get; private set; }

		#endregion Members.

		#region Constructors, Destructor and Initializers.

		//====================================================================================================
		// Constructors, Destructor and Initializers.
		//====================================================================================================

		public StateMachine (TState initialState)
		{
			if (!typeof(TState).IsEnum)
				throw (new GenericArgumentTypeException<TState>("The generic parameter [TState] has to be of type [System.Enum] instead of [" + typeof(TState).Name + "].", "TState"));
			if (!typeof(TCommand).IsEnum)
				throw (new GenericArgumentTypeException<TCommand>("The generic parameter [TCommand] has to be of type [System.Enum] instead of [" + typeof(TState).Name + "].", "TCommand"));
			if (!Enum.GetNames(typeof(TState)).ToList().Contains("None"))
				throw (new GenericArgumentTypeException<TState>("The generic parameter [TState] must contain an enumeration value called [None].", "TState"));
			if (!Enum.GetNames(typeof(TCommand)).ToList().Contains("None"))
				throw (new GenericArgumentTypeException<TCommand>("The generic parameter [TCommand] must contain an enumeration value called [None].", "TCommand"));
			if (initialState.ToString().Equals("None"))
				throw (new ArgumentException("The [initialState] of a state machine cannot be [None].", "initialState"));

			this.InitialState = initialState;
			this.State = initialState;

			this._StateHistory = new List<TState>();
			this._CommandHistory = new List<TCommand>();
			this._TransitionHistory = new List<TransitionEvent<TState, TCommand>>();

			this._AllTransitions = new List<Transition<TState, TCommand>>();

			this._AllowedStates = new List<TState>();
			this._AllowedCommands = new List<TCommand>();
			this._AllowedTransitions = new List<Transition<TState, TCommand>>();
		}

		#endregion Constructors, Destructor and Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public object SyncRoot { get { return (this._SyncRoot); } }

		public int MaxWidthOfState { get { return (System.Enum.GetNames(typeof(TState)).Max(name => name.Length)); } }
		public int MaxWidthOfCommand { get { return (System.Enum.GetNames(typeof(TCommand)).Max(name => name.Length)); } }

		public ReadOnlyCollection<TState> AllStates { get { return (HouseOfSynergy.PowerTools.Library.Utility.EnumUtilities.GetValues<TState>()).AsReadOnly(); } }
		public ReadOnlyCollection<TCommand> AllCommands { get { return (HouseOfSynergy.PowerTools.Library.Utility.EnumUtilities.GetValues<TCommand>().AsReadOnly()); } }
		public ReadOnlyCollection<TState> StateHistory { get { return (this._StateHistory.AsReadOnly()); } }
		public ReadOnlyCollection<TCommand> CommandHistory { get { return (this._CommandHistory.AsReadOnly()); } }
		public ReadOnlyCollection<Transition<TState, TCommand>> AllTransitions { get { return (this._AllTransitions.AsReadOnly()); } }
		public ReadOnlyCollection<TransitionEvent<TState, TCommand>> TransitionHistory { get { return (this._TransitionHistory.AsReadOnly()); } }

		/// <summary>
		/// Gets a list of commands that can currently be executed.
		/// </summary>
		public ReadOnlyCollection<TState> AvailableStates { get { return (this._AllowedStates.AsReadOnly()); } }

		/// <summary>
		/// Gets a list of commands that can currently be executed.
		/// </summary>
		public ReadOnlyCollection<TCommand> AvailableCommands { get { return (this._AllowedCommands.AsReadOnly()); } }

		/// <summary>
		/// Gets a list of transitions that can be currently performed.
		/// </summary>
		public ReadOnlyCollection<Transition<TState, TCommand>> AvailableTransitions { get { return (this._AllowedTransitions.AsReadOnly()); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public void AddTransition (TCommand command, TState from, TState to) { lock (this._SyncRoot) { this.AddTransition(new Transition<TState, TCommand>(command, from, to)); } }

		public void AddTransition (List<TCommand> commands, TState from, TState to) { lock (this._SyncRoot) { commands.ForEach(command => this.AddTransition(new Transition<TState, TCommand>(command, from, to))); } }

		public void AddTransition (TCommand command, List<TState> fromStates, TState to) { lock (this._SyncRoot) { fromStates.ForEach(from => this.AddTransition(new Transition<TState, TCommand>(command, from, to))); } }

		public void AddTransition (TCommand command, TState from, List<TState> toStates) { lock (this._SyncRoot) { toStates.ForEach(to => this.AddTransition(new Transition<TState, TCommand>(command, from, to))); } }

		public void AddTransition (Transition<TState, TCommand> transition)
		{
			lock (this._SyncRoot)
			{
				if (this.Locked)
					throw (new NotSupportedException("The state machine is locked for adding transitions."));

				foreach (var t in this._AllTransitions)
				{
					if (t.From.ToString().Equals("None"))
					{
						throw (new ArgumentException("A transition with [None] in the [From] and/or [To] fields is not valid.", "transition"));
					}

					if (t.To.ToString().Equals("None"))
					{
						throw (new ArgumentException("A transition with [None] in the [From] and/or [To] fields is not valid.", "transition"));
					}

					if (t.Equals(transition))
					{
						throw (new DuplicateTransitionException("A transition with this signature already exists: (Command: " + transition.Command.ToString() + ", From: " + transition.From.ToString() + ", To: " + transition.To.ToString() + ").", "transition"));
					}

					if ((t.From.ToString() == transition.From.ToString()) && (t.Command.ToString() == transition.Command.ToString()))
					{
						throw (new DuplicateTransitionException("A transition with this signature already exists: (Command: " + transition.Command.ToString() + ", From: " + transition.From.ToString() + ", To: " + transition.To.ToString() + ").", "transition"));
					}
				}

				this._AllTransitions.Add(new Transition<TState, TCommand>(transition));
			}
		}

		public void Lock () { lock (this._SyncRoot) { if (!this.Locked) this.Locked = true; } }

		public void UnLock () { lock (this._SyncRoot) { if (this.Locked) this.Locked = false; } }

		/// <summary>
		/// Strictly for illegal state transition testing. Do not expose or use in client code.
		/// </summary>
		protected internal void SetState (TState state) { this.State = state; throw (new InvalidOperationException("This function is strictly meant for testing only.")); }

		public bool RequestTransition (TCommand command)
		{
			bool result = false;
			Transition<TState, TCommand> transition = null;
			TransitionEvent<TState, TCommand> transitionEvent = null;

			lock (this._SyncRoot)
			{
				if (command.ToString() == "None")
				{
					transitionEvent = new TransitionEvent<TState, TCommand>
					(
						this._TransitionHistory.Count + 1,
						this,
						DateTimeOffset.Now,
						command,
						this.State,
						this.State,
						true,
						false,
						"The command [" + command.ToString() + "] is not a valid transition request."
					);
				}
				else
				{
					try
					{
						transition = this._AllTransitions.SingleOrDefault(t => (t.Command.Equals(command) && t.From.Equals(this.State)));

						if (transition == null)
						{
							transitionEvent = new TransitionEvent<TState, TCommand>
							(
								this._TransitionHistory.Count + 1,
								this,
								System.DateTimeOffset.Now,
								command,
								this.State,
								this.State,
								true,
								false,
								"The command [" + command.ToString() + "] is not a valid transition request while the machine is in state [" + this.State.ToString() + "]."
							);
						}
						else
						{
							this.State = transition.To;

							transitionEvent = new TransitionEvent<TState, TCommand>
							(
								this._TransitionHistory.Count + 1,
								this,
								System.DateTimeOffset.Now,
								transition.Command,
								transition.From,
								transition.To,
								false,
								true,
								"The command [" + command.ToString() + "] was executed successfully and the machine state has been updated from [" + transition.From.ToString() + "] to [" + transition.To.ToString() + "]."
							);
						}
					}
					catch //(Exception exception)
					{
						throw;
					}
				}

				result = transitionEvent.Succeeded;
				this._TransitionHistory.Add(transitionEvent);

				if (result)
				{
					this.OnStateChanged(transitionEvent);
				}
			}

			return (result);
		}

		public bool RequestTransition (TState state)
		{
			bool result = false;
			Transition<TState, TCommand> transition = null;
			TransitionEvent<TState, TCommand> transitionEvent = null;

			lock (this._SyncRoot)
			{
				if (state.ToString() == "None")
				{
					transitionEvent = new TransitionEvent<TState, TCommand>
					(
						this._TransitionHistory.Count + 1,
						this,
						DateTimeOffset.Now,
						state,
						this.State,
						this.State,
						true,
						false,
						"The state [" + state.ToString() + "] is not a valid transition request."
					);
				}
				else
				{
					transition = this._AllTransitions.SingleOrDefault(t => (t.From.Equals(this.State) && t.To.Equals(state)));

					if (transition == null)
					{
						transitionEvent = new TransitionEvent<TState, TCommand>
						(
							this._TransitionHistory.Count + 1,
							this,
							System.DateTimeOffset.Now,
							state,
							this.State,
							this.State,
							true,
							false,
							"The state [" + state.ToString() + "] is not a valid transition request while the machine is in state [" + this.State.ToString() + "]."
						);
					}
					else
					{
						this.State = transition.To;

						transitionEvent = new TransitionEvent<TState, TCommand>
						(
							this._TransitionHistory.Count + 1,
							this,
							System.DateTimeOffset.Now,
							transition.From,
							transition.From,
							transition.To,
							false,
							true,
							"The state [" + state.ToString() + "] was executed successfully and the machine state has been updated from [" + transition.From.ToString() + "] to [" + transition.To.ToString() + "]."
						);
					}
				}

				result = transitionEvent.Succeeded;
				this._TransitionHistory.Add(transitionEvent);

				if (result)
				{
					this.OnStateChanged(transitionEvent);
				}
			}

			return (result);
		}

		#endregion Methods.

		#region Events.

		//====================================================================================================
		// Events.
		//====================================================================================================

		protected virtual void OnStateChanged (TransitionEvent<TState, TCommand> transitionEvent)
		{
			EventHandler<StateMachine<TState, TCommand>, StateChangedEventArgs<TState, TCommand>> instance = null;

			try
			{
				instance = this.StateChangedEvent;

				if (instance != null)
				{
					instance(this, new StateChangedEventArgs<TState, TCommand>(transitionEvent));
				}
			}
			catch
			{
			}
		}

		#endregion Events.
	}
}