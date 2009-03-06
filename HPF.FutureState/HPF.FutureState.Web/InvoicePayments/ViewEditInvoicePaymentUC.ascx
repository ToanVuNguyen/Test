<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEditInvoicePaymentUC.ascx.cs" Inherits="HPF.FutureState.Web.InvoicePayments.ViewEditInvoicePayment" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table width="100%">
<colgroup>
<col width="16%" />
<col width="84%" />
</colgroup>
    <tr>
        <td colspan="2" align="center">
            <h1>View/Edit Invoice Payment</h1></td>
    </tr>
    <tr>
        <td colspan="2" class="ErrorMessage">
            <asp:BulletedList ID="lblErrorMessage" runat="server" BulletStyle="Square">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="left" colspan="2">
        <h3>
            Payment Infomation:</h3></td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            HPF Payment ID:</td>
        <td>
            <asp:Label ID="lblPaymentID" runat="server" CssClass="Text"></asp:Label></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Funding Source*:</td>
        <td>
            <asp:DropDownList ID="ddlFundingSource" runat="server" CssClass="Text" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Number*:</td>
        <td>
            <asp:TextBox ID="txtPaymentNum" runat="server" CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Date*:</td>
        <td>
            <asp:TextBox ID="txtPaymentDt" runat="server" CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Type*:</td>
        <td>
            <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="Text" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Amount*:</td>
        <td>
            <asp:TextBox ID="txtPaymentAmt" runat="server"  CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Reconciliation File:</td>
        <td>
            <asp:FileUpload ID="fileUpload"  runat="server" Width="100%" CssClass="Text" 
                Height="18px" />
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Comments:</td>
        <td>
            <asp:TextBox ID="txtComment" runat="server" CssClass="Text" Rows="6" TextMode="MultiLine"  Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="MyButton" 
                width="100px" onclick="btnSave_Click"/>
            &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="MyButton" Width="100px"
                    OnClick="btnCancel_Click" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="hiddenIsSave" />
<script type="text/javascript" language="javascript">
    var paymentId = document.getElementById('<%=lblPaymentID.ClientID %>');
    var fundingSource = document.getElementById('<%=ddlFundingSource.ClientID %>');
    var paymentNum = document.getElementById('<%=txtPaymentNum.ClientID %>');
    var paymentDt = document.getElementById('<%=txtPaymentDt.ClientID %>');        
    var paymentType = document.getElementById('<%=ddlPaymentType.ClientID %>');        
    var paymentAmt = document.getElementById('<%=txtPaymentAmt.ClientID %>');        
    var fileUpload = document.getElementById('<%=fileUpload.ClientID %>');
    var txtComment = document.getElementById('<%=txtComment.ClientID %>');
    var hidden = document.getElementById('<%=hiddenIsSave.ClientID %>');
    var invoicePayment = function(paymentId, fundingSource, paymentNum, paymentDt, paymentType, 
    paymentAmt, fileUpload, txtComment)
    {        
        this.paymentId      = paymentId   ; 
        this.fundingSource  = fundingSource;
        this.paymentNum     = paymentNum    ;   
        this.paymentDt      = paymentDt    ;
        this.paymentType    = paymentType  ;
        this.paymentAmt     = paymentAmt   ;
        this.fileUpload     = fileUpload   ;
        this.txtComment     = txtComment   ;
    }        
    
    var paymentBefore = new invoicePayment(paymentId.value, fundingSource.value, paymentNum.value, paymentDt.value, paymentType.value, 
                                            paymentAmt.value, fileUpload.value, txtComment.value);
    var paymentAfter = new invoicePayment();
    function ConfirmToCancel()
    {   
        paymentAfter = new invoicePayment(paymentId.value, fundingSource.value, paymentNum.value, paymentDt.value, paymentType.value, 
                                            paymentAmt.value, fileUpload.value, txtComment.value);        
        if(ComparePaymentObject(paymentAfter))
        {            
             if (confirm("Do you want to save changes?")==true)
                hidden.value ="true";
             else
                hidden.value ="false";
        }
        else
            hidden.value ="false";
        return true;
        
    }
    
    function ComparePaymentObject(paymentAfter)
    {
        if( paymentAfter.paymentId != paymentBefore.paymentId
            ||  paymentAfter.fundingSource != paymentBefore.fundingSource
            ||  paymentAfter.paymentNum != paymentBefore.paymentNum
            ||  paymentAfter.paymentDt != paymentBefore.paymentDt
            ||  paymentAfter.paymentAmt != paymentBefore.paymentAmt
            ||  paymentAfter.fileUpload != paymentBefore.fileUpload
            ||  paymentAfter.txtComment != paymentBefore.txtComment
            ||  paymentAfter.paymentType != paymentBefore.paymentType
        )
            return true;
        else
            return false;
    }
</script>