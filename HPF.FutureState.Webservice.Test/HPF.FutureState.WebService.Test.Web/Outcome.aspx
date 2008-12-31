<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Outcome.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.Outcome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Outcome Item</title>
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
        <td colspan="2" class="style1">Outcome Item </td>
    </tr>
    <tr>
        <td>Outcome type ID:</td>
        <td><asp:TextBox ID="txtOutcomeTypeId" runat="server"></asp:TextBox></td>        
    </tr>
    <tr>
        <td>None profit referral key num:</td>
        <td><asp:TextBox ID="txtNoneprofit" runat="server"></asp:TextBox></td>        
    </tr>
    <tr>
        <td>Ext ref other name:</td>
        <td><asp:TextBox ID="txtExtRefOtherName" runat="server"></asp:TextBox></td>        
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
