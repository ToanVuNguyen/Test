<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewInvoiceResultPage.aspx.cs" Inherits="HPF.FutureState.Web.NewInvoiceResultPage" Title="New Invoice Result Page" %>
<%@ Register src="AppNewInvoice/NewInvoiceResults.ascx" tagname="NewInvoiceResults" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:NewInvoiceResults ID="NewInvoiceResults1" runat="server" />
</asp:Content>
