using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Net.Http;
using AzureSearchBackupRestore;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Models;
using System.Web.Script.Serialization;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class TestAzureSearchController : Controller
    {
        private IndexSearch _indexsSearch = new IndexSearch();

        private static string TargetSearchServiceName = ConfigurationManager.AppSettings["TargetSearchServiceName"];
        private static string TargetSearchServiceApiKey = ConfigurationManager.AppSettings["TargetSearchServiceApiKey"];
        private static HttpClient HttpClient;
        private static Uri ServiceUri;

        // GET: TestAzureSearch
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult uploadjson(long id)
        {

            var result = uploadjson_fn();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpPost]
        public JsonResult uploadjson_fn()
        {
            ServiceUri = new Uri("https://" + TargetSearchServiceName + ".search.windows.net");
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add("api-key", TargetSearchServiceApiKey);

            // DeleteIndex

            try
            {
                Uri uri = new Uri(ServiceUri, "/indexes/" + "cobox");
                //HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(HttpClient, HttpMethod.Delete, uri);
                //response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) { }


            //CreateTargetIndex

            string json = System.IO.File.ReadAllText(Server.MapPath("~/Schema_and_Data/") + "cobox" + ".schema");
            try
            {
                Uri uri = new Uri(ServiceUri, "/indexes");
               // HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(HttpClient, HttpMethod.Post, uri, json);
               // response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) { }


            //Upload Index DATA

            try
            {
                foreach (string fileName in Directory.GetFiles(Server.MapPath("~/Schema_and_Data/"), "cobox" + "*.json"))
                {
                    Console.WriteLine("Uploading documents from file {0}", fileName);
                    string jsonupload = System.IO.File.ReadAllText(fileName);
                    Uri uri = new Uri(ServiceUri, "/indexes/" + "cobox" + "/docs/index");
                    HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(HttpClient, HttpMethod.Post, uri, jsonupload);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex) { }


            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        public JsonResult searchindex(string searchtxt)
        {

            var Data = searchindex_fn(searchtxt);
            string json = new JavaScriptSerializer().Serialize(Data.Data);

            json = json.Replace("[", "").Replace("]", "");

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpPost]
        public JsonResult searchindex_fn(string searchtxt)
        {

            var response = _indexsSearch.Search(searchtxt);
            return new JsonResult
            {
                // ***************************************************************************************************************************
                // If you get an error here, make sure to check that you updated the SearchServiceName and SearchServiceApiKey in Web.config
                // ***************************************************************************************************************************

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new Indexes() { Results = response.Results }
            };

        }

        [HttpGet]
        public ActionResult Suggest(string term, bool fuzzy = true)
        {
            // Call suggest query and return results
            var response = _indexsSearch.Suggest(term, fuzzy);
            List<string> suggestions = new List<string>();
            foreach (var result in response.Results)
            {
                suggestions.Add(result.Text);
            }

            // Get unique items
            List<string> uniqueItems = suggestions.Distinct().ToList();

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = uniqueItems
            };

        }

        public ActionResult LookUp(string id)
        {
            // Take a key ID and do a lookup to get the job details
            if (id != null)
            {
                var response = _indexsSearch.LookUp(id);
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new IndexesLookup() { Result = response }
                };
            }
            else
            {
                return null;
            }

        }

    }
}