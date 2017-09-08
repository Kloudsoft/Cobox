using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
	public sealed class CultureInfoComparer:
		IComparer<CultureInfo>,
		IComparer
	{
		public enum CultureInfoSortOrder { None, Ascending, Descending, }
		public enum CultureInfoSortMember { None = -1, Name = 0, LCID = 1, NativeName = 2, DisplayName = 3, EnglishName = 4, TwoLetterISOLanguageName = 5, ThreeLetterISOLanguageName = 6, ThreeLetterWindowsLanguageName = 7, }

		public bool CaseSensitive { get; set; }
		public CultureInfoSortOrder Order { get; set; }
		public CultureInfoSortMember Member { get; set; }

		public CultureInfoComparer (CultureInfoSortMember member = CultureInfoSortMember.None, CultureInfoSortOrder order = CultureInfoSortOrder.None, bool caseSensitive = false)
		{
			this.Order = order;
			this.Member = member;
			this.CaseSensitive = caseSensitive;
		}

		public int ColumnIndex { get { return ((int) this.Member); } }

		public int Compare (object x, object y)
		{
			return (this.Compare((x as ListViewItem).Tag as CultureInfo, (y as ListViewItem).Tag as CultureInfo));
		}

		public int Compare (CultureInfo x, CultureInfo y)
		{
			var result = 0;
			var stringComparison = this.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

			switch (this.Member)
			{
				case CultureInfoSortMember.Name: { result = string.Compare(x.Name, y.Name, stringComparison); break; }
				case CultureInfoSortMember.LCID: { result = x.LCID.CompareTo(y.LCID); break; }
				case CultureInfoSortMember.NativeName: { result = string.Compare(x.NativeName, y.NativeName, stringComparison); break; }
				case CultureInfoSortMember.DisplayName: { result = string.Compare(x.DisplayName, y.DisplayName, stringComparison); break; }
				case CultureInfoSortMember.EnglishName: { result = string.Compare(x.EnglishName, y.EnglishName, stringComparison); break; }
				case CultureInfoSortMember.TwoLetterISOLanguageName: { result = string.Compare(x.TwoLetterISOLanguageName, y.TwoLetterISOLanguageName, stringComparison); break; }
				case CultureInfoSortMember.ThreeLetterISOLanguageName: { result = string.Compare(x.ThreeLetterISOLanguageName, y.ThreeLetterISOLanguageName, stringComparison); break; }
				case CultureInfoSortMember.ThreeLetterWindowsLanguageName: { result = string.Compare(x.ThreeLetterWindowsLanguageName, y.ThreeLetterWindowsLanguageName, stringComparison); break; }
			}

			if (this.Order == CultureInfoSortOrder.Descending) { result = -result; }

			return (result);
		}
	}
}