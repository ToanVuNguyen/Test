<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true %>

<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->

<%


	On Error Resume Next

	Dim obj, RC
	err.Clear() 
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage	
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If

	'Server.ScriptTimeout = 1000

	RC = obj.InvoiceSessions(Request("acctPrd"), Request("invoiceDate"), sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Main.CMain.InvoiceSessions", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
	obj = Nothing

    Response.Redirect("../splash.aspx")

%>
