//***********************************************************************************************
//   Type definitions for Leadtools.js
//   Updated: 4/16/2016 15:47
//   Version: 19.0.0.4
//   Reference with:
//   /// <reference path="Leadtools.d.ts" />
//   Copyright (c) 1991-2015 All Rights Reserved. LEAD Technologies, Inc.
//   http://www.leadtools.com
//***********************************************************************************************

declare module lt {

   class LeadEventArgs {
      constructor();
      static empty: LeadEventArgs; // read-only
      static addToEvent(target: any, eventName: string, method: Function): void;
      static removeFromEvent(target: any, eventName: string, method: Function): void;
   }

   interface LeadEventHandler {
      (sender: any, e: LeadEventArgs): void;
   }

   class LeadEvent {
      add(value: LeadEventHandler): LeadEventHandler;
      remove(value: LeadEventHandler): void;
      static create(target: any, eventName: string): LeadEvent;
      invoke(sender: any, e: LeadEventArgs): void;
   }

   class LeadEventType extends LeadEvent {
      add(value: LeadEventHandler): LeadEventHandler;
      remove(value: LeadEventHandler): void;
   }

   enum NotifyLeadCollectionChangedAction {
      add = 0,
      remove = 1,
      replace = 2,
      move = 3,
      reset = 4
   }

   interface NotifyLeadCollectionChangedEventHandler {
      (sender: any, e: NotifyLeadCollectionChangedEventArgs): void;
   }

   class NotifyLeadCollectionChangedEventType extends LeadEvent {
      add(value: NotifyLeadCollectionChangedEventHandler): NotifyLeadCollectionChangedEventHandler;
      remove(value: NotifyLeadCollectionChangedEventHandler): void;
   }

   class NotifyLeadCollectionChangedEventArgs extends LeadEventArgs {
      static create(action: NotifyLeadCollectionChangedAction): NotifyLeadCollectionChangedEventArgs;
      static createMove(oldStartingIndex: number, newStartingIndex: number): NotifyLeadCollectionChangedEventArgs;
      static createRemove(index: number): NotifyLeadCollectionChangedEventArgs;
      static createReplace(index: number): NotifyLeadCollectionChangedEventArgs;
      get_action(): NotifyLeadCollectionChangedAction;
      get_newItems(): any[];
      get_oldItems(): any[];
      get_newStartingIndex(): number;
      get_oldStartingIndex(): number;
      constructor();
      action: NotifyLeadCollectionChangedAction; // read-only
      newItems: any[]; // read-only
      oldItems: any[]; // read-only
      newStartingIndex: number; // read-only
      oldStartingIndex: number; // read-only
   }

   class LeadCollection {
      get_count(): number;
      clear(): void;
      remove(item: any): void;
      add(item: any): void;
      contains(item: any): boolean;
      getEnumerator(): any;
      get_item(i: number): any;
      set_item(i: number, value: any): void;
      toArray(): any[];
      insertItem(index: number, item: any): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      insert(index: number, item: any): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: NotifyLeadCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(index: number): void;  // protected
      setItem(index: number, item: any): void;  // protected
      clearItems(): void;  // protected
      indexOf(item: any): number;
      add_collectionChanged(value: NotifyLeadCollectionChangedEventHandler): void;
      remove_collectionChanged(value: NotifyLeadCollectionChangedEventHandler): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: NotifyLeadCollectionChangedEventType; // read-only
   }

   interface ImageProcessingProgressEventHandler {
      (sender: any, e: ImageProcessingProgressEventArgs): void;
   }

   class ImageProcessingProgressEventType extends LeadEvent {
      add(value: ImageProcessingProgressEventHandler): ImageProcessingProgressEventHandler;
      remove(value: ImageProcessingProgressEventHandler): void;
   }

   class ImageProcessingProgressEventArgs extends LeadEventArgs {
      static create(percentage: number): ImageProcessingProgressEventArgs;
      get_percentage(): number;
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      percentage: number; // read-only
      cancel: boolean;
   }

