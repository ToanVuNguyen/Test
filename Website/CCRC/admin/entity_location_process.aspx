<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
 On Error Resume Next

Dim obj
Dim RC

    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Dim sLocationId, sEntityId, sLocationName, sActiveInd, sPhone
    sLocationId = Request.QueryString("locationId")
    sEntityId = Request.QueryString("entityId")
    sLocationName = Request.QueryString("locationName")
    sActiveInd = Request.QueryString("activeInd")
    sPhone = Request.QueryString("phone")
Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

RC = 0
obj = Server.CreateObject("CCRC_Admin.CAdmin")
If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
	Response.End 
End If

Select case ucase(Request("FormMode"))
Case "ADD"
            Call obj.Entity_Location_Add(sEntityId, sLocationName, sActiveInd, sPhone, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Entity_Location_Add", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If

Case "EDIT"
            Call obj.Entity_Location_Update(sLocationId, sLocationName, sActiveInd, sPhone, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Entity_Location_Update", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End        
	End If

Case "DELETE"
            RC = obj.Entity_Loction_Delete(sLocationId, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Entity_Loction_Delete", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
End Select

obj = Nothing
Response.Redirect("entity_location_list.aspx?entityId=" & Request.QueryString("entityId") )      

%>
