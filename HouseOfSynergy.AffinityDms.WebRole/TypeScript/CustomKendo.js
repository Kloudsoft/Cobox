var AffinityDms;
(function (AffinityDms) {
    var CustomKendo;
    (function (CustomKendo) {
        var KendoFunctions = (function () {
            function KendoFunctions() {
            }
            KendoFunctions.prototype.getPdf = function (ElementById, ElementByClass, FileNameWithoutExt, AppendDate) {
                if (ElementByClass == null) {
                    ElementByClass = "";
                }
                if (ElementById == null) {
                    ElementById = "";
                }
                if (FileNameWithoutExt == null) {
                    FileNameWithoutExt = "Print";
                }
                var date = new Date();
                if (AppendDate) {
                    FileNameWithoutExt += "_" + date.getDate() + "." + date.getMonth() + "." + date.getFullYear() + "_" + date.getUTCHours() + "." + date.getUTCMinutes();
                }
                FileNameWithoutExt += ".pdf";
                var A4Settings = new AffinityDms.Settings.PdfSettings();
                var pdfOptions;
                pdfOptions = A4Settings.GetA4GridPdfSettings($("#page-template").html(), FileNameWithoutExt, null, false);
                if (ElementByClass.trim() != "") {
                    if ($(ElementByClass) != null) {
                        kendo.drawing.drawDOM($(ElementByClass), pdfOptions).then(function (group) {
                            kendo.drawing.pdf.saveAs(group, FileNameWithoutExt);
                        });
                    }
                }
                else if (ElementById.trim() != "") {
                    if ($(ElementById) != null) {
                        kendo.drawing.drawDOM($(ElementById), pdfOptions).then(function (group) {
                            kendo.drawing.pdf.saveAs(group, FileNameWithoutExt);
                        });
                    }
                }
            };
            return KendoFunctions;
        }());
        CustomKendo.KendoFunctions = KendoFunctions;
        var KendoTreeViewEvents = (function () {
            function KendoTreeViewEvents() {
            }
            return KendoTreeViewEvents;
        }());
        CustomKendo.KendoTreeViewEvents = KendoTreeViewEvents;
        var TreeViewInformation = (function () {
            function TreeViewInformation() {
                this.Events = new KendoTreeViewEvents();
            }
            return TreeViewInformation;
        }());
        CustomKendo.TreeViewInformation = TreeViewInformation;
    })(CustomKendo = AffinityDms.CustomKendo || (AffinityDms.CustomKendo = {}));
})(AffinityDms || (AffinityDms = {}));
//# sourceMappingURL=CustomKendo.js.map