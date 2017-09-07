//***********************************************************************************************
//   Type definitions for Leadtools.Annotations.Documents.js
//   Updated: 4/10/2016 09:54
//   Version: 19.0.0.2
//   Reference with:
//   /// <reference path="Leadtools.Annotations.Documents.d.ts" />
//   Copyright (c) 1991-2015 All Rights Reserved. LEAD Technologies, Inc.
//   http://www.leadtools.com
//***********************************************************************************************

// Required references
/// <reference path="Leadtools.d.ts"/>
/// <reference path="Leadtools.Annotations.Core.d.ts"/>

declare module lt.Annotations.Documents {

   interface IAnnBatesElement {
      get_friendlyName(): string;
      set_friendlyName(value: string): void;
      asString(): string;
      clone(): IAnnBatesElement;
      add_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      friendlyName: string;
      propertyChanged: lt.Annotations.Core.AnnPropertyChangedEventType; // read-only
   }

   class AnnBatesDateTime {
      add_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      get_currentDateTime(): Date;
      set_currentDateTime(value: Date): void;
      get_format(): string;
      set_format(value: string): void;
      get_kind(): AnnDateTimeKind;
      set_kind(value: AnnDateTimeKind): void;
      asString(): string;
      get_friendlyName(): string;
      set_friendlyName(value: string): void;
      toString(): string;
      onPropertyChanged(e: lt.Annotations.Core.AnnPropertyChangedEventArgs): void;
      clone(): IAnnBatesElement;
      constructor();
      currentDateTime: Date;
      format: string;
      kind: AnnDateTimeKind;
      friendlyName: string;
      propertyChanged: lt.Annotations.Core.AnnPropertyChangedEventType; // read-only
   }

   class AnnBatesNumber {
      add_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      get_numberOfDigits(): number;
      set_numberOfDigits(value: number): void;
      get_startNumber(): number;
      set_startNumber(value: number): void;
      get_autoIncrement(): boolean;
      set_autoIncrement(value: boolean): void;
      get_prefixText(): string;
      set_prefixText(value: string): void;
      get_suffixText(): string;
      set_suffixText(value: string): void;
      get_useAllDigits(): boolean;
      set_useAllDigits(value: boolean): void;
      asString(): string;
      get_friendlyName(): string;
      set_friendlyName(value: string): void;
      toString(): string;
      onPropertyChanged(e: lt.Annotations.Core.AnnPropertyChangedEventArgs): void;
      clone(): IAnnBatesElement;
      constructor();
      numberOfDigits: number;
      startNumber: number;
      autoIncrement: boolean;
      prefixText: string;
      suffixText: string;
      useAllDigits: boolean;
      friendlyName: string;
      propertyChanged: lt.Annotations.Core.AnnPropertyChangedEventType; // read-only
   }

   class AnnBatesStamp {
      add_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      get_logo(): AnnBatesStampLogo;
      get_friendlyName(): string;
      set_friendlyName(value: string): void;
      get_font(): lt.Annotations.Core.AnnFont;
      set_font(value: lt.Annotations.Core.AnnFont): void;
      get_foreground(): lt.Annotations.Core.AnnBrush;
      set_foreground(value: lt.Annotations.Core.AnnBrush): void;
      get_horizontalAlignment(): lt.Annotations.Core.AnnHorizontalAlignment;
      set_horizontalAlignment(value: lt.Annotations.Core.AnnHorizontalAlignment): void;
      get_verticalAlignment(): lt.Annotations.Core.AnnVerticalAlignment;
      set_verticalAlignment(value: lt.Annotations.Core.AnnVerticalAlignment): void;
      get_elements(): AnnBatesElementCollection;
      asString(container: lt.Annotations.Core.AnnContainer): string;
      toString(): string;
      clone(): AnnBatesStamp;
      dispose(): void;
      onPropertyChanged(e: lt.Annotations.Core.AnnPropertyChangedEventArgs): void;
      constructor();
      logo: AnnBatesStampLogo; // read-only
      friendlyName: string;
      font: lt.Annotations.Core.AnnFont;
      foreground: lt.Annotations.Core.AnnBrush;
      horizontalAlignment: lt.Annotations.Core.AnnHorizontalAlignment;
      verticalAlignment: lt.Annotations.Core.AnnVerticalAlignment;
      elements: AnnBatesElementCollection; // read-only
      propertyChanged: lt.Annotations.Core.AnnPropertyChangedEventType; // read-only
   }

   class AnnBatesStampComposer {
      static get_renderingEngine(): lt.Annotations.Core.AnnRenderingEngine;
      static set_renderingEngine(value: lt.Annotations.Core.AnnRenderingEngine): void;
      get_stamps(): AnnBatesStampCollection;
      get_targetContainers(): lt.Annotations.Core.AnnContainerCollection;
      dispose(): void;
      save(composer: AnnBatesStampComposer): string;
      static loadFromXmlDocument(document: Document): AnnBatesStampComposer;
      static load(xmlData: string): AnnBatesStampComposer;
      constructor();
      static renderingEngine: lt.Annotations.Core.AnnRenderingEngine;
      stamps: AnnBatesStampCollection; // read-only
      targetContainers: lt.Annotations.Core.AnnContainerCollection; // read-only
   }

