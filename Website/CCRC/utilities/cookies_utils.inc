<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">

'three methods used to replace storing session information in session
'and store this in the user's cookie instead
'to set a value
'setSessVar "valueName", value
'to get a value
'value = getSessVar("valueName")

Sub abandonSession()
	Dim newCookie As HttpCookie = New HttpCookie("CCRCCookie")
	Response.Cookies.Add(newCookie)
End Sub

Function getSessVar(key)
	getSessVar = Request.Cookies("CCRC")(key) & ""
	Response.Write("[" & Request.Cookies("CCRC")(key) & "]")
	Response.End
End Function

Sub setSessVar(key, val)
	Response.Cookies("CCRC")(key) = val & ""
End Sub

Sub clearCookie()
	'Response.Cookies("CCRCCookie") = ""
	Dim newCookie As HttpCookie = New HttpCookie("CCRCCookie")
	Response.Cookies.Add(newCookie)
End Sub

Function getCookieValue(key)
	getCookieValue = Request.Cookies("CCRCCookie")(key) & ""
End Function

Sub setCookieValue(key, val)
	Response.Cookies("CCRCCookie")(key) = val & ""
End Sub
</script>