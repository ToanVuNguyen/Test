<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">

Public Function servicerSelect(name, sValue, disable, required, errorlbl, errormsg, RsServicers)
	Dim sTemp
	RsServicers.MoveFirst
	If disable Then
		Do While Not RsServicers.EOF
			If CInt(sValue) = CInt(RsServicers.Fields("BSN_ENTITY_SEQ_ID").Value) Then
				sTemp = RsServicers.Fields("BSN_ENTITY_NAME").Value
			End If				
			RsServicers.MoveNext
		Loop
		servicerSelect = sTemp
	Else
		sTemp = _
			"<select id='" & name & "' name='" & name & "' onchange='servicer_OnChange();' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Servicer</option>"
		Do While Not RsServicers.EOF
			sTemp = sTemp & "<option value='" & RsServicers.Fields("BSN_ENTITY_SEQ_ID").Value & "'>" & RsServicers.Fields("BSN_ENTITY_NAME").Value & "</option>"
			RsServicers.MoveNext
		Loop
		sTemp = sTemp & "</select>"
		servicerSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If	
	RsServicers.Close
	RsServicers = Nothing
End Function

Public Function servicerSelect2(name, sValue, disable, required, errorlbl, errormsg, RsServicers)
	Dim sTemp
	RsServicers.MoveFirst
	If disable Then
		Do While Not RsServicers.EOF
			If CInt(sValue) = CInt(RsServicers.Fields("BSN_ENTITY_SEQ_ID").Value) Then
				sTemp = RsServicers.Fields("BSN_ENTITY_NAME").Value
			End If				
			RsServicers.MoveNext
		Loop
		servicerSelect2 = sTemp
	Else
		sTemp = _
			"<select id='" & name & "' name='" & name & "' onchange='servicer_OnChange();' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>"
		Do While Not RsServicers.EOF
			sTemp = sTemp & "<option value='" & RsServicers.Fields("BSN_ENTITY_SEQ_ID").Value & "'>" & RsServicers.Fields("BSN_ENTITY_NAME").Value & "</option>"
			RsServicers.MoveNext
		Loop
		sTemp = sTemp & "</select>"
		servicerSelect2 = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If	
	RsServicers.Close
	RsServicers = Nothing
End Function


Public Function agencySelect(name, sValue, disable, required, errorlbl, errormsg, rsAgencies)
	Dim sTemp
	rsAgencies.MoveFirst
	
	If disable Then
		Do While Not rsAgencies.EOF
			If CInt(sValue) = CInt(rsAgencies.Fields("BSN_ENTITY_SEQ_ID").Value) Then
				sTemp = rsAgencies.Fields("BSN_ENTITY_NAME").Value
			End If				
			rsAgencies.MoveNext
		Loop
		agencySelect = sTemp
	Else
		sTemp = _
			"<select id='" & name & "' name='" & name & "' onchange='agency_OnChange();' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Agency</option>"
			Do While Not rsAgencies.EOF
				sTemp = sTemp & "<option value='" & rsAgencies.Fields("BSN_ENTITY_SEQ_ID").Value & "'>" & rsAgencies.Fields("BSN_ENTITY_NAME").Value & "</option>"
				rsAgencies.MoveNext
			Loop
			sTemp = sTemp & "</select>"
			agencySelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	rsAgencies.Close
	 rsAgencies = Nothing
End Function

Public Function counselorSelect(name, sValue, disable, required, errorlbl, errormsg, rsCounselors)
	Dim sTemp
	If isValidRS(rsCounselors) Then
		rsCounselors.MoveFirst
		
		If disable Then
			Do While Not rsCounselors.EOF
				If CInt(sValue) = CInt(rsCounselors.Fields("CCRC_USER_SEQ_ID").Value) Then
					sTemp = rsCounselors.Fields("LAST_NAME").Value & ", " & rsCounselors.Fields("FIRST_NAME").Value
				End If				
				rsCounselors.MoveNext
			Loop
			counselorSelect = sTemp
		Else	
			sTemp = _
				"<select id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
					"<option value=''>Select Counselor</option>"
				Do While Not rsCounselors.EOF
					sTemp = sTemp & "<option value='" & rsCounselors.Fields("CCRC_USER_SEQ_ID").Value & "'>" & rsCounselors.Fields("LAST_NAME").Value & ", " & rsCounselors.Fields("FIRST_NAME").Value & "</option>"
					rsCounselors.MoveNext
				Loop
			sTemp = sTemp & "</select>"
			counselorSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
		End If
	End If
	rsCounselors.Close
	 rsCounselors = Nothing
