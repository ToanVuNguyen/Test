<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->


<%
'Response.Cookies("CCRCCookie")("profileSeqId") = "1"
On Error Resume Next

    'Server.ScriptTimeout = 1000
	
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
	If RedirectIfError("Calling GetAuthorizedUser", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If	

	Dim loanId, borrowerName, agencyName, counselorName, agencyTrackingId, referralType
	Dim referralDate, referralComment, approvedBy, programName, servicer
	Dim formMode, referralId, disable, entityId
	Dim lastName, firstName, miName
	Dim addrLine1, city, state, zipCode, agencyId
	Dim internal, action, userRoles
	Dim RsData, servicers, locations
	Dim summaryRptSentDate, phn, occupants, loanDefaultRsn
	Dim actionItems, followupNotes, successStoryInd, firstContactStatusType
	Dim contactedServicerInd, referralSourceType
	Dim counselingDurationType, mthlyNetIncomeType, firstMortgageType
	Dim creditScore, mothersMaidenName, privacyConsentInd, priorityType
	Dim secondaryContactNumber, emailAddr, bankruptcyInd, bankruptcyAttorneyName, ownerOccupiedInd
	Dim primaryDfltRsnType, secondaryDfltRsnType
	Dim hispanicInd, raceType, mthlyExpenseType
	Dim age, genderType, householdType, householdIncome, incomeCategoryType, intakeScoreType, pitiAmt
	Dim propertyForSaleInd, listPriceAmt, realtyCompanyName, fcNoticeRecdInd, armResetInd, incomeEarnersType
    Dim summarySentOtherType, summarySentOtherDate
    Dim discussedSolutionWithSrvcr
    Dim servicer2,secondMortgageType,varloanId2,secondLoanStatusTypeCode, pitiAmt2
    Dim hUDOutcome, hUDTermiReason, referralHUDTermdate, workedWithAnotherAgencyInd

	loanId = Request.QueryString("loanId")
	zipCode = Request.QueryString("zipCode")	

	formMode = Request.QueryString("formMode")	

    Dim sessionRatingCode

	If formMode = "ADD" Then
		referralId = 0
	Else
		referralId = Request.QueryString("referralId")
	End If	
	
	Dim obj
	obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("XXX Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
    
	Dim Rs
	Rs = obj.Get_Referral(loanId, referralId, zipCode, Request.Cookies("CCRCCookie")("profileSeqId"), sUserId, sSQL, iReturnCode, sMessage)
'response.Write ssql
'response.end
	If RedirectIfError("Calling CCRC_Main.CMain.Get_Referral", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If	
	If isValidRS(Rs) Then
		loanId = Rs.Fields("LOAN_ID").Value
		
		programName = Rs.Fields("program_name").Value
        borrowerName = buildName(Rs.Fields("LAST_NAME").Value, Rs.Fields("FIRST_NAME").Value, Rs.Fields("MI_NAME").Value, "")
		lastName = Rs.Fields("LAST_NAME").Value
		agencyId = Rs.Fields("AGENCY_SEQ_ID").Value
		firstName = Rs.Fields("FIRST_NAME").Value
		miName = Rs.Fields("MI_NAME").Value
		addrLine1 = Rs.Fields("ADDR_LINE_1").Value
		city = Rs.Fields("CITY").Value
		state = Rs.Fields("STATE").Value
		zipCode = Rs.Fields("ZIP").Value
		agencyName = Rs.Fields("BSN_ENTITY_NAME").Value
		counselorName = buildName(Rs.Fields("COUNSELOR_FIRST_NAME").Value, Rs.Fields("COUNSELOR_MID_INIT_NAME").Value, Rs.Fields("COUNSELOR_LAST_NAME").Value, Rs.Fields("COUNSELOR_NAME_SUFFIX_CODE").Value)
		servicer = Rs.Fields("SERVICER").Value
		agencyTrackingId = Rs.Fields("AGENCY_REFERRAL_ID").Value
		referralType = Rs.Fields("REFERRAL_TYPE").Value
		If (Rs.Fields("REFERRAL_DATE").Value Is Nothing) Or Len(Rs.Fields("REFERRAL_DATE").Value) = 0 Then
			referralDate = Now()
		Else
			referralDate = Rs.Fields("REFERRAL_DATE").Value
		End If
		referralDate = Month(referralDate) & "/" & Day(referralDate) & "/" & Year(referralDate)
		referralComment = Rs.Fields("GENERAL_COMMENT").Value
		approvedBy = Rs.Fields("APPROVED_BY").Value
		summaryRptSentDate = Rs.Fields("SUMMARY_RPT_SENT_DATE").Value
		phn = Rs.Fields("PHN").Value
		occupants = Rs.Fields("OCCUPANTS").Value
		loanDefaultRsn = Rs.Fields("LOAN_DEFAULT_RSN").Value
		actionItems = Rs.Fields("ACTION_ITEMS").Value
		followupNotes = Rs.Fields("FOLLOWUP_NOTES").Value
		successStoryInd = Rs.Fields("SUCCESS_STORY_IND").Value
		firstContactStatusType = Rs.Fields("FIRST_CONTACT_STATUS_TYPE").Value
		referralSourceType = Rs.Fields("REFERRAL_SOURCE_TYPE").Value
		contactedServicerInd = Rs.Fields("CONTACTED_SERVICER_IND").Value
		counselingDurationType = Rs.Fields("COUNSELING_DURATION_TYPE").Value
		mthlyExpenseType = Rs.Fields("MTHLY_EXPENSE_TYPE").Value
		mthlyNetIncomeType = Rs.Fields("MTHLY_NET_INCOME_TYPE").Value
		firstMortgageType = Rs.Fields("FIRST_MORTGAGE_TYPE").Value
		creditScore = Rs.Fields("credit_score").Value
		mothersMaidenName = Rs.Fields("mothers_maiden_name").Value
		privacyConsentInd = Rs.Fields("privacy_consent_ind").Value
		priorityType = Rs.Fields("priority_type").Value
		secondaryContactNumber = Rs.Fields("secondary_contact_number").Value
		emailAddr = Rs.Fields("email_addr").Value
		bankruptcyInd = Rs.Fields("bankruptcy_ind").Value
		ownerOccupiedInd = Rs.Fields("owner_occupied_ind").Value
		bankruptcyAttorneyName = Rs.Fields("bankruptcy_attorney_name").Value
		primaryDfltRsnType = Rs.Fields("primary_default_reason").Value
		secondaryDfltRsnType = Rs.Fields("secondary_default_reason").Value
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
        sessionRatingCode = Rs.Fields("SESSION_RATING_TYPE_CODE").Value
        discussedSolutionWithSrvcr = Rs.Fields("DISCUSSED_SOLUTION_WITH_SRVCR_IND").Value
        
        servicer2 = Rs.Fields("second_servicer_name").Value
        secondMortgageType = Rs.Fields("second_mortgage_type").Value
        varloanId2 = Rs.Fields("SECOND_LOAN_ID").Value
        secondLoanStatusTypeCode = Rs.Fields("second_loan_status_type").Value
        pitiAmt2 = Rs.Fields("SECOND_PITI_AMT").Value

        hUDOutcome = Rs.Fields("hud_outcome_type").Value
        hUDTermiReason = Rs.Fields("hud_term_reason_type").Value
        referralHUDTermdate = Rs.Fields("HUD_TERM_DATE").Value
        
        workedWithAnotherAgencyInd = Rs.Fields("WORKED_WITH_ANOTHER_AGENCY_IND").Value
	End If

	If formMode <> "ADD" And referralId & "" = "" Then
		Response.Write ("Referral not found.")
		Response.End			
	End If		
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Referral</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
	</head>
	<body>
		<FORM name="myform" id="myform">
			<INPUT TYPE=hidden VALUE="<%=formMode%>" NAME="formMode" ID="formMode"> 
			<INPUT TYPE=hidden VALUE="<%=userRoles%>" NAME="userRoles" ID="userRoles">
			<INPUT TYPE=hidden VALUE="<%=referralId%>" NAME="referralId" ID="referralId">
			<INPUT TYPE=hidden VALUE="<%=internal%>" NAME="internal" ID="internal">
			<INPUT TYPE=hidden NAME="servicers" ID="servicers" VALUE="<%=servicers%>">
			<INPUT TYPE=hidden NAME="locations" ID="locations" VALUE="<%=locations%>">
			<INPUT TYPE=hidden VALUE="<%=sessionRatingCode%>" NAME="hiddenSessionRating" ID="hiddenSessionRating">
			<%If internal Then%>
				<INPUT TYPE=hidden NAME="loanId" ID="loanId" VALUE="<%=loanId%>">
				<INPUT TYPE=hidden NAME="lastName" ID="lastName" VALUE="<%=lastName%>">
				<INPUT TYPE=hidden NAME="firstName" ID="firstName" VALUE="<%=firstName%>">
				<INPUT TYPE=hidden NAME="miName" ID="miName" VALUE="<%=miName%>">
				<INPUT TYPE=hidden NAME="addrLine1" ID="addrLine1" VALUE="<%=addrLine1%>">
				<INPUT TYPE=hidden NAME="city" ID="city" VALUE="<%=city%>">
				<INPUT TYPE=hidden NAME="state" ID="state" VALUE="<%=state%>">
				<INPUT TYPE=hidden NAME="zipCode" ID="zipCode" VALUE="<%=zipCode%>">
			<%End If%>

	<table border="0" class="pageBody" ID="Table2" width="600">
		<tr><td><h1>Referral: <%=loanId%>/<%=zipCode%></h1></td></tr>
				<%
				Response.Write ("<tr><td><div>")
				Response.Write ("<table border='0' style='height: 100%; width: 100%;'>")
				Response.Write ("<tr><td width='30%'></td><td width='70%'></td></tr>")
				%>
				
				<tr>
					<td><font class="labelWithHelp" title="Privacy consent agreed to?">Privacy consent agreed to?:</font></td>
					<td><%=privacyConsentInd%></td>
				</tr>

				<tr>
					<td><font class="label" title="">Priority Type:</font></td>
					<td><%=priorityType%></td>
				</tr>				

				<tr>
					<td><font class="label" title="">How did you hear about us?:</font></td>
					<td><%=referralSourceType%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Program:</font></td>
					<td><%=programName%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Agency:</font></td>
					<td>
						<%=agencyName%>&nbsp;
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Counselor:</font></td>
					<td>
						<%=counselorName%>&nbsp;
					</td>
				</tr>

				<tr id="existingServicerRow" name="existingServicerRow">
					<td><font class="label" title="">1st Mortgage/Servicer:</font></td>
					<td>
						<%=servicer%>&nbsp;
					</td>
				</tr>
				
				<tr>
					<td><font class="label">1st Mortgage Type (current rate):</font></td>
					<td><%=firstMortgageType%></td>
				</tr>	
				
				<tr>
					<td><font class="label" title="">Has interest reset on ARM loan?:</font></td>
					<td><%=armResetInd%></td>
				</tr>	
				
				<tr>
					<td><font class="label" title="">1st Mortgage loan #:</font></td>
					<td><%=loanId%></td>
				</tr>
				<tr>
					<td><font class="label" title="">1st Mortgage monthly payment (PITI):</font></td>
					<td><%=pitiAmt%></td>
				</tr>
				<tr>
					<td><font class="label" title="">1st Mortgage Loan Status:</font></td>
					<td><%=firstContactStatusType%></td>
				</tr>
                <tr>
                    <td>
                        <font class="label" title="">2nd Mortgage/Servicer: </font>
                    </td>
                    <td><%=servicer2%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="label" title="">2nd Mortgage type: </font>
                    </td>
                    <td><%=secondMortgageType%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="label" title="">2nd Mortgage loan #: </font>
                    </td>
                    <td><%=varloanId2%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="label" title="">2nd Mortgage monthly payment: </font>
                    </td>
                    <td><%=pitiAmt2%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="label" title="">2nd Mortgage loan status: </font>
                    </td>
                    <td><%=secondLoanStatusTypeCode%>&nbsp;
                    </td></tr>

				<tr>
					<td><font class="label" title="">Property for sale?:</font></td>
					<td><%=propertyForSaleInd%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">List Price of Home:</font></td>
					<td><%=listPriceAmt%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Realty Company Name:</font></td>
					<td><%=realtyCompanyName%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Has notice of Foreclosure sale been received?:</font></td>
					<td><%=fcNoticeRecdInd%></td>
				</tr>								
								
				<tr>
					<td><font class="label" title="">Bankruptcy?:</font></td>
					<td><%=bankruptcyInd%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Bankruptcy Attorney:</font></td>
					<td><%=bankruptcyAttorneyName%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Credit Score:</font></td>
					<td><%=creditScore%></td>
				</tr>

				<tr>
					<td><font class="label">Intake Score Type:</font></td>
					<td><%=intakeScoreType%></td>
				</tr>								
 				
				<tr>
					<td><font class="label">Monthly Net Income:</font></td>
					<td><%=mthlyNetIncomeType%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Household Income:</font></td>
					<td><%=householdIncome%></td>
				</tr>
				
				<tr>
					<td><font class="label">Income Category Type:</font></td>
					<td><%=incomeCategoryType%></td>
				</tr>							
				<tr>
					<td><font class="label">Number of adults contributing to household income:</font></td>
					<td><%=incomeEarnersType%></td>
				</tr>					
								
				<tr>
					<td><font class="label">Monthly Expenses:</font></td>
					<td><%=mthlyExpenseType%></td>
				</tr>
				
						
				<tr>
					<td><font class="label" title="">Last/First/MI:</font></td>
					<td><%=lastName%>,&nbsp;<%=firstName%>&nbsp;<%=miName%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Address:</font></td>
					<td><%=addrLine1%></td>
				</tr>
				<tr>
					<td><font class="label" title="">City/State/Zip:</font></td>
					<td><%=city%>,&nbsp;<%=state %>&nbsp;<%=zipCode%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Phone:</font></td>
					<td><%=phn%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">2nd Contact Number:</font></td>
					<td><%=secondaryContactNumber%></td>
				</tr>				
				<tr>
					<td><font class="label" title="">Email Address:</font></td>
					<td><%=emailAddr%></td>
				</tr>								
				<tr>
					<td><font class="label" title="">Mother's Maiden Name:</font></td>
					<td><%=mothersMaidenName%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Owner Occupied:</font></td>
					<td><%=ownerOccupiedInd%></td>
				</tr>				
				<tr>
					<td><font class="label" title=""># of People in House:</font></td>
					<td><%=occupants%></td>
				</tr>
				<tr>
					<td><font class="label">Household Type:</font></td>
					<td><%=householdType%></td>
				</tr>								
				<tr>
					<td><font class="label" title="">Agency Tracking Id:</font></td>
					<td><%=agencyTrackingId%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Referral Received Date:</font></td>
					<td><%=referralDate%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Referral Type:</font></td>
					<td><%=referralType%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Referring Person's Name:</font></td>
					<td><%=approvedBy%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Primary Default Reason Type:</font></td>
					<td><%=primaryDfltRsnType%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Secondary Default Reason Type:</font></td>
					<td><%=secondaryDfltRsnType%></td>
				</tr>				
				<%
			Dim referralResultType
			Rs = obj.Get_Referral_Results(referralId, sUserId, sSQL, iReturnCode, sMessage)
			If IsValidRS(Rs) Then
				Response.Write ("<tr><td colspan='2'><table style='position:relative;left:100;' border='1' cellpadding='1' cellspacing='1'>")
				Response.Write ("<tr><th width='150'>Result Type</th><th width='75'>Date</th></tr>")
				While Not Rs.eof
					Response.Write ("<tr>")
						Response.Write ("<td>")
				            Response.Write(Rs.Fields("DSCR").Value)
						Response.Write ("</td>")
						Response.Write ("<td>")
				            Response.Write(Rs.Fields("REFERRAL_RESULT_DATE").Value)
						Response.Write ("</td>")
					Response.Write ("</tr>")
					Rs.movenext
				end  While
				Response.Write ("</td></tr></table>")
				Rs.close
			Else
				Response.Write ("<tr><td><font class='label' title=''>Referral Result:</font></td>")
				Response.Write ("<tr><td><font class='label' title=''>Result Date:</font></td>")
				Response.Write ("<td>")
				Response.Write ("")
				Response.Write ("</td></tr>")
			End If
			Rs = nothing
			%>
			<tr>
					<td><font class="label" title="">HUD Outcome:</font></td>
					<td><%=hUDOutcome%></td>
				</tr>
				<tr>
					<td><font class="label" title="">HUD Termination Reason:</font></td>
					<td><%=hUDTermiReason%></td>
				</tr>
				<tr>
					<td><font class="label" title="">HUD Termination Date</font></td>
					<td><%=referralHUDTermdate%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Date Counseling Summary sent to 3rd Party/Lender:</font></td>
					<td><%=summaryRptSentDate%></td>
				</tr>

				<tr>
					<td><font class="label" title="">Counseling summary sent by alternate method:</font></td>
					<td><%=summarySentOtherType%></td>
				</tr>				
				
				<tr>
					<td><font class="label" title="">Date Counseling summary sent by alternate method:</font></td>
					<td><%=summarySentOtherDate%></td>
				</tr>				

				<tr>
					<td><font class="labelWithHelp" title="Homeowner talked to servicer in last 30 days?:">Homeowner talked to servicer in last 30 days?:</font></td>
					<td><%=contactedServicerInd%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Have you worked with your mortgage company to develop a solution:</font></td>
					<td><%=discussedSolutionWithSrvcr%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Have you worked with another counseling agency for help with your mortgage?:</font></td>
					<td><%=workedWithAnotherAgencyInd%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Cumulative length of counseling:</font></td>
					<td><%=counselingDurationType%></td>
				</tr>
				<tr id="rowReferralComment1">
					<td><font class="label" title="">Presenting issues/Main reason(s) for default:</font></td>
					<td><font class="listText" title=""><%=loanDefaultRsn%></font></td>
				</tr>
				<tr id="rowReferralComment2">
					<td><font class="label" title="">Recommended action items:</font></td>
					<td><font class="listText" title=""><%=actionItems%></font></td>
				</tr>
				<tr id="rowReferralComment3">
					<td><font class="label" title="">Follow up notes after initial counseling:</font></td>
					<td><font class="listText" title=""><%=followupNotes%></font></td>
				</tr>
				<%
				' BUDGET
				Dim Rs3
				Rs3 = obj.Get_Budget_Items(referralId, sUserId, sSQL, iReturnCode, sMessage)
				
				Dim budgetCategory, amt, eTotal, yTotal, subtotal, total
				subtotal = 0
				eTotal = 0
				yTotal = 0
				total = 0
				
				Response.Write ("<table border=""1"">")
				Response.Write ("<tr><td><b>Budget</b></td></tr>")
				If isValidRS(Rs3) Then
				    DIm i
					i = 0
					Rs3.MoveFirst
					budgetCategory = Rs3.Fields("BUDGET_CATEGORY").Value
					Do While Not Rs3.EOF				
						i = CInt(i) + 1
						If CInt(i) = 1 Then
							Response.Write ("<tr><td><b>" & Rs3.Fields("BUDGET_CATEGORY").Value & "</b></td>")
						End If
						If budgetCategory = Rs3.Fields("BUDGET_CATEGORY").Value Then
							If Rs3.Fields("AMT").Value Is Nothing Then
								amt = 0
							Else
								amt = Rs3.Fields("AMT").Value
							End If
							subtotal = CDbl(subtotal) + CDbl(amt)
							Utilities.PrintDetailRow (i, Rs3.Fields("BUDGET_CATEGORY_TYPE_CODE").Value, Rs3.Fields("BUDGET_CATEGORY").Value, Rs3.Fields("BUDGET_SUBCATEGORY_SEQ_ID").Value, Rs3.Fields("BUDGET_SUBCATEGORY").Value, Rs3.Fields("AMT").Value, total, eTotal, yTotal)
							Rs3.MoveNext
						Else
							Utilities.PrintSubTotalRow (subtotal)
							budgetCategory = Rs3.Fields("BUDGET_CATEGORY").Value
							subtotal = 0
							i = 0
						End If
					Loop
					Utilities.PrintSubTotalRow (subtotal				)
					Utilities.PrintTotalRow (total, eTotal, yTotal)
				End If
				Rs3.Close
				Rs3 = Nothing
				Response.Write ("</table>")
				%>				
			</table>
			</div>
			</td></tr></table>
		</FORM>
	</body>
</html>
<%
	obj = Nothing
	Rs.Close
	Rs = Nothing

%>
