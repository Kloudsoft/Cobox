module AffinityDms.Entities {
    export class DocumentViewer {
        public loadingDlg: Dialogs.DocumentViewerDemoLoadingDlg;
        public run(): void {
            var DocViewerType = <HTMLInputElement>document.getElementById("DocViewerType");
            if (DocViewerType != null) {
                if (DocViewerType.getAttribute("data-id") != null && DocViewerType.getAttribute("data-type") != null) {
                    var id = parseInt(DocViewerType.getAttribute("data-id"));
                    if (id > 0 && DocViewerType.getAttribute("data-type") != "") {
                        var type: DiscussionPostAttachmentType = DiscussionPostAttachmentType.None;
                        switch (DocViewerType.getAttribute("data-type")) {
                            case "Document":
                                {
                                    type = DiscussionPostAttachmentType.Document;
                                    break;
                                }
                            case "External":
                                {
                                    type = DiscussionPostAttachmentType.External;
                                    break;
                                }
                            default:
                                break;
                        }
                        var strUrl = "../../TenantDocumentViewer/GetViewerDocument/"
                        var docViewerType = <HTMLInputElement>document.getElementById("docViewerType");
                        if (docViewerType != null) {
                            if (docViewerType.value = "TemplateDesigner") {
                                strUrl = "../../TenantDocumentViewer/GetTemplateDesigner/";
                            }
                        }
                        $.ajax({
                            type: "POST",
                            url: "../../TenantDocumentViewer/GetViewerDocument/",
                            data: JSON.stringify({ id: id, type: type }),
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            success: function (response: any) {
                                //lt.Documents.DocumentFactory.serviceHost = "http://localhost:19000/";
                                var url = "";
                                if (response != null) {
                                    if (response != "") {
                                        url = response;
                                    }
                                }
                                AffinityDms.Entities.DocumentViewer.prototype.LoadDocument(url, "http://affinity-ecm.azurewebsites.net/", "", "api");

                            },
                            error: function (responseText: any) {
                                alert("Oops an Error Occured");
                            }
                        });
                    }
                }
            }
            else {
                //var docViewerUrl = <HTMLInputElement>document.getElementById("docViewerUrl");
                //var url = "http://demo.leadtools.com/images/pdf/leadtools.pdf";
                //if (docViewerUrl != null) {
                //    if (docViewerUrl.value != "") {
                //        url = docViewerUrl.value;
                //    }
                //}
                //AffinityDms.Entities.DocumentViewer.prototype.LoadDocument(url, "http://affinity-ecm.azurewebsites.net/", "", "api");

            }

        }

        public LoadDocument(url: string, serviceHost: string, servicePath: string, apiPath: string) {
            $("#viewer-container").text("");
            $("#thumbnails-container").text("");
            AffinityDms.Entities.DocumentViewer.prototype.loadingDlg = new AffinityDms.Entities.Dialogs.DocumentViewerDemoLoadingDlg();
            AffinityDms.Entities.DocumentViewer.prototype.loadingDlg.show(false, false, "Verifying Service Connection...");
            //$(AffinityDms.Entities.DocumentViewer.prototype.loadingDlg.dialogUI.processTextLabel).text("Verifying Service Connection...");
			lt.LTHelper.licenseDirectory = "../../Leadtools";
            lt.Documents.DocumentFactory.serviceHost = serviceHost;
            lt.Documents.DocumentFactory.servicePath = servicePath;
            lt.Documents.DocumentFactory.serviceApiPath = apiPath;
            var createOptions = new lt.Documents.UI.DocumentViewerCreateOptions();
            createOptions.viewContainer = document.getElementById("viewer-container");
            createOptions.viewContainer.style.minWidth = "100%";
            createOptions.viewContainer.style.minHeight = "400px";

            // createOptions.viewContainer.style.cssFloat = "right";
            createOptions.viewContainer.style.display = "inline-block";
            createOptions.thumbnailsContainer = document.getElementById("thumbnails-container");
            createOptions.thumbnailsContainer.style.minWidth = "190px";
            createOptions.thumbnailsContainer.style.minHeight = "160px";
            createOptions.thumbnailsContainer.style.display = "inline-block";
            createOptions.thumbnailsContainer.style.cssFloat = "left";
            createOptions.thumbnailsContainer.style.overflow = "hidden";


            createOptions.useAnnotations = true;
            //When using elements we have to style them ourself, with it set to false we use a canvas instead.
            createOptions.thumbnailsCreateOptions.useElements = false;
            createOptions.viewCreateOptions.useElements = true;
            // createOptions.get_viewCreateOptions
            var documentViewer = lt.Documents.UI.DocumentViewerFactory.createDocumentViewer(createOptions);
            documentViewer.view.preferredItemType = lt.Documents.UI.DocumentViewerItemType.svg;
            documentViewer.thumbnails.imageViewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
            documentViewer.commands.run(lt.Documents.UI.DocumentViewerCommands.interactivePanZoom, null);
            AffinityDms.Entities.DocumentViewer.prototype.loadingDlg.show(false, false, "Loading Document...");
            lt.Documents.DocumentFactory.loadFromUri(url, null)
                .done(function (documentResponse) {
                    if (AffinityDms.Entities.DocumentViewer.prototype.loadingDlg == null) {
                        AffinityDms.Entities.DocumentViewer.prototype.loadingDlg = new AffinityDms.Entities.Dialogs.DocumentViewerDemoLoadingDlg();
                    }
                    AffinityDms.Entities.DocumentViewer.prototype.loadingDlg.processing("Set Document...");
                    documentViewer.setDocument(documentResponse);
                    //documentViewer.get_view().imageViewer.sizeMode = ;
                    documentViewer.view.imageViewer.zoom(lt.Controls.ControlSizeMode.fit, 1, documentViewer.view.imageViewer.defaultZoomOrigin);

                    //documentViewer.view.get_imageViewer().scrollMode = lt.Controls.ControlScrollMode.auto;
                    var DocTotalPageNumberId = document.getElementById("DocTotalPageNumberId");
                    var DocCurrentPageNumberId = document.getElementById("DocCurrentPageNumberId");
                    if (DocTotalPageNumberId != null) {
                        DocTotalPageNumberId.textContent = documentViewer.get_pageCount().toString();
                    }
                    if (DocCurrentPageNumberId != null) {
                        DocCurrentPageNumberId.textContent = documentViewer.get_currentPageNumber().toString();
                    }
                    //#region PreviousPageButton: DocPreviousPageId
                    var DocPreviousPageId = document.getElementById("DocPreviousPageId");
                    if (DocPreviousPageId != null) {
                        DocPreviousPageId.onclick = function (e) {
                            var currentDocumentViewer = documentViewer;
                            var DocPreviousPageId = document.getElementById("DocPreviousPageId");
                            if (DocPreviousPageId != null) {
                                if ((currentDocumentViewer.get_currentPageNumber() > 1) && (currentDocumentViewer.get_currentPageNumber() <= currentDocumentViewer.get_pageCount())) {
                                    currentDocumentViewer.gotoPage((currentDocumentViewer.get_currentPageNumber() - 1));
                                    var DocCurrentPageNumberId = document.getElementById("DocCurrentPageNumberId");
                                    if (DocCurrentPageNumberId != null) {
                                        DocCurrentPageNumberId.textContent = currentDocumentViewer.get_currentPageNumber().toString();
                                    }
                                }
                            }
                        };
                    }
                    //#endregion
                    //#region NextPageButton: DocNextPageId
                    var DocNextPageId = document.getElementById("DocNextPageId");
                    if (DocNextPageId != null) {
                        DocNextPageId.onclick = function (e) {
                            var currentDocumentViewer = documentViewer;
                            var DocNextPageId = document.getElementById("DocPreviousPageId");
                            if (DocNextPageId != null) {
                                if ((currentDocumentViewer.get_currentPageNumber() >= 1) && (currentDocumentViewer.get_currentPageNumber() < currentDocumentViewer.get_pageCount())) {
                                    currentDocumentViewer.gotoPage((currentDocumentViewer.get_currentPageNumber() + 1));
                                    var DocCurrentPageNumberId = document.getElementById("DocCurrentPageNumberId");
                                    if (DocCurrentPageNumberId != null) {
                                        DocCurrentPageNumberId.textContent = currentDocumentViewer.get_currentPageNumber().toString();
                                    }
                                }
                            }
                        };
                    }
                    //#endregion
                    //#region ZoomInPercentageButton: ZoomInPercentageId
                    var ZoomInPercentageId = document.getElementById("ZoomInPercentageId");
                    if (ZoomInPercentageId != null) {
                        ZoomInPercentageId.onclick = function (e) {
                            var currentDocumentViewer = documentViewer;
                            var ZoomInPercentageId = document.getElementById("ZoomInPercentageId");
                            if (ZoomInPercentageId != null) {
                                currentDocumentViewer.view.get_zoomRatio();
                                var scalefactor = currentDocumentViewer.view.imageViewer.scaleFactor;
                                currentDocumentViewer.view.imageViewer.zoom(lt.Controls.ControlSizeMode.none, (scalefactor * currentDocumentViewer.view.zoomRatio), currentDocumentViewer.view.imageViewer.defaultZoomOrigin);
                                var ZoomPercentageId = document.getElementById("ZoomPercentageId")
                                if (ZoomPercentageId != null) {
                                    ZoomPercentageId.textContent = (currentDocumentViewer.view.imageViewer.scaleFactor * 100).toString().split(".")[0] + " %";
                                }
                            }
                        };
                    }
                    //#endregion
                    //#region ZoomOutPercentageButton: ZoomOutPercentageId
                    var ZoomOutPercentageId = document.getElementById("ZoomOutPercentageId");
                    if (ZoomOutPercentageId != null) {
                        ZoomOutPercentageId.onclick = function (e) {
                            var currentDocumentViewer = documentViewer;
                            var ZoomOutPercentageId = document.getElementById("ZoomOutPercentageId");
                            if (ZoomOutPercentageId != null) {
                                currentDocumentViewer.view.get_zoomRatio();
                                var scalefactor = currentDocumentViewer.view.imageViewer.scaleFactor;
                                currentDocumentViewer.view.imageViewer.zoom(lt.Controls.ControlSizeMode.none, (scalefactor / currentDocumentViewer.view.zoomRatio), currentDocumentViewer.view.imageViewer.defaultZoomOrigin);
                                var ZoomPercentageId = document.getElementById("ZoomPercentageId")
                                if (ZoomPercentageId != null) {
                                    ZoomPercentageId.textContent = (currentDocumentViewer.view.imageViewer.scaleFactor * 100).toString().split(".")[0] + " %";

                                }
                            }
                        };
                    }
                    //#endregion
                }).fail(function (documentResponse) {
                    var exception = JSON.parse(documentResponse.responseText);
                    AffinityDms.Entities.DocumentViewer.prototype.loadingDlg.show(true, true, exception.detail);
                }).always(function (e) {
                    setTimeout(function () { AffinityDms.Entities.DocumentViewer.prototype.loadingDlg.hide(); }, 2000);
                });
        }
        public docViewerUrlSearch(event): void {
            this.run();
        }
    }
    export module Dialogs {

        export class DocumentViewerDemoLoadingDlg {
            // Create shortcuts for the dialog UI elements
            public dialogUI = {
                dialog: "#documentViewerDemoLoadingDlg", /* The whole dialog */
                processTextLabel: "#processText",
                progress: {
                    bar: "#progressbar",
                    percentage: "#progressPercentage"
                },
                cancelDiv: "#cancelDiv",
                cancelBtn: "#cancel"
            };

            public isCancelled: boolean;

            constructor() {
                $(this.dialogUI.cancelBtn).bind("click", $.proxy(this.cancelBtn_Click, this));
            }

            public show(enableCancellation: boolean, enableProgress: boolean, processText?: string): void {
                this.isCancelled = false;
                $(this.dialogUI.cancelBtn).prop("disabled", false);

                if (enableProgress) {
                    $(this.dialogUI.progress.bar).width("0%");
                    $(this.dialogUI.progress.bar).attr("aria-valuenow", 0);
                } else {
                    $(this.dialogUI.progress.bar).width("100%");
                    $(this.dialogUI.progress.bar).attr("aria-valuenow", 100);
                }

                $(this.dialogUI.progress.percentage).text("");

                enableCancellation ? $(this.dialogUI.cancelDiv).css("display", "block") : $(this.dialogUI.cancelDiv).css("display", "none");
                $(this.dialogUI.processTextLabel).text(processText);
                $(this.dialogUI.dialog).modal();
            }

            public processing(processText: string): void {
                // To change process text , while the dialog is already shown
                $(this.dialogUI.processTextLabel).text(processText);
            }

            public progress(percentage: number): void {
                $(this.dialogUI.progress.bar).width(percentage + "%");
                $(this.dialogUI.progress.bar).attr("aria-valuenow", percentage);
                $(this.dialogUI.progress.percentage).text(percentage + "%");
            }

            public hide(): void {
                $(this.dialogUI.dialog).modal("hide");
            }

            private cancelBtn_Click(e: JQueryEventObject): void {
                this.isCancelled = true;
                this.processing("Canceling Operation...");
                $(this.dialogUI.cancelBtn).prop("disabled", true);
            }
        }
    }
}













  //lt.Documents.DocumentFactory.serviceHost = "http://localhost:19000/";
                    //lt.Documents.DocumentFactory.servicePath = "";
                    //lt.Documents.DocumentFactory.serviceApiPath = "api";
                    //var createOptions = new lt.Documents.UI.DocumentViewerCreateOptions();
                    //createOptions.viewContainer = document.getElementById("viewer-container");
                    //createOptions.thumbnailsContainer = document.getElementById("thumbnails-container");
                    ////When using elements we have to style them ourself, with it set to false we use a canvas instead.
                    //createOptions.thumbnailsCreateOptions.useElements = false;
                    //createOptions.viewCreateOptions.useElements = false;
                    //var documentViewer = lt.Documents.UI.DocumentViewerFactory.createDocumentViewer(createOptions);
                    //documentViewer.view.preferredItemType = lt.Documents.UI.DocumentViewerItemType.svg;
                    //documentViewer.thumbnails.imageViewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
                    //documentViewer.commands.run(lt.Documents.UI.DocumentViewerCommands.interactivePanZoom, null);
                    //var docViewerUrl = <HTMLInputElement>document.getElementById("docViewerUrl");
                    //var url = "http://demo.leadtools.com/images/pdf/leadtools.pdf";
                    //if (docViewerUrl != null) {
                    //    if (docViewerUrl.value != "") {
                    //        url = docViewerUrl.value;
                    //    }
                    //}
                    //lt.Documents.DocumentFactory.loadFromUri(url, null)
                    //    .done(function (document) {
                    //        documentViewer.setDocument(document);
                    //    });








