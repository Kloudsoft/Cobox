
function RenderTreeViewModal(event) {
    var anchorTagDataId = event.currentTarget.getAttribute("data-id");
    if (anchorTagDataId != null) {
        var doc = document.getElementById("folderTreeViewModal");
        var hdocId = document.getElementById("hdocId");
        hdocId.value = anchorTagDataId;
        $("#folderTreeViewModal").modal("show");
        ////==============AUTO FIND DOCUMENT FOLDER ID==================
        ////data-folderid
        //var parentChilds = event.currentTarget.parentElement.parentElement.childNodes;
        //for (var i = 0; i < parentChilds.length; i++) {
        //    var folder = parentChilds[i];
        //    if (folder != null) {
        //        if (folder instanceof HTMLTableCellElement) {
        //            if (folder.childNodes[0] instanceof HTMLSpanElement) {
        //                var folderid = folder.childNodes[0].getAttribute("data-folderid");
        //                if (folderid != null) {
        //                    if (folderid > 0) {
        //                        var CurrentFolderId = document.getElementById("CurrentFolderId");
        //                        CurrentFolderId.value = folderid;
        //                        $("#folderTreeViewModal").modal("show");
        //                        break;
        //                    }
        //                    else {
        //                        alert("Unable to find the required folder");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        ////============================================================
    }
}
function LoadFolderTreeView(folderTreeViewModalBodyStr, folderTreeViewModalRowStr) {

    $.ajax({
        type: "POST",
        url: "/TenantDocumentsFolderWise/GetHieraricalTreeViewData",
        data: JSON.stringify({ folderId: null }),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            var folderId = 1;
            var folderTreeViewBody = document.getElementById(folderTreeViewModalBodyStr);
            folderTreeViewBody.textContent = "";
            var Row = document.createElement("div");
            var rowid = "";
            if (folderTreeViewModalRowStr != "") {
                Row.id = folderTreeViewModalRowStr;
                folderTreeViewBody.appendChild(Row);
                rowid = "#" + folderTreeViewModalRowStr;
            }
            else {
                rowid = "#" + folderTreeViewModalBodyStr;
            }
            $(rowid).kendoTreeView(
                    {
                        "select": OnSelectedFolderTreeViewModal,
                        "dataBound": onDataBoundFolderTreeViewModal,
                        "expanded": true,
                        "dataSource": processTable(response, "Id", "ParentId", folderId),
                        "schema": {
                            "model": { "id": "Id", "hasChildren": "HasChildren" }
                        },
                        "loadOnDemand": false,
                        "template": jQuery('#treeviewFolderPartiaTemplateKendo').html(),
                        "dataTextField": ["Name"]
                    });
        }
    });
}


function CloseFolderTreeViewModal(event) {
    $('#folderTreeViewModal').modal('hide');
}
function onDataBoundFolderTreeViewModal(e) {
    if (e != null) {
        if (e.node != null) {
            var ModalBodyId = document.getElementById("ModalBodyId");
            var ModalBody = document.getElementById(ModalBodyId.value);
            var rowid = "#" + ModalBody.childNodes[0].id;
            var FolderTreeViewObj = $(rowid);
            if (FolderTreeViewObj != null) {
                //$("#FolderTreeViewModal").data("kendoTreeView").expand(".k-item");
                var CurrentFolderId = document.getElementById("CurrentFolderId");
                var nodeId = CurrentFolderId.value;
                SelectNode(nodeId, FolderTreeViewObj)
            }
            //var div = this.element.find('div ul li:first').addClass('k-state-selected');
        }
    }

}
function ExpandSelectedNode(event) {
    var nodeId = event.getAttribute("data-id");
    var FolderTreeViewObj = $("#treeview");
    ExpandNode(nodeId, FolderTreeViewObj)
}
function ExpandNode(nodeId, FolderTreeViewObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        var dataItem = treeView.dataSource.get(nodeId);
        var node = treeView.findByUid(dataItem.uid);
        treeView.expand("li:first");
        treeView.expand(node);
        treeView.select(node);
        var CurrentFolderId = document.getElementById("CurrentFolderId");
        CurrentFolderId.value = nodeId;
        SetSelectedElement(nodeId);
    }
    else {
        alert("Unable to find the folder")
    }
}
function SelectNode(nodeId, FolderTreeViewObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        FolderTreeViewObj.data("kendoTreeView").expand(".k-item");
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        if (treeView != null) {
            var dataItem = treeView.dataSource.get(nodeId);
            if (dataItem != null) {
                var node = treeView.findByUid(dataItem.uid);
                if (node != null) {
                    treeView.expand("li:first");
                    treeView.expand(node);
                    treeView.select(node);
                }
            }
        }
    }
    //else {
    //    alert("Unable to find the folder")
    //}
}

$("#folderTreeViewModal").on("shown.bs.modal", function () {
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "folderTreeViewModalBody";
    LoadFolderTreeView("folderTreeViewModalBody", "folderTreeViewModal_row");

});

