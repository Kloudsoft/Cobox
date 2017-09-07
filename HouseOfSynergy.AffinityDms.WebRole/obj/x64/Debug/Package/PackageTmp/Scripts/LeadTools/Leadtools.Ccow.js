﻿/**************************************************
LEADTOOLS (C) 1991-2016 LEAD Technologies, Inc. ALL RIGHTS RESERVED.
This software is protected by United States and International copyright laws.
Any copying, duplication, deployment, redistribution, modification or other
disposition hereof is STRICTLY PROHIBITED without an express written license
granted by LEAD Technologies, Inc.. This notice may not be removed or otherwise
altered under any circumstances.
Portions of this product are licensed under US patent 5,327,254 and foreign
counterparts.
For more information, contact LEAD Technologies, Inc. at 704-332-5532 or visit
https://www.leadtools.com
**************************************************/
// Leadtools.Ccow.js
// Version:19.0.0.1
(function(){Type.registerNamespace("lt.Ccow");window.lt.Ccow._lT_VersionNumber=function(){};lt.Ccow.ICcowServiceLocator=function(){};lt.Ccow.ICcowServiceLocator.prototype={send:null};lt.Ccow.ICcowServiceLocator.registerInterface("lt.Ccow.ICcowServiceLocator");lt.Ccow.CcowBase=function(a){if(null==a)throw Error("'serviceLocator' cannot be null");this._ccowService=a};lt.Ccow.CcowBase.prototype={_ccowService:null,_send:function(a){var a=this._ccowService.send(a),b=lt.Ccow._utils._stringToDictionary(a);
if(Object.keyExists(b,"exception"))throw a=lt.Ccow._utils.getStringValue(b,"exception"),b=lt.Ccow._utils.getStringValue(b,"exceptionMessage"),new lt.Ccow.CcowException(a,b);return b}};lt.Ccow.Subject=function(){this._Name="";this._Items=new lt.LeadCollection;this._Items.add_collectionChanged(ss.Delegate.create(this,this._items_CollectionChanged))};lt.Ccow.Subject.prototype={get_name:function(){return this._Name},set_name:function(a){return this._Name=a},_ExistInContext:!1,get__existInContext:function(){return this._ExistInContext},
set__existInContext:function(a){return this._ExistInContext=a},get_items:function(){return this._Items},_items_CollectionChanged:function(a,b){if(b.get_action()===lt.NotifyLeadCollectionChangedAction.add){var c=Type.safeCast(b.get_newItems()[0],lt.Ccow.ContextItem);if(String.isNullOrEmpty(this._Name))this._Name=c.get_subject();else if(c.get_subject().toLowerCase()!==this._Name.toLowerCase())throw new lt.ArgumentException("Item subject doesn't match subject definition",c.get_subject());}},toItemNameArray:function(){for(var a=
Array(this._Items.get_count()),b=0;b<this._Items.get_count();b++)a[b]=this._Items.get_item(b).toString();return a},toItemValueArray:function(){for(var a=Array(this._Items.get_count()),b=0;b<this._Items.get_count();b++){var c=Type.safeCast(this._Items.get_item(b),lt.Ccow.ContextItem);a[b]=c.get_value()}return a},hasItem:function(a){for(var a=new lt.Ccow.ContextItem(a),b=ss.IEnumerator.getEnumerator(this._Items);b.moveNext();)if(a.equals(b.current))return!0;return!1},getItem:function(a){for(var a=new lt.Ccow.ContextItem(a),
b=ss.IEnumerator.getEnumerator(this._Items);b.moveNext();){var c=b.current;if(a.equals(c))return c}return null},_isEmpty:function(){for(var a=ss.IEnumerator.getEnumerator(this._Items);a.moveNext();)if(!a.current._isEmpty())return!1;return!0}};Object.defineProperty(lt.Ccow.Subject.prototype,"name",{get:lt.Ccow.Subject.prototype.get_name,set:lt.Ccow.Subject.prototype.set_name,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.Subject.prototype,"items",{get:lt.Ccow.Subject.prototype.get_items,
enumerable:!0,configurable:!0});lt.Ccow.ClientUtils=function(a){lt.Ccow.ClientUtils.initializeBase(this,[a])};lt.Ccow.ClientUtils.prototype={getEncodedHashKey:function(a){a=this._send(String.format("interface=Utils&method=GetEncodedHashKey&hashString={0}",a));return lt.Ccow._utils.getStringValue(a,"key")},getEncodedPublicKey:function(a){a=this._send(String.format("interface=KeyContainer&method=GetEncodedPublicKey&applicationName={0}",a));return lt.Ccow._utils.getStringValue(a,"key")},getEncodedSignKey:function(a,
b){var c=this._send(String.format("interface=KeyContainer&method=GetEncodedSignKey&applicationName={0}&messageDigest={1}",a,b));return lt.Ccow._utils.getStringValue(c,"key")},getWebSocketInfo:function(){var a=this._send("interface=WebSocket&method=GetInfo");return new lt.Ccow.WebSocketInfo(lt.Ccow._utils.getStringValue(a,"address"),lt.Ccow._utils.getNumberValue(a,"portNumber"))},ping:function(){var a=this._send("interface=Utils&method=Ping");return Object.keyExists(a,"Status")?!lt.Ccow._utils.getStringValue(a,
"Status").compareTo("Success"):!1}};lt.Ccow.ContextManagementRegistry=function(a){lt.Ccow.ContextManagementRegistry.initializeBase(this,[a])};lt.Ccow.ContextManagementRegistry.prototype={locate:function(a,b,c,d){try{var e=this._send(String.format("interface=ContextManagementRegistry&method=Locate&componentName={0}&version={1}&descriptiveData={2}&contextParticipant={3}",a,b,c,d)),a=null;if(Object.keyExists(e,"componentUrl"))var f=lt.Ccow._utils.getStringValue(e,"componentUrl"),g=lt.Ccow._utils.getStringValue(e,
"componentParameters"),h=lt.Ccow._utils.getStringValue(e,"site"),a=new lt.Ccow.LocateData(f,g,h);else throw Error("The context management registry could not locate the specified component instance");return a}catch(i){throw new lt.Ccow.CcowException("UnableToLocate",i.message);}}};lt.Ccow.AuthenticationRepository=function(a){lt.Ccow.AuthenticationRepository.initializeBase(this,[a])};lt.Ccow.AuthenticationRepository.prototype={connect:function(a){a=this._send(String.format("interface=AuthenticationRepository&method=Connect&applicationName={0}",
a));return lt.Ccow._utils.getNumberValue(a,"bindingCoupon")},disconnect:function(a){this._send(String.format("interface=AuthenticationRepository&method=Disconnect&bindingCoupon={0}",a))},setAuthenticationData:function(a,b,c,d,e){this._send(String.format("interface=AuthenticationRepository&method=SetAuthenticationData&coupon={0}&logonName={1}&dataFormat={2}&userData={3}&appSignature={4}",a,b,c,d,e))},deleteAuthenticationData:function(a,b,c,d){this._send(String.format("interface=AuthenticationRepository&method=DeleteAuthenticationData&coupon={0}&logonName={1}&dataFormat={2}&appSignature={3}",
a,b,c,d))},getAuthenticationData:function(a,b,c,d){b=this._send(String.format("interface=AuthenticationRepository&method=GetAuthenticationData&coupon={0}&logonName={1}&dataFormat={2}&appSignature={3}",a,b,c,d));a=lt.Ccow._utils.getStringValue(b,"repositorySignature");b=lt.Ccow._utils.getStringValue(b,"userData");return new lt.Ccow.AuthenticationData(b,a)}};lt.Ccow.ContextData=function(a){lt.Ccow.ContextData.initializeBase(this,[a])};lt.Ccow.ContextData.prototype={getItemNames:function(a){a=this._send(String.format("interface=ContextData&method=GetItemNames&contextCoupon={0}",
a));return lt.Ccow._utils.getStringArrayValue(a,"names")},setItemValues:function(a,b,c,d){this._send(String.format("interface=ContextData&method=SetItemValues&participantCoupon={0}&itemNames={1}&itemValues={2}&contextCoupon={3}",a,lt.Ccow._utils.arrayToString(b),lt.Ccow._utils.arrayToString(c),d))},getItemValues:function(a,b,c){a=this._send(String.format("interface=ContextData&method=GetItemValues&itemNames={0}&onlyChanges={1}&contextCoupon={2}",lt.Ccow._utils.arrayToString(a),b,c));return lt.Ccow._utils.getStringArrayValue(a,
"itemValues")}};lt.Ccow.ContextFilter=function(a){lt.Ccow.ContextFilter.initializeBase(this,[a])};lt.Ccow.ContextFilter.prototype={setSubjectsOfInterest:function(a,b){var c=this._send(String.format("interface=ContextFilter&method=SetSubjectsOfInterest&participantCoupon={0}&subjectNames={1}",a,lt.Ccow._utils.arrayToString(b)));return lt.Ccow._utils.getStringArrayValue(c,"names")},getSubjectsOfInterest:function(a){a=this._send(String.format("interface=ContextFilter&method=GetSubjectsOfInterest&participantCoupon={0}",
a));return lt.Ccow._utils.getStringArrayValue(a,"subjectNames")},clearFilter:function(a){this._send(String.format("interface=ContextFilter&method=ClearFilter&participantCoupon={0}",a))}};lt.Ccow.ContextManager=function(a){lt.Ccow.ContextManager.initializeBase(this,[a])};lt.Ccow.ContextManager.prototype={getMostRecentContextCoupon:function(){var a=this._send(String.format("interface=ContextManager&method=GetMostRecentContextCoupon"));return lt.Ccow._utils.getNumberValue(a,"contextCoupon")},joinCommonContext:function(a,
b,c,d){a=this._send(String.format("interface=ContextManager&method=JoinCommonContext&applicationName={0}&contextParticipant={1}&survey={2}&wait={3}",a,b,c,d));return lt.Ccow._utils.getNumberValue(a,"participantCoupon")},leaveCommonContext:function(a){this._send(String.format("interface=ContextManager&method=LeaveCommonContext&participantCoupon={0}",a))},startContextChanges:function(a){a=this._send(String.format("interface=ContextManager&method=StartContextChanges&participantCoupon={0}",a));return lt.Ccow._utils.getNumberValue(a,
"contextCoupon")},endContextChanges:function(a){var b=this._send(String.format("interface=ContextManager&method=EndContextChanges&contextCoupon={0}",a)),a=lt.Ccow._utils.getBoolValue(b,"noContinue"),b=lt.Ccow._utils.getStringArrayValue(b,"responses");return new lt.Ccow.ContextChangesData(b,a)},undoContextChanges:function(a){this._send(String.format("interface=ContextManager&method=UndoContextChanges&contextCoupon={0}",a))},publishChangesDecision:function(a,b){var c=this._send(String.format("interface=ContextManager&method=PublishChangesDecision&contextCoupon={0}&decision={1}",
a,b));return lt.Ccow._utils.getStringArrayValue(c,"listenerURLs")},suspendParticipation:function(a){this._send(String.format("interface=ContextManager&method=SuspendParticipation&participantCoupon={0}",a))},resumeParticipation:function(a,b){this._send(String.format("interface=ContextManager&method=ResumeParticipation&participantCoupon={0}&wait={1}",a,b))}};lt.Ccow.ContextSession=function(a){lt.Ccow.ContextSession.initializeBase(this,[a])};lt.Ccow.ContextSession.prototype={create:function(){var a=
this._send(String.format("interface=ContextSession&method=Create"));return lt.Ccow._utils.getStringValue(a,"newContextManager")},activate:function(a,b,c,d){this._send(String.format("interface=ContextSession&method=Activate&participantCoupon={0}&cmToActivate={1}&nonce={2}&AppSignature={3}",a,b,c,d))}};lt.Ccow.ContextAction=function(a){lt.Ccow.ContextAction.initializeBase(this,[a])};lt.Ccow.ContextAction.prototype={perform:function(a,b,c,d,e,f){d=this._send(String.format("interface=ContextAction&method=Perform&cpCallBackURL={0}&cpErrorURL={1}&partcipantCoupon={2}&inputNames={3}&inputValues={4}&appSignature={5}",
a,b,c,lt.Ccow._utils.arrayToString(d),lt.Ccow._utils.arrayToString(e),f));a=lt.Ccow._utils.getNumberValue(d,"actionCoupon");b=lt.Ccow._utils.getStringArrayValue(d,"outputNames");c=lt.Ccow._utils.getStringArrayValue(d,"outputValues");d=lt.Ccow._utils.getStringValue(d,"managerSignature");return new lt.Ccow.ContextActionData(a,b,c,d)}};lt.Ccow.Constants=function(){};lt.Ccow.ContextItem=function(a){this._Value=this._Suffix=this._Name=this._NameDescriptor=this._Role=this._Subject=this._SubjectDescriptor=
"";var b=-1;if(null==a)throw new lt.ArgumentNullException("item");for(b=a.indexOf("]");-1!==b;){var c=a.indexOf("[");String.isNullOrEmpty(this._SubjectDescriptor)?this._SubjectDescriptor=a.substring(c,b-c+1):this._NameDescriptor=a.substring(c,b-c+1);a=a.remove(c,b-c+1);b=a.indexOf("]")}a=a.split(".");if(0<a.length&&(this._Subject=a[0],1<a.length&&(this._Role=a[1]),2<a.length&&(this._Name=a[2]),3<a.length))this._Suffix=a[3]};lt.Ccow.ContextItem.prototype={get_subjectDescriptor:function(){return this._SubjectDescriptor},
set_subjectDescriptor:function(a){return this._SubjectDescriptor=a},get_subject:function(){return this._Subject},set_subject:function(a){return this._Subject=a},get_role:function(){return this._Role},set_role:function(a){return this._Role=a},get_nameDescriptor:function(){return this._NameDescriptor},set_nameDescriptor:function(a){return this._NameDescriptor=a},get_name:function(){return this._Name},set_name:function(a){return this._Name=a},get_suffix:function(){return this._Suffix},set_suffix:function(a){return this._Suffix=
a},get_value:function(){return this._Value},set_value:function(a){return this._Value=a},toString:function(){var a;a=""+this._SubjectDescriptor;a+=this._Subject;a=a+"."+this._Role;a=a+"."+this._NameDescriptor;a+=this._Name;a=a+"."+this._Suffix;"."===a[a.length-1]&&a.remove(a.length-1,1);return a},equals:function(a){return Type.canCast(a,lt.Ccow.ContextItem)?(a=Type.safeCast(a,lt.Ccow.ContextItem),a.get_name()===this._Name&&a.get_nameDescriptor()===this._NameDescriptor&&a.get_role()===this._Role&&a.get_subject()===
this._Subject&&a.get_subjectDescriptor()===this._SubjectDescriptor&&a._Suffix===this._Suffix):!1},_isEmpty:function(){return null==this.get_value()||Type.canCast(this.get_value(),String)&&!Type.safeCast(this.get_value(),String).length?!0:!1}};Object.defineProperty(lt.Ccow.ContextItem.prototype,"subjectDescriptor",{get:lt.Ccow.ContextItem.prototype.get_subjectDescriptor,set:lt.Ccow.ContextItem.prototype.set_subjectDescriptor,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextItem.prototype,
"subject",{get:lt.Ccow.ContextItem.prototype.get_subject,set:lt.Ccow.ContextItem.prototype.set_subject,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextItem.prototype,"role",{get:lt.Ccow.ContextItem.prototype.get_role,set:lt.Ccow.ContextItem.prototype.set_role,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextItem.prototype,"nameDescriptor",{get:lt.Ccow.ContextItem.prototype.get_nameDescriptor,set:lt.Ccow.ContextItem.prototype.set_nameDescriptor,enumerable:!0,
configurable:!0});Object.defineProperty(lt.Ccow.ContextItem.prototype,"name",{get:lt.Ccow.ContextItem.prototype.get_name,set:lt.Ccow.ContextItem.prototype.set_name,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextItem.prototype,"suffix",{get:lt.Ccow.ContextItem.prototype.get_suffix,set:lt.Ccow.ContextItem.prototype.set_suffix,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextItem.prototype,"value",{get:lt.Ccow.ContextItem.prototype.get_value,set:lt.Ccow.ContextItem.prototype.set_value,
enumerable:!0,configurable:!0});lt.Ccow.CcowException=function(a,b){throw Error(String.format("Exception: {0}\nException Message: {1}",a,b));};lt.Ccow.ClientContext=function(a,b){lt.Ccow.ClientContext.initializeBase(this,[a]);this._clientUtils$1=new lt.Ccow.ClientUtils(a);this._id$1=b;var c=this._clientUtils$1.getWebSocketInfo();this._webSocket$1=new WebSocket(String.format("ws://{0}:{1}/",c.get_address(),c.get_portNumber()));c=ss.Delegate.create(this,function(){this._webSocket$1.send(String.format("id={0}",
this._id$1))});ss.Delegate.create(this,function(){this._onError$1(Error("WebSocket general error."))});var d=ss.Delegate.create(this,function(a){this.onMessage(Type.safeCast(a.data,String))});this._webSocket$1.onopen=c;this._webSocket$1.onmessage=d};lt.Ccow.ClientContext.prototype={_clientUtils$1:null,_id$1:null,_webSocket$1:null,dispose:function(){this._webSocket$1.close()},add_error:function(a){this.__error$1=ss.Delegate.combine(this.__error$1,a)},remove_error:function(a){this.__error$1=ss.Delegate.remove(this.__error$1,
a)},__error$1_handler_get:function(){null==this.__error$1_handler&&(this.__error$1_handler=ss.EventHandler.create(this,this.add_error,this.remove_error));return this.__error$1_handler},__error$1:null,__error$1_handler:null,add_contextChangesPending:function(a){this.__contextChangesPending$1=ss.Delegate.combine(this.__contextChangesPending$1,a)},remove_contextChangesPending:function(a){this.__contextChangesPending$1=ss.Delegate.remove(this.__contextChangesPending$1,a)},__contextChangesPending$1_handler_get:function(){null==
this.__contextChangesPending$1_handler&&(this.__contextChangesPending$1_handler=ss.EventHandler.create(this,this.add_contextChangesPending,this.remove_contextChangesPending));return this.__contextChangesPending$1_handler},__contextChangesPending$1:null,__contextChangesPending$1_handler:null,add_contextChangesAccepted:function(a){this.__contextChangesAccepted$1=ss.Delegate.combine(this.__contextChangesAccepted$1,a)},remove_contextChangesAccepted:function(a){this.__contextChangesAccepted$1=ss.Delegate.remove(this.__contextChangesAccepted$1,
a)},__contextChangesAccepted$1_handler_get:function(){null==this.__contextChangesAccepted$1_handler&&(this.__contextChangesAccepted$1_handler=ss.EventHandler.create(this,this.add_contextChangesAccepted,this.remove_contextChangesAccepted));return this.__contextChangesAccepted$1_handler},__contextChangesAccepted$1:null,__contextChangesAccepted$1_handler:null,add_contextChangesCanceled:function(a){this.__contextChangesCanceled$1=ss.Delegate.combine(this.__contextChangesCanceled$1,a)},remove_contextChangesCanceled:function(a){this.__contextChangesCanceled$1=
ss.Delegate.remove(this.__contextChangesCanceled$1,a)},__contextChangesCanceled$1_handler_get:function(){null==this.__contextChangesCanceled$1_handler&&(this.__contextChangesCanceled$1_handler=ss.EventHandler.create(this,this.add_contextChangesCanceled,this.remove_contextChangesCanceled));return this.__contextChangesCanceled$1_handler},__contextChangesCanceled$1:null,__contextChangesCanceled$1_handler:null,add_commonContextTerminated:function(a){this.__commonContextTerminated$1=ss.Delegate.combine(this.__commonContextTerminated$1,
a)},remove_commonContextTerminated:function(a){this.__commonContextTerminated$1=ss.Delegate.remove(this.__commonContextTerminated$1,a)},__commonContextTerminated$1_handler_get:function(){null==this.__commonContextTerminated$1_handler&&(this.__commonContextTerminated$1_handler=ss.EventHandler.create(this,this.add_commonContextTerminated,this.remove_commonContextTerminated));return this.__commonContextTerminated$1_handler},__commonContextTerminated$1:null,__commonContextTerminated$1_handler:null,add_ping:function(a){this.__ping$1=
ss.Delegate.combine(this.__ping$1,a)},remove_ping:function(a){this.__ping$1=ss.Delegate.remove(this.__ping$1,a)},__ping$1_handler_get:function(){null==this.__ping$1_handler&&(this.__ping$1_handler=ss.EventHandler.create(this,this.add_ping,this.remove_ping));return this.__ping$1_handler},__ping$1:null,__ping$1_handler:null,_onError$1:function(a){null!=this.__error$1&&this.__error$1(this,new lt.Ccow.ContextErrorEventArgs(a))},onMessage:function(a){var a=lt.Ccow._utils._stringToDictionary(a),b=lt.Ccow._utils.getStringValue(a,
"method");if(!String.isNullOrEmpty(b)){var c=-1E3;Object.keyExists(a,"contextCoupon")&&(c=lt.Ccow._utils.getNumberValue(a,"contextCoupon"));null!=this.__contextChangesPending$1&&!b.compareTo("ContextChangesPending")?this.__contextChangesPending$1(this,new lt.Ccow.ContextEventArgs(c)):null!=this.__contextChangesAccepted$1&&!b.compareTo("ContextChangesAccepted")?this.__contextChangesAccepted$1(this,new lt.Ccow.ContextEventArgs(c)):null!=this.__contextChangesCanceled$1&&!b.compareTo("ContextChangesCanceled")?
this.__contextChangesCanceled$1(this,new lt.Ccow.ContextEventArgs(c)):null!=this.__commonContextTerminated$1&&!b.compareTo("CommonContextTerminated")?this.__commonContextTerminated$1(this,lt.LeadEventArgs.Empty):null!=this.__ping$1&&!b.compareTo("Ping")&&this.__ping$1(this,lt.LeadEventArgs.Empty)}}};Object.defineProperty(lt.Ccow.ClientContext.prototype,"error",{get:lt.Ccow.ClientContext.prototype.__error$1_handler_get,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ClientContext.prototype,
"contextChangesPending",{get:lt.Ccow.ClientContext.prototype.__contextChangesPending$1_handler_get,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ClientContext.prototype,"contextChangesAccepted",{get:lt.Ccow.ClientContext.prototype.__contextChangesAccepted$1_handler_get,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ClientContext.prototype,"contextChangesCanceled",{get:lt.Ccow.ClientContext.prototype.__contextChangesCanceled$1_handler_get,enumerable:!0,configurable:!0});
Object.defineProperty(lt.Ccow.ClientContext.prototype,"commonContextTerminated",{get:lt.Ccow.ClientContext.prototype.__commonContextTerminated$1_handler_get,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ClientContext.prototype,"ping",{get:lt.Ccow.ClientContext.prototype.__ping$1_handler_get,enumerable:!0,configurable:!0});lt.Ccow.ContextEventArgs=function(a){lt.Ccow.ContextEventArgs.initializeBase(this);this._contextCoupon$1=a};lt.Ccow.ContextEventArgs.prototype={_contextCoupon$1:0,
get_contextCoupon:function(){return this._contextCoupon$1}};Object.defineProperty(lt.Ccow.ContextEventArgs.prototype,"contextCoupon",{get:lt.Ccow.ContextEventArgs.prototype.get_contextCoupon,enumerable:!0,configurable:!0});lt.Ccow.ContextErrorEventArgs=function(a){lt.Ccow.ContextErrorEventArgs.initializeBase(this);this._exception$1=a};lt.Ccow.ContextErrorEventArgs.prototype={_exception$1:null,get_error:function(){return this._exception$1}};Object.defineProperty(lt.Ccow.ContextErrorEventArgs.prototype,
"error",{get:lt.Ccow.ContextErrorEventArgs.prototype.get_error,enumerable:!0,configurable:!0});lt.Ccow.SecureContextData=function(a){lt.Ccow.SecureContextData.initializeBase(this,[a])};lt.Ccow.SecureContextData.prototype={getItemNames:function(a){a=this._send(String.format("interface=SecureContextData&method=GetItemNames&contextCoupon={0}",a));return lt.Ccow._utils.getStringArrayValue(a,"names")},setItemValues:function(a,b,c,d,e){this._send(String.format("interface=SecureContextData&method=SetItemValues&participantCoupon={0}&itemNames={1}&itemValues={2}&contextCoupon={3}&appSignature={4}",
a,lt.Ccow._utils.arrayToString(b),lt.Ccow._utils.arrayToString(c),d,e))},getItemValues:function(a,b,c,d,e){b=this._send(String.format("interface=SecureContextData&method=GetItemValues&participantCoupon={0}&itemNames={1}&onlyChanges={2}&contextCoupon={3}&appSignature={4}",a,lt.Ccow._utils.arrayToString(b),c,d,e));a=lt.Ccow._utils.getStringArrayValue(b,"itemValues");b=lt.Ccow._utils.getStringValue(b,"managerSignature");return new lt.Ccow.SecureItemValues(a,b)}};lt.Ccow.SecureBinding=function(a){lt.Ccow.SecureBinding.initializeBase(this,
[a])};lt.Ccow.SecureBinding.prototype={initializeBinding:function(a,b,c){b=this._send(String.format("interface=SecureBinding&method=InitializeBinding&bindeeCoupon={0}&propertyNames={1}&propertyValues={2}",a,lt.Ccow._utils.arrayToString(b),lt.Ccow._utils.arrayToString(c)));a=lt.Ccow._utils.getStringValue(b,"binderPublicKey");b=lt.Ccow._utils.getStringValue(b,"mac");return new lt.Ccow.BindingData(a,b)},finalizeBinding:function(a,b,c){a=this._send(String.format("interface=SecureBinding&method=FinalizeBinding&bindeeCoupon={0}&bindeePublicKey={1}&mac={2}",
a,b,c));return lt.Ccow._utils.getStringArrayValue(a,"privileges")}};lt.Ccow.ImplementationInformation=function(a){lt.Ccow.ImplementationInformation.initializeBase(this,[a])};lt.Ccow.ImplementationInformation.prototype={get_componentName:function(){var a=this._send(String.format("interface=ImplementationInformation&method=ComponentName"));return lt.Ccow._utils.getStringValue(a,"componentName")},get_revMajorNum:function(){var a=this._send(String.format("interface=ImplementationInformation&method=RevMajorNum"));
return lt.Ccow._utils.getStringValue(a,"revMajorNum")},get_revMinorNum:function(){var a=this._send(String.format("interface=ImplementationInformation&method=RevMinorNum"));return lt.Ccow._utils.getStringValue(a,"revMinorNum")},get_partNumber:function(){var a=this._send(String.format("interface=ImplementationInformation&method=PartNumber"));return lt.Ccow._utils.getStringValue(a,"partNumber")},get_manufacturer:function(){var a=this._send(String.format("interface=ImplementationInformation&method=Manufacturer"));
return lt.Ccow._utils.getStringValue(a,"manufacturer")},get_targetOS:function(){var a=this._send(String.format("interface=ImplementationInformation&method=TargetOS"));return lt.Ccow._utils.getStringValue(a,"targetOS")},get_targetOSRev:function(){var a=this._send(String.format("interface=ImplementationInformation&method=TargetOSRev"));return lt.Ccow._utils.getStringValue(a,"targetOSRev")},get_whenInstalled:function(){var a=this._send(String.format("interface=ImplementationInformation&method=WhenInstalled"));
return lt.Ccow._utils.getStringValue(a,"whenInstalled")}};Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"componentName",{get:lt.Ccow.ImplementationInformation.prototype.get_componentName,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"revMajorNum",{get:lt.Ccow.ImplementationInformation.prototype.get_revMajorNum,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"revMinorNum",{get:lt.Ccow.ImplementationInformation.prototype.get_revMinorNum,
enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"partNumber",{get:lt.Ccow.ImplementationInformation.prototype.get_partNumber,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"manufacturer",{get:lt.Ccow.ImplementationInformation.prototype.get_manufacturer,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"targetOS",{get:lt.Ccow.ImplementationInformation.prototype.get_targetOS,
enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"targetOSRev",{get:lt.Ccow.ImplementationInformation.prototype.get_targetOSRev,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ImplementationInformation.prototype,"whenInstalled",{get:lt.Ccow.ImplementationInformation.prototype.get_whenInstalled,enumerable:!0,configurable:!0});lt.Ccow.LocateData=function(a,b,c){this._site=this._componentParameters=this._componentUrl="";this._componentUrl=
a;this._componentParameters=b;this._site=c};lt.Ccow.LocateData.prototype={get_componentUrl:function(){return this._componentUrl},get_componentParameters:function(){return this._componentParameters},get_site:function(){return this._site}};Object.defineProperty(lt.Ccow.LocateData.prototype,"componentUrl",{get:lt.Ccow.LocateData.prototype.get_componentUrl,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.LocateData.prototype,"componentParameters",{get:lt.Ccow.LocateData.prototype.get_componentParameters,
enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.LocateData.prototype,"site",{get:lt.Ccow.LocateData.prototype.get_site,enumerable:!0,configurable:!0});lt.Ccow.AuthenticationData=function(a,b){this._repositorySignature=this._userData="";this._userData=a;this._repositorySignature=b};lt.Ccow.AuthenticationData.prototype={get_userData:function(){return this._userData},get_repositorySignature:function(){return this._repositorySignature}};Object.defineProperty(lt.Ccow.AuthenticationData.prototype,
"userData",{get:lt.Ccow.AuthenticationData.prototype.get_userData,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.AuthenticationData.prototype,"repositorySignature",{get:lt.Ccow.AuthenticationData.prototype.get_repositorySignature,enumerable:!0,configurable:!0});lt.Ccow.ContextChangesData=function(a,b){this._responses=[];this._responses=a;this._noContinue=b};lt.Ccow.ContextChangesData.prototype={_noContinue:!1,get_noContinue:function(){return this._noContinue},get_responses:function(){return this._responses}};
Object.defineProperty(lt.Ccow.ContextChangesData.prototype,"noContinue",{get:lt.Ccow.ContextChangesData.prototype.get_noContinue,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextChangesData.prototype,"responses",{get:lt.Ccow.ContextChangesData.prototype.get_responses,enumerable:!0,configurable:!0});lt.Ccow.ContextActionData=function(a,b,c,d){this._outputNames=[];this._outputValues=[];this._managerSignature="";this._actionCoupon=a;this._outputNames=b;this._outputValues=c;this._managerSignature=
d};lt.Ccow.ContextActionData.prototype={_actionCoupon:0,get_actionCoupon:function(){return this._actionCoupon},get_outputNames:function(){return this._outputNames},get_outputValues:function(){return this._outputValues},get_managerSignature:function(){return this._managerSignature}};Object.defineProperty(lt.Ccow.ContextActionData.prototype,"actionCoupon",{get:lt.Ccow.ContextActionData.prototype.get_actionCoupon,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextActionData.prototype,
"outputNames",{get:lt.Ccow.ContextActionData.prototype.get_outputNames,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextActionData.prototype,"outputValues",{get:lt.Ccow.ContextActionData.prototype.get_outputValues,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.ContextActionData.prototype,"managerSignature",{get:lt.Ccow.ContextActionData.prototype.get_managerSignature,enumerable:!0,configurable:!0});lt.Ccow.BindingData=function(a,b){this._mac=this._binderPublicKey=
"";this._binderPublicKey=a;this._mac=b};lt.Ccow.BindingData.prototype={get_binderPublicKey:function(){return this._binderPublicKey},get_mac:function(){return this._mac}};Object.defineProperty(lt.Ccow.BindingData.prototype,"binderPublicKey",{get:lt.Ccow.BindingData.prototype.get_binderPublicKey,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.BindingData.prototype,"mac",{get:lt.Ccow.BindingData.prototype.get_mac,enumerable:!0,configurable:!0});lt.Ccow.SecureItemValues=function(a,b){this._itemValues=
[];this._managerSignature="";this._itemValues=a;this._managerSignature=b};lt.Ccow.SecureItemValues.prototype={get_itemValues:function(){return this._itemValues},get_managerSignature:function(){return this._managerSignature}};Object.defineProperty(lt.Ccow.SecureItemValues.prototype,"itemValues",{get:lt.Ccow.SecureItemValues.prototype.get_itemValues,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.SecureItemValues.prototype,"managerSignature",{get:lt.Ccow.SecureItemValues.prototype.get_managerSignature,
enumerable:!0,configurable:!0});lt.Ccow.WebSocketInfo=function(a,b){this._address="";this._portNumer=b;this._address=a};lt.Ccow.WebSocketInfo.prototype={_portNumer:0,get_portNumber:function(){return this._portNumer},get_address:function(){return this._address}};Object.defineProperty(lt.Ccow.WebSocketInfo.prototype,"portNumber",{get:lt.Ccow.WebSocketInfo.prototype.get_portNumber,enumerable:!0,configurable:!0});Object.defineProperty(lt.Ccow.WebSocketInfo.prototype,"address",{get:lt.Ccow.WebSocketInfo.prototype.get_address,
enumerable:!0,configurable:!0});lt.Ccow.ContextManagementRegistryLocator=function(){};lt.Ccow.ContextManagementRegistryLocator.prototype={_url:"http://localhost:2116/",send:function(a){return this.onSendData(this._url,a)}};lt.Ccow.InterfaceInformation=function(a){lt.Ccow.InterfaceInformation.initializeBase(this,[a])};lt.Ccow.InterfaceInformation.prototype={interrogate:function(a){a=this._send(String.format("interface=InterfaceInformation&method=Interrogate&interfaceName={0}",a));return lt.Ccow._utils.getBoolValue(a,
"implemented")}};lt.Ccow.ListenerRegistrar=function(a){lt.Ccow.ListenerRegistrar.initializeBase(this,[a])};lt.Ccow.ListenerRegistrar.prototype={register:function(a,b){var c=this._send(String.format("interface=ListenerRegistrar&method=Register&url={0}&participantCoupon={1}",a,b));return lt.Ccow._utils.getNumberValue(c,"contextCoupon")},unregister:function(a){this._send(String.format("interface=ListenerRegistrar&method=Unregister&url={0}",a))}};lt.Ccow._utils=function(){};lt.Ccow._utils.getBoolValue=
function(a,b){return Boolean.parse(Type.safeCast(a[b],String))};lt.Ccow._utils.getNumberValue=function(a,b){return parseInt(Type.safeCast(a[b],String))};lt.Ccow._utils.getStringValue=function(a,b){return Type.safeCast(a[b],String)};lt.Ccow._utils.getStringArrayValue=function(a,b){var c=lt.Ccow._utils.getStringValue(a,b);return null==c||1>c.length?[]:c.split("|")};lt.Ccow._utils.arrayToString=function(a){var b="";if(1>a.length)return b;for(var b=b+a[0].toString(),c=1;c<a.length;c++)b+=String.format("|{0}",
a[c].toString());return b};lt.Ccow._utils._stringToDictionary=function(a){var b={};a.startsWith('"')&&(a=a.substring(1));a.endsWith('"')&&(a=a.remove(a.length-1));for(var a=a.split("&"),c=0;c<a.length;c++){var d=a[c].indexOf("="),e=d+1,d=a[c].substring(0,d),e=a[c].substring(e,a[c].length);b[d]=e}return b};lt.Ccow._lT_VersionNumber.registerClass("lt.Ccow._lT_VersionNumber");lt.Ccow.CcowBase.registerClass("lt.Ccow.CcowBase");lt.Ccow.Subject.registerClass("lt.Ccow.Subject");lt.Ccow.ClientUtils.registerClass("lt.Ccow.ClientUtils",
lt.Ccow.CcowBase);lt.Ccow.ContextManagementRegistry.registerClass("lt.Ccow.ContextManagementRegistry",lt.Ccow.CcowBase);lt.Ccow.AuthenticationRepository.registerClass("lt.Ccow.AuthenticationRepository",lt.Ccow.CcowBase);lt.Ccow.ContextData.registerClass("lt.Ccow.ContextData",lt.Ccow.CcowBase);lt.Ccow.ContextFilter.registerClass("lt.Ccow.ContextFilter",lt.Ccow.CcowBase);lt.Ccow.ContextManager.registerClass("lt.Ccow.ContextManager",lt.Ccow.CcowBase);lt.Ccow.ContextSession.registerClass("lt.Ccow.ContextSession",
lt.Ccow.CcowBase);lt.Ccow.ContextAction.registerClass("lt.Ccow.ContextAction",lt.Ccow.CcowBase);lt.Ccow.Constants.registerClass("lt.Ccow.Constants");lt.Ccow.ContextItem.registerClass("lt.Ccow.ContextItem");lt.Ccow.CcowException.registerClass("lt.Ccow.CcowException");lt.Ccow.ClientContext.registerClass("lt.Ccow.ClientContext",lt.Ccow.CcowBase);lt.Ccow.ContextEventArgs.registerClass("lt.Ccow.ContextEventArgs",lt.LeadEventArgs);lt.Ccow.ContextErrorEventArgs.registerClass("lt.Ccow.ContextErrorEventArgs",
lt.LeadEventArgs);lt.Ccow.SecureContextData.registerClass("lt.Ccow.SecureContextData",lt.Ccow.CcowBase);lt.Ccow.SecureBinding.registerClass("lt.Ccow.SecureBinding",lt.Ccow.CcowBase);lt.Ccow.ImplementationInformation.registerClass("lt.Ccow.ImplementationInformation",lt.Ccow.CcowBase);lt.Ccow.LocateData.registerClass("lt.Ccow.LocateData");lt.Ccow.AuthenticationData.registerClass("lt.Ccow.AuthenticationData");lt.Ccow.ContextChangesData.registerClass("lt.Ccow.ContextChangesData");lt.Ccow.ContextActionData.registerClass("lt.Ccow.ContextActionData");
lt.Ccow.BindingData.registerClass("lt.Ccow.BindingData");lt.Ccow.SecureItemValues.registerClass("lt.Ccow.SecureItemValues");lt.Ccow.WebSocketInfo.registerClass("lt.Ccow.WebSocketInfo");lt.Ccow.ContextManagementRegistryLocator.registerClass("lt.Ccow.ContextManagementRegistryLocator",null,lt.Ccow.ICcowServiceLocator);lt.Ccow.InterfaceInformation.registerClass("lt.Ccow.InterfaceInformation",lt.Ccow.CcowBase);lt.Ccow.ListenerRegistrar.registerClass("lt.Ccow.ListenerRegistrar",lt.Ccow.CcowBase);lt.Ccow._utils.registerClass("lt.Ccow._utils");
lt.Ccow._lT_VersionNumber.l_VER_PRODUCT="LEADTOOLS\u00ae for JavaScript";lt.Ccow._lT_VersionNumber.l_VER_COMPANYNAME_STR="LEAD Technologies, Inc.";lt.Ccow._lT_VersionNumber.l_VER_LEGALTRADEMARKS_STR="LEADTOOLS\u00ae is a trademark of LEAD Technologies, Inc.";lt.Ccow._lT_VersionNumber.l_VER_LEGALCOPYRIGHT_STR="\u00a9 1991-2016 LEAD Technologies, Inc.";lt.Ccow._lT_VersionNumber.l_VER_DLLEXT=".dll";lt.Ccow._lT_VersionNumber.l_VER_EXEEXT=".exe";lt.Ccow._lT_VersionNumber.l_VER_PLATFORM="";lt.Ccow._lT_VersionNumber.l_VER_PLATFORM_FOR=
"";lt.Ccow._lT_VersionNumber.l_VER_PRODUCTNAME_STR="LEADTOOLS\u00ae for JavaScript";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_XXX="Leadtools.Xxx.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_XXX="LEADTOOLS Xxx";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_KERNEL="lt.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_KERNEL="Leadtools";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_CONTROLS="lt.Controls.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_CONTROLS=
"Controls";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_DOCUMENTS_UI="lt.Documents.UI.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_DOCUMENTS_UI="Documents User Interface";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_CONTROLS_MEDICAL="lt.Controls.Medical.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_CONTROLS_MEDICAL="Medical Controls";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_DOCUMENTS="lt.Documents.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_DOCUMENTS=
"Documents";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_ANNOTATIONS_CORE="lt.Annotations.Core.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_ANNOTATIONS_CORE="Annotations Core";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_ANNOTATIONS_AUTOMATION="lt.Annotations.Automation.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_ANNOTATIONS_AUTOMATION="Annotations Automation";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_ANNOTATIONS_DESIGNERS="lt.Annotations.Designers.dll";
lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_ANNOTATIONS_DESIGNERS="Annotations Designers";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_ANNOTATIONS_RENDERING="lt.Annotations.Rendering.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_ANNOTATIONS_RENDERING="Annotations Rendering";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_CCOW="Leadtools.Ccow.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_CCOW="Leadtools CCOW Library";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_ANNOTATIONS_DOCUMENTS=
"Leadtools.Annotations.Documents.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_ANNOTATIONS_DOCUMENTS="Annotations Documents";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_ANNOTATIONS_LEGACY="Leadtools.Annotations.Legacy.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_ANNOTATIONS_LEGACY="Annotations Legacy";lt.Ccow._lT_VersionNumber.l_VER_ORIGINALFILENAME_STR_ANNOTATIONS_JAVASCRIPT="Leadtools.Annotations.JavaScript.dll";lt.Ccow._lT_VersionNumber.l_VER_FILEDESCRIPTION_STR_ANNOTATIONS_JAVASCRIPT=
"JavaScripot Annotations";lt.Ccow._lT_VersionNumber.l_VER_PRODUCTVERSION_DOT_STR="19.0.0.0";lt.Ccow._lT_VersionNumber.l_VER_FILEVERSION_DOT_STR="19.0.0.1";lt.Ccow.Constants.webPassCodeNames=["Technology","PubKeyScheme","PubKeySize","HashAlgo"];lt.Ccow.Constants.webPassCodeValues=["Web","RSA","512","MD5"]})();
