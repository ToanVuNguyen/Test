<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
    
    Response.Buffer = True
    Response.ExpiresAbsolute = DateTime.Now.AddDays (-1)
    Response.Expires = 0
    Response.CacheControl = "no-cache"


    'Response.Cookies("CCRCCookie")("profileRoles") = "SomeRole"
    'Response.Cookies("CCRCCookie")("profileSeqId") = "1"

    'Dim formMode, referralId, disable, entityId
    Dim formMode, referralId, entityId
    Dim internal, action, userRoles
    Dim RsPrograms, RsData, servicers, locations
    Dim agencies, counselors, counselorId
    Dim sessionRatingCode, sessionTypeCode, servicer, theLocation, theProgram
    Dim varloanId, borrowerName, agencyName, counselorName, agencyTrackingId, referralTypeCode
    Dim varlastName, firstName, miName
    Dim varaddrLine1, city, state, zipCode, agencyId
	Dim hispanicInd, raceType, dupeInd
	Dim Rs
	dupeInd=""
	Dim obj
	Dim creditScore, mothersMaidenName, privacyConsentInd, priorityTypeCode
    Dim discussedSolutionWithSrvcr,servicer2,secondMortgageType,varloanId2,secondLoanStatusTypeCode
    Dim hUDOutcome, hUDTermiReason, referralHUDTermdate
    Dim RsServicers2
    Dim contactedServicerInd, referralSourceTypeCode, workedWithAnotherAgencyInd
	Dim RsAgencies, RsCounselors
	Dim RsServicers, RsLocations
	Dim newServicer, newLocation
    Dim propertyForSaleInd, realtyCompanyName, fcNoticeRecdInd, armResetInd, incomeEarnersType
	Dim secondaryContactNumber, emailAddr, bankruptcyInd, bankruptcyAttorneyName, ownerOccupiedInd
    Dim age, genderType, householdType, incomeCategoryType, intakeScoreType
    Dim pitiAmt As Integer
    Dim householdIncome As Integer
    Dim listPriceAmt As Integer
    Dim pitiAmt2 As Integer
    
    Dim HUDTermdate
	Dim referralDate, referralComment, approvedBy
	Dim summaryRptSentDate, phn, occupants, loanDefaultRsn
	Dim actionItems, followupNotes, successStoryInd, firstContactStatusTypeCode
	
	Dim initialFICO, initialFICODate
	Dim currentFICO, currentFICODate
	Dim counselingDurationType, mthlyNetIncomeType, mthlyExpenseType, firstMortgageType
	
	
	Dim primaryDfltRsnType, secondaryDfltRsnType
    Dim profileSeqId
	
	Dim url
	Dim summarySentOtherType, summarySentOtherDate
    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    sessionRatingCode = "1"
    sessionTypeCode = "1"
	On Error Resume Next

    'Server.ScriptTimeout = 1000
	
	err.Clear() 
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
	If RedirectIfError("Calling GetAuthorizedUser", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If	

	On Error Resume Next
	
        
    varloanId = Request.QueryString("loanId")
    
    zipCode = Request.QueryString("zipCode")
    userRoles = Request.Cookies("CCRCCookie")("profileRoles")
    entityId = Request.Cookies("CCRCCookie")("entitySeqId")
    profileSeqId = Request.Cookies("CCRCCookie")("profileSeqId")
    formMode = Request.QueryString("formMode")

     
    If formMode = "ADD" Then
        referralId = 0
        varloanId = varloanId.ToString().Replace(" ", "") 'Trim white-space 
    Else
        referralId = Request.QueryString("referralId")
    End If
	
    ' varloanId = 1
    ' zipCode = 60559
    'userRoles = 83
    'entityId = 1
    'profileSeqId = 1
    'formMode = "EDIT"
    'referralId = 19202
    
    
    'Dim obj
    obj = CreateObject("CCRC_Main.CMain")
    'obj = new CCRC_Main.CMain 
    If RedirectIfError("XXX Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If
    
    If Not obj Is Nothing Then
        'Response.Write("HI")
        Response.Flush()
    End If
    Rs = obj.Get_Referral(varloanId, referralId, zipCode, profileSeqId, sUserId, sSQL, iReturnCode, sMessage)
    'response.Write ssql
    'response.end
    If RedirectIfError("Calling CCRC_Main.CMain.Get_Referral", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If
    internal = False
    If isValidRS(Rs) Then
	
        'dim fld
        'for each fld in Rs.fields
        '	response.Write fld.name & "<br>"
        'next	
        'response.End
        'Response.Write("HI2")
        varloanId = Rs.Fields("LOAN_ID").Value
		
        If Rs.Fields("INTERNAL_IND").Value = "Y" Then
            internal = True
        End If
		
        borrowerName = buildName(Rs.Fields("FIRST_NAME").Value, Rs.Fields("MI_NAME").Value, Rs.Fields("LAST_NAME").Value, "")
        varlastName = Rs.Fields("LAST_NAME").Value
        firstName = Rs.Fields("FIRST_NAME").Value
        miName = Rs.Fields("MI_NAME").Value
        varaddrLine1 = Rs.Fields("ADDR_LINE_1").Value
        city = Rs.Fields("CITY").Value
        state = Rs.Fields("STATE").Value
        zipCode = Rs.Fields("ZIP").Value

        referralId = Rs.Fields("REFERRAL_SEQ_ID").Value
        If referralId <> "0" Then
            formMode = "EDIT"
        End If

        agencyId = Rs.Fields("AGENCY_SEQ_ID").Value
        counselorId = Rs.Fields("COUNSELOR_SEQ_ID").Value
        agencyName = Rs.Fields("BSN_ENTITY_NAME").Value
        counselorName = buildName(Rs.Fields("COUNSELOR_FIRST_NAME").Value, Rs.Fields("COUNSELOR_MID_INIT_NAME").Value, Rs.Fields("COUNSELOR_LAST_NAME").Value, Rs.Fields("COUNSELOR_NAME_SUFFIX_CODE").Value)
        servicer = Rs.Fields("SERVICER_SEQ_ID").Value
        theLocation = Rs.Fields("SERVICER_LOCTN_SEQ_ID").Value
        theProgram = Rs.Fields("PROGRAM_SEQ_ID").Value
        agencyTrackingId = Rs.Fields("AGENCY_REFERRAL_ID").Value
        referralTypeCode = Rs.Fields("REFERRAL_TYPE_CODE").Value
		
        'If IsNull(Rs.Fields("REFERRAL_DATE").Value) Or Len(Rs.Fields("REFERRAL_DATE").Value) = 0 Then
        '''PB
        'Response.Write(Rs.Fields("REFERRAL_DATE").Value)
        
        'Response.End()
        If Not IsDate(Rs.Fields("REFERRAL_DATE").Value) Then
            referralDate = Date.Now
        Else
            referralDate = Rs.Fields("REFERRAL_DATE").Value
        End If
        'If IsDBNull(Rs.Fields("REFERRAL_DATE").Value) Or Len(Rs.Fields("REFERRAL_DATE").Value) = 0 Then
        'If IsDBNull(Rs.Fields("REFERRAL_DATE").Value) Or Rs.Fields("REFERRAL_DATE").Value Is Nothing Or Len(Rs.Fields("REFERRAL_DATE").Value) = 0 Then
        'referralDate = Now()
        'Else
        '   referralDate = Rs.Fields("REFERRAL_DATE").Value
        'End If
        'Response.Write(referralDate & "HI3<br>")
        referralDate = Month(referralDate) & "/" & Day(referralDate) & "/" & Year(referralDate)
        referralComment = Rs.Fields("GENERAL_COMMENT").Value
        approvedBy = Rs.Fields("APPROVED_BY").Value
        sessionRatingCode = Rs.Fields("SESSION_RATING_TYPE_CODE").Value
        sessionTypeCode = Rs.Fields("SESSION_TYPE_CODE").Value
        summaryRptSentDate = Rs.Fields("SUMMARY_RPT_SENT_DATE").Value
        phn = Rs.Fields("PHN").Value
        occupants = Rs.Fields("OCCUPANTS").Value
        loanDefaultRsn = Rs.Fields("LOAN_DEFAULT_RSN").Value
        actionItems = Rs.Fields("ACTION_ITEMS").Value
        followupNotes = Rs.Fields("FOLLOWUP_NOTES").Value
        successStoryInd = Rs.Fields("SUCCESS_STORY_IND").Value
        firstContactStatusTypeCode = Rs.Fields("FIRST_CONTACT_STATUS_TYPE_CODE").Value
        referralSourceTypeCode = Rs.Fields("REFERRAL_SOURCE_TYPE_CODE").Value
        contactedServicerInd = Rs.Fields("CONTACTED_SERVICER_IND").Value
        ' Mortgage Field
        servicer2 = Rs.Fields("SECOND_SERVICER_SEQ_ID").Value
        secondMortgageType = Rs.Fields("SECOND_MORTGAGE_TYPE_CODE").Value
        discussedSolutionWithSrvcr = Rs.Fields("DISCUSSED_SOLUTION_WITH_SRVCR_IND").Value
        varloanId2 = Rs.Fields("SECOND_LOAN_ID").Value
        pitiAmt2 = Rs.Fields("SECOND_PITI_AMT").Value
        secondLoanStatusTypeCode = Rs.Fields("SECOND_LOAN_STATUS_TYPE_CODE").Value
        ' New HUD Fields
        hUDOutcome = Rs.Fields("HUD_OUTCOME_TYPE_CODE").Value
        hUDTermiReason = Rs.Fields("HUD_TERM_REASON_TYPE_CODE").Value
        referralHUDTermdate = Rs.Fields("HUD_TERM_DATE").Value
        referralHUDTermdate = Month(Rs.Fields("HUD_TERM_DATE").Value) & "/" & Day(Rs.Fields("HUD_TERM_DATE").Value) & "/" & Year(Rs.Fields("HUD_TERM_DATE").Value)
        workedWithAnotherAgencyInd = Rs.Fields("WORKED_WITH_ANOTHER_AGENCY_IND").Value
        
        initialFICO = Rs.Fields("INITIAL_FICO").Value
        '''If IsNull(Rs.Fields("INITIAL_FICO_DATE").Value) Or Len(Rs.Fields("INITIAL_FICO_DATE").Value) = 0 Then
        '''PB
        'If Rs.Fields("INITIAL_FICO_DATE").Value Is Nothing Or Len(Rs.Fields("INITIAL_FICO_DATE").Value) = 0 Then
        'initialFICODate = Now()
        'Else
        '   initialFICODate = Rs.Fields("INITIAL_FICO_DATE").Value
        'End If
        
        If Not IsDate(Rs.Fields("INITIAL_FICO_DATE").Value) Then
            initialFICODate = Now()
        Else
            initialFICODate = Rs.Fields("INITIAL_FICO_DATE").Value
        End If
        initialFICODate = Month(initialFICODate) & "/" & Day(initialFICODate) & "/" & Year(initialFICODate)
        currentFICO = Rs.Fields("CURRENT_FICO").Value
        ''''If IsNull(Rs.Fields("CURRENT_FICO_DATE").Value) Or Len(Rs.Fields("CURRENT_FICO_DATE").Value) = 0 Then
        '''PB
        '     If Rs.Fields("CURRENT_FICO_DATE").Value Is Nothing Or Len(Rs.Fields("CURRENT_FICO_DATE").Value) = 0 Then
        '	
        '   currentFICODate = Now()
        'Else
        '    currentFICODate = Rs.Fields("CURRENT_FICO_DATE").Value
        'End If
        
        If Not IsDate(Rs.Fields("CURRENT_FICO_DATE").Value) Then
            currentFICODate = Now()
        Else
            currentFICODate = Rs.Fields("CURRENT_FICO_DATE").Value
        End If
        
        
        currentFICODate = Month(currentFICODate) & "/" & Day(currentFICODate) & "/" & Year(currentFICODate)
        counselingDurationType = Rs.Fields("COUNSELING_DURATION_TYPE_CODE").Value
        mthlyNetIncomeType = Rs.Fields("MTHLY_NET_INCOME_TYPE_CODE").Value
        mthlyExpenseType = Rs.Fields("MTHLY_EXPENSE_TYPE_CODE").Value
        firstMortgageType = Rs.Fields("FIRST_MORTGAGE_TYPE_CODE").Value
        creditScore = Rs.Fields("credit_score").Value
        mothersMaidenName = Rs.Fields("mothers_maiden_name").Value
        privacyConsentInd = Rs.Fields("privacy_consent_ind").Value
        priorityTypeCode = Rs.Fields("priority_type_code").Value
        secondaryContactNumber = Rs.Fields("secondary_contact_number").Value
        emailAddr = Rs.Fields("email_addr").Value
        bankruptcyInd = Rs.Fields("bankruptcy_ind").Value
        ownerOccupiedInd = Rs.Fields("owner_occupied_ind").Value
        bankruptcyAttorneyName = Rs.Fields("bankruptcy_attorney_name").Value
        primaryDfltRsnType = Rs.Fields("primary_dflt_rsn_type_code").Value
        secondaryDfltRsnType = Rs.Fields("secondary_dflt_rsn_type_code").Value
        hispanicInd = Rs.Fields("hispanic_ind").Value
        raceType = Rs.Fields("race_type_code").Value
        dupeInd = Rs.Fields("dupe_ind").Value
        age = Rs.Fields("age").Value
        genderType = Rs.Fields("gender_type_code").Value
        householdIncome = Rs.Fields("household_income_amt").Value
        householdType = Rs.Fields("household_type_code").Value
        incomeCategoryType = Rs.Fields("income_category_type_code").Value
        intakeScoreType = Rs.Fields("intake_score_type_code").Value
        pitiAmt = Rs.Fields("piti_amt").Value
        propertyForSaleInd = Rs.Fields("PROPERTY_FOR_SALE_IND").Value
        fcNoticeRecdInd = Rs.Fields("FC_NOTICE_RECD_IND").Value
        '		listPriceAmt = iif(isnull(Rs.Fields("LIST_PRICE_AMT").Value),0,Rs.Fields("LIST_PRICE_AMT").Value)
        listPriceAmt = Rs.Fields("LIST_PRICE_AMT").Value
        realtyCompanyName = Rs.Fields("REALTY_COMPANY_NAME").Value
        armResetInd = Rs.Fields("ARM_RESET_IND").Value
        incomeEarnersType = Rs.Fields("INCOME_EARNERS_TYPE_CODE").Value
        summarySentOtherType = Rs.Fields("SUMMARY_SENT_OTHER_TYPE_CODE").Value
        summarySentOtherDate = Rs.Fields("SUMMARY_SENT_OTHER_DATE").Value
    End If
	
    If Not isValidRS(Rs) Then
        ' For Issue 64 Where Referral Data is to be Todays Date
        referralDate = Date.Now.ToString("d")
    End If
	
    If Not pitiAmt2 >= 0 Then
        pitiAmt2 = 0
    End If
    'response.Write primaryDfltRsnType
    'response.End
	
    Err.Clear()
	
    If formMode <> "ADD" And referralId & "" = "" Then
        Response.Write("Referral not found.")
        Response.End()
    End If
	
    obj = CreateObject("CCRC_Admin.CAdmin")
    If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If
	
    If InStr(1, userRoles, "83") > 0 Then
        RsAgencies = obj.Get_Program_Entities("", "3", True, "Y", sUserId, sSQL, iReturnCode, sMessage)
        If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Program_Entities", eRetPage, eRetBtnText) Then
            obj = Nothing
            Response.Clear()
            Response.End()
        End If
        If isValidRS(RsAgencies) Then
            RsAgencies.MoveFirst()
            Do While Not RsAgencies.EOF
                agencies = agencies & RsAgencies.Fields("PROGRAM_SEQ_ID").Value & "|" & RsAgencies.Fields("BSN_ENTITY_SEQ_ID").Value & "|" & RsAgencies.Fields("BSN_ENTITY_NAME").Value & "^"
                RsAgencies.MoveNext()
            Loop
        End If
		
        RsCounselors = obj.Get_Entity_Type_Users("3", sUserId, sSQL, iReturnCode, sMessage)
        If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Entity_Type_Users", eRetPage, eRetBtnText) Then
            obj = Nothing
            Response.Clear()
            Response.End()
        End If
        If isValidRS(RsCounselors) Then
            RsCounselors.MoveFirst()
            Do While Not RsCounselors.EOF
                counselors = counselors & RsCounselors.Fields("BSN_ENTITY_SEQ_ID").Value & "|" & RsCounselors.Fields("CCRC_USER_SEQ_ID").Value & "|" & RsCounselors.Fields("LAST_NAME").Value & ", " & RsCounselors.Fields("FIRST_NAME").Value & "^"
                RsCounselors.MoveNext()
            Loop
        End If
    End If
		
    If InStr(1, userRoles, "83") = 0 Then
        '		If formMode = "EDIT" Then
        RsPrograms = obj.Get_User_Programs(entityId, sUserId, "N", sSQL, iReturnCode, sMessage)
        '		Else
        '			RsPrograms = obj.Get_User_Programs(entityId, sUserId, "Y", sSQL, iReturnCode, sMessage)
        '		End If
    Else
        RsPrograms = obj.Get_Programs(sUserId, sSQL, iReturnCode, sMessage)
    End If
    If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_User_Programs", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If

    'disable edits if not a Counselor or Super User
    disable = True
    If InStr(1, userRoles, "83") > 0 Or InStr(1, userRoles, "85") > 0 Then
        disable = False
    End If
	
    '	userRoles = "85"
    '	response.Write "{" & dupeInd & "}<BR>"
    '	response.Write "{" & dupeInd<>"Y" & "}<BR>"
    '	response.End
    '    If InStr(1,userRoles,"83") > 0 Then
    '		disable = false
    '    ElseIf InStr(1,userRoles,"85") > 0 And dupeInd <> "Y" Then
    '    ElseIf InStr(1,userRoles,"85") > 0 Then
    '		disable = false
    '	End If
	
    RsServicers = obj.Get_Servicers(referralId, sUserId, sSQL, iReturnCode, sMessage)
    If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Servicers", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If

    RsServicers2 = obj.Get_Servicers(referralId, sUserId, sSQL, iReturnCode, sMessage)
    If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Servicers", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If

    '	RsLocations = obj.Get_Entity_Locations("", "2", sUserId, sSQL, iReturnCode, sMessage)	
    '	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Entity_Locations", eRetPage, eRetBtnText) Then
    '		obj = Nothing
    '		Response.Clear 
    '		Response.End 
    '	End If	

    RsData = obj.Get_Programs_And_Servicers_And_Locations(referralId, sUserId, sSQL, iReturnCode, sMessage)
    If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Programs_And_Servicers_And_Locations", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If
    obj = Nothing
	
    Dim program, theServicer
    If isValidRS(RsData) Then
        RsData.MoveFirst()
        Do While Not RsData.EOF
            program = RsData.Fields("PROGRAM_NAME").Value
            Do While Not RsData.EOF
                If program = RsData.Fields("PROGRAM_NAME").Value Then
                    theServicer = RsData.Fields("BSN_ENTITY_NAME").Value
                    servicers = servicers & RsData.Fields("PROGRAM_SEQ_ID").Value & "|" & RsData.Fields("BSN_ENTITY_SEQ_ID").Value & "|" & RsData.Fields("BSN_ENTITY_NAME").Value & "|" & RsData.Fields("CONTACT_NAME").Value & "|" & RsData.Fields("CONTACT_EMAIL").Value & "|" & RsData.Fields("PHONE").Value & "^"
                Else
                    Exit Do
                End If
                Do While Not RsData.EOF
                    If theServicer = RsData.Fields("BSN_ENTITY_NAME").Value And program = RsData.Fields("PROGRAM_NAME").Value Then
                        '						locations = locations & RsData.Fields("PROGRAM_SEQ_ID").Value & "|" & RsData.Fields("BSN_ENTITY_SEQ_ID").Value & "|" & RsData.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value & "|" & RsData.Fields("LOCTN_NAME").Value & "^"
                        RsData.MoveNext()
                    Else
                        Exit Do
                    End If
                Loop
            Loop
        Loop
        RsData.Close()
    End If
    RsData = Nothing
    servicers = Left(servicers, Len(servicers) - 1)
    '	locations = Left(locations, Len(locations) - 1)
    obj = Nothing

    obj = CreateObject("CCRC_Main.CMain")
    If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
        obj = Nothing
        Response.Clear()
        Response.End()
    End If
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Referral</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<script language="JavaScript" src="/ccrc/utilities/validation.js"></script>
			<script language="JavaScript" src="/ccrc/utilities/detectChanges.js"></script>
			<SCRIPT LANGUAGE="JavaScript" SRC="/ccrc/utilities/calendar.js"></SCRIPT>
			<script language="JavaScript">
				var bSubmitForm = false;
				var bShowCalendar = false;
				//var initialValues;			
				document.onkeydown = handleKeyDown;
				function handleKeyDown() {
					switch (window.event.keyCode) {
						case 13: 		//enter
							submitForm();
							break;
						case 8:			//backspace
							window.event.keyCode = 0;
							break;
					}
					return;
				}
			
				function onLoad() {
					if("False" == document.all.disable.value) 
					{
					    if (document.all.program != null)
					    {
						if("" != document.all.program.value)
							program_OnChange();
						}
						if (document.all.newServicerRow != null)
					    {
						if("" == document.all.newServicerRow.style.display)
							document.all.newServicerRow.style.display = "none";
						}
					}
					initialValues = buildValues(document.myform);
					
				}

				function addServicerAndLocation() {
//						document.all.contactInfo.value = "";
						document.all.existingServicerRow.style.display = "none";
						document.all.newServicerRow.style.display = "";
						document.myform.newLocation.value = "Default";
						document.myform.newServicer.focus();
				}

				function cancelAddServicer2() {
						document.all.existingServicerRow.style.display = "";
						document.all.newServicerRow.style.display = "none";
						program_OnChange();						
				}
				
				function unLoad() {
					if(false == bSubmitForm && false == bShowCalendar) {
						if(true == dataChanged(document.myform))
							event.returnValue = "Data has changed.";
					}
					if(true == bShowCalendar)
						bShowCalendar = false;
				}				
		
				function isDefined(s) {
					return "undefined" != typeof(s);
				}

				var helpWindow = null;
				function popupHelp(serverName) {
					if (null == helpWindow || helpWindow.closed) {
						helpWindow = window.open('http://' + serverName + '/ccrc/help/session_rating.htm','help','toolbar=no,status=no,location=no,menubar=no,dependent=yes,resizable=yes,scrollbars=yes,width=400,height=500,left=0,top=0');
					} else {
						helpWindow.location.href = "http://" + serverName + "/ccrc/help/session_rating.htm";						
						helpWindow.focus();
					}
				}
				
				function resultClick(url) {
					if(dataChanged(document.myform))
						alert("Data has changed.  Click 'Save' before entering a session result.");
					else
						location.href = url;
				}
				
				function mySetDateField(myDateField) {
					bShowCalendar = true;
					if('referralDate' == myDateField)
						setDateField(document.myform.referralDate);
					else if('summaryRptSentDate' == myDateField)
						setDateField(document.myform.summaryRptSentDate);
					else if('referralResultDate' == myDateField)
						setDateField(document.myform.referralResultDate);
					else if('summarySentOtherDate' == myDateField)
						setDateField(document.myform.summarySentOtherDate);
					else if('referraltermdate' == myDateField)
						setDateField(document.myform.referraltermdate);
						
//					else if('initialFICODate' == myDateField)
//						setDateField(document.myform.initialFICODate);
//					else if('currentFICODate' == myDateField)
//						setDateField(document.myform.currentFICODate);
				}		
				
				function setOptionValue(s) {
					document.myform.hiddenSessionRating.value = s;
				}

				function cancelForm() {
					location.replace("search.aspx?formMode=SEARCH");
				}				
				
				function submitForm() {
					var frm = document.myform;
					
					if (bSubmitForm) 
						return true;
					else
						bSubmitForm = true;
												
					if (validate()) 
					{
						if (dataChanged(frm)) 
						{
							document.myform.submit();
							return true;
						}
						else
						{
							bSubmitForm = false;
						}
					}
					else
					{
						bSubmitForm = false;
						alert(document.all.results.innerText);
					}
					return false;					
				}

				function RequiredFieldValidate(refdate)
				{
				 	//alert(refdate);
				    	var datDate1=Date.parse('8/15/2008');
                    			var one_day=1000*60*60*24
                    			var datDate2= Date.parse(refdate);
                    			if (((datDate2-datDate1)/(24*60*60*1000)) > 0)
                    			{
                        			//alert(((datDate2-datDate1)/(24*60*60*1000)));
                        			return true;
                    			}
                    			else
					{
						//alert(((datDate2-datDate1)/(24*60*60*1000)));
                        			return false;
					}
				}


				
				function ValidateRefDate(refdate)
				{
				    var datDate1=new Date()
                    var one_day=1000*60*60*24
                    var datDate2= Date.parse(refdate);
                    if (((datDate2-datDate1)/(24*60*60*1000)) > 0)
                    {
                        //alert("Referral data cannot be greater than Today.")
                        document.all.results.innerText = "Referral data cannot be greater than Today.";
                        return false;
                    }
                    else
                        return true;
				}
  				
				function validate() 
				{
					var pv = new Validator();
					if (!ValidateRefDate(document.myform.referralDate.value))
					        return false;
					with (document.myform) 
					{					
					
					    pv.addField(document.myform.privacyConsentInd);
						
						pv.addField(document.myform.priorityTypeCode);
						pv.addField(document.myform.referralSourceTypeCode);
						pv.addField(document.myform.program);
						pv.addField(document.myform.agencyId);
						pv.addField(document.myform.counselorId);

						if("False" == document.all.disable.value)
						{
							if("" == document.all.newServicerRow.style.display)
							{						
								pv.addField(document.myform.newServicer);
//								pv.addField(document.myform.newLocation);
							}
							else
							{
								pv.addField(document.myform.servicer);
//								pv.addField(document.myform.theLocation);
							}
						}
						else
						{
							pv.addField(document.myform.servicer);
//							pv.addField(document.myform.theLocation);
						}
						
						pv.addField(document.myform.firstMortgageType);
						pv.addField(document.myform.armResetInd);
						pv.addField(document.myform.loanId);
						pv.addField(document.myform.pitiAmt);
						pv.addField(document.myform.firstContactStatusType);
						pv.addField(document.myform.servicer2);
						pv.addField(document.myform.secondMortgageType);
						pv.addField(document.myform.varloanId2);
						pv.addField(document.myform.pitiAmt2);
						pv.addField(document.myform.secondLoanStatusTypeCode);
                        pv.addField(document.myform.propertyForSaleInd);
                        // Newly Added
                        //pv.addField(document.myform.listPriceAmt);
                        pv.addField(document.myform.realtyCompanyName);
                        pv.addField(document.myform.fcNoticeRecdInd);
                        pv.addField(document.myform.bankruptcyInd);
                        pv.addField(document.myform.mthlyNetIncomeType);
                        pv.addField(document.myform.householdIncome);
                        pv.addField(document.myform.incomeEarnersType);
                        pv.addField(document.myform.mthlyExpenseType);
                        
						if(isDefined(document.myform.hidloanId))
						{
							pv.addField(document.myform.hidloanId);
							pv.addField(document.myform.hidlastName);							
							pv.addField(document.myform.hidfirstName);							
							pv.addField(document.myform.hidaddrLine1);							
							pv.addField(document.myform.hidcity);							
							pv.addField(document.myform.hidstate);
							pv.addField(document.myform.hidzipCode);							
						}
						
							
						pv.addField(document.myform.lastName);							
						pv.addField(document.myform.firstName);	
						pv.addField(document.myform.addrLine1);							
						pv.addField(document.myform.city);							
						pv.addField(document.myform.state);
						
						pv.addField(document.myform.zipCode);
						
						pv.addField(document.myform.secondaryContactNumber);
                        pv.addField(document.myform.emailAddr);
                        pv.addField(document.myform.age);
						pv.addField(document.myform.genderType);
						pv.addField(document.myform.hispanicInd);
						pv.addField(document.myform.raceType);
						pv.addField(document.myform.ownerOccupiedInd);
						pv.addField(document.myform.occupants);
						pv.addField(document.myform.householdType);
						pv.addField(document.myform.referralDate);
						pv.addField(document.myform.referralType);
						pv.addField(document.myform.primaryDfltRsnType);
						pv.addField(document.myform.secondaryDfltRsnType);
						
						if(isDefined(document.myform.referralResultType))
						{
							if("3" == document.myform.referralResultType.value)
							{
								document.all.results.innerText = "Selected Referral Result is no longer valid.  Please choose a different one.";
								return false;
							}
							pv.addField(document.myform.referralResultType);
							pv.addField(document.myform.referralResultDate);
						}

						if (RequiredFieldValidate(document.myform.referralDate.value))
						{
						     document.myform.hUDOutcome.required='true';
						     document.myform.discussedSolutionWithSrvcr.required='true';
						     document.myform.workedWithAnotherAgencyInd.required='true';
						}
						else
						{
						        document.myform.hUDOutcome.required='false';
						     document.myform.discussedSolutionWithSrvcr.required='false';
						     document.myform.workedWithAnotherAgencyInd.required='false';
						}


                        			pv.addField(document.myform.hUDOutcome);
						pv.addField(document.myform.hUDTermiReason);
						pv.addField(document.myform.referraltermdate);
						
						pv.addField(document.myform.contactedServicerInd);
						// New Mortgage Field
						pv.addField(document.myform.discussedSolutionWithSrvcr);
						pv.addField(document.myform.workedWithAnotherAgencyInd);
						pv.addField(document.myform.counselingDurationType);
						
						
						pv.addField(document.myform.sessionType);
						
						
						
						
						
						
						
						
						
						
						
//						pv.addField(document.myform.successStoryInd);
//						pv.addField(document.myform.initialFICO);
//						pv.addField(document.myform.initialFICODate);
//						pv.addField(document.myform.currentFICO);
//						pv.addField(document.myform.currentFICODate);
						
						
						
						
//						pv.addField(document.myform.summaryRptSentDate);
						
						
						
						
						
						
						

						
						
//						pv.addField(document.myform.incomeCategoryType);
						
						
						
						
						


						if("N" == document.myform.privacyConsentInd.value && "18601" != document.myform.servicer.value)
						{
							document.all.results.innerText = "Servicer must be '*No Consent' if Privacy Consent is 'No'";
							return false;
						}
						
						if("Y" == document.myform.propertyForSaleInd.value)
						{
							if("" == document.myform.realtyCompanyName.value || "" == document.myform.listPriceAmt.value)
							{
								document.all.results.innerText = "List Price of Home and Realty Company Name must have values.";
								return false;
							}
						}
						

						if("" != document.myform.summarySentOtherType.value)
						{
							if("" == document.myform.summarySentOtherDate.value)
							{
								document.all.results.innerText = "Date Counseling summary sent by alternate method must have a value.";
								return false;
							}						
						}						
																		
					}
					return pv.validate(document.all.results);
				}
				
				function deleteReferral() 
				{
					document.myform.formMode.value = "DELETE";
					location.replace("referral_process.aspx?formMode=DELETE&referralId=" + document.myform.referralId.value);
				}

				function program_OnChange()
				{
					var rows;
					var cols;
					var opt;

//					if(isDefined(document.all.counselorId))
					if(document.all.userRoles.value.indexOf("83") > -1)
					{
						var agency = document.all.agencyId.value;

						//clear servicer options
						document.myform.agencyId.innerHTML = "";
						
						opt = 0;
						rows = document.myform.agencies.value.split("^");
						for(i = 0; i <= rows.length - 1; i++)
						{
							cols = rows[i].split("|");
							if(cols[0] == document.myform.program.value)
							{
								var myNewOption = new Option(cols[2],cols[1]);
								document.myform.agencyId.options[opt] = myNewOption;
								if(cols[1] == agency)
									document.myform.agencyId.options[opt].selected = true;
								opt++;
							}							
						}
						agency_OnChange();
					}
					
					if("N" == document.myform.program[document.myform.program.selectedIndex].tag)
					{
						document.all.existingServicerRow.style.display = "";
						document.all.newServicerRow.style.display = "none";
						if(isDefined(document.all.addServicer))
							document.all.addServicer.style.display="none";
					}
					else
					{
						if(isDefined(document.all.addServicer))
							document.all.addServicer.style.display="";
					}
										
					if(document.all.existingServicerRow.style.display == "")
					{
						var servicer = document.all.servicer.value;					

						//clear servicer options
						document.myform.servicer.innerHTML = "";
						//alert(servicer);
						opt = 0;
						rows = document.myform.servicers.value.split("^");
						for(i = 0; i <= rows.length - 1; i++)
						{
							cols = rows[i].split("|");
							if(cols[0] == document.myform.program.value)
							{
								var myNewOption = new Option(cols[2],cols[1]);
								document.myform.servicer.options[opt] = myNewOption;
								if(cols[1] == servicer)
									document.myform.servicer.options[opt].selected = true;
								opt++;
							}							
						}
						servicer_OnChange();
					}
				}
				
				function servicer_OnChange()
				{
//					var rows;
//					var cols;
//					var opt;

//					var location = document.all.theLocation.value;
										
					//clear options
//					document.myform.theLocation.innerHTML = "";
					
//					opt = 0;
//					rows = document.myform.locations.value.split("^");
//					for(i = 0; i <= rows.length - 1; i++)
//					{
//						cols = rows[i].split("|");
//						if(cols[0] == document.myform.program.value &&
//							cols[1] == document.myform.servicer.value)
//						{
//							var myNewOption = new Option(cols[3],cols[2]);
//							document.myform.theLocation.options[opt] = myNewOption;
//							if(cols[2] == location)
//								document.myform.theLocation.options[opt].selected = true;
//							opt++;
//						}							
//					}

//					if(isDefined(document.all.contactInfo))
//					{
//						document.all.contactInfo.innerHTML= "";
//						rows = document.myform.servicers.value.split("^");
//						for(i = 0; i <= rows.length - 1; i++)
//						{
//							cols = rows[i].split("|");
//							if(cols[1] == document.myform.servicer.value)
//							{
//								if("" == cols[3])
//									document.all.contactInfo.innerHTML = "";
//								else
//									document.all.contactInfo.innerHTML = "Servicer contact info: " + cols[3] + ", " + cols[4] + ", " + cols[5];
//							}
//						}
//					}
				}
				
				function agency_OnChange()
				{
					var rows;
					var cols;
					var opt;
					var counselor ;
					if( document.getElementById("counselorId")!=null)  // if user is not in 83 role then counselorId will not genarate
					{
				    	counselor = document.all.counselorId.value;
    					//clear options
					    document.myform.counselorId.innerHTML = "";
					}
					opt = 0;
					rows = document.myform.counselors.value.split("^");
					for(i = 0; i <= rows.length - 1; i++)
					{
						cols = rows[i].split("|");
						if(cols[0] == document.myform.agencyId.value)
						{
							var myNewOption = new Option(cols[2],cols[1]);
							document.myform.counselorId.options[opt] = myNewOption;
							if(cols[1] == counselor)
								document.myform.counselorId.options[opt].selected = true;
							opt++;
						}							
					}					
				}				
				
				function bankruptcy_OnChange()
				{
					if("Y" == document.myform.bankruptcyInd.value)
					{
						document.myform.bankruptcyAttorneyName.disabled = false;
					}
					else
					{
						document.myform.bankruptcyAttorneyName.value = "";
						document.myform.bankruptcyAttorneyName.disabled = true;
					}
				}								
			</script>
	</head>
	<body onload="javascript:onLoad();" onbeforeunload="javascript:unLoad();">
		<FORM name="myform" id="myform" method="post" action="referral_process.aspx">
			<INPUT TYPE=hidden VALUE="<%=formMode%>" NAME="formMode" ID="formMode"> 
			<INPUT TYPE=hidden VALUE="<%=userRoles%>" NAME="userRoles" ID="userRoles">
			<INPUT TYPE=hidden VALUE="<%=referralId%>" NAME="referralId" ID="referralId">
			<INPUT TYPE=hidden VALUE="<%=disable%>" NAME="disable" ID="disable"> 
			<INPUT TYPE=hidden VALUE="<%=internal%>" NAME="internal" ID="internal">
			<INPUT TYPE=hidden NAME="servicers" ID="servicers" VALUE="<%=servicers%>">
			<INPUT TYPE=hidden NAME="locations" ID="locations" VALUE="<%=locations%>">
			<INPUT TYPE=hidden NAME="agencies" ID="agencies" VALUE="<%=agencies%>">
			<INPUT TYPE=hidden NAME="counselors" ID="counselors" VALUE="<%=counselors%>">
			<INPUT TYPE=hidden VALUE="<%=sessionRatingCode%>" NAME="hiddenSessionRating" ID="hiddenSessionRating" columnName="SESSION_RATING_TYPE_CODE" columnType="string">
			 
				<INPUT TYPE=hidden NAME="hidloanId" ID="hidloanId" VALUE="<%=varloanId%>">
				<INPUT TYPE=hidden NAME="hidlastName" ID="hidlastName" VALUE="<%=varlastName%>">
				<INPUT TYPE=hidden NAME="hidfirstName" ID="hidfirstName" VALUE="<%=firstName%>">
				<INPUT TYPE=hidden NAME="hidmiName" ID="hidmiName" VALUE="<%=miName%>">
				<INPUT TYPE=hidden NAME="hidaddrLine1" ID="hidaddrLine1" VALUE="<%=varaddrLine1%>">
				<INPUT TYPE=hidden NAME="hidcity" ID="hidcity" VALUE="<%=city%>">
				<INPUT TYPE=hidden NAME="hidstate" ID="hidstate" VALUE="<%=state%>">
				<INPUT TYPE=hidden NAME="hidzipCode" ID="hidzipCode" VALUE="<%=zipCode%>">
			 

	<table border="0" class="pageBody" ID="Table2" width="600" >
		<tr><td><h1>Referral: <%=varloanId%>/<%=zipCode%><%=IIf(dupeInd.ToString() = "Y", ": Duplicate", "")%></h1></td></tr>
				<%
				Response.Write ("<tr><td><div style='overflow: auto;'>")
				Response.Write ("<table border='0' >")
				response.Write ("<tr><td width='30%'></td><td width='70%'></td></tr>")
				'if user is a counselor, display message if the referral is being 
				' worked on by another agency
				 If InStr(1,userRoles,"85") > 0 Then
					If formMode <> "ADD" Then		
						If CInt(entityId) <> CInt(Rs.Fields("AGENCY_SEQ_ID").Value) Then
							
				                Dim strTemp = "<tr><td colspan='2'>Loan: " & Rs.Fields("LOAN_ID").value & ", Zip Code: " & Rs.Fields("ZIP").value & " - case being worked on by " & buildName(Rs.Fields("COUNSELOR_FIRST_NAME").value, Rs.Fields("COUNSELOR_MID_INIT_NAME").value, Rs.Fields("COUNSELOR_LAST_NAME").value, Rs.Fields("COUNSELOR_NAME_SUFFIX_CODE").value) & "/" & Rs.Fields("PRIMARY_PHN").value & " of " & Rs.Fields("BSN_ENTITY_NAME").value & "/" & Rs.Fields("PHONE").value & "<br>Last outcome date: " & Rs.Fields("REFERRAL_RESULT_DATE").value & "<br>Last outcome: " & Rs.Fields("REFERRAL_RESULT_TYPE").value & "</td></tr>"
				                Response.Write(strTemp)
				                Response.Write("</td></tr></table></FORM></body></html>")
				                obj = Nothing
							Rs.Close
							Rs = Nothing
				                Response.End()
						End If
					End If
				End If%>

				<tr>
					<td><font class="labelWithHelp" title="Privacy consent agreed to?">Privacy consent agreed to?:</font></td>
					<td><%=privacyConsentIndSelect("privacyConsentInd", privacyConsentInd, disable, "true", "Privacy consent agreed to", "")%></td>
				</tr>

				<tr>
					<td><font class="label" title="">Priority Type:</font></td>
					<td><%=priorityTypeSelect("priorityTypeCode", priorityTypeCode, disable, "true", "Priority Type", "")%></td>
				</tr>

				<tr>
					<td><font class="label" title="">How did you hear about us?:</font></td>
					<td><%=referralSourceSelect("referralSourceTypeCode", referralSourceTypeCode, disable, "true", "Referral Source", "")%></td>
				</tr>

				<tr>
					<td><font class="label" title="">Program:</font></td>
					<td><%=programSelect("program", theProgram, disable, "true", "Program", "Program is required.", RsPrograms)%></td>
				</tr>
				
				<%If InStr(1,userRoles,"83") > 0 Then%>				
					<tr>
						<td><font class="label" title="">Agency:</font></td>
						<td>
							<%=agencySelect("agencyId", agencyId, disable, true, "Agency", "Agency is required", RsAgencies)%>&nbsp;
						</td>
					</tr>
					<tr>
						<td><font class="label" title="">Counselor:</font></td>
						<td>
							<%=counselorSelect("counselorId", counselorId, disable, true, "counselorId", "counselorId is required", RsCounselors)%>&nbsp;
						</td>
					</tr>
				<%Else%>
					<%If formMode = "EDIT" Then%>
						<tr>
							<td class="label">Agency/Counselor:</td>
							<td><%=agencyName%>/<%=counselorName%>
												<INPUT TYPE=hidden VALUE="<%=agencyId%>" NAME="agencyId" ID="agencyId"> 
                            					<INPUT TYPE=hidden VALUE="<%=counselorId%>" NAME="counselorId" ID="counselorId"> 

							</td>
						</tr>
					<%End If%>				
				<%End If%>				
				<tr id="existingServicerRow" name="existingServicerRow">
					<td><font class="label" title="">1st Mortgage/Servicer:</font></td>
					<td>
						<%=servicerSelect("servicer", servicer, disable, "true", "Servicer", "1st Mortgage/Servicer is required", RsServicers)%>&nbsp;
						<INPUT TYPE=hidden VALUE="<%=theLocation%>" NAME="theLocation" ID="theLocation"> 
<!--						
						<==locationSelect("theLocation", theLocation, disable, "true", "Location", "Location is required", RsLocations)%>
-->						
<!--						
						<input name="addServicer" id="addServicer" value="Add" class="btn" type="button" accesskey="a" onclick="addServicerAndLocation();" title="Add new servicer" style="display:<%=iif(instr(1,userRoles,"83")>0,"",iif(instr(1,userRoles,"85")>0,"","none"))%>">
-->						
					</td>
				</tr>
				<tr>
					<td><font class="label">1st Mortgage Type (current rate):</font></td>
					<td><%=firstMortgageTypeSelect("firstMortgageType", firstMortgageType, disable, "true", "", "1st Mortgage Type is required.")%></td>
				</tr>	
				
				<tr>
					<td><font class="label" title="">Has interest reset on ARM loan?:</font></td>
					<td><%=armResetIndSelect("armResetInd", armResetInd, disable, "true", "Arm ReIndicator", "Arm ReIndicator is required")%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">1st Mortgage loan #:</font></td>
					<td>
						<%If disable Or internal Then
						        Response.Write(varloanId)
						Else%>
						<input detectChanges maxlength="25" type="text" id="loanId" name="loanId" value="<%=varloanId%>" required="true" datatype="anytext" errorlbl="Loan Id" errormsg="Loan id is required.">
						<%End If%>
					</td>
				</tr>
				
				<tr>
					<td><font class="label" title="">1st Mortgage monthly payment (PITI):</font></td>
					<td>
						<%If disable Then%>
						<%=pitiAmt%>
						<%Else%>
						<input maxlength="15" detectChanges type="text" id="pitiAmt" name="pitiAmt" value="<%=pitiAmt%>" required="true" datatype="positiveinteger" errorlbl="PITI at Intake" errormsg="1st Mortgage monthly payment (PITI) is required and must be a positive integer.">
						<%End If%>
					</td>
				</tr>
				
				<tr>
					<td><font class="label" title="">1st Mortgage Loan Status:</font></td>
					<td><%=firstContactStatusTypeSelect("firstContactStatusType", firstContactStatusTypeCode, disable, "true", "1st Mortgage Loan Status", "")%></td>
				</tr>
				<tr id="ServicerRow2" name="ServicerRow2">
					<td><font class="label" title="">2nd Mortgage/Servicer:</font></td>
					<td><%=servicerSelect2("servicer2", servicer2, disable, "false", "", "", RsServicers2)%>&nbsp;</td>
				</tr>
				<tr id="ServicerTypeRow2" name="ServicerTypeRow2">
					<td><font class="label" title="">2nd Mortgage type:</font></td>
					<td><%=secondMortgageTypeSelect("secondMortgageType", secondMortgageType, disable, "false", "", "")%></td>
				</tr>
				<tr id="MortgageLoan2" name="MortgageLoan2">
					<td><font class="label" title="">2nd Mortgage loan #:</font></td>
					<td><input detectChanges maxlength="25" type="text" id="varloanId2" name="varloanId2" value="<%=varloanId2%>" required="false" datatype="anytext" errorlbl="" errormsg=""></td>
				</tr>
				<tr id="MortgageMonthlyPayment2" name="MortgageMonthlyPayment2">
					<td><font class="label" title="">2nd Mortgage monthly payment:</font></td>
					<td>
						<input maxlength="15" detectChanges type="text" id="pitiAmt2" name="pitiAmt2" value="<%=pitiAmt2%>" required="false" datatype="integer" errorlbl="PITI2 at Intake" errormsg="PITI2 must be a positive integer."/>
					</td>
				</tr>
				<tr id="MortgageLoanStatus2" name="MortgageLoanStatus2">
					<td><font class="label" title="">2nd Mortgage loan status:</font></td>
					<td><%=secondContactStatusTypeSelect("secondLoanStatusTypeCode", secondLoanStatusTypeCode, disable, "false", "", "")%></td>

				</tr>
				
				<%If Not disable Then%>
					<tr id="newServicerRow" name="newServicerRow">
						<td><font class="label" title="">Servicer</font></td>
						<td>
							<input detectChanges type="text" id="newServicer" name="newServicer" value="" datatype="anytext" required="true"  errorlbl="New Servicer" errormsg="New Servicer is required.">
							<INPUT TYPE=hidden VALUE="<%=newLocation%>" NAME="newLocation" ID="newLocation"> 
<!--							
							<input detectChanges type="text" id="newLocation" name="newLocation" value="" datatype="anytext" required="true"  errorlbl="New Location" errormsg="New Location is required.">
-->							
							<input name="cancelAddServicer" id="cancelAddServicer" value="Cancel" class="btn" type="button" accesskey="c" onclick="cancelAddServicer2();" title="Cancel adding new servicer" style="display:<%=iif(instr(1,userRoles,"83")>0,"",iif(instr(1,userRoles,"85")>0,"","none"))%>">
						</td>
					</tr>
				<%End If%>
				
				
				<tr>
					<td><font class="label" title="">Property for sale?:</font></td>
					<td><%=propertyForSaleIndSelect("propertyForSaleInd", propertyForSaleInd, disable, "true", "Property For Sale Indicator", "Property For Sale Indicator is required")%></td>
				</tr>
				
				<tr>
					<td><font class="label" title="">List Price of Home:</font></td>
					<td>
						<%If disable Then%>
						<%=listPriceAmt%>
						<%Else%>
						<input maxlength="15" detectChanges type="text" id="listPriceAmt" name="listPriceAmt" value="<%=listPriceAmt%>" required="false" datatype="positiveinteger" errorlbl="" errormsg="List Price Amt must be a positive integer value.">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Realty Company Name:</font></td>
					<td>
						<%If disable Then%>
						<%=realtyCompanyName%>
						<%Else%>
						<input maxlength="50" detectChanges type="text" id="realtyCompanyName" name="realtyCompanyName" value="<%=realtyCompanyName%>" required="false" datatype="anytext" errorlbl="" errormsg="">
						<%End If%>
					</td>
				</tr>				
				<tr>
					<td><font class="label" title="">Has notice of Foreclosure sale been received?:</font></td>
					<td><%=fcNoticeRecdIndSelect("fcNoticeRecdInd", fcNoticeRecdInd, "true", disable, "Foreclosure Sale Notice Received Indicator", "Foreclosure Sale Notice Received Indicator is required")%></td>
				</tr>				
				
				<tr>
					<td><font class="label" title="">Bankruptcy?:</font></td>
					<td><%=bankruptcyIndSelect("bankruptcyInd", bankruptcyInd, disable, "true", "Bankruptcy", "")%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Bankruptcy Attorney:</font></td>
					<td>
						<%If disable Then%>
						<%=bankruptcyAttorneyName%>
						<%Else%>
						<input maxlength="50" detectChanges type="text" id="bankruptcyAttorneyName" name="bankruptcyAttorneyName" value="<%=bankruptcyAttorneyName%>" required="false" datatype="anytext" errorlbl="" errormsg="">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Credit Score:</font></td>
					<td>
						<%If disable Then%>
						<%=creditScore%>
						<%Else%>
						<input maxlength="3" detectChanges type="text" id="creditScore" name="creditScore" value="<%=creditScore%>" required="false" datatype="anytext" errorlbl="" errormsg="">
						<%End If%>
					</td>
				</tr>

				<tr>
					<td><font class="label">Intake Score Type:</font></td>
					<td><%=intakeScoreTypeSelect("intakeScoreType", intakeScoreType, disable, "false", "Intake Score", "")%></td>
				</tr>								
								
											
				<tr>
					<td><font class="label">Monthly Net Income:</font></td>
					<td><%=mthlyNetIncomeTypeSelect("mthlyNetIncomeType", mthlyNetIncomeType, disable, "true", "Monthly Net Income", "")%></td>
				</tr>	
				
				<tr>
					<td><font class="label" title="">Household Gross Annual Income:</font></td>
					<td>
						<%If disable Then%>
						<%=householdIncome%>
						<%Else%>
						<input maxlength="15" detectChanges type="text" id="householdIncome" name="householdIncome" value="<%=householdIncome%>" required="true" datatype="positiveinteger" errorlbl="Household Income" errormsg="Household Income is required and must be a positive integer.">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label">Income Category Type:</font></td>
					<td><%=incomeCategoryTypeSelect("incomeCategoryType", incomeCategoryType, disable, "true", "Income Category Type", "Income Category Type is required.")%></td>
				</tr>							
				<tr>
					<td><font class="label">Number of adults contributing to household income:</font></td>
					<td><%=incomeEarnersTypeSelect("incomeEarnersType", incomeEarnersType, disable, "true", "Income Earners Type", "Income Earners Type is required.")%></td>
				</tr>	

				<tr>
					<td><font class="label">Monthly Expenses:</font></td>
					<td><%=mthlyExpenseTypeSelect("mthlyExpenseType", mthlyExpenseType, disable, "true", "Monthly Expenses", "")%></td>
				</tr>				

				
				
				<tr>
					<td><font class="label" title="">Last/First/MI:</font></td>
					<td>
						<%If disable Or internal Then%>
						<%=varlastName%>,&nbsp;<%=firstName%>&nbsp;<%=miName%>
						<%Else%>
						<input detectChanges maxlength="30" type="text" id="lastName" name="lastName" value="<%=varlastName%>" datatype="anytext" required="true"  errorlbl="Last Name" errormsg="Last name is required.">
						<input detectChanges maxlength="30" type="text" id="firstName" name="firstName" value="<%=firstName%>" required="true" datatype="anytext" errorlbl="First Name" errormsg="First name is required.">
						<input detectChanges type="text" id="miName" name="miName" style="width: 20px" maxlength="1"  required="true"  value="<%=miName%>">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Address:</font></td>
					<td>
						<%If disable Or internal Then%>
						<%=varaddrLine1%>
						<%Else%>
						<input maxlength="50" detectChanges type="text" id="addrLine1" name="addrLine1" value="<%=varaddrLine1%>" required="true" datatype="anytext" errorlbl="Address" errormsg="Address is required.">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">City/State/Zip:</font></td>
					<td>
						<%If disable Or internal Then%>
						<%=city%>,&nbsp;<%=stateSelect("state", state, disable, internal, "true", "State", "State is required.")%>&nbsp;<%=zipCode%>
						<%Else%>
						<input type="text" detectChanges id="city" name="city" value="<%=city%>" required="true" maxlength="30" datatype="anytext" errorlbl="City" errormsg="City is required.">
						<%=stateSelect("state", state, disable, internal, "true", "State", "State is required.")%>
						<input detectChanges type="text" id="zipCode" name="zipCode" value="<%=zipCode%>" style="width: 50px" maxlength="5" required="true" datatype="anytext" errorlbl="Zip" errormsg="Zip is required.">
						<%End If%>
					</td>
				</tr>		
				<tr>
					<td><font class="label" title="">Phone:</font></td>
					<td>
						<%If disable Then%>
						<%=phn%>
						<%Else%>
						<input type="text" detectChanges id="phn" name="phn" value="<%=phn%>" required="false" maxlength="15" datatype="anytext">
<!--						
						<input type="text" detectChanges id="phn1" name="phn1" value="<=phn1%>" required="false" maxlength="3" style="width: 25px" datatype="anytext">-
						<input type="text" detectChanges id="phn2" name="phn2" value="<=phn2%>" required="false" maxlength="3" style="width: 25px" datatype="anytext">-
						<input type="text" detectChanges id="phn3" name="phn3" value="<=phn3%>" required="false" maxlength="4" style="width: 37px" datatype="anytext">
-->						
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">2nd Contact Number:</font></td>
					<td>
						<%If disable Then%>
							<%=secondaryContactNumber%>
						<%Else%>
							<input detectChanges maxlength="30" type="text" id="secondaryContactNumber" name="secondaryContactNumber" value="<%=secondaryContactNumber%>" datatype="anytext" required="true"  errorlbl="2nd Contact Number" errormsg="2nd Contact Number is required.">
						<%End If%>
					</td>
				</tr>				
				<tr>
					<td><font class="label" title="">Email Address:</font></td>
					<td>
						<%If disable Then%>
							<%=emailAddr%>
						<%Else%>
							<input detectChanges maxlength="50" type="text" id="emailAddr" name="emailAddr" value="<%=emailAddr%>" datatype="anytext" required="true"  errorlbl="Email Address" errormsg="Email Address is required.">
						<%End If%>
					</td>
				</tr>
				
				<%If InStr(1,userRoles,"83") <> 0 Or InStr(1,userRoles,"29") Or InStr(1,userRoles,"35") <> 0 Or InStr(1,userRoles,"84") <> 0  Or InStr(1,userRoles,"85") <> 0 Or InStr(1,userRoles,"123") <> 0 Or InStr(1,userRoles,"125") <> 0 Then%>
					<tr>
						<td><font class="label" title="">Age:</font></td>
						<td>
							<%If disable Then%>
								<%=age%>
							<%Else%>
								<input detectChanges maxlength="3" type="text" id="age" name="age" value="<%=age%>" datatype="positiveinteger" required="true"  errorlbl="Age" errormsg="Age is required.">
							<%End If%>
						</td>
					</tr>
					<tr>
						<td><font class="label" title="">Gender:</font></td>
						<td><%=genderTypeSelect("genderType", genderType, disable, "true", "Gender", "Gender is requiried")%></td>
					</tr>				
				<%Else%>
					<INPUT TYPE=hidden NAME="age" ID="Hidden1" VALUE="<%=age%>">
					<INPUT TYPE=hidden NAME="genderType" ID="genderType" VALUE="<%=genderType%>">
				<%End If%>
				<tr>
					<td><font class="label" title="">Mother's Maiden Name:</font></td>
					<td>
						<%If disable Then%>
							<%=mothersMaidenName%>
						<%Else%>
							<input detectChanges maxlength="30" type="text" id="Text1" name="mothersMaidenName" value="<%=mothersMaidenName%>" datatype="anytext" required="false"  errorlbl="" errormsg="">
						<%End If%>
					</td>
				</tr>

				<%If InStr(1,userRoles,"83") <> 0 Or InStr(1,userRoles,"29") Or InStr(1,userRoles,"35") <> 0 Or InStr(1,userRoles,"84") <> 0  Or InStr(1,userRoles,"85") <> 0 Or InStr(1,userRoles,"123") <> 0 Or InStr(1,userRoles,"125") <> 0 Then%>
					<tr>
						<td><font class="label" title="">Hispanic Indicator:</font></td>
						<td><%=hispanicIndSelect("hispanicInd", hispanicInd, disable, "true", "Hispanic Indicator", "")%></td>
					</tr>
					<tr>
						<td><font class="label">Race:</font></td>
						<td><%=raceTypeSelect("raceType", raceType, disable, "true", "Race", "")%></td>
					</tr>				
				<%Else%>
					<INPUT TYPE=hidden NAME="hispanicInd" ID="hispanicInd" VALUE="<%=hispanicInd%>">
					<INPUT TYPE=hidden NAME="raceType" ID="raceType" VALUE="<%=raceType%>">
				<%End If%>
				
				<tr>
					<td><font class="label" title="">Owner Occupied:</font></td>
					<td><%=ownerOccupiedIndSelect("ownerOccupiedInd", ownerOccupiedInd, disable, "true", "Owner Occupied", "")%></td>
				</tr>
				<tr>
					<td><font class="label" title=""># of People in House:</font></td>
					<td>
						<%If disable Then%>
						<%=occupants%>
						<%Else%>
						<input maxlength="2" style="width: 20px" detectChanges type="text" id="occupants" name="occupants" value="<%=occupants%>" required="true" datatype="anytext" errorlbl="Occupants" errormsg="# Occupants is required.">
						<%End If%>
					</td>
				</tr>
				
				<tr>
					<td><font class="label">Household Type:</font></td>
					<td><%=householdTypeSelect("householdType", householdType, disable, "true", "Household Type", "Household Type is required.")%></td>
				</tr>				

				<tr>
					<td><font class="label" title="">Agency Tracking Id:</font></td>
					<td>
						<%If disable Then%>
						<%=agencyTrackingId%>
						<%Else%>
						<input maxlength="15" detectChanges type="text" id="agencyTrackingId" name="agencyTrackingId" value="<%=agencyTrackingId%>">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Referral Received Date:</font></td>
					<td>
						<%If disable Then%>
						<%=referralDate%>
						<%Else%>
						<input detectChanges type="text" id="referralDate" name="referralDate" value="<%=referralDate%>" required="true"  datatype="date" errorlbl="Referral Received Date" errormsg="Referral received date is invalid.">
						<%
									Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""mySetDateField('referralDate'); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
									Response.Write ("(mm/dd/yyyy)")
								%>
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Referral Type:</font></td>
					<td><%=referralTypeSelect("referralType", referralTypeCode, disable, "true", "Referral Type", "")%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Referring Person's Name:</font></td>
					<td>
						<%If disable Then%>
						<%=approvedBy%>
						<%Else%>
						<input maxlength="50" detectChanges type="text" id="approvedBy" name="approvedBy" value="<%=approvedBy%>">
						<%End If%>
					</td>
				</tr>								
				
			<INPUT TYPE=hidden VALUE="<%=sessionTypeCode%>" NAME="sessionType" ID="sessionType" >
<!--				
				<tr>
					<td><font class="label" title="">Session Type:</font></td>
					<td><%=sessionTypeSelect("sessionType", sessionTypeCode, disable, "true", "Session Type", "")%></td>
				</tr>
-->					
							
				<tr>
					<td><font class="label" title="">Primary Default Reason Type:</font></td>
					<td><%=primaryDfltRsnTypeSelect("primaryDfltRsnType", primaryDfltRsnType, disable, "true", "Primary Default Reason Type", "")%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Secondary Default Reason Type:</font></td>
					<td><%=secondaryDfltRsnTypeSelect("secondaryDfltRsnType", secondaryDfltRsnType, disable, "true", "Secondary Default Reason Type", "")%></td>
				</tr>

			<%
			Dim referralResultType
			Rs = obj.Get_Referral_Results(referralId, sUserId, sSQL, iReturnCode, sMessage)
			If IsValidRS(Rs) Then
				Response.Write ("<tr><td colspan='2'><table style='position:relative;left:50;' border='1' cellpadding='1' cellspacing='1'>")
				Response.Write ("<tr><th width='150'>Result Type</th><th width='75'>Date</th><th width='75'>Action</th></tr>")
				If InStr(1,userRoles,"83") > 0 Or InStr(1,userRoles,"85") > 0 Then
					
			            url = """" & "referralResult.aspx?formMode=ADD&referralId=" & referralId & """"
					Response.Write ("<tr><td></td><td></td><td><input name='add' id='add' value='Add' class='btn' type='button' accesskey='a' onclick='javascript:resultClick(" & url & ");' title='Add new result.'></td></tr>")
				End If
				While Not Rs.eof
					Response.Write ("<tr>")
						Response.Write ("<td>")
			            Response.Write(Rs.Fields("DSCR").value)
						Response.Write ("</td>")
						Response.Write ("<td>")
			            Response.Write(Rs.Fields("REFERRAL_RESULT_DATE").Value)
						Response.Write ("</td>")
						If InStr(1,userRoles,"83") > 0 Or InStr(1,userRoles,"85") > 0 Then
'							url = """" & "referralResult.asp?formMode=EDIT&referralId=" & referralId & "&referralResultId=" & Rs.Fields("REF_RESULT_TYPE_HIST_SEQ_ID") & """"
'							Response.Write ("<td><input name='edit' id='edit' value='Edit' class='btn' type='button' accesskey='e' onclick='javascript:resultClick(" & url & ");' title='Edit result.'>")
			                url = """" & "referralResult_process.aspx?formMode=DELETE&referralId=" & referralId & "&referralResultId=" & Rs.Fields("REF_RESULT_TYPE_HIST_SEQ_ID").Value & """"
							If InStr(1,userRoles,"83") > 0 Or _
									(InStr(1,userRoles,"85") > 0 And _
									Rs.Fields("REFERRAL_RESULT_DATE") is Nothing) THEN ' Or _
									'Rs.Fields("REFERRAL_RESULT_DATE") = Format(Date, "YYYYMM"))) Then
								Response.Write ("<td><input name='delete' id='delete' value='Delete' class='btn' type='button' accesskey='d' onclick='javascript:resultClick(" & url & ");' title='Delete result.'></td>")
							End If
						End If
						If InStr(1,userRoles,"83") > 0 Then
			                Response.Write("<td>" & Rs.Fields("ACCT_PRD").Value & "<td>")
						End If
					Response.Write ("</tr>")
					Rs.movenext
				end  While
				Response.Write ("</td></tr></table>")
				Rs.close
			Else
				Response.Write ("<tr><td><font class='label' title=''>Referral Result:</font></td>")
				Response.Write ("<td>" & referralResultSelect4NewReferral("referralResultType", referralResultType, disable, "true", "Referral Result", "Referral Result is required.") & "</td></tr>")
				Response.Write ("<tr><td><font class='label' title=''>Result Date:</font></td>")
				Response.Write ("<td>")
				If disable Then
					Response.Write ("")
				Else
					Response.Write ("<input datatype='date' id='referralResultDate' required='true' detectChanges name='referralResultDate' value='" & Date.Now().ToString("d") & "' errorlbl='Result Date' errormsg='Result date is invalid.'>")
					Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""mySetDateField('referralResultDate'); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
					Response.Write ("(mm/dd/yyyy)")
				End If
				Response.Write ("</td></tr>")
			End If
			Rs = nothing
			%>			
			    <tr id="HUDOutcome" name="HUDOutcome">
					<td><font class="label" title=""</font>HUD Outcome:</td> 
					<td><%=HUDOutcomeTypeSelect("hUDOutcome", hUDOutcome, disable, "true", "", "HUD Outcome is required.")%></td> 
				</tr>
				<tr id="HUDTerminationReason" name="HUDTerminationReason">
					<td><font class="label" title=""</font>HUD Termination Reason:</td>
					<td><%=HUDTerminationReasonSelect("hUDTermiReason", hUDTermiReason, disable, "false", "", "")%></td>
				</tr>
				<tr id="HUDTerminationDate" name="HUDTerminationDate">
					<td><font class="label" title=""</font>HUD Termination Date:</td>
					<td>
					    <%If disable Then%>
						<%=referralHUDTermdate%>
						<%Else%>
						<input detectChanges type="text" id="referraltermdate" name="referraltermdate" value="<%=referralHUDTermdate%>" datatype="date" errorlbl="" errormsg="">
						<%
							Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""mySetDateField('referraltermdate'); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
							Response.Write ("(mm/dd/yyyy)")
							%>
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Date Counseling Summary sent to 3rd Party/Lender:</font></td>
					<td>
						<%If disable Then%>
						<%=summaryRptSentDate%>
						<%Else%>
						<input detectChanges type="text" id="summaryRptSentDate" name="summaryRptSentDate" value="<%=summaryRptSentDate%>" readonly datatype="date" errorlbl="System Email Sent Date" errormsg="Counseling summary report date is invalid.">
						<%End If%>
					</td>
				</tr>

				<tr>
					<td><font class="label" title="">Counseling summary sent by alternate method:</font></td>
					<td><%=summarySentOtherTypeSelect("summarySentOtherType", summarySentOtherType, disable, "false", "", "")%></td>
				</tr>				
				<tr>
					<td><font class="label" title="">Date Counseling summary sent by alternate method:</font></td>
					<td>
						<%If disable Then%>
						<%=summarySentOtherDate%>
						<%Else%>
						<input detectChanges type="text" id="summarySentOtherDate" name="summarySentOtherDate" value="<%=summarySentOtherDate%>" datatype="date" errorlbl="" errormsg="">
							<%
							Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""mySetDateField('summarySentOtherDate'); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
							Response.Write ("(mm/dd/yyyy)")
							%>
						<%End If%>
					</td>
				</tr>
				
				<INPUT TYPE=hidden VALUE="" NAME="referralComment" ID="referralComment">
<!--				
				<tr id="rowReferralComment">
					<td><font class="label" title="">Detail description of<br>problems<br>Consumer action plan/<br>Prepayment plan<br>(if any):</font></td>
					<td>
						<%If disable Then%>
						<%=referralComment%>
						<%Else%>
						<textarea detectChanges id="referralComment" cols="66" rows="5" NAME="referralComment"><%=referralComment%></textarea>
						<%End If%>
					</td>
				</tr>
-->
				<tr>
					<td><font class="labelWithHelp" title="Homeowner had actual conversation with Servicer about solutions/received workout pkg in last 30 days">Homeowner talked to servicer in last 30 days?:</font></td>
					<td><%=contactedServicerIndSelect("contactedServicerInd", contactedServicerInd, disable, "true", "Homeowner had actual conversation with Servicer about solutions/received workout pkg in last 30 days", "")%></td>
				</tr>
				<tr>
					<td><font class="labelWithHelp" title="Have you worked with your mortgage company to develop a solution:">Have you worked with your mortgage company to develop a solution:</font></td>
					<td><%=contactedMortgageCompanyIndSelect("discussedSolutionWithSrvcr", discussedSolutionWithSrvcr, disable, "true", "Have you worked with your mortgage company to develop a solution", "")%></td>
				</tr>
				<tr>
					<td><font class="labelWithHelp" title="Have you worked with another counseling agency for help with your mortgage?">Have you worked with another counseling agency for help with your mortgage?:</font></td>
					<td><%=workedWithAnotherAgencyIndSelect("workedWithAnotherAgencyInd", workedWithAnotherAgencyInd, disable, "true", "Have you worked with another counseling agency for help with your mortgage", "")%></td>
				</tr>
				<tr>
					<td><font class="labelWithHelp" title="Includes phone time (homeowner, lender & 3rd party), creating action plan and CCRC database entry.">Cumulative length of counseling:</font></td>
					<td><%=counselingDurationTypeSelect("counselingDurationType", counselingDurationType, disable, "true", "", "Cumulative length of counseling is required.")%></td>
				</tr>

				<INPUT TYPE=hidden VALUE="" NAME="successStoryInd" ID="successStoryInd">
				<INPUT TYPE=hidden VALUE="" NAME="initialFICO" ID="initialFICO">
				<INPUT TYPE=hidden VALUE="" NAME="initialFICODate" ID="initialFICODate">
				<INPUT TYPE=hidden VALUE="" NAME="currentFICO" ID="currentFICO">
				<INPUT TYPE=hidden VALUE="" NAME="currentFICODate" ID="currentFICODate">
<!--				
				<tr>
					<td><font class="labelWithHelp" title="Major progress made in counseling. Communication bridged between homeowner and lender. Good chance of mortgage becoming and remaining current or a successful pref/c sale.">Possible Success Story?:</font></td>
					<td><%=successStoryIndSelect("successStoryInd", successStoryInd, disable, "true", "Success Story Indicator", "")%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Initial FICO:</font></td>
					<td>
						<%If disable Then%>
						<%=initialFICO%>
						<%Else%>
						<input maxlength="3" detectChanges type="text" id="initialFICO" name="initialFICO" value="<%=initialFICO%>">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Initial FICO Date:</font></td>
					<td>
						<%If disable Then%>
						<%=initialFICODate%>
						<%Else%>
						<input detectChanges type="text" id="initialFICODate" name="initialFICODate" value="<%=initialFICODate%>" datatype="date" errorlbl="Initial FICO Date" errormsg="Initial FICO date is invalid.">
						<%
									Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""mySetDateField('initialFICODate'); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
									Response.Write ("(mm/dd/yyyy)")
								%>
						<%End If%>
					</td>
				</tr>
				
				<tr>
					<td><font class="label" title="">Current FICO:</font></td>
					<td>
						<%If disable Then%>
						<%=currentFICO%>
						<%Else%>
						<input maxlength="3" detectChanges type="text" id="Text1" name="currentFICO" value="<%=currentFICO%>">
						<%End If%>
					</td>
				</tr>
				<tr>
					<td><font class="label" title="">Current FICO Date:</font></td>
					<td>
						<%If disable Then%>
						<%=currentFICODate%>
						<%Else%>
						<input detectChanges type="text" id="Text2" name="currentFICODate" value="<%=currentFICODate%>" datatype="date" errorlbl="Current FICO Date" errormsg="Current FICO date is invalid.">
						<%
									Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""mySetDateField('currentFICODate'); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
									Response.Write ("(mm/dd/yyyy)")
								%>
						<%End If%>
					</td>
				</tr>
-->				
				<tr id="rowReferralComment1">
					<td><font class="label" title="">Presenting issues/Main reason(s) for default:</font></td>
					<td>
						<%If disable Then%>
						<%=loanDefaultRsn%>
						<%Else%>
						<textarea detectChanges id="loanDefaultRsn" cols="66" rows="5" NAME="loanDefaultRsn"><%=loanDefaultRsn%></textarea>
						<%End If%>
					</td>
				</tr>
				<tr id="rowReferralComment2">
					<td><font class="label" title="">Recommended action items:</font></td>
					<td>
						<%If disable Then%>
						<%=actionItems%>
						<%Else%>
						<textarea detectChanges id="actionItems" cols="66" rows="5" NAME="actionItems"><%=actionItems%></textarea>
						<%End If%>
					</td>
				</tr>
				<tr id="rowReferralComment3">
					<td><font class="label" title="">Follow up notes after initial counseling:</font></td>
					<td>
						<%If disable Then%>
						<%=followupNotes%>
						<%Else%>
						<textarea detectChanges id="followupNotes" cols="66" rows="5" NAME="followupNotes"><%=followupNotes%></textarea>
						<%End If%>
					</td>
				</tr>
			<%If InStr(1,userRoles,"83") <> 0 Or InStr(1,userRoles,"29") Then%>
					<tr>
						<td><font class="label" title="">Duplicate?:</font></td>
						<td><%=dupeIndSelect("dupeInd", dupeInd, disable, "false", "Duplicate", "")%></td>
					</tr>
			<%Else%>
				<INPUT TYPE=hidden NAME="dupeInd" ID="dupeInd" VALUE="<%=dupeInd%>">
				<%End If%>					
			</table>
			</div>
			<%If InStr(1,userRoles,"83") <> 0 Or InStr(1,userRoles,"85") <> 0 Then%>
			<tr><td><table ID="Table1">
					<tr>
						<td>
							<input name="save" id="save" value="Save" class="btn" type="button" accesskey="s" onclick="submitForm();" title="Save the changed information and continue">
						</td>
						<td valign="bottom" bgcolor=""><font class="label" title="">Did you update length of counseling?</FONT></SPAN></font></td>
						<td>
							<input name="cancel" id="cancel" value="Cancel" class="btn" type="button" accesskey="s" onclick="cancelForm();" title="Cancel all changes">
						</td>
						<!-- <td><div id="contactInfo" name="contactInfo" class="label"></div></td> -->
					</tr>
				</table></td></tr>
				<tr><td><table>
					<tr>
						<td><!-- Validation results section-->
							<div name="results" id="results" class="errorText"></div>
						</td>
					</tr>
				</table></td></tr>
			<%End If
			If formMode <> "ADD" Then%>
				<tr><td><a href="referralBudget.aspx?referralId=<%=referralId%>">Click here for Budget page</a></td></tr>
				<tr><td><a href="referral_print.aspx?referralId=<%=referralId%>">Click here for a printable copy</a></td></tr>
				<tr><td><a href="newcounselingSummary.aspx?referralId=<%=referralId%>">Click here to email a Counseling Summary</a></td></tr>
			<%End If%>
			</td></tr></table>
		</FORM>
	</body>
</html>
<%
    obj = Nothing
    If Not Rs Is Nothing Then
        Rs.Close()
    End If
    Rs = Nothing



%>

