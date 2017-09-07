module HTML5Demos {

   export class DemoInteractiveMode {
      public interactiveMode: lt.Controls.ImageViewerInteractiveMode;
      public resetToPanZoom: boolean;

      constructor(mode: lt.Controls.ImageViewerInteractiveMode, reset: boolean) {
         this.resetToPanZoom = reset;
         this.interactiveMode = mode;
      }
   }

   // Return value from Service Raster/Info
   export class ImageInfo {
      public uri: string;
      public formatId: number;
      public formatName: string;
      public mimeType: string;
      public browserCompatible: boolean;
      public width: number;
      public height: number;
      public bitsPerPixel: number;
      public bytesPerLine: number;
      public sizeDisk: number;
      public sizeMemory: number;
      public compression: string;
      public viewPerspective: string;
      public order: string;
      public colorSpace: string;
      public pageNumber: number;
      public totalPages: number;
      public hasResolution: boolean;
      public xResolution: number;
      public yResolution: number;
      public isRotated: boolean;
      public isSigned: boolean;
      public hasAlpha: boolean;
   }

   export class DemoImage {
      public url: string;
      public useDpi: boolean;
      public dpi: number;
      public isLoadedImageBitonal: boolean;

      constructor(url: string, useDpi: boolean, dpi: number, isLoadedImageBitonal: boolean) {
         this.url = url;
         this.useDpi = useDpi;
         this.dpi = dpi;
         this.isLoadedImageBitonal = isLoadedImageBitonal;
      }
   }

   export class CommonApp {
      // Demo viewer
      private _viewer: lt.Controls.ImageViewer;
      get imageViewerControl(): lt.Controls.ImageViewer {
         return this._viewer;
      }

      private _demoImages: DemoImage[];
      // DPI to set 
      private _imageDPI: number;
      // Indicate whether the image is bitonal (black/white) and it is faster to use scale to gray interpolation than resample.
      private _isLoadedImageBitonal: boolean;
      get isLoadedImageBitonal(): boolean {
         return this._isLoadedImageBitonal;
      }

      private _useService: boolean = true;

      private _tempUrl: string;

      private _currentImageUrl: string;
      get currentImageUrl(): string {
         return this._currentImageUrl;
      }
      private _imageLoadedFromService: boolean = false;
      get imageLoadedFromService(): boolean {
         return this._imageLoadedFromService;
      }

      // Array of interactive modes that used by the demo
      private _interactiveModes: DemoInteractiveMode[] = [];

      public get interactiveModes(): DemoInteractiveMode[] {
         return this._interactiveModes;
      }

      // Common dialogs 
      private _openFileDlg: Dialogs.OpenFileDlg;
      private _aboutDlg: Dialogs.AboutDlg;
      private _loadingDlg: Dialogs.LoadingDlg;
      // Common UI elements 
      public commonUI = {
         openBtn: "#openFile",
         /* Used when resetToPanZoom in DemoInteractiveMode class is set to true */
         panZoomBtn: "#panZoom",
         interactiveModesBtns: ".interactiveModesBtns", /* All interactive modes buttons */
         fitBtn: "#fit", actualSizeBtn: "#actualSize",
         zoomInBtn: "#zoomIn", zoomOutBtn: "#zoomOut",
         aboutBtn: "#about"
      };

      constructor() {
         window.onresize = ((e: Event) => this.onResize(e));
         window.onunload = (e: Event) => {
            this.dispose();
            this.unload();
         };
      }

      private onResize(e: Event) {
         // For demos that have a sidebar
         Utils.DemoHelper.resetSidebars();
      }

      private dispose(): void {
         if (this._viewer)
            this._viewer.dispose();
      }

      // Abstract methods
      public get demoImages(): DemoImage[] { return; }
      public get customDemoInteractiveMode(): DemoInteractiveMode { return; }
      public get demoName(): string { return; }
      public viewer_ItemChangedCustomAction(sender: any, e: lt.Controls.ImageViewerItemChangedEventArgs): void { /* Do nothing */ }
      public unload(): void { /* Do nothing */ }

      public static runDemo(page: CommonApp): void {
         if (!lt.LTHelper.supportsCanvas || (lt.LTHelper.browser == lt.LTBrowser.internetExplorer && lt.LTHelper.device != lt.LTDevice.desktop && lt.LTHelper.version <= 9)) {
            // No support for the canvas element, redirect to the error page
            window.location.replace("../Resources/NoHTML5Support.html");
            return;
         }
         // Run the demo
         page.run();
      }

      private verifyServicePromise: JQueryXHR = null;

      // Virtual method
      public run(): void {
         // Set demo images
         this._demoImages = this.demoImages;

         // Initial setup
         this.initUI();
         this.createViewer();
         this.initInteractiveModes();

         // Sometimes service connection values need to be specified outside of the client side code, like in a configuration file.
         // Here we show how that approach is used, and provide manual setting of the properties as a backup.
         this.verifyService();
      }

      public verifyService(): void {
         this.beginOperation("Verifying Service Connection...");
         $.getJSON("../serviceConfig.json")
            .done((json: {}) => {

               // If other than "LEADTOOLS", specify where your license directory is.
               lt.LTHelper.licenseDirectory = json["licenseDirectory"];

               this.initService(json);
            })
            .fail(() => {

               // If other than "LEADTOOLS", specify where your license directory is.
               lt.LTHelper.licenseDirectory = "../LEADTOOLS";

               // The json configuration file wasn't found. Just manually set.
               this.initService(null);
            })
            .always(() => {
               // Regardless of what happens, this runs after.
               this.verifyServicePromise = Utils.ServiceHelper.verifyService();
               this.verifyServicePromise.done((response: Utils.PingResponse) => {
                  // Check if the LEADTOOLS license on the server is usable, otherwise, show a warning
                  if (!response.isLicenseChecked) {
                     // The server has failed to check the license, could be an invalid license or one that does not exist
                     window.alert("Warning!\n\nThe LEADTOOLS License used in the service could not be found. This demo may not function as expected.");
                  } else if (response.isLicenseExpired) {
                     // The server has detected that the license used has expired
                     window.alert("Warning!\n\nThe LEADTOOLS Kernel has expired. This demo may not function as expected.");
                  }
                  // If the kernel is not release, log it (for debugging)
                  if (response.kernelType != null && response.kernelType != "Release") {
                     console.log("Server LEADTOOLS Kernel type: " + response.kernelType);
                  }
                  // On Success
                  if (this.demoImages != null && this.demoImages.length > 0)
                     this.selectNewImage(0);
               })
               this.verifyServicePromise.fail((xhr: JQueryXHR, statusText: string, errorThrown: string) => {
                  // On Failure
                  this.endOperation(false);
                  window.alert("The LEADTOOLS Service could not be reached - Check your service path.");
               });
            });
      }

      private initService(json?: {}): void {
         Utils.ServiceHelper.init();
         // If you have a different route to verify service, change it here
         //Utils.ServiceHelper.serviceTestResource = "Path/To/Ping"

         Utils.ServiceHelper.serviceHost = (json && json["serviceHost"]) ? json["serviceHost"] : null;
         Utils.ServiceHelper.servicePath = (json && json["servicePath"]) ? json["servicePath"] : null;

         // Change the relative path from our client side (/webapp) to service routing (/api)
         Utils.ServiceHelper.serviceApiPath = (json && json["serviceApiPath"]) ? json["serviceApiPath"] : "../../api";
      }

      private initUI(): void {
         // Bind events to UI elements
         $(this.commonUI.openBtn).bind("click", $.proxy(this.openBtn_Click, this));
         $(this.commonUI.interactiveModesBtns).bind("click", $.proxy(this.interactiveModesBtns_BtnClicked, this));
         $(this.commonUI.fitBtn).bind("click", $.proxy(this.fitBtn_Click, this));
         $(this.commonUI.actualSizeBtn).bind("click", $.proxy(this.actualSizeBtn_Click, this));
         $(this.commonUI.zoomInBtn).bind("click", $.proxy(this.zoomInBtn_Click, this));
         $(this.commonUI.zoomOutBtn).bind("click", $.proxy(this.zoomOutBtn_Click, this));
         $(this.commonUI.aboutBtn).bind("click", $.proxy(this.aboutBtn_Click, this));

         if (Utils.DemoHelper.isTouchDevice()) {
            $(".headerToolbarDiv").css("overflow", "hidden");
            var headerToolbarDiv: HTMLDivElement = <HTMLDivElement>document.getElementsByClassName('headerToolbarDiv').item(0);
            //  Handle touch scroll 
            Utils.DemoHelper.touchScroll(headerToolbarDiv);
         }

         // Initiate demo dialogs
         this.InitDialogs();
      }

      private InitDialogs(): void {
         // Init open file dialog
         this._openFileDlg = new Dialogs.OpenFileDlg();
         // set open file dialog events handlers
         this._openFileDlg.goClick = ((url: string) => this.openFileDlg_GoClick(url));
         this._openFileDlg.fileSelect = ((selectedIndex: number) => this.openFileDlg_FileSelect(selectedIndex));

         // Init loading dialog 
         this._loadingDlg = new Dialogs.LoadingDlg();

         // Init about dialog
         this._aboutDlg = new Dialogs.AboutDlg(this.demoName);
      }

      private createViewer(): void {
         // For interpolation (scaleToGray and resample)
         lt.Controls.ImageViewer.imageProcessingLibrariesPath = "../Common/Libs";

         var createOptions: lt.Controls.ImageViewerCreateOptions = new lt.Controls.ImageViewerCreateOptions(<HTMLDivElement>document.getElementById("imageViewerDiv"));
         this._viewer = new lt.Controls.ImageViewer(createOptions);
         this._viewer.autoCreateCanvas = true;
         this._viewer.itemError.add((sender: any, e: lt.Controls.ImageViewerItemErrorEventArgs) => this.viewer_ItemError(sender, e));
         this._viewer.itemChanged.add((sender: any, e: lt.Controls.ImageViewerItemChangedEventArgs) => this.viewer_ItemChanged(sender, e));
         this._viewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
         this._viewer.viewVerticalAlignment = lt.Controls.ControlAlignment.center;
         this._viewer.autoResetOptions = lt.Controls.ImageViewerAutoResetOptions.all;

         if (lt.LTHelper.OS == lt.LTOS.android)
            this._viewer.enableRequestAnimationFrame = true;

         if (lt.LTHelper.msPointerEnabled && !lt.LTHelper.supportsMouse)
            this._viewer.scrollMode = lt.Controls.ControlScrollMode.hidden;
      }

      // Viewer on itemError handler
      private viewer_ItemError(sender: any, e: lt.Controls.ImageViewerItemErrorEventArgs): void {
         window.alert("Cannot open: " + this._tempUrl);
         this.endOperation(false);
      }

      // Viewer on itemChanged handler
      private viewer_ItemChanged(sender: any, e: lt.Controls.ImageViewerItemChangedEventArgs): void {
         if (e.reason == lt.Controls.ImageViewerItemChangedReason.url) {
            this.endOperation(true);
            this.viewer_ItemChangedCustomAction(sender, e);
         }
      }

      private initInteractiveModes(): void {
         this._interactiveModes = [];

         // Add derived class specific (Add custom mode)
         var customMode: DemoInteractiveMode = this.customDemoInteractiveMode;
         if (customMode != null) {
            this._interactiveModes.push(customMode);
         }

         // Add pan zoom
         var panZoom: lt.Controls.ImageViewerPanZoomInteractiveMode = new lt.Controls.ImageViewerPanZoomInteractiveMode();
         panZoom.doubleTapSizeMode = lt.Controls.ControlSizeMode.none;
         var inertiaScrollOptions = panZoom.inertiaScrollOptions;
         inertiaScrollOptions.isEnabled = true;
         panZoom.inertiaScrollOptions = inertiaScrollOptions;
         this._interactiveModes.push(new DemoInteractiveMode(
            panZoom,
            false));

         // Add magnify glass, only if not in elements mode
         if (!this._viewer.useElements) {
            this._interactiveModes.push(new DemoInteractiveMode(
               new lt.Controls.ImageViewerMagnifyGlassInteractiveMode(),
               false));
         }

         var workCompleted = () => {
            // Unmarke all interactive modes buttons
            Utils.DemoHelper.checked($(this.commonUI.interactiveModesBtns), false);
            // Mark the pan/zoom button
            Utils.DemoHelper.checked($(this.commonUI.panZoomBtn), true);
            // Go back to Pan and Zoom mode
            this._viewer.defaultInteractiveMode = this._interactiveModes[parseInt($(this.commonUI.panZoomBtn).val())].interactiveMode;
         };

         for (var i: number = 0; i < this._interactiveModes.length; i++) {
            var mode: DemoInteractiveMode = this._interactiveModes[i];
            var interactiveMode: lt.Controls.ImageViewerInteractiveMode = mode.interactiveMode;
            if (interactiveMode != null) {
               interactiveMode.idleCursor = "crosshair";

               if (interactiveMode.name == "PanZoom")
                  interactiveMode.workingCursor = "move";
               else
                  interactiveMode.workingCursor = "crosshair";

               interactiveMode.workOnBounds = true;

               if (mode.resetToPanZoom)
                  interactiveMode.add_workCompleted(workCompleted);
            }
         }

         this._viewer.defaultInteractiveMode = this._interactiveModes[0].interactiveMode;
      }

      // Virtual
      public destroyViewer(): void {
         // removes viewer, for purpose of reconstructing in opposite mode (Elements / Non-Elements)
         if (!this._viewer)
            throw "viewer does not exist - viewer must be created first";
         this._viewer.dispose();
         this._viewer = null;
      }

      // Virtual
      public recreateViewer(elementsMode: boolean): void {
         if (this._viewer)
            throw "viewer exists - must dispose of old viewer first";

         if (elementsMode) {
            var createOptions = new lt.Controls.ImageViewerCreateOptions(<HTMLDivElement>document.getElementById("imageViewerDiv"));
            createOptions.useElements = true;
            this._viewer = new lt.Controls.ImageViewer(createOptions);
            this._viewer.itemError.add((sender: any, e: lt.Controls.ImageViewerItemErrorEventArgs) => this.viewer_ItemError(sender, e));
            this._viewer.itemChanged.add((sender: any, e: lt.Controls.ImageViewerItemChangedEventArgs) => this.viewer_ItemChanged(sender, e));
            this._viewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
            this._viewer.viewVerticalAlignment = lt.Controls.ControlAlignment.center;
            this._viewer.autoResetOptions = lt.Controls.ImageViewerAutoResetOptions.all;

            if (lt.LTHelper.msPointerEnabled && !lt.LTHelper.supportsMouse)
               this._viewer.scrollMode = lt.Controls.ControlScrollMode.hidden;
         }
         else {
            this.createViewer();
         }
         this.initInteractiveModes();
      }

      // Virtual
      public beforeSelectNewImage(index: number): boolean {
         return true;
      }

      private selectNewImage(index: number): void {
         this._imageLoadedFromService = true;
         var demoImage: DemoImage = this._demoImages[index];
         var newImageUrl: string = "Resources/Images/" + demoImage.url;
         var location: string = window.location.href;
         // Remove the application name
         var index = location.lastIndexOf("/");
         this._tempUrl = "";
         this._tempUrl = location.substring(0, index + 1) + newImageUrl;
         // If it is the same image, no need to wait for the load
         if (newImageUrl == this._viewer.imageUrl)
            return;

         this.beginOperation("Please Wait... Loading New Image");

         // Set the DPI when the image has finished loading.
         if (demoImage.useDpi)
            this._imageDPI = demoImage.dpi;
         else
            this._imageDPI = 0;

         this._isLoadedImageBitonal = demoImage.isLoadedImageBitonal;
         this._viewer.beginUpdate();
         this._viewer.imageUrl = newImageUrl;
         this._viewer.endUpdate();
      }

      public loadImageFromURL(url: string, useService: boolean): void {
         this.beginOperation("Please Wait... Loading New Image");

         // Reset DPI
         this._imageDPI = 0;
         // Disable 1BPP
         this._isLoadedImageBitonal = false;

         // keep track of how image is loaded
         this._imageLoadedFromService = useService;

         if (useService) {
            // Things can be changed beforehand with 
            //Utils.ServiceHelper.serviceName = "[Service.name.here]";
            var rest: string = [Utils.ServiceHelper.serviceUri, "Raster", "Info"].join("/");
            var params = {
               uri: url
            };

            $.get(rest, params)
               .done((info: ImageInfo) => {
                  if (info.formatId == 0) {
                     // Unrecognized image format
                     window.alert("Unrecognized image file format.");
                     return;
                  }

                  // Save the DPI
                  this._imageDPI = info.xResolution;
                  // 1BPP (bitonal B/W image)
                  this._isLoadedImageBitonal = (info.bitsPerPixel == 1);

                  var width: number = 0;
                  var height: number = 0;

                  if (lt.LTHelper.device == lt.LTDevice.mobile || lt.LTHelper.device == lt.LTDevice.tablet) {
                     // To save bandwidth and memory on mobile devices, and to get around the
                     // iOS limitation of loading large image sizes, limit the maximum image size
                     // to 1000 by 1000 pixels
                     width = 1000;
                     height = 1000;
                     this._imageDPI = 0; // Since we resized the image, the DPI is not true anymore
                  }

                  // Now load it
                  var imageRest = [Utils.ServiceHelper.serviceUri, "Raster", "Load"].join("/");
                  var imageParams = {
                     uri: url
                  };
                  if (width != 0 && height != 0) {
                     imageParams["width"] = width;
                     imageParams["height"] = height;
                  }
                  imageRest = Utils.ServiceHelper.addParamsToUrl(imageRest, imageParams);
                  this._tempUrl = url;
                  this._viewer.imageUrl = imageRest;
               })
               .fail((xhr: JQueryXHR, statusText: string, errorThrown: string) => {
                  this.endOperation(false);
                  Utils.DemoHelper.showRequestError(xhr, statusText, errorThrown);
               })
         }
         else {
            // Load it directly
            this._tempUrl = url;
            this._viewer.imageUrl = url;
         }
      }

      public beginOperation(processText: string): void {
         this._loadingDlg.show(processText);
      }

      public endOperation(imageChanged: boolean): void {
         this._loadingDlg.hide();

         if (imageChanged) {
            this._currentImageUrl = this._tempUrl;
            this._viewer.beginUpdate();
            var resolution: lt.LeadSizeD = lt.LeadSizeD.create(this._imageDPI == 0 ? 96 : this._imageDPI, this._imageDPI == 0 ? 96 : this._imageDPI);
            this._viewer.imageResolution = resolution;
            this._viewer.useDpi = (this._imageDPI != 0);

            if (lt.LTHelper.device == lt.LTDevice.mobile || lt.LTHelper.device == lt.LTDevice.tablet) {
               // Default on tablet and mobile is to fit width
               this._viewer.zoom(lt.Controls.ControlSizeMode.fitWidth, 1, this._viewer.defaultZoomOrigin);
            }

            this._viewer.endUpdate();
         }

         this._tempUrl = null;
      }

      // Common UI events handlers
      private openBtn_Click(e: JQueryEventObject): void {
         this._openFileDlg.show();
      }

      // Handle all interactive modes buttons , these buttons are behave as radio buttons group.
      private interactiveModesBtns_BtnClicked(e: JQueryEventObject): void {
         // Unmarke all interactive modes buttons
         Utils.DemoHelper.checked($(this.commonUI.interactiveModesBtns), false);
         // Mark the selected one
         Utils.DemoHelper.checked($(e.currentTarget), true);

         var modeIndex = parseInt($(e.currentTarget).val());

         this._viewer.defaultInteractiveMode = this._interactiveModes[modeIndex].interactiveMode;
      }

      private fitBtn_Click(e: JQueryEventObject): void {
         this._viewer.zoom(lt.Controls.ControlSizeMode.fitAlways, 1.0, this._viewer.defaultZoomOrigin);
         this._viewer.scrollOffset = lt.LeadPointD.create(0, 0);
      }

      private actualSizeBtn_Click(e: JQueryEventObject): void {
         this._viewer.zoom(lt.Controls.ControlSizeMode.actualSize, 1.0, this._viewer.defaultZoomOrigin);
         this._viewer.scrollOffset = lt.LeadPointD.create(0, 0);
      }

      private zoomInBtn_Click(e: JQueryEventObject): void {
         var newScaleFactor = this._viewer.scaleFactor + 0.1;
         if (newScaleFactor <= this._viewer.maximumScaleFactor) {
            this._viewer.zoom(lt.Controls.ControlSizeMode.none, newScaleFactor, this._viewer.defaultZoomOrigin);
         }
      }

      private zoomOutBtn_Click(e: JQueryEventObject): void {
         var newScaleFactor = this._viewer.scaleFactor - 0.1;
         if (newScaleFactor >= this._viewer.minimumScaleFactor) {
            this._viewer.zoom(lt.Controls.ControlSizeMode.none, newScaleFactor, this._viewer.defaultZoomOrigin);
         }
      }

      private aboutBtn_Click(e: JQueryEventObject): void {
         this._aboutDlg.show();
      }

      // Open file dialog events handlars
      private openFileDlg_FileSelect(selectedIndex: number): void {
         if (this.beforeSelectNewImage(selectedIndex)) {
            this.selectNewImage(selectedIndex);
         }
      }

      private openFileDlg_GoClick(url: string): void {
         if (url != null && url.length > 0) {
            this.loadImageFromURL(url, this._useService);
         }
      }
   }
}
