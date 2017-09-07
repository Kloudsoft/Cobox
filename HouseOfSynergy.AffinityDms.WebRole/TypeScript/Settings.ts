/// <reference path="../Scripts/typings/kendo-ui/kendo-ui.d.ts" />

module AffinityDms.Settings {
    export enum Constants {
        ///Error And Success Messages Timer
        PopUpNotificationTimer = 10000, // 10seconds

    }

    export class PdfSettings {
        constructor() { }
        public GetA4GridPdfSettings(pdfTemplateHtmlString: string, fileName: string, proxyUrl: string, forceProxy: boolean): kendo.ui.GridPdf {
            var A4PdfOptions: kendo.ui.GridPdf = {
                allPages : true,
                avoidLinks : true,
                date : new Date(),
                fileName: fileName,
                forceProxy:false,
                landscape : false,
                margin : "2cm",
                paperSize : "A4",
                repeatHeaders: true,
                scale : 0.1,
                template : pdfTemplateHtmlString,
            }
            if (forceProxy) {
                A4PdfOptions.forceProxy = true;
            }
            if (proxyUrl != null) {
                A4PdfOptions.proxyURL = "";
            }
            //A4PdfOptions.subject = "";
            //A4PdfOptions.title = "";
            //A4PdfOptions.keywords = "";
            //A4PdfOptions.creator = "";
            // A4PdfOptions.author = "";
            return A4PdfOptions;
        }
    }
}
//AffinityDms.Settings.Constants.PopUpNotificationTimer[AffinityDms.Settings.Constants.PopUpNotificationTimer.toString()]