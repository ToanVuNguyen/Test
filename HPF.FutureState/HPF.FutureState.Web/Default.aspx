<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HPF.FutureState.Web.Default" Title="Home" %>

<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="hpf" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div>
    <div>

        <hpf:UserControlLoader ID="UserControlLoader1" runat="server">
        </hpf:UserControlLoader>
    </div>
    <hpf:TabControl ID="TabControl1" runat="server" />
    <br />    
    <br />


    </div>    
</asp:Content>
