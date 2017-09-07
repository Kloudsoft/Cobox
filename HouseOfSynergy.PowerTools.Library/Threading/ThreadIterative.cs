using System;
using System.Diagnostics;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.EventArguments;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Threading
{
	public abstract class ThreadIterative:
		Disposable
	{
		#region Events.

		//====================================================================================================
		// Events.
		//====================================================================================================

		public event EventHandler<EventArgs> Started;
		public event EventHandler<EventArgs> Stopped;
		public event EventHandler<CancelExceptionEventArgs> Starting;
		public event EventHandler<CancelExceptionEventArgs> Stopping;

		#endregion Events.

		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public bool _Paused = false;
		private Thread Thread = null;
		private bool Terminate = false;
		private Stopwatch Stopwatch = null;
		private TimeSpan _Interval = TimeSpan.Zero;
		protected bool IsBackground { get; private set; }

		public bool Running { get; private set; }
		public object SyncRootThreadBase { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		protected ThreadIterative (bool isBackground = false)
		{
			this.Running = false;
			this.Disposed = false;
			this.IsBackground = isBackground;
			this.SyncRootThreadBase = new object();

			this.Thread = null;
			this.Stopwatch = new Stopwatch();
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public TimeSpan Elapsed { get { return (this.Stopwatch.Elapsed); } }
		public bool Paused { get { return (this._Paused); } private set { if (this.CanPause) { this._Paused = value; } } }
		public TimeSpan Interval { get { return (this._Interval); } protected set { if (this.Interval <= TimeSpan.Zero) { throw (new Exception("The interval property must be greater than [TimeSpan.Zero].")); } else { this._Interval = value; } } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public void Start ()
		{
			CancelExceptionEventArgs e = null;

			lock (this.SyncRootThreadBase)
			{
				if (this.Disposed)
				{
					throw (new ObjectDisposedException(this.GetType().FullName));
				}

				if (!this.Running)
				{
					e = new CancelExceptionEventArgs();
					this.OnStarting(e);

					if (!e.Cancel)
					{
						e = new CancelExceptionEventArgs();
						this.RaiseEventStarting(e);

						if (!e.Cancel)
						{
							this.Paused = false;
							this.Running = true;
							this.Terminate = false;
							this.Stopwatch.Reset();

							this.Thread = new Thread(new ParameterizedThreadStart(this.Process));
							this.Thread.IsBackground = this.IsBackground;
							this.Thread.Start();
						}
					}
				}
			}
		}

		public void Stop ()
		{
			this.Stop(true);
		}

		private void Stop (bool throwOnDisposed)
		{
			this.Stop((object) throwOnDisposed);
		}

		private void Stop (object throwOnDisposed)
		{
			CancelExceptionEventArgs e = null;

			lock (this.SyncRootThreadBase)
			{
				if (((bool) throwOnDisposed) && (this.Disposed))
				{
					throw (new ObjectDisposedException(this.GetType().FullName));
				}

				if (this.Running)
				{
					e = new CancelExceptionEventArgs();
					try { this.OnStopping(e); }
					catch { }

					if (!e.Cancel)
					{
						e = new CancelExceptionEventArgs();
						try { this.RaiseEventStopping(e); }
						catch { }

						if (!e.Cancel)
						{
							try
							{
								this.Terminate = true;

								this.Thread.Join();
							}
							catch
							{
								try { this.Thread.Abort(); }
								catch { }
							}
							finally
							{
								this.Thread = null;
								this.Running = false;
								this.Stopwatch.Stop();
							}

							try { this.OnStopped(); }
							catch { }
							try { this.RaiseEventStopped(); }
							catch { }
						}
					}
				}
			}
		}

		private void Process (object cancellationToken)
		{
			var iteration = 0L;
			Exception exception = null;

			if (this.Disposed) { throw (new ObjectDisposedException(this.GetType().FullName)); }
			if (cancellationToken == null) { throw (new ArgumentNullException("cancellationToken")); }
			if (!(cancellationToken is CancellationToken)) { throw (new ArgumentException("The argument [cancellationToken] must be of type [CancellationToken].", "cancellationToken")); }

			var token = (CancellationToken) cancellationToken;

			this.Stopwatch.Restart();

			try { this.OnStarted(); }
			catch { }
			try { this.RaiseEventStarted(); }
			catch { }

			try
			{
				do
				{
					if (!this._Paused)
					{
						if (this.OnIteration(iteration))
						{
							iteration++;
						}
						else
						{
							break;
						}
					}

					Thread.Sleep(this.Interval);
				}
				while (!this.Terminate);
			}
			catch (OperationCanceledException)
			{
				// Do nothing.
			}
			catch (Exception e)
			{
				exception = e;
			}

			Trace.Assert((exception == null), string.Format("The type [{0}] threw an exception:\r\n\r\n{1}.", this.GetType().Name, ((exception == null) ? ("") : (exception.ToString()))));

			this.Stopwatch.Stop();

			if (!token.IsCancellationRequested)
			{
				new Thread(new ParameterizedThreadStart(this.Stop)) { Priority = ThreadPriority.Highest, IsBackground = false, }.Start(false);
			}
		}

		protected abstract void OnStarted ();
		protected abstract void OnStopped ();
		public abstract bool CanPause { get; }
		protected abstract bool OnIteration (long iteration);
		protected abstract void OnStarting (CancelExceptionEventArgs e);
		protected abstract void OnStopping (CancelExceptionEventArgs e);

		public void Pause ()
		{
			lock (this.SyncRootThreadBase)
			{
				if (!this.Paused)
				{
					if (this.Running)
					{
						this.Paused = true;
						this.Stopwatch.Stop();
					}
				}
			}
		}

		public void Resume ()
		{
			lock (this.SyncRootThreadBase)
			{
				if (this.Paused)
				{
					this.Paused = false;

					if (this.Running)
					{
						this.Stopwatch.Start();
					}
				}
			}
		}

		#endregion Methods.

		#region Methods: Events.

		//====================================================================================================
		// Methods: Events.
		//====================================================================================================

		private void RaiseEventStarting (CancelExceptionEventArgs e)
		{
			this.Starting.SafeInvoke(this, e);
		}

		private void RaiseEventStarted ()
		{
			this.Started.SafeInvoke(this, new EventArgs());
		}

		private void RaiseEventStopping (CancelExceptionEventArgs e)
		{
			this.Stopping.SafeInvoke(this, e);
		}

		private void RaiseEventStopped ()
		{
			this.Stopped.SafeInvoke(this, new EventArgs());
		}

		#endregion Methods: Events.

		#region Interface Implementation: System.IDisposable.

		//====================================================================================================
		// Interface Implementation: System.IDisposable.
		//====================================================================================================

		private bool Disposed { get; set; }

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.

					this.Stop(true);
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion Interface Implementation: System.IDisposable.
	}
}