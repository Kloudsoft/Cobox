//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HouseOfSynergy.AffinityDms.Library
//{
//	public sealed class RouteMapMvcViewInfo
//	{
//		public string Path { get; private set; }
//		public string Name { get; private set; }
//		public RouteMapMvcViewType Type { get; private set; }

//		public List<RouteMapMvcActionInfo> Actions { get; private set; }

//		public RouteMapMvcViewInfo (RouteMapMvcViewType type, string path, string name, IEnumerable<RouteMapMvcActionInfo> actions = null)
//		{
//			this.Type = type;
//			this.Path = path;
//			this.Name = name;

//			if (actions == null) { this.Actions = new List<RouteMapMvcActionInfo>(); } else { this.Actions = actions.ToList(); }
//		}

//		public string FullName { get { return (this.Path + this.Name); } }
//	}
//}