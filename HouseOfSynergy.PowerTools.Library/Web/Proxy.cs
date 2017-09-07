using System;
using System.Net;
using HouseOfSynergy.PowerTools.Library.Delegates;
using HouseOfSynergy.PowerTools.Library.EventArguments;

namespace HouseOfSynergy.PowerTools.Library.Web
{
	public class Proxy
	{
		public bool Use { get; set; }
		public Uri Address { get; set; }
		public ICredentials Credentials { get; set; }
		public bool BypassProxyOnLocal { get; set; }

		public Proxy ()
		{
			this.Initialize();
		}

		public Proxy (bool use, Uri address, ICredentials credentials, bool bypassProxyOnLocal)
		{
			this.Initialize(use, address, credentials, bypassProxyOnLocal);
		}

		public void Initialize ()
		{
			WebProxy proxy = null;

			try
			{
				proxy = this.GetCurrentWebProxy();

				if (proxy == null)
				{
					this.Use = false;
					this.Address = null;
					this.Credentials = CredentialCache.DefaultNetworkCredentials;
					this.BypassProxyOnLocal = false;
				}
				else
				{
					this.Use = proxy.Address.AbsoluteUri != "";
					this.Address = proxy.Address;
					this.Credentials = proxy.Credentials;
					this.BypassProxyOnLocal = proxy.BypassProxyOnLocal;
				}
			}
			catch
			{
				this.Use = false;
				//this.Address = new System.Uri("");
				this.Credentials = CredentialCache.DefaultNetworkCredentials;
				this.BypassProxyOnLocal = true;
			}
		}

		public void Initialize (bool use, Uri address, ICredentials credentials, bool bypassProxyOnLocal)
		{
			this.Use = use;
			this.Address = address;
			this.Credentials = credentials;
			this.BypassProxyOnLocal = bypassProxyOnLocal;
		}

		public WebProxy GetWebProxyFromCurrentSettings ()
		{
			WebProxy proxy = null;

			if (this.Use)
			{
				proxy = new WebProxy
				(
					this.Address,
					this.BypassProxyOnLocal,
					null,
					this.Credentials
				);
			}

			return (proxy);
		}

		public WebProxy GetCurrentWebProxy ()
		{
			Uri uri = null;
			WebProxy proxy = null;

			using (var client = new WebClient())
			{
				uri = client.Proxy.GetProxy(Global.Instance.ApplicationInfo.ManufacturerUrl);

				if (uri.ToString().ToLower() != Global.Instance.ApplicationInfo.ManufacturerUrl.ToString().ToLower())
				{
					proxy = new WebProxy
					(
						client.Proxy.GetProxy(Global.Instance.ApplicationInfo.ManufacturerUrl),
						client.Proxy.IsBypassed(Global.Instance.ApplicationInfo.ManufacturerUrl),
						null,
						client.Credentials
					);
				}
			}

			return (proxy);
		}
	}
}