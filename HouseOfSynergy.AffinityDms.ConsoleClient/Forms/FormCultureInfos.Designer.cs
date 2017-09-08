namespace HouseOfSynergy.AffinityDms.ConsoleClient.Forms
{
	partial class FormCultureInfos
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCultureInfos));
			this.ListViewCultures = new System.Windows.Forms.ListView();
			this.ColumnHeaderCultureName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeaderCultureLocaleId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeaderCultureNameNative = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeaderCultureNameDisplay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeaderCultureNameEnglish = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeaderCultureNameIsoTwoLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeaderCultureNameIsoThreeLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeaderCultureNameWindowsThreeLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ListViewCultures
			// 
			this.ListViewCultures.AllowColumnReorder = true;
			this.ListViewCultures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListViewCultures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderCultureName,
            this.ColumnHeaderCultureLocaleId,
            this.ColumnHeaderCultureNameNative,
            this.ColumnHeaderCultureNameDisplay,
            this.ColumnHeaderCultureNameEnglish,
            this.ColumnHeaderCultureNameIsoTwoLetter,
            this.ColumnHeaderCultureNameIsoThreeLetter,
            this.ColumnHeaderCultureNameWindowsThreeLetter});
			this.ListViewCultures.FullRowSelect = true;
			this.ListViewCultures.GridLines = true;
			this.ListViewCultures.HideSelection = false;
			this.ListViewCultures.Location = new System.Drawing.Point(12, 12);
			this.ListViewCultures.MultiSelect = false;
			this.ListViewCultures.Name = "ListViewCultures";
			this.ListViewCultures.Size = new System.Drawing.Size(738, 319);
			this.ListViewCultures.TabIndex = 0;
			this.ListViewCultures.UseCompatibleStateImageBehavior = false;
			this.ListViewCultures.View = System.Windows.Forms.View.Details;
			this.ListViewCultures.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewCultures_ColumnClick);
			this.ListViewCultures.SelectedIndexChanged += new System.EventHandler(this.ListViewCultures_SelectedIndexChanged);
			this.ListViewCultures.DoubleClick += new System.EventHandler(this.ListViewCultures_DoubleClick);
			// 
			// ColumnHeaderCultureName
			// 
			this.ColumnHeaderCultureName.Text = "Name";
			this.ColumnHeaderCultureName.Width = 50;
			// 
			// ColumnHeaderCultureLocaleId
			// 
			this.ColumnHeaderCultureLocaleId.Text = "Locale Id";
			this.ColumnHeaderCultureLocaleId.Width = 70;
			// 
			// ColumnHeaderCultureNameNative
			// 
			this.ColumnHeaderCultureNameNative.Text = "Native";
			this.ColumnHeaderCultureNameNative.Width = 54;
			// 
			// ColumnHeaderCultureNameDisplay
			// 
			this.ColumnHeaderCultureNameDisplay.Text = "Display";
			// 
			// ColumnHeaderCultureNameEnglish
			// 
			this.ColumnHeaderCultureNameEnglish.Text = "English";
			this.ColumnHeaderCultureNameEnglish.Width = 59;
			// 
			// ColumnHeaderCultureNameIsoTwoLetter
			// 
			this.ColumnHeaderCultureNameIsoTwoLetter.Text = "ISO 2";
			this.ColumnHeaderCultureNameIsoTwoLetter.Width = 46;
			// 
			// ColumnHeaderCultureNameIsoThreeLetter
			// 
			this.ColumnHeaderCultureNameIsoThreeLetter.Text = "ISO 3";
			this.ColumnHeaderCultureNameIsoThreeLetter.Width = 46;
			// 
			// ColumnHeaderCultureNameWindowsThreeLetter
			// 
			this.ColumnHeaderCultureNameWindowsThreeLetter.Text = "Win 3";
			this.ColumnHeaderCultureNameWindowsThreeLetter.Width = 349;
			// 
			// ButtonOk
			// 
			this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOk.Location = new System.Drawing.Point(544, 337);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(100, 32);
			this.ButtonOk.TabIndex = 1;
			this.ButtonOk.Text = "&Ok";
			this.ButtonOk.UseVisualStyleBackColor = true;
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(650, 337);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(100, 32);
			this.ButtonCancel.TabIndex = 2;
			this.ButtonCancel.Text = "&Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// FormCultureInfos
			// 
			this.AcceptButton = this.ButtonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(762, 381);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOk);
			this.Controls.Add(this.ListViewCultures);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FormCultureInfos";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Cultures";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCultureInfos_FormClosing);
			this.Load += new System.EventHandler(this.FormCultureInfos_Load);
			this.Shown += new System.EventHandler(this.FormCultureInfos_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView ListViewCultures;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureName;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureLocaleId;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureNameNative;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureNameDisplay;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureNameEnglish;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureNameIsoTwoLetter;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureNameIsoThreeLetter;
		private System.Windows.Forms.ColumnHeader ColumnHeaderCultureNameWindowsThreeLetter;
		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Button ButtonCancel;
	}
}