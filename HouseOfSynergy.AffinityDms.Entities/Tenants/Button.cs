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
	public partial class Button:
		IEntity<Button>
	{
		public virtual long Id { get; set; }
		public virtual long ScreenId { get; set; }
        public virtual Screen Screen { get; set; }
        public virtual string Name { get; set; }

		private ICollection<RoleRight> _RoleRights = null;
		public virtual ICollection<RoleRight> RoleRights { get { if (this._RoleRights == null) { this._RoleRights = new List<RoleRight>(); } return (this._RoleRights); } protected set { this._RoleRights = value; } }

		public Button ()
		{
		}
	
		public void Initialize()
		{
		}

		public Button Clone()
		{
			return (new Button().CopyFrom(this));
		}

		public Button CopyTo(Button destination)
		{
			return (destination.CopyFrom(this));
		}

		public Button CopyFrom(Button source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Button FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}