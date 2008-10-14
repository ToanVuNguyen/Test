<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true CodeFile="counselingSummary.aspx.vb" inherits=referrals_counselingSummary %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/formatting.inc'-->
<!-----INCLUDE virtual='/ccrc/utilities/formParser.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/stringUtilities.inc'-->

<%

    'Response.Cookies("CCRCCookie")("profileRoles") = "SomeRole"
    'Response.Cookies("CCRCCookie")("profileSeqId") = "1"


	'On Error Resume Next

	Server.ScriptTimeout = 1000
	'Response.Write("Before Call1")
	err.clear()
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
	If RedirectIfError("Calling GetAuthorizedUser", eRetPage, eRetBtnText) Then
		'obj = Nothing
		Response.Clear 
		Response.End 
	End If	

	Dim loanId, borrowerName, agencyName, counselorName, servicerName, servicerContactName, servicerContactEmail
	dIM agencyTrackingId, referralType
	Dim referralDate, referralComment, approvedBy
	Dim referralId
	Dim lastName, firstName, miName
	Dim addrLine1, city, state, zipCode
	Dim summaryRptSentDate, phn, bphn, occupants, loanDefaultRsn
	Dim actionItems, followupNotes, successStoryInd, firstContactStatusType
	Dim contactedServicerInd, referralSourceType
	Dim counselingDurationType, mthlyNetIncomeType, firstMortgageType
	Dim creditScore, mothersMaidenName, privacyConsentInd, priorityType
	Dim secondaryContactNumber, emailAddr, bankruptcyInd, bankruptcyAttorneyName, ownerOccupiedInd
	Dim primaryDfltRsnType, secondaryDfltRsnType
	Dim programName, hispanicInd, raceType
	Dim age, genderType, householdType, householdIncome, incomeCategoryType, intakeScoreType, pitiAmt
	Dim propertyForSaleInd, listPriceAmt, realtyCompanyName, fcNoticeRecdInd, armResetInd, incomeEarnersType
	Dim summarySentOtherType, summarySentOtherDate
	
	referralId = Request.QueryString("referralId")
	loanId = Request.QueryString("loanId")
	zipCode = Request.QueryString("zipCode")
    'Response.Write("Before Call")
    
	Dim obj
	obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
	
