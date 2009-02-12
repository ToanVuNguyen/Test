<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" MasterPageFile="~/_layouts/application.master" AutoEventWireup="true"
    CodeBehind="MultiUpload.aspx.cs" Inherits="HPF.Web.MultiUpload" Title="Untitled Page" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <h2>
        Upload document(s)
    </h2>
    <div>
        File 1:
        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        File 2:
        <asp:FileUpload ID="FileUpload2" runat="server" /><br />
        File 2:
        <asp:FileUpload ID="FileUpload3" runat="server" /><br />
        File 2:
        <asp:FileUpload ID="FileUpload4" runat="server" /><br />
        File 2:
        <asp:FileUpload ID="FileUpload5" runat="server" /><br />
    </div>
    <div style="text-align:right;">
        <asp:Button ID="ButtonUpload" runat="server" Text="OK" Width="7.5em" />
        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Width="7.5em" />
    </div>
</asp:Content>
