<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Accounting.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Accounting" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

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
        <asp:GridView ID="grvBillingInfo" runat="server" AutoGenerateColumns="false" Width="100%">
        <HeaderStyle CssClass="FixedHeader" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <RowStyle CssClass="RowStyle" />
        <Columns>
        <asp:BoundField HeaderText="Invoice Date" DataField="InvoiceDate" DataFormatString="{0:d}" />
        <asp:BoundField HeaderText="Funding Source" DataField="FundingSourceName" />
        <asp:BoundField HeaderText="Invoice #" DataField="InvoiceId" />
        <asp:BoundField HeaderText="Loan #" DataField="Loan" />
        <asp:BoundField HeaderText="1st/2nd" DataField="InDisputeIndicator" />
        <asp:BoundField HeaderText="Bill Amt" DataField="InvoiceCaseBillAmount" DataFormatString="{0:C}"  />
        <asp:BoundField HeaderText="Pd Amt" DataField="InvoiceCasePaymentAmount" DataFormatString="{0:C}" />
        <asp:BoundField HeaderText="Paid Date" DataField="PaidDate" DataFormatString="{0:d}" />
        <asp:BoundField HeaderText="Reject Reason" DataField="PaymentRejectReasonCode" />
        </Columns>    
        <EmptyDataTemplate>There is no data match</EmptyDataTemplate>   
        </asp:GridView>
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
        <asp:GridView ID="grvPaymentInfo" runat="server" AutoGenerateColumns="false" Width="80%">
        <HeaderStyle CssClass="FixedHeader" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <RowStyle CssClass="RowStyle" />
        <Columns>
        <asp:BoundField HeaderText="Payable Date" DataField="PaymentDate"  DataFormatString="{0:d}"/>
        <asp:BoundField HeaderText="Agency" DataField="AgencyName" />
        <asp:BoundField HeaderText="Payable #" DataField="AgencyPayableId" />
        <asp:BoundField HeaderText="Pay Amt" DataField="PaymentAmount" DataFormatString="{0:C}"  />
        <asp:BoundField HeaderText="NFMC Diff Elig" DataField="NFMCDiffererencePaidInd" />
        <asp:BoundField HeaderText="NFMC Diff Pd" DataField="NFMCDifferenceEligibleInd" />
        </Columns>
        <EmptyDataTemplate>There is no data match</EmptyDataTemplate>
        </asp:GridView>
        </td>
        </tr>
    </table>

 </div>
 <div align="center">
 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="MyButton" 
         Width="100px" onclick="btnSave_Click"  />
 </div>
