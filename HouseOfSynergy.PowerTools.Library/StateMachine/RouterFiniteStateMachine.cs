using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.StateMachine
{
	public sealed class RouterFiniteStateMachine:
		FiniteStateMachine<RouterFiniteStateMachine.EnumState, RouterFiniteStateMachine.EnumCommand>
	{
		public enum EnumCommand { None, PlugIn, SwitchOn, Boot, Reboot, SwitchOff, UnPlug }

		public enum EnumState { None, Unplugged, PluggedIn, SwitchedOn, Booting, Running, Rebooting, SwitchedOff, Error }

		public RouterFiniteStateMachine ()
			: base(EnumState.Unplugged)
		{
			this.AddTransition(EnumCommand.PlugIn, EnumState.Unplugged, EnumState.PluggedIn);

			this.AddTransition(EnumCommand.SwitchOn, EnumState.PluggedIn, EnumState.SwitchedOn);
			this.AddTransition(EnumCommand.None, EnumState.PluggedIn, EnumState.Error);
			this.AddTransition(EnumCommand.UnPlug, EnumState.PluggedIn, EnumState.Unplugged);

			this.AddTransition(EnumCommand.Boot, EnumState.SwitchedOn, EnumState.Booting);
			this.AddTransition(EnumCommand.None, EnumState.SwitchedOn, EnumState.Error);
			this.AddTransition(EnumCommand.SwitchOff, EnumState.SwitchedOn, EnumState.SwitchedOff);
			this.AddTransition(EnumCommand.UnPlug, EnumState.SwitchedOn, EnumState.Unplugged);

			this.AddTransition(EnumCommand.PlugIn, EnumState.Booting, EnumState.Running);
			this.AddTransition(EnumCommand.None, EnumState.Booting, EnumState.Error);
			this.AddTransition(EnumCommand.SwitchOff, EnumState.Booting, EnumState.SwitchedOff);
			this.AddTransition(EnumCommand.UnPlug, EnumState.Booting, EnumState.Unplugged);

			this.AddTransition(EnumCommand.PlugIn, EnumState.Running, EnumState.Rebooting);
			this.AddTransition(EnumCommand.None, EnumState.Running, EnumState.Error);
			this.AddTransition(EnumCommand.SwitchOff, EnumState.Running, EnumState.SwitchedOff);
			this.AddTransition(EnumCommand.UnPlug, EnumState.Running, EnumState.Unplugged);

			this.AddTransition(EnumCommand.PlugIn, EnumState.Rebooting, EnumState.Running);
			this.AddTransition(EnumCommand.None, EnumState.Rebooting, EnumState.Error);
			this.AddTransition(EnumCommand.SwitchOff, EnumState.Rebooting, EnumState.SwitchedOff);
			this.AddTransition(EnumCommand.UnPlug, EnumState.Rebooting, EnumState.Unplugged);

			this.AddTransition(EnumCommand.SwitchOn, EnumState.SwitchedOff, EnumState.SwitchedOn);
			this.AddTransition(EnumCommand.UnPlug, EnumState.SwitchedOff, EnumState.Unplugged);

			this.AddTransition(EnumCommand.Reboot, EnumState.Error, EnumState.Rebooting);
			this.AddTransition(EnumCommand.SwitchOff, EnumState.Error, EnumState.SwitchedOff);
			this.AddTransition(EnumCommand.UnPlug, EnumState.Error, EnumState.Unplugged);
		}
	}
}