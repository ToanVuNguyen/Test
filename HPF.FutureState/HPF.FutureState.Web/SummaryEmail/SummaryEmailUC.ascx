<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SummaryEmailUC.ascx.cs"
    Inherits="HPF.FutureState.Web.SummaryEmail.SummaryEmailUC" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

<table width="100%">
    <tr>
        <td align="center">
            <h1>
                Email Summary</h1>
        </td>
    </tr>
    <tr >
    <td>
    <asp:Label ID="lblMessgage" runat="server" CssClass="ErrorMessage" Text=""></asp:Label>
    <asp:RequiredFieldValidator ID="reqtxtTo" runat="server" Display="Dynamic" ControlToValidate="txtTo" ErrorMessage="Input email address"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="reqtxtSubject" runat="server" Display="Dynamic" ControlToValidate="txtSubject" ErrorMessage="You have to input subject "></asp:RequiredFieldValidator>
    </td>
    </tr>
   
    <tr>
        <td align="right">
            <table width="100%">
                <colgroup>
                    <col width="10%" />
                    <col width="90%" />
                </colgroup>
                <tr>
                    <td class="sidelinks" colspan="2" align="left">
                        Email Information:
                    </td>
                    
                </tr>
                <tr>
                    <td class="sidelinks" align="right">
                        To*:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtTo" runat="server" Width="98%" CssClass="Text" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sidelinks" align="right">
                        Subject*:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSubject" runat="server" Width="98%" CssClass="Text" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sidelinks" align="right" valign="top">
                        Body:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtBody" runat="server" Rows="20" TextMode="MultiLine" MaxLength="2000"
                            Width="98%" CssClass="Text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="MyButton" 
                            Width="100px" onclick="btnSend_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="MyButton" Width="100px" OnClientClick="window.close();" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
