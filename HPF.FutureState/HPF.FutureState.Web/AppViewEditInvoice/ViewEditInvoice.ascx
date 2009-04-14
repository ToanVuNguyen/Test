<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEditInvoice.ascx.cs"
    Inherits="HPF.FutureState.Web.AppViewEditInvoice.ViewEditInvoice" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<asp:ScriptManager runat="server" ID="myscriptManager">
</asp:ScriptManager>
<table style="width: 100%;" cellpadding="1" cellspacing="1">
    <tr>
        <td colspan="7" align="center">
            <h1>
                View/Edit Invoice</h1>
        </td>
    </tr>
    <tr>
        <td class="Text" colspan="7">
            <asp:BulletedList ID="lblErrorMessage" runat="server" BulletStyle="Square" 
                CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" nowrap="nowrap">
            Funding Source:
        </td>
        <td class="Text" >
            <asp:Label ID="lblFundingSource" runat="server" CssClass="Text" Text="Citi Group"></asp:Label>
        </td>
        <td align="right" class="sidelinks" nowrap="nowrap">
            Total Cases:
        </td>
        <td class="Text" >
            <asp:Label ID="lblTotalCases" runat="server" Text="6"></asp:Label>
        </td>
        <td align="right" class="sidelinks" nowrap="nowrap">
            Reject Reason:
        </td>
        <td  width="180">
            <asp:DropDownList ID="dropRejectReason" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="left"  style="vertical-align: bottom">
            
                <asp:Button ID="btnReject" runat="server" CssClass="MyButton" Text="Reject Marked Cases"
                    Width="130px" OnClick="btnReject_Click" />
            
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" nowrap="nowrap">
            Period Start:
        </td>
        <td class="Text" >
            <asp:Label ID="lblPeriodStart" runat="server" Text="11/01/2008"></asp:Label>
        </td>
        <td align="right" class="sidelinks" nowrap="nowrap">
            Invoice Total:
        </td>
        <td class="Text" >
            <asp:Label ID="lblInvoiceTotal" runat="server"  Text="$700.00"></asp:Label>
        </td>
        <td align="right" class="sidelinks" nowrap="nowrap">
            &nbsp;
            HPF Payment ID:</td>
        <td >
            <asp:TextBox ID="txtPaymentID" runat="server" CssClass="Text" ></asp:TextBox>
        </td>
        <td align="left" >
            
                <asp:Button ID="btnPay" runat="server" CssClass="MyButton" Text="Pay Marked Cases"
                    Width="130px" OnClick="btnPay_Click" />
            
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" nowrap="nowrap">
            Period End:
        </td>
        <td class="Text" >
            <asp:Label ID="lblPeriodEnd" runat="server" Text="11/30/2008"></asp:Label>
        </td>
        <td align="right" class="sidelinks" colspan="2">
            &nbsp;
        </td>
        <td align="right" class="sidelinks">
            &nbsp;</td>
        <td  class="Text">
            &nbsp;</td>
        <td align="left" >
            
                <asp:Button ID="btnUnpay" runat="server" CssClass="MyButton" Text="Unpay Marked Cases"
                    Width="130px" OnClick="btnUnpay_Click" />
            
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" nowrap="nowrap">
            Invoice Number:
        </td>
        <td class="Text" >
            <asp:Label ID="lblInvoiceNumber" runat="server" Text="50032"></asp:Label>
        </td>
        <td align="right" class="sidelinks" >
            Total Paid:
        </td>
        <td class="Text" >
            <asp:Label ID="lblTotalPaid" runat="server" Text="$200.00"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td >
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
        </td>
        <td class="Text">
            &nbsp;
        </td>
        <td align="right" class="sidelinks" >
            Total Rejected:
        </td>
        <td class="Text" >
            <asp:Label ID="lblTotalRejected" runat="server" Text="$0.00"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td >
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" nowrap="nowrap" valign="top" >
            Invoice Comments:
        </td>
        <td colspan="6">
            <asp:TextBox ID="txtInvoiceComments" runat="server" CssClass="Text" Height="80px"
                TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left" class="sidelinks" colspan="7">
            Invoice Items: &nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" class="sidelinks" colspan="7">
            <cc1:StatefullScrollPanel ID="panInvoiceCases" runat="server" CssClass="ScrollTable"
                Width="100%" Visible="true" BorderWidth="1" BorderColor="LightGray">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvViewEditInvoice" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                            Width="100%" onrowcreated="grvViewEditInvoice_RowCreated">
                            <RowStyle CssClass="RowStyle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                            <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkCheckAll" runat="server" OnClick="selectAll(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelected" runat="server" OnClick="checkBoxClick()"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ForeclosureCaseId" HeaderText="Case ID"  ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="AgencyCaseNum" HeaderText="Agency Case ID"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="CaseCreateDt" HeaderText="Create Dt." DataFormatString="{0:d}"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="CaseCompleteDate" HeaderText="Complete Dt."  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="InvoiceCaseBillAmount" DataFormatString="{0:C}"  ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                                    HeaderText="Amount" />
                                <asp:BoundField DataField="LoanNumber" HeaderText="Loan Number"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="ServicerName" HeaderText="Servicer"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="PaidDate" HeaderText="Paid Date"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="InvoiceCasePaymentAmount" HeaderText="Paid Amt" DataFormatString="{0:C}" HeaderStyle-Wrap="false"
                                     ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentRejectReasonCode" HeaderText="Reject Reason"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="InvenstorLoanId" HeaderText="Investor Loan ID"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                        <table width="100%">
                            <tr>
                                <td align="right" class="sidelinks">
                                    Total Cases:
                                </td>
                                <td class="Text">
                                    <asp:Label ID="lblTotalCase1" runat="server"></asp:Label>
                                </td>
                                <td align="right" class="sidelinks">
                                    Invoice Total:
                                </td>
                                <td class="Text">
                                    <asp:Label ID="lblInvoiceTotal1" runat="server"></asp:Label>
                                </td>
                                <td align="right" class="sidelinks">
                                    Total Paid:
                                </td>
                                <td class="Text">
                                    <asp:Label ID="lblTotalPaid1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="SelectedRowIndex" runat="server"  />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="7">
            <asp:Button ID="Button1" runat="server" CssClass="MyButton" 
                onclick="Button1_Click" Text="Close" Width="130px" />
        </td>
    </tr>
