<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">

Public Function reportTypeSelect(name, sValue, required, errorlbl, errormsg)

	Dim sTemp
	sTemp = _
		"<select id='" & name & "' name='" & name & "' class='sel' style='width: 160' required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
			"<option value=''>Select Report Type</option>" & _
			"<option value='activity.asp'>Activity</option>" & _
	"</select>"

	reportTypeSelect = Replace(sTemp, " value='" & sValue & "'", " value='" & sValue & "' selected", 1)

End Function

Public Function reportServicerSelect(name, required, errorlbl, errormsg, RsServicers)
	Dim sTemp
	RsServicers.MoveFirst
	sTemp = _
		"<select id='" & name & "' name='" & name & "' class='sel' style='width: 160' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
			"<option value='*' 'selected'>All Servicers/Locations</option>"
	Do While Not RsServicers.EOF
		sTemp = sTemp & "<option value='" & RsServicers.Fields("BSN_ENTITY_SEQ_ID").Value & "'>" & RsServicers.Fields("NAME").Value & "</option>"
		RsServicers.MoveNext
	Loop
	sTemp = sTemp & "</select>"
	reportServicerSelect = sTemp
	RsServicers.Close
	RsServicers = Nothing
End Function

Public Function reportAgencySelect(name, required, errorlbl, errormsg, RsAgencies)
	Dim sTemp
	RsAgencies.MoveFirst
	sTemp = _
		"<select id='" & name & "' name='" & name & "' class='sel' style='width: 160' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>" & _
			"<option value='*' 'selected'>All Agencies</option>"
	Do While Not RsAgencies.EOF
		sTemp = sTemp & "<option value='" & RsAgencies.Fields("BSN_ENTITY_SEQ_ID").Value & "'>" & RsAgencies.Fields("NAME").Value & "</option>"
		RsAgencies.MoveNext
	Loop
	sTemp = sTemp & "</select>"
	reportAgencySelect = sTemp
	RsAgencies.Close
	RsAgencies = Nothing
End Function

Public Function pickList(rs, name, sValue, required, errorlbl, errormsg, key, display)
	Dim sTemp as String
	sTemp = "<select id='" & name & "' name='" & name & "' size='4' MULTIPLE class='listText' style='WIDTH:200px' required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>"
	
	Dim Str as String
	Dim Str1 as String
	While Not rs.eof 
		Str = rs(display).Value
		Str1 = rs(key).Value
		'Response.Write(Str)
		sTemp = sTemp & "<option value='" & Str1 & "'>" & Str & "</option>"
		rs.movenext
	End While
	rs.close
	rs = nothing
	sTemp = sTemp & "</select>"

	If Not sValue = "" Then
		Dim IDs
		IDs = Split(sValue, ",")
		Dim j
		j = UBound(IDs)
		Dim i
		For i = 0 To j
			sTemp = Replace(sTemp, " value='" & Trim(IDs(i)) & "'", " value='" & Trim(IDs(i)) & "' selected", 1)
	Next 'i
	End If
	pickList = sTemp
	End Function

	Public Function pickList2(rs, name, sValue, required, errorlbl, errormsg, key, display1, display2)
	Dim sTemp
	sTemp = "<select id='" & name & "' name='" & name & "' size='4' MULTIPLE class='listText' style='WIDTH:200px' required='" & required & "' datatype='selectlist' errorlbl='" & errorlbl & "' errormsg='" & errormsg & "'>"

	While Not rs.eof 
		sTemp = sTemp & "<option value='" & rs(key) & "'>" & rs(display1) & " - " & rs(display2) & "</option>"
		rs.movenext
	End While
	rs.close
	rs = nothing
	sTemp = sTemp & "</select>"

	If Not sValue = "" Then
		Dim IDs
		IDs = Split(sValue, ",")
		Dim j
		j = UBound(IDs)
		Dim i
		For i = 0 To j
			sTemp = Replace(sTemp, " value='" & Trim(IDs(i)) & "'", " value='" & Trim(IDs(i)) & "' selected", 1)
		Next 'i
	End If	
	pickList2 = sTemp
	
End Function

Public Function WriteSelectList (FieldName, FieldValue, FormMode, ValueList, DescList)
	Dim i
	if FormMode = "DELETE" then 
		for i = 0 to UBound(ValueList)
			if ValueList(i) = trim(FieldValue) then Response.Write (DescList(i))
		next
	else 
		Response.Write ("<SELECT NAME=""" & FieldName & """>" & chr(13))
		for i = 0 to UBound(ValueList)
			Response.Write ("      <OPTION VALUE=""" & ValueList(i) & """")
			if ValueList(i) = trim(FieldValue) then Response.Write (" SELECTED ")
			Response.Write ( ">" & DescList(i) & "</option>" & chr(13))
		next
		Response.Write ("      </select>" & chr(13))
	end if
End Function

Public Function WriteTextField(FieldName, FieldValue, FormMode, FieldDscr, FieldSize, FieldLength, FieldType, Required, DefaultValue)
    if FormMode = "ADD" then 
        Response.Write ("<input NAME="""& FieldName &""" Value="""& FieldValue &""" SIZE="""& FieldSize &""" MAXLENGTH="""&  FieldLength &""" > ")
    end if
End Function
			
Public Function  WriteRsToExcel(rs)
	Dim i
	
	Response.Write("<table>")
	Response.Write("<tr>")
	For i = 0 To rs.Fields.Count - 1
		Response.Write("<td>" & rs.Fields(i).Name & "</td>")
	Next
	Response.Write("</tr>")
	rs.MoveFirst
	Do While Not rs.EOF
		Response.Write("<tr>")
		For i = 0 To rs.Fields.Count - 1
			Response.Write("<td>" & rs.Fields(i).Value & "</td>")
		Next
		Response.Write("</tr>")
		rs.MoveNext
	Loop
	Response.Write("</table>")
End Function

</script>