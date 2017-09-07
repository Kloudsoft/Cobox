//***********************************************************************************************
//   Type definitions for Leadtools.Annotations.Core.js
//   Updated: 4/11/2016 20:21
//   Version: 19.0.0.31
//   Reference with:
//   /// <reference path="Leadtools.Annotations.Core.d.ts" />
//   Copyright (c) 1991-2015 All Rights Reserved. LEAD Technologies, Inc.
//   http://www.leadtools.com
//***********************************************************************************************

// Required references
/// <reference path="Leadtools.d.ts"/>

declare module lt.Annotations.Core {

   class AnnUnitConverter {
      static set_smartEnglishMaximumUnit(value: AnnUnit): void;
      static get_smartEnglishMaximumUnit(): AnnUnit;
      static set_smartMetricMaximumUnit(value: AnnUnit): void;
      static get_smartMetricMaximumUnit(): AnnUnit;
      static getUnits(): { [key: number]: string };
      static setUnitAbbreviation(unit: AnnUnit, newValue: string): string;
      static getUnitAbbreviation(unit: AnnUnit): string;
      static getAngularUnitAbbreviation(unit: AnnAngularUnit): string;
      static convert(value: number, sourceUnit: AnnUnit, destinationUnit: AnnUnit): number;
      static convertToPixels(value: number, sourceUnit: AnnUnit, dpi: number): number;
      static convertFromPixels(value: number, destinationUnit: AnnUnit, dpi: number): number;
      static convertAngularUnit(value: number, sourceUnit: AnnAngularUnit, destinationUnit: AnnAngularUnit): number;
      static smartEnglishMaximumUnit: AnnUnit;
      static smartMetricMaximumUnit: AnnUnit;
   }

   class ExceptionHelper {
      static invalidOperationException(message: string): void;
      static argumentNullException(paramName: string): void;
      static argumentOutOfRangeException(paramName: string, actualValue: any, message: string): void;
      static argumentException(message: string, paramName: string): void;
   }

   enum PointType {
      none = 0,
      closeFigure = 1,
      lineTo = 2,
      bezierTo = 4,
      moveTo = 6
   }

   class PolyPoint {
      get_points(): lt.LeadPointD[];
      set_points(value: lt.LeadPointD[]): void;
      get_pointTypes(): PointType[];
      set_pointTypes(value: PointType[]): void;
      static fromEllipse(rect: lt.LeadRectD, angle: number, center: lt.LeadPointD): PolyPoint;
      hitTest(testPoint: lt.LeadPointD, hitTestBuffer: number, hitTestInterior: boolean): boolean;
      constructor();
      points: lt.LeadPointD[];
      pointTypes: PointType[];
   }

   class RulerMetadata {
      constructor();
      multiply: number;
      tickUnit: number;
      outFactor: number;
      outDivisor: number;
   }

   class RulerHelper {
      static getRulerLength(points: lt.LeadPointD[]): number;
      static getRulerLengthFromTwoPoints(startPoint: lt.LeadPointD, endPoint: lt.LeadPointD): number;
      static getLengthString(length: lt.LeadLengthD, precision: number, unit: AnnUnit, unitsAbbreviations: { [key: number]: string }): string;
      static getLengthStringFromPoint(startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, precision: number, unit: AnnUnit, unitsAbbreviations: { [key: number]: string }): string;
      static getLengthStringFromPoints(points: lt.LeadPointD[], precision: number, unit: AnnUnit, unitsAbbreviations: { [key: number]: string }): string;
      static getGaugePoints(mapper: AnnContainerMapper, startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, gaugeLength: lt.LeadLengthD, fixedStateOperations: AnnFixedStateOperations): lt.LeadPointD[];
      static getTickMarkSize(i: number, tickUnit: number): number;
      static getTickMarkFactor(tickDistance: number, unit: AnnUnit, min: number, max: number, outData: RulerMetadata): number;
      static getTickMarks(mapper: AnnContainerMapper, startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, tickMarkLength: lt.LeadLengthD, unit: AnnUnit, fixedStateOperations: AnnFixedStateOperations): lt.LeadPointD[];
   }

   enum ScrambleImageFlags {
      none = 0,
      encrypt = 1,
      decrypt = 2,
      intersect = 4,
      reserved3 = 8,
      reserved4 = 16,
      reserved5 = 32,
      reserved6 = 64,
      reserved7 = 128,
      reserved8 = 256,
      reserved9 = 512,
      allflags = 767
   }

   class ScrambleImage {
      get_imageData(): Uint8Array;
      set_imageData(value: Uint8Array): void;
      get_imageWidth(): number;
      set_imageWidth(value: number): void;
      get_imageHeight(): number;
      set_imageHeight(value: number): void;
      get_rectangle(): lt.LeadRectD;
      set_rectangle(value: lt.LeadRectD): void;
      get_key(): string;
      set_key(value: string): void;
      get_flags(): ScrambleImageFlags;
      set_flags(value: ScrambleImageFlags): void;
      scramble(): void;
      scrambleData(originalData: Uint8Array, originalWidth: number, originalHeight: number, colStart: number, rowStart: number, width: number, height: number, key: string, flag: ScrambleImageFlags): void;
      constructor();
      imageData: Uint8Array;
      imageWidth: number;
      imageHeight: number;
      rectangle: lt.LeadRectD;
      key: string;
      flags: ScrambleImageFlags;
   }

   interface AnnCheckModifierCallback {
      (annKey: AnnKeys): boolean;
   }

   class Utils {
      static isFlipedReveresd(mapper: AnnContainerMapper): number;
      static compare(str1: string, str2: string, ignoreCase: boolean): boolean;
      static getExtendedPoint(start: lt.LeadPointD, end: lt.LeadPointD, forRectangle: boolean, forLine: boolean, forCrossProduct: boolean): lt.LeadPointD;
      static divide(num: number, den: number): number;
      static setCheckModifierCallback(checkModifierCallback: AnnCheckModifierCallback): void;
      static checkModifierKey(annKey: AnnKeys): boolean;
      static isZero(d: number): boolean;
      static isEqual(d1: number, d2: number): boolean;
      static degreesToRadian(angle: number): number;
      static findAngle(pt1: lt.LeadPointD, pt2: lt.LeadPointD): number;
      static distance(pt0: lt.LeadPointD, pt1: lt.LeadPointD): number;
      static subtractPoint(pt1: lt.LeadPointD, pt0: lt.LeadPointD): lt.LeadPointD;
      static getUnitVectorPerpendicular(pt0: lt.LeadPointD, pt1: lt.LeadPointD): lt.LeadPointD;
      static getUnitVector(pt0: lt.LeadPointD, pt1: lt.LeadPointD): lt.LeadPointD;
      static transformPoint(unitVector: lt.LeadPointD, length: lt.LeadLengthD, offset: lt.LeadPointD): lt.LeadPointD;
      static intersect(point0: lt.LeadPointD, point1: lt.LeadPointD, point2: lt.LeadPointD, point3: lt.LeadPointD): lt.LeadPointD;
      static hitTestLine(startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, testPoint: lt.LeadPointD, hitTestBuffer: number, checkBounds: boolean): boolean;
      static hitTestPolygon(points: LeadPointCollection, testPoint: lt.LeadPointD): boolean;
      static hitTestPolygonArray(points: lt.LeadPointD[], testPoint: lt.LeadPointD): boolean;
      static hitTestBezier(points: lt.LeadPointD[], startPointIndex: number, testPoint: lt.LeadPointD, hitTestBuffer: number): boolean;
      static fixRectangle(rect: lt.LeadRectD, mapper: AnnContainerMapper, operations: AnnFixedStateOperations): lt.LeadRectD;
      static fixPoint(point: lt.LeadPointD, mapper: AnnContainerMapper, operations: AnnFixedStateOperations): lt.LeadPointD;
      static getBoundingRectangle(pt0: lt.LeadPointD, pt1: lt.LeadPointD): lt.LeadRectD;
      static splineCurve(points: lt.LeadPointD[], tension: number): lt.LeadPointD[];
      static calculateSplineCurve(pt0: lt.LeadPointD, pt1: lt.LeadPointD, pt2: lt.LeadPointD, pt3: lt.LeadPointD, tension: number): lt.LeadPointD[];
      static splineClosedCurve(points: lt.LeadPointD[], tension: number): lt.LeadPointD[];
      static precisionFormat(precision: number, value: number): string;
      static enumToString(type: any, value: number): string;
      static enumParse(type: any, value: string): number;
      static fromBase64String(data: string): number[];
      static toBase64String(data: number[]): string;
      static isRightAngle(angle: number): boolean;
      static getAutomationOffset(control: IAnnAutomationControl, container: AnnContainer): lt.LeadPointD;
      static getAutomationSize(control: IAnnAutomationControl, container: AnnContainer): lt.LeadSizeD;
      static setRenderingEngine(control: IAnnAutomationControl, engine: AnnRenderingEngine): void;
      static realizeRedactWinforms(control: IAnnAutomationControl, container: AnnContainer, annRedact: AnnRedactionObject): lt.LeadRectD;
      static restoreRedactWinforms(control: IAnnAutomationControl, container: AnnContainer, annRedact: AnnRedactionObject): lt.LeadRectD;
      static applyEncrypt(control: IAnnAutomationControl, container: AnnContainer, annEncrypt: AnnEncryptObject): lt.LeadRectD;
      static dataProviderCanReadWrite(control: IAnnAutomationControl): boolean;
      static toLowerCase(input: string): string;
      static toStringInvariantCulture(input: number): string;
      static doubleDelta: number;
   }

   enum AnnFormat {
      unknown = 0,
      annotations = 1
   }

   class AnnCodecsInfo {
      get_format(): AnnFormat;
      set_format(value: AnnFormat): void;
      get_version(): number;
      set_version(value: number): void;
      get_pages(): number[];
      set_pages(value: number[]): void;
      constructor();
      format: AnnFormat;
      version: number;
      pages: number[];
   }

   interface AnnSerializeObjectEventHandler {
      (sender: any, e: AnnSerializeObjectEventArgs): void;
   }

   class AnnSerializeObjectEventType extends lt.LeadEvent {
      add(value: AnnSerializeObjectEventHandler): AnnSerializeObjectEventHandler;
      remove(value: AnnSerializeObjectEventHandler): void;
   }

   class AnnSerializeObjectEventArgs extends lt.LeadEventArgs {
      static create(typeName: string, annObject: AnnObject, error: Error): AnnSerializeObjectEventArgs;
      get_typeName(): string;
      get_error(): Error;
      get_annObject(): AnnObject;
      set_annObject(value: AnnObject): void;
      get_skipObject(): boolean;
      set_skipObject(value: boolean): void;
      typeName: string; // read-only
      error: Error; // read-only
      annObject: AnnObject;
      skipObject: boolean;
   }

   class AnnSerializeOptions {
      add_serializeObject(value: AnnSerializeObjectEventHandler): void;
      remove_serializeObject(value: AnnSerializeObjectEventHandler): void;
      get_saveLockPassword(): boolean;
      set_saveLockPassword(value: boolean): void;
      constructor();
      saveLockPassword: boolean;
      serializeObject: AnnSerializeObjectEventType; // read-only
   }

   class AnnDeserializeOptions {
      add_deserializeObject(value: AnnSerializeObjectEventHandler): void;
      remove_deserializeObject(value: AnnSerializeObjectEventHandler): void;
      add_deserializeObjectError(value: AnnSerializeObjectEventHandler): void;
      remove_deserializeObjectError(value: AnnSerializeObjectEventHandler): void;
      constructor();
      deserializeObject: AnnSerializeObjectEventType; // read-only
      deserializeObjectError: AnnSerializeObjectEventType; // read-only
   }

