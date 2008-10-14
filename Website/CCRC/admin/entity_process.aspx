<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
 On Error Resume Next

Dim obj
Dim RC

    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Dim sactiveInd, sagencyType, scontactEmail, scontactName, sdisplayInd, sentityId, sentityName, sentityType, sfax, sffsInd, shasContractInd, sphone
    
    sactiveInd = Request.QueryString("activeInd")
    sagencyType = Request.QueryString("agencyType")
    scontactEmail = Request.QueryString("contactEmail")
    scontactName = Request.QueryString("contactName")
    sdisplayInd = Request.QueryString("displayInd")
    sentityId = Request.QueryString("entityId")
    sentityName = Request.QueryString("entityName")
    sentityType = Request.QueryString("entityType")
    sfax = Request.QueryString("fax")
    sffsInd = Request.QueryString("ffsInd")
    shasContractInd = Request.QueryString("hasContractInd")
    sphone = Request.QueryString("phone")

Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

RC = 0
obj = Server.CreateObject("CCRC_Admin.CAdmin")
If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	Response.End 
End If

Select case ucase(Request.QueryString("FormMode"))
Case "ADD"
            Call obj.Entity_Add(sentityName, sentityType, scontactName, scontactEmail, sphone, sfax, shasContractInd, sdisplayInd, sagencyType, sffsInd, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Entity_Add", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
	obj = Nothing
	If Len(Request.QueryString("returnPage")) > 0 Then
		Response.Redirect(Request.QueryString("returnPage") & "?programId=" & Request.QueryString("programId") & "&entityType=" & Request.QueryString("entityType"))
	Else 
                Response.Redirect("entity_list.aspx?entityType=" & Request.QueryString("entityType"))
	End If

Case "EDIT"
  RC = obj.Entity_Update(sentityId, sentityName,sentityType, scontactName,	scontactEmail, sphone, sfax, 	sactiveInd, shasContractInd, 	sdisplayInd, sagencyType,	sffsInd,	sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Entity_Update", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
  obj = Nothing
            Response.Redirect("entity_list.aspx?entityType=" & Request.QueryString("entityType"))

Case "DELETE"
            RC = obj.Entity_Delete(sentityId, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Entity_Delete", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
  obj = Nothing
  Response.Redirect("entity_list.aspx?entityType=" & Request.QueryString("entityType"))
End Select

 
%>
