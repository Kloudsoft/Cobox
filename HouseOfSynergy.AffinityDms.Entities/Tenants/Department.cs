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
	public partial class Department:
		IEntity<Department>
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }

		private ICollection<User> _Users = null;
		public virtual ICollection<User> Users { get { if (this._Users == null) { this._Users = new List<User>(); } return (this._Users); } protected set { this._Users = value; } }

		private ICollection<Folder> _Folders = null;
		public virtual ICollection<Folder> Folders { get { if (this._Folders == null) { this._Folders = new List<Folder>(); } return (this._Folders); } protected set { this._Folders = value; } }

		public Department ()
		{
		}
	
		public void Initialize()
		{
		}

		public Department Clone ()
		{
			return (new Department().CopyFrom(this));
		}

		public Department CopyTo (Department destination)
		{
			return (destination.CopyFrom(this));
		}

		public Department CopyFrom (Department source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Department FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}