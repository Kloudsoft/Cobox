module HTML5Demos {

   export module Dialogs {

      export class LoadingDlg {
         // Create shortcuts for the dialog UI elements
         private dialogUI = {
            dialog: "#loadingDialog", /* The whole dialog */
            processTextLable: "#processText"
         };

         public show(processText: string): void {
            $(this.dialogUI.processTextLable).text(processText);
            $(this.dialogUI.dialog).modal();
         }

         public processing(processText: string) {
            // To change process text , while the dialog is already shown
            $(this.dialogUI.processTextLable).text(processText);
         }

         public hide(): void {
            $(this.dialogUI.dialog).modal("hide");
         }
      }
   }
}
