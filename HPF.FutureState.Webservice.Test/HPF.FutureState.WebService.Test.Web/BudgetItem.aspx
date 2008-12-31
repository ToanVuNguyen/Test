<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetItem.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.BudgetItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Budget Item</title>
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
        <td colspan="2" class="style1">Budget Item </td>
    </tr>
    <tr>
        <td>Budget subcategory ID:</td>
        <td><asp:TextBox ID="txtbudgetSubcategoryId" runat="server"></asp:TextBox></td>        
    </tr>
    <tr>
        <td>Budget item amt:</td>
        <td><asp:TextBox ID="txtBudgetItemAmt" runat="server"></asp:TextBox></td>        
    </tr>
    <tr>
        <td>Budget note:</td>
        <td><asp:TextBox ID="txtBudgetNote" runat="server"></asp:TextBox></td>        
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
