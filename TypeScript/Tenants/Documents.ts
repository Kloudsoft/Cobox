/// <reference path="../../scripts/typings/kendo-ui/kendo-ui.d.ts" />
module AffinityDms.Entities {
    export class DocumentElementViewModel {
        DocumentElementId: number;
        DocuemntOcrText: string;
    }
    export class FileUploadStatus {
        FileName: string;
        File: string;
        Finalized: boolean;
        StatusMessage: string;
    }

    export class FolderTreeViewModel {
        Id: number;
        Name: string;
        HasChildren: boolean;
        ParentId: number;
    }

    export class DocumentFolderTreeViewModel {
        public Parentfolder: AffinityDms.Entities.Folder;
        public ChildFolders: Array<AffinityDms.Entities.Folder> = new Array();
        public ParentFolderDocuments: Array<AffinityDms.Entities.Document> = new Array();
        //
    }
    export class Documents {
        constructor() { }
        //public A(): any {
        //    //var date: string = "/Date(633898800000)/";
        //}
        public SubmitRenameFolder(event): any {
            var target = event.target;
            var folderid = parseInt( event.target.getAttribute("data-id"));
            var foldername = event.target.getAttribute("data-name");
            var newName = $("#FolderRenameModalTextBox").val();
            if (folderid <= 0) {
                $("#FolderRenameModalErrorHeaderDiv").text("No folder selected");
                $("#FolderRenameModalErrorHeader").show();
                    setTimeout(function () {
                        $('#FolderRenameModalErrorHeader').fadeOut('fast');
                        $("#FolderRenameModalErrorHeader").css("display", "none");
                        $("#FolderRenameModalErrorHeaderDiv").text("");
                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                
            }
            else if (newName.trim() == "") {
                $("#FolderRenameModalErrorHeaderDiv").text("Folder name is required");
                $("#FolderRenameModalErrorHeaderDiv").text("No folder selected");
                $("#FolderRenameModalErrorHeader").show();
                setTimeout(function () {
                    $('#FolderRenameModalErrorHeader').fadeOut('fast');
                    $("#FolderRenameModalErrorHeader").css("display", "none");
                    $("#FolderRenameModalErrorHeaderDiv").text("");
                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
            }
            else if (newName == foldername) {
                $("#FolderRenameModalErrorHeaderDiv").text("Folder name can not be same");
                $("#FolderRenameModalErrorHeaderDiv").text("No folder selected");
                $("#FolderRenameModalErrorHeader").show();
                setTimeout(function () {
                    $('#FolderRenameModalErrorHeader').fadeOut('fast');
                    $("#FolderRenameModalErrorHeader").css("display", "none");
                    $("#FolderRenameModalErrorHeaderDiv").text("");
                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
            }

            else {
                var url = "../../TenantDocumentsFolderWise/RenameFolder?folderId=" + folderid + "&folderName=" + newName;
                $.ajax({
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseData: any) {
                        if (responseData == false) { responseData = "Unable to proccess the following request";}
                        if (responseData == true) {
                            var foldermodalnameid = "#PartialTreeviewFolderName_" + $("#FolderRenameModalSubmitBtn").attr("data-id");
                            var H_foldernameid = "#H_PartialTreeviewFolder_"+ $("#FolderRenameModalSubmitBtn").attr("data-id");

                            $(foldermodalnameid).attr("data-name", $("#FolderRenameModalTextBox").val());
                            $(H_foldernameid).attr("data-name", $("#FolderRenameModalTextBox").val());
                            $(foldermodalnameid).text($("#FolderRenameModalTextBox").val());

                            $("#FolderRenameModalHeader").text("");
                            $("#FolderRenameModalSubmitBtn").attr("data-id","");
                            $("#FolderRenameModalSubmitBtn").attr("data-name", "");
                            $("#FolderRenameModalErrorHeader").css("display", "none");
                            $("#FolderRenameModalErrorHeaderDiv").text("");
                            $("#FolderRenameModalTextBox").val("")
                            $("#FolderRenameModal").modal("hide");

                        }
                        else {
                            if (typeof (responseData) === "string")
                            {
                                $("#FolderRenameModalErrorHeaderDiv").text(responseData);
                                $("#FolderRenameModalErrorHeader").show();
                                setTimeout(function () {
                                    $('#FolderRenameModalErrorHeader').fadeOut('fast');
                                    $("#FolderRenameModalErrorHeader").css("display", "none");
                                    $("#FolderRenameModalErrorHeaderDiv").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                    },
                    error: function (responseText) {
                        alert(responseText);
                    }
                });
            }



        }
        public DocumentMoveArray(event): any {
            var inputeletempNo = document.getElementsByName("DocumentNo");
            var inputeleActive = document.getElementsByName("item.MoveItem");
            var nestedArray: Array<Array<string>> = new Array();
            var folderdropdown = <HTMLSelectElement>document.getElementById("FolderTreeDropDown");
            //if (folderdropdown.length > 0) {

            //    alert(element.value);
            //}
            var element = <HTMLOptionElement>document.getElementById("FolderTreeOptions" + folderdropdown.selectedIndex);
            var movetoid = element.value;
            for (var i = 0; i < inputeleActive.length; i++) {

                if (i % 2 == 0) {
                    var temparray: Array<string> = new Array();
                    var inputeleActiveValue = <HTMLInputElement>inputeleActive[i];
                    if (inputeleActiveValue.checked) {
                        temparray.push(movetoid);
                        temparray.push(inputeletempNo[i / 2].textContent);
                        nestedArray.push(temparray);
                    }

                }
            }
            $.ajax({
                type: "POST",
                url: "/TenantDocuments/DocumentListing",
                data: JSON.stringify(nestedArray),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (responseText: string) {
                    if (responseText.toString().search("An Error Occured:") == 0) {
                        //var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                        //messdiv.style.color = "Red";
                        //messdiv.textContent = responseText.toString();
                        //messdiv.style.fontSize = "1.5em";
                    }
                    else {
                        window.location.href = "/TenantDocuments/DocumentListing";
                        //var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                        //messdiv.textContent = responseText.toString();
                        //messdiv.style.color = "Green";
                        //messdiv.style.fontSize = "1.5em";
                    }
                },
                error: function (responseText) {


                }
            });
        }
        public MoveItemsModal(event): any {


        }
        public SelectedFolderTreeDropDownItem(event: any): any {

        }
        private filldropdown(data: any): void {
            var folderdropdown = <HTMLSelectElement>document.getElementById("FolderTreeDropDown");
            if (folderdropdown.length > 0) {
                folderdropdown.innerHTML = "";
            }

            for (var i = 0; i < data.length; i++) {
                var opt = document.createElement("option");
                opt.id = "FolderTreeOptions" + i;
                opt.value = data[i].ID.toString();
                opt.textContent = data[i].Name;
                folderdropdown.appendChild(opt);
            }
            //var e = document.createEvent('MouseEvents');
            //e.initEvent("mousedown",false,false);
            //folderdropdown.dispatchEvent(e);
        }
        public FolderTreeDropDownFiller(event): any {

            $.get("/TenantDocuments/GetAllFoldersnameAndId", (data: Array<Folder>) => { this.filldropdown(data); }, "json");
            //var request = $.ajax({
            //    type: "GET",
            //    url: "/TenantDocuments/GetAllFoldersnameAndId",
            //    dataType: "json"
            //});

            //request.done(this.filldropdown);

            //request.fail(function (jqXHR, textStatus, errorThrown: SyntaxError) {
            //    // Some error
            //});


            //$.ajax({
            //    type: "GET",
            //    url: "/TenantDocuments/GetAllFoldersnameAndId",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: 'json',
            //    success: function (responseData: any) {
            //        if (responseData!=null) {
            //            this.filldropdown(responseData);
            //        }
            //        else {

            //        }
            //    },
            //    error: function (responseText) {


            //    }
            //});
        }

        public AddSearchDocument(event): any {

            var DocumentName = <HTMLInputElement>document.getElementById("documentname");
            var TemplateName = <HTMLInputElement>document.getElementById("templatename");
            var FolderName = <HTMLInputElement>document.getElementById("folderame");
            var TagsUser = <HTMLInputElement>document.getElementById("usertags");
            var TagsGlobal = <HTMLInputElement>document.getElementById("tagsglobal");
            var Content = <HTMLInputElement>document.getElementById("content");
            var StartDate = <HTMLInputElement>document.getElementById("startdate");
            var EndDate = <HTMLInputElement>document.getElementById("enddate");
            var SearchName = <HTMLInputElement>document.getElementById("txtSearchName");

            if (SearchName.value != '') {
                $.ajax({
                    type: "POST",
                    url: "../../TenantDocuments/SaveSearchCriteria",
                    data: JSON.stringify({ DocumentName: DocumentName.value, TemplateName: TemplateName.value, FolderName: FolderName.value, TagsUser: TagsUser.value, TagsGlobal: TagsGlobal.value, Content: Content.value, StartDate: StartDate.value, EndDate: EndDate.value, SearchName: SearchName.value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: string) {
                        if (responseText.toString().toLowerCase() == "true") {
                            var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                            messdiv.textContent = "Record has been saved successfully";
                            messdiv.style.color = "Green";
                            messdiv.style.fontSize = "1.5em";
                            messdiv.style.display = "";
                            BindPreDropdown();
                        }
                        //else if (responseText.toString().toLowerCase() == "login failed") {
                        //    alert("You are not not logged in. Please Login");
                        //    window.location.href = "/Login/SignIn";
                        //}
                        else {
                            var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                            messdiv.style.color = "Red";
                            messdiv.textContent = responseText.toString();
                            messdiv.style.fontSize = "1.5em";
                            messdiv.style.display = "";
                        }
                    },
                    error: function (responseText) {


                    }
                });
            }
            else
                alert("Please enter search name");
        }

        public SearchDocument(event): any {

            funcSearchDocuments(event);
        }

        public BindPreviousSearch(event): any {

            BindPreDropdown();
        }

        public ResetInstantDocumentViewer() {
            var div6 = document.getElementById("divid6");
            if (div6 != null) {
                var ReatedDocumentsContainer = document.getElementById("ReatedDocumentsContainer");
                if (ReatedDocumentsContainer != null) {
                    ReatedDocumentsContainer.textContent = "";
                }
                var DocumentStatusContainer = document.getElementById("DocumentStatusContainer");
                if (DocumentStatusContainer != null) {
                    DocumentStatusContainer.textContent = "";
                }
                var documentNameDiv6 = document.getElementById("documentNameDiv6");
                if (documentNameDiv6 != null) {
                    documentNameDiv6.textContent = "";
                }
                var viewercontainer = document.getElementById("viewer-container");
                if (viewercontainer != null) {
                    viewercontainer.textContent = "";
                }
                if ($("#divid6Close") != null) {
                    $("#divid6Close").click();
                }
            }

        }
        public PopulateInControls(event): any {


            var ddlPreviousSearch = <HTMLSelectElement>document.getElementById("ddlPreviousSearch");
            var opt = <HTMLOptionElement>ddlPreviousSearch[ddlPreviousSearch.selectedIndex];

            if (parseInt(opt.id) > 0) {
                var optid = parseInt(opt.id.trim());
                $.ajax({
                    type: "GET",
                    url: "/TenantDocuments/PopulatePreviousSearchById?searchId=" + optid,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseData) {
                        var DocumentName = <HTMLInputElement>document.getElementById("documentname");
                        var TemplateName = <HTMLInputElement>document.getElementById("templatename");
                        var FolderName = <HTMLInputElement>document.getElementById("folderame");
                        var TagsUser = <HTMLInputElement>document.getElementById("usertags");
                        var TagsGlobal = <HTMLInputElement>document.getElementById("tagsglobal");
                        var Content = <HTMLInputElement>document.getElementById("content");
                        var StartDate = <HTMLInputElement>document.getElementById("startdate");
                        var EndDate = <HTMLInputElement>document.getElementById("enddate");

                        DocumentName.value = responseData.Filename == null ? '' : responseData.Filename;
                        TemplateName.value = responseData.TemplateName == null ? '' : responseData.TemplateName;
                        FolderName.value = responseData.FolderName == null ? '' : responseData.FolderName;
                        TagsUser.value = responseData.TagsUser == null ? '' : responseData.TagsUser;
                        TagsGlobal.value = responseData.TagsGlobal == null ? '' : responseData.TagsGlobal;
                        Content.value = responseData.Content == null ? '' : responseData.Content;
                        //alert(responseData.DateTimeFrom);
                        if ((responseData.DateTimeFrom != null) || (responseData.DateTimeFrom != '')) {
                            var value = new Date(parseInt(responseData.DateTimeFrom.substr(6)));
                            //var ret = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
                            var month: string = monthformater(value.getMonth().toString());
                            var date: string = dateformater(value.getDate().toString());
                            var ret = value.getFullYear() + "-" + month + "-" + date;

                            StartDate.value = ret;
                            //alert(ret);
                        }

                        else {
                            StartDate.value = StartDate.defaultValue;
                        }

                        if (responseData.DateTimeUpTo != null || responseData.DateTimeUpTo != '') {
                            var value = new Date(parseInt(responseData.DateTimeUpTo.substr(6)));
                            var month: string = monthformater(value.getMonth().toString());
                            var date: string = dateformater(value.getDate().toString());
                            var ret = value.getFullYear() + "-" + month + "-" + date;
                            EndDate.value = ret;
                            //  alert(ret);
                        }

                        else {
                            EndDate.value = EndDate.defaultValue;
                        }


                    },
                    error: function (responseData) {


                    }
                });
            }
            else
                alert("Please select any previous saved search");
        }
        public EditImage(event): any {
            var imgelement = <HTMLImageElement>event.currentTarget;
            if (imgelement != null) {
                var id = imgelement.parentElement.parentElement.childNodes[0].textContent;
                window.location.href = "../Pages/ImageEditor.aspx?id=" + id;
            }
        }
        public MarkPublic(event): any {
            var publicprivate = <HTMLImageElement>event.currentTarget;
            if (publicprivate != null) {
                //alert();
                //var id = publicprivate.parentElement.parentElement.childNodes[0].textContent;
                var id = publicprivate.getAttribute("data-id");
                //alert(id);
                $.ajax({
                    type: "GET",
                    url: "/TenantDocuments/MarkAsPublic?documentid=" + parseInt(id),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: string) {
                        //alert(id);
                        if (responseText.toString().toLowerCase() == "true") {
                            var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                            if (messdiv != null) {
                                messdiv.textContent = "Mark as Public has been done successfully";
                                messdiv.style.color = "Green";
                                messdiv.style.fontSize = "1.5em";
                                messdiv.style.display = "";
                                funcSearchDocuments(" ");
                            }
                            else {
                                var CurrentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
                                if (CurrentFolderId != null) {
                                    var id = parseInt(CurrentFolderId.value);
                                    SetSelectedElement(id);
                                }
                            }
                            /*
                            ("<img src='/Images/MarkPublic.jpg' title='Public' style='height:20px;width:20px' data-id='"+item.Id+"' onclick='javascript: documents.MarkPublic(event)'>  ")
                                                         :
                                                            ("<img src='/Images/MarkPrivate.jpg' title='Private' style='height:20px;width:20px' data-id='" + item.Id + "' onclick='javascript: documents.MarkPrivate(event)'> ")



                            */
                        }
                        else {
                            var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                            if (messdiv != null) {
                                messdiv.style.color = "Red";
                                messdiv.textContent = responseText.toString();
                                messdiv.style.fontSize = "1.5em";
                                messdiv.style.display = "";
                            }
                            else {
                                var CurrentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
                                if (CurrentFolderId != null) {
                                    var id = parseInt(CurrentFolderId.value);
                                    SetSelectedElement(id);
                                }
                            }
                        }
                    },
                    error: function (responseText) {
                    }
                });
            }
        }

        public MarkPrivate(event): any {

            var publicprivate = <HTMLImageElement>event.currentTarget;
            if (publicprivate != null) {

                //var id = imgelement.parentElement.parentElement.childNodes[0].textContent;
                var id = publicprivate.getAttribute("data-id");
                $.ajax({
                    type: "GET",
                    url: "/TenantDocuments/MarkAsPrivate?documentid=" + parseInt(id),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: string) {
                        //                    alert(responseText);
                        if (responseText.toString().toLowerCase() == "true") {
                            funcSearchDocuments(" ");
                        }
                        else {
                            if ($("#DocumentFolderWiseErrorMessage") != null) {
                                $("#DocumentFolderWiseErrorMessageText").text(responseText.toString());
                                $("#DocumentFolderWiseErrorMessage").css("display", "");
                                setTimeout(function () {
                                    $('#DocumentFolderWiseErrorMessage').fadeOut('fast');
                                    $("#DocumentFolderWiseErrorMessage").css("display", "none");
                                    $("#DocumentFolderWiseErrorMessageText").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                    },
                    error: function (responseText) {
                    }
                });
            }
        }

        public OCR(event): any {
            var imgelement = <HTMLImageElement>event.currentTarget;
            if (imgelement != null) {
                //alert();
                //var id = imgelement.parentElement.parentElement.childNodes[0].textContent;
                var id = imgelement.getAttribute("data-id");
                $.ajax({
                    type: "POST",
                    url: "/TenantOcrClassification/CallOCR?documentid=" + parseInt(id),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: string) {
                        if (responseText.toString().toLowerCase() == "true") {
                            var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                            messdiv.textContent = "OCR has been performed successfully";
                            messdiv.style.color = "Green";
                            messdiv.style.fontSize = "1.5em";
                            messdiv.style.display = "";
                            funcSearchDocuments(" ");
                        }
                        else {
                            var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
                            messdiv.style.color = "Red";
                            messdiv.textContent = responseText.toString();
                            messdiv.style.fontSize = "1.5em";
                            messdiv.style.display = "";
                        }
                    },
                    error: function (responseText) {


                    }
                });
            }
        }

        public AddDocumentIndexRow(event): any {
            try {
                var divcounter = <HTMLInputElement>document.getElementById("divcounter");
                //var btnAddRow = <HTMLInputElement>document.getElementById("btnaddrowid");
                var rowsContainer = <HTMLInputElement>document.getElementById("RowsContainer");
                var rowContainer = document.createElement("tr");
                rowContainer.id = "Row_" + divcounter.value;
                rowContainer.setAttribute("data-counter", divcounter.value);

                //rowContainer.style.marginBottom = "5px";
                var addNameTD = document.createElement("td");
                //addNameTD.id = "addnamedivid_" + divcounter.value;
                //addNameTD.style.display = "inline-block";
                //addNameTD.style.marginRight = "10px";
                //addNameTD.style.width = "120px";
                var addNameInput = <HTMLInputElement>document.createElement("input");
                addNameInput.setAttribute("type", "text");
                addNameInput.setAttribute("placeholder", "Name");
                addNameInput.id = "addnameid_" + divcounter.value;
                addNameInput.className = "form-control";
                addNameTD.appendChild(addNameInput);
                rowContainer.appendChild(addNameTD);

                var addValueTD = document.createElement("td");
                //addValueTD.id = "addvaluedivid_" + divcounter.value;
                //addValueTD.style.display = "inline-block";
                //addValueTD.style.marginRight = "10px";
                //addValueTD.style.width = "120px";
                var addValueInput = <HTMLInputElement>document.createElement("input");
                addValueInput.setAttribute("type", "text");
                addValueInput.setAttribute("placeholder", "Value");
                addValueInput.id = "addvalueid_" + divcounter.value;
                addValueInput.className = "form-control";
                addValueTD.appendChild(addValueInput);
                rowContainer.appendChild(addValueTD);

                var addDatatypeTD = document.createElement("td");
                //addDatatypeDiv.id = "adddatatypedivid_" + divcounter.value;
                //addDatatypeDiv.style.display = "inline-block";
                //addDatatypeDiv.style.marginRight = "10px";
                //addDatatypeDiv.style.width = "180px";
                var addDatatypeSelect = <HTMLSelectElement>document.createElement("select");
                addDatatypeSelect.id = "adddatatypeid_" + divcounter.value;
                addDatatypeSelect.className = "form-control";


                var addDatatypeOption = <HTMLOptionElement>document.createElement("option");
                addDatatypeOption.id = "adddatatypeopt_" + divcounter.value;
                addDatatypeOption.text = "Select a datatype";
                addDatatypeOption.value = "-1";
                addDatatypeSelect.appendChild(addDatatypeOption);

                var datatypeMembers: string[] = Object.keys(DocumentIndexDataType).map(k => DocumentIndexDataType[k]).filter(v => typeof v === "string");
                var datatypeValues: number[] = Object.keys(DocumentIndexDataType).map(k => DocumentIndexDataType[k]).filter(v => typeof v === "number");
                if (datatypeMembers.length != datatypeValues.length) { throw ("Unable to add an Item"); }
                //for (var item in datatypeMembers) {
                //    addDatatypeOption = <HTMLOptionElement>document.createElement("option");
                //    addDatatypeOption.id = (parseInt(item)).toString();
                //    addDatatypeOption.text = item;
                //    addDatatypeOption.value = (parseInt(item)).toString();
                //    addDatatypeSelect.appendChild(addDatatypeOption);
                //}
                for (var i = 0; i < datatypeMembers.length; i++) {
                    addDatatypeOption = <HTMLOptionElement>document.createElement("option");
                    addDatatypeOption.id = (datatypeValues[i]).toString();
                    addDatatypeOption.text = datatypeMembers[i];
                    addDatatypeOption.value = (datatypeValues[i]).toString();
                    addDatatypeSelect.appendChild(addDatatypeOption);
                }

                addDatatypeTD.appendChild(addDatatypeSelect);
                rowContainer.appendChild(addDatatypeTD);

                var addActionTD = document.createElement("td");
                //addValueDiv.id = "btnremoverowdivid_" + divcounter.value;
                //addValueDiv.style.display = "inline-block";
                //addDatatypeDiv.style.marginRight = "10px";
                var addActionInputbtn = document.createElement("input");
                addActionInputbtn.setAttribute("type", "button");
                addActionInputbtn.setAttribute("value", "X");
                addActionInputbtn.setAttribute("title", "Remove Row");
                addActionInputbtn.setAttribute("data-counter", divcounter.value);
                addActionInputbtn.id = "btnremoverowid_" + divcounter.value;
                addActionInputbtn.className = "btn btn-danger";
                addActionInputbtn.onclick = function (e) {
                    var target = <HTMLInputElement>e.target;
                    var id = "Row_" + target.getAttribute("data-counter");
                    AffinityDms.Entities.Documents.prototype.RemoveRow(id);
                }
                //.setAttribute("onclick", "javascript: documentDesigner.RemoveRow(" + rowContainer.id + ")");
                //addValueInput.onclick = this.RemoveRow(addValueInput.id);
                addActionTD.appendChild(addActionInputbtn);
                rowContainer.appendChild(addActionTD);
                rowsContainer.appendChild(rowContainer);
                //var addhrDiv = document.createElement("hr");
                //addhrDiv.setAttribute("size", "1");
                //rowContainer.appendChild(addhrDiv);
                divcounter.value = (parseInt(divcounter.value) + 1).toString();
                var id = "#" + addNameInput.id;
                $(id).focus().select();
                //var AddIndexBtn = <HTMLInputElement>event.target;
                //AddIndexBtn.setAttribute("data-lastAdded", id);

                //AddIndexBtn.onmouseup = function (e) {
                //    var addIndexBtn = <HTMLInputElement>e.target;
                //    var id = "#" + addIndexBtn.getAttribute("data-lastAdded");
                //    $(id).focus().select();
                //};
            }
            catch (exception) {
                alert(exception.toString())
            }

        }
        public RemoveRow(addValueInput: any): any {
            var rowContainer;
            if (addValueInput instanceof HTMLTableRowElement) {
                rowContainer = addValueInput;
                rowContainer.remove();
            }
            else if (typeof addValueInput === "string") {
                rowContainer = <HTMLTableRowElement>document.getElementById(addValueInput);
                rowContainer.remove();
            }
            else {
                alert("Unable to remove the following element");
            }

        }
        public ZoomInDiv(event: any): any {
            var parentDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("ElementsContainer");
            parentDiv.style.transformOrigin = "left top";
            var val = parentDiv.style.transform.toString().toLowerCase();
            var res = val.replace("scale(", "");
            res = res.replace(")", "");
            parentDiv.style.transform = "scale(" + ((parseFloat(res)) + 0.01) + ")";
        }
        public ZoomOutDiv(event: any): any {
            var parentDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("ElementsContainer");
            parentDiv.style.transformOrigin = "left top";
            var val = parentDiv.style.transform.toString().toLowerCase();
            var res = val.replace("scale(", "");
            res = res.replace(")", "");
            parentDiv.style.transform = "scale(" + ((parseFloat(res)) - 0.01) + ")";
        }
        public FitDiv(event: any): any {
            var divImage: HTMLDivElement = <HTMLDivElement>document.getElementById("DocumentImage");
            var parentDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("ElementsContainer");
            var MainContainer: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
            parentDiv.style.zoom = "1";
            var dph = divImage.clientHeight;
            var pdh = parentDiv.clientHeight;
            var dpw = divImage.clientWidth;
            var pdw = parentDiv.clientWidth;
            var val = parentDiv.style.transform.toString().toLowerCase();
            var res = val.replace("scale(", "");
            res = res.replace(")", "");
            var imgheight = (divImage.clientHeight * (parseFloat(res)));
            var imgwidth = (divImage.clientWidth * (parseFloat(res)));
            if ((imgheight > parentDiv.clientHeight) || (imgwidth > parentDiv.clientWidth)) {
                while ((imgheight > parentDiv.clientHeight) || (imgwidth > parentDiv.clientWidth)) {
                    this.ZoomOutDiv(event);
                    val = parentDiv.style.transform.toString().toLowerCase();
                    res = val.replace("scale(", "");
                    res = res.replace(")", "");
                    imgheight = (divImage.clientHeight * (parseFloat(res)));
                    imgwidth = (divImage.clientWidth * (parseFloat(res)));
                }
            }
            else {
                while ((imgheight < parentDiv.clientHeight) && (imgwidth < parentDiv.clientWidth)) {
                    this.ZoomInDiv(event);
                    val = parentDiv.style.transform.toString().toLowerCase();
                    res = val.replace("scale(", "");
                    res = res.replace(")", "");
                    imgheight = (divImage.clientHeight * (parseFloat(res)));
                    imgwidth = (divImage.clientWidth * (parseFloat(res)));
                }
            }

        }
        public OnDivMouseWheel(event: MouseWheelEvent): any {
            if (event.wheelDelta > 0) {
                this.ZoomInDiv(event);
            }
            else {
                this.ZoomOutDiv(event);
            }
        }
        public ImageMoving = false;
        public OnDivImageMouseDown(event: MouseEvent): any {

            this.ImageMoving = true;
            var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
            H_ImageMove.value = "true";
            var divImage: HTMLDivElement = <HTMLDivElement>document.getElementById("DocumentImage");
            var curYPos = event.pageY;
            var curXPos = event.pageX;
            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
            maindiv.onmousemove = (e: any) => {
                var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
                if (H_ImageMove.value == "true") {
                    maindiv.scrollTop = maindiv.scrollTop + (curYPos - e.pageY);
                    curYPos = e.pageY;
                    maindiv.scrollLeft = maindiv.scrollLeft + (curXPos - e.pageX);//e.pageX;
                    curXPos = e.pageX;
                }
                maindiv.onmouseup = (ev: any) => {
                    var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
                    H_ImageMove.value = "false";
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
                    maindiv.onmousemove = null;
                }
            }
        }
        public OnDivImageMouseUp(event: MouseEvent): any {
            var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
            H_ImageMove.value = "false";
            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
            maindiv.onmousemove = null;
        }
        public onVersionSubmit(event): void {
            var versionNo = <HTMLInputElement>document.getElementById("VersionNo");
            var H_Documentid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Documentid")
            var H_DocumentidVal = -1;
            if (H_Documentid != null) {
                H_DocumentidVal = parseInt(H_Documentid.value);
            }
            $.ajax({
                type: "POST",
                url: "../../TenantDocumentIndex/UpdateVersion",
                data: JSON.stringify({ documentId: H_DocumentidVal, version: versionNo.value }),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (responseText: string) {
                    if (responseText.toString().toLowerCase() == "true") {
                        if ($("#DocumentIndexSuccessMessage") != null) {
                            $("#DocumentIndexSuccessMessage").css("display", "");
                            $("#DocumentIndexSuccessMessageText").text("Version successfully updated!!");
                            setTimeout(function () {
                                $('#DocumentIndexSuccessMessage').fadeOut('fast');
                                $("#DocumentIndexSuccessMessage").css("display", "none");
                                $("#DocumentIndexSuccessMessageText").text("");
                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                        }
                    }
                    else {
                        if ($("#DocumentIndexErrorMessage") != null) {
                            $("#DocumentIndexErrorMessage").css("display", "");
                            $("#DocumentIndexErrorMessageText").text(responseText.toString());
                            setTimeout(function () {
                                $('#DocumentIndexErrorMessage').fadeOut('fast');
                                $("#DocumentIndexErrorMessage").css("display", "none");
                                $("#DocumentIndexErrorMessageText").text("");
                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                        }
                    }
                },
                error: function (responseText: any) {
                    if ($("#DocumentIndexErrorMessage") != null) {
                        $("#DocumentIndexErrorMessage").css("display", "");
                        $("#DocumentIndexErrorMessageText").text("Oops an Error Occured");
                        setTimeout(function () {
                            $('#DocumentIndexErrorMessage').fadeOut('fast');
                            $("#DocumentIndexErrorMessage").css("display", "none");
                            $("#DocumentIndexErrorMessageText").text("");
                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                    }
                    //alert("Oops an Error Occured: " + responseText.toString());
                }
            });


        }
        public onCheckin(event): void {
            var H_Documentid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Documentid")
            var H_DocumentidVal = -1;
            if (H_Documentid != null) {
                H_DocumentidVal = parseInt(H_Documentid.value);
            }
            var documentIndexList: Array<DocumentIndex> = AffinityDms.Entities.Documents.prototype.CreateArrayOfDocumentIndexes();
            if (documentIndexList == null) { documentIndexList = new Array<DocumentIndex>(); }
            if (documentIndexList.length > 0) {
                var documentIndexListArray = JSON.stringify(documentIndexList);
                var H_Documentid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Documentid")
                $.ajax({
                    type: "POST",
                    url: "../../TenantDocumentIndex/SaveDocumentIndexes",
                    data: JSON.stringify({ documentIndexes: documentIndexListArray, documentId: H_DocumentidVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: string) {
                        if (responseText.toString().toLowerCase() == "true") {
                            if ($("#DocumentIndexSuccessMessage") != null) {
                                $("#DocumentIndexSuccessMessage").css("display", "");
                                $("#DocumentIndexSuccessMessageText").text("Document Saved Successfully");
                                $.ajax({
                                    type: "POST",
                                    url: "../../TenantDocumentIndex/Checkin",
                                    data: JSON.stringify({ documentId: H_DocumentidVal }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: 'json',
                                    success: function (responseText: string) {
                                        if (responseText.toString().toLowerCase() == "true") {
                                            var btncheckin = <HTMLInputElement>document.getElementById("btnCheckin");
                                            btncheckin.style.display = "none";
                                            location.reload(true);
                                            //window.location.href = "../../TenantDocumentsFolderWise/index?ErrorMessage=''&SuccessMessage='Document CheckedIn Successfull'"
                                            // alert("Check In successfull!!");
                                        }
                                        else if (responseText.toString().toLowerCase() == "false") {
                                            var btncheckin = <HTMLInputElement>document.getElementById("btnCheckin");
                                            btncheckin.style.display = "block";
                                        }
                                        else {
                                            if ($("#DocumentIndexErrorMessage") != null) {
                                                $("#DocumentIndexErrorMessage").css("display", "");
                                                $("#DocumentIndexErrorMessageText").text("An Error Occurred " + responseText.toString());
                                                setTimeout(function () {
                                                    $('#DocumentIndexErrorMessage').fadeOut('fast');
                                                    $("#DocumentIndexErrorMessage").css("display", "none");
                                                    $("#DocumentIndexErrorMessageText").text("");
                                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                            }
                                        }
                                    },
                                    error: function (responseText: any) {
                                        if ($("#DocumentIndexErrorMessage") != null) {
                                            $("#DocumentIndexErrorMessage").css("display", "");
                                            $("#DocumentIndexErrorMessageText").text("Oops an Error Occured");
                                            setTimeout(function () {
                                                $('#DocumentIndexErrorMessage').fadeOut('fast');
                                                $("#DocumentIndexErrorMessage").css("display", "none");
                                                $("#DocumentIndexErrorMessageText").text("");
                                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                        }
                                    }
                                });
                                //setTimeout(function () {
                                //    $('#DocumentIndexSuccessMessage').fadeOut('fast');
                                //    $("#DocumentIndexSuccessMessage").css("display", "none");
                                //    $("#DocumentIndexSuccessMessage").text("");
                                //}, 2000);
                            }
                        }
                        else {
                            throw new Error("Unable to perform save operation");
                        }
                    },
                    error: function (responseText: any) {
                        if ($("#DocumentIndexErrorMessage") != null) {
                            $("#DocumentIndexErrorMessage").css("display", "");
                            $("#DocumentIndexErrorMessageText").text("Oops an error occued");
                            setTimeout(function () {
                                $('#DocumentIndexErrorMessage').fadeOut('fast');
                                $("#DocumentIndexErrorMessage").css("display", "none");
                                $("#DocumentIndexErrorMessageText").text("");
                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                        }
                    }
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "../../TenantDocumentIndex/Checkin",
                    data: JSON.stringify({ documentId: H_DocumentidVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: string) {
                        if (responseText.toString().toLowerCase() == "true") {
                            var btncheckin = <HTMLInputElement>document.getElementById("btnCheckin");
                            btncheckin.style.display = "none";
                            location.reload(true);
                            //window.location.href = "../../TenantDocumentsFolderWise/index?ErrorMessage=''&SuccessMessage='Document CheckedIn Successfull'"
                            // alert("Check In successfull!!");
                        }
                        else if (responseText.toString().toLowerCase() == "false") {
                            var btncheckin = <HTMLInputElement>document.getElementById("btnCheckin");
                            btncheckin.style.display = "block";
                        }
                        else {
                            if ($("#DocumentIndexErrorMessage") != null) {
                                $("#DocumentIndexErrorMessage").css("display", "");
                                $("#DocumentIndexErrorMessageText").text("An Error Occurred " + responseText.toString());
                                setTimeout(function () {
                                    $('#DocumentIndexErrorMessage').fadeOut('fast');
                                    $("#DocumentIndexErrorMessage").css("display", "none");
                                    $("#DocumentIndexErrorMessageText").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                    },
                    error: function (responseText: any) {
                        if ($("#DocumentIndexErrorMessage") != null) {
                            $("#DocumentIndexErrorMessage").css("display", "");
                            $("#DocumentIndexErrorMessageText").text("Oops an Error Occured");
                            setTimeout(function () {
                                $('#DocumentIndexErrorMessage').fadeOut('fast');
                                $("#DocumentIndexErrorMessage").css("display", "none");
                                $("#DocumentIndexErrorMessageText").text("");
                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                        }
                    }
                });
            }
        }
        public onSubmitForm(event): void {
            try {
                var documentIndexList: Array<DocumentIndex> = AffinityDms.Entities.Documents.prototype.CreateArrayOfDocumentIndexes();
                if (documentIndexList != null) {
                    if (documentIndexList.length > 0) {
                        var documentIndexListArray = JSON.stringify(documentIndexList);
                        var H_Documentid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Documentid")
                        var H_DocumentVal = -1;
                        if (H_Documentid != null) {
                            H_DocumentVal = parseInt(H_Documentid.value);
                        }
                        $.ajax({
                            type: "POST",
                            url: "../../TenantDocumentIndex/SaveDocumentIndexes",
                            data: JSON.stringify({ documentIndexes: documentIndexListArray, documentId: H_DocumentVal }),
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            success: function (responseText: string) {
                                if (responseText.toString().toLowerCase() == "true") {
                                    if ($("#DocumentIndexSuccessMessage") != null) {
                                        $("#DocumentIndexSuccessMessage").css("display", "");
                                        $("#DocumentIndexSuccessMessageText").text("Document Saved Successfully");
                                        setTimeout(function () {
                                            $('#DocumentIndexSuccessMessage').fadeOut('fast');
                                            $("#DocumentIndexSuccessMessage").css("display", "none");
                                            $("#DocumentIndexSuccessMessageText").text("");
                                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                    }
                                }
                                else {
                                    throw new Error("Unable to perform save operation");
                                }
                            },
                            error: function (responseText: any) {
                                if ($("#DocumentIndexErrorMessage") != null) {
                                    $("#DocumentIndexErrorMessage").css("display", "");
                                    $("#DocumentIndexErrorMessageText").text("Oops an error occued");
                                    setTimeout(function () {
                                        $('#DocumentIndexErrorMessage').fadeOut('fast');
                                        $("#DocumentIndexErrorMessage").css("display", "none");
                                        $("#DocumentIndexErrorMessageeText").text("");
                                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                }
                            }
                        });
                    }
                    else {
                        throw new Error("No data Found");
                    }
                }
                else {
                    throw new Error("No data Found");
                }

            }
            catch (ex) {
                if ($("#DocumentIndexErrorMessage") != null) {
                    $("#DocumentIndexErrorMessage").css("display", "");
                    $("#DocumentIndexErrorMessageText").text(ex);
                    setTimeout(function () {
                        $('#DocumentIndexErrorMessage').fadeOut('fast');
                        $("#DocumentIndexErrorMessage").css("display", "none");
                        $("#DocumentIndexErrorMessageText").text("");
                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                }
            }

        }
        public UpdateOcrIndexedZones(event): any {
            var indexValues: NodeList = <NodeList>document.getElementsByName("IndexDataTypeValue");
            var DocumentElementVM = new Array();
            if (indexValues != null) {
                for (var i = 0; i < indexValues.length; i++) {
                    var indexValue = <HTMLInputElement>indexValues[i];
                    var DocumentElement = new DocumentElementViewModel();
                    DocumentElement.DocumentElementId = parseInt(indexValue.getAttribute("data-id"));
                    DocumentElement.DocuemntOcrText = indexValue.value;
                    DocumentElementVM.push(DocumentElement);
                }
                var DocumentElementVMStr = JSON.stringify(DocumentElementVM);
                $.ajax({
                    type: "POST",
                    url: "/TenantDocumentIndex/UpdateDocumentElement",
                    data: JSON.stringify({ DocumentElementVM: DocumentElementVMStr }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: any) {
                        if (responseText == true) {
                            if ($("#DocumentIndexSuccessMessage") != null) {
                                $("#DocumentIndexSuccessMessage").css("display", "");
                                $("#DocumentIndexSuccessMessageText").text("Document Updated");
                                setTimeout(function () {
                                    $('#DocumentIndexSuccessMessage').fadeOut('fast');
                                    $("#DocumentIndexSuccessMessage").css("display", "none");
                                    $("#DocumentIndexSuccessMessageText").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                        else {
                            if ($("#DocumentIndexErrorMessage") != null) {
                                $("#DocumentIndexErrorMessage").css("display", "");
                                $("#DocumentIndexErrorMessageText").text(responseText);
                                setTimeout(function () {
                                    $('#DocumentIndexErrorMessage').fadeOut('fast');
                                    $("#DocumentIndexErrorMessage").css("display", "none");
                                    $("#DocumentIndexErrorMessageText").text("");
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                            }
                        }
                    },
                    error: function (responseText) {
                    }
                });
            }
        }
        public CreateArrayOfDocumentIndexes(): Array<DocumentIndex> {
            //try {
            var documentIndexArray: Array<DocumentIndex> = new Array();
            var rowsContainer = <HTMLDivElement>document.getElementById("RowsContainer");
            for (var i = 0; i <= rowsContainer.childNodes.length; i++) {

                if (rowsContainer.childNodes[i] instanceof HTMLTableRowElement) {
                    //  var htmltrelement = <HTMLTableRowElement>rowsContainer.childNodes[i];
                    var rowContainer = <HTMLTableRowElement>rowsContainer.childNodes[i];
                    //var numberid: string[] = htmltrelement.id.split("_");
                    var idnumber = rowContainer.getAttribute("data-counter");//numberid[numberid.length - 1];
                    // var rowid: string = "Rows_" + idnumber;
                    var addnameid = "addnameid_" + idnumber;
                    var addvalueid = "addvalueid_" + idnumber;
                    var adddatatypeid = "adddatatypeid_" + idnumber;
                    //var rowContainer = <HTMLDivElement>document.getElementById(rowid);
                    var addName = <HTMLInputElement>document.getElementById(addnameid);
                    var addvalue = <HTMLInputElement>document.getElementById(addvalueid);
                    var adddatatype = <HTMLSelectElement>document.getElementById(adddatatypeid);
                    var documentindex = new DocumentIndex();
                    if (addName.value.length <= 0) { throw new Error("Please select a Name. Name can not be empty"); }
                    else {
                        documentindex.Name = addName.value;
                    }
                    if (addvalue.value.length <= 0) { throw new Error("Please select a Value. Value can not be empty"); }
                    else {
                        documentindex.Value = addvalue.value;
                    }
                    var opt = <HTMLOptionElement>adddatatype[adddatatype.selectedIndex];
                    if (parseInt(opt.value) >= 0) {
                        documentindex.DataType = DocumentIndexDataType[opt.value];
                        documentIndexArray.push(documentindex);
                    }
                    else {
                        throw new Error("Please select a Datatype. Datatype can not be empty");
                    }
                }
            }
            return documentIndexArray;
            //}
            //catch (exception) {
            //    throw;
            //}
        }

        public AddFolder(event): void {
            var CurrentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
            var folderNameTxtbx = <HTMLInputElement>document.getElementById("folderNameTxtbx");
            if (folderNameTxtbx.value.trim() != "") {
                $.ajax({
                    type: "POST",
                    url: "../../TenantDocumentsFolderWise/AddFolder",
                    data: JSON.stringify({ parentFolderId: CurrentFolderId.value, folderName: folderNameTxtbx.value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: any) {
                        if (typeof responseText === "number") {
                            var parentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
                            var folderNameTxtbx = <HTMLInputElement>document.getElementById("folderNameTxtbx");
                            var H_MainTreeViewId = <HTMLInputElement>document.getElementById("H_MainTreeViewId");
                            var treeviewId = "";
                            if (H_MainTreeViewId != null) {
                                treeviewId = H_MainTreeViewId.value;
                            }
                            else {
                                treeviewId = "treeview";
                            }
                            AffinityDms.Entities.Documents.prototype.AppendNode(responseText, folderNameTxtbx.value, treeviewId);
                            folderNameTxtbx.value = "";
                            var closeFolderAddBtn = <HTMLInputElement>document.getElementById('closeFolderAddBtn');
                            closeFolderAddBtn.click();
                            var addFolderModalError = document.getElementById("addFolderModalError");
                            if (addFolderModalError != null) { addFolderModalError.textContent = ""; addFolderModalError.style.display = "none"; }
                            var addFolderModalSuccess = document.getElementById("addFolderModalSuccess");
                            if (addFolderModalSuccess != null) { addFolderModalSuccess.textContent = "Folder added successfully!"; addFolderModalSuccess.style.display = ""; }
                            $("#addFolderModal").modal("hide");
                        }
                        else {
                            var addFolderModalError = document.getElementById("addFolderModalError");
                            if (addFolderModalError != null) { addFolderModalError.textContent = responseText.toString(); addFolderModalError.style.display = ""; }
                            var addFolderModalSuccess = document.getElementById("addFolderModalSuccess");
                            if (addFolderModalSuccess != null) { addFolderModalSuccess.textContent = ""; addFolderModalSuccess.style.display = "none"; }
                        }
                    },
                    error: function (responseText: any) {
                        var addFolderModalError = document.getElementById("addFolderModalError");
                        if (addFolderModalError != null) { addFolderModalError.textContent = "Something went wrong. Please reload the page and retry"; addFolderModalError.style.display = ""; }
                        var addFolderModalSuccess = document.getElementById("addFolderModalSuccess");
                        if (addFolderModalSuccess != null) { addFolderModalSuccess.textContent = ""; addFolderModalSuccess.style.display = "none"; }
                    }
                });
            }
            else {

            }
        }
        public AppendNode(newFodlerId: number, folderName: string, treeViewId): any {
            var treeId = "#" + treeViewId;
            var treeview = $(treeId).data("kendoTreeView");
            var selectedNode = treeview.select();
            // passing a false value as the second append() parameter
            // will append the new node to the root group
            if (selectedNode.length == 0) {
                selectedNode = null;
            }
            var user: any = window["GetCurrentLoggedInUser"]();
            treeview.append({
                text: folderName,
                Id: newFodlerId,
                HasChildren: false,
                Name: folderName,
                UserCreatedById: user,
                Template: $("#treeviewFolderPartiaTemplateKendo").text()
            }, selectedNode);
            setTimeout(function () {
                var PartialTreeviewFolderEdits = document.getElementsByClassName("PartialTreeviewFolderEdit");
                AffinityDms.Entities.Documents.prototype.setFolderToolTip(newFodlerId);
            }, 2000);

        }
        public setFolderToolTip(id:any):any {
        var foldereditid = "PartialTreeviewFolderEdit_" + id;
        var folderediticon = "PartialTreeviewFolderEditIcon_" + id;
        var PartialTreeviewFolderEditBtn = document.getElementById(foldereditid);
        var PartialTreeviewFolderName = document.getElementById(PartialTreeviewFolderEditBtn.getAttribute("data-foldernamecontainerid"));
        var folderid = PartialTreeviewFolderName.getAttribute("data-id");
        var foldername = PartialTreeviewFolderName.getAttribute("data-name");
        var str = ("<ul>"
            + "<li style='cursor:pointer'>"
            + "<a style='cursor:pointer' onclick='javascript: ShowRenameFolderModal(event)' data-id='" + folderid + "' data-name='" + foldername + "'>"
            + "<i class='ace-icon fa fa-pencil-square-o' data-id='" + folderid + "'  data-name='" + foldername + "'></i>Rename</a>"
            + "</li>"
            + "<li style='cursor:pointer'>"
            + "<a style='cursor:pointer' data-toggle='modal' data-target='#RightsModal' data-id='" + folderid + "' data-type='Folder'>"
            + "<i class='ace-icon fa fa-users' data-type='Folder' data-id='" + folderid + "'></i>User Rights</a>"
            + "</li>"
            + "</ul>");
        //+ "<li style='cursor:pointer'>"
        //+ "<a style='cursor:pointer' href='\\#' data-toggle='modal' data-target='\\#RightsModal' data-id='" + folderid + "' data-type='Folder'>"
        //+ "<i class='ace-icon fa fa-users' data-type='document' data-id='" + folderid + "'></i>User Rights</a>"
        //+ "</li>"
        $("#" + foldereditid).attr("title", str);
        var tooltipholder:any = $("#" + foldereditid);
        tooltipholder.tooltip({
            placement: "auto bottom",
            html: "true",
            trigger: "focus",
            container: "div#divid3",
            title: function () {
                return $(this).attr('title');
            },
        });
        $("#" + folderediticon).on("click", function (e) {
            return false;
            //var targetid = "PartialTreeviewFolderEdit_" + e.target.getAttribute("data-id");
            //$("#" + targetid).tooltip('show');
            //return false;
        })
    }

        //public SetKendoFolderTreeViewModal():void {
        //var FolderTreeViewModal = $("FolderTreeViewModal").data("kendoTreeView");
        //// FolderTreeViewModal.data('kendoTreeView')
        //if (FolderTreeViewModal != null) {

        //    var CurrentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
        //    //var FolderTreeViewModal:any  = document.getElementById("FolderTreeViewModal");
        //    //FolderTreeViewModal.data('kendoTreeView');
        //    var dataItem = FolderTreeViewModal.dataSource.get(CurrentFolderId.value);
        //    if (dataItem != null) {
        //        var node = FolderTreeViewModal.findByUid(dataItem.uid);
        //        node = FolderTreeViewModal.findByUid(dataItem.uid);
        //        if (node != null) {
        //            var li = node[0];
        //            li.setAttribute("aria-selected", "true");
        //            li.setAttribute("id", "treeview_tv_active");
        //            for (var i = 0; i < li.childNodes.length; i++) {
        //                var chkdiv = li.childNodes[i]
        //                if (chkdiv instanceof HTMLDivElement) {
        //                    for (var j = 0; j < chkdiv.childNodes.length; j++) {
        //                        var chkspan = chkdiv.childNodes[j];
        //                        if (chkspan instanceof HTMLSpanElement) {
        //                            chkspan.className = "k-in k-state-selected";
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //}


        //public RenderTreeView(event): void {


        //    //$.ajax({

        //    //    type: "POST",
        //    //    data: JSON.stringify({ id: null }),
        //    //    url: "../../TenantDocumentsFolderWise/Folder_Read",
        //    //    contentType: "application/json; charset=utf-8",
        //    //    dataType: "json",
        //    //    success: function (data) {
        //    //        var treeview = $("treeview").data("kendoTreeView");
        //    //        treeview.dataSource.data(data);
        //    //    },
        //    //    error: function (responseText: any) {
        //    //        alert(responseText);
        //    //    }
        //    //});
        //}

        public CheckViewType(ViewType: string): string {
            switch (ViewType) {
                case "Folder":
                    return "Folder";
                case "Document":
                    return "Document";
                case "Template":
                    return "Template";
                case "Form":
                    return "Form";
                default:
                    return "";
            }
        }

        public MoveDocuemtinFolder(event): void {
            var _docId = <HTMLInputElement>document.getElementById("hdocId");
            var _folderId = <HTMLInputElement>document.getElementById("MoveFolderTo");
            if (_folderId != null) {
                var hfolderid = _folderId.value;
                var hdocId = _docId.value;
                $.ajax({
                    type: "POST",
                    data: JSON.stringify({ folderId: hfolderid, documentId: hdocId }),
                    url: "../../TenantDocumentIndex/MoveDocumentFolder",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data == true) {
                            var closeFolderAddBtn = <HTMLInputElement>document.getElementById('closeFolderAddBtn');
                            closeFolderAddBtn.click();
                            var _folderId = <HTMLInputElement>document.getElementById("MoveFolderTo");

                            var docId = $("#hdocId").val();
                            var documentlistgrid = document.getElementById("DocumentListGrid");
                            var documentlistgridmodal = document.getElementById("DocumentListGridModal");
                            var currentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");

                            if (documentlistgrid != null) {
                                funcSearchDocuments(" ");
                            }
                            if (documentlistgridmodal != null) {
                                //var id = docId;
                                //var HtmlTDs = document.getElementsByName("DocumentNo");
                                //for (var i = 0; i < HtmlTDs.length; i++) {
                                //    if (HtmlTDs[i].textContent == id) {
                                //        HtmlTDs[i].parentElement.remove();
                                //    }
                                //}
                                AffinityDms.Entities.Documents.prototype.populateDocumentGridByDocumentID(currentFolderId.value);
                            }
                            currentFolderId.value = _folderId.value;
                            _folderId.value = "-1";
                            //alert(data);
                        }
                        else {
                            if ($("#FolderTreeViewModalErrorHeader") != null) {
                                $("#FolderTreeViewModalErrorDiv").text("");
                                $("#FolderTreeViewModalErrorDiv").text(data);
                                $("#FolderTreeViewModalErrorHeader").show();
                                setTimeout(function () {
                                    $("#FolderTreeViewModalErrorDiv").text("");
                                    $("#FolderTreeViewModalErrorHeader").hide();
                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);

                            }
                        }

                    },
                    error: function (data) {
                        alert(data);
                    }
                });
            }
            else {
                alert("unable to find the specified folder");
            }

        }

        public populateDocumentGridByDocumentID(folderid): void {
            if (folderid != null) {
                if (folderid > 0) {
                    $.ajax({
                        type: "POST",
                        url: "../../TenantDocumentsFolderWise/GetDocumentByFolderId?folderId=",
                        data: JSON.stringify({ folderId: folderid }),
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (responseData) {
                            if (responseData != null) {
                                var documents = JSON.parse(responseData);
                                if (typeof document === "string") {
                                    $("#DocumentListGridModal").data("kendoGrid").dataSource.data([]);
                                }
                                else {
                                    $("#DocumentListGridModal").data("kendoGrid").dataSource.data(documents);
                                }
                            }
                            else
                                $("#DocumentListGridModal").data("kendoGrid").dataSource.data([]);
                        },
                        error: function (responseText) {
                            alert(responseText);
                        }
                    });
                }
                else {
                    alert("Unable to find the folder");
                }
            }
            else {
                alert("Unable to find the folder");
            }

        }

        public SaveRights(event): void {
            var UserRightsCheckBoxes = document.getElementsByName("CheckUserRights");
            var UserList: Array<number> = new Array();
            var ViewType = <HTMLInputElement>document.getElementById("ViewType");
            var ViewId = <HTMLInputElement>document.getElementById("ViewId");
            if (ViewType != null && ViewId != null) {
                for (var i = 0; i < UserRightsCheckBoxes.length; i++) {
                    var CheckBox = <HTMLInputElement>UserRightsCheckBoxes[i];
                    if (CheckBox != null) {
                        if (CheckBox.checked == true) {
                            var id: number = parseInt(CheckBox.getAttribute("data-id"));
                            UserList.push(id);
                        }
                    }
                }
                var urlstr: string = "";
                switch (ViewType.value) {
                    case "Folder":
                        urlstr = "../../TenantFolders/SetUserRights"
                        break;
                    case "Document":
                        urlstr = "../../TenantDocuments/SetUserRights"
                        break;
                    case "Template":
                        urlstr = "../../TenantTemplates/SetUserRights"
                        break;
                    case "Form":
                        urlstr = "../../TenantForms/SetUserRights"
                        break;
                }
                $.ajax({
                    url: urlstr,
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json charset=utf-8",
                    data: JSON.stringify({ UserList: UserList, Id: ViewId.value }),
                    success: function (response) {
                        if (typeof response === "boolean") {
                            $('#closeFolderRightsBtn').click();
                            //if (response == true) {
                            //    alert("User rights successfully applied.");
                            //}
                            if (response == false) {
                                alert("Unable to apply user rights.");
                            }
                        }
                        else if (typeof response === "string") {
                            alert("Oops an error occurred: " + response);
                        }
                    },
                    error: function (response) {
                        alert(response);
                    }
                });
            }
        }

        public AddDocuments(event): void {
            //var 
        }


        public processDocument(event): void {
            var radios = document.getElementsByName("templateSelection");
            for (var i = 0; i < radios.length; i++) {
                var radio = <HTMLInputElement>radios[i];
                if (radio.checked) {
                    var templateid = radio.getAttribute("data-id");
                    var hdoc = <HTMLInputElement>document.getElementById("hDocId");
                    if (hdoc != null) {
                        var docId: number = parseInt(hdoc.value);
                        var tempid: number = parseInt(templateid);
                        $.ajax({
                            type: "POST",
                            url: "../../TenantDocumentManualClassification/ProcessDocument",
                            dataType: "json",
                            data: JSON.stringify({ documentId: docId, templateId: tempid }),
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                alert("Your document is in proccess for manual classification");
                            },
                            error: function (data) {
                                alert(data);
                            }
                        });
                    }

                }
            }
        }

        public UploadDocumentsToAzure(event): void {
            if ($("#DocumentUploadModalErrorHeader") != null) {
                $("#DocumentUploadModalErrorHeader").hide();
                $("#DocumentUploadModalErrorDiv").hide();
                $("#DocumentUploadModalErrorDiv").text("");

            }
            var _folderId = <HTMLInputElement>document.getElementById("MoveFolderTo");
            if (_folderId != null) {
                var hfolderid = _folderId.value;
                var parsedId = parseInt(hfolderid);
                if (parsedId > 1) {
                    //========= Start clear status div when load popup=========//
                    var divStatus = <HTMLDivElement>document.getElementById("divStatus");
                    divStatus.textContent = "";
                    //========= End clear status div when load popup=========//

                    //========== disable upload control =======================//
                    disableFileControl();

                    $.ajax({
                        type: "POST",
                        data: JSON.stringify({ folderId: hfolderid }),
                        url: "../../TenantDocumentsFolderWise/UploadDocuemntsToAzure",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data: any) {

                            if (data instanceof Object) {
                                var filesStatus: Array<FileUploadStatus> = <Array<FileUploadStatus>>data;

                                var divStatus = <HTMLDivElement>document.getElementById("divStatus");
                                var table = document.createElement("table")
                                table.className = "table";

                                var thead = document.createElement("thead")

                                var tr = document.createElement("tr");

                                var th1 = document.createElement("th");
                                var thtext1 = document.createTextNode("Document")
                                th1.appendChild(thtext1);

                                var th2 = document.createElement("th");
                                var thtext2 = document.createTextNode("Status")
                                th2.appendChild(thtext2);

                                var th3 = document.createElement("th");
                                var thtext3 = document.createTextNode("Message")
                                th3.appendChild(thtext3);

                                tr.appendChild(th1);
                                tr.appendChild(th2);
                                tr.appendChild(th3);

                                thead.appendChild(tr);

                                table.appendChild(thead);

                                var tbody = document.createElement("tbody");

                                for (var i = 0; i < filesStatus.length; i++) {

                                    var tr = document.createElement('tr');

                                    var td1 = document.createElement('td');
                                    var td2 = document.createElement('td');
                                    var td3 = document.createElement('td');

                                    var text1 = document.createTextNode(filesStatus[i].FileName);

                                    var text2;
                                    if (filesStatus[i].Finalized) {
                                        text2 = document.createTextNode('Succeeded');
                                        tr.className = "success";
                                        $("#DocumentUploadModal").modal("hide");
                                    }
                                    else {
                                        text2 = document.createTextNode('Failed');
                                        tr.className = "danger";
                                    }
                                    var text3;
                                    if (filesStatus[i].StatusMessage.length >= 60) {
                                        text3 = document.createTextNode(filesStatus[i].StatusMessage.substring(0, 60) + "...");
                                    }
                                    else
                                        text3 = document.createTextNode(filesStatus[i].StatusMessage);

                                    td1.appendChild(text1);
                                    td2.appendChild(text2);
                                    td3.appendChild(text3);
                                    td3.title = filesStatus[i].StatusMessage;
                                    tr.appendChild(td1);
                                    tr.appendChild(td2);
                                    tr.appendChild(td3);

                                    tbody.appendChild(tr);
                                }

                                table.appendChild(tbody);
                                divStatus.appendChild(table);

                            }

                            // enable upload control
                            enableFileControl();
                            // refresh File Control
                            resetFileControl()

                            //this code is used for close popup
                            //var closeFolderAddBtn = <HTMLInputElement>document.getElementById('closeFolderAddBtn');
                            //closeFolderAddBtn.click();
                            var currentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
                            AffinityDms.Entities.Documents.prototype.populateDocumentGridByDocumentID(currentFolderId.value);
                        },
                        error: function (data) {
                            //enable upload control
                            enableFileControl();

                            alert("Opps an error occured " + data);
                        }
                    });
                }
                else {
                    if ($("#DocumentUploadModalErrorHeader") != null) {
                        $("#DocumentUploadModalErrorHeader").show();
                        $("#DocumentUploadModalErrorDiv").show();

                        $("#DocumentUploadModalErrorDiv").text("No Folder Selected");
                        setTimeout(function () {
                            $('#DocumentUploadModalErrorDiv').fadeOut('fast');
                            $("#DocumentUploadModalErrorHeader").css("display", "none");
                            $("#DocumentUploadModalErrorDiv").text("");
                            $("#DocumentUploadModalErrorDiv").hide();

                        }, 2000);
                    }
                }
            }
            else {
                if ($("#DocumentUploadModalErrorHeader") != null) {
                    $("#DocumentUploadModalErrorHeader").show();
                    $("#DocumentUploadModalErrorDiv").show();

                    $("#DocumentUploadModalErrorDiv").text("Unable to find the specified folder");
                    setTimeout(function () {
                        $('#DocumentUploadModalErrorDiv').fadeOut('fast');
                        $("#DocumentUploadModalErrorHeader").css("display", "none");
                        $("#DocumentUploadModalErrorDiv").text("");
                        $("#DocumentUploadModalErrorDiv").hide();

                    }, 2000);
                }

            }
        }

        public SaveUserSelectionModal(): any {
            var SelectedId = <HTMLInputElement>document.getElementById("SelectedId");
            if (SelectedId != null) {
                //var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
                //TenantUsersSelectionListGrid.textContent = "";
                var SelectedUsersList = new Array<number>();
                var SelectedUsers = <NodeList>document.getElementsByName("CheckUserSelection");
                if (SelectedUsers != null) {
                    for (var i = 0; i < SelectedUsers.length; i++) {
                        var SelectedUser = <HTMLInputElement>SelectedUsers[i];
                        if (SelectedUser.checked) {
                            var uid = SelectedUser.getAttribute("data-id");
                            if (uid != null) {
                                var id = parseInt(uid);
                                SelectedUsersList.push(id);
                            }
                        }
                    }
                }
                var documentid = parseInt(SelectedId.value);
                var SelectedUsersListStringify = JSON.stringify(SelectedUsersList);
                $.ajax({
                    type: "POST",
                    url: "../../TenantDocuments/AddRemoveUser",
                    data: JSON.stringify({ documentId: documentid, selectedUsers: SelectedUsersList }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (response: any) {
                        try {
                            var users: Array<User> = <Array<User>>JSON.parse(response);
                            if (users.length > 0) {
                                var modal: any = $('#UserSelectionModal');
                                modal.modal('hide');
                            }
                        }
                        catch (e) {
                            alert(response);
                        }
                    },
                    error: function (responseText: any) {
                        alert("Oops an Error Occured");
                    }
                });
                //if (SelectedUsersList.length <= 0) {
                //    alert("No Users Selected");
                //}
                //else {
                //    var documentid = parseInt(SelectedId.value);
                //    var SelectedUsersListStringify = JSON.stringify(SelectedUsersList);
                //    $.ajax({
                //        type: "POST",
                //        url: "../../TenantDocuments/AddRemoveUser",
                //        data: JSON.stringify({ documentId: documentid, selectedUsers: SelectedUsersList }),
                //        contentType: "application/json; charset=utf-8",
                //        dataType: 'json',
                //        success: function (response: any) {
                //            try {
                //                var users: Array<User> = <Array<User>>JSON.parse(response);
                //                if (users.length > 0) {
                //                    var modal: any = $('#UserSelectionModal');
                //                    modal.modal('hide');
                //                }
                //            }
                //            catch (e) {
                //                alert(response);
                //            }
                //        },
                //        error: function (responseText: any) {
                //            alert("Oops an Error Occured");
                //        }
                //    });
                //}

            }
        }

        public SendExternalUserInvite(event): any {
            try {
                var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                EmailErrorDiv.style.display = "none";
                EmailErrorDiv.textContent = "";
                var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                EmailSuccessDiv.style.display = "none";
                EmailSuccessDiv.textContent = "";
                var SelectedId = <HTMLInputElement>document.getElementById("SelectedId");
                if (SelectedId != null) {
                    var InviteUserEmail = <HTMLInputElement>document.getElementById("InviteUserEmail");
                    if (InviteUserEmail != null) {
                        if (InviteUserEmail.value != "") {
                            var SelectedIdVal = SelectedId.value;
                            var datatype = SelectedId.getAttribute("data-type");
                            var url: string = "../../TenantDocuments/InviteUser";
                            $.ajax({
                                type: "POST",
                                url: url,
                                data: JSON.stringify({ documentId: SelectedIdVal, email: InviteUserEmail.value }),
                                contentType: "application/json; charset=utf-8",
                                dataType: 'json',
                                success: function (response: string) {
                                    try {
                                        var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                                        var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                                        var InviteUserName = <HTMLInputElement>document.getElementById("InviteUserName");
                                        var InviteUserEmail = <HTMLInputElement>document.getElementById("InviteUserEmail");
                                        if (response.toString() == "true") {
                                            EmailSuccessDiv.textContent = "Invitation successfully sent.";
                                            EmailSuccessDiv.style.display = "";
                                            EmailErrorDiv.style.display = "none";
                                            EmailErrorDiv.textContent = "";
                                            InviteUserEmail.value = "";
                                        }
                                        else if (response.toString() == "false") {
                                            EmailSuccessDiv.textContent = "";
                                            EmailSuccessDiv.style.display = "none";
                                            EmailErrorDiv.style.display = "";
                                            EmailErrorDiv.textContent = "Unable to send invitation. Please try resending an invitation";
                                        }
                                        else {
                                            throw response;
                                        }
                                    }
                                    catch (e) {
                                        EmailSuccessDiv.textContent = "";
                                        EmailSuccessDiv.style.display = "none";
                                        EmailErrorDiv.style.display = "";
                                        EmailErrorDiv.textContent = e.toString();
                                        InviteUserEmail.value = "";
                                    }
                                },
                                error: function (responseText: any) {
                                    alert("Oops an Error Occured");
                                }

                            });
                        }
                        else {
                            throw "Email is Required";
                        }

                    }
                    else {
                        throw "Email is Required";
                    }

                }
                else {
                    throw "Unable to find the Following Document";
                }
            }
            catch (e) {
                var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                EmailErrorDiv.style.display = "";
                EmailErrorDiv.textContent = e;
                var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                EmailSuccessDiv.style.display = "none";
                EmailSuccessDiv.textContent = "";

            }
        }

        public RenameDocument(event): any {
            var target = event.target;
            var docid = target.getAttribute("data-id");
            if (docid != null) {
                if ($("#DocumentRenameModalTextBox") != null) {
                    var name: string = $("#DocumentRenameModalTextBox").val();
                    if (name != null) {
                        if (name.trim() != "") {
                            $.ajax({
                                type: "POST",
                                url: "../../TenantDocumentsFolderWise/RenameDocument",
                                data: JSON.stringify({ id: docid, documentName: name }),
                                contentType: "application/json; charset=utf-8",
                                dataType: 'json',
                                success: function (response: any) {
                                    if (typeof (response) === "boolean") {
                                        $("#DocumentRenameModalTextBox").val("");
                                        $("#DocumentRenameModalTextBox").attr("placeholder", "");
                                        $("#DocumentRenameModalSubmitBtn").attr("data-id", "");
                                        $("#DocumentRenameModalSubmitBtn").attr("data-name", "");
                                        $("#DocumentRenameModalErrorHeaderDiv").text("");
                                        $("#DocumentRenameModalErrorHeader").hide();
                                        $("#DocumentRenameModal").modal("hide");
                                        if ($("#CurrentFolderId") != null) {
                                            var folderId = $("#CurrentFolderId").val();
                                            SetSelectedElement(folderId);
                                        }
                                    }
                                    else if (typeof (response) === "string") {
                                        $("#DocumentRenameModalErrorHeaderDiv").text(response);
                                        $("#DocumentRenameModalErrorHeader").show();
                                        setTimeout(function () {
                                            $("#DocumentRenameModalErrorHeaderDiv").text("");
                                            $("#DocumentRenameModalErrorHeader").hide();
                                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer)
                                    }
                                },
                                error: function (responseText: any) {
                                    $("#DocumentRenameModalErrorHeaderDiv").text("Oops an Error occured");
                                    $("#DocumentRenameModalErrorHeader").show();
                                    setTimeout(function () {
                                        $("#DocumentRenameModalErrorHeaderDiv").text("");
                                        $("#DocumentRenameModalErrorHeader").hide();
                                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer)
                                }
                            });
                        }
                        else {
                            $("#DocumentRenameModalErrorHeaderDiv").text("Name can not be left blank");
                            $("#DocumentRenameModalErrorHeader").show();
                            setTimeout(function () {
                                $("#DocumentRenameModalErrorHeaderDiv").text("");
                                $("#DocumentRenameModalErrorHeader").hide();
                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer)
                        }
                    }
                    else {
                        $("#DocumentRenameModalErrorHeaderDiv").text("Unable to fine document name");
                        $("#DocumentRenameModalErrorHeader").show();
                        setTimeout(function () {
                            $("#DocumentRenameModalErrorHeaderDiv").text("");
                            $("#DocumentRenameModalErrorHeader").hide();
                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer)
                    }

                }
            }
            else {
                $("#DocumentRenameModalErrorHeaderDiv").text("Unable to find requested document");
                $("#DocumentRenameModalErrorHeader").show();
                setTimeout(function () {
                    $("#DocumentRenameModalErrorHeaderDiv").text("");
                    $("#DocumentRenameModalErrorHeader").hide();
                }, AffinityDms.Settings.Constants.PopUpNotificationTimer)
            }
        }

        public GetIndexUserForAssignment(RowId: string): void {
            var row = document.getElementById(RowId);
            if (row != null) {
                $.ajax({
                    type: "GET",
                    url: "../../TenantUsers/GetIndexUsers",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var users = new Array<User>()
                        if (typeof (data) === "string") {
                            users = <Array<User>>JSON.parse(data);
                        }
                        else {
                            users = <Array<User>>data;
                        }
                        if (users != null) {
                            var ModalBodyId = <HTMLInputElement>document.getElementById("ModalBodyId");
                            var row = document.getElementById(ModalBodyId.value);
                            if (row != null) {
                                row.textContent = "";
                                //ModalBodyId.value;
                                var select = document.createElement("Select");
                                select.id = "UserAssignmentSelection";
                                select.classList.add("form-control");
                                for (var i = 0; i < users.length; i++) {
                                    var opt = document.createElement("option");
                                    opt.id = users[i].Id.toString();
                                    opt.value = users[i].Id.toString();
                                    opt.textContent = users[i].NameFull.toString();
                                    select.appendChild(opt);
                                }
                                row.appendChild(select);
                            }
                        }
                    },
                    error: function (data) {
                        alert(data);
                    }
                });
            }
        }
        public SubmitAssignment(event): void {
            var hdocId = <HTMLInputElement>document.getElementById("hdocId");
            var selectList = <HTMLSelectElement>document.getElementById("UserAssignmentSelection");
            var userid = selectList.options[selectList.selectedIndex].id;
            if (hdocId != null && selectList != null) {
                $.ajax({
                    type: "POST",
                    url: "../../TenantUsers/SubmitAssignment",
                    data: JSON.stringify({ id: hdocId.value, userId: userid, docType: DiscussionPostAttachmentType.Document }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (typeof (data) === "string") {
                            alert(data);
                        }
                        else if (data == true) {
                            var modal: any = $('#UserAssignmentModal');
                            modal.modal('hide');
                            var CurrentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
                            if (CurrentFolderId != null) {
                                var id = parseInt(CurrentFolderId.value);
                                SetSelectedElement(id);
                            }

                        }
                    },
                    error: function (data) {
                        alert(data);
                    }
                });
            }


        }


    }
}

function SetSelectedElement(folderId: number): void {
    if ($("#divid5") != null) {
        $("#divid5").load("../../TenantDocumentsFolderWise/GetSelectedFolderDocuments?folderid=" + folderId, function () {
            $(window).scrollTop(0);
        });
    }
    else {
    }

    //$.ajax({
    //    type: "POST",
    //    url: "../../TenantDocumentsFolderWise/GetSelectedFolderDetails",
    //    data: JSON.stringify({ folderid: folderId }),
    //    contentType: "application/json; charset=utf-8",
    //    dataType: 'json',
    //    success: function (response: any) {
    //        if (typeof response === "string") {
    //            var data = JSON.parse(response);

    //            if (data instanceof Object) {
    //                var documentFolderTreeViewModel = new AffinityDms.Entities.DocumentFolderTreeViewModel();
    //                documentFolderTreeViewModel.ChildFolders = data.ChildFolders;
    //                documentFolderTreeViewModel.Parentfolder = data.Parentfolder;
    //                documentFolderTreeViewModel.ParentFolderDocuments = data.ParentFolderDocuments;
    //                //var DocumentFolderTreeVM = <AffinityDms.Entities.DocumentFolderTreeViewModel>documentFolderTreeViewModel;
    //                //////////////if (documentFolderTreeViewModel.ChildFolders != null) {
    //                //////////////    $("#FolderListGrid").data("kendoGrid").dataSource.data(documentFolderTreeViewModel.ChildFolders);
    //                //////////////}
    //                //////////////else {
    //                //////////////    $("#FolderListGrid").data("kendoGrid").dataSource.data([]);
    //                //////////////}

    //                if (documentFolderTreeViewModel.ParentFolderDocuments != null) {
    //                    $("#DocumentListGridModal").data("kendoGrid").dataSource.data(documentFolderTreeViewModel.ParentFolderDocuments);
    //                }
    //                else {
    //                    $("#DocumentListGridModal").data("kendoGrid").dataSource.data([]);
    //                }
    //                $(window).scrollTop(0);
    //            }
    //            else {
    //                alert("An Error Occurred " + data.toString());
    //            }
    //        }
    //        else {
    //            alert("An Error Occurred " + data.toString());
    //        }

    //    },
    //    error: function (responseText) {
    //        alert("Oops an Error Occured");
    //    }
    //});

}

function monthformater(value: string): string {
    switch (value) {
        case "0":
            value = "01";
            return value;
        case "1":
            value = "02";
            return value;
        case "2":
            value = "03";
            return value;
        case "3":
            value = "04";
            return value;
        case "4":
            value = "05";
            return value;
        case "5":
            value = "06";
            return value;
        case "6":
            value = "07";
            return value;
        case "7":
            value = "08";
            return value;
        case "8":
            value = "09";
            return value;
        default:
            var mon = (parseInt(value) + 1).toString();
            return mon;
    }
}

function dateformater(value: string): string {
    switch (value) {
        case "0":
            value = "00";
            return value;
        case "1":
            value = "01";
            return value;
        case "2":
            value = "02";
            return value;
        case "3":
            value = "03";
            return value;
        case "4":
            value = "04";
            return value;
        case "5":
            value = "05";
            return value;
        case "6":
            value = "06";
            return value;
        case "7":
            value = "07";
            return value;
        case "8":
            value = "08";
            return value;
        case "9":
            value = "09";
            return value;
        default:
            return value;
    }
}

function BindPreDropdown() {

    var ddlPreviousSearch = <HTMLSelectElement>document.getElementById("ddlPreviousSearch");
    //var divMessage = <HTMLDivElement>document.getElementById("MessageDiv");

    var ddllength = ddlPreviousSearch.options.length;
    for (var i = 0; i < ddllength; i++) {
        ddlPreviousSearch.remove(0);
    }
    var opt = document.createElement("option");
    opt.id = "0";
    opt.value = "0";
    opt.textContent = "Please Select";
    ddlPreviousSearch.appendChild(opt);
    $.ajax({
        type: "GET",
        url: "/TenantDocuments/BindPreviousSearch",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (responseData) {
            for (var i = 0; i < responseData.length; i++) {
                var ddlprevioussearch = <HTMLSelectElement>document.getElementById("ddlPreviousSearch");
                var opt = document.createElement("option");
                opt.id = responseData[i].Id;
                opt.value = responseData[i].Name;
                opt.textContent = responseData[i].Name;
                ddlprevioussearch.appendChild(opt);
                //divMessage.textContent = "";
            }

        },
        error: function (responseData) {

            //divMessage.textContent = "";
        }
    });
}

function funcSearchDocuments(event) {

    var DocumentName = <HTMLInputElement>document.getElementById("documentname");
    var TemplateName = <HTMLInputElement>document.getElementById("templatename");
    var FolderName = <HTMLInputElement>document.getElementById("folderame");
    var TagsUser = <HTMLInputElement>document.getElementById("usertags");
    var TagsGlobal = <HTMLInputElement>document.getElementById("tagsglobal");
    var Content = <HTMLInputElement>document.getElementById("content");
    var StartDate = <HTMLInputElement>document.getElementById("startdate");
    var EndDate = <HTMLInputElement>document.getElementById("enddate");
    var txtSearchName = <HTMLInputElement>document.getElementById("txtSearchName");
    //var divMessage = <HTMLDivElement>document.getElementById("MessageDiv");
    var currentFolderId = <HTMLInputElement>document.getElementById("CurrentFolderId");
    if (DocumentName != null) {
        $.ajax({
            type: "POST",
            url: "/TenantDocuments/DocumentListing",
            data: JSON.stringify({ DocumentName: DocumentName.value, TemplateName: TemplateName.value, FolderName: FolderName.value, TagsUser: TagsUser.value, TagsGlobal: TagsGlobal.value, Content: Content.value, StartDate: StartDate.value, EndDate: EndDate.value }),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (responseData) {
                if (responseData != null) {
                    var documents = JSON.parse(responseData);
                    if (typeof document === "string") {
                        $("#DocumentListGrid").data("kendoGrid").dataSource.data([]);
                    }
                    else {
                        $("#DocumentListGrid").data("kendoGrid").dataSource.data(documents);
                    }
                }
                else
                    $("#DocumentListGrid").data("kendoGrid").dataSource.data([]);
            },
            error: function (responseText) {
                alert(responseText);
            }
        });
    }
    else {
        AffinityDms.Entities.Documents.prototype.populateDocumentGridByDocumentID(currentFolderId.value)
    }
}

function disableFileControl() {

    //disable upload control
    var upload = $("#files").data("kendoUpload");
    upload.disable();
}

function enableFileControl() {
    //enable upload control

    var upload = $("#files").data("kendoUpload");
    upload.enable();
}

function resetFileControl() {
    //========= Start Refresh file upload control by removing css classes====//
    $(".k-upload-files").remove();
    $(".k-upload-status").remove();
    $(".k-upload.k-header").addClass("k-upload-empty");
    $(".k-upload-button").removeClass("k-state-focused");
    //========= End Refresh file upload control by removing css classes======//
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


