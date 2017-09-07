//***********************************************************************************************
//   Type definitions for Leadtools.Ccow.js
//   Updated: 3/23/2016 10:37
//   Version: 19.0.0.1
//   Reference with:
//   /// <reference path="Leadtools.Ccow.d.ts" />
//   Copyright (c) 1991-2015 All Rights Reserved. LEAD Technologies, Inc.
//   http://www.leadtools.com
//***********************************************************************************************

// Required references
/// <reference path="Leadtools.d.ts"/>

declare module lt.Ccow {

   class CcowBase {
   }

   class Subject {
      get_name(): string;
      set_name(value: string): void;
      get_items(): lt.LeadCollection;
      toItemNameArray(): string[];
      toItemValueArray(): any[];
      hasItem(itemName: string): boolean;
      getItem(itemName: string): ContextItem;
      constructor();
      name: string;
      items: lt.LeadCollection; // read-only
   }

   class ClientUtils extends CcowBase {
      getEncodedHashKey(hashString: string): string;
      getEncodedPublicKey(applicationName: string): string;
      getEncodedSignKey(applicationName: string, messageDigest: string): string;
      getWebSocketInfo(): WebSocketInfo;
      ping(): boolean;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ContextManagementRegistry extends CcowBase {
      locate(componentName: string, version: string, descriptiveData: string, contextParticipant: string): LocateData;
      constructor(locator: ContextManagementRegistryLocator);
   }