   class AnnBatesStampLogo {
      add_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      get_picture(): lt.Annotations.Core.AnnPicture;
      set_picture(value: lt.Annotations.Core.AnnPicture): void;
      get_logoRect(): lt.LeadRectD;
      set_logoRect(value: lt.LeadRectD): void;
      get_stretchLogo(): boolean;
      set_stretchLogo(value: boolean): void;
      get_angle(): number;
      set_angle(value: number): void;
      get_opacity(): number;
      set_opacity(value: number): void;
      get_text(): string;
      set_text(value: string): void;
      get_font(): lt.Annotations.Core.AnnFont;
      set_font(value: lt.Annotations.Core.AnnFont): void;
      onPropertyChanged(e: lt.Annotations.Core.AnnPropertyChangedEventArgs): void;
      clone(): AnnBatesStampLogo;
      constructor();
      picture: lt.Annotations.Core.AnnPicture;
      logoRect: lt.LeadRectD;
      stretchLogo: boolean;
      angle: number;
      opacity: number;
      text: string;
      font: lt.Annotations.Core.AnnFont;
      propertyChanged: lt.Annotations.Core.AnnPropertyChangedEventType; // read-only
   }

   class AnnBatesStampTranslator {
      get_expressionStartSymbol(): string;
      set_expressionStartSymbol(value: string): void;
      get_expressionEndSymbol(): string;
      set_expressionEndSymbol(value: string): void;
      get_expressionSeparatingSymbol(): string;
      set_expressionSeparatingSymbol(value: string): void;
      readFromString(elementsExpression: string): IAnnBatesElement[];
      writeElementToString(element: IAnnBatesElement): string;
      writeElementsToString(elements: IAnnBatesElement[]): string;
      constructor();
      expressionStartSymbol: string;
      expressionEndSymbol: string;
      expressionSeparatingSymbol: string;
   }

   class AnnBatesText {
      add_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: lt.Annotations.Core.AnnPropertyChangedEventHandler): void;
      get_text(): string;
      set_text(value: string): void;
      static create(text: string): AnnBatesText;
      asString(): string;
      get_friendlyName(): string;
      set_friendlyName(value: string): void;
      toString(): string;
      onPropertyChanged(e: lt.Annotations.Core.AnnPropertyChangedEventArgs): void;
      clone(): IAnnBatesElement;
      text: string;
      friendlyName: string;
      propertyChanged: lt.Annotations.Core.AnnPropertyChangedEventType; // read-only
   }

   enum AnnDateTimeKind {
      utc = 0,
      local = 1
   }

   class AnnBatesElementCollection {
      get_count(): number;
      clear(): void;
      remove(batesElement: IAnnBatesElement): void;
      add(batesElement: IAnnBatesElement): void;
      contains(batesElement: IAnnBatesElement): boolean;
      getEnumerator(): any;
      get_item(i: number): IAnnBatesElement;
      set_item(i: number, value: IAnnBatesElement): void;
      toArray(): IAnnBatesElement[];
      insertItem(index: number, item: IAnnBatesElement): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: lt.Annotations.Core.AnnNotifyCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(annBatesElement: IAnnBatesElement): void;  // protected
      setItem(index: number, item: IAnnBatesElement): void;  // protected
      clearItems(): void;  // protected
      indexOf(annBatesElement: IAnnBatesElement): number;
      add_collectionChanged(value: lt.Annotations.Core.AnnNotifyCollectionChangedEventHandler): void;
      remove_collectionChanged(value: lt.Annotations.Core.AnnNotifyCollectionChangedEventHandler): void;
      sendToBack(annBatesElement: IAnnBatesElement, last: boolean): void;
      bringToFront(annBatesElement: IAnnBatesElement, first: boolean): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: lt.Annotations.Core.AnnNotifyCollectionChangedEventType; // read-only
   }

   class AnnBatesStampCollection {
      get_count(): number;
      clear(): void;
      remove(annBatesStamp: AnnBatesStamp): void;
      add(annBatesStamp: AnnBatesStamp): void;
      contains(annBatesStamp: AnnBatesStamp): boolean;
      getEnumerator(): any;
      get_item(i: number): AnnBatesStamp;
      set_item(i: number, value: AnnBatesStamp): void;
      toArray(): AnnBatesStamp[];
      insertItem(index: number, item: AnnBatesStamp): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: lt.Annotations.Core.AnnNotifyCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(annBatesStamp: AnnBatesStamp): void;  // protected
      setItem(index: number, item: AnnBatesStamp): void;  // protected
      clearItems(): void;  // protected
      indexOf(annBatesStamp: AnnBatesStamp): number;
      add_collectionChanged(value: lt.Annotations.Core.AnnNotifyCollectionChangedEventHandler): void;
      remove_collectionChanged(value: lt.Annotations.Core.AnnNotifyCollectionChangedEventHandler): void;
      sendToBack(annBatesStamp: AnnBatesStamp, last: boolean): void;
      bringToFront(annBatesStamp: AnnBatesStamp, first: boolean): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: lt.Annotations.Core.AnnNotifyCollectionChangedEventType; // read-only
   }
}
