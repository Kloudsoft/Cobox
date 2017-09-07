//***********************************************************************************************
//   Type definitions for Leadtools.Documents.UI.js
//   Updated: 4/10/2016 18:47
//   Version: 19.0.0.8
//   Reference with:
//   /// <reference path="Leadtools.Documents.UI.d.ts" />
//   Copyright (c) 1991-2015 All Rights Reserved. LEAD Technologies, Inc.
//   http://www.leadtools.com
//***********************************************************************************************

// Required references
/// <reference path="Leadtools.d.ts"/>
/// <reference path="Leadtools.Annotations.Automation.d.ts"/>
/// <reference path="Leadtools.Annotations.Core.d.ts"/>
/// <reference path="Leadtools.Annotations.Designers.d.ts"/>
/// <reference path="Leadtools.Annotations.Rendering.JavaScript.d.ts"/>
/// <reference path="Leadtools.Controls.d.ts"/>
/// <reference path="Leadtools.Documents.d.ts"/>

declare module lt.Documents.UI {

   interface DocumentViewerAsyncDone {
      (operation: DocumentViewerAsyncOperation): void;
   }

   interface DocumentViewerAsyncError {
      (operation: DocumentViewerAsyncOperation, error: Error): void;
   }

   interface DocumentViewerAsyncAlways {
      (operation: DocumentViewerAsyncOperation): void;
   }

   class DocumentViewerAsyncOperation {
      get_userState(): any;
      set_userState(value: any): void;
      get_done(): DocumentViewerAsyncDone;
      set_done(value: DocumentViewerAsyncDone): void;
      get_error(): DocumentViewerAsyncError;
      set_error(value: DocumentViewerAsyncError): void;
      get_always(): DocumentViewerAsyncAlways;
      set_always(value: DocumentViewerAsyncAlways): void;
      constructor();
      userState: any;
      done: DocumentViewerAsyncDone;
      error: DocumentViewerAsyncError;
      always: DocumentViewerAsyncAlways;
   }

   class DocumentViewerAnnotations {
      get_documentViewer(): DocumentViewer;
      get_automationManager(): lt.Annotations.Automation.AnnAutomationManager;
      get_automation(): lt.Annotations.Automation.AnnAutomation;
      get_automationControl(): lt.Annotations.Core.IAnnAutomationControl;
      get_interactiveMode(): lt.Controls.ImageViewerInteractiveMode;
      isContainerModified(pageNumber: number): boolean;
      setContainerModified(pageNumber: number, isModified: boolean): void;
      get_isLoading(): boolean;
      initialize(): void;
      groupSelectedObjects(groupName: string): void;
      ungroupSelectedObjects(): void;
      get_useRotateThumbs(): boolean;
      set_useRotateThumbs(value: boolean): void;
      get_renderOnThumbnails(): boolean;
      set_renderOnThumbnails(value: boolean): void;
      selectedTextToReviewObject(pageNumber: number, textReviewObject: lt.Annotations.Core.AnnTextReviewObject): boolean;
      dispose(): void;
      documentViewer: DocumentViewer; // read-only
      automationManager: lt.Annotations.Automation.AnnAutomationManager; // read-only
      automation: lt.Annotations.Automation.AnnAutomation; // read-only
      automationControl: lt.Annotations.Core.IAnnAutomationControl; // read-only
      interactiveMode: lt.Controls.ImageViewerInteractiveMode; // read-only
      isLoading: boolean; // read-only
      useRotateThumbs: boolean;
      renderOnThumbnails: boolean;
   }

