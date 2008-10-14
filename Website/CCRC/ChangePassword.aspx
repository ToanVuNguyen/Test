<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="ChangePassword" %>
<html>
<head>
    <title>Change Password</title>
    <link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
         <table cellpadding=0 cellspacing="0">
				<tr>
					<td>Old Password:</td>
					<td>
						<asp:TextBox id="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="* Required" Display="Dynamic"
							ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
				</tr>


				<tr>
					<td>New Password:</td>
					<td>
						<asp:TextBox id="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
						<asp:RequiredFieldValidator id="ReqPassword" runat="server" ErrorMessage="* Required" Display="Dynamic"
							ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td>Confirm New Password:</td>
					<td>
						<asp:TextBox id="txtPasswordConfirm" runat="server" TextMode="Password"></asp:TextBox>
						<asp:RequiredFieldValidator id="ReqPasswordConfirm" runat="server" ErrorMessage="* Required" Display="Dynamic"
							ControlToValidate="txtPasswordConfirm"></asp:RequiredFieldValidator>
						<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="* Passwords must match" Display="Dynamic"
							ControlToValidate="txtPasswordConfirm" ControlToCompare="txtPassword"></asp:CompareValidator></td>
				</tr>
				
								<tr>
					<td ></td>
					<td>
						<asp:Button id="cmdOk" runat="server" Text="Ok" Font-Size="11px"></asp:Button>&nbsp;
						<asp:Button id="cmdCancel" runat="server" Text="Cancel"  Font-Size="11px" CausesValidation="False"></asp:Button></td>
				</tr>
				<tr>
					<td ></td>
					<td>
						<asp:Label id="lblMsg" runat="server" ForeColor="red"></asp:Label></td>
				</tr>


</table>
     </form>
</body>
</html>
