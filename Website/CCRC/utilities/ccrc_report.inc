<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">
'Public Function RedirectIfError(currentContext, eReturnPage, eReturnBtnText)
'	If err Then
'		If currentContext = "" Then
'			currentContext = "Building Page"
'		End If
'		
'		If Right(eRetPage, 1) = "?" Then
'			eRetPage = Replace(eRetPage, "?", "")
'		End If
'		eRetPage = server.URLEncode(eRetPage)
'		
'		Response.Clear
'		Response.Redirect getBasePath() & "error.aspx?errNum=" & err.number & "&errDesc=" & server.URLEncode (err.description) & "&errSrc=" & server.URLEncode (err.source) & "&URL=" & Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("URL")  & "&currContext=" & Server.URLEncode(currentContext) & "&ERetPage=" & eReturnPage & "&ERetBtnText=" & eReturnBtnText
	'		RedirectIfError = True
	''	Else
	''		RedirectIfError = False
	'	End If
	'End Function

	'Public Function getBasePath()
	'	Dim sURL
	'	Dim sPath
	'
	'	sURL = Request.ServerVariables("SERVER_NAME")
	'	if instr(1, sURL, "avenue", vbTextCompare) > 0 then
	'		getBasePath = "/"
	'	else
	'		sPath = Request.ServerVariables("PATH_INFO")
	'		getBasePath = Left(sPath, InStr(2, sPath, "/", vbTextCompare))
	'	end if
	'
	'End Function
	'
	'Public Function isValidRS(rsIn)
	'	isValidRS = False
	'	If IsObject(rsIn) Then
	'		If Not (rsIn Is Nothing) Then
	'If Not (rsIn.EOF And rsIn.BOF) Then
	'isValidRS = True
	'End If
	'End If
	'End If
	'End Function


	Sub WriteRsToExcel(rs)
	Dim i

	If Not (rs.EOF) Then
	rs.MoveFirst
	'	For i = 0 To rs.Fields.Count - 1
	'		Response.Write(rs.Fields(i).Name & " - " & rs.Fields(i).Value & "<BR>")
    '	Next
    '	response.End

    Response.Write("<table>")
	Response.Write("<tr>")
		For i = 0 To rs.Fields.Count - 1
		Select Case UCase(rs.Fields(i).Name)
		Case "REFERRAL_SEQ_ID", "LOAN_ID", _
		"LAST_NAME", "FIRST_NAME", "ADDR_LINE_1", "CITY", "STATE", "ZIP", "PHN", "OCCUPANTS", _
		"REFERRAL_DATE", "REFERRAL_TYPE", "REFERRAL_SOURCE_TYPE", "FIRST_CONTACT_STATUS_TYPE", _
		"PROGRAM_NAME", "FIRST_MORTGAGE_SERVICER", "CONTACTED_SERVICER_IND", "REFERRAL_RESULT_DATE", "REFERRAL_RESULT", "SUMMARY_RPT_SENT_DATE", _
		"AGENCY", "AGENCY_REFERRAL_ID", "COUNSELOR_LAST_NAME", "COUNSELOR_FIRST_NAME", _
		"PAYABLE_IND", "HAS_CONTRACT_IND", "ACCT_PRD", "COUNSELING_DURATION_TYPE", "MTHLY_NET_INCOME_TYPE", "FIRST_MORTGAGE_TYPE", _
		"CREDIT_SCORE", "MOTHERS_MAIDEN_NAME", "PRIVACY_CONSENT_IND", "PRIORITY_TYPE", "SECONDARY_CONTACT_NUMBER", "EMAIL_ADDR", _
		"BANKRUPTCY_IND", "BANKRUPTCY_ATTORNEY_NAME", "OWNER_OCCUPIED_IND", "PRIMARY", "SECONDARY", _
		"HOUSEHOLD_TYPE", "HOUSEHOLD_INCOME_AMT", "INCOME_CATEGORY_TYPE", "INTAKE_SCORE_TYPE", "PITI_AMT", _
		"ARM_RESET_IND", "PROPERTY_FOR_SALE_IND", "LIST_PRICE_AMT", "REALTY_COMPANY_NAME", "FC_NOTICE_RECD_IND", _
		"INCOME_EARNERS_TYPE", "SUMMARY_SENT_OTHER_TYPE", "SUMMARY_SENT_OTHER_DATE", _
		"RESULT1", "RESULT2", "RESULT3", _
		"DISCUSSED_SOLUTION_WITH_SRVCR_IND", "WORKED_WITH_ANOTHER_AGENCY_IND", "SECOND_MOTGAGE_SERVICER", "SECOND_MORTGAGE_TYPE", _
		"SECOND_LOAN_ID", "SECOND_PITI_AMT",  "SECOND_LOAN_STATUS_TYPE", "HUD_OUTCOME_TYPE", "HUD_TERM_REASON_TYPE", "HUD_TERM_DATE", _
		"PAID_DATE", "CHECK_NBR", "PAID_AMT", "DUPE_IND", "INVOICE_DATE","GENDER_TYPE"
		Response.Write("<td>" & UCase(rs.Fields(i).Name) & "</td>")
        Case "APPROVED_BY"
        Response.Write("<td>REFERRING LENDER REP</td>"	)
        End Select
        If Request.Cookies("CCRCCookie")("entityTypeCode") <> "2" And (rs.Fields(i).Name = "HISPANIC_IND" Or rs.Fields(i).Name = "RACE_TYPE" Or rs.Fields(i).Name = "AGE" Or rs.Fields(i).Name = "GENDER_TYPE") Then
			Response.Write("<td>" & rs.Fields(i).Name & "</td>")
            End If
            Next
            Response.Write("</tr>")
	rs.MoveFirst
	Do While Not rs.EOF
		Response.Write("<tr>")
		For i = 0 To rs.Fields.Count - 1
			Select Case UCase(rs.Fields(i).Name)
				Case "REFERRAL_SEQ_ID", "LOAN_ID", "ZIP", "AGENCY_REFERRAL_ID"
					Response.Write("<td>'" & rs.Fields(i).Value & "</td>")
			Case "LAST_NAME", "FIRST_NAME", "ADDR_LINE_1", "CITY", "STATE", "PHN", "OCCUPANTS", _
			"REFERRAL_DATE", "REFERRAL_TYPE", "REFERRAL_SOURCE_TYPE", "FIRST_CONTACT_STATUS_TYPE", _
			"PROGRAM_NAME", "FIRST_MORTGAGE_SERVICER", "CONTACTED_SERVICER_IND", "APPROVED_BY", "REFERRAL_RESULT_DATE", "REFERRAL_RESULT", _
			"SUMMARY_RPT_SENT_DATE", "AGENCY", "COUNSELOR_LAST_NAME", "COUNSELOR_FIRST_NAME", _
			"PAYABLE_IND", "HAS_CONTRACT_IND", "ACCT_PRD", "COUNSELING_DURATION_TYPE", "MTHLY_NET_INCOME_TYPE", "FIRST_MORTGAGE_TYPE", _
			"CREDIT_SCORE", "MOTHERS_MAIDEN_NAME", "PRIVACY_CONSENT_IND", "PRIORITY_TYPE", "SECONDARY_CONTACT_NUMBER", "EMAIL_ADDR", _
			"BANKRUPTCY_IND", "BANKRUPTCY_ATTORNEY_NAME", "OWNER_OCCUPIED_IND", "PRIMARY", "SECONDARY", _
			"HOUSEHOLD_TYPE", "HOUSEHOLD_INCOME_AMT", "INCOME_CATEGORY_TYPE", "INTAKE_SCORE_TYPE", "PITI_AMT", _
			"ARM_RESET_IND", "PROPERTY_FOR_SALE_IND", "LIST_PRICE_AMT", "REALTY_COMPANY_NAME", "FC_NOTICE_RECD_IND", _
			"INCOME_EARNERS_TYPE", "SUMMARY_SENT_OTHER_TYPE", "SUMMARY_SENT_OTHER_DATE", _
			"RESULT1", "RESULT2", "RESULT3", _
			"DISCUSSED_SOLUTION_WITH_SRVCR_IND", "WORKED_WITH_ANOTHER_AGENCY_IND", "SECOND_MOTGAGE_SERVICER", "SECOND_MORTGAGE_TYPE", _
			"SECOND_LOAN_ID", "SECOND_PITI_AMT",  "SECOND_LOAN_STATUS_TYPE", "HUD_OUTCOME_TYPE", "HUD_TERM_REASON_TYPE", "HUD_TERM_DATE", _
			"PAID_DATE", "CHECK_NBR", "PAID_AMT", "DUPE_IND", "INVOICE_DATE","GENDER_TYPE"
			Response.Write("<td>" & rs.Fields(i).Value & "</td>")
            End Select
            If Request.Cookies("CCRCCookie")("entityTypeCode") <> "2" And (rs.Fields(i).Name = "HISPANIC_IND" Or rs.Fields(i).Name = "RACE_TYPE" Or rs.Fields(i).Name = "AGE" Or rs.Fields(i).Name = "GENDER_TYPE") Then
            Response.Write("<td>" & rs.Fields(i).Value & "</td>")
            End If
            Next
            Response.Write("</tr>")
        rs.MoveNext
        Loop
        Response.Write("</table>")
		End If
		End Sub
	</script>