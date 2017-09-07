using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public class MenuSingleKey:
		Menu
	{
		public MenuSingleKey ()
			: base(InputType.SingleKey)
		{
		}

		public new OptionSingleKey OptionSelected { get { return ((OptionSingleKey) base.OptionSelected); } }
		public new List<OptionSingleKey> Options { get { return (base.Options.ConvertAll<OptionSingleKey>(option => ((OptionSingleKey) option))); } }
		public new OptionSingleKey OptionDefault { get { return ((OptionSingleKey) base.OptionDefault); } protected set { base.OptionDefault = value; } }

		public void AddOption (OptionSingleKey option)
		{
			base.Options.Add(option);
		}
	}
}