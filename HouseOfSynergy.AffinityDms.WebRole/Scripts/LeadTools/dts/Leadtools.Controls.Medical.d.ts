//***********************************************************************************************
//   Type definitions for Leadtools.Controls.Medical.js
//   Updated: 4/18/2016 19:17
//   Version: 19.0.0.6
//   Reference with:
//   /// <reference path="Leadtools.Controls.Medical.d.ts" />
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

declare module lt.Controls.Medical {

   class AutomationImageViewer extends lt.Controls.ImageViewer {
      get_automationScrollOffset(): lt.LeadPointD;
      get_customObjects(): LeadCollection;
      set_customObjects(value: LeadCollection): void;
      get_automationObject(): any;
      set_automationObject(value: any): void;
      automationAttach(container: lt.Annotations.Core.AnnContainer): void;
      automationDetach(): void;
      add_automationDoubleClick(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationDoubleClick(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      get_automationDpiX(): number;
      get_automationDpiY(): number;
      get_automationEnabled(): boolean;
      add_automationEnabledChanged(value: lt.LeadEventHandler): void;
      remove_automationEnabledChanged(value: lt.LeadEventHandler): void;
      get_automationGetContainersCallback(): lt.Annotations.Core.AnnAutomationControlGetContainersCallback;
      set_automationGetContainersCallback(value: lt.Annotations.Core.AnnAutomationControlGetContainersCallback): void;
      add_automationGotFocus(value: lt.LeadEventHandler): void;
      remove_automationGotFocus(value: lt.LeadEventHandler): void;
      automationInvalidate(invalidateRect: lt.LeadRectD): void;
      add_automationLostFocus(value: lt.LeadEventHandler): void;
      remove_automationLostFocus(value: lt.LeadEventHandler): void;
      add_automationPointerDown(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationPointerDown(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      add_automationPointerMove(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationPointerMove(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      add_automationPointerUp(value: lt.Annotations.Core.AnnPointerEventHandler): void;
      remove_automationPointerUp(value: lt.Annotations.Core.AnnPointerEventHandler): void;
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
      get_renderingEngine(): lt.Annotations.Core.AnnRenderingEngine;
      set_renderingEngine(value: lt.Annotations.Core.AnnRenderingEngine): void;
      onItemChanged(e: lt.Controls.ImageViewerItemChangedEventArgs): void;  // protected
      onTransformChanged(e: lt.LeadEventArgs): void;
      onAutomationPointerDown(args: lt.Annotations.Core.AnnPointerEventArgs): void;
      onAutomationPointerMove(args: lt.Annotations.Core.AnnPointerEventArgs): void;
      onAutomationPointerUp(args: lt.Annotations.Core.AnnPointerEventArgs): void;
      onAutomationDoubleClick(args: lt.Annotations.Core.AnnPointerEventArgs): void;
      get_automationDataProvider(): lt.Annotations.Core.AnnDataProvider;
      set_automationDataProvider(value: lt.Annotations.Core.AnnDataProvider): void;
      get_automationAntiAlias(): boolean;
      set_automationAntiAlias(value: boolean): void;
      get_automationContainerIndex(): number;
      set_automationContainerIndex(value: number): void;
      constructor(createOptions: lt.Controls.ImageViewerCreateOptions, divID: string);
      automationScrollOffset: lt.LeadPointD; // read-only
      customObjects: LeadCollection;
      automationObject: any;
      automationDpiX: number; // read-only
      automationDpiY: number; // read-only
      automationEnabled: boolean; // read-only
      automationGetContainersCallback: lt.Annotations.Core.AnnAutomationControlGetContainersCallback;
      automationTransform: lt.LeadMatrix; // read-only
      automationUseDpi: boolean; // read-only
      automationXResolution: number; // read-only
      automationYResolution: number; // read-only
      renderingEngine: lt.Annotations.Core.AnnRenderingEngine;
      automationDataProvider: lt.Annotations.Core.AnnDataProvider;
      automationAntiAlias: boolean;
      automationContainerIndex: number;
      automationDoubleClick: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationEnabledChanged: lt.LeadEventType; // read-only
      automationGotFocus: lt.LeadEventType; // read-only
      automationLostFocus: lt.LeadEventType; // read-only
      automationPointerDown: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationPointerMove: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationPointerUp: lt.Annotations.Core.AnnPointerEventType; // read-only
      automationSizeChanged: lt.LeadEventType; // read-only
      automationTransformChanged: lt.LeadEventType; // read-only
      automationUseDpiChanged: lt.LeadEventType; // read-only
   }

   class AutomationInteractiveMode extends lt.Controls.ImageViewerInteractiveMode {
      get_id(): number;
      start(viewer: lt.Controls.ImageViewer): void;
      stop(viewer: lt.Controls.ImageViewer): void;
      get_name(): string;
      constructor();
      id: number; // read-only
      name: string; // read-only
   }

   class DICOMImageInformation {
      get_autoScaleIntercept(): number;
      set_autoScaleIntercept(value: number): void;
      get_autoScaleSlope(): number;
      set_autoScaleSlope(value: number): void;
      get_image(): HTMLImageElement;
      set_image(value: HTMLImageElement): void;
      get_canvas(): HTMLCanvasElement;
      set_canvas(value: HTMLCanvasElement): void;
      get_width(): number;
      set_width(value: number): void;
      get_height(): number;
      set_height(value: number): void;
      get_bitsPerPixel(): number;
      set_bitsPerPixel(value: number): void;
      get_minValue(): number;
      set_minValue(value: number): void;
      get_maxValue(): number;
      set_maxValue(value: number): void;
      get_luT32(): number[];
      set_luT32(value: number[]): void;
      get_luT2(): any[];
      set_luT2(value: any[]): void;
      get_data32(): number[];
      set_data32(value: number[]): void;
      get_windowWidth(): number;
      set_windowWidth(value: number): void;
      get_windowCenter(): number;
      set_windowCenter(value: number): void;
      get_lowBit(): number;
      set_lowBit(value: number): void;
      get_highBit(): number;
      set_highBit(value: number): void;
      get_modalityIntercept(): number;
      set_modalityIntercept(value: number): void;
      get_modalitySlope(): number;
      set_modalitySlope(value: number): void;
      get_signed(): boolean;
      set_signed(value: boolean): void;
      get_photometricInterpretation(): string;
      set_photometricInterpretation(value: string): void;
      get_lutDescriptor(): number[];
      set_lutDescriptor(value: number[]): void;
      get_firstStoredPixelValueMapped(): number;
      set_firstStoredPixelValueMapped(value: number): void;
      clone(): DICOMImageInformation;
      resize(resizeFactor: number): DICOMImageInformation;
      constructor();
      autoScaleIntercept: number;
      autoScaleSlope: number;
      image: HTMLImageElement;
      canvas: HTMLCanvasElement;
      width: number;
      height: number;
      bitsPerPixel: number;
      minValue: number;
      maxValue: number;
      luT32: number[];
      luT2: any[];
      data32: number[];
      windowWidth: number;
      windowCenter: number;
      lowBit: number;
      highBit: number;
      modalityIntercept: number;
      modalitySlope: number;
      signed: boolean;
      photometricInterpretation: string;
      lutDescriptor: number[];
      firstStoredPixelValueMapped: number;
   }

   interface OnPrepareDataCompleted {
      (): void;
   }

   enum WindowLevelPaletteType {
      none = 0,
      cool = 1,
      cyanHot = 2,
      fire = 3,
      icA2 = 4,
      ice = 5,
      orangeHot = 6,
      rainbowRGB = 7,
      redHot = 8,
      spectrum = 9
   }

   class DICOMImageInformationRenderer {
      dispose(): void;
      get_information(): DICOMImageInformation;
      get_minimumWindowLevelWidth(): number;
      get_maximumWindowLevelWidth(): number;
      get_minimumWindowLevelCenter(): number;
      get_maximumWindowLevelCenter(): number;
      get_originalWindowLevelWidth(): number;
      get_originalWindowLevelCenter(): number;
      get_windowLevelWidth(): number;
      get_windowLevelCenter(): number;
      reset(): void;
      updateWindowLevelLUT(width: number, center: number): void;
      add_changed(value: lt.LeadEventHandler): void;
      remove_changed(value: lt.LeadEventHandler): void;
      onChanged(e: lt.LeadEventArgs): void;
      static shouldResize(width: number, height: number): number;
      renderUsingCanvas(canvas: HTMLCanvasElement, isNewImageData: boolean): void;
      renderInPlace(chunk: ChunkData): void;
      render(canvas: HTMLCanvasElement): void;
      add_updateImageData(value: lt.LeadEventHandler): void;
      remove_updateImageData(value: lt.LeadEventHandler): void;
      onUpdateImageData(e: lt.LeadEventArgs): void;
      loadPaletteData(xmlFileName: string): void;
      get_paletteType(): WindowLevelPaletteType;
      set_paletteType(value: WindowLevelPaletteType): void;
      constructor(information: DICOMImageInformation);
      information: DICOMImageInformation; // read-only
      minimumWindowLevelWidth: number; // read-only
      maximumWindowLevelWidth: number; // read-only
      minimumWindowLevelCenter: number; // read-only
      maximumWindowLevelCenter: number; // read-only
      originalWindowLevelWidth: number; // read-only
      originalWindowLevelCenter: number; // read-only
      windowLevelWidth: number; // read-only
      windowLevelCenter: number; // read-only
      paletteType: WindowLevelPaletteType;
      changed: lt.LeadEventType; // read-only
      updateImageData: lt.LeadEventType; // read-only
   }

   class ImageProcessing {
      static getHistogramPoint(imageData: ImageData, threshold: number): lt.LeadPointD;
      static levelIntensity(imageData: ImageData, low: number, high: number): void;
      static stretchIntensity(imageData: ImageData, threshold: number): void;
      constructor();
   }

   enum MedicalViewerSizeMode {
      none = 0,
      actualSize = 1,
      fit = 2,
      trueSize = 3
   }

   class LayoutManagerItem {
      get_userData(): any;
      set_userData(value: any): void;
      get_div(): HTMLDivElement;
      get_divID(): string;
      set_divID(value: string): void;
      get_parent(): LayoutManager;
      set_parent(value: LayoutManager): void;
      get_displayRectangle(): lt.LeadRectD;
      set_displayRectangle(value: lt.LeadRectD): void;
      get_bounds(): lt.LeadRectD;
      set_bounds(value: lt.LeadRectD): void;
      get_selected(): boolean;
      set_selected(value: boolean): void;
      onSizeChanged(): void;
      constructor();
      userData: any;
      div: HTMLDivElement; // read-only
      divID: string;
      parent: LayoutManager;
      displayRectangle: lt.LeadRectD;
      bounds: lt.LeadRectD;
      selected: boolean;
   }

   class LayoutManager {
      get_items(): LeadCollection;
      get_selectedItems(): LeadCollection;
      get_selectedItem(): LayoutManagerItem;
      set_selectedItem(value: LayoutManagerItem): void;
      selectItem(item: LayoutManagerItem, select: boolean): void;
      get_div(): HTMLDivElement;
      onSizeChanged(): void;
      beginUpdate(): void;
      canUpdate(): boolean;
      endUpdate(): void;
      constructor(divParent: HTMLDivElement);
      items: LeadCollection; // read-only
      selectedItems: LeadCollection; // read-only
      selectedItem: LayoutManagerItem;
      div: HTMLDivElement; // read-only
   }

   class GridLayout {
      get_splitterSize(): number;
      set_splitterSize(value: number): void;
      get_rows(): number;
      set_rows(value: number): void;
      get_columns(): number;
      set_columns(value: number): void;
      get_horizontalSplitters(): number[];
      get_verticalSplitters(): number[];
      get_splittersMovable(): boolean;
      set_splittersMovable(value: boolean): void;
      constructor(viewer: MedicalViewer, rows: number, columns: number);
      splitterSize: number;
      rows: number;
      columns: number;
      horizontalSplitters: number[]; // read-only
      verticalSplitters: number[]; // read-only
      splittersMovable: boolean;
   }

   enum CellsArrangment {
      grid = 0,
      random = 1
   }

   class MedicalViewer extends lt.Controls.InteractiveService {
      onSizeChanged(): void;
      renderViewer(): void;
      layoutCells(): void;
      refreshReferenceLine(): void;
      get_totalCells(): number;
      set_totalCells(value: number): void;
      synchronizeSeries(cell: Cell): void;
      get_emptyDivs(): LayoutManager;
      invalidate(): void;
      get_emptyCellColor(): string;
      set_emptyCellColor(value: string): void;
      get_selectedSeries(): LeadCollection;
      get_activeSeries(): Cell;
      set_activeSeries(value: Cell): void;
      get_showReferenceLine(): boolean;
      set_showReferenceLine(value: boolean): void;
      get_showFirstAndLastReferenceLine(): boolean;
      set_showFirstAndLastReferenceLine(value: boolean): void;
      get_enableSynchronization(): boolean;
      set_enableSynchronization(value: boolean): void;
      explode(cell: Cell, exploded: boolean): void;
      get_exploded(): boolean;
      get_explodedCell(): Cell;
      add_beforeCellExploded(value: CellExplodedEventHandler): void;
      remove_beforeCellExploded(value: CellExplodedEventHandler): void;
      add_afterCellExploded(value: CellExplodedEventHandler): void;
      remove_afterCellExploded(value: CellExplodedEventHandler): void;
      onAfterCellExploded(cell: Cell): void;  // protected
      onBeforeCellExploded(cell: Cell): void;  // protected
      get_divId(): string;
      set_cellsArrangment(value: CellsArrangment): void;
      get_cellsArrangment(): CellsArrangment;
      get_gridLayout(): GridLayout;
      onDragStarted(e: lt.Controls.InteractiveDragStartedEventArgs): void;  // protected
      onDragDelta(e: lt.Controls.InteractiveDragDeltaEventArgs): void;  // protected
      onDragCompleted(e: lt.Controls.InteractiveDragCompletedEventArgs): void;  // protected
      constructor(parent: HTMLDivElement, rows: number, columns: number);
      totalCells: number;
      emptyDivs: LayoutManager; // read-only
      emptyCellColor: string;
      selectedSeries: LeadCollection; // read-only
      activeSeries: Cell;
      showReferenceLine: boolean;
      showFirstAndLastReferenceLine: boolean;
      enableSynchronization: boolean;
      exploded: boolean; // read-only
      explodedCell: Cell; // read-only
      divId: string; // read-only
      cellsArrangment: CellsArrangment;
      gridLayout: GridLayout; // read-only
      beforeCellExploded: CellExplodedEventType; // read-only
      afterCellExploded: CellExplodedEventType; // read-only
      layout: LayoutManager;
      selectedCellChanged: lt.LeadEventType;
      emptyCellClicked: lt.LeadEventType;
   }

   enum FrameArrangment {
      grid = 0,
      custom = 1,
      rowSymmetric = 2,
      colSymmetric = 3
   }

   class CellGridLayout {
      get_value1(): number;
      set_value1(value: number): void;
      get_value2(): number;
      set_value2(value: number): void;
      get_value3(): number;
      set_value3(value: number): void;
      get_value4(): number;
      set_value4(value: number): void;
      get_rows(): number;
      set_rows(value: number): void;
      get_columns(): number;
      set_columns(value: number): void;
      constructor(cell: Cell, rows: number, columns: number);
      value1: number;
      value2: number;
      value3: number;
      value4: number;
      rows: number;
      columns: number;
   }

   class EmptyCell extends LayoutManagerItem {
      dispose(): void;
      get_position(): number;
      set_position(value: number): void;
      get_rowPosition(): number;
      set_rowPosition(value: number): void;
      get_columnsPosition(): number;
      set_columnsPosition(value: number): void;
      constructor(manager: LayoutManager, viewer: MedicalViewer, id: string, rowPosition: number, columnPosition: number);
      position: number;
      rowPosition: number;
      columnsPosition: number;
   }

   interface ImageURLChangedEventHandler {
      (sender: any, e: ImageURLChangedEventArgs): void;
   }

   class ImageURLChangedEventType extends lt.LeadEvent {
      add(value: ImageURLChangedEventHandler): ImageURLChangedEventHandler;
      remove(value: ImageURLChangedEventHandler): void;
   }

   class ImageURLChangedEventArgs extends CellEventArgs {
      constructor(frame: Frame);
   }

   interface ImageProcessingReadyEventHandler {
      (sender: any, e: ImageProcessingReadyEventArgs): void;
   }

   class ImageProcessingReadyEventType extends lt.LeadEvent {
      add(value: ImageProcessingReadyEventHandler): ImageProcessingReadyEventHandler;
      remove(value: ImageProcessingReadyEventHandler): void;
   }

   class ImageProcessingReadyEventArgs extends lt.LeadEventArgs {
      get_imageProcessing(): lt.ImageProcessing;
      get_frame(): Frame;
      constructor(frame: Frame, imageProcessing: lt.ImageProcessing);
      imageProcessing: lt.ImageProcessing; // read-only
      frame: Frame; // read-only
   }

   enum MPRStatus {
      ok = 0,
      imagePositionNotReady = 2,
      allFramesNotReady = 4,
      cellNotValid = 8,
      notEnoughFrames = -1
   }

   interface CellEventHandler {
      (sender: any, e: CellEventArgs): void;
   }

   class CellEventType extends lt.LeadEvent {
      add(value: CellEventHandler): CellEventHandler;
      remove(value: CellEventHandler): void;
   }

   class CellEventArgs extends lt.LeadEventArgs {
      get_frame(): Frame;
      constructor(frame: Frame);
      frame: Frame; // read-only
   }

   interface CellExplodedEventHandler {
      (sender: any, e: CellExplodedEventArgs): void;
   }

   class CellExplodedEventType extends lt.LeadEvent {
      add(value: CellExplodedEventHandler): CellExplodedEventHandler;
      remove(value: CellExplodedEventHandler): void;
   }

   class CellExplodedEventArgs extends lt.LeadEventArgs {
      get_cell(): Cell;
      constructor(cell: Cell);
      cell: Cell; // read-only
   }

   interface FrameAttachedEventHandler {
      (sender: any, e: FrameAttachedEventArgs): void;
   }

   class FrameAttachedEventType extends lt.LeadEvent {
      add(value: FrameAttachedEventHandler): FrameAttachedEventHandler;
      remove(value: FrameAttachedEventHandler): void;
   }

   class FrameAttachedEventArgs extends lt.LeadEventArgs {
      get_subCell(): SubCell;
      get_frame(): Frame;
      constructor(subcell: SubCell, frame: Frame);
      subCell: SubCell; // read-only
      frame: Frame; // read-only
   }

   enum CellMPRType {
      axial = 0,
      sagittal = 1,
      coronal = 2,
      none = -1
   }

   class MPRCell extends Cell {
      get_generator(): Cell;
      reloadMPR(): void;
      static canDoMPR(cell: Cell): MPRStatus;
      constructor(generator: Cell, vertical: boolean, divID: string);
      generator: Cell; // read-only
      mprImage: HTMLCanvasElement;
   }

   interface ScrollChangedEventHandler {
      (sender: any, e: ScrollChangedEventArgs): void;
   }

   class ScrollChangedEventType extends lt.LeadEvent {
      add(value: ScrollChangedEventHandler): ScrollChangedEventHandler;
      remove(value: ScrollChangedEventHandler): void;
   }

   class ScrollChangedEventArgs extends lt.LeadEventArgs {
      get_scrollOffset(): number;
      get_previousScrollOffset(): number;
      constructor(previousScrollOffset: number, scrollOffset: number);
      scrollOffset: number; // read-only
      previousScrollOffset: number; // read-only
   }

   class Cell extends LayoutManagerItem {
      onSizeChanged(): void;
      updateSubCellCount(newCount: number): void;
      get_imageViewer(): AutomationImageViewer;
      set_imageViewer(value: AutomationImageViewer): void;
      get_viewer(): MedicalViewer;
      get_gridLayout(): CellGridLayout;
      get_progress(): ProgressLoading;
      set_progress(value: ProgressLoading): void;
      get_windowLevel(): WindowLevelInteractiveMode;
      set_windowLevel(value: WindowLevelInteractiveMode): void;
      get_drawCrossHairLines(): boolean;
      set_drawCrossHairLines(value: boolean): void;
      set_borderThickness(value: number): void;
      get_borderThickness(): number;
      get_arrangment(): FrameArrangment;
      set_arrangment(value: FrameArrangment): void;
      get_showFrameBorder(): boolean;
      set_showFrameBorder(value: boolean): void;
      get_currentOffset(): number;
      set_currentOffset(value: number): void;
      get_framesMappingIndex(): number[];
      set_framesMappingIndex(value: number[]): void;
      get_frames(): LeadCollection;
      get_overlays(): LeadCollection;
      invalidate(): void;
      add_dispoing(value: lt.LeadEventHandler): void;
      remove_dispoing(value: lt.LeadEventHandler): void;
      dispose(): void;
      supportWindowLevel(frameIndex: number): boolean;
      add_outOfBounds(value: lt.LeadEventHandler): void;
      remove_outOfBounds(value: lt.LeadEventHandler): void;
      add_currentOffsetChanged(value: lt.LeadEventHandler): void;
      remove_currentOffsetChanged(value: lt.LeadEventHandler): void;
      add_cellClicked(value: lt.LeadEventHandler): void;
      remove_cellClicked(value: lt.LeadEventHandler): void;
      add_imageURLChanged(value: ImageURLChangedEventHandler): void;
      remove_imageURLChanged(value: ImageURLChangedEventHandler): void;
      add_postRender(value: lt.Controls.ImageViewerRenderEventHandler): void;
      remove_postRender(value: lt.Controls.ImageViewerRenderEventHandler): void;
      add_sizeChanged(value: lt.LeadEventHandler): void;
      remove_sizeChanged(value: lt.LeadEventHandler): void;
      add_scrollChanged(value: ScrollChangedEventHandler): void;
      remove_scrollChanged(value: ScrollChangedEventHandler): void;
      add_progressCompleted(value: lt.LeadEventHandler): void;
      remove_progressCompleted(value: lt.LeadEventHandler): void;
      add_imageProcessingReady(value: ImageProcessingReadyEventHandler): void;
      remove_imageProcessingReady(value: ImageProcessingReadyEventHandler): void;
      get_mprType(): CellMPRType;
      set_mprType(value: CellMPRType): void;
      get_children(): LeadCollection;
      beginUpdate(): void;
      endUpdate(): void;
      get_selectedItems(): LeadCollection;
      set_selectedItems(value: LeadCollection): void;
      get_selectedItem(): SubCell;
      set_selectedItem(value: SubCell): void;
      get_automationManager(): lt.Annotations.Automation.AnnAutomationManager;
      get_automation(): lt.Annotations.Automation.AnnAutomation;
      get_selectedBorderColor(): string;
      set_selectedBorderColor(value: string): void;
      get_unselectedBorderColor(): string;
      set_unselectedBorderColor(value: string): void;
      get_selectedSubCellBorderColor(): string;
      set_selectedSubCellBorderColor(value: string): void;
      get_highlightedSubCellBorderColor(): string;
      set_highlightedSubCellBorderColor(value: string): void;
      get_useBackCanvas(): boolean;
      set_useBackCanvas(value: boolean): void;
      get_overlayTextVisible(): boolean;
      set_overlayTextVisible(value: boolean): void;
      suspendCalculation(): void;
      resumeCalcuation(): void;
      setCommand(commandID: number, command: ActionCommand): void;
      stopCommand(commandID: number): void;
      getCommand(commandID: number): any;
      runCommand(commandID: number): void;
      get_seriesNumber(): number;
      set_seriesNumber(value: number): void;
      get_patientName(): string;
      set_patientName(value: string): void;
      get_seriesInstanceUID(): string;
      set_seriesInstanceUID(value: string): void;
      get_frameOfReferenceUID(): string;
      set_frameOfReferenceUID(value: string): void;
      get_position(): number;
      set_position(value: number): void;
      get_rowPosition(): number;
      set_rowPosition(value: number): void;
      get_columnsPosition(): number;
      set_columnsPosition(value: number): void;
      get_linked(): boolean;
      set_linked(value: boolean): void;
      withinVisibleRange(index: number): boolean;
      constructor(viewer: MedicalViewer, divID: string, rows: number, columns: number);
      mprType: CellMPRType;
      children: LeadCollection; // read-only
      selectedItems: LeadCollection;
      selectedItem: SubCell;
      automationManager: lt.Annotations.Automation.AnnAutomationManager; // read-only
      automation: lt.Annotations.Automation.AnnAutomation; // read-only
      selectedBorderColor: string;
      unselectedBorderColor: string;
      selectedSubCellBorderColor: string;
      highlightedSubCellBorderColor: string;
      useBackCanvas: boolean;
      overlayTextVisible: boolean;
      seriesNumber: number;
      patientName: string;
      seriesInstanceUID: string;
      frameOfReferenceUID: string;
      position: number;
      rowPosition: number;
      columnsPosition: number;
      linked: boolean;
      imageViewer: AutomationImageViewer;
      viewer: MedicalViewer; // read-only
      gridLayout: CellGridLayout; // read-only
      progress: ProgressLoading;
      windowLevel: WindowLevelInteractiveMode;
      drawCrossHairLines: boolean;
      borderThickness: number;
      arrangment: FrameArrangment;
      showFrameBorder: boolean;
      currentOffset: number;
      framesMappingIndex: number[];
      frames: LeadCollection; // read-only
      overlays: LeadCollection; // read-only
      outOfBounds: lt.LeadEventType; // read-only
      currentOffsetChanged: lt.LeadEventType; // read-only
      cellClicked: lt.LeadEventType; // read-only
      imageURLChanged: ImageURLChangedEventType; // read-only
      postRender: lt.Controls.ImageViewerRenderEventType; // read-only
      sizeChanged: lt.LeadEventType; // read-only
      scrollChanged: ScrollChangedEventType; // read-only
      progressCompleted: lt.LeadEventType; // read-only
      imageProcessingReady: ImageProcessingReadyEventType; // read-only
      dispoing: lt.LeadEventType; // read-only
   }

   class SubCell extends lt.Controls.ImageViewerItem {
      get_backColor(): string;
      set_backColor(value: string): void;
      get_bounds(): lt.LeadRectD;
      set_bounds(value: lt.LeadRectD): void;
      get_selected(): boolean;
      set_selected(value: boolean): void;
      get_div(): HTMLDivElement;
      get_divID(): string;
      set_divID(value: string): void;
      get_overlayCanvas(): HTMLCanvasElement;
      get_parentCell(): Cell;
      get_foreColor(): string;
      set_foreColor(value: string): void;
      add_frameAttached(value: FrameAttachedEventHandler): void;
      remove_frameAttached(value: FrameAttachedEventHandler): void;
      invalidate(): void;
      updateOverlay(): void;
      get_attachedFrame(): Frame;
      set_attachedFrame(value: Frame): void;
      get_annotationCanvas(): HTMLCanvasElement;
      set_annotationCanvas(value: HTMLCanvasElement): void;
      onSizeChanged(): void;
      dispose(): void;
      constructor(cell: Cell, divID: string);
      backColor: string;
      bounds: lt.LeadRectD;
      selected: boolean;
      div: HTMLDivElement; // read-only
      divID: string;
      overlayCanvas: HTMLCanvasElement; // read-only
      parentCell: Cell; // read-only
      foreColor: string;
      attachedFrame: Frame;
      annotationCanvas: HTMLCanvasElement;
      frameAttached: FrameAttachedEventType; // read-only
   }

   class MRTISubCell extends SubCell {
      updateWindowlevel(all: boolean): void;
      updateWindowlevelOnTheRest(): void;
      get_chunkList(): ChunkData[];
      get_enableMRTI(): boolean;
      set_enableMRTI(value: boolean): void;
      onSizeChanged(): void;
      static getFloatImageRect(imageViewer: lt.Controls.ImageViewer, item: lt.Controls.ImageViewerItem): lt.LeadRectD;
      paintTiles(frameContext: CanvasRenderingContext2D): boolean;
      connect(image: MRTIImage): void;
      get_tileResolution(): lt.LeadSizeD;
      set_tileResolution(value: lt.LeadSizeD): void;
      get_tileBackGroundResolution(): lt.LeadSizeD;
      set_tileBackGroundResolution(value: lt.LeadSizeD): void;
      updateView(counter: number, reRequest: boolean): void;
      get_currentScale(): number;
      set_currentScale(value: number): void;
      get_position(): lt.LeadPointD;
      set_position(value: lt.LeadPointD): void;
      clearUnnecessaryChunks(): void;
      constructor(cell: Cell, divID: string);
      chunkList: ChunkData[]; // read-only
      enableMRTI: boolean;
      tileResolution: lt.LeadSizeD;
      tileBackGroundResolution: lt.LeadSizeD;
      currentScale: number;
      position: lt.LeadPointD;
      thumbnailSize: lt.LeadSizeD;
      fullScreenCanvas: HTMLCanvasElement;
      mrtiBackCanvas: HTMLCanvasElement;
   }

   class Frame {
      set_rotateAngle(value: number): void;
      get_offsetX(): number;
      set_offsetX(value: number): void;
      getFullSize(): lt.LeadSizeD;
      get_offsetY(): number;
      set_offsetY(value: number): void;
      get_scale(): number;
      set_scale(value: number): void;
      zoom(sizeMode: MedicalViewerSizeMode, scaleFactor: number): void;
      get_scaleMode(): MedicalViewerSizeMode;
      reset(): void;
      get_useDPI(): boolean;
      set_useDPI(value: boolean): void;
      set_information(value: DICOMImageInformation): void;
      get_imagePosition(): number[];
      set_imagePosition(value: number[]): void;
      get_imageOrientation(): number[];
      set_imageOrientation(value: number[]): void;
      isImageDataAvailable(): boolean;
      get_wlRenderer(): DICOMImageInformationRenderer;
      get_width(): number;
      set_width(value: number): void;
      get_height(): number;
      set_height(value: number): void;
      get_rowSpacing(): number;
      set_rowSpacing(value: number): void;
      get_columnSpacing(): number;
      set_columnSpacing(value: number): void;
      get_projectionOrientation(): ProjectionOrientationType;
      set_projectionOrientation(value: ProjectionOrientationType): void;
      get_patientProjection(): string[];
      set_patientProjection(value: string[]): void;
      get_instanceNumber(): number;
      set_instanceNumber(value: number): void;
      get_bitPerpixel(): number;
      set_bitPerpixel(value: number): void;
      get_defaultWindowLevelWidth(): number;
      get_defaultWindowLevelCenter(): number;
      setWindowLevelDefaultValues(width: number, center: number): void;
      get_windowWidth(): number;
      get_windowCenter(): number;
      setWindowLevel(width: number, center: number): void;
      get_minValue(): number;
      set_minValue(value: number): void;
      get_maxValue(): number;
      set_maxValue(value: number): void;
      get_lowBit(): number;
      set_lowBit(value: number): void;
      get_highBit(): number;
      set_highBit(value: number): void;
      get_bitStored(): number;
      set_bitStored(value: number): void;
      get_rescaleintercept(): number;
      set_rescaleintercept(value: number): void;
      get_rescaleSlope(): number;
      set_rescaleSlope(value: number): void;
      get_voiLUTSequence(): number[];
      set_voiLUTSequence(value: number[]): void;
      get_imageType(): string[];
      set_imageType(value: string[]): void;
      get_lossyCompression(): boolean;
      set_lossyCompression(value: boolean): void;
      get_isWaveForm(): boolean;
      set_isWaveForm(value: boolean): void;
      get_frameOfReferenceID(): string;
      set_frameOfReferenceID(value: string): void;
      get_photometricInterpretation(): string;
      set_photometricInterpretation(value: string): void;
      get_flipped(): boolean;
      set_flipped(value: boolean): void;
      get_reversed(): boolean;
      set_reversed(value: boolean): void;
      get_rotateAngle(): number;
      add_imageDataReady(value: lt.LeadEventHandler): void;
      remove_imageDataReady(value: lt.LeadEventHandler): void;
      get_imageProcessingList(): LeadCollection;
      getPreviewCanvas(): HTMLCanvasElement;
      get_mrtiInfo(): MRTIImage;
      set_mrtiInfo(value: MRTIImage): void;
      get_retakeIndex(): number;
      set_retakeIndex(value: number): void;
      get_nFrame(): Frame;
      get_retakes(): LeadCollection;
      get_subCell(): SubCell;
      get_shutter(): ShutterObject;
      get_lineProfile(): LineProfileObject;
      get_imageQuality(): string;
      get_container(): lt.Annotations.Core.AnnContainer;
      get_enableDraw(): boolean;
      set_enableDraw(value: boolean): void;
      get_parentCell(): Cell;
      get_inverted(): boolean;
      set_inverted(value: boolean): void;
      get_wlData(): WindowLevelData;
      get_userData(): any;
      set_userData(value: any): void;
      dispose(): void;
      get_isDataReady(): boolean;
      get_information(): DICOMImageInformation;
      constructor(parent: Cell);
      imageProcessingList: LeadCollection; // read-only
      mrtiInfo: MRTIImage;
      retakeIndex: number;
      nFrame: Frame;
      retakes: LeadCollection;
      subCell: SubCell; // read-only
      shutter: ShutterObject; // read-only
      lineProfile: LineProfileObject; // read-only
      imageQuality: string; // read-only
      container: lt.Annotations.Core.AnnContainer; // read-only
      enableDraw: boolean;
      parentCell: Cell; // read-only
      inverted: boolean;
      wlData: WindowLevelData; // read-only
      userData: any;
      isDataReady: boolean; // read-only
      information: DICOMImageInformation;
      imagePosition: number[];
      imageOrientation: number[];
      wlRenderer: DICOMImageInformationRenderer; // read-only
      width: number;
      height: number;
      rowSpacing: number;
      columnSpacing: number;
      projectionOrientation: ProjectionOrientationType;
      patientProjection: string[];
      instanceNumber: number;
      bitPerpixel: number;
      defaultWindowLevelWidth: number; // read-only
      defaultWindowLevelCenter: number; // read-only
      windowWidth: number; // read-only
      windowCenter: number; // read-only
      minValue: number;
      maxValue: number;
      lowBit: number;
      highBit: number;
      bitStored: number;
      rescaleintercept: number;
      rescaleSlope: number;
      voiLUTSequence: number[];
      imageType: string[];
      lossyCompression: boolean;
      isWaveForm: boolean;
      frameOfReferenceID: string;
      photometricInterpretation: string;
      flipped: boolean;
      reversed: boolean;
      rotateAngle: number;
      offsetX: number;
      offsetY: number;
      scale: number;
      scaleMode: MedicalViewerSizeMode; // read-only
      useDPI: boolean;
      imageDataReady: lt.LeadEventType; // read-only
      lowResImage: ChunkData;
      thumbnailImage: ChunkData;
   }

   enum OverlayTextType {
      windowLevel = 0,
      instanceNumber = 1,
      userData = 2,
      imageQuality = 3,
      frameNumber = 4,
      leftOrientation = 5,
      rightOrientation = 6,
      topOrientation = 7,
      bottomOrientation = 8,
      mprType = 9,
      retakeImage = 10
   }

   enum OverlayAlignment {
      topLeft = 0,
      topRight = 1,
      bottomLeft = 2,
      bottomRight = 3,
      centerTop = 4,
      centerLeft = 5,
      centerRight = 6,
      centerBottom = 7,
      none = -1
   }

   class OverlayText {
      get_text(): string;
      set_text(value: string): void;
      get_positionIndex(): number;
      set_positionIndex(value: number): void;
      get_alignment(): OverlayAlignment;
      set_alignment(value: OverlayAlignment): void;
      get_type(): OverlayTextType;
      set_type(value: OverlayTextType): void;
      get_color(): string;
      set_color(value: string): void;
      get_weight(): number;
      set_weight(value: number): void;
      constructor();
      text: string;
      positionIndex: number;
      alignment: OverlayAlignment;
      type: OverlayTextType;
      color: string;
      weight: number;
   }

   enum MedicalNotifyCollectionChangedAction {
      reset = 0,
      add = 1,
      move = 2,
      remove = 3,
      replace = 4,
      insert = 5
   }

   interface LeadCollectionChangedEventHandler {
      (sender: any, e: LeadCollectionChangedEventArgs): void;
   }

   class LeadCollectionChangedEventType extends lt.LeadEvent {
      add(value: LeadCollectionChangedEventHandler): LeadCollectionChangedEventHandler;
      remove(value: LeadCollectionChangedEventHandler): void;
   }

   class LeadCollectionChangedEventArgs extends lt.LeadEventArgs {
      static create(action: MedicalNotifyCollectionChangedAction): LeadCollectionChangedEventArgs;
      get_action(): MedicalNotifyCollectionChangedAction;
      constructor(action: MedicalNotifyCollectionChangedAction);
      action: MedicalNotifyCollectionChangedAction; // read-only
      newItems: any[];
   }

   class LeadCollection {
      get_count(): number;
      clear(): void;
      insert(item: any, index: number): void;
      remove(item: any): void;
      add(item: any): void;
      contains(item: any): boolean;
      getEnumerator(): any;
      get_item(i: number): any;
      set_item(i: number, value: any): void;
      toArray(): any[];
      insertItem(index: number, item: any): void;  // protected
      move(oldIndex: number, newIndex: number): void;
      moveItem(oldIndex: number, newIndex: number): void;  // protected
      onCollectionChanged(e: LeadCollectionChangedEventArgs): void;  // protected
      removeAt(index: number): void;
      removeItem(item: any): void;  // protected
      setItem(index: number, item: any): void;  // protected
      clearItems(): void;  // protected
      indexOf(item: any): number;
      add_collectionChanged(value: LeadCollectionChangedEventHandler): void;
      remove_collectionChanged(value: LeadCollectionChangedEventHandler): void;
      constructor();
      count: number; // read-only
      item(index: number, value?: any): any;
      collectionChanged: LeadCollectionChangedEventType; // read-only
   }

   class Tools {
      static resetCanvas(canvas: HTMLCanvasElement): void;
      constructor();
   }

   class ActionCommand {
      get_parent(): Cell;
      set_parent(value: Cell): void;
      get_viewer(): lt.Controls.ImageViewer;
      get_button(): lt.Controls.MouseButtons;
      set_button(value: lt.Controls.MouseButtons): void;
      dispose(item: any): void;
      get_isStarted(): boolean;
      init(): any;
      start(actionObject: any): void;
      constructor();
      parent: Cell;
      viewer: lt.Controls.ImageViewer;
      button: lt.Controls.MouseButtons;
      isStarted: boolean;
   }

   class PinchAction extends ActionCommand {
      init(): any;
      start(actionObject: any): void;
      dispose(item: any): void;
      constructor();
   }

   class OffsetAction extends ActionCommand {
      init(): any;
      start(actionObject: any): void;
      dispose(item: any): void;
      constructor();
   }

   class LineProfileAction extends ActionCommand {
      init(): any;
      start(actionObject: any): void;
      dispose(item: any): void;
      constructor();
   }

   class ScaleAction extends ActionCommand {
      init(): any;
      start(actionObject: any): void;
      dispose(item: any): void;
      constructor();
   }

   class MagnifyAction extends ActionCommand {
      init(): any;
      start(actionObject: any): void;
      constructor();
   }

   class LeadPoint3D {
      get_x(): number;
      set_x(value: number): void;
      get_y(): number;
      set_y(value: number): void;
      get_z(): number;
      set_z(value: number): void;
      static create(x: number, y: number, z: number): LeadPoint3D;
      static get_empty(): LeadPoint3D;
      constructor(x: number, y: number, z: number);
      x: number;
      y: number;
      z: number;
      static empty: LeadPoint3D; // read-only
   }

   class CrossHairInteractiveMode extends lt.Controls.ImageViewerInteractiveMode {
      get_id(): number;
      static get3DCursor(targetCell: Cell, sourceCell: Cell, point: LeadPoint3D): LeadPoint3D;
      start(viewer: lt.Controls.ImageViewer): void;
      stop(viewer: lt.Controls.ImageViewer): void;
      static scrollCrossHair(cell: Cell, x: number, y: number): void;
      get_name(): string;
      constructor();
      id: number; // read-only
      name: string; // read-only
   }

   class CrossHairAction extends ActionCommand {
      init(): any;
      start(actionObject: any): void;
      constructor();
   }

   class ProbeToolAction extends ActionCommand {
      add_probeToolUpdated(value: ProbeToolEventHandler): void;
      remove_probeToolUpdated(value: ProbeToolEventHandler): void;
      init(): any;
      get_interactiveObject(): ProbeToolInteractiveMode;
      start(actionObject: any): void;
      constructor();
      interactiveObject: ProbeToolInteractiveMode; // read-only
      probeToolUpdated: ProbeToolEventType; // read-only
   }

   class SpyGlassAction extends MagnifyAction {
      add_imageRequested(value: SpyGlassEventHandler): void;
      remove_imageRequested(value: SpyGlassEventHandler): void;
      add_workCompleted(value: SpyGlassEventHandler): void;
      remove_workCompleted(value: SpyGlassEventHandler): void;
      add_positionChanged(value: SpyGlassPositionChangedEventHandler): void;
      remove_positionChanged(value: SpyGlassPositionChangedEventHandler): void;
      add_chunkLoaded(value: ChunkLoadedEventHandler): void;
      remove_chunkLoaded(value: ChunkLoadedEventHandler): void;
      init(): any;
      start(actionObject: any): void;
      constructor();
      imageRequested: SpyGlassEventType; // read-only
      workCompleted: SpyGlassEventType; // read-only
      positionChanged: SpyGlassPositionChangedEventType; // read-only
      chunkLoaded: ChunkLoadedEventType; // read-only
      tiling: boolean;
   }

   class StackAction extends ActionCommand {
      init(): any;
      start(actionObject: any): void;
      constructor();
   }

   class StackModeInteractiveMode extends lt.Controls.ImageViewerInteractiveMode {
      loadImages(cell: Cell): void;
      get_id(): number;
      start(viewer: lt.Controls.ImageViewer): void;
      stop(viewer: lt.Controls.ImageViewer): void;
      get_name(): string;
      constructor();
      id: number; // read-only
      name: string; // read-only
   }

   class WindowLevelAction extends ActionCommand {
      init(): any;
      dispose(item: any): void;
      start(actionObject: any): void;
      constructor();
   }

   class AutomationInteractiveAction extends ActionCommand {
      init(): any;
      get_objectID(): number;
      set_objectID(value: number): void;
      start(actionObject: any): void;
      constructor(objectID: number);
      objectID: number;
   }

   class CobbAngle {
      add_cobbAngleChanged(value: lt.LeadEventHandler): void;
      remove_cobbAngleChanged(value: lt.LeadEventHandler): void;
      dispose(): void;
      get_line1(): lt.Annotations.Core.AnnPolylineObject;
      set_line1(value: lt.Annotations.Core.AnnPolylineObject): void;
      get_line2(): lt.Annotations.Core.AnnPolylineObject;
      set_line2(value: lt.Annotations.Core.AnnPolylineObject): void;
      get_angle(): number;
      get_backgroundColor(): string;
      set_backgroundColor(value: string): void;
      get_borderColor(): string;
      set_borderColor(value: string): void;
      draw(context: CanvasRenderingContext2D): void;
      constructor(automation: lt.Annotations.Automation.AnnAutomation, line1: lt.Annotations.Core.AnnPolylineObject, line2: lt.Annotations.Core.AnnPolylineObject);
      line1: lt.Annotations.Core.AnnPolylineObject;
      line2: lt.Annotations.Core.AnnPolylineObject;
      angle: number; // read-only
      backgroundColor: string;
      borderColor: string;
      cobbAngleChanged: lt.LeadEventType; // read-only
   }

   class ProgressLoading {
      draw(context: CanvasRenderingContext2D, displayRect: lt.LeadRectD): void;
      setColor(r: number, g: number, b: number): void;
      set_totalFrames(value: number): void;
      get_totalFrames(): number;
      set_progressPercent(value: number): void;
      get_progressPercent(): number;
      set_progress(value: number): void;
      get_progress(): number;
      constructor(cell: Cell);
      totalFrames: number;
      progressPercent: number;
      progress: number;
   }

   enum LineProfileChannel {
      red = 0,
      green = 1,
      blue = 2,
      gray = 3
   }

   class LineProfileObject {
      beginUpdate(): void;
      endUpdate(): void;
      static getLinePoints(startPoint: lt.LeadPointD, endPoint: lt.LeadPointD): lt.LeadPointD[];
      get_logicalStartPoint(): lt.LeadPointD;
      set_logicalStartPoint(value: lt.LeadPointD): void;
      get_logicalEndPoint(): lt.LeadPointD;
      set_logicalEndPoint(value: lt.LeadPointD): void;
      get_physicalStartPoint(): lt.LeadPointD;
      set_physicalStartPoint(value: lt.LeadPointD): void;
      get_physicalEndPoint(): lt.LeadPointD;
      set_physicalEndPoint(value: lt.LeadPointD): void;
      get_context(): CanvasRenderingContext2D;
      set_context(value: CanvasRenderingContext2D): void;
      draw(context: CanvasRenderingContext2D): void;
      constructor(frame: Frame);
      logicalStartPoint: lt.LeadPointD;
      logicalEndPoint: lt.LeadPointD;
      physicalStartPoint: lt.LeadPointD;
      physicalEndPoint: lt.LeadPointD;
      context: CanvasRenderingContext2D;
   }

   class ShutterObject {
      static isValid(annotationobject: lt.Annotations.Core.AnnObject): boolean;
      get_objects(): LeadCollection;
      set_objects(value: LeadCollection): void;
      get_visible(): boolean;
      set_visible(value: boolean): void;
      attach(item: lt.Controls.ImageViewerItem): void;
      onSizeChanged(): void;
      updateView(): void;
      get_fillStyle(): string;
      set_fillStyle(value: string): void;
      constructor(frame: Frame, automation: lt.Annotations.Automation.AnnAutomation);
      objects: LeadCollection;
      visible: boolean;
      fillStyle: string;
   }

   interface SpyGlassPositionChangedEventHandler {
      (sender: any, e: SpyGlassPositionChangedEventArgs): void;
   }

   class SpyGlassPositionChangedEventType extends lt.LeadEvent {
      add(value: SpyGlassPositionChangedEventHandler): SpyGlassPositionChangedEventHandler;
      remove(value: SpyGlassPositionChangedEventHandler): void;
   }

   class SpyGlassPositionChangedEventArgs extends lt.LeadEventArgs {
      get_chunkList(): ChunkData[];
      get_displayRect(): lt.LeadRectD;
      get_canvas(): HTMLCanvasElement;
      get_inputCanvas(): HTMLCanvasElement;
      get_subCell(): SubCell;
      constructor(canvas: HTMLCanvasElement, inputCanvas: HTMLCanvasElement, displayRect: lt.LeadRectD, subCell: SubCell, chunkList: ChunkData[]);
      chunkList: ChunkData[]; // read-only
      displayRect: lt.LeadRectD; // read-only
      canvas: HTMLCanvasElement; // read-only
      inputCanvas: HTMLCanvasElement; // read-only
      subCell: SubCell; // read-only
   }

   interface SpyGlassEventHandler {
      (sender: any, e: SpyGlassEventArgs): void;
   }

   class SpyGlassEventType extends lt.LeadEvent {
      add(value: SpyGlassEventHandler): SpyGlassEventHandler;
      remove(value: SpyGlassEventHandler): void;
   }

   class SpyGlassEventArgs extends lt.LeadEventArgs {
      get_inputCanvas(): HTMLCanvasElement;
      set_inputCanvas(value: HTMLCanvasElement): void;
      get_canvas(): HTMLCanvasElement;
      set_canvas(value: HTMLCanvasElement): void;
      get_userData(): any;
      set_userData(value: any): void;
      constructor(inputCanvas: HTMLCanvasElement, canvas: HTMLCanvasElement, userData: any);
      inputCanvas: HTMLCanvasElement;
      canvas: HTMLCanvasElement;
      userData: any;
   }

   class MedicalSpyGlassInteractiveMode extends lt.Controls.ImageViewerMagnifyGlassInteractiveMode {
      add_imageRequested(value: SpyGlassEventHandler): void;
      remove_imageRequested(value: SpyGlassEventHandler): void;
      add_positionChanged(value: SpyGlassPositionChangedEventHandler): void;
      remove_positionChanged(value: SpyGlassPositionChangedEventHandler): void;
      refresh(): void;
      dispose(): void;
      updateView(subCell: MRTISubCell): void;
      start(viewer: lt.Controls.ImageViewer): void;
      stop(viewer: lt.Controls.ImageViewer): void;
      get_scale(): number;
      set_scale(value: number): void;
      get_text(): string;
      set_text(value: string): void;
      get_showOverlayText(): boolean;
      set_showOverlayText(value: boolean): void;
      get_name(): string;
      constructor();
      scale: number;
      text: string;
      showOverlayText: boolean;
      name: string; // read-only
      imageRequested: SpyGlassEventType; // read-only
      positionChanged: SpyGlassPositionChangedEventType; // read-only
      tiling: boolean;
   }

   interface ProbeToolEventHandler {
      (sender: any, e: ProbeToolEventArgs): void;
   }

   class ProbeToolEventType extends lt.LeadEvent {
      add(value: ProbeToolEventHandler): ProbeToolEventHandler;
      remove(value: ProbeToolEventHandler): void;
   }

   class ProbeToolEventArgs extends lt.LeadEventArgs {
      get_position(): lt.LeadPointD;
      get_pixelValue(): string;
      set_pixelValue(value: string): void;
      get_target(): Frame;
      constructor(position: lt.LeadPointD, target: Frame);
      position: lt.LeadPointD; // read-only
      pixelValue: string;
      target: Frame; // read-only
   }

   class ProbeToolInteractiveMode extends lt.Controls.ImageViewerInteractiveMode {
      get_id(): number;
      start(viewer: lt.Controls.ImageViewer): void;
      stop(viewer: lt.Controls.ImageViewer): void;
      add_probeToolUpdated(value: ProbeToolEventHandler): void;
      remove_probeToolUpdated(value: ProbeToolEventHandler): void;
      get_backgroundColor(): string;
      set_backgroundColor(value: string): void;
      get_textColor(): string;
      set_textColor(value: string): void;
      get_showBorder(): boolean;
      set_showBorder(value: boolean): void;
      get_name(): string;
      static getPixelValue(frame: Frame, x: number, y: number): number[];
      static getHuValue(frame: Frame, x: number, y: number, data: number): string;
      constructor(cell: Cell);
      id: number; // read-only
      backgroundColor: string;
      textColor: string;
      showBorder: boolean;
      name: string; // read-only
      probeToolUpdated: ProbeToolEventType; // read-only
   }

   interface ChunkLoadedEventHandler {
      (sender: any, e: ChunkLoadedEventArgs): void;
   }

   class ChunkLoadedEventType extends lt.LeadEvent {
      add(value: ChunkLoadedEventHandler): ChunkLoadedEventHandler;
      remove(value: ChunkLoadedEventHandler): void;
   }

   class ChunkLoadedEventArgs extends lt.LeadEventArgs {
      get_chunk(): ChunkData;
      constructor(chunk: ChunkData);
      chunk: ChunkData; // read-only
   }

   interface DebugEventHandler {
      (sender: any, e: DebugEventArgs): void;
   }

   class DebugEventType extends lt.LeadEvent {
      add(value: DebugEventHandler): DebugEventHandler;
      remove(value: DebugEventHandler): void;
   }

   class DebugEventArgs extends lt.LeadEventArgs {
      constructor(text: string, param1: number, param2: number, param3: number);
      param1: number;
      param2: number;
      param3: number;
      text: string;
   }

   class ChunkStatus {
      constructor();
      inverted: boolean;
      visible: boolean;
      processed: boolean;
   }

   class ChunkData {
      dispose(): void;
      equal(chunk: ChunkData): boolean;
      clone(): ChunkData;
      canvas: HTMLCanvasElement;
      backCanvas: HTMLCanvasElement;
      resolution: lt.LeadSizeD;
      rect: lt.LeadRectD;
      status: ChunkStatus;
   }

   class RecycledData {
      constructor(canvas: HTMLCanvasElement, backCanvas: HTMLCanvasElement);
      canvas: HTMLCanvasElement;
      backCanvas: HTMLCanvasElement;
   }

   class MRTIImage {
      get_frameIndex(): number;
      set_frameIndex(value: number): void;
      get_imageUri(): string;
      set_imageUri(value: string): void;
      get_imageName(): string;
      set_imageName(value: string): void;
      get_mimeType(): string;
      set_mimeType(value: string): void;
      get_fullSize(): lt.LeadSizeD;
      set_fullSize(value: lt.LeadSizeD): void;
      get_fullDpi(): lt.LeadSizeD;
      set_fullDpi(value: lt.LeadSizeD): void;
      get_resolutions(): lt.LeadSizeD[];
      set_resolutions(value: lt.LeadSizeD[]): void;
      get_tileSize(): lt.LeadSizeD;
      set_tileSize(value: lt.LeadSizeD): void;
      get_supportWindowLevel(): boolean;
      set_supportWindowLevel(value: boolean): void;
      constructor();
      frameIndex: number;
      imageUri: string;
      imageName: string;
      mimeType: string;
      fullSize: lt.LeadSizeD;
      fullDpi: lt.LeadSizeD;
      resolutions: lt.LeadSizeD[];
      tileSize: lt.LeadSizeD;
      supportWindowLevel: boolean;
   }

   enum ProjectionOrientationType {
      none = 0,
      faceToFace = 1,
      faceToBack = 2
   }

   class FramePosition {
      constructor();
      orientation: number[];
      position: number[];
   }

   class StackSynchronization {
      static alignFrames(frameToAlign: Frame, cell: Cell): number;
      static findBestAlignment(frameToAlign: FramePosition, frames: FramePosition[]): number;
      constructor();
   }

   class LeadLine {
      constructor(x1: number, y1: number, x2: number, y2: number);
      point1: lt.LeadPointD;
      point2: lt.LeadPointD;
   }

   class ReferenceLine {
      static rotate(line: LeadLine, angle: number, width: number, height: number): LeadLine;
      static flip(line: LeadLine, width: number, height: number): LeadLine;
      static reverse(line: LeadLine, width: number, height: number): LeadLine;
      static draw(context: CanvasRenderingContext2D, displayRect: lt.LeadRectD, imageRect: lt.LeadRectD, line: LeadLine, lineNumber: string): boolean;
      static find(orientation1: number[], imageposition1: number[], voxelspacing1: lt.LeadPointD, width1: number, height1: number, orientation2: number[], imageposition2: number[], voxelspacing2: lt.LeadPointD, width2: number, height2: number): LeadLine;
      constructor();
   }

   class WindowLevelData {
      get_interactiveMode(): WindowLevelInteractiveMode;
      get_renderer(): DICOMImageInformationRenderer;
      set_renderer(value: DICOMImageInformationRenderer): void;
      get_image(): HTMLImageElement;
      set_image(value: HTMLImageElement): void;
      get_canvas(): HTMLCanvasElement;
      set_canvas(value: HTMLCanvasElement): void;
      get_dragDeltaSensitivity(): number;
      set_dragDeltaSensitivity(value: number): void;
      get_windowLevelSensitivity(): number;
      get_enableWindowLevelSensitivity(): boolean;
      set_enableWindowLevelSensitivity(value: boolean): void;
      constructor();
      interactiveMode: WindowLevelInteractiveMode;
      renderer: DICOMImageInformationRenderer;
      image: HTMLImageElement;
      canvas: HTMLCanvasElement;
      dragDeltaSensitivity: number;
      windowLevelSensitivity: number; // read-only
      enableWindowLevelSensitivity: boolean;
   }

   class WindowLevelInteractiveMode extends lt.Controls.ImageViewerInteractiveMode {
      get_id(): number;
      get_data(): WindowLevelData;
      set_data(value: WindowLevelData): void;
      get_name(): string;
      reset(): void;
      start(viewer: lt.Controls.ImageViewer): void;
      stop(viewer: lt.Controls.ImageViewer): void;
      get_mrtiEnabled(): boolean;
      set_mrtiEnabled(value: boolean): void;
      add_render(value: lt.LeadEventHandler): void;
      remove_render(value: lt.LeadEventHandler): void;
      add_workDelta(value: lt.Controls.InteractiveDragDeltaEventHandler): void;
      remove_workDelta(value: lt.Controls.InteractiveDragDeltaEventHandler): void;
      add_renderCompleted(value: lt.LeadEventHandler): void;
      remove_renderCompleted(value: lt.LeadEventHandler): void;
      dispose(): void;
      constructor();
      id: number; // read-only
      data: WindowLevelData;
      name: string; // read-only
      mrtiEnabled: boolean;
      render: lt.LeadEventType; // read-only
      workDelta: lt.Controls.InteractiveDragDeltaEventType; // read-only
      renderCompleted: lt.LeadEventType; // read-only
   }
}