   class AnnCodecs {
      get_serializeOptions(): AnnSerializeOptions;
      set_serializeOptions(value: AnnSerializeOptions): void;
      get_deserializeOptions(): AnnDeserializeOptions;
      set_deserializeOptions(value: AnnDeserializeOptions): void;
      get_loadSourceResolution(): lt.LeadSizeD;
      set_loadSourceResolution(value: lt.LeadSizeD): void;
      get_loadTargetResolution(): lt.LeadSizeD;
      set_loadTargetResolution(value: lt.LeadSizeD): void;
      get_loadUseDpi(): boolean;
      set_loadUseDpi(value: boolean): void;
      loadFromXmlDocument(document: Document, pageNumber: number): AnnContainer;
      getInfo(xmlData: string): AnnCodecsInfo;
      getInfoFromXmlDocument(document: Document): AnnCodecsInfo;
      load(xmlData: string, pageNumber: number): AnnContainer;
      save(container: AnnContainer, format: AnnFormat, xmlData: string, savePageNumber: number): string;
      loadAll(xmlData: string): AnnContainer[];
      saveAll(containers: AnnContainer[], format: AnnFormat): string;
      saveLayer(layer: AnnLayer, format: AnnFormat, xmlData: string): string;
      constructor();
      serializeOptions: AnnSerializeOptions;
      deserializeOptions: AnnDeserializeOptions;
      loadSourceResolution: lt.LeadSizeD;
      loadTargetResolution: lt.LeadSizeD;
      loadUseDpi: boolean;
   }

   class AnnContainer {
      static create(offset: lt.LeadPointD, size: lt.LeadSizeD, mapper: AnnContainerMapper): AnnContainer;
      add_objectsPropertyChanged(value: AnnPropertyChangedEventHandler): void;
      remove_objectsPropertyChanged(value: AnnPropertyChangedEventHandler): void;
      get_selectionObject(): AnnSelectionObject;
      set_selectionObject(value: AnnSelectionObject): void;
      get_pageNumber(): number;
      set_pageNumber(value: number): void;
      get_mapper(): AnnContainerMapper;
      set_mapper(value: AnnContainerMapper): void;
      get_size(): lt.LeadSizeD;
      set_size(value: lt.LeadSizeD): void;
      get_offset(): lt.LeadPointD;
      set_offset(value: lt.LeadPointD): void;
      get_fill(): AnnBrush;
      set_fill(value: AnnBrush): void;
      get_stroke(): AnnStroke;
      set_stroke(value: AnnStroke): void;
      get_userMode(): AnnUserMode;
      set_userMode(value: AnnUserMode): void;
      get_userData(): any;
      set_userData(value: any): void;
      get_isEnabled(): boolean;
      set_isEnabled(value: boolean): void;
      get_labels(): AnnLabel[];
      get_children(): AnnObjectCollection;
      get_isVisible(): boolean;
      set_isVisible(value: boolean): void;
      get_hitTestBuffer(): number;
      set_hitTestBuffer(value: number): void;
      get_hitTestBehavior(): AnnHitTestBehavior;
      set_hitTestBehavior(value: AnnHitTestBehavior): void;
      get_layers(): AnnLayerCollection;
      get_stateId(): string;
      set_stateId(value: string): void;
      add_objectAdded(value: AnnObjectCollectionEventHandler): void;
      remove_objectAdded(value: AnnObjectCollectionEventHandler): void;
      add_objectRemoved(value: AnnObjectCollectionEventHandler): void;
      remove_objectRemoved(value: AnnObjectCollectionEventHandler): void;
      hitTestPoint(point: lt.LeadPointD): AnnObject[];
      hitTestRect(rect: lt.LeadRectD): AnnObject[];
      unselect(annObject: AnnObject): boolean;
      select(annObject: AnnObject): boolean;
      rotate(angle: number): void;
      clone(): AnnContainer;
      get_activeLayer(): AnnLayer;
      set_activeLayer(value: AnnLayer): void;
      resize(newSize: lt.LeadSizeD, flags: AnnResizeContainerFlags, mode: AnnResizeMode): void;
      get_groupsRoles(): AnnGroupsRoles;
      set_groupsRoles(value: AnnGroupsRoles): void;
      get_totalObjectsInvalidRect(): lt.LeadRectD;
      set_totalObjectsInvalidRect(value: lt.LeadRectD): void;
      constructor();
      selectionObject: AnnSelectionObject;
      pageNumber: number;
      mapper: AnnContainerMapper;
      size: lt.LeadSizeD;
      offset: lt.LeadPointD;
      fill: AnnBrush;
      stroke: AnnStroke;
      userMode: AnnUserMode;
      userData: any;
      isEnabled: boolean;
      labels: AnnLabel[]; // read-only
      children: AnnObjectCollection; // read-only
      isVisible: boolean;
      hitTestBuffer: number;
      hitTestBehavior: AnnHitTestBehavior;
      layers: AnnLayerCollection; // read-only
      stateId: string;
      activeLayer: AnnLayer;
      groupsRoles: AnnGroupsRoles;
      totalObjectsInvalidRect: lt.LeadRectD;
      objectsPropertyChanged: AnnPropertyChangedEventType; // read-only
      objectAdded: AnnObjectCollectionEventType; // read-only
      objectRemoved: AnnObjectCollectionEventType; // read-only
   }

   class AnnContainerMapper {
      get_scrollOffset(): lt.LeadPointD;
      set_scrollOffset(value: lt.LeadPointD): void;
      get_ignoreDpiScale(): boolean;
      set_ignoreDpiScale(value: boolean): void;
      get_burnScaleFactor(): number;
      set_burnScaleFactor(value: number): void;
      get_offset(): lt.LeadPointD;
      set_offset(value: lt.LeadPointD): void;
      normalizeRectangle(rect: lt.LeadRectD, operation: AnnFixedStateOperations): number;
      clone(): AnnContainerMapper;
      get_burnFontDpi(): boolean;
      set_burnFontDpi(value: boolean): void;
      get_calibrationScale(): number;
      calibrate(sourceLength: lt.LeadLengthD, sourceUnit: AnnUnit, destinationLength: lt.LeadLengthD, destinationUnit: AnnUnit): void;
      get_transform(): lt.LeadMatrix;
      get_sourceDpiX(): number;
      get_sourceDpiY(): number;
      get_targetDpiX(): number;
      get_targetDpiY(): number;
      static createDefault(): AnnContainerMapper;
      get_deviceDpiX(): number;
      set_deviceDpiX(value: number): void;
      get_deviceDpiY(): number;
      set_deviceDpiY(value: number): void;
      updateTransform(transform: lt.LeadMatrix): void;
      updateDestinationRectangle(destinationRect: lt.LeadRectD, sourceSize: lt.LeadSizeD): void;
      mapResolutions(sourceDpiX: number, sourceDpiY: number, targetDpiX: number, targetDpiY: number): void;
      pointsToContainerCoordinates(points: lt.LeadPointD[]): lt.LeadPointD[];
      internalPointToContainerCoordinates(point: lt.LeadPointD, transform: lt.LeadMatrix): lt.LeadPointD;
      pointToContainerCoordinates(point: lt.LeadPointD): lt.LeadPointD;
      getHitTestBuffer(hitTestBuffer: number): number;
      fontToContainerCoordinates(font: AnnFont): AnnFont;
      get_fontRelativeToDevice(): boolean;
      set_fontRelativeToDevice(value: boolean): void;
      strokeFromContainerCoordinates(stroke: AnnStroke, operation: AnnFixedStateOperations): AnnStroke;
      fontFromContainerCoordinates(font: AnnFont, operation: AnnFixedStateOperations): AnnFont;
      sizeToContainerCoordinates(size: lt.LeadSizeD): lt.LeadSizeD;
      sizeFromContainerCoordinates(size: lt.LeadSizeD): lt.LeadSizeD;
      lengthFromContainerCoordinates(length: lt.LeadLengthD, operation: AnnFixedStateOperations): number;
      lengthToContainerCoordinates(value: number): lt.LeadLengthD;
      pointFromContainerCoordinates(point: lt.LeadPointD, operation: AnnFixedStateOperations): lt.LeadPointD;
      pointsFromContainerCoordinates(points: lt.LeadPointD[], operation: AnnFixedStateOperations): lt.LeadPointD[];
      rectFromContainerCoordinates(rect: lt.LeadRectD, operation: AnnFixedStateOperations): lt.LeadRectD;
      rectToContainerCoordinates(rect: lt.LeadRectD): lt.LeadRectD;
      constructor(sourceDpiX: number, sourceDpiY: number, targetDpiX: number, targetDpiY: number);
      scrollOffset: lt.LeadPointD;
      ignoreDpiScale: boolean;
      burnScaleFactor: number;
      offset: lt.LeadPointD;
      burnFontDpi: boolean;
      calibrationScale: number;
      transform: lt.LeadMatrix;
      sourceDpiX: number; // read-only
      sourceDpiY: number; // read-only
      targetDpiX: number; // read-only
      targetDpiY: number; // read-only
      deviceDpiX: number;
      deviceDpiY: number;
      fontRelativeToDevice: boolean;
   }

   interface IAnnObjectCloneable {
      clone(): AnnObject;
   }

