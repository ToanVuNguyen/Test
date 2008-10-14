<%@ Page Language="VB" aspcompat=true AutoEventWireup="false"%>
<% 
    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Dim obj
	Dim varloanId, referralId, zipCode, profileSeqId
    obj = CreateObject("CCRC_Main.CMain")
	varloanId = "55555"
zipCode = "55555"
profileSeqId="30479"
    Dim Rs
    Rs = obj.Get_Referral(varloanId, referralId, zipCode, profileSeqId, sUserId, sSQL, iReturnCode, sMessage)

 %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
 <HEAD>
  <TITLE>Test Page </TITLE>
 </HEAD>

 <BODY>
  This is a test page
 </BODY>
</HTML>
