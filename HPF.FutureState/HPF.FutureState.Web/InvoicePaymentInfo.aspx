﻿<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoicePaymentInfo.aspx.cs" Inherits="HPF.FutureState.Web.ViewEditInvoicePayment" Title="Invoice Payment Info" %>

<%@ Register src="InvoicePayments/ViewEditInvoicePaymentUC.ascx" tagname="ViewEditInvoicePaymentUC" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <uc1:ViewEditInvoicePaymentUC ID="ViewEditInvoicePaymentUC1" runat="server" />

</asp:Content>