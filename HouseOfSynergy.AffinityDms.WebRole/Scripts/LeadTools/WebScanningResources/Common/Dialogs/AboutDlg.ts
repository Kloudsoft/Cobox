module HTML5Demos {

   export module Dialogs {

      export class AboutDlg {
         // Create shortcuts for the dialog UI elements
         private dialogUI = {
            dialog: "#aboutDialog", /* The whole dialog */
            demoNameLable: "#demoName"
         };

         constructor(demoName: string) {
            $(this.dialogUI.demoNameLable).text(demoName);
         }

         public show(): void {
            $(this.dialogUI.dialog).modal();
         }
      }
   }
}
