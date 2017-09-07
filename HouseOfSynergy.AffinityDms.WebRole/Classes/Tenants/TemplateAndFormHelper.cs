using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.OcrLibrary;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.PowerTools.Library.Log;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
    /// <summary>
    /// Contains helper methods for form and template.
    /// </summary>
    public class TemplateAndFormHelper
    {
        public static bool GetListOfFormElements(TenantUserSession tenantUserSession, List<TemplateElement> elements, out List<TemplateElement> elementslist, out Exception exception)
        {
            exception = null;
            bool result = false;
            elementslist = null;
            try
            {
                elementslist = new List<TemplateElement>();
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
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        public static bool GetListOfTemplateElementDetails(TenantUserSession tenantUserSession, List<TemplateElement> elements, out List<TemplateElementDetail> elementdetails, out Exception exception)
        {
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
        public static bool UpdateSettings(TenantUserSession tenantUserSession, bool? IsFinalized, bool? IsActive, long templateid, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                if (templateid > 0)
                {
                    if (IsFinalized != null && IsActive != null)
                    {
                        bool final = Convert.ToBoolean(IsFinalized);
                        bool active = Convert.ToBoolean(IsActive);
                        result = ElementManagement.UpdateTemplateFinalizeAndStatus(tenantUserSession, templateid, final, active, out exception);
                        if (exception != null)
                        {
                            throw exception;
                        }
                        if (result)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }

        public static bool GetUsersForSelection(TenantUserSession tenantUserSession, long templateId, TemplateType templateType, out List<User> users, out Exception exception)
        {
            exception = null;
            bool result = false;
            users = null;
            try
            {
                if (templateId > 0)
                {
                    result = TenantUserManagement.GetUsers(tenantUserSession, out users, out exception);
                    if (exception != null) { throw exception; }
                    if (users.Count > 0)
                    {
                        Template template = null;
                        ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateId, out template, out exception);
                        if (exception != null) { throw exception; }
                        List<UserTemplate> userTemplates = null;
                        result = ElementManagement.GetTemplateUsersByTemplateId(tenantUserSession, templateId, out userTemplates, out exception);
                        if (exception != null) { throw exception; }
                        if (userTemplates.Count > 0)
                        {
                            List<User> userList = users.ToList();
                            users.Clear();

                            foreach (var user in userList)
                            {

                                if (user.Id != template.CheckedOutByUserId)
                                {
                                    foreach (var userTemplate in userTemplates)
                                    {
                                        if (userTemplate.User.Id == user.Id)
                                        {
                                            user.IsUserSelected = true;
                                        }
                                    }
                                    users.Add(user);
                                }


                            }
                        }
                        else
                        {
                            users = users.Where(x => x.Id != template.CheckedOutByUserId).ToList();
                        }
                    }
                    else
                    {
                        users = new List<Entities.Tenants.User>();
                    }
                }
                else
                {
                    throw (new Exception("Unable to find the following Template"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool SetSelectedUsers(TenantUserSession tenantUserSession, long templateId, List<long> selectedUsersIds,TemplateType templateType, out List<User> users, out Exception exception)
        {
            exception = null;
            users = new List<User>();
            bool result = false;
            try
            {
                if (selectedUsersIds.Count <= 0)
                {
                    throw (new Exception("No Users Selected"));
                }
                if (templateId > 0)
                {

                    foreach (var selectedUserId in selectedUsersIds)
                    {
                        User user = null;
                        bool dbresult = TenantUserManagement.GetUserById(tenantUserSession, selectedUserId, out user, out exception);
                        if (exception != null) { throw exception; }
                        users.Add(user);
                    }
                    if (users.Count > 0)
                    {
                        ElementManagement.AddRemoveDiscourseUsers(tenantUserSession, templateId, users, out exception);
                        if (exception != null) { throw exception; }
                    }
                    else
                    {
                        throw (new Exception("No Users Found"));
                    }
                }
                else
                {
                    throw (new Exception("Unable to find the following chat"));
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        /// <summary>
        /// Adds element and element details in the appopriate database table and Create template files for OCR.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenant User Session</param>
        /// <param name="TemplateElementsList">Template Elements</param>
        /// <param name="TemplateElementDetailsViewModelList">Template Elements Details</param>
        /// /// <param name="Templateid">Template Id for Elements and Element Details</param>
        /// <param name="exception">Provides exception details</param>
        /// <returns>returns true if successfull</returns>
        public static bool SaveDesign(TenantUserSession tenantUserSession, string serverpath, List<TemplateElement> TemplateElementsList, List<TemplateElementDetailViewModel> TemplateElementDetailsViewModelList, long templateid, out Exception exception)
        {
            exception = null;
            bool dbresult = false;
            bool result = false;
            try
            {
                ////JavaScriptSerializer JavaSerialize = new JavaScriptSerializer();
                ////List<TemplateElement> TemplateElementsList = JavaSerialize.Deserialize<List<TemplateElement>>(Elements[0]);
                ////List<TemplateElementDetailViewModel> TemplateElementDetailsViewModelList = JavaSerialize.Deserialize<List<TemplateElementDetailViewModel>>(ElementDetails[0]);
                //long generatedtemplateid = 0;
               
                dbresult = ElementManagement.DeleteElementAndElementDetailsByTemplateId(tenantUserSession, templateid, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (dbresult)
                {
                    result = SaveTemplate(tenantUserSession, serverpath, TemplateElementsList, TemplateElementDetailsViewModelList, templateid, out exception);
                }
                
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Adds element and element details in the appopriate database table and Create template files for OCR.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenant User Session</param>
        /// <param name="TemplateElementsList">Template Elements</param>
        /// <param name="TemplateElementDetailsViewModelList">Template Elements Details</param>
        /// /// <param name="Templateid">Template Id for Elements and Element Details</param>
        /// <param name="exception">Provides exception details</param>
        /// <returns>returns true if successfull</returns>
        private static bool SaveTemplate(TenantUserSession tenantUserSession, string serverpath, List<TemplateElement> TemplateElementsList, List<TemplateElementDetailViewModel> TemplateElementDetailsViewModelList, long Templateid, out Exception exception)
        {
            bool result = false;
            exception = null;
            bool dbresult = false;
            try
            {
                foreach (var element in TemplateElementsList)
                {
                    if (element.ElementType == ElementType.Barcode2D)
                    {
                       // VintasoftBarcode barcode = new VintasoftBarcode();
                        //Image img = barcode.WriteBarcode(element.Text);//Server.MapPath("~/Images") + @"/testingBarcode.jpg"
                        //element.Data = barcode.ConvertImageToByteArray(img);
                        //element.BarcodeValue = element.Text;
                        //element.Text = string.Empty;
                        Image img = null;
                        Image sampleImage = new Bitmap((int)200, (int)200);
                        Barcode.WriteBarcode(sampleImage, element.Text, out img, out exception);
                        ImageConverter _imageConverter = new ImageConverter();
                        byte[] imgByteArr = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                        element.Data = imgByteArr;
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
                //int tempid = (int)Templateid;
                Template template = null;
                dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, (long)Templateid, out template, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if ((template.TemplateType == TemplateType.Template) && (template != null))
                {
                    result = SaveTemplateFiles(tenantUserSession, template, serverpath, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                exception = ex;
            }
            return result;
        }




        private static bool SaveTemplateFiles(TenantUserSession tenantUserSession, Template template, string serverpath, out Exception exception)
        {
            long templateId = template.Id;
            bool result = false;
            bool dbresult = false;
            exception = null;
            //string dateTime = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(".", "_").Trim();
            //string UniqueTempFolderValue = Guid.NewGuid().ToString() + "_" + dateTime;
            //string serverpath = HttpContext.Server.MapPath("");
            //LeadToolsOCR lto = new LeadToolsOCR(serverpath, tenantUserSession.Tenant.Id.ToString(), UniqueTempFolderValue, out exception);
            //if (exception != null) { throw exception; }
            try
            {

                if (template != null)
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

                        TemplateCreation templatecreation = new TemplateCreation();
                        using
                        (
                            var ocrEngineSettings = new OcrEngineSettings
                            (
                                new MockLogger(),
                                File.ReadAllText(Path.Combine(serverpath, "App_Data", "LeadTools", "License", "eval-license-files.lic.key")),
                                Path.Combine(serverpath, "App_Data", "LeadTools", "License", "eval-license-files.lic"),
                                Path.Combine(serverpath, "App_Data"),
                                Path.Combine(serverpath, "App_Data", "LeadTools", "Runtime")
                            )
                        )
                        {
                            templatecreation.AddMasterForm(ocrEngineSettings,tenantUserSession.Tenant, entieselement, entieselementdetailslist, templatename, templateimage,out exception);
                            if (exception!=null)
                            {
                                throw exception;
                            }
                        }

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //string LeadtoolsTempDir = lto.LeadtoolsTempDir;
                //DirectoryInfo directory = new DirectoryInfo(Path.Combine(serverpath, LeadtoolsTempDir));
                //if (directory.Exists)
                //{
                //    directory.Delete(true);
                //}
                //if (Directory.Exists(directory.FullName))
                //{
                //    foreach (FileInfo file in directory.GetFiles())
                //    {
                //        file.Delete();
                //    }
                //}
                exception = ex;
                result = false;
            }
            return result;
        }


        //     private static bool SaveTemplateFiles( TenantUserSession tenantUserSession,Template template,string serverpath, out Exception exception)
        //     {
        //         long templateId = template.Id;
        //         bool result = false;
        //         bool dbresult = false;
        //         exception = null;
        //string dateTime = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(".", "_").Trim();
        //string UniqueTempFolderValue = Guid.NewGuid().ToString() + "_" + dateTime;
        ////string serverpath = HttpContext.Server.MapPath("");
        //LeadToolsOCR lto = new LeadToolsOCR(serverpath, tenantUserSession.Tenant.Id.ToString(), UniqueTempFolderValue, out exception);
        //if (exception != null) { throw exception; }
        //try
        //{

        //	if (template != null)
        //             {
        //                 string templatename = template.Id.ToString();
        //                 Stream s = new MemoryStream(template.TemplateImage);
        //                 Image templateimage = Image.FromStream(s);
        //                 List<TemplateElement> entieselement = null;
        //                 dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, templateId, out entieselement, out exception);
        //                 if (exception != null)
        //                 {
        //                     throw exception;
        //                 }
        //                 if (dbresult)
        //                 {
        //                     List<List<TemplateElementDetail>> entieselementdetailslist = new List<List<TemplateElementDetail>>();
        //                     foreach (var element in entieselement)
        //                     {
        //                         if (element.ElementType == Convert.ToInt16(ElementType.Table))
        //                         {
        //                             List<TemplateElementDetail> elementdetails = new List<TemplateElementDetail>();
        //                             dbresult = ElementManagement.GetElementDetailListByElementId(tenantUserSession, element.Id, out elementdetails, out exception);
        //                             if (exception != null)
        //                             {
        //                                 throw exception;
        //                             }
        //                             if (dbresult && (elementdetails.Count > 0))
        //                             {
        //                                 entieselementdetailslist.Add(elementdetails);
        //                             }
        //                         }
        //                     }
        //                     lto.AddMasterForm(entieselement, entieselementdetailslist, templatename, templateimage);
        //			string LeadtoolsTempDir = lto.LeadtoolsTempDir;
        //                     string LeadToolsTemplatesDir = lto.LeadToolsTemplatesDir;
        //                     DirectoryInfo directory = new DirectoryInfo(Path.Combine(serverpath,LeadtoolsTempDir));
        //                     string directorypath = Path.Combine(serverpath, LeadToolsTemplatesDir);
        //                     if (!(Directory.Exists(directorypath)))
        //                     {
        //                         Directory.CreateDirectory(directorypath);
        //                     }
        //                     foreach (FileInfo file in directory.GetFiles())
        //                     {
        //                         file.CopyTo(Path.Combine(directorypath, file.Name), true);
        //                         file.Delete();
        //                     }
        //			directory.Delete();
        //			result = true;
        //                 }
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //	string LeadtoolsTempDir = lto.LeadtoolsTempDir;
        //	DirectoryInfo directory = new DirectoryInfo(Path.Combine(serverpath, LeadtoolsTempDir));
        //             if (directory.Exists)
        //             {
        //                 directory.Delete(true);
        //             }
        //	if (Directory.Exists(directory.FullName))
        //	{
        //		foreach (FileInfo file in directory.GetFiles())
        //		{
        //			file.Delete();
        //		}
        //	}
        //	exception = ex;
        //	result = false;
        //}
        //         return result;
        //     }

    }
}