   class ImageViewerAutomationControl {
      dispose(): void;
      get_imageViewer(): lt.Controls.ImageViewer;
      set_imageViewer(value: lt.Controls.ImageViewer): void;
      get_automationScrollOffset(): lt.LeadPointD;
      get_automationObject(): any;
      set_automationObject(value: any): void;
      add_automationPointerDown(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationPointerDown(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      add_automationPointerMove(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationPointerMove(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      add_automationPointerUp(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationPointerUp(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      add_automationDoubleClick(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationDoubleClick(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      onAutomationPointerDown(e: lt.Annotations.Core.AnnPointerEventArgs): void;
      onAutomationPointerMove(e: lt.Annotations.Core.AnnPointerEventArgs): void;
      onAutomationPointerUp(e: lt.Annotations.Core.AnnPointerEventArgs): void;
      onAutomationDoubleClick(e: lt.Annotations.Core.AnnPointerEventArgs): void;
      get_automationDpiX(): number;
      get_automationDpiY(): number;
      get_automationEnabled(): boolean;
      add_automationEnabledChanged(value: lt.LeadEventHandler): void;
      remove_automationEnabledChanged(value: lt.LeadEventHandler): void;
      add_automationGotFocus(value: lt.LeadEventHandler): void;
      remove_automationGotFocus(value: lt.LeadEventHandler): void;
      add_automationLostFocus(value: lt.LeadEventHandler): void;
      remove_automationLostFocus(value: lt.LeadEventHandler): void;
      add_automationSizeChanged(value: lt.LeadEventHandler): void;
      remove_automationSizeChanged(value: lt.LeadEventHandler): void;
      get_automationTransform(): lt.LeadMatrix;
      add_automationTransformChanged(value: lt.LeadEventHandler): void;
      remove_automationTransformChanged(value: lt.LeadEventHandler): void;
      get_automationUseDpi(): boolean;
      add_automationUseDpiChanged(value: lt.LeadEventHandler): void;
      remove_automationUseDpiChanged(value: lt.LeadEventHandler): void;
      get_automationXResolution(): number;
      get_automationYResolution(): number;
      automationInvalidate(rc: lt.LeadRectD): void;
      get_automationAntiAlias(): boolean;
      set_automationAntiAlias(value: boolean): void;
      get_renderingEngine(): lt.Annotations.Core.AnnRenderingEngine;
      set_renderingEngine(value: lt.Annotations.Core.AnnRenderingEngine): void;
      get_automationGetContainersCallback(): lt.Annotations.Core.AnnAutomationControlGetContainersCallback;
      set_automationGetContainersCallback(value: lt.Annotations.Core.AnnAutomationControlGetContainersCallback): void;
      get_automationContainerIndex(): number;
      set_automationContainerIndex(value: number): void;
      automationAttach(container: lt.Annotations.Core.AnnContainer): void;
      automationDetach(): void;
      get_automationDataProvider(): lt.Annotations.Core.AnnDataProvider;
      set_automationDataProvider(value: lt.Annotations.Core.AnnDataProvider): void;
      constructor();
      imageViewer: lt.Controls.ImageViewer;
      automationScrollOffset: lt.LeadPointD; // read-only
      automationObject: any;
      automationDpiX: number; // read-only
      automationDpiY: number; // read-only
      automationEnabled: boolean; // read-only
      automationTransform: lt.LeadMatrix; // read-only
      automationUseDpi: boolean; // read-only
      automationXResolution: number; // read-only
      automationYResolution: number; // read-only
      automationAntiAlias: boolean;
      renderingEngine: lt.Annotations.Core.AnnRenderingEngine;
      automationGetContainersCallback: lt.Annotations.Core.AnnAutomationControlGetContainersCallback;
      automationContainerIndex: number;
      automationDataProvider: lt.Annotations.Core.AnnDataProvider;
      automationPointerDown: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationPointerMove: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationPointerUp: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationDoubleClick: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationEnabledChanged: lt.LeadEventType; // read-only
      automationGotFocus: lt.LeadEventType; // read-only
      automationLostFocus: lt.LeadEventType; // read-only
      automationSizeChanged: lt.LeadEventType; // read-only
      automationTransformChanged: lt.LeadEventType; // read-only
      automationUseDpiChanged: lt.LeadEventType; // read-only
   }

   class DocumentViewerBookmarks {
      get_documentViewer(): DocumentViewer;
      get_isLoading(): boolean;
      dispose(): void;
      get_treeView(): HTMLElement;
      documentViewer: DocumentViewer; // read-only
      isLoading: boolean; // read-only
      treeView: HTMLElement; // read-only
   }

   interface DocumentViewerCanRunCommand {
      (documentViewer: DocumentViewer, command: DocumentViewerCommand, value: any): boolean;
   }

   interface DocumentViewerRunCommand {
      (documentViewer: DocumentViewer, command: DocumentViewerCommand, value: any): any;
   }

   interface DocumentViewerIsSlowCommand {
      (documentViewer: DocumentViewer, command: DocumentViewerCommand, value: any): boolean;
   }

   class DocumentViewerCommand {
      get_name(): string;
      set_name(value: string): void;
      get_canRunHandler(): DocumentViewerCanRunCommand;
      set_canRunHandler(value: DocumentViewerCanRunCommand): void;
      get_runHandler(): DocumentViewerRunCommand;
      set_runHandler(value: DocumentViewerRunCommand): void;
      get_isSlowHandler(): DocumentViewerIsSlowCommand;
      set_isSlowHandler(value: DocumentViewerIsSlowCommand): void;
      get_tag(): any;
      set_tag(value: any): void;
      get_value(): any;
      set_value(value: any): void;
      get_hasState(): boolean;
      set_hasState(value: boolean): void;
      get_state(): boolean;
      set_state(value: boolean): void;
      preRun(documentViewer: DocumentViewer, value: any): boolean;
      postRun(documentViewer: DocumentViewer, value: any): void;
      constructor();
      name: string;
      canRunHandler: DocumentViewerCanRunCommand;
      runHandler: DocumentViewerRunCommand;
      isSlowHandler: DocumentViewerIsSlowCommand;
      tag: any;
      value: any;
      hasState: boolean;
      state: boolean;
   }

   class DocumentViewerCommands {
      getCommand(commandName: string): DocumentViewerCommand;
      get_documentViewer(): DocumentViewer;
      canRun(commandName: string, value: any): boolean;
      run(commandName: string, value: any): any;
      isSlow(commandName: string, value: any): boolean;
      documentViewer: DocumentViewer; // read-only
      static pageFirst: string;
      static pageNext: string;
      static pagePrevious: string;
      static pageLast: string;
      static pageGoto: string;
      static viewZoomIn: string;
      static viewZoomOut: string;
      static viewZoomPercentage: string;
      static viewFitWidth: string;
      static viewFitPage: string;
      static viewActualSize: string;
      static viewRotateClockwise: string;
      static viewRotateCounterClockwise: string;
      static viewItemType: string;
      static layoutSingle: string;
      static layoutVertical: string;
      static layoutDouble: string;
      static layoutHorizontal: string;
      static interactivePanZoom: string;
      static interactivePan: string;
      static interactiveZoom: string;
      static interactiveZoomTo: string;
      static interactiveMagnifyGlass: string;
      static interactiveSelectText: string;
      static interactiveAutoPan: string;
      static interactiveRubberBand: string;
      static textCopy: string;
      static textSelectAll: string;
      static textClearSelection: string;
      static textExport: string;
      static textFindNext: string;
      static textFindPrevious: string;
      static textGet: string;
      static annotationsUndo: string;
      static annotationsRedo: string;
      static annotationsCut: string;
      static annotationsCopy: string;
      static annotationsPaste: string;
      static annotationsDelete: string;
      static annotationsSelectAll: string;
      static annotationsClearSelection: string;
      static annotationsUserModeDesign: string;
      static annotationsUserModeRun: string;
      static annotationsUserModeRender: string;
      static annotationsBringToFront: string;
      static annotationsSendToBack: string;
      static annotationsBringToFirst: string;
      static annotationsSendToLast: string;
      static annotationsFlip: string;
      static annotationsReverse: string;
      static annotationsGroup: string;
      static annotationsUngroup: string;
      static annotationsLock: string;
      static annotationsUnlock: string;
      static annotationsResetRotatePoints: string;
      static annotationsAntiAlias: string;
      static annotationsProperties: string;
      static annotationsUseRotateThumbs: string;
      static annotationsEnableToolTips: string;
      static annotationsRenderOnThumbnails: string;
   }

   class DocumentViewerCreateOptions {
      get_viewContainer(): HTMLElement;
      set_viewContainer(value: HTMLElement): void;
      get_thumbnailsContainer(): HTMLElement;
      set_thumbnailsContainer(value: HTMLElement): void;
      get_bookmarksContainer(): HTMLElement;
      set_bookmarksContainer(value: HTMLElement): void;
      get_useAnnotations(): boolean;
      set_useAnnotations(value: boolean): void;
      get_viewCreateOptions(): lt.Controls.ImageViewerCreateOptions;
      get_thumbnailsCreateOptions(): lt.Controls.ImageViewerCreateOptions;
      constructor();
      viewContainer: HTMLElement;
      thumbnailsContainer: HTMLElement;
      bookmarksContainer: HTMLElement;
      useAnnotations: boolean;
      viewCreateOptions: lt.Controls.ImageViewerCreateOptions; // read-only
      thumbnailsCreateOptions: lt.Controls.ImageViewerCreateOptions; // read-only
   }

   class DocumentViewerDiagnostics {
      get_documentViewer(): DocumentViewer;
      get_showLinks(): boolean;
      set_showLinks(value: boolean): void;
      get_showTextCharacters(): boolean;
      set_showTextCharacters(value: boolean): void;
      documentViewer: DocumentViewer; // read-only
      showLinks: boolean;
      showTextCharacters: boolean;
   }

   class DocumentViewer {
      dispose(): void;
      get_useAjaxImageLoading(): boolean;
      set_useAjaxImageLoading(value: boolean): void;
      prepareToSave(): JQueryPromise<void>;
      get_document(): lt.Documents.Document;
      get_autoDisposeDocument(): boolean;
      set_autoDisposeDocument(value: boolean): void;
      get_hasDocument(): boolean;
      get_pageCount(): number;
      get_view(): DocumentViewerView;
      get_thumbnails(): DocumentViewerThumbnails;
      get_bookmarks(): DocumentViewerBookmarks;
      get_annotations(): DocumentViewerAnnotations;
      get_commands(): DocumentViewerCommands;
      get_text(): DocumentViewerText;
      get_userName(): string;
      set_userName(value: string): void;
      get_diagnostics(): DocumentViewerDiagnostics;
      setDocument(document: lt.Documents.Document): void;
      add_operation(value: DocumentViewerOperationEventHandler): void;
      remove_operation(value: DocumentViewerOperationEventHandler): void;
      onOperation(e: DocumentViewerOperationEventArgs): void;  // protected
      get_currentPageNumber(): number;
      gotoPage(pageNumber: number): void;
      gotoBookmark(bookmark: lt.Documents.DocumentBookmark): void;
      runLinkTarget(linkTarget: lt.Documents.DocumentLinkTarget): void;
      useAjaxImageLoading: boolean;
      document: lt.Documents.Document; // read-only
      autoDisposeDocument: boolean;
      hasDocument: boolean; // read-only
      pageCount: number; // read-only
      view: DocumentViewerView; // read-only
      thumbnails: DocumentViewerThumbnails; // read-only
      bookmarks: DocumentViewerBookmarks; // read-only
      annotations: DocumentViewerAnnotations; // read-only
      commands: DocumentViewerCommands; // read-only
      text: DocumentViewerText; // read-only
      userName: string;
      diagnostics: DocumentViewerDiagnostics; // read-only
      currentPageNumber: number; // read-only
      operation: DocumentViewerOperationEventType; // read-only
      static annotationsInteractiveModeId: number;
      static pageLinksInteractiveModeId: number;
      static selectTextInteractiveModeId: number;
   }

   class AnnotationsInteractiveMode extends lt.Controls.ImageViewerInteractiveMode {
      get_id(): number;
      get_automationControl(): lt.Annotations.Core.IAnnAutomationControl;
      set_automationControl(value: lt.Annotations.Core.IAnnAutomationControl): void;
      get_name(): string;
      canStartWork(e: lt.Controls.InteractiveEventArgs): boolean;  // protected
      start(imageViewer: lt.Controls.ImageViewer): void;
      stop(imageViewer: lt.Controls.ImageViewer): void;
      constructor();
      id: number; // read-only
      automationControl: lt.Annotations.Core.IAnnAutomationControl;
      name: string; // read-only
   }

   class DocumentViewerPageLinksInteractiveMode extends lt.Controls.ImageViewerInteractiveMode {
      get_documentViewer(): DocumentViewer;
      get_name(): string;
      get_id(): number;
      get_runLinkKeyModifier(): lt.Controls.Keys;
      set_runLinkKeyModifier(value: lt.Controls.Keys): void;
      start(imageViewer: lt.Controls.ImageViewer): void;
      stop(imageViewer: lt.Controls.ImageViewer): void;
      canStartWork(e: lt.Controls.InteractiveEventArgs): boolean;  // protected
      constructor(documentViewer: DocumentViewer);
      documentViewer: DocumentViewer; // read-only
      name: string; // read-only
      id: number; // read-only
      runLinkKeyModifier: lt.Controls.Keys;
   }

   class DocumentViewerSelectTextInteractiveMode extends lt.Controls.ImageViewerRubberBandInteractiveMode {
      get_documentViewer(): DocumentViewer;
      set_documentViewer(value: DocumentViewer): void;
      get_shape(): lt.Controls.ImageViewerRubberBandShape;
      set_shape(value: lt.Controls.ImageViewerRubberBandShape): void;
      get_name(): string;
      get_id(): number;
      start(imageViewer: lt.Controls.ImageViewer): void;
      stop(imageViewer: lt.Controls.ImageViewer): void;
      onRubberBandStarted(e: lt.Controls.ImageViewerRubberBandEventArgs): void;  // protected
      onRubberBandDelta(e: lt.Controls.ImageViewerRubberBandEventArgs): void;  // protected
      onRubberBandCompleted(e: lt.Controls.ImageViewerRubberBandEventArgs): void;  // protected
      constructor();
      documentViewer: DocumentViewer;
      shape: lt.Controls.ImageViewerRubberBandShape;
      name: string; // read-only
      id: number; // read-only
   }

   enum DocumentViewerOperation {
      setDocument = 0,
      loadingThumbnails = 1,
      getThumbnail = 2,
      loadingPages = 3,
      getPage = 4,
      runCommand = 5,
      gotoPage = 6,
      itemTypeChanged = 7,
      getText = 8,
      pageTextSelectionChanged = 9,
      textSelectionChanged = 10,
      renderItemPlaceholder = 11,
      renderSelectedText = 12,
      gotoBookmark = 13,
      runLink = 14,
      loadingAnnotations = 15,
      getAnnotations = 16,
      createAutomation = 17,
      destroyAutomation = 18,
      automationStateChanged = 19,
      selectedTextToReviewObject = 20,
      loadingBookmarks = 21,
      hoverLink = 22
   }

   interface DocumentViewerOperationEventHandler {
      (sender: any, e: DocumentViewerOperationEventArgs): void;
   }

   class DocumentViewerOperationEventType extends lt.LeadEvent {
      add(value: DocumentViewerOperationEventHandler): DocumentViewerOperationEventHandler;
      remove(value: DocumentViewerOperationEventHandler): void;
   }

   class DocumentViewerOperationEventArgs extends lt.LeadEventArgs {
      get_operation(): DocumentViewerOperation;
      get_error(): Error;
      get_data1(): any;
      get_data2(): any;
      get_pageNumber(): number;
      get_isPostOperation(): boolean;
      get_abort(): boolean;
      set_abort(value: boolean): void;
      constructor(operation: DocumentViewerOperation, error: Error, data1: any, data2: any, pageNumber: number, isPostOperation: boolean);
      operation: DocumentViewerOperation; // read-only
      error: Error; // read-only
      data1: any; // read-only
      data2: any; // read-only
      pageNumber: number; // read-only
      isPostOperation: boolean; // read-only
      abort: boolean;
   }

   class DocumentViewerFindText {
      clone(): DocumentViewerFindText;
      get_pageNumber(): number;
      set_pageNumber(value: number): void;
      get_text(): string;
      set_text(value: string): void;
      get_matchCase(): boolean;
      set_matchCase(value: boolean): void;
      get_wholeWordsOnly(): boolean;
      set_wholeWordsOnly(value: boolean): void;
      get_gotoNextPage(): boolean;
      set_gotoNextPage(value: boolean): void;
      get_recursive(): boolean;
      set_recursive(value: boolean): void;
      get_autoSelect(): boolean;
      set_autoSelect(value: boolean): void;
      get_autoEnsureVisible(): boolean;
      set_autoEnsureVisible(value: boolean): void;
      constructor();
      pageNumber: number;
      text: string;
      matchCase: boolean;
      wholeWordsOnly: boolean;
      gotoNextPage: boolean;
      recursive: boolean;
      autoSelect: boolean;
      autoEnsureVisible: boolean;
   }

   class DocumentViewerText {
      get_documentViewer(): DocumentViewer;
      get_renderSelection(): boolean;
      set_renderSelection(value: boolean): void;
      get_autoGetText(): boolean;
      set_autoGetText(value: boolean): void;
      getDocumentPageText(pageNumber: number): lt.Documents.DocumentPageText;
      getAllDocumentPageText(): void;
      hasAnyDocumentPageText(): boolean;
      hasDocumentPageText(pageNumber: number): boolean;
      setDocumentPageText(pageText: lt.Documents.DocumentPageText, pageNumber: number): void;
      getSelectedTextItems(pageNumber: number): DocumentViewerTextItem[];
      setSelectedTextItems(pageNumber: number, items: DocumentViewerTextItem[]): void;
      clearSelection(pageNumber: number): boolean;
      selectAll(pageNumber: number): void;
      selectText(area: lt.LeadRectD, mode: DocumentViewerSelectTextMode): void;
      hasSelectedText(pageNumber: number): boolean;
      getSelectedText(pageNumber: number): string;
      exportText(pageNumber: number): string;
      copy(pageNumber: number): void;
      clearLastFindText(): void;
      get_lastFindText(): DocumentViewerFindText;
      find(findText: DocumentViewerFindText, isFirst: boolean, findNext: boolean): DocumentViewerTextItem[];
      get_lineKeyModifier(): lt.Controls.Keys;
      set_lineKeyModifier(value: lt.Controls.Keys): void;
      get_wordKeyModifier(): lt.Controls.Keys;
      set_wordKeyModifier(value: lt.Controls.Keys): void;
      get_characterKeyModifier(): lt.Controls.Keys;
      set_characterKeyModifier(value: lt.Controls.Keys): void;
      dispose(): void;
      static get_selectedFill(): string;
      static set_selectedFill(value: string): void;
      documentViewer: DocumentViewer; // read-only
      renderSelection: boolean;
      autoGetText: boolean;
      lastFindText: DocumentViewerFindText; // read-only
      lineKeyModifier: lt.Controls.Keys;
      wordKeyModifier: lt.Controls.Keys;
      characterKeyModifier: lt.Controls.Keys;
      static selectedFill: string;
   }

   enum DocumentViewerSelectTextMode {
      line = 0,
      word = 1,
      character = 2
   }

   class DocumentViewerTextItem {
      clone(): DocumentViewerTextItem;
      get_pageNumber(): number;
      set_pageNumber(value: number): void;
      get_firstCharacterIndex(): number;
      set_firstCharacterIndex(value: number): void;
      get_lastCharacterIndex(): number;
      set_lastCharacterIndex(value: number): void;
      get_bounds(): lt.LeadRectD;
      set_bounds(value: lt.LeadRectD): void;
      constructor();
      pageNumber: number;
      firstCharacterIndex: number;
      lastCharacterIndex: number;
      bounds: lt.LeadRectD;
   }

   class DocumentViewerThumbnails {
      get_documentViewer(): DocumentViewer;
      get_imageViewer(): lt.Controls.ImageViewer;
      get_useGrids(): boolean;
      set_useGrids(value: boolean): void;
      get_gridPixelSize(): number;
      set_gridPixelSize(value: number): void;
      get_isLoading(): boolean;
      invalidate(bounds: lt.LeadRectD): void;
      dispose(): void;
      get_workerCount(): number;
      set_workerCount(value: number): void;
      get_lazyLoad(): boolean;
      set_lazyLoad(value: boolean): void;
      documentViewer: DocumentViewer; // read-only
      imageViewer: lt.Controls.ImageViewer; // read-only
      useGrids: boolean;
      gridPixelSize: number;
      isLoading: boolean; // read-only
      workerCount: number;
      lazyLoad: boolean;
   }

   class DocumentViewerView {
      get_documentViewer(): DocumentViewer;
      get_imageViewer(): lt.Controls.ImageViewer;
      get_useSvgBackImage(): boolean;
      set_useSvgBackImage(value: boolean): void;
      get_zoomRatio(): number;
      set_zoomRatio(value: number): void;
      get_itemType(): DocumentViewerItemType;
      get_preferredItemType(): DocumentViewerItemType;
      set_preferredItemType(value: DocumentViewerItemType): void;
      runLinkTarget(linkTarget: lt.Documents.DocumentLinkTarget): void;
      invalidate(bounds: lt.LeadRectD): void;
      dispose(): void;
      get_workerCount(): number;
      set_workerCount(value: number): void;
      get_lazyLoad(): boolean;
      set_lazyLoad(value: boolean): void;
      documentViewer: DocumentViewer; // read-only
      imageViewer: lt.Controls.ImageViewer; // read-only
      useSvgBackImage: boolean;
      zoomRatio: number;
      itemType: DocumentViewerItemType; // read-only
      preferredItemType: DocumentViewerItemType;
      workerCount: number;
      lazyLoad: boolean;
   }

   enum DocumentViewerItemType {
      image = 0,
      svg = 1
   }

   class DocumentViewerFactory {
      static createDocumentViewer(createOptions: DocumentViewerCreateOptions): DocumentViewer;
   }
}
