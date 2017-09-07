namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	/// <summary>
	/// An interface to allow deep cloning of objects designed in mind for mutable as well as immutable types.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ICloneable<T>
		where T: ICloneable<T>
	{
		/// <summary>
		/// Creates a deep clone of the source object.
		/// </summary>
		/// <returns>Returns a deep clone of the source object.</returns>
		T Clone ();
	}
}