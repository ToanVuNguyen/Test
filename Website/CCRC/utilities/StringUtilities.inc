<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">
Function Char13ToHTMLBreak(strText)
	'* receives text string replacing chr(13) with "<br>"
	'* Used to display text while preserving intended line breaks.
	Dim strOut, strChar
	
	If strText Is Nothing Then
		Char13ToHTMLBreak = ""
		Exit Function
	End If
    Dim i
	For i = 1 to len(strText)
		strChar = Mid(strText, i, 1)
		If strChar = Chr(13) Then
			strOut = strOut + "<br>"
		Else
			strOut = strOut + strChar
		End If
	Next

	Char13ToHTMLBreak = strOut

End Function

Function DupeQuotes(strText)
	'* parses passed text placing additional quote with each quote in a string
	Dim strOut, strChar
	
	If  strText Is Nothing Then
		DupeQuotes = ""
		Exit Function
	End If
    Dim i
	For i = 1 to len(strText)
		strChar = Mid(strText, i, 1)
		If strChar = """" Then
			strOut = strOut + strChar + """"
		Else
			strOut = strOut + strChar
		End If
	Next

	DupeQuotes = strOut

End Function

Function ScrubTicks(varTemp)
    dIM tick 
	If   varTemp IS Nothing Then
		ScrubTicks = ""
	Else
		ScrubTicks = Trim(varTemp)
		tick = InStr(1, varTemp, "'")
		do while tick > 0
			varTemp = left(varTemp, tick-1) & "''" &  right(varTemp, len(varTemp) - tick)
			if tick + 1 < len(varTemp) then
				tick = InStr(tick+2, varTemp, "'")
			else
				tick = 0
			end if
		loop
		ScrubTicks = varTemp
	End If
End Function
</Script>
