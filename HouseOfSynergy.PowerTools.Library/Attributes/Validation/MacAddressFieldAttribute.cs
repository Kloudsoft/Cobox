using System.Collections.Generic;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public class MacAddressFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.StringSingleLineFieldAttribute
	{
		public MacAddressFieldAttribute
		(
			System.Type Type,
			int Ordinal,
			string Name,
			string Description,
			string Label,
			string Tooltip,
			bool Required,
			bool ReadOnly
		)
			: base(Type, Ordinal, Name, Description, Label, Tooltip, Required, ReadOnly, 17, 17)
		{
		}

		public override void OnHelpTextRequest (List<string> helpTextLines)
		{
			base.OnHelpTextRequest(helpTextLines);

			helpTextLines.Add("This field requires a valid MAC Address in the following format: [xx-xx-xx-xx-xx-xx].");
			helpTextLines.Add("Where [X] Represents a number between [0] and [9] or a letter between [a] and [f] inclusive.");
		}

		protected override bool OnValidate ()
		{
			bool result = false;

			result = base.OnValidate();

			if (result)
			{
				result = HouseOfSynergy.PowerTools.Library.Network.MacAddress.Validate(this.TextBoxControl.Text);
			}

			return (result);
		}
	}
}