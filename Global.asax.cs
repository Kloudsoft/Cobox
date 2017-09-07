using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using HouseOfSynergy.AffinityDms.ResourceProvider;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HouseOfSynergy.AffinityDms.WebRole
{
    public class MvcApplication:
		System.Web.HttpApplication
    {
        
        protected void Application_Start()
		{
            //Trace.TraceInformation("WebRole Global App Start");
            //====================================================================================================
            #region Mandatory.
            //====================================================================================================

            AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			//====================================================================================================
			#endregion Mandatory.
			//====================================================================================================

			GlobalConfiguration.Configuration.Formatters.JsonFormatter.Indent = true;
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            
			//====================================================================================================
			#region Third-Party License Registration.
			//====================================================================================================

			// TODO: Add Telerik Kendo UI.
			// TODO: Add Telerik ASP .NET WebForms.
			// TODO: Add Telerik ASP .NET MVC.
			// TODO: Add GrapeCity ActiveAnalysis.
			// TODO: Add VintaSoft Barcode.
			// TODO: Add VintaSoft Imaging.
			// TODO: Add VintaSoft Twain.
			// TODO: Add Xamarin.
			// TODO: Add DevExpress.
			// TODO: Add Infragistics.
			// TODO: Add LeadTools Imaging SDK Suite.

			// NewtonSoft Json Schema.
			Newtonsoft.Json.Schema.License.RegisterLicense("3195-Oaw8M+bhnr9k7KWvyhOnd5hFYgru6qi+6fHKEWXQMqoVq6uWmmPJt2ZbEkr/h/An5LC5K4CVmdRO5iCX2EH/uRd3mgCV7s3b32ea/KAcFzXoFn0FpnIpehlAceUchLy4iMsOB35NyLABCy8UkxMfIkDGa3R3AOUFj9n7cSW3fFt7IklkIjozMTk1LCJFeHBpcnlEYXRlIjoiMjAxNy0wMy0yNFQxNzo1ODozNS4wMTM3ODc3WiIsIlR5cGUiOiJKc29uU2NoZW1hSW5kaWUifQ==");

			//====================================================================================================
			#endregion Third-Party License Registration.
			//====================================================================================================

			//====================================================================================================
			#region Culture & Resource Initialization.
			//====================================================================================================

			{
				Exception exception = null;
				var filename = this.Server.MapPath("App_Data/Resources/Resources.xml");
				if (!ResourceManagement.Load(filename, out exception)) { throw (exception); }
			}

            //====================================================================================================
            #endregion Culture & Resource Initialization.
            //====================================================================================================

            //====================================================================================================
            #region Unclassified.
            //====================================================================================================

            //GlobalConfiguration.Configuration.Filters.Add(new RestAuthorizeAttribute());

            //====================================================================================================
            #endregion Unclassified.
            //====================================================================================================
            //Trace.TraceInformation("WebRole Global App End");
        }
    }
}