   interface ImageProcessingCompletedEventHandler {
      (sender: any, e: ImageProcessingCompletedEventArgs): void;
   }

   class ImageProcessingCompletedEventType extends LeadEvent {
      add(value: ImageProcessingCompletedEventHandler): ImageProcessingCompletedEventHandler;
      remove(value: ImageProcessingCompletedEventHandler): void;
   }

   class ImageProcessingCompletedEventArgs extends LeadEventArgs {
      create(imageData: ImageData, results: any[][]): ImageProcessingCompletedEventArgs;
      get_imageData(): ImageData;
      get_results(): any[][];
      constructor();
      imageData: ImageData; // read-only
      results: any[][]; // read-only
   }

   interface ImageProcessingErrorEventHandler {
      (sender: any, e: ImageProcessingErrorEventArgs): void;
   }

   class ImageProcessingErrorEventType extends LeadEvent {
      add(value: ImageProcessingErrorEventHandler): ImageProcessingErrorEventHandler;
      remove(value: ImageProcessingErrorEventHandler): void;
   }

   class ImageProcessingErrorEventArgs extends LeadEventArgs {
      static create(error: Error): ImageProcessingErrorEventArgs;
      get_error(): Error;
      constructor();
      error: Error; // read-only
   }

   class ImageProcessing {
      get_jsFilePath(): string;
      set_jsFilePath(value: string): void;
      get_command(): string;
      set_command(value: string): void;
      get_imageData(): ImageData;
      set_imageData(value: ImageData): void;
      get_arguments(): any[][];
      add_progress(value: ImageProcessingProgressEventHandler): void;
      remove_progress(value: ImageProcessingProgressEventHandler): void;
      add_completed(value: ImageProcessingCompletedEventHandler): void;
      remove_completed(value: ImageProcessingCompletedEventHandler): void;
      add_error(value: ImageProcessingErrorEventHandler): void;
      remove_error(value: ImageProcessingErrorEventHandler): void;
      get_isRunning(): boolean;
      abort(): void;
      get_runInMainThread(): boolean;
      set_runInMainThread(value: boolean): void;
      run(): void;
      constructor();
      jsFilePath: string;
      command: string;
      imageData: ImageData;
      arguments: any[][]; // read-only
      isRunning: boolean; // read-only
      runInMainThread: boolean;
      progress: ImageProcessingProgressEventType; // read-only
      completed: ImageProcessingCompletedEventType; // read-only
      error: ImageProcessingErrorEventType; // read-only
   }

   class LeadLengthD {
      static create(value: number): LeadLengthD;
      clone(): LeadLengthD;
      get_value(): number;
      set_value(value: number): void;
      static equals(length1: LeadLengthD, length2: LeadLengthD): boolean;
      toString(): string;
      toJSON(): any;
      static fromJSON(jsonObject: any): LeadLengthD;
      constructor();
      value: number;
   }

   class LeadDoubleTools {
      static areClose(value1: number, value2: number): boolean;
      static lessThan(value1: number, value2: number): boolean;
      static greaterThan(value1: number, value2: number): boolean;
      static lessThanOrClose(value1: number, value2: number): boolean;
      static greaterThanOrClose(value1: number, value2: number): boolean;
      static isOne(value: number): boolean;
      static isZero(value: number): boolean;
      static areClosePoints(point1: LeadPointD, point2: LeadPointD): boolean;
      static areCloseSizes(size1: LeadSizeD, size2: LeadSizeD): boolean;
      static areCloseRects(rect1: LeadRectD, rect2: LeadRectD): boolean;
      static isBetweenZeroAndOne(value: number): boolean;
      static doubleToInt(value: number): number;
      static rectHasNaN(rect: LeadRectD): boolean;
      static isNaN(value: number): boolean;
      static isInfinity(value: number): boolean;
      static normalizeAngle(angle: number): number;
      static naN: number;
      static positiveInfinity: number;
      static negativeInfinity: number;
   }

