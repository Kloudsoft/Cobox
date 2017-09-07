namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public class StringSingleLineFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.TextBoxFieldAttribute
	{
		public int LengthMinimum { get; private set; }

		public int LengthMaximum { get; private set; }

		public bool AllowMultiline { get; private set; }

		public StringSingleLineFieldAttribute
		(
			System.Type Type,
			int Ordinal,
			string Name,
			string Description,
			string Label,
			string Tooltip,
			bool Required,
			bool ReadOnly,
			int LengthMinimum,
			int LengthMaximum
		)
			: base(Type, Ordinal, Name, Description, Label, Tooltip, Required, ReadOnly)
		{
			this.LengthMinimum = LengthMinimum;
			this.LengthMaximum = LengthMaximum;
		}

		public override System.Windows.Forms.TextBox CreateTextBoxControl ()
		{
			base.CreateTextBoxControl();

			this.TextBoxControl.MaxLength = this.LengthMaximum;

			return (this.TextBoxControl);
		}

		protected override bool OnValidate ()
		{
			bool result = false;

			if (this.Required)
			{
				if (this.TextBoxControl.Text.Trim().Length > 0)
				{
					if ((this.TextBoxControl.Text.Length >= this.LengthMinimum) && (this.TextBoxControl.Text.Length <= this.LengthMaximum))
					{
						result = true;
					}
				}
			}
			else
			{
				result = true;
			}

			return (result);
		}
	}
}