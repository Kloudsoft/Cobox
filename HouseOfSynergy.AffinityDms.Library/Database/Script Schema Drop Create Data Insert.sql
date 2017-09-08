USE [AffinityDmsTenant_0000000000000000001]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.User_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Tenant_TenantId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.User_dbo.Department_DepartmentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_dbo.User_dbo.Department_DepartmentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Template_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Template]'))
ALTER TABLE [dbo].[Template] DROP CONSTRAINT [FK_dbo.Template_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Template_dbo.User_CheckedOutByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Template]'))
ALTER TABLE [dbo].[Template] DROP CONSTRAINT [FK_dbo.Template_dbo.User_CheckedOutByUserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ScanSession_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ScanSession]'))
ALTER TABLE [dbo].[ScanSession] DROP CONSTRAINT [FK_dbo.ScanSession_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.User_UserCreatedById]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder] DROP CONSTRAINT [FK_dbo.Folder_dbo.User_UserCreatedById]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.Folder_ParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder] DROP CONSTRAINT [FK_dbo.Folder_dbo.Folder_ParentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.Department_DepartmentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder] DROP CONSTRAINT [FK_dbo.Folder_dbo.Department_DepartmentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] DROP CONSTRAINT [FK_dbo.Document_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_CheckedOutByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] DROP CONSTRAINT [FK_dbo.Document_dbo.User_CheckedOutByUserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_AssignedToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] DROP CONSTRAINT [FK_dbo.Document_dbo.User_AssignedToUserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_AssignedByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] DROP CONSTRAINT [FK_dbo.Document_dbo.User_AssignedByUserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] DROP CONSTRAINT [FK_dbo.Document_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.ScanSession_ScanSessionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] DROP CONSTRAINT [FK_dbo.Document_dbo.ScanSession_ScanSessionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.Folder_FolderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] DROP CONSTRAINT [FK_dbo.Document_dbo.Folder_FolderId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStage_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStage]'))
ALTER TABLE [dbo].[WorkflowStage] DROP CONSTRAINT [FK_dbo.WorkflowStage_dbo.WorkflowTemplate_WorkflowTemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStage_dbo.WorkflowMaster_WorkflowMasterId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStage]'))
ALTER TABLE [dbo].[WorkflowStage] DROP CONSTRAINT [FK_dbo.WorkflowStage_dbo.WorkflowMaster_WorkflowMasterId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping] DROP CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.WorkflowTemplate_WorkflowTemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.WorkflowMaster_WorkflowMasterId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping] DROP CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.WorkflowMaster_WorkflowMasterId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping] DROP CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Screen_dbo.Screen_ParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
ALTER TABLE [dbo].[Screen] DROP CONSTRAINT [FK_dbo.Screen_dbo.Screen_ParentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateElement_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateElement]'))
ALTER TABLE [dbo].[TemplateElement] DROP CONSTRAINT [FK_dbo.TemplateElement_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStagesInstance_dbo.EntityWorkflowMapping_EntityWorkflowMappingId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStagesInstance]'))
ALTER TABLE [dbo].[WorkflowStagesInstance] DROP CONSTRAINT [FK_dbo.WorkflowStagesInstance_dbo.EntityWorkflowMapping_EntityWorkflowMappingId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActor_dbo.WorkflowStage_WorkflowStageId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActor]'))
ALTER TABLE [dbo].[WorkflowActor] DROP CONSTRAINT [FK_dbo.WorkflowActor_dbo.WorkflowStage_WorkflowStageId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePosts_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]'))
ALTER TABLE [dbo].[DiscoursePosts] DROP CONSTRAINT [FK_dbo.DiscoursePosts_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePosts_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]'))
ALTER TABLE [dbo].[DiscoursePosts] DROP CONSTRAINT [FK_dbo.DiscoursePosts_dbo.Discourses_DiscourseId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Button_dbo.Screen_ScreenId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Button]'))
ALTER TABLE [dbo].[Button] DROP CONSTRAINT [FK_dbo.Button_dbo.Screen_ScreenId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersions_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]'))
ALTER TABLE [dbo].[DiscoursePostVersions] DROP CONSTRAINT [FK_dbo.DiscoursePostVersions_dbo.Discourses_DiscourseId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersions_dbo.DiscoursePosts_PostId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]'))
ALTER TABLE [dbo].[DiscoursePostVersions] DROP CONSTRAINT [FK_dbo.DiscoursePostVersions_dbo.DiscoursePosts_PostId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowRule_dbo.WorkflowStage_WorkflowStageId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowRule]'))
ALTER TABLE [dbo].[WorkflowRule] DROP CONSTRAINT [FK_dbo.WorkflowRule_dbo.WorkflowStage_WorkflowStageId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowRuleInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowRuleInstance]'))
ALTER TABLE [dbo].[WorkflowRuleInstance] DROP CONSTRAINT [FK_dbo.WorkflowRuleInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDelegation_dbo.User_ToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDelegation]'))
ALTER TABLE [dbo].[UserDelegation] DROP CONSTRAINT [FK_dbo.UserDelegation_dbo.User_ToUserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDelegation_dbo.User_FormUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDelegation]'))
ALTER TABLE [dbo].[UserDelegation] DROP CONSTRAINT [FK_dbo.UserDelegation_dbo.User_FormUserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateElementDetail_dbo.TemplateElement_ElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateElementDetail]'))
ALTER TABLE [dbo].[TemplateElementDetail] DROP CONSTRAINT [FK_dbo.TemplateElementDetail_dbo.TemplateElement_ElementId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion] DROP CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateParentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateOriginalId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion] DROP CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateOriginalId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateCurrentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion] DROP CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateCurrentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Session_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_dbo.Session_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Session_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_dbo.Session_dbo.Tenant_TenantId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowAction_dbo.WorkflowActor_WorkflowActorId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowAction]'))
ALTER TABLE [dbo].[WorkflowAction] DROP CONSTRAINT [FK_dbo.WorkflowAction_dbo.WorkflowActor_WorkflowActorId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserAction_dbo.WorkflowStage_WorkflowStagesId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]'))
ALTER TABLE [dbo].[WorkflowUserAction] DROP CONSTRAINT [FK_dbo.WorkflowUserAction_dbo.WorkflowStage_WorkflowStagesId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserAction_dbo.WorkflowAction_WorkflowActionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]'))
ALTER TABLE [dbo].[WorkflowUserAction] DROP CONSTRAINT [FK_dbo.WorkflowUserAction_dbo.WorkflowAction_WorkflowActionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserActionInstance_dbo.WorkflowStagesInstance_WorkflowStageInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]'))
ALTER TABLE [dbo].[WorkflowUserActionInstance] DROP CONSTRAINT [FK_dbo.WorkflowUserActionInstance_dbo.WorkflowStagesInstance_WorkflowStageInstanceId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserActionInstance_dbo.WorkflowAction_WorkflowActionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]'))
ALTER TABLE [dbo].[WorkflowUserActionInstance] DROP CONSTRAINT [FK_dbo.WorkflowUserActionInstance_dbo.WorkflowAction_WorkflowActionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.SessionMessages_dbo.Session_SessionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionMessages]'))
ALTER TABLE [dbo].[SessionMessages] DROP CONSTRAINT [FK_dbo.SessionMessages_dbo.Session_SessionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscourseUsers_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]'))
ALTER TABLE [dbo].[DiscourseUsers] DROP CONSTRAINT [FK_dbo.DiscourseUsers_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscourseUsers_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]'))
ALTER TABLE [dbo].[DiscourseUsers] DROP CONSTRAINT [FK_dbo.DiscourseUsers_dbo.Discourses_DiscourseId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TenantSubscription_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription] DROP CONSTRAINT [FK_dbo.TenantSubscription_dbo.Tenant_TenantId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TenantSubscription_dbo.Subscription_SubscriptionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription] DROP CONSTRAINT [FK_dbo.TenantSubscription_dbo.Subscription_SubscriptionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateInstance_dbo.TemplateVersion_TemplateVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateInstance]'))
ALTER TABLE [dbo].[TemplateInstance] DROP CONSTRAINT [FK_dbo.TemplateInstance_dbo.TemplateVersion_TemplateVersionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplatePage_dbo.TemplateVersion_TemplateVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplatePage]'))
ALTER TABLE [dbo].[TemplatePage] DROP CONSTRAINT [FK_dbo.TemplatePage_dbo.TemplateVersion_TemplateVersionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTag_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTag]'))
ALTER TABLE [dbo].[TemplateTag] DROP CONSTRAINT [FK_dbo.TemplateTag_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTagUser_dbo.User_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]'))
ALTER TABLE [dbo].[TemplateTagUser] DROP CONSTRAINT [FK_dbo.TemplateTagUser_dbo.User_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTagUser_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]'))
ALTER TABLE [dbo].[TemplateTagUser] DROP CONSTRAINT [FK_dbo.TemplateTagUser_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocument_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocument]'))
ALTER TABLE [dbo].[UserDocument] DROP CONSTRAINT [FK_dbo.UserDocument_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocument_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocument]'))
ALTER TABLE [dbo].[UserDocument] DROP CONSTRAINT [FK_dbo.UserDocument_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocumentLabels_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocumentLabels]'))
ALTER TABLE [dbo].[UserDocumentLabels] DROP CONSTRAINT [FK_dbo.UserDocumentLabels_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserFolder_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserFolder]'))
ALTER TABLE [dbo].[UserFolder] DROP CONSTRAINT [FK_dbo.UserFolder_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserFolder_dbo.Folder_FolderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserFolder]'))
ALTER TABLE [dbo].[UserFolder] DROP CONSTRAINT [FK_dbo.UserFolder_dbo.Folder_FolderId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserInvites_dbo.User_User_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserInvites]'))
ALTER TABLE [dbo].[UserInvites] DROP CONSTRAINT [FK_dbo.UserInvites_dbo.User_User_Id]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserLabel_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserLabel]'))
ALTER TABLE [dbo].[UserLabel] DROP CONSTRAINT [FK_dbo.UserLabel_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserRole_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserRole_dbo.Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserTemplates_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTemplates]'))
ALTER TABLE [dbo].[UserTemplates] DROP CONSTRAINT [FK_dbo.UserTemplates_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserTemplates_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTemplates]'))
ALTER TABLE [dbo].[UserTemplates] DROP CONSTRAINT [FK_dbo.UserTemplates_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActorsInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]'))
ALTER TABLE [dbo].[WorkflowActorsInstance] DROP CONSTRAINT [FK_dbo.WorkflowActorsInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActorsInstance_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]'))
ALTER TABLE [dbo].[WorkflowActorsInstance] DROP CONSTRAINT [FK_dbo.WorkflowActorsInstance_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance] DROP CONSTRAINT [FK_dbo.WorkflowInstance_dbo.WorkflowTemplate_WorkflowTemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance] DROP CONSTRAINT [FK_dbo.WorkflowInstance_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance] DROP CONSTRAINT [FK_dbo.WorkflowInstance_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleDelegation_dbo.UserDelegation_UserDelegationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDelegation]'))
ALTER TABLE [dbo].[RoleDelegation] DROP CONSTRAINT [FK_dbo.RoleDelegation_dbo.UserDelegation_UserDelegationId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleDelegation_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDelegation]'))
ALTER TABLE [dbo].[RoleDelegation] DROP CONSTRAINT [FK_dbo.RoleDelegation_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Screen_ScreenId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight] DROP CONSTRAINT [FK_dbo.RoleRight_dbo.Screen_ScreenId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight] DROP CONSTRAINT [FK_dbo.RoleRight_dbo.Role_RoleId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Button_ButtonId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight] DROP CONSTRAINT [FK_dbo.RoleRight_dbo.Button_ButtonId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.User_User_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] DROP CONSTRAINT [FK_dbo.RoleUsers_dbo.User_User_Id]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.Role_Role_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] DROP CONSTRAINT [FK_dbo.RoleUsers_dbo.Role_Role_Id]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.WorkflowRule_WorkflowRuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail] DROP CONSTRAINT [FK_dbo.RuleDetail_dbo.WorkflowRule_WorkflowRuleId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail] DROP CONSTRAINT [FK_dbo.RuleDetail_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail] DROP CONSTRAINT [FK_dbo.RuleDetail_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.WorkflowRuleInstance_WorkFlowRuleInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance] DROP CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.WorkflowRuleInstance_WorkFlowRuleInstanceId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance] DROP CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance] DROP CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditLog_dbo.Screen_Screen_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditLog]'))
ALTER TABLE [dbo].[AuditLog] DROP CONSTRAINT [FK_dbo.AuditLog_dbo.Screen_Screen_Id]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditTrailEntry_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]'))
ALTER TABLE [dbo].[AuditTrailEntry] DROP CONSTRAINT [FK_dbo.AuditTrailEntry_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditTrailEntry_dbo.User_EntityUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]'))
ALTER TABLE [dbo].[AuditTrailEntry] DROP CONSTRAINT [FK_dbo.AuditTrailEntry_dbo.User_EntityUserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] DROP CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] DROP CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] DROP CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Discourses_DiscourseId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.DiscoursePostVersions_PostVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] DROP CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.DiscoursePostVersions_PostVersionId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.User_IndexerId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues] DROP CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.User_IndexerId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.TemplateElement_IndexElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues] DROP CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.TemplateElement_IndexElementId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues] DROP CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.TemplateElementDetail_TemplateElementDetailId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement] DROP CONSTRAINT [FK_dbo.DocumentElement_dbo.TemplateElementDetail_TemplateElementDetailId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.TemplateElement_TemplateElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement] DROP CONSTRAINT [FK_dbo.DocumentElement_dbo.TemplateElement_TemplateElementId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement] DROP CONSTRAINT [FK_dbo.DocumentElement_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentFragments_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentFragments]'))
ALTER TABLE [dbo].[DocumentFragments] DROP CONSTRAINT [FK_dbo.DocumentFragments_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentIndex_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentIndex]'))
ALTER TABLE [dbo].[DocumentIndex] DROP CONSTRAINT [FK_dbo.DocumentIndex_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentSearchCriteria_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentSearchCriteria]'))
ALTER TABLE [dbo].[DocumentSearchCriteria] DROP CONSTRAINT [FK_dbo.DocumentSearchCriteria_dbo.User_UserId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTag_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTag]'))
ALTER TABLE [dbo].[DocumentTag] DROP CONSTRAINT [FK_dbo.DocumentTag_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTagUser_dbo.User_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]'))
ALTER TABLE [dbo].[DocumentTagUser] DROP CONSTRAINT [FK_dbo.DocumentTagUser_dbo.User_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTagUser_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]'))
ALTER TABLE [dbo].[DocumentTagUser] DROP CONSTRAINT [FK_dbo.DocumentTagUser_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTemplate_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]'))
ALTER TABLE [dbo].[DocumentTemplate] DROP CONSTRAINT [FK_dbo.DocumentTemplate_dbo.Template_TemplateId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTemplate_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]'))
ALTER TABLE [dbo].[DocumentTemplate] DROP CONSTRAINT [FK_dbo.DocumentTemplate_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentXmlElement_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentXmlElement]'))
ALTER TABLE [dbo].[DocumentXmlElement] DROP CONSTRAINT [FK_dbo.DocumentXmlElement_dbo.Document_DocumentId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ElementValue_dbo.TemplateElement_ElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementValue]'))
ALTER TABLE [dbo].[ElementValue] DROP CONSTRAINT [FK_dbo.ElementValue_dbo.TemplateElement_ElementId]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ElementValue_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementValue]'))
ALTER TABLE [dbo].[ElementValue] DROP CONSTRAINT [FK_dbo.ElementValue_dbo.Document_DocumentId]
/****** Object:  Table [dbo].[Department]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
DROP TABLE [dbo].[Department]
/****** Object:  Table [dbo].[Tenant]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant]') AND type in (N'U'))
DROP TABLE [dbo].[Tenant]
/****** Object:  Table [dbo].[User]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
/****** Object:  Table [dbo].[Template]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Template]') AND type in (N'U'))
DROP TABLE [dbo].[Template]
/****** Object:  Table [dbo].[ScanSession]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ScanSession]') AND type in (N'U'))
DROP TABLE [dbo].[ScanSession]
/****** Object:  Table [dbo].[Folder]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Folder]') AND type in (N'U'))
DROP TABLE [dbo].[Folder]
/****** Object:  Table [dbo].[Document]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Document]') AND type in (N'U'))
DROP TABLE [dbo].[Document]
/****** Object:  Table [dbo].[WorkflowMaster]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowMaster]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowMaster]
/****** Object:  Table [dbo].[WorkflowTemplate]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowTemplate]
/****** Object:  Table [dbo].[WorkflowStage]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowStage]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowStage]
/****** Object:  Table [dbo].[Discourses]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Discourses]') AND type in (N'U'))
DROP TABLE [dbo].[Discourses]
/****** Object:  Table [dbo].[EntityWorkflowMapping]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]') AND type in (N'U'))
DROP TABLE [dbo].[EntityWorkflowMapping]
/****** Object:  Table [dbo].[Screen]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Screen]') AND type in (N'U'))
DROP TABLE [dbo].[Screen]
/****** Object:  Table [dbo].[TemplateElement]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateElement]') AND type in (N'U'))
DROP TABLE [dbo].[TemplateElement]
/****** Object:  Table [dbo].[WorkflowStagesInstance]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowStagesInstance]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowStagesInstance]
/****** Object:  Table [dbo].[WorkflowActor]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowActor]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowActor]
/****** Object:  Table [dbo].[DiscoursePosts]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]') AND type in (N'U'))
DROP TABLE [dbo].[DiscoursePosts]
/****** Object:  Table [dbo].[Button]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Button]') AND type in (N'U'))
DROP TABLE [dbo].[Button]
/****** Object:  Table [dbo].[DiscoursePostVersions]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]') AND type in (N'U'))
DROP TABLE [dbo].[DiscoursePostVersions]
/****** Object:  Table [dbo].[Role]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
/****** Object:  Table [dbo].[WorkflowRule]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowRule]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowRule]
/****** Object:  Table [dbo].[WorkflowRuleInstance]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowRuleInstance]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowRuleInstance]
/****** Object:  Table [dbo].[UserDelegation]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDelegation]') AND type in (N'U'))
DROP TABLE [dbo].[UserDelegation]
/****** Object:  Table [dbo].[TemplateElementDetail]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateElementDetail]') AND type in (N'U'))
DROP TABLE [dbo].[TemplateElementDetail]
/****** Object:  Table [dbo].[Subscription]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subscription]') AND type in (N'U'))
DROP TABLE [dbo].[Subscription]
/****** Object:  Table [dbo].[TemplateVersion]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateVersion]') AND type in (N'U'))
DROP TABLE [dbo].[TemplateVersion]
/****** Object:  Table [dbo].[Session]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
DROP TABLE [dbo].[Session]
/****** Object:  Table [dbo].[WorkflowAction]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowAction]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowAction]
/****** Object:  Table [dbo].[WorkflowUserAction]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowUserAction]
/****** Object:  Table [dbo].[WorkflowUserActionInstance]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowUserActionInstance]
/****** Object:  Table [dbo].[SessionMessages]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionMessages]') AND type in (N'U'))
DROP TABLE [dbo].[SessionMessages]
/****** Object:  Table [dbo].[DiscourseUsers]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]') AND type in (N'U'))
DROP TABLE [dbo].[DiscourseUsers]
/****** Object:  Table [dbo].[TableHistory]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TableHistory]') AND type in (N'U'))
DROP TABLE [dbo].[TableHistory]
/****** Object:  Table [dbo].[TenantSubscription]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription]') AND type in (N'U'))
DROP TABLE [dbo].[TenantSubscription]
/****** Object:  Table [dbo].[TemplateInstance]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateInstance]') AND type in (N'U'))
DROP TABLE [dbo].[TemplateInstance]
/****** Object:  Table [dbo].[TemplatePage]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplatePage]') AND type in (N'U'))
DROP TABLE [dbo].[TemplatePage]
/****** Object:  Table [dbo].[TemplateTag]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateTag]') AND type in (N'U'))
DROP TABLE [dbo].[TemplateTag]
/****** Object:  Table [dbo].[TemplateTagUser]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]') AND type in (N'U'))
DROP TABLE [dbo].[TemplateTagUser]
/****** Object:  Table [dbo].[UserDocument]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDocument]') AND type in (N'U'))
DROP TABLE [dbo].[UserDocument]
/****** Object:  Table [dbo].[UserDocumentLabels]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDocumentLabels]') AND type in (N'U'))
DROP TABLE [dbo].[UserDocumentLabels]
/****** Object:  Table [dbo].[UserFolder]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserFolder]') AND type in (N'U'))
DROP TABLE [dbo].[UserFolder]
/****** Object:  Table [dbo].[UserInvites]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInvites]') AND type in (N'U'))
DROP TABLE [dbo].[UserInvites]
/****** Object:  Table [dbo].[UserLabel]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLabel]') AND type in (N'U'))
DROP TABLE [dbo].[UserLabel]
/****** Object:  Table [dbo].[UserRole]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
DROP TABLE [dbo].[UserRole]
/****** Object:  Table [dbo].[UserTemplates]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTemplates]') AND type in (N'U'))
DROP TABLE [dbo].[UserTemplates]
/****** Object:  Table [dbo].[WorkflowActorsInstance]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowActorsInstance]
/****** Object:  Table [dbo].[WorkflowInstance]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowInstance]
/****** Object:  Table [dbo].[RoleDelegation]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleDelegation]') AND type in (N'U'))
DROP TABLE [dbo].[RoleDelegation]
/****** Object:  Table [dbo].[RoleRight]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleRight]') AND type in (N'U'))
DROP TABLE [dbo].[RoleRight]
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND type in (N'U'))
DROP TABLE [dbo].[RoleUsers]
/****** Object:  Table [dbo].[RuleDetail]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RuleDetail]') AND type in (N'U'))
DROP TABLE [dbo].[RuleDetail]
/****** Object:  Table [dbo].[RuleDetailInstance]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]') AND type in (N'U'))
DROP TABLE [dbo].[RuleDetailInstance]
/****** Object:  Table [dbo].[Culture]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Culture]') AND type in (N'U'))
DROP TABLE [dbo].[Culture]
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__MigrationHistory]
/****** Object:  Table [dbo].[AuditLog]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog]') AND type in (N'U'))
DROP TABLE [dbo].[AuditLog]
/****** Object:  Table [dbo].[AuditTrailEntry]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]') AND type in (N'U'))
DROP TABLE [dbo].[AuditTrailEntry]
/****** Object:  Table [dbo].[DiscoursePostVersionAttachments]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]') AND type in (N'U'))
DROP TABLE [dbo].[DiscoursePostVersionAttachments]
/****** Object:  Table [dbo].[DocumentCorrectiveIndexValues]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentCorrectiveIndexValues]
/****** Object:  Table [dbo].[DocumentElement]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentElement]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentElement]
/****** Object:  Table [dbo].[DocumentFragments]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentFragments]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentFragments]
/****** Object:  Table [dbo].[DocumentIndex]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentIndex]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentIndex]
/****** Object:  Table [dbo].[DocumentSearchCriteria]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentSearchCriteria]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentSearchCriteria]
/****** Object:  Table [dbo].[DocumentTag]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentTag]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentTag]
/****** Object:  Table [dbo].[DocumentTagUser]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentTagUser]
/****** Object:  Table [dbo].[DocumentTemplate]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentTemplate]
/****** Object:  Table [dbo].[DocumentXmlElement]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentXmlElement]') AND type in (N'U'))
DROP TABLE [dbo].[DocumentXmlElement]
/****** Object:  Table [dbo].[ElementValue]    Script Date: 6/30/2017 7:39:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ElementValue]') AND type in (N'U'))
DROP TABLE [dbo].[ElementValue]
/****** Object:  Table [dbo].[WorkflowTemplate]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowTemplate](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoOfStages] [int] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowMaster]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowMaster](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoofStages] [int] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[TableHistory]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TableHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TableHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Action] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [datetime] NOT NULL,
	[RowId] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DataType] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueOldBinary] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueOldText] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueOldGeographic] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueNewBinary] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueNewGeographic] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueNewText] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.TableHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Subscription]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subscription]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Subscription](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MasterSubscriptionId] [bigint] NOT NULL,
	[SubscriptionType] [int] NOT NULL,
	[IsDemo] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NumberOfFormsAllowed] [int] NOT NULL,
	[NumberOfPagesAllowed] [int] NOT NULL,
	[NumberOfUsersAllowed] [int] NOT NULL,
	[NumberOfTemplatesAllowed] [int] NOT NULL,
	[NumberOfFormsUsed] [int] NOT NULL,
	[NumberOfPagesUsed] [int] NOT NULL,
	[NumberOfUsersUsed] [int] NOT NULL,
	[NumberOfTemplatesUsed] [int] NOT NULL,
	[AllowScanning] [bit] NOT NULL,
	[AllowBranding] [bit] NOT NULL,
	[AllowTemplateWorkflows] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Subscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Screen]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Screen]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Screen](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Screen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Tenant]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tenant](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MasterTenantId] [bigint] NOT NULL,
	[TenantType] [int] NOT NULL,
	[MasterTenantToken] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AuthenticationType] [int] NOT NULL,
	[Domain] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompanyName] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContactOwnerNameGiven] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerNameFamily] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerAddress] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerCity] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerState] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerZipCode] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerCountry] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerPhone] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerFax] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactOwnerEmail] [nvarchar](254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorNameGiven] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorNameFamily] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorAddress] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorCity] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorState] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorZipCode] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorCountry] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorPhone] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorFax] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactAdministratorEmail] [nvarchar](254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingNameGiven] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingNameFamily] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingAddress] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingCity] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingState] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingZipCode] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingCountry] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingPhone] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingFax] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactBillingEmail] [nvarchar](254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalNameGiven] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalNameFamily] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalAddress] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalCity] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalState] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalZipCode] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalCountry] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalPhone] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalFax] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactTechnicalEmail] [nvarchar](254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RsaKeyPublic] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RsaKeyPrivate] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UrlApi] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UrlResourceGroup] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UrlStorage] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UrlStorageBlob] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UrlStorageTable] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UrlStorageQueue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UrlStorageFile] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StorageAccessKeyPrimary] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StorageAccessKeySecondary] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StorageConnectionStringPrimary] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StorageConnectionStringSecondary] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DatabaseConnectionString] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.Tenant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Role]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Discourses]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Discourses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Discourses](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.Discourses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContextKey] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Department]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Department](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Culture]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Culture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Culture](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LocaleId] [int] NOT NULL,
	[NameNative] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameDisplay] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameEnglish] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameIsoTwoLetter] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameIsoThreeLetter] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameWindowsThreeLetter] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.Culture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Button]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Button]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Button](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ScreenId] [bigint] NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.Button] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[AuditLog]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AuditLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LogTime] [datetime] NOT NULL,
	[ActiveUserId] [int] NOT NULL,
	[ActualUserId] [int] NOT NULL,
	[ScreenId] [int] NOT NULL,
	[FieldName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PreviousValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NewValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Variance] [int] NOT NULL,
	[Screen_Id] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.AuditLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowStage]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowStage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowStage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StageNo] [bigint] NOT NULL,
	[StageDescription] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsVoting] [bit] NOT NULL,
	[WorkflowMasterId] [bigint] NOT NULL,
	[WorkflowTemplateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowStage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[User]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TenantId] [bigint] NOT NULL,
	[Email] [nvarchar](254) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PasswordHash] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PasswordSalt] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameGiven] [nvarchar](35) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameFamily] [nvarchar](35) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address1] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address2] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[City] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ZipOrPostCode] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Country] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PhoneWork] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PhoneMobile] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[InviteUrl] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InviteGuid] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[AuthenticationType] [int] NOT NULL,
	[DepartmentId] [bigint] NULL,
	[ActiveDirectory_NameDisplay] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActiveDirectory_ObjectId] [uniqueidentifier] NOT NULL,
	[ActiveDirectory_UsageLocation] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActiveDirectory_JobTitle] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActiveDirectory_Department] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActiveDirectory_ManagerId] [uniqueidentifier] NOT NULL,
	[ActiveDirectory_AuthenticationPhone] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActiveDirectory_AuthenticationPhoneAlternate] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActiveDirectory_AuthenticationEmail] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActiveDirectory_RoleDisplayName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[TenantSubscription]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TenantSubscription](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MasterTenantSubscriptionId] [bigint] NOT NULL,
	[TenantSubscriptionType] [int] NOT NULL,
	[IsDemo] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateTimeStart] [datetime] NOT NULL,
	[DateTimeExpires] [datetime] NOT NULL,
	[NumberOfFormsAllowed] [int] NOT NULL,
	[NumberOfPagesAllowed] [int] NOT NULL,
	[NumberOfUsersAllowed] [int] NOT NULL,
	[NumberOfTemplatesAllowed] [int] NOT NULL,
	[NumberOfFormsUsed] [int] NOT NULL,
	[NumberOfPagesUsed] [int] NOT NULL,
	[NumberOfUsersUsed] [int] NOT NULL,
	[NumberOfTemplatesUsed] [int] NOT NULL,
	[AllowScanning] [bit] NOT NULL,
	[AllowBranding] [bit] NOT NULL,
	[AllowTemplateWorkflows] [bit] NOT NULL,
	[RequireDelegationAcceptance] [bit] NOT NULL,
	[Time] [datetime] NOT NULL,
	[TenantId] [bigint] NOT NULL,
	[SubscriptionId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.TenantSubscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserRole]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserLabel]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLabel]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserLabel](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[LabelName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.UserLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserInvites]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInvites]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserInvites](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InviterUserId] [bigint] NOT NULL,
	[InviteeUserId] [bigint] NOT NULL,
	[URL] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[URLExpiryDate] [datetime] NOT NULL,
	[URLCreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[User_Id] [bigint] NULL,
 CONSTRAINT [PK_dbo.UserInvites] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowActor]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowActor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowActor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkflowStageId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowActor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserDelegation]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDelegation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserDelegation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[ActiveTag] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[FormUserId] [bigint] NULL,
	[ToUserId] [bigint] NULL,
 CONSTRAINT [PK_dbo.UserDelegation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Session]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Session](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[TenantId] [bigint] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[SessionId] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SessionType] [int] NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[DateTimeExpiration] [datetime] NOT NULL,
	[Token] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserAgent] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DeviceType] [int] NOT NULL,
	[IPAddressString] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RsaKeyPublic] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RsaKeyPrivate] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RijndaelKey] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RijndaelInitializationVector] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CultureName] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowRule]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowRule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowRule](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkflowStageId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowRule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Folder]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Folder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Folder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FolderType] [int] NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[DateTimeModified] [datetime] NULL,
	[UserCreatedById] [bigint] NOT NULL,
	[ParentId] [bigint] NULL,
	[DepartmentId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Folder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[DiscoursePosts]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DiscoursePosts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DiscourseId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DiscoursePosts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[AuditTrailEntry]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AuditTrailEntry](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TransactionId] [bigint] NULL,
	[DateTime] [datetime] NOT NULL,
	[EntityType] [int] NOT NULL,
	[EntityTypeId] [bigint] NOT NULL,
	[AuditTrailActionType] [int] NOT NULL,
	[Description] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [bigint] NOT NULL,
	[EntityUserId] [bigint] NULL,
	[EntityDocumentOriginalId] [bigint] NULL,
	[EntityDocumentParentId] [bigint] NULL,
	[EntityDiscourseId] [bigint] NULL,
	[EntityDiscoursePostId] [bigint] NULL,
	[EntityDiscoursePostVersionId] [bigint] NULL,
	[EntityDiscoursePostVersionAttachmentId] [bigint] NULL,
 CONSTRAINT [PK_dbo.AuditTrailEntry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[Template]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Template]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Template](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityState] [int] NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TemplateType] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsFinalized] [bit] NOT NULL,
	[TemplateImage] [varbinary](max) NULL,
	[TemplateOriginalId] [bigint] NOT NULL,
	[TemplateParent] [bigint] NULL,
	[VersionCount] [int] NULL,
	[CheckedOut] [bit] NOT NULL,
	[VersionMajor] [int] NOT NULL,
	[VersionMinor] [int] NOT NULL,
	[CheckedOutByUserId] [bigint] NOT NULL,
	[CheckedOutDateTime] [datetime] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Template] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentSearchCriteria]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentSearchCriteria]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentSearchCriteria](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [bigint] NOT NULL,
	[DateTimeFrom] [datetime] NULL,
	[DateTimeUpTo] [datetime] NULL,
	[TagsUser] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TagsGlobal] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Content] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Filename] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FolderName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TemplateName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.DocumentSearchCriteria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[DiscourseUsers]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DiscourseUsers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DiscourseId] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DiscourseUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleUsers](
	[Role_Id] [bigint] NOT NULL,
	[User_Id] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.RoleUsers] PRIMARY KEY CLUSTERED 
(
	[Role_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[RoleRight]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleRight]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleRight](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[ScreenId] [bigint] NOT NULL,
	[ButtonId] [bigint] NULL,
 CONSTRAINT [PK_dbo.RoleRight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[ScanSession]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ScanSession]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ScanSession](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Finalized] [bit] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.ScanSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[SessionMessages]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionMessages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SessionMessages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SessionMessageType] [int] NOT NULL,
	[SessionId] [bigint] NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.SessionMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[RoleDelegation]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleDelegation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleDelegation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserDelegationId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.RoleDelegation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[TemplateVersion]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TemplateVersion](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateCurrentId] [bigint] NOT NULL,
	[TemplateOriginalId] [bigint] NOT NULL,
	[TemplateParentId] [bigint] NULL,
	[VersionMajor] [int] NOT NULL,
	[VersionMinor] [int] NOT NULL,
 CONSTRAINT [PK_dbo.TemplateVersion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[TemplateTagUser]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TemplateTagUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TemplateId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.TemplateTagUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[TemplateTag]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateTag]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TemplateTag](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TemplateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.TemplateTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DiscoursePostVersions]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DiscoursePostVersions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Comments] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PostId] [bigint] NOT NULL,
	[DiscourseId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DiscoursePostVersions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[Document]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Document]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Document](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateId] [bigint] NULL,
	[FolderId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Length] [bigint] NOT NULL,
	[Hash] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Uploaded] [bit] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsPrivate] [bit] NOT NULL,
	[SourceType] [int] NOT NULL,
	[FullTextOCRXML] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDigital] [bit] NOT NULL,
	[DeviceName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileNameClient] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileNameServer] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ScanSessionId] [bigint] NULL,
	[IsCancelled] [bit] NOT NULL,
	[IsInTransit] [bit] NOT NULL,
	[DocumentType] [int] NOT NULL,
	[DocumentQueueType] [int] NOT NULL,
	[DocumentIndexType] [int] NOT NULL,
	[State] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Confidence] [int] NULL,
	[CountAttemptOcr] [int] NOT NULL,
	[IsFinalized] [bit] NOT NULL,
	[DocumentOriginalId] [bigint] NOT NULL,
	[DocumentParent] [bigint] NULL,
	[VersionCount] [int] NOT NULL,
	[VersionMajor] [int] NOT NULL,
	[VersionMinor] [int] NOT NULL,
	[CheckedOutByUserId] [bigint] NOT NULL,
	[CheckedOutDateTime] [datetime] NOT NULL,
	[WorkflowState] [int] NOT NULL,
	[ProcessInstanceId] [uniqueidentifier] NULL,
	[SchemeCode] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IndexingLevel] [int] NOT NULL,
	[IndexingIteration] [int] NOT NULL,
	[AssignedToUserId] [bigint] NULL,
	[AssignedByUserId] [bigint] NULL,
	[AssignmentState] [int] NOT NULL,
	[AssignedDate] [datetime] NULL,
	[AcceptAssignmentDate] [datetime] NULL,
	[LatestCheckedOutByUserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[RuleDetail]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RuleDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RuleDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Condition] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Value] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Action] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TemplateField] [bigint] NOT NULL,
	[WorkflowRuleId] [bigint] NOT NULL,
	[TemplateId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.RuleDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[TemplateElement]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TemplateElement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ElementId] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ElementType] [int] NOT NULL,
	[ElementDataType] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Text] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[X] [real] NOT NULL,
	[Y] [real] NOT NULL,
	[X2] [real] NULL,
	[Y2] [real] NULL,
	[Radius] [real] NULL,
	[Diameter] [real] NULL,
	[Width] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Height] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DivX] [real] NULL,
	[DivY] [real] NULL,
	[DivWidth] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DivHeight] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MinHeight] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MinWidth] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ForegroundColor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BackgroundColor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Hint] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MinChar] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaxChar] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateTime] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FontName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FontSize] [real] NULL,
	[FontStyle] [int] NULL,
	[FontColor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BorderStyle] [int] NULL,
	[BarcodeType] [int] NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ImageSource] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SizeMode] [int] NOT NULL,
	[IsSelected] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FontGraphicsUnit] [int] NOT NULL,
	[ColorForegroundA] [int] NULL,
	[ColorForegroundR] [int] NULL,
	[ColorForegroundG] [int] NULL,
	[ColorForegroundB] [int] NULL,
	[ColorBackroundA] [int] NULL,
	[ColorBackroundR] [int] NULL,
	[ColorBackroundG] [int] NULL,
	[ColorBackroundB] [int] NULL,
	[Data] [varbinary](max) NULL,
	[ElementMobileOrdinal] [int] NULL,
	[ElementIndexType] [int] NULL,
	[DocumentIndexDataType] [int] NOT NULL,
	[BarcodeValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Discriminator] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TemplateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.TemplateElement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowAction]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowAction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowAction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ActionDescription] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkflowActorId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserTemplates]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTemplates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserTemplates](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TemplateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.UserTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserFolder]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserFolder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserFolder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FolderId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.UserFolder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserDocumentLabels]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDocumentLabels]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserDocumentLabels](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[LabelId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.UserDocumentLabels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[UserDocument]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDocument]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserDocument](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DocumentId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.UserDocument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowUserAction]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowUserAction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkflowActionId] [bigint] NOT NULL,
	[WorkflowStagesId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowUserAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowInstance]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowInstance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkflowTemplateId] [bigint] NOT NULL,
	[TemplateId] [bigint] NOT NULL,
	[DocumentId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[EntityWorkflowMapping]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EntityWorkflowMapping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkflowMasterId] [bigint] NOT NULL,
	[WorkflowTemplateId] [bigint] NOT NULL,
	[DocumentId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.EntityWorkflowMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[ElementValue]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ElementValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ElementValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentId] [bigint] NULL,
	[ElementId] [bigint] NOT NULL,
	[Value] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.ElementValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentXmlElement]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentXmlElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentXmlElement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OcrXml] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OcrText] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DocumentId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentXmlElement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentTemplate]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentTemplate](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[TemplateId] [bigint] NOT NULL,
	[Confidence] [int] NULL,
 CONSTRAINT [PK_dbo.DocumentTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentTagUser]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentTagUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentTagUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentTag]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentTag]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentTag](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DocumentId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DiscoursePostVersionAttachments]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DiscoursePostVersionAttachments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttachmentType] [int] NOT NULL,
	[PostVersionId] [bigint] NOT NULL,
	[TemplateId] [bigint] NULL,
	[DocumentId] [bigint] NULL,
	[DiscourseId] [bigint] NULL,
	[FileNameClient] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileNameServer] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.DiscoursePostVersionAttachments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[TemplatePage]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplatePage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TemplatePage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateVersionId] [bigint] NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Number] [bigint] NOT NULL,
	[Width] [real] NOT NULL,
	[Height] [real] NOT NULL,
	[ImageBackground] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.TemplatePage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[TemplateInstance]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TemplateInstance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateVersionId] [bigint] NOT NULL,
	[Number] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.TemplateInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[TemplateElementDetail]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemplateElementDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TemplateElementDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ElementId] [bigint] NOT NULL,
	[ElementDetailId] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ElementType] [int] NOT NULL,
	[Name] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Text] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[X] [real] NOT NULL,
	[Y] [real] NOT NULL,
	[Width] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Height] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ForegroundColor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BackgroundColor] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BorderStyle] [int] NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SizeMode] [int] NULL,
	[ElementDivId] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TemplateId] [bigint] NULL,
	[Discriminator] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.TemplateElementDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentCorrectiveIndexValues]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentCorrectiveIndexValues](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[IndexElementId] [bigint] NOT NULL,
	[IndexerId] [bigint] NOT NULL,
	[IndexValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentCorrectiveIndexValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentIndex]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentIndex]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentIndex](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [nvarchar](999) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DataType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentIndex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentFragments]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentFragments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentFragments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FullTextOcr] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocumentId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentFragments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[DocumentElement]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentElement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OcrXml] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OcrText] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocumentId] [bigint] NOT NULL,
	[TemplateElementId] [bigint] NOT NULL,
	[Confidience] [bigint] NOT NULL,
	[TemplateElementDetailId] [bigint] NULL,
 CONSTRAINT [PK_dbo.DocumentElement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowStagesInstance]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowStagesInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowStagesInstance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StageNo] [int] NOT NULL,
	[StageDescription] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsVoting] [bit] NOT NULL,
	[EntityWorkflowMappingId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowStagesInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowUserActionInstance]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowUserActionInstance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkflowStageInstanceId] [bigint] NOT NULL,
	[WorkflowActionId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowUserActionInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowRuleInstance]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowRuleInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowRuleInstance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkFlowStagesInstanceId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowRuleInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[WorkflowActorsInstance]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkflowActorsInstance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkFlowStagesInstanceId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.WorkflowActorsInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/****** Object:  Table [dbo].[RuleDetailInstance]    Script Date: 6/30/2017 7:40:02 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RuleDetailInstance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Condition] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Value] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Action] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkFlowRuleInstanceId] [bigint] NOT NULL,
	[TemplateId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.RuleDetailInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__ScanSessio__Guid__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ScanSession] ADD  DEFAULT (newsequentialid()) FOR [Guid]
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Screen_dbo.Screen_ParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
ALTER TABLE [dbo].[Screen]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Screen_dbo.Screen_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Screen] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Screen_dbo.Screen_ParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Screen]'))
ALTER TABLE [dbo].[Screen] CHECK CONSTRAINT [FK_dbo.Screen_dbo.Screen_ParentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Button_dbo.Screen_ScreenId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Button]'))
ALTER TABLE [dbo].[Button]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Button_dbo.Screen_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screen] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Button_dbo.Screen_ScreenId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Button]'))
ALTER TABLE [dbo].[Button] CHECK CONSTRAINT [FK_dbo.Button_dbo.Screen_ScreenId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditLog_dbo.Screen_Screen_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditLog]'))
ALTER TABLE [dbo].[AuditLog]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AuditLog_dbo.Screen_Screen_Id] FOREIGN KEY([Screen_Id])
REFERENCES [dbo].[Screen] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditLog_dbo.Screen_Screen_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditLog]'))
ALTER TABLE [dbo].[AuditLog] CHECK CONSTRAINT [FK_dbo.AuditLog_dbo.Screen_Screen_Id]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStage_dbo.WorkflowMaster_WorkflowMasterId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStage]'))
ALTER TABLE [dbo].[WorkflowStage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowStage_dbo.WorkflowMaster_WorkflowMasterId] FOREIGN KEY([WorkflowMasterId])
REFERENCES [dbo].[WorkflowMaster] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStage_dbo.WorkflowMaster_WorkflowMasterId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStage]'))
ALTER TABLE [dbo].[WorkflowStage] CHECK CONSTRAINT [FK_dbo.WorkflowStage_dbo.WorkflowMaster_WorkflowMasterId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStage_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStage]'))
ALTER TABLE [dbo].[WorkflowStage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowStage_dbo.WorkflowTemplate_WorkflowTemplateId] FOREIGN KEY([WorkflowTemplateId])
REFERENCES [dbo].[WorkflowTemplate] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStage_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStage]'))
ALTER TABLE [dbo].[WorkflowStage] CHECK CONSTRAINT [FK_dbo.WorkflowStage_dbo.WorkflowTemplate_WorkflowTemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.User_dbo.Department_DepartmentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_dbo.Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.User_dbo.Department_DepartmentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_dbo.User_dbo.Department_DepartmentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.User_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_dbo.Tenant_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.User_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_dbo.User_dbo.Tenant_TenantId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TenantSubscription_dbo.Subscription_SubscriptionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TenantSubscription_dbo.Subscription_SubscriptionId] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscription] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TenantSubscription_dbo.Subscription_SubscriptionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription] CHECK CONSTRAINT [FK_dbo.TenantSubscription_dbo.Subscription_SubscriptionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TenantSubscription_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TenantSubscription_dbo.Tenant_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TenantSubscription_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription] CHECK CONSTRAINT [FK_dbo.TenantSubscription_dbo.Tenant_TenantId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserRole_dbo.Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserRole_dbo.Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserRole_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserRole_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserLabel_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserLabel]'))
ALTER TABLE [dbo].[UserLabel]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserLabel_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserLabel_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserLabel]'))
ALTER TABLE [dbo].[UserLabel] CHECK CONSTRAINT [FK_dbo.UserLabel_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserInvites_dbo.User_User_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserInvites]'))
ALTER TABLE [dbo].[UserInvites]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserInvites_dbo.User_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserInvites_dbo.User_User_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserInvites]'))
ALTER TABLE [dbo].[UserInvites] CHECK CONSTRAINT [FK_dbo.UserInvites_dbo.User_User_Id]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActor_dbo.WorkflowStage_WorkflowStageId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActor]'))
ALTER TABLE [dbo].[WorkflowActor]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowActor_dbo.WorkflowStage_WorkflowStageId] FOREIGN KEY([WorkflowStageId])
REFERENCES [dbo].[WorkflowStage] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActor_dbo.WorkflowStage_WorkflowStageId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActor]'))
ALTER TABLE [dbo].[WorkflowActor] CHECK CONSTRAINT [FK_dbo.WorkflowActor_dbo.WorkflowStage_WorkflowStageId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDelegation_dbo.User_FormUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDelegation]'))
ALTER TABLE [dbo].[UserDelegation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserDelegation_dbo.User_FormUserId] FOREIGN KEY([FormUserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDelegation_dbo.User_FormUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDelegation]'))
ALTER TABLE [dbo].[UserDelegation] CHECK CONSTRAINT [FK_dbo.UserDelegation_dbo.User_FormUserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDelegation_dbo.User_ToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDelegation]'))
ALTER TABLE [dbo].[UserDelegation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserDelegation_dbo.User_ToUserId] FOREIGN KEY([ToUserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDelegation_dbo.User_ToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDelegation]'))
ALTER TABLE [dbo].[UserDelegation] CHECK CONSTRAINT [FK_dbo.UserDelegation_dbo.User_ToUserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Session_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.Tenant_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Session_dbo.Tenant_TenantId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.Tenant_TenantId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Session_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Session_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Session_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_dbo.Session_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowRule_dbo.WorkflowStage_WorkflowStageId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowRule]'))
ALTER TABLE [dbo].[WorkflowRule]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowRule_dbo.WorkflowStage_WorkflowStageId] FOREIGN KEY([WorkflowStageId])
REFERENCES [dbo].[WorkflowStage] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowRule_dbo.WorkflowStage_WorkflowStageId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowRule]'))
ALTER TABLE [dbo].[WorkflowRule] CHECK CONSTRAINT [FK_dbo.WorkflowRule_dbo.WorkflowStage_WorkflowStageId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.Department_DepartmentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Folder_dbo.Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.Department_DepartmentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder] CHECK CONSTRAINT [FK_dbo.Folder_dbo.Department_DepartmentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.Folder_ParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Folder_dbo.Folder_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Folder] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.Folder_ParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder] CHECK CONSTRAINT [FK_dbo.Folder_dbo.Folder_ParentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.User_UserCreatedById]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Folder_dbo.User_UserCreatedById] FOREIGN KEY([UserCreatedById])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Folder_dbo.User_UserCreatedById]') AND parent_object_id = OBJECT_ID(N'[dbo].[Folder]'))
ALTER TABLE [dbo].[Folder] CHECK CONSTRAINT [FK_dbo.Folder_dbo.User_UserCreatedById]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePosts_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]'))
ALTER TABLE [dbo].[DiscoursePosts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePosts_dbo.Discourses_DiscourseId] FOREIGN KEY([DiscourseId])
REFERENCES [dbo].[Discourses] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePosts_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]'))
ALTER TABLE [dbo].[DiscoursePosts] CHECK CONSTRAINT [FK_dbo.DiscoursePosts_dbo.Discourses_DiscourseId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePosts_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]'))
ALTER TABLE [dbo].[DiscoursePosts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePosts_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePosts_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePosts]'))
ALTER TABLE [dbo].[DiscoursePosts] CHECK CONSTRAINT [FK_dbo.DiscoursePosts_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditTrailEntry_dbo.User_EntityUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]'))
ALTER TABLE [dbo].[AuditTrailEntry]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AuditTrailEntry_dbo.User_EntityUserId] FOREIGN KEY([EntityUserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditTrailEntry_dbo.User_EntityUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]'))
ALTER TABLE [dbo].[AuditTrailEntry] CHECK CONSTRAINT [FK_dbo.AuditTrailEntry_dbo.User_EntityUserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditTrailEntry_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]'))
ALTER TABLE [dbo].[AuditTrailEntry]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AuditTrailEntry_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AuditTrailEntry_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AuditTrailEntry]'))
ALTER TABLE [dbo].[AuditTrailEntry] CHECK CONSTRAINT [FK_dbo.AuditTrailEntry_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Template_dbo.User_CheckedOutByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Template]'))
ALTER TABLE [dbo].[Template]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Template_dbo.User_CheckedOutByUserId] FOREIGN KEY([CheckedOutByUserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Template_dbo.User_CheckedOutByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Template]'))
ALTER TABLE [dbo].[Template] CHECK CONSTRAINT [FK_dbo.Template_dbo.User_CheckedOutByUserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Template_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Template]'))
ALTER TABLE [dbo].[Template]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Template_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Template_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Template]'))
ALTER TABLE [dbo].[Template] CHECK CONSTRAINT [FK_dbo.Template_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentSearchCriteria_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentSearchCriteria]'))
ALTER TABLE [dbo].[DocumentSearchCriteria]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentSearchCriteria_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentSearchCriteria_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentSearchCriteria]'))
ALTER TABLE [dbo].[DocumentSearchCriteria] CHECK CONSTRAINT [FK_dbo.DocumentSearchCriteria_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscourseUsers_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]'))
ALTER TABLE [dbo].[DiscourseUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscourseUsers_dbo.Discourses_DiscourseId] FOREIGN KEY([DiscourseId])
REFERENCES [dbo].[Discourses] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscourseUsers_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]'))
ALTER TABLE [dbo].[DiscourseUsers] CHECK CONSTRAINT [FK_dbo.DiscourseUsers_dbo.Discourses_DiscourseId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscourseUsers_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]'))
ALTER TABLE [dbo].[DiscourseUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscourseUsers_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscourseUsers_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscourseUsers]'))
ALTER TABLE [dbo].[DiscourseUsers] CHECK CONSTRAINT [FK_dbo.DiscourseUsers_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.Role_Role_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleUsers_dbo.Role_Role_Id] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Role] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.Role_Role_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_dbo.RoleUsers_dbo.Role_Role_Id]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.User_User_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleUsers_dbo.User_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.User_User_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_dbo.RoleUsers_dbo.User_User_Id]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Button_ButtonId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleRight_dbo.Button_ButtonId] FOREIGN KEY([ButtonId])
REFERENCES [dbo].[Button] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Button_ButtonId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight] CHECK CONSTRAINT [FK_dbo.RoleRight_dbo.Button_ButtonId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleRight_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight] CHECK CONSTRAINT [FK_dbo.RoleRight_dbo.Role_RoleId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Screen_ScreenId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleRight_dbo.Screen_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screen] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleRight_dbo.Screen_ScreenId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleRight]'))
ALTER TABLE [dbo].[RoleRight] CHECK CONSTRAINT [FK_dbo.RoleRight_dbo.Screen_ScreenId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ScanSession_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ScanSession]'))
ALTER TABLE [dbo].[ScanSession]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScanSession_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ScanSession_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ScanSession]'))
ALTER TABLE [dbo].[ScanSession] CHECK CONSTRAINT [FK_dbo.ScanSession_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.SessionMessages_dbo.Session_SessionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionMessages]'))
ALTER TABLE [dbo].[SessionMessages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SessionMessages_dbo.Session_SessionId] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.SessionMessages_dbo.Session_SessionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[SessionMessages]'))
ALTER TABLE [dbo].[SessionMessages] CHECK CONSTRAINT [FK_dbo.SessionMessages_dbo.Session_SessionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleDelegation_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDelegation]'))
ALTER TABLE [dbo].[RoleDelegation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleDelegation_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleDelegation_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDelegation]'))
ALTER TABLE [dbo].[RoleDelegation] CHECK CONSTRAINT [FK_dbo.RoleDelegation_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleDelegation_dbo.UserDelegation_UserDelegationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDelegation]'))
ALTER TABLE [dbo].[RoleDelegation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleDelegation_dbo.UserDelegation_UserDelegationId] FOREIGN KEY([UserDelegationId])
REFERENCES [dbo].[UserDelegation] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleDelegation_dbo.UserDelegation_UserDelegationId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleDelegation]'))
ALTER TABLE [dbo].[RoleDelegation] CHECK CONSTRAINT [FK_dbo.RoleDelegation_dbo.UserDelegation_UserDelegationId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateCurrentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateCurrentId] FOREIGN KEY([TemplateCurrentId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateCurrentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion] CHECK CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateCurrentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateOriginalId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateOriginalId] FOREIGN KEY([TemplateOriginalId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateOriginalId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion] CHECK CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateOriginalId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateParentId] FOREIGN KEY([TemplateParentId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateVersion_dbo.Template_TemplateParentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateVersion]'))
ALTER TABLE [dbo].[TemplateVersion] CHECK CONSTRAINT [FK_dbo.TemplateVersion_dbo.Template_TemplateParentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTagUser_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]'))
ALTER TABLE [dbo].[TemplateTagUser]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateTagUser_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTagUser_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]'))
ALTER TABLE [dbo].[TemplateTagUser] CHECK CONSTRAINT [FK_dbo.TemplateTagUser_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTagUser_dbo.User_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]'))
ALTER TABLE [dbo].[TemplateTagUser]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateTagUser_dbo.User_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTagUser_dbo.User_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTagUser]'))
ALTER TABLE [dbo].[TemplateTagUser] CHECK CONSTRAINT [FK_dbo.TemplateTagUser_dbo.User_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTag_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTag]'))
ALTER TABLE [dbo].[TemplateTag]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateTag_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateTag_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateTag]'))
ALTER TABLE [dbo].[TemplateTag] CHECK CONSTRAINT [FK_dbo.TemplateTag_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersions_dbo.DiscoursePosts_PostId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]'))
ALTER TABLE [dbo].[DiscoursePostVersions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePostVersions_dbo.DiscoursePosts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[DiscoursePosts] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersions_dbo.DiscoursePosts_PostId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]'))
ALTER TABLE [dbo].[DiscoursePostVersions] CHECK CONSTRAINT [FK_dbo.DiscoursePostVersions_dbo.DiscoursePosts_PostId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersions_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]'))
ALTER TABLE [dbo].[DiscoursePostVersions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePostVersions_dbo.Discourses_DiscourseId] FOREIGN KEY([DiscourseId])
REFERENCES [dbo].[Discourses] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersions_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersions]'))
ALTER TABLE [dbo].[DiscoursePostVersions] CHECK CONSTRAINT [FK_dbo.DiscoursePostVersions_dbo.Discourses_DiscourseId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.Folder_FolderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.Folder_FolderId] FOREIGN KEY([FolderId])
REFERENCES [dbo].[Folder] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.Folder_FolderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.Folder_FolderId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.ScanSession_ScanSessionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.ScanSession_ScanSessionId] FOREIGN KEY([ScanSessionId])
REFERENCES [dbo].[ScanSession] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.ScanSession_ScanSessionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.ScanSession_ScanSessionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_AssignedByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.User_AssignedByUserId] FOREIGN KEY([AssignedByUserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_AssignedByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.User_AssignedByUserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_AssignedToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.User_AssignedToUserId] FOREIGN KEY([AssignedToUserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_AssignedToUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.User_AssignedToUserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_CheckedOutByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.User_CheckedOutByUserId] FOREIGN KEY([CheckedOutByUserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_CheckedOutByUserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.User_CheckedOutByUserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Document_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Document_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Document]'))
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_dbo.Document_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RuleDetail_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail] CHECK CONSTRAINT [FK_dbo.RuleDetail_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RuleDetail_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail] CHECK CONSTRAINT [FK_dbo.RuleDetail_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.WorkflowRule_WorkflowRuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RuleDetail_dbo.WorkflowRule_WorkflowRuleId] FOREIGN KEY([WorkflowRuleId])
REFERENCES [dbo].[WorkflowRule] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetail_dbo.WorkflowRule_WorkflowRuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetail]'))
ALTER TABLE [dbo].[RuleDetail] CHECK CONSTRAINT [FK_dbo.RuleDetail_dbo.WorkflowRule_WorkflowRuleId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateElement_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateElement]'))
ALTER TABLE [dbo].[TemplateElement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateElement_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateElement_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateElement]'))
ALTER TABLE [dbo].[TemplateElement] CHECK CONSTRAINT [FK_dbo.TemplateElement_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowAction_dbo.WorkflowActor_WorkflowActorId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowAction]'))
ALTER TABLE [dbo].[WorkflowAction]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowAction_dbo.WorkflowActor_WorkflowActorId] FOREIGN KEY([WorkflowActorId])
REFERENCES [dbo].[WorkflowActor] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowAction_dbo.WorkflowActor_WorkflowActorId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowAction]'))
ALTER TABLE [dbo].[WorkflowAction] CHECK CONSTRAINT [FK_dbo.WorkflowAction_dbo.WorkflowActor_WorkflowActorId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserTemplates_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTemplates]'))
ALTER TABLE [dbo].[UserTemplates]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserTemplates_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserTemplates_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTemplates]'))
ALTER TABLE [dbo].[UserTemplates] CHECK CONSTRAINT [FK_dbo.UserTemplates_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserTemplates_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTemplates]'))
ALTER TABLE [dbo].[UserTemplates]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserTemplates_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserTemplates_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTemplates]'))
ALTER TABLE [dbo].[UserTemplates] CHECK CONSTRAINT [FK_dbo.UserTemplates_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserFolder_dbo.Folder_FolderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserFolder]'))
ALTER TABLE [dbo].[UserFolder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserFolder_dbo.Folder_FolderId] FOREIGN KEY([FolderId])
REFERENCES [dbo].[Folder] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserFolder_dbo.Folder_FolderId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserFolder]'))
ALTER TABLE [dbo].[UserFolder] CHECK CONSTRAINT [FK_dbo.UserFolder_dbo.Folder_FolderId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserFolder_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserFolder]'))
ALTER TABLE [dbo].[UserFolder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserFolder_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserFolder_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserFolder]'))
ALTER TABLE [dbo].[UserFolder] CHECK CONSTRAINT [FK_dbo.UserFolder_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocumentLabels_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocumentLabels]'))
ALTER TABLE [dbo].[UserDocumentLabels]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserDocumentLabels_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocumentLabels_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocumentLabels]'))
ALTER TABLE [dbo].[UserDocumentLabels] CHECK CONSTRAINT [FK_dbo.UserDocumentLabels_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocument_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocument]'))
ALTER TABLE [dbo].[UserDocument]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserDocument_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocument_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocument]'))
ALTER TABLE [dbo].[UserDocument] CHECK CONSTRAINT [FK_dbo.UserDocument_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocument_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocument]'))
ALTER TABLE [dbo].[UserDocument]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserDocument_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.UserDocument_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDocument]'))
ALTER TABLE [dbo].[UserDocument] CHECK CONSTRAINT [FK_dbo.UserDocument_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserAction_dbo.WorkflowAction_WorkflowActionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]'))
ALTER TABLE [dbo].[WorkflowUserAction]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowUserAction_dbo.WorkflowAction_WorkflowActionId] FOREIGN KEY([WorkflowActionId])
REFERENCES [dbo].[WorkflowAction] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserAction_dbo.WorkflowAction_WorkflowActionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]'))
ALTER TABLE [dbo].[WorkflowUserAction] CHECK CONSTRAINT [FK_dbo.WorkflowUserAction_dbo.WorkflowAction_WorkflowActionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserAction_dbo.WorkflowStage_WorkflowStagesId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]'))
ALTER TABLE [dbo].[WorkflowUserAction]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowUserAction_dbo.WorkflowStage_WorkflowStagesId] FOREIGN KEY([WorkflowStagesId])
REFERENCES [dbo].[WorkflowStage] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserAction_dbo.WorkflowStage_WorkflowStagesId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserAction]'))
ALTER TABLE [dbo].[WorkflowUserAction] CHECK CONSTRAINT [FK_dbo.WorkflowUserAction_dbo.WorkflowStage_WorkflowStagesId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowInstance_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance] CHECK CONSTRAINT [FK_dbo.WorkflowInstance_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowInstance_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance] CHECK CONSTRAINT [FK_dbo.WorkflowInstance_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowInstance_dbo.WorkflowTemplate_WorkflowTemplateId] FOREIGN KEY([WorkflowTemplateId])
REFERENCES [dbo].[WorkflowTemplate] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowInstance_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowInstance]'))
ALTER TABLE [dbo].[WorkflowInstance] CHECK CONSTRAINT [FK_dbo.WorkflowInstance_dbo.WorkflowTemplate_WorkflowTemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping] CHECK CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.WorkflowMaster_WorkflowMasterId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.WorkflowMaster_WorkflowMasterId] FOREIGN KEY([WorkflowMasterId])
REFERENCES [dbo].[WorkflowMaster] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.WorkflowMaster_WorkflowMasterId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping] CHECK CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.WorkflowMaster_WorkflowMasterId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.WorkflowTemplate_WorkflowTemplateId] FOREIGN KEY([WorkflowTemplateId])
REFERENCES [dbo].[WorkflowTemplate] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.EntityWorkflowMapping_dbo.WorkflowTemplate_WorkflowTemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityWorkflowMapping]'))
ALTER TABLE [dbo].[EntityWorkflowMapping] CHECK CONSTRAINT [FK_dbo.EntityWorkflowMapping_dbo.WorkflowTemplate_WorkflowTemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ElementValue_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementValue]'))
ALTER TABLE [dbo].[ElementValue]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ElementValue_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ElementValue_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementValue]'))
ALTER TABLE [dbo].[ElementValue] CHECK CONSTRAINT [FK_dbo.ElementValue_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ElementValue_dbo.TemplateElement_ElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementValue]'))
ALTER TABLE [dbo].[ElementValue]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ElementValue_dbo.TemplateElement_ElementId] FOREIGN KEY([ElementId])
REFERENCES [dbo].[TemplateElement] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.ElementValue_dbo.TemplateElement_ElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementValue]'))
ALTER TABLE [dbo].[ElementValue] CHECK CONSTRAINT [FK_dbo.ElementValue_dbo.TemplateElement_ElementId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentXmlElement_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentXmlElement]'))
ALTER TABLE [dbo].[DocumentXmlElement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentXmlElement_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentXmlElement_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentXmlElement]'))
ALTER TABLE [dbo].[DocumentXmlElement] CHECK CONSTRAINT [FK_dbo.DocumentXmlElement_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTemplate_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]'))
ALTER TABLE [dbo].[DocumentTemplate]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentTemplate_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTemplate_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]'))
ALTER TABLE [dbo].[DocumentTemplate] CHECK CONSTRAINT [FK_dbo.DocumentTemplate_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTemplate_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]'))
ALTER TABLE [dbo].[DocumentTemplate]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentTemplate_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTemplate_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTemplate]'))
ALTER TABLE [dbo].[DocumentTemplate] CHECK CONSTRAINT [FK_dbo.DocumentTemplate_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTagUser_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]'))
ALTER TABLE [dbo].[DocumentTagUser]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentTagUser_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTagUser_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]'))
ALTER TABLE [dbo].[DocumentTagUser] CHECK CONSTRAINT [FK_dbo.DocumentTagUser_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTagUser_dbo.User_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]'))
ALTER TABLE [dbo].[DocumentTagUser]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentTagUser_dbo.User_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTagUser_dbo.User_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTagUser]'))
ALTER TABLE [dbo].[DocumentTagUser] CHECK CONSTRAINT [FK_dbo.DocumentTagUser_dbo.User_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTag_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTag]'))
ALTER TABLE [dbo].[DocumentTag]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentTag_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentTag_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentTag]'))
ALTER TABLE [dbo].[DocumentTag] CHECK CONSTRAINT [FK_dbo.DocumentTag_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.DiscoursePostVersions_PostVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.DiscoursePostVersions_PostVersionId] FOREIGN KEY([PostVersionId])
REFERENCES [dbo].[DiscoursePostVersions] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.DiscoursePostVersions_PostVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] CHECK CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.DiscoursePostVersions_PostVersionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Discourses_DiscourseId] FOREIGN KEY([DiscourseId])
REFERENCES [dbo].[Discourses] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Discourses_DiscourseId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] CHECK CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Discourses_DiscourseId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] CHECK CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DiscoursePostVersionAttachments_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DiscoursePostVersionAttachments]'))
ALTER TABLE [dbo].[DiscoursePostVersionAttachments] CHECK CONSTRAINT [FK_dbo.DiscoursePostVersionAttachments_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplatePage_dbo.TemplateVersion_TemplateVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplatePage]'))
ALTER TABLE [dbo].[TemplatePage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplatePage_dbo.TemplateVersion_TemplateVersionId] FOREIGN KEY([TemplateVersionId])
REFERENCES [dbo].[TemplateVersion] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplatePage_dbo.TemplateVersion_TemplateVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplatePage]'))
ALTER TABLE [dbo].[TemplatePage] CHECK CONSTRAINT [FK_dbo.TemplatePage_dbo.TemplateVersion_TemplateVersionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateInstance_dbo.TemplateVersion_TemplateVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateInstance]'))
ALTER TABLE [dbo].[TemplateInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateInstance_dbo.TemplateVersion_TemplateVersionId] FOREIGN KEY([TemplateVersionId])
REFERENCES [dbo].[TemplateVersion] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateInstance_dbo.TemplateVersion_TemplateVersionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateInstance]'))
ALTER TABLE [dbo].[TemplateInstance] CHECK CONSTRAINT [FK_dbo.TemplateInstance_dbo.TemplateVersion_TemplateVersionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateElementDetail_dbo.TemplateElement_ElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateElementDetail]'))
ALTER TABLE [dbo].[TemplateElementDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TemplateElementDetail_dbo.TemplateElement_ElementId] FOREIGN KEY([ElementId])
REFERENCES [dbo].[TemplateElement] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.TemplateElementDetail_dbo.TemplateElement_ElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TemplateElementDetail]'))
ALTER TABLE [dbo].[TemplateElementDetail] CHECK CONSTRAINT [FK_dbo.TemplateElementDetail_dbo.TemplateElement_ElementId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues] CHECK CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.TemplateElement_IndexElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.TemplateElement_IndexElementId] FOREIGN KEY([IndexElementId])
REFERENCES [dbo].[TemplateElement] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.TemplateElement_IndexElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues] CHECK CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.TemplateElement_IndexElementId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.User_IndexerId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.User_IndexerId] FOREIGN KEY([IndexerId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentCorrectiveIndexValues_dbo.User_IndexerId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentCorrectiveIndexValues]'))
ALTER TABLE [dbo].[DocumentCorrectiveIndexValues] CHECK CONSTRAINT [FK_dbo.DocumentCorrectiveIndexValues_dbo.User_IndexerId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentIndex_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentIndex]'))
ALTER TABLE [dbo].[DocumentIndex]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentIndex_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentIndex_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentIndex]'))
ALTER TABLE [dbo].[DocumentIndex] CHECK CONSTRAINT [FK_dbo.DocumentIndex_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentFragments_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentFragments]'))
ALTER TABLE [dbo].[DocumentFragments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentFragments_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentFragments_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentFragments]'))
ALTER TABLE [dbo].[DocumentFragments] CHECK CONSTRAINT [FK_dbo.DocumentFragments_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentElement_dbo.Document_DocumentId] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.Document_DocumentId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement] CHECK CONSTRAINT [FK_dbo.DocumentElement_dbo.Document_DocumentId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.TemplateElement_TemplateElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentElement_dbo.TemplateElement_TemplateElementId] FOREIGN KEY([TemplateElementId])
REFERENCES [dbo].[TemplateElement] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.TemplateElement_TemplateElementId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement] CHECK CONSTRAINT [FK_dbo.DocumentElement_dbo.TemplateElement_TemplateElementId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.TemplateElementDetail_TemplateElementDetailId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DocumentElement_dbo.TemplateElementDetail_TemplateElementDetailId] FOREIGN KEY([TemplateElementDetailId])
REFERENCES [dbo].[TemplateElementDetail] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DocumentElement_dbo.TemplateElementDetail_TemplateElementDetailId]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentElement]'))
ALTER TABLE [dbo].[DocumentElement] CHECK CONSTRAINT [FK_dbo.DocumentElement_dbo.TemplateElementDetail_TemplateElementDetailId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStagesInstance_dbo.EntityWorkflowMapping_EntityWorkflowMappingId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStagesInstance]'))
ALTER TABLE [dbo].[WorkflowStagesInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowStagesInstance_dbo.EntityWorkflowMapping_EntityWorkflowMappingId] FOREIGN KEY([EntityWorkflowMappingId])
REFERENCES [dbo].[EntityWorkflowMapping] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowStagesInstance_dbo.EntityWorkflowMapping_EntityWorkflowMappingId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowStagesInstance]'))
ALTER TABLE [dbo].[WorkflowStagesInstance] CHECK CONSTRAINT [FK_dbo.WorkflowStagesInstance_dbo.EntityWorkflowMapping_EntityWorkflowMappingId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserActionInstance_dbo.WorkflowAction_WorkflowActionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]'))
ALTER TABLE [dbo].[WorkflowUserActionInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowUserActionInstance_dbo.WorkflowAction_WorkflowActionId] FOREIGN KEY([WorkflowActionId])
REFERENCES [dbo].[WorkflowAction] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserActionInstance_dbo.WorkflowAction_WorkflowActionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]'))
ALTER TABLE [dbo].[WorkflowUserActionInstance] CHECK CONSTRAINT [FK_dbo.WorkflowUserActionInstance_dbo.WorkflowAction_WorkflowActionId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserActionInstance_dbo.WorkflowStagesInstance_WorkflowStageInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]'))
ALTER TABLE [dbo].[WorkflowUserActionInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowUserActionInstance_dbo.WorkflowStagesInstance_WorkflowStageInstanceId] FOREIGN KEY([WorkflowStageInstanceId])
REFERENCES [dbo].[WorkflowStagesInstance] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowUserActionInstance_dbo.WorkflowStagesInstance_WorkflowStageInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowUserActionInstance]'))
ALTER TABLE [dbo].[WorkflowUserActionInstance] CHECK CONSTRAINT [FK_dbo.WorkflowUserActionInstance_dbo.WorkflowStagesInstance_WorkflowStageInstanceId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowRuleInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowRuleInstance]'))
ALTER TABLE [dbo].[WorkflowRuleInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowRuleInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId] FOREIGN KEY([WorkFlowStagesInstanceId])
REFERENCES [dbo].[WorkflowStagesInstance] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowRuleInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowRuleInstance]'))
ALTER TABLE [dbo].[WorkflowRuleInstance] CHECK CONSTRAINT [FK_dbo.WorkflowRuleInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActorsInstance_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]'))
ALTER TABLE [dbo].[WorkflowActorsInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowActorsInstance_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActorsInstance_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]'))
ALTER TABLE [dbo].[WorkflowActorsInstance] CHECK CONSTRAINT [FK_dbo.WorkflowActorsInstance_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActorsInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]'))
ALTER TABLE [dbo].[WorkflowActorsInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkflowActorsInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId] FOREIGN KEY([WorkFlowStagesInstanceId])
REFERENCES [dbo].[WorkflowStagesInstance] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.WorkflowActorsInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkflowActorsInstance]'))
ALTER TABLE [dbo].[WorkflowActorsInstance] CHECK CONSTRAINT [FK_dbo.WorkflowActorsInstance_dbo.WorkflowStagesInstance_WorkFlowStagesInstanceId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.Template_TemplateId] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.Template_TemplateId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance] CHECK CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.Template_TemplateId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance] CHECK CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.User_UserId]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.WorkflowRuleInstance_WorkFlowRuleInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.WorkflowRuleInstance_WorkFlowRuleInstanceId] FOREIGN KEY([WorkFlowRuleInstanceId])
REFERENCES [dbo].[WorkflowRuleInstance] ([Id])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RuleDetailInstance_dbo.WorkflowRuleInstance_WorkFlowRuleInstanceId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RuleDetailInstance]'))
ALTER TABLE [dbo].[RuleDetailInstance] CHECK CONSTRAINT [FK_dbo.RuleDetailInstance_dbo.WorkflowRuleInstance_WorkFlowRuleInstanceId]
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (1, N'Custom', N'', 1)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (2, N'Administrator', N'', 2)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (3, N'Scanner', N'', 3)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (4, N'Uploader', N'', 4)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (5, N'Indexer', N'', 5)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (6, N'TemplateCreator', N'', 6)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (7, N'FormCreator', N'', 7)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (8, N'WorkflowActor', N'', 8)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (9, N'WorkflowCreator', N'', 9)
INSERT [dbo].[Role] ([Id], [Name], [Description], [RoleType]) VALUES (10, N'Reporting', N'', 10)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name]) VALUES (4, N'Accounting')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (2, N'Board of Directors')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (1, N'CEO')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (3, N'Finance')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (5, N'Human Resource')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (6, N'Information Technology')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (10, N'Manufacturing')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (8, N'Marketing')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (9, N'Operations')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (7, N'Sales')
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Tenant] ON 

INSERT [dbo].[Tenant] ([Id], [MasterTenantId], [TenantType], [MasterTenantToken], [AuthenticationType], [Domain], [CompanyName], [ContactOwnerNameGiven], [ContactOwnerNameFamily], [ContactOwnerAddress], [ContactOwnerCity], [ContactOwnerState], [ContactOwnerZipCode], [ContactOwnerCountry], [ContactOwnerPhone], [ContactOwnerFax], [ContactOwnerEmail], [ContactAdministratorNameGiven], [ContactAdministratorNameFamily], [ContactAdministratorAddress], [ContactAdministratorCity], [ContactAdministratorState], [ContactAdministratorZipCode], [ContactAdministratorCountry], [ContactAdministratorPhone], [ContactAdministratorFax], [ContactAdministratorEmail], [ContactBillingNameGiven], [ContactBillingNameFamily], [ContactBillingAddress], [ContactBillingCity], [ContactBillingState], [ContactBillingZipCode], [ContactBillingCountry], [ContactBillingPhone], [ContactBillingFax], [ContactBillingEmail], [ContactTechnicalNameGiven], [ContactTechnicalNameFamily], [ContactTechnicalAddress], [ContactTechnicalCity], [ContactTechnicalState], [ContactTechnicalZipCode], [ContactTechnicalCountry], [ContactTechnicalPhone], [ContactTechnicalFax], [ContactTechnicalEmail], [RsaKeyPublic], [RsaKeyPrivate], [UrlApi], [UrlResourceGroup], [UrlStorage], [UrlStorageBlob], [UrlStorageTable], [UrlStorageQueue], [UrlStorageFile], [StorageAccessKeyPrimary], [StorageAccessKeySecondary], [StorageConnectionStringPrimary], [StorageConnectionStringSecondary], [DatabaseConnectionString]) VALUES (1, 1, 2, N'{1de468a3-21a5-458b-b47b-f840726c1799}', 1, N'kloud-soft.com', N'Kloud-Soft Private Limited', N'Raheel', N'Khan', N'E-11/4', N'Islamabad', N'ICT', N'44000', N'Pakistan', N'+92 (321) 517-3303', N'+92 (321) 517-3303', N'raheel.khan@houseofsynergy.com', N'Raheel', N'Khan', N'E-11/4', N'Islamabad', N'ICT', N'44000', N'Pakistan', N'+92 (321) 517-3303', N'+92 (321) 517-3303', N'raheel.khan@houseofsynergy.com', N'Raheel', N'Khan', N'E-11/4', N'Islamabad', N'ICT', N'44000', N'Pakistan', N'+92 (321) 517-3303', N'+92 (321) 517-3303', N'raheel.khan@houseofsynergy.com', N'Raheel', N'Khan', N'E-11/4', N'Islamabad', N'ICT', N'44000', N'Pakistan', N'+92 (321) 517-3303', N'+92 (321) 517-3303', N'raheel.khan@houseofsynergy.com', N'<?xml version="1.0" encoding="utf-16"?>
<RSAParameters xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Exponent>AQAB</Exponent>
  <Modulus>qhOSzXno0JqNOyTAZjpsFDD2QCot4bpebFPJnKGIyuBB9MU1RGveB91ltG3+fpNicCZU2S7GaX2vehH9pOl9yIbl/T59lOgQSWPrcH2SCgODYA4CNWqAauyWOusz5YRuKPQsckqPVeQDYpe2a5bFDqNUYmnhO80lhtwPqDP2eEc=</Modulus>
</RSAParameters>', N'<?xml version="1.0" encoding="utf-16"?>
<RSAParameters xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Exponent>AQAB</Exponent>
  <Modulus>qhOSzXno0JqNOyTAZjpsFDD2QCot4bpebFPJnKGIyuBB9MU1RGveB91ltG3+fpNicCZU2S7GaX2vehH9pOl9yIbl/T59lOgQSWPrcH2SCgODYA4CNWqAauyWOusz5YRuKPQsckqPVeQDYpe2a5bFDqNUYmnhO80lhtwPqDP2eEc=</Modulus>
  <P>wgVZuX+Db8vkr2jbjjmeHjPMjwcGzgP8UJHweVI0Ow0kX+kK6IrToOuI5f+HZxXQofYU5knBGAY/eo3JYlGZsQ==</P>
  <Q>4GgX6If9mqm+Fm35EUZzPTL1t2NhRIUvCWrfayDFWs+Ees0eouF8h5E1H0/ocSRgZeKngKSgiJFqW1/opfM3dw==</Q>
  <DP>AlFsDAJRALHv7oSTMvTPRc8an47W7vkvN4s200w/IYF7sMWpArQ96QtHXHAcHwgssPKutz2kV/QjKASvpkQRUQ==</DP>
  <DQ>DAoJnlQNawrIQ/PKdP7Ol+3v+NLAGSj8CQlOyzSK4gBYHy56AksOn0dPDkr/MXK+KTkru18Zrbn/dr+Cf8S4qQ==</DQ>
  <InverseQ>ENci532tOXEMY0op6Y0Kzoc5NpaZNFjoHZNi9q6hTZvFa2AsRh4qrk8IbpEcXzmZWuvg9NrcsMpcyiN6WErJVQ==</InverseQ>
  <D>U87iY9ODmKvYFYFmN9npIUgBSJwgUN/cIKhnLIJjHInGpp25QnafCiQctn0PUANDmj0hSYZlql0PZ+lOooGWg7hY+H8IpoBocVtrj2UuWOhKdauBj+Qdh9uvMxC4Z2p/k264nV9mnDAMyLMGAV6jIlG4bBV+42jaHUSk3qAoYlE=</D>
</RSAParameters>', N'http://affinity-ecm-tenantportal.azurewebsites.net/Api/', N'kloudsoft-rg-affinity', N'kloudsoftstorage', N'https://kloudsoftstorage.blob.core.windows.net/', N'https://kloudsoftstorage.table.core.windows.net/', N'https://kloudsoftstorage.queue.core.windows.net/', N'https://kloudsoftstorage.file.core.windows.net/', N'r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==', N'Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==', N'DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==;', N'DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==;', N'Data Source=Lenovo;Initial Catalog=AffinityDmsTenant_0000000000000000001;Integrated Security=True;Persist Security Info=True;MultipleActiveResultSets=True')
SET IDENTITY_INSERT [dbo].[Tenant] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (1, 1, N'raheel.khan@houseofsynergy.com', N'admin', N'1000:Y9sV3XAAaylEi1LHUulfvcsKs1jQPXfR:Dgc+yL5PUhYmyRucx9kxK2qeLN/9waAo', N'1000:Y9sV3XAAaylEi1LHUulfvcsKs1jQPXfR:Dgc+yL5PUhYmyRucx9kxK2qeLN/9waAo', N'Administrator', N'Global', N'', N'', N'', N'', N'', N'', N'', CAST(N'2017-04-22T11:25:13.307' AS DateTime), NULL, NULL, 1, 1, 6, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (2, 1, N'raheel.khan@houseofsynergy.com', N'raheel.khan', N'1000:YEfCKGkPkNqLeU1MajTSgErI2cnKHO3r:DDjy7qem/MCfXgbNWqSuatQztVanmOZf', N'1000:YEfCKGkPkNqLeU1MajTSgErI2cnKHO3r:DDjy7qem/MCfXgbNWqSuatQztVanmOZf', N'Raheel', N'Khan', N'E-11/4', N'', N'Islamabad', N'44000', N'Pakistan', N'', N'+92 (321) 517-3303', CAST(N'2017-04-22T11:25:13.390' AS DateTime), NULL, NULL, 1, 1, 10, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (3, 1, N'sjunaid@houseofsynergy.com', N'junaid.sayed', N'1000:QCvl/Syknevmo9mXGIE+NKIKMvVKhzEb:dS1jVrOLh+z1mNeYGhoZYTCcxYIHCZ7E', N'1000:QCvl/Syknevmo9mXGIE+NKIKMvVKhzEb:dS1jVrOLh+z1mNeYGhoZYTCcxYIHCZ7E', N'Junaid', N'Sayed', N'G-13', N'', N'Islamabad', N'44000', N'Pakistan', N'', N'+92 (321) 245-2112', CAST(N'2017-04-22T11:25:13.447' AS DateTime), NULL, NULL, 1, 1, 7, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (4, 1, N'rizwan.khan@houseofsynergy.com', N'rizwan.khan', N'1000:YmBKTJFwXT570RoP8XfN5+5kDDngNIwf:8zRk8hH/iYiRviKBxMf0IEI7SitVg2eU', N'1000:YmBKTJFwXT570RoP8XfN5+5kDDngNIwf:8zRk8hH/iYiRviKBxMf0IEI7SitVg2eU', N'Rizwan', N'Khan', N'?', N'', N'Karachi', N'75500', N'Pakistan', N'', N'+92 (321) 280-7325', CAST(N'2017-04-22T11:25:13.477' AS DateTime), NULL, NULL, 1, 1, 5, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (5, 1, N'danish.muhammad@houseofsynergy.com', N'danish.muhammad', N'1000:B7PtvlXdz0mTSI0WsBn66Ug9cSght2aD:w9Y3aAe+koG2g5NWDL+5wMZbFN1VIwGp', N'1000:B7PtvlXdz0mTSI0WsBn66Ug9cSght2aD:w9Y3aAe+koG2g5NWDL+5wMZbFN1VIwGp', N'Danish', N'Muhammad', N'?', N'', N'Karachi', N'75500', N'Pakistan', N'', N'+92 (345) 243-5474', CAST(N'2017-04-22T11:25:13.507' AS DateTime), NULL, NULL, 1, 1, 7, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (6, 1, N'uzma.hashmi@houseofsynergy.com', N'uzma.hashmi', N'1000:wocCAVVekt36Qu0EMT6gzPq6S7664Hai:08bcfly9LWWHZUbVrRAA7ohunTZAhvJE', N'1000:wocCAVVekt36Qu0EMT6gzPq6S7664Hai:08bcfly9LWWHZUbVrRAA7ohunTZAhvJE', N'Uzma', N'Hashmi', N'?', N'', N'Karachi', N'75500', N'Pakistan', N'', N'+92 (303) 289-8969', CAST(N'2017-04-22T11:25:13.537' AS DateTime), NULL, NULL, 1, 1, 8, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (7, 1, N'kausar.khan@houseofsynergy.com', N'kausar.khan', N'1000:kCBKEh89j9z4mT9Gh01Rn1S7aMIMByBO:ncIrc0RrCt+hd3Zepf/G31AcoeLP/5i1', N'1000:kCBKEh89j9z4mT9Gh01Rn1S7aMIMByBO:ncIrc0RrCt+hd3Zepf/G31AcoeLP/5i1', N'Kausar', N'Khan', N'?', N'', N'Islamabad', N'75500', N'Pakistan', N'', N'+92 (334) 500-4781', CAST(N'2017-04-22T11:25:13.560' AS DateTime), NULL, NULL, 1, 1, 9, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (8, 1, N'lawrence@kloud-soft.com', N'lawrence', N'1000:gUw5QwVoohnhvk79BSnXgbsHXzI4E9S+:/uWyTEHqDpJmVWFlp+VCduNlaeFhg6r8', N'1000:gUw5QwVoohnhvk79BSnXgbsHXzI4E9S+:/uWyTEHqDpJmVWFlp+VCduNlaeFhg6r8', N'Lawrence', N'', N'', N'', N'', N'', N'Singapore', N'', N'', CAST(N'2017-04-22T11:25:13.580' AS DateTime), NULL, NULL, 1, 1, 8, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (9, 1, N'richard@kloud-soft.com', N'richard', N'1000:PLKrQYCtTvbqvxEwlRe9UJoGInRb4DIU:WRaxscCx+HUaN5PLjrkIFtL9Z0mBKE7j', N'1000:PLKrQYCtTvbqvxEwlRe9UJoGInRb4DIU:WRaxscCx+HUaN5PLjrkIFtL9Z0mBKE7j', N'Richard', N'Chong', N'', N'', N'', N'', N'Singapore', N'', N'', CAST(N'2017-04-22T11:25:13.607' AS DateTime), NULL, NULL, 1, 1, 8, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (10, 1, N'defang@kloud-soft.com', N'jeff', N'1000:uqdwWGMKLnFOvAAICRVnqJgD900KM5+F:Gs6ogc1l6h8Sqsq9w8+Dkjrd1s57Mzll', N'1000:uqdwWGMKLnFOvAAICRVnqJgD900KM5+F:Gs6ogc1l6h8Sqsq9w8+Dkjrd1s57Mzll', N'Jeff', N'', N'', N'', N'', N'', N'Singapore', N'', N'', CAST(N'2017-04-22T11:25:13.627' AS DateTime), NULL, NULL, 1, 1, 10, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([Id], [TenantId], [Email], [UserName], [PasswordHash], [PasswordSalt], [NameGiven], [NameFamily], [Address1], [Address2], [City], [ZipOrPostCode], [Country], [PhoneWork], [PhoneMobile], [DateTimeCreated], [InviteUrl], [InviteGuid], [IsActive], [AuthenticationType], [DepartmentId], [ActiveDirectory_NameDisplay], [ActiveDirectory_ObjectId], [ActiveDirectory_UsageLocation], [ActiveDirectory_JobTitle], [ActiveDirectory_Department], [ActiveDirectory_ManagerId], [ActiveDirectory_AuthenticationPhone], [ActiveDirectory_AuthenticationPhoneAlternate], [ActiveDirectory_AuthenticationEmail], [ActiveDirectory_RoleDisplayName]) VALUES (11, 1, N'ben@kloud-soft.com', N'ben', N'1000:rA8D0qFwp6ES0afTeLdl3MUAS45sQjHE:JuOARMzbMFJ4lf+yvqWHTioQg/F5zNk7', N'1000:rA8D0qFwp6ES0afTeLdl3MUAS45sQjHE:JuOARMzbMFJ4lf+yvqWHTioQg/F5zNk7', N'Ben', N'Tan', N'', N'', N'', N'', N'Singapore', N'', N'', CAST(N'2017-04-22T11:25:13.650' AS DateTime), NULL, NULL, 1, 1, 7, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (1, 1, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (2, 2, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (3, 3, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (4, 4, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (5, 5, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (6, 6, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (7, 7, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (8, 8, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (9, 9, 1)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (10, 10, 1)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 1)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 2)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 3)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 4)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 5)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 6)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 7)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 8)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 9)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 10)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (1, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (2, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (3, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (4, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (5, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (6, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (7, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (8, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (9, 11)
INSERT [dbo].[RoleUsers] ([Role_Id], [User_Id]) VALUES (10, 11)
SET IDENTITY_INSERT [dbo].[Folder] ON 

INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (1, N'Kloud-Soft Private Limited', 1, CAST(N'2017-04-22T11:25:15.617' AS DateTime), CAST(N'2017-04-22T11:25:15.617' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (2, N'Enterprise', 2, CAST(N'2017-04-22T11:25:16.663' AS DateTime), CAST(N'2017-04-22T11:25:16.663' AS DateTime), 1, 1, NULL)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (3, N'Accounting', 3, CAST(N'2017-04-22T11:25:16.820' AS DateTime), CAST(N'2017-04-22T11:25:16.820' AS DateTime), 1, 2, 4)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (4, N'Board of Directors', 3, CAST(N'2017-04-22T11:25:16.883' AS DateTime), CAST(N'2017-04-22T11:25:16.883' AS DateTime), 1, 2, 2)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (5, N'CEO', 3, CAST(N'2017-04-22T11:25:16.960' AS DateTime), CAST(N'2017-04-22T11:25:16.960' AS DateTime), 1, 2, 1)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (6, N'Finance', 3, CAST(N'2017-04-22T11:25:17.037' AS DateTime), CAST(N'2017-04-22T11:25:17.037' AS DateTime), 1, 2, 3)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (7, N'Human Resource', 3, CAST(N'2017-04-22T11:25:17.083' AS DateTime), CAST(N'2017-04-22T11:25:17.083' AS DateTime), 1, 2, 5)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (8, N'Information Technology', 3, CAST(N'2017-04-22T11:25:17.147' AS DateTime), CAST(N'2017-04-22T11:25:17.147' AS DateTime), 1, 2, 6)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (9, N'Manufacturing', 3, CAST(N'2017-04-22T11:25:17.197' AS DateTime), CAST(N'2017-04-22T11:25:17.197' AS DateTime), 1, 2, 10)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (10, N'Marketing', 3, CAST(N'2017-04-22T11:25:17.260' AS DateTime), CAST(N'2017-04-22T11:25:17.260' AS DateTime), 1, 2, 8)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (11, N'Operations', 3, CAST(N'2017-04-22T11:25:17.353' AS DateTime), CAST(N'2017-04-22T11:25:17.353' AS DateTime), 1, 2, 9)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (12, N'Sales', 3, CAST(N'2017-04-22T11:25:17.490' AS DateTime), CAST(N'2017-04-22T11:25:17.490' AS DateTime), 1, 2, 7)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (13, N'Shared', 4, CAST(N'2017-04-22T11:25:17.573' AS DateTime), CAST(N'2017-04-22T11:25:17.573' AS DateTime), 1, 1, NULL)
INSERT [dbo].[Folder] ([Id], [Name], [FolderType], [DateTimeCreated], [DateTimeModified], [UserCreatedById], [ParentId], [DepartmentId]) VALUES (14, N'Private', 6, CAST(N'2017-04-22T11:25:17.640' AS DateTime), CAST(N'2017-04-22T11:25:17.640' AS DateTime), 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Folder] OFF
SET IDENTITY_INSERT [dbo].[UserFolder] ON 

INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (1, 1, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (2, 2, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (3, 3, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (4, 4, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (5, 5, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (6, 6, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (7, 7, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (8, 8, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (9, 9, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (10, 10, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (11, 11, 1, 1)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (12, 1, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (13, 2, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (14, 3, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (15, 4, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (16, 5, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (17, 6, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (18, 7, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (19, 8, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (20, 9, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (21, 10, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (22, 11, 1, 2)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (23, 1, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (24, 2, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (25, 3, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (26, 4, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (27, 5, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (28, 6, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (29, 7, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (30, 8, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (31, 9, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (32, 10, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (33, 11, 1, 3)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (34, 1, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (35, 2, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (36, 3, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (37, 4, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (38, 5, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (39, 6, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (40, 7, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (41, 8, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (42, 9, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (43, 10, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (44, 11, 1, 4)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (45, 1, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (46, 2, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (47, 3, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (48, 4, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (49, 5, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (50, 6, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (51, 7, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (52, 8, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (53, 9, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (54, 10, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (55, 11, 1, 5)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (56, 1, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (57, 2, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (58, 3, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (59, 4, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (60, 5, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (61, 6, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (62, 7, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (63, 8, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (64, 9, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (65, 10, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (66, 11, 1, 6)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (67, 1, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (68, 2, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (69, 3, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (70, 4, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (71, 5, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (72, 6, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (73, 7, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (74, 8, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (75, 9, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (76, 10, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (77, 11, 1, 7)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (78, 1, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (79, 2, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (80, 3, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (81, 4, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (82, 5, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (83, 6, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (84, 7, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (85, 8, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (86, 9, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (87, 10, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (88, 11, 1, 8)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (89, 1, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (90, 2, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (91, 3, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (92, 4, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (93, 5, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (94, 6, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (95, 7, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (96, 8, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (97, 9, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (98, 10, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (99, 11, 1, 9)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (100, 1, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (101, 2, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (102, 3, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (103, 4, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (104, 5, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (105, 6, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (106, 7, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (107, 8, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (108, 9, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (109, 10, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (110, 11, 1, 10)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (111, 1, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (112, 2, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (113, 3, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (114, 4, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (115, 5, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (116, 6, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (117, 7, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (118, 8, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (119, 9, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (120, 10, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (121, 11, 1, 11)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (122, 1, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (123, 2, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (124, 3, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (125, 4, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (126, 5, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (127, 6, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (128, 7, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (129, 8, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (130, 9, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (131, 10, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (132, 11, 1, 12)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (133, 1, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (134, 2, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (135, 3, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (136, 4, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (137, 5, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (138, 6, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (139, 7, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (140, 8, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (141, 9, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (142, 10, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (143, 11, 1, 13)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (144, 1, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (145, 2, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (146, 3, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (147, 4, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (148, 5, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (149, 6, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (150, 7, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (151, 8, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (152, 9, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (153, 10, 1, 14)
INSERT [dbo].[UserFolder] ([Id], [UserId], [IsActive], [FolderId]) VALUES (154, 11, 1, 14)
SET IDENTITY_INSERT [dbo].[UserFolder] OFF
SET IDENTITY_INSERT [dbo].[Subscription] ON 

INSERT [dbo].[Subscription] ([Id], [MasterSubscriptionId], [SubscriptionType], [IsDemo], [IsActive], [Description], [NumberOfFormsAllowed], [NumberOfPagesAllowed], [NumberOfUsersAllowed], [NumberOfTemplatesAllowed], [NumberOfFormsUsed], [NumberOfPagesUsed], [NumberOfUsersUsed], [NumberOfTemplatesUsed], [AllowScanning], [AllowBranding], [AllowTemplateWorkflows]) VALUES (1, 1, 0, 1, 1, N'Text subscription 1.', 758, 21, 583, 408, 0, 0, 0, 0, 1, 1, 1)
INSERT [dbo].[Subscription] ([Id], [MasterSubscriptionId], [SubscriptionType], [IsDemo], [IsActive], [Description], [NumberOfFormsAllowed], [NumberOfPagesAllowed], [NumberOfUsersAllowed], [NumberOfTemplatesAllowed], [NumberOfFormsUsed], [NumberOfPagesUsed], [NumberOfUsersUsed], [NumberOfTemplatesUsed], [AllowScanning], [AllowBranding], [AllowTemplateWorkflows]) VALUES (2, 2, 0, 0, 1, N'Text subscription 2.', 703, 97, 596, 837, 0, 0, 0, 0, 1, 1, 1)
INSERT [dbo].[Subscription] ([Id], [MasterSubscriptionId], [SubscriptionType], [IsDemo], [IsActive], [Description], [NumberOfFormsAllowed], [NumberOfPagesAllowed], [NumberOfUsersAllowed], [NumberOfTemplatesAllowed], [NumberOfFormsUsed], [NumberOfPagesUsed], [NumberOfUsersUsed], [NumberOfTemplatesUsed], [AllowScanning], [AllowBranding], [AllowTemplateWorkflows]) VALUES (3, 3, 0, 0, 1, N'Text subscription 3.', 648, 451, 639, 956, 0, 0, 0, 0, 1, 1, 1)
INSERT [dbo].[Subscription] ([Id], [MasterSubscriptionId], [SubscriptionType], [IsDemo], [IsActive], [Description], [NumberOfFormsAllowed], [NumberOfPagesAllowed], [NumberOfUsersAllowed], [NumberOfTemplatesAllowed], [NumberOfFormsUsed], [NumberOfPagesUsed], [NumberOfUsersUsed], [NumberOfTemplatesUsed], [AllowScanning], [AllowBranding], [AllowTemplateWorkflows]) VALUES (4, 4, 0, 0, 1, N'Text subscription 4.', 246, 421, 398, 597, 0, 0, 0, 0, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Subscription] OFF
SET IDENTITY_INSERT [dbo].[TenantSubscription] ON 

INSERT [dbo].[TenantSubscription] ([Id], [MasterTenantSubscriptionId], [TenantSubscriptionType], [IsDemo], [IsActive], [DateTimeStart], [DateTimeExpires], [NumberOfFormsAllowed], [NumberOfPagesAllowed], [NumberOfUsersAllowed], [NumberOfTemplatesAllowed], [NumberOfFormsUsed], [NumberOfPagesUsed], [NumberOfUsersUsed], [NumberOfTemplatesUsed], [AllowScanning], [AllowBranding], [AllowTemplateWorkflows], [RequireDelegationAcceptance], [Time], [TenantId], [SubscriptionId]) VALUES (1, 1, 0, 1, 1, CAST(N'2017-04-22T11:24:27.240' AS DateTime), CAST(N'2018-04-22T11:24:27.240' AS DateTime), 758, 21, 583, 408, 0, 0, 0, 0, 1, 1, 1, 0, CAST(N'2017-04-22T11:24:27.240' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[TenantSubscription] OFF
SET IDENTITY_INSERT [dbo].[Culture] ON 

INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (1, N'en', 9, N'English', N'English', N'English', N'en', N'eng', N'ENU')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (2, N'en-US', 1033, N'English (United States)', N'English (United States)', N'English (United States)', N'en', N'eng', N'ENU')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (3, N'en-GB', 2057, N'English (United Kingdom)', N'English (United Kingdom)', N'English (United Kingdom)', N'en', N'eng', N'ENG')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (4, N'zh-Hans', 4, N'中文(简体)', N'Chinese (Simplified)', N'Chinese (Simplified)', N'zh', N'zho', N'CHS')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (5, N'zh-Hant', 31748, N'中文(繁體)', N'Chinese (Traditional)', N'Chinese (Traditional)', N'zh', N'zho', N'CHT')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (6, N'zh-SG', 4100, N'中文(新加坡)', N'Chinese (Simplified, Singapore)', N'Chinese (Simplified, Singapore)', N'zh', N'zho', N'ZHI')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (7, N'zh-TW', 1028, N'中文(台灣)', N'Chinese (Traditional, Taiwan)', N'Chinese (Traditional, Taiwan)', N'zh', N'zho', N'CHT')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (8, N'ur', 32, N'اُردو', N'Urdu', N'Urdu', N'ur', N'urd', N'URD')
INSERT [dbo].[Culture] ([Id], [Name], [LocaleId], [NameNative], [NameDisplay], [NameEnglish], [NameIsoTwoLetter], [NameIsoThreeLetter], [NameWindowsThreeLetter]) VALUES (9, N'ur-PK', 1056, N'اُردو (پاکستان)', N'Urdu (Islamic Republic of Pakistan)', N'Urdu (Pakistan)', N'ur', N'urd', N'URD')
SET IDENTITY_INSERT [dbo].[Culture] OFF
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201704221125049_InitialCreate', N'HouseOfSynergy.AffinityDms.DataLayer.Contexts.ContextTenant', 0x1F8B0800000000000400EDBDDB72DC489220FABE66FB0F343DEDAEF58A2555F7EC5499B46BBC882ACE882287A4AAABFB250DCC8492A842021C0049897DEC7CD979389F747EE120708D8BC7FD0264769ACC4412E1E11EE1E1E1E1E1E1E1F1FFFD3FFFEFBBFFF37D931E3DC74599E4D9FB576F5EFFF0EA28CE96F92AC9D6EF5F6DABAFFFF35F5FFD9FFFFD5FFFCBBB0FABCDF7A35F7BB81F115C5D332BDFBF7AACAAA79F8F8FCBE563BC89CAD79B6459E465FEB57ABDCC37C7D12A3F7EFBC30F3F1DBF79731CD7285ED5B88E8EDEDD6EB32AD9C4CD1FF59F6779B68C9FAA6D945EE5AB382DBBEF75C95D83F5E873B489CBA76819BF7FF54BBE2DE3EBAF772F595CAC5F5E9F7CFD9A6449F572BE295F9F4755F4297A898BD735C22AFE5E95AF8E4ED224AA1B7917A75F5F1D45599657515577E1E72F657C571579B6BE7BAA3F44E9FDCB535CC37D8DD232EEBAF6F308AEDACB1FDEA25E1E8F157B54CB6D59E51B4D846F7EECD8764C573762FEAB81AD35633FD40350BDA05E37CC7DFFEA64BB4AAA4FF9FAD5114DECE7B3B4408042DE37F892B8AC59BFD9E4D9EB1EDD9F8E142AFD6990AC5A00D1BF3F1D9D6DD36A5BC4EFB3785B1551FAA7A39BED439A2CFF3D7EB9CFFF88B3F7D9364DF10ED55DAACB880FF5A79B227F8A8BEAE536FEDA75F372F5EAE898AC774C571CAA61755A065C66D5BFFCF9D5D1E79A78F490C683BC60CCBAABF222FE18D79D8DAA78751355555C640847DC709CA14ED1AA59765FCF8C9E602DD271FB3743538CE7645925CF712DE505D1FA1FDF9A60AA27A60B4C77CB228E335B2C17499CAED0AF3D9A7A1ED7DAEAD5D155F4FD539CADABC7F7AFEA5F5F1D5D24DFE355FFA543FD254B6AE55657AA8AAD94D24D113F27B5F4FE1AA55BFFD43EC7DFC210FA352A92A856B89AC3F0397A4ED68D8C83C3FAEAE8364E9BE2F231796A35EEA005163DCC45916F6EF314D3375DD1E22EDF164D9B72B8FC3E2AD67145B6EADDF1A8C684CAAD276FA7DAEEE32CCAAAF2758BEDA0D9F4345B98291B15756B986EA98AF2E9B6AA6A010665B92D0324992818E4B49763B2B49772D506B5FD01DBD361EC21C6F61005CCBC224BA159256A0FC2719BAC1F2B98474331C026BA8CE11403A0CBACB61ADC32456ED16D827969A4835A4170A5835A6C071DA4A783000B04A468A5C9FEF2839222335E6F2573B053383814A3ABC642463FB010BA3A42600F28EA504E9B5C580243BF5C4DC401E1612EEACD45C438DB99E8663EB7E2656934B8988B1CDB0198AD3ABA42B6547730D04ADD1431D3912E77A820B40C087EBB5C690A974AE2A01F9CEE17FEF2839B6596A27A1E97CB22796A9D6CD69B154DE2484C10CD81329A12AD0C8D45BEEC060D85C0B7DC4985A1DA32E45C126BAA050642B66B2C01F50156ACABA6504D498BE0D6085A02B7425927F59D71A5977A7C07DD14DE76613CAA2A388493DB6EF640931A985C3AB3076C508F72D1428CED210A982944965ACF229733E8307BF4664FCB3BDBF9F3611325A9608D7EFB973FFB58A3D1804B4C93377E4C939BA82CBFE5C5EA97A87C1450FFE9A79F7C52BF8BD22A3C75F4FFC7E4391619653FFEC517E58B6893A42FC1499FAC56455C966FC2B3BBA3FC363CE5B3467F84A6FAF7E4E9BAB8C9CBEAACC63141A7F36D561513F4FBE631CFE2BFE6C51F423DFA2FDE485FE50F492A62B827E2FDB1FA5911A315CCFAB4FD327B4EAAF84B215A8FDC1C70B5943E6E93A1D1EDEF925A651B0FD0D739CD6B4B2ACAF4F5C2B67A446BFCB2B10398AD2A54ACBDFF7E8A8A6AA3708807063C9C2745BCACAD939705FA7A9E944F69249A576E0685A67DFDF07BFDDB25354446111C23D22F65B48E3FE5CBC8916F428FFABFE50FF749259CAB7E088F02119CF45594D52C2F1C8F2339491A3518BC67401B4E5264C4D70A70E2C6C8CC7A3F6D403BCB4E5BF88953E0EE93CF1EE3E51FF1EA7A5B9DE7CB2D9273D8E7D4972EC60AA72FF4169A0BC4ECEEF990BA1BFD11C17DBCA9195871DC787DA9B0035C20A6037C48DD0EE01A0662FC50CCBAFCE83296CD348076E3927259572963649DC28C6D3A4DC3916E15AA1874AED030BAEED25EA2CEF202CDAA7A7A5D66ABF87B13D3266AB8B81ADD0F1134A75BC22AA6BD944C52EEC4144F4633F9ED9B745296C93A8B57A72FE2C68D70DC669220FC065370D64DBFCFD59A7E9F4B9BDE82C89BDEC19936FD2E8E8AE5E359515BE5451289C58284E50A0900C6ED06046BDA95FB68CD3FF3A080B88DC7CBB9AD2680749B7B91A72B5E2BDBB2066FB7B74393616C2554CEB41204D26F65B14118CEE3346E01F84A708459A0A6428E71008469370FCE246840D66C12869106A0183C60A061DCC63708CFE8B8E71D0651165BD493AA365C2FB3B242B1D582050F04A6963900065EDC2040ED98CE0107879343393BCC6411CB53AA5C3FA233CAEEE2B2E48A2106C0348E2E03823B2900EDE6099BC66B96B04936CD51B4BFB936B7D8CEB66A927069A180B80D142E2D20907E73D1C914A791A888D528F877A05158A176637283F583358C4000D9DA61681435588416F230361818A5FC885258ED912026811D2223622C07CFA8B122908D78B909035B172FBF696D39D834AC086C1A5E6ED2B44FD143CC59258662B0616309D82EACD86DEC9062B001D426361441A749625D3C08310606CC82A1943F0B4610DD59808E7D2ED2FCDB09F280950A860BB702D5720E1CDC071EB0558047AF1A5C0579F4F80E811EBA811EAD706A1EE5B40A7492F82AFA366EE3E2B5C3314DB8C697A7348F56E3D1A6E9899FC41BEEE97CF6B2BC299267EC30C2B4F9ADCE610E2AF1CFBAD78F6BF0FBF87B757D76FBDBD527FFE7BDE579B24EAA28B5E5C479FC9C2C6347C329BBA19D3684CED2447C6CE7497A7AFA7771F18C56FDD0F4B17DA5A6E6BB2CCFD0F297A6F613F7B25E4CEF8B282B93CA5A747AAF1D3D8DC80243ACFFB18DB7EC0C054A0DF137DE7E2E7EAC5477942BFCB09470F3B625863129D6C1286779F635A95768E686BF4210D249BDC26F9EAAEB6541D5D596BE8B248BD2E41FF682DC73F5BAA815618DD3764DEFF1F5578135A66767D8359CB2E34F87E92AFA3DB7E4748F29C96C31D107A9B68CC64EB65D8936DA2D7CAD770BFCB94741E806A51579BDF918B62174E4874CED3FC6F5A2278E20741505566BAD1AF5A7F8394E2D676A87EAB242463E165A64982D863855D38DE1224E138D2A63FA170F4CA3CB0C7B758E211E8558166A82725B8D2D30C1F109EDF02BEB09CAF505D027C26E4F8FE98DBFEC9459B7D5BDCFD3EDC1B1B4D59427553F5E4685DB66D13EDCB673C36A74E251B68D698BA24414A321B89580BE7060F95DE255F01FBF323641358645AD86A0AF5E62593EA4B17C103BA0A10950B76818EEF93F03681AB27051446B150164C105833240C907620435657E3380E2E6372042C693105CB653601671228AFC6E2105AC4600722E3750A60C568A6A59B0D0E25693477AA296932783DAAD179E23B0F4A0F3043E9442078CCF177A0C1ACC97735E91ED763CFF6D932AA9C4055841D07A0C4EDE071C58B7275D4595B58B02055A4F40F0DB4D8269B7B83950E8776F57D1D353BD2791359D5307EA03082AE80C0CAFDBABF62443DC8B1E0668757792CC6D6557AE9F7F65F04F8A9B460002EDC30368B88DC481745BDA6B1F71334728A08D43FC0AB7810384491C814D1830B7454696391E132138A72788D0F09C969260E27653B0A6A7D2E2F3E88122000EF48181E2778105B53B89C663EA3B57DD495545CBC7D69473743E2DA47238B5D63BB50E718D731C1DF66482D8CFD280BA0ECD51206CDDB98687F9C3DE43B35A2FD2BAB103AA678F6EC651F9A8D1F5B5B1813FB07EEC4B17A04620B5A40496DD4DCA2A98EE2BFD3B91B89741645E27D5AE601C110F0C06B8900D0C07963F30BC0AA6D1BBE27862858191C1F2A38E7506467F5576BE001FD65ACD08B1FC29597AD7D26E93FAE9E8028EF98A4FD405268D9CC93F8288E73C06174471C95BCE40AA292D27FD20D4AAEF4592D950A8AEAA3ADB3B493F9A6DA3784C4810FE6050706EB42DE284972DCF41EBEA695DBE713D690E43459B565F6972272747B95A3B80C8E9A374251FBA82C0BBB6AFDABE4EE01516A15EF1B65354A2CC1BAE29E9F106D29DF21816287F6E93832AD15425EEA21637DDAAEBD91844836D1D4368AC40F9C12CAAD68A8B9DA350218AB69A1E54B98D31A9D40F73FDDE6A43F7BA53A9D98496B5D39DED3AE5586922A4076D3991E1E52AA1DF24069CFE3E853B6138FB19FB133C8200738CC7944A5AC83DD0539ECBA32BCECD34EEF11D66B0DE0C6E07868D30EEF94994EB9EB048122CBE9DF2C90B4FEF6D0C691BE86330B26022F5E8F01ACB707CB689D663BB6AEC28EFAF8A91AB86DED52D991E9FDB5B32AA17466C997DB85E63B407736B0D2805BA5B6685A4975D79FE481F81EE4E8FC2B85D721DE82E09AD518BAAE29EA83271577E225FC786089ACB00F113D33190BACD17468E0E564217FF0E359A07C33D2365007DA52F1B0848529889E0F8E21D389519C47AA05894D2CC98E158AE2A09A7494880C538009FB704946910A6520EAF050B2D6E351CD1CD87346DBD46D3E5ED566CB45D8B85DE7B86D6082D68770F246FFA00E96C4BAD987F8EDB32FB3D34ADE45DA7073AECA90DBD62BC104157118A9E1228D806B419DED9E107B171AFF7388DB73734B97821D77CE3CCCA51D65945AEFD651DDAC314D7749BB56C1B497A3BDEEB2831FE24E2BBEE53736DDD7A5F1DF1F08E65AE1F939FD4E7E7604050B22CEFA3FEDB40A1C69FEAF3E36F96F57F7B4B2390D0D384BF8D56C9B6D4AB739ED43F2A2C2A5DA9D65F935525CA57E7E9E9E35F62F47E7078BAE7C933233BD21A8CB4486BB8E2AA9490333E8A295D2559384A619877512F70EB22DF66ABB33CCD3DDCE5A0E89D46CB3F42D2FB2509701FA61EADB3C7C87F676A2C41E8D0AE758FD297551253C01DA1BBE41FB19E0A6B6A552FE3F1A4D2810EAA156832E5C52A2EF45B781A15880863D511DF252744E8C2BBF70E36C786C316CA2F2D241E57581A33D314803527E325F6BCA65799FE58444F8FC9B2AC2B5AE6E66B24765C0D4EF4CE30C9CAB736953FDA543ED5AF8C5624D32E0F750D7A3CD435E8F05057AFBF68BBE6FE10BEDB0BB6AFDA5E172B7410AFD5AC7E9B0CA62C650B15DD6AA802B877E54068CE974E5786D183E810B4483635672BE1B2F2E6EDBF3AD9BB1A7A141D6619A38FF994938D6955E4FADDD56A07493DC6B8F6F819C898B3514922321ADEF0D45774F64891E8CE1081BE080165A7C014B4613F34C4919B3F4808281537BB6C424A17978D0EE165ED76E3C4164D36D7A756108D837B7B9A13AC6604185FB9052EFB30335CF07CEF3BEAC1C09C9CF8120009690F6EBD16874DEE293DBE13AFCFAA9DC345694ABB4216B5E4A1D7C24B99AABD944373AE6C6AF7477B55707CB449A13DE87E3DDD7FBD2C7EDBF8CF185593097292E63A18C3D16AD6C664245450867DBBBA8843499229BB25C42875322F5C829B6359D73616AD0D0E775EB27EF0766A86DD69C7D3A4537D4DE5AE755B2DCD0EF6B1A30E635CFA967B897469911F1605CB78173365C55152E18EC781681A33A77A90B32DAF799EFE19C35AFEC98241F63D04C0F49434D0F61F3E760CE95236B50134DDCB94E5A0E99CF5E95D96B83739BE687746CC87EF559C35CF967A34667A225E8D9AA3D3A88C314F162C694AAB7FF21C206ED6ECA8CB74B49D7AB221DC0783D591073B889D2B5B60DE1805225B3A7EB9275B92272F242F6378D4FAA68771321FADBC03CA7A80BDE5E94A0BB0980F3A404F079CE5D92A916C9ECC666290E94E11411940FC77A57F2F02491FFB6CE8B4F7D8DCA673503A53B7BA1ACE4D022EBA476E7D2FB539B591349B07031F01B9682E245760F3FB378A7040597754EB30DD53AE68B54640545CAD1210EEC33AA1692B2AB9D954B355A9E9D7BB2A5AC7C0C3CCB65A4D35F785D76946EFBAB5E7A7AE5621B9A9DE615E7D499FE16A6ADDE6D4B5723D608F8413DD70AB6248EC0725A3A7641AEE7DCEED4E1D1A246AEAEA8D13757559FE9A570DEA2EB6DC3081D907E8FD41775A0F440F2B3E1094332B09EDA75591B134F46A9B5A57F51E212F4A35CD4FC2AA7040AF26D7D852ACEEC2C2D458FBDC2E054A86A66419D1ED38B2D5DB4DA25AF729B242342C17D46B73D7450D1456CB2338F75CAD8E20F2C3E2A8B7388EEC2B2BFB10DF1E9B2BBF83710899ADC3D6FA3D60BE0B57FC7EB08EEA19C74D73BDC52BAA2CB3233CA351942AB9D95C4C6A55A8F75BB420EB8EAED05326663D741745AD86E618BBB9A7428BA5DB0D548BF5B036CCD73BF339CFBFB6B3C6F6AEF8595ECB635C19E4D9F6BBC9B150BAEA5B1A4053EBEA9C7E1404F66B4781AEC19AAA2020D72A85A1DDF967BCB8650E4AC5D61B6360176A7863DCA82757DE989999DB5265D07A0CE4DE8C0535D138AE0B124CECA7A0603D18C88ED419AF1762E567E25C913B55E4E3C042095D268E464168C892244406AC1852ECEE503358553A33BA4914DC3C0BB096AC4B18B062AFF01A4E96CC6612BA5E321BA48725D3CC47D30CB8ED9A8164C8CB3A219D0E2DCC82922F70A560E0444B050B6C65FAFA5AEC78D358B430DA4EE0C4DD13A724D6C314D69BC22DD702E6CA25A4CB935DE86FB60BE60A5F35F85AC629A292759C0FADDA2BE72BF988D0B53218311F1482D99ADE1DB739DA08767E674FCB7B9859A2B8D24313CBFD7AEFC884E7F5496EF45B873DFA08773C4CF6F93AD2ED360B2EDEC3C23D169C37B17010D1BB58049CBF596EEC3911C51DBA34E94796B8BFE37298CC87BB2DBEA9F42EB78B244E1D993A4D3CD73FDFF5188377076925257A9BD0FA0E0C869C7E9D8D2A12B5CBE89D5572DD71BB4609DA0AAE6556669BEB57E568BC078DEFC37C7313F6FD39BFFE278883B0084ED28B85307E58B59FD56A01BD4424ADECF08A0BCCDD26F26B780CF2B03E8D53DBF40ABBA2AD3A7DDDC039DCBDB17373B9B2B4763DB018502B404C3103C50F2766419DBED4C9682025C5263D906721BD44C83A56CFD2DE284518981DC47BBB5B48623FE8377DFD76313ADE1DA56D70BB9F14E72BE87BC05C60A3721670E0E0BC053C60532B4CE522002CD01E6ED7090FF2D52FE7393AD2F3A517580A07DD6013B6E34835589E1BBA38E963256341D766E796B412775AC96B06C85D10ECC6A670D3A477E9D32ABBDE7DE4EC9E2686F2A042F45488AB74A9A1765996299F304981733DE100FC244F0494ABD9D05A51CE6704427B9815FB3D2B66904C8D9238E9EC6A809466580BE92C871A8599394482CAB9B98609202BBB1B61707D2C83E33CA8003D1560348798239293E65525DB3B5D53AF96F209AD389B1D4FE561EAF5F859A702510ABB12481027AB79378B5CAFE61DDAC354D69BCA3DFBCEB64561E291E7A0BB2E92357A35D915BE9B086C9DC4526907F92AFA7D7C93C1EC3CB7C79464DA98A46AA6E3BC50DB74F4174C1D56ED7040B9AB340FDEF4FDA67EE4D5B467475DAC407B20B90E1D204D8F615A39D31A8BBE8A7C285A48E591E8C09D3C407B51446B1F6F0DF6780F6A574FED5ED4A8D1CB43D74BFFAFC54C75DA4ACB88F891D5014AFEB0EA08EAE419CEE65D4FD713A3417A9815D33CBF8CFE7710EAEFF8F18B9F7EFAC907D9F3A88A10CD81323E091B311C21FCCEF88698F0754C1282FBE42205E6640574E852C7501E66F84E380FA75E0269973A08205FF8EC5DEA1826972E750AED6156ECF7ACF0EA52D79E598C078E0BA434C31CFBE128CC8C4B1D2AE72E8C6E5DEA23A7DDB9D47B9C0715B0BB2EF5A9574BF984569CCDBE5CEA3D7ED8A53E94F25DEA238893D5FCB74D3ABC2EE776411F311F26B4DE84BE5E1635F3C22FEA355D474F5AEF98D6184555AC3B3038B906C181ADA6EA459EAEDCD9DB2DB6C394D49B9232B7D05FFEC5CF63E468AC180F0DFE59DFE313DF279BF8AC8823EC8661FFD918DF55BE4ABE261042B9EDD2B5E5F4C5D688313AE93B8F9FA2A25279F796AF6C0614B0BA198A17EDC8914A862965550B0BA26B986066874821F6AA06D0816D115FED75E5BA0D1B380234AB43C99E9211054C93C852B306757624BF510418D332AC94D9900120BA07A682A346459E711A2538379499BDC32C16358B02645A4794F31A4902D97978B199EBC8FA1D301E9658A74BACAB2310234DA4ADC2192F0C5FCBEB4C3269EBD8FD2F55266A195F1969F986DCDAAB23C6C384DA5DBF503B82EEF677BD8CB958A039F603B4863BF10361CB18ED04C28A98894A975BCDD3BB6594DDC5A5CB684A0CE561A63A5DFA5413D4E8EEDD9C66C7997823FA719B0C48DADF7D8660A128C5E41F06197A821C7F493679C4DC07767A58397FBB870339D38E38525A3DD2658C7E64009C1D727D8A1E6267F91B19C40765394DA457C3FCD01E6766F46187330BC69F8900AC9393A1BB382A968F6745520F4F12B93E1D22B11FE6C02EB8A35D6C28FA051E09BDAEB3B887FBF2749FEBD645F14FED9262CD3539A18F69FE10890EF0DC903ACBB3AAD1389EE95C24693D7B9D489CCA2ED19170ABDD0EF243CC3CD687D48BDC901F008C1BF903C1DADB46711AB73D746A180D580F2B82F6EB8C4575DE5C3DB5DC497DC8564EF0B48EA32676DAE6FE5CDDB16A6B9953F5222F36E0E225D111B94A25BE8FAA9EB54247D028ED8B119674080120A0630882D37530236C231ECE6B0104CC82244DD8B26248F6ED0031B8F6A5BC5C95F33D2497EF2D808CEB1D94DD6301040F9C3D1840603DE8D5F04E74525402E407042693646AB279D201189313AB600A45A10390FE31F39E3BF69C1FBCE6534DCD76086CB128389F25464E2B01633BC2DD01EC48334166C4F79944997DF8FE9414117E60618CB2991AE1B98D64F6642DDEB4FBBAEC193F27CB98BDEE897DD63D1CBE3959AD8A5A4EFA4E84EED26D19D50AA95577E1A3963BEA45F28C6D9702924F7ECF56519CD66D988EF865BD3E25E82CAC9995BFC6ED63A1D6AD91F8BDDA056FEA80A14E435ED53FB86F320C4764342C76B00583B0E75B1C38FD6C2B68C913B6B607611BD99670DBD6153B7B94887B18283C0874760848B2DAB1B9D7613D587D9A8E2E827B3CB38528B635C6CC8C4257377335D5916B35443B6A64EACA68A2F50AC76E829DE59B4D9E75F3EC30AFF4E6D555545628619F8BFD508B85999BEDB0E3848CE627814062C6BB31384EB6D523E2E2B2E134D32FA858FB06DC264A441DF9D1C92365E869B1287B91584E7FF113F2858E2BA36575FD2D6BCFF83E26CFC2B17377488A53BD8836492AB298DD93ED76490E6C55759A67CD940FD849744EE3FFD816A7F8F7E4E9ACAEE820765183ADF936AB8AB09CBD79CCB3B0BDBC405A3320BD0F9BE68164014FFFEC8EE2C96A53DB0165BDB8D70B66703DC4500FAB8F08F281F512413BA47E220807D55304E5C0FA8A647758BD45D00EAABF08CA01F5184137A83E3B4DD2B426105C936174C3EAB08E7060EDD5510DA9B73A924135564733B0AEEA991B564B755483EAA78E6640CDD4510CAA93EEE3E5635D2D4A836B25827258BD34900EAC9906BA2175D34034A8761AA806D64F238BC36AA8816E501D35500DA8A5069A61F494E393E3B007C5921083223D794A4290B98DCBC6E9FFB1C8B74F21082217777310178CD4699A3F0424778FDCA901E9FDC736F67146C5A5876E927827D7D13A592EEB45B89D789B48A8B3FDD0BD8B9779B60A48B956A559DCBC29DA520ADC719A7CB8FEA32CF50F51C9B420FCE9AB380A441A57C13B6CA5C22EF4423DEEB60FC3457FB87D3D75081C7F9B860705BC4BC3057597F6A623C2A4F8C0BFF35AE620D50DDB39A7E7D738E2C359B6F95936CE4737E7DA38465F67DC97E579BC192EB99AA6977096D5B98BB26D2E99B98DE68D4B6B7C9FB79B87B8B8FE8A6E799527699A7F8BF17136B82FD663BC41912D4E31367AC729C6E18945A7581B5ED68D75C94877E81A2EBA4337B0D01E65330828134986991FA6D3AE41765A44D9CA15B2BEA7FDE3F4A52DD6DBF83FB7F5241E6FD620EBF7A979CFDE1675AB0B6CC3FC9DC43359AD217C9391301D20B311039019673258D6B49455303336FDD897ECEB8D5253D42CE0D7BD3577B0E36CED389716DC3FA1EDA694E5CDD3359183697630CD0EA65920D3CCDA0DE4D4DE606EE5A81A28460B777317387B4ED0F98ABB74342DC6C3AAADB76AB75C2B9CA41F6E50C54E9230DC7EF27FD071FBA9716ABC3849A05363EB2E503B41676C509865396E870ECC728C15816955F0726BADE03C79E72169E744B91A1AC64BAFAE8449D736080228E0630928DF58B195789F6C5749755F4449FAA10D4A7123E414DA83A8EB897ACDB9AC8C96E06E55CD316EADEA474901F6B7467BDAB1AAED2C1EA5EB64C9B94D070178D9F57A4CF261CBA696E10649EBDA8A7DEEC7EB2259A364E556488CDED5EA5024E5B2D680252B365AB56FF2D2923EC2D0A92867884EAA2A5A3E5A3D1D360E33B8CC50AA7881838F4B0E1F8A597E04A0CE9236D03464CD556AA8FD6AD9AD478EDCBA1DB6C3E238CB479E68CB315F4629A9824C7C52F5FF9F237C0B15D0915AFF5FABA0A7349A20D90FFAFF43B64E93F2711AE297657EFF2DFF1423619BAE058F451C4FD986BF26D92AFF56066C877A2C1442F14B52EB5577DB101CE741CD6AEE41105E89AEFDD145F47E6BA27B77ACF5CFCB9E06892575B309BBCDBF09F339BA0B7D25B650BE0835E98CAED3D569BD9D0810D2DB9373F420B91AB18F71BE2EA2A7C70077421A929FE36F21D959939BA0871E46502346B73D54BBCCCA2E26C9D1DA44E13DAC4F9AEB53C73FDE8EDCE8B4587855CDA95B986A3E27E68914910553098F7C12C30251EC920A9691ED2DB21B87490F719C87D9E276B6586DB955937D858C32D299ED661AE3AFC9AA1A37B3753B537D03EF9738593F5696482E37E89661B4FC635DE4DB6C7C50B2330B5498E7436BA169AAA2B12038AEB60281F534D57643E829A45B108DD602BE2C2FD2685D0E8CB1D35C9FF2FC8FEDD36B928857D5554F83555CA42FB524E17E2A7278AF6224FDFD54686E853796D6FB573F30A240C09E35DC18A0DF88A1897C3643A5B7E24A4D74553C82FF2806FFF294E6D10A83677519017F59B3E73B06FE1731782F694D1005D6897F115743F1757495FF25AED2C76C9D2CF14AFFAA5689A6F593B8DA6DFC941755A359FB91FC819D3DED3C11CC1D2867A597F9C312DAE139D4389295A750136E739E144D56F417D549749AA395496D067DF88E2C09AC497F361085565D7B148191C00E0FFD55FE90A4F159FD7B11A94A405B075D7ABF7B29AB78A32A02B535F547953F51C424B2D055A275B044A776B580364AB4EB69917F432FD2B31525FAF5BAD6075CCDAA24B0FDB9B44791C549ECB0D0DE36771954C5F53C592715A64C2452FA25FB23CBBF653CF9D41ACA26114580F11CE8CC6C50EB553257D64451B6E5AF425A6C6F2CAA006C1FE8CC8CED3A7389182299E22787E8ADC51075F9B5BC0E4F43638787A6DD481036B1B842A30656C4884A945D5B831A57C98A7C1555CB47746D456D29EEC0AF6A7E264F69ACBA107FC9683A9205B803FF923DC74573AAA6BACDF9958697EC706E8A1CA5C2C1C7E5278B99D06F9842CC0882D60ECF8CBF4609DA2BDEE7A771BB7B5EA94E1166B04D94D8493DFAEBCCB71AA3A8ECF070B53D511FA4F6B63D7F9058E7C1EFF566148337B2D992B21E36C43B140B390641FAB42304247778B87B0F95B281DE6927D5E166DC039205EB5313DBA6B65A2137196F895212A3BEF3ADABD7A77E0028CD4C68FE1A3FE8F9215425E6A648F8E2A2354C5E5DEC2389990D0C21E596B35989DD1FD2D8B32EC528CC8CD9DD553D556E7FAF1EF2EFAA13018147451CA9AACEE65C4E556FDED69B8F5C55719E3DC6CB3FF0964BECFBDB7AC58EDAB34535FBFE97BC48FE81B2EAA69F126C2D921BF7C8494FD69118F86749B1C4DA2539B9388D0A74C8FBF69C7B72C10C5A9B0EB587960D31823ECBD3ED66F449BD315179DD0C1943EB7CCEC39ECACCE6A296D59A3E3DD653B72A9557A5A82CBFE585B2D9FAB9B67A0A14B2A6361D9BF664541DC9ACECD24E5BD8339D787B94188CC20E4BCBE2CDB9AA9C2C305D211191C58FE73CF1D099F3BE1DA23499998DA3D60ADC3D70AA36940D66B2868D57B461A067050DD29AD980B5C1B56AE3D51CEF28EBE84689D28A57766E895BBF1225DDC6D45B1C5BF7C3739617E884BDDEBA3603D560447BBC6DE9592EF8846726247AAEF45554A93BA0EEB60F9BA41278A09486F2224FEB2E799CCA23811D1E9ADB3CAF54C7A5EE495C3C15491913B564FEA2A1D6D9639272BD848C103CD63BAB154148626DB5354822B2638E322E0812925D1082270948364037458E9CA3040DF90107AA429291EC824860A3D8AD2E2BBCCF889D91C2CC264CF3B489A2A7EC79C99B2E6A0B4CFC9C788D8B1A09CC8CC93A5AA90B20D20B8952554864AC902C9C948A47318A85235EB5F73FC330423B2C0397D9D7BCD8446DF4BF9A1CFC352A32FC6058B63215451E54103E7072C1FAD91582C4765820AEB4E2CEFACCD11616249EDAC8E310EDF8B0B49958D4068538F990EDFBE87349D9DE8F3E29919E35A62BF5B8FFBE351745B4265A25B11845FB3A5523B2C7D1F979540D49C2D5A06A490E31AACCB993EC26C863846D244C4C23384797A7FB032CA91D9E82BF26F1376517CC4AD943FEA166927AA0781AABCFBDDB5A39639E1AC9DCBBCAB10802C97CBB8BA362F9A83AB3CEF2EC6B82A924D9D114BA169B36676DD7DB4A754A35F09799C64CA2F0CBCEA8EA2125E7387D4005FB63861B787D2DD981645C15498C0DC51BE928B7153A429897507A3AD9563C49B18864C9C063759A2B88B8BB49220543D5E691BD5B7401136BAB44265A5EC2552582F12143878B6DF8E7591AD5B6FBD7EE46D3384325B27281F2F025FF88D9201E693071F1C7F078685F4722364D9DEE75D3BE8AF436DEAAB9518E4BE75B89D8D4E2DCAFB2232FDF4A44A6AE442DB1C44640E6D7699A2942A028422214FFAAB2269E9465BE4C1A21A056E4451FC777DAE6EA235BF3215B1DB5176339F0E305DA97212BE628355D7C70B2ACD7BBF7AFFE07D35939FAE13595117D6B1992A87F78FD9A95CCDBF86B8CB24126E82DE40CDD0F4DB28A003942779C936C993C45A95A43A8EA476A77F4D1B00C84E892F3F829CED0F57A354EABB480AC09B767204B32ED58C6B577C7983069CAD87DAE27632DBC3719EBD04F2F636443A6903192D33A32D6D69C5EC61AFB265ED5168EAA26A36B3897338680A2A4B916335E3B420A1A8FD92A6DA0EB4E276C7866DDC5F0175FD4607850D070502D69E3D000646D6CB0278113372584B88919AED202220DF4F452D6E71E192F12946AE2065494CADDB077938987064D911C8209AAE5C26F2F9C02E684965201CF549A42250B9F8FC06A6A47A69AA9B02AEB4A96E2D42A93DBA2A964729F1468B3AE6A0946B3868492C286984C57861442BC4153C91F3E02AACA707AA95B80CB9AC2B2CDA9289441A55DAA06ADD92DD712A684144D09CF764C3936BB308D559A84170A25B4A955D288148D291764B82921C50D66F82E4A99D81FC3807A94AD29DC30DC060417265DC7CBA4CE963E5640DDB3C7AD0109D4188AA02E4B7C0261454ADA8E00922565F64E79F686DE90E91B7AF725DF789355D4113D89ED26253517DB4D952721A554C23395A60C99CCA73EEB183A350892EC2882A9213AEB30D18C7C428050F208383EF7E0B625E0B90797F13B2170433692364470210A3B94AB48252C227D39442AAABBA7F568430A541869E9557D6AF12BA02ED5E2A552BB50B5FEBAF1D4CA95EE9D54B7D21544AA9523C12A9A9521231056CF8A95D794807A95C774A5DD711FC13C1751A3F59BAA28D013D2AFE0D1D4040BBBB2A27622869C864D208D9C01D159EBA75683543BCEE32A4AD2854C3285B51496F4B682C9561CA638996C2A352BFC5A0D0F884A3BA69647C914EB04C75087756C09AA373B9A720985E784FB2844A5764EAF4CC9B13250A92D82B928D67E2E6AEE9D885A9EF74A242DB9C086DC13817C98600F04F26817F42A6334C95C49BC0A0642A8B1AE4FE9479235650271DB492F529FA0FC769B8EAF40A23F3A8DDC7DE22B415504902842757594A2326D403E59304FDA519741012457976F2A4D42382F289C934B757BD57741BEAB269326A892487AFBB7CBD5F5A8901220ABD4BB705E14AA4A9B028AA6681054C571A838373944A7A9ED27B95EE557D59749155D2AA02790CC11CCB31695B3633A210578A529AA53068A93530D7BB864CDB73B057524D2C9EA314585C9D0114865DB78EFFA92D7A4B092C81B001D116C2A4E2681A30522DFF100B090C48D603AE206210FBFC511B42280640918BC131B1BACFDC2B8340ACE8B104D1085C6A11E5670762AA4116B37BE9751111C1CDE8B001104040B5ED360EF0205B526AC60410CD759E79A4DEAD48656D3783D3B8BAD2273AB9858590095898D2C7E8B027B4B76DDC4EA5B31787DA48616B78648F2D45D6A2A84C29B5ED2B604143B2EE377C20C637AD17FD0173CBAA6770164080AB4606881E4B56D0AC1E40D8C8E5A9C8DA0B68A5D5B4AE16A221105D74D15F9E4909A5E38C50D0B2899E2C1D803B1D47120736B6A0BA78AFB984F6D0EDE63292F2613524BDF7183AC9C5C4ADB7CDDA4912C9750B096483AFBB4E0FAE20993D2DDDD38964A61FF034AA490393AD2D8229A4C1A3F3439D6C7C63C3D25D97A41090F4F52542A43B209D6D359DE95080B245575521889AA4EE30248ACCE20ED85E05E8C3A5E1A91A385C5589445DA56AF0532ED5B7A0ED031E2D7A4422EE0A24ABB40C433147AA50D985AF50934F60C36667A0D9C5C6FEFC546AD39892F09DF888EF2D643230DB2D0D09DC66D30D6DF3E2231949B1BD0AED61B4B55A967514C2EFBE32696F10D76DB7B99C8493188249EAD6C22F5F22688233495E2EEAC045DB98501655C79E07494FA4C22E2C85936CC5FB6CB8ACE38255452EF9C865AB56A8792DF2E500CBD3EFB423BF4B478AAEDE19B8DA6276E0EC0AB91DE8D111887E9A511D5280719F9B998361A6D0D28F01A03B88346CDB01BD1B90D25AAA4936D4C29D5D8DC6E3AA9743E8078AAF047A519936F2999FC54F7D15A41F47068E73247201704E6D4E5BEA50CEA6748F18258B1537255375C9E011602164955076772AD9820314DD24EA80901650A62F24E89D482EA899EBA6A6AF854592D01B1DA52935E67AA8BE8F344EA8B60CB6ECA9BA6B0F99534AE98A1021E055F3236B980EDAE74F54F5DF47F9F6D0B44412A669C7A22793378AA44466DBAA41C9216051442C948E8486357757642791319C9645B2D904876C43424D25D2A2DB5364D2893E448E888645B7372895C503DD35887FB1ADE96E2818040F69405DDCD824CF7798A3599668B8ED45D17C91ABD413EBDDCA93D38E1E59189291F9698EA31899DBACE3B241FD67D34425651E72562D9835F325273793442952701A45195672A4D994DB269C3E4FD6AD57504564D5A77204FBF1E6B82CAADABCCFCF393DE8B225AAB2A57BA8A37291D290824B3070A258D4CB7A7904086333B2575CDAC517F09820417495B036992549A2231DD1B107043028A18CCEC9D12AFA1F5C2435710DA9B2AE31DBA62E5A11458E04357212B764AAE940E5D2160E1AB60FA87AE2089C08F6F0A9A1050A64C0F5D6723520BAA277AEA8A7B14E64A65714FC3B4A5D799EA0A7D262665CB8ECA5BE719D211B8BE8A3F891B288844CEEF21ACBCDB93081DCD99DD943A4D15E757BF098FFA79147C49D9E46A6D0F74DA6F9BB47BD04143C6B04ADE240DA721D06C235828A9033A3F85EC01FCD92D09547B740806F7712831A72786C4BD0E296EC68F0ACD48D0A05BD82A1207D673AEF0386400010421BD4BA0900B414551C8A8DD90C9F8292AAAA6371779BA121A760C2828790394AEDA63D10312D716F912316E0F434815B7FF4A8234D49E832849760814A02F31126E0DFC8B50F0CD00A7E73B223EA8CDADE08B5DB3141C243C23888E4396461CD617CBA11E406E380C55A13C698051D7E6F68758E3B0A090D8C022235236005E8ED4785DB7F8DD0B203E7C1EA8106FAB4D2D4392106E024A5D7244CA8644A96CE8B88BCA061B104E5CF463AE278EB5C634E45911D79BEED5E98B4C600860A77243620EBB52899A104E8040DEAAAE5943C5E97D019D084837E55DB735F6FA2A87CF3D5A6505E4F8E499A21F72F34E327407D6AABB6594DDC54DC0ADD842A60121A1C16074E486411D56F5F0C807901B1E53676F268F4160D8904BF502DE5DD73A07C70DC88F4034DD993FA2C684D442009F55C863D5A6172C698A5806D2B9488972BDFABFE1C86D4648493249DE3AF945EFA1F54AD178D2303C23D99930F06EAA88BB1D5DC1F0908E4FD1439C2A9C99B1759C9F97012424F1280D94EF73327ECF434B1ACC1C25BFF4E4E7637D3FFAE478F21474FC2ACE258FA5204898E839F99CBCDB21C58ECB999D90BA463D13174DF9E206C0F2CE4474640C422BBB0DEB49B2043D0C20520246CC7EE56CDBDE49B3DED5567955775226A4328FEBACEAEC0826922A5C536A0CAA3603F3EE2E8E8AE5E35991547191446A3B02A08E689125C14D760B10C169F60E8296045C680503B013DAF13C4EE375F3717151E41B69E40100CFD38323A86E1402444451C8DC3935240D09A4E604FC5673D9179B49650CF504EB8350BE005848B648301DD9820884555E8216041028018367AFAC80B68F7FF2CD387135EFE285D3E2F944B8B4FCC91CD0AC69C40F180B55491CABCE6501BDCF7596CF16DAEBE2D991987AE9249B117EE12439AD742E904F6E9869BDFAC0ABE06C8B3AB7971E641D0EB509B57CE16152291B8E5DDB9F57F50FE16BD91C7830BA023EBE1689190F3B74404E80789230496F03089884232A2D98FA88FC6EFB502E8BE4A955C37116D51B66EC9340D6241541A1C300B4244F460B3C53A7C17C89A1222342C8A3229F940413AB38611A57D4072DB9E4578113BC2230BDF4C15CFC534AA1BCDB01E44FCE1B25D3AEA93EB5CC89AF6BE0404EE56A8A1B615067C2498BF6858C89E56308C56B8758662D75BD5437C184C1AD2452AEC2F115DC0A920F686891CCDC2161114743CB22A1CD05658A08E889A29F7731F2B9692C1E7F24712010A0CE5C0724D6691215F1BB17CA510072612724E8327B4E644F1D50703CD96941741D9A38626357A666A79B483D799F47305E9715231DB968C35FC26689079A232C3377627E342D96272264419D6A5861D2411CC0B7869D22D1209F0BBB21417DB0E6C9B2CA8B52D1DDCFA9E44CAA78F80511B624A84F4993743E94CC4978B413D287BA235FE906289E7C2100DD756E441A7E9963680792198691B31792A1C5E81771A4C508C68BACD0514314468E8CA8099E71E404DBA5008202F75C8530AA30BDA4284889B284C8226EAC9C9A9A9DBB4DD68FD5423E0F30385E371B10DDBEE278A1E377A509663C1500EA81E602C0CF9D980C6DBBEF96451CF3EF9CD3801E24A6430C5E366FDAE65B68C80604951A92A92AA4DB1A9309CEE9B6AAF26C31F480AF6A184848745A20CD2C822C668EBA51144A23F1E1762F80FC7019A042BBAD3CB50049D40E01A52E38228543A20CAD6D40EAE14465E7F44CD76049EE37020ACEA4038DA938890E8E52594CDCC5A7820D082029202F55E84E9CFBED64BB4AAA4FF95AA6522838485A7A101D79A1D19A2A16DD1EDFD72C4B3F6455F1D265BA167A49F855B87C18A1B5D901130A1EE92D6F4B806925E7BC4A23C65AD3CE33AC235AF21640D22670D5899A308168ED94E36EC86ED4F9A5871449DDABDD82F8337145D163F6EADE7F0D7A82AC567D5FFC3E792F6B590051541D1495A650552797D09B68AD2F9D50259164DE285D1650A4A32F91EE165D9586051448D138CC5718DBE5BEAE53D535E2A26B0BFA3BFE5EF5718ECB6D59E59B28CBF2AA41F073ADCFCFD2020D7AF9FE55556CD99C7F08EB5D5C519673F9EAA82D024D6146044924AD710BA1E8CD5E098276870A21E8F7F81204982F8AC581B98114D0F030482B8F27502C82F11C4801090F81B4F21846C7221803D8644892B2962A24E82895CE495545CBC7988B154FBBD3CD8FB68A262D2172CD364B1BAA89AFEB976AFF35B0F3467B40AB34EC636C0F8B698CAA51949DB102578494710ECF04F2DB35BCC4A7D83C384712BFAD700622456282F65320AAAC68AF622A30A4055445CB6503FC009D4C0B02177B019D085CA59520EEA351505511EA3E5C078753467E57A14B882AE8494839AF33F05138163F08A8D4FE16BEAC60AD404228236CBB29E38332BA36944880AE01D041C7D1AD24843242A432E54847282DA99549ABC6DC12CF29E5EE8A943F0DA3DC5595D9A33D31E9E0418904A94F4C764C5588B0D0CA0BED7D04CE7BAC58195513142A45A764069061BEB009A92C09D49E47D43E65B3A75B322F8A682D5B567B1865A46D8E3C11CA0642DD0C82C7042B56462518620C9DF2108B8C7CF2A68A62FBF0F79CF94DC45F4D9620EEDFA96491F54F96C85A363C7607B6087B7950815FFCD68CA50A3BDC219D3FBCCDC55E44D018C32E47B67820BB1B1A8AA3496750E40E280DA8D26C2C9F15A7CD58562085BDB6182109211F23C1F8288E0D9D69848B6948ED2155A3C84F036BCFD683A38480BCFBCF4346DEB397755582520BD9780B8A2718FDCD2A0544C249A13619C85391046C1573EC24C179B64DAB6D01A2EA8AE483193DA4F12F49595B372FD02812C58A0BB4C8D4618F3C1491DE70A49FF45453C8303F26AB96167569B2CED0935EED4A8741538A8906A55DAD409A75B2C6E0631FBA47B8C618DFAD1C61EF4CC71076AE1B8A07C724134C18D4256953611094CF4DDC1F2AA79B3D83A80C6E3E1974F6182FFF8857D7DB4A41861860853ED1751CB08941E99351B82F7281394F01367140053D826B802CA29DB1224671D0026C227CC56E99D59F098D9EEB52CA35A88E623F81AA723E8E1B3155764264847C65FDFD7ED8AC2E9A6C0DCDBE6B08AA3983A716DBF6484391950DB066079BC9E991810D7EA9683AE5DF023CB0124F7B5E1D85DE72AA8A99AAC8491EEEC9E67AB3D0A94D720A54A1B7640D3103C19555096DB8893C5C0D97F34966C730C06EB9E3DB7E19DEC75331F4F8C0FCEE70EB407CC23CA9020EF151866014399D31D354C02F5E1D853E72AA3AE01E0F737015469D708F2F670AF61A2CB07C63C0D411ED35784E7B45CC0013B9C363218D9D7758F29A914034D510C8A549090FC4EFD10BAE20B36A54200116F3C7990CD30D148930032B9733BA8A48809518CB452CE0A10F7E31912972B6D155D43B494B936B26D2F8015EF2F0DAAB82F6247B2160A4B882F22424EB09A6F670F2AE3EC129DC7E192819BE3E34405B22BB8AC672D331C1B3747654F82CE60C9F83458B8AA3922F526405F5E582A8E76C1122B1F219E86C9161F48AC04EE2C2AAF74F642519312C94710445B2810F67007C53AECBEFB62A0A88AF9C603D019395A9014C0779E28CFD6D9CCF828A50E3B31C8497771CAAC6632D11FBA6C0541037C0483A4CCF170F89303B654EE2B5B4FB8C55F6C4559C8280B744DF5D32382F86A674419942CE32E04A1DA66B49780948913266010FFBA8546BF68D9A43B80A4160FC4E01D0109B88684E018F20749E571E8C24CF87468328B59FE72F3360856F3F19460A5FCC24AC204095FA81D770C41A022567166131C94EED1475150440AB59076A0A88DB4155BC21D40F1D0E2D54427C6079FF983A22B6E9196C2CEA4066F140988D5457601E5349A3A7745D0FCC644808C4D10773DB39A0C3594E0D799FE18A22C3029E7F3AC827E5A6A2F5CBAFA4DB6D45DBD798AD935ABEED85A605DD093E5FE10AF23E83F544FC1C2E63293014461E620D6A715317CE1664BB20762AD5E3775CA53AE8B181EA8979AC444AC06ACE383AE5F5450A5C3ED4613A84C08425001EEFC300D114D8AD0C9F3CCF01D1EAA758D3543885BE4BD73361AAD591BCB1A7331D343128FA21A48844F61E735151D5FB21A71A744AB08B36639BF65760F9E322AF2C678E14876809066F752A8C889CA8D87BEAD8C62187771010B0771283520D8BA205A8844C345B24726B4B59C918F5316B88530CB8B3CAA7369CEA9A472930168F67371C824195D8B0A2299E9A09E11542D1144FC73403DC263D056342C2DADBC802DE11800ADDC3E11D708B40277045B5FD70C69FEE1636D75F0EC2C9BB83838B9833DE1357E011813444AC294558597E80D736A4834E3EBFE1448EC8373834B8EF8267EA0CD3E6966B5671F944659770C6A4212D60F7F7D9B62824D162BC2AF26E726A8AF8A774AB45863F50082E4DBECB9CAEC14A28D7BA524FA9F4EBAE1949A562F7CC473A59A4DADC1D8035A65B5FC7E10C1E500A98C51B0B1B9E496F6AA8DECE70742323D8BD648D5B18D23AF2D8499D5B189A9795E7760B43FF2A81624D155EE85E1ED0E5F55CAF0D0C2DC3D23429307A84D6E8FC50C92143479C0226627D73C6B86638946E5B5090F2CE911544CC1AD26029B08CC21AE89AC54090B7098501352480B709351428DE2694EC87BBAB67924D280827EF8E6C13CA262B53B96B1668130A8D09774FC507D61B74EECECA5C8EB89B2B19F79DF06C349F54983640EB74B1AFE4926D034ED11C1C8EF7DCF34D5DD0B4A5CCB5880937EF5E9539914350815538BC4607B16A0ED98663150819D147772C945E3DE2402A74507AD9489361C1AF198D943907E5226EC15554BA09D674C13F1831C448A5C000138E0EC9331743624C80892C94A07B0C30C82A3CA7A788592C36803F43DB5D3284AFE96918A5E6F3B5BB3E2B849ADD051BC65CA85CCB9306E1379C82847840646615F08046E591051D99F607571600287EEB596088172A7C00307158E16E6A7434F95E641240DA78BE8F5883037C4FB0F38E236E9E15716D57AE4E5F04FD27E1A43D20C02DB941E20AB105EB13260B56DE0E446141EC7A61BFB27688BC4A0596E499AB2319187EDB6950880B64DA6901231864214481C87A2D90071C4E612CF1BED84B068E0D608A88C336BC11059CB2400AFD1085916AB224D42D9F81A0345BAB6A865647595983CD107CDFDDA7B596B0810257EC0D59CB118B28A412BF42DF3F77DC03EE5F0998C7422B7493A9E480752C4E41C09CCB382F328761973213B6E36930B1014E41F3EC79B9254F230A92F3B3252C3E3EE371C9F018515ED98687733D3A245F94906A7D085C3EBD805AA2392B7EE5429982E79DF7F8CAC5E2A2C837A21D38042A1618A0064FFA88F738247208A1F579993FC71F03E1E73400C0F8FD00A0C10BFC39F55489E8123F80322C5B88962A7108AFA1D533ACA207BEE1D879C6071FBBFD54E4BFC400032ACF17FE2B0C1693D0FF4B0C0D7EC5307C2EAC6471530CBF575C2A270DBB1FF6B0F4A34280DB80032AD8F0C335402782920381830FDA32D3FDB1E714F6D6D0027CF1086099AC8EA0AF92AA2013C9F6883829C30EEEBA813E3B08CD445855192A80E677965F098EE0EC5EC312C66F723186669C206C1F2B97764510A2AFCC0EEFE73F838BAC7B968CAFA33A08B92AE99A6EAB917A81E00DBECBDE73DDBA8A2E5D993B57A7DF4156732A3084B38E935092359700B658BB493C01825A10BEF6993AE111280E22EE0206C9E3C3F0689E841B382ACF32D1F8F1841CC020C4AD1E0179FD879D86224421668430540E8052906461689CC68C1086C3516D77C3106E56040E6B78F092CE71AA59B08B8751E08D95647E3064216A9370428D00E22E0D703CAE2000396746349E1D130BAC4DB023028310BB0646409EA341EE5EC070703A0EE2B1E9BCB8E34A9DB6EDB057B31151B94DD68FD54238CC3888B8C91824AFDF0D88BCF3382668CFEF70A85B5277CB228EC193700646A5E52DA8132674A8C0E36F54E2820FA7DBAAAAAD56BC6D2C2358207EF3195888152D90980F2C228E38C03C35E7055F20480069D3F9A2A0D1FF1042D0D1E0478C9100A278160C0E8E8C01DB2C40E2B5E3CD63D29FF2B560CC69107EBB2948A8FB3D8898013422FF2C18DFD3EE829D79668F005AD21FB0129747E423E0325EC1B83DAE9F345555766932CA0F8B7C33A7DFCA30E989BBEBEBA2DBE6DC3A22579BB8AAE84EBA5A92252985B057F66F9A1CD9EA3C05E1E5BD85AA897879234D612BC4EC9687EF8E5B3C6779564549161743D9BBE33B74E73DEA3EBC3BAE4196F153B58DD2AB7C15A7655FD0DFDE186B765F8EEE9EA265DD9DB3FF79F7EAE8FB26CDCAF7AF1EABEAE9E7E3E3B2415DBEDE24CB222FF3AFD5EB65BE398E56F9F1DB1F7EF8E9F8CD9BE34D8BE37849D850EFA8D60E94EA7D6DCD2CAAB4265DB7F42229CAEA3CAAA28708BDD67AB6DA006059157FAF20D7EFBB81CF3D2D7245844C3C54E1FEE529EE6BA0DFDB5ABFE4DB32BEFE7AF752935DBFBC3EF9FA35C96AE0F34DF91AB5F053F41217AFBBD694C3924A931859DC5FF46E1810030B285BB5AE7CB78CD2A8B829F2A7B8A85EBA0E5DAE6ADEE4E976938D7FD332CAAF5D93BA4F36318962F8A88EE7A489DF695C82547BC8122D8CB5C872306225EA185B9B82C6367E55C77491C4E90AFD4AA2C23EABE3BA29E2E7A416AF26EC89C44715A9E3FC1C7F03D08D5FD531FD1A15499B5210C7347E6531BD3BA6649C9E67C7CC44A3941E3D7395E6756F30FA9BD5B07DAC30A77915FDCC68562CB525B2D998D0AD18BFCE66CCFB5DA6BF316F29188C39AFA29F3177A7D9D4A567A23117FA685C0DFB40C460E40575FD0C3E224863E8BF4D2142ADE8D398C6AFB31225DF52642840BBB3589CC743880B898828D09366C470569EDBAFB3911ED109912B09EA69184811BFEA9CB51064EEF30DFD0947DEF7A81B8E78A8D16E77DC348EF1AB3AA60F9BE6E9681C4DF7494F6A584D367ED5317DCBF25B5EAC7E89CA47DAFCC54BF431DE456905636C4BF474F6C77A4B9DB18ABBFBAC87EB22DA24E90B8BACFFAEB1655FAD8AB82CDF50DBF5E1AB36A6B720A6B73A98CEEAE9416269BFA863F87BF2745DA04B5FC82945A2A28A345A956F91AB9A6A58FF5143BA1EF32C46512294688D9F35715DE50F494ABB21F0020DCB0065B84E3671970780B20EE84275BC6DCCD99782D21AD8675D5C1FB709AD0DB1EF1AD8CAD6D945E11ABE6AC8FFB67AAC757CB26C5CBFAC3D0495EBD86C7D02175A839325BAEEBFF304DDE3CB8B9705FA7A9E944F6944C9B810D09CDEF5C3EFF56FB0EB118232A7F4055D2EF894B78C1793A340CD69FE5BFE709F54F4B4E44399531AC75F4C0B8733A77615653587381E6310CC9C1639631A9526A60A56704AFF24ADE2226B9231683604ABE9AA45801DA654C19C7E73B3AE9DFAACF126059ECD6E401480EE6A47C0CBC9ABB02BE057F5B533E8CE9299BDC1F85DE3A0A5C99243E31ABFFADA59728FCBE26C5D513B83FE9B3A167677A1BBABF8F294E6D18A36AAC6AF213D4097E54D913C336A0CFBACE1856C8EDF597B07FFAE213DDB34BDAF27D0F5D9ED6F579F2819A2CA74FA7B9EAC932AA2EDCFF1B38E25F69C2C63760CF0EF3AC7926953E72C4D98159C2ED3C77A1717CF280A08C2DA97E9F89B870447ACD39928D2199933742A99A6F4C4200A74F05D66F74594954945E3C30A3446BB4F0BCCC83759A28FF13FB6F116983640B13EEE2629071F3756AC31FA15A32FBA4FFAFB4B7863A9B71BCFBE26AB9839E6C6BF6BEEED4FAAAA5EF0AAEB6541A3A40A75A4F122C9A234F9072BDD5881FEF85E17B5DEAAEB337B41A05C1F7B1F730B61EECB34C211DA48A9868B54480251A28DF12AFA3D2F408C5D893EC624E3616C4B3424EA315EFE11AFAEB7D5E90B64C740E526D8E1190595AB63C79ED6A4673C55A41332932FEBF5A10F57642215D8629D75093D18C3BAFAF0EF3AFEA55A39D6E6F9A7F83966FC5544913ECECB7A0F08782180628D3D5ABDEAAEB378D526ED6036E64CA93E665882D9525DCC4D862456C69842FD169F7390F6253A3B601488393609C00C4268EC4FD00DCB4AA62FF850F3D955F31F72F2B8CDC6B3C075CABA256DBAF9D644E8674BCEB8CA359DE4638B01473455A6A1C44796300A9C2C9ACAF93058B81CC348135B2F0B0C3ABC60B7377B932B8B20DAC146110473C3E54FC9929A04ED27FFA13D934B012F77A89775C27659082511AEF5CF4E84EA9032217809D5A709E1C8700826280EFD2A9B2E090EE902E9BFEA190B909510660A4C2EBCBE23CE86FE1B869E49EAEF86427317BDB113AA519426C8955CF11E125010297E553FD2D491663D0844818601C6066C684767B88EADEE79CA6EE3C89229E68C6BEFF6B0E5DB446B4E67BB227D9C3C8F3954AE8F1DF298D365537ACC47BF11CF4F7CF0BFCFC7FFBE136B11F35EAAFF501A8BA5498EC293C1E3D41FE5D6576676843A91B8099E78756DF974A42C0C202E064F76504B8E46827DD6C6C59A1B4481363EC46E2ECEB130640C947B4BED3B63017CD75A577F23ABFFA653F76F64DDBF69D1A5EE6CFCA6755BE36F54EDBF69D5BE8D56C996F236F4DF74F695F58F8A767E8F5F35CEBE93151D2CD87DD288158C9BBBC464B460F74DA74FCFBFD1FD79D692891AFE6F0C062DC9A8E101868C5FB530416CC13EABE3AA8D410817F6590B17D0C3F1AB4EDC6B11AF8BDA325FD5986833962954C77B1A2DFFE0E2650A35A434A17717ED172DDE9D3D4605C3BAF6A3069EE83B80A7FF388517F4A2B609D81566FCAA87E9AEDE08B398DAAF9A98AA17DA43817DD6C3054AE9F059433EF362151740CB88021D792F96F90A707810051A7B4B36AB8E764A9DC6EDD0E719230C35BC402362A91EFB2B365E69F8AAE383B98BD378C95CD6C3BFEBC9C5C7227A7A4C96E5978C0EA1654B75361BB5588D5AF084DE72D0A5C6986F85986F2D307F1462FE6881F95488F9541B335A15B86CC60B4DF1424CC60B4DF1422CC60B4DF1420CC60BB5D69888595F80C700F918BA4D4F7B39F7BA5821AF23B82DA220F4F7837010385B6A185F0EEFE93820DAFA1F50DB6489DEA953916C6A3656F48A4715F976C54CECAB537D7BD4B5E30EA26BE1C45343B70B0EBDA6FD1CF70D5DA689957521639F3571017311FFAE778784DEEDF7DF66375B02B81C2952167322B0CBF17A59FCB6A196ADFE9B1616D66B367C9CDACBCE999740B1AECF3DE139DD13DD8B4B5463BA9727C52D1E816633E58896E94DB8CB12FD7EFDF5BF5939FB5BCAFFDDDEE7DF22DA61CF3F2C420AA213EA4481F5CBE8FA640E9EFFF979BB77CD7BEADAEBE5C24BA5EF55B255F73ECF5F5B5DF3E17B1567283FC26E6AE6F3E499A74ADB920087FF35835709D269478D44711833305A6F7F3C7F099AB1E4A88D4CE011B133E1BCFB14281699FA12D4D0EC820FC1A50DA8B30A4D95017A9BC69D192A780FD4593E68869A49766805247E246D5030CC66B3FF1CD6424131C6500A3EBD96F44FAC22BEF2120AF060A60AB6DB89984E88693E67579F4D02A76730BFD4D078D2E58EF792FD10DC55E8E92A9974B350B39325B28921A489A468214F32447E24AAA1FA39679CE4ED474D3C5CF1644B75620E7ECDAB9A8174C441FF55F7064FCFEF6E5C184B8607341B59075BE853D441820692AE88C78FA08F64CB8A5D1CD9527DCCBC251C2AF76DBE4FA88647368650BF2D250BB5CB43B01B0BF8E73CFFDA2E1C94931AFBAEA36ACFF25A466320BE0B2B989DB4751D0DB4D6DB2EF18795DDFDCA3E77E53EF104394129B2434C908690C504E1D4F76B113422CC1BD7A150E39289D64347D38B86E7842724253BE10898E4A4A5C6D57B40B1BEE435E2CE93BCA1707632833C49E1E466A466213B22247EB54B4B5530C8DA39EC288781487195B3939FDEE117CAD167E9E0DBD57D81EDDA36F9714B986316ABE395C3B18ACDB14A6F30374FA2C3671E5D91BED437AE798ED0F76587A39923F94A1F2205144DCB426307CFB7E1DC9B73CDF1E65CFF737873907510F2F0C6C1B14DE8031BBFDEE639E4479E58FE5A674948292429DABA4F821F21BA3F44DE8BB573DC7286942596AA935DF3545AAEDDA208440A040AB94B9F3839E77DE4F5E817236311F608D6F623382E36383B75DBB66F149AABE184C1300DB014C3BE09C54EAF648D4805D801E2740C844A5CDD8F44B9792CD45D72DC9D545901B2ED53A42C5456E01CFB3DD9B36D51003B28A0581F77882CC53CDC63A9861A9F5126E089B305F45443A40BE86959E40BE0A3F0337B86B77BE9D73589823D756410397182645C6928598807A7BE27A7ADD3FB50E87FBBFBE12E8C534E7224413EA4A9F378FBDDA960646CD277EFD6B675279554806D2B369CA6AFD7C830EC9B50ECFCB6B5EF9EEF6D6B4FC770DBCAAFFECFB16DDD4995F5DB260D98AC6BA466A1B84448FC48DA3EA4EC9A48CE2EF274E5F7F24C4BC1409E7815FDC890BD6DDDB697B58CF1EF5A567693DBFBAC882326A88229D4C77B95AF92AF090FF158AAA7EFBB069DBE408A9F2854C70BFB764C7C3AE7F1535454E0DC244A66333BC76679D5FE031513AD2FA83CF54C9DD026F4AF57472A86F66058FD3A376BB0ED3DDDA2F1EB6CA4A96E7D76177B7F4F1C2363204FC2DA53AB01FE72E036A2D2D792FD719B50C8DA2F1AC20E3F6669F494E5CE6DCB3F450FB1D7107F8698E5069D8363173CD44DD36954C3C7D98847DFB9BB382A968F674552C5451205D8608B096A6CB26588E6AA73DD9801BD32BD28F20DAC66DB127D8C5F9EEE7318635BA271341DADCBC6814B1E490F5FF5307D4CF307FA310DFCBBD60D9F2AA6DFC71A3EEAAC29699C31F2307ED535C558D9C2BFEB8704B0F8C892D928A346F5C769BC8E7CDF72252999AE5202047EB4CE5D55EF30CF99A7D0B1CF3A6999562CA6E1A3DEBDB4E726D694B99AD67DD6EA9FC6D311FC59546C20ED8A7FD7984539846BFC3A9BD983921D84993D242593DB9C120473DE7893331FC24796CE463E426C9DCDB7CD61B7CC6E44E1BE5EE0A158BBFE6BC88D6DC740BA31D8676D5CAC039F2898DE1DD057FDF0FD292922C07B0194EB28FE3FE28CD6FACD273D393B59334626F659C749F39C2C812733F1EF1AFEC39B93D5AAA8C7F3AE2A122619145DA88EF7B68CFE3D7EB9D93EA4C992444A9668632C9267C65CA18A347026BF67AB284EEBDA1446BC401FDF65AD1C13E4496AA4EDD778C93C462786D4D8BC6CD36A5B00B63D5130B7C5E7AAFEE1396B1C49C97C29E222F064DA1354B9AA97289F6675D881FCF1ED12ECF78603A26074B101AEE847AADADC7BB09D4297E9DA3DAC8CE2DFCDDA08ACB940B1C6E6745B3DD6A3902C1B2DCBB6182AD7F1186FA284B639BA6F3A6EA8CD5394BD009A1C2FD0736B45CBEAFA5BD6BA8C3ED6FB7036890D04624EE322DA24E98B98480F6346A53348F824060033FC67F504E5236F4BCD30230706FB10205D6C86FBEFC9D319F318140860C8957C9B558588313D8019FE9BC73C13B4BE2B36C37D117DE7636E0ACDF07ED830EFF500C5DAB84F569B7AC129AB02BDF9239EB73C507B9A8279CC85B5A32A9AD730A01D3DEE3C07A0EC28F1E73D0466474BA40760404B2E0AF4020C68478FAF2720303B5A3CBDC102D9D1E1EB11084C9BD66992A6B5212AD6242C900D1D81F600A04C298934060D624A83AB25887253EC7CCD400298E21769031AC49843020D408398D2E0CF7A12C0143F6FA6E3C5A6B8F9B39B04D0C67F1F2F1FB37AC3928A67360466474B30BB4138736AA219CE0299D3E1CE720AC29C027FA6D320E63444B39D05B2E09660C6B340E674F8B39E0631A7C19BF92480397EFEECA741F6CDB3FEA5484F9E12EAB8A3FBA685E5362EF36DB18C3F16F9F689C147956A61462E39E4D9A5710EDF4DB09DA6F9030F635B6682F53E7AA0DF9E660A4DF0FEC736A67DB84CA1095E14F4C443DB96E9048334D54E96CB5ACDB7C2B88968FDC30532A773172FF36C25A5848169D3AA75411637D9EDDA033651D7B8B0D654C51D15406B9DD0460F51C92063CE693950333B51B8DB3E0CF1F0FE4F17706AC6270D6224FE4F1D70FAA213081A4EF73402AFCF3B99606174EEDB9CC71B2A20B6FF36C5AD9D3EB6A189FD83C31EBA22C3780A3A1B3653A8112CBDDD3CC4C5F5571480579EA469FE8D0E028121F429DCA0C365210512429F020ADD10532021F4290CB9FF84545828C3F1A89B2B1A8CB6D87024F8B8B162C331E0E3C68A2DB8CFC74F81689C46A2A1AA4BB28C5902A9224D9CA74594AD609C639126CEBE977D36DE12400EC068EC3EE2FFDCD69A648C9644A6D5539BEF98DC8B880035D6898409834F7403EA5DC5FB895645F94A38550C4F20EBC7D2EE99CEE291DB3AE6568ED8BED92BCBC6F5F31E07EB4383C2C1FA38581FFB647D4C7895ED327B4EFCA7F06EA9185E61E355F6B34AB6D40AE8F20555A48B33E6E38CF5717EB9FD443931D107ADFACD2EF985BD60471569E1EC6E48804889329FABF684F32948FA029BB40541D315B8B9C2D43419FD4A22C23ECF46044EB6ABA4BA2FA224AD018AC4AB622569BD18C88314831FA9A8296665043EAB4315E9BB05617FA00EA67180485CF877136C7457C9129D70F17ED0DAB787A080710862BACD8D1B2DD0F20BC24596E862EC7374F09E3CE04399528253E4F160B4A924E532DF162547E0886263DC3779C9EB000D6245A37B6B41811406E982E2495545CB4728D58E6A9DD9AC49DD653B9F4B5147C26009E2D6F4B3F4B05604CF80E01A2379FD95995BE357BDB67C8E58CB16FFAE87AD16CA7AB3F9C2A21B0AF4F07DC8D669523EB2F886023D7C97657EFF2DFF1457159D68872DD5C7FC58C4B108375EAE87FDAF49B6AAB7EE420A10CC6C54401385F34B52D6938FB1F25C860408C8A8040308AB7BB245114D562B609F352C3327AF9FF7C97C4FA9798C7F9FC22EBECDBFD1ACEE3EE985DAE83D46C2C3D4DCE6BD4E57A7B531460707D165FA58D9CCE264893EC68F71BE2EA2A7473A2C122AD7C4FE39FEC6E50256A68F55D866AA5C1F3B87C743C97C9467FF505E80B77A695A16AFD2857E97977A0D8F3D81668A758F1EA0B38619CAC98DE7E411381D0BF980ABEFA06C589BF47E0E7355A59587E5AFC9AAA24CEFEE933A8E5FE264FD4869D9FE9B86037E83E2C3A3E51FEB22DF66B420D085E1E7E34959E6CBA489BA119C712DDA449EF6C7581D22F5A3AA7AEEAF2097D8886D711F15EB184AD5A134191B14D0AC419C1B48EBB7EAAEB9BE60DA2AED06D5BC5E2568148F2ECBCFDB347DFFEA6B9496F43912DCD777C7A014A80B0A4AF7D7F4BA3497111C07944E107A210C1886118DE50834881CC805D6A0E0826A3DB08D37FC53BE5EDC2DEBAD79663EBA0C22F038A5065018620A97255B3B240E469A6E57480D201FE97E2168AEA925F5A0D120C34AD37D19FE2EFB0FDDC5917A331DA7E558EF6EF9186FA2A61BE553B4445ED31AE22229CAAABFF8D182BC3AAADBFE9CACE2A266FB4B59C59B46545EDFFD677A96264DB6BC1EE02ACA92AF71D926047AFFEAED0F6FDEBE3A3A4993A84449ADD2AFAF8EBE6FD2ACFC79B92DAB7C1365595E355D7FFFEAB1AA9E7E3E3E2E1B8AE5EB4DB22CF232FF5ABD5EE69BE368951FD7B87E3C7EF3E6385E6D8EE9EA1D5A252C3FFCD46329CB15712A8CD9AB946890C2F0EEDF6346ACFAD1BD8DBF1EF104E1DD315DF11D204C88FCFB570FE8C8A5EAE6DBC7B81E771446701321275B8600E3A6ADAF8ED0A2811C37C3C2712CA45077A67582B4645635D6AAF95B134F1B88D09F3FB5C89A06EBE3D946A93D9E76EEDAE1B848E274D51AD22D92EC19A5B08F8AFFB689BEFF771C5D55B017F1686C3745FC9CD4CAB6CBC1E600E3E7F89B3B64BF4645D2466ADB327DC14A2D3D3F7F6E9E347EFFEAFF6A2AFE7C74F9DB62A8FBA7A3EBA2D61C3F1FFD70F47F4B5A801BCDC279DBAD567B336BDD09E570C4AA3D607D55DE78B1E49587EB745B55744CF82E0F17AD8CB4A785D6AC501397574757D1F74F71B6AE1EDFBFFACB0FAE661A32936F9BCDF5DE8C1EEA92D1D8B515AD466E6AC96967A211FDBEAA07FD8018BB3FF2A5303B65D393454AB8ED240B85F22C680FA854ED03E5E144D6DE7E0DE9942A83B49D3568B7157D985FACCF67970777BC47A8CDE2BEAAD500770969040AE3ED5FFE6C2436524DF4C64013DD4465F92D2F56BF4428584680FCA79F7E32467E17A59573E4E8FF2E239700F38F7F3142DC67DF728BB94BB6F5C6392F3AC46F9D236E33773946FAF7E4E9BA4051896D2E2DD74DEE7367B91666942D0B5D92924CEE7F31C37C953F34997D1CE366DE05B1F424B5872F5F0A46C5196D705B6CED232D2DBA6D96FCE7364E1AEDFF3541A7589A18874B37BDFA37F07201E9CCCDDD2EE453E0DA8B125E5DDD58E7B900CF93A27923E36581BE0E41970E8692C67FFDF07BFDDBA5D2C06AF83047025FD00B0E28AA55C98A36E9C2BFE50FF749C54E4A27C8B177CE7DA0BF8AB29A3D8547FE93B3A4CB25E8A127009D93141989CD253DEF04417BCE099DE6F9B676061A7A0C954DFCFECEC63E99F95D0C9999A1DF57B6D1A8E3DBEEDA0DE8ABEEC8561238986A4C129AB626122F7B8E2F4F691EADE2958D0120DD6999586297E59007D4BC65ED69B7AD49725183A310D6EBB3DBDFAE3EB9B1E4CAF35A08AA28B5E95DFB069B2EF795E66AD206CBF727E08EC7B6477F1717CF7D089743F428A704F6C89481B319AB6FA3F22ECB33740A99A676F3EBB2BCCC9A7BB44965252EDDAA666DA177789A8CADAE9035A3608BACCB6A6DD19AE11A85E5BEEF2CCFBE22E3917B04AD223ECDD6FCA45EFA374FD5F5B2B0E9D86579812EB926FFB09344E8D2ACD59A46DE8EE523533AF46F838D1BAED9F0AAC37315FD9E5BF1BCC793647678CE1EE3E51FF1EA7A5B9DBE181B312C122B836644E76CCAF40975AC2771FD27CA96DC5F9750DBD3A90858133016435E38436F4E3D4849B6FE143FC7A9D5E4EE105D5671416CEF8D6296EA756F9DC5ABF1AD716D59A35158F9613A5C16A24FA3B06F0ED258D662DA37EB1CC3034D1EB5BD32CA4D3936CE05CE4F28DD5725573E6E4ED5C4D7EFF76723EECA153CF2C6D668A2722DE8874EE1F5AD16951938290673D4C8F73C54B66A029ECF43BF0D636D2B678D70036824B1E24D9F53F75DCF83FD511BF7F913BA94EB60187462695C8C01520FFB330EDE27E7CC5C9956EBF71E0DBB3BBFC006AD10A59B78E72E2193D1926D2982534C047D61DCAF58AD79281F172103335460BDFDB83FE2D25D64B4DDA78267FAA48FDE6D48AF75B8703F94B63B231792EEC8F13AEC6E36CD5B712DA29A5F0F4D2E18A3D5A347E9CC973BE6A108E2CB55F2A00FDE0B1BE61F1CC293388467B8480C47687BB75878767CCCC1FFE3EBE04EDBC4F890C6FBE5D4EC3A3412B2DAD674D86C57EF0ECD988BCE1C9534C4C2F1A526C17546B595FDBB1B97DD6F3D962246912A9A1DFC9B55EDDFDEF2AAABB4FC6F56B56FA355B22D6D309C27F58F6A74739AE0E8924349C6512D74AC4B11E502D779F2CC950B35D63C732543B1BE1A67149129F246055B6DD9B9C5E6AEA317B5C66F137A9DE5696EE27E67718E49C2DCE1FC257174DC5073EFAC51A52E7045DF9DE1A2CD63CB51CD2A6871324676576F546D266783A37A4979CBAD2A0E87429A2363D0B64DA7750BF295C8A3A09696C4558293C60FD0E57272810F8DFB1516D862E62EB98BD378895D9BB216C78F6D36DAF24B96F0DC008A567D2D4EA30A3CB10ACA2350DDBA43F5D11DAA535B5448B5BB61D580C99A5303266B460D986CF88436166E3C71DD4EA5BDD258EF5D9137CEA261FD6E4C1251AB150E8170B9D849757AD49D2244A71A45B2A95956B10B0679DDFBEDBF9ABA48C33924B43D516779812E4825CF71D38296B17BE358988357AA41CC783834DA4122B06F8BA15774A86BDF0277D3179D4E8D3B6D97F184DDE0EF9DAFED7A59FCB671134358A372E62A9AC34CA5FCAB565ADBCD7C6DBDC909EE4EB63C5CEBBD9A711525C0919D7E0F7B4C1E325B8194F66726DA88991BF1E208839D7FCF91FBDD995BC22E60714FBDE333F50DEF8A97D18D53C8A1192476BF68ECFACE936747C770FCCD4FA01D9A8B68AE8E2B876D918EB1A5E364986EF103671F95E0CD912173BB4DE36E9105DF60DA65591ADEE8B062A58FE16171F66FEF39458AEE9D5EA4F93734C8EC9D510DB18611ED7C44CC0CC3B0FAABC238A7F76746AA06865061B73AB27E57A127CD9C483B8DCAE77893B4F667C49B7E7DCE6DB65B0D0A55C979A32F3997E5AF7995646B9B38DA765CFBC1EC1E7231B320604C3E640F24B53FA23776ACAC0CF53C8DC26AB5E99159AD7C2C12BBBB4EC17D9BDA9AB1E5FDFE88A5C735F0739E7F6D1712BB6087B3BC16AEB8D2B9D761B6E0EDCFA852EB9CA12B5C6BA9331010174BDD41AD5ACBFE094AFBB93FB24F4C692BF67718ACE4017ED3C1F900EED525F0B63FFEC2F609C1B792900E83CFD98936F7FB36C0A4E0DA8E807D3E1C6ADB6BAD334AFFDE98FD1106CF5E98A99602039FF7FE8CE93FBDAFBBB7DC9AC7402DCD70C2096B23C42D82839FDC9F66DEC3EBCAFE3C9D9FF3EB9DF10FEC9F2B7CFA8DEA3CB5CB0EF8209B5DC71E9ECECCEBD06CA7169E7197B89F6231DAE0B652016072B2779C662FAB1DA7751FEDD1999A5A5CE04C971E93A1DBAF347B3B3D7CAACB831B1940B8F76F7331E54B512E72CEED80D2D8BB0CB17DC7CEB645617BF768C0E164D7C2CFEFA7D1A411899336B52902AD5AD4A3B009629E36B19FF65DC68B225AEFD765C6E165B5A5A3E42AF3DD251357E9F76704E770EF13FDAF7346E3E87A81C9738BFA3914F473531EB65233915493A13B6CA566337CE1B752FBF7EEF0AE6FA5764069FCB649FF59935CA8E252BAF2BDEBF2D03E92BD3F3220B7EA4C5E1E6E98A4678181565C93C3F1AC8823EC80D738D3798FEF2A5FA18729850855B6414879766D3B7D3156BF18062B3D6CB1D977B1C93F8F9FA2A2329FB458750F395246F4FB3E7315788D7E1978FCE64F4797E559BA45D1EFF1EAE7A30B441A7DFBD23CE3FAF3D17DCD75340E5A5B322D7B6CDF54EAAE5B63ED7818F5A0AFEA63E5C5DE8BDF1F59912EBF6F3CE6BC37C2ED7851FEB84D0624ECCBD1CE3DA12E5E099A612408BEADFD143DC47B144D3C078F44C3525F2E89BE8D77319AA2674552332C89F66700BDEC30A65C637B157851E41BDB3D448FEBCBD37D6E8BEB3E5A978D635383D9AA783FA6F943C4B80A6C319FE559856C72C768D11BC899A6D4A9E54A47D68DAE3CEBDC18B0C7ADB764C469BC8EF6EB7AD95D55EFF4CEB1D7188D4D930FD9CA099ED62247074616CE10DD14BE90F4161B63B53956B67112DCE7C60DE8AB7A700EA05BBAFB3813A65C2349ED62DC0A1C85973DE5BEED27A71CF3FB7AD1350E376AAB5AD1176F2575F56D2B19636F9C05277498E7EA1DFFF0FD292922DC77608CF23EFF23167B204CF8D7DC7658CBCC46A3B091F83959DABF677C73B25A15F520DF550596DFC4592B6FCBA8D63337DB873459BA396EEB3016C93366EE58A24C7ECF56519CD678DD22BCCC922A413E9C46447F8D9740BA5BA3A0B6B36D5A6D0BB905EED0F9DD6982ABFAC77E252022FA653B9F184DACB1AE0C75DDE7C05510328D786DB4FCED8F00B4C99478F6809159612B457893C075C948699C6CAB47C4A3652380D64B7A2DCD8978C1FC51FFA635BA211D652F52C566F2BA2E72E044CBEAFA5BD6FA453ED61B5D49220F332F114EE422DA2429B3ACB8A4D22DE03AEB802E89B3665EF9EB02F217387753E104FE9E3C9D61B9F45DE5052078946FB3AAF0CAA69BC73CF3DA878BE8BB4FF41F3628758C98417F362470B2DA24595256057A52C0F7EC6688799DE50435BFB39D20E571D613747CCE7E82905F2D40F2CEAB362048F9D40A04217FDA8120E3534B9C26695A6F737DEB078C8C57CDD0D1F1AB133A221EB54147C1A71EE848F8D5003DA7BCCEFD8E88CF59DF91F037DF3B023E67FA7DBC7CCCEAAD4EEA7BAE1384BCCEF68192DFF93E90F138E3071A3EE7FC40C4EFAC1FF9E575DE0F647CCEFC8188BFB93F90F032FBB5FCCD1A0815DDCD4A91F8457AF294B842751B97F9B658C61F8B7CFBE40A2972B12127AF5B74A769FEE018E53DAAE418E77F6C63474FF88D3851908F13941DBE93E5B25E005AC1DC44ACCE7182FB2E5EE6D9CA31F65A0B6471933DAA3D71F2D0019A84DB7EA0EBDA0F51C950F1EF72BFDB3E8CA1D27BE97EC77BE8C8158FA3B43E2C2DCFE30DF65485090607F74FBBB3EF2676CCED49FA98DFD418DFE7EDE6212EAEBFA218ACF2244DF36FE371BFD19BC11DBE1B941DD1213E7430EF125F1F14E91267C3C3BAA1EE18E80A59C33D57C806D6D9226C588F6EDE64964FB534884E8B285BB940D4F7AF4F9E58DA60BC8DFF735B4FD431D60C2DD74F6D1E4D0BB4F7C978E0661E4233714C9578F950398127107889E1DBE335DCE9EABD87EBB6C6A3F587A55680EFB0D41E965AF74BADD65D94CBEC39D9A7F4A66D7F0A8364463C5CB1135C5F6E3FB9F1C4DC7E6A36382F4EEEC8D4D8BA406627E85C2C2E88DB0BE358FA8597AB2A08F3A7FDBADF3BE58D8586950A81718E46EF64BB4AAAFB224AD20FCDA9C6DE8C61DDA7AC8C3839F0B57D81B1939DD3C87D9B4579C462AD78C7D16F53FD5B478B2A667930BD6331D5A46C596EDC02BCBACD5DC5164F7F399F9FDED9062B2F679311CEA45CE6DBA2140AAA01BA9BBC74DD4284B24BE6EC0FF3495545CB47387384136DDE5D18D91F2D0E2F84F3482805ACDDF932C29FC433DAC3D5FF7F8E7043D1D27D50FF5FCB62BD5F7274EB09FDFF215BA749F9E80EE16599DF7FCB3FC5485EDC627D2CE2D835DEBF26D9AADE755AE2563F1744287E496A99DF2B030DC14A0DDD1F1D3D9F69B49FECB3329E3A3B53766347DEE6DFD84BC0C6C7DCB8D16785ACB993769DAE4E6BABC4D1317C8F52298FAB0EC28F71BE2EA2A74747F1430DDACFF137D75DAF517A6AA92147B5DF7BD9BFE7DFA8976CACDE321970D83D83D0F87D25917C8EC7F566AFAE07071D53A5B768E51E20ED9551E7684AA98D84D8993E269DACAAC1902C6294D04B13C32F71B27EACAC505C6E50E062B4FC635DE4DB6C18FC9A390F8D4277A918516A1FD3B71E50DD053B8340D8C157AC3BDB0622DA33A0AB699DA5C78B8B5BC3023F29CB7C993474FAA9D37947167551B2CE9049088CE1876C758478D0F6A2EFC25D9C7E7DDD7EB8AAF7E7C9539A2C6BA2EF5FFDF0FAF51B861D238EE14D081CCFF891C4F53F1844B524C4C89593A090F50CDDBAAA19C98A4D922D93A728C5DB4D0129AA71C4CE011D5D721E3FC51952CE74DF5468913C87290F0428619771E1DD3136D89A32D0E6FF3AC840481918D3B54D2B03678FF1F28F7875BDAD5C6882830828D0A2593E9D10E0AEDDC5F017570446086208C7AF1AC2809386D1B5255EC482D3554FB2417448852071DC30BD6474FB80C5E8F52FE5228255E40FEF00602838CC718494140EEA57B4F0FE859632B6B32A2DA08E8EE62378D369265309DE793DA523B9335557ACBAB55F7F764E7094D79CA984A73F029F566E16A0D65458E6D4F48D6C73B42B6BDA8E2C643352488D2F679AF58BD9B051257B242ACADBBCB94986E71DF7A4A2106CEFAD2D05936EBAFBD395809E979E248164FCB8D3A33F7463A73C2F831020E1DD367974D172D7BB91F88687D650EEB8D9A135B4D35B1D43A0C0D43EDD41B860069A4A938A6F1744C816EEBEB8D05DDA0901E91BD03DB63DBCE076961745DCDCE9690E239BC81AB90AEA9FEC86646728D31721A831A038C1805E450B7CA5DCAF8481BD54A28FC0BB064FAE926889E36F84740E8954C4099252A6CCCF5648E74CC891B4E80828FEF8E32C844338CBC229A0E924664235A343926AE65C96B3F3B88A92743191F4808D1121EF21F64592E0EEA9109E5A8E247A08EA89FB81578C9F998D72D218DFE95554DBD8B928AA5E454D6367535559FB1A06D85335A56E4F4FADA568210AB7B9B710C41DDCDA9BC8E4E43BFB3E81CCED361D2EAA2CD01F9DE6EB3EF1950D8480186818405D88D8C610E8A1622FA204F6238058011D54A18A9A7B41357772296B6F262EB03F73FEE9050905895457A22E4B643B7838FD4557083AEE4778A85EA90ACED0C6B9494CF3CC68F349AE91B407594572C6068088F162DF1214283E10E89AA6184D19164848F3204677157B778F1DEB160A1AE6AEC448F1E8E832C7420374DBAFCCA8EB39A2899349CBB8D606B49247A21CC3660F6C63AC333B61166382E037C262AAC10F1565A139F093C65660838E5BCF4A5B1FEE966717848168EFFC8482D8794D6D48A0464C6F476889DCAE5B11CA62390F23A26FC5E04E09674AD0A44111F1EC27096956305DDA09E3821190FE8354501840687C775570C45C989100D10D9D5C901A7D372329D25CF0F6407ED4D7C4990B8F8E83CD835D3317F75A701BC7D0BBD6B4B39C5C86DA775148B3582E3F6D2D70A0FBA25DD33D5DBB67AD79DA364E26331F9A844363639E9E926CBD1031D0B7E8802D22707320F64494E0DEED85485D8C3A527A92ED480C9495542934B669102FB2663EF22EB557A9657D836D9EA1E84D6B8ACF53A78534CDEDB5DA6C4CF4E618AD24DC053A8ACD99D6513EE11413A141FC5BF27A1AC6E5E1A71E6578642797BF7173C2B8ACA03D4BF8580CA1B841607B1B9B61E4C89A498C0629F5839A633B3713553757F99B4EE559CA617B6E3317AD4784C4C29A791E7268157ABB37B2A71D973BB3C576B87CAF137DED210869DA28EB69829276EBFC9049D3701FAD038808460DC4D57CDF79A1C07BB353D25037384066288A1A4F10F6203B14DD9B9D128605D5FAB0EA6122C998484DECB07404120D4406444616ECBC5010DDD92989E853DFF67F9F6D8B82B9C3E8536B40696F99B29D1710BA473A32D20DC9EC44A57D2E3748DEB983B0A80ACBF886F13C169AAE0F01CD90838CC864047F3D7B5A29392431B51DFFF95FAE19DE0DD24D5CAA95D86DC7139706CDFCE6205DFA5C12C11926A4749E33708E2928A7C826689C7B727E127551446B870A4A458A7A9220C2B1706FA465E8D24E494823D913E4256DE882D8BA92BD918BB63F3B2514833C088F649CCB047D24437CDF1B79503D92998D34843992A1A8F104610F8E64E8DEEC94302CA8D687550F1349C6446A6297A5A3DBD187140FC84112E8ED85490444C769323F0909A43C1019101959B0F3524174673725E2B74DDA251D0DA83546A2204ABC78E76504E8D46E498A5AE26897AED639658B0E292AC669A267242CD0CDA9009A05A43BD99DB5904203F76B37A4267E8A8AAA919B8B3C5D094D9301941415ECB3968A69E911B8FA4F7E24626C680099E8BAA2240443C3E6200612FBD4B11028B94F765200D437B1D30F3F6A6B2BB09E9D5D2321068DD7B91FCAC585754485DAA447FADD80B73FC4B35E5955CF71EC35C623ECE8B7A0538FBF24005079E895567B543FDC8A1F6EE4B1DEA9509B38BE0F53F567455CEF7656A72FFE747E683B2F94AED7D4F303A7A7DF21422D77ADEDB5B6953B37DFB5769713EBF9BB6594DDC54DC89E67EB0EA344E021BEEFF49CC77B327B036F98EFDC56DB8D9DE2D385213580EEF884540358DBA61709B749AD67280921C3F8B5C460F24B63830C84896B0939E8A1235976680DC0CF493F450F711AF80CBAA1C93D88EE4A77FE2480ED93920770F253805E4AFAA42AF2D425CE84640EB9EB438A8851CEFAC925A4098424EE9CF045C37EE9C0E9F0AFF5ECFA2242F465F62B492B014617759CD91273BCA013DAE630BE9CD380CFC014B98BA362F9785624555C2451202B94240ACA0C0DB217D242756A27B4CC799CC6EBE6E3E2A2C837F602A270063DD2646D54AC68A76582EA8C9AEBB2D84CFBA85F8E92BC0DF2E0F93547821899418F2ADA6941A03A337BA50008C1F8A7D8EC309AD7739711CDB19B465AC62A73594CEEF3C35232DD52D2727FDAED8B4E6E56FBD56426395983AD2A66F95827958AFE5CB4FB7955FF10BE7D059E90C1A763C28352821C846D28F2734C16EE888CEA8F0AC5A90FC9EEB60FE5B2489EDA4523CEA27A1B857D12480706450E2A51A093FB88264E1DC7B1C57EE40527114068808E29090E5661C2B458A8ED5A72D3C20243BB8BB2D2B57CBE52D2569B5A3E64B95B1D4844C8D8FA70A3AE91AF77D271EE4D0B8833AE475AC32CD9B9F1D6B1556632E49EA3EC820F76B0E8BA5D89AC1BD2C2F46E768FDBC8292FF2077527EC4C4C4D433C7B4E9CA43E557028B5B4184CFDE79D1FF9AE23AAE3BE9874E09BA89E00B7E4E078299F715221875C3D366AF299DE8CB53CAD8F93419FEA95859043BF3B398F9BD1EF23E2C8E7403DCAC1DC5E620D251B16EFAE4E2E25A80701D604F41B83A4FDB8D3433F7463F6833D0C34FA85AF0498818206696EA3AD3C0221471B014E3FDAE5E22EDF16820743AD071CFDCFE888F1E34E0FF8D00DD5019FCEC2C746FC3E2AD631DF6FE724D828F488870C3052A635ED9E0E35F536593F560BCF2A7D20C460E9BEEEFC1C6FFBB1135ABD1DF1BB6511C7A22BB14D31791BB6FD34F761877A3687816FDB35D9D09F6EAB2ACF16439BF9D3BD8524C6ACFFA4E5BF9B66F8BBA6CE6EF8DB764D3DFC21A6BDB2FCECDC84D790AD89677B4B5E96E04879B82533BDADC424388271EDDCA863BD53A1367182A393ED2AA93EE5EB2053BD2746A0193FEEF8C00F1D519FF0D319F34D63EF6B04E987AC2A5EBA54B8DE0FE928B2AC20E0653BBDB1A37BA342721C84D9C8855F57ED411C442427158421F94A77BC40BF702B4D8763F02EAD4A9A75F098872DF49A2947E7115A4759D6B54E79A8764E2E4337D13A88FC2866EABFA16F129005FB263A37AAB709A6119B76D1ABEB54758DB8E8DA7296AFE28BA428ABF3A88A1EA2923D5040B5EEE28AB2635F1DB525A08179B77C8C37D1FB57AB87BC1EFFE881B44919B92129F4E62E83BF2F80B0F76512DCFD8697C1DD1740B8FB32096ECC83C2A0C7CA200A58B102110E7E3E6A29D6F1188FC13C1641D8C752050A1CEC7CCC52AC63AC2183792C82B08FA5320A92479B59BA920A606B84754AF5468ADA2321AD41A54D7423EEB9424735290E4B9112CBD519ADD10C8E1053E542C2ED1D0B19C531B68A2136164174C652C59923A0C4828866923265EA391B41170708514F0720C50EC36978B89D87C1458C80F310A9368FCF150642D40855AE505C6CEFCACA87A4875318981E54AF21BC6181C1A0669010B25515B8F8CCAEB10010B8E2027012FA7DA019AA2A68010C06B50186546CC55D852E1C2BB4830614B5848695B4A5FD9B799789690A070E94081854912957515981DA9F061031A187D1190819FF15D8AE4CB009741410ECCA45043B107582F09A4E034848260AF6790F8E9660295D1C48441B87D398E492C92D9FD41A2A4DA8CA642A4CB9570243820511F54ED9906013717229ABA9286D4549073B8BE78DA2A2A461B5A55AA13D10B09A942BB7AB1FC6E6C178EE3ADE948AAC8806409D14C7426720242495369FE42508706B2B9678F216876A2FF9FB200642D4CB0148D130BD28A2B5C4321D4144A6690FA56E133776B4806E572E22DA81A86E8A4099254A855B210599651E4B179113EC2B6920059915384DC8629ECCAA3B4F80977BB9FDC481445DC5E124F4FB2762189A7D0144A72F93F50D7B5E90ED135608F6052B5718316E3FF042DE6829F687785483214494C2FE570C404308BBDB834249EC6064E2D8A59057154A3AB72A573069409170D2B02A9CC012B0C16CC000B83CC060143CC8429A3400CFABAC4153205842A15214283A41158FCA00202036E4DB92AFC76D4A0C60196E0BE0D5B72D53C24D66C6E1D02181F83449381943C594E534B5A8E137C7C119D017F2A4BF2D970F19765D19A42351345DB18408134EC1906220B8C765389084ECD936ADB605C4C1A104223314CA241281FF9294F5DE04EA12590C4A2101A168EB0AB6322C88C8DAD5DEB6DCC08A842C1651BC51F1BB8C776B402DCCB7FBFA5256E6B1C35F76CD5BD4A5C93A434FFEB5B831686AD5A341E9F3693A3869E81A7C7447D4A02DD0A616D7AA3C26BB64D2DD2ECDAA4A77A18CACBBD2DDB3C778F947BCBADE560AE3CB00EF5497F183BC0576C6097498032A683C7D9CDAB67EFC2AEA3874484AD66F4BDCB2A0DBCD2F885363092FA03A8A1DA37C106CFF38DE052946F6689D8B1B07F5C34C75B1626B04902ED34170C59E568E1539033D28633F6DE6C59C052F7C43C0235E9D70F233F9946B2EDBAACD350AD4339398658F2A71CC02EE8ACD42395DAA8375B7378D95EC143EB0CBCED3FEFBA616F704CAA2CB8897DBC6C180E6D398A34FD4735E1D47DD99AF66A0428AC6976105F62C0BEC854D9CB029C2BEF5203F9DC75BF2D8994098D410C85946F9F109CE71DCF2200345615804336140673246F34524620CACA3FD0F581362B42CBACB9E0DCC50CAB94157092743615804C6B32D040C1257F0CF1E614C1F848D13E9E15C9A7A3ADA32D555F4DEE5F948592F2EEA8A9DAC105CCA58050E03386794C836E0C2FA300D2C186BC00628C0137C4B08608B725D7E674501AB4D8F550250098CFC10DC069F4250AD3913DBD0A60515C2C8671C082F6716115D4970090C9AE4E1A0DD1A923048676C1983C0845225A8E5A17B201636C293C02488D9B462565E0CBDEEC26E855C62C0E51D230282893E8171BE2EA4D08025E35C156A6708CC8762662361298DE2B6CB3C87060DE2D28D11B88B7D02E2361059D85502546D3DE1AE2393B3006F8CFA5407A0C3CC742D665AB083C95C20E2060BEC63CEF362C3093678B02486DEB1D1EC0A2C612AC93B08B248164A3F0B563572ACC5274E8D904CD29C96CED8A3687BF12B85D13853185EED9D2A52C10A99045790778EBC0146740CBED7359104B5A8A97B6E0BBAA92C7F94EAF96613D808021907C223DB2E98EB8BA07CE921F0CE02BEF43137965831E4DF3F722D8F22C5AF5833C402309D5CC2F7C274045313430029E26F4CC5582577E3CC99CB5E70C3BD3BCD82C5E7ABBC7278C787908D0AD7F92C0D1756CA589A724B460DCB54F23A0766132E5C78422B7B8039D5A762AF956BD9265443D18B2E84F7EBC70AE72167A22AEEA3B5981B04A0CF7316EC92265119BA7D69DEF1EE6225D7AB07C2F98850A22E8AD25D761DA245A1561EF316D8F3B84FC108752EF8630174E37C90220F51477DF06EFFF7D9B62824410FBC2A3E25028AF5955D7377C7942E97BB064FA0ECEF3BCD123AE5A8DA3C1980F78811D268D69D8B601DEE0B6944B04AEBC8A36E0C02E76618C1AA1FAFA958D30B0367169A39B000CB18A2C0B011DA2793E874280486B1D019331A0E2B45A952903E9940246621AAC3F9561CC8026FFF0103FAEC3CBDFF10657FB1888197EC3F403897EB0B27510DDD655737628061E45ADE7C60CFE33E0923BA255D911303B4575604BA05C1744F5D24FCC903944A6998523E6E77B30990D45880C3FB940636CB13814390B7C98229D238690EA40F464C15123D76113A82927005AEE2833D20A920476863BAAD459B0D0BE6090B25600393FEAB650437B117519B4CE8D5D4E4A4E9B2EA2C5F4DD230BE3AAA646F1874724C6CC6B58C68109746119B946DA8E76C1CBBB6B73FB82309403910BBA93ACBF7A59100AEBBD88220427EE624268767455CAF00ABD317412F49389782EB55F1F46B4A8F51B0EC7420AEC7D177DA192CCB2157EF30302EC70FC8D2D85414A55FB4184902AD60387138A78D0F3ABA437754EE9B87B967EEBDABD23451BB971A0A6F79FF479F1B54D2510ADCF7B691C845C8EC1DE17483160CE9033C84610D02681FEC081D7D4E6690E99E21828D471ACCE924089626ACED89EE3184422D1F3A612E470F641A5FA99284C07DB0074E594C30469280D84C7EC64CBF8B8B22DF88765C10A8EB9D179BBD78D49DFC9CC40637DE723CC931FF621F00E6B2CB247E32E62B17A66176D2E5F14F5053486A7818C9C99843C9373FA9280CB8ABD3A0590B148322B9B04E27C4048190FD8E8EC9120E6C0039A0828D11B42952D810C179CFF1EA43913D03B08CDE0B36A538CC09591D41D7800CE46DC70499C5A9CD172F877AB70D936645378903424855D9238096750BE8CA5C9921089CC4CA5D77D897E3B99FDC7D13F8D3BF8370DD310D5D61D13DAE73CB9B63CB6BB786D00861D81A00E57CCD0E70608CD0B52F2208CF467010D7DD245F7318EAF59F9D74B1718F087B8841B8EE20ECB971E5B119045118F40140B9EE668860F3A61BBDB389BCE0C7EF360FDE2503A6BC97881A875A2194EF11C0F5B8A3DF985AED47275BCC458F8FBBA5C420C49B3F66CB378FCE958BBB7C5BC0B7EF5820975D44FF33A33E7E74D9C5FBA858C7A011C402B9F688F8EEE26DB27EAC164219C5415C8F60839BA9D67D75D8C1BB6511C7E0E11E03233AD86B9110677AEDA749BB79BAADAADA581D508223C902F19BDCC212EDED3FCDA1A3FCC124015C8FA4325B8C8EDC117941640809E0BA6B2D08131902D734E85EF3CCD7A77C2D183B1AC475177BFC44BDF1A39B2E8E2F997521873CC34600ED7209E1BCD236F65DF0F49A3D0B543BBFE3DDEE77094CCEB7E1E61DE41592D41139548CEFFC817840939F2D74C626F4649C0E8B40F870ECB9A17DCE6481325BDE1DB738CEF2AC8A922C2E86B277C7EDDB77DD87FACF7A9B5523BFCA57715A365FDF1DDF6EEBDA9BB8FDEB3C6E5E71EB51BCAB716671937C6344DAC35C665FF39B227F8A8BA6FD788B7A90BEB81BA8ABB88A5651159D1455F2355A5675F112F9A85090727304FDFED587CD43BCBACCAEB7D5D3B6AABB1C6F1E52622ABD3B16D37F77CCB4F9DD75EB1576D185BA9949DD85F83A3BDD26E96A68F74594D2CFE2F0509CD5DCFF18D7DFDBB1ACEA9FF1FA65C0F439CF141175EC3B8F9FE26C15639756AEB3BBE83936695BAD003FC5EB68F9527F7F4E9A70411E12F940906C7F779E44EB22DA941D8EB17EFD672DC3ABCDF7FFFDFF038DD22013F2F10600, N'6.1.3-40302')