End Function

Public Function locationSelect(name, sValue, disable, required, errorlbl, errormsg, RsLocations)
	Dim sTemp
	RsLocations.MoveFirst
	If disable Then
		Do While Not RsLocations.EOF
			If CInt(sValue) = CInt(RsLocations.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value) Then
				sTemp = RsLocations.Fields("LOCTN_NAME").Value
			End If				
			RsLocations.MoveNext
		Loop
		locationSelect = sTemp
	Else
		sTemp = _
			"<select id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Location</option>"
		Do While Not RsLocations.EOF
			sTemp = sTemp & "<option value='" & RsLocations.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value & "'>" & RsLocations.Fields("LOCTN_NAME").Value & "</option>"
			RsLocations.MoveNext
		Loop
		sTemp = sTemp & "</select>"
		locationSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If	
	RsLocations.Close
	 RsLocations = Nothing
End Function

Public Function userLocationSelect(name, sValue, required, errorlbl, errormsg, RsLocations)
	Dim sTemp
	RsLocations.MoveFirst
	sTemp = _
		"<select id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
			"<option value=''>Select Location</option>"
	Do While Not RsLocations.EOF
		sTemp = sTemp & "<option value='" & RsLocations.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value & "'>" & RsLocations.Fields("LOCTN_NAME").Value & "</option>"
		RsLocations.MoveNext
	Loop
	sTemp = sTemp & "</select>"
	userLocationSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	RsLocations.Close
	 RsLocations = Nothing
End Function

