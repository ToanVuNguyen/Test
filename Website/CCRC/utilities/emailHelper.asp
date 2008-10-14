<%@ LANGUAGE="VBSCRIPT" %>
<% Option Explicit %>
<!--#include file="formParser.asp"-->

<%

Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage

Response.Buffer = True		
if NOT Response.IsClientConnected then
  response.end
end if

Dim myReferer
Dim myServer
Dim myIP
Dim myOS
Dim myBrowser

myReferer = Request.Servervariables("HTTP_REFERER")
myServer = Request.Servervariables("SERVER_NAME")
myIP = Request.Servervariables("REMOTE_ADDR")
myBrowser = Request.Servervariables("HTTP_USER_AGENT")

Dim myHeader
Dim myFooter
Dim errCode
Dim strContentType
Dim strAppendMail
Dim strUplMsg
Dim NL
Dim WebID
Dim strBodyConfirm
Dim strStrConfirm
Dim WebName
Dim SendToName
Dim SendToAddress
Dim CommentFooter
Dim UserId
Dim bolReadMenuDB
Dim x
Dim strErrorMessage
Dim WebMenu
Dim myString
Dim myChar
Dim atLocation
Dim atCount
Dim tempLocation
Dim txtCount
Dim myText
Dim strMailName
Dim txtLocation
Dim tempTxtLacation
Dim myRestrictDomain
Dim myWebMasterDomain
Dim intElementCount
Dim strBody
Dim i
Dim str
Dim HTML
Dim strTo
Dim strFrom
Dim strSubject
Dim lngImportance
Dim bolGoodName
Dim bolSendMultiple
Dim frmItem
Dim strMsgInfo
Dim strTruncate
Dim myStr
Dim myChr
Dim atLoc
Dim FromName
Dim FromAddress
Dim myCDOMail
Dim strPartial
Dim upl
Dim item
Dim filename1
Dim filename2
Dim filename3
Dim objFSO
Dim strFileName1
Dim strFileName2
Dim strFileName3
Dim myTxt
Dim tempTxtLocation
Dim SendToMultiple
Dim strHTTPHeader
Dim strCc
Dim strBcc
Dim SiteSeqIDorName
Dim Rtrn_WebSeqID
Dim Rtrn_Webname
Dim Rtrn_WebDSCR
Dim Rtrn_Comment_Text
Dim Rtrn_Frame_ID
Dim Rtrn_ContactName
Dim Rtrn_ContactPhone
Dim Rtrn_ContactEmail
Dim Rtrn_UpdateDate
Dim Rtrn_UpdateUser
Dim RecordsReturned 
Dim SQLStmt
Dim SQLCode
Dim SQLMessage          

Dim RC
Dim obj
Dim MyLogNoun       
Dim MyLogVerb       
Dim MyLogReferer    
Dim MyLogSort       
Dim MyLogText       
Dim MyLogCounter    
Dim MyLogErrorNumber
Dim Web_Seq_ID
Dim ReturnCode
Dim Message
Dim myLen
Dim FldName(100)
Dim FldValue(100)
Dim FldLabel(100)
Dim FldOrder(100)
Dim FldCounter
Dim idx
Dim Seperator

errCode = 0

%>
<!--#INCLUDE FILE="StringUtilities.asp"-->
<!--#INCLUDE virtual="/ice/runtime/includes/getuserid.asp"-->
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
	End If
  
  'UserID = ""
  UserID = CurrentUserID
  
  If Not IsObject(upl.File("FILE1")) Then
    strUplMsg = strUplMsg 
 
  Else
