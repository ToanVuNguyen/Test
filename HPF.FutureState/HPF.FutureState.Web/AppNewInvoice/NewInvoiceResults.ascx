<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewInvoiceResults.ascx.cs" Inherits="HPF.FutureState.Web.AppNewInvoice.NewInvoiceResults" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager runat="server"></asp:ScriptManager>

<table style="width:100%;" >
    <tr>
        <td colspan="5">
            <h1 align="center">New Invoice Results</h1></td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="15%">
            Funding Source:</td>
        <td style="vertical-align: top" >
            <asp:Label ID="lblFundingSource" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td align="right" class="sidelinks" >
            Total Cases:</td>
        <td style="vertical-align: top" >
            <asp:Label ID="lblTotalCases" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td align="center" >
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Period Start:</td>
        <td style="vertical-align: top">
            <asp:Label ID="lblPeriodStart" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td align="right" class="sidelinks">
            Total Amount:</td>
        <td style="vertical-align: top">
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td align="center">
            <asp:Button ID="btnRemoveMarkedCases" runat="server" CssClass="MyButton" 
                Text="Remove Marked Cases" Width="150px" 
                onclick="btnRemoveMarkedCases_Click" />
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Period End:</td>
        <td style="vertical-align: top">
            <asp:Label ID="lblPeriodEnd" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td colspan="3" align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Invoice 
            <br />
            Comments:</td>
        <td colspan="4">
            <asp:TextBox ID="txtComment" runat="server" CssClass="Text" Height="70px" 
                TextMode="MultiLine" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left"  colspan="5" class="ErrorMessage">
            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td align="left" class="sidelinks" colspan="5">
                        Invoice Items:</td>
    </tr>
    <tr>
        <td align="center" colspan="5">
            <cc1:StatefullScrollPanel ID="panInvoiceResultsPage" runat="server" CssClass="ScrollTable"
                BorderStyle="Inset" BorderColor="Gray" BorderWidth="1px" Width="100%">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvNewInvoiceResults" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" Width="100%"
                            OnDataBound="grvNewInvoiceResults_DataBound" OnRowDataBound="grvNewInvoiceResults_RowDataBound">
                            <RowStyle CssClass="RowStyle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="chkHeaderCaseIDCheck"
                                            ID="chkCheckAll" />
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
                                <asp:BoundField DataField="Amount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="Amount" />
                                <asp:BoundField DataField="AccountLoanNumber" HeaderText="Primary Loan Num" />
                                <asp:BoundField DataField="ServicerName" HeaderText="Servicer" />
                                <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name" />
                            </Columns>
                            <EmptyDataTemplate>
                                There is no data match !
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
    </tr>
    <tr>
        <td align="center" class="sidelinks" colspan="5">
            <asp:Button ID="btnGenerateInvoice" runat="server" CssClass="MyButton" 
                Text="Generate Invoice &amp; Export File" Width="180px" 
                onclick="btnGenerateInvoice_Click" />
                &nbsp
            <asp:Button ID="btnCancel" runat="server" CssClass="MyButton" 
                Text="Cancel Invoice" Width="180px" onclick="btnCancel_Click" />
        </td>
    </tr>
</table>
