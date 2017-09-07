module AffinityDms.CustomKendo {
    export class KendoFunctions {
        constructor() { }
        public getPdf(ElementById: string, ElementByClass: string, FileNameWithoutExt: string, AppendDate: boolean): any {
            if (ElementByClass == null) { ElementByClass = ""; }
            if (ElementById == null) { ElementById = ""; }
            if (FileNameWithoutExt == null) { FileNameWithoutExt = "Print"; }

            var date = new Date();
            if (AppendDate) { FileNameWithoutExt += "_" + date.getDate() + "." + date.getMonth() + "." + date.getFullYear() + "_" + date.getUTCHours() + "." + date.getUTCMinutes(); }
            FileNameWithoutExt += ".pdf";
            var A4Settings = new AffinityDms.Settings.PdfSettings();
            var pdfOptions: any;
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
        }
    }
    export class KendoTreeViewEvents {
        public onChange: string;
        public onSelect: string;
        public onCheck: string;
        public onCollapse: string;
        public onExpand: string;
        public onDragStart: string;
        public onDrag: string;
        public onDrop: string;
        public onDragEnd: string;
    }
    export class TreeViewInformation {
        public Name: string;
        public Template: string;
        public LoadOnDemand: boolean;
        public Expanded: boolean;
        public Events: KendoTreeViewEvents = new KendoTreeViewEvents();
    }

}