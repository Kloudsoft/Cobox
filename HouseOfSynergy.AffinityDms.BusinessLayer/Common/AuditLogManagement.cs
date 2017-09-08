using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;

namespace HouseOfSynergy.AffinityDms.BusinessLayer
{
	public static class AuditLogManagement
	{
		//public static void CreateLog<T> (Tenant tenant, IEntity<T> entity, IEntity<T> entityType, int actualUserId, int activeUserId, int screenId, bool IsIndexAction)
		//	where T: IEntity<T>
		//{
		//	IEntity<T> previousEntity = null;

		//	//For Tenant Table
		//	//Get Previous Values
		//	if (entity.Id > 0)
		//	{

		//		//Tenants Type
		//		using (var context = new ContextTenant(tenant.DatabaseConnectionString))
		//		{
		//			previousEntity = context.Tenants.Where(u => u.Id == entity.Id).FirstOrDefault();
		//			if (previousEntity != null)
		//			{
		//				PropertyInfo [] previousProperties = previousEntity.GetType().GetProperties();
		//				PropertyInfo [] currentProperties = entity.GetType().GetProperties();
		//				List<AuditLog> auditLogs = new List<AuditLog>();


		//				for (int i = 0; i < previousProperties.Length; i++)
		//				{
		//					PropertyInfo previousProp = previousProperties [i];
		//					PropertyInfo currentProp = currentProperties [i];

		//					AuditLog log = new AuditLog();

		//					log.ActiveUserId = activeUserId;
		//					log.ActualUserId = actualUserId;
		//					log.ScreenId = screenId;
		//					log.FieldName = previousProp.Name;

		//					log.PreviousValue = previousProp.GetValue(previousProperties [i]).ToString();
		//					log.NewValue = currentProp.GetValue(currentProperties [i]).ToString();

		//					var type = Nullable.GetUnderlyingType(previousProp.PropertyType) ?? previousProp.PropertyType;

		//					if (type == typeof(int))
		//					{
		//						log.Variance = Convert.ToInt32(log.NewValue) - Convert.ToInt32(log.PreviousValue);
		//					}
		//					auditLogs.Add(log);
		//				}

		//				context.AuditLogs.AddRange(auditLogs);
		//			}
		//		}

		//	}
		//	else
		//	{
		//		using (var context = new ContextTenant(tenant.DatabaseConnectionString))
		//		{
		//			PropertyInfo [] currentProperties = entity.GetType().GetProperties();
		//			List<AuditLog> auditLogs = new List<AuditLog>();

		//			if (IsIndexAction)
		//			{
		//				AuditLog log = new AuditLog();

		//				log.ActiveUserId = activeUserId;
		//				log.ActualUserId = actualUserId;
		//				log.ScreenId = screenId;
		//				log.FieldName = null;
		//				log.PreviousValue = null;
		//				log.NewValue = null;
		//				auditLogs.Add(log);
		//				context.AuditLogs.AddRange(auditLogs);
		//			}
		//			else
		//			{

		//				for (int i = 0; i < currentProperties.Length; i++)
		//				{
		//					PropertyInfo currentProp = currentProperties [i];

		//					AuditLog log = new AuditLog();

		//					log.ActiveUserId = activeUserId;
		//					log.ActualUserId = actualUserId;
		//					log.ScreenId = screenId;
		//					log.FieldName = currentProp.Name;

		//					log.PreviousValue = null;
		//					log.NewValue = currentProp.GetValue(currentProperties [i]).ToString();

		//					var type = Nullable.GetUnderlyingType(currentProp.PropertyType) ?? currentProp.PropertyType;

		//					if (type == typeof(int))
		//					{
		//						log.Variance = Convert.ToInt32(log.NewValue) - Convert.ToInt32(log.PreviousValue);
		//					}
		//					auditLogs.Add(log);
		//				}

		//				context.AuditLogs.AddRange(auditLogs);
		//			}
		//		}
		//	}
		//}
	}
}