using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.AffinityDms.ConsoleClient.Classes;
using System.Globalization;
using HouseOfSynergy.AffinityDms.Library;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;
using HouseOfSynergy.AffinityDms.ResourceProvider.Classes;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Forms
{
	public partial class FormResourceEditior:
		Form
	{
		private ResourceContainer ResourceContainer = null;

		public FormResourceEditior ()
		{
			this.InitializeComponent();

			this.ResourceContainer = new ResourceContainer();
		}

		private void FormResourceEditor_Load (object sender, EventArgs e)
		{
		}

		private void FormResourceEditor_Shown (object sender, EventArgs e)
		{
			this.RefreshData();

			this.ListViewCultures.Select();
			if (this.ListViewCultures.Items.Count > 0) { this.ListViewCultures.Items [this.ResourceContainer.Cultures.ToList().FindIndex(c => c.Name == Thread.CurrentThread.CurrentCulture.Name)].Selected = true; }
		}

		private void FormResourceEditor_FormClosing (object sender, FormClosingEventArgs e)
		{
		}

		private void ToolStripButtonNew_Click (object sender, EventArgs e)
		{
			var message
				= "You are about to create a new resource."
				+ Environment.NewLine
				+ Environment.NewLine
				+ "All existing changes will be discarded."
				+ Environment.NewLine
				+ Environment.NewLine
				+ "Are you sure you want to continue?";

			var result = MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (result == DialogResult.Yes)
			{
				this.ResourceContainer = new ResourceContainer();

				this.RefreshData();
			}
		}

		private void ToolStripButtonImport_Click (object sender, EventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog())
			{
				openFileDialog.AddExtension = false;
				openFileDialog.CheckFileExists = true;
				openFileDialog.CheckPathExists = true;
				openFileDialog.DefaultExt = "";
				openFileDialog.DereferenceLinks = true;
				openFileDialog.Filter = "Xml Files (*.xml)|*.xml";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;
				openFileDialog.RestoreDirectory = false;
				openFileDialog.SupportMultiDottedExtensions = true;
				openFileDialog.Title = "Select an Xml Resource file";
				openFileDialog.ValidateNames = true;

				if (openFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					try
					{
						Exception exception = null;

						if (!this.ResourceContainer.Load(openFileDialog.FileName, out exception)) { throw (exception); }

						this.RefreshData();

						if (this.ListViewCultures.Items.Count > 0)
						{
							if (this.ListViewCultures.SelectedItems.Count == 0)
							{
								for (int i = 0; i < this.ListViewCultures.Items.Count; i++)
								{
									if ((this.ListViewCultures.Items [i].Tag as Culture).Name == Thread.CurrentThread.CurrentCulture.Name)
									{
										this.ListViewCultures.Items [i].Selected = true;
									}
								}
							}

							if (this.ListViewCultures.SelectedItems.Count == 0)
							{
								this.ListViewCultures.Items [0].Selected = true;
							}
						}

						if (this.ListViewTranslations.Items.Count > 0)
						{
							if (this.ListViewTranslations.SelectedItems.Count == 0)
							{
								this.ListViewTranslations.Items [0].Selected = true;
							}
						}
					}
					catch (Exception exception)
					{
						MessageBox.Show(this, exception.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					}
				}
			}
		}

		private void ToolStripButtonExport_Click (object sender, EventArgs e)
		{
			using (var saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.AddExtension = true;
				saveFileDialog.CheckFileExists = false;
				saveFileDialog.CheckPathExists = true;
				saveFileDialog.CreatePrompt = false;
				saveFileDialog.DefaultExt = ".xml";
				saveFileDialog.FileName = "Resources.xml";
				saveFileDialog.Filter = "Xml Files (*.xml)|*.xml";
				saveFileDialog.FilterIndex = 1;
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.RestoreDirectory = false;
				saveFileDialog.SupportMultiDottedExtensions = true;
				saveFileDialog.Title = "Save as";
				saveFileDialog.ValidateNames = true;

				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					try
					{
						Exception exception = null;

						if (!this.ResourceContainer.Save(saveFileDialog.FileName, out exception)) { throw (exception); }
					}
					catch (Exception exception)
					{
						MessageBox.Show(this, exception.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					}
				}
			}
		}

		private void ToolStripButtonCulturesAdd_Click (object sender, EventArgs e)
		{
			var found = false;

			using (var form = new FormCultureInfos())
			{
				if (form.ShowDialog(this) == DialogResult.OK)
				{
					for (int i = 0; i < this.ListViewCultures.Items.Count; i++)
					{
						var cultureInfo = this.ListViewCultures.Items [i].Tag as Culture;

						if (cultureInfo.LocaleId == form.SelectedCultureInfo.LCID)
						{
							found = true;

							break;
						}
					}

					if (found)
					{
						MessageBox.Show(this, "The selected culture already exists.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					}
					else
					{
						this.ResourceContainer.Add(Culture.FromCultureInfo(form.SelectedCultureInfo));

						this.RefreshDataCultures(true);
						this.RefreshDataCultureTranslations(true);

						for (int i = 0; i < this.ListViewCultures.Items.Count; i++)
						{
							var cultureInfo = this.ListViewCultures.Items [i].Tag as Culture;

							if (cultureInfo.LocaleId == form.SelectedCultureInfo.LCID)
							{
								this.ListViewCultures.Items [i].Selected = true;
								this.ListViewCultures.Items [i].EnsureVisible();

								break;
							}
						}
					}
				}
			}
		}

		private void ToolStripButtonCulturesDelete_Click (object sender, EventArgs e)
		{
			if (this.ListViewCultures.SelectedItems.Count > 0)
			{
				var message
					= "You are about to delete the ("
					+ this.ListViewCultures.SelectedItems.Count.ToString()
					+ ") cultures."
					+ Environment.NewLine
					+ Environment.NewLine
					+ "This action will delete all related translation entries and cannot be undone."
					+ Environment.NewLine
					+ Environment.NewLine
					+ "Are you sure you want to continue?";

				var result = MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

				for (int i = 0; i < this.ListViewCultures.SelectedItems.Count; i++)
				{
					var culture = this.ListViewCultures.SelectedItems [i].Tag as Culture;

					if (result == DialogResult.Yes)
					{
						var item = this.ResourceContainer.Cultures.SingleOrDefault(c => c.Id == culture.Id);

						if (item == null)
						{
							MessageBox.Show(this, "The requested culture was not found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						}
						else
						{
							this.ResourceContainer.Remove(item);
						}
					}
				}

				this.RefreshData();
			}
		}

		private void ListViewCultures_SelectedIndexChanged (object sender, EventArgs e)
		{
			this.RefreshDataCultureTranslations();

			this.RefreshControls();
		}

		private void ListViewCultures_ColumnClick (object sender, ColumnClickEventArgs e)
		{
			this.ListViewCultures.ListViewItemSorter
				= this.ListViewCultures.ListViewItemSorter
				?? new CultureComparer();

			var comparer = this.ListViewCultures.ListViewItemSorter as CultureComparer;

			if (e.Column == comparer.ColumnIndex)
			{
				if (comparer.Order == CultureComparer.CultureSortOrder.None)
				{
					comparer.Order = CultureComparer.CultureSortOrder.Ascending;
				}
				else if (comparer.Order == CultureComparer.CultureSortOrder.Ascending)
				{
					comparer.Order = CultureComparer.CultureSortOrder.Descending;
				}
				else if (comparer.Order == CultureComparer.CultureSortOrder.Descending)
				{
					comparer.Order = CultureComparer.CultureSortOrder.Ascending;
				}
			}
			else
			{
				comparer.Order = CultureComparer.CultureSortOrder.Ascending;
				comparer.Member = (CultureComparer.CultureSortMember) e.Column;
			}

			this.ListViewCultures.Sort();
		}

		private void ToolStripButtonTranslationsAdd_Click (object sender, EventArgs e)
		{
			MessageBox.Show(this, "This feature is not implemented yet.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
		}

		private void ToolStripButtonTranslationsEdit_Click (object sender, EventArgs e)
		{
			MessageBox.Show(this, "This feature is not implemented yet.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
		}

		private void ToolStripButtonTranslationsDelete_Click (object sender, EventArgs e)
		{
			if (this.ListViewTranslations.SelectedItems.Count > 0)
			{
				var message
					= "You are about to delete ("
					+ this.ListViewTranslations.SelectedItems.Count.ToString()
					+ ") entries."
					+ Environment.NewLine
					+ Environment.NewLine
					+ "This action will delete all related translation entries from all cultures and cannot be undone."
					+ Environment.NewLine
					+ Environment.NewLine
					+ "Are you sure you want to continue?";

				var result = MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

				if (result == DialogResult.Yes)
				{
					for (int i = 0; i < this.ListViewTranslations.SelectedItems .Count; i++)
					{
						var translation = this.ListViewTranslations.SelectedItems [i].Tag as Translation;
						var item = this.ResourceContainer.Translations.SingleOrDefault(t => t.Id == translation.Id);

						if (item == null)
						{
							MessageBox.Show(this, "The requested translation was not found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						}
						else
						{
							this.ResourceContainer.Remove(item);
						}
					}

					this.RefreshData();
				}
			}
		}

		private void ListViewTranslations_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (this.ListViewTranslations.SelectedItems.Count == 1)
			{
				var translation = this.ListViewTranslations.SelectedItems [0].Tag as Translation;

				this.ButtonTranslationsSave.Tag = translation;
				this.TextBoxTranslationsName.Tag = translation;
				this.TextBoxTranslationsName.Text = translation.Name;

				this.ButtonTranslationsAdd.Enabled = true;
				this.ButtonTranslationsSave.Enabled = true;
			}
			else
			{
				this.TextBoxTranslationsName.Text = "";
				this.ButtonTranslationsSave.Tag = null;
				this.TextBoxTranslationsName.Tag = null;

				this.ButtonTranslationsAdd.Enabled = true;
				this.ButtonTranslationsSave.Enabled = false;
			}

			this.RefreshControls();
		}

		private void ListViewTranslations_DoubleClick (object sender, EventArgs e)
		{
			if (this.ListViewTranslations.SelectedItems.Count == 1)
			{
				this.TextBoxTranslationsName.Select();
				this.TextBoxTranslationsName.SelectAll();
			}
		}

		private void ListViewTranslations_KeyDown (object sender, KeyEventArgs e)
		{
			if (this.ListViewTranslations.SelectedItems.Count == 1)
			{
				if (e.KeyCode == Keys.Enter)
				{
					this.TextBoxTranslationsName.Select();
					this.TextBoxTranslationsName.SelectAll();
				}
			}
		}

		private void ListViewTranslations_KeyPress (object sender, KeyPressEventArgs e)
		{
		}

		private void ListViewTranslations_KeyUp (object sender, KeyEventArgs e)
		{
		}

		private void TextBoxTranslationsName_Enter (object sender, EventArgs e)
		{
			(sender as TextBox).SelectAll();
		}

		private void TextBoxTranslationsName_TextChanged (object sender, EventArgs e)
		{
			this.RefreshControls();
		}

		private void TextBoxTranslationsName_KeyDown (object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.ButtonTranslationsSave.PerformClick();
			}
		}

		private void TextBoxTranslationsName_KeyPress (object sender, KeyPressEventArgs e)
		{
		}

		private void TextBoxTranslationsName_KeyUp (object sender, KeyEventArgs e)
		{
		}

		private void ButtonTranslationsSave_Click (object sender, EventArgs e)
		{
			var button = sender as Button;
			var translation = button.Tag as Translation;

			if (this.ListViewTranslations.SelectedItems.Count == 1)
			{
				if (this.ListViewTranslations.SelectedItems [0].Tag == button.Tag)
				{
					var valid = true;
					var name = this.TextBoxTranslationsName.Text.Trim();

					valid &= ((valid) && (!string.IsNullOrWhiteSpace(name)));
					valid &= ((valid) && (Regex.IsMatch(name, @"^[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz]+[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_]*$")));

					if (valid)
					{
						var translations = this.ResourceContainer.Translations.Where(t => (string.Compare(name, t.Name, StringComparison.OrdinalIgnoreCase) == 0));

						if (translations.Any())
						{
							MessageBox.Show(this, "The specified name is already in use.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						}
						else
						{
							translation.Name = name;

							this.RefreshDataTranslations();
							this.RefreshDataCultureTranslations();

							for (int i = 0; i < this.ListViewTranslations.Items.Count; i++)
							{
								if (this.ListViewTranslations.Items [i].Tag == translation)
								{
									this.ListViewTranslations.Items [i].Selected = true;
									this.ListViewTranslations.Items [i].EnsureVisible();

									break;
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show(this, "Invalid translation entry selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				}
			}
			else
			{
				MessageBox.Show(this, "No translation entry selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
		}

		private void ButtonTranslationsAdd_Click (object sender, EventArgs e)
		{
			var name = this.TextBoxTranslationsName.Text.Trim();
			var translations = this.ResourceContainer.Translations.Where(t => (string.Compare(name, t.Name, StringComparison.OrdinalIgnoreCase) == 0));

			if (translations.Any())
			{
				MessageBox.Show(this, "The specified name is already in use.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
			else
			{
				var translation = new Translation() { Name = name, Comment = "", };
				this.ResourceContainer.Add(translation);

				this.RefreshDataTranslations(true);
				this.RefreshDataCultureTranslations(true);

				for (int i = 0; i < this.ListViewTranslations.Items.Count; i++)
				{
					if (this.ListViewTranslations.Items [i].Tag == translation)
					{
						this.ListViewTranslations.Items [i].Selected = true;
						this.ListViewTranslations.Items [i].EnsureVisible();

						break;
					}
				}
			}
		}

		private void ToolStripComboBoxCultureEntryReference_SelectedIndexChanged (object sender, EventArgs e)
		{
			this.RefreshDataCultureTranslations();
		}

		private void ToolStripButtonReferenceCopy_Click (object sender, EventArgs e)
		{
			var cultureReference = this.ToolStripComboBoxCultureEntryReference.SelectedItem as Culture;

			if (cultureReference != null)
			{
				for (int i = 0; i < this.DataGridView.Rows.Count; i++)
				{
					var cultureTranslation = this.DataGridView.Rows [i].DataBoundItem as CultureTranslation;

					cultureTranslation.Value = cultureTranslation.ValueReference;
				}

				this.DataGridView.Refresh();
			}
		}

		private void ToolStripButtonMicrosoftTranslatorFill_Click (object sender, EventArgs e)
		{
			if ((this.ListViewCultures.SelectedItems.Count == 1) && (this.ResourceContainer.CultureTranslations.Any()) && (this.ToolStripComboBoxCultureEntryReference.SelectedIndex > 0))
			{
				var cultureTarget = (this.ListViewCultures.SelectedItems [0].Tag as Culture);
				var cultureSource = (this.ToolStripComboBoxCultureEntryReference.SelectedItem as Culture);
				var cultureInfoTarget = CultureInfo.GetCultureInfo(cultureTarget.Name);
				var cultureInfoSource = CultureInfo.GetCultureInfo(cultureSource.Name);

				var message
					= "This action will overwrite any changes"
					+ Environment.NewLine
					+ "you have made to the targeted culture."
					+ Environment.NewLine
					+ Environment.NewLine
					+ "Source Culture: "
					+ (cultureSource).ToString()
					+ Environment.NewLine
					+ "Target Culture: "
					+ (cultureTarget).ToString()
					+ Environment.NewLine
					+ Environment.NewLine
					+ "Are you sure you want to continue?";

				var result = MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

				if (result == DialogResult.Yes)
				{
					try
					{
						var header = "";
						Exception exception = null;

						this.Enabled = false;
						this.Cursor = Cursors.WaitCursor;

						if (MicrosoftTranslatorApiHelper.GetAccessToken(out header, out exception))
						{
							var cultureTranslations = this.ResourceContainer.CultureTranslations.Where(ct => ct.Culture.Name == cultureTarget.Name);

							foreach (var cultureTranslation in cultureTranslations)
							{
								var textSource = "";
								var textTranslated = "";

								textSource = cultureTranslation.ValueReference;

								if (MicrosoftTranslatorApiHelper.GetTranslation(header, textSource, cultureInfoSource, cultureInfoTarget, out textTranslated, out exception))
								{
									cultureTranslation.Value = textTranslated;

									this.DataGridView.Refresh();
								}
								else
								{
									throw (exception);
								}
							}
						}
						else
						{
							throw (exception);
						}
					}
					catch (Exception exception)
					{
						MessageBox.Show(this, exception.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					}
					finally
					{
						this.Cursor = Cursors.Default;
						this.Enabled = true;

						this.DataGridView.Refresh();
					}
				}
			}
		}

		private void DataGridView_CellValueChanged (object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var cultureReference = this.ToolStripComboBoxCultureEntryReference.SelectedItem as Culture;
				var cultureTranslation = this.DataGridView.Rows [e.RowIndex].DataBoundItem as CultureTranslation;
				var cultureSelected = ((this.ListViewCultures.SelectedItems.Count == 1) ? (this.ListViewCultures.SelectedItems [0].Tag as Culture) : ((Culture) null));

				if (e.ColumnIndex == this.DataGridView.Columns ["ColumnCultureTranslationValue"].Index)
				{
					if ((cultureSelected != null) && (cultureReference != null) && (cultureSelected.Id == cultureReference.Id))
					{
						cultureTranslation.ValueReference = cultureTranslation.Value;
						this.DataGridView.Rows [e.RowIndex].Cells [e.ColumnIndex].Value = cultureTranslation.Value;

						this.DataGridView.Refresh();
					}
				}
			}
		}

		private void DataGridView_CellFormatting (object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == this.DataGridView.Columns ["ColumnCultureTranslationName"].Index)
			{
				this.DataGridView.Rows [e.RowIndex].Cells [e.ColumnIndex].Value = (this.DataGridView.Rows [e.RowIndex].DataBoundItem as CultureTranslation).Translation.Name;
			}
		}

		private void DataGridView_CellContentClick (object sender, DataGridViewCellEventArgs e)
		{
			var cultureReference = this.ToolStripComboBoxCultureEntryReference.SelectedItem as Culture;
			var cultureTranslation = this.DataGridView.Rows [e.RowIndex].DataBoundItem as CultureTranslation;
			var cultureSelected = ((this.ListViewCultures.SelectedItems.Count == 1) ? (this.ListViewCultures.SelectedItems [0].Tag as Culture) : ((Culture) null));

			if (e.ColumnIndex == this.DataGridView.Columns ["ColumnCultureTranslationCopyReference"].Index)
			{
				if ((cultureSelected != null) && (cultureReference != null) && (cultureSelected.Id == cultureReference.Id))
				{
					cultureTranslation.Value = cultureTranslation.ValueReference;
					this.DataGridView.Rows [e.RowIndex].Cells [e.ColumnIndex].Value = cultureTranslation.ValueReference;

					this.DataGridView.Refresh();
				}
			}
			else if (e.ColumnIndex == this.DataGridView.Columns ["ColumnCultureTranslationCopyToAll"].Index)
			{
				if ((cultureSelected != null) && (cultureReference != null) && (cultureSelected.Id == cultureReference.Id))
				{
					var cultureTranslations = this.ResourceContainer.CultureTranslations.Where(ct => ct.Translation.Id == cultureTranslation.Translation.Id);

					foreach (var ct in cultureTranslations)
					{
						ct.Value = cultureTranslation.Value;
					}

					this.DataGridView.Refresh();
				}
			}
			else if (e.ColumnIndex == this.DataGridView.Columns ["ColumnCultureTranslationGetTranslationMicrosoft"].Index)
			{
				if ((cultureSelected != null) && (cultureReference != null) && (cultureSelected.Id == cultureReference.Id))
				{
					var header = "";
					Exception exception = null;
					MicrosoftTranslatorApiHelper.GetAccessToken(out header, out exception);

					cultureTranslation.ValueReference = cultureTranslation.Value;
					this.DataGridView.Rows [e.RowIndex].Cells [e.ColumnIndex].Value = cultureTranslation.Value;

					this.DataGridView.Refresh();
				}
			}
		}

		private void RefreshData (bool updateUi = true)
		{
			this.RefreshDataCultures(updateUi);
			this.RefreshDataTranslations(updateUi);
			this.RefreshDataCultureTranslations(updateUi);

			this.RefreshControls();
		}

		private void RefreshDataCultures (bool updateUi = true)
		{
			var index = 0;
			var foundInvalidCultureEntry = false;

			this.ToolStripComboBoxCultureEntryReference.Items.Clear();
			this.ToolStripComboBoxCultureEntryReference.Items.Add("None");
			foreach (var culture in this.ResourceContainer.Cultures) { this.ToolStripComboBoxCultureEntryReference.Items.Add(culture); }
			if (this.ToolStripComboBoxCultureEntryReference.SelectedIndex < 0) { this.ToolStripComboBoxCultureEntryReference.SelectedIndex = this.ResourceContainer.Cultures.ToList().FindIndex(c => c.Name == Thread.CurrentThread.CurrentCulture.Name); }
			if (this.ToolStripComboBoxCultureEntryReference.SelectedIndex < 0) { this.ToolStripComboBoxCultureEntryReference.SelectedIndex = 0; }

			if (this.ToolStripComboBoxCultureEntryReference.Items.Count == 0)
			{
				this.ToolStripComboBoxCultureEntryReference.Width = 200;
			}
			else
			{
				if (this.ResourceContainer.Cultures.Count == 0)
				{
					this.ToolStripComboBoxCultureEntryReference.Width = 200;
				}
				else
				{
					using (var graphics = this.ToolStripCultureEntries.CreateGraphics())
					{
						var text = new string('W', this.ResourceContainer.Cultures.Max(c => c.ToString().Length));
						this.ToolStripComboBoxCultureEntryReference.Width = (int) (graphics.MeasureString(text, this.ToolStripComboBoxCultureEntryReference.Font).Width + 10);
					}
				}
			}

			this.ListViewCultures.BeginUpdate();
			this.ListViewCultures.Items.Clear();

			foreach (var culture in this.ResourceContainer.Cultures)
			{
				var cultureInfo = CultureInfo
					.GetCultures(CultureTypes.AllCultures)
					.SingleOrDefault(ci => (ci.Name == culture.Name));

				if (cultureInfo == null)
				{
					foundInvalidCultureEntry = true;
				}
				else
				{
					var item = new ListViewItem();

					index++;
					item.Tag = culture;
					item.Text = culture.Id.ToString();
					item.SubItems.Add(cultureInfo.Name);
					item.SubItems.Add(cultureInfo.LCID.ToString());
					item.SubItems.Add(cultureInfo.NativeName);
					item.SubItems.Add(cultureInfo.DisplayName);
					item.SubItems.Add(cultureInfo.EnglishName);
					item.SubItems.Add(cultureInfo.TwoLetterISOLanguageName);
					item.SubItems.Add(cultureInfo.ThreeLetterISOLanguageName);
					item.SubItems.Add(cultureInfo.ThreeLetterWindowsLanguageName);

					this.ListViewCultures.Items.Add(item);
				}
			}

			for (int i = 0; i < this.ListViewCultures.Columns.Count; i++)
			{
				this.ListViewCultures.Columns [i].Width = -2;
			}

			this.ListViewCultures.EndUpdate();

			if ((updateUi) && (foundInvalidCultureEntry))
			{
				MessageBox.Show(this, "Not all cultures were loaded.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}

			this.RefreshControls();
		}

		private void RefreshDataTranslations (bool updateUi = true)
		{
			this.ListViewTranslations.BeginUpdate();
			this.ListViewTranslations.Items.Clear();

			foreach (var translation in this.ResourceContainer.Translations)
			{
				var item = new ListViewItem();

				item.Tag = translation;
				item.Text = translation.Id.ToString();
				item.SubItems.Add(translation.Name);
				item.SubItems.Add(translation.Comment);

				this.ListViewTranslations.Items.Add(item);
			}

			for (int i = 0; i < this.ListViewTranslations.Columns.Count; i++)
			{
				this.ListViewTranslations.Columns [i].Width = -2;
			}

			this.ListViewTranslations.EndUpdate();

			this.RefreshControls();
		}

		private void RefreshDataCultureTranslations (bool updateUi = true)
		{
			if (this.ListViewCultures.SelectedItems.Count == 1)
			{
				var culture = this.ListViewCultures.SelectedItems [0].Tag as Culture;

				var cultureTranslations	= this.ResourceContainer
						.CultureTranslations
						.Where(ct => ct.CultureId == culture.Id)
						.ToList();

				this.DataGridView.AutoGenerateColumns = false;

				foreach (var cultureTranslation in cultureTranslations)
				{
					if (this.ToolStripComboBoxCultureEntryReference.SelectedIndex <= 0)
					{
						cultureTranslation.ValueReference = "";
						this.DataGridView.Columns ["ColumnCultureTranslationValueReference"].Visible = false;
					}
					else
					{
						this.DataGridView.Columns ["ColumnCultureTranslationValueReference"].Visible = true;

						try
						{
							var cultureReference = this.ToolStripComboBoxCultureEntryReference.SelectedItem as Culture;
							var cultureTranslationReference = this.ResourceContainer.CultureTranslations.SingleOrDefault(ct => ((ct.CultureId == cultureReference.Id) && (ct.TranslationId == cultureTranslation.TranslationId)));

							cultureTranslation.ValueReference = cultureTranslationReference.Value;
						}
						catch (Exception exception)
						{
							cultureTranslation.ValueReference = exception.ToString();
						}
					}
				}

				this.DataGridView.Columns ["ColumnCultureTranslationId"].DataPropertyName = "Id";
				this.DataGridView.Columns ["ColumnCultureTranslationValueReference"].DataPropertyName = "ValueReference";
				this.DataGridView.Columns ["ColumnCultureTranslationValue"].DataPropertyName = "Value";
				this.DataGridView.Columns ["ColumnCultureTranslationComment"].DataPropertyName = "Comment";

				this.DataGridView.DataSource = cultureTranslations;
			}
			else
			{
				this.DataGridView.DataSource = null;
			}

			this.RefreshControls();
		}

		private void RefreshControls ()
		{
			var valid = true;
			var textBox = this.TextBoxTranslationsName;
			var translation = textBox.Tag as Translation;

			var name = textBox.Text.Trim();
			var translations = this.ResourceContainer.Translations.Where(t => (string.Compare(name, t.Name, StringComparison.OrdinalIgnoreCase) == 0));

			valid &= ((valid) && (!translations.Any()));
			valid &= ((valid) && (!string.IsNullOrWhiteSpace(name)));
			valid &= ((valid) && (Regex.IsMatch(name, @"^[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz]+[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_]*$")));

			if (this.ListViewTranslations.SelectedItems.Count == 1)
			{
				if (translation == null)
				{
					this.ButtonTranslationsSave.Enabled = false;
				}
				else
				{
					if (this.ListViewTranslations.SelectedItems [0].Tag == translation)
					{
						this.ButtonTranslationsSave.Enabled = valid;
					}
					else
					{
						this.ButtonTranslationsSave.Enabled = false;
					}
				}
			}
			else
			{
				this.ButtonTranslationsSave.Enabled = false;
			}

			this.ButtonTranslationsAdd.Enabled = valid;

			this.ToolStripButtonCulturesDelete.Enabled = (this.ListViewCultures.SelectedItems.Count > 0);
			this.ToolStripButtonTranslationsEdit.Enabled = (this.ListViewTranslations.SelectedItems.Count == 1);
			this.ToolStripButtonTranslationsDelete.Enabled = (this.ListViewTranslations.SelectedItems.Count > 0);

			this.ToolStripButtonReferenceCopy.Enabled = (this.ToolStripComboBoxCultureEntryReference.SelectedIndex > 0);
			this.ToolStripButtonMicrosoftTranslatorFill.Enabled = ((this.ListViewCultures.SelectedItems.Count == 1) && (this.ResourceContainer.CultureTranslations.Any()) && (this.ToolStripComboBoxCultureEntryReference.SelectedIndex > 0));
		}
	}
}