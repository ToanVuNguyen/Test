<%@ Page Language="VB" aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%


        On Error Resume Next
	
        Dim formMode = ""
        formMode = Request.QueryString("formMode")
        hidformMode.value = formMode
        Button1.Value = formMode
 %>
 
 <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Referral Search</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<script language="JavaScript">
		function onLoad() {
			document.all.loanId.focus();
		}
		
		function referral() {
			location.href = "referral.aspx?formMode=" + document.all.hidformMode.value + "&loanId=" + document.all.loanId.value + "&zipCode=" + document.all.zipCode.value;
		}
		</script>
	</head>
	<body onload="onLoad()">
		<br>
		<br>
		<h1>Referrals</h1>
		<form runat="server">
			<input type="hidden" id="hidformMode" runat="server"  name="hidformMode" >
			<table class="pageBody">
				<tr>
					<td class="label" title="">Account #:</td>
					<td>
						<input type="text" id="loanId" name="loanId" value="" maxlength="25">
					</td>
				</tr>
				<tr>
					<td class="label" title="">Property Zip Code:</td>
					<td>
						<input type="text" id="zipCode" name="zipCode" value="" maxlength="5">
					</td>
				</tr>
				<tr>
				<tr>
					<td></td>
					<td><input type="button" runat="server"  onclick="javascript:referral();" ID="Button1" NAME="Button1"></td>
				</tr>				
			</table>
		</form>
	</body>
</html>


