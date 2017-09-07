using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
	public static class HttpRequestUtilities
	{
		public static string GetClientIpAddressFromHttpRequestBase (HttpRequestBase request)
		{
			// AWS compatibility.
			string ip = request.Headers ["X-Forwarded-For"];

			if (string.IsNullOrEmpty(ip)) { ip = request.UserHostAddress; }

			return (ip);
		}

		public static string GetClientIpAddressFromHttpRequestMessage (HttpRequestMessage request)
		{
			if (request.Properties.ContainsKey("MS_HttpContext"))
			{
				return (((HttpContextWrapper) request.Properties ["MS_HttpContext"]).Request.UserHostAddress);
			}
			else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
			{
				return (((RemoteEndpointMessageProperty) request.Properties [RemoteEndpointMessageProperty.Name]).Address);
			}
			else if (HttpContext.Current != null)
			{
				return (HttpContext.Current.Request.UserHostAddress);
			}
			else
			{
				return (null);
			}
		}
	}
}