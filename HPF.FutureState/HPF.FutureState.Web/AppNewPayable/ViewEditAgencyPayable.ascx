﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEditAgencyPayable.ascx.cs" Inherits="HPF.FutureState.Web.AppNewPayable.ViewEditAgencyPayable" %>
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
                        View/Edit Agency Payable</h1>
                </td>
            </tr>
            <tr>
                <td class="Text" colspan="8">
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Agency:
                </td>
                <td class="Text">
                    <asp:Label ID="lblAgency" runat="server" CssClass="Text" Text="Citi Group"></asp:Label>
                </td>
                <td align="right" class="sidelinks">
                    Total Cases:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblTotalCases" runat="server" CssClass="Text" Text="6"></asp:Label>
                </td>
                <td align="right" class="sidelinks">
                    Takeback Reason:
                </td>
                <td>
                    <asp:DropDownList ID="ddlTakebackReason" runat="server" CssClass="Text">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <span onclick="return confirm('Are you sure you wish to reject the selected case(s)?')">
                        <asp:Button ID="btnTakeBackMarkCase" runat="server" CssClass="MyButton" Text="Takeback Marked Cases"
                            Width="130px" OnClick="btnTakeBackMarkCase_Click" />
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
                    Total Payable:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblTotalPayable" runat="server" CssClass="Text" Text="$700.00"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <span onclick="return confirm('Are you sure you wish to pay the selected case(s)?')">
                        <asp:Button ID="btnPayUnpayMarkCase" runat="server" CssClass="MyButton" Text="Pay/Unpay Marked Cases"
                            Width="130px" onclick="btnPayUnpayMarkCase_Click" />
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
                <td align="right" class="sidelinks" colspan="2">
                    Total NFMC Up Charge Paid:
                </td>
                <td class="Text">
                    <asp:Label ID="lblTotalChargePaid" runat="server" Text="$15.00" CssClass="Text"  ></asp:Label></td>
                <td align="right" class="sidelinks">
                    HPF Payment ID:
                </td>
                <td class="Text">
                    <asp:TextBox ID="txtPaymentID" runat="server" ></asp:TextBox>
                </td>
                <td align="center">
                    <span onclick="return confirm('Are you sure you wish to unpay the selected case(s)?')">
                        <asp:Button ID="btnReprintPayable" runat="server" CssClass="MyButton" Text="Reprint Payable"
                            Width="130px" />
                    </span>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                   Payable Number:
                </td>
                <td class="Text">
                    <asp:Label ID="lblPayableNumber" runat="server" CssClass="Text" Text="50032"></asp:Label>
                </td>
                <td align="right" class="sidelinks">
                    Grand Total Paid:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblGrandTotalPaid" runat="server" CssClass="Text" Text="$200.00"></asp:Label>
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
                    Unpaid NFMC Eligible Cases:
                </td>
                <td class="Text" colspan="2">
                    <asp:Label ID="lblUnpaidNFMCEligibleCase" runat="server" CssClass="Text" Text="$0.00"></asp:Label>
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
                    Payable Comments:
                </td>
                <td colspan="7">
                    <asp:TextBox ID="txtPayableComments" runat="server" CssClass="Text" Height="80px"
                        TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="sidelinks">
                    Payable Items:
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
                                <asp:GridView ID="grvViewEditAgencyPayable" runat="server" CellPadding="2" ForeColor="#333333"
                                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" Width="100%"
                                    SelectedRowStyle-BackColor="Yellow">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
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
                                        <asp:BoundField DataField="AgencyCaseID" HeaderText="Agency Case ID" />
                                        <asp:BoundField DataField="CompleteDt" HeaderText="Complete Dt." />
                                        <asp:BoundField DataField="PaymentAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Amount" />
                                        <asp:BoundField DataField="LoanNumber" HeaderText="Loan Number" />
                                        <asp:BoundField DataField="ServicerName" HeaderText="Servicer" />
                                        <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name" />
                                        <asp:BoundField DataField="NFMCDifferenceEligibleInd" HeaderText="NFMC?" />
                                        <asp:BoundField DataField="NFMCDifferencePaidAmt" HeaderText="NFMC Paid Amt" DataFormatString="{0:C}"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="TakebackReason" HeaderText="Takeback Reason" />
                                        <asp:BoundField DataField="TakebackDate" HeaderText="Takeback Date" />
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
                                <asp:Label ID="lblTotalCase_ft" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="sidelinks">
                                Payable Total:
                            </td>
                            <td class="Text">
                                <asp:Label ID="lblPayableTotal_ft" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="sidelinks">
                                Total NFMC Up change Paid:
                            </td>
                            <td class="Text">
                                <asp:Label ID="lblTotalNFMCUpChangePaid_ft" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        

    <%--</ContentTemplate>
</asp:UpdatePanel>--%>