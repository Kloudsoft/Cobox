using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.OcrWorkerRole
{
	//public static class Business
	//{
	//	public static bool GetTenants (out List<Tenant> tenants, out Exception exception)
	//	{
	//		var result = false;

	//		tenants = null;
	//		exception = null;

	//		try
	//		{
	//			using (var context = new ContextMaster())
	//			{
	//				tenants = context
	//					.Tenants
	//					.AsNoTracking()
	//					.Include(t => t.TenantSubscriptions)
	//					.Where(t => (t.TenantSubscriptions.Any(ts => ts.IsActive)))
	//					.ToList()
	//					.Where(t => t.TenantSubscriptions.Any(ts => ts.NumberOfPagesRemaining > 0))
	//					.ToList();
	//			}

	//			result = true;
	//		}
	//		catch (Exception e)
	//		{
	//			exception = e;
	//		}

	//		return (result);
	//	}

	//	public static bool TenantDocumentGet (Tenant tenant, out Document document, out Exception exception)
	//	{
	//		var result = false;

	//		document = null;
	//		exception = null;

	//		try
	//		{
	//			using (var context = new ContextTenant(tenant.DatabaseConnectionString))
	//			{
	//				document = context
	//					.Documents
	//					.AsNoTracking()
	//					.Include(d => d.User)
	//					.Include(d => d.Folder)
	//					.FirstOrDefault(d => d.State == DocumentState.Queued);
	//			}

	//			result = true;
	//		}
	//		catch (Exception e)
	//		{
	//			exception = e;
	//		}

	//		return (result);
	//	}

 //       public static bool TenantDocumentUpdate(Tenant tenant, Document document, out Exception exception)
 //       {
 //           var result = false;

 //           exception = null;

 //           try
 //           {
 //               var templates = new List<Template>();
 //               var documentFragments = new List<DocumentFragment>();

 //               using (var context = new ContextTenant(tenant.DatabaseConnectionString))
 //               {
 //                   templates = context
 //                       .Templates
 //                       .AsNoTracking()
 //                       .Include(t => t.Elements)
 //                       .ToList();

 //                   // document
 //                   // Zones from OCR: Keep in a memory data structure.
 //                   // Zones to element matching will happen in memory only.
 //               }

 //               // Perform OCR and time-consuming tasks.

 //               using (var context = new ContextTenant(tenant.DatabaseConnectionString))
 //               {
 //                   using (var transaction = context.Database.BeginTransaction())
 //                   {
 //                       try
 //                       {
 //                           //context.Templates.Attach(templates[0]);
 //                           context.SaveChanges();

 //                           // Do not call this line from anywhere else.
 //                           transaction.Commit();
 //                       }
 //                       catch (Exception e)
 //                       {
 //                           transaction.Rollback();
 //                       }
 //                   }
 //               }

 //               result = true;
 //           }
 //           catch (Exception e)
 //           {
 //               exception = e;
 //           }

 //           return (result);
 //       }

 //       public static bool TenantDocumentPerformOcr(Tenant tenant, Document document, out Exception exception)
 //       {
 //           var result = false;

 //           exception = null;

 //           try
 //           {
 //               using (var context = new ContextTenant(tenant.DatabaseConnectionString))
 //               {
 //                   // Call your library Ocr functions and save Document state.
 //               }

 //               result = true;
 //           }
 //           catch (Exception e)
 //           {
 //               exception = e;
 //           }

 //           return (result);
 //       }
 //   }
}
