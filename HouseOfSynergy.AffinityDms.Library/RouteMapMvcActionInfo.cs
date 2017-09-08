//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace HouseOfSynergy.AffinityDms.Library
//{
//	public sealed class RouteMapMvcActionInfo
//	{
//		public string Name { get; private set; }
//		public UrlParameter Id { get; private set; }
//		public FormMethod FormMethodType { get; private set; }

//		public RouteMapMvcViewInfo View { get; private set; }
//		public RouteMapMvcControllerInfo Controller { get; private set; }

//		public RouteMapMvcActionInfo (FormMethod formMethodType, string actionName, RouteMapMvcControllerInfo controller = null, RouteMapMvcViewInfo view = null, UrlParameter id = null)
//		{
//			this.Name = actionName;
//			this.FormMethodType = formMethodType;
//			this.Id = id ?? UrlParameter.Optional;

//			this.View = view;
//			this.Controller = controller;
//		}
//	}
//}