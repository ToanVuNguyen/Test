<%@ Page Language="VB" aspcompat=true AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Dim sagencyType
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	Dim objCCRC
    objCCRC = CreateObject("CCRC_Admin.CAdmin")
    
    sagencyType = Request.QueryString("agencyType")

	Dim Rs
    Rs = objCCRC.GetCounselorDirectory(sagencyType, sUserId, sSQL, iReturnCode, sMessage)
    '    Response.Write(sSQL)
'	response.End
	objCCRC = Nothing
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Users by Agency</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main_w.css" type="text/css">
			<link rel="stylesheet" href="/ccrc/css/report.css" type="text/css">
				<script language="javascript">
		function submit_form(action, reporttype) {
			if ("REPORT_LIST" == action) {
				document.all.master.action = "CCRCReports.aspx";
			} 
			else if ("REPORT_CONFIG" == action) {
				document.all.master.action = "ccrc_Report_Config.aspx";
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
			Dim agencyId
			Response.Write("<form id=""master"" method=""post"" action="""">")				
			
			Response.Write("  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""95%"">")
			If Request.QueryString("agencyType") = "1" Then
				Response.Write("    <tr><td colspan=""2"" width=""100%"" class=""HeaderCell"">Counselors by Agency</td></tr>")
			Else
				Response.Write("    <tr><td colspan=""2"" width=""100%"" class=""HeaderCell"">NeighborWorks Counselors Familiar with CCRC (by State/City)</td></tr>")
			End If
			Response.Write("  </table>")
	
			Response.Write("<BR>")
			If Not isValidRS(Rs) Then
				Response.Write("<table>")
				Response.Write("<tr><td>No records found.</tr></td>")
				Response.Write("</table>")
			Else
				Response.Write("<table border=""1"">")
				Rs.MoveFirst
				Do While Not Rs.EOF
					agencyId = Rs.Fields("BSN_ENTITY_SEQ_ID").Value
					Response.Write("<tr><td nowrap class=""ReportRowGroup1"">" & Rs.Fields("BSN_ENTITY_NAME").Value & "</td><td nowrap class=""ReportRowGroup1"">" & Rs.Fields("PHONE").Value & "</td><td></td></tr>")
					Do While Not Rs.EOF 
						If CInt(agencyId) = CInt(Rs.Fields("BSN_ENTITY_SEQ_ID").Value) Then
		                    'Response.Write("<tr><td></td><td nowrap class=""ReportRowGroup1"">" & buildName(Rs.Fields("FIRST_NAME").Value,"",Rs.Fields("LAST_NAME").Value,"") & "<td nowrap class=""ReportRowGroup1"">" & Rs.Fields("PRIMARY_PHN").Value & "</td><td nowrap class=""ReportRowGroup1"">" & Rs.Fields("PRIMARY_EMAIL_ADDR").Value & "</td></tr>")							
		                    Response.Write("<tr><td></td><td nowrap class=""ReportRowGroup1"">" & buildName(Rs.Fields(10).Value, "", Rs.Fields(11).Value, "") & "<td nowrap class=""ReportRowGroup1"">" & Rs.Fields(21).Value & "</td><td nowrap class=""ReportRowGroup1"">" & Rs.Fields(19).Value & "</td></tr>")

						Else
							Exit Do
						End If
						Rs.MoveNext
					Loop
					Response.Write("<tr><td></td><td></td><td></td></tr>")
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
