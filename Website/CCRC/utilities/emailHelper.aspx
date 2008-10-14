<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true CodeFile="emailHelper.aspx.vb" inherits=utilities_emailHelper  validateRequest=false  %> 
<!--#INCLUDE virtual='/ccrc/utilities/formParser.inc'-->
<%

    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Response.Buffer = True
    If Not Response.IsClientConnected Then
        Response.End()
    End If

    Dim myHeader
    Dim myFooter

    Dim strContentType
    Dim strAppendMail
    Dim strUplMsg
    Dim WebName
    Dim SendToName
 
    Dim CommentFooter
    Dim UserId
    Dim bolReadMenuDB
    Dim strBody
    
    Dim str
    Dim HTML
    Dim strTo
    Dim strFrom
    Dim strSubject
    Dim lngImportance
    Dim myStr
    Dim myChr
    Dim atLoc
    Dim FromName
    Dim FromAddress
    Dim myCDOMail
    Dim filename1
    Dim filename2
    Dim filename3
    Dim objFSO
    Dim strFileName1
    Dim strFileName2
    Dim strFileName3
    errCode = 0

    Dim strHTTPHeader
    Dim strCc
    Dim strBcc
    
    'Dim NL
    'Dim strBodyConfirm
    'Dim strStrConfirm
    'Dim SiteSeqIDorName
    'Dim Rtrn_WebSeqID
    'Dim Rtrn_Webname
    'Dim Rtrn_WebDSCR
    'Dim Rtrn_Comment_Text
    'Dim Rtrn_Frame_ID
    'Dim Rtrn_ContactName
    'Dim Rtrn_ContactPhone
    'Dim Rtrn_ContactEmail
    'Dim Rtrn_UpdateDate
    'Dim Rtrn_UpdateUser
    'Dim RecordsReturned 
    'Dim SQLStmt
    'Dim SQLCode
    'Dim SQLMessage          



    'Dim myTxt
    'Dim tempTxtLocation
       
    'Dim ReturnCode
    'Dim Message
    'Dim myLen
    'Dim FldName(100)
    'Dim FldValue(100)
    'Dim FldLabel(100)
    'Dim FldOrder(100)
    'Dim FldCounter
    'Dim idx
    'Dim Seperator


    'Dim myReferer
    'Dim myServer
    'Dim myIP
    'Dim myOS
    'Dim myBrowser

    'myReferer = Request.Servervariables("HTTP_REFERER")
    'myServer = Request.Servervariables("SERVER_NAME")
    'myIP = Request.Servervariables("REMOTE_ADDR")
    'myBrowser = Request.Servervariables("HTTP_USER_AGENT")
    'Dim x

    'Dim WebMenu
    'Dim myString
    'Dim myChar
    'Dim atLocation
    'Dim atCount
    'Dim tempLocation
    'Dim txtCount
    'Dim myText

    'Dim txtLocation
    'Dim tempTxtLacation
    '   Dim intElementCount
    'Dim i 
    'Dim bolGoodName

    'Dim frmItem
    'Dim strMsgInfo
    'Dim strTruncate
    'Dim strPartial

    'Dim item

%>
<!--#INCLUDE FILE="StringUtilities.inc"-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%

strContentType = lcase(Request.ServerVariables("HTTP_CONTENT_TYPE"))

'response.Write strContentType & "<br>"
'response.End

if instr(strContentType, "multipart/form-data") > 0 then

