<%@ Language=VBScript %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<html>
<head>

<script language="JavaScript" src="/ccrc/utilities/global.js"></script>

<script language='JavaScript'>
function onSystemExit() {
	findAppFrame().data.location.href = "splash.asp?exitCode=<%=Request.QueryString("exitCode")%>";
	return;
}
</script>	
</head>
<body onLoad='onSystemExit();'>
</body>
</html>