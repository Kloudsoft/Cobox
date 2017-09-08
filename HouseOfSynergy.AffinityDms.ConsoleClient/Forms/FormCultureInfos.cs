using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HouseOfSynergy.AffinityDms.ConsoleClient.Classes;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Forms
{
	public partial class FormCultureInfos:
		Form
	{
		public CultureInfo SelectedCultureInfo { get; private set; }

		public FormCultureInfos ()
		{
			this.InitializeComponent();
		}

		private void FormCultureInfos_Load (object sender, EventArgs e)
		{
		}

		private void FormCultureInfos_Shown (object sender, EventArgs e)
		{
			this.RefreshData();

			this.ListViewCultures.Select();
		}

		private void FormCultureInfos_FormClosing (object sender, FormClosingEventArgs e)
		{
		}

		private void ListViewCultures_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (this.ListViewCultures.SelectedItems.Count == 1)
			{
				this.SelectedCultureInfo = this.ListViewCultures.SelectedItems [0].Tag as CultureInfo;
			}
			else
			{
				this.SelectedCultureInfo = null;
			}

			this.RefreshControls();
		}

		private void ListViewCultures_DoubleClick (object sender, EventArgs e)
		{
			if (this.ListViewCultures.SelectedItems.Count == 1)
			{
				this.ButtonOk.PerformClick();
			}
		}

		private void ListViewCultures_ColumnClick (object sender, ColumnClickEventArgs e)
		{
			this.ListViewCultures.ListViewItemSorter
				= this.ListViewCultures.ListViewItemSorter
				?? new CultureInfoComparer();

			var comparer = this.ListViewCultures.ListViewItemSorter as CultureInfoComparer;

			if (e.Column == comparer.ColumnIndex)
			{
				if (comparer.Order == CultureInfoComparer.CultureInfoSortOrder.None)
				{
					comparer.Order = CultureInfoComparer.CultureInfoSortOrder.Ascending;
				}
				else if (comparer.Order == CultureInfoComparer.CultureInfoSortOrder.Ascending)
				{
					comparer.Order = CultureInfoComparer.CultureInfoSortOrder.Descending;
				}
				else if (comparer.Order == CultureInfoComparer.CultureInfoSortOrder.Descending)
				{
					comparer.Order = CultureInfoComparer.CultureInfoSortOrder.Ascending;
				}
			}
			else
			{
				comparer.Order = CultureInfoComparer.CultureInfoSortOrder.Ascending;
				comparer.Member = (CultureInfoComparer.CultureInfoSortMember) e.Column;
			}

			this.ListViewCultures.Sort();
		}

		private void ButtonOk_Click (object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void ButtonCancel_Click (object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;

			this.Close();
		}

		private void RefreshData ()
		{
			var index = 0;
			var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

			this.ListViewCultures.BeginUpdate();
			this.ListViewCultures.Items.Clear();

			foreach (var culture in cultures)
			{
				var item = new ListViewItem();

				index++;
				item.Tag = culture;
				item.Text = culture.Name;
				item.SubItems.Add(culture.LCID.ToString());
				item.SubItems.Add(culture.NativeName);
				item.SubItems.Add(culture.DisplayName);
				item.SubItems.Add(culture.EnglishName);
				item.SubItems.Add(culture.TwoLetterISOLanguageName);
				item.SubItems.Add(culture.ThreeLetterISOLanguageName);
				item.SubItems.Add(culture.ThreeLetterWindowsLanguageName);

				this.ListViewCultures.Items.Add(item);
			}

			for (int i = 0; i < this.ListViewCultures.Columns.Count; i++)
			{
				this.ListViewCultures.Columns [i].Width = -2;
			}

			this.ListViewCultures.EndUpdate();

			this.RefreshControls();
		}

		private void RefreshControls ()
		{
			this.ButtonOk.Enabled = this.ListViewCultures.SelectedItems.Count == 1;
		}
	}
}