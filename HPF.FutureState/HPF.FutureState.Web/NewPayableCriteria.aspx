<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewPayableCriteria.aspx.cs" Inherits="HPF.FutureState.Web.WebForm2" Title="Untitled Page" %>
<%@ Register src="AppNewPayable/NewPayableCriteriaUC.ascx" tagname="NewPayableCriteriaUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
    <uc1:NewPayableCriteriaUC ID="NewPayableCriteria1" runat="server" />
    
</asp:Content>
