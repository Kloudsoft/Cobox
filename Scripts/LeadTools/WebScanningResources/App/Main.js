var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var HTML5Demos;
(function (HTML5Demos) {
    var ScanningDemo;
    (function (ScanningDemo) {
        var ScanningDemoApp = (function (_super) {
            __extends(ScanningDemoApp, _super);
            function ScanningDemoApp() {
                _super.apply(this, arguments);
                this._totalPages = 0;
                this._totalScannedPages = 0;
                this._lastPageNumber = 0;
                // Demo UI
                this.demoUI = {
                    scanBtn: "#scan",
                    selectSourceBtn: "#selectSource",
                    printBtn: "#print",
                    editImageBtn: "#editImage",
                    previousPageBtn: "#previousPage",
                    nextPageBtn: "#nextPage"
                };
                this.thumbnails = {
                    container: "#thumbnailsDiv",
                    thumbnailsImgs: ".thumbnailImg"
                };
            }
            Object.defineProperty(ScanningDemoApp.prototype, "demoName", {
                // Override get method
                get: function () {
                    return "LEADTOOLS JavaScript Scanning Demo";
                },
                enumerable: true,
                configurable: true
            });
            // Override method 
            ScanningDemoApp.prototype.run = function () {
                // call base class run method
                // which calls verifyService()
                _super.prototype.run.call(this);
                if (lt.LTHelper.device == lt.LTDevice.desktop) {
                    if (lt.LTHelper.OS == lt.LTOS.windows || lt.LTHelper.OS == lt.LTOS.windows7 || lt.LTHelper.OS == lt.LTOS.windows8)
                        this._isWindowsEnvironment = true;
                }
                this.initDemoUI();
                this.initWebScanning();
                this._imageProcessingDlg.scanningService = this._scanningService;
            };
            // Override method 
            ScanningDemoApp.prototype.verifyService = function () { };
            // Override method 
            ScanningDemoApp.prototype.unload = function () {
                if (this._scanningService != null)
                    this._scanningService.stop(null, null);
            };
            ScanningDemoApp.prototype.initDemoUI = function () {
                var _this = this;
                // Bind events to UI elements
                $(this.demoUI.scanBtn).bind("click", $.proxy(this.scanBtn_Click, this));
                $(this.demoUI.selectSourceBtn).bind("click", $.proxy(this.selectSourceBtn_Click, this));
                $(this.demoUI.printBtn).bind("click", $.proxy(this.printBtn_Click, this));
                $(this.demoUI.editImageBtn).bind("click", $.proxy(this.editImageBtn_Click, this));
                $(this.demoUI.previousPageBtn).bind("click", $.proxy(this.previousPageBtn_Click, this));
                $(this.demoUI.nextPageBtn).bind("click", $.proxy(this.nextPageBtn_Click, this));
                // Init select source dialog
                this._selectSourceDlg = new HTML5Demos.Dialogs.SelectSourceDlg();
                this._selectSourceDlg.OkClick = (function (scanSource) { return _this.selectSourceDlg_OkClick(scanSource); });
                // Init error dialog
                this._errorDlg = new HTML5Demos.Dialogs.ErrorDlg(this._isWindowsEnvironment);
                this._imageProcessingDlg = new HTML5Demos.Dialogs.ImageProcessingDlg();
                this._imageProcessingDlg.applyClick = (function (args) { return _this.imageProcessingDlg_applyClick(args); });
            };
            ScanningDemoApp.prototype.initWebScanning = function () {
                var _this = this;
                var webScanningSupported = true;
                if (lt.LTHelper.device == lt.LTDevice.desktop) {
                    // Web scanning only supported on windows and linux
                    if (lt.LTHelper.OS == lt.LTOS.linux) {
                        //HolePunchRemove command is not supported in Linux
                        this._imageProcessingDlg.hideHolePunchRemoveTab();
                    }
                    else if (!this._isWindowsEnvironment) {
                        webScanningSupported = false;
                        alert("Web scanning is only supported on desktop Windows and Linux browsers.");
                    }
                    if (webScanningSupported) {
                        this.beginOperation("Starting Scanning Service");
                        if (this._isWindowsEnvironment && (lt.LTHelper.browser != lt.LTBrowser.edge && lt.LTHelper.browser != lt.LTBrowser.chrome)) {
                            var scanningPlugingHtml = "";
                            scanningPlugingHtml += "<embed id='webscanningObject' type='application/x-vnd.leadtools.webscanning' style='width: 0px; height: 0px'>";
                            scanningPlugingHtml += "<object id='webscanningObjectX' classid='clsid:8637A734-DE7A-4845-A7D1-71437EB6BB9D' style='width: 0px; height: 0px'></object>";
                            $("#scanningServiceContainer").html(scanningPlugingHtml);
                        }
                        this.startScanningService(function () {
                            if (_this._isWindowsEnvironment) {
                                _this._scanningService.isAvailable(function (available) {
                                    if (!available) {
                                        this.enableScanningButtons(false);
                                        alert("No TWAIN Data Source installed.");
                                    }
                                }, null);
                            }
                        });
                    }
                }
                else {
                    webScanningSupported = false;
                    window.alert("Web scanning is only supported on desktop Windows and Linux browsers.");
                }
                this.enableScanningControls(webScanningSupported);
            };
            ScanningDemoApp.prototype.startScanningService = function (onSuccess) {
                var _this = this;
                var started = false;
                var count = 20;
                var timeout = 1000;
                if (this._isWindowsEnvironment) {
                    var usePlugins = lt.LTHelper.browser != lt.LTBrowser.edge && lt.LTHelper.browser != lt.LTBrowser.chrome;
                    this._plugin = null;
                    this._scanningService = new lt.Scanning.JavaScript.TwainScanning("http://localhost/ScanService/");
                    var interval = setInterval(function () {
                        _this._scanningService.init(function () {
                            // Scanning service - init succeed
                            clearInterval(interval);
                            _this._scanningService.start(function () {
                                // Scanning service - start succeed
                                _this.enableScanningControls(true);
                                _this.endOperation(false);
                                if (onSuccess != null)
                                    onSuccess();
                            }, function () {
                                // Scanning service - start failed
                                _this._errorDlg.show();
                            });
                        }, function () {
                            // Scanning service - init failed
                            if (!started) {
                                started = true;
                                if (usePlugins) {
                                    try {
                                        _this._plugin = document.getElementById("webscanningObjectX");
                                        _this._plugin.StartWebScanningService();
                                    }
                                    catch (e) {
                                        _this._plugin = document.getElementById("webscanningObject");
                                        try {
                                            _this._plugin.StartWebScanningService();
                                        }
                                        catch (e) { }
                                    }
                                }
                                else {
                                    window.location.href = "Leadtools.WebScanning.Host:" + lt.LTHelper.browser;
                                }
                            }
                            if (count-- == 0) {
                                _this._errorDlg.show();
                                _this.endOperation(false);
                                clearInterval(interval);
                            }
                        });
                    }, timeout);
                }
                else {
                    this._scanningService = new lt.Scanning.JavaScript.SaneScanning(50000);
                    var interval = setInterval(function () {
                        _this._scanningService.init(function () {
                            // Scanning service - init succeed
                            clearInterval(interval);
                            _this._scanningService.start(function () {
                                // Scanning service - start succeed
                                _this.enableScanningControls(true);
                                _this.endOperation(false);
                                if (onSuccess != null)
                                    onSuccess();
                            }, function () {
                                // Scanning service - start failed
                                _this._errorDlg.show();
                            });
                        }, function () {
                            // Scanning service - init failed
                            if (!started) {
                                started = true;
                                if (lt.LTHelper.browser == lt.LTBrowser.firefox) {
                                    window.open("web+ltwebscanning://").close();
                                }
                                else {
                                    window.location.assign("web+ltwebscanning://");
                                }
                            }
                            if (count-- == 0) {
                                _this._errorDlg.show();
                                _this.endOperation(false);
                                clearInterval(interval);
                            }
                        });
                    }, timeout);
                }
            };
            // Unreferenced 
            ScanningDemoApp.setTwainCapability = function (scanningService, capabilityType, itemType, val, onSuccess, onFailure) {
                var twainService = scanningService.getHandle();
                twainService.setCapabilityValue(capabilityType, itemType, val, null, onSuccess, onFailure);
            };
            // Unreferenced 
            ScanningDemoApp.getTwainCapability = function (scanningService, capabilityType, getMethod, onSuccess, onFailure) {
                var twainService = scanningService.getHandle();
                twainService.getCapability(capabilityType, getMethod, null, function (capability) {
                    var message;
                    message = "Capability: {0}\nContainer Type: {1}\n\n".format(capability.information.capabilityType, capability.information.containerType);
                    if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.Enumeration) {
                        var twEnum = capability.enumeration;
                        var twValues = twEnum.values;
                        message += "Item Type: {0}\nCurrent Index: {1}\nDefault Index: {2}\nValues: ".format(twEnum.itemType, twEnum.currentIndex, twEnum.defaultIndex);
                        for (var i = 0; i < twValues.length; i++) {
                            message += twValues[i];
                            if (i != (twValues.length - 1))
                                message += ", ";
                        }
                    }
                    else if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.OneValue) {
                        var twOneVal = capability.oneValue;
                        message += "Item Type: {0}\nValue: {1}".format(twOneVal.itemType, twOneVal.value);
                    }
                    else if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.Range) {
                        var twRange = capability.range;
                        message += "Item Type: {0}\nCurrent Value: {1}\nDefault Value: {2}\nStep Size: {3}\nMinimum: {4}\nMaximum: {5}".format(twRange.itemType, twRange.currentValue, twRange.defaultValue, twRange.stepSize, twRange.minimumValue, twRange.maximumValue);
                    }
                    else if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.Array) {
                        var twArray = capability.array;
                        var twValues = twArray.values;
                        message += "Item Type: {0}\nValues: ".format(twArray.itemType);
                        for (var i = 0; i < twValues.length; i++) {
                            message += twValues[i];
                            if (i != (twValues.length - 1))
                                message += ", ";
                        }
                    }
                    else {
                        message += "Unsupported Container Type";
                    }
                    alert(message);
                    onSuccess();
                }, function (err) {
                    onFailure(err);
                });
            };
            ScanningDemoApp.prototype.enableScanningControls = function (enable) {
                $(this.demoUI.scanBtn).prop("disabled", !enable);
                $(this.demoUI.selectSourceBtn).prop("disabled", !enable);
            };
            ScanningDemoApp.prototype.addThumbnail = function (imageIndex) {
                var _this = this;
                this._totalPages++;
                this._totalScannedPages++;
                var imageElement = document.createElement('img');
                imageElement.id = 'img_' + imageIndex;
                imageElement.classList.add("thumbnailImg");
                // Attach image index as data to the image element
                $(imageElement).data("data", imageIndex);
                // Thumbnail image width and height
                var width = $(this.thumbnails.container).width();
                var height = $(this.thumbnails.container).width();
                // Get page source
                imageElement.src = this._scanningService.getPage(imageIndex, lt.Twain.JavaScript.RasterImageFormat.Unknown, 0, width, height);
                $(this.thumbnails.container).append(imageElement);
                // Handle click event
                imageElement.onclick = function (e) { return _this.thumbnailImage_Click(e); };
            };
            ScanningDemoApp.prototype.thumbnailImage_Click = function (e) {
                var pageNumber = parseInt($(e.currentTarget).data("data"));
                this.gotoPage(pageNumber);
            };
            ScanningDemoApp.prototype.updateThumbnail = function () {
                var imgElement = $('#img_' + this._currentPageNumber);
                var width = $(this.thumbnails.container).width();
                var height = $(this.thumbnails.container).width();
                imgElement[0].src = this._scanningService.getPage(this._currentPageNumber, lt.Twain.JavaScript.RasterImageFormat.Unknown, 0, width, height);
                this.imageViewerControl.imageUrl = this._scanningService.getPage(this._currentPageNumber, lt.Twain.JavaScript.RasterImageFormat.Unknown, 0, 0, 0);
            };
            ScanningDemoApp.prototype.gotoPage = function (pageNumber) {
                if (pageNumber > 0) {
                    // Unmark all thumbnail images
                    HTML5Demos.Utils.DemoHelper.checked($(this.thumbnails.thumbnailsImgs), false);
                    // Get selected thumbnail image
                    var selectedThumbnailImg = $('#img_' + pageNumber);
                    // Mark it
                    HTML5Demos.Utils.DemoHelper.checked(selectedThumbnailImg, true);
                    this._currentPageNumber = pageNumber;
                    // Set viewer image
                    this.imageViewerControl.imageUrl = this._scanningService.getPage(pageNumber, lt.Twain.JavaScript.RasterImageFormat.Unknown, 0, 0, 0);
                }
                else {
                    this._currentPageNumber = -1;
                    this.imageViewerControl.imageUrl = null;
                }
            };
            ScanningDemoApp.prototype.scanBtn_Click = function (e) {
                var _this = this;
                this.beginOperation("Scanning...");
                this._scanningService.acquire(function (status) {
                    // Scanning service - acquire succeed
                    var newPageAdded = false;
                    for (; _this._totalScannedPages < status.pageCount; _this._lastPageNumber++) {
                        newPageAdded = true;
                        _this.addThumbnail(_this._lastPageNumber + 1);
                        $(_this.demoUI.printBtn).prop("disabled", false);
                        $(_this.demoUI.editImageBtn).prop("disabled", false);
                        $(_this.demoUI.previousPageBtn).prop("disabled", false);
                        $(_this.demoUI.nextPageBtn).prop("disabled", false);
                    }
                    if (!status.isScanning) {
                        _this.endOperation(false);
                        if (newPageAdded) {
                            _this.gotoPage(_this._lastPageNumber);
                        }
                        else {
                            alert(status.errMessage);
                        }
                    }
                }, function () {
                    // Scanning service - acquire failed
                    _this.startScanningService(function () {
                        _this.scanBtn_Click(e);
                    });
                });
            };
            ScanningDemoApp.prototype.selectSourceBtn_Click = function (e) {
                var _this = this;
                this._scanningService.getStatus(function (status) {
                    // Scanning service - getStatus succeed
                    var scanSource = status.selectedSource;
                    _this._scanningService.getSources(function (sources) {
                        // Scanning service - getSources succeed
                        var options = "";
                        for (var i = 0; i < sources.length; i++) {
                            if (sources[i] == scanSource)
                                options += "<option selected='selected'>" + sources[i] + "</option>";
                            else
                                options += "<option>" + sources[i] + "</option>";
                        }
                        _this._selectSourceDlg.show(options);
                    }, function () {
                        // Scanning service - getSources failed
                        _this._errorDlg.show();
                    });
                }, function () {
                    // Scanning service - getStatus failed
                    _this.startScanningService(function () {
                        _this.selectSourceBtn_Click(e);
                    });
                });
            };
            ScanningDemoApp.prototype.printBtn_Click = function (e) {
                var windowContent = '<!DOCTYPE html>';
                windowContent += '<html>';
                windowContent += '<body>';
                for (var i = 1; i <= this._totalPages; i++) {
                    windowContent += "<img src='" + this._scanningService.getPage(i, 0, 0, 0, 0) + "' style=\"width:100%;height:100%;\">";
                }
                windowContent += '</body>';
                windowContent += '</html>';
                var printWin = window.open('', '', 'width=500,height=500');
                printWin.document.open();
                printWin.document.write(windowContent);
                printWin.document.close();
                printWin.print();
                printWin.close();
            };
            ScanningDemoApp.prototype.editImageBtn_Click = function (e) {
                this._imageProcessingDlg.show(this._currentPageNumber);
            };
            ScanningDemoApp.prototype.selectSourceDlg_OkClick = function (scanSource) {
                var _this = this;
                this.beginOperation("Selecting Source");
                this._scanningService.selectSource(scanSource, function () {
                    // Scanning service - selectSource succeed
                    _this.endOperation(false);
                }, function () {
                    // Scanning service - selectSource failed
                    _this._errorDlg.show();
                    _this.endOperation(false);
                });
            };
            ScanningDemoApp.prototype.imageProcessingDlg_applyClick = function (args) {
                var _this = this;
                this.beginOperation("Processing...");
                this._scanningService.applyImageProcessingCommand(this._currentPageNumber, this._currentPageNumber, HTML5Demos.Dialogs.commandNames[args.command], args.param, function () {
                    // Scanning service - applyImageProcessingCommand succeed
                    _this._scanningService.getStatus(function (status) {
                        // Scanning service - getStatus succeed
                        _this.endOperation(false);
                        if (status.errCode == 1)
                            _this.updateThumbnail();
                        else
                            alert(status.errMessage);
                    }, function () {
                        // Scanning service - getStatus failed
                        _this.endOperation(false);
                    });
                }, function () {
                    // Scanning service - applyImageProcessingCommand failed
                    _this._scanningService.getStatus(function (status) {
                        // Scanning service - getStatus succeed
                        _this.endOperation(false);
                    }, function () {
                        // Scanning service - getStatus failed
                        _this.endOperation(false);
                    });
                });
            };
            ScanningDemoApp.prototype.previousPageBtn_Click = function (e) {
                if (this._currentPageNumber > 1)
                    this.gotoPage(this._currentPageNumber - 1);
            };
            ScanningDemoApp.prototype.nextPageBtn_Click = function (e) {
                if (this._currentPageNumber < this._totalPages)
                    this.gotoPage(this._currentPageNumber + 1);
            };
            return ScanningDemoApp;
        }(HTML5Demos.CommonApp));
        ScanningDemo.ScanningDemoApp = ScanningDemoApp;
    })(ScanningDemo = HTML5Demos.ScanningDemo || (HTML5Demos.ScanningDemo = {}));
})(HTML5Demos || (HTML5Demos = {}));
window.onload = function () {
    HTML5Demos.CommonApp.runDemo(new HTML5Demos.ScanningDemo.ScanningDemoApp());
};
//# sourceMappingURL=Main.js.map