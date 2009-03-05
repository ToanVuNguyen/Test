<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Accounting.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Accounting" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<div style="overflow:auto; height:400px;">

    <table  width="100%">
        <tr>
            <td>
               <asp:Label ID="lblMessage" runat="server" CssClass="ErrorMessage" Text=""></asp:Label>
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
               <asp:DropDownList ID="ddlNerverBillReason" runat="server" CssClass="Text" Width="150px"></asp:DropDownList>
               </td>
               </tr>
               </table>
               </td>
        </tr>
        <tr>
            <td class="sidelinks" >
             Billing Infomation:
               </td>
        </tr>
        <tr>
        <td>
        <asp:Panel runat="server" ID="panBillingInfo" CssClass="ScrollTable" BorderStyle="None">
        <asp:GridView ID="grvBillingInfo" runat="server" AutoGenerateColumns="false" 
                Width="100%" onrowdatabound="grvBillingInfo_RowDataBound">
        <HeaderStyle CssClass="FixedHeader" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <RowStyle CssClass="RowStyle" />
        <Columns>
        <asp:BoundField HeaderText="Invoice Date" DataField="InvoiceDate" DataFormatString="{0:d}" />
        <asp:BoundField HeaderText="Funding Source" DataField="FundingSourceName" />
        <asp:BoundField HeaderText="Invoice #" DataField="InvoiceId" />
        <asp:BoundField HeaderText="Loan #" DataField="Loan" />
        <asp:BoundField HeaderText="1st/2nd" DataField="InDisputeIndicator" />
        <asp:BoundField HeaderText="Bill Amt" DataField="InvoiceCaseBillAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"  />
        <asp:BoundField HeaderText="Pd Amt" DataField="InvoiceCasePaymentAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
        <asp:BoundField HeaderText="Paid Date" DataField="PaidDate" DataFormatString="{0:d}" />
        <asp:TemplateField HeaderText="Reject Reason">
        <ItemTemplate>
        <asp:Label ID="lblPaymentRejectReasonDesc" runat="server" Text='<%#Eval("PaymentRejectReasonCode") %>' ></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>    
        <EmptyDataTemplate>There is no billing information</EmptyDataTemplate>   
        </asp:GridView>
        </asp:Panel>
        </td>
        </tr>
        <tr>
        <td></td>
        </tr>
        <tr>
        <td>
        <table>
        <tr>
        <td  class="sidelinks" align="right">
        Never Pay Reason: 
        </td>
        <td>
        <asp:DropDownList ID="ddlNeverPayReason" runat="server" CssClass="Text" Width="150px"></asp:DropDownList>
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
        <asp:Panel ID="panPaymentInfo" runat="server" CssClass="ScrollTable" BorderStyle="None">
        <asp:GridView ID="grvPaymentInfo" runat="server" AutoGenerateColumns="false" Width="80%">
        <HeaderStyle CssClass="FixedHeader" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <RowStyle CssClass="RowStyle" />
        <Columns>
        <asp:BoundField HeaderText="Payable Date" DataField="PaymentDate"  DataFormatString="{0:d}"/>
        <asp:BoundField HeaderText="Agency" DataField="AgencyName" />
        <asp:BoundField HeaderText="Payable #" DataField="AgencyPayableId" />
        <asp:BoundField HeaderText="Pay Amt" DataField="PaymentAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"  />
        <asp:BoundField HeaderText="NFMC Diff Elig" DataField="NFMCDifferenceEligibleInd" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <EmptyDataTemplate>There is not payment information</EmptyDataTemplate>
        </asp:GridView>
        </asp:Panel>
        </td>
        </tr>
    </table>

 </div>
 <div align="center">
 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="MyButton" 
         Width="100px" onclick="btnSave_Click"  />
 </div>
<script type="text/javascript" language="javascript">
    var neverPayReason = document.getElementById('<%=ddlNeverPayReason.ClientID %>');
    var neverBillReason = document.getElementById('<%=ddlNerverBillReason.ClientID %>');
    var forecloseCase = function(neverPayReason, neverBillReason) {
        this.neverPayReason = neverPayReason;
        this.neverBillReason= neverBillReason;
    }
    var foreclosureCaseBefore = new forecloseCase(neverPayReason.value, neverBillReason.value);
    var foreclosureCaseAfter = new forecloseCase();
    TabControl.onChanged = function ChangeData() {
    foreclosureCaseAfter = new forecloseCase(neverPayReason.value, neverBillReason.value);
    
    if (ComparePaymentObject(foreclosureCaseAfter)) {
            return (confirm('Your data is changed, You do not want to save it. ') );
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
   
</script>