function CloseAddFolderModal(event) {
    $('#addFolderModal').modal('hide');
}
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
function ToggleDocumentSearch() {
    if (DivDocumentSearch.style.display === "none") {
        DivDocumentSearch.style.display = "block";
    }
    else {
        DivDocumentSearch.style.display = "none";
    }
}
function Clearbtn(event) {
    var submitbtn = document.getElementById("btnSubmitForm");
    var txtDocName = document.getElementById("documentname");
    var txtTempName = document.getElementById("templatename");
    var txtFolderName = document.getElementById("folderame");
    var txtUserTags = document.getElementById("usertags");
    var txtGlobalTags = document.getElementById("tagsglobal");
    var txtContent = document.getElementById("content");
    var txtStartDate = document.getElementById("startdate");
    var txtEndDate = document.getElementById("enddate");
    var txtSearchName = document.getElementById("txtSearchName");
    var MessageDiv = document.getElementById("MessageDiv");
    var btnAddSearchCriteria = document.getElementById("btnAddSearchCriteria");

    txtSearchName.style.display = "none";
    btnAddSearchCriteria.style.display = "none";
    MessageDiv.value = "";
    txtDocName.value = "";
    txtTempName.value = "";
    txtFolderName.value = "";
    txtUserTags.value = "";
    txtGlobalTags.value = "";
    txtContent.value = "";
    MessageDiv.value = "";
    txtStartDate.value = null;
    txtEndDate.value = null;

    var index = window.location.href.indexOf("?");

    window.location.href
        = ((index < 0)
        ? ("/Tenants/Documents")
        : ("/Tenants/Documents" + window.location.href.slice(window.location.href.indexOf("?"), window.location.href.length)));
}
function ToggleSearchName() {
    var txtSearchName = document.getElementById("txtSearchName");
    txtSearchName.style.display = "";
    var btnAddSearchCriteria = document.getElementById("btnAddSearchCriteria");
    btnAddSearchCriteria.style.display = "";

}
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
            var SelectedId = document.getElementById("SelectedId");
        SelectedId.value = anchor.getAttribute("data-id");
        $("#UserSelectionModal").modal("show");
    }
}
$("#UserSelectionModal").on("show.bs.modal", function (e) {
    var SelectedId = document.getElementById("SelectedId");
    if (SelectedId != null) {
        var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
        TenantUsersSelectionListGrid.textContent = "";
        $(this).find(".modal-body").load("/TenantDocuments/GetAllUsersForDocument?documentId=" + SelectedId.value);
    }
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
            var SelectedId = document.getElementById("SelectedId");
        SelectedId.value = anchor.getAttribute("data-id");
        $("#InviteExternalUserModal").modal("show");
    }
}
$("#InviteUserEmail").keypress(function (event) {
    if (event.which || event.keyCode) { if ((event.which == 13) || (event.keyCode == 13)) { $("#InviteUserBtn").click(); return false; } else { return true; } }
});
$("#closeInviteExternalUserModalBtn").on("click", function (e) {
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.value = "";
});
$("#InviteExternalUserModal").on("show.bs.modal", function (e) {
    var EmailErrorDiv = document.getElementById("EmailErrorDiv");
    EmailErrorDiv.textContent = "";
    var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
    EmailSuccessDiv.textContent = "";
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.textContent = "";
});


document.load = Imagedocument.BindPreviousSearch(" ");


//DocumentFolderWise.cshtml



function InviteExternalUser(event) {
    var anchor;
    if (event.currentTarget != null) {
        if (event.currentTarget instanceof HTMLAnchorElement) {
            anchor = event.currentTarget;
        }
    }
    if (anchor != null) {
        if (anchor.getAttribute("data-id") != null)
            var SelectedId = document.getElementById("SelectedId");
        SelectedId.value = anchor.getAttribute("data-id");
        $("#InviteExternalUserModal").modal("show");
    }
}

$("#closeInviteExternalUserModalBtn").on("click", function (e) {
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.value = "";
});
$("#InviteExternalUserModal").on("show.bs.modal", function (e) {
    var EmailErrorDiv = document.getElementById("EmailErrorDiv");
    EmailErrorDiv.textContent = "";
    var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
    EmailSuccessDiv.textContent = "";
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.textContent = "";
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
            var SelectedId = document.getElementById("SelectedId");
        SelectedId.value = anchor.getAttribute("data-id");
        $("#UserSelectionModal").modal("show");
    }
}
$("#UserSelectionModal").on("show.bs.modal", function (e) {
    var SelectedId = document.getElementById("SelectedId");
    if (SelectedId != null) {
        var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
        TenantUsersSelectionListGrid.textContent = "";
        $(this).find(".modal-body").load("/TenantDocuments/GetAllUsersForDocument?documentId=" + SelectedId.value);
    }
});


$("#UserAssignmentModal").on("shown.bs.modal", function () {
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "AssignUserRow";
    AffinityDms.Entities.Documents.prototype.GetIndexUserForAssignment("AssignUserRow");
});

$("#folderTreeViewModal").on("shown.bs.modal", function () {
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "folderTreeViewModalBody";
    LoadFolderTreeView("folderTreeViewModalBody", "folderTreeViewModal_row");

});

function onDataBoundFolderTreeViewModal(e) {
    if (e != null) {
        if (e.node != null) {
            var ModalBodyId = document.getElementById("ModalBodyId");
            var ModalBody = document.getElementById(ModalBodyId.value);
            var rowid = "#" + ModalBody.childNodes[0].id;
            var FolderTreeViewObj = $(rowid);
            if (FolderTreeViewObj != null) {
                //$("#FolderTreeViewModal").data("kendoTreeView").expand(".k-item");
                var CurrentFolderId = document.getElementById("CurrentFolderId");
                var nodeId = CurrentFolderId.value;
                SelectNode(nodeId, FolderTreeViewObj)
            }
            //var div = this.element.find('div ul li:first').addClass('k-state-selected');
        }
    }

}

function AssignForIndexing(event) {
    var anchorTagDataId = event.currentTarget.getAttribute("data-id");
    if (anchorTagDataId != null) {
        var hdocId = document.getElementById("hdocId");
        hdocId.value = anchorTagDataId;
        $("#UserAssignmentModal").modal("show");

    }
}
//function RenderTreeViewModal(event) {
//    var anchorTagDataId = event.currentTarget.getAttribute("data-id");
//    if (anchorTagDataId != null) {
//        var doc = document.getElementById("folderTreeViewModal");
//        var hdocId = document.getElementById("hdocId");
//        hdocId.value = anchorTagDataId;
//        $("#folderTreeViewModal").modal("show");

//    }
//}

