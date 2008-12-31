<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetAsset.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.BudgetAsset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Budget Asset</title>
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
        <td colspan="2" class="style1">Budget Asset</td>
    </tr>
    <tr>
        <td>Asset name:</td>
        <td><asp:TextBox ID="txtAssetName" runat="server"></asp:TextBox></td>        
    </tr>
    <tr>B
        <td>Asset value:</td>
        <td><asp:TextBox ID="txtAssetValue" runat="server"></asp:TextBox></td>        
    </tr>   
    </table>
    </div>
    </form>
</body>
</html>
