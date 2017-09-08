using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Containers;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Utilities
{
	public static class DocumentUtilities
	{
		public static ReadOnlyCollection<FileContainer> AllowedFileTypes { get; private set; }
		public static ReadOnlyCollection<FileContainer> AllowedImageTypes { get; private set; }
		public static ReadOnlyCollection<FileContainer> AllowedDocumentTypes { get; private set; }

		public static ReadOnlyDictionary<FileType, ReadOnlyCollection<FileFormatType>> FileTypeFileFormats { get; private set; }
		public static ReadOnlyDictionary<FileFormatType, FileType> FileFormatFileTypes { get; private set; }

		static DocumentUtilities ()
		{
			var fileTypeFileFormats = new Dictionary<FileType, ReadOnlyCollection<FileFormatType>>();
			var fileFormatFileTypes = new Dictionary<FileFormatType, FileType>();

			fileFormatFileTypes.Add(FileFormatType.None, FileType.None);
			fileFormatFileTypes.Add(FileFormatType.Bmp, FileType.Image);
			fileFormatFileTypes.Add(FileFormatType.Png, FileType.Image);
			fileFormatFileTypes.Add(FileFormatType.Jpg, FileType.Image);
			fileFormatFileTypes.Add(FileFormatType.Tiff, FileType.Image);
			fileFormatFileTypes.Add(FileFormatType.Pdf, FileType.Document);
			fileFormatFileTypes.Add(FileFormatType.Unknown, FileType.Unknown);

			var fileTypes = EnumUtilities.GetValues<FileType>();

			foreach (var fileType in fileTypes)
			{
				fileTypeFileFormats.Add(fileType, fileFormatFileTypes.Where(p => p.Value == fileType).ToList().ConvertAll(p => p.Key).AsReadOnly());
			}

			DocumentUtilities.FileTypeFileFormats = new ReadOnlyDictionary<FileType, ReadOnlyCollection<FileFormatType>>(fileTypeFileFormats);
			DocumentUtilities.FileFormatFileTypes = new ReadOnlyDictionary<FileFormatType, FileType>(fileFormatFileTypes);

			DocumentUtilities.AllowedImageTypes = new ReadOnlyCollection<FileContainer>
			(
				new FileContainer []
				{
					new FileContainer(DocumentUtilities.FileFormatFileTypes[FileFormatType.Bmp], FileFormatType.Bmp, "Bitmap", "", "bmp"),
					new FileContainer(DocumentUtilities.FileFormatFileTypes[FileFormatType.Png], FileFormatType.Png, "Portable Network Graphics", "", "png"),
					new FileContainer(DocumentUtilities.FileFormatFileTypes[FileFormatType.Jpg], FileFormatType.Jpg, "Joint Photographic Experts Group", "", "jpg", "jpeg"),
					new FileContainer(DocumentUtilities.FileFormatFileTypes[FileFormatType.Tiff], FileFormatType.Tiff, "Tagged Image File Format", "", "tiff"),
				}
			);

			DocumentUtilities.AllowedDocumentTypes = new ReadOnlyCollection<FileContainer>
			(
				new FileContainer []
				{
					new FileContainer(DocumentUtilities.FileFormatFileTypes[FileFormatType.Pdf], FileFormatType.Pdf, "Portable Document Format", "", "pdf"),
				}
			);

			DocumentUtilities.AllowedFileTypes = DocumentUtilities.AllowedImageTypes.Concat(DocumentUtilities.AllowedDocumentTypes).ToList().AsReadOnly();
		}

		public static FileType GetFileType (FileInfo fileInfo)
		{
			var allowedFileType = DocumentUtilities.AllowedFileTypes.SingleOrDefault
			(
				f => f.Extensions.Contains(DocumentUtilities.GetFileExtension(fileInfo))
			);

			return (allowedFileType == null ? FileType.None : allowedFileType.FileType);
		}

		public static FileFormatType GetFileFormatType (FileInfo fileInfo)
		{
			var allowedFileType = DocumentUtilities.AllowedFileTypes.SingleOrDefault
			(
				f => f.Extensions.Contains(DocumentUtilities.GetFileExtension(fileInfo))
			);

			return (allowedFileType == null ? FileFormatType.None : allowedFileType.FileFormatType);
		}

		public static string GetFileExtension (FileInfo fileInfo) { return (fileInfo.Extension.TrimStart('.').ToLower()); }

		public static FileType GetFileType (string filename) { return (DocumentUtilities.GetFileType(new FileInfo(filename))); }
		public static FileFormatType GetFileFormatType (string filename) { return (DocumentUtilities.GetFileFormatType(new FileInfo(filename))); }

		public static FileContainer GetFileContainer (string filename) { return (DocumentUtilities.GetFileContainer(new FileInfo(filename))); }
		public static FileContainer GetFileContainer (FileInfo fileInfo) { return (DocumentUtilities.AllowedFileTypes.SingleOrDefault(f => f.Extensions.Contains(fileInfo.Extension.TrimStart('.').ToLower()))); }

		public static bool IsFileTypeSupported (string filename) { return (DocumentUtilities.IsFileTypeSupported(new FileInfo(filename))); }
		public static bool IsFileTypeSupported (FileInfo fileInfo) { var fileType = DocumentUtilities.GetFileType(fileInfo); return ((fileType != FileType.None) && (fileType != FileType.Unknown)); }
		public static bool IsFileFormatTypeSupported (string filename) { return (DocumentUtilities.IsFileFormatTypeSupported(new FileInfo(filename))); }
		public static bool IsFileFormatTypeSupported (FileInfo fileInfo) { var fileFormatType = DocumentUtilities.GetFileFormatType(fileInfo); return ((fileFormatType != FileFormatType.None) && (fileFormatType != FileFormatType.Unknown)); }

		public static bool IsFileTypeImage (string filename) { return (DocumentUtilities.IsFileTypeImage(new FileInfo(filename))); }
		public static bool IsFileTypeImage (FileInfo fileInfo) { return (DocumentUtilities.GetFileType(fileInfo) == FileType.Image); }
		public static bool IsFileTypeImage (FileFormatType fileFormatType) { return (DocumentUtilities.FileFormatFileTypes [fileFormatType] == FileType.Image); }
		public static bool IsFileTypeDocument (string filename) { return (DocumentUtilities.IsFileTypeDocument(new FileInfo(filename))); }
		public static bool IsFileTypeDocument (FileInfo fileInfo) { return (DocumentUtilities.GetFileType(fileInfo) == FileType.Document); }
		public static bool IsFileTypeDocument (FileFormatType fileFormatType) { return (DocumentUtilities.FileFormatFileTypes [fileFormatType] == FileType.Document); }

		//public static bool IsFileFormatXXXXX (string filename) { DocumentUtilities.XXXXX(filename); }
		//public static bool IsFileFormatXXXXX (FileInfo fileInfo) { return (DocumentUtilities.GetFileFormatType(fileInfo) == FileFormatType.XXXXX); }
	}
}