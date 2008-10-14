<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual="/ccrc/utilities/global.inc"-->
<!--#INCLUDE virtual="/ccrc/utilities/ccrc.inc"-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
 On Error Resume Next

Dim obj
Dim RC

Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

    Dim activeInd, entityId, ntDomain, ntUserId, peDomain, peRoles, peUserid, profileId, userLocation

    activeInd = Request.QueryString("activeInd")
    entityId = Request.QueryString("entityId")
    ntDomain = Request.QueryString("ntDomain")
    ntUserId = Request.QueryString("ntUserId")
    peDomain = Request.QueryString("peDomain")
    peRoles = Request.QueryString("peRoles")
    peUserid = Request.QueryString("peUserid")
    profileId = Request.QueryString("profileId")
    userLocation = Request.QueryString("userLocation")

    
RC = 0
obj = Server.CreateObject("CCRC_Admin.CAdmin")
If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
	obj = Nothing
	Response.Clear 
        Response.End()
End If

    Dim mode
entityId = ucase(Request("entityId"))
mode = ucase(Request.QueryString("FormMode"))
If mode = "" Then
	mode = ucase(Request("FormMode"))
	If mode = "" Then
		If Request.QueryString("peDomain") <> "" Then
		  mode = "ADD"
		End If
	End If
    End If
        

Select Case mode
Case "ADD"
	If Request.QueryString("peCancel") <> "Y" Then
                'Call obj.User_Add(Request.QueryString("peDomain"), Request.QueryString("peUserid"), Request.QueryString("entityId"), Request.QueryString("peRoles"),sUserId, sSQL, iReturnCode, sMessage)
                Call obj.User_Add(peDomain, peUserid, entityId, peRoles, sUserId, sSQL, iReturnCode, sMessage)
		If RedirectIfError("Calling CCRC_Admin.CAdmin.User_Add", eRetPage, eRetBtnText) Then
			obj = Nothing
                    Response.Write("Error")
                    'Response.Clear()
                    Response.End()
		End If
		obj = Nothing
                Response.Redirect("user.aspx?formMode=EDIT" & "&entityId=" & entityId & "&ntDomain=" & Request.QueryString("peDomain") & "&ntUserId=" & Request.QueryString("peUserid"))
	End If

Case "EDIT"
            'RC = obj.User_Update(Request.QueryString("ntDomain"), Request.QueryString("ntUserId"), Request.QueryString("activeInd"), Request.QueryString("userLocation"), sUserId, sSQL, iReturnCode, sMessage)
            RC = obj.User_Update(ntDomain, ntUserId, activeInd, userLocation, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.User_Update", eRetPage, eRetBtnText) Then
		obj = Nothing
                'Response.Clear 
                Response.End()
	End If
	obj = Nothing

Case "DELETE"
            'RC = obj.User_Delete(Request.QueryString("profileId"), sUserId, sSQL, iReturnCode, sMessage)
            RC = obj.User_Delete(profileId, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.User_Delete", eRetPage, eRetBtnText) Then
		obj = Nothing
                'Response.Clear 
                Response.End()
	End If
	obj = Nothing

Case "CHANGESTATUS"
            'RC = obj.User_Update_Status(Request.QueryString("profileId"), Request.QueryString("activeInd"), sUserId, sSQL, iReturnCode, sMessage)
            RC = obj.User_Update_Status(profileId, activeInd, sUserId, sSQL, iReturnCode, sMessage)

	If RedirectIfError("Calling CCRC_Admin.CAdmin.User_Update_Status", eRetPage, eRetBtnText) Then
		obj = Nothing
                'Response.Clear 
                Response.End()
	End If
	obj = Nothing

End Select

    Response.Redirect("user_list.aspx?entityId=" & entityId)

%>
    

