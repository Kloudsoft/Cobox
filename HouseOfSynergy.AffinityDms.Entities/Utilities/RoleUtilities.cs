using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Utilities
{
	public static class RoleUtilities
	{
		public static ReadOnlyDictionary<MasterRoleType, ReadOnlyCollection<MasterActionType>> MasterRoleActions { get; private set; }
		public static ReadOnlyDictionary<MasterActionType, ReadOnlyCollection<MasterRoleType>> MasterActionRolesAll { get; private set; }
		public static ReadOnlyDictionary<MasterActionType, ReadOnlyCollection<MasterRoleType>> MasterActionRolesAny { get; private set; }

		public static ReadOnlyDictionary<TenantRoleType, ReadOnlyCollection<TenantActionType>> TenantRoleActions { get; private set; }
		public static ReadOnlyDictionary<TenantActionType, ReadOnlyCollection<TenantRoleType>> TenantActionRolesAll { get; private set; }
		public static ReadOnlyDictionary<TenantActionType, ReadOnlyCollection<TenantRoleType>> TenantActionRolesAny { get; private set; }

		static RoleUtilities ()
		{
			//====================================================================================================
			#region Master.
			//===========================================================================Master=========================

			var masterRoleActions = new Dictionary<MasterRoleType, ReadOnlyCollection<MasterActionType>>();
			var masterActionRolesAll = new Dictionary<MasterActionType, ReadOnlyCollection<MasterRoleType>>();
			var masterActionRolesAny = new Dictionary<MasterActionType, ReadOnlyCollection<MasterRoleType>>();
			var masterActionTypes = EnumUtilities.GetValues<MasterActionType>().Where(a => a != MasterActionType.None).ToList().AsReadOnly();

			masterRoleActions.Add(MasterRoleType.Administrator, masterActionTypes);
			masterRoleActions.Add(MasterRoleType.Reporting, new MasterActionType [] { MasterActionType.DashboardView, }.ToList().AsReadOnly());
			masterActionRolesAll.Add(MasterActionType.DashboardView, new MasterRoleType [] { MasterRoleType.Reporting, }.ToList().AsReadOnly());

			foreach (var actionType in EnumUtilities.GetValues<MasterActionType>())
			{
				masterActionRolesAny.Add(actionType, new MasterRoleType [] { MasterRoleType.Administrator, }.ToList().AsReadOnly());
			}

			RoleUtilities.MasterRoleActions = new ReadOnlyDictionary<MasterRoleType, ReadOnlyCollection<MasterActionType>>(masterRoleActions);
			RoleUtilities.MasterActionRolesAll = new ReadOnlyDictionary<MasterActionType, ReadOnlyCollection<MasterRoleType>>(masterActionRolesAll);
			RoleUtilities.MasterActionRolesAny = new ReadOnlyDictionary<MasterActionType, ReadOnlyCollection<MasterRoleType>>(masterActionRolesAny);

			//====================================================================================================
			#endregion Master.
			//====================================================================================================

			//====================================================================================================
			#region Tenants.
			//====================================================================================================

			var tenantRoleActions = new Dictionary<TenantRoleType, ReadOnlyCollection<TenantActionType>>();
			var tenantActionRolesAll = new Dictionary<TenantActionType, ReadOnlyCollection<TenantRoleType>>();
			var tenantActionRolesAny = new Dictionary<TenantActionType, ReadOnlyCollection<TenantRoleType>>();
			var tenantActionTypes = EnumUtilities.GetValues<TenantActionType>().Where(a => a != TenantActionType.None).ToList().AsReadOnly();

			tenantRoleActions.Add(TenantRoleType.Administrator, tenantActionTypes);
			tenantRoleActions.Add(TenantRoleType.Uploader, new TenantActionType [] { TenantActionType.DocumentsView, }.ToList().AsReadOnly());
			tenantActionRolesAll.Add(TenantActionType.DocumentsView, new TenantRoleType [] { TenantRoleType.Uploader, }.ToList().AsReadOnly());

			foreach (var actionType in EnumUtilities.GetValues<TenantActionType>())
			{
				tenantActionRolesAny.Add(actionType, new TenantRoleType [] { TenantRoleType.Administrator, }.ToList().AsReadOnly());
			}

			RoleUtilities.TenantRoleActions = new ReadOnlyDictionary<TenantRoleType, ReadOnlyCollection<TenantActionType>>(tenantRoleActions);
			RoleUtilities.TenantActionRolesAll = new ReadOnlyDictionary<TenantActionType, ReadOnlyCollection<TenantRoleType>>(tenantActionRolesAll);
			RoleUtilities.TenantActionRolesAny = new ReadOnlyDictionary<TenantActionType, ReadOnlyCollection<TenantRoleType>>(tenantActionRolesAny);

			//====================================================================================================
			#endregion Tenants.
			//====================================================================================================
		}
	}
}