   class AuthenticationRepository extends CcowBase {
      connect(applicationName: string): number;
      disconnect(bindingCoupon: number): void;
      setAuthenticationData(coupon: number, logonName: string, dataFormat: string, userData: string, appSignature: string): void;
      deleteAuthenticationData(coupon: number, logonName: string, dataFormat: string, appSignature: string): void;
      getAuthenticationData(coupon: number, logonName: string, dataFormat: string, appSignature: string): AuthenticationData;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ContextData extends CcowBase {
      getItemNames(contextCoupon: number): string[];
      setItemValues(participantCoupon: number, itemNames: string[], itemValues: any[], contextCoupon: number): void;
      getItemValues(itemNames: string[], onlyChanges: boolean, contextCoupon: number): any[];
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ContextFilter extends CcowBase {
      setSubjectsOfInterest(participantCoupon: number, subjectNames: string[]): string[];
      getSubjectsOfInterest(participantCoupon: number): string[];
      clearFilter(participantCoupon: number): void;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ContextManager extends CcowBase {
      getMostRecentContextCoupon(): number;
      joinCommonContext(applicationName: string, contextParticipant: string, survey: boolean, wait: boolean): number;
      leaveCommonContext(participantCoupon: number): void;
      startContextChanges(participantCoupon: number): number;
      endContextChanges(contextCoupon: number): ContextChangesData;
      undoContextChanges(contextCoupon: number): void;
      publishChangesDecision(contextCoupon: number, decision: string): string[];
      suspendParticipation(participantCoupon: number): void;
      resumeParticipation(participantCoupon: number, wait: boolean): void;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ContextSession extends CcowBase {
      create(): string;
      activate(participantCoupon: number, cmToActivate: string, nonce: string, appSignature: string): void;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ContextAction extends CcowBase {
      perform(cpCallBackURL: string, cpErrorURL: string, participantCoupon: number, inputNames: string[], inputValues: string[], appSignature: string): ContextActionData;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class Constants {
      constructor();
      static webPassCodeNames: string[];
      static webPassCodeValues: string[];
   }

   class ContextItem {
      get_subjectDescriptor(): string;
      set_subjectDescriptor(value: string): void;
      get_subject(): string;
      set_subject(value: string): void;
      get_role(): string;
      set_role(value: string): void;
      get_nameDescriptor(): string;
      set_nameDescriptor(value: string): void;
      get_name(): string;
      set_name(value: string): void;
      get_suffix(): string;
      set_suffix(value: string): void;
      get_value(): any;
      set_value(value: any): void;
      toString(): string;
      equals(obj: any): boolean;
      constructor(item: string);
      subjectDescriptor: string;
      subject: string;
      role: string;
      nameDescriptor: string;
      name: string;
      suffix: string;
      value: any;
   }

   class ClientContext extends CcowBase {
      dispose(): void;
      add_error(value: ContextErrorEventHandler): void;
      remove_error(value: ContextErrorEventHandler): void;
      add_contextChangesPending(value: ContextEventHandler): void;
      remove_contextChangesPending(value: ContextEventHandler): void;
      add_contextChangesAccepted(value: ContextEventHandler): void;
      remove_contextChangesAccepted(value: ContextEventHandler): void;
      add_contextChangesCanceled(value: ContextEventHandler): void;
      remove_contextChangesCanceled(value: ContextEventHandler): void;
      add_commonContextTerminated(value: lt.LeadEventHandler): void;
      remove_commonContextTerminated(value: lt.LeadEventHandler): void;
      add_ping(value: lt.LeadEventHandler): void;
      remove_ping(value: lt.LeadEventHandler): void;
      onMessage(message: string): void;
      constructor(serviceLocator: ICcowServiceLocator, id: string);
      error: ContextErrorEventType; // read-only
      contextChangesPending: ContextEventType; // read-only
      contextChangesAccepted: ContextEventType; // read-only
      contextChangesCanceled: ContextEventType; // read-only
      commonContextTerminated: lt.LeadEventType; // read-only
      ping: lt.LeadEventType; // read-only
   }

   interface ContextEventHandler {
      (sender: any, e: ContextEventArgs): void;
   }

   class ContextEventType extends lt.LeadEvent {
      add(value: ContextEventHandler): ContextEventHandler;
      remove(value: ContextEventHandler): void;
   }

   class ContextEventArgs extends lt.LeadEventArgs {
      get_contextCoupon(): number;
      constructor(contextCoupon: number);
      contextCoupon: number; // read-only
   }

   interface ContextErrorEventHandler {
      (sender: any, e: ContextErrorEventArgs): void;
   }

   class ContextErrorEventType extends lt.LeadEvent {
      add(value: ContextErrorEventHandler): ContextErrorEventHandler;
      remove(value: ContextErrorEventHandler): void;
   }

   class ContextErrorEventArgs extends lt.LeadEventArgs {
      get_error(): Error;
      constructor(error: Error);
      error: Error; // read-only
   }

   class SecureContextData extends CcowBase {
      getItemNames(contextCoupon: number): string[];
      setItemValues(participantCoupon: number, itemNames: string[], itemValues: any[], contextCoupon: number, appSignature: string): void;
      getItemValues(participantCoupon: number, itemNames: string[], onlyChanges: boolean, contextCoupon: number, appSignature: string): SecureItemValues;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class SecureBinding extends CcowBase {
      initializeBinding(bindeeCoupon: number, propertyNames: string[], propertyValues: any[]): BindingData;
      finalizeBinding(bindeeCoupon: number, bindeePublicKey: string, mac: string): string[];
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ImplementationInformation extends CcowBase {
      get_componentName(): string;
      get_revMajorNum(): string;
      get_revMinorNum(): string;
      get_partNumber(): string;
      get_manufacturer(): string;
      get_targetOS(): string;
      get_targetOSRev(): string;
      get_whenInstalled(): string;
      constructor(serviceLocator: ICcowServiceLocator);
      componentName: string; // read-only
      revMajorNum: string; // read-only
      revMinorNum: string; // read-only
      partNumber: string; // read-only
      manufacturer: string; // read-only
      targetOS: string; // read-only
      targetOSRev: string; // read-only
      whenInstalled: string; // read-only
   }

   class LocateData {
      get_componentUrl(): string;
      get_componentParameters(): string;
      get_site(): string;
      componentUrl: string; // read-only
      componentParameters: string; // read-only
      site: string; // read-only
   }

   class AuthenticationData {
      get_userData(): string;
      get_repositorySignature(): string;
      userData: string; // read-only
      repositorySignature: string; // read-only
   }

   class ContextChangesData {
      get_noContinue(): boolean;
      get_responses(): string[];
      noContinue: boolean; // read-only
      responses: string[]; // read-only
   }

   class ContextActionData {
      get_actionCoupon(): number;
      get_outputNames(): string[];
      get_outputValues(): string[];
      get_managerSignature(): string;
      actionCoupon: number; // read-only
      outputNames: string[]; // read-only
      outputValues: string[]; // read-only
      managerSignature: string; // read-only
   }

   class BindingData {
      get_binderPublicKey(): string;
      get_mac(): string;
      binderPublicKey: string; // read-only
      mac: string; // read-only
   }

   class SecureItemValues {
      get_itemValues(): any[];
      get_managerSignature(): string;
      itemValues: any[]; // read-only
      managerSignature: string; // read-only
   }

   class WebSocketInfo {
      get_portNumber(): number;
      get_address(): string;
      portNumber: number; // read-only
      address: string; // read-only
   }

   interface ICcowServiceLocator {
      send(data: string): string;
   }

   class ContextManagementRegistryLocator {
      send(data: string): string;
      onSendData(url: string, data: string): string;  // protected
      constructor();
   }

   class InterfaceInformation extends CcowBase {
      interrogate(interfaceName: string): boolean;
      constructor(serviceLocator: ICcowServiceLocator);
   }

   class ListenerRegistrar extends CcowBase {
      register(url: string, participantCoupon: number): number;
      unregister(url: string): void;
      constructor(serviceLocator: ICcowServiceLocator);
   }
}