   class AnnObject {
      get_internalRotateGripperLocation(): lt.LeadPointD;
      set_internalRotateGripperLocation(value: lt.LeadPointD): void;
      get_internalThumbLocations(): lt.LeadPointD[];
      set_internalThumbLocations(value: lt.LeadPointD[]): void;
      get_userId(): string;
      set_userId(value: string): void;
      get_isAlignmentTarget(): boolean;
      set_isAlignmentTarget(value: boolean): void;
      get_layer(): AnnLayer;
      set_layer(value: AnnLayer): void;
      get_id(): number;
      setId(id: number): void;
      get_stateId(): string;
      set_stateId(value: string): void;
      get_opacity(): number;
      set_opacity(value: number): void;
      get_renderedObjectBounds(): lt.LeadRectD;
      set_renderedObjectBounds(value: lt.LeadRectD): void;
      add_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      get_friendlyName(): string;
      static dateToString(value: Date): string;
      static dateFromString(value: string): Date;
      get_reviews(): AnnReview[];
      get_metadata(): { [key: string]: string };
      onPropertyChanged(e: AnnPropertyChangedEventArgs): void;
      saveProperties(propertyBag: { [key: string]: string }): void;
      loadProperties(propertyBag: { [key: string]: string }): void;
      get_supportsFill(): boolean;
      get_supportsStroke(): boolean;
      get_supportsSelectionStroke(): boolean;
      get_supportsFont(): boolean;
      get_supportsOpacity(): boolean;
      get_supportsContent(): boolean;
      get_fill(): AnnBrush;
      set_fill(value: AnnBrush): void;
      get_stroke(): AnnStroke;
      set_stroke(value: AnnStroke): void;
      get_selectionStroke(): AnnStroke;
      set_selectionStroke(value: AnnStroke): void;
      get_font(): AnnFont;
      set_font(value: AnnFont): void;
      get_hyperlink(): string;
      set_hyperlink(value: string): void;
      get_lockPicture(): number;
      set_lockPicture(value: number): void;
      get_contentPicture(): number;
      set_contentPicture(value: number): void;
      get_internalAdd(): boolean;
      set_internalAdd(value: boolean): void;
      get_fixedStateOperations(): AnnFixedStateOperations;
      set_fixedStateOperations(value: AnnFixedStateOperations): void;
      get_groupName(): string;
      set_groupName(value: string): void;
      get_parent(): any;
      set_parent(value: any): void;
      get_points(): LeadPointCollection;
      onPointsChanged(): void;  // protected
      get_password(): string;
      set_password(value: string): void;
      get_bounds(): lt.LeadRectD;
      get_isLocked(): boolean;
      get_tag(): any;
      set_tag(value: any): void;
      get_isVisible(): boolean;
      set_isVisible(value: boolean): void;
      get_isSelected(): boolean;
      set_isSelected(value: boolean): void;
      get_rotateCenter(): lt.LeadPointD;
      set_rotateCenter(value: lt.LeadPointD): void;
      get_rotateGripper(): lt.LeadLengthD;
      set_rotateGripper(value: lt.LeadLengthD): void;
      get_canRotate(): boolean;
      get_labels(): { [key: string]: AnnLabel };
      getBoundingRectangle(): lt.LeadRectD;  // protected
      lock(password: string): void;
      unlock(password: string): void;
      scaleVector(scaleX: number, scaleY: number, unitVectorX: lt.LeadPointD, unitVectorY: lt.LeadPointD, centerPoint: lt.LeadPointD): void;
      scale(scaleX: number, scaleY: number, origin: lt.LeadPointD): void;
      translate(offsetX: number, offsetY: number): void;
      rotate(angle: number, origin: lt.LeadPointD): void;
      normalize(): void;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      clone(): AnnObject;
      create(): AnnObject;  // protected
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      get_hitTestInterior(): boolean;
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      getArea(): number;
      get_internalRotateCenterLocation(): lt.LeadPointD;
      set_internalRotateCenterLocation(value: lt.LeadPointD): void;
      constructor();
      userId: string;
      isAlignmentTarget: boolean;
      layer: AnnLayer;
      id: number; // read-only
      stateId: string;
      opacity: number;
      renderedObjectBounds: lt.LeadRectD;
      friendlyName: string; // read-only
      reviews: AnnReview[]; // read-only
      metadata: { [key: string]: string }; // read-only
      supportsFill: boolean; // read-only
      supportsStroke: boolean; // read-only
      supportsSelectionStroke: boolean; // read-only
      supportsFont: boolean; // read-only
      supportsOpacity: boolean; // read-only
      supportsContent: boolean; // read-only
      fill: AnnBrush;
      stroke: AnnStroke;
      selectionStroke: AnnStroke;
      font: AnnFont;
      hyperlink: string;
      lockPicture: number;
      contentPicture: number;
      internalAdd: boolean;
      fixedStateOperations: AnnFixedStateOperations;
      groupName: string;
      parent: any;
      points: LeadPointCollection; // read-only
      password: string;
      bounds: lt.LeadRectD; // read-only
      isLocked: boolean; // read-only
      tag: any;
      isVisible: boolean;
      isSelected: boolean;
      rotateCenter: lt.LeadPointD;
      rotateGripper: lt.LeadLengthD;
      canRotate: boolean; // read-only
      labels: { [key: string]: AnnLabel }; // read-only
      hitTestInterior: boolean; // read-only
      internalRotateCenterLocation: lt.LeadPointD;
      internalRotateGripperLocation: lt.LeadPointD;
      internalThumbLocations: lt.LeadPointD[];
      propertyChanged: AnnPropertyChangedEventType; // read-only
      static none: number;
      static groupObjectId: number;
      static selectObjectId: number;
      static lineObjectId: number;
      static rectangleObjectId: number;
      static ellipseObjectId: number;
      static polylineObjectId: number;
      static polygonObjectId: number;
      static curveObjectId: number;
      static closedCurveObjectId: number;
      static pointerObjectId: number;
      static freehandObjectId: number;
      static hiliteObjectId: number;
      static textObjectId: number;
      static textRollupObjectId: number;
      static textPointerObjectId: number;
      static noteObjectId: number;
      static stampObjectId: number;
      static rubberStampObjectId: number;
      static hotspotObjectId: number;
      static freehandHotspotObjectId: number;
      static buttonObjectId: number;
      static pointObjectId: number;
      static redactionObjectId: number;
      static rulerObjectId: number;
      static polyRulerObjectId: number;
      static protractorObjectId: number;
      static crossProductObjectId: number;
      static encryptObjectId: number;
      static audioObjectId: number;
      static richTextObjectId: number;
      static mediaObjectId: number;
      static imageObjectId: number;
      static stickyNoteObjectId: number;
      static textHiliteObjectId: number;
      static textStrikeoutObjectId: number;
      static textUnderlineObjectId: number;
      static textRedactionObjectId: number;
      static userObjectId: number;
      static subjectMetadataKey: string;
      static authorMetadataKey: string;
      static modifiedMetadataKey: string;
      static titleMetadataKey: string;
      static contentMetadataKey: string;
      static createdMetadataKey: string;
      static _rotateGripperOriginal: number;
   }

   class AnnRectangleObject extends AnnObject {
      get_friendlyName(): string;
      get_isFlipped(): boolean;
      get_isReversed(): boolean;
      get_angle(): number;
      get_rect(): lt.LeadRectD;
      set_rect(value: lt.LeadRectD): void;
      get_supportsFill(): boolean;
      get_supportsStroke(): boolean;
      create(): AnnObject;  // protected
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      constructor();
      friendlyName: string; // read-only
      isFlipped: boolean; // read-only
      isReversed: boolean; // read-only
      angle: number; // read-only
      rect: lt.LeadRectD;
      supportsFill: boolean; // read-only
      supportsStroke: boolean; // read-only
   }

   class AnnImageObject extends AnnRectangleObject {
      get_friendlyName(): string;
      get_picture(): AnnPicture;
      set_picture(value: AnnPicture): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      get_hitTestInterior(): boolean;
      constructor();
      friendlyName: string; // read-only
      picture: AnnPicture;
      hitTestInterior: boolean; // read-only
   }

   class AnnHotspotObject extends AnnImageObject {
      get_friendlyName(): string;
      get_supportsFill(): boolean;
      get_supportsStroke(): boolean;
      get_defaultPicture(): number;
      set_defaultPicture(value: number): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      get_hitTestInterior(): boolean;
      constructor();
      friendlyName: string; // read-only
      supportsFill: boolean; // read-only
      supportsStroke: boolean; // read-only
      defaultPicture: number;
      hitTestInterior: boolean; // read-only
   }

   class AnnMediaObject extends AnnHotspotObject {
      get_friendlyName(): string;
      get_media(): AnnMedia;
      set_media(value: AnnMedia): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      get_hitTestInterior(): boolean;
      constructor();
      friendlyName: string; // read-only
      media: AnnMedia;
      hitTestInterior: boolean; // read-only
   }

   class AnnAudioObject extends AnnMediaObject {
      get_friendlyName(): string;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      get_hitTestInterior(): boolean;
      constructor();
      friendlyName: string; // read-only
      hitTestInterior: boolean; // read-only
   }

   class AnnPolylineObject extends AnnObject {
      get_friendlyName(): string;
      get_supportsStroke(): boolean;
      get_supportsFill(): boolean;
      get_isClosed(): boolean;
      set_isClosed(value: boolean): void;
      get_fillRule(): AnnFillRule;
      set_fillRule(value: AnnFillRule): void;
      create(): AnnObject;  // protected
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      clone(): AnnObject;
      constructor();
      friendlyName: string; // read-only
      supportsStroke: boolean; // read-only
      supportsFill: boolean; // read-only
      isClosed: boolean;
      fillRule: AnnFillRule;
   }

   class AnnPolyRulerObject extends AnnPolylineObject {
      get_friendlyName(): string;
      get_tickMarksStroke(): AnnStroke;
      set_tickMarksStroke(value: AnnStroke): void;
      get_measurementUnit(): AnnUnit;
      set_measurementUnit(value: AnnUnit): void;
      get_unitsAbbreviation(): { [key: number]: string };
      get_tickMarksLength(): lt.LeadLengthD;
      set_tickMarksLength(value: lt.LeadLengthD): void;
      get_showTickMarks(): boolean;
      set_showTickMarks(value: boolean): void;
      get_showTickValue(): boolean;
      set_showTickValue(value: boolean): void;
      get_gaugeLength(): lt.LeadLengthD;
      set_gaugeLength(value: lt.LeadLengthD): void;
      get_showGauge(): boolean;
      set_showGauge(value: boolean): void;
      get_precision(): number;
      set_precision(value: number): void;
      getRulerLength(calibrationScale: number): lt.LeadLengthD;
      getRulerLengthAsString(calibrationScale: number): string;
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      create(): AnnObject;  // protected
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      constructor();
      friendlyName: string; // read-only
      tickMarksStroke: AnnStroke;
      measurementUnit: AnnUnit;
      unitsAbbreviation: { [key: number]: string }; // read-only
      tickMarksLength: lt.LeadLengthD;
      showTickMarks: boolean;
      showTickValue: boolean;
      gaugeLength: lt.LeadLengthD;
      showGauge: boolean;
      precision: number;
   }

   class AnnCrossProductObject extends AnnPolyRulerObject {
      get_friendlyName(): string;
      get_supportsFill(): boolean;
      get_firstEndPoint(): lt.LeadPointD;
      set_firstEndPoint(value: lt.LeadPointD): void;
      get_firstStartPoint(): lt.LeadPointD;
      set_firstStartPoint(value: lt.LeadPointD): void;
      get_intersectionPoint(): lt.LeadPointD;
      set_intersectionPoint(value: lt.LeadPointD): void;
      get_secondEndPoint(): lt.LeadPointD;
      set_secondEndPoint(value: lt.LeadPointD): void;
      get_secondStartPoint(): lt.LeadPointD;
      set_secondStartPoint(value: lt.LeadPointD): void;
      updatePoint(pointIndex: number, pt: lt.LeadPointD): void;
      updateSecondPoints(): void;
      moveLine(lineIndex: number, offsetX: number, offsetY: number): void;
      updateIntersectionPoint(): void;
      get_hitTestedRuler(): string;
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      clone(): AnnObject;
      create(): AnnObject;  // protected
      getRulerLength(calibrationScale: number): lt.LeadLengthD;
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      constructor();
      friendlyName: string; // read-only
      supportsFill: boolean; // read-only
      firstEndPoint: lt.LeadPointD;
      firstStartPoint: lt.LeadPointD;
      intersectionPoint: lt.LeadPointD;
      secondEndPoint: lt.LeadPointD;
      secondStartPoint: lt.LeadPointD;
      hitTestedRuler: string; // read-only
      static firstRulerHitTestObject: string;
      static secondRulerHitTestObject: string;
   }

   class AnnCurveObject extends AnnPolylineObject {
      get_friendlyName(): string;
      get_tension(): number;
      set_tension(value: number): void;
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      create(): AnnObject;  // protected
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      getBoundingRectangle(): lt.LeadRectD;  // protected
      constructor();
      friendlyName: string; // read-only
      tension: number;
   }

   class AnnEllipseObject extends AnnRectangleObject {
      get_friendlyName(): string;
      create(): AnnObject;  // protected
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      constructor();
      friendlyName: string; // read-only
   }

   class AnnEncryptObject extends AnnRectangleObject {
      get_friendlyName(): string;
      get_primaryPicture(): AnnPicture;
      set_primaryPicture(value: AnnPicture): void;
      get_secondaryPicture(): AnnPicture;
      set_secondaryPicture(value: AnnPicture): void;
      get_defaultPrimaryPicture(): number;
      set_defaultPrimaryPicture(value: number): void;
      get_defaultSecondaryPicture(): number;
      set_defaultSecondaryPicture(value: number): void;
      get_key(): number;
      set_key(value: number): void;
      get_resetKeyIfApplied(): boolean;
      set_resetKeyIfApplied(value: boolean): void;
      get_serializeKeyIfEncrypted(): boolean;
      set_serializeKeyIfEncrypted(value: boolean): void;
      get_encryptor(): boolean;
      set_encryptor(value: boolean): void;
      get_isEncrypted(): boolean;
      get_canEncrypt(): boolean;
      get_canDecrypt(): boolean;
      get_supportsStroke(): boolean;
      get_supportsFill(): boolean;
      get_canRotate(): boolean;
      get_hitTestInterior(): boolean;
      rotate(angle: number, origin: lt.LeadPointD): void;
      translate(offsetX: number, offsetY: number): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      apply(provider: AnnDataProvider, container: AnnContainer): void;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      scaleVector(scaleX: number, scaleY: number, unitVectorX: lt.LeadPointD, unitVectorY: lt.LeadPointD, centerPoint: lt.LeadPointD): void;
      constructor();
      friendlyName: string; // read-only
      primaryPicture: AnnPicture;
      secondaryPicture: AnnPicture;
      defaultPrimaryPicture: number;
      defaultSecondaryPicture: number;
      key: number;
      resetKeyIfApplied: boolean;
      serializeKeyIfEncrypted: boolean;
      encryptor: boolean;
      isEncrypted: boolean; // read-only
      canEncrypt: boolean; // read-only
      canDecrypt: boolean; // read-only
      supportsStroke: boolean; // read-only
      supportsFill: boolean; // read-only
      canRotate: boolean; // read-only
      hitTestInterior: boolean; // read-only
   }

