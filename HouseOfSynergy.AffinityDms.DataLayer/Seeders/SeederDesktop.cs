using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Desktop;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.DataLayer.Seeders
{
	public static class SeederDesktop
	{
		public static void Seed (ContextDesktop context)
		{
			try
			{
				//====================================================================================================
				#region Culture Creation.
				//====================================================================================================

				{
					var cultureInfos = CultureInfo
						.GetCultures(CultureTypes.AllCultures)
						.Where
						(
							c =>
							(
								(c.Name == "en")
								|| (c.Name == "en-US")
								|| (c.Name == "en-GB")
								|| (c.Name == "zh-Hans")
								|| (c.Name == "zh-Hant")
								|| (c.Name == "zh-SG")
								|| (c.Name == "zh-TW")
								|| (c.Name == "ur")
								|| (c.Name == "ur-PK")
							)
						)
						.OrderBy(c => (c.Name != "en"))
						.ThenBy(c => (c.Name != "en-US"))
						.ThenBy(c => (c.Name != "en-GB"))
						.ThenBy(c => (c.Name != "zh-Hans"))
						.ThenBy(c => (c.Name != "zh-Hant"))
						.ThenBy(c => (c.Name != "zh-SG"))
						.ThenBy(c => (c.Name != "zh-TW"))
						.ThenBy(c => (c.Name != "ur"))
						.ThenBy(c => (c.Name != "ur-PK"))
						.ToList();

					foreach (var cultureInfo in cultureInfos)
					{
						var culture = new Culture();

						culture.Name = cultureInfo.Name;
						culture.LocaleId = cultureInfo.LCID;
						culture.NameNative = cultureInfo.NativeName;
						culture.NameDisplay = cultureInfo.DisplayName;
						culture.NameEnglish = cultureInfo.EnglishName;
						culture.NameIsoTwoLetter = cultureInfo.TwoLetterISOLanguageName;
						culture.NameIsoThreeLetter = cultureInfo.ThreeLetterISOLanguageName;
						culture.NameWindowsThreeLetter = cultureInfo.ThreeLetterWindowsLanguageName;

						context.Cultures.Add(culture);
					}

					context.SaveChanges();
				}

				//====================================================================================================
				#endregion Culture Creation.
				//====================================================================================================

				//====================================================================================================
				#region Document Creation.
				//====================================================================================================

				//{
				//	var count = 0;
				//	var index = 0;
				//	var extension = "*.*";
				//	var random = new Random();
				//	var list = new List<FileInfo>();
				//	IEnumerable<FileInfo> files = null;
				//	var searchOption = SearchOption.AllDirectories;
				//	var directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

				//	count = 0;
				//	extension = "*.bmp";
				//	searchOption = SearchOption.AllDirectories;
				//	directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				//	files = directory.EnumerateFiles(extension, searchOption);
				//	foreach (var file in files) { try { count++; list.Add(file); if (count >= 100) { break; } } catch { } }

				//	count = 0;
				//	extension = "*.png";
				//	searchOption = SearchOption.AllDirectories;
				//	directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				//	files = directory.EnumerateFiles(extension, searchOption);
				//	foreach (var file in files) { try { count++; list.Add(file); if (count >= 100) { break; } } catch { } }

				//	count = 0;
				//	extension = "*.jpg";
				//	searchOption = SearchOption.AllDirectories;
				//	directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				//	files = directory.EnumerateFiles(extension, searchOption);
				//	foreach (var file in files) { try { count++; list.Add(file); if (count >= 100) { break; } } catch { } }

				//	count = 0;
				//	extension = "*.pdf";
				//	searchOption = SearchOption.AllDirectories;
				//	directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				//	files = directory.EnumerateFiles(extension, searchOption);
				//	foreach (var file in files) { try { count++; list.Add(file); if (count >= 100) { break; } } catch { } }

				//	for (int i = 0; i < (list.Count * 10); i++)
				//	{
				//		var x = random.Next(0, list.Count);
				//		var y = random.Next(0, list.Count);

				//		var temp = list [x];
				//		list [x] = list [y];
				//		list [y] = temp;
				//	}

				//	using (var algorithm = HashAlgorithm.Create(GlobalConstants.AlgorithmHashName))
				//	{
				//		foreach (var file in list)
				//		{
				//			try
				//			{
				//				var documentEntry = new DocumentEntry();

				//				index++;
				//				file.Refresh();

				//				documentEntry.UserId = 1;
				//				documentEntry.TenantId = 1;
				//				documentEntry.Index = index;
				//				documentEntry.Size = file.Length;
				//				documentEntry.Filename = file.FullName;

				//				documentEntry.CloudDocumentId = null;
				//				documentEntry.DocumentEntryState = DocumentEntryState.Imported;

				//				documentEntry.CloudFolderId = null;
				//				documentEntry.CloudFolderName = null;

				//				documentEntry.DateTimeCreated = file.CreationTime;
				//				documentEntry.DateTimeLastAccessed = file.LastAccessTime;
				//				documentEntry.DateTimeLastModified = file.LastWriteTime;

				//				documentEntry.CalculateHash(algorithm);

				//				context.DocumentEntries.Add(documentEntry);
				//			}
				//			catch (Exception e)
				//			{
				//				//Debug.Print(e.ToString());
				//				Debugger.Break();

				//				continue;
				//			}
				//		}
				//	}

				//	context.SaveChanges();
				//}

				//====================================================================================================
				#endregion Document Creation.
				//====================================================================================================
			}
			catch (Exception exception)
			{
				if (AffinityConfiguration.IsConfigurationDebug)
				{
					Debugger.Break();

					if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
					{
						Debug.Print(exception.ToString());
					}
				}
				else
				{
					throw;
				}
			}
		}
	}
}