using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public class ACLManagement
    {

        #region ACL EXCEPTION HANDLING

        #endregion
        public static bool Document(TenantUserSession tenantUserSession, Document document, out Exception exception,bool ForceACLExceptionHandler = false)
        {
            exception = null;
            bool result = false;
            try
            {
                if (document == null) { throw (new Exception("Requested document was not found")); }
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var userDocuments = context.UserDocuments.Where(u => u.UserId == tenantUserSession.User.Id).ToList();
                    if (!(userDocuments.Any(ud => ud.DocumentId == document.Id))) { throw (new Exception("You aren not authorized to access the document")); }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                if (ForceACLExceptionHandler) { }
            }
            return result;
        }
        public static bool Document(TenantUserSession tenantUserSession, List<Document> documents, out Exception exception, bool ForceACLExceptionHandler = false)
        {
            // Use this for document viewer pages.
            //Document doc = new Entities.Tenants.Document();
            //var docs = new Entities.Tenants.Document[] { doc }.ToList();
            //ACLManagement.Document(tenantUserSession, docs, out exception, ForceACLExceptionHandler);
            //doc = docs.SingleOrDefault();


            exception = null;
            bool result = false;
            try
            {
                if (documents == null) { throw (new Exception("Requested documents were not found")); }
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var userDocuments = context.UserDocuments.Where(u => u.UserId == tenantUserSession.User.Id).ToList();
                    documents = documents.Where(d => userDocuments.Any(ud => ud.DocumentId == d.DocumentOriginalId)).ToList();
                    //if (!(documents.Any())) { throw (new Exception("You aren not authorized to access the following documents")); }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                if (ForceACLExceptionHandler) { }
            }
            return result;
        }
    }
}
