using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Log
{
	public sealed class Logger:
		Disposable,
		ILogger
	{
		private bool Disposed { get; set; }
		private object _SyncRoot = new object();
		private FileStream FileStream { get; set; }
		public FileInfo FileInfo { get; private set; }
		private StreamWriter StreamWriter { get; set; }
		public DirectoryInfo DirectoryInfo { get; private set; }

		public Logger (string directory) : this(new DirectoryInfo(directory)) { }

		public Logger (DirectoryInfo directory)
		{
			lock (this._SyncRoot)
			{
				this.DirectoryInfo = new DirectoryInfo(directory.FullName);

				this.DirectoryInfo.Refresh();
				if (!this.DirectoryInfo.Exists) { this.DirectoryInfo.Create(); this.DirectoryInfo.Refresh(); }

				this.DirectoryInfo = new DirectoryInfo(Path.Combine(this.DirectoryInfo.FullName, "Log"));
				if (!this.DirectoryInfo.Exists) { this.DirectoryInfo.Create(); this.DirectoryInfo.Refresh(); }

				this.FileInfo = new FileInfo(Path.Combine(this.DirectoryInfo.FullName, "Log " + DateTimeOffset.Now.DateTime.ToString(@"yyyy-MM-dd") + ".txt"));
				if (!this.FileInfo.Exists) { this.FileInfo.Create().Close(); this.FileInfo.Refresh(); }

				this.FileStream = File.Open(this.FileInfo.FullName, FileMode.Append, FileAccess.Write, FileShare.Read);
				this.StreamWriter = new StreamWriter(this.FileStream);
				this.StreamWriter.AutoFlush = true;
			}
		}

		public object SyncRoot { get { return (this._SyncRoot); } }

		public void Write (string message) { lock (this._SyncRoot) { this.Write(message, (Exception) null); } }
		public void Write (LogEntry entry) { lock (this._SyncRoot) { this.Write(entry.ToString()); } }
		public void Write (Exception exception) { lock (this._SyncRoot) { this.Write("", exception); } }
		public void Write (IEnumerable<Exception> exceptions) { this.Write(new AggregateException(exceptions)); }
		public void Write (string message, IEnumerable<Exception> exceptions) { this.Write(message, new AggregateException(exceptions)); }

		public void Write (string message, Exception exception)
		{
			int count = 0;
			string text = "";
			Exception innerException = null;
			DateTime dateTime = DateTime.MinValue;
			AggregateException aggregateException = null;

			lock (this._SyncRoot)
			{
				dateTime = DateTime.Now;

				text
					= dateTime.ToString("yyyy-MM-dd HH:mm:ss")
					+ ": "
					;

				if ((message != null) && (message.Trim().Length > 0))
				{
					text += message;
				}

				if (exception != null)
				{
					if (exception is AggregateException)
					{
						aggregateException = exception as AggregateException;

						text
							= text
							+ Environment.NewLine
							+ new string(' ', 21)
							+ "Aggregate Exception: "
							+ exception.GetType().FullName
							+ Environment.NewLine
							+ new string(' ', 21)
							+ "Message: "
							+ exception.Message
							+ Environment.NewLine
							+ new string(' ', 21)
							+ "Stack Trace: "
							+ Environment.NewLine
							+ this.GetFormattedStackTrace(exception.StackTrace)
							;

						foreach (var e in aggregateException.InnerExceptions)
						{
							text
								= text
								+ Environment.NewLine
								+ new string(' ', 30)
								+ "Exception: "
								+ exception.GetType().FullName
								+ Environment.NewLine
								+ new string(' ', 30)
								+ "Message: "
								+ exception.Message
								+ Environment.NewLine
								+ new string(' ', 30)
								+ "Stack Trace: "
								+ Environment.NewLine
								+ this.GetFormattedStackTrace(exception.StackTrace, 30)
								;
						}
					}
					//else if (exception is System.Data.Entity.Validation.DbEntityValidationException)
					//{
					//    text
					//        = text
					//        + System.Environment.NewLine
					//        + new string(' ', 21)
					//        + "Exception: "
					//        + exception.GetType().FullName
					//        + System.Environment.NewLine
					//        + new string(' ', 21)
					//        + "Message: "
					//        + exception.Message
					//        + System.Environment.NewLine
					//        + new string(' ', 21)
					//        + "Validation Errors: "
					//        + System.Environment.NewLine
					//        ;

					//    var exp = exception as System.Data.Entity.Validation.DbEntityValidationException;

					//    foreach (var error in exp.EntityValidationErrors)
					//    {
					//        foreach (var err in error.ValidationErrors)
					//        {
					//            text
					//                = text
					//                + new string(' ', 25)
					//                + err.PropertyName
					//                + ": "
					//                + err.ErrorMessage
					//                + System.Environment.NewLine
					//                ;
					//        }
					//    }
					//}
					else
					{
						text
							= text
							+ Environment.NewLine
							+ new string(' ', 21)
							+ "Exception: "
							+ exception.GetType().FullName
							+ Environment.NewLine
							+ new string(' ', 21)
							+ "Message: "
							+ exception.Message
							+ Environment.NewLine
							+ new string(' ', 21)
							+ "Stack Trace: "
							+ Environment.NewLine
							+ this.GetFormattedStackTrace(exception.StackTrace)
							;

						innerException = exception.InnerException;
						while (innerException != null)
						{
							count++;

							text
								= text
								+ Environment.NewLine
								+ new string(' ', (count * 3) + 21)
								+ "Exception: "
								+ innerException.GetType().FullName
								+ Environment.NewLine
								+ new string(' ', (count * 3) + 21)
								+ "Message: "
								+ innerException.Message
								+ Environment.NewLine
								+ new string(' ', (count * 3) + 21)
								+ "Stack Trace: "
								+ Environment.NewLine
								+ this.GetFormattedStackTrace(innerException.StackTrace, (count * 3) + 21)
								;

							innerException = innerException.InnerException;
						}
					}
				}

				this.StreamWriter.WriteLine(text);
				this.StreamWriter.Flush();
			}
		}

		private string GetFormattedStackTrace (string trace, int indentationCount = 21)
		{
			string [] lines = null;

			if (trace == null)
			{
				lines = new string [] { };
			}
			else
			{
				lines = trace.Split(new string [] { Environment.NewLine }, StringSplitOptions.None);
			}

			return (string.Join(Environment.NewLine, lines.ToList().ConvertAll<string>(line => new string(' ', indentationCount) + line)));
		}

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Dispose managed resources only.

					lock (this._SyncRoot)
					{
						if (this.StreamWriter != null)
						{
							try
							{
								this.StreamWriter.Flush();
							}
							finally
							{
								try { this.StreamWriter.Close(); }
								catch { }
							}
							try { this.StreamWriter.Dispose(); }
							finally { this.StreamWriter = null; }
						}

						if (this.FileStream != null)
						{
							try { this.FileStream.Dispose(); }
							finally { this.FileStream = null; }
						}
					}
				}

				// Dispose unmanaged resources here.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}
	}
}