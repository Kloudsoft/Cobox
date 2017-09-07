


        function templateTypeChanged() {
            var dropdown = document.getElementById("TemplateType");
            if (dropdown.options[dropdown.selectedIndex].value == 1) {
                var DivImageobj = document.getElementById("DivImage");
                DivImageobj.style.display = "";
            }
            else {
                var DivImageobj = document.getElementById("DivImage");
                DivImageobj.style.display = "none";
            }
        }


       
            //document.onload = designer.Run();
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
                    $(this).find(".modal-body").textContext = "";
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
            if (event.currentTarget != null)
            {
                if (event.currentTarget instanceof HTMLAnchorElement) {
                    anchor = event.currentTarget;
                }
            }
            if (anchor != null)
            {
                if (anchor.getAttribute("data-id")!=null)
                    var SelectedTemplate = document.getElementById("SelectedTemplate");
                SelectedTemplate.value = anchor.getAttribute("data-id");
                SelectedTemplate.setAttribute("data-type","Template")
                $("#UserSelectionModal").modal("show");
            }
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
                    var SelectedTemplate = document.getElementById("SelectedTemplate");
                SelectedTemplate.value = anchor.getAttribute("data-id");
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
        $("#UserSelectionModal").on("show.bs.modal", function (e) {
            var SelectedTemplate = document.getElementById("SelectedTemplate");
            if (SelectedTemplate != null)
            {
                var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
                TenantUsersSelectionListGrid.textContent = "";
                $(this).find(".modal-body").load("/TenantTemplates/GetAllUsersForTemplate?templateId=" + SelectedTemplate.value);
            }
        });


     
