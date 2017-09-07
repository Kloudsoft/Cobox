using System.Collections.Generic;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public class IpAddressFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.StringSingleLineFieldAttribute
	{
		public IpAddressFieldAttribute
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
			: base(Type, Ordinal, Name, Description, Label, Tooltip, Required, ReadOnly, 7, 15)
		{
		}

		public override void OnHelpTextRequest (List<string> helpTextLines)
		{
			base.OnHelpTextRequest(helpTextLines);

			helpTextLines.Add("This field requires a valid IP Address in the following format: [X.X.X.X].");
			helpTextLines.Add("Where [X] Represents a number between [0] and [255] inclusive.");
		}

		protected override bool OnValidate ()
		{
			bool result = false;

			result = base.OnValidate();

			if (result)
			{
				result = HouseOfSynergy.PowerTools.Library.Network.IpAddressV4.Validate(this.TextBoxControl.Text);
			}

			return (result);
		}
	}
}