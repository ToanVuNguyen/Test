<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateNewInvoice.aspx.cs" Inherits="HPF.FutureState.Web.AppNewInvoicePage" Title="New Invoice Criteria"  %>
<%@ Register src="AppNewInvoice/NewInvoiceCriteria.ascx" tagname="AppNewInvoice" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:AppNewInvoice ID="AppNewInvoice1" runat="server" />
</asp:Content>