function CloseAddFolderModal(event) {
    $('#addFolderModal').modal('hide');
}
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
function onDataBound(e) {
    //below Statement Expand all data in tree view
    var treeView = document.getElementById("treeview");
    if (treeView != null) {
        $("#treeview").data("kendoTreeView").expand(".k-item");
    }
}
$("#InviteUserEmail").keypress(function (event) {
    if (event.which || event.keyCode) { if ((event.which == 13) || (event.keyCode == 13)) { $("#InviteUserBtn").click(); return false; } else { return true; } }
});
function OnSelected(event) {
    var CurrentFolderId = document.getElementById("CurrentFolderId");
    var treeView = $("#treeview").data('kendoTreeView');

    //var dataItem = treeView.dataSource.get(CurrentFolderId.value);
    //var node = treeView.findByUid(dataItem.uid);
    //if (node != null)
    //{
    //    var li = node[0];
    //    li.setAttribute("aria-selected", "false");
    //    li.removeAttribute("id");
    //    //k-in k-state-selected
    //    for (var i = 0; i < li.childNodes.length; i++)
    //    {
    //        var chkdiv = li.childNodes[i]
    //        if (chkdiv instanceof HTMLDivElement)
    //        {
    //            for (var j = 0; j < chkdiv.childNodes.length; j++) {
    //                var chkspan = chkdiv.childNodes[j];
    //                if (chkspan instanceof HTMLSpanElement) {
    //                    chkspan.class = "k-in";
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}
    dataItem = this.dataItem(event.node);
    //node = treeView.findByUid(dataItem.uid);
    //if (node != null) {
    //    var li = node[0];
    //    li.setAttribute("aria-selected", "true");
    //    li.setAttribute("id", "treeview_tv_active");
    //    for (var i = 0; i < li.childNodes.length; i++) {
    //        var chkdiv = li.childNodes[i]
    //        if (chkdiv instanceof HTMLDivElement) {
    //            for (var j = 0; j < chkdiv.childNodes.length; j++) {
    //                var chkspan = chkdiv.childNodes[j];
    //                if (chkspan instanceof HTMLSpanElement) {
    //                    chkspan.class = "k-in k-state-selected";
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}
    CurrentFolderId.value = dataItem.id;
    var MoveFolderTo = document.getElementById("MoveFolderTo");
    MoveFolderTo.value = dataItem.id;
    SetSelectedElement(dataItem.id);
}

function ExpandSelectedNode(event) {
    var nodeId = event.getAttribute("data-id");
    var FolderTreeViewObj = $("#treeview");
    ExpandNode(nodeId, FolderTreeViewObj)
}
function ExpandNode(nodeId, FolderTreeViewObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        var dataItem = treeView.dataSource.get(nodeId);
        var node = treeView.findByUid(dataItem.uid);
        treeView.expand("li:first");
        treeView.expand(node);
        treeView.select(node);
        var CurrentFolderId = document.getElementById("CurrentFolderId");
        CurrentFolderId.value = nodeId;
        SetSelectedElement(nodeId);
    }
    else {
        alert("Unable to find the folder")
    }
}
function SelectNode(nodeId, FolderTreeViewObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        FolderTreeViewObj.data("kendoTreeView").expand(".k-item");
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        if (treeView != null) {
            var dataItem = treeView.dataSource.get(nodeId);
            if (dataItem != null) {
                var node = treeView.findByUid(dataItem.uid);
                if (node != null) {
                    treeView.expand("li:first");
                    treeView.expand(node);
                    treeView.select(node);
                }
            }
        }
    }
    //else {
    //    alert("Unable to find the folder")
    //}
}

//==============================================Upload Document Folder TreeView===================================//
$("#DocumentUploadModal").on("shown.bs.modal", function () {

    //========= Start clear status div when load popup=========//
    var divStatus = document.getElementById("divStatus");
    divStatus.textContent = "";
    //========= End clear status div when load popup=========//

    resetFileControl();

    //========= Start Rebind Treeview========================================//
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "DocumentUploadBody";
    LoadFolderTreeView("DocumentUploadBody", "DocumentUploadRow");
    //========= End Rebind Treeview==========================================//
});
$(document).on("ready", function () {
    $("#files").kendoUpload({
        async: {
            saveUrl: "../../TenantDocumentsFolderWise/UploadDocumentsToFolder",
            removeUrl: "../../TenantDocumentsFolderWise/RemoveDocumentsToFolder",
            autoUpload: true
        },
        success: onSuccess
    });
});

function onSuccess(e) {
    // Array with information about the uploaded files
    //var files = e.files;

    //if (e.operation == "upload") {
    //    alert("Successfully uploaded " + files.length + " files");
    //}
}

function CloseUploadDocumentModal(event) {

    //========= Start clear status div when load popup=========//
    var divStatus = document.getElementById("divStatus");
    divStatus.textContent = "";
    //========= End clear status div when load popup=========//

    resetFileControl();

    $('#DocumentUploadModal').modal('hide');
}

//===============================================End Upload Document Folder TreeView==============================//

$(document).ready(function () {
    var discourseTooltipUsersNames = document.getElementsByName("discourseTooltipUsers");
    for (var i = 0; i < discourseTooltipUsersNames.length; i++) {
        var id = "#" + discourseTooltipUsersNames[i].id;
        SetUserToolTip(id)
    }
});
function SetUserToolTip(tooltipIdHash) {
    $(tooltipIdHash).tooltip({
        placement: "auto bottom",
        html: "true",
        trigger: "hover",
        title: function () {
            return $(this).attr('title');
        },
    });
}
function onDiscourseListingGridBind(e) {
    var discourseTooltipUsersTemp = document.getElementsByName("discourseTooltipUsersTemp");
    if (discourseTooltipUsersTemp != null) {
        for (var a = 0; a < discourseTooltipUsersTemp.length; a++) {
            if (discourseTooltipUsersTemp[a] != null) {
                var id = discourseTooltipUsersTemp[a].getAttribute("data-id");
                var tooltipId = "discourseTooltipUsers" + id;
                var discourseTooltipUsers = document.getElementById(tooltipId);
                if (discourseTooltipUsers != null) {
                    var str = discourseTooltipUsersTemp[a].innerHTML.toString().replaceAll('"', '^').replaceAll("'", '"').replaceAll("^", "'");
                    document.getElementById(tooltipId).title = str;
                    var tooltipIdHash = '#' + tooltipId;
                    SetUserToolTip(tooltipIdHash);
                }
            }
        }
    }
}

