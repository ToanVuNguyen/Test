Imports CCRC
Partial Class referrals_NewCounselingSummary
    Inherits System.Web.UI.Page
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
             Dim loanId, borrowerName, agencyName, counselorName, servicerName, servicerContactName, servicerContactEmail
            Dim agencyTrackingId, referralType
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
            Dim age, genderType, householdType, incomeCategoryType, intakeScoreType
            Dim pitiAmt As Integer
            Dim householdIncome As Integer
            Dim propertyForSaleInd, listPriceAmt, realtyCompanyName, fcNoticeRecdInd, armResetInd, incomeEarnersType
            Dim summarySentOtherType, summarySentOtherDate
            Dim summaryDate, fromName, fromAddress, subject
            Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
            Dim discussedSolutionWithSrvcr
            Dim servicer2, secondMortgageType, varloanId2, secondLoanStatusTypeCode, pitiAmt2
            Dim hUDOutcome, hUDTermiReason, referralHUDTermdate, workedWithAnotherAgencyInd

            referralId = Request.QueryString("referralId")
            loanId = Request.QueryString("loanId")
            zipCode = Request.QueryString("zipCode")

            Dim obj
            obj = CreateObject("CCRC_Main.CMain")


            Call obj.UpdateDateCounselingSummarySent(referralId, Date.Now, sUserId, sSQL, iReturnCode, sMessage)
            ' Response.write(Request.Cookies("CCRCCookie")("profileSeqId"))    
            Dim Rs
            'Rs = obj.Get_Referral(loanId, referralId, zipCode, 1, sUserId, sSQL, iReturnCode, sMessage)
            Rs = obj.Get_Referral(loanId, referralId, zipCode, Request.Cookies("CCRCCookie")("profileSeqId"), sUserId, sSQL, iReturnCode, sMessage)

            servicerContactEmail = ""
            fromName = ""

 		

            If isValidRS(Rs) Then
                '   Response.Write("Hi")
                referralId = Rs.Fields("REFERRAL_SEQ_ID").Value

                borrowerName = Rs.Fields("LAST_NAME").Value & ", " & Rs.Fields("FIRST_NAME").Value & " " & Rs.Fields("MI_NAME").Value
                loanId = Rs.Fields("LOAN_ID").Value
                summaryDate = Now
                approvedBy = Rs.Fields("APPROVED_BY").Value
                servicerName = Rs.Fields("SERVICER").Value
                servicerContactName = Rs.Fields("SERVICER_CONTACT_NAME").Value
                servicerContactEmail = Rs.Fields("SERVICER_CONTACT_EMAIL").Value
 		'Response.Write(Rs.Fields("SERVICER_CONTACT_EMAIL").Value)
                'Response.End()



                counselorName = Rs.Fields("COUNSELOR_FIRST_NAME").Value & Rs.Fields("COUNSELOR_MID_INIT_NAME").Value & Rs.Fields("COUNSELOR_LAST_NAME").Value & Rs.Fields("COUNSELOR_NAME_SUFFIX_CODE").Value
                fromName = Rs.Fields("COUNSELOR_FIRST_NAME").Value & Rs.Fields("COUNSELOR_MID_INIT_NAME").Value & Rs.Fields("COUNSELOR_LAST_NAME").Value & Rs.Fields("COUNSELOR_NAME_SUFFIX_CODE").Value
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
                'householdIncome = Rs.Fields("household_income_amt").Value

		Dim temp_piti_amt
		temp_piti_amt = "*" & Rs.Fields("household_income_amt").Value & "*"

		If  Len(temp_piti_amt) = 2 Then
                	'householdIncome = 0
		Else
			householdIncome = Rs.Fields("household_income_amt").Value
		End If


                householdType = Rs.Fields("household_type").Value
                incomeCategoryType = Rs.Fields("income_category_type").Value
                intakeScoreType = Rs.Fields("intake_score_type").Value
                'pitiAmt = Rs.Fields("piti_amt").Value
		temp_piti_amt = "*" & Rs.Fields("piti_amt").Value & "*"

		If  Len(temp_piti_amt) = 2 Then
                	pitiAmt = 0
		Else
			pitiAmt = Rs.Fields("piti_amt").Value
		End If

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

            Else
                Response.Write("Invlid Rs")
                Response.End()
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
            '	emailBody = emailBody & "<tr><td><b>Hispanic Indicator:</b> " & hispanicInd & "</td></tr>" & vbCrLf
            '	emailBody = emailBody & "<tr><td><b>Race:</b> " & raceType & "</td></tr>" & vbCrLf

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
            Dim referralResultType
            Dim i
            i = 1
            Rs = obj.Get_Referral_Results(referralId, sUserId, sSQL, iReturnCode, sMessage)


            emailBody = emailBody & "<table border='1' cellpadding='1' cellspacing='1'>" & vbCrLf
            If isValidRS(Rs) Then
                '		emailBody = emailBody & "<tr><td colspan='2'><table style='position:relative;left:100;' border='1' cellpadding='1' cellspacing='1'>"
                emailBody = emailBody & "<tr><th>Result Type</th><th>Date</th></tr>" & vbCrLf
                While Not Rs.eof
                    emailBody = emailBody & "<tr><td>" & Rs.Fields("DSCR").Value & "</td><td>" & Rs.Fields("REFERRAL_RESULT_DATE").Value & "</td></tr>" & vbCrLf
                    i = i + 1
                    Rs.movenext()
                End While
                Rs.close()
            Else
                emailBody = emailBody & "<tr><td><b>Referral Result:</b></td></tr>" & vbCrLf
                emailBody = emailBody & "<tr><td><b>Result Date:</b></td></tr>" & vbCrLf
            End If
            emailBody = emailBody & "</table>" & vbCrLf
            Rs = Nothing

            emailBody2 = emailBody2 & "<table width='600px'>" & vbCrLf

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
            emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(loanDefaultRsn, Chr(34)) & "</td></tr><br>" & vbCrLf

            emailBody2 = emailBody2 & "<tr><td><b>Recommended action items:</b></td></tr>" & vbCrLf
            emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(actionItems, Chr(34)) & "</td></tr><br>" & vbCrLf

            emailBody2 = emailBody2 & "<tr><td><b>Follow up notes after initial counseling:</b></td></tr>" & vbCrLf
            emailBody2 = emailBody2 & "<tr><td>" & stripCharsInBag(followupNotes, Chr(34)) & "</td></tr><br>" & vbCrLf

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
                Rs3.MoveFirst()
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
                        'PrintDetailRow(ByVal irow, ByVal budgetCategoryType, ByVal category, ByVal budgetSubcategorySeqId, ByVal budgetSubcategory, ByVal amt, ByRef total, ByRef eTotal, ByRef yTotal, ByRef emailBody3)
                        'PrintDetailRow(i, Rs3.Fields("BUDGET_CATEGORY_TYPE_CODE").Value, Rs3.Fields("BUDGET_CATEGORY").Value, Rs3.Fields("BUDGET_SUBCATEGORY_SEQ_ID").Value, Rs3.Fields("BUDGET_SUBCATEGORY").Value, Rs3.Fields("AMT").Value, total, eTotal, yTotal, emailBody3)
                        If CInt(Rs3.Fields("BUDGET_CATEGORY_TYPE_CODE").Value) = 1 Then
                            total = CDbl(total) + CDbl(amt)
                            yTotal = CDbl(yTotal) + CDbl(amt)
                        Else
                            total = CDbl(total) - CDbl(amt)
                            eTotal = CDbl(eTotal) + CDbl(amt)
                        End If
                        If CInt(i) = 1 Then
                            emailBody3 = emailBody3 & "<td>" & Rs3.Fields("BUDGET_SUBCATEGORY").Value & "</td><td>" & amt & "</td></tr>"
                        Else
                            emailBody3 = emailBody3 & "<tr><td></td><td>" & Rs3.Fields("BUDGET_SUBCATEGORY").Value & "</td><td>" & amt & "</td></tr>"
                        End If
                        Rs3.MoveNext()
                    Else
                        PrintSubtotalRow(subtotal, emailBody3)
                        budgetCategory = Rs3.Fields("BUDGET_CATEGORY").Value
                        subtotal = 0
                        i = 0
                    End If

                Loop
                emailBody3 = emailBody3 & "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"
                emailBody3 = emailBody3 & "<tr><td></td><td><b>Income Total:</b></td><td><b>" & yTotal & "</b></td></tr>"
                emailBody3 = emailBody3 & "<tr><td></td><td><b>Expense Total:</b></td><td><b>" & eTotal & "</b></td></tr>"
                emailBody3 = emailBody3 & "<tr><td></td><td><b>Total Surplus or Deficit:</b></td><td><b>" & total & "</b></td></tr>"



            End If
            Rs3.Close()
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



            Label1.Text = emailHeader + vbCrLf + emailBody + vbCrLf + emailBody2 + vbCrLf + emailBody3

		 

            sendToName.Text = servicerName
