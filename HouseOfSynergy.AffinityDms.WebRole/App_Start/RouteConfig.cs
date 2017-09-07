using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole
{
	public class RouteConfig
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			// TODO: Ignore direct controller urls.
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(name : "Home", url : "", defaults : new { controller = "Home", action = "Index", id = UrlParameter.Optional, });

			routes.MapRoute(name : "MasterHome", url : "Master/Home", defaults : new { controller = "MasterHome", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterSignIn", url : "Master/SignIn", defaults : new { controller = "MasterSignIn", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterSignOut", url : "Master/SignOut", defaults : new { controller = "MasterSignOut", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterDashboard", url : "Master/Dashboard", defaults : new { controller = "MasterDashboard", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterUser", url : "Master/User", defaults : new { controller = "MasterUser", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterUsers", url : "Master/Users", defaults : new { controller = "MasterUsers", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterTenant", url : "Master/Tenant", defaults : new { controller = "MasterTenant", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterTenants", url : "Master/Tenants", defaults : new { controller = "MasterTenants", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterSubscription", url : "Master/Subscription", defaults : new { controller = "MasterSubscription", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterSubscriptions", url : "Master/Subscriptions", defaults : new { controller = "MasterSubscriptions", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterTenantSubscription", url : "Master/TenantSubscription", defaults : new { controller = "MasterTenantSubscription", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "MasterTenantSubscriptions", url : "Master/TenantSubscriptions", defaults : new { controller = "MasterTenantSubscriptions", action = "Index", id = UrlParameter.Optional, });

			routes.MapRoute(name : "TenantHome", url : "Tenants/Home", defaults : new { controller = "TenantHome", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantSignIn", url : "Tenants/SignIn", defaults : new { controller = "TenantSignIn", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantSignOut", url : "Tenants/SignOut", defaults : new { controller = "TenantSignOut", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantDashboard", url : "Tenants/Dashboard", defaults : new { controller = "TenantDashboard", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantUser", url : "Tenants/User", defaults : new { controller = "TenantUser", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantUsers", url : "Tenants/Users", defaults : new { controller = "TenantUsers", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantForm", url : "Tenants/Form", defaults : new { controller = "TenantForm", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantFormDesign", url : "Tenants/FormDesign", defaults : new { controller = "TenantFormDesign", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantFormRenderWeb", url : "Tenants/FormRenderWeb", defaults : new { controller = "TenantFormRenderWeb", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantFormRenderMobile", url : "Tenants/FormRenderMobile", defaults : new { controller = "TenantFormRenderMobile", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantFormRenderPrint", url : "Tenants/FormRenderPrint", defaults : new { controller = "TenantFormRenderPrint", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantForms", url : "Tenants/Forms", defaults : new { controller = "TenantForms", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantDocument", url : "Tenants/Document", defaults : new { controller = "TenantDocument", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantDocuments", url : "Tenants/Documents", defaults : new { controller = "TenantDocuments", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantTemplate", url : "Tenants/Template", defaults : new { controller = "TenantTemplate", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantTemplateDesign", url : "Tenants/TemplateDesign", defaults : new { controller = "TenantTemplateDesign", action = "Index", id = UrlParameter.Optional, });
			routes.MapRoute(name : "TenantTemplates", url : "Tenants/Templates", defaults : new { controller = "TenantTemplates", action = "Index", id = UrlParameter.Optional, });
            routes.MapRoute(name:  "TenantTemplateIndexList", url: "Tenants/TenantTemplateIndexList", defaults: new { controller = "TenantTemplateIndexList", action = "Index", id = UrlParameter.Optional, });
            routes.MapRoute(name: "TenantTemplateWorkflow", url: "Tenants/TemplatesWorkflow", defaults: new { controller = "TemplatesWorkflow", action = "Index", id = UrlParameter.Optional, });


            routes.MapRoute(name : "Default", url : "{controller}/{action}/{id}", defaults : new { controller = "Home", action = "Index", id = UrlParameter.Optional, });
		}
	}
}