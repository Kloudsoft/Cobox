//***********************************************************************************************
//   Type definitions for Leadtools.Annotations.Rendering.JavaScript.js
//   Updated: 4/14/2016 22:06
//   Version: 19.0.0.38
//   Reference with:
//   /// <reference path="Leadtools.Annotations.Rendering.JavaScript.d.ts" />
//   Copyright (c) 1991-2015 All Rights Reserved. LEAD Technologies, Inc.
//   http://www.leadtools.com
//***********************************************************************************************

// Required references
/// <reference path="Leadtools.d.ts"/>
/// <reference path="Leadtools.Annotations.Core.d.ts"/>

declare module lt.Annotations.Rendering {

   class AnnHtml5RenderingEngine extends lt.Annotations.Core.AnnRenderingEngine {
      get_context(): CanvasRenderingContext2D;
      set_context(value: CanvasRenderingContext2D): void;
      attach(container: lt.Annotations.Core.AnnContainer, context: CanvasRenderingContext2D): void;
      detach(): void;
      static setFont(context: CanvasRenderingContext2D, font: lt.Annotations.Core.AnnFont): void;
      measureString(text: string, font: lt.Annotations.Core.AnnFont): lt.LeadSizeD;
      static measureTextHeight(context: CanvasRenderingContext2D, font: lt.Annotations.Core.AnnFont): number;
      static setFill(context: CanvasRenderingContext2D, brush: lt.Annotations.Core.AnnBrush): void;
      static setFillOpacityAndBounds(context: CanvasRenderingContext2D, brush: lt.Annotations.Core.AnnBrush, opacity: number, brushBounds: lt.LeadRectD): void;
      static setFillWithOpacity(context: CanvasRenderingContext2D, brush: lt.Annotations.Core.AnnBrush, opacity: number): void;
      static setStroke(context: CanvasRenderingContext2D, stroke: lt.Annotations.Core.AnnStroke): void;
      static setStrokeWithOpacity(context: CanvasRenderingContext2D, stroke: lt.Annotations.Core.AnnStroke, opacity: number): void;
      static drawCurve(context: CanvasRenderingContext2D, points: lt.LeadPointD[], tension: number): void;
      static drawClosedCurve(context: CanvasRenderingContext2D, points: lt.LeadPointD[], tension: number): void;
      static drawEllipse(context: CanvasRenderingContext2D, rc: lt.LeadRectD): void;
      drawPicture(picture: lt.Annotations.Core.AnnPicture, rc: lt.LeadRectD, annObject: lt.Annotations.Core.AnnObject): void;
      onLoadPicture(e: lt.Annotations.Core.AnnLoadPictureEventArgs): void;
      static getTextSize(text: string, annFont: lt.Annotations.Core.AnnFont, layoutArea: lt.LeadSizeD): lt.LeadSizeD;
      renderGrid(runMode: boolean, container: lt.Annotations.Core.AnnContainer): void;
      constructor();
      context: CanvasRenderingContext2D;
   }

   class AnnSvgRenderingEngine extends lt.Annotations.Core.AnnRenderingEngine {
      static exportSvg(container: lt.Annotations.Core.AnnContainer, mapper: lt.Annotations.Core.AnnContainerMapper, renderers: { [key: number]: lt.Annotations.Core.IAnnObjectRenderer }): string;
      get_element(): HTMLElement;
      renderGrid(runMode: boolean, container: lt.Annotations.Core.AnnContainer): void;
      render(clipRect: lt.LeadRectD, clear: boolean): void;
      measureString(text: string, font: lt.Annotations.Core.AnnFont): lt.LeadSizeD;
      attach(container: lt.Annotations.Core.AnnContainer, element: HTMLElement): void;
      detach(): void;
      get_stateless(): boolean;
      constructor();
      element: HTMLElement; // read-only
      stateless: boolean; // read-only
   }

   class AnnCrossProductObjectRenderer extends AnnPolyRulerObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnCurveObjectRenderer extends AnnPolylineObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnEllipseObjectRenderer extends AnnObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnFreehandHotspotObjectRenderer extends AnnPolylineObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnHiliteObjectRenderer extends AnnRectangleObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnHotspotObjectRenderer extends AnnRectangleObjectRenderer {
      get_showAtRunMode(): boolean;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
      showAtRunMode: boolean; // read-only
   }

