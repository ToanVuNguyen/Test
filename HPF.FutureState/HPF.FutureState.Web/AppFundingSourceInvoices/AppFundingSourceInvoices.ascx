<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppFundingSourceInvoices.ascx.cs" Inherits="HPF.FutureState.Web.AppFundingSourceInvoices.AppFundingSourceInvoices" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table style="width:100%;">
    <tr>
        <td align="center" colspan="6">
            <h1>Funding Source Invoices</h1></td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Funding Source</td>
        <td>
            <asp:DropDownList ID="dropFundingSource" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            Period Start:</td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Period End:</td>
        <td>
            <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnRefreshList" runat="server" CssClass="MyButton" 
                Text="Refresh List" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="sidelinks">
            Invoice List:</td>
        <td colspan="4">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5" rowspan="17">
            <asp:Panel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable" BorderStyle="Inset"
                BorderColor="Gray" BorderWidth="1px" Visible="false">
                <asp:GridView ID="grvForeClosureCaseSearch" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="100%"  SelectedRowStyle-BackColor="Yellow" 
                    onrowdatabound="grvForeClosureCaseSearch_RowDataBound" 
                   
                    onrowcreated="grvForeClosureCaseSearch_RowCreated">
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle"  />
                    <Columns>
                        <asp:BoundField DataField="FundingSourceId" HeaderText="Funding Source" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvoiceId" HeaderText="Invoice ID" />
                        <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" />
                        <asp:BoundField DataField="InvoicePeriod" HeaderText="Invoice Period" />
                        <asp:BoundField DataField="InvoiceBillAmt" HeaderText="Invoice Amount" />
                        <asp:BoundField DataField="InvoicePmtAmt" HeaderText="Payment Amount" />
                        <asp:BoundField DataField="PropertyZipStatusCd" HeaderText="Invoice Status" />
                        <asp:BoundField DataField="InvoiceComment" HeaderText="Comments" />
                    </Columns>
                    <EmptyDataTemplate>
                    There is no data match !
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
            
        </td>
        <td height="20" width="120">
            <asp:Button ID="btnNewInvoice" runat="server" CssClass="MyButton" 
                Text="New Invoice" Width="120px" />
        </td>
    </tr>
    <tr>
        <td height="20" width="120">
            <asp:Button ID="btnViewEditInvoice" runat="server" CssClass="MyButton" 
                Text="View/Edit Invoice" Width="120px" />
        </td>
    </tr>
    <tr>
        <td height="20" width="120">
            <asp:Button ID="btnCancelInvoice" runat="server" CssClass="MyButton" 
                Text="Cancel Invoice" Width="120px" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>

