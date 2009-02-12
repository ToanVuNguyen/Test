<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FundingSourceInvoice.aspx.cs" Inherits="HPF.FutureState.Web.AppFundingSourceInvoicesPage" Title="Funding Source Invoice" %>
<%@ Register src="AppFundingSourceInvoices/AppFundingSourceInvoices.ascx" tagname="AppFundingSourceInvoices" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:AppFundingSourceInvoices ID="AppFundingSourceInvoices1" runat="server" />
</asp:Content>
