using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Utility;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using Newtonsoft.Json;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Data.OleDb;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;


namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class VendorDetailsController : Controller
    {
        // GET: VendorDetails
        [HttpGet]
        public ActionResult Index(string ErrorMessage, string SuccessMessage)
        {
            Exception exception = null;
            TenantUserSession tenantUserSession = null;

            if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

            List<Vendor> vendor = new List<Vendor>();
            try
            {

                //GetMaxVersionDocuments

                bool dbresult = VendorManagement.GetAllVendors(tenantUserSession, out vendor, out exception);

                
                if (exception != null)
                {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
            this.ViewBag.CurrentUser = tenantUserSession.User.Id;

            return this.View("~/Views/Tenants/Vendors/VendorDetails.cshtml", vendor);

        }



        [HttpPost]
        public ActionResult UploadExcel()
        {
            Vendor vendor = new Vendor();
            List<string> data = new List<string>();
            if (Request.Files.Count > 0)
            {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    
                    string filename = file.FileName;
                    string targetpath = Server.MapPath("~/UploadedFiles/");
                    file.SaveAs(targetpath + filename);
                   
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";

                    if (filename.EndsWith(".xls"))
                    {   connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile); }
                    else if (filename.EndsWith(".xlsx"))
                    { connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);}

                    List<string> values = new List<string>();

                    OleDbConnection objConn = new OleDbConnection(connectionString);
                    objConn.Open();
                    OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);
                    OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                    objAdapter1.SelectCommand = objCmdSelect;
                    DataSet objDataset1 = new DataSet();  objAdapter1.Fill(objDataset1); objConn.Close();

                    Exception exception = null;
                    TenantUserSession tenantUserSession = null;
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    foreach (DataRow myDataRow in objDataset1.Tables[0].Rows)
                    {
                                vendor.VendorName = myDataRow[0].ToString();
                                vendor.Gst = myDataRow[1].ToString();
                                vendor.Address = myDataRow[2].ToString();
                                vendor.Phone = myDataRow[3].ToString();
                                vendor.Email = myDataRow[4].ToString();
                                vendor.ContactPerson = myDataRow[5].ToString();
                                vendor.VendorId = 0;
                                try{ bool dbresult = VendorManagement.AddVendor(tenantUserSession, vendor, out exception);if (exception != null){throw exception;}}
                                catch (Exception exx) { }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))  {  System.IO.File.Delete(pathToExcelFile);}
                    return Json("success", JsonRequestBehavior.AllowGet);
            }
            return this.View("~/Views/Tenants/Vendors/VendorDetails.cshtml", vendor);
        }



        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<Vendor> vendor = new List<Vendor>();
            if (ModelState.IsValid)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;
                
                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    bool dbresult = VendorManagement.GetAllVendors(tenantUserSession, out vendor, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                catch (Exception exx) { }

            }

            return Json(vendor.ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, Vendor vendor)
        {
            if (vendor != null && ModelState.IsValid)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;

                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    bool dbresult = VendorManagement.AddVendor(tenantUserSession, vendor, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                catch (Exception exx) { }
                    
                
            }

            return Json(new[] { vendor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Vendor vendor)
        {
            if (vendor != null && ModelState.IsValid)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;

                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    bool dbresult = VendorManagement.UpdateVendor(tenantUserSession, vendor, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                catch (Exception exx) { }


            }
            return Json(new[] { vendor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, Vendor vendor)
        {
            if (vendor != null && ModelState.IsValid)
            {
                Exception exception = null;
                TenantUserSession tenantUserSession = null;

                try
                {
                    if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }
                    bool dbresult = VendorManagement.DeleteVendor(tenantUserSession, vendor, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                }
                catch (Exception exx) { }


            }
            return Json(new[] { vendor }.ToDataSourceResult(request, ModelState));
        }
    }
}