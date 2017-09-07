using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Scheduling.Triggers
{
	public sealed class TriggerComparer:
		IComparer<Trigger>
	{
		public enum MemberType
		{
			None,
			Guid,
		}

		public enum OrderType
		{
			None,
			Ascending,
			Descending,
		}

		public OrderType Order { get; private set; }
		public MemberType Member { get; private set; }

		public TriggerComparer (MemberType member = MemberType.Guid, OrderType order = OrderType.Ascending)
		{
			this.Order = order;
			this.Member = member;
		}

		public int Compare (Trigger x, Trigger y)
		{
			int result = 0;

			switch (this.Member)
			{
				case MemberType.Guid:
				{
					result = x.Key.CompareTo(y.Key);

					break;
				}
			}

			if (this.Order == OrderType.Descending) { result = -result; }

			return (result);
		}

		public static readonly TriggerComparer Default = new TriggerComparer(MemberType.Guid, OrderType.Ascending);
	}
}