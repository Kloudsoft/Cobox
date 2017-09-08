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
	public partial class ScanSession:
		IEntity<ScanSession>
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual DateTime DateTimeCreated { get; set; }

		public virtual Guid Guid { get; set; }
		public virtual bool Finalized { get; set; }

		public virtual long UserId { get; set; }
		public virtual User User { get; set; }

		private ICollection<Document> _Documents = null;
		public virtual ICollection<Document> Documents { get { if (this._Documents == null) { this._Documents = new List<Document>(); } return (this._Documents); } protected set { this._Documents = value; } }

		public ScanSession ()
		{
		}

		public void Initialize ()
		{
		}

		public ScanSession Clone ()
		{
			return (new ScanSession().CopyFrom(this));
		}

		public ScanSession CopyTo (ScanSession destination)
		{
			return (destination.CopyFrom(this));
		}

		public ScanSession CopyFrom (ScanSession source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public ScanSession FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}