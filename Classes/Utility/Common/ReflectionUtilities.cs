using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Web;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
	public static class ReflectionUtilities
	{
		private static readonly Type TypeTenantActionType = typeof(TenantActionType);
		private static readonly Type TypeMasterActionType = typeof(MasterActionType);
		private static readonly Type TypeTenantMvcActionAuthorizeAttribute = typeof(TenantMvcActionAuthorizeAttribute);
		private static readonly Type TypeMasterMvcActionAuthorizeAttribute = typeof(MasterMvcActionAuthorizeAttribute);

		public static bool Validate (MethodBase methodBase, MasterUser user)
		{
			int i;
			var memberInfo = methodBase as MemberInfo;
			var masterActionTypes = new List<MasterActionType>();
			var attribute = memberInfo.GetCustomAttribute(typeof(MasterMvcActionAuthorizeAttribute), false);

			if (attribute != null)
			{
				var customAttributeDataCollection = memberInfo.GetCustomAttributesData();

				foreach (var customAttributeData in customAttributeDataCollection)
				{
					if (customAttributeData.AttributeType == typeof(MasterMvcActionAuthorizeAttribute))
					{
						foreach (var constructorArguments in customAttributeData.ConstructorArguments)
						{
							var customAttributeTypedArguments = constructorArguments.Value as ReadOnlyCollection<CustomAttributeTypedArgument>;

							foreach (var customAttributeTypedArgument in customAttributeTypedArguments)
							{
								if (customAttributeTypedArgument.Value.GetType() == ReflectionUtilities.TypeMasterActionType.GetEnumUnderlyingType())
								{
									masterActionTypes.Add((MasterActionType) customAttributeTypedArgument.Value);
								}
							}
						}
					}
				}

				foreach (var masterActionType in masterActionTypes)
				{
					if (Enum.IsDefined(ReflectionUtilities.TypeMasterActionType, masterActionType))
					{
						throw (new Exception("The arguments "));
					}
				}
			}

			return (MasterUserManagement.IsUserActionAllowed(user, masterActionTypes));
		}

		public static bool Validate (MethodBase methodBase, User user)
		{
			var memberInfo = methodBase as MemberInfo;
			var tenantActionTypes = new List<TenantActionType>();
			var attribute = memberInfo.GetCustomAttribute(typeof(TenantMvcActionAuthorizeAttribute), false);

			if (attribute != null)
			{
				var customAttributeDataCollection = memberInfo.GetCustomAttributesData();

				foreach (var customAttributeData in customAttributeDataCollection)
				{
					if (customAttributeData.AttributeType == typeof(TenantMvcActionAuthorizeAttribute))
					{
						foreach (var constructorArguments in customAttributeData.ConstructorArguments)
						{
							var customAttributeTypedArguments = constructorArguments.Value as ReadOnlyCollection<CustomAttributeTypedArgument>;

							foreach (var customAttributeTypedArgument in customAttributeTypedArguments)
							{
								if (customAttributeTypedArgument.Value.GetType() == ReflectionUtilities.TypeTenantActionType.GetEnumUnderlyingType())
								{
									tenantActionTypes.Add((TenantActionType) customAttributeTypedArgument.Value);
								}
							}
						}
					}
				}

				foreach (var tenantActionType in tenantActionTypes)
				{
					if (Enum.IsDefined(ReflectionUtilities.TypeTenantActionType, tenantActionType))
					{
						throw (new Exception("The arguments "));
					}
				}
			}

			return (TenantUserManagement.IsUserActionAllowed(user, tenantActionTypes));
		}
	}
}