function onDiscourseListingGridChange(e) {
    var grid = e.sender;
    $.map(this.select(), function (item) {
        var id = item.childNodes[0].textContent;
        window.location.href = "/TenantDiscourse/Index/" + id;
        return $(item).text();
    });
    //var data = this.dataItem(this.select());
    //var YourRowID = data.id;//IMP
    //var currentDataItem = grid.dataItem(this.select());
}


var documentDesigner;
$(document).ready(function (e) {
    documentDesigner = new AffinityDms.Entities.FormTestDesigner();
    var docViewer = new AffinityDms.Entities.DocumentViewer();
    docViewer.run();
})
function SaveIndexingValue(event) {
    $("#IndexConfirmationModal").modal("show");
}
$("#IndexConfirmationBtn").click(function (e) {
    var itemid = document.getElementsByName("item[0].Id");
    var itemDocId = document.getElementsByName("item[0].DocumentId");
    if (itemid != null) {
        $.ajax({
            type: "POST",
            url: "../../TenantDocumentCorrectiveIndex/ConfirmIndexing",
            data: JSON.stringify({ id: itemid[0].value, documentId: itemDocId[0].value }),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (response) {
                if (typeof (response) === "string") {
                    alert(response);
                }
                else if (response == true) {
                    window.location.href = "../../TenantDocumentsFolderWise/Index?ErrorMessage=''&SuccessMessage='Document indexing successfully submitted'";
                }
                else {
                    alert("Oops! An error occured");
                }
            },
            error: function (e) {
                alert("Oops! An error occured");
            }
        });
    }
    else {
        alert("No Indexes found");
    }

})


$(document).ready(function (e) {
    var docViewer = new AffinityDms.Entities.DocumentViewer();
    docViewer.run();
});
var discourses = new AffinityDms.Entities.Discourses();
function ViewDiscourseModal(event) {

    var id = event.target.getAttribute("data-id");
    var url = "../../TenantDiscourse/PartialDiscourse/" + id;
    $("#DiscourseModalContainer").text("");
    $("#DiscourseModalLabel").text("View Chat");
    $("#DiscourseModalContainer").load(url, function () {
        if ($("#discourseCommentEditor") != null) {
            if ($("#showDiscourseComment") != null) {
                $("#showDiscourseComment").hide();
            }
            $("#discourseCommentEditor").show();
            var eee = $("#CommentTextEditor").data("kendoEditor");
            //var editor = $('#CommentTextEditor');//.kendoEditor();
            if (eee != null) {
                setTimeout(function () { eee.focus(); }, 1000);
            }
        }
    });
    $("#DiscourseModal").modal({ backdrop: 'static' })
}
function CloseDiscourseModal(event) {
    $("#DiscourseModalContainer").text("");
    $("#DiscourseModal").modal("hide")
}


$(document).ready(function (e) {
    var docViewer = new AffinityDms.Entities.DocumentViewer();
    docViewer.run();
});
function ViewDiscourseModal(event) {

    var id = event.target.getAttribute("data-id");
    var url = "../../TenantDiscourse/PartialDiscourse/" + id;
    $("#DiscourseModalContainer").text("");
    $("#DiscourseModalContainer").load(url);
    $("#DiscourseModal").modal({ backdrop: 'static' })
}
function CloseDiscourseModal(event) {
    $("#DiscourseModalContainer").text("");
    $("#DiscourseModal").modal("hide")


}





$("#folderTreeViewModal").on("shown.bs.modal", function () {
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "folderTreeViewModalBody";
    LoadFolderTreeView("folderTreeViewModalBody", "folderTreeViewModal_row");

});

function onDataBoundFolderTreeViewModal(e) {
    if (e != null) {
        if (e.node != null) {
            var ModalBodyId = document.getElementById("ModalBodyId");
            var ModalBody = document.getElementById(ModalBodyId.value);
            var rowid = "#" + ModalBody.childNodes[0].id;
            var FolderTreeViewObj = $(rowid);
            if (FolderTreeViewObj != null) {
                //$("#FolderTreeViewModal").data("kendoTreeView").expand(".k-item");
                var CurrentFolderId = document.getElementById("CurrentFolderId");
                var nodeId = CurrentFolderId.value;
                SelectNode(nodeId, FolderTreeViewObj)
            }
            //var div = this.element.find('div ul li:first').addClass('k-state-selected');
        }
    }

}

function SelectNode(nodeId, FolderTreeViewObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        FolderTreeViewObj.data("kendoTreeView").expand(".k-item");
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        if (treeView != null) {
            var dataItem = treeView.dataSource.get(nodeId);
            if (dataItem != null) {
                var node = treeView.findByUid(dataItem.uid);
                if (node != null) {
                    treeView.expand("li:first");
                    treeView.expand(node);
                    treeView.select(node);
                }
            }
        }
    }
    //else {
    //    alert("Unable to find the folder")
    //}
}
function ExpandSelectedNode(event) {
    var nodeId = event.getAttribute("data-id");
    var FolderTreeViewObj = $("#treeview");
    ExpandNode(nodeId, FolderTreeViewObj)
}
function ExpandNode(nodeId, FolderTreeViewObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        var dataItem = treeView.dataSource.get(nodeId);
        var node = treeView.findByUid(dataItem.uid);
        treeView.expand("li:first");
        treeView.expand(node);
        treeView.select(node);
        var CurrentFolderId = document.getElementById("CurrentFolderId");
        CurrentFolderId.value = nodeId;
        SetSelectedElement(nodeId);
    }
    else {
        alert("Unable to find the folder")
    }
}





    function ShowDocumentRenameModal(e) {
        var target = e.target;
        var id = target.getAttribute("data-id");
        var name = target.getAttribute("data-name");
        var modalId = "#DocumentRenameModal";
        if ($("#DocumentRenameModalSubmitBtn") != null) {
            $("#DocumentRenameModalSubmitBtn").attr("data-id", id);
            $("#DocumentRenameModalSubmitBtn").attr("data-name", name);
            $("#DocumentRenameModalTextBox").attr("placeholder", name).val(name);

            $("#DocumentRenameModalErrorHeader").hide();
            $("#DocumentRenameModalErrorHeaderDiv").text("");
            $("#DocumentRenameModal").modal("show");
        }
    }
