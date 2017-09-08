using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Desktop
{
	public class ScanSession:
		IEntity<ScanSession>,
		IPersistXmlElement<ScanSession>,
		ICopyable<ScanSession>
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual DateTime DateTimeCreated { get; set; }

		public virtual Guid Guid { get; set; }
		public virtual bool Finalized { get; set; }

		public virtual long UserId { get; set; }
		public virtual long TenantId { get; set; }
		public virtual long? CloudScanSessionId { get; set; }

        private ICollection<DocumentEntry> _DocumentEntries = null;
		public virtual ICollection<DocumentEntry> DocumentEntries { get { if (this._DocumentEntries == null) { this._DocumentEntries = new List<DocumentEntry>(); } return (this._DocumentEntries); } protected set { this._DocumentEntries = value; } }

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

		public override string ToString ()
		{
			return (this.Name);
		}
	}
}