<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageEvalTemplate.aspx.cs" Inherits="HPF.FutureState.Web.ManageEvalTemplate" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="HPF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:ScriptManager ID="myScript" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="myupdatepan" runat="server">
<ContentTemplate>     
<table width="100%">
    <tr>
        <td colspan="2" align="center" >
            <h1>
                Manage Evaluation Template
            </h1>
        </td>
    </tr>
    <tr>
        <td colspan="2" align = "left">
        <asp:BulletedList ID="lblInstruction" CssClass="sidelinks" runat="server">
        <asp:ListItem>To update existing template information, please use section drop down 
            list for retrieval, make the change, then press Update button.</asp:ListItem>
        <asp:ListItem>To add new template , simply type the information and press Add New 
            button.</asp:ListItem>
        </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Template:</td>        
        <td  align="left">
            <asp:DropDownList ID="ddlTemplate" runat="server" Height="16px" 
                CssClass="Text" Width="200px" AutoPostBack="true" 
                onselectedindexchanged="ddlTemplate_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td colspan="2">
        <asp:Label ID="lblErrorMessage" CssClass="ErrorMessage" runat="server">
        </asp:Label>
    </td>
    </tr>
    <tr>
            <td colspan="2">
                <table cellpadding="0" cellspacing="0" width="100%" align="left">
                    <tr>
                        <td>
                            <HPF:TabControl ID="tabControl" runat="server">
                            </HPF:TabControl>
                        </td>
                    </tr>
                    <tr >
                        <td style="border: solid 1px #8FC4F6">
                            <HPF:UserControlLoader ID="UserControlLoader1" runat="server">
                            </HPF:UserControlLoader>
                        </td>
                    </tr>
                </table>
            </td>
     </tr>
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
 </asp:Content>