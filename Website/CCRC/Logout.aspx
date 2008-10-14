<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Logout.aspx.vb" Inherits="Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<table width="100%">
<tr>
				<td colspan="2" align="left" valign="top"><img src="images/banner_topborder.gif" width="100%" height="12"></td>
			</tr>
			<tr>
				<td  colspan="2" align="left" valign="absmiddle">
					<p><img src="/CCRC/images/banner.jpg"></p>			
				</td>
			</tr>
			<tr>
				<td colspan="2"  align="left" valign="top"><img src="images/banner_bottomborder.gif" width="100%" height="9"></td>
			</tr>
				<TR>
					<TD style="WIDTH: 118px"></TD>
					<TD>
						<asp:Label id="lblMsg" runat="server" ForeColor="red"> </asp:Label></TD>
				</TR>

				<TR>
					<TD style="WIDTH: 118px"></TD>
					<TD>
						<a href="login.aspx" target="_parent">Login again </a>
				</TR>

</table>    
    </div>
    </form>
</body>
</html>