   class AnnFreehandHotspotObject extends AnnPolylineObject {
      get_friendlyName(): string;
      get_supportsFill(): boolean;
      get_supportsStroke(): boolean;
      get_picture(): AnnPicture;
      set_picture(value: AnnPicture): void;
      get_defaultPicture(): number;
      set_defaultPicture(value: number): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      get_hitTestInterior(): boolean;
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      constructor();
      friendlyName: string; // read-only
      supportsFill: boolean; // read-only
      supportsStroke: boolean; // read-only
      picture: AnnPicture;
      defaultPicture: number;
      hitTestInterior: boolean; // read-only
   }

   class AnnGroupObject extends AnnRectangleObject {
      get_friendlyName(): string;
      get_children(): AnnObjectCollection;
      add_objectAdded(value: lt.LeadEventHandler): void;
      remove_objectAdded(value: lt.LeadEventHandler): void;
      add_objectRemoved(value: lt.LeadEventHandler): void;
      remove_objectRemoved(value: lt.LeadEventHandler): void;
      rotate(angle: number, origin: lt.LeadPointD): void;
      scale(scaleX: number, scaleY: number, origin: lt.LeadPointD): void;
      translate(offsetX: number, offsetY: number): void;
      constructor();
      friendlyName: string; // read-only
      children: AnnObjectCollection; // read-only
      objectAdded: lt.LeadEventType; // read-only
      objectRemoved: lt.LeadEventType; // read-only
   }

   class AnnHiliteObject extends AnnRectangleObject {
      get_friendlyName(): string;
      get_hiliteColor(): string;
      set_hiliteColor(value: string): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      get_hitTestInterior(): boolean;
      get_supportsStroke(): boolean;
      constructor();
      friendlyName: string; // read-only
      hiliteColor: string;
      hitTestInterior: boolean; // read-only
      supportsStroke: boolean; // read-only
   }

   class AnnTextObject extends AnnRectangleObject {
      get_friendlyName(): string;
      get_supportsFont(): boolean;
      get_supportsFill(): boolean;
      get_supportsContent(): boolean;
      get_text(): string;
      set_text(value: string): void;
      get_textRotate(): AnnTextRotate;
      set_textRotate(value: AnnTextRotate): void;
      get_horizontalAlignment(): AnnHorizontalAlignment;
      set_horizontalAlignment(value: AnnHorizontalAlignment): void;
      get_verticalAlignment(): AnnVerticalAlignment;
      set_verticalAlignment(value: AnnVerticalAlignment): void;
      get_textBackground(): AnnBrush;
      set_textBackground(value: AnnBrush): void;
      get_textForeground(): AnnBrush;
      set_textForeground(value: AnnBrush): void;
      get_padding(): AnnThickness;
      set_padding(value: AnnThickness): void;
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      create(): AnnObject;  // protected
      get_wordWrap(): boolean;
      set_wordWrap(value: boolean): void;
      constructor();
      friendlyName: string; // read-only
      supportsFont: boolean; // read-only
      supportsFill: boolean; // read-only
      supportsContent: boolean; // read-only
      text: string;
      textRotate: AnnTextRotate;
      horizontalAlignment: AnnHorizontalAlignment;
      verticalAlignment: AnnVerticalAlignment;
      textBackground: AnnBrush;
      textForeground: AnnBrush;
      padding: AnnThickness;
      wordWrap: boolean;
   }

   class AnnNoteObject extends AnnTextObject {
      get_friendlyName(): string;
      get_shadowBorderWidth(): lt.LeadLengthD;
      set_shadowBorderWidth(value: lt.LeadLengthD): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      constructor();
      friendlyName: string; // read-only
      shadowBorderWidth: lt.LeadLengthD;
   }

   class AnnPointerObject extends AnnPolylineObject {
      get_friendlyName(): string;
      get_supportsFill(): boolean;
      get_pointerPosition(): AnnPointerPosition;
      set_pointerPosition(value: AnnPointerPosition): void;
      get_arrowLength(): lt.LeadLengthD;
      set_arrowLength(value: lt.LeadLengthD): void;
      getArrowPoints(): lt.LeadPointD[];
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      create(): AnnObject;  // protected
      getBoundingRectangle(): lt.LeadRectD;  // protected
      constructor();
      friendlyName: string; // read-only
      supportsFill: boolean; // read-only
      pointerPosition: AnnPointerPosition;
      arrowLength: lt.LeadLengthD;
   }

   class AnnPointObject extends AnnObject {
      get_friendlyName(): string;
      get_supportsFill(): boolean;
      get_supportsStroke(): boolean;
      get_defaultPicture(): number;
      set_defaultPicture(value: number): void;
      get_picture(): AnnPicture;
      set_picture(value: AnnPicture): void;
      get_centerPoint(): lt.LeadPointD;
      set_centerPoint(value: lt.LeadPointD): void;
      get_radius(): lt.LeadLengthD;
      set_radius(value: lt.LeadLengthD): void;
      get_showPicture(): boolean;
      set_showPicture(value: boolean): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      getBoundingRectangle(): lt.LeadRectD;  // protected
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      get_canRotate(): boolean;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      constructor();
      friendlyName: string; // read-only
      supportsFill: boolean; // read-only
      supportsStroke: boolean; // read-only
      defaultPicture: number;
      picture: AnnPicture;
      centerPoint: lt.LeadPointD;
      radius: lt.LeadLengthD;
      showPicture: boolean;
      canRotate: boolean; // read-only
   }

   class AnnProtractorObject extends AnnPolyRulerObject {
      get_friendlyName(): string;
      get_supportsFill(): boolean;
      get_centerPoint(): lt.LeadPointD;
      set_centerPoint(value: lt.LeadPointD): void;
      get_firstPoint(): lt.LeadPointD;
      set_firstPoint(value: lt.LeadPointD): void;
      get_secondPoint(): lt.LeadPointD;
      set_secondPoint(value: lt.LeadPointD): void;
      get_acute(): boolean;
      set_acute(value: boolean): void;
      get_angularUnit(): AnnAngularUnit;
      set_angularUnit(value: AnnAngularUnit): void;
      get_angularUnitsAbbreviation(): { [key: number]: string };
      get_anglePrecision(): number;
      set_anglePrecision(value: number): void;
      get_arcRadius(): lt.LeadLengthD;
      set_arcRadius(value: lt.LeadLengthD): void;
      get_showArc(): boolean;
      set_showArc(value: boolean): void;
      getRulerLengthFromPoints(startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, calibrationScale: number): lt.LeadLengthD;
      getRulerLengthAsStringFromPoints(startPoint: lt.LeadPointD, endPoint: lt.LeadPointD, calibrationScale: number): string;
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      create(): AnnObject;  // protected
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      constructor();
      friendlyName: string; // read-only
      supportsFill: boolean; // read-only
      centerPoint: lt.LeadPointD;
      firstPoint: lt.LeadPointD;
      secondPoint: lt.LeadPointD;
      acute: boolean;
      angularUnit: AnnAngularUnit;
      angularUnitsAbbreviation: { [key: number]: string }; // read-only
      anglePrecision: number;
      arcRadius: lt.LeadLengthD;
      showArc: boolean;
   }

   class AnnRedactionObject extends AnnRectangleObject {
      get_friendlyName(): string;
      get_supportsStroke(): boolean;
      get_supportsOpacity(): boolean;
      get_canRotate(): boolean;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      get_imageData(): number[];
      set_imageData(value: number[]): void;
      get_isRealized(): boolean;
      get_canRestore(): boolean;
      realize(provider: AnnDataProvider, container: AnnContainer): void;
      restore(provider: AnnDataProvider, container: AnnContainer): void;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      constructor();
      friendlyName: string; // read-only
      supportsStroke: boolean; // read-only
      supportsOpacity: boolean; // read-only
      canRotate: boolean; // read-only
      imageData: number[];
      isRealized: boolean; // read-only
      canRestore: boolean; // read-only
   }

   class AnnRubberStampObject extends AnnRectangleObject {
      get_friendlyName(): string;
      get_supportsFill(): boolean;
      get_supportsStroke(): boolean;
      get_rubberStampType(): AnnRubberStampType;
      set_rubberStampType(value: AnnRubberStampType): void;
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      create(): AnnObject;  // protected
      get_hitTestInterior(): boolean;
      constructor();
      friendlyName: string; // read-only
      supportsFill: boolean; // read-only
      supportsStroke: boolean; // read-only
      rubberStampType: AnnRubberStampType;
      hitTestInterior: boolean; // read-only
   }

   class AnnSelectionObject extends AnnRectangleObject {
      get_alignmentTarget(): AnnObject;
      set_alignmentTarget(value: AnnObject): void;
      get_selectionOpacity(): number;
      set_selectionOpacity(value: number): void;
      get_supportsContent(): boolean;
      get_friendlyName(): string;
      get_selectedObjects(): AnnObjectCollection;
      adjustBounds(): void;
      group(groupName: string): void;
      ungroup(groupName: string): void;
      lock(password: string): void;
      unlock(password: string): void;
      get_supportsFill(): boolean;
      get_canRotate(): boolean;
      rotate(angle: number, origin: lt.LeadPointD): void;
      scale(scaleX: number, scaleY: number, origin: lt.LeadPointD): void;
      translate(offsetX: number, offsetY: number): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      get_hitTestInterior(): boolean;
      applyProperties(): void;
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      constructor();
      alignmentTarget: AnnObject;
      selectionOpacity: number;
      supportsContent: boolean; // read-only
      friendlyName: string; // read-only
      selectedObjects: AnnObjectCollection; // read-only
      supportsFill: boolean; // read-only
      canRotate: boolean; // read-only
      hitTestInterior: boolean; // read-only
   }

   class AnnStampObject extends AnnTextObject {
      get_friendlyName(): string;
      get_pictureSizeMode(): AnnSizeMode;
      set_pictureSizeMode(value: AnnSizeMode): void;
      get_picture(): AnnPicture;
      set_picture(value: AnnPicture): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      get_hitTestInterior(): boolean;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      constructor();
      friendlyName: string; // read-only
      pictureSizeMode: AnnSizeMode;
      picture: AnnPicture;
      hitTestInterior: boolean; // read-only
   }

   class AnnStickyNoteObject extends AnnObject {
      get_supportsStroke(): boolean;
      get_supportsSelectionStroke(): boolean;
      get_supportsFill(): boolean;
      get_supportsFont(): boolean;
      get_canRotate(): boolean;
      get_friendlyName(): string;
      get_hitTestInterior(): boolean;
      get_picture(): AnnPicture;
      set_picture(value: AnnPicture): void;
      get_defaultPicture(): number;
      set_defaultPicture(value: number): void;
      create(): AnnObject;  // protected
      scale(scaleX: number, scaleY: number, origin: lt.LeadPointD): void;
      scaleVector(scaleX: number, scaleY: number, unitVectorX: lt.LeadPointD, unitVectorY: lt.LeadPointD, centerPoint: lt.LeadPointD): void;
      clone(): AnnObject;
      getBoundingRectangle(): lt.LeadRectD;  // protected
      getInvalidateRect(mapper: AnnContainerMapper, renderer: IAnnObjectRenderer): lt.LeadRectD;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      constructor();
      supportsStroke: boolean; // read-only
      supportsSelectionStroke: boolean; // read-only
      supportsFill: boolean; // read-only
      supportsFont: boolean; // read-only
      canRotate: boolean; // read-only
      friendlyName: string; // read-only
      hitTestInterior: boolean; // read-only
      picture: AnnPicture;
      defaultPicture: number;
   }

