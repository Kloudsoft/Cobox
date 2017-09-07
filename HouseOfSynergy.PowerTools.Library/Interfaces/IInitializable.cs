namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IInitializable
	{
		/// <summary>
		/// Represents a method that initializes POCO members to defaults.
		/// Used in many other interfaces throughout the framework such as IPersistXml, etc.
		/// </summary>
		void Initialize ();
	}
}