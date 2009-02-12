<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceInfo.aspx.cs" Inherits="HPF.FutureState.Web.AppViewEditInvoicePage" Title="View/Edit Invoice" %>
<%@ Register src="AppViewEditInvoice/ViewEditInvoice.ascx" tagname="ViewEditInvoice" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:ViewEditInvoice ID="ViewEditInvoice1" runat="server" />
</asp:Content>