   class AnnTextReviewObject extends AnnObject {
      get_canRotate(): boolean;
      get_hitTestInterior(): boolean;
      get_canTranslate(): boolean;
      set_canTranslate(value: boolean): void;
      get_canScale(): boolean;
      set_canScale(value: boolean): void;
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      get_supportsSelectionStroke(): boolean;
      get_supportsFill(): boolean;
      get_supportsStroke(): boolean;
      scale(scaleX: number, scaleY: number, origin: lt.LeadPointD): void;
      scaleVector(scaleX: number, scaleY: number, unitVectorX: lt.LeadPointD, unitVectorY: lt.LeadPointD, centerPoint: lt.LeadPointD): void;
      translate(offsetX: number, offsetY: number): void;
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      addRectangle(rect: lt.LeadRectD): void;
      setRectangles(rects: lt.LeadRectD[]): void;
      getRectangles(): lt.LeadRectD[];
      constructor();
      canRotate: boolean; // read-only
      hitTestInterior: boolean; // read-only
      canTranslate: boolean;
      canScale: boolean;
      supportsSelectionStroke: boolean; // read-only
      supportsFill: boolean; // read-only
      supportsStroke: boolean; // read-only
   }

   class AnnTextHiliteObject extends AnnTextReviewObject {
      create(): AnnObject;  // protected
      clone(): AnnObject;
      get_friendlyName(): string;
      constructor();
      friendlyName: string; // read-only
   }

   class AnnTextPointerObject extends AnnTextObject {
      get_friendlyName(): string;
      get_pointerPosition(): lt.LeadPointD;
      set_pointerPosition(value: lt.LeadPointD): void;
      get_fixedPointer(): boolean;
      set_fixedPointer(value: boolean): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      scale(scaleX: number, scaleY: number, origin: lt.LeadPointD): void;
      translate(offsetX: number, offsetY: number): void;
      rotate(angle: number, origin: lt.LeadPointD): void;
      hitTest(point: lt.LeadPointD, hitTestBuffer: number): boolean;
      getBoundingRectangle(): lt.LeadRectD;  // protected
      constructor();
      friendlyName: string; // read-only
      pointerPosition: lt.LeadPointD;
      fixedPointer: boolean;
   }

   class AnnTextRedactionObject extends AnnTextReviewObject {
      get_supportsOpacity(): boolean;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      get_friendlyName(): string;
      constructor();
      supportsOpacity: boolean; // read-only
      friendlyName: string; // read-only
   }

   class AnnTextRollupObject extends AnnNoteObject {
      get_friendlyName(): string;
      get_expanded(): boolean;
      set_expanded(value: boolean): void;
      create(): AnnObject;  // protected
      clone(): AnnObject;
      serialize(options: AnnSerializeOptions, parentNode: Node, document: Document): void;
      deserialize(options: AnnDeserializeOptions, element: Node, document: Document): void;
      constructor();
      friendlyName: string; // read-only
      expanded: boolean;
   }

   class AnnTextStrikeoutObject extends AnnTextReviewObject {
      create(): AnnObject;  // protected
      clone(): AnnObject;
      get_friendlyName(): string;
      constructor();
      friendlyName: string; // read-only
   }

   class AnnTextUnderlineObject extends AnnTextReviewObject {
      create(): AnnObject;  // protected
      clone(): AnnObject;
      get_friendlyName(): string;
      constructor();
      friendlyName: string; // read-only
   }

   interface AnnLoadPictureEventHandler {
      (sender: any, e: AnnLoadPictureEventArgs): void;
   }

   class AnnLoadPictureEventType extends lt.LeadEvent {
      add(value: AnnLoadPictureEventHandler): AnnLoadPictureEventHandler;
      remove(value: AnnLoadPictureEventHandler): void;
   }

   class AnnLoadPictureEventArgs extends lt.LeadEventArgs {
      static create(picture: AnnPicture, annObject: AnnObject, container: AnnContainer, error: Error): AnnLoadPictureEventArgs;
      get_picture(): AnnPicture;
      get_annObject(): AnnObject;
      get_container(): AnnContainer;
      get_error(): Error;
      picture: AnnPicture; // read-only
      annObject: AnnObject; // read-only
      container: AnnContainer; // read-only
      error: Error; // read-only
   }

   class AnnRenderingEngine {
      get_stateless(): boolean;
      get_snapToGridOptions(): AnnSnapToGridOptions;
      set_snapToGridOptions(value: AnnSnapToGridOptions): void;
      burnToRect(destinationRect: lt.LeadRectD): void;
      burnToRectWithDpi(destinationRect: lt.LeadRectD, sourceDpiX: number, sourceDpiY: number, targetDpiX: number, targetDpiY: number): void;
      get_renderers(): { [key: number]: IAnnObjectRenderer };
      set_renderers(value: { [key: number]: IAnnObjectRenderer }): void;
      static get_containerLabelRenderer(): IAnnLabelRenderer;
      static set_containerLabelRenderer(value: IAnnLabelRenderer): void;
      attachContainer(container: AnnContainer): void;
      attach(container: AnnContainer, context: any): void;
      detach(): void;
      get_container(): AnnContainer;
      get_clipRectangle(): lt.LeadRectD;
      get_loadingPictureStroke(): AnnStroke;
      set_loadingPictureStroke(value: AnnStroke): void;
      get_loadingPictureFill(): AnnBrush;
      set_loadingPictureFill(value: AnnBrush): void;
      get_renderState(): AnnRenderState;
      set_renderState(value: AnnRenderState): void;
      get_resources(): AnnResources;
      set_resources(value: AnnResources): void;
      burn(): void;
      render(clipRect: lt.LeadRectD, runMode: boolean): void;
      renderLayer(clipRect: lt.LeadRectD, layer: AnnLayer, container: AnnContainer, runMode: boolean): void;
      renderLayers(clipRect: lt.LeadRectD, layers: AnnLayerCollection, container: AnnContainer, runMode: boolean): void;
      renderContainer(clipRect: lt.LeadRectD, container: AnnContainer, runMode: boolean): void;
      add_loadPicture(value: AnnLoadPictureEventHandler): void;
      remove_loadPicture(value: AnnLoadPictureEventHandler): void;
      onLoadPicture(e: AnnLoadPictureEventArgs): void;
      measureString(text: string, font: AnnFont): lt.LeadSizeD;
      renderGrid(runMode: boolean, container: AnnContainer): void;
      constructor();
      stateless: boolean; // read-only
      snapToGridOptions: AnnSnapToGridOptions;
      renderers: { [key: number]: IAnnObjectRenderer };
      static containerLabelRenderer: IAnnLabelRenderer;
      container: AnnContainer; // read-only
      clipRectangle: lt.LeadRectD; // read-only
      loadingPictureStroke: AnnStroke;
      loadingPictureFill: AnnBrush;
      renderState: AnnRenderState;
      resources: AnnResources;
      loadPicture: AnnLoadPictureEventType; // read-only
   }

   enum AnnHitTestBehavior {
      intersects = 0,
      contains = 1
   }

   enum AnnKeys {
      none = 0,
      back = 8,
      tab = 9,
      enter = 13,
      shiftKey = 16,
      controlKey = 17,
      menu = 18,
      escape = 27,
      space = 32,
      insert = 45,
      deleteKey = 46,
      keyCode = 65535,
      shift = 65536,
      control = 131072,
      alt = 262144,
      modifiers = -65536
   }

   enum AnnPointerPosition {
      start = 0,
      end = 1
   }

   enum AnnTextDecorations {
      none = 0,
      baseline = 1,
      overLine = 2,
      strikethrough = 4,
      underline = 8
   }

   enum AnnUserMode {
      design = 0,
      run = 1,
      render = 2
   }

   enum AnnFixedStateOperations {
      none = 0,
      scrolling = 1,
      zooming = 2,
      fontSize = 4,
      strokeWidth = 8,
      lengthValue = 16
   }

   enum AnnNotifyCollectionChangedAction {
      add = 0,
      remove = 1,
      replace = 2,
      move = 3,
      reset = 4
   }

   enum AnnFontWeight {
      normal = 0,
      thin = 1,
      extraLight = 2,
      light = 3,
      medium = 4,
      semiBold = 5,
      bold = 6,
      extraBold = 7,
      black = 8,
      extraBlack = 9
   }

   enum AnnFontStyle {
      normal = 0,
      italic = 1,
      oblique = 2
   }

   enum AnnFontStretch {
      normal = 0,
      ultraCondensed = 1,
      extraCondensed = 2,
      condensed = 3,
      semiCondensed = 4,
      semiExpanded = 5,
      expanded = 6,
      extraExpanded = 7,
      ultraExpanded = 8
   }

   enum AnnMouseButton {
      none = 0,
      left = 1,
      right = 2,
      middle = 3
   }

   enum AnnUnit {
      unit = 0,
      display = 1,
      document = 2,
      smartEnglish = 3,
      smartMetric = 4,
      inch = 5,
      millimeter = 6,
      point = 7,
      feet = 8,
      yard = 9,
      micrometer = 10,
      centimeter = 11,
      meter = 12,
      twip = 13,
      pixel = 14
   }

   enum AnnDesignerOperationStatus {
      idle = 0,
      start = 1,
      working = 2,
      end = 3,
      canceled = 4
   }

   enum AnnEditDesignerOperation {
      none = 0,
      moveThumb = 1,
      move = 2,
      moveName = 3,
      rotate = 4,
      moveRotateCenterThumb = 5,
      moveRotateGripperThumb = 6
   }

   enum AnnAngularUnit {
      radian = 0,
      degree = 1
   }

   enum AnnFillRule {
      evenOdd = 0,
      nonzero = 1
   }

   enum AnnTextRotate {
      rotate0 = 0,
      rotate90 = 1,
      rotate180 = 2,
      rotate270 = 3
   }

   enum AnnHorizontalAlignment {
      left = 0,
      center = 1,
      right = 2
   }

   enum AnnVerticalAlignment {
      top = 0,
      center = 1,
      bottom = 2
   }

   enum AnnStrokeLineCap {
      flat = 0,
      square = 1,
      round = 2,
      triangle = 3
   }

   enum AnnStrokeLineJoin {
      miter = 0,
      bevel = 1,
      round = 2,
      miterClipped = 3
   }

   enum AnnTransparentMode {
      none = 0,
      useColor = 1,
      topLeftPixel = 2
   }

   enum AnnRubberStampType {
      stampApproved = 0,
      stampAssigned = 1,
      stampChecked = 2,
      stampClient = 3,
      stampCopy = 4,
      stampDraft = 5,
      stampExtended = 6,
      stampFax = 7,
      stampFaxed = 8,
      stampImportant = 9,
      stampInvoice = 10,
      stampNotice = 11,
      stampOfficial = 12,
      stampOnFile = 13,
      stampPaid = 14,
      stampPassed = 15,
      stampPending = 16,
      stampProcessed = 17,
      stampReceived = 18,
      stampRejected = 19,
      stampRelease = 20,
      stampSent = 21,
      stampShipped = 22,
      stampTopSecret = 23,
      stampUrgent = 24,
      stampVoid = 25
   }

   enum AnnResizeMode {
      fit = 0,
      fitAlways = 1,
      fitWidth = 2,
      fitHeight = 3,
      stretch = 4
   }

   enum AnnResizeContainerFlags {
      none = 0,
      resizeObjects = 1,
      autoCalibrate = 2
   }

   enum AnnLabelRestriction {
      none = 0,
      restrictToContainer = 1,
      restrictToObjectBounds = 2,
      restrictToUserRect = 4
   }

   enum AnnLabelPositionMode {
      normal = 0,
      relativeToObject = 1
   }

   enum AnnRenderState {
      none = 0,
      lock = 1,
      content = 2,
      label = 4,
      all = 7
   }

   enum AnnLinearGradientMode {
      horizontal = 0,
      vertical = 1,
      diagonal = 2
   }

   enum AnnSizeMode {
      actualSize = 0,
      stretch = 1
   }

   enum AnnStrokeAlignment {
      center = 0,
      inset = 1
   }

   interface AnnDrawDesignerEventHandler {
      (sender: any, e: AnnDrawDesignerEventArgs): void;
   }

   class AnnDrawDesignerEventType extends lt.LeadEvent {
      add(value: AnnDrawDesignerEventHandler): AnnDrawDesignerEventHandler;
      remove(value: AnnDrawDesignerEventHandler): void;
   }

