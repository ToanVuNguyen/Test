<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseLoan.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.CaseLoan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Case Loan</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
            font-size: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td colspan=4 class="style1">Case Loan </td>
    </tr>
    <tr>
    <td>Servicer ID:</td>
    <td><asp:TextBox ID="txtServicerId" runat="server"></asp:TextBox></td>
    <td>Other Service name:</td>
    <td><asp:TextBox ID="txtOtherServiceName" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Acc num:</td>
    <td><asp:TextBox ID="txtAccNum" runat="server"></asp:TextBox></td>
    <td>Loan 1st 2nd:</td>
    <td><asp:TextBox ID="txtLoan1st2nd" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Morgage type code:</td>
    <td><asp:TextBox ID="txtMorgageTypeCode" runat="server"></asp:TextBox></td>
    <td>Arm reset indicator: </td>
    <td><asp:TextBox ID="txtArmResetInd" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Term length code:</td>
    <td><asp:TextBox ID="txtTermLengthCd" runat="server"></asp:TextBox></td>
    <td>Loan delinq status code:</td>
    <td><asp:TextBox ID="txtLoanDelinqCd" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Current loan balance amt:</td>
    <td><asp:TextBox ID="txtCurrentLoanBalanceAmt" runat="server"></asp:TextBox></td>
    <td>Orig loan amt:</td>
    <td><asp:TextBox ID="txtOrigLoanAmt" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Interest rate:</td>
    <td><asp:TextBox ID="txtInterestRate" runat="server"></asp:TextBox></td>
    <td>Originating lender name:</td>
    <td><asp:TextBox ID="txtOrigLenderName" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Orig mortgage co FDIC NCUS num:</td>
    <td><asp:TextBox ID="txtOrigMorgageNum" runat="server"></asp:TextBox></td>
    <td>Orig mortgage con name:</td>
    <td><asp:TextBox ID="txtOrigMotgateConName" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Original loan num:</td>
    <td><asp:TextBox ID="txtOrigLoanNum" runat="server"></asp:TextBox></td>
    <td>FDIC NCUA num current servicer TBD:</td>
    <td><asp:TextBox ID="txtFDICNCUANum" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Current servicer name TBD:</td>
    <td><asp:TextBox ID="txtCurrentServiceNameTBD" runat="server"></asp:TextBox></td>
    <td>Freddie loan num:</td>
    <td><asp:TextBox ID="txtFreddieLoanNUm" runat="server"></asp:TextBox></td>
    </tr>
    </table>    
    </div>
    </form>
</body>
</html>