   class AnnImageObjectRenderer extends AnnRectangleObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnLabelRenderer {
      get_rotateWithParent(): boolean;
      set_rotateWithParent(value: boolean): void;
      initialize(renderingEngine: lt.Annotations.Core.AnnRenderingEngine): void;
      get_offsetHeight(): boolean;
      set_offsetHeight(value: boolean): void;
      get_renderingEngine(): lt.Annotations.Core.AnnRenderingEngine;
      getBounds(mapper: lt.Annotations.Core.AnnContainerMapper, label: lt.Annotations.Core.AnnLabel, operations: lt.Annotations.Core.AnnFixedStateOperations): lt.LeadRectD;
      renderLabel(mapper: lt.Annotations.Core.AnnContainerMapper, label: lt.Annotations.Core.AnnLabel, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      constructor();
      rotateWithParent: boolean;
      offsetHeight: boolean;
      renderingEngine: lt.Annotations.Core.AnnRenderingEngine; // read-only
   }

   class AnnNoteObjectRenderer extends AnnTextObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnObjectRenderer {
      get_clipPath(): boolean;  // protected
      set_clipPath(value: boolean): void;  // protected
      beginClipPath(): any;  // protected
      endClipPath(state: any): void;  // protected
      addObject(annObject: lt.Annotations.Core.AnnObject): void;
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      get_locationsThumbStyle(): lt.Annotations.Core.IAnnThumbStyle;
      set_locationsThumbStyle(value: lt.Annotations.Core.IAnnThumbStyle): void;
      get_rotateCenterThumbStyle(): lt.Annotations.Core.IAnnThumbStyle;
      set_rotateCenterThumbStyle(value: lt.Annotations.Core.IAnnThumbStyle): void;
      get_rotateGripperThumbStyle(): lt.Annotations.Core.IAnnThumbStyle;
      set_rotateGripperThumbStyle(value: lt.Annotations.Core.IAnnThumbStyle): void;
      initialize(renderingEngine: lt.Annotations.Core.AnnRenderingEngine): void;
      renderLocked(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderSelection(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderAlignmentTarget(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderContent(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      renderThumbs(mapper: lt.Annotations.Core.AnnContainerMapper, thumbLocations: lt.LeadPointD[], operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      renderRotatePointThumbs(mapper: lt.Annotations.Core.AnnContainerMapper, rotateCenterLocation: lt.LeadPointD, rotateGripperLocation: lt.LeadPointD, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      getRenderPoints(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): lt.LeadPointD[];
      get_renderingEngine(): lt.Annotations.Core.AnnRenderingEngine;
      get_labelRenderer(): lt.Annotations.Core.IAnnLabelRenderer;
      set_labelRenderer(value: lt.Annotations.Core.IAnnLabelRenderer): void;
      constructor();
      clipPath: boolean;
      locationsThumbStyle: lt.Annotations.Core.IAnnThumbStyle;
      rotateCenterThumbStyle: lt.Annotations.Core.IAnnThumbStyle;
      rotateGripperThumbStyle: lt.Annotations.Core.IAnnThumbStyle;
      renderingEngine: lt.Annotations.Core.AnnRenderingEngine; // read-only
      labelRenderer: lt.Annotations.Core.IAnnLabelRenderer;
   }

   class AnnPointerObjectRenderer extends AnnPolylineObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnPointObjectRenderer extends AnnObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnPolylineObjectRenderer extends AnnObjectRenderer {
      set_useSplineMode(value: boolean): void;
      get_useSplineMode(): boolean;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
      useSplineMode: boolean;
   }

   class AnnPolyRulerObjectRenderer extends AnnPolylineObjectRenderer {
      drawTickMarks(mapper: lt.Annotations.Core.AnnContainerMapper, startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, tickMarkLength: lt.LeadLengthD, unit: lt.Annotations.Core.AnnUnit, stroke: lt.Annotations.Core.AnnStroke, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      drawGauge(mapper: lt.Annotations.Core.AnnContainerMapper, startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, gaugeLength: lt.LeadLengthD, stroke: lt.Annotations.Core.AnnStroke, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      drawLengthText(mapper: lt.Annotations.Core.AnnContainerMapper, label: lt.Annotations.Core.AnnLabel, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnProtractorObjectRenderer extends AnnPolyRulerObjectRenderer {
      getAngleText(angle: number, precision: number, unit: lt.Annotations.Core.AnnAngularUnit, unitsAbbreviation: { [key: number]: string }): string;
      getPoint(size: lt.LeadSizeD, firstPoint: lt.LeadPointD, centerPoint: lt.LeadPointD, secondPoint: lt.LeadPointD, angle1: number, angle2: number, acute: boolean, arcRadius: number, unit: lt.Annotations.Core.AnnAngularUnit): lt.LeadPointD;
      drawArc(mapper: lt.Annotations.Core.AnnContainerMapper, center: lt.LeadPointD, startAngle: number, sweepAngle: number, radius: lt.LeadLengthD, stroke: lt.Annotations.Core.AnnStroke, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      drawAngleText(mapper: lt.Annotations.Core.AnnContainerMapper, label: lt.Annotations.Core.AnnLabel, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnRectangleObjectRenderer extends AnnObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnRubberStampObjectRenderer extends AnnRectangleObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnStampObjectRenderer extends AnnTextObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnStickyNoteObjectRenderer extends AnnObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderSelection(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderContent(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      constructor();
   }

   class AnnTextObjectRenderer extends AnnRectangleObjectRenderer {
      get_flipReverseText(): boolean;
      set_flipReverseText(value: boolean): void;
      get_fixedPadding(): boolean;
      set_fixedPadding(value: boolean): void;
      getTextSize(text: string, annFont: lt.Annotations.Core.AnnFont, layoutArea: lt.LeadSizeD): lt.LeadSizeD;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
      flipReverseText: boolean;
      fixedPadding: boolean;
   }

   class AnnTextPointerObjectRenderer extends AnnTextObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnTextRedactionObjectRenderer extends AnnTextReviewObjectRenderer {
      renderShape(mapper: lt.Annotations.Core.AnnContainerMapper, annTextReviewObject: lt.Annotations.Core.AnnTextReviewObject, context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      constructor();
   }

   class AnnTextReviewObjectRenderer extends AnnObjectRenderer {
      renderShape(mapper: lt.Annotations.Core.AnnContainerMapper, annTextReviewObject: lt.Annotations.Core.AnnTextReviewObject, context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderSelection(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderContent(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      constructor();
   }

   class AnnTextHiliteObjectRenderer extends AnnTextReviewObjectRenderer {
      renderShape(mapper: lt.Annotations.Core.AnnContainerMapper, annTextReviewObject: lt.Annotations.Core.AnnTextReviewObject, context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      constructor();
   }

   class AnnTextRollupObjectRenderer extends AnnNoteObjectRenderer {
      getRenderPoints(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): lt.LeadPointD[];
      constructor();
   }

   class AnnTextStrikeoutObjectRenderer extends AnnTextReviewObjectRenderer {
      get_thickness(): number;
      set_thickness(value: number): void;
      get_position(): number;
      set_position(value: number): void;
      renderShape(mapper: lt.Annotations.Core.AnnContainerMapper, annTextReviewObject: lt.Annotations.Core.AnnTextReviewObject, context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      constructor();
      thickness: number;
      position: number;
   }

   class AnnTextUnderlineObjectRenderer extends AnnTextReviewObjectRenderer {
      get_thickness(): number;
      set_thickness(value: number): void;
      get_position(): number;
      set_position(value: number): void;
      renderShape(mapper: lt.Annotations.Core.AnnContainerMapper, annTextReviewObject: lt.Annotations.Core.AnnTextReviewObject, context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      constructor();
      thickness: number;
      position: number;
   }

   class AnnThumbStyle {
      get_renderer(): lt.Annotations.Core.IAnnObjectRenderer;
      set_renderer(value: lt.Annotations.Core.IAnnObjectRenderer): void;
      clone(): AnnThumbStyle;
      get_size(): lt.LeadSizeD;
      set_size(value: lt.LeadSizeD): void;
      get_fill(): lt.Annotations.Core.AnnBrush;
      set_fill(value: lt.Annotations.Core.AnnBrush): void;
      get_stroke(): lt.Annotations.Core.AnnStroke;
      set_stroke(value: lt.Annotations.Core.AnnStroke): void;
      get_isVisible(): boolean;
      set_isVisible(value: boolean): void;
      renderHitTest(location: lt.LeadPointD, testPoint: lt.LeadPointD, hitTestBuffer: number, mapper: lt.Annotations.Core.AnnContainerMapper): boolean;
      hitTest(location: lt.LeadPointD, testPoint: lt.LeadPointD, hitTestBuffer: number): boolean;
      render(renderer: lt.Annotations.Core.IAnnObjectRenderer, mapper: lt.Annotations.Core.AnnContainerMapper, location: lt.LeadPointD, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      create(): AnnThumbStyle;  // protected
      addPath(context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      constructor();
      renderer: lt.Annotations.Core.IAnnObjectRenderer;
      size: lt.LeadSizeD;
      fill: lt.Annotations.Core.AnnBrush;
      stroke: lt.Annotations.Core.AnnStroke;
      isVisible: boolean;
   }

   class AnnRectangleThumbStyle extends AnnThumbStyle {
      create(): AnnThumbStyle;  // protected
      addPath(context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      constructor();
   }

   class AnnEllipseThumbStyle extends AnnThumbStyle {
      create(): AnnThumbStyle;  // protected
      addPath(context: CanvasRenderingContext2D, rect: lt.LeadRectD): void;  // protected
      constructor();
   }

   class AnnEncryptObjectRenderer extends AnnRectangleObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnMediaObjectRenderer extends AnnHotspotObjectRenderer {
      get_showAtRunMode(): boolean;
      constructor();
      showAtRunMode: boolean; // read-only
   }

   class AnnSvgCrossProductObjectRenderer extends AnnSvgPolyRulerObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgCurveObjectRenderer extends AnnSvgPolylineObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgEllipseObjectRenderer extends AnnSvgObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgEncryptObjectRenderer extends AnnSvgRectangleObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgFreehandHotspotObjectRenderer extends AnnSvgPolylineObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgHiliteObjectRenderer extends AnnSvgRectangleObjectRenderer {
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgHotspotObjectRenderer extends AnnSvgRectangleObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      get_showAtRunMode(): boolean;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
      showAtRunMode: boolean; // read-only
   }

   class AnnSvgMediaObjectRenderer extends AnnSvgHotspotObjectRenderer {
      get_showAtRunMode(): boolean;
      constructor();
      showAtRunMode: boolean; // read-only
   }

   class AnnSvgNoteObjectRenderer extends AnnSvgTextObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgObjectRenderer extends AnnObjectRenderer {
      createElement(): any;
      removeElementById(id: string): void;
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      addObject(annObject: lt.Annotations.Core.AnnObject): void;
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      renderLocked(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      constructor();
   }

   class AnnSvgPointerObjectRenderer extends AnnSvgPolylineObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgPointObjectRenderer extends AnnSvgObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgPolylineObjectRenderer extends AnnSvgObjectRenderer {
      setActivePolyLineID(Id: string): void;
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgPolyRulerObjectRenderer extends AnnSvgPolylineObjectRenderer {
      get_tickmarksId(): string;  // protected
      set_tickmarksId(value: string): void;  // protected
      drawTickMarks(mapper: lt.Annotations.Core.AnnContainerMapper, startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, tickMarkLength: lt.LeadLengthD, unit: lt.Annotations.Core.AnnUnit, stroke: lt.Annotations.Core.AnnStroke, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      drawGauge(mapper: lt.Annotations.Core.AnnContainerMapper, startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, gaugeLength: lt.LeadLengthD, stroke: lt.Annotations.Core.AnnStroke, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      drawLengthText(mapper: lt.Annotations.Core.AnnContainerMapper, label: lt.Annotations.Core.AnnLabel, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      addObject(annObject: lt.Annotations.Core.AnnObject): void;
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
      tickmarksId: string;
   }

   class AnnSvgProtractorObjectRenderer extends AnnSvgPolyRulerObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      getAngleText(angle: number, precision: number, unit: lt.Annotations.Core.AnnAngularUnit, unitsAbbreviation: { [key: number]: string }): string;
      getPoint(size: lt.LeadSizeD, firstPoint: lt.LeadPointD, centerPoint: lt.LeadPointD, secondPoint: lt.LeadPointD, angle1: number, angle2: number, acute: boolean, arcRadius: number, unit: lt.Annotations.Core.AnnAngularUnit): lt.LeadPointD;
      drawArc(mapper: lt.Annotations.Core.AnnContainerMapper, center: lt.LeadPointD, startAngle: number, sweepAngle: number, radius: lt.LeadLengthD, stroke: lt.Annotations.Core.AnnStroke, operations: lt.Annotations.Core.AnnFixedStateOperations): void;  // protected
      drawAngleText(mapper: lt.Annotations.Core.AnnContainerMapper, label: lt.Annotations.Core.AnnLabel, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgRectangleObjectRenderer extends AnnSvgObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgRubberStampObjectRenderer extends AnnSvgRectangleObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgStampObjectRenderer extends AnnSvgTextObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgTextObjectRenderer extends AnnSvgRectangleObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      get_flipReverseText(): boolean;
      set_flipReverseText(value: boolean): void;
      getTextSize(text: string, annFont: lt.Annotations.Core.AnnFont, layoutArea: lt.LeadSizeD): lt.LeadSizeD;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
      flipReverseText: boolean;
   }

   class AnnSvgTextPointerObjectRenderer extends AnnSvgTextObjectRenderer {
      createObject(annObject: lt.Annotations.Core.AnnObject): void;  // protected
      removeObject(annObject: lt.Annotations.Core.AnnObject): void;
      render(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): void;
      constructor();
   }

   class AnnSvgTextRollupObjectRenderer extends AnnSvgNoteObjectRenderer {
      getRenderPoints(mapper: lt.Annotations.Core.AnnContainerMapper, annObject: lt.Annotations.Core.AnnObject): lt.LeadPointD[];
      constructor();
   }

   class AnnSvgThumbStyle {
      get_renderer(): lt.Annotations.Core.IAnnObjectRenderer;
      set_renderer(value: lt.Annotations.Core.IAnnObjectRenderer): void;
      clone(): AnnSvgThumbStyle;
      get_size(): lt.LeadSizeD;
      set_size(value: lt.LeadSizeD): void;
      get_fill(): lt.Annotations.Core.AnnBrush;
      set_fill(value: lt.Annotations.Core.AnnBrush): void;
      get_stroke(): lt.Annotations.Core.AnnStroke;
      set_stroke(value: lt.Annotations.Core.AnnStroke): void;
      get_isVisible(): boolean;
      set_isVisible(value: boolean): void;
      renderHitTest(location: lt.LeadPointD, testPoint: lt.LeadPointD, hitTestBuffer: number, mapper: lt.Annotations.Core.AnnContainerMapper): boolean;
      hitTest(location: lt.LeadPointD, testPoint: lt.LeadPointD, hitTestBuffer: number): boolean;
      render(renderer: lt.Annotations.Core.IAnnObjectRenderer, mapper: lt.Annotations.Core.AnnContainerMapper, location: lt.LeadPointD, operations: lt.Annotations.Core.AnnFixedStateOperations): void;
      create(): AnnSvgThumbStyle;  // protected
      addPath(element: HTMLElement, rect: lt.LeadRectD): void;  // protected
      constructor();
      renderer: lt.Annotations.Core.IAnnObjectRenderer;
      size: lt.LeadSizeD;
      fill: lt.Annotations.Core.AnnBrush;
      stroke: lt.Annotations.Core.AnnStroke;
      isVisible: boolean;
   }

   class AnnSvgRectangleThumbStyle extends AnnSvgThumbStyle {
      create(): AnnSvgThumbStyle;  // protected
      addPath(element: HTMLElement, rect: lt.LeadRectD): void;  // protected
      constructor();
   }

   class AnnSvgEllipseThumbStyle extends AnnSvgThumbStyle {
      create(): AnnSvgThumbStyle;  // protected
      addPath(element: HTMLElement, rect: lt.LeadRectD): void;  // protected
      constructor();
   }
}
