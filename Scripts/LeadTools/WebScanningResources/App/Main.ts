module HTML5Demos {
    export module ScanningDemo {
        interface ScanningPlugin extends HTMLElement {
            StartWebScanningService(): any;
            EndWebScanningService(): any;
        }

        export class ScanningDemoApp extends CommonApp {
            private _scanningService: lt.Scanning.JavaScript.IScanning;
            private _isWindowsEnvironment: boolean;
            private _plugin: any;

            private _totalPages: number = 0;
            private _totalScannedPages: number = 0;
            private _lastPageNumber: number = 0;
            private _currentPageNumber: number;

            // Demo UI
            private demoUI = {
                scanBtn: "#scan",
                selectSourceBtn: "#selectSource",
                printBtn: "#print",
                editImageBtn: "#editImage",
                previousPageBtn: "#previousPage",
                nextPageBtn: "#nextPage"
            };

            private thumbnails = {
                container: "#thumbnailsDiv",
                thumbnailsImgs: ".thumbnailImg"
            }

            // Override get method
            get demoName(): string {
                return "LEADTOOLS JavaScript Scanning Demo";
            }

            private _selectSourceDlg: Dialogs.SelectSourceDlg;
            private _imageProcessingDlg: Dialogs.ImageProcessingDlg;
            private _errorDlg: Dialogs.ErrorDlg;

            // Override method 
            public run() {
                // call base class run method
                // which calls verifyService()
                super.run();

                if (lt.LTHelper.device == lt.LTDevice.desktop) {
                    if (lt.LTHelper.OS == lt.LTOS.windows || lt.LTHelper.OS == lt.LTOS.windows7 || lt.LTHelper.OS == lt.LTOS.windows8)
                        this._isWindowsEnvironment = true;
                }

                this.initDemoUI();
                this.initWebScanning();
                this._imageProcessingDlg.scanningService = this._scanningService;
            }

            // Override method 
            public verifyService(): void { /* Do nothing */ }

            // Override method 
            public unload(): void {
                if (this._scanningService != null)
                    this._scanningService.stop(null, null);
            }

            private initDemoUI(): void {
                // Bind events to UI elements
                $(this.demoUI.scanBtn).bind("click", $.proxy(this.scanBtn_Click, this));
                $(this.demoUI.selectSourceBtn).bind("click", $.proxy(this.selectSourceBtn_Click, this));
                $(this.demoUI.printBtn).bind("click", $.proxy(this.printBtn_Click, this));
                $(this.demoUI.editImageBtn).bind("click", $.proxy(this.editImageBtn_Click, this));
                $(this.demoUI.previousPageBtn).bind("click", $.proxy(this.previousPageBtn_Click, this));
                $(this.demoUI.nextPageBtn).bind("click", $.proxy(this.nextPageBtn_Click, this));

                // Init select source dialog
                this._selectSourceDlg = new Dialogs.SelectSourceDlg();
                this._selectSourceDlg.OkClick = ((scanSource: string) => this.selectSourceDlg_OkClick(scanSource));
                // Init error dialog
                this._errorDlg = new Dialogs.ErrorDlg(this._isWindowsEnvironment);

                this._imageProcessingDlg = new Dialogs.ImageProcessingDlg();
                this._imageProcessingDlg.applyClick = ((args: Dialogs.ImageProcessingEventArgs) => this.imageProcessingDlg_applyClick(args));
            }

            private initWebScanning(): void {
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

                        this.startScanningService(() => {
                            if (this._isWindowsEnvironment) {
                                (<lt.Scanning.JavaScript.TwainScanning>this._scanningService).isAvailable(function (available) {
                                    if (!available) {
                                        this.enableScanningButtons(false);
                                        alert("No TWAIN Data Source installed.");
                                    }
                                }, null);
                            }
                        });
                    }
                } else {
                    webScanningSupported = false;
                    window.alert("Web scanning is only supported on desktop Windows and Linux browsers.");
                }

                this.enableScanningControls(webScanningSupported);
            }

            private startScanningService(onSuccess: () => void): void {
                var started = false;
                var count = 20;
                var timeout = 1000;
                if (this._isWindowsEnvironment) {
                    var usePlugins = lt.LTHelper.browser != lt.LTBrowser.edge && lt.LTHelper.browser != lt.LTBrowser.chrome;
                    this._plugin = null;
                    this._scanningService = new lt.Scanning.JavaScript.TwainScanning("http://localhost/ScanService/");
                    var interval = setInterval(() => {
                        this._scanningService.init(
                            () => {
                                // Scanning service - init succeed
                                clearInterval(interval);

                                this._scanningService.start(
                                    () => {
                                        // Scanning service - start succeed
                                        this.enableScanningControls(true);
                                        this.endOperation(false);

                                        if (onSuccess != null)
                                            onSuccess();
                                    },
                                    () => {
                                        // Scanning service - start failed
                                        this._errorDlg.show();
                                    });
                            },
                            () => {
                                // Scanning service - init failed
                                if (!started) {
                                    started = true;
                                    if (usePlugins) {
                                        try {
                                            this._plugin = <ScanningPlugin>document.getElementById("webscanningObjectX");
                                            this._plugin.StartWebScanningService();
                                        } catch (e) {
                                            this._plugin = <ScanningPlugin>document.getElementById("webscanningObject");
                                            try {
                                                this._plugin.StartWebScanningService();
                                            } catch (e) { }
                                        }
                                    } else {
                                        window.location.href = "Leadtools.WebScanning.Host:" + lt.LTHelper.browser;
                                    }
                                }

                                if (count-- == 0) {
                                    this._errorDlg.show();
                                    this.endOperation(false);
                                    clearInterval(interval);
                                }
                            });
                    }, timeout);
                } else {
                    this._scanningService = new lt.Scanning.JavaScript.SaneScanning(50000);
                    var interval = setInterval(() => {
                        this._scanningService.init(
                            () => {
                                // Scanning service - init succeed
                                clearInterval(interval);

                                this._scanningService.start(
                                    () => {
                                        // Scanning service - start succeed
                                        this.enableScanningControls(true);

                                        this.endOperation(false);

                                        if (onSuccess != null)
                                            onSuccess();
                                    },
                                    () => {
                                        // Scanning service - start failed
                                        this._errorDlg.show();
                                    });
                            },
                            () => {
                                // Scanning service - init failed
                                if (!started) {
                                    started = true;
                                    if (lt.LTHelper.browser == lt.LTBrowser.firefox) {
                                        window.open("web+ltwebscanning://").close();
                                    } else {
                                        window.location.assign("web+ltwebscanning://");
                                    }
                                }

                                if (count-- == 0) {
                                    this._errorDlg.show();
                                    this.endOperation(false);
                                    clearInterval(interval);
                                }
                            });
                    }, timeout);
                }
            }

            // Unreferenced 
            public static setTwainCapability(scanningService: lt.Scanning.JavaScript.IScanning, capabilityType: lt.Twain.JavaScript.TwainCapabilityType, itemType: lt.Twain.JavaScript.TwainItemType, val: string, onSuccess: () => void, onFailure: (err: string) => void) {
                var twainService: lt.Twain.JavaScript.TwainService = <lt.Twain.JavaScript.TwainService>scanningService.getHandle();
                twainService.setCapabilityValue(capabilityType, itemType, val, null, onSuccess, onFailure);
            }
            // Unreferenced 
            public static getTwainCapability(scanningService: lt.Scanning.JavaScript.IScanning, capabilityType: lt.Twain.JavaScript.TwainCapabilityType, getMethod: lt.Twain.JavaScript.TwainGetCapabilityMode, onSuccess: () => void, onFailure: (err: string) => void) {
                var twainService: lt.Twain.JavaScript.TwainService = <lt.Twain.JavaScript.TwainService>scanningService.getHandle();
                twainService.getCapability(capabilityType, getMethod, null, function (capability: lt.Twain.JavaScript.TwainCapability) {
                    var message: string;
                    message = "Capability: {0}\nContainer Type: {1}\n\n".format(capability.information.capabilityType, capability.information.containerType);

                    if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.Enumeration) {
                        var twEnum: lt.Twain.JavaScript.TwainEnumerationCapability = capability.enumeration;
                        var twValues: any[] = twEnum.values;

                        message += "Item Type: {0}\nCurrent Index: {1}\nDefault Index: {2}\nValues: ".format(twEnum.itemType, twEnum.currentIndex, twEnum.defaultIndex);
                        for (var i = 0; i < twValues.length; i++) {
                            message += twValues[i];
                            if (i != (twValues.length - 1))
                                message += ", ";
                        }
                    }
                    else if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.OneValue) {
                        var twOneVal: lt.Twain.JavaScript.TwainOneValueCapability = capability.oneValue;

                        message += "Item Type: {0}\nValue: {1}".format(twOneVal.itemType, twOneVal.value);
                    }
                    else if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.Range) {
                        var twRange: lt.Twain.JavaScript.TwainRangeCapability = capability.range;

                        message += "Item Type: {0}\nCurrent Value: {1}\nDefault Value: {2}\nStep Size: {3}\nMinimum: {4}\nMaximum: {5}".format(
                            twRange.itemType,
                            twRange.currentValue,
                            twRange.defaultValue,
                            twRange.stepSize,
                            twRange.minimumValue,
                            twRange.maximumValue);
                    }
                    else if (capability.information.containerType == lt.Twain.JavaScript.TwainContainerType.Array) {
                        var twArray: lt.Twain.JavaScript.TwainArrayCapability = capability.array;
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
                }, function (err: string) {
                    onFailure(err);
                });
            }

            private enableScanningControls(enable: boolean): void {
                $(this.demoUI.scanBtn).prop("disabled", !enable);
                $(this.demoUI.selectSourceBtn).prop("disabled", !enable);
            }

            private addThumbnail(imageIndex: number): void {
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
                imageElement.onclick = (e: MouseEvent) => this.thumbnailImage_Click(e);
            }

            private thumbnailImage_Click(e: MouseEvent): void {
                var pageNumber = parseInt($(e.currentTarget).data("data"));
                this.gotoPage(pageNumber);
            }

            public updateThumbnail() {
                var imgElement = $('#img_' + this._currentPageNumber);
                var width = $(this.thumbnails.container).width();
                var height = $(this.thumbnails.container).width();
                (<HTMLImageElement>imgElement[0]).src = this._scanningService.getPage(this._currentPageNumber, lt.Twain.JavaScript.RasterImageFormat.Unknown, 0, width, height);

                this.imageViewerControl.imageUrl = this._scanningService.getPage(this._currentPageNumber, lt.Twain.JavaScript.RasterImageFormat.Unknown, 0, 0, 0);
            }

            private gotoPage(pageNumber: number): void {
                if (pageNumber > 0) {
                    // Unmark all thumbnail images
                    Utils.DemoHelper.checked($(this.thumbnails.thumbnailsImgs), false);
                    // Get selected thumbnail image
                    var selectedThumbnailImg = $('#img_' + pageNumber);
                    // Mark it
                    Utils.DemoHelper.checked(selectedThumbnailImg, true);

                    this._currentPageNumber = pageNumber;
                    // Set viewer image
                    this.imageViewerControl.imageUrl = this._scanningService.getPage(pageNumber, lt.Twain.JavaScript.RasterImageFormat.Unknown, 0, 0, 0);
                }
                else {
                    this._currentPageNumber = -1;
                    this.imageViewerControl.imageUrl = null;
                }
            }

            private scanBtn_Click(e: JQueryEventObject): void {
                this.beginOperation("Scanning...");
                this._scanningService.acquire(
                    (status) => {
                        // Scanning service - acquire succeed
                        var newPageAdded: boolean = false;
                        for (; this._totalScannedPages < status.pageCount; this._lastPageNumber++) {
                            newPageAdded = true;
                            this.addThumbnail(this._lastPageNumber + 1);

                            $(this.demoUI.printBtn).prop("disabled", false);
                            $(this.demoUI.editImageBtn).prop("disabled", false);
                            $(this.demoUI.previousPageBtn).prop("disabled", false);
                            $(this.demoUI.nextPageBtn).prop("disabled", false);
                        }

                        if (!status.isScanning) {
                            this.endOperation(false);
                            if (newPageAdded) {
                                this.gotoPage(this._lastPageNumber);
                            } else {
                                alert(status.errMessage);
                            }
                        }
                    },
                    () => {
                        // Scanning service - acquire failed
                        this.startScanningService(() => {
                            this.scanBtn_Click(e);
                        });
                    });
            }

            private selectSourceBtn_Click(e: JQueryEventObject): void {
                this._scanningService.getStatus(
                    (status) => {
                        // Scanning service - getStatus succeed
                        var scanSource = status.selectedSource;
                        this._scanningService.getSources(
                            (sources) => {
                                // Scanning service - getSources succeed
                                var options = "";
                                for (var i = 0; i < sources.length; i++) {
                                    if (sources[i] == scanSource)
                                        options += "<option selected='selected'>" + sources[i] + "</option>";
                                    else
                                        options += "<option>" + sources[i] + "</option>";
                                }

                                this._selectSourceDlg.show(options);
                            },
                            () => {
                                // Scanning service - getSources failed
                                this._errorDlg.show();
                            });
                    },
                    () => {
                        // Scanning service - getStatus failed
                        this.startScanningService(() => {
                            this.selectSourceBtn_Click(e);
                        });
                    });
            }

            private printBtn_Click(e: JQueryEventObject): void {
                var windowContent: string = '<!DOCTYPE html>';
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
            }

            private editImageBtn_Click(e: JQueryEventObject): void {
                this._imageProcessingDlg.show(this._currentPageNumber);
            }

            private selectSourceDlg_OkClick(scanSource: string): void {
                this.beginOperation("Selecting Source");
                this._scanningService.selectSource(scanSource,
                    () => {
                        // Scanning service - selectSource succeed
                        this.endOperation(false);
                    },
                    () => {
                        // Scanning service - selectSource failed
                        this._errorDlg.show();
                        this.endOperation(false);
                    })
            }

            private imageProcessingDlg_applyClick(args: Dialogs.ImageProcessingEventArgs): void {
                this.beginOperation("Processing...");
                this._scanningService.applyImageProcessingCommand(this._currentPageNumber, this._currentPageNumber, Dialogs.commandNames[args.command], args.param,
                    () => {
                        // Scanning service - applyImageProcessingCommand succeed
                        this._scanningService.getStatus(
                            (status) => {
                                // Scanning service - getStatus succeed
                                this.endOperation(false);
                                if (status.errCode == 1)
                                    this.updateThumbnail();
                                else
                                    alert(status.errMessage);
                            },
                            () => {
                                // Scanning service - getStatus failed
                                this.endOperation(false);
                            });
                    },
                    () => {
                        // Scanning service - applyImageProcessingCommand failed
                        this._scanningService.getStatus(
                            (status) => {
                                // Scanning service - getStatus succeed
                                this.endOperation(false);
                            }, () => {
                                // Scanning service - getStatus failed
                                this.endOperation(false);
                            });
                    });
            }

            private previousPageBtn_Click(e: JQueryEventObject): void {
                if (this._currentPageNumber > 1)
                    this.gotoPage(this._currentPageNumber - 1);
            }

            private nextPageBtn_Click(e: JQueryEventObject): void {
                if (this._currentPageNumber < this._totalPages)
                    this.gotoPage(this._currentPageNumber + 1);
            }
        }
    }
}

window.onload = () => {
    HTML5Demos.CommonApp.runDemo(new HTML5Demos.ScanningDemo.ScanningDemoApp());
};