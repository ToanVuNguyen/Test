<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoicePayment.aspx.cs"  EnableEventValidation="false" Inherits="HPF.FutureState.Web.InvoicePayment" %>

<%@ Register src="InvoicePayments/InvoicePaymentsUC.ascx" tagname="InvoicePaymentsUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
        <uc1:InvoicePaymentsUC ID="InvoicePaymentsUC1" runat="server" />
</asp:Content>