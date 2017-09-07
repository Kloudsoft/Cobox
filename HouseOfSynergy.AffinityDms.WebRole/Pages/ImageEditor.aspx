<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageEditor.aspx.cs" Inherits="HouseOfSynergy.AffinityDms.WebRole.Pages.ImageEditor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Editor" TagPrefix="tools" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Widgets" TagPrefix="widgets" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Dialogs" TagPrefix="dialogs" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultCS.aspx.cs" Inherits="Telerik.Web.Examples.ImageEditor.Overview.DefaultCS" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>Image Editor</title>
    <link href="../../common/styles.css" rel="stylesheet" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link href="Content/TelerikImageEditorStyle.css" rel="stylesheet" />
    <script src="Scripts/TelerikImageEditor.js" type="text/jscript"></script>
    <script type="text/javascript">
        function redirectToDocumentListing()
        {
            window.location.href = "/TenantDocuments/index";
        }
     
    </script>
</head>
<body>
    <input type="button" id="BackBtn" onclick="redirectToDocumentListing(); return false;" value="Back"/>
    <form id="form1" runat="server" style="margin-top:50px">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <div style="display: inline-block;width:20%;float:left;margin-left:20px">
        </div>
        <div  style="display: inline-block;width:80%;margin-right:auto;margin-left:10%" >
            <div>
                <asp:Label id="lblMessage" runat="server" style="color:red;"></asp:Label>
            </div>
            <input type="hidden" id="hDocumentId" runat="server" value="0" />
        <div class="demo-container size-wide">
            <telerik:RadAjaxPanel runat="server" >
                <telerik:RadImageEditor RenderMode="Lightweight" ID="RadImageEditor1" runat="server" ImageUrl="~/ImageEditor/images/hay.jpg" Skin="BlackMetroTouch"
                    OnImageLoading="RadImageEditor1_ImageLoading" Width="100%" Height="610px" OnImageSaving="RadImageEditor1_ImageSaving" AllowedSavingLocation="Server">
                </telerik:RadImageEditor>
            </telerik:RadAjaxPanel>
        </div>
        </div>
    </form>

</body>
</html>
