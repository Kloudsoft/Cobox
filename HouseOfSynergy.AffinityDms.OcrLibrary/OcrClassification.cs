using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leadtools;
using Leadtools.ImageProcessing.Core;
using Leadtools.ImageProcessing;
using System.Drawing;
using Leadtools.Forms.Ocr;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using System.IO;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.PowerTools.Library.Utility;
using Microsoft.ApplicationInsights;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Log;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public class OcrClassification:
		Disposable
	{
		bool Disposed = false;
        //public bool PerformOCR(Image image,long documentid, out Exception exception) {
        //    exception = null;
        //    bool result = BeginClassification(image, documentid,out exception);
        //    return result;
        //}
        //public bool PerformOCR(Bitmap bitmap, long documentid, out Exception exception)
        //{
        //    exception = null;
        //    System.Drawing.Image image = bitmap;
        //    exception = null;
        //    bool result = BeginClassification(image, documentid, out exception);
        //    return result;
        //}



        /// <summary>
        /// OCR Process taking Image parameter as input 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="inputDocument"></param>
        /// <param name="document"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        //public bool PerformOCR(OcrEngineSettings ocrEngineSettings , Tenant tenant, Image image, Document inputDocument, List<Template> AllTemplates, List<TemplateElement> AllElements, out Document document, out List<DocumentFragment> documentfragments, out List<DocumentTemplate> documentTemplate, out Exception exception)
        //{
        //	documentfragments = null;
        //	document = null;
        //	documentTemplate = null;
        //	exception = null;
        //	bool result = BeginClassification(ocrEngineSettings ,tenant, image, inputDocument, AllTemplates, AllElements, out document, out documentfragments, out documentTemplate, out exception);
        //	return result;
        //}
        //============================================

        //============================================
        #region Begin Classsification Final New
        //============================================
        /// <summary>
        /// Start Classification on documents
        /// </summary>
        /// <param name="ocrEngineSettings"></param>
        /// <param name="tenant"></param>
        /// <param name="image"></param>
        /// <param name="inputDocument"></param>
        /// <param name="AllTemplates"></param>
        /// <param name="AllElements"></param>
        /// <param name="documentObj"></param>
        /// <param name="documentfragments"></param>
        /// <param name="documenttemplate"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public bool BeginOcrClassification (Global global, OcrEngineSettings ocrEngineSettings, Tenant tenant, Image image, Document inputDocument, List<Template> AllTemplates, out Document documentObj, out List<DocumentElement> documentElements, out List<DocumentFragment> documentfragments, out List<DocumentTemplate> documenttemplate, out Exception exception)
		{
			exception = null;
			bool result = false;
			bool returnedresult = false;
			string ocrxmlresult = "";
			string ocrtextresult = "";
			documentObj = inputDocument;
			documentElements = null;
			documentfragments = new List<DocumentFragment>();
			documenttemplate = new List<DocumentTemplate>();
			TemplateMatching templatematching = new TemplateMatching();
			try
			{
				#region Step #1 (Part A) Perform Full Text XML OCR
				RasterImage rasterimage = LeadToolsImageEnhancement.ConvertImageToRasterImage(image);
				templatematching.GetFullTextXMLOCR(ocrEngineSettings, rasterimage, out exception, out ocrxmlresult);
				if (exception != null)
				{
					throw exception;
				}
				#region Update XML data in Document table
				if (!(string.IsNullOrWhiteSpace(ocrxmlresult)))
				{
					documentObj.FullTextOCRXML = ocrxmlresult;
				}
				#endregion

				if (exception != null)
				{
					throw exception;
				}
				#region Step #1 (Part B) Perform Full Text OCR
				templatematching.GetFullTextOCR(ocrEngineSettings, rasterimage, out exception, out ocrtextresult);
				if (exception != null)
				{
					throw exception;
				}


				#region Save Data in Document Fragment

				string ocrdocxml = documentObj.FullTextOCRXML;
				List<string> strwords = HouseOfSynergy.PowerTools.Library.Utility.StringUtilities.BreakOcrXmlResult(ocrdocxml);
				foreach (var item in strwords)
				{
					var documentfragment = new DocumentFragment();
					documentfragment.FullTextOcr = item;
					documentfragment.DocumentId = inputDocument.Id;
					documentfragments.Add(documentfragment);
				}
				#endregion

				#region Get all template elements that has index values
				var templateelements = AllTemplates
					.Where(x => x.IsFinalized)
					.SelectMany(x => x.Elements)
					.Where(x => x.ElementIndexType == ElementIndexType.Label)
					.OrderBy(x => x.TemplateId)
					.ToList();

				if (exception != null)
				{
					throw exception;
				}
				#endregion
				#region Matching Document XML data for template elements index values
				var filteredtemplateelements = new List<TemplateElement>();
				if (inputDocument.DocumentQueueType == DocumentQueueType.Manual)
				{
					filteredtemplateelements = templateelements;
					returnedresult = true;
				}
				else
				{
					returnedresult = FindMatchingTemplate(inputDocument, templateelements, out filteredtemplateelements, out exception);
				}
				#endregion
				if (exception != null)
				{
					throw exception;
				}
				if (returnedresult)
				{
					#region Perform Matching of document by templates
					if (exception != null) { throw exception; }
					Document matcheddocument = null;
					result = templatematching.DoTemplateMatching(global, ocrEngineSettings, tenant, image, inputDocument, AllTemplates, filteredtemplateelements, out matcheddocument, out documentElements, out documenttemplate, out exception);
					documentObj.TemplateId = matcheddocument.TemplateId;
					documentObj.Confidence = matcheddocument.Confidence;
					documentObj.State = matcheddocument.State;
					if (exception != null)
					{
						throw exception;
					}
					if (result)
					{

					}
					#endregion
				}
				#endregion
				#endregion

				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				global.Logger.Write(ex.ToString());
				return result;
			}
		}

		//============================================
		#endregion Begin Classsification Final New
		//============================================

		public static bool FindMatchingTemplate (Document entitiesdocument, IReadOnlyCollection<TemplateElement> templatematched, out List<TemplateElement> templateelements, out Exception exception)
		{
			bool result = false;

			exception = null;
			templateelements = null;

			try
			{
				templateelements = (List<TemplateElement>) templatematched;
				// result &= FindMatchingTemplateImplementationOne(entitiesdocument, templatematched, out templateelements, out exception);
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}
		}

		public static bool FindMatchingTemplateImplementationOne (Document entitiesdocument, IReadOnlyCollection<TemplateElement> templatematched, out List<TemplateElement> templateelements, out Exception exception)
		{
			bool result = false;

			exception = null;
			templateelements = null;

			try
			{
				//foreach (var templateelement in templatematched)
				//{
				//    string[] splitedtemplatevalue = templateelement.Value.Split(' ');
				//    for (int i = 0; i < splitedtemplatevalue.Length; i++)
				//    {
				//        if (entitiesdocument.FullTextOCRXML.Contains(templateelement.Value))
				//        {
				//            templateelements.Add(templateelement);
				//        }
				//    }
				//}

				result = true;
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			return (result);
		}

		public static bool FindMatchingTemplateImplementationTwo (Document entitiesdocument, IReadOnlyCollection<TemplateElement> templatematched, out List<TemplateElement> templateelements, out Exception exception)
		{
			exception = null;
			templateelements = null;
			bool result = false;
			try
			{
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}

		}
		public static bool FindMatchingTemplateImplementationThree (Document entitiesdocument, IReadOnlyCollection<TemplateElement> templatematched, out List<TemplateElement> templateelements, out Exception exception)
		{
			exception = null;
			templateelements = null;
			bool result = false;
			try
			{
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}

		}
		public static bool FindMatchingTemplateImplementationFour (Document entitiesdocument, IReadOnlyCollection<string> templatematched, out List<TemplateElement> templateelements, out Exception exception)
		{
			exception = null;
			templateelements = null;
			bool result = false;
			try
			{
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}

		}





















		/**/
		//============================================
		#region Begin Classsification Final Old
		//============================================

		///// <summary>
		///// Start Classification on documents
		///// </summary>
		///// <param name="image"></param>
		///// <param name="inputDocument"></param>
		///// <param name="AllTemplates"></param>
		///// <param name="AllElements"></param>
		///// <param name="document"></param>
		///// <param name="exception"></param>
		///// <returns></returns>
		//private bool BeginClassification(string MainFilesDirPath, Tenant tenant, Image image, Document inputDocument, List<Template> AllTemplates, List<TemplateElement> AllElements, out Document documentObj, out List<DocumentFragment> documentfragments, out List<DocumentTemplate> documenttemplate, out Exception exception)
		//{
		//    exception = null;
		//    bool result = false;
		//    bool returnedresult = false;
		//    string ocrxmlresult = "";
		//    string ocrtextresult = "";
		//    documentObj = inputDocument;
		//    documentfragments = new List<DocumentFragment>();
		//    documenttemplate = new List<DocumentTemplate>();
		//    try
		//    {
		//        #region Step #1 (Part A) Perform Full Text XML OCR
		//        ocrxmlresult = FullXmlOCR(MainFilesDirPath, tenant, image, out exception);
		//        if (exception != null)
		//        {
		//            throw exception;
		//        }
		//        #region Update XML data in Document table
		//        if (!(string.IsNullOrWhiteSpace(ocrxmlresult)))
		//        {
		//            documentObj.FullTextOCRXML = ocrxmlresult;
		//        }
		//        #endregion

		//        if (exception != null)
		//        {
		//            throw exception;
		//        }
		//        #region Step #1 (Part B) Perform Full Text OCR

		//        ocrtextresult = FullTextOCR(MainFilesDirPath, tenant, image, out exception);
		//        if (exception != null)
		//        {
		//            throw exception;
		//        }


		//        #region Save Data in Document Fragment

		//        string ocrdocxml = documentObj.FullTextOCRXML;
		//        List<string> strwords = HouseOfSynergy.PowerTools.Library.Utility.StringUtilities.BreakOcrXmlResult(ocrdocxml);
		//        foreach (var item in strwords)
		//        {
		//            var documentfragment = new DocumentFragment();
		//            documentfragment.FullTextOcr = item;
		//            documentfragment.DocumentId = inputDocument.Id;
		//            documentfragments.Add(documentfragment);
		//        }
		//        #endregion

		//        #region Get all template elements that has index values
		//        List<TemplateElement> templateelements = AllElements.Where(x => x.ElementIndexType == 1).Where(x => x.Template.IsFinalized == true).OrderBy(x => x.TemplateId).Select(x => x).ToList();
		//        if (exception != null)
		//        {
		//            throw exception;
		//        }
		//        #endregion
		//        #region Matching Document XML data fro template elements index values
		//        var filteredtemplateelements = new List<TemplateElement>();
		//        returnedresult = FindMatchingTemplate(inputDocument, templateelements, out filteredtemplateelements, out exception);
		//        #endregion

		//        if (exception != null)
		//        {
		//            throw exception;
		//        }
		//        if (returnedresult)
		//        {
		//            #region Perform Matching of document by templates
		//            // string serverpath = HttpContext.Current.Server.MapPath("");
		//            LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(MainFilesDirPath, tenant.Id.ToString(), string.Empty, out exception);
		//            if (exception != null) { throw exception; }
		//            Document matcheddocument = null;


		//            result = leadtoolsOCR.DoTemplateMatching(image, inputDocument, AllTemplates, AllElements, filteredtemplateelements, out matcheddocument, out documenttemplate, out exception);
		//            documentObj.TemplateId = matcheddocument.TemplateId;
		//            documentObj.Confidence = matcheddocument.Confidence;
		//            documentObj.State = matcheddocument.State;
		//            if (exception != null)
		//            {
		//                throw exception;
		//            }
		//            if (result)
		//            {

		//            }
		//            #endregion
		//        }
		//        #endregion
		//        #endregion

		//        return result;
		//    }
		//    catch (Exception ex)
		//    {
		//        exception = ex;
		//        return result;
		//    }

		//}


		//============================================
		#endregion Begin Classsification Final Old
		//============================================
		/**/
























		/// <summary>
		/// OCR Process taking Image parameter as input 
		/// </summary>
		/// <param name="tenantUserSession"></param>
		/// <param name="image"></param>
		/// <param name="inputDocument"></param>
		/// <param name="document"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		public bool PerformOCR (TenantUserSession tenantUserSession, Image image, Document inputDocument, out Document document, out Exception exception)
		{
			exception = null;
			bool result = BeginClassification(tenantUserSession, image, inputDocument, out document, out exception);
			return result;
		}
		/////=========Commented Out For Now===================
		///// <summary>
		///// OCR Process overloaded function taking Bitmap parameter as input
		///// </summary>
		///// <param name="bitmap"></param>
		///// <param name="inputDocument"></param>
		///// <param name="document"></param>
		///// <param name="exception"></param>
		///// <returns></returns>
		//public bool PerformOCR (TenantUserSession tenantUserSession, Bitmap bitmap, Document inputDocument, out Document document, out Exception exception)
		//{
		//    exception = null;
		//    System.Drawing.Image image = bitmap;
		//    exception = null;
		//    bool result = BeginClassification(tenantUserSession, image, inputDocument, out document, out exception);
		//    return result;
		//}



		/// <summary>
		/// OCR Process taking Image parameter as input 
		/// </summary>
		/// <param name="tenantUserSession"></param>
		/// <param name="image"></param>
		/// <param name="inputDocument"></param>
		/// <param name="ocrresultinfo"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		public bool PerformOCR (TenantUserSession tenantUserSession, Image image, Document inputDocument, out OcrResultInfo ocrresultinfo, out Exception exception)
		{
			exception = null;
			ocrresultinfo = null;
			bool result = BeginClassification(tenantUserSession, image, inputDocument, out ocrresultinfo, out exception);
			ocrresultinfo.Image = image;
			return result;
		}
		/////=========Commented Out For Now===================
		///// <summary>
		///// OCR Process overloaded function taking Bitmap parameter as input
		///// </summary>
		///// <param name="bitmap"></param>
		///// <param name="inputDocument"></param>
		///// <param name="ocrresultinfo"></param>
		///// <param name="exception"></param>
		///// <returns></returns>
		//public bool PerformOCR(TenantUserSession tenantUserSession, Bitmap bitmap, Document inputDocument, out OcrResultInfo ocrresultinfo, out Exception exception)
		//{
		//    exception = null;
		//    System.Drawing.Image image = bitmap;
		//    exception = null;
		//    bool result = BeginClassification(tenantUserSession, image, inputDocument, out document, out exception);
		//    return result;
		//}











		/// <summary>
		/// Start Classification on documents
		/// </summary>
		/// <param name="image"></param>
		/// <param name="inputDocument"></param>
		/// <param name="document"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		private bool BeginClassification (TenantUserSession tenantUserSession, Image image, Document inputDocument, out Document document, out Exception exception)
		{
			exception = null;
			bool result = false;
			bool returnedresult = false;
			string ocrxmlresult = "";
			string ocrtextresult = "";
			document = null;
			try
			{
				#region Step #1 (Part A) Perform Full Text XML OCR
				ocrxmlresult = FullXmlOCR(image, out exception);
				if (exception != null)
				{
					throw exception;
				}
				if (!(string.IsNullOrWhiteSpace(ocrxmlresult)))
				{
					//Enties.Document document = new Enties.Document();
					//dbresult = BusinessLayer.DocumentManagement.GetDocumentById(documentid, out document, out exception);

					#region Update XML data in Document table
					inputDocument.FullTextOCRXML = ocrxmlresult;
					ocrxmlresult = "";
					returnedresult = DocumentManagement.UpdateDocument(tenantUserSession, inputDocument, out exception);
					#endregion

					if (exception != null)
					{
						throw exception;
					}
					if (returnedresult)
					{
						#region Step #1 (Part B) Perform Full Text OCR

						ocrtextresult = FullTextOCR(image, out exception);
						if (exception != null)
						{
							throw exception;
						}
						if (!(string.IsNullOrWhiteSpace(ocrtextresult)))
						{
							var documentfragments = new List<DocumentFragment>();

							#region Save Data in Document Fragment

							#region old code saving 999 character
							//if (ocrtextresult.ToString().Length > 999)
							//{

							//    //IEnumerable<char> chararr = ocrtextresult.Where(x => (x != '\n') || (x != '\r') || (x != '\t') || (x != '\b') || (x != '\0')).Select(x => x);

							//    //double TotalLoop = (Convert.ToDouble(ocrtextresult.Length) / 999.0);
							//    //double Counter = Math.Floor(TotalLoop);
							//    //int couunter = 0;
							//    //for (int i = 1; i <= Counter; i++)
							//    //{
							//    //    Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //    documentfragment.FullTextOcr = ocrtextresult.ToString().Substring(couunter, (i * 999));
							//    //    documentfragment.DocumentId = inputDocument.Id;
							//    //    documentfragments.Add(documentfragment);
							//    //    str +=  documentfragment.FullTextOcr;
							//    //    couunter = i * 999;
							//    //}
							//    //if (TotalLoop - Counter != 0)
							//    //{
							//    //    Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //    documentfragment.FullTextOcr = ocrtextresult.ToString().Substring(couunter);
							//    //    documentfragment.DocumentId = inputDocument.Id;
							//    //    documentfragments.Add(documentfragment);
							//    //}



							//    //int counter = 0;
							//    //while (counter < ocrtextresult.Length)
							//    //{
							//    //    if ((counter + 999) > ocrtextresult.Length)
							//    //    {
							//    //        int calcdifference = (((ocrtextresult.Length - counter) + counter));
							//    //        Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //        documentfragment.FullTextOcr = ocrtextresult.Substring(counter);//, calcdifference);
							//    //        documentfragment.DocumentId = inputDocument.Id;
							//    //        documentfragments.Add(documentfragment);
							//    //        counter = calcdifference;
							//    //    }
							//    //    else
							//    //    {
							//    //        Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //        documentfragment.FullTextOcr = ocrtextresult.Substring(counter, (counter + 999));
							//    //        documentfragment.DocumentId = inputDocument.Id;
							//    //        documentfragments.Add(documentfragment);
							//    //        counter += 999;
							//    //    }
							//    //}
							//}
							//else
							//{
							//    Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    documentfragment.FullTextOcr = ocrtextresult;
							//    documentfragment.DocumentId = inputDocument.Id;
							//    documentfragments.Add(documentfragment);
							//}
							#endregion

							List<string> strwords = HouseOfSynergy.PowerTools.Library.Utility.StringUtilities.BreakOcrXmlResult(inputDocument.FullTextOCRXML);
							foreach (var item in strwords)
							{
								var documentfragment = new DocumentFragment();
								documentfragment.FullTextOcr = item;
								documentfragment.DocumentId = inputDocument.Id;
								documentfragments.Add(documentfragment);
							}

							foreach (var documentfragment in documentfragments)
							{
								returnedresult = DocumentManagement.AddDocumentFragment(tenantUserSession, documentfragment, out exception);
								if (exception != null)
								{
									throw exception;
								}
							}

							#endregion

							#region Get all template elements that has index values
							List<TemplateElement> templateelements = null;
							returnedresult = ElementManagement.GetElementsContainingElementIndexType(tenantUserSession, out templateelements, out exception);
							if (exception != null)
							{
								throw exception;
							}
							#endregion
							//XmlDocument xmldoc = new XmlDocument();
							//xmldoc.LoadXml(ocrxmlresult);
							//XmlNodeList xmlnodes = xmldoc.GetElementsByTagName("zone");
							//if (dbresult) {
							//    foreach (var templateelement in templateelements)
							//    {
							//        //string[] splittemplateindexvalues = templateelement.Value.Split(' ');
							//        //for (int i = 0; i < splittemplateindexvalues.Length; i++)
							//        //{
							//        //}
							//    }
							//}

							//                                   IReadOnlyCollection<TemplateElement> templatematched = templateelements;

							#region Matching Document XML data fro template elements index values
							var filteredtemplateelements = new List<TemplateElement>();
							returnedresult = FindMatchingTemplate(inputDocument, templateelements, out filteredtemplateelements, out exception);
							#endregion

							if (exception != null)
							{
								throw exception;
							}
							if (returnedresult)
							{
								#region Perform Matching of document by templates
								string serverpath = HttpContext.Current.Server.MapPath("");
								LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(serverpath, tenantUserSession.Tenant.Id.ToString(), string.Empty, out exception);
								if (exception != null) { throw exception; }
								result = leadtoolsOCR.DoTemplateMatching(tenantUserSession, image, inputDocument.Id, filteredtemplateelements, out exception);
								if (exception != null)
								{
									throw exception;
								}
								if (result)
								{

								}
								#endregion
							}

						}
						else
						{
							// IF OCR TEXT RETURNS EMPTY STRING
						}
						#endregion

					}
				}
				else
				{
					// IF OCR XML RETURNS EMPTY STRING
				}

				#endregion

				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}

		}

		/// <summary>
		/// Start Classification on documents
		/// </summary>
		/// <param name="image"></param>
		/// <param name="inputDocument"></param>
		/// <param name="ocrresultinfo"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		private bool BeginClassification (TenantUserSession tenantUserSession, Image image, Document inputDocument, out OcrResultInfo ocrresultinfo, out Exception exception)
		{
			exception = null;
			bool result = false;
			bool returnedresult = false;
			string ocrxmlresult = "";
			string ocrtextresult = "";
			ocrresultinfo = null;
			try
			{
				ocrresultinfo = new OcrResultInfo();
				#region Step #1 (Part A) Perform Full Text XML OCR
				ocrxmlresult = FullXmlOCR(image, out exception);
				if (exception != null)
				{
					throw exception;
				}
				if (!(string.IsNullOrWhiteSpace(ocrxmlresult)))
				{
					//Enties.Document document = new Enties.Document();
					//dbresult = BusinessLayer.DocumentManagement.GetDocumentById(documentid, out document, out exception);
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(ocrxmlresult);
					StringWriter sw = new StringWriter();
					XmlTextWriter xw = new XmlTextWriter(sw);
					xmlDoc.WriteTo(xw);
					ocrresultinfo.OcrFullXmlData = sw.ToString();
					#region Update XML data in Document table
					inputDocument.FullTextOCRXML = ocrxmlresult;
					ocrxmlresult = "";
					returnedresult = DocumentManagement.UpdateDocument(tenantUserSession, inputDocument, out exception);
					#endregion

					if (exception != null)
					{
						throw exception;
					}
					if (returnedresult)
					{
						#region Step #1 (Part B) Perform Full Text OCR

						ocrtextresult = FullTextOCR(image, out exception);
						if (exception != null)
						{
							throw exception;
						}
						if (!(string.IsNullOrWhiteSpace(ocrtextresult)))
						{
							ocrresultinfo.OcrFullTextData = ocrtextresult;
							var documentfragments = new List<DocumentFragment>();

							#region Save Data in Document Fragment

							#region old code saving 999 character
							//if (ocrtextresult.ToString().Length > 999)
							//{

							//    //IEnumerable<char> chararr = ocrtextresult.Where(x => (x != '\n') || (x != '\r') || (x != '\t') || (x != '\b') || (x != '\0')).Select(x => x);

							//    //double TotalLoop = (Convert.ToDouble(ocrtextresult.Length) / 999.0);
							//    //double Counter = Math.Floor(TotalLoop);
							//    //int couunter = 0;
							//    //for (int i = 1; i <= Counter; i++)
							//    //{
							//    //    Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //    documentfragment.FullTextOcr = ocrtextresult.ToString().Substring(couunter, (i * 999));
							//    //    documentfragment.DocumentId = inputDocument.Id;
							//    //    documentfragments.Add(documentfragment);
							//    //    str +=  documentfragment.FullTextOcr;
							//    //    couunter = i * 999;
							//    //}
							//    //if (TotalLoop - Counter != 0)
							//    //{
							//    //    Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //    documentfragment.FullTextOcr = ocrtextresult.ToString().Substring(couunter);
							//    //    documentfragment.DocumentId = inputDocument.Id;
							//    //    documentfragments.Add(documentfragment);
							//    //}



							//    //int counter = 0;
							//    //while (counter < ocrtextresult.Length)
							//    //{
							//    //    if ((counter + 999) > ocrtextresult.Length)
							//    //    {
							//    //        int calcdifference = (((ocrtextresult.Length - counter) + counter));
							//    //        Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //        documentfragment.FullTextOcr = ocrtextresult.Substring(counter);//, calcdifference);
							//    //        documentfragment.DocumentId = inputDocument.Id;
							//    //        documentfragments.Add(documentfragment);
							//    //        counter = calcdifference;
							//    //    }
							//    //    else
							//    //    {
							//    //        Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    //        documentfragment.FullTextOcr = ocrtextresult.Substring(counter, (counter + 999));
							//    //        documentfragment.DocumentId = inputDocument.Id;
							//    //        documentfragments.Add(documentfragment);
							//    //        counter += 999;
							//    //    }
							//    //}
							//}
							//else
							//{
							//    Enties.DocumentFragment documentfragment = new Enties.DocumentFragment();
							//    documentfragment.FullTextOcr = ocrtextresult;
							//    documentfragment.DocumentId = inputDocument.Id;
							//    documentfragments.Add(documentfragment);
							//}
							#endregion

							List<string> strwords = HouseOfSynergy.PowerTools.Library.Utility.StringUtilities.BreakOcrXmlResult(inputDocument.FullTextOCRXML);
							foreach (var item in strwords)
							{
								var documentfragment = new DocumentFragment();
								documentfragment.FullTextOcr = item;
								documentfragment.DocumentId = inputDocument.Id;
								documentfragments.Add(documentfragment);
							}

							foreach (var documentfragment in documentfragments)
							{
								returnedresult = DocumentManagement.AddDocumentFragment(tenantUserSession, documentfragment, out exception);
								if (exception != null)
								{
									throw exception;
								}
							}

							#endregion

							#region Get all template elements that has index values
							List<TemplateElement> templateelements = null;
							returnedresult = ElementManagement.GetElementsContainingElementIndexType(tenantUserSession, out templateelements, out exception);
							if (exception != null)
							{
								throw exception;
							}
							#endregion
							//XmlDocument xmldoc = new XmlDocument();
							//xmldoc.LoadXml(ocrxmlresult);
							//XmlNodeList xmlnodes = xmldoc.GetElementsByTagName("zone");
							//if (dbresult) {
							//    foreach (var templateelement in templateelements)
							//    {
							//        //string[] splittemplateindexvalues = templateelement.Value.Split(' ');
							//        //for (int i = 0; i < splittemplateindexvalues.Length; i++)
							//        //{
							//        //}
							//    }
							//}

							//                                   IReadOnlyCollection<TemplateElement> templatematched = templateelements;

							#region Matching Document XML data fro template elements index values
							var filteredtemplateelements = new List<TemplateElement>();
							returnedresult = FindMatchingTemplate(inputDocument, templateelements, out filteredtemplateelements, out exception);
							#endregion

							if (exception != null)
							{
								throw exception;
							}
							if (returnedresult)
							{
								#region Perform Matching of document by templates
								string serverpath = HttpContext.Current.Server.MapPath("");
								LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(serverpath, tenantUserSession.Tenant.Id.ToString(), string.Empty, out exception);
								if (exception != null) { throw exception; }
								result = leadtoolsOCR.DoTemplateMatching(tenantUserSession, image, inputDocument.Id, filteredtemplateelements, ref ocrresultinfo, out exception);
								if (exception != null)
								{
									throw exception;
								}
								if (result)
								{

								}
								#endregion
							}

						}
						else
						{
							// IF OCR TEXT RETURNS EMPTY STRING
						}
						#endregion

					}
				}
				else
				{
					// IF OCR XML RETURNS EMPTY STRING
				}

				#endregion

				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}

		}

		private string FullXmlOCR (Image image, out Exception exception)
		{
			exception = null;
			string result = "";
			try
			{
				string serverpath = HttpContext.Current.Server.MapPath("~");
				LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(serverpath, string.Empty, string.Empty, out exception);
				if (exception != null) { throw exception; }
				RasterImage rasterImage = LeadToolsImageEnhancement.ConvertImageToRasterImage(image);
				bool resultreturned = leadtoolsOCR.GetFullTextXMLOCR(rasterImage, out exception, out result);
				if (!(rasterImage.IsDisposed))
				{
					rasterImage.Dispose();
				}
				if (exception != null)
				{
					throw exception;
				}
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}
		}
		private string FullTextOCR (Image image, out Exception exception)
		{
			exception = null;
			string result = "";
			try
			{
				string serverpath = HttpContext.Current.Server.MapPath("~");
				LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(serverpath, string.Empty, string.Empty, out exception);
				if (exception != null) { throw exception; }
				RasterImage rasterImage = LeadToolsImageEnhancement.ConvertImageToRasterImage(image);
				bool resultreturned = leadtoolsOCR.GetFullTextOCR(rasterImage, out exception, out result);
				if (!(rasterImage.IsDisposed))
				{
					rasterImage.Dispose();
				}
				if (exception != null)
				{
					throw exception;
				}
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}
		}
		private string FullXmlOCR (string MainFilesDirPath, Tenant tenant, Image image, out Exception exception)
		{
			exception = null;
			string result = "";
			try
			{
				LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(MainFilesDirPath, tenant.Id.ToString(), string.Empty, out exception);
				if (exception != null) { throw exception; }
				RasterImage rasterImage = LeadToolsImageEnhancement.ConvertImageToRasterImage(image);
				bool resultreturned = leadtoolsOCR.GetFullTextXMLOCR(rasterImage, out exception, out result);
				if (!(rasterImage.IsDisposed))
				{
					rasterImage.Dispose();
				}
				if (exception != null)
				{
					throw exception;
				}
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}
		}
		private string FullTextOCR (string MainFilesDirPath, Tenant tenant, Image image, out Exception exception)
		{
			exception = null;
			string result = "";
			try
			{
				LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(MainFilesDirPath, tenant.Id.ToString(), string.Empty, out exception);
				if (exception != null) { throw exception; }
				RasterImage rasterImage = LeadToolsImageEnhancement.ConvertImageToRasterImage(image);
				bool resultreturned = leadtoolsOCR.GetFullTextOCR(rasterImage, out exception, out result);
				if (!(rasterImage.IsDisposed))
				{
					rasterImage.Dispose();
				}
				if (exception != null)
				{
					throw exception;
				}
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}
		}

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// TODO: Release managed resources here
					// GC will automatically release Managed resources by calling the destructor,
					// but Dispose() need to release managed resources manually
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}
	}
}