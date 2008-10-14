<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">

	Sub  GetAuthorizedUser(ByRef  sDomainUser, ByRef  sDomain, ByRef  sUserId)
	Dim LOGON_USER
	sDomainUser =  LOGON_USER
	sDomainUser = Request.Cookies("CCRCCookie")("CurrentUser")
	sUserId = Request.Cookies("CCRCCookie")("CurrentUser")
	sDomain = "rfc"
	End Sub

</script>