$("#DocumentRenameModal").on("shown.bs.modal", function (e) {
    $("#DocumentRenameModalTextBox").focus().select();
});
//addFolderModalError
function DisplayVersionsModal(docId) {
    if ($("#documentHistoryModal") != null) {
        $("#documentHistoryModal").modal({ backdrop: 'static' });
        $("#documentHistoryModalBody").text("");
        var url = "/TenantDocumentsFolderWise/GetDocumentVersions?id=" + docId;
        $("#documentHistoryModalBody").load(url);


    }
}
function instantDocumentViewer(event) {
    var div6 = document.getElementById("divid6");
    if (div6 != null) {
        AffinityDms.Entities.Documents.prototype.ResetInstantDocumentViewer();
        var docViewer = new AffinityDms.Entities.DocumentViewer();
        $("#div6").removeClass("show");
        $("#div6").addClass("hide");

        var id = event.target.getAttribute("data-id");
        var type = event.target.getAttribute("data-type");
        var name = event.target.getAttribute("data-name");
        var ReatedDocumentsContainer = document.getElementById("ReatedDocumentsContainer");
        if (ReatedDocumentsContainer != null) {
            $("#ReatedDocumentsContainer").load("/TenantDocumentsFolderWise/GetRelatedDocuments?id=" + id);
        }
        var DocumentStatusContainer = document.getElementById("DocumentStatusContainer");
        if (DocumentStatusContainer != null) {
            $("#DocumentStatusContainer").load("/Document/GetDocumentStatus?id=" + id)
        }

        var filename = "";
        if (name != null)
        {
            filename = name.replace("." + name.split('.').pop(), "");
        }
        $("#documentNameDiv6").text(filename);
        var DocViewerType = document.getElementById("DocViewerType");
        DocViewerType.setAttribute("data-id", id);
        // var url = "/TenantDocumentViewer/GetPartialDocumentView?id=" + id + "&type=" + type;
        /*$('#divid2').removeClass('show');
          $('#divid2').addClass('hide');*/
        //$('#divid3').addClass('class405');
        $('#divid5').addClass('col-xs-6');
        $('#divid5').css("width", 420);
        $('#divid6').css("left", 885);
        $('#divid4').addClass('class1');
        $('#divid4').removeClass('class2');
        $('#divid6').addClass('show col-xs-6');
        $('#side-content').animate({ scrollLeft: $('#side-content').offset().left }, 1500);
        // $('#side-content').scrollLeft(2500);
        /*$('#divid5').removeClass('col-sm-12');
          $('#divid6').removeClass('col-xs-12');*/
        //  $("#documentImageDiv6").load(url);
        //$('#divid2').removeClass('show');
        //$('#divid2').addClass('hide');
        //$('#divid4').removeClass('show');
        //$('#divid4').addClass('hide');
        //$('#divid3').removeClass('show');
        //$('#divid3').addClass('hide');
        //$('#divid5').removeClass('show');
        //$('#divid5').addClass('hide');
        //$('#divid6').removeClass('show');
        //$('#divid6').addClass('hide');
        docViewer.run();
    }
}
function UpdateTreeViewSelection(value) {
    var MoveFolderTo = document.getElementById("MoveFolderTo");
    var CurrentFolderId = document.getElementById("CurrentFolderId");
    MoveFolderTo.value = value;
    CurrentFolderId.value = value;
}
function parentNodesSelected(event) {
    AffinityDms.Entities.Documents.prototype.ResetInstantDocumentViewer();
    var id = event.target.closest("a").getAttribute("data-id");
    var Name = event.target.closest("a").getAttribute("data-Name");

    UpdateTreeViewSelection(id);
    if ($("#DocumentUploadModalFolderName") != null) { $("#DocumentUploadModalFolderName").text(Name); }
    SetSelectedElement(id);
    $("#divid5").removeClass("hide");
    $("#divid5").addClass("show");
}


function InviteExternalUser(event) {
    var anchor;
    if (event.currentTarget != null) {
        if (event.currentTarget instanceof HTMLAnchorElement) {
            anchor = event.currentTarget;
        }
    }
    if (anchor != null) {
        if (anchor.getAttribute("data-id") != null)
            var SelectedId = document.getElementById("SelectedId");
        SelectedId.value = anchor.getAttribute("data-id");
        $("#InviteExternalUserModal").modal("show");
    }
}

$("#closeInviteExternalUserModalBtn").on("click", function (e) {
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.value = "";
});
$("#addFolderModal").on("show.bs.modal", function (e) {
    $("#addFolderModalError").hide();
    $("#addFolderModalError").text("");
    $("#addFolderModalSuccess").hide();
    $("#addFolderModalSuccess").text("");

})
$("#addFolderModal").on("shown.bs.modal", function (e) {
    $("#folderNameTxtbx").val("")
    $("#folderNameTxtbx").focus().select();
});
$("#InviteExternalUserModal").on("show.bs.modal", function (e) {
    var EmailErrorDiv = document.getElementById("EmailErrorDiv");
    EmailErrorDiv.textContent = "";
    var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
    EmailSuccessDiv.textContent = "";
    var InviteUserEmail = document.getElementById("InviteUserEmail");
    InviteUserEmail.textContent = "";
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
            var SelectedId = document.getElementById("SelectedId");
        SelectedId.value = anchor.getAttribute("data-id");
        $("#UserSelectionModal").modal("show");
    }
}
$("#UserSelectionModal").on("show.bs.modal", function (e) {
    var SelectedId = document.getElementById("SelectedId");
    if (SelectedId != null) {
        var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
        TenantUsersSelectionListGrid.textContent = "";
        $(this).find(".modal-body").load("/TenantDocuments/GetAllUsersForDocument?documentId=" + SelectedId.value);
    }
});
function OnSelectedFolderTreeViewModal(event) {
    var MoveFolderTo = document.getElementById("MoveFolderTo");
    var dataItem = this.dataItem(event.node);
    var nodeId = dataItem.id;
    if (nodeId == null || nodeId == "") {
        nodeId = dataItem.Id
    }
    MoveFolderTo.value = nodeId;
    // MoveFolderTo.setAttribute("data-Name", dataItem.Name);
    if ($("#DocumentUploadModalFolderName") != null) { $("#DocumentUploadModalFolderName").text(dataItem.Name); }
}
function getMoveToFolderId() {
    var MoveFolderTo = document.getElementById("MoveFolderTo");
    if (MoveFolderTo != null) {
        return MoveFolderTo.value;
    }
}

