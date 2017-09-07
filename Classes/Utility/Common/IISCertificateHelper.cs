using Microsoft.ApplicationInsights;
using Microsoft.Web.Administration;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
    public sealed class IISCertificateHelper
    {
        public static bool IISCertificateConfiguration(TelemetryClient telemetryClient,string site, string clientCertIssuer, string clientCertName, string usernameOfServerUser, string passwordOfServerUser, string base64EncodedCertificateData, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                using (var server = new ServerManager())
                {
                    var siteName = string.Format("{0}_{1}", RoleEnvironment.CurrentRoleInstance.Id, site);
                    telemetryClient.TrackEvent($"TelemetryWeb:: IISConfiguration: {siteName} ");
                    var config = server.GetApplicationHostConfiguration();
                    telemetryClient.TrackEvent($"TelemetryWeb:: IISConfiguration: Start Configurating Access Session ");
                    ConfigureAccessSection(config, siteName);
                    telemetryClient.TrackEvent($"TelemetryWeb:: IISConfiguration: Start iis Client Certificate Mapping Authentication Section ");
                    var iisClientCertificateMappingAuthenticationSection = EnableIisClientCertificateMappingAuthentication(config, siteName);
                    telemetryClient.TrackEvent($"TelemetryWeb:: IISConfiguration: Start Configure Many To One Mappings");
                    ConfigureManyToOneMappings(iisClientCertificateMappingAuthenticationSection, clientCertIssuer, clientCertName, usernameOfServerUser, passwordOfServerUser);
                    telemetryClient.TrackEvent($"TelemetryWeb:: IISConfiguration: Start Configure One To One Mappings");
                    ConfigureOneToOneMappings(iisClientCertificateMappingAuthenticationSection, usernameOfServerUser, passwordOfServerUser, base64EncodedCertificateData);

                    server.CommitChanges();
                    result = true;
                    telemetryClient.TrackEvent($"TelemetryWeb:: IISConfiguration: Result = {result}");
                }
            }
            catch (Exception e)
            {
                telemetryClient.TrackEvent($"TelemetryWeb:: IISConfiguration: Result = {e.ToString()}");
                exception = e;
            }
            return result;
        }
        private static void ConfigureAccessSection(Configuration config, string siteName)
        {
            var accessSection = config.GetSection("system.webServer/security/access", siteName);
            accessSection.SetAttributeValue("sslFlags", "Ssl,SslNegotiateCert,SslRequireCert");
            //accessSection["sslFlags"] = @"Ssl,SslNegotiateCert,SslRequireCert";
        }
        private static void ConfigureOneToOneMappings(ConfigurationSection iisClientCertificateMappingAuthenticationSection, string usernameOfServerUser, string passwordOfServerUser, string base64EncodedCertificateData)//(string site, string rootCertName,string usernameOfServerUser, string passwordOfServerUser,string Base64EncodedCertificateData)
        {
            var oneToOneMappings = iisClientCertificateMappingAuthenticationSection.GetCollection("oneToOneMappings");

            var oneToOneElement = oneToOneMappings.CreateElement("add");
            oneToOneElement.SetAttributeValue("enabled", true);
            oneToOneElement.SetAttributeValue("userName", usernameOfServerUser);
            oneToOneElement.SetAttributeValue("password", passwordOfServerUser);
            oneToOneElement.SetAttributeValue("certificate", base64EncodedCertificateData);
            //oneToOneElement["enabled"] = true;
            //oneToOneElement["userName"] = usernameOfServerUser;
            //oneToOneElement["password"] = passwordOfServerUser;
            //oneToOneElement["certificate"] = base64EncodedCertificateData;

            oneToOneMappings.Add(oneToOneElement);
        }
        private static ConfigurationSection EnableIisClientCertificateMappingAuthentication(Configuration config, string siteName)
        {
            var iisClientCertificateMappingAuthenticationSection = config.GetSection("system.webServer/security/authentication/iisClientCertificateMappingAuthentication", siteName);
            iisClientCertificateMappingAuthenticationSection.SetAttributeValue("enabled", true);
            //iisClientCertificateMappingAuthenticationSection["enabled"] = true;
            return iisClientCertificateMappingAuthenticationSection;
        }
        private static void ConfigureManyToOneMappings(ConfigurationSection iisClientCertificateMappingAuthenticationSection, string clientCertIssuer, string clientCertName, string usernameOfServerUser, string passwordOfServerUser)
        {
            var manyToOneMappings = iisClientCertificateMappingAuthenticationSection.GetCollection("manyToOneMappings");
            var element = manyToOneMappings.CreateElement("add");
            element.SetAttributeValue("enabled", true);
            element.SetAttributeValue("userName", usernameOfServerUser);
            element.SetAttributeValue("password", passwordOfServerUser);
            element.SetAttributeValue("name", "Allowed Clients");
            element.SetAttributeValue("permissionMode", "Allow");


            //element["name"] = @"Allowed Clients";
            //element["enabled"] = true;
            //element["permissionMode"] = @"Allow";
            //element["userName"] = usernameOfServerUser;
            //element["password"] = passwordOfServerUser;

            manyToOneMappings.Add(element);

            ConfigureCertificateRules(element, clientCertIssuer, clientCertName);
        }
        private static void ConfigureCertificateRules(ConfigurationElement element, string clientCertIssuer, string clientCertName)
        {
            var rulesCollection = element.GetCollection("rules");

            var issuerRule = rulesCollection.CreateElement("add");
            issuerRule.SetAttributeValue("certificateField", "Issuer");
            issuerRule.SetAttributeValue("certificateSubField", "CN");
            issuerRule.SetAttributeValue("matchCriteria", clientCertIssuer);
            issuerRule.SetAttributeValue("compareCaseSensitive", true);

            //issuerRule["certificateField"] = @"Issuer";
            //issuerRule["certificateSubField"] = @"CN";
            //issuerRule["matchCriteria"] = clientCertIssuer;
            //issuerRule["compareCaseSensitive"] = true;

            var subjectRule = rulesCollection.CreateElement("add");
            subjectRule.SetAttributeValue("certificateField", "Subject");
            subjectRule.SetAttributeValue("certificateSubField", "CN");
            subjectRule.SetAttributeValue("matchCriteria", clientCertName);
            subjectRule.SetAttributeValue("compareCaseSensitive", true);

            //subjectRule["certificateField"] = @"Subject";
            //subjectRule["certificateSubField"] = @"CN";
            //subjectRule["matchCriteria"] = clientCertName;
            //subjectRule["compareCaseSensitive"] = true;

            rulesCollection.Add(issuerRule);
            rulesCollection.Add(subjectRule);
        }
    }
}