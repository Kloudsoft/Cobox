using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	/// <summary>
	/// An interface to allow arbitrary creation of deep copies of objects designed for mutable.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ICopyable<T>:
		IInitializable,
		ICloneable<T>
		where T: ICopyable<T>
	{
		/// <summary>
		/// Performs a deep copy from source to destination.
		/// </summary>
		/// <param name="source">The source object to be copied from.</param>
		/// <returns>Returns [this] for easy method chaining.</returns>
		T CopyFrom (T source);

		/// <summary>
		/// Performs a deep copy from source to destination.
		/// </summary>
		/// <param name="destination">The destination object to be copied to.</param>
		/// <returns>Returns [destination] for easy method chaining.</returns>
		T CopyTo (T destination);
	}
}