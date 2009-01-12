<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAccountsPayableUC.ascx.cs" Inherits="HPF.FutureState.Web.AgencyAccountsPayable.AgencyAccountsPayableUC" %>

<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td colspan="5" class="Header">
            Agency Accounts Payable page</td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency:</td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" Height="16px" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td class="sidelinks" align="right">
            Period Start:</td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Width="150px" MaxLength="100"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td class="sidelinks"  align="right">
            Period End:</td>
        <td>
            <asp:TextBox ID="txtPeriodEnd" runat="server" Width="150px" MaxLength="100"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnRefreshList" runat="server" Text="Refresh List"  Width="100px" CssClass="MyButton"/>
        </td>
    </tr>
    <tr>
        <td colspan="5" class="sidelinks" >
            Invoice List:</td>
    </tr>
    <tr>
        <td colspan="4">
        <asp:Panel ID="panInvoiceList" runat="server"  CssClass="ScrollTable">
        <asp:GridView ID="grvInvoiceList" runat="server"  BorderStyle="None" Width="100%"  AutoGenerateColumns="false">
        <HeaderStyle CssClass="FixedHeader" />
        </asp:GridView>
        </asp:Panel>
        </td>
        <td>
            <table >
                <tr>
                    <td>
                        <asp:Button ID="btnNewPayable" runat="server" Text="New Payable" Width="100px" CssClass="MyButton" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnViewPayable" runat="server" Text="View/Edit Payable" CssClass="MyButton" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable"  CssClass="MyButton" Width="100px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
