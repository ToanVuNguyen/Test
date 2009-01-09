
<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RetrieveCallLog.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.RetrieveCallLog" Title="HPF Webservice Test Application - Retrieve CallLog" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<div style="text-align:left"><h1>Retrieve CallLog</h1></div>        
<div style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #49A3FF">
    <table align="center">
        <tr>
            <td align="center" class="sidelinks" colspan="2">
                Authentication Info</td>
        </tr>
        <tr>
            <td align="right">
    
    <asp:Label CssClass="sidelinks"  ID="Label28" runat="server" Text="Username" ></asp:Label>
            </td>
            <td>
    <asp:TextBox CssClass="Text" ID="txtUsername" runat="server" Text="admin" Width="128px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
    <asp:Label CssClass="sidelinks" ID="Label29" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
    <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" Text="admin" Width="128px"></asp:TextBox>
    
            </td>
        </tr>
    </table>
    <br />
    <br />
    
    </div>
    <br />
    <div>
    <table>
    
    <tr>
    <td class="sidelinks">
    Call Log ID: 
    </td>
    <td>
    <asp:TextBox ID="txt_CallLogId" runat="server" CssClass="Text"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td></td>
    <td><asp:Button ID="btn_Submit" runat="server" Text="Submit" 
            onclick="btn_Submit_Click" CssClass="MyButton" /></td>
    </tr>
    <tr>
        <td class="Text">Status: </td>
        <td class="Text"><asp:Label ID="lbl_Status" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td class="Text">Message: </td>
        <td class="Text"><asp:Label ID="lbl_Message" runat="server" Text=""></asp:Label></td>
    </tr>
    </table>
        <asp:GridView ID="gv_results" runat="server">
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server">
        </asp:ObjectDataSource>
    </div>
</asp:Content>