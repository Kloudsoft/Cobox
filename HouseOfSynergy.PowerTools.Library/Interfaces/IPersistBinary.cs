using System.IO;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistBinary<T>:
		IInitializable
	{
		int Length { get; }

		/// <summary>
		/// Creates an binary representation of T.
		/// </summary>
		/// <param name="element">The [stream] to populate values to.</param>
		/// <returns>The populated instance of [stream] that was passed in as a paremeter for chained method syntax.</returns>
		BinaryWriter ToBinary (BinaryWriter writer);

		/// <summary>
		/// Initializes T and copies values from the provided [stream].
		/// </summary>
		/// <param name="element">The [stream] to populate values from.</param>
		/// <returns>Retuens a self reference for chanied syntax.</returns>
		T FromBinary (BinaryReader reader);
	}
}