</table>
<script type="text/javascript" language="javascript">    
var id='<%=SelectedRowIndex.ClientID %>';
var mypanel = document.getElementById('<%=panInvoiceCases.ClientID %>');
    
if(mypanel != null)
{                        
    mypanel.style.height = screen.height - 650; 
}
    
function validate(chk)
{
    var SelectedIndex = document.getElementById(id);    
  if (chk.checked == 1)
    SelectedIndex.value='true';
}
function onRejectClick()
{
    var SelectedIndex = document.getElementById(id);
    if(SelectedIndex.value!='')
    {
        Popup.showModal('modalReject');
        return false;
    }
    return true;
}
function onPayClick()
{
    var SelectedIndex = document.getElementById(id);
    if(SelectedIndex.value!='')
    {
        Popup.showModal('modalPay');
        return false;
    }
    return true;
}
function onUnPayClick()
{
    var SelectedIndex = document.getElementById(id);
    if(SelectedIndex.value!='')
    {
        Popup.showModal('modalUnPay');
        return false;
    }
    return true;
}
function selectAll(involker) 
    {
        // Since ASP.NET checkboxes are really HTML input elements
        //  let's get all the inputs 
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0 ; i < inputElements.length ; i++) 
        {
            var myElement = inputElements[i];
            // Filter through the input types looking for checkboxes
            if (myElement.type === "checkbox") 
            {
               // Use the involker (our calling element) as the reference 
               //  for our checkbox status
                myElement.checked = involker.checked;
            }
        }
        var SelectedIndex = document.getElementById(id);
        if(SelectedIndex!=null)
        {
            if(involker.checked==true)
                SelectedIndex.value='true';
            else      
                SelectedIndex.value='';
        }
    }
function checkBoxClick()
{
    var SelectedIndex = document.getElementById(id);
    var inputElements = document.getElementsByTagName('input');
    for (var i = 0 ; i < inputElements.length ; i++) 
    {
        var myElement = inputElements[i];
        // Filter through the input types looking for checkboxes
        if (myElement.type === "checkbox") 
        {
            if(myElement.checked ==true)
            {
                SelectedIndex.value='true';
                return;
            }
        }
    }
    SelectedIndex.value='';
}
</script>
<div id="modalPay" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    WARN0557--Are you sure you wish to pay the selected case(s)?
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYesPay" runat="server" OnClientClick="Popup.hide('modalPay');" 
                        CssClass="MyButton" Text="Yes" onclick="btnYesPay_Click" Width="70px" />
                    &nbsp;
                    <asp:Button ID="btnNoPay" runat="server" OnClientClick="Popup.hide('modalPay');return false;" CssClass="MyButton" Width="70px" Text="No" />
                </td>
            </tr>
        </table>        
</div>
<div id="modalUnPay" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    WARN0560--Are you sure you wish to unpay the selected case(s)
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYesUnPay" runat="server" OnClientClick="Popup.hide('modalUnPay');" 
                        CssClass="MyButton" Text="Yes"  Width="70px" onclick="btnYesUnPay_Click" />
                    &nbsp;
                    <asp:Button ID="btnNoUnPay" runat="server" OnClientClick="Popup.hide('modalUnPay');return false;" CssClass="MyButton" Width="70px" Text="No" />
                </td>
            </tr>
        </table>        
</div>
<div id="modalReject" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    WARN0554--Are you sure you wish to reject the selected case(s)?
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYesReject" runat="server" OnClientClick="Popup.hide('modalReject');" 
                        CssClass="MyButton" Text="Yes"  Width="70px" 
                        onclick="btnYesReject_Click" />
                    &nbsp;
                    <asp:Button ID="btnNoReject" runat="server" OnClientClick="Popup.hide('modalReject');return false;" CssClass="MyButton" Width="70px" Text="No" />
                </td>
            </tr>
        </table>        
</div>