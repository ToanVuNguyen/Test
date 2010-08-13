<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchQCSelectionCase.aspx.cs" Inherits="HPF.FutureState.Web.SearchQCSelectionCase" %>
<%@ Register src="AppQCSelectionCaseSearch/AppQCSelectionCaseSearchUC.ascx" tagname="AppQCSelectionCaseSearch" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<html >
<body>
    <div>
        <uc1:AppQCSelectionCaseSearch ID="AppQCSelectionCaseSearchPage1" runat="server" />
    </div>
</body>
</html>
</asp:Content>
