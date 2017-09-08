using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
	public enum MasterActionType
	{
		None,
		DashboardView,
		UsersAdd,
		UsersEdit,
		UsersView,
		SubscriptionsAdd,
		SubscriptionsEdit,
		SubscriptionsView,
		TenantsAdd,
		TenantsEdit,
		TenantsView,
		TenantSubscriptionsAdd,
		TenantSubscriptionsEdit,
		TenantSubscriptionsView,
	}

	public enum TenantActionType
	{
		None,
		DashboardView,
		UsersAdd,
		UsersEdit,
		UsersView,
		FoldersAdd,
		FoldersEdit,
		FoldersView,
		TemplatesAdd,
		TemplatesEdit,
		TemplatesView,
		TemplatesDesign,
		FormsAdd,
		FormsEdit,
		FormsView,
		FormsDesign,
		DocumentsAdd,
		DocumentsEdit,
		DocumentsView,
		DocumentsScan,
		DocumentsUpload,
		DocumentsIndex,
		WorkflowAdd,
		WorkflowEdit,
		WorkflowView,
		WorkflowAction,
		WorkflowDesign,
	}
}