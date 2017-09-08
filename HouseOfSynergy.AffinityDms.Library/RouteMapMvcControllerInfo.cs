//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HouseOfSynergy.AffinityDms.Library
//{
//	public class RouteMapMvcControllerInfo
//	{
//		public string Url { get; private set; }

//		public RouteMapMvcControllerType Type { get; private set; }

//		public List<RouteMapMvcViewInfo> Views { get; private set; }
//		public List<RouteMapMvcActionInfo> Actions { get; private set; }

//		public RouteMapMvcControllerInfo (RouteMapMvcControllerType type, string url)
//		{
//			this.Url = url;
//			this.Type = type;

//			this.Views = new List<RouteMapMvcViewInfo>();
//			this.Actions = new List<RouteMapMvcActionInfo>();
//		}

//		public string RouteName { get { return (this.Type.ToString()); } }
//		public string ControllerName { get { return (this.Type.ToString()); } }
//	}
//}