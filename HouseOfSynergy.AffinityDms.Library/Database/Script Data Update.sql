USE [AffinityDmsMaster]

UPDATE
	Tenant
SET
	MasterTenantId = 1,
	MasterTenantToken = N'{90a0a900-6e0c-4161-a71f-ee40883626a9}',
	AuthenticationType = 3,
	Domain = 'kloud-soft.com',
	CompanyName = 'Kloud-Soft Private Limited',
	UrlApi = 'http://affinity-ecm-tenantportal.azurewebsites.net/',
	UrlResourceGroup = 'kloudsoft-rg-affinity',
	UrlStorage = 'kloudsoftstorage',
	UrlStorageBlob = 'https://kloudsoftstorage.blob.core.windows.net/',
	UrlStorageTable = 'https://kloudsoftstorage.table.core.windows.net/',
	UrlStorageQueue = 'https://kloudsoftstorage.queue.core.windows.net/',
	UrlStorageFile = 'https://kloudsoftstorage.file.core.windows.net/',
	StorageAccessKeyPrimary = 'r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==',
	StorageConnectionStringPrimary = 'DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==;',
	StorageAccessKeySecondary = 'Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==',
	StorageConnectionStringSecondary = 'DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==;',
	DatabaseConnectionString = 'Data Source=MONSTER\MSSQLEXPRESS2014;Initial Catalog=AffinityDmsTenant_0000000000000000001;Integrated Security=True;MultipleActiveResultSets=True'
WHERE
	Tenant.Id = 1

UPDATE
	Tenant
SET
	MasterTenantId = 2,
	MasterTenantToken = N'{1ec390bd-02a9-40fb-89db-c7582163b0f2}',
	AuthenticationType = 3,
	Domain = 'cobox.com',
	CompanyName = 'Cobox',
	UrlApi = 'http://cobox-demo.azurewebsites.net/',
	UrlResourceGroup = 'affinity-ecm',
	UrlStorage = 'affinityecmstorage',
	UrlStorageBlob = 'https://affinityecmstorage.blob.core.windows.net/',
	UrlStorageTable = 'https://affinityecmstorage.table.core.windows.net/',
	UrlStorageQueue = 'https://affinityecmstorage.queue.core.windows.net/',
	UrlStorageFile = 'https://affinityecmstorage.file.core.windows.net/',
	StorageAccessKeyPrimary = '5xuKR9BvlL2xS251eAI0cuPwYjkt2Mhru2xCqx+KiEcPsqXZgmILe6N0FPHuI9g8lNeeyRkOJcDrrhFGsOab/g==',
	StorageConnectionStringPrimary = 'DefaultEndpointsProtocol=https;AccountName=coboxstorage;AccountKey=5xuKR9BvlL2xS251eAI0cuPwYjkt2Mhru2xCqx+KiEcPsqXZgmILe6N0FPHuI9g8lNeeyRkOJcDrrhFGsOab/g==;',
	StorageAccessKeySecondary = 'BQFfUpo/G7BudTk6nac0b4I7YjhcBf/ygfRat/QY7ndis7y4dcxVt2guDV0s816/o9Qu6oN7IfKPjMnfOLSAcg==',
	StorageConnectionStringSecondary = 'DefaultEndpointsProtocol=https;AccountName=coboxstorage;AccountKey=BQFfUpo/G7BudTk6nac0b4I7YjhcBf/ygfRat/QY7ndis7y4dcxVt2guDV0s816/o9Qu6oN7IfKPjMnfOLSAcg==;',
	DatabaseConnectionString = 'Data Source=MONSTER\MSSQLEXPRESS2014;Initial Catalog=AffinityDmsTenant_0000000000000000002;Integrated Security=True;MultipleActiveResultSets=True'
WHERE
	Tenant.Id = 2

USE [AffinityDmsMaster]
SELECT	Id, MasterTenantId, MasterTenantToken, AuthenticationType, Domain, CompanyName, UrlApi, UrlResourceGroup, UrlStorage, UrlStorageBlob, UrlStorageTable, UrlStorageQueue, 
		UrlStorageFile, StorageAccessKeyPrimary, StorageAccessKeySecondary, StorageConnectionStringPrimary, StorageConnectionStringSecondary, DatabaseConnectionString, TenantType
FROM	dbo.Tenant



USE [AffinityDmsTenant_0000000000000000001]

UPDATE
	Tenant
