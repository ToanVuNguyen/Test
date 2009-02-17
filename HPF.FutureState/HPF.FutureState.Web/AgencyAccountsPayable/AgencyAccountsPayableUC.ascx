<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAccountsPayableUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AgencyAccountsPayable.AgencyAccountsPayableUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="myscript" runat="server">
</asp:ScriptManager>
<table width="90%">
    <colgroup>
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <col width="20%" />
        <col width="50%" />
    </colgroup>
    <tr>
        <td colspan="5" class="Header">
            Agency Accounts Payable page
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency:
        </td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" Height="16px" CssClass="Text">
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
        <td>
            <asp:Button ID="btnRefreshList" runat="server" Text="Refresh List" Width="120px"
                CssClass="MyButton" OnClick="btnRefreshList_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="ErrorMessage"></asp:Label>
            <asp:RequiredFieldValidator ID="reqtxtPeriodStart" Display="Dynamic" runat="server"
                ErrorMessage="Must input date" ControlToValidate="txtPeriodStart"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="reqtxtPeriodEnd" Display="Dynamic" runat="server"
                ErrorMessage="Must input date" ControlToValidate="txtPeriodEnd"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cmptxtPeriodStart" runat="server" Display="Dynamic" ErrorMessage="An invalid date was provided for <Period Start>"
                ControlToValidate="txtPeriodStart" ValueToCompare="1/1/1900" Operator="GreaterThan"
                Type="Date"></asp:CompareValidator>
            <asp:CompareValidator ID="cmptxtPeriodEnd" Display="Dynamic" runat="server" ErrorMessage="An invalid date was provided for <Period End>"
                ControlToValidate="txtPeriodEnd" ValueToCompare="1/1/1900" Operator="GreaterThan"
                Type="Date"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="5" class="sidelinks">
            Payable List:
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <cc1:StatefullScrollPanel ID="panInvoiceList" runat="server" CssClass="ScrollTable"
                Width="840px">
                <asp:UpdatePanel ID="myupdatepanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvInvoiceList" runat="server" BorderStyle="None" Width="100%"
                            AutoGenerateColumns="false" OnSelectedIndexChanged="grvInvoiceList_SelectedIndexChanged">
                            <RowStyle CssClass="RowStyle"  />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1"   ForeColor="#333333" />
                        <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle"  />
                            <Columns>
                                <asp:BoundField HeaderText="Agency" DataField="AgencyName" />
                                <asp:BoundField HeaderText="Payable#" DataField="AgencyPayableId" />
                                <asp:BoundField HeaderText="Payable Dt" DataField="PaymentDate" DataFormatString="{0:d}" />
                                <asp:TemplateField HeaderText="Payable Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayablePeriod" runat="server" Text='<%#Eval("PeriodStartDate","{0:d}")+" - "+Eval("PeriodEndDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:C}"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Status" DataField="StatusCode" />
                                <asp:BoundField HeaderText="Comments" DataField="PaymentComment" />
                                <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton"
                                    ItemStyle-HorizontalAlign="Center" HeaderText="Select" />
                            </Columns>
                            <EmptyDataTemplate>
                                There is no data match.</EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
        <td valign="top">
            <table style="vertical-align: top;">
                <tr>
                    <td>
                        <asp:Button ID="btnNewPayable" runat="server" Text="New Payable" Width="120px" CssClass="MyButton"
                            OnClick="btnNewPayable_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnViewPayable" runat="server" Text="View/Edit Payable" CssClass="MyButton"
                            Width="120px" OnClick="btnViewPayable_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable" CssClass="MyButton"
                            Width="120px" OnClick="btnCancelPayable_Click" />
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
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
