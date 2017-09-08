using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
	public partial class UserFolder:
		IEntity<UserFolder>
	{
		public virtual long Id { get; set; }

		public virtual User User { get; set; }
		public virtual long UserId { get; set; }
		public bool IsActive { get; set; }
		public virtual Folder Folder { get; set; }
		public virtual long FolderId { get; set; }

		public UserFolder ()
		{
		}

		public void Initialize ()
		{
		}

		public UserFolder Clone ()
		{
			return (new UserFolder().CopyFrom(this));
		}

		public UserFolder CopyTo (UserFolder destination)
		{
			return (destination.CopyFrom(this));
		}

		public UserFolder CopyFrom (UserFolder source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public UserFolder FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}