'	Call obj.UpdateDateCounselingSummarySent(referralId, Date, sUserId, sSQL, iReturnCode, sMessage)
   ' Response.write(Request.Cookies("CCRCCookie")("profileSeqId"))    
	Dim Rs
	'Rs = obj.Get_Referral(loanId, referralId, zipCode, 1, sUserId, sSQL, iReturnCode, sMessage)
	Rs = obj.Get_Referral(loanId, referralId, zipCode, Request.Cookies("CCRCCookie")("profileSeqId"), sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Main.CMain.Get_Referral", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If	
	'Response.Write(rs.RecordCount)
    Dim summaryDate,fromName,fromAddress ,subject
	If isValidRS(Rs) Then
	 '   Response.Write("Hi")
		referralId = Rs.Fields("REFERRAL_SEQ_ID").Value

		borrowerName = buildName(Rs.Fields("FIRST_NAME").Value, Rs.Fields("MI_NAME").Value, Rs.Fields("LAST_NAME").Value, "")
		loanId = Rs.Fields("LOAN_ID").Value
		summaryDate = Now
		approvedBy = Rs.Fields("APPROVED_BY").Value
		servicerName = Rs.Fields("SERVICER").Value
		servicerContactName = Rs.Fields("SERVICER_CONTACT_NAME").Value
		servicerContactEmail = Rs.Fields("SERVICER_CONTACT_EMAIL").Value
		counselorName = buildName(Rs.Fields("COUNSELOR_FIRST_NAME").Value, Rs.Fields("COUNSELOR_MID_INIT_NAME").Value, Rs.Fields("COUNSELOR_LAST_NAME").Value, Rs.Fields("COUNSELOR_NAME_SUFFIX_CODE").Value)
		fromName = buildName(Rs.Fields("COUNSELOR_FIRST_NAME").Value, Rs.Fields("COUNSELOR_MID_INIT_NAME").Value, Rs.Fields("COUNSELOR_LAST_NAME").Value, Rs.Fields("COUNSELOR_NAME_SUFFIX_CODE").Value)
		agencyName = Rs.Fields("BSN_ENTITY_NAME").Value		
		loanDefaultRsn = Rs.Fields("LOAN_DEFAULT_RSN").Value
		actionItems = Rs.Fields("ACTION_ITEMS").Value
		followupNotes = Rs.Fields("FOLLOWUP_NOTES").Value
		fromAddress = Rs.Fields("PRIMARY_EMAIL_ADDR").Value
		If Rs.Fields("PRIMARY_PHN").Value <> "" Then
			phn = "I can be reached at " & Rs.Fields("PRIMARY_PHN").Value & "."
		Else
			phn = ""
		End If
		creditScore = Rs.Fields("credit_score").Value
		mothersMaidenName = Rs.Fields("mothers_maiden_name").Value
		privacyConsentInd = Rs.Fields("privacy_consent_ind").Value
		priorityType = Rs.Fields("priority_type").Value
		secondaryContactNumber = Rs.Fields("secondary_contact_number").Value
		emailAddr = Rs.Fields("email_addr").Value
		bankruptcyInd = Rs.Fields("bankruptcy_ind").Value
		bankruptcyAttorneyName = Rs.Fields("bankruptcy_attorney_name").Value
		ownerOccupiedInd = Rs.Fields("owner_occupied_ind").Value
		programName = Rs.Fields("program_name").Value
		firstMortgageType = Rs.Fields("first_mortgage_type").Value
		mthlyNetIncomeType = Rs.Fields("mthly_Net_Income_Type").Value
		addrLine1 = Rs.Fields("ADDR_LINE_1").Value
		city = Rs.Fields("CITY").Value
		state = Rs.Fields("STATE").Value
		zipCode = Rs.Fields("ZIP").Value
		bphn = Rs.Fields("phn").Value
		agencyTrackingId = Rs.Fields("AGENCY_REFERRAL_ID").Value
		occupants = Rs.Fields("OCCUPANTS").Value
		referralDate = Rs.Fields("REFERRAL_DATE").Value
		referralType = Rs.Fields("REFERRAL_TYPE").Value
		firstContactStatusType = Rs.Fields("first_Contact_Status_Type").Value
		summaryRptSentDate = Rs.Fields("SUMMARY_RPT_SENT_DATE").Value
		contactedServicerInd = Rs.Fields("contacted_servicer").Value
		counselingDurationType = Rs.Fields("counseling_duration_type").Value
		primaryDfltRsnType = Rs.Fields("primary_default_reason").Value
		secondaryDfltRsnType = Rs.Fields("secondary_default_reason").Value
		hispanicInd = Rs.Fields("hispanic_ind").Value
		raceType = Rs.Fields("race_type").Value
		age = Rs.Fields("age").Value
		genderType = Rs.Fields("gender_type").Value
		householdIncome = Rs.Fields("household_income_amt").Value
		householdType = Rs.Fields("household_type").Value
		incomeCategoryType = Rs.Fields("income_category_type").Value
		intakeScoreType = Rs.Fields("intake_score_type").Value
		pitiAmt = Rs.Fields("piti_amt").Value
		propertyForSaleInd = Rs.Fields("PROPERTY_FOR_SALE_IND").Value		
		fcNoticeRecdInd = Rs.Fields("FC_NOTICE_RECD_IND").Value
		listPriceAmt = Rs.Fields("LIST_PRICE_AMT").Value
		realtyCompanyName = Rs.Fields("REALTY_COMPANY_NAME").Value
		armResetInd = Rs.Fields("ARM_RESET_IND").Value
		incomeEarnersType = Rs.Fields("INCOME_EARNERS_TYPE").Value
		summarySentOtherType = Rs.Fields("SUMMARY_SENT_OTHER_TYPE").Value
		summarySentOtherDate = Rs.Fields("SUMMARY_SENT_OTHER_DATE").Value				
	End If	

	subject = "HPF Counseling Summary for loan #" & loanId & ", priority " & priorityType
	
	Dim emailHeader, emailBody, emailBody2, emailBody3
	emailHeader = emailHeader & "<table>" & vbCrLf
	emailHeader = emailHeader & "<tr><td><b>Date of Summary:</b> " & summaryDate & "</td></tr>" & vbCrLf
	emailHeader = emailHeader & "</table>" & vbCrLf
	
	emailBody = emailBody & "<table>" & vbCrLf
	
	emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody = emailBody & "<tr><td><b>Referral Received Date:</b> " & referralDate & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Priority Type:</b> " & priorityType & "</td></tr>" & vbCrLf

	emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody = emailBody & "<tr><td><b>Servicer:</b> " & servicerName & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Account #:</b> " & loanId & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Property for sale?:</b> " & propertyForSaleInd & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>List Price of Home:</b> " & listPriceAmt & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Realty Company Name:</b> " & realtyCompanyName & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Has notice of Foreclosure sale been received?:</b> " & fcNoticeRecdInd & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Borrower Name:</b> " & borrowerName & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Address:</b> " & addrLine1 & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>City/State/Zip:</b> " & city & ", " & state & "&nbsp;" & zipCode & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Phone:</b> " & bphn & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Secondary Contact Number:</b> " & secondaryContactNumber & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Email Address:</b> " & emailAddr & "</td></tr>" & vbCrLf
'	emailBody = emailBody & "<tr><td><b>Hispanic Indicator:</b> " & hispanicInd & "</td></tr>" & vbCrLf
'	emailBody = emailBody & "<tr><td><b>Race:</b> " & raceType & "</td></tr>" & vbCrLf

	emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody = emailBody & "<tr><td><b>Loan Status at First Contact:</b> " & firstContactStatusType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Primary Default Reason Type:</b> " & primaryDfltRsnType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Secondary Default Reason Type:</b> " & secondaryDfltRsnType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>1st Mortgage Type (current rate):</b> " & firstMortgageType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Has interest reset on ARM loan?:</b> " & armResetInd & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Monthly Net Income:</b> " & mthlyNetIncomeType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Household Income:</b> " & householdIncome & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Income Category Type:</b> " & incomeCategoryType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Number of adults contributing to household income:</b> " & incomeEarnersType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Owner Occupied:</b> " & ownerOccupiedInd & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b># of People in House:</b> " & occupants & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Household Type:</b> " & householdType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Bankruptcy?:</b> " & bankruptcyInd & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>bankruptcy Attorney:</b> " & bankruptcyAttorneyName & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Credit Score:</b> " & creditScore & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>Intake Score Type:</b> " & intakeScoreType & "</td></tr>" & vbCrLf
	emailBody = emailBody & "<tr><td><b>PITI at Intake:</b> " & pitiAmt & "</td></tr>" & vbCrLf
	
	emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody = emailBody & "</table>" & vbCrLf
	Dim referralResultType
	Dim i
	i = 1
	Rs = obj.Get_Referral_Results(referralId, sUserId, sSQL, iReturnCode, sMessage)

	emailBody = emailBody & "<table border='1' cellpadding='1' cellspacing='1'>" & vbCrLf
	If IsValidRS(Rs) Then
'		emailBody = emailBody & "<tr><td colspan='2'><table style='position:relative;left:100;' border='1' cellpadding='1' cellspacing='1'>"
		emailBody = emailBody & "<tr><th>Result Type</th><th>Date</th></tr>" & vbCrLf
		While Not Rs.eof
			emailBody = emailBody & "<tr><td>" & Rs.Fields("DSCR").Value & "</td><td>" & Rs.Fields("REFERRAL_RESULT_DATE").Value & "</td></tr>" & vbCrLf
			i = i + 1
			Rs.movenext
		end While
		Rs.close
	Else
		emailBody = emailBody & "<tr><td><b>Referral Result:</b></td></tr>" & vbCrLf
		emailBody = emailBody & "<tr><td><b>Result Date:</b></td></tr>" & vbCrLf
	End If
	emailBody = emailBody & "</table>" & vbCrLf
	Rs = nothing
	
	emailBody2 = emailBody2 & "<table width='1200px'>" & vbCrLf
	emailBody2 = emailBody2 & "<tr><td><b>Presenting issues/Main reason(s) for default:</b></td></tr>" & vbCrLf
	emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(loanDefaultRsn,chr(34)) & "</td></tr><br>" & vbCrLf
	
	emailBody2 = emailBody2 & "<tr><td><b>Recommended action items:</b></td></tr>" & vbCrLf
	emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(actionItems,chr(34)) & "</td></tr><br>" & vbCrLf
	
	emailBody2 = emailBody2 & "<tr><td><b>Follow up notes after initial counseling:</b></td></tr>" & vbCrLf
	emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(followupNotes,chr(34)) & "</td></tr><br>" & vbCrLf

	emailBody2 = emailBody2 & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody2 = emailBody2 & "<tr><td><b>Agency:</b> " & agencyName & "</td></tr>" & vbCrLf
	emailBody2 = emailBody2 & "<tr><td><b>Agency Tracking Id:</b> " & agencyTrackingId & "</td></tr>" & vbCrLf
	emailBody2 = emailBody2 & "<tr><td><b>Privacy consent agree to?:</b> " & privacyConsentInd & "</td></tr>" & vbCrLf

	emailBody2 = emailBody2 & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody2 = emailBody2 & "<tr><td><b>Summary report sent by alternate method:</b> " & summarySentOtherType & "</td></tr>" & vbCrLf
	emailBody2 = emailBody2 & "<tr><td><b>Summary report sent by alternate method date:</b> " & summarySentOtherDate & "</td></tr>" & vbCrLf	
	emailBody2 = emailBody2 & "</table>" & vbCrLf

	emailBody2 = emailBody2 & "<BR>" & vbCrLf
	
	' BUDGET
	Dim Rs3
	Rs3 = obj.Get_Budget_Items(referralId, sUserId, sSQL, iReturnCode, sMessage)
	
	Dim budgetCategory, amt, eTotal, yTotal, subtotal, total
	subtotal = 0
	eTotal = 0
	yTotal = 0
	total = 0
	
	emailBody3 = "<table border='1'>"
	emailBody3 = emailBody3 & "<tr><td><b>Budget</b></td>"
	If isValidRS(Rs3) Then
		i = 0
		Rs3.MoveFirst
		budgetCategory = Rs3.Fields("BUDGET_CATEGORY").Value
		Do While Not Rs3.EOF				
			i = CInt(i) + 1
			If CInt(i) = 1 Then
				emailBody3 = emailBody3 & "<tr><td><b>" & Rs3.Fields("BUDGET_CATEGORY").Value & "</b></td>"
			End If
			If budgetCategory = Rs3.Fields("BUDGET_CATEGORY").Value Then
				If  Rs3.Fields("AMT").Value Is Nothing Then
					amt = 0
				Else
					amt = Rs3.Fields("AMT").Value
				End If
				subtotal = CDbl(subtotal) + CDbl(amt)
				PrintDetailRow (i, Rs3.Fields("BUDGET_CATEGORY_TYPE_CODE").Value, Rs3.Fields("BUDGET_CATEGORY").Value, Rs3.Fields("BUDGET_SUBCATEGORY_SEQ_ID").Value, Rs3.Fields("BUDGET_SUBCATEGORY").Value, Rs3.Fields("AMT").Value, total, eTotal, yTotal, emailBody3)
				Rs3.MoveNext
			Else
				PrintSubTotalRow (subtotal, emailBody3)
				budgetCategory = Rs3.Fields("BUDGET_CATEGORY").Value
				subtotal = 0
				i = 0
			End If
		Loop
		PrintSubTotalRow (subtotal, emailBody3)
		PrintTotalRow (total, eTotal, yTotal, emailBody3)
	End If
	Rs3.Close
	Rs3 = Nothing
	emailBody3 = emailBody3 & "</table>"
	emailBody3 = emailBody3 & "<BR>"
	
	emailBody3 = emailBody3 & "<table>" & vbCrLf
	emailBody3 = emailBody3 & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody3 = emailBody3 & "<tr><td><b>Counselor:</b> " & counselorName & "</td></tr>" & vbCrLf
	emailBody3 = emailBody3 & "<tr><td><b>Email:</b> " & fromAddress & "</td></tr>" & vbCrLf
	emailBody3 = emailBody3 & "<tr><td><b>Phone:</b> " & phn & "</td></tr>" & vbCrLf

	emailBody3 = emailBody3 & "<tr><td height='10'></td></tr>" & vbCrLf	
	emailBody3 = emailBody3 & "</table>" & vbCrLf	
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Referral</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<script language="JavaScript">
				var bSubmitForm = false;

				function unLoad() {
					if(false == bSubmitForm) {
						event.returnValue = "Please email Conseling Summary before leaving this page.";
					}
				}
				
				function submitForm() {
					if (bSubmitForm) 
						return true;
					else
						bSubmitForm = true;
					return false;					
				}
								
			</script>
		
	</head>
	<body onbeforeunload="javascript:unLoad();">
		<FORM ACTION="../utilities/emailHelper.aspx" ENCTYPE="multipart/form-data" METHOD="POST" ID="Form1">
			<INPUT TYPE="text" style="display:none" NAME="referralId" ID="referralId" VALUE="<%=referralId%>">
			<INPUT TYPE="text" style="display:none" NAME="emailHeader" ID="emailHeader" VALUE="<%=emailHeader%>">
			<INPUT TYPE="text" style="display:none" NAME="emailBody" ID="emailBody" VALUE="<%=emailBody%>">
			<INPUT TYPE="text" style="display:none" NAME="emailBody2" ID="emailBody2" VALUE="<%=emailBody2%>">
			<INPUT TYPE="text" style="display:none" NAME="emailBody3" ID="emailBody3" VALUE="<%=emailBody3%>">
			<INPUT TYPE="text" style="display:none" NAME="subject" ID="subject" VALUE="<%=subject%>">
			<INPUT TYPE="text" style="display:none" NAME="firstContactStatusType" ID="firstContactStatusType" VALUE="<%=firstContactStatusType%>">

				<%Response.Write (emailHeader & emailBody & emailBody2 & emailBody3)%>

			<table border="0" class="pageBody" ID="Table2" width="600">				
				<tr><td><b>To Name:</b></td><td> <input type="text" id="sendToName" name="sendToName" value="<%=servicerName%>" datatype="anytext"></td></tr>
				<tr><td><b>To Email:</b></td><td> <input type="text" id="sendToAddress" name="sendToAddress" value="<%=servicerContactEmail%>" datatype="anytext"></td></tr>
				<tr><td><b>From Name:</b></td><td> <input type="text" id="fromName" name="fromName" value="<%=fromName%>" datatype="anytext"></td></tr>
				<tr><td><b>From Email:</b></td><td> <input type="text" id="fromAddress" name="fromAddress" value="<%=fromAddress%>" datatype="anytext"></td></tr>
<!--				<tr><td><b>Subject:</b></td><td> <input type="text" id="Subject" name="Subject" value="<%=subject%>" datatype="anytext"></td></tr> -->
			</table>

			<table border="0" class="pageBody" ID="Table3" width="600">
				<tr><td><b>Comment:</b></td></tr>
				<tr><td><textarea id="emailComment" cols="66" rows="5" NAME="emailComment"><%=phn%></textarea></td></tr>
				<tr><td><b>Attachment 1:</b></td></tr>
				<tr><td><INPUT TYPE="FILE" NAME="FILE1" ID="FILE1"></td></tr>
				<tr><td><b>Attachment 2:</b></td></tr>
				<tr><td><INPUT TYPE="FILE" NAME="FILE2" ID="FILE2"></td></tr>
				<tr><td><b>Attachment 3:</b></td></tr>
				<tr><td><INPUT TYPE="FILE" NAME="FILE3" ID="File3"></td></tr>
				<tr><td><input type="submit" value="Send" onclick="submitForm();"></td></tr>
			</table>
		</FORM>
	</body>
</html>
<%
	obj = Nothing
	if not rs Is nothing
	    
	    Rs.Close
	End if
	Rs = Nothing






%>
