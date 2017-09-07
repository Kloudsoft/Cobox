using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantFormController : Controller
    {
        // GET: TenantForm
        public ActionResult Index(int? id, string ErrorMessage)
        {
            Exception exception = null;
            Template template = null;
            TenantUserSession tenantUserSession = null;

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                if (!(string.IsNullOrEmpty(ErrorMessage)))
                {
                    this.ViewBag.ErrorMessage = ErrorMessage;
                }
                else
                {
                    this.ViewBag.ErrorMessage = ErrorMessage = string.Empty;
                }
                if (id > 0)
                {
                    long ID = (long)id;
                    bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((template != null) && dbresult)
                    {
                        if (template.TemplateType == TemplateType.Form)
                        {
                            this.ViewBag.Id = ID;
                            this.ViewBag.TemplateType = template.TemplateType;
                            this.ViewBag.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            throw (new Exception("The following template is not a form type."));
                        }
                    }
                    else
                    {
                        exception = new Exception("Unable to find the template");
                        throw exception;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = exception.Message;
            }
            return this.View("~/Views/Tenants/Forms/Form.cshtml", template);
        }
        [HttpPost]
        public ActionResult Index(Template template, string H_Templateid)
        {
            Exception exception = null;
            bool dbresult = false;
            long templateid = 0;
            try
            {
                if (string.IsNullOrEmpty(template.Title)) { throw (new Exception("Title is Required"));}
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                if (H_Templateid != null)
                {
                    templateid = long.Parse(H_Templateid);
                    Template sourcetemplate = null;
                    dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateid, out sourcetemplate, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((sourcetemplate != null) && dbresult)
                    {
                        if (sourcetemplate.TemplateType == TemplateType.Form)
                        {
                            sourcetemplate.EntityState = TemplateEntityState.Web;
                            sourcetemplate.Title = template.Title;
                            sourcetemplate.Description = template.Description;

                            dbresult = ElementManagement.UpdateTemplate(tenantUserSession, sourcetemplate, out exception);

                            if (exception != null)
                            {
                                throw exception;
                            }
                            if (dbresult)
                            {
                                this.ViewBag.ErrorMessage = string.Empty;
                                return RedirectToAction("Index", "TenantForms", null);
                            }
                            else
                            {
                                Exception ex = new Exception("Unable to update the template");
                                throw ex;
                            }
                        }
                        else
                        {
                            throw (new Exception("The following template is not a form type."));
                        }
                    }
                }
                else
                {

                    template.EntityState = TemplateEntityState.Web;
                    template.TemplateImage = null;
                    template.TemplateType = TemplateType.Form;
                    template.UserId = tenantUserSession.User.Id;
                    template.CheckedOutByUserId = tenantUserSession.User.Id;
                    dbresult = ElementManagement.CreateTemplate(tenantUserSession, template, out templateid, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (templateid > 0 && (dbresult))
                    {
                        this.ViewBag.ErrorMessage = string.Empty;
                        return RedirectToAction("Index", "TenantFormDesign", new { id = templateid });
                    }
                }
            }
            catch (Exception ex)
            {
                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
                return this.View("~/Views/Tenants/Forms/Form.cshtml", template);
            }
            return RedirectToAction("Index");
        }
    }
}