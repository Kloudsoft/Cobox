module HTML5Demos {

   export module Dialogs {
      export class SelectSourceDlg {
         // Create shortcuts for the dialog UI elements
         private dialogUI = {
            dialog: "#selectSourceDialog", /* The whole dialog */
            sourcesSelectElement: "#sources",
            OkBtn: "#selectSourceDialog_Ok"
         }

         // Events 
         private _OkClick: { (scanSource: string): void };
         set OkClick(value: { (scanSource: string): void }) {
            this._OkClick = value;
         }

         constructor() {
            this.Init();
         }

         private Init(): void {
            $(this.dialogUI.OkBtn).bind("click", $.proxy(this.OkBtn_Click, this));
         }

         public show(options: string): void {
            $(this.dialogUI.sourcesSelectElement).html(options);
            $(this.dialogUI.dialog).modal();
         }

         private OkBtn_Click(e: JQueryEventObject): void {
            if (this._OkClick != null)
               this._OkClick($(this.dialogUI.sourcesSelectElement).val());

            $(this.dialogUI.dialog).modal("hide");
         }
      }
   }
} 