<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewPayableSelection.aspx.cs" Inherits="HPF.FutureState.Web.WebForm3" Title="New Payable Selection" %>
<%@ Register src="AppNewPayable/NewAgencyPayableResultsUC.ascx" tagname="NewAgencyPayableResultsUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   <uc1:NewAgencyPayableResultsUC ID="NewAgencyPayableResults" runat="server" />
</asp:Content>