'    upl.Path = "d:\application\comments\temp"
'    upl.MaxBytes = 1000000        'set upload limit to 1mb
    '--- Set the filename. You can put any valid filename here.
    If IsObject(upl.File("FILE1")) Then
      strFileName1 = Mid(upl.File("FILE1").FileName, InstrRev(upl.File("FILE1").FileName, "\") + 1)
      filename1 = strFileName1
    End if

    If IsObject(upl.File("FILE2")) Then
      strFileName2 = Mid(upl.File("FILE2").FileName, InstrRev(upl.File("FILE2").FileName, "\") + 1)
      filename2 = strFilename2         
    End if

    If IsObject(upl.File("FILE3")) Then 
      strFileName3 = Mid(upl.File("FILE3").FileName, InstrRev(upl.File("FILE3").FileName, "\") + 1)
      filename3 = strFileName3
    End if

    On Error Resume Next
    '--- Save the file now
    If IsObject(upl.File("FILE1")) Then
      Call upl.File("file1").SaveToDisk(strTempPath)
    End if
    If IsObject(upl.File("FILE2")) Then  
      Call upl.File("file2").SaveToDisk(strTempPath)
    End if
    If IsObject(upl.File("FILE3")) Then
      Call upl.File("file3").SaveToDisk(strTempPath)
    End if

    strUplMsg = strUplMsg & "<em>File(s) uploaded successfully</em>"

    if Err <> 0 Then 
      if err <> -2147352567 Then
        Response.Write Err.number & "<br>" & Err.Description & "<br>" & Err.Source & "<br>"
        strUplMsg = strUplMsg & "<h1><font COLOR=""#ff0000"">An error occurred " & _
          "when saving the file on the server.</font></h1>" & _
          "Possible causes include:" & _
          "<ul>" & _
           "<li>An incorrect filename was specified" & _
            "<li>File permissions do not allow writing to the specified area" & _
          "</ul>"
      end if  
    End If 
  end if

  NL = Chr(13) & Chr(10)

  'WebID = upl.form("WebID")

  if upl.form("WebID") > "" then
    WebID = upl.form("WebID")
  end if
  if upl.form("Web_ID") > "" then
    WebID = upl.form("Web_ID")
  end if
  if upl.form("Web_Seq_ID") > "" then
    WebID = upl.form("Web_Seq_ID")
  end if

  WebName = upl.form("WebName")
  SendToName = upl.form("SendToName")
  SendToAddress = upl.form("SendToAddress")
  CommentFooter = upl.form("CommentFooter")
  UserID = upl.form("UserID")
  bolReadMenuDB = False

  if Len(SendToAddress) > 0 And Len(SendToName) > 0 Then
    'Set up for email send to an individual
  Elseif Len(upl.form("SendToMultiple")) > 0 Then
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
  end if

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
  
  if Len(upl.Form("FromName")) > 0 then
    if Len(upl.Form("FromAddress")) > 0 then
      myStr = upl.Form("FromAddress")
      myChr = "@"
      atLoc = Instr(myStr, myChr)
      if atLoc = 0 then
        FromName = "Reusable Comments Form: " & upl.Form("FromName")
        FromAddress = "Webmaster@" & strMailName
      else
        FromName = upl.Form("FromName")
        FromAddress = upl.Form("FromAddress") 
      end if
    end if  
  else
    FromName = "Reusable Comments Form"
    FromAddress =   "Webmaster@" & strMailName     
  end if
  strCc = upl.Form("FromAddress")
      
  if Len(upl.form("SendToMultiple")) > 0 then
    strTo = upl.form("SendToMultiple")
  else
    strTo = SendToName & " (" & SendToAddress & ")"
  end if

  call CommentsSettings

  strFrom = FromName & " (" & FromAddress & ")"

  if Len(upl.Form("Subject")) > 0 then
	if "Sale Pending" = upl.form("firstContactStatusType") then
	    strSubject = "***URGENT - CONTACT BORROWER*** " & upl.form("Subject")
	else
	    strSubject = upl.form("Subject")
	end if	   
  else
    strSubject = "Reusable Comment Form"
  end if

  If len(upl.form("Cc")) > 0 then
    strCc = upl.form("Cc")
  End if 

  If len(upl.form("Bcc")) > 0 then
    strBcc = upl.form("Bcc")
  End if 

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
  If Len(strHTTPheader) > 0 then
    HTML = HTML & "<tr><td width=""15"">&nbsp;</td><td><b><big>" & "HTTP Header Information: </big></b><br>"
    HTML = HTML & strHTTPheader & "</td></tr>"
  End if
  HTML = HTML & "</table></font></body>"
  HTML = HTML & "</html>"
    
  on error resume next
  Set myCDOMail = CreateObject("CDO.Message")
  if err.number <> 0 then
    strErrorMessage = "An error has occurred."
    errCode = 2
    err.clear
  end if
    
'response.Write strTo & "<br>"
'response.Write strFrom & "<br>"


  myCDOMail.MimeFormatted = True
  myCDOMail.To = strTo
'  myCDOMail.From = strFrom
  myCDOMail.From = FromAddress
  myCDOMail.Subject = strSubject
  myCDOMail.HTMLBody = HTML 

  If len(strCc) > 0 Then
    myCDOMail.CC = strCc
    if err.number <> 0 then
      strErrorMessage = "An error has occurred."
      errCode = 3
      err.clear
    end if
  End if

  If len(strBcc) > 0 Then
    myCDOMail.BCC = strBcc
    if err.number <> 0 then
      strErrorMessage = "An error has occurred."
      errCode = 4
      err.clear
    end if
  End if

  If IsObject(upl.File("FILE1")) Then
    If Len(strFileName1) > 0 Then
      myCDOMail.AddAttachment(strTempPath & strFileName1)
      if err.number <> 0 then
        strErrorMessage = "An error has occurred."
        errCode = 5
        err.clear
      end if
    End If
  End If

  If IsObject(upl.File("FILE2")) Then
    If Len(strFileName2) > 0 Then
      myCDOMail.AddAttachment(strTempPath & strFileName2)
      if err.number <> 0 then
        strErrorMessage = "An error has occurred."
        errCode = 6
        err.clear
      end if
    End if
  End if

  If IsObject(upl.File("FILE3")) Then
    If Len(strFileName3) > 0 Then
      myCDOMail.AddAttachment(strTempPath & strFileName3)
      if err.number <> 0 then
        strErrorMessage = "An error has occurred."
        errCode = 7
        err.clear
      end if
    End if
  End if

  myCDOMail.Send 
  Set myCDOMail  = Nothing
  
	Dim obj2
	Set obj2 = CreateObject("CCRC_Main.CMain")
	Call obj2.UpdateDateCounselingSummarySent(upl.form("referralId"), Date, UserId, sSQL, iReturnCode, sMessage)
	Set obj2 = Nothing    

  'instantiate FSO and delete the attachment file
'response.Write Err.Number 
'response.End    

  on error resume next    
  Set objFSO = CreateObject("Scripting.FileSystemObject")
  If IsObject(upl.File("FILE1")) Then
    If objFSO.FileExists(strTempPath & strFileName1) Then
      objFSO.DeleteFile(strTempPath & strFileName1)  
    End if
  End if
  If IsObject(upl.File("FILE2")) Then
    If objFSO.FileExists(strTempPath & strFileName2) Then
      objFSO.DeleteFile(strTempPath & strFileName2)    
    End if
  End if
  If IsObject(upl.File("FILE3")) Then
    If objFSO.FileExists(strTempPath & strFileName3) Then
      objFSO.DeleteFile(strTempPath & strFileName3)    
    End if
  End if 
  Set objFSO = Nothing

  If Err.Number <> 0 Then
    If Err.Number <> -2147352567 Then
      If Err.Number <> -2147467259 Then
        Response.Write "<br>Number: " &  Err.Number & "<br>"
        Response.Write "Descr: " & Err.Description & "<br>"
        Response.Write "Source: " &  Err.Source & "<br>"
        strErrorMessage = "The email cannot be sent due to a problem with the server." & "<br>" & "<br>" & _
          "Contact support for help in resolving the problem."
        errCode = 8
        WriteErrorMessage
        response.end
      End if
    End If
  End if

  'turn on or off the error code testing
  'strErrorMessage = "test"
  'errCode = 1
'  Call Logtransaction

  if strErrorMessage > "" then
    WriteErrorMessage
  else
    If Len(upl.form("ConfirmURL")) > 0 then
       Response.Redirect(upl.form("ConfirmURL"))
    else
      'response.redirect "/ice/comments/commentthankyou.asp?" & request.querystring
      myHeader = upl.form("header")
      myFooter = upl.form("footer")

      response.redirect "../referrals/referral.asp?referralId=" & upl.form("referralId") & "&FORMODE=EDIT"

    end if
  end if
end if

'-----------------------------------------------------------------------------------------
'-----------------------------------------------------------------------------------------
Sub WriteErrorMessage
  if errCode > 1 then ProcessErrors
  response.redirect "/ice/comments/commentthankyou.asp?err=" & errCode
  response.end
End Sub
'-----------------------------------------------------------------------------------------
'-----------------------------------------------------------------------------------------
sub ProcessErrors()
  Dim objCDO
  on error resume next
  Set objCDO = Server.CreateObject("CDO.Message")
  objCDO.To = "ebusiness@rfc.com"
  objCDO.From = "ebusiness@rfc.com"
  objCDO.Subject = "A Comments Error Occurred"
  objCDO.HTMLBody = "At " & Now & " the following errors occurred on " & _
    "the page " & Request.ServerVariables("SCRIPT_NAME") & _
    ": " & _
    chr(10) & chr(10) & strErrorMessage & _
    chr(10) & chr(10) & "ReferringPage = " & Request.ServerVariables("HTTP_REFERER") & chr(10)                 
  objCDO.Send
  Set objCDO = Nothing
end sub  
'-----------------------------------------------------------------------------------------
'-----------------------------------------------------------------------------------------
Sub uploadSetup
  on error resume next
  Set upl = New FileUploader
  upl.Upload()
  if err.number <> 0 then
    strErrorMessage = "The comment cannot be sent due to a problem with the server." & "<br><br>" & _
      err.number & " : " & err.description & "<BR>" & _
      "Contact support for help in resolving the problem."
    errCode = 15
    WriteErrorMessage
  end if
End Sub
'-----------------------------------------------------------------------------------------
'-----------------------------------------------------------------------------------------
Sub Logtransaction
  Set obj = Server.CreateObject("ICE20_Runtime.Viewer")
  MyLogNoun        = "CMT"
  MyLogVerb        = "SEND"
  MyLogReferer     = "" 'Request.ServerVariables("HTTP_REFERER")
  MyLogSort        = 50
  MyLogText        = "Sending comment with : " & Request.ServerVariables("URL")
  MyLogCounter     = 0
  MyLogErrorNumber = 0
  Web_Seq_ID = WebID
  on error resume next
  RC = obj.Log_Event(CurrentDomainUser, Web_Seq_ID, MyLogVerb, MyLogNoun, Request.ServerVariables("HTTP_REFERER"), MyLogSort, MyLogText, "", MyLogErrorNumber, MyLogCounter, ReturnCode, Message)
  Set obj = Nothing 
End Sub
'----------------------------------------------------------------------------------------------------
'----------------------------------------------------------------------------------------------------
Sub CommentsSettings
%>
<!--#INCLUDE FILE="CommentsSettings.asp"-->
<% 
End Sub
'----------------------------------------------------------------------------------------------------
Sub QuickSort(DataArray, PointerArray, inLow, inHi)

   Dim pivot
   Dim tmpSwap
   Dim tmpLow
   Dim tmpHi
   
   tmpLow = inLow
   tmpHi = inHi
   
   pivot = LCase(DataArray(PointerArray((inLow + inHi) / 2)))
   While (tmpLow <= tmpHi)
     While (LCase(DataArray(PointerArray(tmpLow))) < pivot And tmpLow < inHi)
       tmpLow = tmpLow + 1
     Wend
     While (pivot < LCase(DataArray(PointerArray(tmpHi))) And tmpHi > inLow)
       tmpHi = tmpHi - 1
     Wend
     If (tmpLow <= tmpHi) Then
       tmpSwap = PointerArray(tmpLow)
       PointerArray(tmpLow) = PointerArray(tmpHi)
       PointerArray(tmpHi) = tmpSwap
       tmpLow = tmpLow + 1
       tmpHi = tmpHi - 1
     End If
   Wend
   
   If (inLow < tmpHi) Then Call QuickSort(DataArray, PointerArray, inLow, tmpHi)
   If (tmpLow < inHi) Then Call QuickSort(DataArray, PointerArray, tmpLow, inHi)
  
end sub
'----------------------------------------------------------------------------------------------------
%>
