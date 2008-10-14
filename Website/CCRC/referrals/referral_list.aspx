<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true CodeFile="referral_list.aspx.vb" inherits=referrals_referral_list %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->


<%

On Error Resume Next
    err.clear
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
	
	Dim programIds, agencyIds, servicerIds, locationIds, hasContract, actionPlan
	Dim acctPrd1, acctPrd2, startDate, endDate, reportType, outcomeCode, outcomeTypeDscr

	programIds = Request.QueryString("programIds")
	agencyIds = Request.QueryString("agencyIds")
	servicerIds = Request.QueryString("servicerIds")
	locationIds = Request.QueryString("locationIds")
	acctPrd1 = Request.QueryString("acctPrd1")
	acctPrd2 = Request.QueryString("acctPrd2")
	startDate = Request.QueryString("startDate")
	endDate = Request.QueryString("endDate")
	reportType = Request.QueryString("reportType")	
	outcomeCode = Request.QueryString("outcomeCde")	
	hasContract = Request.QueryString("hasContract")	
	actionPlan = Request.QueryString("actionPlan")	



	'Server.ScriptTimeout = 1000 
	
	Dim obj
	obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating object CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If			
	
	Dim Rs
	
	Rs = obj.Get_Referral_List(programIds, agencyIds, servicerIds, _
					locationIds, acctPrd1, acctPrd2, startDate, endDate, reportType, _
					outcomeCode, hasContract, actionPlan, _
					Request.Cookies("CCRCCookie")("profileSeqId"), _
					sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Main.CMain.Get_Referral_Activity", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If			
	If isValidRs(Rs) Then
		outcomeTypeDscr = Rs.Fields("REFERRAL_RESULT").Value
	End If
'	response.Write sSQL
'	
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Search Results</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
	</head>
	<body>		
		<% 
		response.Write ("<h1>" & " " & outcomeTypeDscr & ": "		)
		If Len(acctPrd1) > 0 Then
			response.Write (acctPrd1 & " - " & acctPrd2)
		Else
			response.Write( startDate & " - " & endDate)
		End If
		response.Write ("</h1>")
		response.Write ("<table border=""1"" cellspacing=""1"" cellpadding=""1"" width=""75%"">")
		Response.Write ("<tr><td>Account Id</td><td>Zip</td><td>Agency Tracking Id</td><td>Borrower</td><td>Address</td><td>Agency</td><td>Counselor</td><td>Outcome Date</td><td>Outcome</td><td>Action</td></tr>")
		If Not isValidRS(Rs) Then
			Response.Write ("<tr><td>No records found.</tr></td>")
		Else
			Rs.MoveFirst
			Do While Not Rs.EOF
				Call WriteRecord(Rs)
				Rs.MoveNext 
			Loop
			Rs.Close
			Rs = Nothing
		End If
		Response.Write ("</table></body></html>")



%>
</body>
</html>