   class LeadMatrix {
      static create(m11: number, m12: number, m21: number, m22: number, offsetX: number, offsetY: number): LeadMatrix;
      clone(): LeadMatrix;
      static get_identity(): LeadMatrix;
      get_isIdentity(): boolean;
      get_determinant(): number;
      get_hasInverse(): boolean;
      get_m11(): number;
      set_m11(value: number): void;
      get_m12(): number;
      set_m12(value: number): void;
      get_m21(): number;
      set_m21(value: number): void;
      get_m22(): number;
      set_m22(value: number): void;
      get_offsetX(): number;
      set_offsetX(value: number): void;
      get_offsetY(): number;
      set_offsetY(value: number): void;
      setIdentity(): void;
      static multiply(trans1: LeadMatrix, trans2: LeadMatrix): LeadMatrix;
      append(matrix: LeadMatrix): void;
      prepend(matrix: LeadMatrix): void;
      rotate(angle: number): void;
      rotatePrepend(angle: number): void;
      rotateAt(angle: number, centerX: number, centerY: number): void;
      rotateAtPrepend(angle: number, centerX: number, centerY: number): void;
      scale(scaleX: number, scaleY: number): void;
      scalePrepend(scaleX: number, scaleY: number): void;
      scaleAt(scaleX: number, scaleY: number, centerX: number, centerY: number): void;
      scaleAtPrepend(scaleX: number, scaleY: number, centerX: number, centerY: number): void;
      skew(skewX: number, skewY: number): void;
      skewPrepend(skewX: number, skewY: number): void;
      translate(offsetX: number, offsetY: number): void;
      translatePrepend(offsetX: number, offsetY: number): void;
      transformPoint(point: LeadPointD): LeadPointD;
      transformVector(point: LeadPointD): LeadPointD;
      transformRect(rect: LeadRectD): LeadRectD;
      transformPoints(points: LeadPointD[]): void;
      invert(): void;
      static equals(matrix1: LeadMatrix, matrix2: LeadMatrix): boolean;
      toString(): string;
      toJSON(): any;
      static fromJSON(jsonObject: any): LeadMatrix;
      constructor();
      static identity: LeadMatrix; // read-only
      isIdentity: boolean; // read-only
      determinant: number; // read-only
      hasInverse: boolean; // read-only
      m11: number;
      m12: number;
      m21: number;
      m22: number;
      offsetX: number;
      offsetY: number;
   }

   class LeadPointD {
      static get_empty(): LeadPointD;
      get_isEmpty(): boolean;
      static create(x: number, y: number): LeadPointD;
      clone(): LeadPointD;
      get_x(): number;
      set_x(value: number): void;
      get_y(): number;
      set_y(value: number): void;
      static equals(point1: LeadPointD, point2: LeadPointD): boolean;
      toString(): string;
      toJSON(): any;
      static fromJSON(jsonObject: any): LeadPointD;
      constructor();
      static empty: LeadPointD; // read-only
      isEmpty: boolean; // read-only
      x: number;
      y: number;
   }

