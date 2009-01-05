<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HPF.FutureState.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
    <link href="Styles/HPF.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td align="center" style="height: 100%" width="100%">
                <img alt="" src="Styles/Images/hpf_logo.gif" 
                    style="width: 199px; height: 41px" /></td>
        </tr>
        <tr>
            <td align="center">
    
        <table style="width:27%; height: 73px; background-color: #60A5DE;" >
            <tr>
                <td align="center" colspan="2" class="baseline">
                    Log In</td>
            </tr>
            <tr>
                <td align="right" class="sidelinks2">
                    User Name:</td>
                <td align="left">
                    <asp:TextBox ID="txt_username" runat="server" CssClass="Text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks2">
                    Password:</td>
                <td align="left">
                    <asp:TextBox ID="txt_password" runat="server" TextMode="Password" 
                        CssClass="Text"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btn_login" runat="server" Text="Log In" class="MyButton" onclick="btn_login_Click" />
                </td>
            </tr>
        </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lb_message" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    </form>
</body>
</html>