   class AnnDrawDesignerEventArgs extends lt.LeadEventArgs {
      static create(obj: AnnObject, operationStatus: AnnDesignerOperationStatus): AnnDrawDesignerEventArgs;
      get_object(): AnnObject;
      get_operationStatus(): AnnDesignerOperationStatus;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      object: AnnObject; // read-only
      operationStatus: AnnDesignerOperationStatus; // read-only
      cancel: boolean;
   }

   interface AnnEditDesignerEventHandler {
      (sender: any, e: AnnEditDesignerEventArgs): void;
   }

   class AnnEditDesignerEventType extends lt.LeadEvent {
      add(value: AnnEditDesignerEventHandler): AnnEditDesignerEventHandler;
      remove(value: AnnEditDesignerEventHandler): void;
   }

   class AnnEditDesignerEventArgs extends lt.LeadEventArgs {
      static create(obj: AnnObject, operation: AnnEditDesignerOperation, moveThumbIndex: number, operationStatus: AnnDesignerOperationStatus): AnnEditDesignerEventArgs;
      get_object(): AnnObject;
      get_operation(): AnnEditDesignerOperation;
      get_operationStatus(): AnnDesignerOperationStatus;
      get_moveThumbIndex(): number;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      object: AnnObject; // read-only
      operation: AnnEditDesignerOperation; // read-only
      operationStatus: AnnDesignerOperationStatus; // read-only
      moveThumbIndex: number; // read-only
      cancel: boolean;
   }

   interface AnnEditContentEventHandler {
      (sender: any, e: AnnEditContentEventArgs): void;
   }

   class AnnEditContentEventType extends lt.LeadEvent {
      add(value: AnnEditContentEventHandler): AnnEditContentEventHandler;
      remove(value: AnnEditContentEventHandler): void;
   }

   class AnnEditContentEventArgs extends lt.LeadEventArgs {
      static create(annObject: AnnObject, bounds: lt.LeadRectD): AnnEditContentEventArgs;
      get_targetObject(): AnnObject;
      get_bounds(): lt.LeadRectD;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      targetObject: AnnObject; // read-only
      bounds: lt.LeadRectD; // read-only
      cancel: boolean;
   }

   interface AnnEditTextEventHandler {
      (sender: any, e: AnnEditTextEventArgs): void;
   }

   class AnnEditTextEventType extends lt.LeadEvent {
      add(value: AnnEditTextEventHandler): AnnEditTextEventHandler;
      remove(value: AnnEditTextEventHandler): void;
   }

   class AnnEditTextEventArgs extends lt.LeadEventArgs {
      static create(textObject: AnnTextObject, bounds: lt.LeadRectD): AnnEditTextEventArgs;
      get_textObject(): AnnTextObject;
      get_bounds(): lt.LeadRectD;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      textObject: AnnTextObject; // read-only
      bounds: lt.LeadRectD; // read-only
      cancel: boolean;
   }

   interface AnnLockObjectEventHandler {
      (sender: any, e: AnnLockObjectEventArgs): void;
   }

   class AnnLockObjectEventType extends lt.LeadEvent {
      add(value: AnnLockObjectEventHandler): AnnLockObjectEventHandler;
      remove(value: AnnLockObjectEventHandler): void;
   }

   class AnnLockObjectEventArgs extends lt.LeadEventArgs {
      static create(obj: AnnObject): AnnLockObjectEventArgs;
      get_object(): AnnObject;
      get_password(): string;
      set_password(value: string): void;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      object: AnnObject; // read-only
      password: string;
      cancel: boolean;
   }

   interface AnnPointerEventHandler {
      (sender: any, e: AnnPointerEventArgs): void;
   }

   class AnnPointerEventType extends lt.LeadEvent {
      add(value: AnnPointerEventHandler): AnnPointerEventHandler;
      remove(value: AnnPointerEventHandler): void;
   }

   class AnnPointerEventArgs extends lt.LeadEventArgs {
      static create(button: AnnMouseButton, point: lt.LeadPointD): AnnPointerEventArgs;
      clone(): AnnPointerEventArgs;
      get_button(): AnnMouseButton;
      get_location(): lt.LeadPointD;
      set_location(value: lt.LeadPointD): void;
      get_isHandled(): boolean;
      set_isHandled(value: boolean): void;
      constructor();
      button: AnnMouseButton; // read-only
      location: lt.LeadPointD;
      isHandled: boolean;
   }

   interface AnnNotifyCollectionChangedEventHandler {
      (sender: any, e: AnnNotifyCollectionChangedEventArgs): void;
   }

   class AnnNotifyCollectionChangedEventType extends lt.LeadEvent {
      add(value: AnnNotifyCollectionChangedEventHandler): AnnNotifyCollectionChangedEventHandler;
      remove(value: AnnNotifyCollectionChangedEventHandler): void;
   }

   class AnnNotifyCollectionChangedEventArgs extends lt.LeadEventArgs {
      static create(action: AnnNotifyCollectionChangedAction): AnnNotifyCollectionChangedEventArgs;
      get_action(): AnnNotifyCollectionChangedAction;
      get_newItems(): any[];
      get_newStartingIndex(): number;
      get_oldItems(): any[];
      get_oldStartingIndex(): number;
      constructor();
      action: AnnNotifyCollectionChangedAction; // read-only
      newItems: any[]; // read-only
      newStartingIndex: number; // read-only
      oldItems: any[]; // read-only
      oldStartingIndex: number; // read-only
   }

   interface AnnObjectCollectionEventHandler {
      (sender: any, e: AnnObjectCollectionEventArgs): void;
   }

   class AnnObjectCollectionEventType extends lt.LeadEvent {
      add(value: AnnObjectCollectionEventHandler): AnnObjectCollectionEventHandler;
      remove(value: AnnObjectCollectionEventHandler): void;
   }

   class AnnObjectCollectionEventArgs extends lt.LeadEventArgs {
      static create(obj: AnnObject): AnnObjectCollectionEventArgs;
      get_object(): AnnObject;
      constructor();
      object: AnnObject; // read-only
   }

   enum PropertyChangedStatus {
      after = 0,
      before = 1
   }

   interface AnnPropertyChangedEventHandler {
      (sender: any, e: AnnPropertyChangedEventArgs): void;
   }

   class AnnPropertyChangedEventType extends lt.LeadEvent {
      add(value: AnnPropertyChangedEventHandler): AnnPropertyChangedEventHandler;
      remove(value: AnnPropertyChangedEventHandler): void;
   }

   class AnnPropertyChangedEventArgs extends lt.LeadEventArgs {
      get_propertyName(): string;
      get_oldValue(): any;
      get_newValue(): any;
      get_status(): PropertyChangedStatus;
      set_status(value: PropertyChangedStatus): void;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor(propertyName: string, status: PropertyChangedStatus, oldValue: any, newValue: any);
      propertyName: string; // read-only
      oldValue: any; // read-only
      newValue: any; // read-only
      status: PropertyChangedStatus;
      cancel: boolean;
   }

   interface AnnRunDesignerEventHandler {
      (sender: any, e: AnnRunDesignerEventArgs): void;
   }

   class AnnRunDesignerEventType extends lt.LeadEvent {
      add(value: AnnRunDesignerEventHandler): AnnRunDesignerEventHandler;
      remove(value: AnnRunDesignerEventHandler): void;
   }

   class AnnRunDesignerEventArgs extends lt.LeadEventArgs {
      static create(obj: AnnObject, operationStatus: AnnDesignerOperationStatus): AnnRunDesignerEventArgs;
      get_object(): AnnObject;
      get_operationStatus(): AnnDesignerOperationStatus;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      object: AnnObject; // read-only
      operationStatus: AnnDesignerOperationStatus; // read-only
      cancel: boolean;
   }

   interface AnnToolTipEventHandler {
      (sender: any, e: AnnToolTipEventArgs): void;
   }

   class AnnToolTipEventType extends lt.LeadEvent {
      add(value: AnnToolTipEventHandler): AnnToolTipEventHandler;
      remove(value: AnnToolTipEventHandler): void;
   }

   class AnnToolTipEventArgs extends lt.LeadEventArgs {
      static create(annObject: AnnObject, bounds: lt.LeadRectD): AnnToolTipEventArgs;
      get_annotationObject(): AnnObject;
      get_bounds(): lt.LeadRectD;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      annotationObject: AnnObject; // read-only
      bounds: lt.LeadRectD; // read-only
      cancel: boolean;
   }

   class AnnBrush {
      createBrush(): AnnBrush;  // protected
      clone(): AnnBrush;
      constructor();
   }

   class AnnSolidColorBrush extends AnnBrush {
      static create(color: string): AnnBrush;
      get_color(): string;
      set_color(value: string): void;
      createBrush(): AnnBrush;  // protected
      clone(): AnnBrush;
      constructor();
      color: string;
   }

   class AnnGradientStop {
      get_color(): string;
      set_color(value: string): void;
      get_offset(): number;
      set_offset(value: number): void;
      clone(): AnnGradientStop;
      constructor(color: string, offset: number);
      color: string;
      offset: number;
   }

   class AnnGradientBrush extends AnnBrush {
      get_gradientStops(): AnnGradientStopCollection;
      clone(): AnnBrush;
      constructor();
      gradientStops: AnnGradientStopCollection; // read-only
   }

   class AnnLinearGradientBrush extends AnnGradientBrush {
      get_linearGradientMode(): AnnLinearGradientMode;
      set_linearGradientMode(value: AnnLinearGradientMode): void;
      createBrush(): AnnBrush;  // protected
      static create(mode: AnnLinearGradientMode): AnnBrush;
      clone(): AnnBrush;
      constructor();
      linearGradientMode: AnnLinearGradientMode;
   }

   class AnnContainerCollection {
      get_count(): number;
      clear(): void;
      remove(annContainer: AnnContainer): void;
      add(annContainer: AnnContainer): void;
      contains(annContainer: AnnContainer): boolean;
      getEnumerator(): any;
      get_item(i: number): AnnContainer;
      set_item(i: number, value: AnnContainer): void;
      toArray(): AnnContainer[];
      insertItem(index: number, item: AnnContainer): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: AnnNotifyCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(annContainer: AnnContainer): void;  // protected
      setItem(index: number, item: AnnContainer): void;  // protected
      clearItems(): void;  // protected
      indexOf(annContainer: AnnContainer): number;
      add_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      remove_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      sendToBack(obj: AnnContainer, last: boolean): void;
      bringToFront(obj: AnnContainer, first: boolean): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: AnnNotifyCollectionChangedEventType; // read-only
   }

   class AnnGradientStopCollection {
      get_count(): number;
      clear(): void;
      remove(annGradientStop: AnnGradientStop): void;
      add(annGradientStop: AnnGradientStop): void;
      contains(annGradientStop: AnnGradientStop): boolean;
      getEnumerator(): any;
      get_item(i: number): AnnGradientStop;
      set_item(i: number, value: AnnGradientStop): void;
      toArray(): AnnGradientStop[];
      insertItem(index: number, item: AnnGradientStop): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: AnnNotifyCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(annGradientStop: AnnGradientStop): void;  // protected
      setItem(index: number, item: AnnGradientStop): void;  // protected
      clearItems(): void;  // protected
      indexOf(annGradientStop: AnnGradientStop): number;
      add_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      remove_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      sendToBack(annGradientStop: AnnGradientStop, last: boolean): void;
      bringToFront(annGradientStop: AnnGradientStop, first: boolean): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: AnnNotifyCollectionChangedEventType; // read-only
   }

   class AnnDataProvider {
      get_canRead(): boolean;
      get_canWrite(): boolean;
      getImageData(container: AnnContainer, bounds: lt.LeadRectD): number[];
      setImageData(container: AnnContainer, bounds: lt.LeadRectD, data: number[]): void;
      encrypt(container: AnnContainer, bounds: lt.LeadRectD, key: number): void;
      decrypt(container: AnnContainer, bounds: lt.LeadRectD, key: number): void;
      fill(container: AnnContainer, bounds: lt.LeadRectD, color: string): void;
      constructor();
      canRead: boolean; // read-only
      canWrite: boolean; // read-only
   }

   class AnnDouble {
      static isNaN(d: number): boolean;
      static isInfinity(d: number): boolean;
      static parseInvariantCulture(input: string): number;
      static naN: number;
      static positiveInfinity: number;
      static negativeInfinity: number;
   }

