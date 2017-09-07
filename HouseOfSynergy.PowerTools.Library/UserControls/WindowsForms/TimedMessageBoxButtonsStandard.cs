using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	[Flags]
	public enum TimedMessageBoxButtonsStandard
	{
		None = 1 << 0,
		Yes = 2 << 0,
		No = 2 << 1,
		Ok = 2 << 2,
		Cancel = 2 << 3,
		Close = 2 << 4,
	}
}