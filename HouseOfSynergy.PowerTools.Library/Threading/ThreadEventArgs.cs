using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.PowerTools.Library.Threading
{
	public abstract class ThreadEventArgs:
		EventArgs
	{
		public TimeSpan Elapsed { get; private set; }
		public DateTimeOffset DateTimeOffset { get; private set; }

		protected ThreadEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed)
			: base()
		{
			this.Elapsed = elapsed;
			this.DateTimeOffset = dateTimeOffset;
		}
	}

	public abstract class ThreadExceptionEventArgs:
		ThreadEventArgs
	{
		public ReadOnlyCollection<Exception> Exceptions { get; private set; }

		protected ThreadExceptionEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed)
			: base(dateTimeOffset, elapsed)
		{
			this.Exceptions = new ReadOnlyCollection<Exception>(new Exception [] { }.ToList());
		}

		protected ThreadExceptionEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, Exception exception)
			: base(dateTimeOffset, elapsed)
		{
			if (exception == null)
			{
				this.Exceptions = new ReadOnlyCollection<Exception>(new Exception [] { }.ToList());
			}
			else
			{
				this.Exceptions = new ReadOnlyCollection<Exception>(new Exception [] { exception }.ToList());
			}
		}

		protected ThreadExceptionEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, IEnumerable<Exception> exceptions)
			: base(dateTimeOffset, elapsed)
		{
			this.Exceptions = new ReadOnlyCollection<Exception>((exceptions ?? (new Exception [] { })).ToList());
		}
	}

	public sealed class ThreadErrorEventArgs:
		ThreadExceptionEventArgs
	{
		public ThreadErrorEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed)
			: base(dateTimeOffset, elapsed)
		{
		}

		public ThreadErrorEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, Exception exception)
			: base(dateTimeOffset, elapsed, exception)
		{
		}

		public ThreadErrorEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, IEnumerable<Exception> exceptions)
			: base(dateTimeOffset, elapsed, exceptions)
		{
		}
	}

	public sealed class ThreadProgressEventArgs:
		ThreadExceptionEventArgs
	{
		public int Minimum { get; private set; }
		public int Maximum { get; private set; }
		public object Data { get; private set; }
		public int Processed { get; private set; }
		public string Message { get; private set; }
		public ProgressType ProgressType { get; set; }

		public ThreadProgressEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, string message, object data = null)
			: base(dateTimeOffset, elapsed)
		{
			this.Data = data;
			this.Minimum = 0;
			this.Maximum = 0;
			this.Processed = 0;
			this.Message = message;
			this.ProgressType = ProgressType.UnDeterministic;
		}

		public ThreadProgressEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, string message, Exception exception, object data = null)
			: base(dateTimeOffset, elapsed, exception)
		{
			this.Data = data;
			this.Minimum = 0;
			this.Maximum = 0;
			this.Processed = 0;
			this.Message = message;
			this.ProgressType = ProgressType.UnDeterministic;
		}

		public ThreadProgressEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, string message, IEnumerable<Exception> exceptions, object data = null)
			: base(dateTimeOffset, elapsed, exceptions)
		{
			this.Data = data;
			this.Minimum = 0;
			this.Maximum = 0;
			this.Processed = 0;
			this.Message = message;
			this.ProgressType = ProgressType.UnDeterministic;
		}

		public ThreadProgressEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, string message, int minimum, int maximum, int processed, object data = null)
			: base(dateTimeOffset, elapsed)
		{
			this.Data = data;
			this.Minimum = minimum;
			this.Maximum = maximum;
			this.Message = message;
			this.Processed = processed;
			this.ProgressType = ProgressType.Deterministic;
		}

		public ThreadProgressEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, string message, int minimum, int maximum, int processed, Exception exception, object data = null)
			: base(dateTimeOffset, elapsed, exception)
		{
			this.Data = data;
			this.Minimum = minimum;
			this.Maximum = maximum;
			this.Message = message;
			this.Processed = processed;
			this.ProgressType = ProgressType.Deterministic;
		}

		public ThreadProgressEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, string message, int minimum, int maximum, int processed, IEnumerable<Exception> exceptions, object data = null)
			: base(dateTimeOffset, elapsed, exceptions)
		{
			this.Data = data;
			this.Minimum = minimum;
			this.Maximum = maximum;
			this.Message = message;
			this.Processed = processed;
			this.ProgressType = ProgressType.Deterministic;
		}

		public float PercentageCompleted { get { return ((((float) this.Processed) / ((float) this.Maximum)) * 100F); } }
	}

	public sealed class ThreadCompletedEventArgs:
		ThreadExceptionEventArgs
	{
		public ThreadCompletedEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed)
			: base(dateTimeOffset, elapsed)
		{
		}

		public ThreadCompletedEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, Exception exception)
			: base(dateTimeOffset, elapsed, exception)
		{
		}

		public ThreadCompletedEventArgs (DateTimeOffset dateTimeOffset, TimeSpan elapsed, IEnumerable<Exception> exceptions)
			: base(dateTimeOffset, elapsed, exceptions)
		{
		}
	}
}