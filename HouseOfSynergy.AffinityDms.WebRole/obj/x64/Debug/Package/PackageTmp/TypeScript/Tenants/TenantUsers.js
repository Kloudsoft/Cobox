/// <reference path="../../scripts/typings/kendo-ui/kendo-ui.d.ts" />
var AffinityDms;
(function (AffinityDms) {
    var Entities;
    (function (Entities) {
        var TenantUsers = (function () {
            function TenantUsers() {
            }
            TenantUsers.prototype.SaveDepartment = function (event) {
                var DepartmentTextBx = document.getElementById("DepartmentTextBx");
                if (DepartmentTextBx != null) {
                    if (DepartmentTextBx.value != "" || DepartmentTextBx.value != " ") {
                        $.ajax({
                            type: 'GET',
                            url: '/TenantDepartment/AddDepartment',
                            data: { departmentName: DepartmentTextBx.value },
                            dataType: 'json',
                            success: function (data) {
                                var department = data;
                                if (typeof data === "string") {
                                    alert(data);
                                }
                                else if (!(typeof department === "Object")) {
                                    var departmentDropDown = document.getElementById("DepartmentId");
                                    var opt = departmentDropDown.options[departmentDropDown.selectedIndex];
                                    opt.selected = false;
                                    var option = document.createElement("option");
                                    option.id = department.Id.toString();
                                    option.value = department.Id.toString();
                                    option.textContent = department.Name;
                                    option.selected = true;
                                    departmentDropDown.appendChild(option);
                                    var DepartmentTextBx = document.getElementById("DepartmentTextBx");
                                    DepartmentTextBx.value = "";
                                    $("#closeDepartmentAddBtn").click();
                                }
                                //$.each(data, function (index, element) {
                                //    $('body').append($('<div>', {
                                //        text: element.name
                                //    }));
                                //});
                            }
                        });
                    }
                    else {
                        alert("Department can not be null");
                    }
                }
            };
            TenantUsers.prototype.RemoveUser = function (userId, userGridId) {
                $("#SelectedUser").val(userId);
                $.ajax({
                    type: 'GET',
                    url: '/TenantUser/RemoveUser',
                    data: { id: userId },
                    dataType: 'json',
                    success: function (data) {
                        if (typeof (data) === "string") {
                            if ($("#UsersErrorMessage") != null) {
                                $("#UsersErrorMessageText").text(data);
                                $("#UsersErrorMessage").css("display", "");
                                setTimeout(function () {
                                    $('#UsersErrorMessage').fadeOut('fast');
                                    $("#UsersErrorMessage").css("display", "none");
                                    $("#UsersErrorMessageText").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                        else if (typeof (data) === "boolean") {
                            if (data == true) {
                                var id = parseInt($("#SelectedUser").val());
                                var elem = document.getElementById("item[" + id + "].Id");
                                if (elem != null) {
                                    var td = elem.parentElement;
                                    var tr = td.parentElement;
                                    tr.parentElement.removeChild(tr);
                                }
                            }
                        }
                    }
                });
            };
            return TenantUsers;
        }());
        Entities.TenantUsers = TenantUsers;
    })(Entities = AffinityDms.Entities || (AffinityDms.Entities = {}));
})(AffinityDms || (AffinityDms = {}));
//# sourceMappingURL=TenantUsers.js.map