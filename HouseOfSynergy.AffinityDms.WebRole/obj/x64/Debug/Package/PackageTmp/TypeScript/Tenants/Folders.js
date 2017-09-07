var AffinityDms;
(function (AffinityDms) {
    var Entities;
    (function (Entities) {
        var Folders = (function () {
            function Folders() {
            }
            return Folders;
        }());
        Entities.Folders = Folders;
    })(Entities = AffinityDms.Entities || (AffinityDms.Entities = {}));
})(AffinityDms || (AffinityDms = {}));
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
                SelectNode(nodeId, FolderTreeViewObj);
            }
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
function OnSelectedFolderTreeViewModal(event) {
    var MoveFolderTo = document.getElementById("MoveFolderTo");
    var dataItem = this.dataItem(event.node);
    MoveFolderTo.value = dataItem.Id; // (dataItem.id != null) ? (dataItem.id) : (dataItem.Id);
}
function LoadFolderTreeView(folderTreeViewModalBody, folderTreeViewModalRow) {
    var folderTreeViewModalBodyelem = document.getElementById(folderTreeViewModalBody);
    folderTreeViewModalBodyelem.textContent = "";
    var Row = document.createElement("div");
    Row.id = folderTreeViewModalRow;
    folderTreeViewModalBodyelem.appendChild(Row);
    var rowid = "#" + folderTreeViewModalRow;
    var kendotreeview = $(rowid);
    kendotreeview.kendoTreeView({
        "select": OnSelectedFolderTreeViewModal,
        "dataBound": onDataBoundFolderTreeViewModal,
        "expanded": true,
        "dataSource": {
            "transport": {
                "read": { "url": "/TenantDocumentsFolderWise/Folder_Read" }
            },
            "schema": {
                "model": { "id": "Id", "hasChildren": "HasChildren" }
            }
        }, "loadOnDemand": false, "template": jQuery('#treeview-template-FolderTreeViewModal').html(), "dataTextField": ["Name"]
    });
}
//# sourceMappingURL=Folders.js.map