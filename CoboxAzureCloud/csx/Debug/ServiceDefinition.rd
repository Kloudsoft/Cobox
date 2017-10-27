<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CoboxAzureCloud" generation="1" functional="0" release="0" Id="47c03c46-02d0-4849-ab02-25e68a435cb3" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CoboxAzureCloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HouseOfSynergy.AffinityDms.WebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/LB:HouseOfSynergy.AffinityDms.WebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HouseOfSynergy.AffinityDms.WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/MapHouseOfSynergy.AffinityDms.WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HouseOfSynergy.AffinityDms.WebRole:StorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/MapHouseOfSynergy.AffinityDms.WebRole:StorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="HouseOfSynergy.AffinityDms.WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/MapHouseOfSynergy.AffinityDms.WebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/MapWorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1:StorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/MapWorkerRole1:StorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/MapWorkerRole1Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HouseOfSynergy.AffinityDms.WebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapHouseOfSynergy.AffinityDms.WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHouseOfSynergy.AffinityDms.WebRole:StorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRole/StorageConnectionString" />
          </setting>
        </map>
        <map name="MapHouseOfSynergy.AffinityDms.WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRoleInstances" />
          </setting>
        </map>
        <map name="MapWorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/WorkerRole1/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRole1:StorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/WorkerRole1/StorageConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/WorkerRole1Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HouseOfSynergy.AffinityDms.WebRole" generation="1" functional="0" release="0" software="D:\DEVMVC\Cobox\CoboxAzureCloud\csx\Debug\roles\HouseOfSynergy.AffinityDms.WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="8080" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="StorageConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HouseOfSynergy.AffinityDms.WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HouseOfSynergy.AffinityDms.WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WorkerRole1" generation="1" functional="0" release="0" software="D:\DEVMVC\Cobox\CoboxAzureCloud\csx\Debug\roles\WorkerRole1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="StorageConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HouseOfSynergy.AffinityDms.WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/WorkerRole1Instances" />
            <sCSPolicyUpdateDomainMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/WorkerRole1UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/WorkerRole1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="HouseOfSynergy.AffinityDms.WebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="WorkerRole1UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="HouseOfSynergy.AffinityDms.WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRole1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HouseOfSynergy.AffinityDms.WebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRole1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="37526df6-6d5e-4116-8185-0bb724ae655f" ref="Microsoft.RedDog.Contract\ServiceContract\CoboxAzureCloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="305c07db-5749-4d0d-a5cd-ba59a4ac84bd" ref="Microsoft.RedDog.Contract\Interface\HouseOfSynergy.AffinityDms.WebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CoboxAzureCloud/CoboxAzureCloudGroup/HouseOfSynergy.AffinityDms.WebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>