SET
	MasterTenantId = 1,
	MasterTenantToken = N'{90a0a900-6e0c-4161-a71f-ee40883626a9}',
	AuthenticationType = 3,
	Domain = 'kloud-soft.com',
	CompanyName = 'Kloud-Soft Private Limited',
	UrlApi = 'http://affinity-ecm-tenantportal.azurewebsites.net/',
	UrlResourceGroup = 'kloudsoft-rg-affinity',
	UrlStorage = 'kloudsoftstorage',
	UrlStorageBlob = 'https://kloudsoftstorage.blob.core.windows.net/',
	UrlStorageTable = 'https://kloudsoftstorage.table.core.windows.net/',
	UrlStorageQueue = 'https://kloudsoftstorage.queue.core.windows.net/',
	UrlStorageFile = 'https://kloudsoftstorage.file.core.windows.net/',
	StorageAccessKeyPrimary = 'r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==',
	StorageConnectionStringPrimary = 'DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=r6IWIGAysm+Q5z021jeMQPAmFiO1YwFVd4/6t7fadnSou4tfLW/2Agt5gBJ24xfpEcGMPg7A3DW6iFTAUTOmtA==;',
	StorageAccessKeySecondary = 'Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==',
	StorageConnectionStringSecondary = 'DefaultEndpointsProtocol=https;AccountName=kloudsoftstorage;AccountKey=Ku2WBKJ0ARho9Vwqy6V0MhW6EZ5zN/pUbU3VO5xUD87Yr30zSWL8nwajxAkGw85CbgzHgJEXBjEvnPyZRCpn6w==;',
	DatabaseConnectionString = 'Data Source=MONSTER\MSSQLEXPRESS2014;Initial Catalog=AffinityDmsTenant_0000000000000000001;Integrated Security=True;MultipleActiveResultSets=True'
WHERE
	Tenant.Id = 1

USE [AffinityDmsTenant_0000000000000000001]
SELECT	Id, MasterTenantId, MasterTenantToken, AuthenticationType, Domain, CompanyName, UrlApi, UrlResourceGroup, UrlStorage, UrlStorageBlob, UrlStorageTable, UrlStorageQueue, 
		UrlStorageFile, StorageAccessKeyPrimary, StorageAccessKeySecondary, StorageConnectionStringPrimary, StorageConnectionStringSecondary, DatabaseConnectionString, TenantType
FROM	dbo.Tenant



USE [AffinityDmsTenant_0000000000000000002]

UPDATE
	Tenant
SET
	MasterTenantId = 2,
	MasterTenantToken = N'{1ec390bd-02a9-40fb-89db-c7582163b0f2}',
	AuthenticationType = 3,
	Domain = 'cobox.com',
	CompanyName = 'Cobox',
	UrlApi = 'http://cobox-demo.azurewebsites.net/',
	UrlResourceGroup = 'affinity-ecm',
	UrlStorage = 'coboxstorage',
	UrlStorageBlob = 'https://coboxstorage.blob.core.windows.net/',
	UrlStorageTable = 'https://coboxstorage.table.core.windows.net/',
	UrlStorageQueue = 'https://coboxstorage.queue.core.windows.net/',
	UrlStorageFile = 'https://coboxstorage.file.core.windows.net/',
	StorageAccessKeyPrimary = '5xuKR9BvlL2xS251eAI0cuPwYjkt2Mhru2xCqx+KiEcPsqXZgmILe6N0FPHuI9g8lNeeyRkOJcDrrhFGsOab/g==',
	StorageConnectionStringPrimary = 'DefaultEndpointsProtocol=https;AccountName=coboxstorage;AccountKey=5xuKR9BvlL2xS251eAI0cuPwYjkt2Mhru2xCqx+KiEcPsqXZgmILe6N0FPHuI9g8lNeeyRkOJcDrrhFGsOab/g==;',
	StorageAccessKeySecondary = 'BQFfUpo/G7BudTk6nac0b4I7YjhcBf/ygfRat/QY7ndis7y4dcxVt2guDV0s816/o9Qu6oN7IfKPjMnfOLSAcg==',
	StorageConnectionStringSecondary = 'DefaultEndpointsProtocol=https;AccountName=coboxstorage;AccountKey=BQFfUpo/G7BudTk6nac0b4I7YjhcBf/ygfRat/QY7ndis7y4dcxVt2guDV0s816/o9Qu6oN7IfKPjMnfOLSAcg==;',
	DatabaseConnectionString = 'Data Source=MONSTER\MSSQLEXPRESS2014;Initial Catalog=AffinityDmsTenant_0000000000000000002;Integrated Security=True;MultipleActiveResultSets=True'
WHERE
	Tenant.Id = 1

USE [AffinityDmsTenant_0000000000000000002]
SELECT	Id, MasterTenantId, MasterTenantToken, AuthenticationType, Domain, CompanyName, UrlApi, UrlResourceGroup, UrlStorage, UrlStorageBlob, UrlStorageTable, UrlStorageQueue, 
		UrlStorageFile, StorageAccessKeyPrimary, StorageAccessKeySecondary, StorageConnectionStringPrimary, StorageConnectionStringSecondary, DatabaseConnectionString, TenantType
FROM	dbo.Tenant