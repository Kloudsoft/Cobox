using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Log
{
	public class LogEntry
	{
		public string Text { get; private set; }
		public LogEntryType Type { get; private set; }
		public Exception Exception { get; private set; }
		public DateTimeOffset DateTimeOffset { get; private set; }

		private LogEntry (LogEntryType type, DateTimeOffset dateTimeOffset, string text, Exception exception)
		{
			this.Text = text;
			this.Type = type;
			this.Exception = exception;
			this.DateTimeOffset = dateTimeOffset;
		}

		public LogEntry (string text) : this(LogEntryType.Info, DateTimeOffset.Now, text, null) { }
		public LogEntry (Exception exception) : this(LogEntryType.Error, DateTimeOffset.Now, "", exception) { }
		public LogEntry (string text, Exception exception) : this(LogEntryType.Error, DateTimeOffset.Now, text, exception) { }
		public LogEntry (DateTimeOffset dateTimeOffset, string text) : this(LogEntryType.Info, dateTimeOffset, text, null) { }
		public LogEntry (DateTimeOffset dateTimeOffset, Exception exception) : this(LogEntryType.Error, dateTimeOffset, "", exception) { }
		public LogEntry (DateTimeOffset dateTimeOffset, string text, Exception exception) : this(LogEntryType.Error, dateTimeOffset, text, exception) { }

		public override string ToString ()
		{
			if (this.Exception == null)
			{
				return (string.Join("[{0}]: {1}: [{2}].", this.DateTimeOffset, this.Type, this.Text));
			}
			else
			{
				return (string.Join("[{0}]: {1}: [{2}].", this.DateTimeOffset, this.Type, this.Text + Environment.NewLine + this.Exception.Message));
			}
		}
	}
}