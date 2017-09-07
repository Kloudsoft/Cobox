using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static ListViewItem.ListViewSubItem Add (this ListViewItem.ListViewSubItemCollection listViewSubItemCollection, string name, string value)
		{
			ListViewItem.ListViewSubItem listViewSubItem = null;

			listViewSubItem = listViewSubItemCollection.Add(value);
			listViewSubItem.Name = name;

			return (listViewSubItem);
		}

		#region System.Windows.Forms.

		//====================================================================================================
		// System.Windows.Forms.
		//====================================================================================================

		public static void SleepForFifthSecond (this Form form)
		{
			Thread.Sleep(TimeSpan.FromSeconds(1));
		}

		public static void SleepForHalfSecond (this Form form)
		{
			Thread.Sleep(TimeSpan.FromSeconds(1));
		}

		public static void SleepForOneSecond (this Form form)
		{
			Thread.Sleep(TimeSpan.FromSeconds(1));
		}

		public static void Sleep (this Form form, int millisecondsTimeout)
		{
			Thread.Sleep(millisecondsTimeout);
		}

		public static void Sleep (this Form form, TimeSpan timeout)
		{
			Thread.Sleep(timeout);
		}

		public static void DisableForm (this Form form)
		{
			form.Enabled = false;
			form.Cursor = Cursors.WaitCursor;
		}

		public static void LookLessBusy (this Form form)
		{
			form.DisableForm();
			form.Sleep(TimeSpan.FromSeconds(0.2));
			form.EnableForm();
		}

		public static void LookHalfBusy (this Form form)
		{
			form.DisableForm();
			form.Sleep(TimeSpan.FromSeconds(0.5));
			form.EnableForm();
		}

		public static void LookVeryBusy (this Form form)
		{
			form.DisableForm();
			form.Sleep(TimeSpan.FromSeconds(1));
			form.EnableForm();
		}

		public static void EnableForm (this Form form)
		{
			form.Cursor = Cursors.Default;
			form.Enabled = true;
		}

		#endregion System.Windows.Forms.

		#region MessageBox.Show().

		//====================================================================================================
		// System.Windows.Forms.MessageBox.Show().
		//====================================================================================================

		public static DialogResult ShowMessageBox (this Form form, string text) { return (MessageBox.Show(text)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text) { return (MessageBox.Show(owner, text)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption) { return (MessageBox.Show(text, caption)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption) { return (MessageBox.Show(owner, text, caption)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons) { return (MessageBox.Show(text, caption, buttons)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons) { return (MessageBox.Show(owner, text, caption, buttons)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) { return (MessageBox.Show(text, caption, buttons, icon)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) { return (MessageBox.Show(owner, text, caption, buttons, icon)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) { return (MessageBox.Show(text, caption, buttons, icon, defaultButton)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) { return (MessageBox.Show(owner, text, caption, buttons, icon, defaultButton)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options) { return (MessageBox.Show(text, caption, buttons, icon, defaultButton, options)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options) { return (MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton) { return (MessageBox.Show(text, caption, buttons, icon, defaultButton, options, displayHelpButton)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath) { return (MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath) { return (MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator) { return (MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword) { return (MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator) { return (MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword) { return (MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword)); }
		public static DialogResult ShowMessageBox (this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param) { return (MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param)); }
		public static DialogResult ShowMessageBox (this Form form, IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param) { return (MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param)); }

		#endregion MessageBox.Show().
	}
}