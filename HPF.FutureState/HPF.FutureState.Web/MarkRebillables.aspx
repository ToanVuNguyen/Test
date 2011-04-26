<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarkRebillables.aspx.cs" Inherits="HPF.FutureState.Web.WebForm9" Title="Mark Rebillable Invoice Cases" %>
<%@ Register src="MarkRebillables/MarkRebillables.ascx" tagname="MarkRebillables" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:MarkRebillables ID="MarkRebillables1" runat="server" />
</asp:Content>