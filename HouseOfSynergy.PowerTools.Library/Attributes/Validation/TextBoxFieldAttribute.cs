namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public abstract class TextBoxFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.FieldAttribute
	{
		public System.Windows.Forms.TextBox TextBoxControl { get; private set; }

		public TextBoxFieldAttribute
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
			: base
			(
				Type,
				Ordinal,
				Name,
				Description,
				Label,
				Tooltip,
				Required,
				ReadOnly
			)
		{
		}

		public virtual System.Windows.Forms.TextBox CreateTextBoxControl ()
		{
			this.TextBoxControl = new System.Windows.Forms.TextBox();

			this.TextBoxControl.Tag = this;
			this.TextBoxControl.TabStop = !this.ReadOnly;
			this.TextBoxControl.ReadOnly = this.ReadOnly;
			this.TextBoxControl.BackColor = this.ReadOnly ? System.Drawing.Color.LightGray : System.Drawing.SystemColors.WindowText;

			this.ValueControl = this.TextBoxControl;

			return (this.TextBoxControl);
		}
	}
}