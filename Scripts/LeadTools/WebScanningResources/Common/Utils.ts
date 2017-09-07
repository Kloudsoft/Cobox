module HTML5Demos {

   export module Utils {

      // Return value from Service Ping
      export class PingResponse {
         public message: string;
         public isLicenseChecked: boolean;
         public isLicenseExpired: boolean;
         public kernelType: string;
      }

      export class ServiceHelper {

         // Build our service url for the first time
         // Only needed once per life of application
         public static init(): void {
            ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
         }

         // Meant for host, such as "localhost:1234"
         private static _serviceHost: string = null;
         public static get serviceHost(): string { return ServiceHelper._serviceHost; }
         public static set serviceHost(value: string) {
            ServiceHelper._serviceHost = value;
            ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
         }

         // Meant for service name and path to it, such as "this/that/myService"
         private static _servicePath: string = null;
         public static get servicePath(): string { return ServiceHelper._servicePath; }
         public static set servicePath(value: string) {
            ServiceHelper._servicePath = value;
            ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
         }

         // Meant for path to service's API endpoints, such as "api" or "routing/controllers"
         private static _serviceApiPath: string = null;
         public static get serviceApiPath(): string { return ServiceHelper._serviceApiPath; }
         public static set serviceApiPath(value: string) {
            ServiceHelper._serviceApiPath = value;
            ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
         }


         // The combined serviceUri to use
         private static _serviceUri: string = null;
         public static get serviceUri(): string { return ServiceHelper._serviceUri; }

         private static buildServiceUrl(): string {
            // set up our service url
            var serviceUri: string = null;
            var location = window.location;
            var serviceHost = ServiceHelper._serviceHost;
            var servicePath = ServiceHelper._servicePath;
            var serviceApiPath = ServiceHelper._serviceApiPath;

            // get protocol, host, and port ( "http:" + // + "localhost" + : + "8080")
            if (serviceHost == null)
               serviceUri = location.protocol + "//" + location.host;
            else
               serviceUri = serviceHost;

            // get serviceApp  ( /path/to/service )
            if (servicePath == null) {
               var pathname = location.pathname;
               // If we are on a page instead of in a directory (we are at .../___.html),
               // then only keep the path to the directory
               var dotIndex = pathname.indexOf(".");
               if (dotIndex > -1) {
                  var lastSlashIndex = pathname.lastIndexOf("/");
                  if (lastSlashIndex > -1)
                     pathname = pathname.substring(0, lastSlashIndex);
               }
               serviceUri += ServiceHelper.clean(pathname); // starts with "/"
            }
            else {
               serviceUri += ServiceHelper.clean(servicePath);
            }

            // api path ( 'api' )
            if (serviceApiPath != null && serviceApiPath.length > 0)
               serviceUri += ServiceHelper.clean(serviceApiPath);

            return serviceUri;
         }

         private static clean(value: string): string {
            var length = value.length;
            if (value.charAt(0) !== "/") {
               //value = value.substring(1, length - 1);
               value = "/" + value;
               length += 1;
            }
            if (value.charAt(length - 1) === "/")
               value = value.substring(0, length - 1);
            return value;
         }

         // The default resource to send a request for to ensure the service is properly set up
         // Usually a "Ping" method or similar
         private static _serviceTestResource: string = "Test/Ping";
         public static get serviceTestResource(): string { return ServiceHelper._serviceTestResource; }
         public static set serviceTestResource(value: string) {
            ServiceHelper._serviceTestResource = value;
         }

         // Checks that the service is properly working
         // For use before anything else
         public static verifyService(): JQueryXHR {
            var serviceUrl = [ServiceHelper.serviceUri, ServiceHelper.serviceTestResource].join("/");
            var d: JQueryDeferred<Utils.PingResponse> = $.Deferred();
            return $.get(serviceUrl);
         }

         public static addParamsToUrl(url: string, params: {}): string {
            url += "?";
            var keys: string[] = Object.keys(params);
            keys.forEach((key: string, idx: number) => {
               url += key + "=" + params[key];
               if (idx != keys.length - 1) {
                  url += "&";
               }
            })
            return url;
         }
      }

      export class DemoHelper {

         public static byteArrayToArrayBuffer(array: number[]): ArrayBuffer {
            var bytes = new Uint8Array(array.length);
            array.forEach((val, i) => {
               bytes[i] = val;
            })
            return bytes.buffer;
         }

         public static createThumbnailCanvas(originalCanvas: HTMLCanvasElement, thumbnailCanvasRef: HTMLCanvasElement, maxWidth: number, maxHeight: number): void {

            var scaleFactor = 1;
            if (originalCanvas.width > originalCanvas.height) {
               scaleFactor = maxWidth / originalCanvas.width;
            }
            else {
               scaleFactor = maxHeight / originalCanvas.height;
            }

            var scaleCanvas: HTMLCanvasElement = document.createElement('canvas');
            scaleCanvas.width = originalCanvas.width * scaleFactor;
            scaleCanvas.height = originalCanvas.height * scaleFactor;

            thumbnailCanvasRef.width = originalCanvas.width * scaleFactor;
            thumbnailCanvasRef.height = originalCanvas.height * scaleFactor;
            var thumbnailCtx: CanvasRenderingContext2D = <CanvasRenderingContext2D>thumbnailCanvasRef.getContext("2d");

            var scaleCtx: CanvasRenderingContext2D = <CanvasRenderingContext2D>scaleCanvas.getContext("2d");
            scaleCtx.fillStyle = "rgba(0, 0, 0, 0)";
            scaleCtx.fillRect(0, 0, scaleCanvas.width, scaleCanvas.height);

            scaleCtx.scale(scaleFactor, scaleFactor);
            scaleCtx.drawImage(originalCanvas, 0, 0);

            thumbnailCtx.drawImage(scaleCanvas, 0, 0);
         }

         public static cloneCanvas(oldCanvas: HTMLCanvasElement): HTMLCanvasElement {
            var newCanvas: HTMLCanvasElement = document.createElement('canvas');
            var context: CanvasRenderingContext2D = <CanvasRenderingContext2D>newCanvas.getContext('2d');

            newCanvas.width = oldCanvas.width;
            newCanvas.height = oldCanvas.height;

            context.drawImage(oldCanvas, 0, 0);

            return newCanvas;
         }

         public static isTouchDevice(): boolean {
            try {
               document.createEvent('TouchEvent');
               return 'ontouchstart' in document.documentElement;
            }
            catch (ex) {
               return false;
            }
         }

         public static touchScroll(div: HTMLDivElement) {
            if (DemoHelper.isTouchDevice()) {
               div.addEventListener('touchstart', (e: any) => DemoHelper.touchScrollStart(e), false);
               div.addEventListener('touchmove', (e: any) => DemoHelper.touchScrollMove(e), false);
            }
         }

         public static touchScrollStart(e: any): void {
            var targetTouchePageY = e.touches[0].pageY;
            var targetTouchePageX = e.touches[0].pageX;
            e.currentTarget._scrollStartPosY = e.currentTarget.scrollTop + targetTouchePageY;
            e.currentTarget._scrollStartPosX = e.currentTarget.scrollLeft + targetTouchePageX;
         }

         public static touchScrollMove(e: any): void {
            e.preventDefault();

            var targetTouchePageY = e.touches[0].pageY;
            var targetTouchePageX = e.touches[0].pageX;
            e.currentTarget.scrollTop = e.currentTarget._scrollStartPosY - targetTouchePageY;
            e.currentTarget.scrollLeft = e.currentTarget._scrollStartPosX - targetTouchePageX;
         }

         public static checked(element: JQuery, check: boolean): any {
            check ? element.addClass("checked") : element.removeClass("checked");
         }

         public static initSidebars(): void {
            // For collapsing left sidebar
            if ($('.sidebar-left').length > 0) {
               $('#sidebarCollapseBtn-left').click(function () {
                  $('.sidebar-left').toggleClass('activeSidebar');
                  $('#sidebarCollapseBtn-left').toggleClass('collapseIcon');
               });
            }
            // For collapsing right sidebar
            if ($('.sidebar-right').length > 0) {
               $('#sidebarCollapseBtn-right').click(function () {
                  $('.sidebar-right').toggleClass('activeSidebar');
                  $('#sidebarCollapseBtn-right').toggleClass('collapseIcon');
               });
            }
         }

         public static resetSidebars(): void {
            // Reset left sidebar
            if ($('.sidebar-left').length > 0) {
               $('.sidebar-left').removeClass('activeSidebar');
               $('#sidebarCollapseBtn-left').removeClass('collapseIcon');
            }
            // Reset right sidebar
            if ($('.sidebar-right').length > 0) {
               $('.sidebar-right').removeClass('activeSidebar');
               $('#sidebarCollapseBtn-right').removeClass('collapseIcon');
            }
         }

         public static initCollapsiblePanels(): void {
            // change the status of collapsible panels headers             
            $('.panel-collapse').on('hidden.bs.collapse', function (e: JQueryEventObject) {
               $(e.currentTarget).prev().find(".collapse-expand").removeClass("toggleToCollapse");
               $(e.currentTarget).prev().find(".collapse-expand").addClass("toggleToExpand");
            });
            $('.panel-collapse').on('show.bs.collapse', function (e: JQueryEventObject) {
               $(e.currentTarget).prev().find(".collapse-expand").removeClass("toggleToExpand");
               $(e.currentTarget).prev().find(".collapse-expand").addClass("toggleToCollapse");
            });
         }

         public static selectText(textElement: HTMLTextAreaElement, startIndex: number, endIndex: number): void {
            if (textElement.setSelectionRange) {
               textElement.setSelectionRange(startIndex, endIndex);
            }
            else if (textElement.createTextRange) {
               var range = textElement.createTextRange();
               range.moveStart("character", startIndex);
               range.moveEnd("character", endIndex);
               range.select();
            }

            textElement.focus();
         }

         public static isValidURI(uri: string): boolean {
            var RegExp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
            if (RegExp.test(uri)) {
               return true;
            } else {
               return false;
            }
         }

         public static showRequestError(jqueryXHR: JQueryXHR, statusText: string, errorThrown: string): void {
            var body: any = null;
            var detail: string = null;
            try {
               body = JSON.parse(jqueryXHR.responseText);
               detail = "(" + body["detail"] + ")";
               console.error("Error: " + detail);
            }
            catch (e) {
               console.error("Could not parse JSON from Error")
               detail = "(Error " + jqueryXHR.status + ": " + jqueryXHR.statusText + ")"
            }

            var message: string = [
               "An error has occurred in the application.",
               detail
            ].join("\n");
            window.alert(message);
            jqueryXHR = null;
         }
      }
   }
}