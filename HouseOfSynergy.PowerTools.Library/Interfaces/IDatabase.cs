using System;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IDatabase:
		IInitializable
	{
		bool Save ();
		bool Load ();
		bool Save (out Exception exception);
		bool Load (out Exception exception);
	}
}