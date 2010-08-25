<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageEvalSection.aspx.cs" Inherits="HPF.FutureState.Web.ManageEvalSection" %>

<%@ Register src="AppManageEvalSection/AppManageEvalSectionUC.ascx" tagname="AppManageEvalSection" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<html >
<body>
    <div>
        <uc1:AppManageEvalSection ID="AppManageEvalSectionPage1" runat="server" />
    </div>
</body>
</html>
</asp:Content>