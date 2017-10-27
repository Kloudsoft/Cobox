
/// <reference path="../../scripts/typings/kendo-ui/kendo-ui.d.ts" />
module AffinityDms.Entities {
    export class Attachments {
        public Id: number;
        public Name: string;
        public Url: string;
        public Datatype: DiscussionPostAttachmentType;
    }
    export class Discourses {

        public AddAttachmentsToCommentDiv(AttachmentList: Array<Attachments>) {
            var PostAttachmentsDiv = document.getElementById("PostAttachmentsDiv");
            var AttachmentsNode = document.getElementsByName("PostAttachements");
            if (AttachmentList.length > 0) {
                for (var i = 0; i < AttachmentList.length; i++) {
                    var currentAttachment = AttachmentList[i];
                    var IsSelectedItemExist = false;
                    if (AttachmentsNode != null) {
                        for (var j = 0; j < AttachmentsNode.length; j++) {
                            var attachmentDiv = <HTMLDivElement>AttachmentsNode[j];
                            if (((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Document.toString()) || ((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.External.toString()) || ((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Template.toString()) || ((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Form.toString())) {
                                if ((parseInt(attachmentDiv.getAttribute("data-id"))) == currentAttachment.Id && (attachmentDiv.getAttribute("data-type")) == currentAttachment.Datatype.toString()) {
                                    if (attachmentDiv.getAttribute("data-type") == DiscussionPostAttachmentType.External.toString()) {
                                        if (attachmentDiv.getAttribute("data-name") == currentAttachment.Name) {
                                            IsSelectedItemExist = false;
                                        }
                                    }
                                    else {
                                        IsSelectedItemExist = true;
                                    }
                                }
                            }
                            else if (((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Live.toString())) {
                                if ((attachmentDiv.getAttribute("data-url")) == currentAttachment.Url && (attachmentDiv.getAttribute("data-type")) == currentAttachment.Datatype.toString()) {
                                    IsSelectedItemExist = true;
                                }
                            }
                        }
                    }
                    if (!IsSelectedItemExist) {
                        var postAttach = document.createElement("div");
                        if (currentAttachment.Id != null) {
                            postAttach.setAttribute("data-id", currentAttachment.Id.toString());
                        }
                        else {
                            postAttach.setAttribute("data-id", "");
                        }
                        postAttach.setAttribute("data-type", currentAttachment.Datatype.toString());
                        if (currentAttachment.Url != null) {
                            postAttach.setAttribute("data-url", currentAttachment.Url.toString());
                        }
                        else {
                            postAttach.setAttribute("data-url", "");
                        }
                        postAttach.setAttribute("name", "PostAttachements");
                        postAttach.setAttribute("data-name", currentAttachment.Name);

                        postAttach.style.display = "inline-flex";
                        postAttach.style.margin = "10px";
                        var attachmentImage = document.createElement("img");
                        var imgsrc: string = "";

                        /*
                                    var templateImageSrc = "../../Images/FileIcons/Templete.png";
            var formImageSrc = "../../Images/FileIcons/Form.png";
            var documentImageSrc = "../../Images/FileIcons/Documents.png";
            var externalFilesImageSrc = "../../Images/FileIcons/Attachment.png";
            var urlImageSrc = "../../Images/FileIcons/Link.png";
                        */
                        if (currentAttachment.Datatype == DiscussionPostAttachmentType.Document) {
                            imgsrc = "../../Images/FileIcons/Documents.png";
                        }
                        else if (currentAttachment.Datatype == DiscussionPostAttachmentType.Template) {
                            imgsrc = "../../Images/FileIcons/Template.png";
                        }
                        else if (currentAttachment.Datatype == DiscussionPostAttachmentType.Form) {
                            imgsrc = "../../Images/FileIcons/Form.png";
                        }
                        else if (currentAttachment.Datatype == DiscussionPostAttachmentType.External) {
                            imgsrc = "../../Images/FileIcons/Attachment.png";
                        }
                        else if (currentAttachment.Datatype == DiscussionPostAttachmentType.Live) {
                            imgsrc = "../../Images/FileIcons/Link.png";
                        }
                        attachmentImage.title = currentAttachment.Name;
                        attachmentImage.src = imgsrc;
                        attachmentImage.style.width = "48px";
                        attachmentImage.style.height = "60px";
                        attachmentImage.style.padding = "0px";
                        var DeleteAttachment = document.createElement("a");
                        DeleteAttachment.className = "attachment-close";
                        DeleteAttachment.onclick = (event: MouseEvent): any => {
                            AffinityDms.Entities.Discourses.prototype.DeleteCommentAttachment(event);
                        };
                        DeleteAttachment.textContent = "x";
                        postAttach.appendChild(attachmentImage);
                        postAttach.appendChild(DeleteAttachment);
                        PostAttachmentsDiv.appendChild(postAttach);
                    }
                }
            }
            $("#uploadAttachmentsContainer").text("");
            $("#uploadAttachmentsDiv").hide();
            $("#discourseCommentEditor").show();
        }
        public hideDiscourseBtn(event: any) {
            var discourseCommentEditorId = "#discourseCommentEditor";
            var showDiscourseCommentId = "#showDiscourseComment";
            $(discourseCommentEditorId).hide();
            $(showDiscourseCommentId).show();
        }
        public showDiscourseBtn(event: any) {
            var discourseCommentEditorId = "#discourseCommentEditor";
            var showDiscourseCommentId = "#showDiscourseComment";
            $(showDiscourseCommentId).hide();
            $(discourseCommentEditorId).show();
        }
        public CloseHistoryViewerDiv(event) {
            var showDiscourseCommentId = "#showDiscourseComment";
            var HistoryViewerDiv = "#HistoryViewerDiv";
            $(HistoryViewerDiv).hide();
            $(showDiscourseCommentId).show();
            $("#discourseCommentEditor").show();
        }
        public CloseUploadAttachmentsDiv(event) {
            var discourseCommentEditorId = "#discourseCommentEditor";
            var uploadAttachmentsDiv = "#uploadAttachmentsDiv";
            $(uploadAttachmentsDiv).hide();
            $(discourseCommentEditorId).show();
        }
        public AddRemoveChatUsers(event) {
            $("#UserManagementContainer").text("");
            $("#userManagementSuccess").hide();
            $("#userManagementError").hide();
            $("#userManagementSuccess").text("");
            $("#userManagementError").text("");
            $("#InviteUserEmail").val("");

            var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
            var url = "/TenantDiscourse/GetAllUsersForDiscourse?discourseId=" + DiscourseNo_H.value;
            $("#AddRemoveUserDiv").css("display", "inline-table");
            $("#UserManagementContainer").load(url, function () {
                var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
                if (DiscourseNo_H != null) {
                    if (parseInt(DiscourseNo_H.value) <= 0) {
                        var SelectedUsers = <NodeList>document.getElementsByName("CheckUserSelection");
                        var DiscourseNewUsersInvites = document.getElementsByName("DiscourseNewUsersInvite");
                        if (SelectedUsers != null && DiscourseNewUsersInvites != null) {
                            for (var i = 0; i < SelectedUsers.length; i++) {
                                for (var j = 0; j < DiscourseNewUsersInvites.length; j++) {
                                    var SelectedUser = <HTMLInputElement>SelectedUsers[i];
                                    if (SelectedUser.getAttribute("data-id") == DiscourseNewUsersInvites[j].getAttribute("data-id")) {
                                        SelectedUser.checked = true;
                                    }
                                }
                            }
                        }
                    }

                }
            });
        }
        public CloseAddRemoveChatUsersDiv(event) {
            $("#userManagementSuccess").hide();
            $("#userManagementError").hide();
            $("#userManagementSuccess").text("");
            $("#userManagementError").text("");
            $("#UserManagementContainer").text("");
            $("#InviteUserEmail").val("");
            $("#AddRemoveUserDiv").hide();
        }
        public AddRemoveUserBtn(event) {
            /*
            ="addRemoveSubmitBtn" style="display:none">Submit</button>
                                                    <button class="btn btn-primary btn-radius pull-right" id="inviteExternalSubmitBtn"
            */
            $("#userManagementSuccess").hide();
            $("#userManagementError").hide();
            $("#userManagementSuccess").text("");
            $("#userManagementError").text("");
            $("#UserManagementBtns").hide();
            $("#UserManagementHeader").text("Add / Remove Users");
            var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
            $("#UserManagementContainer").text("");
            var url = "/TenantDiscourse/GetAllUsersForDiscourse?discourseId=" + DiscourseNo_H.value;
            $("#UserManagementContainer").load(url, function () {
                var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
                if (DiscourseNo_H != null) {
                    if (parseInt(DiscourseNo_H.value) <= 0) {

                        var SelectedUsers = <NodeList>document.getElementsByName("CheckUserSelection");
                        var DiscourseNewUsersInvites = document.getElementsByName("DiscourseNewUsersInvite");
                        if (SelectedUsers != null && DiscourseNewUsersInvites != null) {
                            for (var i = 0; i < SelectedUsers.length; i++) {
                                for (var j = 0; j < DiscourseNewUsersInvites.length; j++) {
                                    var SelectedUser = <HTMLInputElement>SelectedUsers[i];
                                    if (SelectedUser.getAttribute("data-id") == DiscourseNewUsersInvites[j].getAttribute("data-id")) {
                                        SelectedUser.checked = true;
                                    }
                                    //if (SelectedUser.checked) {
                                    //    var uid = SelectedUser.getAttribute("data-id");
                                    //    var uname = SelectedUser.getAttribute("data-name");
                                    //    if (uid != null) {
                                    //        var id = parseInt(uid);
                                    //        var name = id + ":" + uname;
                                    //        SelectedUsersList.push(id);
                                    //        SelectedUsersName.push(name)
                                    //    }
                                    //}
                                }
                            }
                        }
                    }

                }
            });
            $("#userManagementDiv").show();
            $("#addRemoveSubmitBtn").show();
            $("#inviteExternalSubmitBtn").hide();
        }
        public AddInternalInviteBtn(event) {
            $("#userManagementSuccess").hide();
            $("#userManagementError").hide();
            $("#userManagementSuccess").text("");
            $("#userManagementError").text("");
            $("#UserManagementBtns").hide();
            $("#UserManagementHeader").text("Invite External User");
            var txtBx = '<input type="email" id="InviteUserEmail" class="form-control text-box" placeholder="Enter User Email" style="max-width: none;">';
            $("#UserManagementContainer").html(txtBx);
            $("#userManagementDiv").show();
            $("#addRemoveSubmitBtn").hide();
            $("#inviteExternalSubmitBtn").show();

        }
        public CloseUserManagementDiv(event) {
            $("#userManagementSuccess").hide();
            $("#userManagementError").hide();
            $("#userManagementSuccess").text("");
            $("#userManagementError").text("");
            $("#UserManagementBtns").show();
            $("#UserManagementHeader").text("")
            $("#UserManagementContainer").text("");
            $("#userManagementDiv").hide();
            $("#addRemoveSubmitBtn").hide();
            $("#inviteExternalSubmitBtn").hide();
        }
        public AddDocumentAsAttachmnet(id: number, Name: string) {
            var AttachmentList = new Array<Attachments>();
            var attachments = new Attachments();
            attachments.Id = id;
            attachments.Datatype = DiscussionPostAttachmentType.Document;
            attachments.Url = "";
            attachments.Name = Name;
            AttachmentList.push(attachments);
            AffinityDms.Entities.Discourses.prototype.AddAttachmentsToCommentDiv(AttachmentList);
        }
        public ShowCreatePostModal(event) {
            var DiscourseModalContainer = document.getElementById("DiscourseModalContainer");
            if (DiscourseModalContainer != null) {
                DiscourseModalContainer.textContent = "";

                //var id = event.target.getAttribute("data-id");
                //var url = "../../TenantDiscourse/PartialDiscourse/" + id;
                //$("#DiscourseModalContainer").text("");
                //$("#DiscourseModalContainer").load(url);
                //$("#DiscourseModal").modal({ backdrop: 'static' })
                $("#DiscourseModalLabel").text("Start Discourse");

                $("#DiscourseModalContainer").load("../../TenantDiscourse/CreateDiscourse", function () {
                    var hdocId = <HTMLInputElement>document.getElementById("hdocId");
                    if (hdocId != null) {
                        var docid = parseInt(hdocId.value);
                        var docName = hdocId.getAttribute("data-name");
                        AffinityDms.Entities.Discourses.prototype.AddDocumentAsAttachmnet(docid, docName);
                    }
                });

                $("#DiscourseModal").modal({ backdrop: 'static' })
            }
        }
        public SaveDynamicSelectionModal(): any {
            var DynamicModalDataType_H = <HTMLInputElement>document.getElementById("DynamicModalDataType_H");
            var PostAttachmentsDiv = document.getElementById("PostAttachmentsDiv");
            var AttachmentsNode = document.getElementsByName("PostAttachements");
            var AttachmentList = new Array<Attachments>();
            if (DynamicModalDataType_H != null) {
                if (PostAttachmentsDiv != null) {
                    if (DynamicModalDataType_H.value == "Document") {
                        var CheckDocumentSelection = document.getElementsByName("CheckDocumentSelection");
                        for (var i = 0; i < CheckDocumentSelection.length; i++) {
                            var ChkSelection = <HTMLInputElement>CheckDocumentSelection[i];
                            if (ChkSelection.checked) {
                                var uid = ChkSelection.getAttribute("data-id");
                                var dataname = ChkSelection.getAttribute("data-name");
                                if (uid != null) {
                                    var id = parseInt(uid);
                                    var attachments = new Attachments();
                                    attachments.Id = id;
                                    attachments.Datatype = DiscussionPostAttachmentType.Document;
                                    attachments.Url = "";
                                    attachments.Name = dataname;
                                    AttachmentList.push(attachments);
                                }
                            }
                        }

                    }
                    else if (DynamicModalDataType_H.value == "Template") {
                        var CheckDocumentSelection = document.getElementsByName("CheckTemplateSelection");
                        for (var i = 0; i < CheckDocumentSelection.length; i++) {
                            var ChkSelection = <HTMLInputElement>CheckDocumentSelection[i];
                            if (ChkSelection.checked) {
                                var uid = ChkSelection.getAttribute("data-id");
                                var dataname = ChkSelection.getAttribute("data-name");
                                if (uid != null) {
                                    var id = parseInt(uid);
                                    var attachments = new Attachments();
                                    attachments.Id = id;
                                    attachments.Datatype = DiscussionPostAttachmentType.Template;
                                    attachments.Url = "";
                                    attachments.Name = dataname;
                                    AttachmentList.push(attachments);
                                }
                            }
                        }
                    }
                    else if (DynamicModalDataType_H.value == "Form") {
                        var CheckDocumentSelection = document.getElementsByName("CheckTemplateSelection");
                        for (var i = 0; i < CheckDocumentSelection.length; i++) {
                            var ChkSelection = <HTMLInputElement>CheckDocumentSelection[i];
                            if (ChkSelection.checked) {
                                var uid = ChkSelection.getAttribute("data-id");
                                var dataname = ChkSelection.getAttribute("data-name");
                                if (uid != null) {
                                    var id = parseInt(uid);
                                    var attachments = new Attachments();
                                    attachments.Id = id;
                                    attachments.Datatype = DiscussionPostAttachmentType.Form;
                                    attachments.Url = "";
                                    attachments.Name = dataname;
                                    AttachmentList.push(attachments);
                                }
                            }
                        }

                    }
                    else if (DynamicModalDataType_H.value == "External") {
                        var CheckDocumentSelection = document.getElementsByName("CheckTemplateSelection");
                        for (var i = 0; i < CheckDocumentSelection.length; i++) {
                            var ChkSelection = <HTMLInputElement>CheckDocumentSelection[i];
                            if (ChkSelection.checked) {
                                var uid = ChkSelection.getAttribute("data-id");
                                var dataname = ChkSelection.getAttribute("data-name");

                                if (uid != null) {
                                    var id = parseInt(uid);
                                    var attachments = new Attachments();
                                    attachments.Id = id;
                                    attachments.Datatype = DiscussionPostAttachmentType.External;
                                    attachments.Url = "";
                                    attachments.Name = dataname;
                                    AttachmentList.push(attachments);
                                }
                            }
                        }
                    }
                    else if (DynamicModalDataType_H.value == "Live") {
                        var DynamicModalLiveTextBox = <HTMLInputElement>document.getElementById("DynamicModalLiveTextBox");
                        if (DynamicModalLiveTextBox != null) {
                            if (DynamicModalLiveTextBox.value != null || DynamicModalLiveTextBox.value != "") {
                                var attachments = new Attachments();
                                attachments.Id = 0;
                                attachments.Datatype = DiscussionPostAttachmentType.Live;
                                attachments.Url = DynamicModalLiveTextBox.value;
                                attachments.Name = DynamicModalLiveTextBox.value;
                                AttachmentList.push(attachments);
                            }
                        }
                    }
                }
            }
            AffinityDms.Entities.Discourses.prototype.AddAttachmentsToCommentDiv(AttachmentList);
        }
        public SetExterFilesAsAttachments(fileName: any) {

            if (fileName instanceof String) {
                var AttachmentList = new Array<Attachments>();
                var attachments = new Attachments();
                attachments.Id = -1;
                attachments.Datatype = DiscussionPostAttachmentType.External;
                //attachments.Url = fileName;
                attachments.Name = fileName;
                AttachmentList.push(attachments);
            }
            else {
                for (var i = 0; i < fileName.length; i++) {
                    var AttachmentList = new Array<Attachments>();
                    var attachments = new Attachments();
                    attachments.Id = -1;
                    attachments.Datatype = DiscussionPostAttachmentType.External;
                    //attachments.Url = fileName[i].name;
                    attachments.Name = fileName[i].name;
                    AttachmentList.push(attachments);
                }
            }
            AffinityDms.Entities.Discourses.prototype.AddAttachmentsToCommentDiv(AttachmentList);
        }
        public ViewCommentHistory(event) {
            var inputelemenet = null;
            if (event.currentTarget instanceof HTMLInputElement) {
                inputelemenet = <HTMLInputElement>event.currentTarget;
            }
            else if (event instanceof HTMLInputElement) {
                inputelemenet = <HTMLInputElement>event.currentTarget;
            }
            if (inputelemenet != null) {
                var DynamicModal: any = $("#DynamicModal");
                var DynamicModalDataType_H = <HTMLInputElement>document.getElementById("DynamicModalDataType_H");
                DynamicModalDataType_H.value = "CommentHistory";
                DynamicModal.modal("show");
                var id = inputelemenet.getAttribute("data-id");
                if (id != null) {
                    if (id > 0) {
                        DynamicModal.find(".modal-body").load("/TenantDiscourse/GetPostVersionHistory?id=" + id);
                    }
                }
            }

        }
        //public PopulateAttachmentsToDiv(AttachmentList: Array<Attachments>)
        //{
        //    var DynamicModalDataType_H = <HTMLInputElement>document.getElementById("DynamicModalDataType_H");
        //    var PostAttachmentsDiv = document.getElementById("PostAttachmentsDiv");
        //    var AttachmentsNode = document.getElementsByName("PostAttachements");
        //    if (AttachmentList.length > 0) {
        //        for (var i = 0; i < AttachmentList.length; i++) {
        //            var currentAttachment = AttachmentList[i];
        //            var IsSelectedItemExist = false;
        //            if (AttachmentsNode != null) {
        //                for (var j = 0; j < AttachmentsNode.length; j++) {
        //                    var attachmentDiv = <HTMLDivElement>AttachmentsNode[j];
        //                    if (((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Document.toString()) || ((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Template.toString()) || ((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Form.toString())) {
        //                        if ((parseInt(attachmentDiv.getAttribute("data-id"))) == currentAttachment.Id && (attachmentDiv.getAttribute("data-type")) == currentAttachment.Datatype.toString()) {
        //                            IsSelectedItemExist = true;
        //                        }
        //                    }
        //                    else if (((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.External.toString()) || ((attachmentDiv.getAttribute("data-type")) == DiscussionPostAttachmentType.Live.toString())) {
        //                        if ((attachmentDiv.getAttribute("data-url")) == currentAttachment.Url && (attachmentDiv.getAttribute("data-type")) == currentAttachment.Datatype.toString()) {
        //                            IsSelectedItemExist = true;
        //                        }
        //                    }
        //                }
        //            }
        //            if (!IsSelectedItemExist) {
        //                var postAttach = document.createElement("div");
        //                postAttach.setAttribute("data-id", currentAttachment.Id.toString());
        //                postAttach.setAttribute("data-type", currentAttachment.Datatype.toString());
        //                postAttach.setAttribute("data-url", currentAttachment.Url.toString());
        //                postAttach.setAttribute("name", "PostAttachements");
        //                postAttach.style.display = "inline-flex";
        //                postAttach.style.margin = "10px";
        //                var attachmentImage = document.createElement("img");
        //                var imgsrc: string = "";
        //                if (currentAttachment.Datatype == DiscussionPostAttachmentType.Document) {
        //                    imgsrc = "../../Images/Generic_File_Icon.png";
        //                }
        //                else if (currentAttachment.Datatype == DiscussionPostAttachmentType.Template) {
        //                    imgsrc = "../../Images/Tif_Image_Icon.png";
        //                }
        //                else if (currentAttachment.Datatype == DiscussionPostAttachmentType.Form) {
        //                    imgsrc = "../../Images/Tif_Image_Icon.png";
        //                }
        //                else if (currentAttachment.Datatype == DiscussionPostAttachmentType.External) {
        //                    imgsrc = "../../Images/Live_Document_Icon.png";
        //                }
        //                else if (currentAttachment.Datatype == DiscussionPostAttachmentType.Live) {
        //                    imgsrc = "../../Images/Live_Document_Icon.png";
        //                }
        //                attachmentImage.title = currentAttachment.Name;
        //                attachmentImage.src = imgsrc;
        //                attachmentImage.style.width = "48px";
        //                attachmentImage.style.height = "48px";
        //                attachmentImage.style.padding = "10px";


        //                //<a href="#" class="close-icon"></a>
        //                var DeleteAttachment = document.createElement("a");
        //                DeleteAttachment.className = "close-icon";
        //                DeleteAttachment.onclick = (event: MouseEvent): any => {
        //                    AffinityDms.Entities.Discourses.prototype.DeleteCommentAttachment(event);
        //                };
        //                postAttach.appendChild(attachmentImage);
        //                postAttach.appendChild(DeleteAttachment);
        //                PostAttachmentsDiv.appendChild(postAttach);
        //            }
        //        }
        //    }
        //}
        public SaveUserSelectionModal(event): any {
            var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");

            if (DiscourseNo_H != null) {
                var SelectedUsersList = new Array<number>();
                var SelectedUsersName = new Array<string>();
                var SelectedUsers = <NodeList>document.getElementsByName("CheckUserSelection");
                if (SelectedUsers != null) {
                    for (var i = 0; i < SelectedUsers.length; i++) {
                        var SelectedUser = <HTMLInputElement>SelectedUsers[i];
                        if (SelectedUser.checked) {
                            var uid = SelectedUser.getAttribute("data-id");
                            var uname = SelectedUser.getAttribute("data-name");
                            if (uid != null) {
                                var id = parseInt(uid);
                                var name = id + ":" + uname;
                                SelectedUsersList.push(id);
                                SelectedUsersName.push(name)
                            }
                        }
                    }
                }
                if ((parseInt(DiscourseNo_H.value)) > 0) {
                    if (SelectedUsersList.length <= 0) {
                        var discourseid = parseInt(DiscourseNo_H.value);
                        var SelectedUsersListStringify = JSON.stringify(SelectedUsersList);
                        $.ajax({
                            type: "POST",
                            url: "../../TenantDiscourse/RemoveAllUsers",
                            data: JSON.stringify({ discourseId: discourseid }),
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            success: function (response: any) {
                                try {
                                    if (typeof (response) == "boolean") {
                                        var DiscourseAddedUsersDiv = <HTMLDivElement>document.getElementById("DiscourseAddedUsers");
                                        DiscourseAddedUsersDiv.textContent = "";
                                        //$("#userManagementDiv").hide();
                                        //$("#UserManagementBtns").show();
                                        $("#AddRemoveUserDiv").hide();
                                    }
                                    else if (typeof (response) == "string") {
                                        throw response;
                                    }
                                }
                                catch (e) {
                                    if ($("#userManagementError") != null) {
                                        $("#userManagementError").text(e);
                                        $("#userManagementError").show();
                                        setTimeout(function () {
                                            $("#userManagementError").text("");
                                            $("#userManagementError").hide();
                                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                    }
                                }
                            },
                            error: function (responseText: any) {
                                alert("Oops an Error Occured");
                            }
                        });
                    }
                    else {
                        var discourseid = parseInt(DiscourseNo_H.value);
                        var SelectedUsersListStringify = JSON.stringify(SelectedUsersList);
                        $.ajax({
                            type: "POST",
                            url: "../../TenantDiscourse/AddRemoveUser",
                            data: JSON.stringify({ discourseId: discourseid, selectedUsers: SelectedUsersList }),
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            success: function (response: any) {
                                //try {
                                var users: Array<User> = <Array<User>>JSON.parse(response);
                                if (users.length > 0) {
                                    //Faraz Remove Above Function TODO Update Participents. right now users are grabbed twice.
                                    $("#DiscourseAddedUsers").text("");
                                    var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
                                    var id = parseInt(DiscourseNo_H.value);
                                    $("#DiscourseAddedUsers").load("../../TenantDiscourse/GetDiscourseUsers?discourseId=" + id);
                                    $("#AddRemoveUserDiv").hide();



                                    //for (var i = 0; i < users.length; i++) {
                                    //    var para = document.createElement("p");
                                    //    para.textContent = users[i].NameFull;
                                    //    para.style.color = "black";
                                    //    DiscourseAddedUsersDiv.appendChild(para);
                                    //}
                                    //if ($("#userManagementSuccess") != null) {
                                    //    $("#userManagementSuccess").text("SuccessFully Updated Invites");
                                    //    $("#userManagementSuccess").show();
                                    //    $("#userManagementError").hide();
                                    //}
                                    //$("#userManagementDiv").hide();
                                    //$("#UserManagementBtns").show();
                                }
                                //}
                                //catch (e) {
                                //    if ($("#userManagementError") != null) {
                                //        $("#userManagementError").text(e);
                                //        $("#userManagementError").show();
                                //        $("#userManagementSuccess").hide();
                                //    }
                                //}

                            },
                            error: function (responseText: any) {
                                alert("Oops an Error Occured");
                            }
                        });
                    }
                }
                else {
                    var DiscourseAddedUsersDiv = <HTMLDivElement>document.getElementById("DiscourseAddedUsers");
                    var DiscourseNewUsersInvites = document.getElementsByName("DiscourseNewUsersInvite");
                    if (DiscourseNewUsersInvites != null) {
                        var length = DiscourseNewUsersInvites.length;
                        for (var i = 0; i < length; i++) {
                            DiscourseNewUsersInvites[0].remove();
                        }
                    }
                    if (SelectedUsersName.length > 0) {
                        for (var i = 0; i < SelectedUsersName.length; i++) {
                            var para = document.createElement("p");
                            var SelectedUsersNameSplit = SelectedUsersName[i].split(':');
                            para.setAttribute("name", "DiscourseNewUsersInvite");
                            para.setAttribute("data-id", SelectedUsersNameSplit[0]);
                            para.setAttribute("data-name", SelectedUsersNameSplit[1]);
                            para.textContent = SelectedUsersNameSplit[1];
                            para.style.color = "black";
                            DiscourseAddedUsersDiv.appendChild(para);
                        }
                        if ($("#userManagementSuccess") != null) {
                            $("#userManagementSuccess").text("SuccessFully Updated Invites");
                            $("#userManagementSuccess").show();
                            $("#userManagementError").hide();
                        }
                        $("#userManagementDiv").hide();
                        $("#UserManagementBtns").show();
                    }
                }


            }
        }
        public PostComment(event): any {
            var editbtn;
            if (event instanceof Event) {
                editbtn = <HTMLButtonElement>event.currentTarget;
            }
            else if (event instanceof HTMLInputElement) {
                editbtn = <HTMLButtonElement>event;
            }
            AffinityDms.Entities.Discourses.prototype.ProccessComment(editbtn, false);

        }
        //public ResetCommentBox(): any {
        //    var editor = $("#CommentTextEditor").data("kendoEditor");
        //    var plaincomment = $("#value").val();
        //    var editorcomment = editor.value("");
        //    var PostAttachmentsDiv = <HTMLDivElement>document.getElementById("PostAttachmentsDiv");
        //    PostAttachmentsDiv.textContent = "";

        //    var postCommentBtn = <HTMLInputElement>document.getElementById("postCommentBtn");
        //    postCommentBtn.style.display = "";

        //    var postEditedCommentBtn = <HTMLInputElement>document.getElementById("postEditedCommentBtn");
        //    postEditedCommentBtn.style.display = "none";

        //    var EditCounter_H = <HTMLInputElement>document.getElementById("EditCounter_H");
        //    var EditPost_H = <HTMLInputElement>document.getElementById("EditPost_H");
        //    EditCounter_H.value = "-1";
        //    EditPost_H.value = "-1";
        //    var PostTitleTxt = <HTMLInputElement>document.getElementById("PostTitleTxt");
        //    PostTitleTxt.value = "";
        //    var TitleInputDiv = <HTMLInputElement>document.getElementById("TitleInputDiv");
        //    if (TitleInputDiv != null) {
        //        TitleInputDiv.style.display = "none";
        //    }

        //}





        public ResetCommentBox(event: any): any {
            var editor = $("#CommentTextEditor").data("kendoEditor");
            var plaincomment = $("#value").val();
            var editorcomment = editor.value("");
            var PostAttachmentsDiv = <HTMLDivElement>document.getElementById("PostAttachmentsDiv");
            PostAttachmentsDiv.textContent = "";

            var postCommentBtn = <HTMLInputElement>document.getElementById("postCommentBtn");
            postCommentBtn.style.display = "";

            var postEditedCommentBtn = <HTMLInputElement>document.getElementById("postEditedCommentBtn");
            postEditedCommentBtn.style.display = "none";
            postEditedCommentBtn.setAttribute("data-id", "-1");

            var PostTitleTxt = <HTMLInputElement>document.getElementById("PostTitleTxt");
            if (PostTitleTxt != null) {
                PostTitleTxt.value = "";
            }

            //var EditCounter_H = <HTMLInputElement>document.getElementById("EditCounter_H");
            //var EditPost_H = <HTMLInputElement>document.getElementById("EditPost_H");
            //EditCounter_H.value = "-1";
            //EditPost_H.value = "-1";
            //var PostTitleTxt = <HTMLInputElement>document.getElementById("PostTitleTxt");
            //PostTitleTxt.value = "";
            //var TitleInputDiv = <HTMLInputElement>document.getElementById("TitleInputDiv");
            //if (TitleInputDiv != null) {
            //    TitleInputDiv.style.display = "none";
            //}
            if (event != null) {
                // AffinityDms.Entities.Discourses.prototype.hideDiscourseBtn(null);
            }
        }










        public GetMonth(month: number): string {
            var strMonth = "January";
            if (month >= 0) {
                switch (month) {
                    case 0:
                        strMonth = "January";
                        break;
                    case 1:
                        strMonth = "February";
                        break;
                    case 2:
                        strMonth = "March";
                        break;
                    case 3:
                        strMonth = "April";
                        break;
                    case 4:
                        strMonth = "May";
                        break;
                    case 5:
                        strMonth = "June";
                        break;
                    case 6:
                        strMonth = "July";
                        break;
                    case 7:
                        strMonth = "August";
                        break;
                    case 8:
                        strMonth = "September";
                        break;
                    case 9:
                        strMonth = "October";
                        break;
                    case 10:
                        strMonth = "November";
                        break;
                    case 11:
                        strMonth = "December";
                        break;
                    default:
                        strMonth = "January";
                        break;
                }
            }

            return strMonth;
        }
        public getDocumentList(): any {
            var DynamicModalRowId_H = <HTMLInputElement>document.getElementById("DynamicModalRowId_H");
            if (DynamicModalRowId_H != null) {
                DynamicModalRowId_H.textContent = "";
            }
            $("#DynamicModal").find(".modal-body").load("/TenantDiscourse/GetAllDocumentsForDiscourse");
        }
        public getTemplateList(): any {
            var DynamicModalRowId_H = <HTMLInputElement>document.getElementById("DynamicModalRowId_H");
            if (DynamicModalRowId_H != null) {
                DynamicModalRowId_H.textContent = "";
            }
            $("#DynamicModal").find(".modal-body").load("/TenantDiscourse/GetAllTemplatesForDiscourse?templateType=" + TemplateType.Template);
        }
        public getFormList(): any {
            var DynamicModalRowId_H = <HTMLInputElement>document.getElementById("DynamicModalRowId_H");
            if (DynamicModalRowId_H != null) {
                DynamicModalRowId_H.textContent = "";
            }
            $("#DynamicModal").find(".modal-body").load("/TenantDiscourse/GetAllTemplatesForDiscourse?templateType=" + TemplateType.Form);
        }
        public getExternal(): any {
            //alert("external");
        }
        public getLive(): any {
            var modalBody = $("#DynamicModal").find(".modal-body");
            var inputElement = document.createElement("input");
            inputElement.classList.add("form-control");
            inputElement.classList.add("text-box");
            inputElement.style.maxWidth = "none";
            inputElement.id = "DynamicModalLiveTextBox";
            inputElement.placeholder = "Insert a Live URL";
            modalBody.append(inputElement);
        }
        //public ShowDynamicModal(event: any): any {
        //    if (event.currentTarget instanceof HTMLAnchorElement) {
        //        var CurrentElement = <HTMLAnchorElement>event.currentTarget;
        //        if (CurrentElement != null) {
        //            var datatype = CurrentElement.getAttribute("data-type");
        //            if (datatype != null) {
        //                var DynamicModalDataType_H = <HTMLInputElement>document.getElementById("DynamicModalDataType_H");
        //                DynamicModalDataType_H.value = datatype;
        //                var DynamicModal: any = $("#DynamicModal");
        //                DynamicModal.modal("show");
        //            }
        //        }
        //    }
        //}
        private ValidateExternalFilesContainsFolderId(): string {
            var folderId = parseInt($("#MoveFolderTo").val());
            return "../../TenantDiscourse/UploadExternalDocumentsToFolder?folderId=" + folderId;
        }
        public onSelect(e): any {
            if ($("#MoveFolderTo") != null) {
                var folderId = parseInt($("#MoveFolderTo").val());

                if (folderId <= 1) {
                    if ($("#DiscourseErrorMessage") != null) {
                        $("#DiscourseErrorMessage").css("display", "");
                        $("#DiscourseErrorMessageText").text("Please select a folder");
                        setTimeout(function () {
                            $('#DiscourseErrorMessage').fadeOut('fast');
                            $("#DiscourseErrorMessage").css("display", "none");
                            $("#DiscourseErrorMessageText").text("");
                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                    }
                    $(".k-file:has([title='" + e.files[0].name + "']) .k-cancel").click();
                    e.preventDefault();
                }
                else
                {
                    e.sender.options.async.saveUrl = "../../TenantDiscourse/UploadExternalDocumentsToFolder?folderId=" + folderId;
                }

            }
            else {
                if ($("#DiscourseErrorMessage") != null) {
                    $("#DiscourseErrorMessage").css("display", "");
                    $("#DiscourseErrorMessageText").text("Can not upload the following folder");
                    setTimeout(function () {
                        $('#DiscourseErrorMessage').fadeOut('fast');
                        $("#DiscourseErrorMessage").css("display", "none");
                        $("#DiscourseErrorMessageText").text("");
                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                }
                $(".k-file:has([title='" + e.files[0].name + "']) .k-cancel").click();
                e.preventDefault();
            }
        }
        
                
        public ShowDynamicModal(event: any): any {
            if (event.currentTarget instanceof HTMLAnchorElement) {
                var CurrentElement = <HTMLAnchorElement>event.currentTarget;
                if (CurrentElement != null) {
                    var datatype = CurrentElement.getAttribute("data-type");
                    if (datatype != null) {
                        //var DynamicModalDataType_H = <HTMLInputElement>document.getElementById("DynamicModalDataType_H");
                        //DynamicModalDataType_H.value = datatype;
                        var uploadAttachmentsHeader = document.getElementById("uploadAttachmentsHeader");
                        var uploadAttachmentsContainer = document.getElementById("uploadAttachmentsContainer");
                        var DynamicModalDataType_H = <HTMLInputElement>document.getElementById("DynamicModalDataType_H");
                        if (DynamicModalDataType_H != null) {
                            DynamicModalDataType_H.value = datatype;
                        }
                        if (uploadAttachmentsHeader != null && uploadAttachmentsContainer != null) {
                            switch (datatype) {
                                case "Document":
                                    // DynamicModalRowId_H.value = "TenantDocumentsSelectionListGrid";//ListGridID
                                    uploadAttachmentsHeader.textContent = "Select Documents";
                                    uploadAttachmentsContainer.textContent = "";

                                    $("#uploadAttachmentsDiv").show();
                                    $("#showDiscourseComment").hide();
                                    $("#discourseCommentEditor").hide();
                                    $("#uploadAttachmentsContainer").load("/TenantDiscourse/GetAllDocumentsForDiscourse");
                                    break;
                                case "Template":
                                    //  DynamicModalRowId_H.value = "TenantTemplatesSelectionListGrid";//ListGridID
                                    uploadAttachmentsHeader.textContent = "Select Templates";
                                    // DynamicModalSccessBtn.style.display = "inline-block";
                                    // AffinityDms.Entities.Discourses.prototype.getTemplateList();
                                    uploadAttachmentsContainer.textContent = "";
                                    $("#uploadAttachmentsDiv").show();
                                    $("#showDiscourseComment").hide();
                                    $("#discourseCommentEditor").hide();
                                    $("#uploadAttachmentsContainer").load("/TenantDiscourse/GetAllTemplatesForDiscourse?templateType=" + TemplateType.Template);
                                    break;
                                case "Form":
                                    //  DynamicModalRowId_H.value = "TenantTemplatesSelectionListGrid";//ListGridID
                                    uploadAttachmentsHeader.textContent = "Select Forms";
                                    // DynamicModalSccessBtn.style.display = "inline-block";
                                    uploadAttachmentsContainer.textContent = "";
                                    $("#uploadAttachmentsDiv").show();
                                    $("#showDiscourseComment").hide();
                                    $("#discourseCommentEditor").hide();
                                    $("#uploadAttachmentsContainer").load("/TenantDiscourse/GetAllTemplatesForDiscourse?templateType=" + TemplateType.Form);
                                    break;
                                case "External":
                                    // DynamicModalRowId_H.value = "";//ListGridID
                                    uploadAttachmentsHeader.textContent = "Add External Files";
                                    uploadAttachmentsContainer.textContent = "";
                                    $("#uploadAttachmentsDiv").show();
                                    $("#showDiscourseComment").hide();
                                    $("#discourseCommentEditor").hide();
                                    $("#MoveFolderTo").val("0");
                                    //uploadAttachmentsContainer
                                    var ModalBodyId: any = document.getElementById("ModalBodyId");
                                    ModalBodyId.value = "uploadAttachmentsContainer";
                                    LoadFolderTreeView("uploadAttachmentsContainer", "uploadAttachmentsContainerRow");
                                    
                                    var ExternalFilesUploader = '<br><div id="divFilesUpload"><input name="files" id="files" type="file" /></div><div id="divStatus" style="max-height:300px;"></div>';//'<br><div id="divFilesUpload"><div class="k-widget k-upload k-header k-upload-empty"><div class="k-dropzone"><div class="k-button k-upload-button"><input name="files" id="files" type="file" data-role="upload" multiple="multiple" autocomplete="off"><span>Select files...</span></div><em>drop files here to upload</em></div></div></div><div id="divStatus" style="max-height:300px;"></div>';//
                                    $("#uploadAttachmentsContainer").append(($("<div>").html(ExternalFilesUploader)))
                                     var divStatus = document.getElementById("divStatus");
                                    if (divStatus != null) { divStatus.textContent = ""; }
                                    resetFileControl();
                                    

                                    $("#files").kendoUpload({
                                        async: {
                                            saveUrl: "../../TenantDiscourse/UploadExternalDocumentsToFolder",//this.ValidateExternalFilesContainsFolderId(),
                                            removeUrl: "../../TenantDiscourse/RemoveExternalDocumentsToFolder",
                                            autoUpload: true
                                        },
                                        select: this.onSelect,
                                        success: onExternalFileUploadSuccess
                                    });
                                     //////////////////var ExternalFilesUploader = '<div id="divFilesUpload"><input name="files" id="files" type="file" /></div><div id="divStatus" style="max-height:300px;"></div>';
                                    //////////////////$("#uploadAttachmentsContainer").html(ExternalFilesUploader);
                                    //////////////////var divStatus = document.getElementById("divStatus");
                                    //////////////////if (divStatus != null) { divStatus.textContent = ""; }
                                    //////////////////resetFileControl();
                                    //////////////////$("#files").kendoUpload({
                                    //////////////////    async: {
                                    //////////////////        saveUrl: "../../TenantDiscourse/UploadExternalDocumentsToFolder",
                                    //////////////////        removeUrl: "../../TenantDiscourse/RemoveExternalDocumentsToFolder",
                                    //////////////////        autoUpload: true
                                    //////////////////    },
                                    //////////////////    success: onExternalFileUploadSuccess
                                    //////////////////});




                                    //$(uploadAttachmentsDiv).load("/TenantDiscourse/GetAllTemplatesForDiscourse?templateType=" + TemplateType.Form);
                                    break;
                                case "Live":
                                    uploadAttachmentsContainer.textContent = "";
                                    $("#uploadAttachmentsDiv").show();
                                    $("#showDiscourseComment").hide();
                                    $("#discourseCommentEditor").hide();
                                    //  DynamicModalRowId_H.value = "";//ListGridID
                                    uploadAttachmentsHeader.textContent = "Add Live File";
                                    // DynamicModalSccessBtn.style.display = "inline-block";
                                    var inputElement = document.createElement("input");
                                    inputElement.classList.add("form-control");
                                    inputElement.classList.add("text-box");
                                    inputElement.style.maxWidth = "none";
                                    inputElement.id = "DynamicModalLiveTextBox";
                                    inputElement.placeholder = "Insert a Live URL";
                                    $("#uploadAttachmentsContainer").append(inputElement);
                                    //AffinityDms.Entities.Discourses.prototype.getLive();
                                    break;
                                case "ViewCommentHistory":
                                    //uploadAttachmentsHeader.textContent = "Comments History";
                                    // DynamicModalSccessBtn.style.display = "inline-block";
                                    uploadAttachmentsContainer.textContent = "";
                                    $("#HistoryViewerDiv").show();
                                    $("#showDiscourseComment").hide();
                                    $("#discourseCommentEditor").hide();
                                    var id = CurrentElement.getAttribute("data-id");
                                    $("#HistoryViewerContainer").load("/TenantDiscourse/GetPostVersionHistory?id=" + id);
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                }
            }

        }
        public ShowExternalDocumentModal(event: any): any {
            if (event.currentTarget instanceof HTMLAnchorElement) {
                var CurrentElement = <HTMLAnchorElement>event.currentTarget;
                if (CurrentElement != null) {
                    var datatype = CurrentElement.getAttribute("data-type");
                    if (datatype != null) {
                        var DynamicModalDataType_H = <HTMLInputElement>document.getElementById("DynamicModalDataType_H");
                        DynamicModalDataType_H.value = datatype;
                        var DocumentUploadModal: any = $("#DocumentUploadModal");
                        DocumentUploadModal.modal("show");
                    }
                }
            }

        }

        public CreatePost(event): any {
            var editor = $("#CommentTextEditor").data("kendoEditor");
            var plaincomment = $("#value").val();
            var editorcomment = editor.value(plaincomment);

            var AttachmentsNode = document.getElementsByName("PostAttachements");
            var documentList = new Array<number>();
            var templateList = new Array<number>();
            var externalList = new Array<string>();
            //var existingExternalList = new Array<number>();
            var liveList = new Array<string>();

            if (AttachmentsNode != null) {
                if (AttachmentsNode.length) {
                    for (var i = 0; i < AttachmentsNode.length; i++) {
                        var attachment = <HTMLDivElement>AttachmentsNode[i];
                        var attachmentType = attachment.getAttribute("data-type");
                        var attachmentid = attachment.getAttribute("data-id");
                        var attachmentUrl = attachment.getAttribute("data-url");
                        var attachmentName = attachment.getAttribute("data-name");

                        if (attachmentType != null) {

                            switch (attachmentType) {
                                case DiscussionPostAttachmentType.Document.toString():
                                    var docId = parseInt(attachmentid);
                                    if (docId != null) {
                                        if (docId > 0) {
                                            documentList.push(docId);
                                        }
                                    }
                                    break;
                                case DiscussionPostAttachmentType.Template.toString():
                                    var tempId = parseInt(attachmentid);
                                    if (tempId != null) {
                                        if (tempId > 0) {
                                            templateList.push(tempId);
                                        }
                                    }
                                    break;
                                case DiscussionPostAttachmentType.Form.toString():
                                    var tempId = parseInt(attachmentid);
                                    if (tempId != null) {
                                        if (tempId > 0) {
                                            templateList.push(tempId);
                                        }
                                    }
                                    break;
                                case DiscussionPostAttachmentType.External.toString():
                                    //var id = parseInt(attachmentid.toString());
                                    //if (id > 0) {
                                    //    existingExternalList.push(id);
                                    //}
                                    //else
                                    //{
                                    if (attachmentName != null) {
                                        externalList.push(attachmentName);
                                    }
                                    //}

                                    break;
                                case DiscussionPostAttachmentType.Live.toString():
                                    var liveVal = attachmentUrl;
                                    if (liveVal != null) {
                                        liveList.push(liveVal);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            var PostTitleTxt = <HTMLInputElement>document.getElementById("PostTitleTxt");
            if (PostTitleTxt != null) {
                debugger;
                if (((editorcomment != null) || (documentList.length > 0) || (templateList.length > 0) || (externalList.length > 0) || (liveList.length > 0)) && (PostTitleTxt.value != null || PostTitleTxt.value != "")) {
                    var DiscourseNewUsersInvite = document.getElementsByName("DiscourseNewUsersInvite");
                    var DiscourseNewUsersInviteList = new Array<number>();

                    if (DiscourseNewUsersInvite.length > 0) {
                        for (var i = 0; i < DiscourseNewUsersInvite.length; i++) {
                            var id = parseInt(DiscourseNewUsersInvite[i].getAttribute("data-id"));
                            DiscourseNewUsersInviteList.push(id);
                        }
                    }
                    else {
                        var SelectedUsers = <NodeList>document.getElementsByName("CheckUserSelection");
                        if (SelectedUsers != null) {
                            for (var i = 0; i < SelectedUsers.length; i++) {
                                var SelectedUser = <HTMLInputElement>SelectedUsers[i];
                                if (SelectedUser.checked) {
                                    var uid = SelectedUser.getAttribute("data-id");
                                    if (uid != null) {
                                        var id = parseInt(uid);
                                        DiscourseNewUsersInviteList.push(id);
                                    }
                                }
                            }
                        }
                    }
                    var DiscourseNewEmailInvite = document.getElementsByName("DiscourseNewEmailInvite");
                    var DiscourseNewEmailInviteList = new Array<string>();
                    if (DiscourseNewEmailInvite != null) {
                        for (var i = 0; i < DiscourseNewEmailInvite.length; i++) {
                            var name = DiscourseNewEmailInvite[i].getAttribute("data-name");
                            DiscourseNewEmailInviteList.push(name);
                        }
                    }


                    $.ajax({
                        type: "POST",
                        url: "../../TenantDiscourse/CreatePost",
                        data: JSON.stringify({ title: PostTitleTxt.value.toString(), comment: editorcomment, documentIds: documentList, templateIds: templateList, external: externalList, live: liveList, userIds: DiscourseNewUsersInviteList, emailList: DiscourseNewEmailInviteList }),
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (response: string) {
                            try {
                                if (response.search("Exception:: ") != -1) {
                                    throw response;
                                }
                                else {
                                    var IsDiscourseModal = <HTMLInputElement>document.getElementById("IsDiscourseModal");
                                    var IsDiscourseDocumentCreatePostModal = <HTMLInputElement>document.getElementById("IsDiscourseDocumentCreatePostModal");
                                    if (IsDiscourseDocumentCreatePostModal != null) {
                                        $("#DiscourseModal").modal("hide");
                                        var DiscourseDocumentPostsContainer = document.getElementById("DiscourseDocumentPostsContainer");
                                        if (DiscourseDocumentPostsContainer != null) {
                                            var hdocId = <HTMLInputElement>document.getElementById("hdocId");
                                            if (hdocId != null) {
                                                $("#DiscourseModalContainer").html("");
                                                var url = "/TenantDiscourse/GetMaxPostVersionByDocumentId?id=" + hdocId.value;
                                                $("#DiscourseDocumentPostsContainer").load(url)
                                            }
                                        }
                                    }
                                    else if (IsDiscourseModal != null) {
                                        if (IsDiscourseModal.value == "false") {
                                            window.location.href = "../../TenantDiscourse/Index?id=" + response;
                                        }
                                    }

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
                }

            }
        }
        public DeleteCommentAttachment(event: any): void {
            if (event instanceof HTMLAnchorElement) {
                var anchorElement = <HTMLAnchorElement>event.currentTarget;
                anchorElement.parentElement.remove();
            }
            else if (event.currentTarget instanceof HTMLAnchorElement) {
                var anchorElement = <HTMLAnchorElement>event.currentTarget;
                anchorElement.parentElement.remove();
            }
        }



        //public EditComment(event): any {
        //    AffinityDms.Entities.Discourses.prototype.ResetCommentBox();
        //    var postCommentBtn = <HTMLInputElement>document.getElementById("postCommentBtn");

        //    var editbtn;
        //    if (event instanceof Event) {
        //        editbtn = <HTMLInputElement>event.currentTarget;
        //    }
        //    else if (event instanceof HTMLInputElement) {
        //        editbtn = <HTMLInputElement>event;
        //    }
        //    if (editbtn.getAttribute("data-counter") != null) {
        //        var postcounter: number = parseInt(editbtn.getAttribute("data-counter"));
        //        var EditCounter_H = <HTMLInputElement>document.getElementById("EditCounter_H");
        //        var EditPost_H = <HTMLInputElement>document.getElementById("EditPost_H");
        //        if (EditCounter_H != null && EditPost_H != null) {
        //            EditCounter_H.value = postcounter.toString();
        //            if (editbtn.getAttribute("data-id") != null) {
        //                var postId = parseInt(editbtn.getAttribute("data-id"));
        //                if (postId > 0) {
        //                    EditPost_H.value = postId.toString();
        //                    var PostComment = <HTMLDivElement>document.getElementById("PostComment_" + postcounter);
        //                    var editor = $("#CommentTextEditor").data("kendoEditor");
        //                    if (PostComment != null) {
        //                        if (PostComment.innerHTML != null) {
        //                            var editorcomment = editor.value(PostComment.innerHTML);
        //                        }
        //                    }
        //                    var commentType = editbtn.getAttribute("data-type");
        //                    if (commentType != null) {
        //                        if (postCommentBtn != null) {
        //                            postCommentBtn.setAttribute("data-CommentType", commentType);
        //                        }
        //                        AffinityDms.Entities.Discourses.prototype.SetPostAttachmentToAttachmentsDiv(postcounter, commentType);
        //                    }
        //                    postCommentBtn.style.display = "none";

        //                    var postEditedCommentBtn = <HTMLInputElement>document.getElementById("postEditedCommentBtn");
        //                    postEditedCommentBtn.style.display = "";
        //                }

        //            }
        //        }

        //    }

        //}





        public EditComment(event): any {
            AffinityDms.Entities.Discourses.prototype.ResetCommentBox(null);
            var editbtn;
            if (event instanceof Event) {
                editbtn = <HTMLAnchorElement>event.currentTarget;
            }
            else if (event instanceof HTMLAnchorElement) {
                editbtn = <HTMLAnchorElement>event;
            }
            var postCommentBtn = <HTMLButtonElement>document.getElementById("postCommentBtn");
            if (postCommentBtn != null) {
                postCommentBtn.style.display = "none";
            }
            var postEditedCommentBtn = <HTMLButtonElement>document.getElementById("postEditedCommentBtn");
            if (postEditedCommentBtn != null) {
                var editbtnPostId = editbtn.getAttribute("data-id");
                postEditedCommentBtn.style.display = "";
                postEditedCommentBtn.setAttribute("data-id", editbtnPostId);
                var id = parseInt(editbtnPostId);
                var postId = "PostComment" + id;
                if (id > 0) {
                    var PostComment = <HTMLDivElement>document.getElementById(postId);
                    var editor = $("#CommentTextEditor").data("kendoEditor");
                    if (PostComment != null) {
                        if (PostComment.innerHTML != null) {
                            var editorcomment = editor.value(PostComment.innerHTML);
                        }
                    }
                    var commentType = editbtn.getAttribute("data-type")
                    AffinityDms.Entities.Discourses.prototype.SetPostAttachmentToAttachmentsDiv(id, commentType);
                    //var commentType = editbtn.getAttribute("data-type");
                    //if (commentType != null) {
                    //    if (postCommentBtn != null) {
                    //        postCommentBtn.setAttribute("data-CommentType", commentType);
                    //    }
                    //    AffinityDms.Entities.Discourses.prototype.SetPostAttachmentToAttachmentsDiv(postcounter, commentType);
                    //}
                }
            }

            //if (editbtn.getAttribute("data-counter") != null) {
            //    var postcounter: number = parseInt(editbtn.getAttribute("data-counter"));
            //    var EditCounter_H = <HTMLInputElement>document.getElementById("EditCounter_H");
            //    var EditPost_H = <HTMLInputElement>document.getElementById("EditPost_H");
            //    if (EditCounter_H != null && EditPost_H != null) {
            //        EditCounter_H.value = postcounter.toString();
            //        if (editbtn.getAttribute("data-id") != null) {
            //            var postId = parseInt(editbtn.getAttribute("data-id"));
            //            if (postId > 0) {
            //                EditPost_H.value = postId.toString();
            //                var PostComment = <HTMLDivElement>document.getElementById("PostComment_" + postcounter);
            //                var editor = $("#CommentTextEditor").data("kendoEditor");
            //                if (PostComment != null) {
            //                    if (PostComment.innerHTML != null) {
            //                        var editorcomment = editor.value(PostComment.innerHTML);
            //                    }
            //                }
            //                var commentType = editbtn.getAttribute("data-type");
            //                if (commentType != null) {
            //                    if (postCommentBtn != null) {
            //                        postCommentBtn.setAttribute("data-CommentType", commentType);
            //                    }
            //                    AffinityDms.Entities.Discourses.prototype.SetPostAttachmentToAttachmentsDiv(postcounter, commentType);
            //                }
            //                postCommentBtn.style.display = "none";

            //                var postEditedCommentBtn = <HTMLInputElement>document.getElementById("postEditedCommentBtn");
            //                postEditedCommentBtn.style.display = "";
            //            }

            //        }
            //    }

            //}

        }












        public SendExternalUserInvite(event): any {
            try {
                var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
                if (DiscourseNo_H != null) {
                    var InviteUserEmail = <HTMLInputElement>document.getElementById("InviteUserEmail");
                    if (InviteUserEmail != null) {
                        if (InviteUserEmail.value != "") {
                            if ((parseInt(DiscourseNo_H.value)) > 0) {
                                var DiscourseNoVal_H = DiscourseNo_H.value;
                                $.ajax({
                                    type: "POST",
                                    url: "../../TenantDiscourse/InviteUser",
                                    data: JSON.stringify({ discourseId: DiscourseNoVal_H, email: InviteUserEmail.value }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: 'json',
                                    success: function (response) {
                                        try {
                                            //var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                                            //var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                                            //var InviteUserName = <HTMLInputElement>document.getElementById("InviteUserName");
                                            var InviteUserEmail = <HTMLInputElement>document.getElementById("InviteUserEmail");
                                            if (response.toString() == "true") {

                                                //Faraz Remove Above Function TODO Update Participents. right now users are grabbed twice.
                                                $("#DiscourseAddedUsers").text("");
                                                var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
                                                var id = parseInt(DiscourseNo_H.value);
                                                $("#DiscourseAddedUsers").load("../../TenantDiscourse/GetDiscourseUsers?discourseId=" + id);
                                                $("#AddRemoveUserDiv").hide();


                                                //if ($("#userManagementSuccess") != null) {
                                                //    $("#userManagementSuccess").text("Invitation successfully sent.");
                                                //    $("#userManagementSuccess").show();
                                                //    $("#userManagementError").hide();
                                                //}
                                                //var DiscourseAddedUsersDiv = <HTMLDivElement>document.getElementById("DiscourseAddedUsers");
                                                //if (DiscourseAddedUsersDiv != null) {
                                                //    var para = document.createElement("p");
                                                //    para.textContent = InviteUserEmail.value;
                                                //    para.style.color = "black";
                                                //    DiscourseAddedUsersDiv.appendChild(para);
                                                //    var copyInviteUrl = document.createElement("a");
                                                //    copyInviteUrl.classList.add("InviteUrlTemp");
                                                //    copyInviteUrl.textContent = "Get New Invite Url";
                                                //    copyInviteUrl.onclick = function (e) {
                                                //        GenerateInviteUrl(e);
                                                //    }  
                                                //    copyInviteUrl.setAttribute("data-email", InviteUserEmail.value);
                                                //    DiscourseAddedUsersDiv.appendChild(copyInviteUrl);

                                                //    $("#userManagementDiv").hide();
                                                //    $("#UserManagementBtns").show();
                                                //}
                                                InviteUserEmail.value = "";
                                            }
                                            else if (response.toString() == "false") {
                                                if ($("#userManagementError") != null) {
                                                    $("#userManagementError").text("Unable to send invitation. ");
                                                    $("#userManagementError").show();
                                                    setTimeout(function () {
                                                        $("#userManagementError").text("");
                                                        $("#userManagementError").hide();
                                                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);

                                                }
                                                //var DiscourseAddedUsersDiv = <HTMLDivElement>document.getElementById("DiscourseAddedUsers");
                                                //if (DiscourseAddedUsersDiv != null) {
                                                //    var para = document.createElement("p");
                                                //    para.textContent = InviteUserEmail.value;
                                                //    para.style.color = "black";
                                                //    DiscourseAddedUsersDiv.appendChild(para);
                                                //}
                                            }
                                            else {
                                                throw response;
                                            }
                                        }
                                        catch (e) {
                                            if ($("#userManagementError") != null) {
                                                $("#userManagementError").text(e);
                                                $("#userManagementError").show();
                                                setTimeout(function () {
                                                    $("#userManagementError").text("");
                                                    $("#userManagementError").hide();
                                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);

                                            }
                                        }
                                    },
                                    error: function (responseText: any) {
                                        if ($("#userManagementError") != null) {
                                            $("#userManagementError").text("Oops an Error Occured");
                                            $("#userManagementError").show();
                                            setTimeout(function () {
                                                $("#userManagementError").text("");
                                                $("#userManagementError").hide();
                                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);

                                        }
                                       
                                    }

                                });
                            }
                            else {

                                var InviteUserEmail = <HTMLInputElement>document.getElementById("InviteUserEmail");
                                if ($("#userManagementSuccess") != null) {
                                    $("#userManagementSuccess").text("Invitation will be sent shortly.");
                                    $("#userManagementSuccess").show();
                                    setTimeout(function () {
                                        $("#userManagementSuccess").text("");
                                        $("#userManagementSuccess").hide();
                                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);

                                }
                               
                                var DiscourseAddedUsersDiv = <HTMLDivElement>document.getElementById("DiscourseAddedUsers");
                                if (DiscourseAddedUsersDiv != null) {
                                    var MailCounter_H = <HTMLInputElement>document.getElementById("MailCounter_H");
                                    if (MailCounter_H != null) {
                                        var isUserInvited = false;
                                        var DiscourseNewEmailInvites = document.getElementsByName("DiscourseNewEmailInvite");
                                        for (var i = 0; i < DiscourseNewEmailInvites.length; i++) {
                                            if (DiscourseNewEmailInvites[i].getAttribute("data-name") == InviteUserEmail.value) {
                                                isUserInvited = true;
                                                break;
                                            }
                                        }
                                        if (isUserInvited) {
                                            if ($("#userManagementError") != null) {
                                                $("#userManagementError").text("User is already invited");
                                                $("#userManagementError").show();
                                                setTimeout(function () {
                                                    $("#userManagementError").text("");
                                                    $("#userManagementError").hide();
                                                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                            }
                                        }
                                        else {
                                            var para = document.createElement("p");
                                            para.setAttribute("name", "DiscourseNewEmailInvite");
                                            para.id = "DiscourseNewEmailInvite" + MailCounter_H.value;
                                            para.setAttribute("data-name", InviteUserEmail.value);
                                            para.textContent = InviteUserEmail.value;
                                            para.style.color = "black";
                                            para.onclick = function () {
                                                var paraid = "#" + para.id;
                                                $(paraid).remove();
                                            }
                                            DiscourseAddedUsersDiv.appendChild(para);
                                            $("#userManagementDiv").hide();
                                            $("#UserManagementBtns").show();
                                            MailCounter_H.value = (parseInt(MailCounter_H.value) + 1).toString();
                                        }

                                    }

                                }
                                InviteUserEmail.value = "";
                            }
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
                    throw "Unable to find the Following Dicourse";
                }
            }
            catch (e) {
                if ($("#userManagementError") != null) {
                    $("#userManagementError").text(e);
                    $("#userManagementError").show();
                    setTimeout(function () {
                        $("#userManagementError").text("");
                        $("#userManagementError").hide();
                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                }
            }
        }
        //public SetPostAttachmentToAttachmentsDiv(postcounter: number, commentType: string) {
        //    var PostAttachments = document.getElementById("PostAttachments_" + postcounter);
        //    if (PostAttachments != null) {
        //        if (PostAttachments.childNodes != null) {
        //            if (PostAttachments.childNodes.length > 0) {
        //                var AttachmentList = new Array<Attachments>();

        //                for (var k = 0; k < PostAttachments.childNodes.length; k++) {
        //                    if (PostAttachments.childNodes[k] instanceof HTMLAnchorElement) {
        //                        var anchor = <HTMLAnchorElement>PostAttachments.childNodes[k];
        //                        var dataid = parseInt(anchor.getAttribute("data-id"));
        //                        var datatype = anchor.getAttribute("data-type");
        //                        var dataurl = anchor.getAttribute("data-url");
        //                        var dataname = anchor.getAttribute("data-name");

        //                        if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Document.toString()) {
        //                            var attachments = new Attachments();
        //                            attachments.Id = dataid;
        //                            attachments.Datatype = DiscussionPostAttachmentType.Document;
        //                            attachments.Url = "";
        //                            attachments.Name = dataname;
        //                            AttachmentList.push(attachments);
        //                        }
        //                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Template.toString()) {
        //                            var attachments = new Attachments();
        //                            attachments.Id = dataid;
        //                            attachments.Datatype = DiscussionPostAttachmentType.Template;
        //                            attachments.Url = "";
        //                            attachments.Name = dataname;
        //                            AttachmentList.push(attachments);
        //                        }
        //                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Form.toString()) {
        //                            var attachments = new Attachments();
        //                            attachments.Id = dataid;
        //                            attachments.Datatype = DiscussionPostAttachmentType.Form;
        //                            attachments.Url = "";
        //                            attachments.Name = dataname;
        //                            AttachmentList.push(attachments);
        //                        }
        //                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.External.toString()) {
        //                            var attachments = new Attachments();
        //                            attachments.Id = dataid;
        //                            attachments.Datatype = DiscussionPostAttachmentType.External;
        //                            //attachments.Url = dataurl;
        //                            attachments.Name = dataname;
        //                            AttachmentList.push(attachments);
        //                        }
        //                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Live.toString()) {
        //                            var attachments = new Attachments();
        //                            attachments.Datatype = DiscussionPostAttachmentType.Live;
        //                            attachments.Id = dataid;
        //                            attachments.Url = dataurl;
        //                            attachments.Name = dataname;
        //                            AttachmentList.push(attachments);
        //                        }

        //                    }
        //                }
        //                AffinityDms.Entities.Discourses.prototype.AddAttachmentsToCommentDiv(AttachmentList);
        //            }
        //        }
        //    }
        //}

        public createNode(text) {
            var node = document.createElement('input');
            node.style.width = '1px';
            node.style.height = '1px';
            node.style.position = 'fixed';
            node.style.top = '5px';
            node.setAttribute("value", text);
            return node;
        }
        public copyNode(node): boolean {
            ////////var selection = getSelection();
            ////////selection.removeAllRanges();

            ////////var range = document.createRange();
            ////////range.selectNodeContents(node);
            ////////selection.addRange(range);

            ////////var result = document.execCommand('copy');
            ////////selection.removeAllRanges();

            //// Create an auxiliary hidden input
            //var aux = document.createElement("input");

            //// Get the text from the element passed into the input
            //aux.setAttribute("value", document.getElementById(elementId).innerHTML);

            //// Append the aux input to the body
            //document.body.appendChild(aux);

            // Highlight the content
            node.focus();
            node.select();

            // Execute the copy command
            var result = document.execCommand("copy");

            // Remove the input from the body
            //document.body.removeChild(node);
            return result;
        }


        //public copyText(text) {
        //    setTimeout(function () {
        //        //var node = AffinityDms.Entities.Discourses.prototype.createNode(text);
        //        //document.body.appendChild(node);
        //        var node = <HTMLInputElement>document.getElementById("InviteUrlHolderText");
        //        var result = false;
        //        if (node != null)
        //        {
        //            alert(text);
        //            $("#InviteUrlHolder").show();
        //            node.value = text;
        //            document.querySelector("#InviteUrlHolderText");
        //            $("#InviteUrlHolderText").select();
        //            result = document.execCommand('copy');
        //           //result = AffinityDms.Entities.Discourses.prototype.copyNode(node);
        //           $("#InviteUrlHolder").hide();
        //        }

        //        //if (node != null) {
        //        //    document.body.removeChild(node);
        //        //}
        //        if (result) {
        //            if ($("#DiscourseSuccessMessage") != null) {
        //                $("#DiscourseSuccessMessage").css("display", "");
        //                $("#DiscourseSuccessMessageText").text("Invite url copied to clipboard.");
        //                setTimeout(function () {
        //                    $('#DiscourseSuccessMessage').fadeOut('fast');
        //                    $("#DiscourseSuccessMessage").css("display", "none");
        //                    $("#DiscourseSuccessMessageText").text("");
        //                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
        //            }
        //            else {
        //                $("#DiscourseErrorMessage").css("display", "");
        //                $("#DiscourseErrorMessageText").text("Unable to copy invite url");
        //                setTimeout(function () {
        //                    $('#DiscourseErrorMessage').fadeOut('fast');
        //                    $("#DiscourseErrorMessage").css("display", "none");
        //                    $("#DiscourseErrorMessageText").text("");
        //                }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
        //            }
        //        }
        //    }, 5000);

        //}
        public SetPostAttachmentToAttachmentsDiv(postcounter: number, commentType: string) {
            var PostAttachments = document.getElementById("PostAttachments" + postcounter);
            if (PostAttachments != null) {
                if (PostAttachments.childNodes != null) {
                    if (PostAttachments.childNodes.length > 0) {
                        var AttachmentList = new Array<Attachments>();

                        for (var k = 0; k < PostAttachments.childNodes.length; k++) {
                            if (PostAttachments.childNodes[k] instanceof HTMLSpanElement) {
                                var spanElement = PostAttachments.childNodes[k];
                                for (var l = 0; l < spanElement.childNodes.length; l++) {
                                    if (spanElement.childNodes[l] instanceof HTMLAnchorElement) {
                                        var anchor = <HTMLAnchorElement>spanElement.childNodes[l];
                                        var dataid = parseInt(anchor.getAttribute("data-id"));
                                        var datatype = anchor.getAttribute("data-type");
                                        var dataurl = anchor.getAttribute("data-url");
                                        var dataname = anchor.getAttribute("data-name");

                                        if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Document.toString()) {
                                            var attachments = new Attachments();
                                            attachments.Id = dataid;
                                            attachments.Datatype = DiscussionPostAttachmentType.Document;
                                            attachments.Url = "";
                                            attachments.Name = dataname;
                                            AttachmentList.push(attachments);
                                        }
                                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Template.toString()) {
                                            var attachments = new Attachments();
                                            attachments.Id = dataid;
                                            attachments.Datatype = DiscussionPostAttachmentType.Template;
                                            attachments.Url = "";
                                            attachments.Name = dataname;
                                            AttachmentList.push(attachments);
                                        }
                                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Form.toString()) {
                                            var attachments = new Attachments();
                                            attachments.Id = dataid;
                                            attachments.Datatype = DiscussionPostAttachmentType.Form;
                                            attachments.Url = "";
                                            attachments.Name = dataname;
                                            AttachmentList.push(attachments);
                                        }
                                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.External.toString()) {
                                            var attachments = new Attachments();
                                            attachments.Id = dataid;
                                            attachments.Datatype = DiscussionPostAttachmentType.External;
                                            //attachments.Url = dataurl;
                                            attachments.Name = dataname;
                                            AttachmentList.push(attachments);
                                        }
                                        else if (datatype == AffinityDms.Entities.DiscussionPostAttachmentType.Live.toString()) {
                                            var attachments = new Attachments();
                                            attachments.Datatype = DiscussionPostAttachmentType.Live;
                                            attachments.Id = dataid;
                                            attachments.Url = dataurl;
                                            attachments.Name = dataname;
                                            AttachmentList.push(attachments);
                                        }

                                    }
                                }
                            }
                        }
                        AffinityDms.Entities.Discourses.prototype.AddAttachmentsToCommentDiv(AttachmentList);
                    }
                }
            }
        }








        public PostEditedComment(event): any {
            var target;
            if (event instanceof Event) {
                target = event.currentTarget;
            }
            else if (event instanceof HTMLButtonElement) {
                target = event;
            }
            AffinityDms.Entities.Discourses.prototype.ProccessComment(target, true);

        }

        public ProccessComment(target: HTMLButtonElement, isEdited: boolean) {
            var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
            if (DiscourseNo_H != null) {
                var editor = $("#CommentTextEditor").data("kendoEditor");
                var plaincomment = $("#value").val();
                var editorcomment = editor.value(plaincomment);
                var editorcommentStr = new String(editorcomment);
                var AttachmentsNode = document.getElementsByName("PostAttachements");
                var documentList = new Array<number>();
                var templateList = new Array<number>();
                var externalList = new Array<string>();
                var existingExternalList = new Array<number>();
                var liveList = new Array<string>();
                if (AttachmentsNode != null) {
                    if (AttachmentsNode.length) {
                        for (var i = 0; i < AttachmentsNode.length; i++) {
                            var attachment = <HTMLDivElement>AttachmentsNode[i];
                            var attachmentType = attachment.getAttribute("data-type");
                            var attachmentid = attachment.getAttribute("data-id");
                            var attachmentUrl = attachment.getAttribute("data-url");
                            var attachmentName = attachment.getAttribute("data-name");

                            if (attachmentType != null) {

                                switch (attachmentType) {
                                    case DiscussionPostAttachmentType.Document.toString():
                                        var docId = parseInt(attachmentid);
                                        if (docId != null) {
                                            if (docId > 0) {
                                                documentList.push(docId);
                                            }
                                        }
                                        break;
                                    case DiscussionPostAttachmentType.Template.toString():
                                        var tempId = parseInt(attachmentid);
                                        if (tempId != null) {
                                            if (tempId > 0) {
                                                templateList.push(tempId);
                                            }
                                        }
                                        break;
                                    case DiscussionPostAttachmentType.Form.toString():
                                        var tempId = parseInt(attachmentid);
                                        if (tempId != null) {
                                            if (tempId > 0) {
                                                templateList.push(tempId);
                                            }
                                        }
                                        break;
                                    case DiscussionPostAttachmentType.External.toString():
                                        var id = parseInt(attachmentid.toString());
                                        if (id > 0) {
                                            existingExternalList.push(id);
                                        }
                                        else {
                                            if (attachmentName != null) {
                                                externalList.push(attachmentName);
                                            }
                                        }

                                        break;
                                    case DiscussionPostAttachmentType.Live.toString():
                                        var liveVal = attachmentUrl;
                                        if (liveVal != null) {
                                            liveList.push(liveVal);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                if ((editorcommentStr != "") || (documentList.length > 0) || (templateList.length > 0) || (externalList.length > 0) || (existingExternalList.length > 0) || (liveList.length > 0)) {

                    if (!isEdited) {
                        if (target != null) {
                            var id = parseInt(target.getAttribute("data-id"));
                            $.ajax({
                                type: "POST",
                                url: "../../TenantDiscourse/PostComment",
                                data: JSON.stringify({ discourseId: DiscourseNo_H.value, comment: editorcomment, documentIds: documentList, templateIds: templateList, external: externalList, live: liveList }),
                                contentType: "application/json; charset=utf-8",
                                dataType: 'json',
                                success: function (response: any) {
                                    if (typeof (response) === "string") {
                                        if ($("#DiscourseErrorMessage") != null) {
                                            $("#DiscourseErrorMessage").css("display", "");
                                            $("#DiscourseErrorMessageText").text(response);
                                            setTimeout(function () {
                                                $('#DiscourseErrorMessage').fadeOut('fast');
                                                $("#DiscourseErrorMessage").css("display", "none");
                                                $("#DiscourseErrorMessageText").text("");
                                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                        }
                                    }
                                    else if (typeof (response) === "number") {
                                        var containerId = "PostDivContainer" + response;
                                        var PostDiv = document.createElement("div");
                                        PostDiv.id = containerId;
                                        if (response > 0) {
                                            var temDataHolder = document.getElementById("temDataHolder");
                                            if (temDataHolder != null) {
                                                var MainPostContainer = document.getElementById("MainPostContainer");
                                                MainPostContainer.appendChild(PostDiv);
                                                var divId = "#" + containerId;
                                                $(divId).load("../../TenantDiscourse/GetPost", { id: response });
                                            }
                                            AffinityDms.Entities.Discourses.prototype.ResetCommentBox("Close");

                                        }
                                    }
                                },
                                error: function (response: any) {
                                    if ($("#DiscourseErrorMessage") != null) {
                                        $("#DiscourseErrorMessage").css("display", "");
                                        $("#DiscourseErrorMessageText").text("Oops! An error occurred");
                                        setTimeout(function () {
                                            $('#DiscourseErrorMessage').fadeOut('fast');
                                            $("#DiscourseErrorMessage").css("display", "none");
                                            $("#DiscourseErrorMessageText").text("");
                                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                    }

                                }
                            });
                        }

                    }
                    else {
                        if (target != null) {
                            var id = parseInt(target.getAttribute("data-id"));

                            $.ajax({
                                type: "POST",
                                url: "../../TenantDiscourse/EditPostComment",
                                data: JSON.stringify({ postId: id, comment: editorcomment, documentIds: documentList, templateIds: templateList, external: externalList, existingExternal: existingExternalList, live: liveList }),
                                contentType: "application/json; charset=utf-8",
                                dataType: 'json',
                                success: function (response: any) {
                                    if (typeof (response) === "string") {
                                        if ($("#DiscourseErrorMessage") != null) {
                                            $("#DiscourseErrorMessage").css("display", "");
                                            $("#DiscourseErrorMessageText").text(response);
                                            setTimeout(function () {
                                                $('#DiscourseErrorMessage').fadeOut('fast');
                                                $("#DiscourseErrorMessage").css("display", "none");
                                                $("#DiscourseErrorMessageText").text("");
                                            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                        }
                                    }
                                    else if (typeof (response) === "number") {
                                        var id = parseInt(target.getAttribute("data-id"));
                                        var type = parseInt(target.getAttribute("data-CommentType"));
                                        var containerId = "#PostDivContainer" + id;
                                        if (response > 0) {
                                            $(containerId).load("../../TenantDiscourse/GetPostComment", { id: response });
                                        }
                                        AffinityDms.Entities.Discourses.prototype.ResetCommentBox("Close");
                                    }

                                },
                                error: function (response: any) {
                                    if ($("#DiscourseErrorMessage") != null) {
                                        $("#DiscourseErrorMessage").css("display", "");
                                        $("#DiscourseErrorMessageText").text("Oops! An error occurred");
                                        setTimeout(function () {
                                            $('#DiscourseErrorMessage').fadeOut('fast');
                                            $("#DiscourseErrorMessage").css("display", "none");
                                            $("#DiscourseErrorMessageText").text("");
                                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                                    }
                                }
                            });

                        }

                    }




                }
            }
        }









        public EditTitleComment(event): any {
            var postCommentBtn = <HTMLInputElement>document.getElementById("postCommentBtn");

            AffinityDms.Entities.Discourses.prototype.ResetCommentBox(null);
            var TitleInputDiv = <HTMLDivElement>document.getElementById("TitleInputDiv");
            if (TitleInputDiv != null) {
                var editbtn;
                if (event instanceof Event) {
                    editbtn = <HTMLInputElement>event.currentTarget;
                }
                else if (event instanceof HTMLInputElement) {
                    editbtn = <HTMLInputElement>event;
                }
                if (editbtn.getAttribute("data-counter") != null) {
                    var postcounter: number = parseInt(editbtn.getAttribute("data-counter"));
                    var EditCounter_H = <HTMLInputElement>document.getElementById("EditCounter_H");
                    var EditPost_H = <HTMLInputElement>document.getElementById("EditPost_H");
                    if (EditCounter_H != null && EditPost_H != null) {
                        EditCounter_H.value = postcounter.toString();
                        if (editbtn.getAttribute("data-id") != null) {
                            var postId = parseInt(editbtn.getAttribute("data-id"));
                            if (postId > 0) {
                                EditPost_H.value = postId.toString();
                                TitleInputDiv.style.display = "block";
                                var PostComment = <HTMLDivElement>document.getElementById("PostComment_" + postcounter);
                                var editor = $("#CommentTextEditor").data("kendoEditor");
                                if (PostComment != null) {
                                    if (PostComment.innerHTML != null) {
                                        var editorcomment = editor.value(PostComment.innerHTML);
                                    }
                                }
                                var commentType = editbtn.getAttribute("data-type");
                                if (commentType != null) {
                                    if (postCommentBtn != null) {
                                        postCommentBtn.setAttribute("data-CommentType", commentType);
                                    }
                                    AffinityDms.Entities.Discourses.prototype.SetPostAttachmentToAttachmentsDiv(postcounter, commentType);
                                }
                                postCommentBtn.style.display = "none";

                                var postEditedCommentBtn = <HTMLInputElement>document.getElementById("postEditedCommentBtn");
                                postEditedCommentBtn.style.display = "";
                            }
                        }
                    }
                }

            }
            //TitleInputDiv
        }


        public OnInviteTextChange(e) {
            //$("#InviteUrlHolder").show();
            ////node.value = text;
            //document.querySelector("#InviteUrlHolderText");
            //$("#InviteUrlHolderText").select();
            //var result = document.execCommand('copy');
            ////result = AffinityDms.Entities.Discourses.prototype.copyNode(node);
            //$("#InviteUrlHolder").hide();
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
    function onExternalFileUploadSuccess(e) {
        // Array with information about the uploaded files
        //var files = e.files;
        AffinityDms.Entities.Discourses.prototype.SetExterFilesAsAttachments(e.files);

        //if (e.operation == "upload") {
        //    alert("Successfully uploaded " + files.length + " files");
        //}
    }
}

function GenerateInviteUrl(e): void {
    var emailTarget = e.target;
    var emailtext = emailTarget.getAttribute("data-email")
    var DiscourseNo_H = <HTMLInputElement>document.getElementById("DiscourseNo_H");
    var DiscourseNoVal_H = DiscourseNo_H.value;
    $.ajax({
        type: "POST",
        url: "../../TenantDiscourse/ResendInvite",
        data: JSON.stringify({ discourseId: DiscourseNoVal_H, email: emailtext }),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            if (typeof (response) === "string") {
                if ($("#DiscourseErrorMessage") != null) {
                    $("#DiscourseErrorMessage").css("display", "");
                    $("#DiscourseErrorMessageText").text(response);
                    setTimeout(function () {
                        $('#DiscourseErrorMessage').fadeOut('fast');
                        $("#DiscourseErrorMessage").css("display", "none");
                        $("#DiscourseErrorMessageText").text("");
                    }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                }
            }
            else {
                if (response == true) {
                    if ($("#DiscourseSuccessMessage") != null) {
                        $("#DiscourseSuccessMessage").css("display", "");
                        $("#DiscourseSuccessMessageText").text("Invitation resent successfully");
                        setTimeout(function () {
                            $('#DiscourseSuccessMessage').fadeOut('fast');
                            $("#DiscourseSuccessMessage").css("display", "none");
                            $("#DiscourseSuccessMessageText").text("");
                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                    }
                }
                else if (response == false) {
                    if ($("#DiscourseErrorMessage") != null) {
                        $("#DiscourseErrorMessage").css("display", "");
                        $("#DiscourseErrorMessageText").text("Invitation resend was unsuccessfull");
                        setTimeout(function () {
                            $('#DiscourseErrorMessage').fadeOut('fast');
                            $("#DiscourseErrorMessage").css("display", "none");
                            $("#DiscourseErrorMessageText").text("");
                        }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
                    }
                }
            }
        }
    })
}