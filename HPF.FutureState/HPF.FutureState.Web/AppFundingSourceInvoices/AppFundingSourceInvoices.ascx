﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppFundingSourceInvoices.ascx.cs" Inherits="HPF.FutureState.Web.AppFundingSourceInvoices.AppFundingSourceInvoices" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager runat="server"></asp:ScriptManager>
<%--<asp:UpdatePanel runat="server">
<ContentTemplate>--%>
<table style="width:100%;">
    <tr>
        <td align="center" colspan="6">
            <h1>Funding Source Invoices</h1>
            
        </td>
    </tr>
    <tr>
        <td align="left" colspan="6">
            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList></td>
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
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text">1/1/2003</asp:TextBox>
        </td>
        <td class="style1">
            &nbsp;</td>
        <td rowspan="2" align="center" style="vertical-align: top">
            <img alt="" src="Styles/Images/HPFLogo.jpg" 
                style="width: 55px; height: 55px" /><br />
            <asp:LinkButton ID="lblPortal" runat="server">Invoices on Portal</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td align="right" class="sidelinks">
            Period End:</td>
        <td>
            <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text">1/1/2010</asp:TextBox>
        </td>
        <td >
            <asp:Button ID="btnRefreshList" runat="server" CssClass="MyButton" 
                Text="Refresh List" onclick="btnRefreshList_Click" />
        </td>
    </tr>
    <tr>
        <td class="sidelinks">
            <asp:Label ID="lblInvoiceList" runat="server" Text="Invoice List" 
                Visible="False"></asp:Label>
        </td>
        <td colspan="4">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5" rowspan="17">
        <cc1:StatefullScrollPanel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable" 
                 Width="800" Visible="true">
                 <asp:UpdatePanel runat="server">
                 <ContentTemplate>
                 
                <asp:GridView ID="grvFundingSourceInvoices" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="100%" ondatabound="grvFundingSourceInvoices_DataBound" 
                    onrowdatabound="grvFundingSourceInvoices_RowDataBound" DataKeyNames="InvoiceId" >
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1"  ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle"  />
                    <Columns>
                        <asp:BoundField DataField="FundingSourceName" HeaderText="Funding Source" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvoiceId" HeaderText="Invoice#" />
                        <asp:TemplateField HeaderText="Invoice Date">
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceDate" runat="server"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="InvoicePeriod" HeaderText="Invoice Period" />
                        <asp:BoundField DataField="InvoiceBillAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Invoice Amount" />
                        <asp:BoundField DataField="InvoicePaymentAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Payment Amount" />
                        <asp:BoundField DataField="StatusCode" HeaderText="Invoice Status" />
                        <asp:BoundField DataField="InvoiceComment" HeaderText="Comments" />
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton" ItemStyle-HorizontalAlign="Center" HeaderText="Select" />
                    </Columns>
                    <EmptyDataTemplate>
                    There is no data match !
                    </EmptyDataTemplate>
                </asp:GridView>
                </ContentTemplate>
                 </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
            
        </td>
        <td height="20" width="120">
            <asp:Button ID="btnNewInvoice" runat="server" CssClass="MyButton" 
                Text="New Invoice" Width="120px" onclick="btnNewInvoice_Click" />
        </td>
    </tr>
    <tr>
        <td height="20" width="120">
            <asp:Button ID="btnViewEditInvoice" runat="server" CssClass="MyButton" 
                Text="View/Edit Invoice" Width="120px" 
                onclick="btnViewEditInvoice_Click" />
        </td>
    </tr>
    <tr>
        <td height="20" width="120">
            <span onclick="return confirm('Do you really want to cancel the Invoice')">
                <asp:Button ID="btnCancelInvoice" runat="server" CssClass="MyButton" 
                    Text="Cancel Invoice" Width="120px" onclick="btnCancelInvoice_Click" />
            </span>
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
<%--</ContentTemplate>
</asp:UpdatePanel>--%>