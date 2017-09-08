namespace HouseOfSynergy.AffinityDms.ConsoleClient.Forms
{
	partial class FormTranslations
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTranslations));
			this.PanelMain = new System.Windows.Forms.Panel();
			this.GroupBoxMain = new System.Windows.Forms.GroupBox();
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.GroupBoxMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// PanelMain
			// 
			this.PanelMain.AutoScroll = true;
			this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelMain.Location = new System.Drawing.Point(3, 23);
			this.PanelMain.Name = "PanelMain";
			this.PanelMain.Size = new System.Drawing.Size(754, 473);
			this.PanelMain.TabIndex = 0;
			// 
			// GroupBoxMain
			// 
			this.GroupBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GroupBoxMain.Controls.Add(this.PanelMain);
			this.GroupBoxMain.Location = new System.Drawing.Point(12, 12);
			this.GroupBoxMain.Name = "GroupBoxMain";
			this.GroupBoxMain.Size = new System.Drawing.Size(760, 499);
			this.GroupBoxMain.TabIndex = 1;
			this.GroupBoxMain.TabStop = false;
			this.GroupBoxMain.Text = "Add/Edit Translation Entries";
			// 
			// ButtonOk
			// 
			this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOk.Location = new System.Drawing.Point(566, 517);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(100, 32);
			this.ButtonOk.TabIndex = 2;
			this.ButtonOk.Text = "&Ok";
			this.ButtonOk.UseVisualStyleBackColor = true;
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.Location = new System.Drawing.Point(672, 517);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(100, 32);
			this.ButtonCancel.TabIndex = 3;
			this.ButtonCancel.Text = "&Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			// 
			// FormTranslations
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOk);
			this.Controls.Add(this.GroupBoxMain);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "FormTranslations";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Translation Entries";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.GroupBoxMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel PanelMain;
		private System.Windows.Forms.GroupBox GroupBoxMain;
		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Button ButtonCancel;
	}
}