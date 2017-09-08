using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public class DepartmentManagement
    {
        public static bool AddDepartment(TenantUserSession tenantUserSession, string departmentName, out Department department, out Exception exception)
        {
            exception = null;
            department = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString)) {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        var departmentExist = context.Departments.Where(x => x.Name == departmentName).SingleOrDefault();
                        if (departmentExist != null) { throw (new Exception("Department already exists")); }
                        Department _department = new Department();
                        _department.Name = departmentName;
                        department = context.Departments.Add(_department);
                        context.SaveChanges();
                        result = true;
                        contextTrans.Commit();
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
