<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoicePaymentsUC.ascx.cs"
    Inherits="HPF.FutureState.Web.InvoicePayments.InvoicePaymentsUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="myScript" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="6" class="Header">
            Invoice Payments
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="left" colspan="2" nowrap>
            Funding Source:
            <asp:DropDownList ID="ddlFundingSource" runat="server" Height="16px" CssClass="Text">
            </asp:DropDownList>
        </td>        
        <td class="sidelinks" align="right">
            Period Start:
        </td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text"  MaxLength="100"></asp:TextBox>
        </td>
        <td colspan="2">
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
            <asp:TextBox ID="txtPeriodEnd" runat="server"  MaxLength="100" CssClass="Text"></asp:TextBox>            
        </td>        
        <td align="right"><asp:Button ID="btnRefreshList" runat="server" Text="Search" Width="120px"
                CssClass="MyButton" OnClick="btnRefreshList_Click" /></td>
    </tr>
    <tr>
        <td colspan="6" class="ErrorMessage">
            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td colspan="6" class="sidelinks">
            Payment List:
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <cc1:StatefullScrollPanel ID="panInvoiceList" runat="server" CssClass="ScrollTable"
                Width="100%">
                <asp:UpdatePanel ID="myUPanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvInvoicePaymentList" runat="server" BorderStyle="None" Width="100%"
                            AutoGenerateColumns="false" DataKeyNames="InvoicePaymentID">
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Funding Source" DataField="FundingSourceName" />
                                <asp:BoundField HeaderText="HPF Payment ID" DataField="InvoicePaymentID" />
                                <asp:BoundField HeaderText="Payment#" DataField="PaymentNum" />
                                <asp:BoundField HeaderText="Payment Date" DataField="PaymentDate" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Payment Code" DataField="PaymentTypeDesc" />
                                <asp:BoundField HeaderText="Payment Amt" DataField="PaymentAmount" DataFormatString="{0:C}"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Comments" DataField="Comments"/>
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
                        <asp:Button ID="btnNewPayable" runat="server" Text="New Payment" Width="120px" CssClass="MyButton"
                            OnClick="btnNewPayable_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnViewEditPayable" runat="server" Text="View/Edit Payment" CssClass="MyButton"
                            Width="120px" OnClick="btnViewPayable_Click" />
                    </td>
                </tr>
                </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
        </td>
    </tr>
</table>
