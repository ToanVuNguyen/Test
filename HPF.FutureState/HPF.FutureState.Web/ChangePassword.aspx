<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HPF.FutureState.Web.ChagePassword" Title="Change Password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div align="center" style="width: 100%">
        
        <table style="width:32%; background-color: #60A5DE; height: 105px;">
            <tr>
                <td align="center" colspan="2" class="sidelinks2">
                    Change Your Password</td>
            </tr>
            <tr>
                <td align="right" class="sidelinks2">
                    Old Password:</td>
                <td>
                    <input id="txt_oldpassword" type="password" runat="server" /></td>
            </tr>
            <tr>
                <td align="right" class="sidelinks2">
                    New Password:</td>
                <td>
                   
                    <input id="txt_newpassword" type="password" runat="server" /></td>
            </tr>
            <tr>
                <td align="right" class="sidelinks2" style="height: 24px">
                    Confirm New Password:</td>
                <td style="height: 24px">
                   
                    <input id="txt_confirmnewpassword" type="password" runat="server" /></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    
                    <asp:Button ID="btn_chagepassword" runat="server" Text="Change Password" 
                        BackColor="White" CssClass="sidelinks" onclick="btn_chagepassword_Click" />
                        &nbsp
                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" BackColor="White" 
                        CssClass="sidelinks" />
                </td>
            </tr>
        </table> 
        &nbsp
        <asp:Label ID="lbl_status" runat="server" CssClass="ErrorMessage"></asp:Label>
    </div>
</asp:Content>
