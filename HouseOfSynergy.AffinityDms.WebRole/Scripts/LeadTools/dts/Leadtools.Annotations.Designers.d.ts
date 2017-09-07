//***********************************************************************************************
//   Type definitions for Leadtools.Annotations.Designers.js
//   Updated: 4/12/2016 16:26
//   Version: 19.0.0.5
//   Reference with:
//   /// <reference path="Leadtools.Annotations.Designers.d.ts" />
//   Copyright (c) 1991-2015 All Rights Reserved. LEAD Technologies, Inc.
//   http://www.leadtools.com
//***********************************************************************************************

// Required references
/// <reference path="Leadtools.d.ts"/>
/// <reference path="Leadtools.Annotations.Core.d.ts"/>

declare module lt.Annotations.Designers {

   class AnnDesigner {
      get_isMouseLeftButtonDown(): boolean;
      set_isMouseLeftButtonDown(value: boolean): void;
      get_targetObject(): lt.Annotations.Core.AnnObject;
      set_targetObject(value: lt.Annotations.Core.AnnObject): void;
      get_finalTargetObject(): lt.Annotations.Core.AnnObject;
      get_container(): lt.Annotations.Core.AnnContainer;  // protected
      set_restrictDesigners(value: boolean): void;
      get_restrictDesigners(): boolean;
      get_clipRectangle(): lt.LeadRectD;  // protected
      clipPoint(point: lt.LeadPointD, clipRect: lt.LeadRectD): lt.LeadPointD;  // protected
      getRenderer(): lt.Annotations.Core.IAnnObjectRenderer;
      get_automationControl(): lt.Annotations.Core.IAnnAutomationControl;
      invalidate(rc: lt.LeadRectD): void;
      get_hasStarted(): boolean;
      start(): void;
      end(): void;
      cancel(): void;
      onPointerDoubleClick(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      snapPointToGrid(position: lt.LeadPointD, clipToContainer: boolean): lt.LeadPointD;
      get_snapToGridOptions(): lt.Annotations.Core.AnnSnapToGridOptions;
      set_snapToGridOptions(value: lt.Annotations.Core.AnnSnapToGridOptions): void;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annObject: lt.Annotations.Core.AnnObject);
      isMouseLeftButtonDown: boolean;
      targetObject: lt.Annotations.Core.AnnObject;
      finalTargetObject: lt.Annotations.Core.AnnObject; // read-only
      container: lt.Annotations.Core.AnnContainer; // read-only
      restrictDesigners: boolean;
      clipRectangle: lt.LeadRectD; // read-only
      automationControl: lt.Annotations.Core.IAnnAutomationControl; // read-only
      hasStarted: boolean; // read-only
      snapToGridOptions: lt.Annotations.Core.AnnSnapToGridOptions;
   }