function onCollapsePartialFolderTreeView(event) {
    var dataItem = this.dataItem(event.node);
    var nodeId = dataItem.id;
    if (nodeId == null || nodeId == "") {
        nodeId = dataItem.Id
    }
    UpdateTreeViewSelection(nodeId);
}
function onExpandPartialFolderTreeView(event) {
    var dataItem = this.dataItem(event.node);
    var nodeId = dataItem.id;
    if (nodeId == null || nodeId == "") {
        nodeId = dataItem.Id
    }
    UpdateTreeViewSelection(nodeId);

}
function onSelectedPartialFolderTreeView(event) {
    //var dataItem = this.dataItem(event.node);
    AffinityDms.Entities.Documents.prototype.ResetInstantDocumentViewer();
    var id = "#" + event.sender.element[0].id;
    var tree = $(id).data('kendoTreeView');
    var dataItem = tree.dataItem(event.node);
    if ($("#DocumentUploadModalFolderName") != null) { $("#DocumentUploadModalFolderName").text(dataItem.Name); }
    var nodeId = dataItem.id;
    if (nodeId == null || nodeId == "") {
        nodeId = dataItem.Id
    }
    UpdateTreeViewSelection(nodeId);
    SetSelectedElement(nodeId);
}
function onDataBoundPartialFolderTreeView(event) {
    //below Statement Expand all data in tree view
    if (event != null) {
        if (event.sender != null) {
            if (event.sender.element.length > 0) {
                if (event.sender.element[0].id != "") {
                    var id = "#" + event.sender.element[0].id;
                    $(id).data("kendoTreeView").expand(".k-item");
                    var TreeViewData = $(id).data("kendoTreeView");
                    var dataItem = this.dataItem(event.node)
                    if (dataItem != null) {
                        if ($("#defaultFolderSelection") != null) {
                            var subFolder = $("#defaultFolderSelection").attr("data-subFolder");
                            if (subFolder > 0) {
                                if (dataItem.Id == subFolder) {
                                    var node = TreeViewData.findByUid(dataItem.uid);
                                    if (node != null) {
                                        TreeViewData.select(node);
                                        TreeViewData.trigger("select", { node: node });
                                        var selectedTreeNode = TreeViewData.select()[0];
                                        $('#divid3').scrollTop(selectedTreeNode.offsetTop);
                                    }
                                }
                            }
                        }
                            
                    }



                        


                }
            }
        }
        //if (event.node == null)
        //{
        //    if ($("#defaultFolderSelection") != null) {
        //        var subFolder = $("#defaultFolderSelection").attr("data-subFolder");
        //        if (subFolder > 0) {

        //            //var parentNodes = document.getElementsByName("parentNodes");
        //            //for (var i = 0; i < parentNodes.length; i++) {
        //            //    if (subFolder == parentNodes[i].getAttribute("data-id")) {
        //            //        $("#defaultFolderSelection").attr("data-subFolder", "0");
        //            //        e
        //            //        parentNodes[i].click();
        //            //    }
        //            //}
        //        }
        //    }

        //}
    }
}

$("div#divid2 li").click(function (e) {
    //alert(e.currentTarget.getAttribute("data-id"));
    if (!e.currentTarget.classList.contains("active")) {
        var ModalId = "PartialTreeViewModal";
        kendo.destroy($("#" + ModalId));
        document.getElementById(ModalId).textContent = "";
        //LoadFolderTreeView("TreeViewFolderDiv", "")
        //var url = "/TenantDocumentsFolderWise/GetFolderTreeView?id=" + e.currentTarget.getAttribute("data-id");
        //$("#FolderTreeViewModal").load(url);
        var MoveFolderTo = document.getElementById("MoveFolderTo");
        //var dataItem = this.dataItem(event.node);
        MoveFolderTo.value = e.currentTarget.getAttribute("data-id");
        loadPartialTreeViewModal(ModalId)

    }

});
function loadPartialTreeViewModal(treeViewDivId) {

    var id = "#" + treeViewDivId;


    //var treeviewEvents = new AffinityDms.CustomKendo.KendoTreeViewEvents();
    //treeviewEvents.onSelect = "onSelectedPartialFolderTreeView";
    //treeviewEvents.onChange = "";
    //treeviewEvents.onCollapse = "onCollapsePartialFolderTreeView";
    //treeviewEvents.onExpand = "onExpandPartialFolderTreeView";
    //treeviewEvents.onCheck = "";
    //treeviewEvents.onDrag = "";
    //treeviewEvents.onDragEnd = "";
    //treeviewEvents.onDragStart = "";
    //treeviewEvents.onDrop = "";
    //var treeviewInfo = new AffinityDms.CustomKendo.TreeViewInformation();
    //treeviewInfo.Name = "PartialTreeViewModals";
    //treeviewInfo.LoadOnDemand = false;
    //treeviewInfo.Expanded = true;
    //treeviewInfo.Template = "treeviewFolderPartiaTemplateKendo";
    //treeviewInfo.Events = treeviewEvents;

    //$(id).load("../../TenantDocumentsFolderWise/GetDynamicTreeview", { folderId: null, kendoTreeViewInformation: treeviewInfo });



    var folderId = getMoveToFolderId();
    $.ajax({
        type: "POST",
        url: "/TenantDocumentsFolderWise/GetHieraricalTreeViewData",
        data: JSON.stringify({ folderId: folderId }),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            var folderId = getMoveToFolderId();
            $(id).kendoTreeView(
              {

                  "select": onSelectedPartialFolderTreeView,
                  "dataBound": onDataBoundPartialFolderTreeView,
                  "collapse": onCollapsePartialFolderTreeView,
                  "expand": onExpandPartialFolderTreeView,
                  "dataSource": processTable(response, "Id", "ParentId", folderId),
                  "schema": {
                      "model": { "id": "Id", "hasChildren": "HasChildren" }
                  },
                  "loadOnDemand": false,
                  "template": jQuery('#treeviewFolderPartiaTemplateKendo').html(),
                  "dataTextField": ["Name"]
              });
        }
    });




}

