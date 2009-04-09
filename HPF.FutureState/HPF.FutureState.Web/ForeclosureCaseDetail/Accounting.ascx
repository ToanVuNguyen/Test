<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Accounting.ascx.cs"
    Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Accounting" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
    <table width="100%">
        <tr>
            <td>
                <asp:BulletedList ID="bullblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:BulletedList>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="sidelinks" align="right">
                            Never Bill Reason:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNerverBillReason" runat="server" CssClass="Text" Width="150px"
                                Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="sidelinks">
                Billing Infomation:
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="panBillingInfo" CssClass="ScrollTable" BorderColor="LightGray" BorderWidth="1">
                    <asp:GridView ID="grvBillingInfo" runat="server" AutoGenerateColumns="false" Width="100%"
                        OnRowDataBound="grvBillingInfo_RowDataBound" BorderStyle="None">
                        <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Invoice Date" DataField="InvoiceDate" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Funding Source" DataField="FundingSourceName" />
                            <asp:BoundField HeaderText="Invoice #" DataField="InvoiceId" />
                            <asp:BoundField HeaderText="Loan #" DataField="Loan" />
                            <asp:BoundField HeaderText="1st/2nd" DataField="InDisputeIndicator" />
                            <asp:BoundField HeaderText="Bill Amt" DataField="InvoiceCaseBillAmount" DataFormatString="{0:C}"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="Pd Amt" DataField="InvoiceCasePaymentAmount" DataFormatString="{0:C}"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="Paid Date" DataField="PaidDate" DataFormatString="{0:d}" />
                            <asp:TemplateField HeaderText="Reject Reason">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentRejectReasonDesc" runat="server" Text='<%#Eval("PaymentRejectReasonCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            There is no billing information</EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="sidelinks" align="right">
                            Never Pay Reason:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNeverPayReason" runat="server" CssClass="Text" Width="150px"
                                Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="sidelinks">
                Payment Information:
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="panPaymentInfo" runat="server" CssClass="ScrollTable" BorderColor="LightGray" BorderWidth="1">
                    <asp:GridView ID="grvPaymentInfo" runat="server" AutoGenerateColumns="false" Width="99%"
                        BorderStyle="None">
                        <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Payable Date" DataField="PaymentDate" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Agency" DataField="AgencyName" />
                            <asp:BoundField HeaderText="Payable #" DataField="AgencyPayableId" />
                            <asp:BoundField HeaderText="Pay Amt" DataField="PaymentAmount" DataFormatString="{0:C}"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="NFMC Diff Elig" DataField="NFMCDifferenceEligibleInd"
                                ItemStyle-HorizontalAlign="Center" />
                                  <asp:BoundField HeaderText="NFMC Diff Pd" DataField="NFMCDifferencePaidAmt" DataFormatString="{0:C}"
                                ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <EmptyDataTemplate>
                            There is not payment information</EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
        <td align="center">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="MyButton" Width="100px"
        OnClick="btnSave_Click" />
        </td>
        </tr>
    </table>
<asp:HiddenField ID="selTabCtrl"  runat="server"  Value=""/>

<script type="text/javascript" language="javascript">
    var msfWARN0450 = '<%= msgWARN0450 %>';
    var neverPayReason = document.getElementById('<%=ddlNeverPayReason.ClientID %>');
    var neverBillReason = document.getElementById('<%=ddlNerverBillReason.ClientID %>');
    var forecloseCase = function(neverPayReason, neverBillReason) {
        this.neverPayReason = neverPayReason;
        this.neverBillReason= neverBillReason;
    }
    var foreclosureCaseBefore = new forecloseCase(neverPayReason.value, neverBillReason.value);
    var foreclosureCaseAfter = new forecloseCase();
    
    var tempTabId = document.getElementById('<%=selTabCtrl.ClientID%>');
        if(tempTabId.value!='')
        {
            tabid = tempTabId.value;
            tempTabId.value='';
            TabControl.SelectTab(tabid);
        }
        
    TabControl.onChanged = function ChangeData(toTabId) {    
        foreclosureCaseAfter = new forecloseCase(neverPayReason.value, neverBillReason.value);            
    
        if (ComparePaymentObject(foreclosureCaseAfter))
        {
            tempTabId.value = toTabId;
            Popup.showModal('mdgCaseAccounting');             
            return false;     
        }
        return true;
    }
    
    function ComparePaymentObject(foreclosureCaseAfter) {
        if (foreclosureCaseAfter.neverPayReason != foreclosureCaseBefore.neverPayReason
            || foreclosureCaseAfter.neverBillReason != foreclosureCaseBefore.neverBillReason
        )
            return true;
        else
            return false;
    }
   
    var panelHeight = (screen.height - 660)/2;
    var panBillingInfo = document.getElementById('<%=panBillingInfo.ClientID %>');
    if(panBillingInfo != null)
    {                
        panBillingInfo.style.height = panelHeight;
    }
    var panPaymentInfo = document.getElementById('<%=panPaymentInfo.ClientID %>');
    if(panPaymentInfo != null)
    {                
        panPaymentInfo.style.height = panelHeight;
    }
</script>

<div id="mdgCaseAccounting" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    <%=msgWARN0450%>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYes" runat="server" OnClientClick="Popup.hide('mdgCaseAccounting');" 
                        CssClass="MyButton" Text="Yes" onclick="btnYes_Click" Width="70px" />
                    &nbsp;
                    <asp:Button ID="btnNo" runat="server" OnClientClick="Popup.hide('mdgCaseAccounting');TabControl.SelectTab(tempTabId.value)" CssClass="MyButton" Width="70px" Text="No" />
                </td>
            </tr>
        </table>        
