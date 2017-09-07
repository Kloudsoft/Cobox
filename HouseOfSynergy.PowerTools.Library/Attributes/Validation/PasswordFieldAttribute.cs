using System.Collections.Generic;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public sealed class PasswordFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.StringSingleLineFieldAttribute
	{
		public bool RequireStrong { get; private set; }

		public PasswordFieldAttribute
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
			int LengthMaximum,
			bool RequireStrong
		)
			: base(Type, Ordinal, Name, Description, Label, Tooltip, Required, ReadOnly, LengthMinimum, LengthMaximum)
		{
			this.RequireStrong = RequireStrong;
		}

		public override System.Windows.Forms.TextBox CreateTextBoxControl ()
		{
			base.CreateTextBoxControl();

			this.TextBoxControl.PasswordChar = '*';

			return (this.TextBoxControl);
		}

		public override void OnHelpTextRequest (List<string> helpTextLines)
		{
			base.OnHelpTextRequest(helpTextLines);

			helpTextLines.Add("Valid characters include [0-9], [a-z], [A-Z] and [~`!@#$%^&*()-_=+{[]}\\|;:'\",.<>].");

			if (this.RequireStrong)
			{
				helpTextLines.Add("This field must contain at lease 1 digit, 1 lowercase letter, 1 uppercase letter and 1 symbol.");
			}
		}

		protected override bool OnValidate ()
		{
			bool result = false;
			int countDigit = 0;
			int countLetterLower = 0;
			int countLetterUpper = 0;
			int countSymbol = 0;

			if ((this.TextBoxControl.Text.Length >= this.LengthMinimum) && (this.TextBoxControl.Text.Length <= this.LengthMaximum))
			{
				if (this.RequireStrong)
				{
					foreach (char c in this.TextBoxControl.Text)
					{
						if (char.IsDigit(c))
							countDigit++;
						if (char.IsLower(c))
							countLetterLower++;
						if (char.IsUpper(c))
							countLetterUpper++;
						if (new List<char>("[~`!@#$%^&*()-_=+{[]}\\|;:'\",.<>]".ToCharArray()).Contains(c))
							countSymbol++;
					}

					result = (countDigit > 0) && (countLetterLower > 0) && (countLetterUpper > 0) && (countSymbol > 0);
				}
			}

			return (result);
		}
	}
}