var HTML5Demos;
(function (HTML5Demos) {
    var Utils;
    (function (Utils) {
        // Return value from Service Ping
        var PingResponse = (function () {
            function PingResponse() {
            }
            return PingResponse;
        }());
        Utils.PingResponse = PingResponse;
        var ServiceHelper = (function () {
            function ServiceHelper() {
            }
            // Build our service url for the first time
            // Only needed once per life of application
            ServiceHelper.init = function () {
                ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
            };
            Object.defineProperty(ServiceHelper, "serviceHost", {
                get: function () { return ServiceHelper._serviceHost; },
                set: function (value) {
                    ServiceHelper._serviceHost = value;
                    ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(ServiceHelper, "servicePath", {
                get: function () { return ServiceHelper._servicePath; },
                set: function (value) {
                    ServiceHelper._servicePath = value;
                    ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(ServiceHelper, "serviceApiPath", {
                get: function () { return ServiceHelper._serviceApiPath; },
                set: function (value) {
                    ServiceHelper._serviceApiPath = value;
                    ServiceHelper._serviceUri = ServiceHelper.buildServiceUrl();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(ServiceHelper, "serviceUri", {
                get: function () { return ServiceHelper._serviceUri; },
                enumerable: true,
                configurable: true
            });
            ServiceHelper.buildServiceUrl = function () {
                // set up our service url
                var serviceUri = null;
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
            };
            ServiceHelper.clean = function (value) {
                var length = value.length;
                if (value.charAt(0) !== "/") {
                    //value = value.substring(1, length - 1);
                    value = "/" + value;
                    length += 1;
                }
                if (value.charAt(length - 1) === "/")
                    value = value.substring(0, length - 1);
                return value;
            };
            Object.defineProperty(ServiceHelper, "serviceTestResource", {
                get: function () { return ServiceHelper._serviceTestResource; },
                set: function (value) {
                    ServiceHelper._serviceTestResource = value;
                },
                enumerable: true,
                configurable: true
            });
            // Checks that the service is properly working
            // For use before anything else
            ServiceHelper.verifyService = function () {
                var serviceUrl = [ServiceHelper.serviceUri, ServiceHelper.serviceTestResource].join("/");
                var d = $.Deferred();
                return $.get(serviceUrl);
            };
            ServiceHelper.addParamsToUrl = function (url, params) {
                url += "?";
                var keys = Object.keys(params);
                keys.forEach(function (key, idx) {
                    url += key + "=" + params[key];
                    if (idx != keys.length - 1) {
                        url += "&";
                    }
                });
                return url;
            };
            // Meant for host, such as "localhost:1234"
            ServiceHelper._serviceHost = null;
            // Meant for service name and path to it, such as "this/that/myService"
            ServiceHelper._servicePath = null;
            // Meant for path to service's API endpoints, such as "api" or "routing/controllers"
            ServiceHelper._serviceApiPath = null;
            // The combined serviceUri to use
            ServiceHelper._serviceUri = null;
            // The default resource to send a request for to ensure the service is properly set up
            // Usually a "Ping" method or similar
            ServiceHelper._serviceTestResource = "Test/Ping";
            return ServiceHelper;
        }());
        Utils.ServiceHelper = ServiceHelper;
        var DemoHelper = (function () {
            function DemoHelper() {
            }
            DemoHelper.byteArrayToArrayBuffer = function (array) {
                var bytes = new Uint8Array(array.length);
                array.forEach(function (val, i) {
                    bytes[i] = val;
                });
                return bytes.buffer;
            };
            DemoHelper.createThumbnailCanvas = function (originalCanvas, thumbnailCanvasRef, maxWidth, maxHeight) {
                var scaleFactor = 1;
                if (originalCanvas.width > originalCanvas.height) {
                    scaleFactor = maxWidth / originalCanvas.width;
                }
                else {
                    scaleFactor = maxHeight / originalCanvas.height;
                }
                var scaleCanvas = document.createElement('canvas');
                scaleCanvas.width = originalCanvas.width * scaleFactor;
                scaleCanvas.height = originalCanvas.height * scaleFactor;
                thumbnailCanvasRef.width = originalCanvas.width * scaleFactor;
                thumbnailCanvasRef.height = originalCanvas.height * scaleFactor;
                var thumbnailCtx = thumbnailCanvasRef.getContext("2d");
                var scaleCtx = scaleCanvas.getContext("2d");
                scaleCtx.fillStyle = "rgba(0, 0, 0, 0)";
                scaleCtx.fillRect(0, 0, scaleCanvas.width, scaleCanvas.height);
                scaleCtx.scale(scaleFactor, scaleFactor);
                scaleCtx.drawImage(originalCanvas, 0, 0);
                thumbnailCtx.drawImage(scaleCanvas, 0, 0);
            };
            DemoHelper.cloneCanvas = function (oldCanvas) {
                var newCanvas = document.createElement('canvas');
                var context = newCanvas.getContext('2d');
                newCanvas.width = oldCanvas.width;
                newCanvas.height = oldCanvas.height;
                context.drawImage(oldCanvas, 0, 0);
                return newCanvas;
            };
            DemoHelper.isTouchDevice = function () {
                try {
                    document.createEvent('TouchEvent');
                    return 'ontouchstart' in document.documentElement;
                }
                catch (ex) {
                    return false;
                }
            };
            DemoHelper.touchScroll = function (div) {
                if (DemoHelper.isTouchDevice()) {
                    div.addEventListener('touchstart', function (e) { return DemoHelper.touchScrollStart(e); }, false);
                    div.addEventListener('touchmove', function (e) { return DemoHelper.touchScrollMove(e); }, false);
                }
            };
            DemoHelper.touchScrollStart = function (e) {
                var targetTouchePageY = e.touches[0].pageY;
                var targetTouchePageX = e.touches[0].pageX;
                e.currentTarget._scrollStartPosY = e.currentTarget.scrollTop + targetTouchePageY;
                e.currentTarget._scrollStartPosX = e.currentTarget.scrollLeft + targetTouchePageX;
            };
            DemoHelper.touchScrollMove = function (e) {
                e.preventDefault();
                var targetTouchePageY = e.touches[0].pageY;
                var targetTouchePageX = e.touches[0].pageX;
                e.currentTarget.scrollTop = e.currentTarget._scrollStartPosY - targetTouchePageY;
                e.currentTarget.scrollLeft = e.currentTarget._scrollStartPosX - targetTouchePageX;
            };
            DemoHelper.checked = function (element, check) {
                check ? element.addClass("checked") : element.removeClass("checked");
            };
            DemoHelper.initSidebars = function () {
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
            };
            DemoHelper.resetSidebars = function () {
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
            };
            DemoHelper.initCollapsiblePanels = function () {
                // change the status of collapsible panels headers             
                $('.panel-collapse').on('hidden.bs.collapse', function (e) {
                    $(e.currentTarget).prev().find(".collapse-expand").removeClass("toggleToCollapse");
                    $(e.currentTarget).prev().find(".collapse-expand").addClass("toggleToExpand");
                });
                $('.panel-collapse').on('show.bs.collapse', function (e) {
                    $(e.currentTarget).prev().find(".collapse-expand").removeClass("toggleToExpand");
                    $(e.currentTarget).prev().find(".collapse-expand").addClass("toggleToCollapse");
                });
            };
            DemoHelper.selectText = function (textElement, startIndex, endIndex) {
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
            };
            DemoHelper.isValidURI = function (uri) {
                var RegExp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
                if (RegExp.test(uri)) {
                    return true;
                }
                else {
                    return false;
                }
            };
            DemoHelper.showRequestError = function (jqueryXHR, statusText, errorThrown) {
                var body = null;
                var detail = null;
                try {
                    body = JSON.parse(jqueryXHR.responseText);
                    detail = "(" + body["detail"] + ")";
                    console.error("Error: " + detail);
                }
                catch (e) {
                    console.error("Could not parse JSON from Error");
                    detail = "(Error " + jqueryXHR.status + ": " + jqueryXHR.statusText + ")";
                }
                var message = [
                    "An error has occurred in the application.",
                    detail
                ].join("\n");
                window.alert(message);
                jqueryXHR = null;
            };
            return DemoHelper;
        }());
        Utils.DemoHelper = DemoHelper;
    })(Utils = HTML5Demos.Utils || (HTML5Demos.Utils = {}));
})(HTML5Demos || (HTML5Demos = {}));
//# sourceMappingURL=Utils.js.map