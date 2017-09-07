module HTML5Demos {

   export module Dialogs {
      export class ErrorDlg {
         // Create shortcuts for the dialog UI elements
         private dialogUI = {
            dialog: "#errorDialog", /* The whole dialog */
            errorDetailsSpan: "#errorDetails",
            downloadApplicationAnchorElement: "#downloadApplication",
            troubleshootingGuideAnchor: "#troubleshootingGuide"
         }

         constructor(isWindowsEnvironment: boolean) {
            if (isWindowsEnvironment) {
               $(this.dialogUI.downloadApplicationAnchorElement).attr("href", "Leadtools.WebScanning.Setup.msi");
               if (lt.LTHelper.browser == lt.LTBrowser.edge) {
                  $(this.dialogUI.troubleshootingGuideAnchor).show();
               }
               $(this.dialogUI.errorDetailsSpan).text("Failed to reach Scanning Service. Please make sure that the Scan Application exe is running");
            } else {
               $(this.dialogUI.downloadApplicationAnchorElement).attr("href", "ltwebscanning-19-0.noarch.rpm");
               $(this.dialogUI.errorDetailsSpan).text("Failed to reach Scanning Application. Please make sure that the Java Scanning Application  is running");
               $(this.dialogUI.troubleshootingGuideAnchor).show();
            }
         }

         public show(): void {
            $(this.dialogUI.dialog).modal();
         }
      }
   }
} 