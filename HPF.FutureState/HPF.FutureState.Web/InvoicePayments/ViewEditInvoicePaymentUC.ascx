<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEditInvoicePaymentUC.ascx.cs" Inherits="HPF.FutureState.Web.InvoicePayments.ViewEditInvoicePayment" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:HiddenField ID="hidFileName" runat="server" />
<table width="100%">
<colgroup>
    
<col width="12%" />
<col width="88%" />
</colgroup>
    <tr>
        <td colspan="2" align="center">
            <h1><asp:Label ID="lblTitle" runat="server" Text="New Invoice Payment"></asp:Label></h1></td>
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
        <td class="sidelinks" align="right" nowrap="nowrap">
            HPF Payment ID:</td>
        <td>
            <asp:Label ID="lblPaymentID" runat="server" CssClass="Text"></asp:Label></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Funding Source*:</td>
        <td>
            <asp:DropDownList ID="ddlFundingSource" runat="server" CssClass="Text" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Payment Number*:</td>
        <td>
            <asp:TextBox ID="txtPaymentNum" runat="server" CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Payment Date*:</td>
        <td>
            <asp:TextBox ID="txtPaymentDt" runat="server" CssClass="Text" Width="150px"></asp:TextBox>
            <cc1:CalendarExtender ID="txtPaymentDt_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtPaymentDt">
            </cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Payment Type*:</td>
        <td>
            <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="Text" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Payment Amount*:</td>
        <td>
            <asp:TextBox ID="txtPaymentAmt" runat="server"  CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Previous Reconciliation File:</td>
        <td>
            <asp:TextBox ID="txtPaymentFile" runat="server"  CssClass="Text" Width="100%" 
                ReadOnly="True" BackColor="#EBEBE4"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            &nbsp;New
            Reconciliation File:</td>
        <td>
            <asp:FileUpload ID="fileUpload" runat="server" Width="100%" CssClass="Text" Height="18px" 
                BackColor="#EBEBE4" onkeypress="return false;" onkeydown="return false;" />
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" valign="top">
            Comments:</td>
        <td>
            <asp:TextBox ID="txtComment" runat="server" CssClass="Text" Rows="6" TextMode="MultiLine"  Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="MyButton" 
                width="100px" OnClientClick="return ConfirmToUpdate();" onclick="btnSave_Click"/>
            &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="MyButton" Width="100px"
                    OnClick="btnCancel_Click" />
        </td>
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
    var paymentFile = document.getElementById('<%=txtPaymentFile.ClientID %>');
    var hidFileName = document.getElementById('<%=hidFileName.ClientID %>');
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
        hidFileName.value=fileUpload.value;
        paymentAfter = new invoicePayment(paymentId.value, fundingSource.value, paymentNum.value, paymentDt.value, paymentType.value, 
                                            paymentAmt.value, fileUpload.value, txtComment.value);        
        if(ComparePaymentObject(paymentAfter))
        {            
             Popup.showModal('modal');
             return false;
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
    function ConfirmToUpdate()
    {
        hidFileName.value=fileUpload.value;
        if(fileUpload.value=='' && paymentFile.value!='')
        {
            Popup.showModal('confirm');
            return false;
        }
        return true;
    }
    paymentFile.style.width= screen.width - 230;
    fileUpload.style.width = screen.width - 230;
</script>
<div id="modal" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    Do you want to save changes?
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYes" runat="server" OnClientClick="Popup.hide('modal');return ConfirmToUpdate();" 
                        CssClass="MyButton" Text="Yes" onclick="btnYes_Click" Width="70px" />
                    &nbsp;
                    <asp:Button ID="btnNo" runat="server" OnClientClick="Popup.hide('modal');" 
                        CssClass="MyButton" Width="70px" Text="No" onclick="btnNo_Click" />
                </td>
            </tr>
        </table>        
    </div>
 
 <div id="confirm" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="350" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    WARN689--This payment was originally created with a Reconcilition file. Do you really want to update it without a Reconciliation File?
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="saveYes" runat="server" OnClientClick="Popup.hide('confirm');" 
                        CssClass="MyButton" Text="Yes" Width="70px" onclick="saveYes_Click"  />
                    &nbsp;
                    <asp:Button ID="saveNo" runat="server" OnClientClick="Popup.hide('confirm');return false;" 
                        CssClass="MyButton" Width="70px" Text="No"  />
                </td>
            </tr>
        </table>        
    </div>