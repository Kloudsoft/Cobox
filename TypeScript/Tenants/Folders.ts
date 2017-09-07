module AffinityDms.Entities {
    export class Folders {

    }
}
function onDataBoundFolderTreeViewModal(e) {
    if (e != null) {
        if (e.node != null) {
            var ModalBodyId: any = document.getElementById("ModalBodyId");
            var ModalBody: any = document.getElementById(ModalBodyId.value);
            var rowid = "#" + ModalBody.childNodes[0].id;
            var FolderTreeViewObj = $(rowid);
            if (FolderTreeViewObj != null) {
                //$("#FolderTreeViewModal").data("kendoTreeView").expand(".k-item");
                var CurrentFolderId: any = document.getElementById("CurrentFolderId");
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
function OnSelectedFolderTreeViewModal(event) {
    var MoveFolderTo: any = document.getElementById("MoveFolderTo");
    var dataItem = this.dataItem(event.node);
    MoveFolderTo.value = dataItem.Id;// (dataItem.id != null) ? (dataItem.id) : (dataItem.Id);
}
function LoadFolderTreeView(folderTreeViewModalBody, folderTreeViewModalRow) {
    var folderTreeViewModalBodyelem = document.getElementById(folderTreeViewModalBody);
    folderTreeViewModalBodyelem.textContent = "";
    var Row = document.createElement("div");
    Row.id = folderTreeViewModalRow;
    folderTreeViewModalBodyelem.appendChild(Row);
    var rowid = "#" + folderTreeViewModalRow;
    var kendotreeview: any = $(rowid);
    kendotreeview.kendoTreeView(
        {
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