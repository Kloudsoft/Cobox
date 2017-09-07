using System;
using System.Collections.Generic;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public class DecimalFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.TextBoxFieldAttribute
	{
		public int PrecisionMinimum { get { return (0); } }

		public int PrecisionMaximum { get { return (28); } }

		public int Precision { get; private set; }

		public decimal Minimum { get; private set; }

		public decimal Maximum { get; private set; }

		public DecimalFieldAttribute
		(
			System.Type Type,
			int Ordinal,
			string Name,
			string Description,
			string Label,
			string Tooltip,
			bool Required,
			bool ReadOnly,
			decimal Minimum,
			decimal Maximum,
			int Precision
		)
			: base(Type, Ordinal, Name, Description, Label, Tooltip, Required, ReadOnly)
		{
			if (Minimum > Maximum)
			{
				throw (new ArgumentException("Minimum cannot be greater than Maximum.", "Minimum, Maximum"));
			}

			if ((Precision < PrecisionMinimum) || (Precision > PrecisionMaximum))
			{
				throw (new ArgumentException("Precision has to be a value between " + PrecisionMinimum.ToString() + " and " + PrecisionMaximum.ToString() + " inclusive.", "Precision"));
			}

			this.Minimum = Minimum;
			this.Maximum = Maximum;
			this.Precision = Precision;
		}

		public override System.Windows.Forms.TextBox CreateTextBoxControl ()
		{
			base.CreateTextBoxControl();

			this.TextBoxControl.MaxLength = 50;

			return (this.TextBoxControl);
		}

		public override void OnHelpTextRequest (List<string> helpTextLines)
		{
			base.OnHelpTextRequest(helpTextLines);

			helpTextLines.Add("This field can accept values between " + this.Minimum.ToString("N" + this.Precision.ToString()) + " and " + this.Maximum.ToString("N" + this.Precision.ToString()) + " inclusively.");
		}

		protected override bool OnValidate ()
		{
			decimal number = 0;
			bool result = false;

			if (decimal.TryParse(this.TextBoxControl.Text, out number))
			{
				if ((number >= this.Minimum) && (number <= this.Maximum))
				{
					result = true;
				}
			}

			return (result);
		}
	}
}