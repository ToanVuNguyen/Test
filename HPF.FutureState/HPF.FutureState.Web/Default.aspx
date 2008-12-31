<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HPF.FutureState.Web.Default" Title="Untitled Page" %>

<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="hpf" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>

        <hpf:UserControlLoader ID="UserControlLoader1" runat="server">
        </hpf:UserControlLoader>
    </div>
    <hpf:TabControl ID="TabControl1" runat="server" />
    <br />    
    <asp:Label ID="txt_Tab" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Button" 
        onclick="Button1_Click1" />


    </div>    
</asp:Content>
