namespace HouseOfSynergy.PowerTools.Library.Database
{
	partial class DatabaseConnectionWizard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseConnectionWizard));
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.GroupBoxDatabase = new System.Windows.Forms.GroupBox();
			this.ComboBoxDatabase = new System.Windows.Forms.ComboBox();
			this.ButtonDatabaseRefresh = new System.Windows.Forms.Button();
			this.ButtonDatabaseAdvanced = new System.Windows.Forms.Button();
			this.TextBoxDatabaseFileLogicalName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ButtonDatabaseFileBrowse = new System.Windows.Forms.Button();
			this.TextBoxDatabaseFile = new System.Windows.Forms.TextBox();
			this.RadioButtonDatabaseFile = new System.Windows.Forms.RadioButton();
			this.RadioButtonDatabaseName = new System.Windows.Forms.RadioButton();
			this.GroupBoxAuthentication = new System.Windows.Forms.GroupBox();
			this.CheckBoxSqlServerPasswordSave = new System.Windows.Forms.CheckBox();
			this.RadioButtonAuthenticationSqlServer = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.RadioButtonAuthenticationWindows = new System.Windows.Forms.RadioButton();
			this.TextBoxSqlServerPassword = new System.Windows.Forms.TextBox();
			this.TextBoxSqlServerUsername = new System.Windows.Forms.TextBox();
			this.GroupBoxServer = new System.Windows.Forms.GroupBox();
			this.ComboBoxServer = new System.Windows.Forms.ComboBox();
			this.ButtonServerRefresh = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.ButtonDataSource = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.ButtonConnectionTest = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.GroupBoxDatabase.SuspendLayout();
			this.GroupBoxAuthentication.SuspendLayout();
			this.GroupBoxServer.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonOk
			// 
			this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOk.Enabled = false;
			this.ButtonOk.Location = new System.Drawing.Point(279, 615);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(75, 23);
			this.ButtonOk.TabIndex = 2;
			this.ButtonOk.Text = "&Ok";
			this.ButtonOk.UseVisualStyleBackColor = true;
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(360, 615);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 3;
			this.ButtonCancel.Text = "&Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.GroupBoxDatabase);
			this.panel1.Controls.Add(this.GroupBoxAuthentication);
			this.panel1.Controls.Add(this.GroupBoxServer);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(423, 597);
			this.panel1.TabIndex = 0;
			// 
			// GroupBoxDatabase
			// 
			this.GroupBoxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.GroupBoxDatabase.Controls.Add(this.ComboBoxDatabase);
			this.GroupBoxDatabase.Controls.Add(this.ButtonDatabaseRefresh);
			this.GroupBoxDatabase.Controls.Add(this.ButtonDatabaseAdvanced);
			this.GroupBoxDatabase.Controls.Add(this.TextBoxDatabaseFileLogicalName);
			this.GroupBoxDatabase.Controls.Add(this.label6);
			this.GroupBoxDatabase.Controls.Add(this.ButtonDatabaseFileBrowse);
			this.GroupBoxDatabase.Controls.Add(this.TextBoxDatabaseFile);
			this.GroupBoxDatabase.Controls.Add(this.RadioButtonDatabaseFile);
			this.GroupBoxDatabase.Controls.Add(this.RadioButtonDatabaseName);
			this.GroupBoxDatabase.Enabled = false;
			this.GroupBoxDatabase.Location = new System.Drawing.Point(3, 366);
			this.GroupBoxDatabase.Name = "GroupBoxDatabase";
			this.GroupBoxDatabase.Size = new System.Drawing.Size(417, 226);
			this.GroupBoxDatabase.TabIndex = 3;
			this.GroupBoxDatabase.TabStop = false;
			this.GroupBoxDatabase.Text = "Connect to a Database";
			// 
			// ComboBoxDatabase
			// 
			this.ComboBoxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBoxDatabase.FormattingEnabled = true;
			this.ComboBoxDatabase.Location = new System.Drawing.Point(6, 52);
			this.ComboBoxDatabase.Name = "ComboBoxDatabase";
			this.ComboBoxDatabase.Size = new System.Drawing.Size(324, 23);
			this.ComboBoxDatabase.TabIndex = 1;
			this.ComboBoxDatabase.DropDown += new System.EventHandler(this.ComboBoxDatabase_DropDown);
			this.ComboBoxDatabase.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDatabase_SelectedIndexChanged);
			this.ComboBoxDatabase.TextChanged += new System.EventHandler(this.ComboBoxDatabase_TextChanged);
			// 
			// ButtonDatabaseRefresh
			// 
			this.ButtonDatabaseRefresh.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonDatabaseRefresh.Location = new System.Drawing.Point(336, 51);
			this.ButtonDatabaseRefresh.Name = "ButtonDatabaseRefresh";
			this.ButtonDatabaseRefresh.Size = new System.Drawing.Size(75, 23);
			this.ButtonDatabaseRefresh.TabIndex = 2;
			this.ButtonDatabaseRefresh.Text = "Re&fresh";
			this.ButtonDatabaseRefresh.UseVisualStyleBackColor = true;
			this.ButtonDatabaseRefresh.Click += new System.EventHandler(this.ButtonDatabaseRefresh_Click);
			// 
			// ButtonDatabaseAdvanced
			// 
			this.ButtonDatabaseAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonDatabaseAdvanced.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonDatabaseAdvanced.Enabled = false;
			this.ButtonDatabaseAdvanced.Location = new System.Drawing.Point(323, 193);
			this.ButtonDatabaseAdvanced.Name = "ButtonDatabaseAdvanced";
			this.ButtonDatabaseAdvanced.Size = new System.Drawing.Size(88, 23);
			this.ButtonDatabaseAdvanced.TabIndex = 8;
			this.ButtonDatabaseAdvanced.Text = "Ad&vanced...";
			this.ButtonDatabaseAdvanced.UseVisualStyleBackColor = true;
			// 
			// TextBoxDatabaseFileLogicalName
			// 
			this.TextBoxDatabaseFileLogicalName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxDatabaseFileLogicalName.Enabled = false;
			this.TextBoxDatabaseFileLogicalName.Location = new System.Drawing.Point(6, 164);
			this.TextBoxDatabaseFileLogicalName.Name = "TextBoxDatabaseFileLogicalName";
			this.TextBoxDatabaseFileLogicalName.Size = new System.Drawing.Size(405, 23);
			this.TextBoxDatabaseFileLogicalName.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Enabled = false;
			this.label6.Location = new System.Drawing.Point(6, 138);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(405, 23);
			this.label6.TabIndex = 6;
			this.label6.Text = "&Logical name:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ButtonDatabaseFileBrowse
			// 
			this.ButtonDatabaseFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonDatabaseFileBrowse.Enabled = false;
			this.ButtonDatabaseFileBrowse.Location = new System.Drawing.Point(336, 108);
			this.ButtonDatabaseFileBrowse.Name = "ButtonDatabaseFileBrowse";
			this.ButtonDatabaseFileBrowse.Size = new System.Drawing.Size(75, 23);
			this.ButtonDatabaseFileBrowse.TabIndex = 5;
			this.ButtonDatabaseFileBrowse.Text = "&Browse";
			this.ButtonDatabaseFileBrowse.UseVisualStyleBackColor = true;
			// 
			// TextBoxDatabaseFile
			// 
			this.TextBoxDatabaseFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxDatabaseFile.Enabled = false;
			this.TextBoxDatabaseFile.Location = new System.Drawing.Point(6, 109);
			this.TextBoxDatabaseFile.Name = "TextBoxDatabaseFile";
			this.TextBoxDatabaseFile.Size = new System.Drawing.Size(324, 23);
			this.TextBoxDatabaseFile.TabIndex = 4;
			// 
			// RadioButtonDatabaseFile
			// 
			this.RadioButtonDatabaseFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.RadioButtonDatabaseFile.Enabled = false;
			this.RadioButtonDatabaseFile.Location = new System.Drawing.Point(6, 80);
			this.RadioButtonDatabaseFile.Name = "RadioButtonDatabaseFile";
			this.RadioButtonDatabaseFile.Size = new System.Drawing.Size(405, 23);
			this.RadioButtonDatabaseFile.TabIndex = 3;
			this.RadioButtonDatabaseFile.Text = "Attac&h a database file:";
			this.RadioButtonDatabaseFile.UseVisualStyleBackColor = true;
			// 
			// RadioButtonDatabaseName
			// 
			this.RadioButtonDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.RadioButtonDatabaseName.Checked = true;
			this.RadioButtonDatabaseName.Location = new System.Drawing.Point(6, 22);
			this.RadioButtonDatabaseName.Name = "RadioButtonDatabaseName";
			this.RadioButtonDatabaseName.Size = new System.Drawing.Size(405, 23);
			this.RadioButtonDatabaseName.TabIndex = 0;
			this.RadioButtonDatabaseName.TabStop = true;
			this.RadioButtonDatabaseName.Text = "Select or enter a &database name:";
			this.RadioButtonDatabaseName.UseVisualStyleBackColor = true;
			// 
			// GroupBoxAuthentication
			// 
			this.GroupBoxAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.GroupBoxAuthentication.Controls.Add(this.CheckBoxSqlServerPasswordSave);
			this.GroupBoxAuthentication.Controls.Add(this.RadioButtonAuthenticationSqlServer);
			this.GroupBoxAuthentication.Controls.Add(this.label5);
			this.GroupBoxAuthentication.Controls.Add(this.label4);
			this.GroupBoxAuthentication.Controls.Add(this.RadioButtonAuthenticationWindows);
			this.GroupBoxAuthentication.Controls.Add(this.TextBoxSqlServerPassword);
			this.GroupBoxAuthentication.Controls.Add(this.TextBoxSqlServerUsername);
			this.GroupBoxAuthentication.Enabled = false;
			this.GroupBoxAuthentication.Location = new System.Drawing.Point(3, 190);
			this.GroupBoxAuthentication.Name = "GroupBoxAuthentication";
			this.GroupBoxAuthentication.Size = new System.Drawing.Size(417, 170);
			this.GroupBoxAuthentication.TabIndex = 2;
			this.GroupBoxAuthentication.TabStop = false;
			this.GroupBoxAuthentication.Text = "Log on to the Server";
			// 
			// CheckBoxSqlServerPasswordSave
			// 
			this.CheckBoxSqlServerPasswordSave.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.CheckBoxSqlServerPasswordSave.Checked = true;
			this.CheckBoxSqlServerPasswordSave.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CheckBoxSqlServerPasswordSave.Enabled = false;
			this.CheckBoxSqlServerPasswordSave.Location = new System.Drawing.Point(85, 138);
			this.CheckBoxSqlServerPasswordSave.Name = "CheckBoxSqlServerPasswordSave";
			this.CheckBoxSqlServerPasswordSave.Size = new System.Drawing.Size(326, 23);
			this.CheckBoxSqlServerPasswordSave.TabIndex = 6;
			this.CheckBoxSqlServerPasswordSave.Text = "&Save my Password";
			this.CheckBoxSqlServerPasswordSave.UseVisualStyleBackColor = true;
			this.CheckBoxSqlServerPasswordSave.CheckedChanged += new System.EventHandler(this.CheckBoxSqlServerPasswordSave_CheckedChanged);
			// 
			// RadioButtonAuthenticationSqlServer
			// 
			this.RadioButtonAuthenticationSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.RadioButtonAuthenticationSqlServer.Location = new System.Drawing.Point(6, 51);
			this.RadioButtonAuthenticationSqlServer.Name = "RadioButtonAuthenticationSqlServer";
			this.RadioButtonAuthenticationSqlServer.Size = new System.Drawing.Size(405, 23);
			this.RadioButtonAuthenticationSqlServer.TabIndex = 1;
			this.RadioButtonAuthenticationSqlServer.Text = "Use S&QL Server Authentication";
			this.RadioButtonAuthenticationSqlServer.UseVisualStyleBackColor = true;
			this.RadioButtonAuthenticationSqlServer.CheckedChanged += new System.EventHandler(this.RadioButtonAuthenticationSqlServer_CheckedChanged);
			// 
			// label5
			// 
			this.label5.Enabled = false;
			this.label5.Location = new System.Drawing.Point(6, 109);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 23);
			this.label5.TabIndex = 4;
			this.label5.Text = "&Password:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Enabled = false;
			this.label4.Location = new System.Drawing.Point(6, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(73, 23);
			this.label4.TabIndex = 2;
			this.label4.Text = "&Username:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RadioButtonAuthenticationWindows
			// 
			this.RadioButtonAuthenticationWindows.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.RadioButtonAuthenticationWindows.Checked = true;
			this.RadioButtonAuthenticationWindows.Location = new System.Drawing.Point(6, 22);
			this.RadioButtonAuthenticationWindows.Name = "RadioButtonAuthenticationWindows";
			this.RadioButtonAuthenticationWindows.Size = new System.Drawing.Size(405, 23);
			this.RadioButtonAuthenticationWindows.TabIndex = 0;
			this.RadioButtonAuthenticationWindows.TabStop = true;
			this.RadioButtonAuthenticationWindows.Text = "Use &Windows Authentication";
			this.RadioButtonAuthenticationWindows.UseVisualStyleBackColor = true;
			this.RadioButtonAuthenticationWindows.CheckedChanged += new System.EventHandler(this.RadioButtonAuthenticationWindows_CheckedChanged);
			// 
			// TextBoxSqlServerPassword
			// 
			this.TextBoxSqlServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSqlServerPassword.Enabled = false;
			this.TextBoxSqlServerPassword.Location = new System.Drawing.Point(85, 109);
			this.TextBoxSqlServerPassword.Name = "TextBoxSqlServerPassword";
			this.TextBoxSqlServerPassword.Size = new System.Drawing.Size(326, 23);
			this.TextBoxSqlServerPassword.TabIndex = 5;
			this.TextBoxSqlServerPassword.TextChanged += new System.EventHandler(this.TextBoxSqlServerPassword_TextChanged);
			this.TextBoxSqlServerPassword.Enter += new System.EventHandler(this.TextBoxSqlServerPassword_Enter);
			// 
			// TextBoxSqlServerUsername
			// 
			this.TextBoxSqlServerUsername.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSqlServerUsername.Enabled = false;
			this.TextBoxSqlServerUsername.Location = new System.Drawing.Point(85, 80);
			this.TextBoxSqlServerUsername.Name = "TextBoxSqlServerUsername";
			this.TextBoxSqlServerUsername.Size = new System.Drawing.Size(326, 23);
			this.TextBoxSqlServerUsername.TabIndex = 3;
			this.TextBoxSqlServerUsername.TextChanged += new System.EventHandler(this.TextBoxSqlServerUsername_TextChanged);
			this.TextBoxSqlServerUsername.Enter += new System.EventHandler(this.TextBoxSqlServerUsername_Enter);
			// 
			// GroupBoxServer
			// 
			this.GroupBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.GroupBoxServer.Controls.Add(this.ComboBoxServer);
			this.GroupBoxServer.Controls.Add(this.ButtonServerRefresh);
			this.GroupBoxServer.Controls.Add(this.label2);
			this.GroupBoxServer.Controls.Add(this.ButtonDataSource);
			this.GroupBoxServer.Controls.Add(this.label3);
			this.GroupBoxServer.Controls.Add(this.textBox1);
			this.GroupBoxServer.Location = new System.Drawing.Point(3, 55);
			this.GroupBoxServer.Name = "GroupBoxServer";
			this.GroupBoxServer.Size = new System.Drawing.Size(417, 129);
			this.GroupBoxServer.TabIndex = 1;
			this.GroupBoxServer.TabStop = false;
			this.GroupBoxServer.Text = "Source";
			// 
			// ComboBoxServer
			// 
			this.ComboBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBoxServer.FormattingEnabled = true;
			this.ComboBoxServer.Location = new System.Drawing.Point(6, 96);
			this.ComboBoxServer.Name = "ComboBoxServer";
			this.ComboBoxServer.Size = new System.Drawing.Size(324, 23);
			this.ComboBoxServer.TabIndex = 4;
			this.ComboBoxServer.DropDown += new System.EventHandler(this.ComboBoxServer_DropDown);
			this.ComboBoxServer.SelectedIndexChanged += new System.EventHandler(this.ComboBoxServer_SelectedIndexChanged);
			this.ComboBoxServer.TextChanged += new System.EventHandler(this.ComboBoxServer_TextChanged);
			// 
			// ButtonServerRefresh
			// 
			this.ButtonServerRefresh.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonServerRefresh.Location = new System.Drawing.Point(336, 96);
			this.ButtonServerRefresh.Name = "ButtonServerRefresh";
			this.ButtonServerRefresh.Size = new System.Drawing.Size(75, 23);
			this.ButtonServerRefresh.TabIndex = 5;
			this.ButtonServerRefresh.Text = "&Refresh";
			this.ButtonServerRefresh.UseVisualStyleBackColor = true;
			this.ButtonServerRefresh.Click += new System.EventHandler(this.ButtonServer_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(6, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(405, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Data &Source";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ButtonDataSource
			// 
			this.ButtonDataSource.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonDataSource.Enabled = false;
			this.ButtonDataSource.Location = new System.Drawing.Point(336, 44);
			this.ButtonDataSource.Name = "ButtonDataSource";
			this.ButtonDataSource.Size = new System.Drawing.Size(75, 23);
			this.ButtonDataSource.TabIndex = 2;
			this.ButtonDataSource.Text = "&Change";
			this.ButtonDataSource.UseVisualStyleBackColor = true;
			this.ButtonDataSource.Click += new System.EventHandler(this.ButtonDataSource_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(6, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(405, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "S&erver Name";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(6, 45);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(324, 23);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "Microsoft SQL Server (SqlClient)";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.label1);
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(417, 46);
			this.panel2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(417, 46);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter information to connect to the selected data source\r\n(Microsoft .NET Framewo" +
	"rk Data Provider for Microsoft SQL Server).";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ButtonConnectionTest
			// 
			this.ButtonConnectionTest.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ButtonConnectionTest.Location = new System.Drawing.Point(12, 615);
			this.ButtonConnectionTest.Name = "ButtonConnectionTest";
			this.ButtonConnectionTest.Size = new System.Drawing.Size(116, 23);
			this.ButtonConnectionTest.TabIndex = 1;
			this.ButtonConnectionTest.Text = "&Test Connection";
			this.ButtonConnectionTest.UseVisualStyleBackColor = true;
			this.ButtonConnectionTest.Click += new System.EventHandler(this.ButtonConnectionTest_Click);
			// 
			// Database
			// 
			this.AcceptButton = this.ButtonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(447, 650);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonConnectionTest);
			this.Controls.Add(this.ButtonOk);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "Database";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Wizard";
			this.Activated += new System.EventHandler(this.Camera_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Camera_FormClosing);
			this.Load += new System.EventHandler(this.Camera_Load);
			this.Shown += new System.EventHandler(this.Camera_Shown);
			this.panel1.ResumeLayout(false);
			this.GroupBoxDatabase.ResumeLayout(false);
			this.GroupBoxDatabase.PerformLayout();
			this.GroupBoxAuthentication.ResumeLayout(false);
			this.GroupBoxAuthentication.PerformLayout();
			this.GroupBoxServer.ResumeLayout(false);
			this.GroupBoxServer.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox GroupBoxServer;
		private System.Windows.Forms.Button ButtonServerRefresh;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button ButtonDataSource;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox GroupBoxDatabase;
		private System.Windows.Forms.GroupBox GroupBoxAuthentication;
		private System.Windows.Forms.CheckBox CheckBoxSqlServerPasswordSave;
		private System.Windows.Forms.RadioButton RadioButtonAuthenticationSqlServer;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton RadioButtonAuthenticationWindows;
		private System.Windows.Forms.TextBox TextBoxSqlServerPassword;
		private System.Windows.Forms.TextBox TextBoxSqlServerUsername;
		private System.Windows.Forms.Button ButtonDatabaseAdvanced;
		private System.Windows.Forms.TextBox TextBoxDatabaseFileLogicalName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button ButtonDatabaseFileBrowse;
		private System.Windows.Forms.TextBox TextBoxDatabaseFile;
		private System.Windows.Forms.RadioButton RadioButtonDatabaseFile;
		private System.Windows.Forms.RadioButton RadioButtonDatabaseName;
		private System.Windows.Forms.Button ButtonConnectionTest;
		private System.Windows.Forms.Button ButtonDatabaseRefresh;
		private System.Windows.Forms.ComboBox ComboBoxDatabase;
		private System.Windows.Forms.ComboBox ComboBoxServer;
	}
}