using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HouseOfSynergy.AffinityDms.ResourceProvider.Classes;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
	public sealed class CultureComparer:
		IComparer<Culture>,
		IComparer
	{
		public enum CultureSortOrder { None, Ascending, Descending, }
		public enum CultureSortMember { None = -1, Id = 0, Name = 1, LocaleId = 2, NameNative = 3, NameDisplay = 4, NameEnglish = 5, NameIsoTwoLetter = 6, NameIsoThreeLetter = 7, NameWindowsThreeLetter = 8, }

		public bool CaseSensitive { get; set; }
		public CultureSortOrder Order { get; set; }
		public CultureSortMember Member { get; set; }

		public CultureComparer (CultureSortMember member = CultureSortMember.None, CultureSortOrder order = CultureSortOrder.None, bool caseSensitive = false)
		{
			this.Order = order;
			this.Member = member;
			this.CaseSensitive = caseSensitive;
		}

		public int ColumnIndex { get { return ((int) this.Member); } }

		public int Compare (object x, object y)
		{
			return (this.Compare((x as ListViewItem).Tag as Culture, (y as ListViewItem).Tag as Culture));
		}

		public int Compare (Culture x, Culture y)
		{
			var result = 0;
			var stringComparison = this.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

			switch (this.Member)
			{
				case CultureSortMember.Id: { result = x.Id.CompareTo(y.Id); break; }
				case CultureSortMember.Name: { result = string.Compare(x.Name, y.Name, stringComparison); break; }
				case CultureSortMember.LocaleId: { result = x.LocaleId.CompareTo(y.LocaleId); break; }
				case CultureSortMember.NameNative: { result = string.Compare(x.NameNative, y.NameNative, stringComparison); break; }
				case CultureSortMember.NameDisplay: { result = string.Compare(x.NameDisplay, y.NameDisplay, stringComparison); break; }
				case CultureSortMember.NameEnglish: { result = string.Compare(x.NameEnglish, y.NameEnglish, stringComparison); break; }
				case CultureSortMember.NameIsoTwoLetter: { result = string.Compare(x.NameIsoTwoLetter, y.NameIsoTwoLetter, stringComparison); break; }
				case CultureSortMember.NameIsoThreeLetter: { result = string.Compare(x.NameIsoThreeLetter, y.NameIsoThreeLetter, stringComparison); break; }
				case CultureSortMember.NameWindowsThreeLetter: { result = string.Compare(x.NameWindowsThreeLetter, y.NameWindowsThreeLetter, stringComparison); break; }
			}

			if (this.Order == CultureSortOrder.Descending) { result = -result; }

			return (result);
		}
	}
}