function processTable(data, idField, foreignKey, rootLevel) {
    var hash = {};

    for (var i = 0; i < data.length; i++) {
        var item = data[i];
        var id = item[idField];
        var parentId = item[foreignKey];

        hash[id] = hash[id] || [];
        hash[parentId] = hash[parentId] || [];

        item.items = hash[id];
        hash[parentId].push(item);
    }
    return hash[rootLevel];
}






$("#UserAssignmentModal").on("shown.bs.modal", function () {
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "AssignUserRow";
    AffinityDms.Entities.Documents.prototype.GetIndexUserForAssignment("AssignUserRow");
});

$("#folderTreeViewModal").on("shown.bs.modal", function () {
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "folderTreeViewModalBody";
    LoadFolderTreeView("folderTreeViewModalBody", "folderTreeViewModal_row");

});

function onDataBoundFolderTreeViewModal(e) {
    if (e != null) {
        if (e.node != null) {
            var ModalBodyIdx = document.getElementById("ModalBodyId");
            var ModalBody = document.getElementById(ModalBodyId.value);
            var rowid = "#" + ModalBody.childNodes[0].id;
            var FolderTreeViewObj = $(rowid);
            if (FolderTreeViewObj != null) {
                //$("#FolderTreeViewModal").data("kendoTreeView").expand(".k-item");
                var CurrentFolderId = document.getElementById("CurrentFolderId");
                var nodeId = CurrentFolderId.value;
                SelectNode(nodeId, FolderTreeViewObj, e, this)
            }
            //var div = this.element.find('div ul li:first').addClass('k-state-selected');
        }
    }

}

function AssignForIndexing(event) {
    var anchorTagDataId = event.currentTarget.getAttribute("data-id");
    if (anchorTagDataId != null) {
        var hdocId = document.getElementById("hdocId");
        hdocId.value = anchorTagDataId;
        $("#UserAssignmentModal").modal("show");

    }
}


function CloseAddFolderModal(event) {
    $('#addFolderModalError').css("display", "none");
    $('#addFolderModalSuccess').css("display", "none");
    $('#folderNameTxtbx').val("");
    $('#addFolderModal').modal('hide');
}
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
function onDataBound(e) {
    //below Statement Expand all data in tree view
    var treeView = document.getElementById("treeview");
    if (treeView != null) {
        $("#treeview").data("kendoTreeView").expand(".k-item");
    }
}
$("#InviteUserEmail").keypress(function (event) {
    if (event.which || event.keyCode) { if ((event.which == 13) || (event.keyCode == 13)) { $("#InviteUserBtn").click(); return false; } else { return true; } }
});

function OnSelected(event) {
    var CurrentFolderId = document.getElementById("CurrentFolderId");
    var treeView = $("#treeview").data('kendoTreeView');

    //var dataItem = treeView.dataSource.get(CurrentFolderId.value);
    //var node = treeView.findByUid(dataItem.uid);
    //if (node != null)
    //{
    //    var li = node[0];
    //    li.setAttribute("aria-selected", "false");
    //    li.removeAttribute("id");
    //    //k-in k-state-selected
    //    for (var i = 0; i < li.childNodes.length; i++)
    //    {
    //        var chkdiv = li.childNodes[i]
    //        if (chkdiv instanceof HTMLDivElement)
    //        {
    //            for (var j = 0; j < chkdiv.childNodes.length; j++) {
    //                var chkspan = chkdiv.childNodes[j];
    //                if (chkspan instanceof HTMLSpanElement) {
    //                    chkspan.class = "k-in";
    //                      k;
    //                }
    //            }
    //        }
    //    }
    //}
    dataItem = this.dataItem(event.node);
    //node = treeView.findByUid(dataItem.uid);
    //if (node != null) {
    //    var li = node[0];
    //    li.setAttribute("aria-selected", "true");
    //    li.setAttribute("id", "treeview_tv_active");
    //    for (var i = 0; i < li.childNodes.length; i++) {
    //        var chkdiv = li.childNodes[i]
    //        if (chkdiv instanceof HTMLDivElement) {
    //            for (var j = 0; j < chkdiv.childNodes.length; j++) {
    //                var chkspan = chkdiv.childNodes[j];
    //                if (chkspan instanceof HTMLSpanElement) {
    //                    chkspan.class = "k-in k-state-selected";
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}
    CurrentFolderId.value = dataItem.id;
    var MoveFolderTo = document.getElementById("MoveFolderTo");
    MoveFolderTo.value = dataItem.id;
    if ($("#DocumentUploadModalFolderName") != null) { $("#DocumentUploadModalFolderName").text(dataItem.Name); }
    SetSelectedElement(dataItem.id);
}


