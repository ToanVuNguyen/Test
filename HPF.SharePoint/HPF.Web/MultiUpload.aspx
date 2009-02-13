<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" MasterPageFile="~/_layouts/application.master" AutoEventWireup="true"
    CodeBehind="MultiUpload.aspx.cs" Inherits="HPF.Web.MultiUpload" Title="Untitled Page" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript">
        function UploadHandler(rdClientId, rdServerId) {
            this.rdClient = document.getElementById(rdClientId);
            this.rdServer = document.getElementById(rdServerId);
            
            addEventListenerEx(this.rdClient, "click", function(e){                
                var divUploadClient = document.getElementById("divUploadFromClient");
                var divUploadServer = document.getElementById("divUploadFromServer");
                
                if(e.target.checked){
                    divUploadClient.style.display = "";
                    divUploadServer.style.display = "none";
                }
            });
            
            addEventListenerEx(this.rdServer, "click", function(e){
                var divUploadClient = document.getElementById("divUploadFromClient");
                var divUploadServer = document.getElementById("divUploadFromServer");
                if(e.target.checked){
                    divUploadClient.style.display = "none";
                    divUploadServer.style.display = "";
                }
            });
        }
    </script>
    <div style="font-weight:bold;">
        Upload Document: 
        <asp:RadioButton ID="RadioButtonFromClient" runat="server" GroupName="Upload" Text="From client" Checked="true" />
        <asp:RadioButton ID="RadioButtonFromServer" runat="server" GroupName="Upload" Text="From server" />
    </div>
    
    <div id="divUploadFromClient">
        <div style="font-weight:bold;">
            Browse to the document you intend to upload.<br />
        </div>
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
    <div id="divUploadFromServer" style="display:none;">
        <div style="font-weight:bold;">
            Enter the server location you intent to upload<br />
        </div>
        <asp:TextBox ID="TextBoxServerLocation" runat="server" Width="200px"></asp:TextBox>
    </div>
    <div style="text-align: right;">
        <asp:Button ID="ButtonUpload" runat="server" Text="OK" Width="7.5em" />
        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Width="7.5em" />
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" Text="Upload document(s)"
        EncodeMethod='HtmlEncode' />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" Text="Upload document(s)"
        EncodeMethod='HtmlEncode' />:
    <SharePoint:ListProperty ID="ListProperty1" Property="LinkTitle" runat="server" />
</asp:Content>
