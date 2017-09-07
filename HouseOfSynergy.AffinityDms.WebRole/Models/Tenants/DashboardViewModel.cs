using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
	public class DashboardViewModel
	{
		public virtual DateTime DateTimeStart { get; set; }
		public virtual DateTime DateTimeExpires { get; set; }

		public virtual int NumberOfFormsAllowed { get; set; }
		public virtual int NumberOfPagesAllowed { get; set; }
		public virtual int NumberOfUsersAllowed { get; set; }
		public virtual int NumberOfTemplatesAllowed { get; set; }

		public virtual int NumberOfFormsUsed { get; set; }
		public virtual int NumberOfPagesUsed { get; set; }
		public virtual int NumberOfUsersUsed { get; set; }
		public virtual int NumberOfTemplatesUsed { get; set; }

		public virtual bool AllowScanning { get; set; }
		public virtual bool AllowBranding { get; set; }
		public virtual bool AllowTemplateWorkflows { get; set; }
	}
}