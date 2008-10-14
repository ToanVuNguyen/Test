<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true CodeFile="referralBudget.aspx.vb" inherits=referrals_referral %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<%


On Error Resume Next

Dim obj
Dim RC

Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

RC = 0
obj = Server.CreateObject("CCRC_Main.CMain")
Select case ucase(Request.QueryString("FormMode"))
Case "ADD"
  RC = obj.Referral_Result_Type_History_Add(Request.QueryString("referralId"), Request.QueryString("referralResultType"), Request.QueryString("referralResultDate"), sUserId, sSQL, iReturnCode, sMessage)
  obj = Nothing

Case "EDIT"
  RC = obj.Referral_Result_Type_History_Update(Request.QueryString("referralResultId"), Request.QueryString("referralResultType"), Request.QueryString("referralResultDate"), Request.QueryString("referralId"), sUserId, sSQL, iReturnCode, sMessage)
  obj = Nothing

Case "DELETE"
  RC = obj.Referral_Result_Type_History_Delete(Request.QueryString("referralResultId"), sUserId, sSQL, iReturnCode, sMessage)
  obj = Nothing
End Select

If RC <> 0 Then
	Response.Write(sMessage & "<br><br>" & sSQL)
	Response.End
End If

If Request.QueryString("referralResultType") <> "9" And Request.QueryString("referralResultType") <> "10" And Request.QueryString("referralResultType") <> "11" Then
	Response.Redirect ("newcounselingSummary.aspx?referralId=" & Request.QueryString("referralId"))
Else
	Response.Redirect ("referral.aspx?referralId=" & Request.QueryString("referralId") & "&FormMode=EDIT")
End If

%>
