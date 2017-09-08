using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Master
{
	public partial class MasterRole:
		IEntity<MasterRole>
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }

		public virtual MasterRoleType RoleType { get; set; }

		private ICollection<MasterUser> _Users = null;
		private ICollection<MasterUserRole> _UserRoles = null;

		public virtual ICollection<MasterUser> Users { get { if (this._Users == null) { this._Users = new List<MasterUser>(); } return (this._Users); } protected set { this._Users = value; } }
		public virtual ICollection<MasterUserRole> UserRoles { get { if (this._UserRoles == null) { this._UserRoles = new List<MasterUserRole>(); } return (this._UserRoles); } protected set { this._UserRoles = value; } }

        public MasterRole()
        {
        }
	
		public void Initialize()
		{
		}

		public MasterRole Clone()
		{
			return (new MasterRole().CopyFrom(this));
		}

		public MasterRole CopyTo(MasterRole destination)
		{
			return (destination.CopyFrom(this));
		}

		public MasterRole CopyFrom(MasterRole source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public MasterRole FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}