setInterval(function () {
    if ($("#DiscourseLastMessage") != null) {
        if ($("#TempMessagesHolder") != null) {
            $("#TempMessagesHolder").load("../../TenantDiscourse/GetLatestPostVersionsByLastRecieved?id=" + parseInt($("#DiscourseLastMessage").val()));
        }
    }
    loop();
}, 1000);
$("div#TempMessagesHolder").bind("DOMSubtreeModified", function (e) {
    var TempMessagesHolder = e.target;
    if (TempMessagesHolder.childNodes != null) {
        var MaxMessageRecievedId = document.getElementById("MaxMessageRecievedId");
        if (MaxMessageRecievedId != null) {
            $("#DiscourseLastMessage").val(MaxMessageRecievedId.value);
            //MaxMessageRecievedId.parentNode.removeChild(MaxMessageRecievedId);
            for (var i = 0; i < TempMessagesHolder.childNodes.length; i++) {
                if (TempMessagesHolder.childNodes[i] instanceof HTMLDivElement) {
                    $("div#MainPostContainer").append(TempMessagesHolder.childNodes[i]);
                    TempMessagesHolder.textContent = "";
                }
            }
        }
    }
});













//ajeeeb




    function documentGridBind(e){
        var documentTooltipTemp = document.getElementsByName("documentTooltipTemp");
        if (documentTooltipTemp != null)
        {
            for (var a = 0; a < documentTooltipTemp.length; a++) {
                if (documentTooltipTemp[a] != null) {
                    var id = documentTooltipTemp[a].getAttribute("data-id");
                    var holderId = "documentTooltipHolder" + id;
                    var documentTooltipHolder = document.getElementById(holderId);
                    if (documentTooltipHolder != null) {
                        var str =  documentTooltipTemp[a].innerHTML.toString().replaceAll('"','^').replaceAll("'",'"').replaceAll("^","'");
                        document.getElementById(holderId).title = str;
                        var idd = '#'+holderId;
                        $(idd).tooltip({
                            placement: "auto bottom",
                            html: "true",
                            trigger: "focus",
                            title: function () {
                                return $(this).attr('title');
                            },
                        });
                    }
                }

            }
        }


        var documentTooltipShareTemp = document.getElementsByName("documentTooltipShareTemp");
        if (documentTooltipShareTemp != null) {
            for (var a = 0; a < documentTooltipShareTemp.length; a++) {
                if (documentTooltipShareTemp[a] != null) {
                    var id = documentTooltipShareTemp[a].getAttribute("data-id");
                    var ShareId = "documentTooltipShare" + id;
                    var documentTooltipShare = document.getElementById(ShareId);
                    if (documentTooltipShare != null) {
                        var str = documentTooltipShareTemp[a].innerHTML.toString().replaceAll('"', '^').replaceAll("'", '"').replaceAll("^", "'");
                        document.getElementById(ShareId).title = str;
                        var shreId = '#' + ShareId;
                        $(shreId).tooltip({
                            placement: "auto bottom",
                            html: "true",
                            trigger: "focus",
                            title: function () {
                                return $(this).attr('title');
                            },
                        });
                    }
                }

            }
        }


    }





        function OnSelectedFolderTreeViewModal(event) {
            var MoveFolderTo = document.getElementById("MoveFolderTo");
            var dataItem = this.dataItem(event.node);
            MoveFolderTo.value = dataItem.id;
        }
    function LoadFolderTreeView(folderTreeViewModalBody, folderTreeViewModalRow) {
        var folderTreeViewModalBody = document.getElementById(folderTreeViewModalBody);
        folderTreeViewModalBody.textContent = "";
        var Row = document.createElement("div");
        Row.id = folderTreeViewModalRow;
        folderTreeViewModalBody.appendChild(Row);
        var rowid = "#" + folderTreeViewModalRow;
        $(rowid).kendoTreeView(
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
    $("#folderTreeViewModal").on("shown.bs.modal", function () {
        var ModalBodyId = document.getElementById("ModalBodyId");
        ModalBodyId.value = "folderTreeViewModalBody";
        LoadFolderTreeView("folderTreeViewModalBody", "folderTreeViewModal_row");

    });
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

    function RenderTreeViewModal(event) {
        var anchorTagDataId = event.currentTarget.getAttribute("data-id");
        if (anchorTagDataId != null) {
            var doc = document.getElementById("folderTreeViewModal");
            var hdocId = document.getElementById("hdocId");
            hdocId.value = anchorTagDataId;
            $("#folderTreeViewModal").modal("show");

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





    