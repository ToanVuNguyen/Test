<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">

' Response.Expires = Now() - 1 ' Expires header setup by ExpiresAbsolute
' 10/07/2002 PJH
' SR 19059
' Make sure all HTTP 1.0 and HTTP 1.1 cache servers don't attempt to cache any ASP content. 
'Response.AddHeader "pragma", "no-cache"
'Response.CacheControl = "no-cache, no-store"
  
'If Not Response.IsClientConnected Then 
'	Response.Clear
'	Response.End
'End If
'
'Dim LOGON_USER
'LOGON_USER = UCase(Request.ServerVariables("LOGON_USER"))

'If ((InStr(1, LOGON_USER, "\", 1) = 0) And  (InStr(1, LOGON_USER, "/", 1) = 0)) Then
'	LOGON_USER = "IRFC\" & LOGON_USER
'ElseIf InStr(1, LOGON_USER, "/", 1) > 0 Then
'	LOGON_USER = Replace(LOGON_USER, "/", "\")
'End If
'response.write logon_user
'response.end

Public Function IIf(condition, truePart, falsePart)
	If condition Then
		IIf = truePart
	Else
		IIf = falsePart
	End If
End Function

Public Function isNullOrEmpty(test)
	If IsDBNull(test) Then
		isNullOrEmpty = True
	ElseIf test Is Nothing Then
		isNullOrEmpty = True
	ElseIf test = "" Then
		isNullOrEmpty = True
	Else
		isNullOrEmpty = False
	End If	
End Function

Public Function yearPart(months)
	yearPart = CInt(months) \ 12
End Function

Public Function monthPart(months)
	monthPart = CInt(months) Mod 12
End Function

Dim eRetPage
Dim eRetBtnText

'If Request.QueryString() And "" = "" Then
'  eRetPage = Request.ServerVariables("URL")
'Else
'  eRetPage = Request.ServerVariables("URL") And "?" & Request.QueryString()
'End If
'eRetBtnText = "Retry"

Public Function RedirectIfError(currentContext, eReturnPage, eReturnBtnText)
	If Err.Number <> 0   Then
		If currentContext = "" Then
			currentContext = "Building Page"
		End If
		
		If Right(eRetPage, 1) = "?" Then
			eRetPage = Replace(eRetPage, "?", "")
		End If
		eRetPage = server.URLEncode(eRetPage)
		
		Response.Clear
        Response.Redirect(getBasePath() & "error.aspx?errNum=" & err.number & "&errDesc=" & server.URLEncode (err.description) & "&errSrc=" & server.URLEncode (err.source) & "&URL=" & Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("URL")  & "&currContext=" & Server.URLEncode(currentContext) & "&ERetPage=" & eReturnPage & "&ERetBtnText=" & eReturnBtnText)
		RedirectIfError = True
	Else
		RedirectIfError = False
	End If
End Function

Public Function escapeLogonUser(LoginID)
  escapeLogonUser = Replace(Replace(LoginID, "\", "\\"), "/", "\\")
End Function

Public Function escapeListName(name)
	escapeListName = Replace(Replace(name & "", "'", "%27"), """", "")
End Function

Public Function isValidRS(rsIn)
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

Public Function Num(nValue)
	If Len(nValue) > 0 Then
		Num = nValue
	Else
		Num = 0
	End If
End Function

Public Function getBasePath()
	Dim sURL
	Dim sPath

	sURL = Request.ServerVariables("SERVER_NAME")
	if instr(1, sURL, "avenue", vbTextCompare) > 0 then
		getBasePath = "/"
	else
		sPath = Request.ServerVariables("PATH_INFO")
		getBasePath = Left(sPath, InStr(2, sPath, "/", vbTextCompare))
	end if
	
End Function

Function ConvertRStoXML(RootName, RowName, Rs)
    
    On Error Resume Next

	Dim NODE_ELEMENT
	NODE_ELEMENT = 1

    Dim objDom2
    objDom2 = CreateObject("MSXML2.DOMDocument.3.0")
    
	If RedirectIfError("1", eRetPage, eRetBtnText) Then
		Response.End 
	End If

    Dim objRoot
    objRoot = objDom2.createNode(NODE_ELEMENT, RootName, "")
    
    objDom2.appendChild(objRoot)
    
    Dim objSegment, objValue, objWorking, Fld
    
    While Not Rs.EOF
        
        objWorking = objDom2.createNode(NODE_ELEMENT, RowName, "")
        
        objSegment = objRoot.appendChild(objWorking)
        
        For Each Fld In Rs.Fields
            
            objValue = objDom2.createNode(NODE_ELEMENT, Fld.Name, "")
            objValue.Text = Fld.Value & vbNullString
            
            objSegment.appendChild(objValue)
        
        Next Fld
        
        Rs.MoveNext

    End While
    
    ConvertRStoXML = objDom2.xml
    
End Function

Public Function SetLookupValue(sValue, Rs)
	Dim sTemp=""
	Rs.MoveFirst
	Do While Not Rs.EOF
		If sValue = Rs.Fields(0).Value Then
			sTemp = Rs.Fields(1).Value
			Exit Do
		End If
		Rs.MoveNext
	Loop
	SetLookupValue = sTemp	
End Function

Public Function LoadDropdownOptions(Rs)
	Dim sTemp=""
	Rs.MoveFirst
	Do While Not Rs.EOF
		sTemp = sTemp & "<option value='" & Rs.Fields(0).Value & "'>" & Rs.Fields(1).Value & "</option>"
		Rs.MoveNext
	Loop
	LoadDropdownOptions = sTemp
End Function

Public Function LoadDefaultReasonOptions(Rs, Primary)
	Dim sTemp=""
	Rs.MoveFirst
	Do While Not Rs.EOF
		If "Y" = Primary Then
			If "N/A" <> Rs.Fields(1).Value Then
				sTemp = sTemp & "<option value='" & Rs.Fields(0).Value & "'>" & Rs.Fields(1).Value & "</option>"
			End If
		Else
			sTemp = sTemp & "<option value='" & Rs.Fields(0).Value & "'>" & Rs.Fields(1).Value & "</option>"
		End If		
		Rs.MoveNext
	Loop
	LoadDefaultReasonOptions = sTemp
End Function
</script>