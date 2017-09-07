using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	public partial class FormTimedMessageBox<TEnum>:
		Form
		where TEnum: struct, IComparable, IFormattable, IConvertible
	{
		public IWin32Window MessageBoxOwner { get; private set; }
		public string MessageBoxText { get; private set; }
		public string MessageBoxCaption { get; private set; }
		public TEnum MessageBoxButtons { get; private set; }
		public MessageBoxIcon MessageBoxIcon { get; private set; }
		public TEnum MessageBoxDefaultButton { get; private set; }
		public TimeSpan MessageBoxTimeout { get; private set; }
		public TEnum MessageBoxDialogResult { get; private set; }
		private Stopwatch Stopwatch { get; set; }
		public bool TimedOut { get; private set; }
		private ReadOnlyCollection<Button> MessageBoxButtonCollection { get; set; }

		internal FormTimedMessageBox
		(
			IWin32Window owner,
			string text,
			string caption,
			TEnum buttons,
			MessageBoxIcon icon,
			TEnum defaultButton,
			TimeSpan timeout
		)
		{
			this.InitializeComponent();

			this.MessageBoxOwner = owner;
			this.MessageBoxText = text;
			this.MessageBoxCaption = caption;
			this.MessageBoxButtons = buttons;
			this.MessageBoxIcon = icon;
			this.MessageBoxDefaultButton = defaultButton;
			this.MessageBoxTimeout = timeout;
			this.MessageBoxDialogResult = this.MessageBoxDefaultButton;

			foreach (var control in this.Controls.OfType<Button>())
			{
				this.Controls.Remove(control);
			}

			var list = new List<Button>();
			var values = Enum.GetValues(typeof(TEnum)).Cast<int>().ToList();
			foreach (var value in values)
			{
				if ((value & ((int) ((object) buttons))) == value)
				{
					var button = new Button();

					button.Tag = (TEnum) ((object) value);
					button.Text = ((TEnum) ((object) value)).ToString();
					button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
					button.Click += new EventHandler(this.Button_Click);

					list.Add(button);
				}
			}

			this.MessageBoxButtonCollection = new ReadOnlyCollection<Button>(list);

			this.Owner = (Form) this.MessageBoxOwner;
			this.Text = this.MessageBoxCaption;
			this.LabelText.Text = this.MessageBoxText;
			this.Stopwatch = new Stopwatch();

			switch (this.MessageBoxIcon)
			{
				//case MessageBoxIcon.Asterisk: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.WinLogo.ToBitmap(); break; }
				case MessageBoxIcon.Error: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.Error.ToBitmap(); break; }
				//case MessageBoxIcon.Exclamation: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.WinLogo.ToBitmap(); break; }
				case MessageBoxIcon.Information: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.Information.ToBitmap(); break; }
				//case MessageBoxIcon.None: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.WinLogo.ToBitmap(); break; }
				case MessageBoxIcon.Question: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.Question.ToBitmap(); break; }
				//case MessageBoxIcon.Stop: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.WinLogo.ToBitmap(); break; }
				case MessageBoxIcon.Warning: { this.PictureBoxMessageBoxIcon.Image = SystemIcons.Warning.ToBitmap(); break; }
			}

			if (this.MessageBoxTimeout < TimeSpan.Zero) { throw (new ArgumentOutOfRangeException("timeout", "The argument [timeout] must be greater than [TimeSpan.Zero].")); }

			if (this.MessageBoxTimeout > TimeSpan.Zero)
			{
				this.TimerTimeout.Start();
			}
		}

		private void FormTimedMessageBox_Load (object sender, EventArgs e)
		{
			var right = this.ClientSize.Width;
			var spacer = this.ClientSize.Height - this.LabelTimeout.Bottom;
			for (int i = (this.MessageBoxButtonCollection.Count - 1); i >= 0; i--)
			{
				right -= this.MessageBoxButtonCollection [i].Width + spacer;
				this.Controls.Add(this.MessageBoxButtonCollection [i]);
				this.MessageBoxButtonCollection [i].Location = new Point(right, this.LabelTimeout.Top);
			}

			if (this.MessageBoxButtonCollection.Count > 0)
			{
				this.LabelTimeout.Width = this.MessageBoxButtonCollection [0].Left - this.LabelTimeout.Left - 6;
			}

			using (var graphics = this.LabelText.CreateGraphics())
			{
				var size = graphics.MeasureString(this.LabelText.Text, this.LabelText.Font, 100);
			}
		}

		private void FormTimedMessageBox_Shown (object sender, EventArgs e)
		{
			this.Stopwatch.Start();
		}

		private void FormTimedMessageBox_FormClosing (object sender, FormClosingEventArgs e)
		{
			this.Stopwatch.Stop();
			this.TimerTimeout.Stop();
		}

		private void Button_Click (object sender, EventArgs e)
		{
			this.MessageBoxDialogResult = (TEnum) (sender as Button).Tag;

			this.Close();
		}

		private void TimerTimeout_Tick (object sender, EventArgs e)
		{
			if (this.MessageBoxTimeout > TimeSpan.Zero)
			{
				var remaining = this.MessageBoxTimeout - this.Stopwatch.Elapsed;

				if ((remaining) <= TimeSpan.Zero)
				{
					this.TimedOut = true;
					this.MessageBoxDialogResult = this.MessageBoxDefaultButton;
					this.Close();
				}
				else
				{
					this.LabelTimeout.Text = "Closing in " + remaining.Add(TimeSpan.FromSeconds(1)).ToString(@"hh\:mm\:ss") + ".";
				}
			}
		}
	}
}