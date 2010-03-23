<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppFundingSourceInvoices.ascx.cs" Inherits="HPF.FutureState.Web.AppFundingSourceInvoices.AppFundingSourceInvoices" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>

<asp:ScriptManager runat="server"></asp:ScriptManager>


<table style="width:100%;" >
    <tr>
        <td align="center" colspan="5">
            <h1>Funding Source Invoices</h1>
            
        </td>
        <td align="center" rowspan="3">
            <asp:HyperLink ID="lblPortal" runat="server" Target="_blank">
            <img alt="Invoices on Portal" src="Styles/Images/HPFLogo.jpg" 
                style="width: 55px; height: 55px" border="0"/><br />
            Invoices on Portal</asp:HyperLink>
            <br />
            
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" nowrap="nowrap" >
            Funding Source*:
        </td>
        <td align="left" width="280"  >
            <asp:DropDownList ID="dropFundingSource" runat="server" CssClass="Text" 
                Width="280px">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" nowrap="nowrap" >
            Period Start*:</td>
        <td colspan="2" >
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text">1/1/2003</asp:TextBox>
            <cc2:CalendarExtender ID="txtPeriodStart_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtPeriodStart">
            </cc2:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Include Cancelled*:</td>
        <td width="280">
            <asp:DropDownList ID="dropIncludeCancel" runat="server" CssClass="Text" 
                Width="47px" Height="16px">
                <asp:ListItem Selected="True" Value="false">No</asp:ListItem>
                <asp:ListItem Value="true">Yes</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" nowrap="nowrap" >
            Period End*:</td>
        <td >
            <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text">1/1/2010</asp:TextBox>
            <cc2:CalendarExtender ID="txtPeriodEnd_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtPeriodEnd">
            </cc2:CalendarExtender>
        </td>
        <td align="right">
            <asp:Button ID="btnRefreshList" runat="server" CssClass="MyButton" 
                Text="Search" onclick="btnRefreshList_Click" Width="120px" />
        </td>
    </tr>
    <tr>
        <td class="ErrorMessage" colspan="6">
            <asp:BulletedList ID="lblErrorMessage" BulletStyle="Square" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" colspan="6">
            <asp:Label ID="lblInvoiceList" runat="server" Text="Invoice List:" 
                Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="5" >
        <cc1:StatefullScrollPanel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable" 
                 Width="100%" Visible="true" BorderColor="LightGray" BorderWidth="1">
                 <asp:UpdatePanel runat="server"   >                 
                 <ContentTemplate>                                 
                <asp:GridView ID="grvFundingSourceInvoices" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="False" CssClass="GridViewStyle" 
                    Width="100%" ondatabound="grvFundingSourceInvoices_DataBound" 
                    onrowdatabound="grvFundingSourceInvoices_RowDataBound" DataKeyNames="InvoiceId" 
                         onselectedindexchanged="grvFundingSourceInvoices_SelectedIndexChanged" >
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1"  ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle"  />
                    <Columns>
                        <asp:BoundField DataField="FundingSourceName" HeaderText="Funding Source" 
                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InvoiceId" HeaderText="Invoice#" 
                            ItemStyle-HorizontalAlign="Right"  HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceDate" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="InvoicePeriod" HeaderText="Invoice Period" 
                            ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="140" 
                            HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InvoiceBillAmount" DataFormatString="{0:C}" 
                            ItemStyle-HorizontalAlign="Right"  HeaderText="Invoice Amt" 
                            HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InvoicePaymentAmount" DataFormatString="{0:C}" 
                            ItemStyle-HorizontalAlign="Right"  HeaderText="Payment Amt" 
                            HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RejectedAmount" DataFormatString="{0:C}" 
                            HeaderText="Rejected Amt" />
                        <asp:BoundField DataField="UnfundedAmount" DataFormatString="{0:C}" 
                            HeaderText="Unfunded Amt" />
                        <asp:BoundField DataField="StatusCode" HeaderText="Invoice Status" 
                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InvoiceComment" HeaderText="Comments" 
                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" 
                            ControlStyle-CssClass="MyButton" ItemStyle-HorizontalAlign="Center" 
                            HeaderText="Select" HeaderStyle-Wrap="false" >
                            <ControlStyle CssClass="MyButton" />
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="SelectedRowIndex" runat="server"  />
                </ContentTemplate>                
                 </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
            
        </td>
        <td width="120" style="vertical-align: top">
            <asp:Button ID="btnNewInvoice" runat="server" CssClass="MyButton" 
                Text="New Invoice" Width="120px" onclick="btnNewInvoice_Click" />
            <asp:Button ID="btnViewEditInvoice" runat="server" CssClass="MyButton" 
                Text="View/Edit Invoice" Width="120px" 
                onclick="btnViewEditInvoice_Click" />
            <asp:Button ID="btnCancelInvoice" runat="server" CssClass="MyButton" 
                Text="Cancel Invoice" Width="120px" onclick="btnCancelInvoice_Click" />
        </td>
    </tr>
    </table>

<script type="text/javascript" language="javascript">    
    var id='<%=SelectedRowIndex.ClientID %>';
     var mypanel = document.getElementById('<%=panForeClosureCaseSearch.ClientID %>');
            if(mypanel != null)
            {                
                mypanel.style.height = screen.height - 495;
            }
        
    function CancelClientClick()
    {
        var SelectedIndex = document.getElementById(id);    
        if(SelectedIndex.value=='')
        {
            return true;
        }
        else
        {
            Popup.showModal('modal');
            return false;
        }
    }
    
</script>
<div id="modal" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    WARN0551--Are you sure you wish to cancel this Invoice?
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYes" runat="server" OnClientClick="Popup.hide('modal');" 
                        CssClass="MyButton" Text="Yes" onclick="btnYes_Click" Width="70px" />
                    &nbsp;
                    <asp:Button ID="btnNo" runat="server" OnClientClick="Popup.hide('modal');return false;" CssClass="MyButton" Width="70px" Text="No" />
                </td>
            </tr>
        </table>        
    </div>