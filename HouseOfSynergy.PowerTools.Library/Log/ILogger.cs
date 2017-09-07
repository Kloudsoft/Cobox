using System;
using System.Diagnostics;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Log
{
	public interface ILogger:
		IDisposable
	{
		void Write (string message);
		void Write (LogEntry entry);
		void Write (Exception exception);
		void Write (string message, Exception exception);
	}
}