Dim str 
str =   "*" &servicerContactEmail & "*"

	    'Response.Write(servicerContactEmail)
Dim strlen = Len(str)
		if (strlen = 2) Then
			sendToAddress.Text = ""
		Else
			sendToAddress.Text = servicerContactEmail	
		End If



	   

            TxtfromName.Text = fromName

            TxtfromAddress.Text = fromAddress
            'Commented by Long Cao as quest from Chris, replace with an other one - 10/10/2008
            'txtSubject.Text = "HPF Counseling Summary for loan #" & loanId & ", priority " & priorityType
            txtSubject.Text = "HPF Summary loan #" & loanId & " / " & zipCode & ", priority " & priorityType
        End If

    End Sub

    Public Function isValidRS(ByVal rsIn)
        isValidRS = False

        If Not (rsIn Is Nothing) Then
            If Not (rsIn.EOF And rsIn.BOF) Then
                isValidRS = True
            Else
            End If
        Else
            'Response.Write("In 1")
        End If

    End Function

    Protected Sub PrintDetailRow(ByVal irow, ByVal budgetCategoryType, ByVal category, ByVal budgetSubcategorySeqId, ByVal budgetSubcategory, ByVal amt, ByRef total, ByRef eTotal, ByRef yTotal, ByRef emailBody3)
        If CInt(budgetCategoryType) = 1 Then
            total = CDbl(total) + CDbl(amt)
            yTotal = CDbl(yTotal) + CDbl(amt)
        Else
            total = CDbl(total) - CDbl(amt)
            eTotal = CDbl(eTotal) + CDbl(amt)
        End If
        If CInt(irow) = 1 Then
            emailBody3 = emailBody3 & "<td>" & budgetSubcategory & "</td><td>" & amt & "</td></tr>"
        Else
            emailBody3 = emailBody3 & "<tr><td></td><td>" & budgetSubcategory & "</td><td>" & amt & "</td></tr>"
        End If
    End Sub
    Protected Sub PrintSubtotalRow(ByVal subtotal, ByRef emailBody3)
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Subtotal:</b></td><td><b>" & subtotal & "</b></td></tr>"
    End Sub

    Protected Sub PrintTotalRow(ByVal total, ByVal eTotal, ByVal yTotal, ByVal emailBody3)
        emailBody3 = emailBody3 & "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Income Total:</b></td><td><b>" & yTotal & "</b></td></tr>"
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Expense Total:</b></td><td><b>" & eTotal & "</b></td></tr>"
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Total Surplus or Deficit:</b></td><td><b>" & total & "</b></td></tr>"
    End Sub
    Public Function stripCharsInBag(ByVal s, ByVal bag)

        Dim sTemp
        If Not IsDBNull(s) Then
            sTemp = CStr(s)
        Else
            sTemp = ""
        End If
        Dim j
        j = Len(bag)

        Dim i
        For i = 1 To j
            sTemp = Replace(sTemp, Mid(bag, i, 1), "")
        Next

        stripCharsInBag = sTemp

    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim savePath As String = "c:\temp\"



        If (FileUpload1.HasFile) Then
            FileUpload1.SaveAs(savePath + FileUpload1.FileName)
        End If
        If (FileUpload3.HasFile) Then
            FileUpload3.SaveAs(savePath + FileUpload3.FileName)
        End If
        If (FileUpload2.HasFile) Then
            FileUpload2.SaveAs(savePath + FileUpload2.FileName)
        End If

        Dim myCDOMail
        myCDOMail = CreateObject("CDO.Message")

        myCDOMail.MimeFormatted = True
        myCDOMail.To = sendToAddress.Text
        'myCDOMail.BCC = TxtfromAddress.Text ----Commented by Long Cao, we do not need to send mail to both send and from e-mail
        myCDOMail.From = TxtfromAddress.Text
        myCDOMail.Subject = txtSubject.Text + " $s$"
        Label1.Text = Label1.Text & "<B>Counselor Comments:</B><BR>"
        Label1.Text = Label1.Text & emailComment.Value



        ' Insert VbCrLf
        Dim EmailStr As String
		Dim Att1 As String
        Dim Att2 As String
        Dim Att3 As String

        EmailStr = Label1.Text
        EmailStr.Replace("</tr>", "</tr>" & vbCrLf)


        myCDOMail.HTMLBody = EmailStr

        If (FileUpload1.HasFile) Then
            myCDOMail.AddAttachment(savePath & FileUpload1.FileName)
			Att1 = FileUpload1.FileName
        End If
        If (FileUpload2.HasFile) Then
            myCDOMail.AddAttachment(savePath & FileUpload2.FileName)
			Att2 = FileUpload2.FileName
        End If
        If (FileUpload3.HasFile) Then
            myCDOMail.AddAttachment(savePath & FileUpload3.FileName)
			Att3 = FileUpload3.FileName
        End If
        myCDOMail.send()
        myCDOMail = Nothing

        If (FileUpload1.HasFile) Then
            System.IO.File.Delete(savePath & FileUpload1.FileName)
        End If
        If (FileUpload2.HasFile) Then
            System.IO.File.Delete(savePath & FileUpload2.FileName)
        End If
        If (FileUpload3.HasFile) Then
            System.IO.File.Delete(savePath & FileUpload3.FileName)
        End If

        Dim ObjReferralEmailHelper As New CCRC.ReferralEmailHelper
		Dim refId = Request.QueryString("referralId")

	'ObjReferralEmailHelper.UpdateReferralforEmail(sendToAddress.Text, emailComment.Value, refId)
        ObjReferralEmailHelper.EmailLogWithAttachment(sendToAddress.Text, TxtfromAddress.Text, refId, "Counselor", Att1, Att2, Att3)
 
        Dim referralId = Request.QueryString("referralId")
        Response.Redirect("referral.aspx?referralId=" & referralId & "&FORMODE=EDIT")


    End Sub
End Class
