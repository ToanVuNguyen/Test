<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Counseling Summary Auto Email</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<script language="JavaScript">
		function Go() {
			document.myform.submit();
		}
		</script>
	</head>
	<body>
		<br>
		<br>
		<h1>Counseling Summary Auto Email</h1>
		<FORM name="myform" id="myform" method="post" action="TestcounselingSummaryAutoEmail.aspx">
			<table class="pageBody">
				<tr>
					<TD>Accounting Period (YYYYMM):</TD>
					<TD><input type="text" id="ACCT_PRD" name="ACCT_PRD" datatype="anytext"></TD>				
				</tr>
				<tr>
					<td><input type="button" value="Go" onclick="javascript:Go();" ID="Button1" NAME="Button1"></td>
				</tr>				
			</table>
		</form>
	</body>
</html>
