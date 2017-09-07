var HTML5Demos;
(function (HTML5Demos) {
    var DemoInteractiveMode = (function () {
        function DemoInteractiveMode(mode, reset) {
            this.resetToPanZoom = reset;
            this.interactiveMode = mode;
        }
        return DemoInteractiveMode;
    }());
    HTML5Demos.DemoInteractiveMode = DemoInteractiveMode;
    // Return value from Service Raster/Info
    var ImageInfo = (function () {
        function ImageInfo() {
        }
        return ImageInfo;
    }());
    HTML5Demos.ImageInfo = ImageInfo;
    var DemoImage = (function () {
        function DemoImage(url, useDpi, dpi, isLoadedImageBitonal) {
            this.url = url;
            this.useDpi = useDpi;
            this.dpi = dpi;
            this.isLoadedImageBitonal = isLoadedImageBitonal;
        }
        return DemoImage;
    }());
    HTML5Demos.DemoImage = DemoImage;
    var CommonApp = (function () {
        function CommonApp() {
            var _this = this;
            this._useService = true;
            this._imageLoadedFromService = false;
            // Array of interactive modes that used by the demo
            this._interactiveModes = [];
            // Common UI elements 
            this.commonUI = {
                openBtn: "#openFile",
                /* Used when resetToPanZoom in DemoInteractiveMode class is set to true */
                panZoomBtn: "#panZoom",
                interactiveModesBtns: ".interactiveModesBtns",
                fitBtn: "#fit", actualSizeBtn: "#actualSize",
                zoomInBtn: "#zoomIn", zoomOutBtn: "#zoomOut",
                aboutBtn: "#about"
            };
            this.verifyServicePromise = null;
            window.onresize = (function (e) { return _this.onResize(e); });
            window.onunload = function (e) {
                _this.dispose();
                _this.unload();
            };
        }
        Object.defineProperty(CommonApp.prototype, "imageViewerControl", {
            get: function () {
                return this._viewer;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CommonApp.prototype, "isLoadedImageBitonal", {
            get: function () {
                return this._isLoadedImageBitonal;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CommonApp.prototype, "currentImageUrl", {
            get: function () {
                return this._currentImageUrl;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CommonApp.prototype, "imageLoadedFromService", {
            get: function () {
                return this._imageLoadedFromService;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CommonApp.prototype, "interactiveModes", {
            get: function () {
                return this._interactiveModes;
            },
            enumerable: true,
            configurable: true
        });
        CommonApp.prototype.onResize = function (e) {
            // For demos that have a sidebar
            HTML5Demos.Utils.DemoHelper.resetSidebars();
        };
        CommonApp.prototype.dispose = function () {
            if (this._viewer)
                this._viewer.dispose();
        };
        Object.defineProperty(CommonApp.prototype, "demoImages", {
            // Abstract methods
            get: function () { return; },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CommonApp.prototype, "customDemoInteractiveMode", {
            get: function () { return; },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CommonApp.prototype, "demoName", {
            get: function () { return; },
            enumerable: true,
            configurable: true
        });
        CommonApp.prototype.viewer_ItemChangedCustomAction = function (sender, e) { };
        CommonApp.prototype.unload = function () { };
        CommonApp.runDemo = function (page) {
            if (!lt.LTHelper.supportsCanvas || (lt.LTHelper.browser == lt.LTBrowser.internetExplorer && lt.LTHelper.device != lt.LTDevice.desktop && lt.LTHelper.version <= 9)) {
                // No support for the canvas element, redirect to the error page
                window.location.replace("../Resources/NoHTML5Support.html");
                return;
            }
            // Run the demo
            page.run();
        };
        // Virtual method
        CommonApp.prototype.run = function () {
            // Set demo images
            this._demoImages = this.demoImages;
            // Initial setup
            this.initUI();
            this.createViewer();
            this.initInteractiveModes();
            // Sometimes service connection values need to be specified outside of the client side code, like in a configuration file.
            // Here we show how that approach is used, and provide manual setting of the properties as a backup.
            this.verifyService();
        };
        CommonApp.prototype.verifyService = function () {
            var _this = this;
            this.beginOperation("Verifying Service Connection...");
            $.getJSON("../serviceConfig.json")
                .done(function (json) {
                // If other than "LEADTOOLS", specify where your license directory is.
                lt.LTHelper.licenseDirectory = json["licenseDirectory"];
                _this.initService(json);
            })
                .fail(function () {
                // If other than "LEADTOOLS", specify where your license directory is.
                lt.LTHelper.licenseDirectory = "../LEADTOOLS";
                // The json configuration file wasn't found. Just manually set.
                _this.initService(null);
            })
                .always(function () {
                // Regardless of what happens, this runs after.
                _this.verifyServicePromise = HTML5Demos.Utils.ServiceHelper.verifyService();
                _this.verifyServicePromise.done(function (response) {
                    // Check if the LEADTOOLS license on the server is usable, otherwise, show a warning
                    if (!response.isLicenseChecked) {
                        // The server has failed to check the license, could be an invalid license or one that does not exist
                        window.alert("Warning!\n\nThe LEADTOOLS License used in the service could not be found. This demo may not function as expected.");
                    }
                    else if (response.isLicenseExpired) {
                        // The server has detected that the license used has expired
                        window.alert("Warning!\n\nThe LEADTOOLS Kernel has expired. This demo may not function as expected.");
                    }
                    // If the kernel is not release, log it (for debugging)
                    if (response.kernelType != null && response.kernelType != "Release") {
                        console.log("Server LEADTOOLS Kernel type: " + response.kernelType);
                    }
                    // On Success
                    if (_this.demoImages != null && _this.demoImages.length > 0)
                        _this.selectNewImage(0);
                });
                _this.verifyServicePromise.fail(function (xhr, statusText, errorThrown) {
                    // On Failure
                    _this.endOperation(false);
                    window.alert("The LEADTOOLS Service could not be reached - Check your service path.");
                });
            });
        };
        CommonApp.prototype.initService = function (json) {
            HTML5Demos.Utils.ServiceHelper.init();
            // If you have a different route to verify service, change it here
            //Utils.ServiceHelper.serviceTestResource = "Path/To/Ping"
            HTML5Demos.Utils.ServiceHelper.serviceHost = (json && json["serviceHost"]) ? json["serviceHost"] : null;
            HTML5Demos.Utils.ServiceHelper.servicePath = (json && json["servicePath"]) ? json["servicePath"] : null;
            // Change the relative path from our client side (/webapp) to service routing (/api)
            HTML5Demos.Utils.ServiceHelper.serviceApiPath = (json && json["serviceApiPath"]) ? json["serviceApiPath"] : "../../api";
        };
        CommonApp.prototype.initUI = function () {
            // Bind events to UI elements
            $(this.commonUI.openBtn).bind("click", $.proxy(this.openBtn_Click, this));
            $(this.commonUI.interactiveModesBtns).bind("click", $.proxy(this.interactiveModesBtns_BtnClicked, this));
            $(this.commonUI.fitBtn).bind("click", $.proxy(this.fitBtn_Click, this));
            $(this.commonUI.actualSizeBtn).bind("click", $.proxy(this.actualSizeBtn_Click, this));
            $(this.commonUI.zoomInBtn).bind("click", $.proxy(this.zoomInBtn_Click, this));
            $(this.commonUI.zoomOutBtn).bind("click", $.proxy(this.zoomOutBtn_Click, this));
            $(this.commonUI.aboutBtn).bind("click", $.proxy(this.aboutBtn_Click, this));
            if (HTML5Demos.Utils.DemoHelper.isTouchDevice()) {
                $(".headerToolbarDiv").css("overflow", "hidden");
                var headerToolbarDiv = document.getElementsByClassName('headerToolbarDiv').item(0);
                //  Handle touch scroll 
                HTML5Demos.Utils.DemoHelper.touchScroll(headerToolbarDiv);
            }
            // Initiate demo dialogs
            this.InitDialogs();
        };
        CommonApp.prototype.InitDialogs = function () {
            var _this = this;
            // Init open file dialog
            this._openFileDlg = new HTML5Demos.Dialogs.OpenFileDlg();
            // set open file dialog events handlers
            this._openFileDlg.goClick = (function (url) { return _this.openFileDlg_GoClick(url); });
            this._openFileDlg.fileSelect = (function (selectedIndex) { return _this.openFileDlg_FileSelect(selectedIndex); });
            // Init loading dialog 
            this._loadingDlg = new HTML5Demos.Dialogs.LoadingDlg();
            // Init about dialog
            this._aboutDlg = new HTML5Demos.Dialogs.AboutDlg(this.demoName);
        };
        CommonApp.prototype.createViewer = function () {
            var _this = this;
            // For interpolation (scaleToGray and resample)
            lt.Controls.ImageViewer.imageProcessingLibrariesPath = "../Common/Libs";
            var createOptions = new lt.Controls.ImageViewerCreateOptions(document.getElementById("imageViewerDiv"));
            this._viewer = new lt.Controls.ImageViewer(createOptions);
            this._viewer.autoCreateCanvas = true;
            this._viewer.itemError.add(function (sender, e) { return _this.viewer_ItemError(sender, e); });
            this._viewer.itemChanged.add(function (sender, e) { return _this.viewer_ItemChanged(sender, e); });
            this._viewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
            this._viewer.viewVerticalAlignment = lt.Controls.ControlAlignment.center;
            this._viewer.autoResetOptions = lt.Controls.ImageViewerAutoResetOptions.all;
            if (lt.LTHelper.OS == lt.LTOS.android)
                this._viewer.enableRequestAnimationFrame = true;
            if (lt.LTHelper.msPointerEnabled && !lt.LTHelper.supportsMouse)
                this._viewer.scrollMode = lt.Controls.ControlScrollMode.hidden;
        };
        // Viewer on itemError handler
        CommonApp.prototype.viewer_ItemError = function (sender, e) {
            window.alert("Cannot open: " + this._tempUrl);
            this.endOperation(false);
        };
        // Viewer on itemChanged handler
        CommonApp.prototype.viewer_ItemChanged = function (sender, e) {
            if (e.reason == lt.Controls.ImageViewerItemChangedReason.url) {
                this.endOperation(true);
                this.viewer_ItemChangedCustomAction(sender, e);
            }
        };
        CommonApp.prototype.initInteractiveModes = function () {
            var _this = this;
            this._interactiveModes = [];
            // Add derived class specific (Add custom mode)
            var customMode = this.customDemoInteractiveMode;
            if (customMode != null) {
                this._interactiveModes.push(customMode);
            }
            // Add pan zoom
            var panZoom = new lt.Controls.ImageViewerPanZoomInteractiveMode();
            panZoom.doubleTapSizeMode = lt.Controls.ControlSizeMode.none;
            var inertiaScrollOptions = panZoom.inertiaScrollOptions;
            inertiaScrollOptions.isEnabled = true;
            panZoom.inertiaScrollOptions = inertiaScrollOptions;
            this._interactiveModes.push(new DemoInteractiveMode(panZoom, false));
            // Add magnify glass, only if not in elements mode
            if (!this._viewer.useElements) {
                this._interactiveModes.push(new DemoInteractiveMode(new lt.Controls.ImageViewerMagnifyGlassInteractiveMode(), false));
            }
            var workCompleted = function () {
                // Unmarke all interactive modes buttons
                HTML5Demos.Utils.DemoHelper.checked($(_this.commonUI.interactiveModesBtns), false);
                // Mark the pan/zoom button
                HTML5Demos.Utils.DemoHelper.checked($(_this.commonUI.panZoomBtn), true);
                // Go back to Pan and Zoom mode
                _this._viewer.defaultInteractiveMode = _this._interactiveModes[parseInt($(_this.commonUI.panZoomBtn).val())].interactiveMode;
            };
            for (var i = 0; i < this._interactiveModes.length; i++) {
                var mode = this._interactiveModes[i];
                var interactiveMode = mode.interactiveMode;
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
        };
        // Virtual
        CommonApp.prototype.destroyViewer = function () {
            // removes viewer, for purpose of reconstructing in opposite mode (Elements / Non-Elements)
            if (!this._viewer)
                throw "viewer does not exist - viewer must be created first";
            this._viewer.dispose();
            this._viewer = null;
        };
        // Virtual
        CommonApp.prototype.recreateViewer = function (elementsMode) {
            var _this = this;
            if (this._viewer)
                throw "viewer exists - must dispose of old viewer first";
            if (elementsMode) {
                var createOptions = new lt.Controls.ImageViewerCreateOptions(document.getElementById("imageViewerDiv"));
                createOptions.useElements = true;
                this._viewer = new lt.Controls.ImageViewer(createOptions);
                this._viewer.itemError.add(function (sender, e) { return _this.viewer_ItemError(sender, e); });
                this._viewer.itemChanged.add(function (sender, e) { return _this.viewer_ItemChanged(sender, e); });
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
        };
        // Virtual
        CommonApp.prototype.beforeSelectNewImage = function (index) {
            return true;
        };
        CommonApp.prototype.selectNewImage = function (index) {
            this._imageLoadedFromService = true;
            var demoImage = this._demoImages[index];
            var newImageUrl = "Resources/Images/" + demoImage.url;
            var location = window.location.href;
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
        };
        CommonApp.prototype.loadImageFromURL = function (url, useService) {
            var _this = this;
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
                var rest = [HTML5Demos.Utils.ServiceHelper.serviceUri, "Raster", "Info"].join("/");
                var params = {
                    uri: url
                };
                $.get(rest, params)
                    .done(function (info) {
                    if (info.formatId == 0) {
                        // Unrecognized image format
                        window.alert("Unrecognized image file format.");
                        return;
                    }
                    // Save the DPI
                    _this._imageDPI = info.xResolution;
                    // 1BPP (bitonal B/W image)
                    _this._isLoadedImageBitonal = (info.bitsPerPixel == 1);
                    var width = 0;
                    var height = 0;
                    if (lt.LTHelper.device == lt.LTDevice.mobile || lt.LTHelper.device == lt.LTDevice.tablet) {
                        // To save bandwidth and memory on mobile devices, and to get around the
                        // iOS limitation of loading large image sizes, limit the maximum image size
                        // to 1000 by 1000 pixels
                        width = 1000;
                        height = 1000;
                        _this._imageDPI = 0; // Since we resized the image, the DPI is not true anymore
                    }
                    // Now load it
                    var imageRest = [HTML5Demos.Utils.ServiceHelper.serviceUri, "Raster", "Load"].join("/");
                    var imageParams = {
                        uri: url
                    };
                    if (width != 0 && height != 0) {
                        imageParams["width"] = width;
                        imageParams["height"] = height;
                    }
                    imageRest = HTML5Demos.Utils.ServiceHelper.addParamsToUrl(imageRest, imageParams);
                    _this._tempUrl = url;
                    _this._viewer.imageUrl = imageRest;
                })
                    .fail(function (xhr, statusText, errorThrown) {
                    _this.endOperation(false);
                    HTML5Demos.Utils.DemoHelper.showRequestError(xhr, statusText, errorThrown);
                });
            }
            else {
                // Load it directly
                this._tempUrl = url;
                this._viewer.imageUrl = url;
            }
        };
        CommonApp.prototype.beginOperation = function (processText) {
            this._loadingDlg.show(processText);
        };
        CommonApp.prototype.endOperation = function (imageChanged) {
            this._loadingDlg.hide();
            if (imageChanged) {
                this._currentImageUrl = this._tempUrl;
                this._viewer.beginUpdate();
                var resolution = lt.LeadSizeD.create(this._imageDPI == 0 ? 96 : this._imageDPI, this._imageDPI == 0 ? 96 : this._imageDPI);
                this._viewer.imageResolution = resolution;
                this._viewer.useDpi = (this._imageDPI != 0);
                if (lt.LTHelper.device == lt.LTDevice.mobile || lt.LTHelper.device == lt.LTDevice.tablet) {
                    // Default on tablet and mobile is to fit width
                    this._viewer.zoom(lt.Controls.ControlSizeMode.fitWidth, 1, this._viewer.defaultZoomOrigin);
                }
                this._viewer.endUpdate();
            }
            this._tempUrl = null;
        };
        // Common UI events handlers
        CommonApp.prototype.openBtn_Click = function (e) {
            this._openFileDlg.show();
        };
        // Handle all interactive modes buttons , these buttons are behave as radio buttons group.
        CommonApp.prototype.interactiveModesBtns_BtnClicked = function (e) {
            // Unmarke all interactive modes buttons
            HTML5Demos.Utils.DemoHelper.checked($(this.commonUI.interactiveModesBtns), false);
            // Mark the selected one
            HTML5Demos.Utils.DemoHelper.checked($(e.currentTarget), true);
            var modeIndex = parseInt($(e.currentTarget).val());
            this._viewer.defaultInteractiveMode = this._interactiveModes[modeIndex].interactiveMode;
        };
        CommonApp.prototype.fitBtn_Click = function (e) {
            this._viewer.zoom(lt.Controls.ControlSizeMode.fitAlways, 1.0, this._viewer.defaultZoomOrigin);
            this._viewer.scrollOffset = lt.LeadPointD.create(0, 0);
        };
        CommonApp.prototype.actualSizeBtn_Click = function (e) {
            this._viewer.zoom(lt.Controls.ControlSizeMode.actualSize, 1.0, this._viewer.defaultZoomOrigin);
            this._viewer.scrollOffset = lt.LeadPointD.create(0, 0);
        };
        CommonApp.prototype.zoomInBtn_Click = function (e) {
            var newScaleFactor = this._viewer.scaleFactor + 0.1;
            if (newScaleFactor <= this._viewer.maximumScaleFactor) {
                this._viewer.zoom(lt.Controls.ControlSizeMode.none, newScaleFactor, this._viewer.defaultZoomOrigin);
            }
        };
        CommonApp.prototype.zoomOutBtn_Click = function (e) {
            var newScaleFactor = this._viewer.scaleFactor - 0.1;
            if (newScaleFactor >= this._viewer.minimumScaleFactor) {
                this._viewer.zoom(lt.Controls.ControlSizeMode.none, newScaleFactor, this._viewer.defaultZoomOrigin);
            }
        };
        CommonApp.prototype.aboutBtn_Click = function (e) {
            this._aboutDlg.show();
        };
        // Open file dialog events handlars
        CommonApp.prototype.openFileDlg_FileSelect = function (selectedIndex) {
            if (this.beforeSelectNewImage(selectedIndex)) {
                this.selectNewImage(selectedIndex);
            }
        };
        CommonApp.prototype.openFileDlg_GoClick = function (url) {
            if (url != null && url.length > 0) {
                this.loadImageFromURL(url, this._useService);
            }
        };
        return CommonApp;
    }());
    HTML5Demos.CommonApp = CommonApp;
})(HTML5Demos || (HTML5Demos = {}));
//# sourceMappingURL=Common.js.map