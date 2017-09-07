using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.EventArguments;
using HouseOfSynergy.PowerTools.Library.Threading;

namespace HouseOfSynergy.PowerTools.Library.IO
{
	public class DirectoryCopier:
		ThreadBase
	{
		public event EventHandler<DirectoryCopierEventArgs> CopyStarted = null;
		public event EventHandler<DirectoryCopierEventArgs> CopyCalculated = null;
		public event EventHandler<DirectoryCopierProgressEventArgs> CopyProgress = null;
		public event EventHandler<DirectoryCopierEventArgs> CopyCompleted = null;
		public event EventHandler<DirectoryCopierEventArgs> CopyCancelled = null;
		public event EventHandler<DirectoryCopierErrorEventArgs> CopyError = null;

		private DirectoryInfo Source { get; set; }
		private DirectoryInfo Destination { get; set; }
		private List<Exception> _Exceptions { get; set; }
		private List<CopyEntry> _CopyEntries { get; set; }

		public DirectoryCopier (string source, string destination)
			: this(new DirectoryInfo(source), new DirectoryInfo(destination))
		{
		}

		public DirectoryCopier (DirectoryInfo source, DirectoryInfo destination)
		{
			if (source == null) { throw (new ArgumentNullException("source")); }
			if (destination == null) { throw (new ArgumentNullException("destination")); }

			source.Refresh();
			destination.Refresh();

			if (!source.Exists) { throw (new FileNotFoundException("The specified source directory does not exist (" + source.FullName + ").", source.FullName)); }
			if (destination.Exists) { throw (new IOException("The specified destination directory already exists (" + destination.FullName + ").")); }

			this.Source = new DirectoryInfo(source.FullName);
			this.Destination = new DirectoryInfo(destination.FullName);

			if (string.Compare(this.Source.FullName, this.Destination.FullName, true, CultureInfo.InvariantCulture) == 0)
			{
				throw (new Exception("The source and destination directories cannot be the same."));
			}

			this._Exceptions = new List<Exception>();
			this._CopyEntries = new List<CopyEntry>();
		}

		public ReadOnlyCollection<Exception> Exceptions { get { return (this._Exceptions.AsReadOnly()); } }
		public ReadOnlyCollection<CopyEntry> CopyEntries { get { return (this._CopyEntries.AsReadOnly()); } }

		protected override void OnStarted () { }
		protected override void OnStopped () { }
		protected override void OnStarting (CancelEventArgs e) { }
		protected override void OnStopping (CancelEventArgs e) { }

		protected sealed override void OnProcess (CancellationToken cancellationToken)
		{
			Exception exception = null;
			DirectorySnapshot snapshot = null;
			FileCopyEntry fileCopyEntry = null;
			DirectoryCopyEntry directoryCopyEntry = null;
			DirectoryCopierProgressEventArgs eventArgs = null;

			try
			{
				this.OnStart();

				snapshot = new DirectorySnapshot(this.Source);

				if (cancellationToken.IsCancellationRequested) { this.OnCancelled(); return; }

				foreach (DirectoryInfo source in snapshot.Directories)
				{
					this._CopyEntries.Add(new DirectoryCopyEntry(source, new DirectoryInfo(source.FullName.ToLower().Replace(this.Source.FullName.ToLower(), this.Destination.FullName))));
				}

				foreach (FileInfo source in snapshot.Files)
				{
					this._CopyEntries.Add(new FileCopyEntry(source, new FileInfo(source.FullName.ToLower().Replace(this.Source.FullName.ToLower(), this.Destination.FullName))));
				}

				if (cancellationToken.IsCancellationRequested) { this.OnCancelled(); return; }

				this.OnCalculated();

				foreach (CopyEntry entry in this._CopyEntries)
				{
					if (cancellationToken.IsCancellationRequested) { this.OnCancelled(); return; }

					eventArgs = new DirectoryCopierProgressEventArgs(this.Source, this.Destination, entry);

					try { this.OnProgress(eventArgs); if (eventArgs.SkipCopyEntry) { continue; } }
					catch (Exception e) { this._Exceptions.Add(e); this.OnError(e); break; }

					if (entry is DirectoryCopyEntry)
					{
						directoryCopyEntry = (DirectoryCopyEntry) entry;

						if (!directoryCopyEntry.Create()) { this.OnError(null); break; }
					}
					else if (entry is FileCopyEntry)
					{
						fileCopyEntry = (FileCopyEntry) entry;

						fileCopyEntry.Attempted = true;

						if (this.CopyFile(fileCopyEntry.Source, fileCopyEntry.Destination, cancellationToken, out exception))
						{
							fileCopyEntry.Completed = true;
						}
						else
						{
							fileCopyEntry.HasErrors = true;

							if (exception != null)
							{
								fileCopyEntry._Exceptions.Add(exception);
							}

							this.OnError(exception);

							break;
						}
					}
				}

				this.OnCompleted();
			}
			catch (Exception e)
			{
				this._Exceptions.Add(e);

				this.OnError(e);
			}
		}

		private bool CopyFile (FileInfo source, FileInfo destination, CancellationToken cancellationToken, out Exception exception)
		{
			int count = 0;
			bool result = false;
			byte [] buffer = null;

			exception = null;

			source.Refresh();
			destination.Refresh();

			buffer = new byte [8192];

			try
			{
				using (FileStream streamSource = source.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					using (FileStream streamDestination = destination.Create())
					{
						while (true)
						{
							if (cancellationToken.IsCancellationRequested)
							{
								exception = new Exception("Operation cancelled.");

								break;
							}
							else
							{
								count = streamSource.Read(buffer, 0, buffer.Length);

								if (count == 0)
								{
									if (streamSource.Position < streamSource.Length)
									{
										exception = new IOException("An unexpected error occured where data could not be read from the source file: (" + source.FullName + ").");

										break;
									}
									else
									{
										result = true;

										break;
									}
								}
								else
								{
									streamDestination.Write(buffer, 0, count);
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		private void OnStart ()
		{
			var instance = this.CopyStarted;

			try
			{
				if (instance != null)
				{
					instance(this, new DirectoryCopierEventArgs(this.Source, this.Destination));
				}
			}
			catch
			{
			}
		}

		private void OnCalculated ()
		{
			var instance = this.CopyCalculated;

			try
			{
				if (instance != null)
				{
					instance(this, new DirectoryCopierEventArgs(this.Source, this.Destination));
				}
			}
			catch
			{
			}
		}

		private void OnProgress (DirectoryCopierProgressEventArgs e)
		{
			var instance = this.CopyProgress;

			try
			{
				if (instance != null)
				{
					instance(this, e);
				}
			}
			catch
			{
			}
		}

		private void OnCompleted ()
		{
			var instance = this.CopyCompleted;

			try
			{
				if (instance != null)
				{
					instance(this, new DirectoryCopierEventArgs(this.Source, this.Destination));
				}
			}
			catch
			{
			}
		}

		private void OnCancelled ()
		{
			var instance = this.CopyCancelled;

			try
			{
				if (instance != null)
				{
					instance(this, new DirectoryCopierEventArgs(this.Source, this.Destination));
				}
			}
			catch
			{
			}
		}

		private void OnError (Exception exception)
		{
			var instance = this.CopyError;

			try
			{
				if (instance != null)
				{
					instance(this, new DirectoryCopierErrorEventArgs(this.Source, this.Destination, exception));
				}
			}
			catch
			{
			}
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}