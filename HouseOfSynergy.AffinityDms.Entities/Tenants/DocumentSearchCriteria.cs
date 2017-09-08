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
	public partial class DocumentSearchCriteria:
		IEntity<DocumentSearchCriteria>
	{
		public virtual long Id { get; set; }

		/// <summary>
		/// The name of the search so the user can retrieve it later.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Id of the user who created the search.
		/// </summary>
		public virtual long UserId { get; set; }
		public virtual User User { get; set; }

		/// <summary>
		/// Values should be limited from 2000-01-01 to DateTime.Now.
		/// </summary>
		public virtual DateTime? DateTimeFrom { get; set; }

		/// <summary>
		/// Values should be limited from 2000-01-01 to DateTime.Now.
		/// </summary>
		public virtual DateTime? DateTimeUpTo { get; set; }

		/// <summary>
		/// User may enter multiple words in a single text field separated by space.
		/// </summary>
		public virtual string TagsUser { get; set; }

		/// <summary>
		/// User may enter multiple words in a single text field separated by space.
		/// </summary>
		public virtual string TagsGlobal { get; set; }

		/// <summary>
		/// User may enter multiple words in a single text field separated by space.
		/// </summary>
		public virtual string Content { get; set; }

		/// <summary>
		/// User may enter multiple words in a single text field separated by space.
		/// </summary>
		public virtual string Filename { get; set; }

		/// <summary>
		/// User may enter multiple words in a single text field separated by space.
		/// </summary>
		public virtual string FolderName { get; set; }

		/// <summary>
		/// User may enter multiple words in a single text field separated by space.
		/// </summary>
		public virtual string TemplateName { get; set; }

		public DocumentSearchCriteria ()
		{
		}
	
		public void Initialize()
		{
		}

		public DocumentSearchCriteria Clone()
		{
			return (new DocumentSearchCriteria().CopyFrom(this));
		}

		public DocumentSearchCriteria CopyTo(DocumentSearchCriteria destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentSearchCriteria CopyFrom(DocumentSearchCriteria source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentSearchCriteria FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}