using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public sealed class WindowComparer:
		System.Collections.IComparer,
		System.Collections.Generic.IComparer<Window>
	{
		public enum EnumMember
		{
			Handle,
			ClassName,
			Caption
		}

		public System.Windows.Forms.SortOrder Order { get; set; }

		public HouseOfSynergy.PowerTools.Library.Win32Api.WindowComparer.EnumMember Member { get; set; }

		public WindowComparer ()
		{
			this.Member = HouseOfSynergy.PowerTools.Library.Win32Api.WindowComparer.EnumMember.Handle;
			this.Order = System.Windows.Forms.SortOrder.Ascending;
		}

		public WindowComparer (HouseOfSynergy.PowerTools.Library.Win32Api.WindowComparer.EnumMember member) : this() { this.Member = member; }

		public WindowComparer (System.Windows.Forms.SortOrder order) : this() { this.Order = order; }

		public WindowComparer (HouseOfSynergy.PowerTools.Library.Win32Api.WindowComparer.EnumMember member, System.Windows.Forms.SortOrder order) { this.Member = member; this.Order = order; }

		public int Compare (object x, object y)
		{
			return (this.Compare((Window) x, (Window) y));
		}

		public int Compare (Window x, Window y)
		{
			int result = 0;

			if (this.Order != System.Windows.Forms.SortOrder.None)
			{
				switch (this.Member)
				{
					//case CloudTech.ATS.Library.WindowComparer.EnumMember.Handle: { result = x.Handle.CompareTo(y.Handle); break; }
					case HouseOfSynergy.PowerTools.Library.Win32Api.WindowComparer.EnumMember.ClassName: { result = x.ClassName.CompareTo(y.ClassName); break; }
					case HouseOfSynergy.PowerTools.Library.Win32Api.WindowComparer.EnumMember.Caption: { result = x.Caption.CompareTo(y.Caption); break; }
				}

				if (this.Order == System.Windows.Forms.SortOrder.Descending)
				{
					result = -result;
				}
			}

			return (result);
		}
	}
}