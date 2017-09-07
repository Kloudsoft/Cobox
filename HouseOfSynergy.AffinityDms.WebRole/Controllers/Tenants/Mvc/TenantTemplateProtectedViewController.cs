using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TenantTemplateProtectedViewController : Controller
    {
        // GET: TenantTemplateProtectedView
        public ActionResult Index(long? id)
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
                    bool result = false;
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
                            byte[] Templateimagebytearr = temp.TemplateImage;
                            ImageConverter Imgconverter = new ImageConverter();
                            Image img = (Image)Imgconverter.ConvertFrom(Templateimagebytearr);
                            this.ViewBag.ModelTemplateImageByteArray = Templateimagebytearr;
                            this.ViewBag.ModelTemplateImage = img;
                        }
                        else
                        {
                            this.ViewBag.ModelTemplateImage = null;
                            this.ViewBag.ModelTemplateImageByteArray = null;
                        }
                        this.ViewBag.Finalized = temp.IsFinalized;
                        this.ViewBag.DesignStatus = temp.IsActive;
                        List<TemplateElement> elements = null;
                        dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, ID, out elements, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        TemplateElementListAndElementDetailListViewModel elementandelementdetialviewmodel = null;
                        if ((elements != null) && dbresult)
                        {
                            List<TemplateElementDetail> elementdetaillist = null;
                            result = TemplateAndFormHelper.GetListOfTemplateElementDetails(tenantUserSession, elements, out elementdetaillist, out exception);
                            if (exception != null)
                            {
                                throw exception;
                            }
                            List<TemplateElement> elementlist = null;
                            result = TemplateAndFormHelper.GetListOfFormElements(tenantUserSession, elements, out elementlist, out exception);
                            elementandelementdetialviewmodel = new TemplateElementListAndElementDetailListViewModel();
                            elementandelementdetialviewmodel.elements = new List<TemplateElement>();
                            elementandelementdetialviewmodel.elementsdetails = new List<TemplateElementDetail>();
                            elementandelementdetialviewmodel.template = new Template();
                            if (elementlist != null)
                            {
                                elementandelementdetialviewmodel.elements = elementlist;
                            }
                            if (elementdetaillist != null)
                            {
                                elementandelementdetialviewmodel.elementsdetails = elementdetaillist;
                            }
                            if (temp != null)
                            {
                                elementandelementdetialviewmodel.template = temp;
                            }
                        }
                        return View("~/Views/Tenants/Templates/TemplateProtectedView.cshtml", elementandelementdetialviewmodel);
                    }
                }
                else
                {
                    throw (new Exception("Invalid Template Id, Unable to load the following Template."));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = ExceptionUtilities.ExceptionToString(ex), SuccessMessage = string.Empty });
            }
            return RedirectToAction("Index", "TenantTemplates", new { ErrorMessage = string.Empty, SuccessMessage = string.Empty });
        }
    }
}