'response.Write "got here"
'response.End

  strAppendMail = ""
  strUplMsg = ""

  call uploadSetup
	dim strTempPath 
	strTempPath = "d:\application\comments\temp\"

	If upl.FileCollection.Count=0 then 
		strUplMsg = "Bad File Selected"
 	Response.Write(strUplMsg )
	Response.End
	End If
  
  'UserID = ""
  'UserID = CurrentUserID
   UserID = System.Web.HttpContext.Current.User.Identity
        'If Not IsObject(upl.File("FILE1")) Then
        
        If upl.File("FILE1") Is Nothing Then
            strUplMsg = strUplMsg
 
        Else
            '    upl.Path = "d:\application\comments\temp"
            '    upl.MaxBytes = 1000000        'set upload limit to 1mb
            '--- Set the filename. You can put any valid filename here.
            If Not upl.File("FILE1") Is Nothing Then
                'If IsObject(upl.File("FILE1")) Then
                strFileName1 = Mid(upl.File("FILE1").FileName, InStrRev(upl.File("FILE1").FileName, "\") + 1)
                filename1 = strFileName1
            End If

            'If IsObject(upl.File("FILE2")) Then
            If Not upl.File("FILE2") Is Nothing Then
                strFileName2 = Mid(upl.File("FILE2").FileName, InStrRev(upl.File("FILE2").FileName, "\") + 1)
                filename2 = strFileName2
            End If

            'If IsObject(upl.File("FILE3")) Then
            If Not upl.File("FILE3") Is Nothing Then
                strFileName3 = Mid(upl.File("FILE3").FileName, InStrRev(upl.File("FILE3").FileName, "\") + 1)
                filename3 = strFileName3
            End If

            'On Error Resume Next
            '--- Save the file now
            'If IsObject(upl.File("FILE1")) Then
            If Not upl.File("FILE1") Is Nothing Then
                Call upl.File("file1").SaveToDisk(strTempPath)
            End If
            'If IsObject(upl.File("FILE2")) Then
            If Not upl.File("FILE2") Is Nothing Then
                Call upl.File("file2").SaveToDisk(strTempPath)
            End If
            If Not upl.File("FILE3") Is Nothing Then
                'If IsObject(upl.File("FILE3")) Then
                Call upl.File("file3").SaveToDisk(strTempPath)
            End If

            strUplMsg = strUplMsg & "<em>File(s) uploaded successfully</em>"

            If Err.Number() <> 0 Then
                If Err.Number() <> -2147352567 Then
                    Response.Write(Err.Number & "<br>" & Err.Description & "<br>" & Err.Source & "<br>")
                    strUplMsg = strUplMsg & "<h1><font COLOR=""#ff0000"">An error occurred " & _
                      "when saving the file on the server.</font></h1>" & _
                      "Possible causes include:" & _
                      "<ul>" & _
                       "<li>An incorrect filename was specified" & _
                        "<li>File permissions do not allow writing to the specified area" & _
                      "</ul>"
                End If
            End If
        End If

        'NL = Chr(13) & Chr(10)

        'WebID = upl.form("WebID")

        If upl.form("WebID") > "" Then
            WebID = upl.form("WebID")
        End If
        If upl.form("Web_ID") > "" Then
            WebID = upl.form("Web_ID")
        End If
        If upl.form("Web_Seq_ID") > "" Then
            WebID = upl.form("Web_Seq_ID")
        End If

        WebName = upl.form("WebName")
        'SendToName = upl.form("SendToName")
        SendToName = Request("SendToName")
        'SendToAddress = upl.form("SendToAddress")
        SendToAddress = Request("SendToAddress")
        CommentFooter = upl.form("CommentFooter")
        UserId = upl.form("UserID")
        bolReadMenuDB = False

        If Len(SendToAddress) > 0 And Len(SendToName) > 0 Then
            'Set up for email send to an individual
        ElseIf Len(upl.form("SendToMultiple")) > 0 Then
            'Set up for email send to an individual
        Else
            strErrorMessage = "A email cannot be sent due to parameter error(s):" & "<br>" & _
              "<b>SendToName = </b>" & SendToName & "<br>" & _
              "<b>SendToAddress = </b>" & SendToAddress
            strErrorMessage = strErrorMessage & "<br>" & "<br>" & _
              "Calling page should specify a SendToName/SendToAddress combination." & "<br>"
            errCode = 1
        End If

        'for each item in upl.form
        '   if x = "Comments" Then
        '      If strErrorMessage = "" And Len(Trim(upl.form("Comments"))) < 1 Then  
        '       bolReadMenuDB = False
        '       strErrorMessage = "A comment cannot be sent because no comments were entered.<br><br>Please try again.<br><br>"
        '       errCode = 1
        '      end if
        '   End If
        'next

        If Len(upl.form("SendToMultiple")) > 0 Then
            bolSendMultiple = True
            SendToMultiple = upl.form("SendToMultiple")
        End If

        '  For Each frmItem in upl.formcollection
        '    Select Case ucase(frmItem)
        '      Case "SENDTOADDRESS", "CC", "BCC", "CMDSUBMIT", "CMDRETURN", "SUBMITIP", "FROMADDRESS", "SUBMITURL", "FROMNAME", "SUBJECT", "SUBMIT", "SUBMITIT", "RETURN", "FILE1", "FILE2", "FILE3", "B2", "B1", "SENDTOMULTIPLE", "SUBJECT", "ELEMENTCOUNT", "USERID", "COMMENTFOOTER", "CONTENTTYPE", "SUBMITIP", "SUBMITSERVER", "SUBMITPLATFORM", "SUBMITBROWSER", "CONFIRMURL", "WEBID", "SENDTONAME", "WEBNAME", "SPECIALFIELDS", "HEADER", "FOOTER", "WEB_ID", "WEB_SEQ_ID"
        '        frmItem= ""
        '    End Select
        '    ' look for hidden label fields, ignore them the first time through, look for them when you find a field...  LLL 04/25/02
        '    if frmItem <> "" then
        '      if len(frmItem) > 6 then
        '        if ucase(right(frmItem, 6)) = "_LABEL" then
        '          frmItem = ""
        '        end if
        '      end if
        '    end if
        '    if frmItem <> "" then
        '      if upl.Form(frmItem & "_LABEL") > "" then
        '        strMsgInfo = strMsgInfo & "<b>" & upl.Form(frmItem & "_LABEL") & ": </b>" & upl.Form(frmItem) & "<BR><BR>"
        '      else
        '        strMsgInfo = strMsgInfo & "<b>" & frmItem & ": </b>" & upl.Form(frmItem) & "<BR><BR>"
        '      end if
        '    end if
        '  Next
        '  if Len(trim(upl.form("specialfields"))) > 0 then
        '    strHTTPHeader=strHTTPHeader+"<b> URL: </b>" & NL & myReferer & "<BR><BR>"
        '    strHTTPHeader=strHTTPHeader+"<b> Server: </b>" & NL & myServer & "<BR><BR>"
        '    strHTTPHeader=strHTTPHeader+"<b> IP Address: </b>" & NL & myIP & "<BR><BR>"
        '    strHTTPHeader=strHTTPHeader+"<b> Browser: </b>" & NL & myBrowser & "<BR><BR>"
        '  else 
        '    str = ""
        '    strHTTPHeader = ""
        '  end if

        '  str = strMsgInfo

        str = str & "<B>" & upl.form("Subject") & "</B><BR><BR>"
        str = str & upl.form("emailHeader")
        str = str & "<BR>"
        str = str & "This summary is intended to provide you with a summary of the CCRC session. If you need more information or details, or would like CCRC input or assistance regarding a resolution with this homeowner, please contact the counselor directly.<BR>"
        '  str = str & "<BR>"
        str = str & upl.form("emailBody")
        str = str & upl.form("emailBody2")
        str = str & upl.form("emailBody3")
        str = str & "<B>Counselor Comments:</B><BR>"
        str = str & upl.form("emailComment")
  
        If Len(upl.Form("FromName")) > 0 Then
            If Len(upl.Form("FromAddress")) > 0 Then
                myStr = upl.Form("FromAddress")
                myChr = "@"
                atLoc = InStr(myStr, myChr)
                If atLoc = 0 Then
                    FromName = "Reusable Comments Form: " & upl.Form("FromName")
                    FromAddress = "Webmaster@" & strMailName
                Else
                    FromName = upl.Form("FromName")
                    FromAddress = upl.Form("FromAddress")
                End If
            End If
        Else
            FromName = "Reusable Comments Form"
            FromAddress = "Webmaster@" & strMailName
        End If
        strCc = upl.Form("FromAddress")
      
        If Len(upl.form("SendToMultiple")) > 0 Then
            strTo = upl.form("SendToMultiple")
        Else
            strTo = SendToName & " (" & SendToAddress & ")"
        End If

        Call CommentsSettings()

        strFrom = FromName & " (" & FromAddress & ")"

        If Len(upl.Form("Subject")) > 0 Then
            If "Sale Pending" = upl.form("firstContactStatusType") Then
                strSubject = "***URGENT - CONTACT BORROWER*** " & upl.form("Subject")
            Else
                strSubject = upl.form("Subject")
            End If
        Else
            strSubject = "Reusable Comment Form"
        End If

        If Len(upl.form("Cc")) > 0 Then
            strCc = upl.form("Cc")
        End If

        If Len(upl.form("Bcc")) > 0 Then
            strBcc = upl.form("Bcc")
        End If

        HTML = HTML & "<html>"
        HTML = HTML & "<head>"
        HTML = HTML & "<title>Comment</title>"
        HTML = HTML & "</head>"
        HTML = HTML & "<body border=""0"" topmargin=""0"" leftmargin=""0"" marginwidth=""0"" "
        HTML = HTML & "marginheight=""0""  background=""/images/background_header_normal.gif"" "
        HTML = HTML & "<body bgcolor=""FFFFFF"" text=""000009""><font face=""Arial"">"
        HTML = HTML & "<table border=""0"" width=""85%"" vspace=""15"">"
        HTML = HTML & "<tr><td width=""15"">&nbsp;</td><td>" & str & "<br></td></tr>"
        HTML = HTML & "<tr><td width=""15"">&nbsp;</td><td>" & strBody & "</td></tr>"
        HTML = HTML & "<br></td></tr>"
        If Len(strHTTPHeader) > 0 Then
            HTML = HTML & "<tr><td width=""15"">&nbsp;</td><td><b><big>" & "HTTP Header Information: </big></b><br>"
            HTML = HTML & strHTTPHeader & "</td></tr>"
        End If
        HTML = HTML & "</table></font></body>"
        HTML = HTML & "</html>"
    
       On Error Resume Next
        myCDOMail = CreateObject("CDO.Message")
        If Err.Number <> 0 Then
            strErrorMessage = "An error has occurred."
		Response.Write(Err.Number & "<br>" & Err.Description & "<br>" & Err.Source & "<br>")
		Response.End()
            errCode = 2
            Err.Clear()
        End If
    
        'response.Write strTo & "<br>"
        'response.Write strFrom & "<br>"

	 
        myCDOMail.MimeFormatted = True
        myCDOMail.To = strTo
        '  myCDOMail.From = strFrom
        myCDOMail.From = FromAddress
        myCDOMail.Subject = strSubject
        myCDOMail.HTMLBody = HTML

        If Len(strCc) > 0 Then
            myCDOMail.CC = strCc
            If Err.Number <> 0 Then
                strErrorMessage = "An error has occurred."
                errCode = 3
		Response.Write("1")
		Response.End()
                Err.Clear()
            End If
        End If

        If Len(strBcc) > 0 Then
            myCDOMail.BCC = strBcc
            If Err.Number <> 0 Then
                strErrorMessage = "An error has occurred."
                errCode = 4
		Response.Write("2" )
		Response.End()
                Err.Clear()
            End If
        End If

        If Not upl.File("FILE1") Is Nothing Then ' If IsObject(upl.File("FILE1")) Then
            If Len(strFileName1) > 0 Then
                myCDOMail.AddAttachment(strTempPath & strFileName1)
                If Err.Number <> 0 Then
                    strErrorMessage = "An error has occurred."
                    errCode = 5
		Response.Write("3" )
		Response.End()	
                    Err.Clear()
                End If
            End If
        End If

        If Not upl.File("FILE2") Is Nothing Then 'If IsObject(upl.File("FILE2")) Then
            If Len(strFileName2) > 0 Then
                myCDOMail.AddAttachment(strTempPath & strFileName2)
                If Err.Number <> 0 Then
                    strErrorMessage = "An error has occurred."
                    errCode = 6
		Response.Write("4")
		Response.End()
                    Err.Clear()
                End If
            End If
        End If

        If Not upl.File("FILE3") Is Nothing Then
            If Len(strFileName3) > 0 Then
                myCDOMail.AddAttachment(strTempPath & strFileName3)
                If Err.Number <> 0 Then
                    strErrorMessage = "An error has occurred."
                    errCode = 7
		Response.Write("5" )
		Response.End()
                    Err.Clear()
                End If
            End If
        End If

        myCDOMail.Send()
        myCDOMail = Nothing
  
        Dim obj2
        obj2 = CreateObject("CCRC_Main.CMain")
        obj2.UpdateDateCounselingSummarySent(upl.form("referralId"), Date.Now, UserId, sSQL, iReturnCode, sMessage)
        obj2 = Nothing

        'instantiate FSO and delete the attachment file
        Response.Write(Err.Number & "<br>" & Err.Description & "<br>" & Err.Source & "<br>1" & upl.form("referralId")& "1" &"*" & upl.form("Subject") &"*" )
        response.End    

        On Error Resume Next
        objFSO = CreateObject("Scripting.FileSystemObject")
        If Not upl.File("FILE1") Is Nothing Then 'If IsObject(upl.File("FILE1")) Then
            If objFSO.FileExists(strTempPath & strFileName1) Then
                objFSO.DeleteFile(strTempPath & strFileName1)
            End If
        End If
        If Not upl.File("FILE2") Is Nothing Then 'If IsObject(upl.File("FILE2")) Then
            If objFSO.FileExists(strTempPath & strFileName2) Then
                objFSO.DeleteFile(strTempPath & strFileName2)
            End If
        End If
        If Not upl.File("FILE3") Is Nothing Then
            If objFSO.FileExists(strTempPath & strFileName3) Then
                objFSO.DeleteFile(strTempPath & strFileName3)
            End If
        End If
        objFSO = Nothing

        If Err.Number <> 0 Then
            If Err.Number <> -2147352567 Then
                If Err.Number <> -2147467259 Then
                    Response.Write("<br>Number: " & Err.Number & "<br>")
                    Response.Write("Descr: " & Err.Description & "<br>")
                    Response.Write("Source: " & Err.Source & "<br>")
                    strErrorMessage = "The email cannot be sent due to a problem with the server." & "<br>" & "<br>" & _
                      "Contact support for help in resolving the problem."
                    errCode = 8
                    WriteErrorMessage()
                    Response.End()
                End If
            End If
        End If

        'turn on or off the error code testing
        'strErrorMessage = "test"
        'errCode = 1
        '  Call Logtransaction

        If strErrorMessage > "" Then
            WriteErrorMessage()
        Else
            If Len(upl.form("ConfirmURL")) > 0 Then
                Response.Redirect(upl.form("ConfirmURL"))
            Else
                'response.redirect "/ice/comments/commentthankyou.asp?" & request.querystring
                myHeader = upl.form("header")
                myFooter = upl.form("footer")

                'Response.Redirect("../referrals/referral.aspx?referralId=" & upl.form("referralId") & "&FORMODE=EDIT")
                Response.Redirect("../referrals/referral.aspx?referralId=" & Request("referralId") & "&FORMODE=EDIT")

            End If
        End If
    End If

'-----------------------------------------------------------------------------------------

%>
<html>
<body>

</body>
</html>
