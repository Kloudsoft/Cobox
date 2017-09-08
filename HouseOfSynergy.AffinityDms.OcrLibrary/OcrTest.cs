//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HouseOfSynergy.AffinityDms.Entities.Common;
//using HouseOfSynergy.AffinityDms.Entities.Tenants;
//using Leadtools;
//using Leadtools.Codecs;
//using Leadtools.Forms.DocumentWriters;
//using Leadtools.Forms.Ocr;

//namespace HouseOfSynergy.AffinityDms.OcrLibrary
//{
//	public class OcrTest
//	{
//		public static string LeadToolsLicenseFile { get; set; }
//		public static string LeadToolsLiceseKey { get; set; }
//		public static string LeadToolsDirectoryRuntime { get; set; }

//		public static void Initialize ()
//		{
//			RasterSupport.SetLicense (OcrTest.LeadToolsLicenseFile, OcrTest.LeadToolsLiceseKey);
//		}

//		public static void CreateTemplateFiles (Tenant tenant, Template template, FileInfo file, DirectoryInfo directoryTemp, List<TemplateElement> elements)
//		{
//			var engine = OcrEngineManager.CreateEngine (OcrEngineType.Advantage, false);

//			if (!engine.IsStarted)
//			{
//				var writer = new DocumentWriter ();

//				var filesTemporary = directoryTemp.GetFiles ();

//				foreach (var fileTemporary in filesTemporary)
//				{
//					try { fileTemporary.Delete (); }
//					catch { }
//				}

//				using (var rasterCodecs = new RasterCodecs ())
//				{
//					using (var codecsImageInfo = new CodecsImageInfo ())
//					{
//						rasterCodecs.Convert ("", "", RasterImageFormat.Png, 0, 0, 0, codecsImageInfo);

//						engine.Startup (rasterCodecs, writer, directoryTemp.FullName, OcrTest.LeadToolsDirectoryRuntime);
//					}
//				}
//			}
//		}
//	}
//}