<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true  %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<%

On Error Resume Next

Dim obj
Dim RC
Dim fld
Dim row, rows, cols, subCategorySeqId

Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

RC = 0
obj = Server.CreateObject("CCRC_Main.CMain")

RC = obj.Referral_Budget_Delete(Request("referralId"), sUserId, sSQL, iReturnCode, sMessage)

rows = split(Request.Form.ToString(),"&")
for each row in rows
	cols = split(row,"=")
	If cols(1) > 0 Then
		If cols(0) <> "referralId" Then
			If cols(1) <> "" Then
				If Left(cols(0),3) = "txt" Then
					subCategorySeqId = Right(cols(0), Len(cols(0))-3)
				End If
				RC = obj.Referral_Budget_Add(Request("referralId"), subCategorySeqId, cols(1), sUserId, sSQL, iReturnCode, sMessage)
			End If
		End If
	End If
next

obj = Nothing

Response.Redirect ("referralBudget.aspx?referralId=" & Request("referralId"))




%>
