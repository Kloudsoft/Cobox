//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Mvc;
//using HouseOfSynergy.PowerTools.Library.Extensions;
//using HouseOfSynergy.PowerTools.Library.Utility;

//namespace HouseOfSynergy.AffinityDms.Library
//{
//	public enum RouteMapMvcViewType
//	{
//		None,

//		Home,

//		MasterHome,
//		MasterDashboard,
//		MasterSignIn,
//		MasterSignOut,
//		MasterUser,
//		MasterUserCurrent,
//		MasterUsers,
//		MasterTenant,
//		MasterTenants,
//		MasterSubscription,
//		MasterSubscriptions,
//		MasterTenantSubscription,
//		MasterTenantSubscriptions,

//		TenantHome,
//		TenantDashboard,
//		TenantSignIn,
//		TenantSignOut,
//		TenantUser,
//		TenantUserCurrent,
//		TenantUsers,
//		TenantTemplateView,
//		TenantTemplateAddEdit,
//		TenantTemplateDesign,
//		TenantTemplates,
//		TenantFormViewWeb,
//		TenantFormViewMobile,
//		TenantFormViewPrint,
//		TenantFormAddEdit,
//		TenantFormDesign,
//		TenantForms,
//		TenantDocumentView,
//		TenantDocumentAddEdit,
//		TenantDocumentDesign,
//		TenantDocuments,
//	}

//	public enum RouteMapMvcControllerType
//	{
//		None,

//		Home,

//		MasterHome,
//		MasterSignIn,
//		MasterSignOut,
//		MasterDashboard,
//		MasterUser,
//		MasterUserCurrent,
//		MasterUsers,
//		MasterTenant,
//		MasterTenants,
//		MasterSubscription,
//		MasterSubscriptions,
//		MasterTenantSubscription,
//		MasterTenantSubscriptions,

//		TenantHome,
//		TenantSignIn,
//		TenantSignOut,
//		TenantDashboard,
//		TenantUser,
//		TenantUserCurrent,
//		TenantUsers,
//		TenantForm,
//		TenantFormDesign,
//		TenantFormRenderWeb,
//		TenantFormRenderMobile,
//		TenantFormRenderPrint,
//		TenantForms,
//		TenantDocument,
//		TenantDocuments,
//		TenantTemplate,
//		TenantTemplateDesign,
//		TenantTemplates,
//	}

//	public static partial class RoutingUtilities
//	{
//		internal const string UrlPathSeparator = "/";
//		internal const string PathMvcViews = @"~/Views/";
//		internal const string RazorFileExtension = "cshtml";
//		internal const string PathMvcViewsMaster = RoutingUtilities.PathMvcViews + @"Master/";
//		internal const string PathMvcViewsTenants = RoutingUtilities.PathMvcViews + @"Tenants/";

//		public static ReadOnlyDictionary<RouteMapMvcViewType, RouteMapMvcViewInfo> MvcViews { get; private set; }
//		public static ReadOnlyDictionary<RouteMapMvcControllerType, RouteMapMvcControllerInfo> MvcControllers { get; private set; }

//		static RoutingUtilities ()
//		{
//			var dictionaryMvcViews = new Dictionary<RouteMapMvcViewType, RouteMapMvcViewInfo>();
//			var dictionaryMvcControllers = new Dictionary<RouteMapMvcControllerType, RouteMapMvcControllerInfo>();

//			RoutingUtilities.ConfigureRouteMaps(dictionaryMvcViews, dictionaryMvcControllers);

//			RoutingUtilities.MvcViews = dictionaryMvcViews.ToReadOnlyDictionary();
//			RoutingUtilities.MvcControllers = dictionaryMvcControllers.ToReadOnlyDictionary();
//		}

//		private static void ConfigureRouteMaps (Dictionary<RouteMapMvcViewType, RouteMapMvcViewInfo> views, Dictionary<RouteMapMvcControllerType, RouteMapMvcControllerInfo> controllers)
//		{
//			//====================================================================================================
//			#region Mvc
//			//====================================================================================================

//			//----------------------------------------------------------------------------------------------------
//			#region Mvc Cotrollers.
//			//----------------------------------------------------------------------------------------------------

//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.Home, @"");

//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterHome, @"Master/Home");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterSignIn, @"Master/SignIn");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterSignOut, @"Master/SignOut");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterDashboard, @"Master/Dashboard");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterUser, @"Master/User/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterUserCurrent, @"Master/UserCurrent");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterUsers, @"Master/Users");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterTenant, @"Master/Tenant/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterTenants, @"Master/Tenants");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterSubscription, @"Master/Subscription/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterSubscriptions, @"Master/Subscriptions");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterTenantSubscription, @"Master/TenantSubscription/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.MasterTenantSubscriptions, @"Master/TenantSubscriptions");

