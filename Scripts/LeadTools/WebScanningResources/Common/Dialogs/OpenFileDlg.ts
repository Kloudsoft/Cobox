module HTML5Demos {

   export module Dialogs {

      export class OpenFileDlg {
         // Create shortcuts for the dialog UI elements 
         private dialogUI = {
            dialog: "#openFileDialog", /* The whole dialog */
            goBtn: "#goBtn", fileSelectElement: "#fileSelect",
            urlDiv: "#urlDiv", fileUrlInput: "#fileUrl",
            imageUrlHelpLabel: "#imageUrlHelpLabel"
         };

         // Events 
         private _goClick: { (url: string): void };
         private _fileSelect: { (selectedIndex: number): void };

         constructor() {
            this.Init();
         }

         private Init(): void {
            $(this.dialogUI.fileSelectElement).prop("selectedIndex", 0);
            $(this.dialogUI.goBtn).bind("click", $.proxy(this.goBtn_Click, this));
            $(this.dialogUI.fileSelectElement).bind("change", $.proxy(this.fileSelectElement_SelectedIndexChanged, this));
         }

         private showUrlDiv(show: boolean): void {
            show ? $(this.dialogUI.urlDiv).show(0) : $(this.dialogUI.urlDiv).hide();
         }

         public show(): void {
            //if (this._useRasterService) {
            //   if (!this._rasterServiceFound)
            //      $(this.dialogUI.imageUrlHelpLabel).text("LEADTOOLS REST Raster Service was not found. Only file formats supported by the browser will be loaded");
            //}

            $(this.dialogUI.dialog).modal();
         }

         // Events mutators
         set goClick(value: { (url: string): void }) {
            this._goClick = value;
         }

         set fileSelect(value: { (selectedIndex: number): void }) {
            this._fileSelect = value;
         }

         private goBtn_Click(e: JQueryEventObject): void {
            if (this._goClick != null)
               // fire the goClick event , and pass the url
               this._goClick($(this.dialogUI.fileUrlInput).val());
         }

         private fileSelectElement_SelectedIndexChanged(e: JQueryEventObject): void {
            var fileSelect: HTMLSelectElement = <HTMLSelectElement>document.getElementById("fileSelect");
            if (fileSelect.value == "Enter URL") {
               this.showUrlDiv(true);
            }
            else {
               this.showUrlDiv(false);

               if (this._fileSelect != null)
                  // fire file select event , and pass the selected index
                  this._fileSelect(fileSelect.selectedIndex);

               $(this.dialogUI.dialog).modal("hide");
            }
         }
      }
   }
}
