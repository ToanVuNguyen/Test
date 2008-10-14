Imports System.Configuration
Partial Class utilities_emailHelper
    Inherits System.Web.UI.Page
    Protected errCode As Integer
    Protected strErrorMessage As String
    Protected obj As Object
    Protected upl
    Protected MyLogNoun As String
    Protected MyLogVerb
    Protected MyLogReferer
    Protected MyLogSort
    Protected MyLogText
    Protected MyLogCounter
    Protected MyLogErrorNumber
    Protected Web_Seq_ID
    Protected WebID
    Protected strMailName
    Protected bolSendMultiple
    Protected RC
    Protected SendToAddress
    Protected SendToMultiple
    Protected myRestrictDomain
    Protected myWebMasterDomain

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub
    '-----------------------------------------------------------------------------------------
    Protected Sub WriteErrorMessage()
        If errCode > 1 Then ProcessErrors()
        Response.Redirect("ICE/commentthankyou.aspx?err=" & errCode)
        Response.End()
    End Sub
    '-----------------------------------------------------------------------------------------
    '-----------------------------------------------------------------------------------------
    Protected Sub ProcessErrors()
        Dim objCDO
        Dim Emailto As String
        Emailto = ConfigurationManager.AppSettings("ERROREMAIL")
        On Error Resume Next
        objCDO = Server.CreateObject("CDO.Message")
        objCDO.To = Emailto
        objCDO.From = Emailto
        objCDO.Subject = "A Comments Error Occurred"
        objCDO.HTMLBody = "At " & Now & " the following errors occurred on " & _
        "the page " & Request.ServerVariables("SCRIPT_NAME") & _
        ": " & _
        Chr(10) & Chr(10) & strErrorMessage & _
        Chr(10) & Chr(10) & "ReferringPage = " & Request.ServerVariables("HTTP_REFERER") & Chr(10)
        objCDO.Send()
        objCDO = Nothing
    End Sub
    '-----------------------------------------------------------------------------------------
    '-----------------------------------------------------------------------------------------
    Protected Sub uploadSetup()
        On Error Resume Next
        upl = New FileUploader
        upl.Upload()
        If Err.Number <> 0 Then
            strErrorMessage = "The comment cannot be sent due to a problem with the server." & "<br><br>" & _
              Err.Number & " : " & Err.Description & "<BR>" & _
              "Contact support for help in resolving the problem."
            errCode = 15
            WriteErrorMessage()
        End If
    End Sub
    '-----------------------------------------------------------------------------------------
    '-----------------------------------------------------------------------------------------
    Protected Sub Logtransaction()
        obj = Server.CreateObject("ICE20_Runtime.Viewer")
        MyLogNoun = "CMT"
        MyLogVerb = "SEND"
        MyLogReferer = "" 'Request.ServerVariables("HTTP_REFERER")
        MyLogSort = 50
        MyLogText = "Sending comment with : " & Request.ServerVariables("URL")
        MyLogCounter = 0
        MyLogErrorNumber = 0
        Web_Seq_ID = WebID
        On Error Resume Next
        'RC = obj.Log_Event(CurrentDomainUser, Web_Seq_ID, MyLogVerb, MyLogNoun, Request.ServerVariables("HTTP_REFERER"), MyLogSort, MyLogText, "", MyLogErrorNumber, MyLogCounter, ReturnCode, Message)
        obj = Nothing
    End Sub
    '----------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------
    Protected Sub QuickSort(ByVal DataArray, ByVal PointerArray, ByVal inLow, ByVal inHi)

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
            End While
            While (pivot < LCase(DataArray(PointerArray(tmpHi))) And tmpHi > inLow)
                tmpHi = tmpHi - 1
            End While
            If (tmpLow <= tmpHi) Then
                tmpSwap = PointerArray(tmpLow)
                PointerArray(tmpLow) = PointerArray(tmpHi)
                PointerArray(tmpHi) = tmpSwap
                tmpLow = tmpLow + 1
                tmpHi = tmpHi - 1
            End If
        End While

        If (inLow < tmpHi) Then Call QuickSort(DataArray, PointerArray, inLow, tmpHi)
        If (tmpLow < inHi) Then Call QuickSort(DataArray, PointerArray, tmpLow, inHi)

    End Sub
    '----------------------------------------------------------------------------------------------------

    Public Function CommentsSettings()
        If bolSendMultiple = True Then
            strMailName = UCase(Mid(SendToMultiple, InStrRev(SendToMultiple, "@") + 1))
            If InStrRev(strMailName, ")") Then
                strMailName = Replace(strMailName, ")", "")
            End If
            If InStrRev(strMailName, ";") Then
                strMailName = Replace(strMailName, ";", "")
            End If
            strMailName = Trim(strMailName)
        Else
            strMailName = UCase(Mid(SendToAddress, InStrRev(SendToAddress, "@") + 1))
        End If

        Select Case strMailName
            Case "CCRCHOPE.COM"
                strMailName = "CCRCHOPE.COM"
            Case "GMACRFC.COM"
                strMailName = "GMACRFC.COM"
            Case "RFC.COM"
                strMailName = "RFC.COM"
            Case "HOMECOMINGS.COM"
                strMailName = "HOMECOMINGS.COM"
            Case "HFWHOLESALE.COM"
                strMailName = "HFWHOLESALE.COM"
            Case "RESMONEY.COM"
                strMailName = "RESMONEY.COM"
            Case "RMCWHOLESALE.COM"
                strMailName = "RMCWHOLESALE.COM"
            Case "GMACRFC.CO.UK"
                strMailName = "GMACRFC.CO.UK"
            Case "GMACMH.COM"
                strMailName = "GMACMH.COM"
            Case ""
                strMailName = ""
                '     Case Else
                '	strErrorMessage = "The comment cannot be sent because the Send To email address or addresses are outside the RFC domain." & "<br>" & "<br>" & _
                '            "<b>WebName:</b> " & WebName & "<br>" & _
                '            "<b>Send To Name:</b> " & SendToName & "<br>" & _
                '            "<b>Send To Address:</b> " & SendToAddress & "<br><br>" & _
                '            "Contact support for help in resolving the problem."
                '            WriteErrorMessage
                '            response.end
        End Select

        'Previous variable names:
        myRestrictDomain = strMailName
        myWebMasterDomain = strMailName
    End Function
End Class