   class LeadRectD {
      static create(x: number, y: number, width: number, height: number): LeadRectD;
      static fromLTRB(left: number, top: number, right: number, bottom: number): LeadRectD;
      clone(): LeadRectD;
      static get_empty(): LeadRectD;
      get_isEmpty(): boolean;
      get_location(): LeadPointD;
      set_location(value: LeadPointD): void;
      get_size(): LeadSizeD;
      set_size(value: LeadSizeD): void;
      get_x(): number;
      set_x(value: number): void;
      get_y(): number;
      set_y(value: number): void;
      get_width(): number;
      set_width(value: number): void;
      get_height(): number;
      set_height(value: number): void;
      get_left(): number;
      get_top(): number;
      get_right(): number;
      get_bottom(): number;
      get_topLeft(): LeadPointD;
      get_topRight(): LeadPointD;
      get_bottomLeft(): LeadPointD;
      get_bottomRight(): LeadPointD;
      static equals(rect1: LeadRectD, rect2: LeadRectD): boolean;
      toString(): string;
      toJSON(): any;
      static fromJSON(jsonObject: any): LeadRectD;
      containsPoint(point: LeadPointD): boolean;
      contains(x: number, y: number): boolean;
      containsRect(rect: LeadRectD): boolean;
      intersectsWith(rect: LeadRectD): boolean;
      intersect(rect: LeadRectD): void;
      static intersectRects(rect1: LeadRectD, rect2: LeadRectD): LeadRectD;
      union(rect: LeadRectD): void;
      static unionRects(rect1: LeadRectD, rect2: LeadRectD): LeadRectD;
      offset(offsetX: number, offsetY: number): void;
      inflate(width: number, height: number): void;
      static inflateRect(rect: LeadRectD, width: number, height: number): LeadRectD;
      scale(scaleX: number, scaleY: number): void;
      constructor();
      static empty: LeadRectD; // read-only
      isEmpty: boolean; // read-only
      location: LeadPointD;
      size: LeadSizeD;
      x: number;
      y: number;
      width: number;
      height: number;
      left: number; // read-only
      top: number; // read-only
      right: number; // read-only
      bottom: number; // read-only
      topLeft: LeadPointD; // read-only
      topRight: LeadPointD; // read-only
      bottomLeft: LeadPointD; // read-only
      bottomRight: LeadPointD; // read-only
   }

   class LeadSizeD {
      static create(width: number, height: number): LeadSizeD;
      clone(): LeadSizeD;
      static get_empty(): LeadSizeD;
      get_isEmpty(): boolean;
      get_width(): number;
      set_width(value: number): void;
      get_height(): number;
      set_height(value: number): void;
      static equals(size1: LeadSizeD, size2: LeadSizeD): boolean;
      toString(): string;
      toJSON(): any;
      static fromJSON(jsonObject: any): LeadSizeD;
      constructor();
      static empty: LeadSizeD; // read-only
      isEmpty: boolean; // read-only
      width: number;
      height: number;
   }

   enum ImageLoaderUrlMode {
      imageUrl = 0,
      ajaxDataUrl = 1,
      ajaxXml = 2
   }

   interface ImageLoaderPreRunEventHandler {
      (sender: any, e: ImageLoaderPreRunEventArgs): void;
   }

   class ImageLoaderPreRunEventType extends LeadEvent {
      add(value: ImageLoaderPreRunEventHandler): ImageLoaderPreRunEventHandler;
      remove(value: ImageLoaderPreRunEventHandler): void;
   }

   class ImageLoaderPreRunEventArgs extends LeadEventArgs {
      get_cancel(): boolean;
      set_cancel(value: boolean): void;
      constructor();
      cancel: boolean;
   }

   class ImageLoaderAjaxOptions {
      set_headers(value: { [key: string]: string }): void;
      get_headers(): { [key: string]: string };
      get_method(): string;
      set_method(value: string): void;
      get_postData(): string;
      set_postData(value: string): void;
      constructor();
      headers: { [key: string]: string };
      method: string;
      postData: string;
   }

