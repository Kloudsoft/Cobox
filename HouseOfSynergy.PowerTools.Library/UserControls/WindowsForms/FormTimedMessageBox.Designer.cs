using System;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	partial class FormTimedMessageBox<TEnum>
		where TEnum: struct, IComparable, IFormattable, IConvertible
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			this.PictureBoxMessageBoxIcon = new System.Windows.Forms.PictureBox();
			this.LabelText = new System.Windows.Forms.Label();
			this.ButtonSample = new System.Windows.Forms.Button();
			this.LabelTimeout = new System.Windows.Forms.Label();
			this.TimerTimeout = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxMessageBoxIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.PictureBoxMessageBoxIcon.Location = new System.Drawing.Point(12, 12);
			this.PictureBoxMessageBoxIcon.Name = "pictureBox1";
			this.PictureBoxMessageBoxIcon.Size = new System.Drawing.Size(64, 64);
			this.PictureBoxMessageBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PictureBoxMessageBoxIcon.TabIndex = 0;
			this.PictureBoxMessageBoxIcon.TabStop = false;
			// 
			// label1
			// 
			this.LabelText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LabelText.Location = new System.Drawing.Point(99, 36);
			this.LabelText.Name = "label1";
			this.LabelText.Size = new System.Drawing.Size(329, 222);
			this.LabelText.TabIndex = 1;
			this.LabelText.Text = "Message.";
			// 
			// ButtonSample
			// 
			this.ButtonSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSample.Location = new System.Drawing.Point(353, 280);
			this.ButtonSample.Name = "ButtonSample";
			this.ButtonSample.Size = new System.Drawing.Size(75, 23);
			this.ButtonSample.TabIndex = 2;
			this.ButtonSample.Text = "&Sample";
			this.ButtonSample.UseVisualStyleBackColor = true;
			// 
			// LabelTimeout
			// 
			this.LabelTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LabelTimeout.Location = new System.Drawing.Point(12, 280);
			this.LabelTimeout.Name = "LabelTimeout";
			this.LabelTimeout.Size = new System.Drawing.Size(335, 23);
			this.LabelTimeout.TabIndex = 3;
			this.LabelTimeout.Text = "Closing in 00:00:00.";
			this.LabelTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TimerTimeout
			// 
			this.TimerTimeout.Interval = 200;
			this.TimerTimeout.Tick += new System.EventHandler(this.TimerTimeout_Tick);
			// 
			// FormTimedMessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(440, 315);
			this.Controls.Add(this.LabelTimeout);
			this.Controls.Add(this.ButtonSample);
			this.Controls.Add(this.LabelText);
			this.Controls.Add(this.PictureBoxMessageBoxIcon);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTimedMessageBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTimedMessageBox_FormClosing);
			this.Load += new System.EventHandler(this.FormTimedMessageBox_Load);
			this.Shown += new System.EventHandler(this.FormTimedMessageBox_Shown);
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxMessageBoxIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox PictureBoxMessageBoxIcon;
		private System.Windows.Forms.Label LabelText;
		private System.Windows.Forms.Button ButtonSample;
		private System.Windows.Forms.Label LabelTimeout;
		private System.Windows.Forms.Timer TimerTimeout;
	}
}