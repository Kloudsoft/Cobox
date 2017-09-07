using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public abstract class Disposable:
		IDisposable
	{
		private bool Disposed { get; set; }
		private Stack<object> _ComDisposableObjects { get; set; }
		private List<IDisposable> _DisposableObjects { get; set; }
		public ReadOnlyCollection<IDisposable> DisposableObjects { get; private set; }

		protected Disposable ()
		{
			this._ComDisposableObjects = new Stack<object>();
			this._DisposableObjects = new List<IDisposable>();
			this.DisposableObjects = new ReadOnlyCollection<IDisposable>(this._DisposableObjects);
		}

		~Disposable ()
		{
			this.Dispose(false);
		}

		public ReadOnlyCollection<object> ComObjects { get { return (this._ComDisposableObjects.ToList().AsReadOnly()); } }

		protected void AddDisposableObject (IDisposable obj)
		{
			this.ThrowOnDisposed();

			this._DisposableObjects.Add(obj);
		}

		protected void AddDisposableObjects (IEnumerable<IDisposable> objects)
		{
			this.ThrowOnDisposed();

			this._DisposableObjects.AddRange(objects);
		}

		protected T AddDisposableObject<T> (T obj)
			where T: IDisposable
		{
			this.ThrowOnDisposed();

			this._DisposableObjects.Add(obj);

			return (obj);
		}

		protected IEnumerable<T> AddDisposableObjects<T> (IEnumerable<T> objects)
			where T: IDisposable
		{
			this.ThrowOnDisposed();

			this._DisposableObjects.AddRange(objects.ToList().ConvertAll<IDisposable>(o => ((IDisposable) o)));

			return (objects);
		}

		protected object PushComDisposableObject (object obj)
		{
			this.ThrowOnDisposed();

			this._ComDisposableObjects.Push(obj);

			return (obj);
		}

		protected IEnumerable<object> PushComDisposableObjects (IEnumerable<object> objects)
		{
			this.ThrowOnDisposed();

			foreach (var item in objects)
			{
				this._ComDisposableObjects.Push(item);
			}

			return (objects);
		}

		protected T PushComDisposableObject<T> (T obj)
		{
			this.ThrowOnDisposed();

			this._ComDisposableObjects.Push(obj);

			return (obj);
		}

		protected IEnumerable<T> PushComDisposableObjects<T> (IEnumerable<T> objects)
		{
			this.ThrowOnDisposed();

			foreach (var item in objects)
			{
				this._ComDisposableObjects.Push(item);
			}

			return (objects);
		}

		protected void ThrowOnDisposed ()
		{
			if (this.Disposed)
			{
				throw (new ObjectDisposedException(this.GetType().FullName));
			}
		}

		public void Dispose ()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.

					try
					{
						for (int i = 0; i < this._DisposableObjects.Count; i++)
						{
							if (this._DisposableObjects [i] != null)
							{
								try { this._DisposableObjects [i].Dispose(); }
								finally { this._DisposableObjects [i] = null; }
							}
						}
					}
					finally
					{
						this._DisposableObjects.Clear();
					}

					try
					{
						while (this._ComDisposableObjects.Count > 0)
						{
							var item = this._ComDisposableObjects.Pop();

							if (item != null)
							{
								if (Marshal.IsComObject(item))
								{
									try { Marshal.ReleaseComObject(item); }
									finally { item = null; }
								}
							}
						}
					}
					finally
					{
						this._ComDisposableObjects.Clear();
					}
				}

				// Unmanaged.

				this.Disposed = true;
			}
		}
	}
}