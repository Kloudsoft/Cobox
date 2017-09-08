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
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Utilities;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Desktop
{
	public class DocumentEntry:
		IEntity<DocumentEntry>
	{
		public virtual long Id { get; set; }

		/// <summary>
		/// The full name of the file including its full path.
		/// </summary>
		private string _Filename = "";

		/// <summary>
		/// The full name of the file including its full path.
		/// </summary>
		public virtual string Filename
		{
			get
			{
				return (this._Filename);
			}
			set
			{
				this._Filename = value;

				if (string.IsNullOrWhiteSpace(this._Filename))
				{
					this.FileInfo = null;
					this.FileType = FileType.None;
					this.FileFormatType = FileFormatType.None;
				}
				else
				{
					try
					{
						this.FileInfo = new FileInfo(this._Filename);

						try { this.Name = this.FileInfo.Name; }
						catch { this.Name = ""; }

						try { this.PathLocal = this.FileInfo.Directory.FullName; }
						catch { this.PathLocal = ""; }

						try { this.FileType = DocumentUtilities.GetFileType(this.FileInfo); }
						catch { this.FileType = FileType.Unknown; }

						try { this.FileFormatType = DocumentUtilities.GetFileFormatType(this.FileInfo); }
						catch { this.FileFormatType = FileFormatType.Unknown; }
					}
					catch
					{
						this.FileInfo = null;
						this.FileType = FileType.Unknown;
						this.FileFormatType = FileFormatType.Unknown;
					}
				}
			}
		}

		/// <summary>
		/// The current state of document.
		/// </summary>
		public virtual DocumentEntryState DocumentEntryState { get; set; }

		/// <summary>
		/// A user-defined name for scanned documents.
		/// </summary>
		public virtual string NameScan { get; set; }
		/// <summary>
		/// The ordinal for display ordering.
		/// </summary>
		public virtual int Index { get; set; }
		/// <summary>
		/// The length of the file in bytes.
		/// </summary>
		public virtual long Size { get; set; }
		/// <summary>
		/// The hash of the file depending on the configured hashing algorithm.
		/// </summary>
		public virtual string Hash { get; set; }

		public virtual DateTime DateTimeCreated { get; set; }
		public virtual DateTime DateTimeLastAccessed { get; set; }
		public virtual DateTime DateTimeLastModified { get; set; }

		public virtual long? CloudDocumentId { get; set; }
		public virtual long? CloudFolderId { get; set; }
		public virtual string CloudFolderName { get; set; }

		public virtual long UserId { get; set; }
		public virtual long TenantId { get; set; }

		public virtual long? ScanSessionId { get; set; }
		public virtual ScanSession ScanSession { get; set; }

		public virtual int CountAttemptRead { get; set; }
		public virtual int CountAttemptQueue { get; set; }
		public virtual int CountAttemptUpload { get; set; }
		public virtual string AttemptHistory { get; set; }

		public DocumentEntry ()
		{
			this.Name = "";
			this.Index = 0;
			this.NameScan = "";
			this.PathLocal = "";
			this.CloudFolderId = null;
			this.CloudFolderName = "";
			this.CloudDocumentId = null;
			this.FileType = FileType.None;
			this.FileFormatType = FileFormatType.None;
			this.DocumentEntryState = DocumentEntryState.Imported;
		}

		public DocumentEntry (FileInfo file)
			: this()
		{
			this.Size = file.Length;
			this.Filename = file.FullName;
			this.DateTimeCreated = file.CreationTime;
			this.DateTimeLastAccessed = file.LastAccessTime;
			this.DateTimeLastModified = file.LastWriteTime;
		}

		public DocumentEntry (FileInfo file, int index)
			: this(file)
		{
			this.Index = index;
			this.Size = file.Length;
			this.Filename = file.FullName;
			this.DateTimeCreated = file.CreationTime;
			this.DateTimeLastAccessed = file.LastAccessTime;
			this.DateTimeLastModified = file.LastWriteTime;
		}

		/// <summary>
		/// Filename without the path.
		/// </summary>
		[NotMapped]
		public string Name { get; private set; }
		/// <summary>
		/// Path of the local folder.
		/// </summary>
		[NotMapped]
		public string PathLocal { get; private set; }

		[NotMapped]
		public FileInfo FileInfo { get; private set; }

		[NotMapped]
		public FileType FileType { get; private set; }

		[NotMapped]
		public FileFormatType FileFormatType { get; private set; }

		[NotMapped]
		public string DocumentEntryStateText
		{
			get
			{
				switch (this.DocumentEntryState)
				{
					case DocumentEntryState.None: { return ("None"); }
					case DocumentEntryState.Imported: { return ("Imported"); }
					case DocumentEntryState.Queued: { return ("Queued"); }
					case DocumentEntryState.Uploading: { return ("Uploading"); }
					case DocumentEntryState.Uploaded: { return ("Uploaded"); }
					default: { return ("Unknown"); }
				}
			}
		}

		public void CalculateHash (HashAlgorithm algorithm)
		{
			using (var stream = this.FileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				this.Hash = Convert.ToBase64String(algorithm.ComputeHash(stream));
			}
		}

		public void Initialize ()
		{
		}

		public DocumentEntry Clone ()
		{
			return (new DocumentEntry().CopyFrom(this));
		}

		public DocumentEntry CopyTo (DocumentEntry destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentEntry CopyFrom (DocumentEntry source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentEntry FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}