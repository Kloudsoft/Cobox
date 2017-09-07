/// <reference path="../../scripts/typings/kendo-ui/kendo-ui.d.ts" />
module AffinityDms.Entities {
    export class ScanSessions {
        constructor() { }
        public MergeBatch(event: any): any {

        }
        public GetScanSessionsInfo(event: any): any {
            var target = <HTMLElement>event.target;
            var type = target.getAttribute("data-type");
            var isClassifiedActive = false;
            $("#sessionHolderDiv").text("");
            if (type == "Classified") {
                isClassifiedActive = true;
                $("#TabUnClassified").removeClass("active");
                $("#TabClassified").addClass("active");
            }
            else {
                isClassifiedActive = false;
                $("#TabUnClassified").addClass("active");
                $("#TabClassified").removeClass("active");
            }
            var id = 0;
            if ($("#H_ScanSession") != null) {
                id = parseInt($("#H_ScanSession").val());
            }
            var a = document.createElement("a");
            if (isClassifiedActive) {
                a.text = "Classified";
                a.href = "http://localhost:14688/TenantScanSession/Index?id=1&IsClassifiedActive=true"

            }
            else
            {
                a.text = "Unclassified";
                a.href = "http://localhost:14688/TenantScanSession/Index?id=1&IsClassifiedActive=false"
            }
            $("#classificationTypeBreadcrumb").text("");
            $("#classificationTypeBreadcrumb").append(a);


            $("#sessionHolderDiv").load("../../TenantScanSession/GetPartialScanSession?id=" + id + "&IsClassifiedActive=" + isClassifiedActive, function (e) {
                
                if ($("#scanSessionDocCount") != null)
                {
                    $("#scanSessionDocCount").text($("#scanDocsInfo").attr("data-count"));
                } 
                if ($("#scanSessionDocStatus") != null) {
                    $("#scanSessionDocStatus").text($("#scanDocsInfo").attr("data-status"));
                }

            });
        }
        public ProccessDocumentsForManualClassification(event: any): any {
            var target = event.target;
            var templateid = parseInt(target.getAttribute("data-id"));
            var tempaltename = target.getAttribute("data-name");
            var ScanSessionCheckboxes:any = document.getElementsByClassName("ScanSessionCheckboxes");
            var documentIds: Array<number> = new Array();
            var counter = 0;
            if ($("#H_SelectedDocument") != null)
            {
                var documentId = parseInt($("#H_SelectedDocument").val());
                if (documentId > 0) {
                 documentIds.push(documentId);
                }
                else {
                    if (ScanSessionCheckboxes != null) {
                        for (var i = 0; i < ScanSessionCheckboxes.length; i++) {
                            if (ScanSessionCheckboxes[i].checked) {
                                documentIds.push(parseInt(ScanSessionCheckboxes[i].getAttribute("data-id")));
                            }
                        }
                    }
                }
                
            }

            var url = "../../TenantDocumentManualClassification/ProcessDocuments";
            $.ajax({
                url: url,
                type: "POST",
                dataType: "json",
                contentType: "application/json charset=utf-8",
                data: JSON.stringify({ documentIds: documentIds, templateId: templateid }),
                success: function (responseData: any) {
                    var templateModal: any = $("#template");

                    if (typeof (responseData) === "string")
                    {
                        templateModal.modal("hide");
                        $("#ScanSessionErrorMessageText").text(responseData);
                        if ($("ScanSessionErrorMessageText") != null) {
                            if ($("#ScanSessionErrorMessageText").text() != null || $("#ScanSessionErrorMessageText").text() != "") {
                                setTimeout(function () {
                                    $('#ScanSessionErrorMessage').fadeOut('fast');
                                    $("#ScanSessionErrorMessage").css("display", "none");
                                    $("#ScanSessionErrorMessageText").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                    }
                    if (responseData == false) {
                        responseData = "Unable to proccess the following request";
                        templateModal.modal("hide");
                        $("#ScanSessionErrorMessageText").text(responseData);
                        if ($("ScanSessionErrorMessageText") != null) {
                            if ($("#ScanSessionErrorMessageText").text() != null || $("#ScanSessionErrorMessageText").text() != "") {
                                setTimeout(function () {
                                    $('#ScanSessionErrorMessage').fadeOut('fast');
                                    $("#ScanSessionErrorMessage").css("display", "none");
                                    $("#ScanSessionErrorMessageText").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                    }
                    if (responseData == true) {
                        templateModal.modal("hide");
                    }

                },
                error: function (responseText) {
                    alert(responseText);
                }
            });
        }
    }
}