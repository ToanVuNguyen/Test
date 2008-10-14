<%@ Page Language="VB" aspcompat=true AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
 	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	Dim objCCRC
	objCCRC = CreateObject("CCRC_Admin.CAdmin")

	Dim Rs
	Rs = objCCRC.Get_Servicers(0, sUserId, sSQL, iReturnCode, sMessage)
'	Response.Write(ssql
'	response.End
	objCCRC = Nothing
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Servicer Contact Information</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main_w.css" type="text/css">
			<link rel="stylesheet" href="/ccrc/css/report.css" type="text/css">
				<script language="javascript">
		function submit_form(action, reporttype) {
			if ("REPORT_LIST" == action) {
				document.all.master.action = "CCRCReports.asp";
			} 
			else if ("REPORT_CONFIG" == action) {
				document.all.master.action = "ccrc_Report_Config.asp";
			} 
			else {
				document.all.master.action = "";
			}
			document.all.master.submit();
			return;
		}
				</script>
	</head>
	<body>
		<%
			Response.Write("<form id=""master"" method=""post"" action="""">")				
			
			Response.Write("  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""95%"">")
			Response.Write("    <tr><td colspan=""2"" width=""100%"" class=""HeaderCell"">Servicer Contact Information - for counselor use only, please don't share with homeowners</td></tr>")
			Response.Write("  </table>")
	
			Response.Write( "<BR>")
			If Not isValidRS(Rs) Then
				Response.Write("<table>")
				Response.Write( "<tr><td>No records found.</tr></td>")
				Response.Write("</table>")
			Else
				Response.Write("<table border=""1"">")
				Response.Write( "<TR>")
				Response.Write( "<TD nowrap class=""ReportRowGroup2"">Servicer</TD>")
				Response.Write( "<TD nowrap class=""ReportRowGroup2"">Email</TD>")
				Response.Write( "<TD nowrap class=""ReportRowGroup2"">Fax</TD>")
				Response.Write( "<TD nowrap class=""ReportRowGroup2"">Phone</TD>")
				Response.Write( "<TD nowrap class=""ReportRowGroup2"">Contact</TD>")
				Response.Write( "</TR>")
				Rs.MoveFirst
				Do While Not Rs.EOF
					Response.Write( "<TR>")
					Response.Write( "<TD nowrap class=""ReportRowGroup2"">" & Rs.Fields("BSN_ENTITY_NAME").Value & "</TD>")
					Response.Write( "<TD nowrap class=""ReportRowGroup2"">" & Rs.Fields("CONTACT_EMAIL").Value & "</TD>")
					Response.Write( "<TD nowrap class=""ReportRowGroup2"">" & Rs.Fields("FAX").Value & "</TD>")
					Response.Write( "<TD nowrap class=""ReportRowGroup2"">" & Rs.Fields("PHONE").Value & "</TD>")
					Response.Write( "<TD nowrap class=""ReportRowGroup2"">" & Rs.Fields("CONTACT_NAME").Value & "</TD>")
					Response.Write( "</TR>")
					Rs.MoveNext
				Loop
				Response.Write("</table>")
				Rs.Close
			End If								
			Response.Write("</form>")
		%>
	</body>
</html>
<%
Rs = Nothing
%>
