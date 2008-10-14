<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Welcome to CCRC</title>
    <link rel="stylesheet" href="css/main.css" type="text/css">
</head>
<body>
   <form id="Form1" runat="server">

       <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
       <tr>
				<td colspan="2" align="left" valign="top"><img src="images/banner_topborder.gif" width="100%" height="12"></td>
			</tr>
			<tr>
				<td  colspan="2" align="left" valign="absmiddle">
					<p><img src="/images/banner.jpg"></p>			
				</td>
			</tr>
			<tr>
				<td colspan="2"  align="left" valign="top"><img src="images/banner_bottomborder.gif" width="100%" height="9"></td>
			</tr>
           <tr>
               <td style="width: 96px">
                   User Name:</td>
               <td>
                   <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUser"
                       Display="Dynamic" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                   
               </td>
           </tr>
           <tr>
               <td style="width: 96px">
                   Password:</td>
               <td>
                   <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                       Display="Dynamic" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                   
               </td>
           </tr>
           <tr>
               <td style="width: 96px">
               </td>
               <td>
                   <asp:Button ID="cmdLogin" runat="server"  Text="Login" /></td>
           </tr>
				<TR>
					<TD style="WIDTH: 118px"></TD>
					<TD>
						<asp:Label id="lblMsg" runat="server" ForeColor="red"></asp:Label></TD>
				</TR>
       </table>

 
</form>

 

</body>

</html>

 


