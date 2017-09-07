using System;
using System.Linq;
using System.Reflection;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public abstract class SingletonDisposable<T>:
		Disposable
		where T: SingletonDisposable<T>
	{
		private bool Disposed { get; set; }

		protected SingletonDisposable ()
		{
			SingletonDisposable<T>.ThrowOnInCompatibleImplementation();
		}

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		private static readonly T _Instance = null;

		static SingletonDisposable ()
		{
			SingletonDisposable<T>.ThrowOnInCompatibleImplementation();

			SingletonDisposable<T>._Instance = (T) Activator.CreateInstance(type : typeof(T), nonPublic : true);
		}

		public static T Instance
		{
			get
			{
				if (SingletonDisposable<T>._Instance.Disposed)
				{
					throw (new ObjectDisposedException(typeof(T).FullName));
				}

				return (SingletonDisposable<T>._Instance);
			}
		}

		private static void ThrowOnInCompatibleImplementation ()
		{
			if (!typeof(T).IsSealed)
			{
				// Force derived classes to be sealed.
				throw (new InvalidOperationException("Classes derived from [SingletonDisposable<T>] must be sealed."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Static | BindingFlags.NonPublic).Any())
			{
				// Disallow derived classes to implement static constructors.
				throw (new InvalidOperationException("Classes derived from [SingletonDisposable<T>] must not have static constructors."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public).Any())
			{
				// Disallow derived classes to implement instance public constructors.
				throw (new InvalidOperationException("Classes derived from [SingletonDisposable<T>] must not have public constructors."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Any(ctor => !ctor.IsPrivate))
			{
				// Disallow derived classes to implement instance protected constructors.
				throw (new InvalidOperationException("Classes derived from [SingletonDisposable<T>] must not have protected constructors."));
			}

			if (!typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Any())
			{
				// Force derived classes to implement a private parameterless constructor.
				throw (new InvalidOperationException("Classes derived from [SingletonDisposable<T>] must have a private parameterless constructor."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Any(ctor => ctor.GetParameters().Length != 0))
			{
				// Force derived classes to implement a private parameterless constructor.
				throw (new InvalidOperationException("Classes derived from [SingletonDisposable<T>] must have a private parameterless constructor."));
			}
		}
	}
}