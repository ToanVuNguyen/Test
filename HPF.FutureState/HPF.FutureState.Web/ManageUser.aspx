<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="HPF.FutureState.Web.ManageUser" %>

<%@ Register src="AppManageUser/AppManageUserUC.ascx" tagname="AppManageUser" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<html >
<body>
    <div>
        <uc1:AppManageUser ID="AppManageUser1" runat="server" />
    </div>
</body>
</html>
</asp:Content>
