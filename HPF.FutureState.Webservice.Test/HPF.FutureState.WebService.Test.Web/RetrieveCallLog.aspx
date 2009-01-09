<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetrieveCallLog.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.RetrieveCallLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    
    <tr>
        <td>
            <asp:Label ID="Label28" runat="server" Text="Username"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" Text="admin" Width="128px"></asp:TextBox>
        </td>
        
        <td>
            <asp:Label ID="Label29" runat="server" Text="Password"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPassword" runat="Server" Text="admin" Width="128px"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
    <td>
    Call Log ID: 
    </td>
    <td>
    <asp:TextBox ID="txt_CallLogId" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td></td>
    <td><asp:Button ID="btn_Submit" runat="server" Text="Submit" 
            onclick="btn_Submit_Click" /></td>
    </tr>
    <tr>
        <td>Status: </td>
        <td><asp:Label ID="lbl_Status" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Message: </td>
        <td><asp:Label ID="lbl_Message" runat="server" Text=""></asp:Label></td>
    </tr>
    </table>
        <asp:GridView ID="gv_results" runat="server">
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server">
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
