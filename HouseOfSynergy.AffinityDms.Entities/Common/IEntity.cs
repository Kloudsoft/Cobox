using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
	public interface IEntity
	{
		/// <summary>
		/// Unique system generated identity.
		/// </summary>
		long Id { get; set; }

		/// <summary>
		/// System generated row version.
		/// </summary>
		//byte [] TimeStamp { get; set; }

		/// <summary>
		/// System generated creation time of row.
		/// </summary>
		//DateTime DateTimeCreated { get; set; }

		/// <summary>
		/// System generated creation time of row.
		/// </summary>
		//DateTime DateTimeModified { get; set; }
	}

	public interface IEntity<T>:
		IEntity,
		IPersistXmlElementTo,
		IPersistXmlElement<T>,
		ICloneable<T>,
		ICopyable<T>,
		IInitializable
		where T: IEntity<T>
	{
	}
}