   class ImageLoader {
      add_preRun(value: ImageLoaderPreRunEventHandler): void;
      remove_preRun(value: ImageLoaderPreRunEventHandler): void;
      add_done(value: LeadEventHandler): void;
      remove_done(value: LeadEventHandler): void;
      add_fail(value: LeadEventHandler): void;
      remove_fail(value: LeadEventHandler): void;
      add_always(value: LeadEventHandler): void;
      remove_always(value: LeadEventHandler): void;
      get_urlMode(): ImageLoaderUrlMode;
      set_urlMode(value: ImageLoaderUrlMode): void;
      get_tag(): any;
      set_tag(value: any): void;
      get_element(): HTMLElement;
      get_isHTMLImageElement(): boolean;
      get_width(): number;
      get_height(): number;
      get_error(): Error;
      get_isWorking(): boolean;
      get_ajaxOptions(): ImageLoaderAjaxOptions;
      set_ajaxOptions(value: ImageLoaderAjaxOptions): void;
      get_url(): string;
      set_url(value: string): void;
      get_imagesHolder(): HTMLElement;
      set_imagesHolder(value: HTMLElement): void;
      abort(): void;
      dispose(): void;
      get_canRun(): boolean;
      run(): void;
      constructor();
      urlMode: ImageLoaderUrlMode;
      tag: any;
      element: HTMLElement; // read-only
      isHTMLImageElement: boolean; // read-only
      width: number; // read-only
      height: number; // read-only
      error: Error; // read-only
      isWorking: boolean; // read-only
      ajaxOptions: ImageLoaderAjaxOptions;
      url: string;
      imagesHolder: HTMLElement;
      canRun: boolean; // read-only
      preRun: ImageLoaderPreRunEventType; // read-only
      done: LeadEventType; // read-only
      fail: LeadEventType; // read-only
      always: LeadEventType; // read-only
   }

   enum LTDevice {
      unknown = 0,
      desktop = 1,
      mobile = 2,
      tablet = 3
   }

   enum LTOS {
      unknown = 0,
      windows = 1,
      mac = 2,
      iOS = 3,
      android = 4,
      linux = 5,
      blackberry = 6,
      webOS = 7,
      windows7 = 8,
      windows8 = 9,
      windows10 = 10
   }

   enum LTBrowser {
      unknown = 0,
      internetExplorer = 1,
      firefox = 2,
      chrome = 3,
      safari = 4,
      opera = 5,
      android = 6,
      edge = 7
   }

   interface LTRender {
      (): void;
   }

   interface LTRequestAnimationFrame {
      (callback: LTRender): number;
   }

   interface LTCancelAnimationFrame {
      (id: number): void;
   }

   class LTHelper {
      static requestAnimationFrame(callback: LTRender): number;
      static cancelAnimationFrame(id: number): void;
      static getElementStyle(element: HTMLElement, styleProp: string): string;
      static getPosition(element: HTMLElement, parent: HTMLElement): LeadPointD;
      static hasClass(element: HTMLElement, className: string): boolean;
      static removeClass(element: HTMLElement, className: string): void;
      static addClass(element: HTMLElement, className: string): void;
      static base64Encode(input: number[], removeLinefeed: boolean): string;
      static base64Decode(input: string): number[];
      static base64DecodeToArrayBuffer(input: string): any;
      static base64DecodeToByteArray(input: string): number[];
      static base64EncodeFromArrayBuffer(buffer: any): string;
      static loadJS(fileName: string, callback: Function): void;
      static removeJS(fileName: string): void;
      static ensureUsableCanvas(canvas: HTMLCanvasElement): number;
      static requestCustomHeaders: any[][];
      static device: LTDevice;
      static OS: LTOS;
      static browser: LTBrowser;
      static version: number;
      static vendor: string;
      static supportsHTMLPointerEvents: boolean;
      static supportsCSSTransitions: boolean;
      static supportsFileReader: boolean;
      static supportsCanvas: boolean;
      static supportsTypedArray: boolean;
      static supportsTouch: boolean;
      static supportsMultiTouch: boolean;
      static supportsMouse: boolean;
      static supportsScroll: boolean;
      static supportsWebGL: boolean;
      static supportsTransform: boolean;
      static supportsTransitionEnd: boolean;
      static supportsAnimationFrame: boolean;
      static supportsWebWorker: boolean;
      static msPointerEnabled: boolean;
      static resizeEvent: string;
      static dragStartEvent: string;
      static dragDeltaEvent: string;
      static dragCompletedEvent: string;
      static dragCancelEvent: string;
      static mouseWheelEvent: string;
      static licenseDirectory: string;
   }
}
