using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Threading
{
	public abstract class ThreadBase:
		Disposable
	{
		#region Events.

		//====================================================================================================
		// Events.
		//====================================================================================================

		public event EventHandler<EventArgs> Started = null;
		public event EventHandler<EventArgs> Stopped = null;
		public event EventHandler<CancelEventArgs> Starting = null;
		public event EventHandler<CancelEventArgs> Stopping = null;

		#endregion Events.

		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private Thread Thread = null;
		private readonly Stopwatch Stopwatch = null;
		private readonly object _SyncRootThreadBase = new object();

		public bool Paused { get; private set; }
		public bool Running { get; private set; }
		protected bool IsBackground { get; private set; }
		public ApartmentState ApartmentState { get; private set; }
		public ThreadPriority ThreadPriority { get; private set; }

		private CancellationTokenSource CancellationTokenSource = null;

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		protected ThreadBase (bool isBackground = false, ApartmentState apartmentState = ApartmentState.MTA, ThreadPriority threadPriority = ThreadPriority.Normal)
		{
			this.Running = false;
			this.Disposed = false;
			this.IsBackground = isBackground;
			this.ApartmentState = apartmentState;
			this.ThreadPriority = threadPriority;
			this._SyncRootThreadBase = new object();

			this.Thread = null;
			this.Stopwatch = new Stopwatch();
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public TimeSpan Elapsed { get { return (this.Stopwatch.Elapsed); } }
		public object SyncRootThreadBase { get { return (this._SyncRootThreadBase); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public void Start ()
		{
			CancelEventArgs e = null;

			lock (this._SyncRootThreadBase)
			{
				if (this.Disposed)
				{
					throw (new ObjectDisposedException(this.GetType().FullName));
				}

				if (!this.Running)
				{
					e = new CancelEventArgs();
					this.OnStarting(e);

					if (!e.Cancel)
					{
						e = new CancelEventArgs();
						this.RaiseEventStarting(e);

						if (!e.Cancel)
						{
							this.Paused = false;
							this.Running = true;
							this.Stopwatch.Reset();
							this.CancellationTokenSource = new CancellationTokenSource();

							this.Thread = new Thread(new ParameterizedThreadStart(this.Process));
							this.Thread.Priority = this.ThreadPriority;
							this.Thread.IsBackground = this.IsBackground;
							this.Thread.SetApartmentState(this.ApartmentState);
							this.Thread.Start(this.CancellationTokenSource.Token);
						}
					}
				}
			}
		}

		public void Stop (TimeSpan? timeout = null)
		{
			this.Stop(true, timeout);
		}

		private void Stop (bool throwOnDisposed, TimeSpan? timeout = null)
		{
			this.Stop((object) new ThreadBaseStopParameters(throwOnDisposed, timeout));
		}

		private void Stop (object threadBaseStopParameters)
		{
			CancelEventArgs e = null;
			var parameters = threadBaseStopParameters as ThreadBaseStopParameters;

			lock (this._SyncRootThreadBase)
			{
				if ((parameters.ThrowOnDisposed) && (this.Disposed))
				{
					throw (new ObjectDisposedException(this.GetType().FullName));
				}

				if (this.Running)
				{
					e = new CancelEventArgs();
					try { this.OnStopping(e); }
					catch { }

					if (!e.Cancel)
					{
						e = new CancelEventArgs();
						try { this.RaiseEventStopping(e); }
						catch { }

						if (!e.Cancel)
						{
                           this.CancellationTokenSource.Cancel(throwOnFirstException: false);

							try
							{
								if (parameters.Timeout.HasValue) { this.Thread.Join(parameters.Timeout.Value); }
								else { this.Thread.Join(); }
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
								this.CancellationTokenSource.Dispose();
								this.CancellationTokenSource = null;
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
				this.OnProcess(token);
			}
			catch (OperationCanceledException)
			{
				// Do nothing.
			}
			catch (Exception e)
			{
				exception = e;

				if (Global.Instance.Debug) { throw; }
			}

			this.Stopwatch.Stop();

			new Thread(new ParameterizedThreadStart(this.Stop)) { Priority = ThreadPriority.Highest, IsBackground = false, }.Start(new ThreadBaseStopParameters(false, TimeSpan.FromSeconds(1)));
		}

		protected abstract void OnStarted ();
		protected abstract void OnStopped ();
		protected abstract void OnStarting (CancelEventArgs e);
		protected abstract void OnStopping (CancelEventArgs e);
		protected abstract void OnProcess (CancellationToken cancellationToken);

		public bool RequestCancellation ()
		{
			lock (this._SyncRootThreadBase)
			{
				if (this.Running)
				{
					if (this.CancellationTokenSource == null)
					{
						return (false);
					}
					else
					{
						if (this.CancellationTokenSource.IsCancellationRequested)
						{
							return (true);
						}
						else
						{
							try
							{
								this.CancellationTokenSource.Cancel(throwOnFirstException : false);

								return (true);
							}
							catch (AggregateException)
							{
								return (true);
							}
							catch
							{
								throw;
							}
						}
					}
				}
				else
				{
					return (false);
				}
			}
		}

		public void Pause ()
		{
			lock (this._SyncRootThreadBase)
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
			lock (this._SyncRootThreadBase)
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

		protected void WaitWhilePausedAndThrowOnCancellation (CancellationToken cancellationToken)
		{
			do
			{
				cancellationToken.ThrowIfCancellationRequested();

				Thread.Sleep(TimeSpan.FromSeconds(0.01));
			}
			while (this.Paused);

			cancellationToken.ThrowIfCancellationRequested();
		}

		#endregion Methods.

		#region Methods: Events.

		//====================================================================================================
		// Methods: Events.
		//====================================================================================================

		private void RaiseEventStarting (CancelEventArgs e)
		{
			this.Starting.SafeInvoke(this, e);
		}

		private void RaiseEventStarted ()
		{
			this.Started.SafeInvoke(this, new EventArgs());
		}

		private void RaiseEventStopping (CancelEventArgs e)
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

					this.Stop(true, TimeSpan.FromSeconds(1));
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion Interface Implementation: System.IDisposable.
	}
}