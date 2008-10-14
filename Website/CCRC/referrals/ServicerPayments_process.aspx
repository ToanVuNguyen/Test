<%@ Page Language="VB" aspcompat=true %>
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<%




	On Error Resume Next

	Dim obj, RC

	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		 obj = Nothing
		Response.Clear 
		Response.End 
	End If

	RC = obj.ProcessServicerPayments(Request("stringData"), sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Main.CMain.ProcessServicerPayments", eRetPage, eRetBtnText) Then
		 obj = Nothing
		Response.Clear 
		Response.End 
	End If
    obj = Nothing
    
    If RC <> Nothing Then
        If RC.ToString.Length = 0 Then
            RC = "Servicer payments processed successfully"
        End If
    Else
        RC = "Servicer payments processed successfully"
    End If
    
	Response.Redirect ("ServicerPayments.aspx?errLog=" & RC)

%>