   class AnnDrawDesigner extends AnnDesigner {
      get_operationStatus(): lt.Annotations.Core.AnnDesignerOperationStatus;
      add_draw(value: lt.Annotations.Core.AnnDrawDesignerEventHandler): void;
      remove_draw(value: lt.Annotations.Core.AnnDrawDesignerEventHandler): void;
      start(): void;
      end(): void;
      onDraw(e: lt.Annotations.Core.AnnDrawDesignerEventArgs): void;  // protected
      get_isTargetObjectAdded(): boolean;
      startWorking(): boolean;  // protected
      working(): boolean;  // protected
      cancel(): void;
      endWorking(): boolean;  // protected
      get_extendedMode(): boolean;
      set_extendedMode(value: boolean): void;
      get_extendedModeModifierKey(): lt.Annotations.Core.AnnKeys;
      set_extendedModeModifierKey(value: lt.Annotations.Core.AnnKeys): void;
      get_isExtendedMode(): boolean;  // protected
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annObject: lt.Annotations.Core.AnnObject);
      operationStatus: lt.Annotations.Core.AnnDesignerOperationStatus; // read-only
      isTargetObjectAdded: boolean; // read-only
      extendedMode: boolean;
      extendedModeModifierKey: lt.Annotations.Core.AnnKeys;
      isExtendedMode: boolean; // read-only
      draw: lt.Annotations.Core.AnnDrawDesignerEventType; // read-only
   }

   class AnnCrossProductDrawDesigner extends AnnDrawDesigner {
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annCrossProductObject: lt.Annotations.Core.AnnCrossProductObject);
   }

   class AnnFreehandDrawDesigner extends AnnDrawDesigner {
      get_spacing(): number;
      set_spacing(value: number): void;
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annPolylineObject: lt.Annotations.Core.AnnPolylineObject);
      spacing: number;
   }

   class AnnPolylineDrawDesigner extends AnnDrawDesigner {
      get_firstPointOnClick(): boolean;
      set_firstPointOnClick(value: boolean): void;
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerDoubleClick(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annPolylineObject: lt.Annotations.Core.AnnPolylineObject);
      firstPointOnClick: boolean;
   }

   class AnnLineDrawDesigner extends AnnPolylineDrawDesigner {
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerDoubleClick(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annLineObject: lt.Annotations.Core.AnnPolylineObject);
   }

   class AnnPointDrawDesigner extends AnnDrawDesigner {
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annPointObject: lt.Annotations.Core.AnnPointObject);
   }

   class AnnProtractorDrawDesigner extends AnnDrawDesigner {
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annProtractorObject: lt.Annotations.Core.AnnProtractorObject);
   }

   class AnnRectangleDrawDesigner extends AnnDrawDesigner {
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annRectObject: lt.Annotations.Core.AnnRectangleObject);
   }

   class AnnStickyNoteDrawDesigner extends AnnDrawDesigner {
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annStickyNoteObject: lt.Annotations.Core.AnnStickyNoteObject);
   }

   class AnnTextPointerDrawDesigner extends AnnDrawDesigner {
      get_defaultText(): string;
      set_defaultText(value: string): void;
      cancel(): void;
      endWorking(): boolean;  // protected
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annTextPointerObject: lt.Annotations.Core.AnnTextPointerObject);
      defaultText: string;
   }

   class AnnTextReviewDrawDesigner extends AnnRectangleDrawDesigner {
      get_finalTargetObject(): lt.Annotations.Core.AnnObject;
      endWorking(): boolean;  // protected
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annTextReviewObject: lt.Annotations.Core.AnnTextReviewObject);
      finalTargetObject: lt.Annotations.Core.AnnObject; // read-only
   }

   class AnnEditDesigner extends AnnDesigner {
      add_editContent(value: lt.Annotations.Core.AnnEditContentEventHandler): void;
      remove_editContent(value: lt.Annotations.Core.AnnEditContentEventHandler): void;
      get_rotateModifierKey(): lt.Annotations.Core.AnnKeys;
      set_rotateModifierKey(value: lt.Annotations.Core.AnnKeys): void;
      add_edit(value: lt.Annotations.Core.AnnEditDesignerEventHandler): void;
      remove_edit(value: lt.Annotations.Core.AnnEditDesignerEventHandler): void;
      getRotationReferencePoints(): lt.LeadPointD[];  // protected
      getRotateCenterPoint(): lt.LeadPointD;  // protected
      getRotateGripper(): lt.LeadPointD;
      get_useRotateThumbs(): boolean;
      set_useRotateThumbs(value: boolean): void;
      getThumbLocations(): lt.LeadPointD[];
      get_thumbsHitTestBuffer(): number;
      set_thumbsHitTestBuffer(value: number): void;
      get_workingBuffer(): number;
      set_workingBuffer(value: number): void;
      cancel(): void;
      get_maintainAspectRatio(): boolean;
      set_maintainAspectRatio(value: boolean): void;
      get_operation(): lt.Annotations.Core.AnnEditDesignerOperation;
      set_operation(value: lt.Annotations.Core.AnnEditDesignerOperation): void;
      get_moveThumbIndex(): number;
      get_showThumbs(): boolean;
      set_showThumbs(value: boolean): void;
      get_ignoreHitTestThumbs(): boolean;
      set_ignoreHitTestThumbs(value: boolean): void;
      get_isModified(): boolean;
      set_isModified(value: boolean): void;
      start(): void;
      end(): void;
      hitTestThumbs(pt: lt.LeadPointD): boolean;
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      getIntersectionWithReferencePoints(): lt.LeadPointD;  // protected
      startWorking(operation: lt.Annotations.Core.AnnEditDesignerOperation, thumbIndex: number): boolean;  // protected
      working(): boolean;  // protected
      endWorking(): boolean;  // protected
      onEdit(e: lt.Annotations.Core.AnnEditDesignerEventArgs): void;  // protected
      move(offsetX: number, offsetY: number): void;  // protected
      callMove(offsetX: number, offsetY: number): void;
      snapObjectToGrid(targetObject: lt.Annotations.Core.AnnObject, clipToContainer: boolean): void;
      invalidate(rc: lt.LeadRectD): void;
      moveThumb(thumbIndex: number, offset: lt.LeadPointD): void;  // protected
      resetRotateThumbs(): void;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerDoubleClick(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annObject: lt.Annotations.Core.AnnObject);
      rotateModifierKey: lt.Annotations.Core.AnnKeys;
      useRotateThumbs: boolean;
      thumbsHitTestBuffer: number;
      workingBuffer: number;
      maintainAspectRatio: boolean;
      operation: lt.Annotations.Core.AnnEditDesignerOperation;
      moveThumbIndex: number; // read-only
      showThumbs: boolean;
      ignoreHitTestThumbs: boolean;
      isModified: boolean;
      editContent: lt.Annotations.Core.AnnEditContentEventType; // read-only
      edit: lt.Annotations.Core.AnnEditDesignerEventType; // read-only
   }

   class AnnCrossProductEditDesigner extends AnnEditDesigner {
      getThumbLocations(): lt.LeadPointD[];
      moveThumb(thumbIndex: number, offset: lt.LeadPointD): void;  // protected
      move(offsetX: number, offsetY: number): void;  // protected
      getRotationReferencePoints(): lt.LeadPointD[];  // protected
      snapObjectToGrid(targetObject: lt.Annotations.Core.AnnObject, clipToContainer: boolean): void;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annCrossProductObject: lt.Annotations.Core.AnnCrossProductObject);
   }

   class AnnPointEditDesigner extends AnnEditDesigner {
      getRotationReferencePoints(): lt.LeadPointD[];  // protected
      getThumbLocations(): lt.LeadPointD[];
      moveThumb(thumbIndex: number, offset: lt.LeadPointD): void;  // protected
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annPointObject: lt.Annotations.Core.AnnPointObject);
   }

   class AnnPolylineEditDesigner extends AnnEditDesigner {
      get_thumbsGap(): number;
      set_thumbsGap(value: number): void;
      getThumbLocations(): lt.LeadPointD[];
      moveThumb(thumbIndex: number, offset: lt.LeadPointD): void;  // protected
      getRotateCenterPoint(): lt.LeadPointD;  // protected
      getRotationReferencePoints(): lt.LeadPointD[];  // protected
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annPolylineObject: lt.Annotations.Core.AnnPolylineObject);
      thumbsGap: number;
   }

   class AnnRectangleEditDesigner extends AnnEditDesigner {
      get_minimumSize(): lt.LeadSizeD;
      set_minimumSize(value: lt.LeadSizeD): void;
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      getThumbLocations(): lt.LeadPointD[];
      moveThumb(thumbIndex: number, offset: lt.LeadPointD): void;  // protected
      snapObjectToGrid(targetObject: lt.Annotations.Core.AnnObject, clipToContainer: boolean): void;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annRectangleObject: lt.Annotations.Core.AnnRectangleObject);
      minimumSize: lt.LeadSizeD;
   }

   class AnnSelectionEditDesigner extends AnnRectangleEditDesigner {
      start(): void;
      get_maintainAspectRatio(): boolean;
      set_maintainAspectRatio(value: boolean): void;
      getThumbLocations(): lt.LeadPointD[];
      moveThumb(thumbIndex: number, offset: lt.LeadPointD): void;  // protected
      end(): void;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annSelectionObject: lt.Annotations.Core.AnnSelectionObject);
      maintainAspectRatio: boolean;
   }

   class AnnTextEditDesigner extends AnnRectangleEditDesigner {
      add_editText(value: lt.Annotations.Core.AnnEditTextEventHandler): void;
      remove_editText(value: lt.Annotations.Core.AnnEditTextEventHandler): void;
      get_acceptsReturn(): boolean;
      set_acceptsReturn(value: boolean): void;
      get_autoSizeAfterEdit(): boolean;
      set_autoSizeAfterEdit(value: boolean): void;
      onPointerDoubleClick(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annTextObject: lt.Annotations.Core.AnnTextObject);
      acceptsReturn: boolean;
      autoSizeAfterEdit: boolean;
      editText: lt.Annotations.Core.AnnEditTextEventType; // read-only
   }

   class AnnTextPointerEditDesigner extends AnnTextEditDesigner {
      getThumbLocations(): lt.LeadPointD[];
      moveThumb(thumbIndex: number, offset: lt.LeadPointD): void;  // protected
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annTextPointerObject: lt.Annotations.Core.AnnTextPointerObject);
   }

   class AnnTextReviewEditDesigner extends AnnEditDesigner {
      move(offsetX: number, offsetY: number): void;  // protected
      getThumbLocations(): lt.LeadPointD[];
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annObject: lt.Annotations.Core.AnnObject);
   }

   class AnnRunDesigner extends AnnDesigner {
      add_run(value: lt.Annotations.Core.AnnRunDesignerEventHandler): void;
      remove_run(value: lt.Annotations.Core.AnnRunDesignerEventHandler): void;
      get_hitTestBuffer(): number;
      set_hitTestBuffer(value: number): void;
      cancel(): void;
      startWorking(): boolean;  // protected
      working(): boolean;  // protected
      endWorking(): boolean;  // protected
      onRun(e: lt.Annotations.Core.AnnRunDesignerEventArgs): void;  // protected
      onPointerDown(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerMove(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerUp(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      onPointerDoubleClick(sender: lt.Annotations.Core.AnnContainer, e: lt.Annotations.Core.AnnPointerEventArgs): boolean;
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annObject: lt.Annotations.Core.AnnObject);
      hitTestBuffer: number;
      run: lt.Annotations.Core.AnnRunDesignerEventType; // read-only
   }

   class AnnTextRollupRunDesigner extends AnnRunDesigner {
      onRun(e: lt.Annotations.Core.AnnRunDesignerEventArgs): void;  // protected
      constructor(automationControl: lt.Annotations.Core.IAnnAutomationControl, container: lt.Annotations.Core.AnnContainer, annTextRollupObject: lt.Annotations.Core.AnnTextRollupObject);
   }
}
