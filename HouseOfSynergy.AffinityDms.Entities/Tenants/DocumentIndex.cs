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
	public partial class DocumentIndex :
		IEntity<DocumentIndex>
	{
		public virtual long Id { get; set; }

		public virtual Document Document { get; set; }
		public virtual long DocumentId { get; set; }

		/// <summary>
		/// Size of the file in bytes.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// The SHA512 computed hash of the file.
		/// </summary>
		public virtual string Value { get; set; }

		/// <summary>
		/// Determines what type of data this index holds (for validation).
		/// </summary>
		public virtual DocumentIndexDataType DataType { get; set; }


        public DocumentIndex ()
		{
		}

		public void Initialize ()
		{
		}

		public DocumentIndex Clone ()
		{
			return (new DocumentIndex().CopyFrom(this));
		}

		public DocumentIndex CopyTo (DocumentIndex destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentIndex CopyFrom (DocumentIndex source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentIndex FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}