<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testNewCounselingSummary.aspx.vb" Inherits="referrals_NewCounselingSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
Testing
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <table border="0" class="pageBody" ID="Table2" width="600">				
				<tr><td><b>To Name:</b></td><td> <asp:TextBox ID="sendToName"  runat="server"></asp:TextBox></td></tr>
				<tr><td><b>To Email:</b></td><td> <asp:TextBox ID="sendToAddress"   runat="server"></asp:TextBox></td></tr>
				<tr><td><b>From Name:</b></td><td> <asp:TextBox ID="TxtfromName" runat="server"></asp:TextBox></td></tr>
				<tr><td><b>From Email:</b></td><td> <asp:TextBox ID="TxtfromAddress"    runat="server"></asp:TextBox></td></tr>
				<tr><td><b> </b></td><td> <asp:TextBox ID="txtSubject" Visible="false" runat="server"></asp:TextBox></td></tr>
			</table>
     <table border="0" class="pageBody" ID="Table3" width="600">
				<tr><td><b>Comment:</b></td></tr>
				<tr><td><textarea id="emailComment" cols="66" rows="5" runat="server" NAME="emailComment"></textarea></td></tr>
				<tr><td><b>Attachment 1:</b></td></tr>
				<tr><td> 
                    <asp:FileUpload ID="FileUpload1" runat="server" /></td></tr>
				<tr><td><b>Attachment 2:</b></td></tr>
				<tr><td> 
                    <asp:FileUpload ID="FileUpload2" runat="server" /></td></tr>
				<tr><td><b>Attachment 3:</b></td></tr>
				<tr><td> 
                    <asp:FileUpload ID="FileUpload3" runat="server" /></td></tr>
				<tr><td> 
                    <asp:Button ID="Button1" runat="server" Text="Send" /></td></tr>
			</table>
    </form>
</body>
</html>
