using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HouseOfSynergy.PowerTools.Library.Generics;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	public static class TimedMessageBox<TEnum>
		where TEnum: struct, IComparable, IFormattable, IConvertible
	{
		static TimedMessageBox ()
		{
			if (!typeof(TEnum).IsEnum) { throw (new GenericArgumentTypeException(typeof(TEnum), "The generic type argument [<TEnum>] must be a valid enumeration.", "<TEnum>")); }
			if (Enum.GetNames(typeof(TEnum)).Length == 0) { throw (new GenericArgumentTypeException(typeof(TEnum), "The generic type argument [<TEnum>] must be a valid enumeration with at least one member.", "<TEnum>")); }
		}

		public static TEnum Show
		(
			IWin32Window owner,
			string text,
			string caption,
			TEnum buttons,
			MessageBoxIcon icon,
			TEnum defaultButton,
			TimeSpan timeout
		)
		{
			var form = new FormTimedMessageBox<TEnum>(owner, text, caption, buttons, icon, defaultButton, timeout);

			form.ShowDialog(owner);

			return (form.MessageBoxDialogResult);
		}
	}
}