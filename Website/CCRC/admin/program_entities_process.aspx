<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
On Error Resume Next

Dim obj
Dim RC

    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Dim sprogramId, sentityType, smsg
    sentityType = Request.Form("entityType")
    sprogramId = Request.Form("programId")
    smsg = Request.Form("msg")
    
Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

RC = 0
obj = Server.CreateObject("CCRC_Admin.CAdmin")
If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	Response.End 
End If

    RC = obj.Program_Entities_Process(sprogramId, sentityType, sUserId, sSQL, iReturnCode, sMessage, smsg)
If RedirectIfError("Calling CCRC_Admin.CAdmin.Program_Entity_Process", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	Response.End 
End If

obj = Nothing
Response.Redirect("program_list.aspx")

%>