//public runPartial(): void {

        //    var DocViewerType = <HTMLInputElement>document.getElementById("DocViewerType");
        //    if (DocViewerType != null) {


        //        if (DocViewerType.getAttribute("data-id") != null && DocViewerType.getAttribute("data-type") != null) {
        //            var id = parseInt(DocViewerType.getAttribute("data-id"));
        //            if (id > 0 && DocViewerType.getAttribute("data-type") != "") {
        //                var type: DiscussionPostAttachmentType = DiscussionPostAttachmentType.None;
        //                switch (DocViewerType.getAttribute("data-type")) {
        //                    case "Document":
        //                        {
        //                            type = DiscussionPostAttachmentType.Document;
        //                            break;
        //                        }
        //                    case "External":
        //                        {
        //                            type = DiscussionPostAttachmentType.External;
        //                            break;
        //                        }
        //                    default:
        //                        break;
        //                }
        //                var strUrl = "../../TenantDocumentViewer/GetViewerDocument/"
        //                var docViewerType = <HTMLInputElement>document.getElementById("docViewerType");
        //                if (docViewerType != null) {
        //                    if (docViewerType.value = "TemplateDesigner") {
        //                        strUrl = "../../TenantDocumentViewer/GetTemplateDesigner/";
        //                    }
        //                }
        //                $.ajax({
        //                    type: "POST",
        //                    url: "../../TenantDocumentViewer/GetViewerDocument/",
        //                    data: JSON.stringify({ id: id, type: type }),
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: 'json',
        //                    success: function (response: any) {
        //                        //lt.Documents.DocumentFactory.serviceHost = "http://localhost:19000/";
        //                        var url = "";
        //                        if (response != null) {
        //                            if (response != "") {
        //                                url = response;
        //                            }
        //                        }
        //                        AffinityDms.Entities.DocumentViewer.prototype.LoadDocument(url, "http://affinity-ecm.azurewebsites.net/", "", "api");
        //                        /*

        //                         lt.Documents.DocumentFactory.serviceHost = "http://affinity-ecm.azurewebsites.net/";
        //                        lt.Documents.DocumentFactory.servicePath = "";
        //                        lt.Documents.DocumentFactory.serviceApiPath = "api";
        //                        var createOptions = new lt.Documents.UI.DocumentViewerCreateOptions();
        //                        createOptions.viewContainer = document.getElementById("viewer-container");
        //                        createOptions.thumbnailsContainer = document.getElementById("thumbnails-container");
        //                        createOptions.useAnnotations = true;
        //                        //When using elements we have to style them ourself, with it set to false we use a canvas instead.
        //                        createOptions.thumbnailsCreateOptions.useElements = false;
        //                        createOptions.viewCreateOptions.useElements = true;
        //                        createOptions.get_viewCreateOptions
        //                        var documentViewer = lt.Documents.UI.DocumentViewerFactory.createDocumentViewer(createOptions);
        //                        documentViewer.view.preferredItemType = lt.Documents.UI.DocumentViewerItemType.svg;
        //                        documentViewer.thumbnails.imageViewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
        //                       documentViewer.commands.run(lt.Documents.UI.DocumentViewerCommands.interactivePanZoom, null);
        //                       var url = "";
        //                        if (response != null) {
        //                            if (response != "") {
        //                                url = response;
        //                            }
        //                        }
        //                        lt.Documents.DocumentFactory.loadFromUri(url, null)
        //                            .done(function (document) {
        //                                documentViewer.setDocument(document);
        //                            });

        //                        */
        //                    },
        //                    error: function (responseText: any) {
        //                        alert("Oops an Error Occured");
        //                    }
        //                });
        //            }
        //        }
        //    }
        //    else {
        //        var docViewerUrl = <HTMLInputElement>document.getElementById("docViewerUrl");
        //        var url = "http://demo.leadtools.com/images/pdf/leadtools.pdf";
        //        if (docViewerUrl != null) {
        //            if (docViewerUrl.value != "") {
        //                url = docViewerUrl.value;
        //            }
        //        }
        //        AffinityDms.Entities.DocumentViewer.prototype.LoadDocument(url, "http://affinity-ecm.azurewebsites.net/", "", "api");
        //        //lt.Documents.DocumentFactory.serviceHost = "http://localhost:19000/";
        //        //lt.Documents.DocumentFactory.servicePath = "";
        //        //lt.Documents.DocumentFactory.serviceApiPath = "api";
        //        //var createOptions = new lt.Documents.UI.DocumentViewerCreateOptions();
        //        //createOptions.viewContainer = document.getElementById("viewer-container");
        //        //createOptions.thumbnailsContainer = document.getElementById("thumbnails-container");
        //        ////When using elements we have to style them ourself, with it set to false we use a canvas instead.
        //        //createOptions.thumbnailsCreateOptions.useElements = false;
        //        //createOptions.viewCreateOptions.useElements = false;
        //        //var documentViewer = lt.Documents.UI.DocumentViewerFactory.createDocumentViewer(createOptions);
        //        //documentViewer.view.preferredItemType = lt.Documents.UI.DocumentViewerItemType.svg;
        //        //documentViewer.thumbnails.imageViewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
        //        //documentViewer.commands.run(lt.Documents.UI.DocumentViewerCommands.interactivePanZoom, null);
        //        //var docViewerUrl = <HTMLInputElement>document.getElementById("docViewerUrl");
        //        //var url = "http://demo.leadtools.com/images/pdf/leadtools.pdf";
        //        //if (docViewerUrl != null) {
        //        //    if (docViewerUrl.value != "") {
        //        //        url = docViewerUrl.value;
        //        //    }
        //        //}
        //        //lt.Documents.DocumentFactory.loadFromUri(url, null)
        //        //    .done(function (document) {
        //        //        documentViewer.setDocument(document);
        //        //    });

        //    }

        //}












  /*

                                     lt.Documents.DocumentFactory.serviceHost = "http://affinity-ecm.azurewebsites.net/";
                                    lt.Documents.DocumentFactory.servicePath = "";
                                    lt.Documents.DocumentFactory.serviceApiPath = "api";
                                    var createOptions = new lt.Documents.UI.DocumentViewerCreateOptions();
                                    createOptions.viewContainer = document.getElementById("viewer-container");
                                    createOptions.thumbnailsContainer = document.getElementById("thumbnails-container");
                                    createOptions.useAnnotations = true;
                                    //When using elements we have to style them ourself, with it set to false we use a canvas instead.
                                    createOptions.thumbnailsCreateOptions.useElements = false;
                                    createOptions.viewCreateOptions.useElements = true;
                                    createOptions.get_viewCreateOptions
                                    var documentViewer = lt.Documents.UI.DocumentViewerFactory.createDocumentViewer(createOptions);
                                    documentViewer.view.preferredItemType = lt.Documents.UI.DocumentViewerItemType.svg;
                                    documentViewer.thumbnails.imageViewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;
                                   documentViewer.commands.run(lt.Documents.UI.DocumentViewerCommands.interactivePanZoom, null);
                                   var url = "";
                                    if (response != null) {
                                        if (response != "") {
                                            url = response;
                                        }
                                    }
                                    lt.Documents.DocumentFactory.loadFromUri(url, null)
                                        .done(function (document) {
                                            documentViewer.setDocument(document);
                                        });

                                    */























