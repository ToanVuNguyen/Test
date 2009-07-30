<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendSummaryToServicer.aspx.cs" Inherits="HPF.FutureState.Web.WebForm5" Title="Send Summaries" %>
<%@ Register src="SendSummaryToServicer/SendSummaryToServicer.ascx" tagname="SendSummaryToServicer" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:SendSummaryToServicer ID="SendSummaryToServicer1" runat="server" />
</asp:Content>