function ExpandSelectedNode(event) {
    var nodeId = event.getAttribute("data-id");
    var FolderTreeViewObj = $("#treeview");
    ExpandNode(nodeId, FolderTreeViewObj)
}
function ExpandNode(nodeId, FolderTreeViewObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        var dataItem = treeView.dataSource.get(nodeId);
        var node = treeView.findByUid(dataItem.uid);
        treeView.expand("li:first");
        treeView.expand(node);
        treeView.select(node);
        var CurrentFolderId = document.getElementById("CurrentFolderId");
        CurrentFolderId.value = nodeId;
        SetSelectedElement(nodeId);
    }
    else {
        alert("Unable to find the folder")
    }
}
function SelectNode(nodeId, FolderTreeViewObj, event, thisObj) {
    if ((nodeId > 0) && (FolderTreeViewObj != null)) {
        var treeView = FolderTreeViewObj.data('kendoTreeView');
        //FolderTreeViewObj.data("kendoTreeView").expand(".k-item");
        treeView.expand(".k-item");
        if (treeView != null) {
            var dataItem = thisObj.dataItem(event.node)
            if (dataItem != null) {
                if (dataItem.Id == nodeId) {
                    var node = treeView.findByUid(dataItem.uid);
                    if (node != null) {
                        treeView.select(node);
                        treeView.trigger("select", { node: node });
                        var selectedTreeNode = treeView.select()[0];
                        selectedTreeNode.scrollIntoView({ block: "end", behavior: "smooth" });

                    }
                }
            }
            if (event.node == null) {
                var a = "";
            }
            //else {
            //    var a = "";
            //}

            ////if (dataItem != null) {
            ////    var node = treeView.findByUid(dataItem.uid);
            ////    if (node != null) {
            ////        treeView.expand("li:first");
            ////        treeView.expand(node);
            ////        treeView.select(node);
            ////    }
            ////}
        }
    }
}

//==============================================Upload Document Folder TreeView===================================//
$("#DocumentUploadModal").on("shown.bs.modal", function () {

    //========= Start clear status div when load popup=========//
    var divStatus = document.getElementById("divStatus");
    divStatus.textContent = "";
    //========= End clear status div when load popup=========//

    resetFileControl();

    //========= Start Rebind Treeview========================================//
    var ModalBodyId = document.getElementById("ModalBodyId");
    ModalBodyId.value = "DocumentUploadBody";
    LoadFolderTreeView("DocumentUploadBody", "DocumentUploadRow");
    //========= End Rebind Treeview==========================================//
});
$(document).on("ready", function () {
    if ($("#defaultFolderSelection") != null)
    {
        var parent = $("#defaultFolderSelection").attr("data-parentFolder");
        if (parent > 0)
        {
            var parentNodes = document.getElementsByName("parentNodes");
            for (var i = 0; i < parentNodes.length; i++) {
                if (parent == parentNodes[i].getAttribute("data-id")) {
                    $("#defaultFolderSelection").attr("data-parentFolder","0");
                    parentNodes[i].click();
                }
            }
        }
    }



    $("#files").kendoUpload({
        async: {
            saveUrl: "../../TenantDocumentsFolderWise/UploadDocumentsToFolder",
            removeUrl: "../../TenantDocumentsFolderWise/RemoveDocumentsToFolder",
            autoUpload: true
        },
        success: onSuccess
    });
    if ($("#DocumentFolderWiseErrorMessage") != null) {
        if ($("#DocumentFolderWiseErrorMessageText").text() != null || $("#DocumentFolderWiseErrorMessageText").text() != "") {
            setTimeout(function () {
                $('#DocumentFolderWiseErrorMessage').fadeOut('fast');
                $("#DocumentFolderWiseErrorMessage").css("display", "none");
                $("#DocumentFolderWiseErrorMessageText").text("");
            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
        }
    }
    if ($("#DocumentFolderWiseSuccessMessage") != null) {
        if ($("#DocumentFolderWiseSuccessMessageText").text() != null || $("#DocumentFolderWiseSuccessMessageText").text() != "") {
            setTimeout(function () {
                $('#DocumentFolderWiseSuccessMessage').fadeOut('fast');
                $("#DocumentFolderWiseSuccessMessage").css("display", "none");
                $("#DocumentFolderWiseSuccessMessageText").text("");
            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
        }
    }
});

function onSuccess(e) {
    // Array with information about the uploaded files
    //var files = e.files;

    //if (e.operation == "upload") {
    //    alert("Successfully uploaded " + files.length + " files");
    //}
}
$("#DocumentUploadModal").on("hidden.bs.modal", function () {
    if ($("#DocumentUploadModalErrorHeader") != null) {
        $("#DocumentUploadModalErrorHeader").hide();
        $("#DocumentUploadModalErrorDiv").text("");
    }
    $.ajax({
        type: "POST",
        url: "../../TenantDocumentsFolderWise/CleanUploadedDocuemnts",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            try {
                if (typeof (response) === "boolean") {
                    $('#DocumentUploadModal').modal('hide');
                }
                else if (typeof (response) === "string") {
                    if ($("#DocumentUploadModalErrorHeader") != null) {
                        $("#DocumentUploadModalErrorHeader").show();
                        $("#DocumentUploadModalErrorDiv").show();
                        $("#DocumentUploadModalErrorDiv").text(response);
                        setTimeout(function () {
                            $("#DocumentUploadModalErrorHeader").hide();
                            $("#DocumentUploadModalErrorDiv").hide();
                            $("#DocumentUploadModalErrorDiv").text("");
                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                    }
                }
            }
            catch (e) {
                if ($("#DocumentUploadModalErrorHeader") != null) {
                    $("#DocumentUploadModalErrorHeader").show();
                    $("#DocumentUploadModalErrorDiv").show();
                    $("#DocumentUploadModalErrorDiv").text(response);
                    setTimeout(function () {
                        $("#DocumentUploadModalErrorHeader").hide();
                        $("#DocumentUploadModalErrorDiv").hide();
                        $("#DocumentUploadModalErrorDiv").text("");
                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                }
            }
        },
        error: function (responseText) {
            if ($("#DocumentUploadModalErrorHeader") != null) {
                $("#DocumentUploadModalErrorHeader").show();
                $("#DocumentUploadModalErrorDiv").show();
                $("#DocumentUploadModalErrorDiv").text("Oops an Error Occured");
                setTimeout(function () {
                    $("#DocumentUploadModalErrorHeader").hide();
                    $("#DocumentUploadModalErrorDiv").hide();
                    $("#DocumentUploadModalErrorDiv").text("");
                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
            }
        }
    });

});
function CloseUploadDocumentModal(event) {

    //========= Start clear status div when load popup=========//
    var divStatus = document.getElementById("divStatus");
    divStatus.textContent = "";
    //========= End clear status div when load popup=========//

    resetFileControl();

}

//===============================================End Upload Document Folder TreeView==============================//