// holds instance of the document viewer control.
            //private documentViewer: lt.Documents.UI.DocumentViewer = null;
            // initialize and load.
            //public run(): void {
            //    var that = this;

            //    // Init the document viewer, pass along the containers
            //    var createOptions =
            //        new lt.Documents.UI.DocumentViewerCreateOptions();



            //    // We will choose to use Elements Mode for this example, but you can disable it
            //    // Elements Mode can be styled with CSS - see the HTML for information
            //    createOptions.viewCreateOptions.useElements = true;
            //    createOptions.thumbnailsCreateOptions.useElements = true;





            //    createOptions.viewContainer =
            //        document.getElementById("viewer-container");
            //    createOptions.thumbnailsContainer =
            //        document.getElementById("thumbnails-container");
            //    createOptions.useAnnotations = false;


            //    // Create the document viewer
            //    this.documentViewer = lt.Documents.UI.DocumentViewerFactory.createDocumentViewer(createOptions);
            //    // We prefer SVG viewing
            //    this.documentViewer.view.preferredItemType = lt.Documents.UI.DocumentViewerItemType.svg;
            //    // Change our thumbnails to be centered horizontally in the provided container
            //    this.documentViewer.thumbnails.imageViewer.viewHorizontalAlignment = lt.Controls.ControlAlignment.center;


            //    // Set the location of the
            //    lt.Documents.DocumentFactory.serviceHost = "http://localhost";
            //    //lt.Documents.DocumentFactory.serviceAppName = "LEADTOOLSDocumentsServiceHost19";
            //    //lt.Documents.DocumentFactory.documentsServiceName = "DocumentsService.svc";
            //    lt.Documents.DocumentFactory.servicePath = ""; // the path to the root of the service, which is nothing for this example
            //    lt.Documents.DocumentFactory.serviceApiPath = "api"; // Routing occurs at "/api", unless you change the routing in the DocumentsService

            //       /* A quick example with a different service location:
            //        * > client: http://localhost:123
            //        * > service: https://www.leadtools.com/path/to/service
            //        * > service routing namespace: /api
            //        * 
            //        * Set these properties with these values:
            //        * serviceHost = "https://www.leadtools.com";
            //        * servicePath = "path/to/service";
            //        * serviceApiPath = "api"
            //        */


            //    // Load a PDF document
            //    var docViewerUrl = <HTMLInputElement>document.getElementById("docViewerUrl");
            //    var url = "http://demo.leadtools.com/images/pdf/leadtools.pdf";

            //    if (docViewerUrl != null)
            //    {
            //        if (docViewerUrl.value != "")
            //        {
            //            url = docViewerUrl.value;
            //        }
            //    }
            //    //var loadDocumentCommand =
            //    //    lt.Documents.LoadDocumentCommand.create(url);
            //    //loadDocumentCommand.run()
            //    //    .done(function (document) {
            //    //        // We have a document
            //    //        // set the thumbnail size to max 80 x 80.
            //    //        // Aspect is maintained.
            //    //        document.images.thumbnailPixelSize =
            //    //            lt.LeadSizeD.create(80, 80);
            //    //        // Set the document in the viewer
            //    //        that.documentViewer.setDocument(document);
            //    //    })
            //    //    .fail(function (error) {
            //    //        alert("Error loading document: " + error)
            //    //    });


            //    var LoadDocumentFromUri = lt.Documents.DocumentFactory.loadFromUri(url, null)
            //        .done((document) => {
            //            this.documentViewer.setDocument(document);
            //        })
            //        .fail((jqXHR, statusText, errorThrown) => {

            //            // Get more information from LEADTOOLS
            //            var serviceError = lt.Documents.ServiceError.parseError(jqXHR, statusText, errorThrown);
            //            console.log(serviceError);

            //            // Show an alert about what the issue is
            //            var lines = [];
            //            lines.push("Document Viewer Error:");
            //            lines.push(serviceError.message);
            //            lines.push(serviceError.detail);
            //            lines.push("See console for details.")
            //            alert(lines.join("\n"));
            //        });



            //}