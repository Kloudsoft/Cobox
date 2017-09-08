using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.Entities.Comparers
{
	public sealed class DocumentComparer:
		IComparer<Document>,
		IComparer
	{
		public enum DocumentSortOrder { None, Ascending, Descending, }
		public enum DocumentSortMember { None = -1, Id = 0, Name = 1, }

		public bool CaseSensitive { get; set; }
		public DocumentSortOrder Order { get; set; }
		public DocumentSortMember Member { get; set; }

		public DocumentComparer (DocumentSortMember member = DocumentSortMember.None, DocumentSortOrder order = DocumentSortOrder.None, bool caseSensitive = false)
		{
			this.Order = order;
			this.Member = member;
			this.CaseSensitive = caseSensitive;
		}

		public int ColumnIndex { get { return ((int) this.Member); } }

		public int Compare (object x, object y)
		{
			return (this.Compare(x as Document, y as Document));
		}

		public int Compare (Document x, Document y)
		{
			var result = 0;
			var stringComparison = this.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

			switch (this.Member)
			{
				case DocumentSortMember.Id: { result = x.Id.CompareTo(y.Id); break; }
				case DocumentSortMember.Name: { result = string.Compare(x.FileNameClient, y.FileNameClient, stringComparison); break; }
			}

			if (this.Order == DocumentSortOrder.Descending) { result = -result; }

			return (result);
		}
	}
}