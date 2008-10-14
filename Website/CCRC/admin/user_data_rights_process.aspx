<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
 On Error Resume Next

Dim obj
Dim RC

Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

RC = 0
obj = Server.CreateObject("CCRC_Admin.CAdmin")
If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	Response.End 
End If

Dim profileId, entityId, msg
profileId = Request.Form("profileId")
entityId =  Request.Form("entityId")
msg = Request.Form("msg")
RC = obj.User_Data_Rights_Process(profileId, entityId, sUserId, sSQL, iReturnCode, sMessage, msg)
If RedirectIfError("Calling CCRC_Admin.CAdmin.Program_Entity_Process", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	'Response.End 
End If

obj = Nothing
Response.Redirect("user_list.aspx?entityId=" & Request.Form("entityId"))
%>
