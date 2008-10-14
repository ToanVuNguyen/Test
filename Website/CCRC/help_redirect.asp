<%@ Language=VBScript %>
<%
Sub Main
On Error Resume Next

	helpForPage = Request.Cookies("CCRCCookie")("helpForPage")
	entitySeqId = Request.Cookies("CCRCCookie")("entitySeqId")
		
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>CCRC Help</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
		<script language='javascript' src='/ccrc/ShowHelp.js'></script>
		<script language='javascript'>
		<!--
			function ShowHelpPage() {
				ShowHelp(document.all.entitySeqId.value, document.all.helpForPage.value);
//				location.replace
			}
		//-->
		</script>
	</head>
	<body onload="javascript:ShowHelpPage();">
		<form>
			<INPUT TYPE=hidden VALUE="<%=helpForPage%>" NAME="helpForPage" ID="helpForPage">
			<INPUT TYPE=hidden VALUE="<%=entitySeqId%>" NAME="entitySeqId" ID="entitySeqId">
		</form>
	</body>
</html>
<%
End Sub

Main
%>
