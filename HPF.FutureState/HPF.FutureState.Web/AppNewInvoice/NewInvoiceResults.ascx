<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewInvoiceResults.ascx.cs" Inherits="HPF.FutureState.Web.AppNewInvoice.NewInvoiceResults" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        height: 17px;
    }
</style>
<table style="width:100%;">
    <tr>
        <td colspan="6">
            <h1 align="center">New Invoice Results</h1></td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Funding Source:</td>
        <td class="style1">
            <asp:Label ID="lblFundingSource" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td align="right" class="sidelinks" colspan="2">
            Total Cases:</td>
        <td class="style1">
            <asp:Label ID="lblTotalCases" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td class="style1">
            <asp:Button ID="btnRemoveMarkedCases" runat="server" CssClass="MyButton" 
                Text="Remove Marked Cases" Width="150px" />
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Period Start:</td>
        <td>
            <asp:Label ID="lblPeriodStart" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td align="right" class="sidelinks" colspan="2">
            Total Amount:</td>
        <td>
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td>
            <asp:Button ID="btnExport" runat="server" CssClass="MyButton" 
                Text="Export to Excel" Width="150px" />
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Period End:</td>
        <td>
            <asp:Label ID="lblPeriodEnd" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td colspan="2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="sidelinks">
            Invoice Items:</td>
        <td>
            &nbsp;</td>
        <td colspan="2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center"  colspan="6">
            <asp:Panel ID="panInvoiceResultsPage" runat="server" CssClass="ScrollTable" BorderStyle="Inset"
                BorderColor="Gray" BorderWidth="1px" Width="100%" >
                <asp:GridView ID="grvNewInvoiceResults" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="100%"  SelectedRowStyle-BackColor="Yellow" 
                    ondatabound="grvNewInvoiceResults_DataBound" 
                    onrowdatabound="grvNewInvoiceResults_RowDataBound" >
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle"  />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="chkCheckAll" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkCaseSelected" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ForeclosureCaseId" HeaderText="Case ID" />
                        <asp:BoundField DataField="AgencyCaseId" HeaderText="Agency Case ID" />
                        <asp:TemplateField HeaderText="Complete Dt.">
                            <ItemTemplate>
                                <asp:Label ID="lblCompleteDate" runat="server"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="AccountLoanNumber" HeaderText="Primary Loan Num" />
                        <asp:BoundField DataField="ServicerName" HeaderText="Servicer" />
                        <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name" />
                    </Columns>
                    <EmptyDataTemplate>
                    There is no data match !
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" colspan="3">
            <asp:Button ID="btnGenerateInvoice" runat="server" CssClass="MyButton" 
                Text="Generate Invoice" Width="150px" />
        </td>
        <td colspan="3">
            <asp:Button ID="btnCancel" runat="server" CssClass="MyButton" 
                Text="Cancel Invoice" Width="150px" />
        </td>
    </tr>
</table>
