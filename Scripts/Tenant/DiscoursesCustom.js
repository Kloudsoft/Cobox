
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
















//ajeeeb








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
                if (typeof (response) === "string")
                {
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