//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantHome, @"Tenants/Home");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantSignIn, @"Tenants/SignIn");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantSignOut, @"Tenants/SignOut");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantDashboard, @"Tenants/Dashboard");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantUser, @"Tenants/User/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantUserCurrent, @"Tenants/UserCurrent");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantUsers, @"Tenants/Users");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantForm, @"Tenants/Form/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantFormDesign, @"Tenants/FormDesign/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantFormRenderWeb, @"Tenants/FormRenderWeb/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantFormRenderMobile, @"Tenants/FormRenderMobile/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantFormRenderPrint, @"Tenants/FormRenderPrint/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantForms, @"Tenants/Forms");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantDocument, @"Tenants/Document/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantDocuments, @"Tenants/Documents");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantTemplate, @"Tenants/Template/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantTemplateDesign, @"Tenants/TemplateDesign/{id}");
//			RoutingUtilities.Add(controllers, RouteMapMvcControllerType.TenantTemplates, @"Tenants/Templates");

//			//----------------------------------------------------------------------------------------------------
//			#endregion Mvc Cotrollers.
//			//----------------------------------------------------------------------------------------------------

//			//----------------------------------------------------------------------------------------------------
//			#region Mvc Views.
//			//----------------------------------------------------------------------------------------------------

//			RoutingUtilities.Add(views, RouteMapMvcViewType.Home, RoutingUtilities.PathMvcViews, "Home");

//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterHome, RoutingUtilities.PathMvcViewsMaster + @"", "Home");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterDashboard, RoutingUtilities.PathMvcViewsMaster + @"", "Dashboard");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterSignIn, RoutingUtilities.PathMvcViewsMaster + @"", "SignIn");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterSignOut, RoutingUtilities.PathMvcViewsMaster + @"", "SignOut");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterUser, RoutingUtilities.PathMvcViewsMaster + @"", "User");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterUserCurrent, RoutingUtilities.PathMvcViewsMaster + @"", "UserCurrent");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterUsers, RoutingUtilities.PathMvcViewsMaster + @"", "Users");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterTenant, RoutingUtilities.PathMvcViewsMaster + @"", "Tenant");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterTenants, RoutingUtilities.PathMvcViewsMaster + @"", "Tenants");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterSubscription, RoutingUtilities.PathMvcViewsMaster + @"", "Subscription");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterSubscriptions, RoutingUtilities.PathMvcViewsMaster + @"", "Subscriptions");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterTenantSubscription, RoutingUtilities.PathMvcViewsMaster + @"", "TenantSubscription");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.MasterTenantSubscriptions, RoutingUtilities.PathMvcViewsMaster + @"", "TenantSubscriptions");

//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantHome, RoutingUtilities.PathMvcViewsTenants + @"", "Home");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantDashboard, RoutingUtilities.PathMvcViewsTenants + @"", "Dashboard");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantSignIn, RoutingUtilities.PathMvcViewsTenants + @"", "SignIn");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantSignOut, RoutingUtilities.PathMvcViewsTenants + @"", "SignOut");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantUser, RoutingUtilities.PathMvcViewsTenants + @"Users/", "User");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantUserCurrent, RoutingUtilities.PathMvcViewsTenants + @"Users/", "UserCurrent");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantUsers, RoutingUtilities.PathMvcViewsTenants + @"Users/", "Users");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantTemplateView, RoutingUtilities.PathMvcViewsTenants + @"Templates/", "TemplateView");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantTemplateAddEdit, RoutingUtilities.PathMvcViewsTenants + @"Templates/", "TemplateAddEdit");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantTemplateDesign, RoutingUtilities.PathMvcViewsTenants + @"Templates/", "TemplateDesign");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantTemplates, RoutingUtilities.PathMvcViewsTenants + @"Templates/", "Templates");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantFormViewWeb, RoutingUtilities.PathMvcViewsTenants + @"Forms/", "FormViewWeb");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantFormViewMobile, RoutingUtilities.PathMvcViewsTenants + @"Forms/", "FormViewMobile");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantFormViewPrint, RoutingUtilities.PathMvcViewsTenants + @"Forms/", "FormViewPrint");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantFormAddEdit, RoutingUtilities.PathMvcViewsTenants + @"Forms/", "FormAddEdit");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantFormDesign, RoutingUtilities.PathMvcViewsTenants + @"Forms/", "FormDesign");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantForms, RoutingUtilities.PathMvcViewsTenants + @"Forms/", "Forms");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantDocumentView, RoutingUtilities.PathMvcViewsTenants + @"", "DocumentView");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantDocumentAddEdit, RoutingUtilities.PathMvcViewsTenants + @"", "DocumentAddEdit");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantDocumentDesign, RoutingUtilities.PathMvcViewsTenants + @"", "DocumentDesign");
//			RoutingUtilities.Add(views, RouteMapMvcViewType.TenantDocuments, RoutingUtilities.PathMvcViewsTenants + @"Documents/", "Documents");

//			//----------------------------------------------------------------------------------------------------
//			#endregion Mvc Views.
//			//----------------------------------------------------------------------------------------------------

//			//----------------------------------------------------------------------------------------------------
//			#region Mvc Actions.
//			//----------------------------------------------------------------------------------------------------

