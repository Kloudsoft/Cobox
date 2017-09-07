using System.Collections.Generic;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public abstract class FieldAttribute:
		System.Attribute
	{
		// Final.
		public string PropertyName { get; set; }

		public System.Type Type { get; private set; }

		public int Ordinal { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public string Label { get; private set; }

		public string ToolTip { get; private set; }

		public bool ReadOnly { get; private set; }

		public bool Required { get; private set; }

		public System.Windows.Forms.Label LabelControl { get; private set; }

		public System.Windows.Forms.Control ValueControl { get; set; }

		public System.Windows.Forms.PictureBox PictureBoxControl { get; private set; }

		public FieldAttribute
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
		{
			this.PropertyName = "";
			this.Type = Type;
			this.Ordinal = Ordinal;
			this.Name = Name;
			this.Description = Description;
			this.Label = Label;
			this.ToolTip = Tooltip;
			this.Required = Required;
			this.ReadOnly = ReadOnly;

			this.LabelControl = null;
			this.ValueControl = null;
		}

		public int NumberOfControlsRequired { get { return (2); } }

		public string HelpText
		{
			get
			{
				string text = "";
				List<string> lines = null;

				text = this.ToolTip;

				lines = new List<string>();
				this.OnHelpTextRequest(lines);

				foreach (string line in lines)
				{
					text += System.Environment.NewLine + "   - " + line;
				}

				return (text);
			}
		}

		public virtual void OnHelpTextRequest (List<System.String> helpTextLines)
		{
			helpTextLines.Add("This field is " + (this.Required ? "required." : "optional."));

			if (this.ReadOnly)
			{
				helpTextLines.Add("This field is read only.");
			}
		}

		public System.Windows.Forms.Label CreateLabelControl ()
		{
			this.LabelControl = new System.Windows.Forms.Label();

			this.LabelControl.Tag = this;

			this.LabelControl.AutoSize = false;
			this.LabelControl.Text = this.Label.ToString() + ":";
			this.LabelControl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

			return (this.LabelControl);
		}

		public System.Windows.Forms.PictureBox CreatePictureBoxControl ()
		{
			this.PictureBoxControl = new System.Windows.Forms.PictureBox();

			this.PictureBoxControl.Tag = this;

			return (this.PictureBoxControl);
		}

		public bool Validate ()
		{
			return (this.OnValidate());
		}

		protected abstract bool OnValidate ();
	}
}