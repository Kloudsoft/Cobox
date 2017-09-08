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
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
	public partial class Screen:
		IEntity<Screen>
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }

        public virtual Screen Parent { get; set; }
        public virtual long? ParentId { get; set; }

		private ICollection<Screen> _Screens = null;
		public virtual ICollection<Screen> Screens { get { if (this._Screens == null) { this._Screens = new List<Screen>(); } return (this._Screens); } protected set { this._Screens = value; } }
		private ICollection<Button> _Buttons = null;
		public virtual ICollection<Button> Buttons { get { if (this._Buttons == null) { this._Buttons = new List<Button>(); } return (this._Buttons); } protected set { this._Buttons = value; } }
		private ICollection<RoleRight> _RoleRights = null;
		public virtual ICollection<RoleRight> RoleRights { get { if (this._RoleRights == null) { this._RoleRights = new List<RoleRight>(); } return (this._RoleRights); } protected set { this._RoleRights = value; } }

		public Screen ()
		{
		}
	
		public void Initialize()
		{
		}

		public Screen Clone()
		{
			return (new Screen().CopyFrom(this));
		}

		public Screen CopyTo(Screen destination)
		{
			return (destination.CopyFrom(this));
		}

		public Screen CopyFrom(Screen source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Screen FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}