//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.Home, RouteMapMvcViewType.Home, FormMethod.Get, "Index");

//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterHome, RouteMapMvcViewType.MasterHome, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterSignIn, RouteMapMvcViewType.MasterSignIn, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterSignIn, RouteMapMvcViewType.MasterSignIn, FormMethod.Post, "SignIn");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterSignOut, RouteMapMvcViewType.MasterSignOut, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterDashboard, RouteMapMvcViewType.MasterDashboard, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterUser, RouteMapMvcViewType.MasterUser, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterUserCurrent, RouteMapMvcViewType.MasterUserCurrent, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterUsers, RouteMapMvcViewType.MasterUsers, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterTenant, RouteMapMvcViewType.MasterTenant, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterTenants, RouteMapMvcViewType.MasterTenants, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterSubscription, RouteMapMvcViewType.MasterSubscription, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterSubscriptions, RouteMapMvcViewType.MasterSubscriptions, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterTenantSubscription, RouteMapMvcViewType.MasterTenantSubscription, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.MasterTenantSubscriptions, RouteMapMvcViewType.MasterTenantSubscriptions, FormMethod.Get, "Index");

//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantHome, RouteMapMvcViewType.TenantHome, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantSignIn, RouteMapMvcViewType.TenantSignIn, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantSignIn, RouteMapMvcViewType.TenantSignIn, FormMethod.Post, "SignIn");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantSignOut, RouteMapMvcViewType.TenantSignOut, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantDashboard, RouteMapMvcViewType.TenantDashboard, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantUser, RouteMapMvcViewType.TenantUser, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantUserCurrent, RouteMapMvcViewType.TenantUserCurrent, FormMethod.Get, "Index");
//			RoutingUtilities.Add(controllers, views, RouteMapMvcControllerType.TenantUsers, RouteMapMvcViewType.TenantUsers, FormMethod.Get, "Index");

//			//----------------------------------------------------------------------------------------------------
//			#endregion Mvc Actions.
//			//----------------------------------------------------------------------------------------------------

//			//====================================================================================================
//			#endregion Mvc
//			//====================================================================================================

//			//====================================================================================================
//			#region Web Api
//			//====================================================================================================

//			//----------------------------------------------------------------------------------------------------
//			#region Web Api Controllers.
//			//----------------------------------------------------------------------------------------------------

//			//----------------------------------------------------------------------------------------------------
//			#endregion Web Api Controllers.
//			//----------------------------------------------------------------------------------------------------

//			//----------------------------------------------------------------------------------------------------
//			#region Web Api Views.
//			//----------------------------------------------------------------------------------------------------

//			//----------------------------------------------------------------------------------------------------
//			#endregion Web Api Views.
//			//----------------------------------------------------------------------------------------------------

//			//====================================================================================================
//			#endregion Web Api
//			//====================================================================================================
//		}

//		/// <summary>
//		/// Add view.
//		/// </summary>
//		private static void Add (Dictionary<RouteMapMvcViewType, RouteMapMvcViewInfo> dictionaryMvcViews, RouteMapMvcViewType routeMapMvcViewType, string path, string name)
//		{
//			dictionaryMvcViews.Add(routeMapMvcViewType, new RouteMapMvcViewInfo(routeMapMvcViewType, path, name + "." + RoutingUtilities.RazorFileExtension));
//		}

//		/// <summary>
//		/// Add controller.
//		/// </summary>
//		private static void Add (Dictionary<RouteMapMvcControllerType, RouteMapMvcControllerInfo> dictionaryMvcControllers, RouteMapMvcControllerType routeMapMvcControllerType, string url)
//		{
//			dictionaryMvcControllers.Add(routeMapMvcControllerType, new RouteMapMvcControllerInfo(routeMapMvcControllerType, url));
//		}

//		/// <summary>
//		/// Add controller action.
//		/// </summary>
//		private static void Add (Dictionary<RouteMapMvcControllerType, RouteMapMvcControllerInfo> dictionaryMvcControllers, Dictionary<RouteMapMvcViewType, RouteMapMvcViewInfo> dictionaryMvcViews, RouteMapMvcControllerType routeMapMvcControllerType, RouteMapMvcViewType routeMapMvcViewType, FormMethod formMethodType, string actionName, UrlParameter id = null)
//		{
//			if (dictionaryMvcViews.ContainsKey(routeMapMvcViewType))
//			{
//				dictionaryMvcControllers [routeMapMvcControllerType].Views.Add(dictionaryMvcViews [routeMapMvcViewType]);
//			}

//			dictionaryMvcControllers [routeMapMvcControllerType].Actions.Add(new RouteMapMvcActionInfo(formMethodType, actionName, dictionaryMvcControllers [routeMapMvcControllerType], dictionaryMvcViews [routeMapMvcViewType], UrlParameter.Optional));
//		}
//	}

//	public interface IMvcControllerRoute<TComtroller>
//		where TComtroller: class, IMvcControllerRoute<TComtroller>, new()
//	{
//		string RouteName { get; }
//		string Url { get; }
//	}
//}