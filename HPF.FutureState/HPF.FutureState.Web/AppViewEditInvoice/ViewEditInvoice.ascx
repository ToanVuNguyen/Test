<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEditInvoice.ascx.cs"
    Inherits="HPF.FutureState.Web.AppViewEditInvoice.ViewEditInvoice" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager runat="server" ID="myscriptManager">
</asp:ScriptManager>
<%--<asp:UpdatePanel runat="server">
    <ContentTemplate>--%>
        <table style="width: 100%;">
            <tr>
                <td colspan="8" align="center">
                    <h1>
                        View/Edit Invoice</h1>
                </td>
            </tr>
            <tr>
                <td class="Text" colspan="8">
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
                    <asp:RegularExpressionValidator ID="valPaymentId" runat="server" ControlToValidate="txtPaymentID"
                        CssClass="ErrorMessage" ErrorMessage="RegularExpressionValidator" ValidationExpression="^[\d ]*$">HPF 
                    Payment ID field is a numberic only entry field.</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Funding Source:
                </td>
                <td class="Text">
                    <asp:Label ID="lblFundingSource" runat="server" CssClass="Text" Text="Citi Group"></asp:Label>
                </td>
                <td align="right" class="sidelinks">
                    Total Cases:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblTotalCases" runat="server" CssClass="Text" Text="6"></asp:Label>
                </td>
                <td align="right" class="sidelinks">
                    Reject Reason:
                </td>
                <td>
                    <asp:DropDownList ID="dropRejectReason" runat="server" CssClass="Text">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <span onclick="return confirm('Are you sure you wish to reject the selected case(s)?')">
                        <asp:Button ID="btnReject" runat="server" CssClass="MyButton" Text="Reject Marked Cases"
                            Width="130px" OnClick="btnReject_Click" />
                    </span>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Period Start:
                </td>
                <td class="Text">
                    <asp:Label ID="lblPeriodStart" runat="server" CssClass="Text" Text="11/01/2008"></asp:Label>
                </td>
                <td align="right" class="sidelinks">
                    Invoice Total:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblInvoiceTotal" runat="server" CssClass="Text" Text="$700.00"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <span onclick="return confirm('Are you sure you wish to pay the selected case(s)?')">
                        <asp:Button ID="btnPay" runat="server" CssClass="MyButton" Text="Pay Marked Cases"
                            Width="130px" onclick="btnPay_Click" />
                    </span>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Period End:
                </td>
                <td class="Text">
                    <asp:Label ID="lblPeriodEnd" runat="server" CssClass="Text" Text="11/30/2008"></asp:Label>
                </td>
                <td align="right" class="sidelinks" colspan="3">
                    &nbsp;
                </td>
                <td align="right" class="sidelinks">
                    HPF Payment ID:
                </td>
                <td>
                    <asp:TextBox ID="txtPaymentID" runat="server" CssClass="Text"></asp:TextBox>
                </td>
                <td align="center">
                    <span onclick="return confirm('Are you sure you wish to unpay the selected case(s)?')">
                        <asp:Button ID="btnUnpay" runat="server" CssClass="MyButton" Text="Unpay Marked Cases"
                            Width="130px" onclick="btnUnpay_Click" />
                    </span>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Invoice Number:
                </td>
                <td class="Text">
                    <asp:Label ID="lblInvoiceNumber" runat="server" CssClass="Text" Text="50032"></asp:Label>
                </td>
                <td align="right" class="sidelinks">
                    Total Paid:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblTotalPaid" runat="server" CssClass="Text" Text="$200.00"></asp:Label>
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
            <tr>
                <td align="right" class="sidelinks">
                    &nbsp;
                </td>
                <td class="Text">
                    &nbsp;
                </td>
                <td align="right" class="sidelinks">
                    Total Rejected:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblTotalRejected" runat="server" CssClass="Text" Text="$0.00"></asp:Label>
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
            <tr>
                <td align="right" class="sidelinks" style="vertical-align: top">
                    Invoice Comments:
                </td>
                <td colspan="7">
                    <asp:TextBox ID="txtInvoiceComments" runat="server" CssClass="Text" Height="80px"
                        TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="sidelinks">
                    Invoice Items:
                </td>
                <td colspan="7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="sidelinks" colspan="8">
                    <cc1:StatefullScrollPanel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable"
                        Width="100%" Visible="true" ScrollBars="Vertical">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvViewEditInvoice" runat="server" CellPadding="2" ForeColor="#333333"
                                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" Width="100%">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1"  ForeColor="#333333" />
                                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkCheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkCheckAllCheck" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ForeclosureCaseId" HeaderText="Case ID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="AgencyCaseNum" HeaderText="Agency Case ID" />
                                        <asp:BoundField DataField="CaseCompleteDate" HeaderText="Complete Dt." />
                                        <asp:BoundField DataField="InvoiceCaseBillAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Amount" />
                                        <asp:BoundField DataField="LoanNumber" HeaderText="Loan Number" />
                                        <asp:BoundField DataField="ServicerName" HeaderText="Servicer" />
                                        <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name" />
                                        <asp:BoundField DataField="PaidDate" HeaderText="Paid Date" />
                                        <asp:BoundField DataField="InvoiceCasePaymentAmount" HeaderText="Paid Amt" DataFormatString="{0:C}"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="PaymentRejectReasonCode" HeaderText="Reject Reason" />
                                        <asp:BoundField DataField="InvenstorLoanId" HeaderText="Investor Loan ID" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        There is no data match !
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </cc1:StatefullScrollPanel>
                    <table width="100%">
                        <tr>
                            <td align="right" class="sidelinks">
                                Total Cases:
                            </td>
                            <td class="Text">
                                <asp:Label ID="lblTotalCase1" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="sidelinks">
                                Invoice Total:
                            </td>
                            <td class="Text">
                                <asp:Label ID="lblInvoiceTotal1" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="sidelinks">
                                Total Paid:
                            </td>
                            <td class="Text">
                                <asp:Label ID="lblTotalPaid1" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        

    <%--</ContentTemplate>
</asp:UpdatePanel>--%>