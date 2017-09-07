using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
    public class AzureConstants
    {
        public const string TenantName = "benztechAD.onmicrosoft.com";//"GraphDir1.onMicrosoft.com";
        public const string TenantId = "4151cea5-30b2-4c0b-81ca-1010d760bafa";//"4fd2b2f2-ea27-4fe5-a8f3-7b1a7c975f34";
        public const string ClientId = "5ca4c6e0-0860-4484-89ca-d58d6ff9b618";//"118473c2-7619-46e3-a8e4-6da8d5f56e12";
        public const string ClientSecret = "qbmHgxz+pA4e+mTwPcWsBwURfOiVM1XdgdYcdC95Le4=";//"hOrJ0r0TZ4GQ3obp+vk3FZ7JBVP+TX353kNo6QwNq7Q=";
        public const string ClientIdForUserAuthn = "f937f30e-f09e-4238-baf2-14ff13163c01";//"66133929-66a4-4edc-aaee-13b04b03207d";
        public const string AuthString = "https://login.microsoftonline.com/" + TenantName;
        public const string ResourceUrl = "https://graph.windows.net";
    }
}