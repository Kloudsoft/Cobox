using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.Entities.Comparers
{
	public sealed class IEntityComparer<T>:
		IComparer<IEntity<T>>,
		IComparer
		where T: IEntity<T>
	{
		public enum IEntitySortMember { None = -1, Id = 0, }
		public enum IEntitySortOrder { None, Ascending, Descending, }

		public bool CaseSensitive { get; set; }
		public IEntitySortOrder Order { get; set; }
		public IEntitySortMember Member { get; set; }

		public IEntityComparer (IEntitySortMember member = IEntitySortMember.None, IEntitySortOrder order = IEntitySortOrder.None, bool caseSensitive = false)
		{
			this.Order = order;
			this.Member = member;
			this.CaseSensitive = caseSensitive;
		}

		public int ColumnIndex { get { return ((int) this.Member); } }

		public int Compare (object x, object y)
		{
			return (this.Compare(x as IEntity<T>, y as IEntity<T>));
		}

		public int Compare (IEntity<T> x, IEntity<T> y)
		{
			var result = 0;
			var stringComparison = this.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

			switch (this.Member)
			{
				case IEntitySortMember.Id: { result = x.Id.CompareTo(y.Id); break; }
			}

			if (this.Order == IEntitySortOrder.Descending) { result = -result; }

			return (result);
		}
	}
}