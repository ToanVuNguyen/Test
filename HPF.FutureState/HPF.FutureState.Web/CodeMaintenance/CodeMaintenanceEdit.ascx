<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CodeMaintenanceEdit.ascx.cs" Inherits="HPF.FutureState.Web.CodeMaintenance.CodeMaintenanceEdit" %>
 <table cellpadding="0" cellspacing="0" border="1" align="center" bordercolor="#60A5DE">
                    <tr>
                        <td>      
<table>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="2">
            <asp:BulletedList ID="lblErrorMessage" BulletStyle="Square" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Code Set*:</td>
        <td>
            <asp:DropDownList ID="drpCodeSet" runat="server" CssClass="Text" 
                Width="280px">
            </asp:DropDownList>
                    </td>
        <td>
            &nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Code:</td>
        <td>
            <asp:TextBox ID="txtCode" runat="server" MaxLength="15"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Code Description:</td>
        <td>
            <asp:TextBox ID="txtCodeDescription" runat="server" MaxLength="80" 
                Width="278px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Code Comment:</td>
        <td>
            <asp:TextBox ID="txtCodeComment" runat="server" MaxLength="300" Width="432px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Sort Order:</td>
        <td>
            <asp:TextBox ID="txtSortOrder" runat="server" Width="61px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Active Indicator:</td>
        <td>
            <asp:DropDownList ID="drpActiveInd" runat="server" Height="20px" Width="64px">
                <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td height="35" valign="bottom">
            <asp:Button ID="bntSave" runat="server" onclick="bntSave_Click" Text="Save" 
                Width="92px" CssClass="MyButton" />
&nbsp;&nbsp;
            <asp:Button ID="bntCancel" runat="server" onclick="bntCancel_Click" 
                Text="Cancel" Width="92px" CssClass="MyButton" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td height="10">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</td>
</tr>
</table>
<asp:HiddenField ID="txtRefCodeItemId" runat="server" />
