var AffinityDms;
(function (AffinityDms) {
    var Entities;
    (function (Entities) {
        //export class TenantSubscriptionsViewModel {
        //    public TenantSubscriptions = new Array<Array<AffinityDms.Entities.TenantSubscription>();
        //    public Subscriptions = new Array<AffinityDms.Entities.Subscription>();
        //}
        var TenantSubscriptionsTS = (function () {
            function TenantSubscriptionsTS() {
            }
            TenantSubscriptionsTS.prototype.GetSubscriptionById = function (event) {
                var H_TenantId = document.getElementById("H_tenantId");
                var dd_subs = document.getElementById("dd_subscription");
                var id = dd_subs.options[dd_subs.selectedIndex].getAttribute("value");
                $.ajax({
                    type: "POST",
                    url: "../../MasterTenantSubscriptions/GetSubscriptionById",
                    data: JSON.stringify({ id: id, tenantid: H_TenantId.value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (response) {
                        if (response instanceof Object) {
                            var TenantSubscriptionsViewModel = response;
                            var tenantsubscriptionsarray = TenantSubscriptionsViewModel.TenantSubscriptions;
                            var subscriptionsarray = TenantSubscriptionsViewModel.Subscriptions;
                            if (tenantsubscriptionsarray == null) {
                                tenantsubscriptionsarray = new Array();
                            }
                            if (tenantsubscriptionsarray == null) {
                                subscriptionsarray = new Array();
                            }
                            var TotalNumberOfFormsAllowed = 0;
                            var NumberOfPagesAllowed = 0;
                            var NumberOfUsersAllowed = 0;
                            var NumberOfTemplatesAllowed = 0;
                            var NumberOfFormsUsed = 0;
                            var NumberOfPagesUsed = 0;
                            var NumberOfUsersUsed = 0;
                            var NumberOfTemplatesUsed = 0;
                            //=================================================
                            //  START: New Subscription 
                            //=================================================
                            var new_Pages = document.getElementById("new_Pages");
                            var new_Forms = document.getElementById("new_Forms");
                            var new_Templates = document.getElementById("new_Templates");
                            var new_Users = document.getElementById("new_Users");
                            var new_PagesUsed = document.getElementById("new_PagesUsed");
                            var new_Branding = document.getElementById("new_Branding");
                            var new_Scanning = document.getElementById("new_Scanning");
                            if ((new_Pages != null) && (new_Forms != null) && (new_Users != null) && (new_Templates != null) && (new_Branding != null) && (new_Scanning != null) && (TenantSubscriptionsViewModel != null)) {
                                new_Pages.textContent = subscriptionsarray[0].NumberOfPagesAllowed.toString();
                                new_Forms.textContent = subscriptionsarray[0].NumberOfFormsAllowed.toString();
                                new_Templates.textContent = subscriptionsarray[0].NumberOfTemplatesAllowed.toString();
                                new_Users.textContent = subscriptionsarray[0].NumberOfUsersAllowed.toString();
                                new_PagesUsed.textContent = subscriptionsarray[0].NumberOfPagesUsed.toString();
                                var scanning = subscriptionsarray[0].AllowScanning;
                                var branding = subscriptionsarray[0].AllowBranding;
                                new_Branding.textContent = branding.toString();
                                new_Scanning.textContent = scanning.toString();
                            }
                            //=================================================
                            //  END: New Subscription 
                            //=================================================
                            //=================================================
                            //  START: Tenant Subscription Balanace
                            //=================================================
                            var balance_Pages = document.getElementById("balance_Pages");
                            var balance_Forms = document.getElementById("balance_Forms");
                            var balance_Templates = document.getElementById("balance_Templates");
                            var balance_Users = document.getElementById("balance_Users");
                            var balance_PagesUsed = document.getElementById("balance_PagesUsed");
                            var balance_Branding = document.getElementById("balance_Branding");
                            var balance_Scanning = document.getElementById("balance_Scanning");
                            if ((balance_Pages != null) && (balance_Forms != null) && (balance_Users != null) && (balance_Templates != null) && (balance_Branding != null) && (balance_Scanning != null) && (TenantSubscriptionsViewModel != null)) {
                                balance_Pages.textContent = tenantsubscriptionsarray[0].NumberOfPagesAllowed.toString();
                                balance_Forms.textContent = tenantsubscriptionsarray[0].NumberOfFormsAllowed.toString();
                                balance_Templates.textContent = tenantsubscriptionsarray[0].NumberOfTemplatesAllowed.toString();
                                balance_Users.textContent = tenantsubscriptionsarray[0].NumberOfUsersAllowed.toString();
                                balance_PagesUsed.textContent = tenantsubscriptionsarray[0].NumberOfPagesUsed.toString();
                                var scanning = tenantsubscriptionsarray[0].AllowScanning;
                                var branding = tenantsubscriptionsarray[0].AllowBranding;
                                balance_Branding.textContent = branding.toString();
                                balance_Scanning.textContent = scanning.toString();
                            }
                            //=================================================
                            //  END: New Subscription Balanace
                            //=================================================
                            //=================================================
                            //  START: Total  Subscription
                            //=================================================
                            var NumberOfPagesAllowed = subscriptionsarray[0].NumberOfPagesAllowed + tenantsubscriptionsarray[0].NumberOfPagesAllowed;
                            var NumberOfUsersAllowed = subscriptionsarray[0].NumberOfUsersAllowed + tenantsubscriptionsarray[0].NumberOfUsersAllowed;
                            var NumberOfTemplatesAllowed = subscriptionsarray[0].NumberOfTemplatesAllowed + tenantsubscriptionsarray[0].NumberOfTemplatesAllowed;
                            var NumberOfFormsAllowed = subscriptionsarray[0].NumberOfFormsAllowed + tenantsubscriptionsarray[0].NumberOfFormsAllowed;
                            var NumberOfPagesUsed = subscriptionsarray[0].NumberOfPagesUsed + tenantsubscriptionsarray[0].NumberOfPagesUsed;
                            //  var NumberOfUsersUsed = (<number>subscriptionsarray[0].NumberOfUsersUsed) + (<number>tenantsubscriptionsarray[0].NumberOfUsersUsed);
                            //   var NumberOfTemplatesUsed = (<number>subscriptionsarray[0].NumberOfTemplatesUsed) + (<number>tenantsubscriptionsarray[0].NumberOfTemplatesUsed);
                            var AllowScanning = subscriptionsarray[0].AllowScanning || tenantsubscriptionsarray[0].AllowScanning;
                            var AllowBranding = subscriptionsarray[0].AllowBranding || tenantsubscriptionsarray[0].AllowBranding;
                            var total_Pages = document.getElementById("total_Pages");
                            var total_Forms = document.getElementById("total_Forms");
                            var total_Templates = document.getElementById("total_Templates");
                            var total_Users = document.getElementById("total_Users");
                            var total_PagesUsed = document.getElementById("total_PagesUsed");
                            var total_Branding = document.getElementById("total_Branding");
                            var total_Scanning = document.getElementById("total_Scanning");
                            if ((total_Pages != null) && (total_Forms != null) && (total_Users != null) && (total_Templates != null) && (total_Branding != null) && (total_Scanning != null) && (TenantSubscriptionsViewModel != null)) {
                                total_Pages.textContent = NumberOfPagesAllowed.toString();
                                total_Forms.textContent = NumberOfFormsAllowed.toString();
                                total_Templates.textContent = NumberOfTemplatesAllowed.toString();
                                total_Users.textContent = NumberOfUsersAllowed.toString();
                                total_PagesUsed.textContent = NumberOfPagesUsed.toString();
                                var scanning = AllowScanning;
                                var branding = AllowBranding;
                                total_Branding.textContent = branding.toString();
                                total_Scanning.textContent = scanning.toString();
                            }
                        }
                        else if (response instanceof String) {
                            alert(response.toString());
                        }
                        else {
                            alert("Details Not Found");
                        }
                    },
                    error: function (responseText) {
                        alert("Oops an Error Occured");
                    }
                });
            };
            TenantSubscriptionsTS.prototype.ApplySubscription = function (event) {
                var H_TenantId = document.getElementById("H_tenantId");
                var dd_subs = document.getElementById("dd_subscription");
                var subsid = dd_subs.options[dd_subs.selectedIndex].getAttribute("value");
                var AllowScanning = document.getElementById("AllowScanning");
                var AllowBranding = document.getElementById("AllowBranding");
                AllowBranding;
                var AllowTemplateWorkflow = document.getElementById("AllowTemplateWorkflow");
                var StartDate = document.getElementById("StartDate");
                var ExpiryDate = document.getElementById("ExpiryDate");
                var divMessage = document.getElementById("divMessage");
                var divException = document.getElementById("divException");
                $.ajax({
                    type: "POST",
                    url: "../../MasterTenantSubscriptions/ApplySubscription",
                    data: JSON.stringify({ subscriptionid: subsid, tenantid: H_TenantId.value, Scanning: AllowScanning.checked, Branding: AllowBranding.checked, TemplateWorkflow: AllowTemplateWorkflow.checked, StartDate: StartDate.value.toString(), ExpiryDate: ExpiryDate.value.toString() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (response) {
                        if (response instanceof Object) {
                            //var response1 = response;
                            //response1.Time
                            //var tenantsubsarray = new Array<AffinityDms.Entities.TenantSubscription>();
                            //tenantsubsarray.push(response);
                            // parse JSON formatted date to javascript date object
                            for (var i = 0; i < response.length; i++) {
                                var startdate = new Date(parseInt(response[i].DateTimeStart.toString().substr(6)));
                                response[i].DateTimeStart = startdate;
                                var expiryate = new Date(parseInt(response[i].DateTimeExpires.toString().substr(6)));
                                response[i].DateTimeExpires = expiryate;
                                var time = new Date(parseInt(response[i].Time.toString().substr(6)));
                                response[i].Time = time;
                            }
                            // format display date (e.g. 04/10/2012)
                            // var displayDate = $.datepicker.formatDate("mm/dd/yy", date);
                            $("#TenantSubscriptionGrid").data("kendoGrid").dataSource.data(response);
                            divMessage.textContent = "Record has been saved successfully";
                            divException.textContent = "";
                        }
                        else if (response) {
                            divException.textContent = response;
                            divMessage.textContent = "";
                        }
                    },
                    error: function (responseText) {
                        alert("Oops an Error Occured");
                    }
                });
            };
            TenantSubscriptionsTS.prototype.ClearTenantSubscriptionSubmission = function () {
                var ddsubscription = document.getElementById("btn_CreateSubsription");
                ddsubscription.style["display"] = "";
                var ddsubscription = document.getElementById("subscription");
                ddsubscription.style["display"] = "none";
                var ddsubscription = document.getElementById("btn_ApplySubscription");
                ddsubscription.style["display"] = "none";
                var cancelbtn = document.getElementById("btn_CancelSubsription");
                cancelbtn.style["display"] = "none";
                //==========================
                var new_Pages = document.getElementById("new_Pages");
                new_Pages.textContent = "";
                var new_Forms = document.getElementById("new_Forms");
                new_Forms.textContent = "";
                var new_Templates = document.getElementById("new_Templates");
                new_Templates.textContent = "";
                var new_Users = document.getElementById("new_Users");
                new_Users.textContent = "";
                var new_PagesUsed = document.getElementById("new_PagesUsed");
                new_PagesUsed.textContent = "";
                var new_Branding = document.getElementById("new_Branding");
                new_Branding.textContent = "";
                var new_Scanning = document.getElementById("new_Scanning");
                new_Scanning.textContent = "";
                var balance_Pages = document.getElementById("balance_Pages");
                balance_Pages.textContent = "";
                var balance_Forms = document.getElementById("balance_Forms");
                balance_Forms.textContent = "";
                var balance_Templates = document.getElementById("balance_Templates");
                balance_Templates.textContent = "";
                var balance_Users = document.getElementById("balance_Users");
                balance_Users.textContent = "";
                var balance_PagesUsed = document.getElementById("balance_PagesUsed");
                balance_PagesUsed.textContent = "";
                var balance_Branding = document.getElementById("balance_Branding");
                balance_Branding.textContent = "";
                var balance_Scanning = document.getElementById("balance_Scanning");
                balance_Scanning.textContent = "";
                var total_Pages = document.getElementById("total_Pages");
                total_Pages.textContent = "";
                var total_Forms = document.getElementById("total_Forms");
                total_Forms.textContent = "";
                var total_Templates = document.getElementById("total_Templates");
                total_Templates.textContent = "";
                var total_Users = document.getElementById("total_Users");
                total_Users.textContent = "";
                var total_PagesUsed = document.getElementById("total_PagesUsed");
                total_PagesUsed.textContent = "";
                var total_Branding = document.getElementById("total_Branding");
                total_Branding.textContent = "";
                var total_Scanning = document.getElementById("total_Scanning");
                total_Scanning.textContent = "";
            };
            return TenantSubscriptionsTS;
        }());
        Entities.TenantSubscriptionsTS = TenantSubscriptionsTS;
    })(Entities = AffinityDms.Entities || (AffinityDms.Entities = {}));
})(AffinityDms || (AffinityDms = {}));
//# sourceMappingURL=TenantSubscriptionTS.js.map