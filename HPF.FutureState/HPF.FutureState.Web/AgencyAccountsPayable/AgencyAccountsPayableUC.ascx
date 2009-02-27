<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAccountsPayableUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AgencyAccountsPayable.AgencyAccountsPayableUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="myscript" runat="server">
</asp:ScriptManager>

<table width="100%">
    <colgroup>
        <col width="5%" />
        <col width="30%" />
        <col width="10%" />
        <col width="20%" />
        <col width="10%" />
        <col width="35%" />
    </colgroup>
    <tr>
        <td colspan="5" class="Header">
            Agency Accounts Payable page
        </td>
        <td rowspan="3" align="center">
            <img alt="" src="Styles/Images/HPFLogo.jpg" style="width: 55px; height: 55px" /><br />
            <asp:LinkButton ID="lblPortal" runat="server">Invoices on Portal</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency:
        </td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" Height="16px" CssClass="Text" Width="200px">
            </asp:DropDownList>
        </td>
        <td class="sidelinks" align="right">
            Period Start:
        </td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Width="150px" MaxLength="100"></asp:TextBox>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td class="sidelinks" align="right">
            Period End:
        </td>
        <td>
            <asp:TextBox ID="txtPeriodEnd" runat="server" Width="150px" MaxLength="100" CssClass="Text"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Button ID="btnRefreshList" runat="server" Text="Refresh List" Width="100px"
                CssClass="MyButton" OnClick="btnRefreshList_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:BulletedList ID="bulMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td colspan="6" class="sidelinks">
            Invoice List:
        </td>
    </tr>
    <tr>
        <td colspan="5">
            
              <cc1:StatefullScrollPanel ID="StatefullScrollPanel1" runat="server" CssClass="ScrollTable"
            Width="840px">

            <asp:UpdatePanel ID="myupdatepanel" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panInvoiceList" runat="server" CssClass="ScrollTable" Width="840px">
                        <asp:GridView ID="grvInvoiceList" runat="server" BorderStyle="None" Width="100%"
                            AutoGenerateColumns="false" OnSelectedIndexChanged="grvInvoiceList_SelectedIndexChanged">
                            <HeaderStyle CssClass="FixedHeader" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Agency" DataField="AgencyName" />
                                <asp:BoundField HeaderText="Payable#" DataField="AgencyPayableId" />
                                <asp:BoundField HeaderText="Payable Dt" DataField="PaymentDate" DataFormatString="{0:d}" />
                                <asp:TemplateField HeaderText="Payable Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayablePeriod" runat="server" Text='<%#Eval("PeriodStartDate","{0:d}")+" - "+Eval("PeriodEndDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:C}" />
                                <asp:BoundField HeaderText="Status" DataField="StatusCode" />
                                <asp:BoundField HeaderText="Comments" DataField="PaymentComment" />
                            </Columns>
                            <EmptyDataTemplate>
                                There is no data match.</EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
        <td valign="top">
            <table style="vertical-align: top;">
                <tr>
                    <td>
                        <asp:Button ID="btnNewPayable" runat="server" Text="New Payable" Width="100px" CssClass="MyButton"
                            OnClick="btnNewPayable_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnViewPayable" runat="server" Text="View/Edit Payable" CssClass="MyButton"
                            Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable" CssClass="MyButton"
                            Width="100px" OnClick="btnCancelPayable_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td colspan="2">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
