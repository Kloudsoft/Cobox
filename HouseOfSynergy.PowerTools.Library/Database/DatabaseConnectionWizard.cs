using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Database
{
	public partial class DatabaseConnectionWizard:
		System.Windows.Forms.Form
	{
		#region Members.

		//====================================================================================================
		// Constants.
		//====================================================================================================

		private bool ConnectionVerified { get; set; }

		private System.Data.SqlClient.SqlConnectionStringBuilder _SqlConnectionStringBuilder { get; set; }

		#endregion Members.

		#region Constructors, Destructor & Initializers.

		//====================================================================================================
		// Constructors, Destructor & Initializers.
		//====================================================================================================

		public DatabaseConnectionWizard (System.Data.SqlClient.SqlConnectionStringBuilder builder = null)
		{
			this.InitializeComponent();

			this.ConnectionVerified = false;
			this._SqlConnectionStringBuilder = builder ?? new System.Data.SqlClient.SqlConnectionStringBuilder();
		}

		#endregion Constructors, Destructor & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public System.Data.SqlClient.SqlConnectionStringBuilder SqlConnectionStringBuilder { get { return (new System.Data.SqlClient.SqlConnectionStringBuilder(this._SqlConnectionStringBuilder.ConnectionString)); } }

		#endregion Properties.

		#region Form Events.

		//====================================================================================================
		// Form Events.
		//====================================================================================================

		private void Camera_Load (object sender, EventArgs e)
		{
		}

		private void Camera_Shown (object sender, EventArgs e)
		{
			this.RefreshData();

			this.ComboBoxServer.SelectAll();
		}

		private void Camera_Activated (object sender, EventArgs e)
		{
		}

		private void Camera_FormClosing (object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
		}

		#endregion Form Events.

		#region Form Control Events.

		//====================================================================================================
		// Form Control Events.
		//====================================================================================================

		private void ButtonDataSource_Click (object sender, EventArgs e)
		{
		}

		private void ComboBoxServer_TextChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;
			this.ComboBoxDatabase.DataSource = null;

			this.RefreshControls();
		}

		private void ComboBoxServer_SelectedIndexChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;
			this.ComboBoxDatabase.DataSource = null;

			this.RefreshControls();
		}

		private void ComboBoxServer_DropDown (object sender, EventArgs e)
		{
			if (this.ComboBoxServer.DataSource == null)
			{
				this.ButtonServerRefresh.PerformClick();
			}
		}

		private void ButtonServer_Click (object sender, EventArgs e)
		{
			string message = "";
			System.Data.DataTable table = null;
			List<string> servers = null;

			this.Enabled = false;
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(0.2D));

			try
			{
				table = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();

				servers = new List<string>();
				for (int i = 0; i < table.Rows.Count; i++)
				{
					servers.Add((table.Rows [i] ["InstanceName"].ToString().Length == 0) ? (table.Rows [i] ["ServerName"].ToString()) : (table.Rows [i] ["ServerName"].ToString() + @"\" + table.Rows [i] ["InstanceName"].ToString()));
				}

				this.ComboBoxServer.DataSource = servers;
				this.ComboBoxServer.SelectedIndex = -1;
				this.ComboBoxServer.Refresh();

				this.Cursor = System.Windows.Forms.Cursors.Default;
				this.Enabled = true;

				this.ComboBoxServer.Focus();
				this.ComboBoxServer.DroppedDown = true;
			}
			catch (Exception exception)
			{
				message
					= "Servers could not be retrieved."
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ exception.Message
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ "Please try again or enter one manually."
					;

				System.Windows.Forms.MessageBox.Show
				(
					this,
					message,
					System.Windows.Forms.Application.ProductName,
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Error,
					System.Windows.Forms.MessageBoxDefaultButton.Button1
				);
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Enabled = true;
		}

		private void RadioButtonAuthenticationWindows_CheckedChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;
			this.RefreshControls();
		}

		private void RadioButtonAuthenticationSqlServer_CheckedChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;
			this.RefreshControls();
		}

		private void TextBoxSqlServerUsername_Enter (object sender, EventArgs e)
		{
			((System.Windows.Forms.TextBox) sender).SelectAll();
		}

		private void TextBoxSqlServerUsername_TextChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;

			this.RefreshControls();
		}

		private void TextBoxSqlServerPassword_Enter (object sender, EventArgs e)
		{
			((System.Windows.Forms.TextBox) sender).SelectAll();
		}

		private void TextBoxSqlServerPassword_TextChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;

			this.RefreshControls();
		}

		private void CheckBoxSqlServerPasswordSave_CheckedChanged (object sender, EventArgs e)
		{
			this.RefreshControls();
		}

		private void ComboBoxDatabase_SelectedIndexChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;

			this.RefreshControls();
		}

		private void ComboBoxDatabase_TextChanged (object sender, EventArgs e)
		{
			this.ConnectionVerified = false;

			this.RefreshControls();
		}

		private void ComboBoxDatabase_DropDown (object sender, EventArgs e)
		{
			if (this.ComboBoxDatabase.DataSource == null)
			{
				this.ButtonDatabaseRefresh.PerformClick();
			}
		}

		private void ButtonDatabaseRefresh_Click (object sender, EventArgs e)
		{
			string message = "";
			System.Data.DataTable table = null;
			List<string> databases = null;
			System.Data.SqlClient.SqlConnectionStringBuilder builder = null;

			this.Enabled = false;
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(0.2D));

			try
			{
				builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
				builder.DataSource = this.ComboBoxServer.Text;
				builder.IntegratedSecurity = this.RadioButtonAuthenticationWindows.Checked;
				if (this.RadioButtonAuthenticationSqlServer.Checked)
				{
					builder.UserID = this.TextBoxSqlServerUsername.Text;
					builder.Password = this.TextBoxSqlServerPassword.Text;
				}

				using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(builder.ConnectionString))
				{
					connection.Open();

					table = connection.GetSchema("Databases");
				}

				databases = new List<string>();
				for (int i = 0; i < table.Rows.Count; i++)
				{
					databases.Add(table.Rows [i] ["database_name"].ToString());
				}

				this.ComboBoxDatabase.DataSource = databases;
				this.ComboBoxDatabase.SelectedIndex = -1;
				this.ComboBoxDatabase.Refresh();
			}
			catch (Exception exception)
			{
				message
					= "Database ccatalog could not be retrieved."
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ exception.Message
					;

				System.Windows.Forms.MessageBox.Show
				(
					this,
					message,
					System.Windows.Forms.Application.ProductName,
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Error,
					System.Windows.Forms.MessageBoxDefaultButton.Button1
				);
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Enabled = true;

			this.RefreshControls();
		}

		private void ButtonConnectionTest_Click (object sender, EventArgs e)
		{
			string message = "";
			System.Data.SqlClient.SqlConnectionStringBuilder builder = null;

			this.Enabled = false;
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(0.2D));

			try
			{
				builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
				builder.DataSource = this.ComboBoxServer.Text;
				builder.InitialCatalog = this.ComboBoxDatabase.Text;
				builder.IntegratedSecurity = this.RadioButtonAuthenticationWindows.Checked;
				if (this.RadioButtonAuthenticationSqlServer.Checked)
				{
					builder.UserID = this.TextBoxSqlServerUsername.Text;
					builder.Password = this.TextBoxSqlServerPassword.Text;
				}

				using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(builder.ConnectionString))
				{
					connection.Open();
				}

				this.ConnectionVerified = true;

				message = "Test connection succeeded.";

				System.Windows.Forms.MessageBox.Show
				(
					this,
					message,
					System.Windows.Forms.Application.ProductName,
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Information,
					System.Windows.Forms.MessageBoxDefaultButton.Button1
				);
			}
			catch (Exception exception)
			{
				message
					= "A connection could not be established."
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ exception.Message
					;

				System.Windows.Forms.MessageBox.Show
				(
					this,
					message,
					System.Windows.Forms.Application.ProductName,
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Error,
					System.Windows.Forms.MessageBoxDefaultButton.Button1
				);
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Enabled = true;

			this.RefreshControls();
		}

		private void ButtonOk_Click (object sender, EventArgs e)
		{
			string message = "";

			try
			{
				this._SqlConnectionStringBuilder.DataSource = this.ComboBoxServer.Text;
				this._SqlConnectionStringBuilder.InitialCatalog = this.ComboBoxDatabase.Text;
				this._SqlConnectionStringBuilder.IntegratedSecurity = this.RadioButtonAuthenticationWindows.Checked;
				this._SqlConnectionStringBuilder.MultipleActiveResultSets = true;
				if (this.RadioButtonAuthenticationSqlServer.Checked)
				{
					this._SqlConnectionStringBuilder.UserID = this.TextBoxSqlServerUsername.Text;
					this._SqlConnectionStringBuilder.Password = this.TextBoxSqlServerPassword.Text;
				}

				this.DialogResult = System.Windows.Forms.DialogResult.OK;

				this.Close();
			}
			catch (Exception exception)
			{
				message
					= "An error occurred:"
					+ System.Environment.NewLine
					+ System.Environment.NewLine
					+ exception.Message
					;

				System.Windows.Forms.MessageBox.Show
				(
					this,
					message,
					System.Windows.Forms.Application.ProductName,
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Error,
					System.Windows.Forms.MessageBoxDefaultButton.Button1
				);
			}

			this.RefreshControls();
		}

		private void ButtonCancel_Click (object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

			this.Close();
		}

		#endregion Form Control Events.

		#region Form Component Events.

		//====================================================================================================
		// Form Component Events.
		//====================================================================================================

		#endregion Form Component Events.

		#region Functions: RefreshData(), RefreshControls() & IsValid().

		//====================================================================================================
		// Functions: RefreshData(), RefreshControls() & IsValid().
		//====================================================================================================

		private void RefreshData ()
		{
			try
			{
				this.ComboBoxServer.Text = this._SqlConnectionStringBuilder.DataSource;

				if (this._SqlConnectionStringBuilder.IntegratedSecurity)
				{
					this.RadioButtonAuthenticationWindows.Checked = true;
				}
				else
				{
					this.RadioButtonAuthenticationSqlServer.Checked = true;
					this.TextBoxSqlServerUsername.Text = this._SqlConnectionStringBuilder.UserID;
					this.TextBoxSqlServerPassword.Text = this._SqlConnectionStringBuilder.Password;
				}

				this.ComboBoxDatabase.Text = this._SqlConnectionStringBuilder.InitialCatalog;
			}
			catch
			{
				this.ComboBoxServer.DataSource = null;
				this.ComboBoxServer.Text = "";
				this.TextBoxSqlServerUsername.Text = "";
				this.TextBoxSqlServerPassword.Text = "";
				this.ComboBoxDatabase.DataSource = null;
				this.ComboBoxDatabase.Text = "";
			}

			this.RefreshControls();
		}

		private void RefreshControls ()
		{
			this.GroupBoxAuthentication.Enabled =
			(
				(this.ComboBoxServer.SelectedIndex >= 0)
				||
				(this.ComboBoxServer.Text.Length > 0)
			);

			this.GroupBoxDatabase.Enabled =
			(
				this.GroupBoxAuthentication.Enabled
				&&
				(
					this.RadioButtonAuthenticationWindows.Checked
					||
					(
						this.RadioButtonAuthenticationSqlServer.Checked
						&&
						(this.TextBoxSqlServerUsername.Text.Trim().Length > 0)
					)
				)
			);

			this.TextBoxSqlServerUsername.Enabled = this.RadioButtonAuthenticationSqlServer.Checked;
			this.TextBoxSqlServerPassword.Enabled = this.RadioButtonAuthenticationSqlServer.Checked;

			this.ButtonConnectionTest.Enabled =
			(
				this.GroupBoxDatabase.Enabled
				&&
				(
					(this.ComboBoxDatabase.SelectedIndex >= 0)
					||
					(this.ComboBoxDatabase.Text.Trim().Length > 0)
				)
			);

			this.ButtonOk.Enabled = this.ConnectionVerified || this.ButtonConnectionTest.Enabled;
		}

		private bool IsValid (bool warn = false)
		{
			return (false);
		}

		#endregion Functions: RefreshData(), RefreshControls() & IsValid().
	}
}