﻿
    document.onload = designer.Run();
function CloseRightsModal(event) {
    $('#RightsModal').modal('hide');
}

$("#RightsModal").on("show.bs.modal", function (e) {
    var ViewType = document.getElementById("ViewType");
    var ViewId = document.getElementById("ViewId");
    if (ViewType != null && ViewId != null) {
        var valuetype = AffinityDms.Entities.Documents.prototype.CheckViewType(e.relatedTarget.getAttribute("data-type"));
        ViewType.value = valuetype;
        ViewId.value = e.relatedTarget.getAttribute("data-id");
        if ((ViewType.value != null && ViewType.value != "") && (ViewId.value != null && ViewId.value != "")) {
            $(this).find(".modal-body").load(("/TenantUsers/GetUserRightsList?id=" + ViewId.value + "&Type=" + ViewType.value));
        }
        else {
            alert("An error occurred while proccessing the request");
        }
    }
});
function UserSelection(event) {
    //href='#' data-toggle='modal' data-target='#UserSelectionModal'
    var anchor;
    if (event.currentTarget != null) {
        if (event.currentTarget instanceof HTMLAnchorElement) {
            anchor = event.currentTarget;
        }
    }
    if (anchor != null) {
        if (anchor.getAttribute("data-id") != null)
            var SelectedTemplate = document.getElementById("SelectedTemplate");
        SelectedTemplate.value = anchor.getAttribute("data-id");
        $("#UserSelectionModal").modal("show");
    }
}
$("#closeInviteExternalUserModalBtn").on("click", function (e) {
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.value = "";
});
$("#InviteUserEmail").keypress(function (event) {
    if (event.which || event.keyCode) { if ((event.which == 13) || (event.keyCode == 13)) { $("#InviteUserBtn").click(); return false; } else { return true; } }
});
$("#InviteExternalUserModal").on("show.bs.modal", function (e) {
    var EmailErrorDiv = document.getElementById("EmailErrorDiv");
    EmailErrorDiv.textContent = "";
    var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
    EmailSuccessDiv.textContent = "";
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.textContent = "";
});
function InviteExternalUser(event) {
    var anchor;
    if (event.currentTarget != null) {
        if (event.currentTarget instanceof HTMLAnchorElement) {
            anchor = event.currentTarget;
        }
    }
    if (anchor != null) {
        if (anchor.getAttribute("data-id") != null)
            var SelectedTemplate = document.getElementById("SelectedTemplate");
        SelectedTemplate.value = anchor.getAttribute("data-id");
        $("#InviteExternalUserModal").modal("show");
    }
}
$("#UserSelectionModal").on("show.bs.modal", function (e) {
    var SelectedTemplate = document.getElementById("SelectedTemplate");
    if (SelectedTemplate != null) {
        var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
        TenantUsersSelectionListGrid.textContent = "";
        $(this).find(".modal-body").load("/TenantForms/GetAllUsersForForm?templateId=" + SelectedTemplate.value);
    }
});