   class AnnFont {
      add_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      onPropertyChanged(e: AnnPropertyChangedEventArgs): void;
      get_fontFamilyName(): string;
      set_fontFamilyName(value: string): void;
      get_fontSize(): number;
      set_fontSize(value: number): void;
      get_fontStretch(): AnnFontStretch;
      set_fontStretch(value: AnnFontStretch): void;
      get_fontStyle(): AnnFontStyle;
      set_fontStyle(value: AnnFontStyle): void;
      get_fontWeight(): AnnFontWeight;
      set_fontWeight(value: AnnFontWeight): void;
      clone(): AnnFont;
      get_isDirty(): boolean;
      set_isDirty(value: boolean): void;
      get_fontHeight(): number;
      set_fontHeight(value: number): void;
      get_textDecoration(): AnnTextDecorations;
      set_textDecoration(value: AnnTextDecorations): void;
      constructor(fontFamilyName: string, sizeInPoints: number);
      fontFamilyName: string;
      fontSize: number;
      fontStretch: AnnFontStretch;
      fontStyle: AnnFontStyle;
      fontWeight: AnnFontWeight;
      isDirty: boolean;
      fontHeight: number;
      textDecoration: AnnTextDecorations;
      propertyChanged: AnnPropertyChangedEventType; // read-only
   }

   enum AnnOperationType {
      createObjects = 0,
      deleteObjects = 1,
      editObjects = 2,
      lockObjects = 3,
      unlockObjects = 4,
      realizeRedact = 5,
      restoreRedact = 6,
      save = 7,
      load = 8,
      burnObjects = 9,
      copyObjects = 10,
      pasteObjects = 11,
      encryptObjects = 12,
      decryptObjects = 13,
      renderingObjects = 14,
      hitTestObjects = 15
   }

   interface AnnOperationInfoEventHandler {
      (sender: any, e: AnnOperationInfoEventArgs): void;
   }

   class AnnOperationInfoEventType extends lt.LeadEvent {
      add(value: AnnOperationInfoEventHandler): AnnOperationInfoEventHandler;
      remove(value: AnnOperationInfoEventHandler): void;
   }

   class AnnOperationInfoEventArgs extends lt.LeadEventArgs {
      get_type(): AnnOperationType;
      get_annObject(): AnnObject;
      get_role(): string;
      set_role(value: string): void;
      get_ignoreUserCheck(): boolean;
      set_ignoreUserCheck(value: boolean): void;
      constructor(type: AnnOperationType, annObject: AnnObject);
      type: AnnOperationType; // read-only
      annObject: AnnObject; // read-only
      role: string;
      ignoreUserCheck: boolean;
   }

   class AnnRoles {
      get_count(): number;
      clear(): void;
      remove(item: string): void;
      add(item: string): void;
      contains(item: string): boolean;
      getEnumerator(): any;
      get_item(i: number): string;
      set_item(i: number, value: string): void;
      toArray(): string[];
      insertItem(index: number, item: string): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      removeAt(index: number): void;
      removeItem(item: string): void;  // protected
      setItem(index: number, item: string): void;  // protected
      clearItems(): void;  // protected
      indexOf(item: string): number;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      static view: string;
      static edit: string;
      static viewAll: string;
      static editAll: string;
      static fullControl: string;
   }

   class AnnGroupsRoles {
      add_generateRole(value: AnnOperationInfoEventHandler): void;
      remove_generateRole(value: AnnOperationInfoEventHandler): void;
      get_currentUser(): string;
      set_currentUser(value: string): void;
      get_groupUsers(): { [key: string]: string[] };
      get_groupRoles(): { [key: string]: AnnRoles };
      getUserGroup(userName: string): string[];
      getUserRoles(userName: string): AnnRoles;
      onGenerateRole(info: AnnOperationInfoEventArgs): string;  // protected
      isCurrentUserInRole(info: AnnOperationInfoEventArgs): boolean;
      isUserInRole(userName: string, info: AnnOperationInfoEventArgs): boolean;
      constructor();
      currentUser: string;
      groupUsers: { [key: string]: string[] }; // read-only
      groupRoles: { [key: string]: AnnRoles }; // read-only
      generateRole: AnnOperationInfoEventType; // read-only
   }

   class AnnLabel {
      clone(): AnnLabel;
      get_stateId(): string;
      set_stateId(value: string): void;
      get_font(): AnnFont;
      set_font(value: AnnFont): void;
      get_text(): string;
      set_text(value: string): void;
      get_originalPosition(): lt.LeadPointD;
      set_originalPosition(value: lt.LeadPointD): void;
      get_positionMode(): AnnLabelPositionMode;
      set_positionMode(value: AnnLabelPositionMode): void;
      get_parent(): AnnObject;
      set_parent(value: AnnObject): void;
      get_offsetHeight(): boolean;
      set_offsetHeight(value: boolean): void;
      get_offset(): lt.LeadPointD;
      set_offset(value: lt.LeadPointD): void;
      get_background(): AnnBrush;
      set_background(value: AnnBrush): void;
      get_foreground(): AnnBrush;
      set_foreground(value: AnnBrush): void;
      get_isVisible(): boolean;
      set_isVisible(value: boolean): void;
      get_renderedLabelBounds(): lt.LeadRectD;
      set_renderedLabelBounds(value: lt.LeadRectD): void;
      get_restrictionMode(): AnnLabelRestriction;
      set_restrictionMode(value: AnnLabelRestriction): void;
      get_restrictionRectangle(): lt.LeadRectD;
      set_restrictionRectangle(value: lt.LeadRectD): void;
      constructor();
      stateId: string;
      font: AnnFont;
      text: string;
      originalPosition: lt.LeadPointD;
      positionMode: AnnLabelPositionMode;
      parent: AnnObject;
      offsetHeight: boolean;
      offset: lt.LeadPointD;
      background: AnnBrush;
      foreground: AnnBrush;
      isVisible: boolean;
      renderedLabelBounds: lt.LeadRectD;
      restrictionMode: AnnLabelRestriction;
      restrictionRectangle: lt.LeadRectD;
   }

   class AnnLayerCollection {
      get_owner(): AnnLayer;
      get_count(): number;
      clear(): void;
      remove(annLayer: AnnLayer): void;
      add(annLayer: AnnLayer): void;
      contains(annLayer: AnnLayer): boolean;
      getEnumerator(): any;
      get_item(i: number): AnnLayer;
      set_item(i: number, value: AnnLayer): void;
      toArray(): AnnLayer[];
      insertItem(index: number, item: AnnLayer): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: AnnNotifyCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(annLayer: AnnLayer): void;  // protected
      setItem(index: number, item: AnnLayer): void;  // protected
      clearItems(): void;  // protected
      indexOf(annLayer: AnnLayer): number;
      add_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      remove_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      sendToBack(layer: AnnLayer, last: boolean): void;
      bringToFront(layer: AnnLayer, first: boolean): void;
      constructor(owner: AnnLayer);
      owner: AnnLayer; // read-only
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: AnnNotifyCollectionChangedEventType; // read-only
   }

   class AnnObjectCollection {
      get_count(): number;
      clear(): void;
      remove(annObject: AnnObject): void;
      add(annObject: AnnObject): void;
      contains(annObject: AnnObject): boolean;
      getEnumerator(): any;
      get_item(i: number): AnnObject;
      set_item(i: number, value: AnnObject): void;
      toArray(): AnnObject[];
      insertItem(index: number, item: AnnObject): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: AnnNotifyCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(annObject: AnnObject): void;  // protected
      setItem(index: number, item: AnnObject): void;  // protected
      clearItems(): void;  // protected
      indexOf(annObject: AnnObject): number;
      add_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      remove_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      sendToBack(obj: AnnObject, last: boolean): void;
      bringToFront(obj: AnnObject, first: boolean): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: AnnNotifyCollectionChangedEventType; // read-only
   }

   class AnnLayer {
      static create(name: string): AnnLayer;
      get_parent(): AnnLayer;
      get_name(): string;
      set_name(value: string): void;
      get_layerId(): string;
      set_layerId(value: string): void;
      get_isVisible(): boolean;
      set_isVisible(value: boolean): void;
      get_children(): AnnObjectCollection;
      get_layers(): AnnLayerCollection;
      constructor();
      parent: AnnLayer;
      name: string;
      layerId: string;
      isVisible: boolean;
      children: AnnObjectCollection; // read-only
      layers: AnnLayerCollection; // read-only
   }

   class AnnMedia {
      clone(): AnnMedia;
      get_source1(): string;
      set_source1(value: string): void;
      get_type1(): string;
      set_type1(value: string): void;
      get_source2(): string;
      set_source2(value: string): void;
      get_type2(): string;
      set_type2(value: string): void;
      get_source3(): string;
      set_source3(value: string): void;
      get_type3(): string;
      set_type3(value: string): void;
      constructor();
      source1: string;
      type1: string;
      source2: string;
      type2: string;
      source3: string;
      type3: string;
   }

   class AnnPicture {
      get_isDirty(): boolean;
      set_isDirty(value: boolean): void;
      get_isLoaded(): boolean;
      set_isLoaded(value: boolean): void;
      get_internalData(): any;
      set_internalData(value: any): void;
      get_source(): string;
      set_source(value: string): void;
      get_pictureData(): string;
      set_pictureData(value: string): void;
      clone(): AnnPicture;
      get_width(): number;
      set_width(value: number): void;
      get_height(): number;
      set_height(value: number): void;
      static get_empty(): AnnPicture;
      constructor(source: string);
      isDirty: boolean;
      isLoaded: boolean;
      internalData: any;
      source: string;
      pictureData: string;
      width: number;
      height: number;
      static empty: AnnPicture; // read-only
   }

