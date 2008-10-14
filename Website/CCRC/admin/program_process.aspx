<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%

On Error Resume Next

Dim obj
Dim RC

    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Dim sprogramName, sstartDate, sendDate, sprogramId
    
    sprogramName = Request.QueryString("programName")
    sstartDate = Request.QueryString("startDate")
    sendDate = Request.QueryString("endDate")
    sprogramId = Request.QueryString("programId")
    
Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

RC = 0
obj = Server.CreateObject("CCRC_Admin.CAdmin")
If RedirectIfError("Creating CCRC_Admin.CAdmin.", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	Response.End 
End If

Select case ucase(Request("FormMode"))
	Case "ADD"
	RC = obj.Program_Add(sprogramName, sstartDate, sendDate, sUserId, sSQL, iReturnCode, sMessage)

	Case "EDIT"
	RC = obj.Program_Update(sprogramId, sprogramName, sstartDate, sendDate, sUserId, sSQL, iReturnCode, sMessage)

	Case "DELETE"
            RC = obj.Program_Delete(sprogramId, sUserId, sSQL, iReturnCode, sMessage)
End Select

If RedirectIfError("Getting program list.", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	Response.End 
Else
	Response.Redirect("program_list.aspx")
End If

%>
