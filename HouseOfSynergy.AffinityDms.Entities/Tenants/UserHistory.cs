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
	public partial class UserHistory:
		IEntity<UserHistory>
	{
		public virtual long Id { get; set; }

        public virtual long UserId { get; set; }
        public virtual User User { get; set; }

        public virtual string FieldName { get; set; }
        public virtual string PreviousValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual string ModifiedBy { get; set; }

		public UserHistory ()
		{
		}
	
		public void Initialize()
		{
		}

		public UserHistory Clone()
		{
			return (new UserHistory().CopyFrom(this));
		}

		public UserHistory CopyTo(UserHistory destination)
		{
			return (destination.CopyFrom(this));
		}

		public UserHistory CopyFrom(UserHistory source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public UserHistory FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}