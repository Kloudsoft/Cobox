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
    public partial class UserInvite :
		IEntity<UserInvite>
	{
		public virtual long Id { get; set; }

        public virtual long InviterUserId {get;set;}
        public virtual User User {get;set;}

		public virtual long InviteeUserId { get; set; }

		public virtual string URL { get; set; }
		public virtual DateTime URLExpiryDate { get; set; }
		public virtual DateTime URLCreatedDate { get; set; }
		public virtual bool IsActive { get; set; }


		public UserInvite ()
		{
		
		}
	
		public void Initialize()
		{
		}

		public UserInvite Clone()
		{
			return (new UserInvite().CopyFrom(this));
		}

		public UserInvite CopyTo(UserInvite destination)
		{
			return (destination.CopyFrom(this));
		}

		public UserInvite CopyFrom(UserInvite source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public UserInvite FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}