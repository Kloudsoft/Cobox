using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
	public class MasterTenantController:
		Controller
	{
		[MasterMvcTokenAuthorize]
		public ActionResult Index (long? id)
		{
			var tenant = new Tenant();

            try
            {
                bool result = false;
                Exception exception = null;
                MasterUserSession MasterUserSession = null;
                if (id != null)
                {
                    tenant.Id = (long)id;
					if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }

                    result = MasterTenantManagement.GetTenantById(tenant.Id, out tenant, out exception);

                    if (exception != null)
                        throw exception;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                    this.ViewBag.Exception = ex.InnerException.Message;
                else
                    this.ViewBag.Exception = ex.Message;
            }

			return (this.View("~/Views/Master/Tenant.cshtml", tenant));
		}

		
        public ActionResult CreateUpdateTenant(Tenant tenant)
		{
			var result = false;
			Exception exception = null;
            Tenant outtenant = tenant;
            outtenant.RsaKeyPrivate = "none";
            outtenant.RsaKeyPublic = "none";
            try
			{

				MasterUserSession MasterUserSession = null;

				if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }
				if (tenant.Id > 0)
				{
					result = MasterTenantManagement.UpdateTenant(MasterUserSession, tenant, out outtenant, out exception);
					if (exception != null)
						throw exception;

					if (result)
						this.ViewBag.Message = "Record has been saved successfully";
				}
				else
				{

                    var keyPair = HouseOfSynergy.PowerTools.Library.Security.Cryptography.Rsa.GenerateKeyPair(HouseOfSynergy.AffinityDms.Library.GlobalConstants.AlgorithmAsymmetricKeySize);

                    tenant.RsaKeyPrivate = keyPair.KeyPrivate.KeyToString();
                    tenant.RsaKeyPublic = keyPair.KeyPublic.KeyToString();
                    tenant.TenantType = EntityMasterTenantType.Master;
                    tenant.AuthenticationType = AuthenticationType.None;
                    result = MasterTenantManagement.CreateTenant(MasterUserSession, tenant, out outtenant, out exception);
					
					if (exception != null)
						throw exception;

					if (result)
					{
						//DataLayer.Contexts.ContextTenant.Initialize(outtenant.DatabaseConnectionString);
						//var contexttenant = new DataLayer.Contexts.ContextTenant(outtenant.DatabaseConnectionString);
						//DataLayer.Seeders.SeederTenant.Seed(contexttenant, outtenant);
						this.ViewBag.Message = "Record has been saved successfully";
					}
						
				}
			}
			catch (Exception ex)
			{
				this.ViewBag.Exception = ExceptionUtilities.ExceptionToString(ex);
			}
			return (this.View("~/Views/Master/Tenant.cshtml", outtenant));

		}
	}
}