Public Function primaryDfltRsnTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("DEFAULT_REASON_TYPE","DSCR","","","","")

	Dim sTemp
	If disable Then
		primaryDfltRsnTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='DEFAULT_REASON_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Primary Default Reason Type</option>"
		sTemp = sTemp & LoadDefaultReasonOptions(Rs,"Y") & "</select>"
		primaryDfltRsnTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function secondaryDfltRsnTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("DEFAULT_REASON_TYPE","DSCR","","","","")

	Dim sTemp
	If disable Then
		secondaryDfltRsnTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='DEFAULT_REASON_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Secondary Default Reason Type</option>"
		sTemp = sTemp & LoadDefaultReasonOptions(Rs,"N") & "</select>"
		secondaryDfltRsnTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function raceTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("RACE_TYPE","RACE_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		raceTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='RACE_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Race Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		raceTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function genderTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("GENDER_TYPE","GENDER_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		genderTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='GENDER_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Gender Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		genderTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function householdTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("household_TYPE","household_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		householdTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='household_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select household Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		householdTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function incomeCategoryTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("income_Category_TYPE","income_Category_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		incomeCategoryTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='incomeCategory_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select incomeCategory Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		incomeCategoryTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function intakeScoreTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("intake_Score_TYPE","intake_Score_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		intakeScoreTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='intake_Score_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Intake Score Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		intakeScoreTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function summarySentOtherTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("summary_sent_other_TYPE","summary_sent_other_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		summarySentOtherTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='summary_sent_other_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Summary Sent Other Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		summarySentOtherTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function incomeEarnersTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("income_earners_TYPE","income_earners_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		incomeEarnersTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='income_earners_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Income Earners</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		incomeEarnersTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function referralTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("REFERRAL_TYPE","REFERRAL_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		referralTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='REFERRAL_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Referral Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		referralTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function secondContactStatusTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("FIRST_CONTACT_STATUS_TYPE","FIRST_CONTACT_STATUS_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		secondContactStatusTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='SECOND_CONTACT_STATUS_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		secondContactStatusTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function firstContactStatusTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("FIRST_CONTACT_STATUS_TYPE","FIRST_CONTACT_STATUS_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		firstContactStatusTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='FIRST_CONTACT_STATUS_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Status Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		firstContactStatusTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function referralSourceSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("REFERRAL_SOURCE_TYPE","DSCR","","","","")

	Dim sTemp
	If disable Then
		referralSourceSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='REFERRAL_SOURCE_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 200' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Referral Source</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		referralSourceSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	Rs = Nothing
	obj = Nothing
	End Function

	Public Function priorityTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	obj = CreateObject("CCRC_Main.CMain")
	'Rs = obj.Get_LookupValues("CCRC_PRIORITY_TYPE","DSCR","","","","")  'Commented by Pb om 6/11/2008
	Rs = obj.Get_LookupValues("PRIORITY_TYPE","DSCR","","","","")

	Dim sTemp
	If disable Then
	priorityTypeSelect = SetLookupValue(sValue, Rs)
	Else
	sTemp = _
	"<select columnName='PRIORITY_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 200' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Priority Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		priorityTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function ownerOccupiedIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		ownerOccupiedIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='OWNER_OCCUPIED_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
		ownerOccupiedIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function dupeIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		dupeIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='DUPE_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
		dupeIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function privacyConsentIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		privacyConsentIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='PRIVACY_CONSENT_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Privacy Consent</option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
		privacyConsentIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function hispanicIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		hispanicIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='HISPANIC_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Hispanic Indicator</option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
		hispanicIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function bankruptcyIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		bankruptcyIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='BANKRUPTCY_IND' columnType='string' id='" & name & "' name='" & name & "' onchange='bankruptcy_OnChange();' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
		bankruptcyIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function successStoryIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		successStoryIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='SUCCESS_STORY_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
		successStoryIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function contactedServicerIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		contactedServicerIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='CONTACTED_SERVICER_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"<option value='U'>Unknown</option>" & _
				"</select>"
		contactedServicerIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function workedWithAnotherAgencyIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		workedWithAnotherAgencyIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='worked_with_another_agency_ind' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Worked Another Agency Indicator</option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
	workedWithAnotherAgencyIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function contactedMortgageCompanyIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		contactedMortgageCompanyIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='DISCUSSED_SOLUTION_WITH_SRVCR_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Contact Mortgage Company </option>" & _
				"<option value='N'>No</option>" & _
				"<option value='Y'>Yes</option>" & _
				"</select>"
	contactedMortgageCompanyIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function counselingDurationTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("COUNSELING_DURATION_TYPE","COUNSELING_DURATION_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		counselingDurationTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select style='' columnName='COUNSELING_DURATION_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Counseling Duration Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		counselingDurationTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	Rs = Nothing
	obj = Nothing
	End Function

	Public Function HUDTerminationReasonSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	obj = CreateObject("CCRC_Main.CMain")
	Rs = obj.Get_LookupValues("HUD_TERM_REASON_TYPE","CAST(HUD_TERM_REASON_TYPE_CODE AS INTEGER)","","","","")

	Dim sTemp
	If disable Then
	HUDTerminationReasonSelect = SetLookupValue(sValue, Rs)
	Else
	sTemp = _
	"<select columnName='HUD_TERM_REASON_TYPE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 350' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		HUDTerminationReasonSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	Rs = Nothing
	obj = Nothing
	End Function

	Public Function HUDOutcomeTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	obj = CreateObject("CCRC_Main.CMain")
	Rs = obj.Get_LookupValues("HUD_OUTCOME_TYPE","CAST(HUD_OUTCOME_TYPE_CODE AS INTEGER)","","","","")

	Dim sTemp
	If disable Then
	HUDOutcomeTypeSelect = SetLookupValue(sValue, Rs)
	Else
	sTemp = _
	"<select columnName='HUD_OUTCOME_TYPE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 350' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select HUD Outcome</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		HUDOutcomeTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function secondMortgageTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("FIRST_MORTGAGE_TYPE","FIRST_MORTGAGE_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		secondMortgageTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='SECOND_MORTGAGE_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 200' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		secondMortgageTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function


Public Function firstMortgageTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("FIRST_MORTGAGE_TYPE","FIRST_MORTGAGE_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		firstMortgageTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='FIRST_MORTGAGE_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 200' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select 1st Mortgage Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		firstMortgageTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function mthlyNetIncomeTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("MTHLY_NET_INCOME_TYPE","MTHLY_NET_INCOME_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		mthlyNetIncomeTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='MTHLY_NET_INCOME_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Monthly Net Income Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		mthlyNetIncomeTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function mthlyExpenseTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("MTHLY_EXPENSE_TYPE","MTHLY_EXPENSE_TYPE_CODE","","","","")

	Dim sTemp
	If disable Then
		mthlyExpenseTypeSelect = SetLookupValue(sValue, Rs)	
	Else
		sTemp = _
			"<select columnName='MTHLY_EXPENSE_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Monthly Expense Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		mthlyExpenseTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function searchTypeSelect(name, sValue, required, errorlbl, errormsg)
	Dim sTemp
	sTemp = _
		"<select id='" & name & "' name='" & name & "' onchange='javascript:searchTypeChange();' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
			"<option value=''>Select Search Item</option>" & _
			"<option value='1'>Loan Number</option>" & _
			"<option value='2'>Borrower Name</option>"
			If Request.Cookies("CCRCCookie")("entityTypeCode") = "2" Then
				sTemp = sTemp & "<option value='4'>Agency Tracking Id</option>"
			End If
	sTemp = sTemp & "</select>"

	searchTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
End Function

Public Function referralResultSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		 obj = Nothing
		Response.Clear 
		Response.End 
	End If	
	 Rs = obj.Get_LookupValues("REFERRAL_RESULT_TYPE","PAYABLE_IND, DSCR DESC","","","","")
	If RedirectIfError("Calling CCRC_Main.CMain.Get_LookupValues", eRetPage, eRetBtnText) Then
		 obj = Nothing
		Response.Clear 
		Response.End 
	End If	

	Dim sTemp
	If disable Then
		referralResultSelect = SetLookupValue(sValue, Rs)
	Else
		sTemp = "<select columnName='REFERRAL_RESULT_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 260' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" 
		sTemp = sTemp &	"<option value=''>Select Referral Result Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		referralResultSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function referralResultSelect4NewReferral(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("REFERRAL_RESULT_TYPE","PAYABLE_IND, DSCR DESC","","","","")
	Dim sTemp
	If disable Then
		referralResultSelect4NewReferral = SetLookupValue(sValue, Rs)
	Else
		sTemp = "<select id='" & name & "' name='" & name & "' class='sel' style='width: 260' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>"
		sTemp = sTemp &	"<option value=''>Select Referral Result Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		referralResultSelect4NewReferral = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function sessionRatingSelect(name, sValue, disable, required, errorlbl, errormsg)
'	Dim Rs, obj
'	 obj = CreateObject("CCRC_Main.CMain")
'	 Rs = obj.Get_LookupValues("SESSION_RATING_TYPE","DSCR","","","","")

	Dim sTemp
	If disable Then
'		sessionRatingSelect = SetLookupValue(sValue, Rs)
		Select Case sValue
			Case "0":
				sTemp = "#0: Session not rated."
			Case "1":
				sTemp = "#1: Great attitude, sufficient resources...only temporary issue."
			Case "2":
				sTemp = "#2: Great attitude, resources could be a problem for a time, longer term solution but we should work with them (even if pre-forclosure sale is the alternative)."		
			Case "3":
				sTemp = "#3: Can't tell one way or the other not enough good information to gain insight."
			Case "4":
				sTemp = "#4: May have the resources but not the desire/attitude."
			Case "5":
				sTemp = "#5: No resources, no desire, or desire but No way to make it ..now or in the foreseeable future."
		End Select
		sessionRatingSelect = sTemp
	Else
		sTemp = "<select columnName='SESSION_RATING_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 560' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>"
		sTemp = sTemp & "<option default value=''>Select Session Rating</option>"
		sTemp = sTemp & "<option value='0'>#0: Session not rated.</option>"
		sTemp = sTemp & "<option value='1'>#1: Great attitude, sufficient resources...only temporary issue.</option>"
		sTemp = sTemp & "<option value='2'>#2: Great attitude, resources could be a problem for a time, longer term solution but we should work with them (even if pre-forclosure sale is the alternative).</option>"		
		sTemp = sTemp & "<option value='3'>#3: Can't tell one way or the other not enough good information to gain insight.</option>"
		sTemp = sTemp & "<option value='4'>#4: May have the resources but not the desire/attitude.</option>"
		sTemp = sTemp & "<option value='5'>#5: No resources, no desire, or desire but No way to make it ..now or in the foreseeable future.</option>"
		sTemp = sTemp & "</select>"
		sessionRatingSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
'	Rs.Close
'	 Rs = Nothing
'	 obj = Nothing
End Function

Public Function sessionTypeSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("SESSION_TYPE","DSCR","","","","")

	Dim sTemp
	If disable Then
		sessionTypeSelect = SetLookupValue(sValue, Rs)
	Else
		sTemp = "<select columnName='SESSION_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>"
		sTemp = sTemp & "<option value=''>Select Session Type</option>"
		sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
		sessionTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

dim disable

Public Function activeIndSelect(name, sValue, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		activeIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='ACTIVE_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Active</option>" & _
				"<option value='Y'>Yes</option>" & _
				"<option value='N'>No</option>" & _
		"</select>"
		activeIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function displayIndSelect(name, sValue, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		displayIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='DISPLAY_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Display</option>" & _
				"<option value='Y'>Yes</option>" & _
				"<option value='N'>No</option>" & _
		"</select>"
		displayIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function ffsIndSelect(name, sValue, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		ffsIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='FFS_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Fee-For-Service</option>" & _
				"<option value='Y'>Yes</option>" & _
				"<option value='N'>No</option>" & _
		"</select>"
		ffsIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function armResetIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		armResetIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='ARM_RESET_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select ARM Reset</option>" & _
				"<option value='Y'>Yes</option>" & _
				"<option value='N'>No</option>" & _
		"</select>"
		armResetIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function fcNoticeRecdIndSelect(name, sValue, required, disable, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		fcNoticeRecdIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='FC_NOTICE_RECD_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Foreclosure Sale</option>" & _
				"<option value='Y'>Yes</option>" & _
				"<option value='N'>No</option>" & _
		"</select>"
		fcNoticeRecdIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function propertyForSaleIndSelect(name, sValue, disable, required, errorlbl, errormsg)
	Dim sTemp
	If disable Then
		propertyForSaleIndSelect = sValue
	Else
		sTemp = _
			"<select columnName='PROPERTY_FOR_SALE_IND' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Property For Sale</option>" & _
				"<option value='Y'>Yes</option>" & _
				"<option value='N'>No</option>" & _
		"</select>"
		propertyForSaleIndSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function

Public Function yesnoSelect(name, sValue, required, errorlbl, errormsg)
	Dim sTemp
	sTemp = _
		"<select id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
			"<option value='N'>No</option>" & _
			"<option value='Y'>Yes</option>" & _
	"</select>"
	yesnoSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
End Function

Public Function entityTypeSelect(name, sValue, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("BSN_ENTITY_TYPE","DSCR","","","","")

	Dim sTemp
	sTemp = "<select columnName='BSN_ENTITY_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>"
	sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
	entityTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function agencyTypeSelect(name, sValue, required, errorlbl, errormsg)
	Dim Rs, obj
	 obj = CreateObject("CCRC_Main.CMain")
	 Rs = obj.Get_LookupValues("CCRC_AGENCY_TYPE","DSCR","","","","")

	Dim sTemp
	sTemp = "<select columnName='AGENCY_TYPE_CODE' columnType='string' id='" & name & "' name='" & name & "' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>"
	sTemp = sTemp & LoadDropdownOptions(Rs) & "</select>"
	agencyTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	Rs.Close
	 Rs = Nothing
	 obj = Nothing
End Function

Public Function programSelect(name, sValue, disable, required, errorlbl, errormsg, RsPrograms)
	On Error Resume Next

	Dim sTemp
	RsPrograms.MoveFirst
	If disable Then
		Do While Not RsPrograms.EOF
			If CInt(sValue) = CInt(RsPrograms.Fields("PROGRAM_SEQ_ID").Value) Then
				sTemp = RsPrograms.Fields("PROGRAM_NAME").Value
			End If				
			RsPrograms.MoveNext
		Loop
		programSelect = sTemp
	Else
		sTemp = _
			"<select id='" & name & "' name='" & name & "' onchange='program_OnChange();' class='sel' style='width: 160' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''>Select Program</option>"
		If sValue = "search" Then
			sTemp = sTemp & "<option value='*'>(All Programs)</option>"
		End If
		Do While Not RsPrograms.EOF
			sTemp = sTemp & "<option tag='" & RsPrograms.Fields("ALLOW_NONCONTRACTED_SVCR_IND").Value & "' value='" & RsPrograms.Fields("PROGRAM_SEQ_ID").Value & "'>" & RsPrograms.Fields("PROGRAM_NAME").Value & "</option>"
			RsPrograms.MoveNext
		Loop
		sTemp = sTemp & "</select>"
		programSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If	
	RsPrograms.Close
	 RsPrograms = Nothing
End Function

Public Function buildAddress(street, city, state, zip)
	On Error Resume Next
	dim sRet 
	sRet = street & ", " & city & ", " & state & " " & zip
	buildAddress = sRet
End Function

Public Function buildName(firstName, middleInitial, lastName, suffix)
	On Error Resume Next
	dim sRet 
	sRet = lastName
	if not IsNullOrEmpty(suffix) then
		sRet = sRet & " " & getNameSuffixDscr(suffix)
	end if
	sRet = sRet & ", " & firstName
	if not IsNullOrEmpty(middleInitial) then
		sRet = sRet & " " & middleInitial & "."
	end if

	buildName = sRet
End Function

Public Function buildNameFMLS(firstName, middleInitial, lastName, suffix)
    On Error Resume Next
	dim sRet 
	sRet = firstName
	if not IsNullOrEmpty(middleInitial) then
		sRet = sRet & " " & middleInitial & "."
	end if

	sRet = sRet & " " & lastName

	if not IsNullOrEmpty(suffix) then
		sRet = sRet & " " & getNameSuffixDscr(suffix)
	end if
	
	buildNameFMLS = sRet
End Function

Public Function getNameSuffixDscr(suffixCode)
	select case (suffixCode)
		case "JR"
			getNameSuffixDscr = "Jr."		
		case "SR"
			getNameSuffixDscr = "Sr."
		case "II","III","IV","V"
			getNameSuffixDscr	= suffixCode
		case else
			getNameSuffixDscr	= suffixCode
	end select
End Function

Public Function stateSelect(name, sValue, disable, internal, required, errorlbl, errormsg)
	Dim sTemp
	If disable Or internal Then
		stateSelect = sValue
	Else
		sTemp = _
			"<select internal columnName='STATE' columnType='string' id='" & name & "' name='" & name & "' class='sel' detectChanges required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
				"<option value=''></option>" & _
				"<option value='AK'>AK</option>" & _
				"<option value='AL'>AL</option>" & _
				"<option value='AR'>AR</option>" & _
				"<option value='AZ'>AZ</option>" & _
				"<option value='CA'>CA</option>" & _
				"<option value='CO'>CO</option>" & _
				"<option value='CT'>CT</option>" & _
				"<option value='DC'>DC</option>" & _
				"<option value='DE'>DE</option>" & _
				"<option value='FL'>FL</option>" & _
				"<option value='GA'>GA</option>" & _
				"<option value='HI'>HI</option>" & _
				"<option value='IA'>IA</option>" & _
				"<option value='ID'>ID</option>" & _
				"<option value='IL'>IL</option>" & _
				"<option value='IN'>IN</option>" & _
				"<option value='KS'>KS</option>" & _
				"<option value='KY'>KY</option>" & _
				"<option value='LA'>LA</option>" & _
				"<option value='MA'>MA</option>" & _
				"<option value='MD'>MD</option>" & _
				"<option value='ME'>ME</option>" & _
				"<option value='MI'>MI</option>" & _
				"<option value='MN'>MN</option>" & _
				"<option value='MO'>MO</option>" & _
				"<option value='MS'>MS</option>" & _
				"<option value='MT'>MT</option>" & _
				"<option value='NC'>NC</option>" & _
				"<option value='ND'>ND</option>" & _
				"<option value='NE'>NE</option>" & _
				"<option value='NH'>NH</option>" & _
				"<option value='NJ'>NJ</option>" & _
				"<option value='NM'>NM</option>" & _
				"<option value='NV'>NV</option>" & _
				"<option value='NY'>NY</option>" & _
				"<option value='OH'>OH</option>" & _
				"<option value='OK'>OK</option>" & _
				"<option value='OR'>OR</option>" & _
				"<option value='PA'>PA</option>" & _
				"<option value='PR'>PR</option>" & _
				"<option value='RI'>RI</option>" & _
				"<option value='SC'>SC</option>" & _
				"<option value='SD'>SD</option>" & _
				"<option value='TN'>TN</option>" & _
				"<option value='TX'>TX</option>" & _
				"<option value='UT'>UT</option>" & _
				"<option value='VA'>VA</option>" & _
				"<option value='VT'>VT</option>" & _
				"<option value='WA'>WA</option>" & _
				"<option value='WI'>WI</option>" & _
				"<option value='WV'>WV</option>" & _
				"<option value='WY'>WY</option>" & _
			"</select>"
			stateSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)
	End If
End Function 
</script>