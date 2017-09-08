using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations.Common;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants;
using HouseOfSynergy.AffinityDms.DataLayer.Initializers;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HouseOfSynergy.AffinityDms.DataLayer.Contexts
{
	public class ContextTenant:
		ContextBase
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public DbSet<Culture> Cultures { get; set; }

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Tenant> Tenants { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<TenantSubscription> TenantSubscriptions { get; set; }

		public DbSet<AuditTrailEntry> AuditTrailEntries { get; set; }

		public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Button> Buttons { get; set; }
		public DbSet<Template> Templates { get; set; }

        public DbSet<ClassifiedFileIndexs> ClassifiedFileIndexs { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Discourse> Discussions { get; set; }
        public DbSet<DiscourseUser> DiscussionUsers { get; set; }
        public DbSet<DiscoursePost> DiscussionPosts { get; set; }
        public DbSet<DiscoursePostVersion> DiscussionPostVersions { get; set; }
        public DbSet<DocumentFragment> DocumentFragments { get; set; }
        public DbSet<DiscoursePostVersionAttachment> DiscussionPostAttachements { get; set; }
        public DbSet<TemplateElement> Elements { get; set; }
        public DbSet<TemplateElementValue> ElementValues { get; set; }
        public DbSet<TemplateInstance> TemplateInstances { get; set; }
        public DbSet<TemplateVersion> TemplateVersions { get; set; }
        public DbSet<TemplatePage> TemplatePages { get; set; }
		public DbSet<ScanSession> ScanSessions { get; set; }
		//public DbSet<ScannerSession> ScannerSessions { get; set; }
		//public DbSet<ScannerSessionDetail> ScannerSessionDetails { get; set; }
		//public DbSet<ScanningJob> ScanningJobs { get; set; }
        public DbSet<UserLabel> UserLabels { get; set; }
        public DbSet<UserDelegation> UserDelegations { get; set; }
        public DbSet<RoleDelegation> RoleDelegations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleRight> RoleRights { get; set; }

		public DbSet<DocumentElement> DocumentElements { get; set; }
		public DbSet<DocumentXmlElement> DocumentXmlElements { get; set; }
		public DbSet<DocumentTemplate> DocumentTemplate { get; set; }
		public DbSet<TemplateTag> TemplateTags { get; set; }
		public DbSet<DocumentTag> DocumentTags { get; set; }
		public DbSet<TemplateTagUser> TemplateUserTags { get; set; }
        public DbSet<DocumentTagUser> DocumentUserTags { get; set; }
        public DbSet<UserFolder> UserFolders { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<UserTemplate> UserTemplates { get; set; }

        public DbSet<WorkflowMaster> WorkflowMasters { get; set; }
        public DbSet<EntityWorkflowMapping> EntityWorkflowMappings { get; set; }
        public DbSet<WorkflowStagesInstance> WorkFlowStagesInstances { get; set; }
        public DbSet<WorkflowRuleInstance> WorkFlowRuleInstances { get; set; }
        public DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }
        public DbSet<WorkflowInstance> WorkFlowInstances { get; set; }
        public DbSet<WorkflowActorsInstance> WorkFlowActorsInstances { get; set; }
        public DbSet<WorkflowUserAction> WorkflowUserActions { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
        public DbSet<WorkflowStage> WorkflowStages { get; set; }
        public DbSet<WorkflowActor> WorkflowActors { get; set; }
        public DbSet<WorkflowRule> WorkFlowRules { get; set; }
        public DbSet<RuleDetail> RuleDetails { get; set; }
		public DbSet<TableHistory> TableHistory { get; set; }
		public DbSet<Folder> Folders { get; set; }
        public DbSet<TemplateElementDetail> ElementDetails { get; set; }
        //public DbSet<UserInvite> UserInvites { get; set; }
        public DbSet<DocumentIndex> DocumentIndexes { get; set; }
        public DbSet<DocumentCorrectiveIndexValue> DocumentCorrectiveIndexValues { get; set; }


        public DbSet<DocumentSearchCriteria> DocumentSearchCriteria { get; set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public ContextTenant (string connectionString, bool proxyCreationEnabled = false, bool lazyLoadingEnabled = false)
			: base(connectionString, proxyCreationEnabled, lazyLoadingEnabled)
		{
            ////Debug.Print(connectionString);
			// Not allowed for databases created outside of EF.
			//this.ObjectContext.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
		}

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public ObjectContext ObjectContext { get { return (((IObjectContextAdapter) this).ObjectContext); } }

		#endregion Properties.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention> ();
			modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention> ();

			modelBuilder.Configurations.Add<AuditLog>(new AuditLogConfiguration());

            modelBuilder.Configurations.Add<Log>(new LogConfiguration());

            modelBuilder.Configurations.Add<Vendor>(new VendorConfiguration());


            modelBuilder.Configurations.Add<Document>(new DocumentConfiguration());
			modelBuilder.Configurations.Add<Template>(new TemplateConfiguration());

			modelBuilder.Configurations.Add<User>(new UserConfiguration());
			modelBuilder.Configurations.Add<Role>(new RoleConfiguration());
			modelBuilder.Configurations.Add<Department>(new DepartmentConfiguration());
			modelBuilder.Configurations.Add<Tenant>(new TenantConfiguration());
			modelBuilder.Configurations.Add<Session>(new SessionConfiguration());
			modelBuilder.Configurations.Add<Subscription>(new SubscriptionConfiguration());
			modelBuilder.Configurations.Add<TenantSubscription>(new TenantSubscriptionConfiguration());

			modelBuilder.Configurations.Add<Folder>(new FolderConfiguration());

			modelBuilder.Configurations.Add<UserFolder>(new UserFolderConfiguration());
			modelBuilder.Configurations.Add<UserDocument>(new UserDocumentConfiguration());

			modelBuilder.Configurations.Add<AuditTrailEntry>(new AuditTrailEntryConfiguration());

			modelBuilder.Configurations.Add<Screen>(new ScreenConfiguration());
            modelBuilder.Configurations.Add<Button>(new ButtonConfiguration());

            modelBuilder.Configurations.Add<TemplateElement>(new TemplateElementConfiguration());
            modelBuilder.Configurations.Add<TemplateElementValue>(new TemplateElementValueConfiguration());
            modelBuilder.Configurations.Add<TemplateVersion>(new TemplateVersionConfiguration());
            modelBuilder.Configurations.Add<TemplateInstance>(new TemplateInstanceConfiguration());
            modelBuilder.Configurations.Add<TemplatePage>(new TemplatePageConfiguration());
			modelBuilder.Configurations.Add<ScanSession>(new ScanSessionConfiguration());
			//modelBuilder.Configurations.Add<ScannerSession>(new ScannerSessionConfiguration());
			//modelBuilder.Configurations.Add<ScannerSessionDetail>(new ScannerSessionDetailConfiguration());
			//modelBuilder.Configurations.Add<ScanningJob>(new ScanningJobConfiguration());
			modelBuilder.Configurations.Add<UserLabel>(new UserLabelConfiguration());
            modelBuilder.Configurations.Add<UserDelegation>(new UserDelegationConfiguration());
            modelBuilder.Configurations.Add<RoleDelegation>(new RoleDelegationConfiguration());
            modelBuilder.Configurations.Add<UserRole>(new UserRoleConfiguration());
            modelBuilder.Configurations.Add<RoleRight>(new RoleRightConfiguration());
            modelBuilder.Configurations.Add<WorkflowMaster>(new WorkflowMasterConfiguration());
            modelBuilder.Configurations.Add<EntityWorkflowMapping>(new EntityWorkflowMappingConfiguration());
            modelBuilder.Configurations.Add<WorkflowStagesInstance>(new WorkflowStagesInstanceConfiguration());
            modelBuilder.Configurations.Add<WorkflowRuleInstance>(new WorkflowRuleInstanceConfiguration());
            modelBuilder.Configurations.Add<RuleDetailInstance>(new RuleDetailInstanceConfiguration());
            modelBuilder.Configurations.Add<WorkflowTemplate>(new WorkflowTemplateConfiguration());
            modelBuilder.Configurations.Add<WorkflowInstance>(new WorkflowInstanceConfiguration());
            modelBuilder.Configurations.Add<WorkflowUserActionInstance>(new WorkflowUserActionInstanceConfiguration());
            modelBuilder.Configurations.Add<WorkflowActorsInstance>(new WorkflowActorsInstanceConfiguration());
            modelBuilder.Configurations.Add<WorkflowUserAction>(new WorkflowUserActionConfiguration());
            modelBuilder.Configurations.Add<WorkflowAction>(new WorkflowActionConfiguration());
            modelBuilder.Configurations.Add<WorkflowStage>(new WorkflowStageConfiguration());
            modelBuilder.Configurations.Add<WorkflowActor>(new WorkflowActorConfiguration());
            modelBuilder.Configurations.Add<WorkflowRule>(new WorkflowRuleConfiguration());
            modelBuilder.Configurations.Add<RuleDetail>(new RuleDetailConfiguration());
            modelBuilder.Configurations.Add<TableHistory>(new TableHistoryConfiguration());
            modelBuilder.Configurations.Add<TemplateElementDetail>(new TemplateElementDetailConfiguration());
           // modelBuilder.Configurations.Add<UserInvite>(new UserInviteConfiguration());

			modelBuilder.Configurations.Add<DocumentElement>(new DocumentElementConfiguration());
			modelBuilder.Configurations.Add<DocumentXmlElement>(new DocumentXmlElementConfiguration());
			modelBuilder.Configurations.Add<DocumentTemplate>(new DocumentTemplateConfiguration());
			modelBuilder.Configurations.Add<TemplateTag>(new TemplateTagConfiguration());
			modelBuilder.Configurations.Add<DocumentTag>(new DocumentTagConfiguration());
			modelBuilder.Configurations.Add<TemplateTagUser>(new TemplateTagUserConfiguration());
			modelBuilder.Configurations.Add<DocumentTagUser>(new DocumentTagUserConfiguration());

			modelBuilder.Configurations.Add<DocumentSearchCriteria>(new DocumentSearchCriteriaConfiguration());
            modelBuilder.Configurations.Add<DocumentIndex>(new DocumentIndexConfiguration());

            base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges ()
		{
			var result = 0;

			try
			{
				result = base.SaveChanges();
			}
			catch (DbEntityValidationException exception)
			{
				if (AffinityConfiguration.IsConfigurationDebug)
				{
					Debugger.Break();
					//Debug.Print(exception.ToString());
				}
				else
				{
					throw;
				}
			}
			catch (Exception exception)
			{
				//Debug.Print(exception.ToString());

				throw;
			}

			return (result);
		}

		protected override bool ShouldValidateEntity (DbEntityEntry entityEntry)
		{
			return (base.ShouldValidateEntity(entityEntry));
		}

		protected override DbEntityValidationResult ValidateEntity (DbEntityEntry entityEntry, IDictionary<object, object> items)
		{
			return (base.ValidateEntity(entityEntry, items));
		}

		#endregion Base Overrides.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static bool HasInitializeBeenCalled { get; private set; }

		static ContextTenant ()
		{
			ContextTenant.HasInitializeBeenCalled = false;
		}

		public static void Initialize (string connectionString, bool force = true)
		{
			IDatabaseInitializer<ContextTenant> initializer = null;

			try
			{
				if (ContextTenant.HasInitializeBeenCalled)
				{
					using (var context = new ContextTenant(connectionString))
					{
						context.Database.Initialize(force : true);
					}
				}
				else
				{
					ContextTenant.HasInitializeBeenCalled = true;

					initializer = new CreateDatabaseIfNotExistsInitializer();
					//initializer = new DropCreateDatabaseAlwaysInitializer();
					//initializer = new DropCreateDatabaseIfModelChangesInitializer();

					Database.SetInitializer<ContextTenant>(initializer);

					using (var context = new ContextTenant(connectionString))
					{
						context.Database.Initialize(force : true);
					}
				}
			}
			catch (Exception exception)
			{
				if (AffinityConfiguration.IsConfigurationDebug)
				{
					Debugger.Break();

					if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
					{
						Debug.Print(exception.ToString());
					}
				}
				else
				{
					throw;
				}
			}
		}

		#endregion Static.
	}
}