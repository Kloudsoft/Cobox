using System;
using System.Linq;
using System.Reflection;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public abstract class Singleton<T>
		where T: Singleton<T>
	{
		protected Singleton ()
		{
			Singleton<T>.ThrowOnInCompatibleImplementation();
		}

		private static readonly T _Instance = null;

		static Singleton ()
		{
			Singleton<T>.ThrowOnInCompatibleImplementation();

			Singleton<T>._Instance = (T) Activator.CreateInstance(type : typeof(T), nonPublic : true);
		}

		public static T Instance { get { return (Singleton<T>._Instance); } }

		private static void ThrowOnInCompatibleImplementation ()
		{
			if (!typeof(T).IsSealed)
			{
				// Force derived classes to be sealed.
				throw (new InvalidOperationException("Classes derived from [Singleton<T>] must be sealed."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Static | BindingFlags.NonPublic).Any())
			{
				// Disallow derived classes to implement static constructors.
				throw (new InvalidOperationException("Classes derived from [Singleton<T>] must not have static constructors."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public).Any())
			{
				// Disallow derived classes to implement instance public constructors.
				throw (new InvalidOperationException("Classes derived from [Singleton<T>] must not have public constructors."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Any(ctor => !ctor.IsPrivate))
			{
				// Disallow derived classes to implement instance protected constructors.
				throw (new InvalidOperationException("Classes derived from [Singleton<T>] must not have protected constructors."));
			}

			if (!typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Any())
			{
				// Force derived classes to implement a private parameterless constructor.
				throw (new InvalidOperationException("Classes derived from [Singleton<T>] must have a private parameterless constructor."));
			}

			if (typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Any(ctor => ctor.GetParameters().Length != 0))
			{
				// Force derived classes to implement a private parameterless constructor.
				throw (new InvalidOperationException("Classes derived from [Singleton<T>] must have a private parameterless constructor."));
			}
		}
	}
}