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
	public partial class Folder:
		IEntity<Folder>
	{
		public virtual long Id { get; set; }

		public virtual string Name { get; set; }
		public virtual FolderType FolderType { get; set; }
		public virtual DateTime DateTimeCreated { get; set; }
        public virtual DateTime? DateTimeModified { get; set; }

        public virtual User UserCreatedBy { get; set; }
        public virtual long UserCreatedById { get; set; }
        public virtual Folder Parent { get; set; }
		public virtual long? ParentId { get; set; }

		public virtual long? DepartmentId { get; set; }
		public virtual Department Department { get; set; }

		private ICollection<Folder> _Folders = null;
		private ICollection<Document> _Documents = null;
		private ICollection<UserFolder> _FolderUsers = null;

        /// <summary>
        /// The list of users with permissions for this folder.
        /// </summary>
		public virtual ICollection<UserFolder> FolderUsers { get { if (this._FolderUsers == null) { this._FolderUsers = new List<UserFolder>(); } return (this._FolderUsers); } protected set { this._FolderUsers = value; } }
        /// <summary>
        /// Child folders.
        /// </summary>
		public virtual ICollection<Folder> Folders { get { if (this._Folders == null) { this._Folders = new List<Folder>(); } return (this._Folders); } protected set { this._Folders = value; } }
        /// <summary>
        /// Child documents.
        /// </summary>
		public virtual ICollection<Document> Documents { get { if (this._Documents == null) { this._Documents = new List<Document>(); } return (this._Documents); } set { this._Documents = value; } }

		public Folder ()
		{
		}

		public Folder (string name)
			: this()
		{
			this.Name = name;
		}

		[NotMapped]
		public bool HasChildren { get { return ((this._Folders != null) && (this._Folders.Count > 0)); }  }
		[NotMapped]
		public bool CanContainFiles { get { return (new FolderType [] { FolderType.EnterpriseChild, FolderType.SharedChild, FolderType.UserChild, FolderType.Child, }.Contains(this.FolderType)); } }

		public string GetPath ()
		{
			var path = "";
			var folder = this;

			path = "/" + this.Name;

			while (folder.Parent != null)
			{
				folder = folder.Parent;

				path = "/" + folder.Name + path;
			}

			return (path);
		}

		public bool ContainsDocuments ()
		{
			var result = false;

			if (this.Documents.Any()) { result = true; }

			if (!result)
			{
				foreach (var folder in this.Folders)
				{
					if (folder.ContainsDocuments())
					{
						result = true;

						break;
					}
				}
			}

			return (result);
		}

		public static bool ConvertListToHeirarchy (List<Folder> folders, out List<Folder> hierarchy, out Exception exception)
		{
			var result = false;
			Action<Folder> setChildren = null;

			hierarchy = null;
			exception = null;

			try
			{
				setChildren = parent =>
				{
					var list
						= folders
						.Where(childItem => childItem.ParentId == parent.Id)
						.ToList();

					foreach (var item in list)
					{
						item.Parent = parent;
						parent.Folders.Add(item);
					}

					// Recursively call the setChildren method for each child.
					parent.Folders
						.ToList()
						.ForEach(setChildren);
                };

				// Initialize the hierarchical list to root level items.
				List<Folder> hierarchicalItems
					= folders
					.Where(rootItem => rootItem.ParentId == null)
					.ToList();

				// Call the SetChildren method to set the children on each root level item.
				hierarchicalItems.ForEach(setChildren);

				result = true;
				hierarchy = hierarchicalItems;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public void Initialize ()
		{
		}

		public Folder Clone ()
		{
			return (new Folder().CopyFrom(this));
		}

		public Folder CopyTo (Folder destination)
		{
			return (destination.CopyFrom(this));
		}

		public Folder CopyFrom (Folder source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Folder FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}