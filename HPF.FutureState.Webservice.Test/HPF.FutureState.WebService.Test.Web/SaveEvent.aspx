<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="SaveEvent.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SaveEvent" Title="HPF Webservice Test Application - Save Event" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div style="text-align:left"><h1>Save Event</h1></div>
    <table style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #49A3FF" width="100%">
        <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="center" class="sidelinks" colspan="2">
                        Authentication Info</td>
                </tr>
                <tr>
                    <td align="right">
            
            <asp:Label CssClass="sidelinks"  ID="Label120" runat="server" Text="Username" ></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtUsername" runat="server" Text="admin" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label121" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" TextMode="Password" Width="128px"></asp:TextBox>
            
                    </td>
                </tr>
                
            </table>
            <br />
            <br />
        </td>
        </tr>
    </table>
    <div>
    <asp:Button ID="btnSave" runat="server" Text="Save Event" onclick="btnSave_Click"/>
    <br />
    <table width="100%">
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label1" runat="server" Text="FCID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFcID" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label2" runat="server" Text="Program Stage ID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtProgramStageId" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label3" runat="server" Text="Event Type Code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtEventTypeCd" runat="server"></asp:TextBox>
            </td>
            
        </tr>
        <tr>            
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label4" runat="server" Text="EventDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtEventDt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label5" runat="server" Text="Rpc Ind"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtRpcInd" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label6" runat="server" Text="Event Outcome Code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtEventOutcomeCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label7" runat="server" Text="Completed Ind"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCompletedInd" runat="server"></asp:TextBox>
            </td>
            
        </tr>
        <tr>            
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label8" runat="server" Text="Counselor Id Ref"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorIdRef" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label9" runat="server" Text="Program Refusal Dt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtProgramRefusalDt" runat="server"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label111" runat="server" Text="Change last User ID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtWorkingUserID" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td> 
        </tr>
     </table>   
     <br />
     <asp:Button ID="btnSave1" runat="server" Text="Save Event" onclick="btnSave_Click"/>
     <br />
    <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
    <br />
    <asp:GridView ID="grdvMessages" runat="server">
    </asp:GridView>
    </div>
</asp:Content>
