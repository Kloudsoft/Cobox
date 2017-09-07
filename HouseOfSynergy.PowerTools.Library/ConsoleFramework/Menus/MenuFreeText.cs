using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public class MenuFreeText:
		Menu
	{
		public MenuFreeText ()
			: base(InputType.FreeText)
		{
		}

		public new List<OptionFreeText> Options { get { return (base.Options.ConvertAll<OptionFreeText>(option => ((OptionFreeText) option))); } }
	}
}