   class LeadPointCollection {
      get_count(): number;
      getEnumerator(): any;
      contains(point: lt.LeadPointD): boolean;
      removeAt(index: number): void;
      add(point: lt.LeadPointD): void;
      clear(): void;
      get_item(i: number): lt.LeadPointD;
      set_item(i: number, value: lt.LeadPointD): void;
      toArray(): lt.LeadPointD[];
      insertItem(index: number, item: lt.LeadPointD): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: AnnNotifyCollectionChangedEventArgs): void;  // protected
      removeItem(point: lt.LeadPointD): void;  // protected
      setItem(index: number, item: lt.LeadPointD): void;  // protected
      clearItems(): void;  // protected
      add_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      remove_collectionChanged(value: AnnNotifyCollectionChangedEventHandler): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: AnnNotifyCollectionChangedEventType; // read-only
   }

   class AnnResources {
      get_images(): AnnPicture[];
      get_rubberStamps(): { [key: number]: AnnPicture };
      constructor();
      images: AnnPicture[]; // read-only
      rubberStamps: { [key: number]: AnnPicture }; // read-only
   }

   class AnnReview {
      clone(): AnnReview;
      get_author(): string;
      set_author(value: string): void;
      get_date(): Date;
      set_date(value: Date): void;
      get_status(): string;
      set_status(value: string): void;
      get_isChecked(): boolean;
      set_isChecked(value: boolean): void;
      get_comment(): string;
      set_comment(value: string): void;
      get_replies(): AnnReview[];
      toString(): string;
      constructor();
      author: string;
      date: Date;
      status: string;
      isChecked: boolean;
      comment: string;
      replies: AnnReview[]; // read-only
      static none: string;
      static created: string;
      static modified: string;
      static cancelled: string;
      static rejected: string;
      static accepted: string;
      static completed: string;
      static reply: string;
   }

   class AnnSnapToGridOptions {
      add_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      clone(): AnnSnapToGridOptions;
      get_gridStroke(): AnnStroke;
      set_gridStroke(value: AnnStroke): void;
      get_gridLength(): number;
      set_gridLength(value: number): void;
      get_lineSpacing(): number;
      set_lineSpacing(value: number): void;
      get_enableSnap(): boolean;
      set_enableSnap(value: boolean): void;
      get_showGrid(): boolean;
      set_showGrid(value: boolean): void;
      get_opacity(): number;
      set_opacity(value: number): void;
      onPropertyChanged(e: AnnPropertyChangedEventArgs): void;
      constructor();
      gridStroke: AnnStroke;
      gridLength: number;
      lineSpacing: number;
      enableSnap: boolean;
      showGrid: boolean;
      opacity: number;
      propertyChanged: AnnPropertyChangedEventType; // read-only
   }

   class AnnStroke {
      add_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      remove_propertyChanged(value: AnnPropertyChangedEventHandler): void;
      static create(stroke: AnnBrush, thickness: lt.LeadLengthD): AnnStroke;
      onPropertyChanged(e: AnnPropertyChangedEventArgs): void;
      get_stroke(): AnnBrush;
      set_stroke(value: AnnBrush): void;
      get_strokeDashArray(): number[];
      set_strokeDashArray(value: number[]): void;
      get_strokeDashCap(): AnnStrokeLineCap;
      set_strokeDashCap(value: AnnStrokeLineCap): void;
      get_strokeDashOffset(): number;
      set_strokeDashOffset(value: number): void;
      get_strokeEndLineCap(): AnnStrokeLineCap;
      set_strokeEndLineCap(value: AnnStrokeLineCap): void;
      get_strokeLineJoin(): AnnStrokeLineJoin;
      set_strokeLineJoin(value: AnnStrokeLineJoin): void;
      get_strokeMiterLimit(): number;
      set_strokeMiterLimit(value: number): void;
      get_strokeStartLineCap(): AnnStrokeLineCap;
      set_strokeStartLineCap(value: AnnStrokeLineCap): void;
      get_strokeThickness(): lt.LeadLengthD;
      set_strokeThickness(value: lt.LeadLengthD): void;
      get_strokeAlignment(): AnnStrokeAlignment;
      set_strokeAlignment(value: AnnStrokeAlignment): void;
      clone(): AnnStroke;
      constructor();
      stroke: AnnBrush;
      strokeDashArray: number[];
      strokeDashCap: AnnStrokeLineCap;
      strokeDashOffset: number;
      strokeEndLineCap: AnnStrokeLineCap;
      strokeLineJoin: AnnStrokeLineJoin;
      strokeMiterLimit: number;
      strokeStartLineCap: AnnStrokeLineCap;
      strokeThickness: lt.LeadLengthD;
      strokeAlignment: AnnStrokeAlignment;
      propertyChanged: AnnPropertyChangedEventType; // read-only
   }

   class AnnThickness {
      get_bottom(): number;
      set_bottom(value: number): void;
      get_left(): number;
      set_left(value: number): void;
      get_top(): number;
      set_top(value: number): void;
      get_right(): number;
      set_right(value: number): void;
      clone(): AnnThickness;
      constructor(left: number, top: number, right: number, bottom: number);
      bottom: number;
      left: number;
      top: number;
      right: number;
   }

   class AnnTransformer {
      static rotatePointAt(point: lt.LeadPointD, angle: number, centerX: number, centerY: number): lt.LeadPointD;
      static rotateRect(rect: lt.LeadRectD, angle: number): lt.LeadRectD;
      static rotateRectAt(rect: lt.LeadRectD, angle: number, centerX: number, centerY: number): lt.LeadRectD;
      static rotatePoint(point: lt.LeadPointD, angle: number): lt.LeadPointD;
      static scalePoint(point: lt.LeadPointD, scaleX: number, scaleY: number): lt.LeadPointD;
      static scalePointAt(point: lt.LeadPointD, scaleX: number, scaleY: number, centerX: number, centerY: number): lt.LeadPointD;
      static translatePoint(point: lt.LeadPointD, offsetX: number, offsetY: number): lt.LeadPointD;
      static scalePointsAt(points: lt.LeadPointD[], scaleX: number, scaleY: number, centerX: number, centerY: number): lt.LeadPointD[];
      static rotatePoints(points: lt.LeadPointD[], angle: number): lt.LeadPointD[];
      static rotateAtPoints(points: lt.LeadPointD[], angle: number, centerX: number, centerY: number): lt.LeadPointD[];
      static translatePoints(points: lt.LeadPointD[], offsetX: number, offsetY: number): lt.LeadPointD[];
   }

   interface AnnAutomationControlGetContainersCallback {
      (): AnnContainerCollection;
   }

   interface IAnnAutomationControl {
      get_automationDpiX(): number;
      get_automationDpiY(): number;
      get_automationXResolution(): number;
      get_automationYResolution(): number;
      get_automationTransform(): lt.LeadMatrix;
      get_automationUseDpi(): boolean;
      get_automationEnabled(): boolean;
      add_automationEnabledChanged(value: lt.LeadEventHandler): void;
      remove_automationEnabledChanged(value: lt.LeadEventHandler): void;
      add_automationSizeChanged(value: lt.LeadEventHandler): void;
      remove_automationSizeChanged(value: lt.LeadEventHandler): void;
      add_automationTransformChanged(value: lt.LeadEventHandler): void;
      remove_automationTransformChanged(value: lt.LeadEventHandler): void;
      add_automationUseDpiChanged(value: lt.LeadEventHandler): void;
      remove_automationUseDpiChanged(value: lt.LeadEventHandler): void;
      add_automationPointerDown(value: AnnPointerEventHandler): void;
      remove_automationPointerDown(value: AnnPointerEventHandler): void;
      add_automationPointerMove(value: AnnPointerEventHandler): void;
      remove_automationPointerMove(value: AnnPointerEventHandler): void;
      add_automationPointerUp(value: AnnPointerEventHandler): void;
      remove_automationPointerUp(value: AnnPointerEventHandler): void;
      add_automationDoubleClick(value: AnnPointerEventHandler): void;
      remove_automationDoubleClick(value: AnnPointerEventHandler): void;
      add_automationLostFocus(value: lt.LeadEventHandler): void;
      remove_automationLostFocus(value: lt.LeadEventHandler): void;
      add_automationGotFocus(value: lt.LeadEventHandler): void;
      remove_automationGotFocus(value: lt.LeadEventHandler): void;
      automationAttach(container: AnnContainer): void;
      automationDetach(): void;
      automationInvalidate(rc: lt.LeadRectD): void;
      get_renderingEngine(): AnnRenderingEngine;
      set_renderingEngine(value: AnnRenderingEngine): void;
      get_automationDataProvider(): AnnDataProvider;
      set_automationDataProvider(value: AnnDataProvider): void;
      get_automationAntiAlias(): boolean;
      set_automationAntiAlias(value: boolean): void;
      onAutomationPointerDown(e: AnnPointerEventArgs): void;
      onAutomationPointerMove(e: AnnPointerEventArgs): void;
      onAutomationPointerUp(e: AnnPointerEventArgs): void;
      onAutomationDoubleClick(e: AnnPointerEventArgs): void;
      get_automationGetContainersCallback(): AnnAutomationControlGetContainersCallback;
      set_automationGetContainersCallback(value: AnnAutomationControlGetContainersCallback): void;
      get_automationContainerIndex(): number;
      set_automationContainerIndex(value: number): void;
      get_automationObject(): any;
      set_automationObject(value: any): void;
      get_automationScrollOffset(): lt.LeadPointD;
      automationDpiX: number; // read-only
      automationDpiY: number; // read-only
      automationXResolution: number; // read-only
      automationYResolution: number; // read-only
      automationTransform: lt.LeadMatrix; // read-only
      automationUseDpi: boolean; // read-only
      automationEnabled: boolean; // read-only
      renderingEngine: AnnRenderingEngine;
      automationDataProvider: AnnDataProvider;
      automationAntiAlias: boolean;
      automationGetContainersCallback: AnnAutomationControlGetContainersCallback;
      automationContainerIndex: number;
      automationObject: any;
      automationScrollOffset: lt.LeadPointD; // read-only
      automationEnabledChanged: lt.LeadEventType; // read-only
      automationSizeChanged: lt.LeadEventType; // read-only
      automationTransformChanged: lt.LeadEventType; // read-only
      automationUseDpiChanged: lt.LeadEventType; // read-only
      automationPointerDown: AnnPointerEventType; // read-only
      automationPointerMove: AnnPointerEventType; // read-only
      automationPointerUp: AnnPointerEventType; // read-only
      automationDoubleClick: AnnPointerEventType; // read-only
      automationLostFocus: lt.LeadEventType; // read-only
      automationGotFocus: lt.LeadEventType; // read-only
   }

   interface IAnnLabelRenderer {
      get_renderingEngine(): AnnRenderingEngine;
      get_offsetHeight(): boolean;
      set_offsetHeight(value: boolean): void;
      initialize(renderingEngine: AnnRenderingEngine): void;
      renderLabel(mapper: AnnContainerMapper, label: AnnLabel, operations: AnnFixedStateOperations): void;
      getBounds(mapper: AnnContainerMapper, label: AnnLabel, operations: AnnFixedStateOperations): lt.LeadRectD;
      renderingEngine: AnnRenderingEngine; // read-only
      offsetHeight: boolean;
   }

   interface IAnnObjectRenderer {
      get_locationsThumbStyle(): IAnnThumbStyle;
      set_locationsThumbStyle(value: IAnnThumbStyle): void;
      get_rotateCenterThumbStyle(): IAnnThumbStyle;
      set_rotateCenterThumbStyle(value: IAnnThumbStyle): void;
      get_rotateGripperThumbStyle(): IAnnThumbStyle;
      set_rotateGripperThumbStyle(value: IAnnThumbStyle): void;
      get_renderingEngine(): AnnRenderingEngine;
      get_labelRenderer(): IAnnLabelRenderer;
      set_labelRenderer(value: IAnnLabelRenderer): void;
      initialize(renderingEngine: AnnRenderingEngine): void;
      getRenderPoints(mapper: AnnContainerMapper, annObject: AnnObject): lt.LeadPointD[];
      render(mapper: AnnContainerMapper, annObject: AnnObject): void;
      renderThumbs(mapper: AnnContainerMapper, thumbLocations: lt.LeadPointD[], operations: AnnFixedStateOperations): void;
      renderRotatePointThumbs(mapper: AnnContainerMapper, rotateCenterLocation: lt.LeadPointD, rotateGripperLocation: lt.LeadPointD, operations: AnnFixedStateOperations): void;
      renderLocked(mapper: AnnContainerMapper, annObject: AnnObject, operations: AnnFixedStateOperations): void;
      renderContent(mapper: AnnContainerMapper, annObject: AnnObject, operations: AnnFixedStateOperations): void;
      renderSelection(mapper: AnnContainerMapper, annObject: AnnObject): void;
      renderAlignmentTarget(mapper: AnnContainerMapper, annObject: AnnObject): void;
      addObject(annObject: AnnObject): void;
      removeObject(annObject: AnnObject): void;
      locationsThumbStyle: IAnnThumbStyle;
      rotateCenterThumbStyle: IAnnThumbStyle;
      rotateGripperThumbStyle: IAnnThumbStyle;
      renderingEngine: AnnRenderingEngine; // read-only
      labelRenderer: IAnnLabelRenderer;
   }

   interface IAnnThumbStyle {
      get_fill(): AnnBrush;
      set_fill(value: AnnBrush): void;
      get_stroke(): AnnStroke;
      set_stroke(value: AnnStroke): void;
      get_size(): lt.LeadSizeD;
      set_size(value: lt.LeadSizeD): void;
      get_isVisible(): boolean;
      set_isVisible(value: boolean): void;
      get_renderer(): IAnnObjectRenderer;
      set_renderer(value: IAnnObjectRenderer): void;
      hitTest(location: lt.LeadPointD, testPoint: lt.LeadPointD, hitTestBuffer: number): boolean;
      renderHitTest(location: lt.LeadPointD, testPoint: lt.LeadPointD, hitTestBuffer: number, mapper: AnnContainerMapper): boolean;
      render(renderer: IAnnObjectRenderer, mapper: AnnContainerMapper, location: lt.LeadPointD, operations: AnnFixedStateOperations): void;
      fill: AnnBrush;
      stroke: AnnStroke;
      size: lt.LeadSizeD;
      isVisible: boolean;
      renderer: IAnnObjectRenderer;
   }
}
