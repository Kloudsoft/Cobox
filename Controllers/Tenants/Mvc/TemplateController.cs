using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
	public class TemplateController: Controller
	{
		#region Action Results
		/// <summary>
		/// View for creating form design
		/// </summary>
		/// <param name="id">Template id</param>
		/// <returns>Returns Form Design View</returns>
		[HttpGet]
		public ActionResult CreateFormDesign (int? id)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			try
			{
				long ID = 0;
				bool result = long.TryParse(id.ToString(), out ID);
				if (result)
				{

					Template template = null;
					result = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
					if (exception != null)
					{
						throw exception;
					}
					if (result)
					{
						if (template != null)
						{
							this.ViewBag.Id = id;
							return View();
						}
					}
				}
			}
			catch(Exception ex)
			{
                exception = ex;
			}
			return RedirectToAction("SignIn", "User");
			//string LeadtoolsTempDir = System.Configuration.ConfigurationManager.AppSettings["LeadtoolsTempDir"];
			//string LeadToolsTemplatesDir = System.Configuration.ConfigurationManager.AppSettings["LeadToolsTemplatesDir"];
			//DirectoryInfo directory = new DirectoryInfo(Server.MapPath(LeadtoolsTempDir));
			//foreach (FileInfo file in directory.GetFiles())
			//{
			//    file.CopyTo(Path.Combine(Server.MapPath(LeadToolsTemplatesDir), file.Name), true);
			//    file.Delete();
			//}

		}
        [HttpGet]
        public ActionResult EditFormDesign(int? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            bool result = false;
            try
            {
                if (id > 0)
                {
                    long ID = long.Parse(id.ToString());
                    Template template = null;
                    bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (dbresult && (template != null))
                    {
                        this.ViewBag.Finalized = template.IsFinalized;
                        this.ViewBag.DesignStatus = template.IsActive;
                        List<TemplateElement> elements = null;
                        dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                        if (elements != null)
                        {
                            this.ViewBag.Id = ID;
                            List<TemplateElement> elementslist = null;
                            result = GetListOfFormElements(elements, out elementslist, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                            if ((elementslist != null) && result)
                            {
                                return View(elementslist);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return RedirectToAction("TemplateListing");
        }
        [HttpGet]
        public ActionResult TemplateListing()
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            List<Template> templates = null;
            try
            {
                User u = new User();
                if (Message != string.Empty)
                {
                    this.ViewBag.Message = Message;
                    Message = string.Empty;
                }
                else
                {
                    Message = string.Empty;
                    this.ViewBag.Message = Message;
                }
                bool dbresult = ElementManagement.GetAllTemplates(tenantUserSession, out templates, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (templates != null)
                {
					return View("~/Views/Tenants/Templates/TemplateListing.cshtml", templates);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return View();
        }
        [HttpGet]
        public ActionResult TemplateDescription()
        {
            Exception exception = null;
            try
            {
                if (errormessage != string.Empty)
                {
                    this.ViewBag.ErrorMessage = errormessage;
                }
                else
                {
                    this.ViewBag.ErrorMessage = errormessage = string.Empty;
                }
                List<SelectListItem> templatetypeList = Enum.GetValues(typeof(TemplateType)).Cast<TemplateType>().Select(temptype => new SelectListItem
                {
                    Text = temptype.ToString(),
                    Value = ((int)temptype).ToString()
                }).ToList();
                this.ViewBag.TemplateTypeList = templatetypeList;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return View();
        }
        [HttpPost]
        public ActionResult TemplateDescription(Template template, HttpPostedFileBase file)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            long templateid = 0;
            try
            {
                if (template.TemplateType == TemplateType.Template)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            byte[] imageByte = null;
                            BinaryReader rdr = new BinaryReader(file.InputStream);
                            imageByte = rdr.ReadBytes((int)file.ContentLength);
                            template.TemplateImage = imageByte;
                        }
                        else
                        {
                            errormessage = "Unable to find an image";
                            return RedirectToAction("TemplateDescription");
                        }
                    }
                    else
                    {
                        errormessage = "Unable to find an image";
                        return RedirectToAction("TemplateDescription");
                    }
                }
                else if (template.TemplateType == TemplateType.Form)
                {
                    template.TemplateImage = null;
                }
                bool dbresult = ElementManagement.CreateTemplate(tenantUserSession, template, out templateid, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (templateid > 0 && (dbresult))
                {
                    this.ViewBag.ErrorMessage = errormessage = string.Empty;
                    if (template.TemplateType == TemplateType.Form)
                    {
                        return RedirectToAction("CreateFormDesign", new { id = templateid });
                    }
                    else if (template.TemplateType == TemplateType.Template)
                    {
                        return RedirectToAction("CreateTemplateDesign", new { id = templateid });
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = errormessage = ex.Message;
                return RedirectToAction("TemplateDescription");
            }
            this.ViewBag.ErrorMessage = errormessage = "Unable to Create Template";
            return RedirectToAction("TemplateDescription");

        }
		[HttpGet]
		public ActionResult EditTemplate (int? id)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			try
			{


				if (errormessage != string.Empty)
				{
					this.ViewBag.ErrorMessage = errormessage;
				}
				else
				{
					this.ViewBag.ErrorMessage = errormessage = string.Empty;
				}
				if (id > 0)
				{
					long ID = long.Parse(id.ToString());
					Template temp = null;
					bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out temp, out exception);
					if (exception != null)
					{
						throw exception;
					}
					if ((temp != null) && dbresult)
					{
						this.ViewBag.Id = ID;
						List<SelectListItem> templatetypeList = Enum.GetValues(typeof(TemplateType)).Cast<TemplateType>().Select(temptype => new SelectListItem
						{
							Text = temptype.ToString(),
							Value = ((int) temptype).ToString(),
						}).ToList();
						List<SelectListItem> templatetypeListfinal = new List<SelectListItem>();
						int counter = 0;
						foreach (var selectlist in templatetypeList)
						{
							if (temp.TemplateType.ToString() == selectlist.Text)
							{
								templatetypeList [counter].Selected = true;
							}
							counter++;
						}

						this.ViewBag.TemplateTypeList = templatetypeList;
						this.ViewBag.ErrorMessage = errormessage = string.Empty;
						return View(temp);
					}
					else
					{
						errormessage = "Unable to find the template";
						return RedirectToAction("EditTemplate");
					}
				}
				else
				{
					errormessage = "Unable to find the template";
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				errormessage = exception.Message;
			}
			return View();
		}
        [HttpPost]
		public ActionResult EditTemplate ([Bind(Include = "Title,Description")]Template template, string H_Templateid)//, HttpPostedFileBase file
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			bool result = false;
			try
			{
				
				long templateid = long.Parse(H_Templateid);
				Template sourcetemplate = null;
				bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateid, out sourcetemplate, out exception);
				if (exception != null)
				{
					throw exception;
				}
				if ((sourcetemplate != null) && dbresult)
				{
					sourcetemplate.Title = template.Title;
					sourcetemplate.Description = template.Description;
				
					result = ElementManagement.UpdateTemplate(tenantUserSession, sourcetemplate, out exception);

					if (exception != null)
					{
						throw exception;
					}
					if (result)
					{
						this.ViewBag.ErrorMessage = errormessage = string.Empty;
						return RedirectToAction("TemplateListing");
					}
					else
					{
						errormessage = "Unable to update the template";
					}
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				errormessage = ex.Message;
			}
			return RedirectToAction("EditTemplate", template.Id);
		}
        public ActionResult RenderFormWebView(int? id)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            try
            {
                if (id > 0)
                {
                    int ID = Convert.ToInt32(id);
                    List<TemplateElement> elements = null;
                    bool dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                    if (elements.Count > 0 && dbresult)
                    {
                        List<TemplateElement> elementslist = null;
                        GetListOfFormElements(elements, out elementslist, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        return View(elementslist);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return View();
        }
        public ActionResult RenderFormMobileView (int? id)
		        {
			        Exception exception = null;
			        TenantUserSession tenantUserSession = null;

					if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			        List<TemplateElement> elements = null;
			        try
			        {
				        if (id > 0)
				        {
					        long ID = long.Parse(id.ToString());
					        bool dbresult = ElementManagement.GetElementsByTemplateIdOrderByOrdinal(tenantUserSession, ID, out elements, out exception);
					        if (exception != null)
					        {
						        throw exception;
					        }
					        if (elements.Count > 0 && dbresult)
					        {
						        List<TemplateElement> elementslist = null;
						        GetListOfFormElements(elements, out elementslist, out exception);
						        if (exception != null)
						        {
							        throw exception;
						        }
						        return View(elementslist);
					        }
				        }
			        }
			        catch (Exception ex)
			        {
				        exception = ex;
			        }
			        return View();
		        }
		public ActionResult RenderFormPrintView (int? id)
		        {
			        Exception exception = null;
			        TenantUserSession tenantUserSession = null;

					if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			        List<TemplateElement> elements = null;

			        try
			        {
				        if (id > 0)
				        {
					        long ID = long.Parse(id.ToString());
					        bool dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
					        if (exception != null)
					        {
						        throw exception;
					        }
					        if (elements.Count > 0 && dbresult)
					        {
						        List<TemplateElement> elementslist = null;
						        GetListOfFormElements(elements, out elementslist, out exception);
						        if (exception != null)
						        {
							        throw exception;
						        }
						        return View(elementslist);
					        }
				        }
			        }
			        catch (Exception ex)
			        {
				        exception = ex;
			        }
			        return View();
		        }
        [HttpPost]
        public JsonResult SaveTemplateAndFormDesign(string[] Elements, string[] ElementDetails, int templateid)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
           // if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.FormsAdd, TenantActionType.FormsEdit, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }

            bool dbresult = false;
            bool result = false;
            try
            {
                JavaScriptSerializer JavaSerialize = new JavaScriptSerializer();
                List<TemplateElement> TemplateElementsList = JavaSerialize.Deserialize<List<TemplateElement>>(Elements[0]);
                List<TemplateElementDetailViewModel> TemplateElementDetailsViewModelList = JavaSerialize.Deserialize<List<TemplateElementDetailViewModel>>(ElementDetails[0]);
                //long generatedtemplateid = 0;
                //dbresult = ElementManagement.CreateNewTemplateIdForFinalizedTemplate(tenantUserSession, templateid, out generatedtemplateid, out exception);
                //if (exception != null)
                //{
                //    throw exception;
                //}
                ////Below condition Checks if the template or form is finalized then it returns a new generated template id to copy existing template
                //if ((generatedtemplateid > 0) && dbresult)
                //{
                //    result = SaveTemplate(generatedtemplateid, TemplateElementsList, TemplateElementDetailsViewModelList, out exception);
                //}
                //else
                //{
                dbresult = ElementManagement.DeleteElementAndElementDetailsByTemplateId(tenantUserSession, templateid, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (dbresult)
                {
                    result = SaveTemplate(templateid, TemplateElementsList, TemplateElementDetailsViewModelList, out exception);
                }
                //}
            }
            catch (Exception ex)
            {
                return Json("Oops an Error Occured: " + ex.Message.ToString());
            }
            return Json(result.ToString());
        }
        [HttpGet]
		public ActionResult CreateTemplateDesign (int? id)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
			//if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.TemplatesAdd, TenantActionType.TemplatesEdit)) { throw (new UserNotAuthorizedException()); }

			try
			{
				if (id > 0)
				{
					long ID = long.Parse(id.ToString());
					this.ViewBag.Id = ID;
					Template temp = null;
					bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out temp, out exception);
					if (exception != null)
					{
						throw exception;
					}
					if ((temp != null) && dbresult)
					{
						if (temp.TemplateImage != null)
						{
							byte [] Templateimagebytearr = temp.TemplateImage;
							ImageConverter Imgconverter = new ImageConverter();
							Image img = (Image) Imgconverter.ConvertFrom(Templateimagebytearr);
							this.ViewBag.ModelTemplateImageByteArray = Templateimagebytearr;
							this.ViewBag.ModelTemplateImage = img;
							return View();
						}
					}
				}
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			this.ViewBag.ModelTemplateImage = null;
			this.ViewBag.ModelTemplateImageByteArray = null;
			return RedirectToAction("TemplateListing");
		}
		[HttpGet]
		public ActionResult EditTemplateDesign (int? id)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			bool result = false;
			try
			{
				if (id > 0)
				{
					long ID = long.Parse(id.ToString());
					this.ViewBag.Id = ID;
					Template template = null;
					bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
					if (exception != null)
					{
						throw exception;
					}
					if ((template != null) && dbresult)
					{
						this.ViewBag.Finalized = template.IsFinalized;
						this.ViewBag.DesignStatus = template.IsActive;
						if (template != null)
						{
							byte [] Templateimagebytearr = template.TemplateImage;
							ImageConverter Imgconverter = new ImageConverter();
							Image img = (Image) Imgconverter.ConvertFrom(Templateimagebytearr);
							this.ViewBag.ModelTemplateImageByteArray = Templateimagebytearr;
							this.ViewBag.ModelTemplateImage = img;
						}
						else
						{
							this.ViewBag.ModelTemplateImageByteArray = null;
							this.ViewBag.ModelTemplateImage = null;
						}

						List<TemplateElement> elements = null;
						dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
						if (exception != null)
						{
							throw exception;
						}

						if ((elements != null) && dbresult)
						{
							if (elements != null)
							{
								List<TemplateElementDetail> elementdetaillist = null;
								result = GetListOfTemplateElementDetails(elements, out elementdetaillist, out exception);
								if (exception != null)
								{
									throw exception;
								}
								if ((elementdetaillist != null) && result)
								{
									List<TemplateElement> elementlist = null;
									result = GetListOfFormElements(elements, out elementlist, out exception);
									var elementandelementdetialviewmodel = new TemplateElementListAndElementDetailListViewModel();
									elementandelementdetialviewmodel.elements = elementlist;
									elementandelementdetialviewmodel.elementsdetails = elementdetaillist;
									return View(elementandelementdetialviewmodel);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			this.ViewBag.ModelTemplateImageByteArray = null;
			this.ViewBag.ModelTemplateImage = null;
			return RedirectToAction("TemplateListing");
		}

        #region REWORK ON TEMPLATE RENDER VIEW
		//public ActionResult RenderTamplateView(int? id)
		//{
		//    Exception exception = null;
		//    if (id > 0)
		//    {
		//        long ID = long.Parse(id.ToString());
		//        List<TemplateElement> elements = null;
		//        bool dbresult = ElementManagement.GetElementsByTemplateId(ID,out elements,out exception);
		//        if (elements.Count > 0)
		//        {
		//            List<TemplateElementDetail> modelelementsdetails = new List<TemplateElementDetail>();
		//            modelelementsdetails = GetListOfTemplateElementDetails(elements);
		//            List<TemplateElement> modelelements = new List<TemplateElement>();
		//            modelelements = GetListOfFormElements(elements);
		//            ElementsDetailsViewModel elementdetialviewmodel = new ElementsDetailsViewModel();
		//            elementdetialviewmodel.elements = modelelements;
		//            elementdetialviewmodel.elementsdetails = modelelementsdetails;
		//            return View(elementdetialviewmodel);
		//        }
		//        else
		//        {
		//            return View();
		//        }
		//    }
		//    return View();
		//}
		#endregion
		
		

		public static string Message = string.Empty;
       
		
		public static string errormessage = "";
		
		

		#endregion Action Results

		#region User Defined Methods

		public bool GetListOfFormElements (List<TemplateElement> elements, out List<TemplateElement> elementslist, out Exception exception)
		{
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			exception = null;
			bool result = false;
			elementslist = null;
			try
			{
				foreach (var element in elements)
				{
					Template entemp = null;
					bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, element.TemplateId, out entemp, out exception);
					if (exception != null)
					{
						throw exception;
					}
					element.Template = entemp;
					if (element.Id > 0)
					{
						List<TemplateElementValue> elementsvalues = null;
						dbresult = ElementManagement.GetElementValuesByElementId(tenantUserSession, element.Id, out elementsvalues, out exception);
						if (exception != null)
						{
							throw exception;
						}
						if ((elementsvalues != null) && dbresult)
						{
							foreach (var value in elementsvalues)
							{
								element.ElementValues.Add(value);
							}
						}
						else
						{
							element.ElementValues.Clear();
						}
					}
					elementslist.Add(element);
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				result = false;
			}
			return result;
		}

		public bool GetListOfTemplateElementDetails (List<TemplateElement> elements, out List<TemplateElementDetail> elementdetails, out Exception exception)
		{
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			elementdetails = null;
			bool result = false;
			exception = null;

			try
			{

				foreach (var element in elements)
				{
					if (element.ElementType == ElementType.Table)
					{
						bool dbresult = ElementManagement.GetElementDetailListByElementId(tenantUserSession, element.Id, out elementdetails, out exception);
						if (exception != null)
						{
							throw exception;
						}
						if ((elementdetails != null) && dbresult)
						{
							result = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			return result;
		}


		public bool SaveTemplateFiles (long templateId, out Exception exception)
		{
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			bool result = false;
			bool dbresult = false;
			exception = null;
			try
			{
				string serverpath = HttpContext.Server.MapPath("~");
				string UniqueTempFolderValue = Guid.NewGuid().ToString() + "_" + DateTime.UtcNow.ToString();

				LeadToolsOCR lto = new LeadToolsOCR(serverpath, tenantUserSession.Tenant.Id.ToString(), UniqueTempFolderValue, out exception);
				if (exception != null) { throw exception; }
				Template template = null;

				dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateId, out template, out exception);
				if (exception != null)
				{
					throw exception;
				}
				if (dbresult && (template != null))
				{
					string templatename = template.Id.ToString();
					Stream s = new MemoryStream(template.TemplateImage);
					Image templateimage = Image.FromStream(s);
					List<TemplateElement> entieselement = null;
					dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, templateId, out entieselement, out exception);
					if (exception != null)
					{
						throw exception;
					}
					if (dbresult)
					{
						List<List<TemplateElementDetail>> entieselementdetailslist = new List<List<TemplateElementDetail>>();
						foreach (var element in entieselement)
						{
							if (element.ElementType == ElementType.Table)
							{
								List<TemplateElementDetail> elementdetails = new List<TemplateElementDetail>();
								dbresult = ElementManagement.GetElementDetailListByElementId(tenantUserSession, element.Id, out elementdetails, out exception);
								if (exception != null)
								{
									throw exception;
								}
								if (dbresult && (elementdetails.Count > 0))
								{
									entieselementdetailslist.Add(elementdetails);
								}
							}
						}
						lto.AddMasterForm(entieselement, entieselementdetailslist, templatename, templateimage);
						string LeadtoolsTempDir = lto.LeadtoolsTempDir;
						string LeadToolsTemplatesDir = lto.LeadToolsTemplatesDir;
						DirectoryInfo directory = new DirectoryInfo(Server.MapPath(LeadtoolsTempDir));
						foreach (FileInfo file in directory.GetFiles())
						{
							file.CopyTo(Path.Combine(Server.MapPath(LeadToolsTemplatesDir), file.Name), true);
							file.Delete();
						}
						directory.Delete();
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				string LeadtoolsTempDir = System.Configuration.ConfigurationManager.AppSettings ["LeadtoolsTempDir"];
				DirectoryInfo directory = new DirectoryInfo(Server.MapPath(LeadtoolsTempDir));
				foreach (FileInfo file in directory.GetFiles())
				{
					file.Delete();
				}
				exception = ex;
				result = false;
			}
			return result;
		}


		public JsonResult UpdateSettings (bool? IsFinalized, bool? IsActive, int templateid)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
			//if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.TemplatesEdit, TenantActionType.FormsEdit)) { throw (new UserNotAuthorizedException()); }

			bool result = false;
			try
			{
				if (templateid > 0)
				{
					long id = long.Parse(templateid.ToString());
					if (IsFinalized != null && IsActive != null)
					{
						bool final = Convert.ToBoolean(IsFinalized);
						bool active = Convert.ToBoolean(IsActive);
						result = ElementManagement.UpdateTemplateFinalizeAndStatus(tenantUserSession, id, final, active, out exception);
						if (exception != null)
						{
							throw exception;
						}
						if (result)
						{
							return Json("Setting were Successfully Updated");
						}
					}
				}
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
			return Json("Error: Unable to Find Settings To Update");
		}


		/// <summary>
		/// Adds element and element details in the appopriate database table and Create template files for OCR.
		/// </summary>
		/// <param name="Templateid">Template Id for Elements and Element Details</param>
		/// <param name="TemplateElementsList">Template Elements</param>
		/// <param name="TemplateElementDetailsViewModelList">Template Elements Details</param>
		/// <param name="exception">Provides exception details</param>
		/// <returns>returns true if successfull</returns>
		public bool SaveTemplate (long Templateid, List<TemplateElement> TemplateElementsList, List<TemplateElementDetailViewModel> TemplateElementDetailsViewModelList, out Exception exception)
		{
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

			bool result = false;
			exception = null;
			bool dbresult = false;
			try
			{
				foreach (var element in TemplateElementsList)
				{
					if (element.ElementType == ElementType.Barcode2D)
					{
						VintasoftBarcode barcode = new VintasoftBarcode();
						Image img = barcode.WriteBarcode(element.Text);//Server.MapPath("~/Images") + @"/testingBarcode.jpg"
						element.Data = barcode.ConvertImageToByteArray(img);
						element.BarcodeValue = element.Text;
						element.Text = string.Empty;
					}
					element.TemplateId = Templateid;
					dbresult = ElementManagement.AddElement(tenantUserSession, element, out exception);
					if (exception != null)
					{
						throw exception;
					}
				}
				if (TemplateElementDetailsViewModelList.Count > 0)
				{
					foreach (var element in TemplateElementDetailsViewModelList)
					{
						long elementid = 0;
						dbresult = ElementManagement.GetElementIdbyElementDivIdandTemplateId(tenantUserSession, element.ElementDivID, Templateid, out elementid, out exception);
						if (exception != null)
						{
							throw exception;
						}
						if (dbresult && (elementid > 0))
						{
							element.TemplateElementDetail.ElementId = elementid;
							ElementManagement.AddTemplateElementDetail(tenantUserSession, element.TemplateElementDetail, out exception);
						}
					}
				}
				int tempid = (int) Templateid;
				result = SaveTemplateFiles(tempid, out exception);
				if (exception != null)
				{
					throw exception;
				}
			}
			catch (Exception ex)
			{
				result = false;
				exception = ex;
			}
			return result;
		}

		#endregion User Defined Methods
	}
}