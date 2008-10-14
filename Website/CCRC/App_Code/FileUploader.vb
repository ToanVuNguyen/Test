
Imports Microsoft.VisualBasic

Public Class FileUploader
    Private Files
    Private mcolFormElem
    Public RawData

    'Private Sub Class_Initialize()

    'End Sub

    'Private Sub Class_Terminate()

    'End Sub

    Public ReadOnly Property FormCollection()
        Get
            Return mcolFormElem
        End Get
    End Property

    Public ReadOnly Property FileCollection()
        Get
            Return Files
        End Get
    End Property

    Public ReadOnly Property File(ByVal sIndex)
        Get
            Dim temp = Nothing
            File = ""
            If Files.Exists(LCase(sIndex)) Then
                temp = Files.Item(LCase(sIndex))
            End If
            Return temp
        End Get
    End Property

    Public ReadOnly Property Form(ByVal sIndex)
        Get
            Dim Temp = ""
            Form = ""
            If mcolFormElem.Exists(LCase(sIndex)) Then
                Temp = mcolFormElem.Item(LCase(sIndex))
            End If
            Return Temp
        End Get
    End Property

    Public Sub Upload()
        Dim biData, sInputName
        Dim nPosBegin, nPosEnd, nPos, vDataBounds, nDataBoundPos
        Dim nPosFile, nPosBound

        biData = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.TotalBytes)
        RawData = biData
        nPosBegin = 1
        ' nPosEnd = InStr(nPosBegin, biData, CByteString(Chr(13)))
        Dim bitestr As String = CByteString(Chr(13))
        nPosEnd = InStr(nPosBegin, biData.ToString, bitestr)

        If (nPosEnd - nPosBegin) <= 0 Then Exit Sub

        vDataBounds = Mid(biData, nPosBegin, nPosEnd - nPosBegin)
        nDataBoundPos = InStr(1, biData, vDataBounds)

        Do Until nDataBoundPos = InStr(biData, vDataBounds & CByteString("--"))

            nPos = InStr(nDataBoundPos, biData, CByteString("Content-Disposition"))
            nPos = InStr(nPos, biData, CByteString("name="))
            nPosBegin = nPos + 6
            nPosEnd = InStr(nPosBegin, biData, CByteString(Chr(34)))
            sInputName = CWideString(Mid(biData, nPosBegin, nPosEnd - nPosBegin))
            nPosFile = InStr(nDataBoundPos, biData, CByteString("filename="))
            nPosBound = InStr(nPosEnd, biData, vDataBounds)

            If nPosFile <> 0 And nPosFile < nPosBound Then
                Dim oUploadFile, sFileName
                oUploadFile = New UploadedFile

                nPosBegin = nPosFile + 10
                nPosEnd = InStr(nPosBegin, biData, CByteString(Chr(34)))
                sFileName = CWideString(Mid(biData, nPosBegin, nPosEnd - nPosBegin))
                oUploadFile.FileName = Right(sFileName, Len(sFileName) - InStrRev(sFileName, "\"))

                nPos = InStr(nPosEnd, biData, CByteString("Content-Type:"))
                nPosBegin = nPos + 14
                nPosEnd = InStr(nPosBegin, biData, CByteString(Chr(13)))

                oUploadFile.ContentType = CWideString(Mid(biData, nPosBegin, nPosEnd - nPosBegin))

                nPosBegin = nPosEnd + 4
                nPosEnd = InStr(nPosBegin, biData, vDataBounds) - 2
                oUploadFile.FileData = Mid(biData, nPosBegin, nPosEnd - nPosBegin)

                If oUploadFile.FileSize > 0 Then
                    Files.Add(LCase(sInputName), oUploadFile)
                End If
            Else
                nPos = InStr(nPos, biData, CByteString(Chr(13)))
                nPosBegin = nPos + 4
                nPosEnd = InStr(nPosBegin, biData, vDataBounds) - 2
                If Not mcolFormElem.Exists(LCase(sInputName)) Then
                    mcolFormElem.Add(LCase(sInputName), CWideString(Mid(biData, nPosBegin, nPosEnd - nPosBegin)))
                End If
            End If

            nDataBoundPos = InStr(nDataBoundPos + Len(vDataBounds), biData, vDataBounds)
        Loop
    End Sub

    'String to byte string conversion
    Private Function CByteString(ByVal sString)
        Dim nIndex
        CByteString = ""
        For nIndex = 1 To Len(sString) - 1
            CByteString = CByteString & Chr(Asc(Mid(sString, nIndex, 1)))
        Next
    End Function

    'Byte string to string conversion
    Private Function CWideString(ByVal bsString)
        Dim nIndex
        CWideString = ""
        For nIndex = 1 To Len(bsString)
            CWideString = CWideString & Chr(Asc(Mid(bsString, nIndex, 1)))
        Next
    End Function

    Public Sub New()
        Files = HttpContext.Current.Server.CreateObject("Scripting.Dictionary")
        mcolFormElem = HttpContext.Current.Server.CreateObject("Scripting.Dictionary")
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If Not Files Is Nothing Then
            'Files.RemoveAll()
            Files = Nothing
        End If
        If Not mcolFormElem Is Nothing Then
            'mcolFormElem.RemoveAll()
            mcolFormElem = Nothing
        End If
    End Sub
End Class

Public Class UploadedFile
    Public ContentType
    Public FileName
    Public FileData

    Public ReadOnly Property FileSize()
        Get
            Return Len(FileData)
        End Get
    End Property

    Public Sub SaveToDisk(ByVal sPath)
        Dim oFS, oFile
        Dim nIndex

        If sPath = "" Or FileName = "" Then Exit Sub
        If Mid(sPath, Len(sPath)) <> "\" Then sPath = sPath & "\"

        oFS = HttpContext.Current.Server.CreateObject("Scripting.FileSystemObject")
        If Not oFS.FolderExists(sPath) Then Exit Sub

        oFile = oFS.CreateTextFile(sPath & FileName, True)

        For nIndex = 1 To Len(FileData)
            oFile.Write(Chr(Asc(Mid(FileData, nIndex, 1))))
        Next

        oFile.Close()
    End Sub

    Public Sub SaveToDatabase(ByRef oField)
        If Len(FileData) = 0 Then Exit Sub

        If Not oField Is Nothing Then
            oField.AppendChunk(FileData)
        End If
    End Sub

End Class


