<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
    'This page contains server side code to validate that the user is a valid
     'active user of CCRC.
          
    '    On Error Resume Next
    ' Err.Clear()
    ' If Response.Cookies("CCRCCookie")("profileSeqId") & "" = "" Then
    'If Err.Number = 438 Then Err.Clear()
    'Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    'sDomain = "rfc"
    'sDomainUser = "mlukens"
    'sUserId = "mlukens"
    'Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
    'If RedirectIfError("Calling GetAuthorizedUser", eRetPage, eRetBtnText) Then
    ' Response.Clear()
    ' Response.End()
    'End If

    'If sDomain = "" Then
    ' sDomain = "IRFC"
    ' End If

    'Dim objCCRC
    'objCCRC = CreateObject("CCRC_Admin.CAdmin")
    'If RedirectIfError("Creating object CCRC_Admin", eRetPage, eRetBtnText) Then
    ' objCCRC = Nothing
    ' Response.Clear()
    'Response.End()
    'End If

    'Dim Rs
    'Rs = objCCRC.Get_User_Info(sDomain, sUserId, sSQL, iReturnCode, sMessage)
		
    '    If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_User_Info", eRetPage, eRetBtnText) Then
    'objCCRC = Nothing
    'Response.Clear()
    'Response.End()
    'End If

    'response.Write	sDomainUser & "<br>"
    'response.Write sDomain & "<br>"
    'response.Write sUserId
    'response.End		
		
    'If Rs.EOF Then
    'Rs.Close()
    'Rs = Nothing
    'Response.Clear()
    'Response.Redirect("exit.aspx?exitCode=2")
    'Response.End()
    'End If

    'Dim entityIsActive
    'entityIsActive = Rs.Fields("BUSINESS_ENTITY_ACTIVE_IND").Value
    'If Not entityIsActive = "Y" Then
    'Rs.Close()
    'Rs = Nothing
    'Response.Clear()
    'Response.Redirect("exit.aspx?exitCode=4")
    'Response.End()
    'End If

    'Dim userIsActive
    '    userIsActive = Rs.Fields("USER_ACTIVE_IND").Value
    '   If Not userIsActive = "Y" Then
    'Rs.Close()
    '    Rs = Nothing
    '   Response.Clear()
    '  Response.Redirect("exit.aspx?exitCode=3")
    '    Response.End()
    '   End If
		
    'user is valid so save session variables
    '  Dim profileSeqId, profileRoles
    ' Dim entitySeqId, entityTypeCode
		
    'profileSeqId = Rs.Fields("CCRC_USER_SEQ_ID").Value
    '    profileRoles = Rs.Fields("USER_ROLE_STRING").Value
    '   entitySeqId = Rs.Fields("BSN_ENTITY_SEQ_ID").Value
    ''  entityTypeCode = Rs.Fields("BSN_ENTITY_TYPE_CODE").Value
    'Rs.Close()
    '    Rs = Nothing
		
    '   Response.Cookies("CCRCCookie").Path = "/"
    '  Response.Cookies("CCRCCookie")("profileSeqId") = profileSeqId
    ' Response.Cookies("CCRCCookie")("profileRoles") = profileRoles
    ''Response.Cookies("CCRCCookie")("profileRoles") = "0"
    '    Response.Cookies("CCRCCookie")("entitySeqId") = entitySeqId
    '   Response.Cookies("CCRCCookie")("entityTypeCode") = entityTypeCode
    '  Response.Cookies("CCRCCookie")("CurrentUser") = sUserId
    ' Response.Cookies("CCRCCookie").Expires = DateTime.Now.AddDays(1)

    'Dim CCRCCookie As New HttpCookie("CCRCCookiexx")
    'CCRCCookie.Value = "xyz"
    'CCRCCookie.Expires = DateTime.Now.AddDays(1)
    'Response.Cookies.Add(CCRCCookie)
        
    'End If
 %>
 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>CCRC Home</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
	</head>
	<body>
		<form>
			<table>
				<tr>
					<td   width="100%" height="100%" align="center" valign="middle">
						<img src="/ccrc/images/family1.jpg" width="188" height="265">
						<img src="/ccrc/images/family2.jpg" width="188" height="265">
						<img src="/ccrc/images/WomanAndChild.jpg" width="188" height="265">
					</td>					
				</tr>
				<tr>
					<td><br>
						Version 2.2<br>
						Date: 07/01/2005<br>
						<br>
						Best viewed at 800 x 600
					</td>					
				</tr>	
				<tr>
					<td><br>
						<br>
						<br>
						<br>
					</td>
				</tr>
				<tr>
					<td width="100%">
						<table>
							<tr>
								<td style="left:100px;" align="left" valign="middle" width="100%" height="100%"><img src="/ccrc/images/Final Logo 2-color.jpg"></td>
								<td style="left:175px;" align="left" valign="middle" width="100%" height="100%"><img src="/ccrc/images/ccrc.jpg"></td>
							</tr>
						</table>
					</td>
				</tr>				
			</table>
		</form>
		
		
		
		
		
		
	</body>
</html>

