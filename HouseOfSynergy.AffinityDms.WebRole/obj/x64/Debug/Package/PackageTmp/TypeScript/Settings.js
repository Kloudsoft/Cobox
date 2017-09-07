/// <reference path="../Scripts/typings/kendo-ui/kendo-ui.d.ts" />
var AffinityDms;
(function (AffinityDms) {
    var Settings;
    (function (Settings) {
        (function (Constants) {
            ///Error And Success Messages Timer
            Constants[Constants["PopUpNotificationTimer"] = 10000] = "PopUpNotificationTimer";
        })(Settings.Constants || (Settings.Constants = {}));
        var Constants = Settings.Constants;
        var PdfSettings = (function () {
            function PdfSettings() {
            }
            PdfSettings.prototype.GetA4GridPdfSettings = function (pdfTemplateHtmlString, fileName, proxyUrl, forceProxy) {
                var A4PdfOptions = {
                    allPages: true,
                    avoidLinks: true,
                    date: new Date(),
                    fileName: fileName,
                    forceProxy: false,
                    landscape: false,
                    margin: "2cm",
                    paperSize: "A4",
                    repeatHeaders: true,
                    scale: 0.1,
                    template: pdfTemplateHtmlString,
                };
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
            };
            return PdfSettings;
        }());
        Settings.PdfSettings = PdfSettings;
    })(Settings = AffinityDms.Settings || (AffinityDms.Settings = {}));
})(AffinityDms || (AffinityDms = {}));
//AffinityDms.Settings.Constants.PopUpNotificationTimer[AffinityDms.Settings.Constants.PopUpNotificationTimer.toString()] 
//# sourceMappingURL=Settings.js.map