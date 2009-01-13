<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAccountsPayableUC.ascx.cs" Inherits="HPF.FutureState.Web.AgencyAccountsPayable.AgencyAccountsPayableUC" %>

<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        height: 24px;
    }
    .style2
    {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        color: #2271A0;
        font-size: 11px;
        font-weight: bold;
        padding-left: 10px;
        height: 24px;
    }
</style>
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
        <td class="style1">
            </td>
        <td class="style1">
            </td>
        <td class="style2"  align="right">
            Period End:</td>
        <td class="style1">
            <asp:TextBox ID="txtPeriodEnd" runat="server" Width="150px" MaxLength="100"></asp:TextBox>
        </td>
        <td class="style1">
            <asp:Button ID="btnRefreshList" runat="server" Text="Refresh List"  Width="100px" CssClass="MyButton"/>
        </td>
    </tr>
    <tr>
    <td colspan="5">
    <asp:Label ID="lblMessage" runat="server" Text=""  CssClass="ErrorMessage" ></asp:Label>
    <asp:RequiredFieldValidator ID="reqtxtPeriodStart"  Display="Dynamic" runat="server" ErrorMessage="Must input date" ControlToValidate="txtPeriodStart"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="reqtxtPeriodEnd" Display="Dynamic"  runat="server" ErrorMessage="Must input date" ControlToValidate="txtPeriodEnd"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cmptxtPeriodStart" runat="server" Display="Dynamic" ErrorMessage="Input correct date format" ControlToValidate="txtPeriodStart" ValueToCompare="1/1/1900" Operator="GreaterThan"></asp:CompareValidator>
    <asp:CompareValidator ID="cmptxtPeriodEnd" Display="Dynamic" runat="server" ErrorMessage="Input correct date format" ControlToValidate="txtPeriodEnd" ValueToCompare="1/1/1900" Operator="GreaterThan"></asp:CompareValidator>
    </td></tr><tr>
        <td colspan="5" class="sidelinks" >
            Invoice List:</td></tr><tr>
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
            &nbsp;</td><td>
            &nbsp;</td><td>
            &nbsp;</td><td>
            &nbsp;</td><td>
            &nbsp;</td></tr></table>