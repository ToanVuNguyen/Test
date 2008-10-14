<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true CodeFile="counselingSummaryAutoEmail.aspx.vb" inherits=referrals_counselingSummaryAutoEmail %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/formatting.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/formParser.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/stringUtilities.inc'-->

<%

	On Error Resume Next

	'Server.ScriptTimeout = 1000
	
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
	Dim emailHeader, emailBody, emailBody2, emailBody3	
	Dim referralResultType, i, str, strTo, strFrom, strSubject, HTML, obj2, emailIt, acctPrd
	Dim age, genderType, householdType, householdIncome, incomeCategoryType, intakeScoreType, pitiAmt
	Dim propertyForSaleInd, listPriceAmt, realtyCompanyName, fcNoticeRecdInd, armResetInd, incomeEarnersType
	Dim summarySentOtherType, summarySentOtherDate
	Dim discussedSolutionWithSrvcr
	Dim servicer2,secondMortgageType,varloanId2,secondLoanStatusTypeCode, pitiAmt2
	Dim hUDOutcome, hUDTermiReason, referralHUDTermdate, workedWithAnotherAgencyInd

	
	acctPrd = Request("ACCT_PRD")
	
	Dim obj
	 obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
	
	Dim Rs, Rs2
	Rs = obj.Get_Referrals_AutoEmail(Request.Cookies("CCRCCookie")("profileSeqId"), sUserId, sSQL, iReturnCode, sMessage)
	'Rs = obj.Get_Referrals_AutoEmail(1, 1, sSQL, iReturnCode, sMessage)
	
	If RedirectIfError("Calling CCRC_Main.CMain.Get_Referral", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If	

	If isValidRS(Rs) Then
		obj2 = CreateObject("CCRC_Main.CMain")
	Dim summaryDate
	Dim fromName 
	Dim fromAddress 
	Dim subject 
		Rs.MoveFirst
		Do While Not Rs.EOF
		
			If Rs.Fields("PROGRAM_SEQ_ID").Value <> "140" And Rs.Fields("SERVICER_CONTACT_EMAIL").Value & "" <> "" And Rs.Fields("SERVICER_CONTACT_EMAIL").Value <> "none" And Not Rs.Fields("SERVICER_CONTACT_EMAIL").Value is nothing Then
'				If Rs.Fields("SERVICER_SEQ_ID").Value = "401" Or Rs.Fields("SERVICER_SEQ_ID").Value = "16641" Or Rs.Fields("SERVICER_SEQ_ID").Value = "17421" _				
'					Or Rs.Fields("SERVICER_SEQ_ID").Value = "17483" Or Rs.Fields("SERVICER_SEQ_ID").Value = "18411" Or Rs.Fields("SERVICER_SEQ_ID").Value = "1562" _
'					Or Rs.Fields("SERVICER_SEQ_ID").Value = "112" Or Rs.Fields("SERVICER_SEQ_ID").Value = "151" Or Rs.Fields("SERVICER_SEQ_ID").Value = "1181" _
'					Or Rs.Fields("SERVICER_SEQ_ID").Value = "261" Or Rs.Fields("SERVICER_SEQ_ID").Value = "148" _
'					Or Rs.Fields("SERVICER_SEQ_ID").Value = "601" Or Rs.Fields("SERVICER_SEQ_ID").Value = "8761" Or Rs.Fields("SERVICER_SEQ_ID").Value = "2702" _
'					Or Rs.Fields("SERVICER_SEQ_ID").Value = "17641" Or Rs.Fields("SERVICER_SEQ_ID").Value = "106" Or Rs.Fields("SERVICER_SEQ_ID").Value = "109" Then
		
					referralId = Rs.Fields("REFERRAL_SEQ_ID").Value

					borrowerName = buildName(Rs.Fields("FIRST_NAME").Value, Rs.Fields("MI_NAME").Value, Rs.Fields("LAST_NAME").Value, "")
					loanId = Rs.Fields("LOAN_ID").Value
					summaryDate = Date.Now()
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
					discussedSolutionWithSrvcr = Rs.Fields("DISCUSSED_SOLUTION_WITH_SRVCR_IND").Value	
					workedWithAnotherAgencyInd = Rs.Fields("WORKED_WITH_ANOTHER_AGENCY_IND").Value
                    
                    servicer2 = Rs.Fields("second_servicer_name").Value
                    secondMortgageType = Rs.Fields("second_mortgage_type").Value
                    varloanId2 = Rs.Fields("SECOND_LOAN_ID").Value
                    secondLoanStatusTypeCode = Rs.Fields("second_loan_status_type").Value
                    pitiAmt2 = Rs.Fields("SECOND_PITI_AMT").Value

                    hUDOutcome = Rs.Fields("hud_outcome_type").Value
                    hUDTermiReason = Rs.Fields("hud_term_reason_type").Value
                    referralHUDTermdate = Rs.Fields("HUD_TERM_DATE").Value

					
					subject = "HPF Counseling Summary for loan #" & loanId & ", priority " & priorityType
					
					emailHeader = "<table>" & vbCrLf
					emailHeader = emailHeader & "<tr><td><b>Date of Summary:</b> " & summaryDate & "</td></tr>" & vbCrLf
					emailHeader = emailHeader & "</table>" & vbCrLf
					
					emailBody = "<table>" & vbCrLf
					
					emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
					emailBody = emailBody & "<tr><td><b>Referral Received Date:</b> " & referralDate & "</td></tr>" & vbCrLf
					emailBody = emailBody & "<tr><td><b>Priority Type:</b> " & priorityType & "</td></tr>" & vbCrLf

					emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
					emailBody = emailBody & "<tr><td><b>1st Mortgage/Servicer:</b> " & servicerName & "</td></tr>" & vbCrLf
					emailBody = emailBody & "<tr><td><b>1st Mortgage Type (current rate):</b> " & firstMortgageType & "</td></tr>" & vbCrLf
					emailBody = emailBody & "<tr><td><b>Has interest reset on ARM loan?:</b> " & armResetInd & "</td></tr>" & vbCrLf
					emailBody = emailBody & "<tr><td><b>1st Mortgage loan #:</b> " & loanId & "</td></tr>" & vbCrLf
					emailBody = emailBody & "<tr><td><b>1st Mortgage monthly payment (PITI):</b> " & pitiAmt & "</td></tr>" & vbCrLf
					emailBody = emailBody & "<tr><td><b>1st Mortgage Loan Status:</b> " & firstContactStatusType & "</td></tr>" & vbCrLf
					
					emailBody = emailBody & "<tr><td><b>2nd Mortgage/Servicer:	</b> " & servicer2 & "</td></tr>" & vbCrLf	
                    emailBody = emailBody & "<tr><td><b>2nd Mortgage type:</b> " & secondMortgageType & "</td></tr>" & vbCrLf			
                    emailBody = emailBody & "<tr><td><b>2nd Mortgage loan #:</b> " & varloanId2 & "</td></tr>" & vbCrLf		
                    emailBody = emailBody & "<tr><td><b>2nd Mortgage monthly payment:</b> " & pitiAmt2 & "</td></tr>" & vbCrLf
                    emailBody = emailBody & "<tr><td><b>2nd Mortgage loan status:</b> " & secondLoanStatusTypeCode & "</td></tr>" & vbCrLf
					
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
'					emailBody = emailBody & "<tr><td><b>Hispanic Indicator:</b> " & hispanicInd & "</td></tr>" & vbCrLf
'					emailBody = emailBody & "<tr><td><b>Race:</b> " & raceType & "</td></tr>" & vbCrLf

					emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
					emailBody = emailBody & "<tr><td><b>Primary Default Reason Type:</b> " & primaryDfltRsnType & "</td></tr>" & vbCrLf
					emailBody = emailBody & "<tr><td><b>Secondary Default Reason Type:</b> " & secondaryDfltRsnType & "</td></tr>" & vbCrLf
					
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
					
					
					emailBody = emailBody & "<tr><td height='10'></td></tr>" & vbCrLf	
					emailBody = emailBody & "</table>" & vbCrLf

					emailIt = "N"
					i = 1
					Rs2 = obj.Get_Referral_Results(referralId, sUserId, sSQL, iReturnCode, sMessage)

					emailBody = emailBody & "<table border='1' cellpadding='1' cellspacing='1'>" & vbCrLf
					If IsValidRS(Rs2) Then
						emailBody = emailBody & "<tr><th>Result Type</th><th>Date</th></tr>" & vbCrLf
						While Not Rs2.eof
'							If Rs2.Fields("ACCT_PRD").Value = "200804" Then
'							If Rs2.Fields("ACCT_PRD").Value & "" <> "" And Not IsNull(Rs2.Fields("ACCT_PRD").Value) Then

							Dim TempAcct_PRD
							Dim TempemailIt
							TempAcct_PRD = Rs2.Fields("ACCT_PRD").Value
							if Len(TempAcct_PRD) = 0 Then
								TempemailIt = "N"
							Else

								If Rs2.Fields("ACCT_PRD").Value = acctPrd Then
									emailIt = "Y"
								End If
							End If
							emailBody = emailBody & "<tr><td>" & Rs2.Fields("DSCR") & "</td><td>" & Rs2.Fields("REFERRAL_RESULT_DATE") & "</td></tr>" & vbCrLf
							i = i + 1
							Rs2.movenext
						end While 
						Rs2.close
					Else
						emailBody = emailBody & "<tr><td><b>Referral Result:</b></td></tr>" & vbCrLf
						emailBody = emailBody & "<tr><td><b>Result Date:</b></td></tr>" & vbCrLf
					End If
					emailBody = emailBody & "</table>" & vbCrLf
					Rs2 = nothing
					
					emailBody2 = "<table width='600px'>" & vbCrLf

					emailBody2 = emailBody2 & "<tr><td height='10'></td></tr>" & vbCrLf	
 
                    emailBody2 = emailBody2 & "<tr><td><b>HUD Outcome:</b></td></tr>" & vbCrLf
                    emailBody2 = emailBody2 & "<tr><td>" & hUDOutcome & "</td></tr><br>" & vbCrLf
                    emailBody2 = emailBody2 & "<tr><td><b>HUD Termination Reason:</b></td></tr>" & vbCrLf
                    emailBody2 = emailBody2 & "<tr><td>" & hUDTermiReason & "</td></tr><br>" & vbCrLf
                    emailBody2 = emailBody2 & "<tr><td><b>HUD Termination date:</b></td></tr>" & vbCrLf
                    emailBody2 = emailBody2 & "<tr><td>" & referralHUDTermdate & "</td></tr><br>" & vbCrLf

                    emailBody2 = emailBody2 & "<tr><td><b>Have you worked with your mortgage company to develop a solution:</b></td></tr>" & vbCrLf
                    emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(discussedSolutionWithSrvcr, Chr(34)) & "</td></tr><br>" & vbCrLf
					
                    emailBody2 = emailBody2 & "<tr><td><b>Have you worked with another counseling agency for help with your mortgage?:</b></td></tr>" & vbCrLf
                    emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(workedWithAnotherAgencyInd, Chr(34)) & "</td></tr><br>" & vbCrLf

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
					Dim Rs3, budgetCategory, amt, eTotal, yTotal, subtotal, total

					Rs3 = obj.Get_Budget_Items(referralId, sUserId, sSQL, iReturnCode, sMessage)
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
								If Rs3.Fields("AMT").Value Is Nothing Then
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
					
				'--------------------------------------------------------------------------
					
					str = ""
					strTo = ""
					strFrom = ""
					strSubject = ""
					HTML = ""
					
					str = str & "<B>" & subject & "</B><BR><BR>"
					str = str & emailHeader
					str = str & "<BR>"
					str = str & "This summary is intended to provide you with a summary of the CCRC session. If you need more information or details, or would like CCRC input or assistance regarding a resolution with this homeowner, please contact the counselor directly.<BR>"
					str = str & emailBody
					str = str & emailBody2
					str = str & emailBody3				  
				      
					'strTo = servicerName & " (" & servicerContactEmail & ")"
					Dim TempservicerContactEmail
					TempservicerContactEmail = "(" & servicerContactEmail & ")"
				      
				    	if (Len(TempservicerContactEmail) = 2 ) then
					    emailIt = "N"
					else
					    strTo = servicerName & " (" & servicerContactEmail & ")"
					end if


					strFrom = fromName & " (" & fromAddress & ")"

					if "Sale Pending" = firstContactStatusType then
						strSubject = "***URGENT - CONTACT BORROWER*** " & subject
					else
						strSubject = subject
					end if	   

					HTML = HTML & "<html>"
					HTML = HTML & "<head>"
					HTML = HTML & "<title>Comment</title>"
					HTML = HTML & "</head>"
					HTML = HTML & "<body border=""0"" topmargin=""0"" leftmargin=""0"" marginwidth=""0"" "
					HTML = HTML & "marginheight=""0""  background=""/images/background_header_normal.gif"" "
					HTML = HTML & "<body bgcolor=""FFFFFF"" text=""000009""><font face=""Arial"">"
					HTML = HTML & "<table border=""0"" width=""85%"" vspace=""15"">"
					HTML = HTML & "<tr><td width=""15"">&nbsp;</td><td>" & str & "<br></td></tr>"
					HTML = HTML & "<br></td></tr>"
					HTML = HTML & "</table></font></body>"
					HTML = HTML & "</html>"
					'Response.write(HTML)
                    Dim strErrorMessage 
                    Dim errCode
					on error resume next
					If emailIt = "Y" Then
					    DIm myCDOMail 
						myCDOMail = CreateObject("CDO.Message")
						if err.number <> 0 then
							strErrorMessage = "Error creating CDO.Message object."
							errCode = 2
							err.clear
						end if
					    
					    if (strTo <> "" or strTo <> Nothing or strTo <> "none" or Len(strTo) <> 0) then
 						    myCDOMail.MimeFormatted = True
						    myCDOMail.To = strTo
						    'myCDOMail.To = "sanikodevait@csc.com"
						    myCDOMail.From = fromAddress
						    myCDOMail.CC = fromAddress
						    myCDOMail.Subject = strSubject  + " $s$"
						    myCDOMail.HTMLBody = HTML 
 						    myCDOMail.Send 
						    myCDOMail  = Nothing
					        Dim UserId
						    Call obj2.UpdateDateCounselingSummarySent(referralId, Date.Now(), sUserId, sSQL, iReturnCode, sMessage)
							LogRefEmail(strTo, fromAddress,referralId)
						    response.Write ("Counseling summary sent to " & Rs.Fields("SERVICER").Value & " for loan id/zip: " & Rs.Fields("LOAN_ID").Value & "/" & Rs.Fields("ZIP").Value & "<BR>")
                        End If
					End If
'				End If				
			End If
			Rs.MoveNext
		Loop
	Else
	    response.Write (" No Referrals to send.")
	End If	

	Rs.Close
	Rs = Nothing
	Rs2.Close
	Rs2 = Nothing
	obj = Nothing
	obj2 = Nothing
	
	'Response.Redirect ("../splash.aspx")



%>
