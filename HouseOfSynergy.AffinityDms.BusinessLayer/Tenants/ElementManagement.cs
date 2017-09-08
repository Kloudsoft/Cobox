using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class ElementManagement
    {
        public static bool AddFileIndexs(TenantUserSession tenantUserSession, ClassifiedFileIndexs cfi, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    
                    if (cfi != null)
                    {
                        context.ClassifiedFileIndexs.Add(cfi);
                        context.SaveChanges();

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool UpdateIndexs(TenantUserSession tenantUserSession, ClassifiedFileIndexs cfi, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                ClassifiedFileIndexs temp = context.ClassifiedFileIndexs.Where(x => x.Id == cfi.Id).Select(x => x).FirstOrDefault();
                temp.indexvalue = cfi.indexvalue;
                context.ClassifiedFileIndexs.Add(temp);
                context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }

        public static bool ActiveInactive(TenantUserSession tenantUserSession, long templateId, bool activeInactive, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var template = context.Templates.Where(t => t.Id == templateId).SingleOrDefault();
                    if (template != null)
                    {
                        if (template.IsActive != activeInactive)
                        {
                            template.IsActive = activeInactive;
                            context.Entry(template).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Verify a max template version by user defined version and proccess new version.
        /// </summary>
        /// <param name="template">Templates max version</param>
        /// <param name="VersionMajor">Version major</param>
        /// <param name="VersionMinor">Version Minor</param>
        /// <param name="major">Newly generated version major</param>
        /// <param name="minor">Newly generated version minor</param>
        /// <param name="exception">Exception occurred</param>
        /// <param name="AutoVersion">Set wether the version is auto generated or not. By default the value is set to auto generate a version</param>
        /// <returns>returns true if successfull</returns>
        private static bool VerifyVersion(Template template, int VersionMajor, int VersionMinor, out int major, out int minor, out Exception exception, bool autoVersion = true)
        {
            exception = null;
            var result = false;
            major = 1;
            minor = 0;
            try
            {
                if (template != null)
                {
                    if (VersionMajor >= 0)
                    {
                        // If previous veraion major is same, new minior should be greater than previous.
                        // Otherwise new major should be greater than old version.
                        if (VersionMajor < template.VersionMajor) { throw (new Exception("Version is less than previous version")); }
                        else if (template.VersionMajor == VersionMajor)
                        {
                            major = VersionMajor;
                            if (VersionMinor < template.VersionMinor) { throw (new Exception("Version is less than previous version")); }
                            else if (template.VersionMinor == VersionMinor) { throw (new Exception("Version can not be same as previous version")); }
                            else { minor = VersionMinor; }
                            result = true;
                        }
                        else
                        {
                            major = VersionMajor;
                            minor = VersionMinor;
                            result = true;
                        }
                    }
                    else
                    {
                        if (autoVersion)
                        {
                            major = template.VersionMajor + 1;
                            minor = 0;
                            result = true;
                        }
                        else
                        {
                            throw (new Exception("Version is less than previous version"));

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

        public static bool UpdateVersion(TenantUserSession tenantUserSession, long templateId, int verMajor, int verMinor, out Exception exception)
        {
            exception = null;
            var result = false;
            try
            {
                int major = 1, minor = 0;
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var template = context.Templates.Where(t => t.Id == templateId).SingleOrDefault();
                    if (template != null)
                    {
                        if (VerifyVersion(template, verMajor, verMinor, out major, out minor, out exception, false))
                        {
                            template.VersionMajor = major;
                            template.VersionMinor = minor;
                            context.Entry(template).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        if (exception != null) { throw exception; }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool GetTemplateUsersByTemplateId(TenantUserSession tenantUserSession, long templateId, out List<UserTemplate> userTemplates, out Exception exception)
        {
            exception = null;
            bool result = false;
            userTemplates = new List<UserTemplate>();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    if (templateId > 0)
                    {
                        userTemplates = context.UserTemplates
                                                .Include(x => x.User)
                                                .Where(x => (x.TemplateId == templateId) && (x.IsActive == true))
                                                .ToList();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool CancelCheckout(TenantUserSession tenantUserSession, long id, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        if (id > 0)
                        {
                            var template = context.Templates.Where(x => x.Id == id).SingleOrDefault();
                            if (template != null)
                            {
                                if (template.IsFinalized) { throw (new Exception("Finalized template can not be canceled")); }
                                else
                                {
                                    var templateElements = context.Elements.Where(x => x.TemplateId == template.Id).ToList();
                                    foreach (var templateElement in templateElements.ToList())
                                    {
                                        if (templateElement.ElementType == ElementType.Table) {
                                            var templateElementDetails = context.ElementDetails.Where(x => x.ElementId == templateElement.Id).ToList();
                                            foreach (var templateElementDetail in templateElementDetails.ToList())
                                            {
                                                context.Entry(templateElementDetail).State = EntityState.Deleted;
                                                context.SaveChanges();
                                            }
                                        }
                                        context.Entry(templateElement).State = EntityState.Deleted;
                                        context.SaveChanges();
                                    }
                                    context.Entry(template).State = EntityState.Deleted;
                                    context.SaveChanges();

                                    {
                                        AzureCloudStorageAccountHelper azureCS = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                                        bool azresult = azureCS.RemoveFile(tenantUserSession, template.Id, DiscussionPostAttachmentType.Template, new System.Threading.CancellationToken(), out exception);
                                        if (exception != null) { throw exception; }
                                        if (azresult)
                                        {
                                            contextTrans.Commit();
                                            result = true;
                                        }
                                    }

                                }
                            }
                            else { throw (new Exception("Unable to find the following template")); }
                        }
                        else { throw (new Exception("Unable to find the following template")); }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }


        /// <summary>
        /// Gets all the elements from the table
        /// </summary>
        /// <param name="elementslist">List of elements recieved from the table</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if Successfull</returns>
        public static bool GetAllElements(TenantUserSession tenantUserSession, out List<TemplateElement> elementslist, out Exception exception)
        {
            exception = null;
            bool result = false;
            elementslist = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    elementslist = context.Elements.AsNoTracking().ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool GetClassifiedIndexesbyIndexValue(TenantUserSession tenantUserSession,List<string>Indexvalues, out List<ClassifiedFileIndexs> elementslist, out Exception exception)
        {
            exception = null;
            bool result = false;
            elementslist = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    elementslist = context.ClassifiedFileIndexs.Where(x=> Indexvalues.Contains(x.indexvalue)).OrderBy(x=>x.indexvalue).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool AddRemoveDiscourseUsers(TenantUserSession tenantUserSession, long templateId, List<User> users, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (users.Count > 0)
                {
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        using (var contextTrans = context.Database.BeginTransaction())
                        {
                            var userTemplates = context.UserTemplates
                                                        .Include(x => x.User)
                                                        .Where(x => x.TemplateId == templateId)
                                                        .ToList();
                            if (userTemplates.Count > 0)
                            {
                                foreach (var userTemplate in userTemplates)
                                {
                                    userTemplate.IsActive = false;
                                }
                            }
                            foreach (var user in users)
                            {
                                bool isUserAdded = false;
                                foreach (var userTemplate in userTemplates)
                                {
                                    if (userTemplate.UserId == user.Id)
                                    {
                                        userTemplate.IsActive = true;
                                        isUserAdded = true;
                                        context.SaveChanges();
                                    }
                                }
                                if (!isUserAdded)
                                {
                                    var userTemplate = new UserTemplate() { TemplateId = templateId, UserId = user.Id, IsActive = true };
                                    context.UserTemplates.Add(userTemplate);
                                    context.SaveChanges();
                                    isUserAdded = true;
                                }
                            }
                            contextTrans.Commit();
                            result = true;
                        }
                    }
                }
                else
                {
                    throw new Exception("No Users Found");
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        /// <summary>
        /// Gets a Sepecific Element by it's Id
        /// </summary>
        /// <param name="ElementId">Id of the element to get</param>
        /// <param name="element">Elements recieved from the table</param>
        /// <param name="exception">Exception occured</param>
        /// <returns>Returns true if Successfull</returns>
        public static bool GetElementById(TenantUserSession tenantUserSession, long ElementId, out TemplateElement element, out Exception exception)
        {
            exception = null;
            element = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    //context.Configuration.LazyLoadingEnabled = true;
                    element = context.Elements.Where(e => e.Id == ElementId).FirstOrDefault<TemplateElement>();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }


        /// <summary>
        /// Gets all the elements from the table by its Template Id
        /// </summary>
        /// <param name="TemplateId">Template Id to get it's list of elements</param>
        /// <param name="elementInfo">List of elements recieved By template id</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool GetElementsByTemplateId(TenantUserSession tenantUserSession, long TemplateId, out List<TemplateElement> elementInfo, out Exception exception)
        {
            bool result = false;
            elementInfo = null;
            exception = null;
            // User userInfo = new User();
            using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                try
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    elementInfo = context.Elements.Where(e => e.TemplateId == TemplateId).ToList();
                    result = true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    result = false;
                }
            }
            return result;
        }

      
        public static bool GetElementsContainingElementIndexType(TenantUserSession tenantUserSession, out List<TemplateElement> templateelements, out Exception exception)
		{
			bool result = false;
			templateelements = null;
			ContextTenant context = null;
			exception = null;
			try
			{
				using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
				{
					//templateelements 
					var indexTypeTemplates = context.Elements.Where(x => x.ElementIndexType == ElementIndexType.LabelValue).Where(x => (x.Template.IsFinalized) && (x.Template.IsActive)).OrderBy(x => x.TemplateId);
					if (indexTypeTemplates.Count() > 0)
					{
						templateelements = indexTypeTemplates.ToList();
					}
					else
					{
						templateelements = new List<TemplateElement>();
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}
		}
		/// <summary>
		/// Gets a Sepecific Template by it's Id
		/// </summary>
		/// <param name="TemplateId">Id of the template to get</param>
		/// <param name="template">Template recieved from the table</param>
		/// <param name="exception">Exception occured</param>
		/// <returns>Returns true if Successfull</returns>
		public static bool GetTemplateByTemplateId(TenantUserSession tenantUserSession, long TemplateId, out Template template, out Exception exception)
        {
            exception = null;
            template = null;
            bool result = false;
            ContextTenant context = null;
            try
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    template = context.Templates.Where(e => e.Id == TemplateId).Select(x => x).FirstOrDefault();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        
        public static bool GetMaxVersionTemplateByTemplateId(TenantUserSession tenantUserSession, long TemplateId, out Template template, out Exception exception)
        {
            exception = null;
            template = null;
            bool result = false;
            ContextTenant context = null;
            try
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    var currentTemplates = context.Templates.Where(x => x.Id == TemplateId).FirstOrDefault();
                    if (currentTemplates == null) { throw (new Exception("Unable to find the following template")); }
                    var templates = context.Templates.Where(x => x.TemplateOriginalId == currentTemplates.TemplateOriginalId).ToList();
                    template = templates.Where(e => e.Id == templates.Select(y=>y.Id).Max()).FirstOrDefault();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Gets all the elements ordered by mobile order from the table by its Template Id
        /// </summary>
        /// <param name="TemplateId">Template Id to get it's list of elements</param>
        /// <param name="elementInfo">List of elements ordered by mobile order recieved By template id</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool GetElementsByTemplateIdOrderByOrdinal(TenantUserSession tenantUserSession, long TemplateId, out List<TemplateElement> elementInfo, out Exception exception)
        {
            bool result = false;
            exception = null;
            elementInfo = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    elementInfo = context.Elements.Where(e => e.TemplateId == TemplateId).OrderBy(e => e.ElementMobileOrdinal).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                exception = ex;
            }
            return result;
        }



        /// <summary>
        /// Get list of template element values by element id
        /// </summary>
        /// <param name="ElementId">Element Id to get it's list of template element values</param>
        /// <param name="elementInfo">List of template element values recieved By element id</param>
        /// <param name="exception">Exceptin occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool GetElementValuesByElementId(TenantUserSession tenantUserSession, long ElementId, out List<TemplateElementValue> elementInfo, out Exception exception)
        {
            exception = null;
            bool result = false;
            elementInfo = null;
            try
            {

                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    elementInfo = context.ElementValues.Where(e => e.ElementId == ElementId).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        /// <summary>
        /// Get list of template element detail by element id
        /// </summary>
        /// <param name="ElementId">Element Id to get it's list of elements</param>
        /// <param name="elementdetails">List of template element details recieved By element id</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool GetElementDetailListByElementId(TenantUserSession tenantUserSession, long ElementId, out List<TemplateElementDetail> elementdetails, out Exception exception)
        {
            bool result = false;
            elementdetails = null;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    elementdetails = context.ElementDetails.Where(e => e.ElementId == ElementId).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }


        //public static bool GetAllMaxVersionTemplates(TenantUserSession tenantUserSession, out List<Template> templateList, out Exception exception)
        //{
        //    bool result = false;
        //    templateList = new List<Template>();

        //    exception = null;

        //    try
        //    {
        //        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //        {
        //            var groups = context.Templates.GroupBy(t => t.TemplateOriginalId);

        //            foreach (var group in groups)
        //            {
        //                var template = group.OrderBy(t => t.VersionMajor).ThenBy(t => t.VersionMinor).FirstOrDefault();
        //            }
        //        }

        //        result = true;
        //    }
        //    catch (Exception e)
        //    {
        //        exception = e;
        //    }

        //    return (result);
        //}



        /// <summary>
		/// Get all max version templates in the table.
		/// </summary>
		/// <param name="templateInfo">List of templates recieved from the table</param>
		/// <param name="exception">Exception occurred</param>
		/// <returns>Returns true if successfull</returns>
		public static bool GetAllMaxVersionTemplates(TenantUserSession tenantUserSession, out List<Template> templateList, out Exception exception)
        {
            bool result = false;
            templateList = new List<Template>();
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;

                    //var templateVersionsParent = context.TemplateVersions.Where(tv => tv.TemplateParent == null).ToList();
                    //foreach (var templateversionparent in templateVersionsParent)
                    //{
                    //    var templateVersionOriginals = context.TemplateVersions
                    //                                        .Where(tv => (tv.TemplateOriginalId == templateversionparent.TemplateOriginalId)).ToList();
                    //    var getmaxtemplateversion = templateVersionOriginals.Where(mtv => mtv.VersionMajor == templateVersionOriginals.Max(tvo => tvo.VersionMajor)).Single();

                    //    var template = context.Templates.Where(t => t.Id == getmaxtemplateversion.TemplateCurrentId).Single();
                    //    templateList.Add(template);
                    //}

                    //new code

                    var templatesParent = context.Templates
                                                 .Include(tv =>tv.CheckedOutByUser)
                                                 .Include(tv => tv.User)
                                                 .Where(tv => tv.TemplateParent == null)
                                          .ToList();

                    //var templatesParent = context.Templates.GroupBy(c => c.TemplateParent).Select(grp => grp.FirstOrDefault()).ToList();

                    foreach (var templateparent in templatesParent)
                    {
                        if (templateparent.IsFinalized)
                        {
                            var templateVersions = context
                                .Templates
                                .Where(tv => tv.TemplateOriginalId == templateparent.TemplateOriginalId)
                                .OrderBy(tv => tv.VersionMajor)
                                .ThenBy(tv => tv.VersionMinor)
                                .Include(tv=> tv.User)
                                .Include(tv => tv.CheckedOutByUser)
                                .ToList();

                            if (templateVersions.Count == 0)
                            {
                                // The template is newly created and has been checked-in.
                                // show row.
                                // enable edit button
                                // enable view button
                            }
                            else
                            {
                                var highestVersion = templateVersions.Last();

                                //previous code
                                if (highestVersion.IsFinalized)
                                {
                                    highestVersion.CheckedOut = false;
                                    templateList.Add(highestVersion);
                                }

                                else
                                {
                                    if (highestVersion.CheckedOutByUserId == tenantUserSession.User.Id)
                                    {
                                        highestVersion.CheckedOut = true;
                                        templateList.Add(highestVersion);
                                    }
                                    else
                                    {
                                        if (templateVersions.Count > 1)
                                        {

                                            if (templateVersions.Count == 1)
                                            {
                                                var secondHighestVersion = templateVersions[templateVersions.Count - 1];
                                                secondHighestVersion.CheckedOut = true;
                                                secondHighestVersion.IsVisible = true;
                                                templateList.Add(secondHighestVersion);
                                            }
                                            else
                                            {
                                                var secondHighestVersion = templateVersions[templateVersions.Count - 2];
                                                secondHighestVersion.CheckedOut = true;
                                                secondHighestVersion.IsVisible = true;
                                                templateList.Add(secondHighestVersion);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (templateparent.CheckedOutByUserId == tenantUserSession.User.Id)
                            {
                                //      enable edit button.
                                //      hide check-out button.
                                //      hide view mode.
                                templateparent.CheckedOut = true;
                                templateList.Add(templateparent);
                            }
                        }

                        //var templateVersionOriginals = context.Templates
                        //                                    .Where(tv => (tv.TemplateOriginalId == templateparent.TemplateOriginalId)).ToList();

                        //var getCheckOutUserTemplates = templateVersionOriginals.Where(tvo => tvo.VersionMajor == templateVersionOriginals.Max(mtv => mtv.VersionMajor)).Single();
                        //var template = new Template();
                        //if (getCheckOutUserTemplates != null)
                        //{
                        //    if (getCheckOutUserTemplates.CheckedOutByUserId == tenantUserSession.User.Id)
                        //    {
                        //        template = templateVersionOriginals.Where(mtv => (mtv.VersionMajor == templateVersionOriginals.Max(tvo => tvo.VersionMajor))).Single();
                        //        if (template != null)
                        //        {
                        //            templateList.Add(template);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        template = templateVersionOriginals
                        //                             .Where
                        //                             (
                        //                                    mtv => 
                        //                                    (
                        //                                        mtv.VersionMajor == templateVersionOriginals
                        //                                                                    .Where(tvo => tvo.IsFinalized == true)
                        //                                                                    .Max(tvo => tvo.VersionMajor)
                        //                                    )

                        //                             ).SingleOrDefault();
                        //        if (template != null)
                        //        {
                        //            templateList.Add(template);
                        //        }
                        //    }
                        //}
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }




        /// <summary>
        /// Get all max version templates in the table.
        /// </summary>
        /// <param name="templateInfo">List of templates recieved from the table</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        //public static bool GetAllMaxVersionTemplates(TenantUserSession tenantUserSession, out List<Template> templateList, out Exception exception)
        //{
        //    bool result = false;
        //    templateList = new List<Template>();
        //    exception = null;
        //    try
        //    {
        //        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //        {
        //            context.Configuration.LazyLoadingEnabled = false;
        //            context.Configuration.ProxyCreationEnabled = false;

        //            //var templateVersionsParent = context.TemplateVersions.Where(tv => tv.TemplateParent == null).ToList();
        //            //foreach (var templateversionparent in templateVersionsParent)
        //            //{
        //            //    var templateVersionOriginals = context.TemplateVersions
        //            //                                        .Where(tv => (tv.TemplateOriginalId == templateversionparent.TemplateOriginalId)).ToList();
        //            //    var getmaxtemplateversion = templateVersionOriginals.Where(mtv => mtv.VersionMajor == templateVersionOriginals.Max(tvo => tvo.VersionMajor)).Single();

        //            //    var template = context.Templates.Where(t => t.Id == getmaxtemplateversion.TemplateCurrentId).Single();
        //            //    templateList.Add(template);
        //            //}

        //            //new code

        //            var templatesParent = context.Templates.Where(tv => tv.TemplateParent == null)
        //                                  .ToList();

        //            //var templatesParent = context.Templates.GroupBy(c => c.TemplateParent).Select(grp => grp.FirstOrDefault()).ToList();

        //            foreach (var templateparent in templatesParent)
        //            {
        //                if (templateparent.IsFinalized)
        //                {
        //                    var templateVersions = context
        //                        .Templates
        //                        .Where(tv => tv.TemplateOriginalId == templateparent.TemplateOriginalId)
        //                        .OrderBy(tv => tv.VersionMajor)
        //                        .ThenBy(tv => tv.VersionMinor)
        //                        .ToList();

        //                    if (templateVersions.Count == 0)
        //                    {
        //                        // The template is newly created and has been checked-in.
        //                        // show row.
        //                        // enable edit button
        //                        // enable view button
        //                    }
        //                    else
        //                    {
        //                        var highestVersion = templateVersions.Last();

        //                        //previous code
        //                        if (highestVersion.IsFinalized)
        //                        {
        //                            highestVersion.CheckedOut = false;
        //                            templateList.Add(highestVersion);
        //                        }

        //                        else
        //                        {
        //                            if (highestVersion.CheckedOutByUserId == tenantUserSession.User.Id)
        //                            {
        //                                highestVersion.CheckedOut = true;
        //                                templateList.Add(highestVersion);
        //                            }
        //                            else
        //                            {
        //                                if (templateVersions.Count > 1)
        //                                {

        //                                    if (templateVersions.Count == 1)
        //                                    {
        //                                        var secondHighestVersion = templateVersions[templateVersions.Count - 1];
        //                                        secondHighestVersion.CheckedOut = true;
        //                                        templateList.Add(secondHighestVersion);
        //                                    }
        //                                    else
        //                                    {
        //                                        var secondHighestVersion = templateVersions[templateVersions.Count - 2];
        //                                        secondHighestVersion.CheckedOut = true;
        //                                        templateList.Add(secondHighestVersion);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (templateparent.CheckedOutByUserId == tenantUserSession.User.Id)
        //                    {
        //                        //      enable edit button.
        //                        //      hide check-out button.
        //                        //      hide view mode.
        //                        templateparent.CheckedOut = true;
        //                        templateList.Add(templateparent);
        //                    }
        //                }

        //                //var templateVersionOriginals = context.Templates
        //                //                                    .Where(tv => (tv.TemplateOriginalId == templateparent.TemplateOriginalId)).ToList();

        //                //var getCheckOutUserTemplates = templateVersionOriginals.Where(tvo => tvo.VersionMajor == templateVersionOriginals.Max(mtv => mtv.VersionMajor)).Single();
        //                //var template = new Template();
        //                //if (getCheckOutUserTemplates != null)
        //                //{
        //                //    if (getCheckOutUserTemplates.CheckedOutByUserId == tenantUserSession.User.Id)
        //                //    {
        //                //        template = templateVersionOriginals.Where(mtv => (mtv.VersionMajor == templateVersionOriginals.Max(tvo => tvo.VersionMajor))).Single();
        //                //        if (template != null)
        //                //        {
        //                //            templateList.Add(template);
        //                //        }
        //                //    }
        //                //    else
        //                //    {
        //                //        template = templateVersionOriginals
        //                //                             .Where
        //                //                             (
        //                //                                    mtv => 
        //                //                                    (
        //                //                                        mtv.VersionMajor == templateVersionOriginals
        //                //                                                                    .Where(tvo => tvo.IsFinalized == true)
        //                //                                                                    .Max(tvo => tvo.VersionMajor)
        //                //                                    )

        //                //                             ).SingleOrDefault();
        //                //        if (template != null)
        //                //        {
        //                //            templateList.Add(template);
        //                //        }
        //                //    }
        //                //}
        //            }

        //            result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        result = false;
        //    }
        //    return result;
        //}

        //public static bool GetAllMaxVersionTemplates(TenantUserSession tenantUserSession, out List<Template> templateList, out Exception exception)
        //{
        //    bool result = false;
        //    templateList = new List<Template>();
        //    exception = null;
        //    try
        //    {
        //        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //        {
        //            context.Configuration.LazyLoadingEnabled = false;
        //            context.Configuration.ProxyCreationEnabled = false;

        //            //var templateVersionsParent = context.TemplateVersions.Where(tv => tv.TemplateParent == null).ToList();
        //            //foreach (var templateversionparent in templateVersionsParent)
        //            //{
        //            //    var templateVersionOriginals = context.TemplateVersions
        //            //                                        .Where(tv => (tv.TemplateOriginalId == templateversionparent.TemplateOriginalId)).ToList();
        //            //    var getmaxtemplateversion = templateVersionOriginals.Where(mtv => mtv.VersionMajor == templateVersionOriginals.Max(tvo => tvo.VersionMajor)).Single();

        //            //    var template = context.Templates.Where(t => t.Id == getmaxtemplateversion.TemplateCurrentId).Single();
        //            //    templateList.Add(template);
        //            //}

        //            //new code

        //            //var templatesParent = context.Templates.Where(tv => tv.TemplateParent == null)
        //            //                      .ToList();

        //            ////var templatesParent = context.Templates.GroupBy(c => c.TemplateParent).Select(grp => grp.FirstOrDefault()).ToList();

        //            //foreach (var templateparent in templatesParent)
        //            //{
        //            //    if (templateparent.IsFinalized)
        //            //    {
        //            //        var templateVersions = context
        //            //            .Templates
        //            //            .Where(tv => tv.TemplateOriginalId == templateparent.TemplateOriginalId)
        //            //            .OrderBy(tv => tv.VersionMajor)
        //            //            .ThenBy(tv => tv.VersionMinor)
        //            //            .ToList();

        //            //        if (templateVersions.Count == 0)
        //            //        {
        //            //            // The template is newly created and has been checked-in.
        //            //            // show row.
        //            //            // enable edit button
        //            //            // enable view button
        //            //        }
        //            //        else
        //            //        {
        //            //            var highestVersion = templateVersions.Last();


        //            //            //previous code
        //            //            //if (highestVersion.IsFinalized)
        //            //            //{
        //            //            //    highestVersion.CheckedOut = false;
        //            //            //    templateList.Add(highestVersion);
        //            //            //}

        //            //            // write new code over here
        //            //            if (highestVersion.IsFinalized)
        //            //            {
        //            //                var higerVersionChildern = context
        //            //                    .Templates
        //            //                    .Where(tv => tv.TemplateParent == highestVersion.Id)
        //            //                    .FirstOrDefault();

        //            //                if (higerVersionChildern != null)
        //            //                {
        //            //                    if (higerVersionChildern.IsFinalized)
        //            //                    {
        //            //                        higerVersionChildern.CheckedOut = false;
        //            //                        templateList.Add(higerVersionChildern);
        //            //                    }
        //            //                    else
        //            //                    {
        //            //                        if (higerVersionChildern.CheckedOutByUserId == tenantUserSession.User.Id)
        //            //                        {
        //            //                            higerVersionChildern.CheckedOut = true;
        //            //                            templateList.Add(higerVersionChildern);
        //            //                        }
        //            //                        else
        //            //                        {
        //            //                            highestVersion.CheckedOut = true;
        //            //                            templateList.Add(highestVersion);
        //            //                        }
        //            //                    }
        //            //                }

        //            //            }
        //            //            else
        //            //            {
        //            //                if (highestVersion.CheckedOutByUserId == tenantUserSession.User.Id)
        //            //                {
        //            //                    highestVersion.CheckedOut = true;
        //            //                    templateList.Add(highestVersion);
        //            //                }
        //            //                else
        //            //                {
        //            //                    if (templateVersions.Count > 1)
        //            //                    {

        //            //                        if (templateVersions.Count == 1)
        //            //                        {
        //            //                            var secondHighestVersion = templateVersions[templateVersions.Count - 1];
        //            //                            secondHighestVersion.CheckedOut = true;
        //            //                            templateList.Add(secondHighestVersion);
        //            //                        }
        //            //                        else
        //            //                        {
        //            //                            var secondHighestVersion = templateVersions[templateVersions.Count - 2];
        //            //                            secondHighestVersion.CheckedOut = true;
        //            //                            templateList.Add(secondHighestVersion);
        //            //                        }
        //            //                    }
        //            //                }
        //            //            }
        //            //        }
        //            //    }
        //            //    else
        //            //    {
        //            //        if (templateparent.CheckedOutByUserId == tenantUserSession.User.Id)
        //            //        {
        //            //            //      enable edit button.
        //            //            //      hide check-out button.
        //            //            //      hide view mode.
        //            //            templateparent.CheckedOut = true;
        //            //            templateList.Add(templateparent);
        //            //        }
        //            //    }

        //            //code new uzma
        //            var templatesParent = context.Templates.Where(tv => tv.TemplateParent == null)
        //                              .ToList();

        //            foreach (var templateparent in templatesParent)
        //            {

        //                var templateVersionOriginals = context.Templates
        //                                                    .Where(tv => (tv.TemplateOriginalId == templateparent.TemplateOriginalId)).ToList();

        //                var getCheckOutUserTemplates = templateVersionOriginals.Where(tvo => tvo.VersionMajor == templateVersionOriginals.Max(mtv => mtv.VersionMajor)).Single();
        //                var template = new Template();
        //                if (getCheckOutUserTemplates != null)
        //                {
        //                    if (getCheckOutUserTemplates.CheckedOutByUserId == tenantUserSession.User.Id)
        //                    {
        //                        template = templateVersionOriginals.Where(mtv => (mtv.VersionMajor == templateVersionOriginals.Max(tvo => tvo.VersionMajor))).Single();
        //                        if (template != null)
        //                        {
        //                            templateList.Add(template);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        template = templateVersionOriginals
        //                                             .Where
        //                                             (
        //                                                    mtv =>
        //                                                    (
        //                                                        mtv.VersionMajor == templateVersionOriginals
        //                                                                                    .Where(tvo => tvo.IsFinalized == true)
        //                                                                                    .Max(tvo => tvo.VersionMajor)
        //                                                    )

        //                                             ).SingleOrDefault();
        //                        if (template != null)
        //                        {
        //                            templateList.Add(template);
        //                        }
        //                    }
        //                }
        //            }

        //            result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        result = false;
        //    }
        //    return result;
        //}

        public static bool GetAllFinalizedTemplates(TenantUserSession tenantUserSession, out List<Template> templateList, out Exception exception)
        {
            bool result = false;
            templateList = null;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    templateList = context.Templates.Where(x => x.IsFinalized == true && x.IsActive == true).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }

        public static bool GetAllTemplatesFirstVersion(TenantUserSession tenantUserSession, out List<Template> templateList, out Exception exception)
        {
            bool result = false;
            templateList = null;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    templateList = context.Templates
                                            .Where(x => (x.IsFinalized == true) && (x.IsActive == true) && (x.Id == x.TemplateOriginalId)).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Get all of templates in the table.
        /// </summary>
        /// <param name="templateInfo">List of templates recieved from the table</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool GetAllTemplates(TenantUserSession tenantUserSession, out List<Template> templateList, out Exception exception)
        {
            bool result = false;
            templateList = null;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    templateList = context.Templates.Select(a => a).ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Add a Template Element.
        /// </summary>
        /// <param name="element">Template element to add in the database.</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool AddElement(TenantUserSession tenantUserSession, TemplateElement element, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Elements.Add(element);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                exception = ex;
            }
            return result;
        }


        //public static string AddElementDetails(List<TemplateElementDetail> elementsdetails)
        //{
        //    try
        //    {
        //        string result = "";
        //        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //        {
        //            foreach (var element in elementsdetails)
        //            {
        //                var elemid = context.Elements.Where(x => x.ElementId.ToString() == element.ElementId.ToString()).FirstOrDefault().ToString();
        //                if (elemid.Length > 0)
        //                {
        //                    element.ElementId = long.Parse(elemid);
        //                    context.ElementDetails.Add(element);
        //                }
        //                context.ElementDetails.Add(element);
        //            }
        //            context.SaveChanges();
        //        }
        //        result = "Form Created Successfully";
        //        return (result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ("Oops an error Occured: " + ex.Message);
        //    }
        //}

        /// <summary>
        /// Get element id by element div id and template id.
        /// </summary>
        /// <param name="ElementDivId">Element div id to find element id</param>
        /// <param name="TemplateId">Template id to find element id</param>
        /// <param name="elementId">Element id recieved by template element div id and template id</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool GetElementIdbyElementDivIdandTemplateId(TenantUserSession tenantUserSession, string ElementDivId, long TemplateId, out long elementId, out Exception exception)
        {
            bool result = false;
            elementId = 0;
            exception = null;
            try
            {
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                elementId = context.Elements.Where(x => ((x.ElementId.ToString() == ElementDivId) && x.TemplateId == TemplateId)).Select(x => x.Id).FirstOrDefault();
                result = true;
            }
            catch (Exception ex)
            {
                elementId = 0;
                result = false;
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Add template element detail.
        /// </summary>
        /// <param name="Templateelementdetail">Template element detail to add.</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool AddTemplateElementDetail(TenantUserSession tenantUserSession, TemplateElementDetail Templateelementdetail, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    if (Templateelementdetail != null)
                    {
                        context.ElementDetails.Add(Templateelementdetail);
                        context.SaveChanges();
                        result = true;
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


        //public static string AddExtendedElementDetails(List<HouseOfSynergy.AffinityDms.Entities.TemplateElementDetailExtended> extendedelementsdetails)
        //{
        //    try
        //    {
        //        string result = "";

        //        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //        {
        //            foreach (var element in extendedelementsdetails)
        //            {
        //                var elemid = context.Elements.Where(x => ((x.ElementId.ToString() == element.ElementDivId.ToString()) && x.TemplateId == element.TemplateId)).Select(x => x.Id).FirstOrDefault().ToString();
        //                if (elemid.Length > 0)
        //                {
        //                    element.ElementId = long.Parse(elemid);
        //                    TemplateElementDetail elementdetail = (TemplateElementDetail)element;
        //                    //Enties.ElementDetail elementdetail = new Enties.ElementDetail();
        //                    //element.ElementId = long.Parse(elemid);
        //                    //elementdetail.ElementId = element.ElementId;
        //                    //elementdetail.ElementDetailId = element.ElementDetailId;

        //                    //elementdetail.ElementType = element.ElementType;
        //                    //elementdetail.Name = element.Name;
        //                    //elementdetail.Text = element.Text;
        //                    //elementdetail.Width = element.Width;
        //                    //elementdetail.Height = element.Height;
        //                    //elementdetail.ForegroundColor = element.ForegroundColor;
        //                    //elementdetail.BackgroundColor = element.BackgroundColor;
        //                    //elementdetail.Value = element.Value;
        //                    //elementdetail.ElementId = element.ElementId;
        //                    //elementdetail.X = element.X;
        //                    //elementdetail.Y = element.Y;
        //                    //elementdetail.BorderStyle = element.BorderStyle;
        //                    //elementdetail.SizeMode = element.SizeMode;

        //                    //elementdetail.Description = element.Description;
        //                    context.ElementDetails.Add(elementdetail);
        //                }
        //            }
        //            context.SaveChanges();
        //        }
        //        result = "Form Created Successfully";
        //        return (result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ("Oops an error Occured: " + ex.Message);
        //    }
        //}



        /// <summary>
        /// Update template finalized and active/inactive.
        /// </summary>
        /// <param name="templateid">Template id that will be updated</param>
        /// <param name="isfinalize">Mark Template Finalize</param>
        /// <param name="isactive">Mark Template Active/Inactive</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool UpdateTemplateFinalizeAndStatus(TenantUserSession tenantUserSession, long templateid, bool isfinalize, bool isactive, out Exception exception)//string[][] templateListarray)
        {
            bool result = false;
            exception = null;
            try
            {
                Template temp = new Template();
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                temp = context.Templates.Where(x => x.Id == templateid).Select(x => x).FirstOrDefault();
                temp.IsFinalized = isfinalize;
                temp.IsActive = isactive;
                context.Templates.Attach(temp);
                context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                context.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;


        }




        /// <summary>
        /// Create new copy of template if a template was marked as finalized and reference out newly created template id.
        /// </summary>
        /// <param name="Templateid">Template id of existing template</param>
        /// <param name="templateId">Template id of new template created</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool CreateNewTemplateIdForFinalizedTemplate(TenantUserSession tenantUserSession, long templateIdParent, out long newTemplateId, out Exception exception, Version version = null)
        {

            bool result = false;
            exception = null;
            newTemplateId = 0;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        var parentTemplate = context.Templates
                                                    .SingleOrDefault(x => (x.Id == templateIdParent));
                        int major = 1, minor = 0;
                        if (parentTemplate.IsFinalized)
                        {

                            VerifyVersion(parentTemplate, version.Major, version.Minor, out major, out minor, out exception);
                            if (exception != null) { throw exception; }
                            result = true;
                        }
                        else
                        {
                            newTemplateId = 0;
                            result = false;
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






        ///// <summary>
        ///// Create new copy of template if a template was marked as finalized and reference out newly created template id.
        ///// </summary>
        ///// <param name="Templateid">Template id of existing template</param>
        ///// <param name="templateId">Template id of new template created</param>
        ///// <param name="exception">Exception occurred</param>
        ///// <returns>Returns true if successfull</returns>
        //public static bool CreateNewTemplateIdForFinalizedTemplate(TenantUserSession tenantUserSession, long templateIdParent, out long newTemplateId, out Exception exception, Version version = null)
        //{

        //    bool result = false;
        //    exception = null;
        //    newTemplateId = 0;
        //    try
        //    {
        //        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //        {
        //            using (var contextTrans = context.Database.BeginTransaction())
        //            {
        //                var parentTemplate = context.Templates
        //                                            .SingleOrDefault(x => (x.Id == templateIdParent));
        //                int major = 1, minor = 0;
        //                if (parentTemplate.IsFinalized)
        //                {

        //                    VerifyVersion(parentTemplate, version.Major, version.Minor, out major, out minor, out exception);
        //                    if (exception != null) { throw exception; }



        //                    if (parentTemplate != null)
        //                    {
        //                        parentTemplate.VersionCount += 1;
        //                        context.SaveChanges();
        //                        //var img = parentTemplate.TemplateImage;
        //                        var parentTemplateCopy = parentTemplate.Clone();
        //                        //long TemplateIdOriginal = parentTemplate.TemplateVersions.Select(x => x.TemplateOriginalId).First();
        //                        long TemplateIdOriginal = parentTemplate.TemplateOriginalId;
        //                        parentTemplateCopy.VersionCount = 1;
        //                        parentTemplateCopy.Id = 0;
        //                        //parentTemplateCopy.TemplateImage = img;
        //                        parentTemplateCopy.IsActive = true;
        //                        parentTemplateCopy.IsFinalized = false;
        //                        parentTemplateCopy.VersionMajor = major;
        //                        parentTemplateCopy.VersionMinor = minor;
        //                        // parentTemplateCopy.TemplateVersions = null;

        //                        if (parentTemplate.TemplateImage != null)
        //                        {
        //                            parentTemplateCopy.TemplateImage = new byte[parentTemplate.TemplateImage.Length];
        //                            parentTemplate.TemplateImage.CopyTo(parentTemplateCopy.TemplateImage, 0);
        //                        }

        //                        var currentTemplate = context.Templates.Add(parentTemplateCopy);
        //                        context.SaveChanges();

        //                        long TemplateIdCurrent = currentTemplate.Id;
        //                        TemplateVersion templateVersion = new TemplateVersion();
        //                        templateVersion.TemplateCurrentId = TemplateIdCurrent;
        //                        templateVersion.TemplateOriginalId = TemplateIdOriginal;
        //                        templateVersion.TemplateParentId = templateIdParent;
        //                        templateVersion.VersionMajor = major;
        //                        templateVersion.VersionMinor = minor;

        //                        context.TemplateVersions.Add(templateVersion);
        //                        context.SaveChanges();

        //                        contextTrans.Commit();
        //                        newTemplateId = TemplateIdCurrent;
        //                    }
        //                    else
        //                    {
        //                        newTemplateId = 0;
        //                        result = false;
        //                    }
        //                }
        //                else
        //                {
        //                    newTemplateId = 0;
        //                    result = false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //    }
        //    return result;

        //}




        public static bool Checkin(TenantUserSession tenantUserSession, long templateid, out Exception exception)
        {
            exception = null;
            var result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var template = context.Templates.Where(t => t.Id == templateid).SingleOrDefault();
                    if (template != null)
                    {
                        if (!template.IsFinalized)
                        {
                            template.IsFinalized = true;
                            context.Entry(template).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        result = true;
                    }
                    else
                    {
                        throw (new Exception("Unable to Find the Following Template"));
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }


        /// <summary>
        /// Create template and reference out new template id
        /// </summary>
        /// <param name="template">Template to create</param>
        /// <param name="templateid">Template id of the created template</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool CreateTemplate(TenantUserSession tenantUserSession, Template template, out long templateid, out Exception exception)
        {
            exception = null;
            templateid = 0;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            template.IsFinalized = false;
                            template.IsActive = true;
                            template.VersionCount = 1;
                            template.VersionMajor = 1;
                            template.VersionMinor = 0;
                            template.CheckedOutByUserId = tenantUserSession.User.Id;
                            template.CheckedOutDateTime = DateTime.Now;
                            var currenttemplate = context.Templates.Add(template);
                            context.SaveChanges();
                            template.TemplateParent = null;//template.Id;
                            template.TemplateOriginalId = template.Id;
                            templateid = template.Id;
                            context.SaveChanges();
                            contextTrans.Commit();
                            result = true;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                exception = ex;
                templateid = 0;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Delete all elements and element details by template id
        /// </summary>
        /// <param name="templateid">Template id to remove it's elements and element details</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool DeleteElementAndElementDetailsByTemplateId(TenantUserSession tenantUserSession, long templateid, out Exception exception)//long elementid)
        {
            bool result = false;
            exception = null;
            bool dbresult = false;
            
            try
            {
                List<TemplateElement> elements = null;
                dbresult = ElementManagement.GetElementsByTemplateId(tenantUserSession, templateid, out elements, out exception);
                if (exception != null)
                {
                    throw exception;
                }
                if (elements.Count > 0 && (dbresult))
                {
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        using (var contextTrans = context.Database.BeginTransaction())
                        {
                            foreach (var element in elements)
                            {
                                if (element.ElementType == ElementType.Table)
                                {
                                    List<TemplateElementDetail> elementdetails = context.ElementDetails.Where(x => x.ElementId == element.Id).Select(x => x).ToList();
                                    foreach (var elementdetail in elementdetails)
                                    {
                                        //context.ElementDetails.Remove(elementdetail);
                                        context.Entry(elementdetail).State = EntityState.Deleted;
                                        context.SaveChanges();
                                    }
                                }
                                //context.Elements.Remove(element);
                                context.Entry(element).State = EntityState.Deleted;
                                context.SaveChanges();
                            }
                            contextTrans.Commit();
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                exception = ex;
            }
            return result;
        }

        //public static string DeleteElementAndElementDetailsByElementId(List<TemplateElement> elements)//long elementid)
        //{
        //    try
        //    {
        //        ContextTenant context;
        //        foreach (var element in elements)
        //        {
        //            //    Element element = context.Elements.Find(elementid);
        //            if (element.ElementType == Convert.ToInt16(ElementType.Table))
        //            {
        //                context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
        //                List<TemplateElementDetail> elementdetails = context.ElementDetails.Where(x => x.ElementId == element.Id).Select(x => x).ToList();
        //                foreach (var elementdeital in elementdetails)
        //                {
        //                    context.ElementDetails.Attach(elementdeital);
        //                    context.ElementDetails.Remove(elementdeital);
        //                    context.SaveChanges();
        //                }
        //            }
        //            context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
        //            //context.Entry(element).State = System.Data.Entity.EntityState.Deleted;
        //            context.Elements.Attach(element);
        //            context.Elements.Remove(element);
        //            context.SaveChanges();
        //        }
        //        return "True";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "An Error Occured: " + ex.Message;
        //    }
        //}
        //public static string UpdateTemplate(Template template)
        //{

        //    ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
        //    try
        //    {
        //        Template temp = context.Templates.Where(x => x.Id == template.Id).Select(x => x).FirstOrDefault();
        //        temp.Title = template.Title;
        //        temp.Description = template.Description;
        //        temp.EntityState = template.EntityState;
        //        temp.TemplateType = template.TemplateType;
        //        context.Templates.Add(temp);
        //        context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
        //        context.SaveChanges();
        //        return ("Template Updated Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        string result = "Oops an error Occured: " + ex.Message;
        //        return (result);
        //    }
        //}


        /// <summary>
        /// Update an existing template.
        /// </summary>
        /// <param name="template">Template to be updated</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool UpdateTemplate(TenantUserSession tenantUserSession, Template template, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                Template temp = context.Templates.Where(x => x.Id == template.Id).Select(x => x).FirstOrDefault();
                temp.Title = template.Title;
                temp.Description = template.Description;
                temp.EntityState = template.EntityState;
                temp.TemplateType = template.TemplateType;
                context.Templates.Add(temp);
                context.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Delete an existing template.
        /// </summary>
        /// <param name="template">Template to be deleted</param>
        /// <param name="exception">Exception occurred</param>
        /// <returns>Returns true if successfull</returns>
        public static bool DeleteTemplate(TenantUserSession tenantUserSession, Template template, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                ContextTenant context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                Template temp = context.Templates.Where(x => x.Id == template.Id).Select(x => x).FirstOrDefault();
                context.Templates.Remove(temp);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }

        //public static List<Role> GetRoles()
        //{
        //    List<Role> roles = null;
        //    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //    {
        //        roles = context.Roles.Include(p => p.Users).Include(p => p.UserRoles).Include(p => p.RoleRights).ToList();
        //    }
        //    return roles;
        //}


        //public static bool UpdateUser(User user)
        //{
        //    try
        //    {


        //        User tempUser = null;
        //        using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //        {
        //            //using (transaction = context.Database.BeginTransaction())
        //            //{
        //            //    try
        //            //    {
        //            //    }
        //            //}
        //            context.Configuration.LazyLoadingEnabled = true;
        //           // context.Configuration. = true;

        //            tempUser = context.Users.Where(u => u.Id == user.Id).FirstOrDefault<User>();


        //            tempUser.FirstName = user.FirstName;
        //            tempUser.LastName = user.LastName;
        //            tempUser.MobilePhoneNo = user.MobilePhoneNo;
        //            tempUser.Address = user.Address;
        //            tempUser.UserName = user.UserName;
        //            tempUser.Email = user.Email;

        //            foreach (Role role in user.Roles)
        //            {
        //                context.UserRoles.RemoveRange(context.UserRoles.Where(u => u.Id == user.Id));


        //                context.UserRoles.AddRange(user.UserRoles);

        //                if (tempUser.UserRoles.Where(u => u.RoleId == role.Id).Count() < 1)
        //                {
        //                    UserRole userRole = new UserRole();
        //                    userRole.User = tempUser;
        //                    userRole.RoleId = role.Id;
        //                    tempUser.UserRoles.Add(userRole);
        //                }
        //            }

        //            context.Users.Attach(tempUser);
        //            context.Entry(tempUser).State = System.Data.Entity.EntityState.Modified;  
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return true;
        //}


        //public static List<Screen> GetScreens()
        //{
        //    List<Screen> screens = null;
        //    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
        //    {
        //        screens = context.Screens.AsNoTracking().ToList();
        //    }
        //    return screens;
        //}

        public static bool CheckoutTemplateAndMakeNewVersion(TenantUserSession tenantUserSession, long templateId,out Template newTemplate,out List<TemplateElement> templateElement,out List<TemplateElementDetail> templateElementDetail, out Exception exception)
        {
            bool result = false;
            exception = null;
            Template template = null;
            newTemplate = null;
            templateElement = null;
            templateElementDetail = null;
            Template copyTemplate = new Template();
            List<TemplateElement> copyTemplateElement = new List<TemplateElement>();
            List<TemplateElementDetail> copyTemplateElementDetail = new List<TemplateElementDetail>();

            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    GetTemplateByTemplateId(tenantUserSession, templateId, out template, out exception);
                    if (exception != null) { throw exception; }

                    if (template != null)
                    {
                        GetElementsByTemplateId(tenantUserSession, templateId, out templateElement, out exception);
                        if (exception != null) { throw exception; }

                        foreach (var item in templateElement)
                        {
                            if (item.ElementType == ElementType.Table)
                            {
                                GetElementDetailListByElementId(tenantUserSession, item.Id, out templateElementDetail, out exception);
                                if (exception != null) { throw exception; }
                                if (templateElementDetail != null)
                                {
                                    foreach (var elmentDetail in templateElementDetail)
                                    {
                                        item.ElementDetails.Add(elmentDetail);
                                        copyTemplateElementDetail.Add(elmentDetail);
                                    }
                                }
                            }
                            template.Elements.Add(item);
                            var element = item;
                            foreach (var templateelement in item.ElementDetails.ToList())
                            {
                                element.ElementDetails.Remove(templateelement);
                            }
                            copyTemplateElement.Add(element);
                        }
                       
                        using (var contextTrans = context.Database.BeginTransaction())
                        {
                            //update version count in checkout template
                            template.VersionCount += 1;
                            context.Templates.Attach(template);

                            //Adding template in DB
                            byte[] imgArr = template.TemplateImage;
                            copyTemplate = template.Clone();
                            copyTemplate.Id = 0;
                            
                            copyTemplate.TemplateImage = imgArr;
                            copyTemplate.TemplateParent = template.Id;
                            copyTemplate.TemplateOriginalId = template.TemplateOriginalId;
                            copyTemplate.VersionCount = 1;
                            copyTemplate.VersionMajor = template.VersionMajor + 1;
                            copyTemplate.VersionMinor = 0;
                            copyTemplate.CheckedOutByUserId = tenantUserSession.User.Id;
                            copyTemplate.CheckedOutDateTime = DateTime.UtcNow;
                            copyTemplate.IsFinalized = false;
                            context.Templates.Add(copyTemplate);
                            context.SaveChanges();

                            //Adding Element in DB
                            foreach (var item in copyTemplateElement)
                            {
                                var elementid = item.Id;
                                item.TemplateId = copyTemplate.Id;
                                var element = context.Elements.Add(item);
                                context.SaveChanges();
                                if (item.ElementType == ElementType.Table) {
                                    //Adding Element Detail in DB
                                    foreach (var elementDetail in copyTemplateElementDetail)
                                    {
                                        if (elementDetail.ElementId == elementid)
                                        {
                                            elementDetail.ElementId = element.Id;
                                            context.ElementDetails.Add(elementDetail);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }

                            
                            contextTrans.Commit();
                            newTemplate = copyTemplate;
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
    }
}