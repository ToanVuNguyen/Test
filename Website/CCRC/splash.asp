<%@ Language=VBScript %>
<!DOCTYPE HTML PUBLIC "-//IETF//DTD HTML//EN">
<HTML>
	<HEAD>
		<title>Welcome to CCRC</title> 
		<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
		<!--#INCLUDE virtual='/ccrc/utilities/cookies_utils.inc'-->
		<%	
Sub Main

	On Error Resume Next
	
	Dim exitCode
	exitCode = Request.QueryString("exitCode")

	If exitCode = "1000" Or exitCode = "5" Then
		abandonSession 'clears all values in cookie
	End If
%>
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<script language="JavaScript" src="global.js"></script>
			<script language="JavaScript">
<!--//standard functions

var loaded = false;
self.status = 'Please wait. Loading page...';

function onLoad() {

	loaded = true;
	self.status = '';

	var exitCode
	exitCode = parseInt(<%=exitCode%>);
	
	switch(exitCode) {
		case 1:		//timeout or access attempt not thru front door
			msgText = "Your session has timed out or you have attempted to enter CCRC incorrectly.\nPlease click Login on the left menu.";
			break;
		case 2:
			msgText = "Your user account is not setup as a valid CCRC user.";
			break;
		case 3:
			msgText = "Your user account is currently in an inactive status.";
			break;
		case 4:
			msgText = "Your company does not currently have access.";
			break;
		case 6:
			msgText = "Error - Insufficient access rights.";
			break;
		case 7: 
			msgText = "Error - Invalid access right code.";
			break;
		case 70: 
			msgText = "Error - You are not a member of a CCRC user group.";
			break;
		case 99:
			msgText = "Error - Unable to validate your user account.";
			break;
		case 999:
			msgText = "Error - invalid navigation";
			break;
		default:
			msgText = "";
	}
	
	if ("" != msgText)
		alert(msgText);
}

-->
			</script>
	</HEAD>
	<body class="static" onLoad="onLoad();">
		<form name="master" id="master">
			<table>
				<tr>
					<td height="242">
						<img src="/ccrc/images/family1.jpg" id="IMG1" height="237" width="188"><img src="/ccrc/images/family2.jpg"><img src="/ccrc/images/WomanAndChild.jpg">
					</td>
				</tr>
				<tr>
					<td><br>
						Version 2.2<br>
						Date: 07/01/2005<br>
						<br>
						Best viewed at 800 x 600
					</td>
				</tr>
				<tr>
					<td><br>
						<br>
						<br>
						<br>
					</td>
				</tr>
				<tr>
					<td width="100%">
						<table ID="Table1">
							<tr>
								<td style="left:100px;" align="left" valign="middle" width="100%" height="100%"><img src="/ccrc/images/Final Logo 2-color.jpg"></td>
								<td style="left:175px;" align="left" valign="middle" width="100%" height="100%"><img src="/ccrc/images/ccrc.jpg"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<%
End Sub

Main
%>
	</body>
</HTML>
