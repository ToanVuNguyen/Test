<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgencyAccountsPayable.aspx.cs" Inherits="HPF.FutureState.Web.WebForm4" Title="Agency Accounts Payable" %>
<%@ Register src="AgencyAccountsPayable/AgencyAccountsPayableUC.ascx" tagname="AgencyAccountsPayableUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:AgencyAccountsPayableUC ID="AgencyAccountsPayableUC1" runat="server" />
</asp:Content>
