<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoicePaymentsUC.ascx.cs"
    Inherits="HPF.FutureState.Web.InvoicePayments.InvoicePaymentsUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<asp:ScriptManager ID="myScript" runat="server">
</asp:ScriptManager>

<table width="100%">
    <tr>
        <td colspan="6" align="center" >
            <h1>
            Invoice Payments
            </h1>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Funding Source*:</td>        
        <td  align="left" nowrap>
            <asp:DropDownList ID="ddlFundingSource" runat="server" Height="16px" 
                CssClass="Text" Width="280px">
            </asp:DropDownList>
        </td>        
        <td  align="right" class="sidelinks" nowrap="nowrap">
            Period Start*:
        </td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text"  MaxLength="100"></asp:TextBox>
            <cc2:CalendarExtender ID="txtPeriodStart_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtPeriodStart">
            </cc2:CalendarExtender>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td width="280" >
        </td>
        <td  align="right" class="sidelinks" nowrap="nowrap">
            Period End*:
        </td>
        <td>
            <asp:TextBox ID="txtPeriodEnd" runat="server"  MaxLength="100" CssClass="Text"></asp:TextBox>            
            <cc2:CalendarExtender ID="txtPeriodEnd_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtPeriodEnd">
            </cc2:CalendarExtender>
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
                            AutoGenerateColumns="false" DataKeyNames="InvoicePaymentID" 
                            onselectedindexchanged="grvInvoicePaymentList_SelectedIndexChanged">
                            <HeaderStyle Wrap="false" CssClass="FixedHeader"  HorizontalAlign="Center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Funding Source" DataField="FundingSourceName"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                <asp:BoundField HeaderText="HPF Payment ID" DataField="InvoicePaymentID"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Payment#" DataField="PaymentNum"  ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Payment Date" DataField="PaymentDate" DataFormatString="{0:d}"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Payment Code" DataField="PaymentTypeDesc"  ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Payment Amt" DataField="PaymentAmount" DataFormatString="{0:C}" HeaderStyle-Wrap="false"
                                     ItemStyle-HorizontalAlign="Right"  />
                                <asp:BoundField HeaderText="Comments" DataField="Comments"  ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false"/>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton"
                                    ItemStyle-HorizontalAlign="Center" HeaderText="Select" HeaderStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="SelectedRowIndex" runat="server"  />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
        <td valign="top" width="120">
                    <asp:Button ID="btnNewPayable" runat="server" Text="New Payment" Width="120px" CssClass="MyButton"
                        OnClick="btnNewPayable_Click" />
                    
                    <asp:Button ID="btnViewEditPayable" runat="server" Text="View/Edit Payment" CssClass="MyButton"
                        Width="120px" OnClick="btnViewPayable_Click" />
        </td>
    </tr>
</table>
