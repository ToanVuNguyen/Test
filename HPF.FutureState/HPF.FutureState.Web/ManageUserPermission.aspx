<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUserPermission.aspx.cs" Inherits="HPF.FutureState.Web.ManageUserPermission" %>

<%@Register Src="~/AppManageUserPermission/AppManageUserPermissionUC.ascx" TagName="AppManageUserPermission" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<html >
<body>
    <div>
        <uc1:AppManageUserPermission ID="AppManageUserPermission1" runat="server" />
    </div>
</body>
</html>
</asp:Content>
