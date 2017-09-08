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
	public partial class TableHistory:
		IEntity<TableHistory>
	{
		public virtual long Id { get; set; }

		public virtual string TableName { get; set; }
        public virtual string Action { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual string RowId { get; set; }
        public virtual string DataType { get; set; }
        public virtual string ValueOldBinary { get; set; }
        public virtual string ValueOldText { get; set; }
        public virtual string ValueOldGeographic { get; set; }
        public virtual string ValueNewBinary { get; set; }
        public virtual string ValueNewGeographic { get; set; }
        public virtual string ValueNewText { get; set; }

		public TableHistory ()
		{
		}
	
		public void Initialize()
		{
		}

		public TableHistory Clone()
		{
			return (new TableHistory().CopyFrom(this));
		}

		public TableHistory CopyTo(TableHistory destination)
		{
			return (destination.CopyFrom(this));
		}

		public TableHistory CopyFrom(TableHistory source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TableHistory FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}