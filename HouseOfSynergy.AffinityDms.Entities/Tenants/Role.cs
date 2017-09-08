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

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
	public partial class Role:
		IEntity<Role>
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }

		public virtual TenantRoleType RoleType { get; set; }

		private ICollection<RoleRight> _RoleRights = null;
		public virtual ICollection<RoleRight> RoleRights { get { if (this._RoleRights == null) { this._RoleRights = new List<RoleRight>(); } return (this._RoleRights); } protected set { this._RoleRights = value; } }
		private ICollection<UserRole> _UserRoles = null;
		public virtual ICollection<UserRole> UserRoles { get { if (this._UserRoles == null) { this._UserRoles = new List<UserRole>(); } return (this._UserRoles); } protected set { this._UserRoles = value; } }
		private ICollection<User> _Users = null;
		public virtual ICollection<User> Users { get { if (this._Users == null) { this._Users = new List<User>(); } return (this._Users); } protected set { this._Users = value; } }

        public Role()
        {
        }
	
		public void Initialize()
		{
		}

		public Role Clone()
		{
			return (new Role().CopyFrom(this));
		}

		public Role CopyTo(Role destination)
		{
			return (destination.CopyFrom(this));
		}

		public Role CopyFrom(Role source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Role FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}