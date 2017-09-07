using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HouseOfSynergy.AffinityDms.Library;


namespace HouseOfSynergy.AffinityDms.WebRole
{
	public static class WebApiConfig
	{
		public static void Register (HttpConfiguration config)
		{

            // Web API configuration and services.
            
            // Web API routes.
            config.MapHttpAttributeRoutes();

			// TODO: Adapt for Web API.
			//foreach (var routeMap in RoutingUtilities.RouteMapsMvc)
			//{
			//	config.Routes.MapHttpRoute(name : routeMap.Value.Name, url : routeMap.Value.Url, defaults : new { controller = routeMap.Value.ControllerName, action = routeMap.Value.Action, id = routeMap.Value.Id, });
			//}
			config.Routes.MapHttpRoute(name : "DefaultApi", routeTemplate : "api/{controller}/{id}", defaults : new { id = RouteParameter.Optional });

		}
	}
}