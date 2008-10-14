<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">


	Dim vbTab2, vbTab3, vbTab4, vbTab5, vbTab6, vbTab7, vbTab8, vbTab9, vbTab10, vbTab11, vbTab12, vbTab13, vbTab14, vbTab15, vbTab16, vbTab17, vbTab18, vbTab19, vbTab20
	'vbTab2 = vbNullString
	'vbTab3 = vbNullString
	'vbTab4 = vbNullString
	'vbTab5 = vbNullString 'vbTab4 & vbTab
	'vbTab6 = vbNullString 'vbTab5 & vbTab
	'vbTab7 = vbNullString 'vbTab6 & vbTab
	'vbTab8 = vbNullString 'vbTab7 & vbTab
	'	vbTab9 = vbNullString 'vbTab8 & vbTab
	'	vbTab10 = vbNullString 'vbTab9 & vbTab
	'	vbTab11 = vbNullString 'vbTab10 & vbTab
	'	vbTab12 = vbNullString 'vbTab11 & vbTab
	'	vbTab13 = vbNullString 'vbTab12 & vbTab
	'	vbTab14 = vbNullString 'vbTab13 & vbTab
	'	vbTab15 = vbNullString 'vbTab14 & vbTab
	'	vbTab16 = vbNullString 'vbTab15 & vbTab
	'	vbTab17 = vbNullString 'vbTab16 & vbTab
	'	vbTab18 = vbNullString 'vbTab17 & vbTab
	'	vbTab19 = vbNullString 'vbTab18 & vbTab
	'	vbTab20 = vbNullString 'vbTab19 & vbTab

	Function Ellipses(strValue, strLen)
	If Len(strValue) > strLen Then
	Ellipses = Trim(Left(strValue, strLen)) & "..."
	Else
	Ellipses = strValue
	End If
	End Function

	Public Function stripSSN(s)
	stripSSN = stripCharsInBag(s, "- ")
	End Function

	Public Function stripMoney(s)
	stripMoney = stripCharsInBag(s, ",")
	End Function

	Public Function stripPhone(s)
	stripPhone = RegExReplace(s, "[^0-9]")
	End Function

	Public Function RegExReplace(s, patrn)

	Dim regEx
	regEx = New System.Web.RegularExpressions.GTRegex()


	regEx.Pattern = patrn
	regEx.Global = True

	RegExReplace = regEx.Replace(s, "")

	regEx = Nothing

	End Function

	Public Function stripCharsInBag(s, bag)

	Dim sTemp
	if NOT isDbNULL(s) THEN
	sTemp = CStr(s)
	Else
	sTemp =""
	End if
	Dim j
	j = Len(bag)

	Dim i
	For i = 1 To j
	sTemp = Replace(sTemp, Mid(bag, i, 1), "")
	Next

	stripCharsInBag = sTemp

	End Function

	Public Function formatMoney(s)
	If isNullOrEmpty(s) Then
	s = 0
	End If
	formatMoney = FormatNumber(s, 2, True, False, True)
	End Function

	Public Function formatFloat(s, NumDigitsAfterDecimal)
	If isNullOrEmpty(s) Then
	s = 0
	End If
	formatFloat = FormatNumber(s, NumDigitsAfterDecimal, True, False, False)
	End Function

	Public Function formatInterest(s)
	If isNullOrEmpty(s) Then
	s = 0
	End If
	formatInterest = FormatNumber(s, 3, True, False, False)
	End Function

	Public Function formatPhone(s)
	If Len(Trim(s)) = 10 Then
	formatPhone = "(" & Mid(s, 1, 3) & ") " & Mid(s, 4, 3) & "-" & Mid(s, 7)
	Else
	formatPhone = s
	End If
	End Function

	Public Function formatSSN(s)
	If Len(Trim(s)) = 9 Then
	formatSSN = Left(s, 3) & "-" & Mid(s, 4, 2) & "-" & Mid(s, 6)
	Else
	formatSSN = s
	End If
	End Function

	Public Function formatTerm(s)
	If isNullOrEmpty(s) Then
	formatTerm = 0
	ElseIf s = "" Then
	formatTerm = 0
	Else
	formatTerm = CInt(s / 12)
	End If
	End Function

	Public Function formatDateYYYYMMDD(s)
	If isNullOrEmpty(s) Then
	formatDateYYYYMMDD = 0
	ElseIf s = "" Then
	formatDateYYYYMMDD = ""
	Else
	formatDateYYYYMMDD = Mid(s,5,2) & "/" & Right(s,2) & "/" & Left(s,4)
	End If
	End Function
</script >