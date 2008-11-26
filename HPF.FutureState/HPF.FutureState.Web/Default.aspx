<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HPF.FutureState.Web.Default" Title="Untitled Page" %>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <asp:Label ID="Label1" runat="server"></asp:Label>
</asp:Content>
