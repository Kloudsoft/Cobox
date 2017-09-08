using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
	public enum FolderType
	{
		/// <summary>
		/// None.
		/// </summary>
		None,

		/// <summary>
		/// The root folder representing the tenant's company name.
		/// </summary>
		Root,

		/// <summary>
		/// The enterprise folder that will contain departmental folders.
		/// </summary>
		EnterpriseRoot,

		/// <summary>
		/// A departmental folder directly under the [EnterpriseRoot] folder.
		/// </summary>
		EnterpriseChild,

		/// <summary>
		/// The shared root folder containing all shared folders.
		/// </summary>
		SharedRoot,

		/// <summary>
		/// A shared folder directly under the [SharedRoot] folder.
		/// </summary>
		SharedChild,

		/// <summary>
		/// The user's root folder containing all user folders.
		/// </summary>
		UserRoot,

		/// <summary>
		/// A user's folder directly under the [UserRoot] folder.
		/// </summary>
		UserChild,

		/// <summary>
		/// The user's root folder containing all user folders.
		/// </summary>
		ProjectRoot,

		/// <summary>
		/// A user's folder directly under the [UserRoot] folder.
		/// </summary>
		ProjectChild,

		/// <summary>
		/// A generic child folder.
		/// </summary>
		Child,
	}
}