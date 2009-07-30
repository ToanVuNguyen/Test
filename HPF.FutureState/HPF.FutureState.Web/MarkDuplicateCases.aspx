<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarkDuplicateCases.aspx.cs" Inherits="HPF.FutureState.Web.WebForm6" Title="Mark Duplicates" %>
<%@ Register src="MarkDuplicateCases/MarkDuplicateCases.ascx" tagname="MarkDuplicateCases" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc1:MarkDuplicateCases ID="MarkDuplicateCases1" runat="server" />
</asp:Content>
