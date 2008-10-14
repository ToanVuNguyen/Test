<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Referral</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<SCRIPT LANGUAGE="JavaScript" SRC="/ccrc/utilities/calendar.js"></SCRIPT>
			<script language="javascript">
			function submitForm() {
				var frm = document.myform;
				document.myform.submit();
				return true;
			}
			
			function cancelForm() {
				location.replace("/home.aspx");
			}
			
			function mySetDateField(myDateField) {
				if('invoiceDate' == myDateField)
					setDateField(document.myform.invoiceDate);
			}					
			</script>
	</head>
	<body>
		<FORM name="myform" id="myform" method="post" action="InvoiceSessions_process.aspx">
			<table border="0" class="pageBody" ID="Table2" width="600">
				<tr>
					<td><font class="label" title="">Accounting Period (YYYYMM):</font></td>
					<td>
						<input maxlength="6" type="text" id="acctPrd" name="acctPrd" required="true" datatype="anytext"
							errorlbl="Loan Id" errormsg="Loan id is required.">
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Invoice Date:</font></td>
					<td>
						<input type="text" id="invoiceDate" name="invoiceDate" datatype="date" errorlbl="Invoice Date"
							errormsg="Invoice date is invalid.">
						<%
							Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""mySetDateField('invoiceDate'); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
							Response.Write ("(mm/dd/yyyy)")
						%>
					</td>
				</tr>
			</table>
			<table ID="Table1">
				<tr>
					<td>
						<input name="save" id="save" value="Save" class="btn" type="button" accesskey="s" onclick="submitForm();"
							title="Save the changed information and continue">
					</td>
					<td>
						<input name="cancel" id="cancel" value="Cancel" class="btn" type="button" accesskey="s"
							onclick="cancelForm();" title="Cancel all changes">
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
