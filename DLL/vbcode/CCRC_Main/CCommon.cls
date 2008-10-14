VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cCommon"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
' -------------------------------------------------------------------------------
' This class file contains common functions are used by most of the other classes.
' Most of these classes instantiate this class at the top, so it's always there.
' It also contains property variables that are retrieved from the registry,
' which the other classes use.
' Created by Lyle Luppes 04/2001
' Last Update 05/30/2002
' -------------------------------------------------------------------------------
Option Explicit
Const sModName = "CCommon"             'module name used in logging
Private m_Reg As String
' root level registry location - first level under hkey_local_machine\software
' if you want to have your settings in Software\Company\MyApp, then set this to Company\MyApp
' the following line should be placed in a module file in your project:
Private Const REGISTRY_ROOT = "MYCompany"
Private sDebugSwitch As String         'debug switch - ON or OFF
Private sServerName As String          'server name for logging
Private sRuntimeDirectory As String    'directory name for application
Private sCacheDirectory As String      'cache directory name for application
Private sEmailErrorsTo As String       'email severe errors to this address
Private sRestrictedMode As String      'application restricted to admins - YES or NO
Private sCacheOnlyMode As String       'application will not read from DB, only cache - YES or NO
Private sErrorFileName As String       'file name where errors and sql gets logged
Private sConnectString As String       'connection string - DSN=xxx;PWD=xxx;UID=xxx;
Private sConnectStringUAL As String    'UAL connection string - DSN=xxx;PWD=xxx;UID=xxx;
Private sDBEngine As String            'database engine - ORACLE, MSSQL, or ACCESS
Private sDBEnvironment As String       'database environment - blank, or DEVL,INTG,PROD, etc.
Private sDBTimeout As String           'database timeout value
Private sDBWildCard As String          'wildcard for sql searches
Private sUALSource As String           'which tables are the UAL data found in?
Private sEmailType As String           'type of email engine to use - CDONTS, SMTPMAILWRAPPER, NONE
Private sEmailFrom As String           'email to use in the from and reply to fields
Private sEmailDomainRestrict As String 'if present, only send emails to this domain
Private sDomainRestrict As String      'if present, only access this domain
Private sSuper_User_Groups As String   'list of NT groups that are super users
Private sDefaultDomain As String       'default domain for functions if one is not supplied
Private sTiming As String              'should functions be timed?
Private sTimingFile As String          'where should timing info be stored
Private sAsyncLogMethod As String      'async logging method - FILE, MSMQ, DATABASE
Private sAsyncLogQueue As String       'async logging MSMQ Queue Name
Private sAsyncLogMenu  As String       'async logging switch for menus
Private sRequestEmail  As String
Private sNewUserEmail  As String
Private sBaseURL       As String
Private sExchangeServer As String      'email server
Private iMaxUserIDLen As Integer       'max length of user id - introduced to support long names for Mexico

Private lErrorNumber            As Long
Private strErrorDescription     As String
Private strErrorSource          As String
    

Private Sub Class_Initialize()
  Call Prime
End Sub

Friend Function Prime() As Integer
  Dim Reg As New cReg
  
  On Error GoTo errorHandler
  
  sConnectString = ""
  sConnectStringUAL = ""
  sErrorFileName = DateifyFileName("c:\ccommon_.log")
  sRestrictedMode = "NO"
  sCacheOnlyMode = "NO"
  sDBEngine = ""
  sDebugSwitch = "ON"
  sDBEnvironment = ""
  sDBTimeout = "15"
  iMaxUserIDLen = 8
    
  m_Reg = AddLastBackSlash(REGISTRY_ROOT)

  'Get the root level info from the Registry
  If Reg.OpenSoftwareKey(m_Reg) Then
    sEmailDomainRestrict = Reg.QueryValueStr("Email_Domain_Restrict")
    sEmailErrorsTo = Reg.QueryValueStr("Email_Errors_To")
    sEmailFrom = Reg.QueryValueStr("Email_From")
    sEmailType = Trim(UCase(Reg.QueryValueStr("Email_Type")))
'    sServerName = Reg.QueryValueStr("Server_Name")
    sRuntimeDirectory = Reg.QueryValueStr("Runtime_Directory")
    If Not Right(sRuntimeDirectory, 1) = "/" Then sRuntimeDirectory = sRuntimeDirectory & "/"
    sCacheDirectory = Reg.QueryValueStr("Cache_Directory")
    If Not Right(sCacheDirectory, 1) = "\" Then sCacheDirectory = sCacheDirectory & "\"
    sSuper_User_Groups = Reg.QueryValueStr("Super_User_Groups")
    If sSuper_User_Groups > "" Then sSuper_User_Groups = Replace(sSuper_User_Groups, ";", ",")
    sDefaultDomain = Reg.QueryValueStr("Default_Domain")
    sDomainRestrict = Reg.QueryValueStr("Domain_Restrict")
    If Not sDefaultDomain > "" Then sDefaultDomain = "WORKGROUP"
    sUALSource = Reg.QueryValueStr("UAL_SOURCE")
    sConnectStringUAL = Reg.QueryValueStr("UAL_DB_ConnectionString")
    sConnectString = Reg.QueryValueStr("DB_ConnectionString")
    sDBTimeout = Reg.QueryValueStr("DB_Timeout")
    If Trim(sDBTimeout) = "" Then sDBTimeout = "15"
    
    If InStr(1, UCase(sConnectString), "ORACLE") > 0 Then
      sDBEngine = "ORACLE"
      sDBWildCard = "%"
    Else
      If InStr(1, UCase(sConnectString), "SQL SERVER") > 0 Or InStr(1, UCase(sConnectString), "SQLOLEDB") > 0 Then
        sDBEngine = "MSSQL"
        sDBWildCard = "%"
      Else
        sDBEngine = Reg.QueryValueStr("DB_Engine")
        sDBWildCard = "%"
      End If
    End If
    sDebugSwitch = Reg.QueryValueStr("Debug_Mode")
    sRestrictedMode = Reg.QueryValueStr("Restricted_Mode")
    sCacheOnlyMode = Reg.QueryValueStr("Cache_Only_Mode")
    sTiming = Reg.QueryValueStr("Timing_Mode")
    sExchangeServer = Reg.QueryValueStr("ExchangeServer")
    sAsyncLogMethod = Reg.QueryValueStr("Async_Log_Method")
    sAsyncLogQueue = Reg.QueryValueStr("Async_Log_Queue")
    sAsyncLogMethod = UCase(Trim(sAsyncLogMethod))
    If Trim(sAsyncLogMethod) = "" Then sAsyncLogMethod = "FILE"
    If Trim(sAsyncLogMethod) = "MSMQ" Then
      If Trim(sAsyncLogQueue) = "" Then sAsyncLogQueue = "CCRC_LOG"
    End If
    sAsyncLogMenu = Reg.QueryValueStr("Async_Log_Menu")
    sAsyncLogMenu = UCase(Trim(sAsyncLogMenu))
    If sAsyncLogMenu <> "ON" And sAsyncLogMenu <> "OFF" Then sAsyncLogMenu = "ON"
    sRequestEmail = Reg.QueryValueStr("Email_Request")
    sNewUserEmail = Reg.QueryValueStr("Email_NewUser")
    sBaseURL = Reg.QueryValueStr("Base_URL")
    If Not Right(sBaseURL, 1) = "/" Then sBaseURL = sBaseURL & "/"
    iMaxUserIDLen = CInt(IIf(Len(Reg.QueryValueStr("Max_UserID_Length")) > 0, Reg.QueryValueStr("Max_UserID_Length"), 8))
  Else
    Call WriteToLog("CCommon:Prime: Could not open registry key HKEY_LOCAL_MACHINE\" & m_Reg)
  End If
  Reg.CloseKey
  
  'Get the application level info from the Registry
'  If Reg.OpenSoftwareKey(m_Reg & App.EXEName) Then
'    sErrorFileName = DateifyFileName(Reg.QueryValueStr("Error_FileName"))
'  Else
'    Call WriteToLog("CCommon:Prime: Could not open registry key HKEY_LOCAL_MACHINE\" & m_Reg & App.EXEName)
'  End If
'  Reg.CloseKey
  
  sErrorFileName = DateifyFileName(Left(App.Path, 1) & ":\Logs\" & App.EXEName & "_.Log")
  sTimingFile = Left(sErrorFileName, Len(sErrorFileName) - 4) & "_Timing.Log"
  If Len(sCacheDirectory) > 1 Then
    If Left(sCacheDirectory, 1) <> Left(sErrorFileName, 1) Then
      sCacheDirectory = Left(sErrorFileName, 1) & Right(sCacheDirectory, Len(sCacheDirectory) - 1)
    End If
  End If
  Set Reg = Nothing
  
  'sServerName = GetServerName
  sServerName = ""   ' only go get it when you need it
  
  Exit Function
  
errorHandler:
  Call WriteToLog("Prime: Error reading registry! " & CStr(Err.Number) & "  " & Err.Description)
End Function

Property Get ServerName() As String
  If ServerName = "" Then
    sServerName = GetServerName
  End If
  ServerName = sServerName
End Property
Property Get RuntimeDirectory() As String
  RuntimeDirectory = sRuntimeDirectory
End Property
Property Get CacheDirectory() As String
  CacheDirectory = sCacheDirectory
End Property
Property Get MaxUserIDLen() As Integer
  MaxUserIDLen = iMaxUserIDLen
End Property
Property Get DefaultDomain() As String
  DefaultDomain = sDefaultDomain
End Property
Property Get SuperUserGroups() As String
  SuperUserGroups = sSuper_User_Groups
End Property
Property Get EmailDomainRestrict() As String
  EmailDomainRestrict = sEmailDomainRestrict
End Property
Property Get DomainRestrict() As String
  DomainRestrict = sDomainRestrict
End Property
Property Get EmailFrom() As String
  EmailFrom = sEmailFrom
End Property
Property Get EmailType() As String
  EmailType = sEmailType
End Property
Property Get EmailErrorsTo() As String
  EmailErrorsTo = sEmailErrorsTo
End Property
Property Get GetConn() As String
  ' left for backward compatibility, because so many functions use this
  GetConn = sConnectString
End Property
Property Get GetConnUAL() As String
  ' left for backward compatibility, because so many functions use this
  GetConnUAL = sConnectStringUAL
End Property
Property Get GetDBEngine() As String
  ' left for backward compatibility, because so many functions use this
  GetDBEngine = sDBEngine
End Property
Property Get ConnectString() As String
  ConnectString = sConnectString
End Property
Property Let ConnectString(sConn As String)
  sConnectString = sConn
End Property
Property Let ConnectStringUAL(sConn As String)
  sConnectStringUAL = sConn
End Property
Property Get ConnectStringUAL() As String
  ConnectStringUAL = sConnectStringUAL
End Property
Property Get DBEngine() As String
  DBEngine = sDBEngine
End Property
Property Get DBWildCard() As String
  DBWildCard = sDBWildCard
End Property
Property Get DBEnvironment() As String
  DBEnvironment = sDBEnvironment
End Property
Property Get DBTimeout() As Long
  If IsNumeric(sDBTimeout) Then
    DBTimeout = CLng(sDBTimeout)
  Else
    DBTimeout = 15
  End If
End Property
Property Get Debug_Mode() As String
  Debug_Mode = sDebugSwitch
End Property
Property Get ErrorFileName() As String
  ErrorFileName = sErrorFileName
End Property
Property Get Restricted_Mode() As String
  Restricted_Mode = sRestrictedMode
End Property
Property Get Cache_Only_Mode() As String
  Cache_Only_Mode = sCacheOnlyMode
End Property
Property Get UALSource() As String
  If InStr(1, UCase(sUALSource), "COR") > 0 Then
    UALSource = "COR"
  Else
    UALSource = "UAL"
  End If
End Property
Property Get Timing() As String
  Timing = sTiming
End Property
Property Get VersionNumber() As String
  VersionNumber = App.Major & "." & App.Minor & "." & App.Revision
End Property
Property Get AsyncLogQueue() As String
  AsyncLogQueue = sAsyncLogQueue
End Property
Property Get AsyncLogMethod() As String
  AsyncLogMethod = sAsyncLogMethod
End Property
Property Get ExchangeServer() As String
  ExchangeServer = sExchangeServer
End Property
Property Get AsyncLogMenu() As String
  AsyncLogMenu = sAsyncLogMenu
End Property
Friend Function GetBaseURL() As String
  GetBaseURL = sBaseURL
End Function
Friend Function GetEmailRequestCopy() As String
  GetEmailRequestCopy = sRequestEmail
End Function
Friend Function GetEmailNewUserCopy() As String
  GetEmailNewUserCopy = sNewUserEmail
End Function


Friend Function SetDatabaseEnvironment(parmEnvironment As String, _
  os_Message As Variant) As Integer
  
  Dim Reg                As New cReg
  Dim ThisConnectString  As String
  Dim CallerID           As String
  Dim ReturnCode         As Integer
  Dim PWLocation         As Integer
  
  On Error GoTo errorHandler
  CallerID = sModName & ":SetDatabaseEnvironment: "
  ReturnCode = -1
  
  parmEnvironment = Trim(UCase(parmEnvironment))
  If parmEnvironment > "" Then
    If Reg.OpenSoftwareKey(m_Reg) Then
      ThisConnectString = Reg.QueryValueStr("DB_ConnectionString_" & parmEnvironment)
      If ThisConnectString > "" Then
        sDBEnvironment = parmEnvironment
        sConnectString = ThisConnectString
        PWLocation = InStr(1, UCase(ThisConnectString), "PWD=")
        If PWLocation > 0 Then ThisConnectString = Left(ThisConnectString, PWLocation)
        os_Message = "Database Environment set to Environment=" & parmEnvironment & "; Value=" & ThisConnectString
        ReturnCode = 0
      Else
        os_Message = "Database Environment Key not found: HKEY_LOCAL_MACHINE\" & m_Reg & "DB_ConnectionString_" & parmEnvironment
        Call WriteToLog(CallerID & os_Message)
      End If
    Else
      os_Message = "Could not open registry key HKEY_LOCAL_MACHINE\" & m_Reg
      Call WriteToLog(CallerID & os_Message)
    End If
    Reg.CloseKey
  End If
  Set Reg = Nothing
  SetDatabaseEnvironment = ReturnCode
  Exit Function
  
errorHandler:
  os_Message = "Error reading registry! " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & os_Message)
  SetDatabaseEnvironment = -1
End Function

Friend Function GetServerName() As String
  Dim CallerID         As String
  Dim Message          As String
  Dim ServerName       As String
  Dim NTSystemInfo     As WinNTSystemInfo
  
  On Error GoTo errorHandler
  CallerID = sModName & ":GetServerName " & vbTab & "()" & vbTab
  
  Set NTSystemInfo = CreateObject("WinNTSystemInfo")
  ServerName = LCase(NTSystemInfo.ComputerName)
  Set NTSystemInfo = Nothing
  
  GetServerName = ServerName
  Exit Function
        
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  If Not (NTSystemInfo Is Nothing) Then Set NTSystemInfo = Nothing
  Call WriteIfDebug(CallerID & " : " & Message)
  GetServerName = "Unknown"
End Function

Friend Function DateifyFileName(parmFileName As Variant) As Variant
  Dim Period   As Integer
  Dim NewDate  As String
  
  NewDate = Format(Year(Now()), "0000") & Format(Month(Now()), "00") & Format(Day(Now()), "00")
  Period = InStr(1, parmFileName, ".")
  If Period > 0 Then
    parmFileName = Left(parmFileName, Period - 1) & NewDate & "." & Right(parmFileName, Len(parmFileName) - Period)
  Else
    parmFileName = parmFileName & NewDate
  End If
  DateifyFileName = parmFileName
End Function

Friend Function GetRegistryAnyKeyValues(is_SoftwareKeyName As String, is_CallerID As String) As String
  Dim CallerID       As String
  Dim RegistryString As String
  Dim Reg            As New cReg
  
  On Error GoTo errorHandler
  CallerID = sModName & ":GetRegistryAnyKeyValues:" & is_CallerID
  
  Call WriteToLog(CallerID & " : Dumping registry values in " & StripLastSlash(is_SoftwareKeyName))
  RegistryString = Reg.EnumLocalMachineSubKey(StripLastSlash(is_SoftwareKeyName))
  Set Reg = Nothing
  
  GetRegistryAnyKeyValues = RegistryString
  Exit Function
        
errorHandler:
  RegistryString = RegistryString & vbCrLf & "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & RegistryString)
  GetRegistryAnyKeyValues = RegistryString
End Function
Friend Function GetRegistryKeyName() As String
  GetRegistryKeyName = m_Reg & App.EXEName
End Function
Friend Function GetRegistryKeyValues(is_CallerID As String) As String
  Dim CallerID       As String
  Dim RegistryString As String
  Dim Reg            As New cReg
  
  On Error GoTo errorHandler
  CallerID = sModName & ":GetRegistryKeyValues:" & is_CallerID
  
  Call WriteToLog(CallerID & " : Dumping registry values in " & StripLastSlash(m_Reg & App.EXEName))
  RegistryString = Reg.EnumLocalMachineSubKey(StripLastSlash(m_Reg & App.EXEName))
  Set Reg = Nothing
  
  GetRegistryKeyValues = RegistryString
  Exit Function
        
errorHandler:
  RegistryString = RegistryString & vbCrLf & "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & RegistryString)
  GetRegistryKeyValues = RegistryString
End Function
Friend Function GetRegistryRootName() As String
  GetRegistryRootName = m_Reg
End Function
Friend Function GetRegistryRootValues(is_CallerID As String) As String
  Dim CallerID       As String
  Dim RegistryString As String
  Dim Reg            As New cReg
  
  On Error GoTo errorHandler
  CallerID = sModName & ":GetRegistryRootValues:" & is_CallerID
  
  Call WriteToLog(CallerID & " : Dumping registry values in " & StripLastSlash(m_Reg))
  RegistryString = Reg.EnumLocalMachineSubKey(StripLastSlash(m_Reg))
  Set Reg = Nothing
  
  GetRegistryRootValues = RegistryString
  Exit Function
        
errorHandler:
  RegistryString = RegistryString & vbCrLf & "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & RegistryString)
  GetRegistryRootValues = RegistryString
End Function

Friend Function WriteIfDebug(is_Message As String, Optional ii_Severity As Integer = 0)
  If Debug_Mode = "ON" Then Call WriteToFile("C:\CMain.log", is_Message)
  If ii_Severity > 0 Then
    Call SendMail(EmailFrom, EmailErrorsTo, "", "", "", ServerName & ": An error has occured! Severity: " & ii_Severity, is_Message)
  End If
End Function
Friend Function WriteToLog(is_Message As String, Optional ii_Severity As Integer = 0)
  On Error Resume Next
  Call WriteToFile("C:\CMain.log", is_Message)
  If ii_Severity > 0 Then
    Call SendMail(EmailFrom, EmailErrorsTo, "", "", "", ServerName & ": An error has occured! Severity: " & ii_Severity, is_Message)
  End If
End Function
Friend Function WriteToFile(filename As String, msg As String)
  ' writes a string to a file - print statement with a ; on the end doesn't write a crlf...
  Dim Message           As String
  Dim FileNumber        As Integer
  
  'On Error GoTo errorHandler
  If filename > "" Then
    FileNumber = FreeFile
    'Open filename For Append As #FileNumber
    'Print #FileNumber, vbCrLf & Now() & vbTab & msg;
    'Close #FileNumber
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
'  Call SendMail(EmailFrom, EmailErrorsTo, "", "", "", ServerName & ": An error occured while writing to log file!", Message)
End Function
Friend Function RecordTiming(CallerID, TimerStart, TimerEnd)
  Dim os_Message    As String
  Dim TimerElapsed  As Double
  Dim FileNumber    As Integer

  On Error GoTo errorHandler
  If sTiming = "ON" Then
    TimerElapsed = (TimerEnd - TimerStart) / 1000
    FileNumber = FreeFile
    Open sTimingFile For Append As #FileNumber
    Print #FileNumber, vbCrLf & Now() & vbTab & CallerID & vbTab & TimerElapsed;
    Close #FileNumber
  End If
  
  Exit Function

errorHandler:
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & ":RecordTiming: " & os_Message, 2)
End Function
Friend Function ReadLogFile(is_UserID As Variant, ii_RecCount As Variant, osa_Data As Variant) As Integer
  ' this function reads the log file, keeping the last (ii_RecCount) records in an array.  since the
  ' last record may be in any position in the array, it then reorders the array so they
  ' are in sequential order before it returns
  Dim FileNumber     As Integer
  Dim InputData      As String
  Dim InputPtr       As Integer
  Dim InputArray()   As String
  Dim MaxEntries     As Integer
  Dim Renum          As Integer
  Dim OutputPtr      As Integer
  Dim CallerID       As String
  Dim Message        As String
  
  On Error GoTo errorHandler
  CallerID = sModName & ":ReadLogFile " & vbTab & "(" & is_UserID & ")" & vbTab
  
  Call WriteToLog(CallerID & ": Reading last " & ii_RecCount & " log entries...")
  
  MaxEntries = ii_RecCount
  ReDim osa_Data(MaxEntries)
  ReDim InputArray(MaxEntries)
  FileNumber = FreeFile
  For InputPtr = 0 To MaxEntries
    InputArray(InputPtr) = ""
  Next
  InputArray(0) = "Reading last " & MaxEntries & " log entries from " & ErrorFileName
  InputPtr = 1
  Open ErrorFileName For Input Access Read Shared As #FileNumber
  Do While Not EOF(FileNumber)
    Line Input #FileNumber, InputData
    InputArray(InputPtr) = InputData
    InputPtr = InputPtr + 1
    If InputPtr > MaxEntries Then InputPtr = 1
  Loop
  Close #FileNumber
  osa_Data(0) = InputArray(0)
  Renum = 1
  For OutputPtr = InputPtr To MaxEntries
    osa_Data(Renum) = InputArray(OutputPtr)
    Renum = Renum + 1
  Next
  If InputPtr > 1 Then
    For OutputPtr = 1 To InputPtr - 1
      osa_Data(Renum) = InputArray(OutputPtr)
      Renum = Renum + 1
    Next
  End If
  ReadLogFile = 0
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & ": " & Message)
End Function
Public Function IsAllNumbers(ByVal lcText As String) As Boolean
' check if string is all numbers, like isnumeric, but works better... commas and periods are ok
  Dim lcChar     As String
  Dim x          As Long
  Dim Message    As String
  Dim ResultFlag As Boolean
  
  On Error GoTo errorHandler
  ResultFlag = True
  For x = 1 To Len(lcText)
    lcChar = Mid$(lcText, x, 1)
    If InStr(1, "0123456789,.-", lcChar) = 0 Then
      ResultFlag = False
      Exit For
    End If
  Next x
  
  IsAllNumbers = ResultFlag
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("IsAllNumbers: " & Message)
  IsAllNumbers = False
End Function
Public Function UrlEncode(ByVal lcText As String) As String
' Converts a string to URLEncoded format
  Dim lcResult As String
  Dim lcFinal  As String
  Dim lcChar   As String
  Dim x        As Long
  Dim Message  As String
  
  On Error GoTo errorHandler
  lcResult = "" ' Work Buffer (1000 bytes at a time)
  lcFinal = ""  ' Final Buffer
  
  For x = 1 To Len(lcText)
     lcChar = Mid$(lcText, x, 1)
      If InStr(1, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", lcChar) <> 0 Then
        lcResult = lcResult + lcChar
      ElseIf lcChar = " " Then
        lcResult = lcResult + "+"
      ElseIf True Then
        lcResult = lcResult + "%" + Right$("0" & Hex(Asc(lcChar)), 2)
      End If
      If Len(lcResult) > 1000 Then
       lcFinal = lcFinal + lcResult
       lcResult = ""
      End If
  Next x
  
  UrlEncode = lcFinal + lcResult
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("UrlEncode: " & Message)
  UrlEncode = ""
End Function
Friend Function NoLongerThan(str As Variant, pLen As Integer) As String
  ' converts null to empty or trims spaces, and chops it off to that length
  Dim ThisStr     As String
  Dim Message     As String
  
  On Error GoTo errorHandler
  If IsEmpty(str) Then
    NoLongerThan = ""
  Else
    If IsNull(str) Then
      NoLongerThan = ""
    Else
      ThisStr = Trim(CStr(str))
      If Len(ThisStr) > pLen Then
        ThisStr = Left(ThisStr, pLen)
      End If
      NoLongerThan = ThisStr
    End If
  End If
Exit Function
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("NoLongerThan: " & Message)
  NoLongerThan = ""
End Function
Friend Function Scrub(str As Variant) As String
  ' converts null to empty or trims spaces
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsEmpty(str) Then
    Scrub = ""
  Else
    If IsNull(str) Then
      Scrub = ""
    Else
      Scrub = Trim(CStr(str))
    End If
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("Scrub: " & Message)
  Scrub = ""
End Function
Function ScrubNumber(varTemp)
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsNull(varTemp) Then
    ScrubNumber = 0
  Else
    If varTemp > "" Then
      If IsNumeric(varTemp) Then
        ScrubNumber = varTemp
      Else
        ScrubNumber = 0
      End If
    Else
      ScrubNumber = 0
    End If
  End If
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ScrubNumber: " & Message)
  ScrubNumber = 0
End Function
Function ScrubNumberNull(varTemp)
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsNull(varTemp) Then
    ScrubNumberNull = "NULL"
  Else
    If varTemp > "" Then
      If IsNumeric(varTemp) Then
        ScrubNumberNull = varTemp
      Else
        ScrubNumberNull = "0"
      End If
    Else
      ScrubNumberNull = "NULL"
    End If
  End If
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ScrubNumberNull: " & Message)
  ScrubNumberNull = "NULL"
End Function
Friend Function ScrubPhone(MyWorkPhn) As String
  Dim MySearchPhoneLen  As Integer
  Dim MySearchPhone     As String
  Dim i                 As Integer
  Dim MySearchPhoneChar As String
  Dim Message           As String

  On Error GoTo errorHandler

  MySearchPhoneLen = Len(MyWorkPhn)
  MySearchPhone = ""
  For i = 0 To MySearchPhoneLen
    MySearchPhoneChar = Mid(MyWorkPhn, i + 1, 1)
    Select Case MySearchPhoneChar
      Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        MySearchPhone = MySearchPhone & MySearchPhoneChar
    End Select
    If i > 50 Then Exit For
  Next
  Select Case Len(MySearchPhone)
  Case 7
    MySearchPhone = Left(MySearchPhone, 3) & "-" & Right(MySearchPhone, 4)
  Case 10
    MySearchPhone = Left(MySearchPhone, 3) & "-" & Mid(MySearchPhone, 4, 3) & "-" & Right(MySearchPhone, 4)
  Case Else
    ' leave it alone...
  End Select
  ScrubPhone = MySearchPhone
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ScrubPhone: " & Message)
  ScrubPhone = ""
End Function
Function ScrubIndicator(varTemp, Optional DefaultValue As String = "N")
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsNull(varTemp) Then
    ScrubIndicator = DefaultValue
  Else
    If varTemp > "" Then
      varTemp = Trim(UCase(varTemp))
      If Not (varTemp = "Y" Or varTemp = "N") Then
        ScrubIndicator = DefaultValue
      Else
       ScrubIndicator = varTemp
      End If
    Else
      ScrubIndicator = DefaultValue
    End If
  End If
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ScrubIndicator: " & Message)
  ScrubIndicator = "N"
End Function
Function CheckBox(varTemp)
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsNull(varTemp) Then
    CheckBox = "N"
  Else
    If LCase(Trim(varTemp)) = "on" Then
      CheckBox = "Y"
    Else
      CheckBox = "N"
    End If
  End If
Exit Function
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("CheckBox: " & Message)
  CheckBox = "N"
End Function
Function Indicator(varTemp)
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsNull(varTemp) Then
    Indicator = "N"
  Else
    If LCase(Trim(varTemp)) = "on" Then
      Indicator = "Y"
    Else
      Indicator = "N"
    End If
  End If
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("Indicator: " & Message)
  Indicator = "N"
End Function
Function ScrubDate(varTemp)
  Dim cDateDelim        As String
  Dim cDateDelim2       As String
  Dim Message           As String
  
  On Error GoTo errorHandler
  
  If DBEngine = "ORACLE" Then
    ScrubDate = OracleDate(varTemp)
  Else
    If DBEngine = "ACCESS" Then
      cDateDelim = "#"
      cDateDelim2 = "#"
    Else
      cDateDelim = "'"
      cDateDelim2 = "'"
    End If
    If varTemp > "" Then
      If IsDate(varTemp) Then
        ScrubDate = cDateDelim & varTemp & cDateDelim2
      Else
        ScrubDate = cDateDelim & "1/1/1980" & cDateDelim2
      End If
    Else
      ScrubDate = "NULL"
    End If
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ScrubDate: " & Message)
  ScrubDate = "NULL"
End Function
Function OracleDate(varTemp)
  Dim myDate    As Date
  Dim pMonth    As Integer
  Dim pDay      As Integer
  Dim pYear     As Integer
  Dim pHour     As Integer
  Dim pMinute   As Integer
  Dim pSeconds  As Integer
  Dim resdate   As String
  Dim Message   As String

  On Error GoTo errorHandler
  
  If varTemp > "" Then
    If IsDate(varTemp) Then

      myDate = CDate(varTemp)
      pMonth = Month(myDate)
      pDay = Day(myDate)
      pYear = Year(myDate)
      pHour = Hour(myDate)
      pMinute = Minute(myDate)
      pSeconds = Second(myDate)

      If pMonth < 10 Then resdate = "0"
      resdate = resdate & pMonth & "/"
      If pDay < 10 Then resdate = resdate & "0"
      resdate = resdate & pDay & "/" & Right(pYear, 4) & " "
      If pHour < 10 Then resdate = resdate & "0"
      resdate = resdate & pHour & ":"
      If pMinute < 10 Then resdate = resdate & "0"
      resdate = resdate & pMinute & ":"
      If pSeconds < 10 Then resdate = resdate & "0"
      resdate = resdate & pSeconds

      OracleDate = "to_date('" & resdate & "', 'MM/DD/YYYY HH24:MI:SS')"
    Else
      OracleDate = "to_date('01/01/2000 00:00:00 AM', 'MM/DD/YYYY HH24:MI:SS')"
    End If
  Else
    OracleDate = "NULL"
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("OracleDate: " & Message)
  OracleDate = "NULL"
End Function


Friend Function DateTime2TextDate(str As Variant) As String
  Dim Message           As String

  On Error GoTo errorHandler
  Select Case DBEngine
  Case "ORACLE"
    DateTime2TextDate = "to_char(" & str & ", 'MM/DD/YYYY HH24:MI')"
  Case "SQLSERVER", "MSSQL", "SQL7", "SQL"
    DateTime2TextDate = "convert(varchar(10), " & str & ", 101) + ' ' + convert(varchar(5), " & str & ", 8)"
  Case "ACCESS"
    DateTime2TextDate = "Format(" & str & ", 'mm/dd/yyyy hh:mm')"
  Case Else
    DateTime2TextDate = "NULL"
  End Select
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("DateTime2TextDate: " & Message)
  DateTime2TextDate = "NULL"
End Function
Friend Function DateTime2TextDayOnly(str As Variant) As String
  Dim Message           As String

  On Error GoTo errorHandler
  Select Case DBEngine
  Case "ORACLE"
    DateTime2TextDayOnly = "to_char(" & str & ", 'MM/DD/YYYY')"
  Case "SQLSERVER", "MSSQL", "SQL7", "SQL"
    DateTime2TextDayOnly = "convert(varchar(10), " & str & ", 101)"
  Case "ACCESS"
    DateTime2TextDayOnly = "Format(" & str & ", 'mm/dd/yyyy')"
  Case Else
    DateTime2TextDayOnly = "NULL"
  End Select
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("DateTime2TextDayOnly: " & Message)
  DateTime2TextDayOnly = "NULL"
End Function
Friend Function DateTime2TextTime(str As Variant) As String
  Dim Message           As String

  On Error GoTo errorHandler
  Select Case DBEngine
  Case "ORACLE"
    DateTime2TextTime = "to_char(" & str & ", 'HH24:MI')"
  Case "SQLSERVER", "MSSQL", "SQL7", "SQL"
    DateTime2TextTime = "convert(varchar(5), " & str & ", 8)"
  Case "ACCESS"
    DateTime2TextTime = "Format(" & str & ", 'hh:mm')"
  Case Else
    DateTime2TextTime = "NULL"
  End Select
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("DateTime2TextTime: " & Message)
  DateTime2TextTime = "NULL"
End Function

Friend Function StripLastSlash(str As Variant) As String
  Dim TmpString         As String
  Dim Message           As String
  
  On Error GoTo errorHandler
  
  If IsNull(str) Then
    StripLastSlash = ""
  Else
    TmpString = Trim(CStr(str))
    If Right(TmpString, 1) = "/" Or Right(TmpString, 1) = "\" Then
      StripLastSlash = Left(TmpString, Len(TmpString) - 1)
    Else
      StripLastSlash = TmpString
    End If
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("StripLastSlash: " & Message)
  StripLastSlash = ""
End Function
Friend Function AddLastForeSlash(str As Variant) As String
  Dim TmpString         As String
  Dim Message           As String

  On Error GoTo errorHandler
  If IsNull(str) Then
    AddLastForeSlash = "/"
  Else
    TmpString = Trim(CStr(str))
    If Right(TmpString, 1) = "/" Then
      AddLastForeSlash = TmpString
    Else
      AddLastForeSlash = TmpString & "/"
    End If
  End If
  Exit Function
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("AddLastForeSlash: " & Message)
  AddLastForeSlash = "/"
End Function
Friend Function AddLastBackSlash(str As Variant) As String
  Dim TmpString         As String
  Dim Message           As String

  On Error GoTo errorHandler
  If IsNull(str) Then
    AddLastBackSlash = "\"
  Else
    TmpString = Trim(CStr(str))
    If Right(TmpString, 1) = "\" Then
      AddLastBackSlash = TmpString
    Else
      AddLastBackSlash = TmpString & "\"
    End If
  End If
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("AddLastBackSlash: " & Message)
  AddLastBackSlash = "\"
End Function

Friend Function SQLScrub(str As Variant) As String
  ' converts null to empty, trims spaces, and converts ' to ''
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsNull(str) Then
    SQLScrub = ""
  Else
    SQLScrub = Trim(Replace(CStr(str), "'", "''"))
  End If
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("SQLScrub: " & Message)
  SQLScrub = ""
End Function
Friend Function SQLScrubAttribute(str As Variant) As String
  ' converts null to empty or trims spaces, removes all unwanted characters and lowercases it
  Dim TmpString         As String
  Dim Message           As String
  
  On Error GoTo errorHandler
  If IsNull(str) Then
    SQLScrubAttribute = ""
  Else
    TmpString = Trim(CStr(str))
    TmpString = Replace(TmpString, Chr(39), "") ' single quote mark
    TmpString = Replace(TmpString, Chr(34), "") ' double quote mark
    TmpString = Replace(TmpString, " ", "_")
    TmpString = Replace(TmpString, ";", "_")
    TmpString = Replace(TmpString, ".", "_")
    TmpString = LCase(TmpString)
    SQLScrubAttribute = TmpString
  End If
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("SQLScrubAttribute: " & Message)
  SQLScrubAttribute = ""
End Function
Friend Function FillArray(osa_Array As Variant, ii_Position As Integer, isa_DataArray As Variant, ii_RecordCount As Integer)
  Dim i                 As Integer
  Dim Message           As String

  On Error GoTo errorHandler
  If ii_RecordCount > 0 Then
    ReDim osa_Array(ii_RecordCount)
    For i = 0 To ii_RecordCount - 1
      osa_Array(i) = isa_DataArray(ii_Position, i)
    Next
  Else
    ReDim osa_Array(0)
    osa_Array(0) = ""
  End If
  FillArray = 0
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("FillArray : " & Message)
End Function

Friend Function ConvertRStoArray(RS1 As adodb.Recordset, oi_RecordCount As Variant, _
  osa_DataArray As Variant) As Long
  Dim ColCount          As Long
  Dim j                 As Long
  Dim RecordsFound      As Long
  Dim Message           As String
  
  On Error GoTo errorHandler
  
  oi_RecordCount = 0
  RecordsFound = 0
  
  If RS1.EOF Then
    ReDim osa_DataArray(0, 0)
    osa_DataArray(0, 0) = "No data found!"
  Else
    RS1.MoveLast
    oi_RecordCount = RS1.RecordCount
    RS1.MoveFirst
    ColCount = RS1.Fields.Count
    ReDim osa_DataArray(ColCount - 1, oi_RecordCount)
    For j = 0 To ColCount - 1
      osa_DataArray(j, 0) = RS1.Fields(j).Name
    Next
    Do While Not RS1.EOF
      RecordsFound = RecordsFound + 1
      For j = 0 To ColCount - 1
        osa_DataArray(j, RecordsFound) = Scrub(RS1(j))
      Next
      RS1.MoveNext
    Loop
  End If
  ConvertRStoArray = RecordsFound

Exit Function
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ConvertRStoArray: " & Message)
  ConvertRStoArray = -1
End Function

Public Function FixUserID(is_UserID As Variant)
  Dim TempVar           As String
  Dim Tick              As Integer
  Dim Tries             As Integer
  Dim Message           As String

  On Error GoTo errorHandler
  TempVar = UCase(is_UserID)
  If IsNull(TempVar) Then
    TempVar = ""
  Else
    TempVar = Replace(Trim(TempVar), "/", "\")
  End If
  FixUserID = TempVar
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("FixUserID : " & Message)
  FixUserID = ""
End Function
Friend Function Raw_UserID(is_UserID As Variant) As String
  Dim MyUser            As String
  Dim SlashPos          As Integer
  Dim Message           As String

  On Error GoTo errorHandler
  
  MyUser = UCase(Trim(is_UserID))
  MyUser = Replace(MyUser, "/", "\")
  SlashPos = InStr(1, MyUser, "\")
  If SlashPos > 0 Then
    MyUser = Right(MyUser, Len(MyUser) - SlashPos)
  End If
  If Len(MyUser) > iMaxUserIDLen Then
    MyUser = Left(MyUser, iMaxUserIDLen)
  Else
    If Len(MyUser) < 1 Then
      MyUser = "UNKNOWN"
    End If
  End If
  Raw_UserID = MyUser
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("Raw_UserID: " & Message)
  Raw_UserID = ""
End Function
Friend Function Split_UserID(is_UserID As Variant, os_Domain As Variant, os_User As Variant, is_Default_Domain As String) As Integer
  Dim MyUser            As String
  Dim SlashPos          As Integer
  Dim ReturnCode        As Integer
  Dim Message           As String

  On Error GoTo errorHandler
  
  ReturnCode = -1
  os_Domain = ""
  os_User = ""
  MyUser = LCase(Trim(is_UserID))
  If MyUser > "" Then
    MyUser = Replace(MyUser, "/", "\")
    SlashPos = InStr(1, MyUser, "\")
    If SlashPos > 0 Then
      os_Domain = Left(MyUser, SlashPos - 1)
      os_User = Right(MyUser, Len(MyUser) - SlashPos)
      If Len(MyUser) < 1 Then
        os_User = "UNKNOWN"
      Else
        ReturnCode = 0
      End If
    End If
  End If
  If os_Domain = "" Then os_Domain = is_Default_Domain
  Split_UserID = ReturnCode
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("Split_UserID: " & Message)
  Split_UserID = -1
End Function
Function CaseInsensitiveReplace(is_StringToSearch As String, is_Find As String, is_Replace As String) As String
Dim FindPos       As Integer
Dim ResultString  As String
Dim os_Message    As String

On Error GoTo errorHandler
ResultString = is_StringToSearch
If is_StringToSearch > "" And is_Find > "" Then
  FindPos = InStr(1, UCase(is_StringToSearch), UCase(is_Find))
  If FindPos > 0 Then
    ResultString = _
      Left(is_StringToSearch, FindPos - 1) & _
      is_Replace & _
      Right(is_StringToSearch, Len(is_StringToSearch) - Len(is_Find) - FindPos + 1)
  End If
End If
CaseInsensitiveReplace = ResultString
Exit Function

errorHandler:
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  CaseInsensitiveReplace = is_StringToSearch
  Call WriteToLog("CaseInsensitiveReplace : " & os_Message, 2)
End Function

Friend Function Audit_Date(is_UserID As Variant, is_Type As String, is_Field_Name As String) As String
  Dim CallerID        As String
  Dim MyUser          As String
  Dim CurrentDateTime As String
  Dim MySQL           As String
  Dim MyType          As String
  Dim CreateDate      As String
  Dim ChangeDate      As String
  Dim Message         As String
  
  On Error GoTo errorHandler
  CallerID = "Audit_Date " & vbTab & "(" & is_UserID & ")" & vbTab
  
  CreateDate = ""
  Select Case DBEngine
  Case "SQLSERVER", "MSSQL", "SQL7", "SQL", "SQLOLEDB"
    CurrentDateTime = "'" & Format(Now(), "mm/dd/yyyy hh:mm:ss") & "',"
    CreateDate = "convert(varchar(10), Create_Date, 101) + ' ' + convert(varchar(8), Create_Date, 8)"
  Case "ACCESS"
    CurrentDateTime = "#" & Format(Now(), "mm/dd/yyyy hh:mm:ss") & "#, "
    CreateDate = "Format(Create_Date, 'mm/dd/yyyy hh:mm')"
  Case Else
    CurrentDateTime = "to_date('" & Format(Now(), "mm/dd/yyyy hh:mm:ss") & "', 'MM/DD/YYYY HH24:MI:SS'), "
    CreateDate = "to_char(Create_Date, 'MM/DD/YYYY HH24:MI')"
  End Select
  MyType = UCase(is_Type)
  Select Case MyType
    Case "UPDATESQL"
      MySQL = " " & is_Field_Name & " = " & CurrentDateTime
    Case "FIELDS"
      MySQL = " " & is_Field_Name
    Case "DISPLAYFIELDS"
      MySQL = " " & is_Field_Name & ", " & CreateDate & " as " & is_Field_Name & "2"
    Case "INSERTSQL"
      MySQL = CurrentDateTime
  End Select
  Audit_Date = MySQL
  Exit Function
  
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
  Audit_Date = ""
End Function
Friend Function Audit_Fields(is_UserID As Variant, is_Type As String) As String
  Dim CallerID        As String
  Dim MyUser          As String
  Dim CurrentDateTime As String
  Dim MySQL           As String
  Dim MyType          As String
  Dim CreateDate      As String
  Dim ChangeDate      As String
  Dim Message         As String
  
  On Error GoTo errorHandler
  CallerID = "Audit_Fields " & vbTab & "(" & is_UserID & ")" & vbTab
  
  MyUser = "'" & Raw_UserID(is_UserID) & "', "
    
  CreateDate = ""
  ChangeDate = ""
  Select Case DBEngine
  Case "SQLSERVER", "MSSQL", "SQL7", "SQL"
    CurrentDateTime = "'" & Format(Now(), "mm/dd/yyyy hh:mm:ss") & "',"
    CreateDate = "convert(varchar(10), Create_Date, 101) + ' ' + convert(varchar(8), Create_Date, 8)"
    ChangeDate = "convert(varchar(10), Chg_Lst_Date, 101) + ' ' + convert(varchar(8), Chg_Lst_Date, 8)"
  Case "ACCESS"
    CurrentDateTime = "#" & Format(Now(), "mm/dd/yyyy hh:mm:ss") & "#, "
    CreateDate = "Format(Create_Date, 'mm/dd/yyyy hh:mm:ss')"
    ChangeDate = "Format(Chg_Lst_Date, 'mm/dd/yyyy hh:mm:ss')"
  Case Else
    CurrentDateTime = "to_date('" & Format(Now(), "mm/dd/yyyy hh:mm:ss") & "', 'MM/DD/YYYY HH24:MI:SS'), "
    CreateDate = "to_char(Create_Date, 'MM/DD/YYYY HH24:MI:SS')"
    ChangeDate = "to_char(Chg_Lst_Date, 'MM/DD/YYYY HH24:MI:SS')"
  End Select
  MyType = UCase(is_Type)
  Select Case MyType
    Case "UPDATESQL"
      MySQL = _
        " Chg_Lst_Date = " & CurrentDateTime & _
        " Chg_Lst_User_ID = " & MyUser & _
        " Chg_Lst_Prog_Name = 'WebDCOM' "
    Case "FIELDS"
      MySQL = " Create_Date, Create_User_ID, Create_Prog_Name, Chg_Lst_Date, Chg_Lst_User_ID, Chg_Lst_Prog_Name "
    Case "DISPLAYFIELDS"
      MySQL = " Create_Date, " & CreateDate & " as Create_Date2, Create_User_ID, Create_Prog_Name, Chg_Lst_Date, " & ChangeDate & " as Chg_Lst_Date2, Chg_Lst_User_ID, Chg_Lst_Prog_Name "
    Case "INSERTSQL"
      MySQL = CurrentDateTime & MyUser & "'WebDCOM', " & CurrentDateTime & MyUser & "'WebDCOM' "
  End Select
  Audit_Fields = MySQL
Exit Function
errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
End Function
Function ExecSQL(ByVal strSQL As String, Optional ByVal strConnectString As String)
  Dim cmd        As adodb.Command
  
  On Error GoTo errorHandler
  ' Create the ADO objects
#If USING_MTX Then
  Set cmd = CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New adodb.Command
#End If
  ' Init the ADO objects & the stored proc parameters
  With cmd
    If strConnectString > "" Then
      .ActiveConnection = strConnectString
    Else
      .ActiveConnection = ConnectString
    End If
    .CommandText = strSQL
    .CommandType = adCmdText
    .CommandTimeout = DBTimeout
    .ActiveConnection.CommandTimeout = DBTimeout
  End With
  ' Execute the query without returning a recordset
  cmd.Execute , , adodb.adExecuteNoRecords
  ' Cleanup and return nothing
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  Exit Function
errorHandler:
  Set cmd = Nothing
  RaiseError sModName, "ExecSQL(" & strSQL & ")"
End Function
Function RunSQLReturnRS(ByVal strSQL As String, Optional ByVal strConnectString As String, Optional ByVal bLock As Boolean = True) As adodb.Recordset
  Dim rs        As adodb.Recordset
  Dim cmd       As adodb.Command
  On Error GoTo errorHandler
  ' Set up Command and Connection objects
  
#If USING_MTX Then
  Set rs = CtxCreateObject("ADODB.Recordset")
  Set cmd = CtxCreateObject("ADODB.Command")
#Else
  Set rs = New adodb.Recordset
  Set cmd = New adodb.Command
#End If
  'Run the procedure
  With cmd
    If strConnectString > "" Then
      .ActiveConnection = strConnectString
    Else
      .ActiveConnection = ConnectString
    End If
    .CommandText = strSQL
    .CommandType = adCmdText
    .CommandTimeout = DBTimeout
    .ActiveConnection.CommandTimeout = DBTimeout
  End With
  With rs
    .CursorLocation = adUseClient
    If bLock Then
        .Open cmd, , adOpenForwardOnly, adLockReadOnly
    Else
        .Open cmd, , adOpenForwardOnly, adLockOptimistic
    End If
  End With
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  Set rs.ActiveConnection = Nothing
  Set RunSQLReturnRS = rs
  Exit Function
errorHandler:
  Set rs = Nothing
  Set cmd = Nothing
  RaiseError sModName, "RunSQLReturnRS(" & strSQL & ")"
End Function
Function RunSQLReturnShapeRS(ByVal strSQL As String, Optional ByVal strConnectString As String) As adodb.Recordset
  Dim rs        As adodb.Recordset
  Dim cmd       As adodb.Command
  Dim cn        As adodb.Connection
  
  On Error GoTo errorHandler
  ' Set up Command and Connection objects
  
#If USING_MTX Then
  Set rs = CtxCreateObject("ADODB.Recordset")
  Set cmd = CtxCreateObject("ADODB.Command")
  Set cn = CtxCreateObject("ADODB.Connection")
#Else
  Set rs = New adodb.Recordset
  Set cmd = New adodb.Command
  Set cn = New adodb.Connection
#End If
  With cn
    If strConnectString > "" Then
      .ConnectionString = strConnectString
    Else
      .ConnectionString = ConnectString
    End If
    .Provider = "MSDataShape"
    .CommandTimeout = DBTimeout
    .Open
  End With
  'Run the procedure
  With cmd
    Set .ActiveConnection = cn
    .CommandText = strSQL
    .CommandType = adCmdText
    .CommandTimeout = DBTimeout
  End With
  With rs
    .CursorLocation = adUseClient
    .Open cmd, , adOpenForwardOnly, adLockReadOnly
  End With
  Set cmd.ActiveConnection = Nothing
  Set cn = Nothing
  Set cmd = Nothing
  Set rs.ActiveConnection = Nothing
  Set RunSQLReturnShapeRS = rs
  Exit Function
errorHandler:
  Set rs = Nothing
  Set cmd = Nothing
  RaiseError sModName, "RunSQLReturnRS(" & strSQL & ")"
End Function
Public Function CtxCreateObject(ByVal sProgID As String) As Object
  Dim ctx  As ObjectContext
  
  Set ctx = GetObjectContext
  If Not (ctx Is Nothing) Then
    Set CtxCreateObject = ctx.CreateInstance(sProgID)
    Set ctx = Nothing
  Else
    Set CtxCreateObject = CreateObject(sProgID)
  End If
  Set ctx = Nothing
End Function
Public Function CtxIsCallerInRoll(parmRole As String) As Boolean
  Dim ctx As ObjectContext
  
  Set ctx = GetObjectContext
  If Not (ctx Is Nothing) Then
    If ctx.IsSecurityEnabled Then
      CtxIsCallerInRoll = ctx.IsCallerInRole(parmRole)
    Else
      CtxIsCallerInRoll = True
    End If
  Else
    CtxIsCallerInRoll = False
  End If
  Set ctx = Nothing
End Function
Public Sub CtxSetAbort()
  Dim ctx As ObjectContext
  
  Set ctx = GetObjectContext
  If Not (ctx Is Nothing) Then
    ctx.SetAbort
  End If
  Set ctx = Nothing
End Sub
Public Sub CtxSetComplete()
  Dim ctx As ObjectContext
  
  Set ctx = GetObjectContext
  If Not (ctx Is Nothing) Then
    ctx.SetComplete
  End If
  Set ctx = Nothing
End Sub
Public Sub CtxRaiseError(module As String, functionName As String)
  ' Save the error information before calling CtxSetAbort in case it has side effects
  Dim lErr   As Long
  Dim sErr   As String
  
  lErr = VBA.Err.Number
  sErr = VBA.Err.Description
  CtxSetAbort
  Err.Raise lErr, SetErrSource(module, functionName), sErr
End Sub
Public Sub RaiseError(module As String, functionName As String)
  Err.Raise Err.Number, SetErrSource(module, functionName), Err.Description
End Sub
Function SetErrSource(modName As String, procName As String) As String
  ' Returns an error message like:  "[FMStocks_DB.Account] VerifyUser [version 5.21.176]"
  SetErrSource = Err.Source & "<br>" & "[" & modName & "]  " & procName & _
    " version " & VersionNumber() & "]"
End Function
Friend Function Add_Where(WhereClause, FieldName, WhereValue, WhereDepth, WhereType)
  Dim Message           As String

  On Error GoTo errorHandler
  If WhereValue > "" Then
    Select Case UCase(WhereType)
      Case "STRING"
        WhereClause = WhereClause & " AND " & FieldName & " "
        Select Case UCase(WhereDepth)
          Case "STARTS"
            Add_Where = WhereClause & "Like '" & WhereValue & "%'"
          Case "CONTAINS"
            Add_Where = WhereClause & "Like '%" & WhereValue & "%'"
          Case Else
            Add_Where = WhereClause & "= '" & WhereValue & "'"
        End Select
      Case "NUMBER"
        If IsNumeric(WhereValue) Then
          WhereClause = WhereClause & " AND " & FieldName & " "
          Select Case UCase(WhereDepth)
            Case "GT"
              Add_Where = WhereClause & "> " & WhereValue
            Case "GTE"
              Add_Where = WhereClause & ">= " & WhereValue
            Case "LT"
              Add_Where = WhereClause & "< " & WhereValue
            Case "LTE"
              Add_Where = WhereClause & "<= " & WhereValue
            Case Else
              Add_Where = WhereClause & "= " & WhereValue
          End Select
        End If
      Case Else
        Add_Where = WhereClause
    End Select
  Else
    Add_Where = WhereClause
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("Add_Where: " & Message)
End Function

Friend Function Add_WhereEx(WhereClause, FieldName, WhereValue, WhereDepth, WhereType, ANDOR, Splat)
  Dim Connector As String
  If WhereValue > "" Then
    If ANDOR = "AND" Then ANDOR = " AND "
    If ANDOR = "OR" Then ANDOR = " OR "
    If (ANDOR = " AND " Or ANDOR = " OR ") Then
      Connector = ANDOR
    Else
      Connector = " AND "
    End If
    Select Case UCase(WhereType)
      Case "STRING"
        If WhereClause > "" Then
          WhereClause = WhereClause & Connector & FieldName & " "
        Else
          WhereClause = " WHERE " & FieldName & " "
        End If
        Select Case UCase(WhereDepth)
          Case "STARTS"
            Add_WhereEx = WhereClause & "Like '" & WhereValue & Splat & "'"
          Case "CONTAINS"
            Add_WhereEx = WhereClause & "Like '" & Splat & WhereValue & Splat & "'"
          Case Else
            Add_WhereEx = WhereClause & "= '" & WhereValue & "'"
        End Select
      Case "NUMBER"
        If IsNumeric(WhereValue) Then
          If WhereClause > "" Then
            WhereClause = WhereClause & Connector & FieldName & " "
          Else
          WhereClause = " WHERE " & FieldName & " "
          End If
          Select Case UCase(WhereDepth)
            Case "GT"
              Add_WhereEx = WhereClause & "> " & WhereValue
            Case "GTE"
              Add_WhereEx = WhereClause & ">= " & WhereValue
            Case "LT"
              Add_WhereEx = WhereClause & "< " & WhereValue
            Case "LTE"
              Add_WhereEx = WhereClause & "<= " & WhereValue
            Case Else
              Add_WhereEx = WhereClause & "= " & WhereValue
          End Select
        End If
      Case Else
        Add_WhereEx = WhereClause
    End Select
  Else
    Add_WhereEx = WhereClause
  End If
End Function


Friend Function Log_Entry_SQL(is_UserID As String, _
  is_Table_Type As String, is_Table_Key As Long, _
  is_Log_Type As String, is_Log_Txt As String, ii_Error_Nbr As Integer) As String
  
  Dim CallerID     As String
  Dim TableName    As String
  Dim TableKey1    As String
  Dim TableKey2    As String
  Dim TableSeq     As String
  Dim os_SQL       As String
  Dim os_Message   As String
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Log_Entry_SQL " & vbTab & "(" & is_UserID & ")" & vbTab

  is_Table_Type = UCase(Trim(is_Table_Type))
  TableName = ""
  Select Case is_Table_Type
    Case "WEB"
      TableName = "Web_Log"
      TableKey1 = "Web_Log_Seq_ID"
      TableKey2 = "Web_Seq_ID"
      TableSeq = "Web_Log_S01.NextVal"
    Case "CHANNEL"
      TableName = "Channel_Log"
      TableKey1 = "Channel_Log_Seq_ID"
      TableKey2 = "Channel_Seq_ID"
      TableSeq = "Channel_Log_S01.NextVal"
    Case "SYSTEM"
      TableName = "System_Log"
      TableKey1 = "System_Log_Seq_ID"
      TableKey2 = "System_Seq_ID"
      TableSeq = "System_Log_S01.NextVal"
  End Select
  If TableName <> "" Then
    Select Case GetDBEngine
      Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
        os_SQL = "Insert into " & TableName & "(" & TableKey2 & ", Log_Type_CODE, Log_Txt, Error_Nbr, PROCESS_IND, " & _
          " CREATE_DATE, CREATE_USER_ID, CREATE_PROG_NAME, CHG_LST_DATE, CHG_LST_USER_ID, CHG_LST_PROG_NAME)" & _
          " Values (" & is_Table_Key & ", '" & UCase(Trim(is_Log_Type)) & "', '" & SQLScrub(is_Log_Txt) & "', " & ii_Error_Nbr & ", 'N', " & _
          Audit_Fields(is_UserID, "INSERTSQL") & ")"
      Case Else
        os_SQL = "Insert into " & TableName & "(" & TableKey1 & "," & TableKey2 & ", Log_Type_CODE, Log_Txt, Error_Nbr, PROCESS_IND, " & _
          " CREATE_DATE, CREATE_USER_ID, CREATE_PROG_NAME, CHG_LST_DATE, CHG_LST_USER_ID, CHG_LST_PROG_NAME)" & _
          " Values (" & TableSeq & "," & is_Table_Key & ", '" & UCase(Trim(is_Log_Type)) & "', '" & SQLScrub(is_Log_Txt) & "', " & ii_Error_Nbr & ", 'N', " & _
        Audit_Fields(is_UserID, "INSERTSQL") & ")"
    End Select
  Else
    os_SQL = ""
  End If
  Log_Entry_SQL = os_SQL
  Exit Function
  
errorHandler:
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & os_Message, 2)
  Log_Entry_SQL = ""
End Function

Friend Function SendMail(ByVal FromAddress As String, ByVal Recipient As String, _
  ByVal CopyTo As String, ByVal BlindCopy As String, ByVal BodyFormat As String, _
  ByVal Subject As String, ByVal Message As String) As Integer
  
  Dim CallerID       As String
  Dim Recipient2     As String
  Dim LogMessage     As String
  Dim Mailer         As Object
  Dim FirstDelimiter As Integer
  
  On Error GoTo errorHandler
  CallerID = sModName & ":SendMail " & vbTab & "()" & vbTab

  Call WriteIfDebug(CallerID & " : Sending mail to " & Recipient & "; Subject: " & Subject & "; From:" & FromAddress)
  Recipient = LCase(Trim(Recipient))
  Recipient2 = ScrubEmailDomain(Recipient)
  If Recipient <> Recipient2 Then
    Call WriteIfDebug(CallerID & " : Modified Recipient from " & Recipient & " to " & Recipient2)
  End If
  If FromAddress = "" Then FromAddress = EmailFrom
  
  ' check to make sure there is only one email address in the from field
  ' cut off at first address if there is more than one
  FromAddress = Replace(FromAddress, ",", ";")
  FirstDelimiter = InStr(1, FromAddress, ";")
  If FirstDelimiter > 0 Then
    FromAddress = Left(FromAddress, FirstDelimiter - 1)
  End If
  
  If (Recipient2 > "" Or BlindCopy > "") And FromAddress > "" Then
    Select Case EmailType
      Case "SMTPMAILWRAPPER"
        Set Mailer = CreateObject("SMTPMailWrapper.cMail")
        Call Mailer.QuickMail(FromAddress, Recipient2, Subject, Message)
        Set Mailer = Nothing
      Case "CDONTS"
        Set Mailer = CreateObject("CDONTS.NewMail")
        Mailer.From = FromAddress
        Mailer.To = Recipient2
        If BlindCopy > "" Then Mailer.Bcc = BlindCopy
        If CopyTo > "" Then Mailer.Cc = CopyTo
        If BodyFormat > "" Then
          BodyFormat = LCase(Trim(BodyFormat))
          Select Case BodyFormat
            Case "html", "1"
              Mailer.BodyFormat = 0
              Mailer.MailFormat = 0
            Case Else
              Mailer.BodyFormat = 1
              Mailer.MailFormat = 1
          End Select
        Else
          Mailer.BodyFormat = 1
          Mailer.MailFormat = 1
        End If
        Mailer.Subject = Subject
        Mailer.Body = Message
        Mailer.Send
        Set Mailer = Nothing
      Case Else
        LogMessage = "From: " & FromAddress & vbCrLf & "To: " & Recipient2 & vbCrLf
        If BlindCopy > "" Then LogMessage = LogMessage & "BCC: " & BlindCopy & vbCrLf
        If CopyTo > "" Then LogMessage = LogMessage & "CC: " & CopyTo & vbCrLf
        LogMessage = LogMessage & "Subject: " & Subject & vbCrLf & Message
        Call WriteToLog(CallerID & " : Email Not Sent:" & vbCrLf & LogMessage, 0)
    End Select
  Else
    If Recipient2 > "" Then
      Call WriteIfDebug(CallerID & " : Could not send email because of email domain restrictions!")
      LogMessage = "To: " & Recipient2 & vbCrLf
    Else
      LogMessage = "To: (BLANK!)" & vbCrLf
    End If
    If CopyTo > "" Then LogMessage = LogMessage & "Cc: " & CopyTo & vbCrLf
    If BlindCopy > "" Then LogMessage = LogMessage & "Bcc: " & BlindCopy & vbCrLf
    If BodyFormat > "" Then LogMessage = LogMessage & "Format: " & BodyFormat & vbCrLf
    LogMessage = LogMessage & "Subject: " & Subject & vbCrLf
    LogMessage = LogMessage & Message & vbCrLf
    Call WriteIfDebug(CallerID & " : Email Not Sent Because of Invalid From or To Address:" & vbCrLf & LogMessage)
  End If
 
Exit_Function:
  SendMail = 0
  Exit Function
 
errorHandler:
  Call WriteToLog(CallerID & " : An error occurred: " & CStr(Err.Number) & "  " & Err.Description)
  On Error Resume Next
  Call WriteToLog(CallerID & " : Error sending email:" & vbCrLf & "To: " & Recipient & vbCrLf & "Subject: " & Subject & vbCrLf & Message)
  If Not (Mailer Is Nothing) Then Set Mailer = Nothing
  SendMail = -1
End Function

Function ScrubEmailDomain(Recipient As String) As String
  Dim Email()         As String
  Dim ValidEmails     As String
  Dim i               As Integer
  Dim ThisDomainDelim As Integer
  Dim ThisDomain      As String
  Dim Message         As String

  On Error GoTo errorHandler
  
  If EmailDomainRestrict > "" Then
    Recipient = Replace(Recipient, " ", "")
    Recipient = LCase(Trim(Replace(Recipient, ",", ";")))
    Recipient = Replace(Recipient, ";;", ";")
    Email = Split(Recipient, ";")
    ValidEmails = ""
    For i = 0 To UBound(Email)
      ThisDomainDelim = InStr(1, Email(i), "@")
      If ThisDomainDelim > 0 Then
        ThisDomain = Right(Email(i), Len(Email(i)) - ThisDomainDelim)
        If InStr(1, EmailDomainRestrict, ThisDomain) > 0 Then
          ' add to list
          ValidEmails = ValidEmails & Email(i) & ";"
        End If
      End If
    Next
    If Right(ValidEmails, 1) = ";" Then ValidEmails = Left(ValidEmails, Len(ValidEmails) - 1)
    ScrubEmailDomain = ValidEmails
  Else
    ScrubEmailDomain = Recipient
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ScrubEmailDomain: " & Message)
  ScrubEmailDomain = ""
End Function
Function ScrubNTDomain(parmDomain As String) As String
  Dim tempDomainRestrict  As String
  Dim tempDomain          As String
  Dim Message             As String

  On Error GoTo errorHandler
  If DomainRestrict > "" Then
    tempDomain = "," & LCase(Trim(parmDomain)) & ","
    tempDomainRestrict = "," & Replace(DomainRestrict, ";", ",") & ","
    If InStr(1, tempDomainRestrict, tempDomain) > 0 Then
      ScrubNTDomain = parmDomain
    Else
      ScrubNTDomain = ""
    End If
  Else
    ScrubNTDomain = parmDomain
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog("ScrubNTDomain: " & Message)
  ScrubNTDomain = ""
End Function

Public Function CallerInRole(is_CallerID As String, isa_Role As String, _
  objContext As ObjectContext) As Boolean

Dim CallerID     As String
Dim IsInRoleFlag As Boolean
Dim i            As Integer
Dim Roles()      As String
Dim Message      As String

' check to see if current user should be able to call this function

' sample usage:
'  If Not CallerInRole(CallerID, "ProcessAdmin,DomainAdmin") = true then
'    os_Message = "RoleCheck: User is not in role CreateUsers!"
'    ReturnCode = -2
'  End If

On Error GoTo errorHandler
CallerID = sModName & ": CallerInRole (" & is_CallerID & ")"
IsInRoleFlag = False
'Call WriteIfDebug(CallerID & " : Checking for role membership")

If isa_Role > "" Then
  Roles = Split(isa_Role, ",")
  If objContext.IsSecurityEnabled = True Then
    For i = 0 To UBound(Roles)
      If objContext.IsCallerInRole(Roles(i)) = True Then
        Message = "User is in role " & Roles(i)
        IsInRoleFlag = True
        Exit For
      Else
        Message = "User is not in role " & Roles(i)
      End If
    Next
  Else
    Message = "Security is not enabled!"
  End If
Else
  Message = "No roles were defined!"
  IsInRoleFlag = True
End If

If IsInRoleFlag = False Then Call WriteIfDebug(CallerID & ": " & Message)
CallerInRole = IsInRoleFlag
Exit Function

errorHandler:
  Call WriteToLog(CallerID & " : " & CStr(Err.Number) & "  " & Err.Description)
  On Error Resume Next
  CallerInRole = False
End Function

Friend Function GetCallerUserID(objContext As ObjectContext) As String

Dim CallerID     As String
Dim UserID       As String
Dim objRequest   As Request

On Error GoTo errorHandler
CallerID = sModName & ": GetCallerUserID ()"
UserID = ""
' go out to the ASP Request object and see who the logged in user is
Set objRequest = objContext("Request")
UserID = objRequest.ServerVariables("AUTH_USER")
UserID = Replace(UserID, "\", "/")
Set objRequest = Nothing

GetCallerUserID = UserID
Exit Function

errorHandler:
  Call WriteIfDebug(CallerID & " : " & CStr(Err.Number) & "  " & Err.Description)
  On Error Resume Next
  If Not (objRequest Is Nothing) Then Set objContext = Nothing
  GetCallerUserID = ""
End Function

Friend Function GetUserGroups(is_UserID As String, objContext As ObjectContext) As String
    
Dim MyUser        As IADsUser
Dim MyUserGroups  As IADsMembers
Dim SelectedGroup As IADsGroup
Dim MyGroupList   As String
Dim CallerID      As String
Dim objSession    As Session
Dim SlashPos      As Integer
Dim Message       As String

On Error GoTo errorHandler
CallerID = sModName & ": GetUserGroups(" & is_UserID & ")"
MyGroupList = ""
    
' go out to the ASP Session object and get the group list if it's there...
On Error Resume Next
Set objSession = objContext("Session")
MyGroupList = objSession("USER_NT_GROUPS")
On Error GoTo errorHandler

If MyGroupList > "" Then
  Call WriteIfDebug(CallerID & " : Retrieved groups for " & is_UserID & " from session: " & MyGroupList)
Else
  Call WriteIfDebug(CallerID & " : Retrieving groups for " & is_UserID & " from NT")
  is_UserID = Trim(Replace(is_UserID, "\", "/"))
  If Left(is_UserID, 1) = "/" Then is_UserID = DefaultDomain & is_UserID
  SlashPos = InStr(1, is_UserID, "/")
  If SlashPos < 1 Then
    is_UserID = DefaultDomain & "/" & is_UserID
  ElseIf SlashPos < 2 Then
    is_UserID = DefaultDomain & is_UserID
  End If
  Set MyUser = GetObject("WinNT://" & is_UserID)
  Set MyUserGroups = MyUser.Groups
  For Each SelectedGroup In MyUserGroups
    If MyGroupList > "" Then
      MyGroupList = MyGroupList & ",'" & SelectedGroup.Name & "'"
    Else
      MyGroupList = "'" & SelectedGroup.Name & "'"
    End If
  Next SelectedGroup
  
  On Error Resume Next
  objSession("USER_NT_GROUPS") = MyGroupList
  On Error GoTo errorHandler
  
  Call WriteIfDebug(CallerID & " : " & is_UserID & " Set objSession(USER_NT_GROUPS)=" & MyGroupList)
End If

Exit_Function:
  If Not (objSession Is Nothing) Then Set objSession = Nothing
  If Not (SelectedGroup Is Nothing) Then Set SelectedGroup = Nothing
  If Not (MyUserGroups Is Nothing) Then Set MyUserGroups = Nothing
  If Not (MyUser Is Nothing) Then Set MyUser = Nothing
  GetUserGroups = MyGroupList
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
  If Not (objSession Is Nothing) Then Set objSession = Nothing
  If Not (SelectedGroup Is Nothing) Then Set SelectedGroup = Nothing
  If Not (MyUserGroups Is Nothing) Then Set MyUserGroups = Nothing
  If Not (MyUser Is Nothing) Then Set MyUser = Nothing
  GetUserGroups = ""
End Function
Public Function ADSI_SuperUser(is_LoggedInUser As Variant) As Boolean
    
Dim MyDomain      As Variant
Dim MyUser        As Variant
Dim SlashPos      As Integer
Dim ReturnCode    As Variant
Dim Message       As Variant
Dim MySuperGroups As String
Dim CallerID      As String
Dim MyValue       As Boolean
Dim MyTeamNames() As String
Dim i             As Integer

On Error GoTo errorHandler
CallerID = "ADSI_SuperUser " & vbTab & "(" & is_LoggedInUser & ")" & vbTab

MySuperGroups = SuperUserGroups
If Not MySuperGroups > "" Then
  ADSI_SuperUser = True
  Exit Function
End If

' set default value
MyValue = False

is_LoggedInUser = Trim(is_LoggedInUser)
If Not is_LoggedInUser > "" Then
  Call WriteIfDebug(CallerID & " : UserID could not be found!")
  ADSI_SuperUser = False
  Exit Function
End If

SlashPos = InStr(1, is_LoggedInUser, "\")
If SlashPos > 0 Then
  MyDomain = Left(is_LoggedInUser, SlashPos - 1)
  MyUser = Right(is_LoggedInUser, Len(is_LoggedInUser) - SlashPos)
Else
  MyDomain = DefaultDomain
  MyUser = is_LoggedInUser
End If
If MyDomain > "" Then
  MyTeamNames = Split(MySuperGroups, ",")
  For i = 0 To UBound(MyTeamNames)
    If Trim(MyTeamNames(i)) > "" Then
      MyValue = ADSI_IsUserInGroup(is_LoggedInUser, MyDomain, MyUser, Trim(MyTeamNames(i)), ReturnCode, Message)
    End If
    If MyValue = True Then Exit For
  Next
End If

If MyValue = False Then
  Call WriteIfDebug(CallerID & " : User claimed to be but is NOT in SuperUser NT Group! (" & MySuperGroups & ")")
Else
  Call WriteIfDebug(CallerID & " : User is in SuperUser NT Group! (" & MySuperGroups & ")")
End If

ADSI_SuperUser = MyValue

Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
  ADSI_SuperUser = False
End Function

Public Function ADSI_IsUserInGroup(is_LoggedInUser As Variant, is_Domain As Variant, is_User As Variant, is_Group As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Boolean

Dim Container  As IADsContainer
Dim MyGroup    As IADsGroup
Dim MyUser     As IADsUser
Dim CallerID   As String
Dim MyAction   As String

On Error GoTo errorHandler
CallerID = "ADSI_IsUserInGroup " & vbTab & "(" & is_LoggedInUser & ")" & vbTab
'Call WriteIfDebug(CallerID & " : " & is_Domain & "\" & is_User & " " & is_Group)

MyAction = "Getting Domain " & is_Domain
Set Container = GetObject("WinNT://" & is_Domain)

MyAction = "Getting User " & is_User
Set MyUser = Container.GetObject("user", is_User)

MyAction = "Getting Group " & is_Group
Set MyGroup = Container.GetObject("group", is_Group)

MyAction = "Checking Membership in Group " & is_Group
If MyGroup.IsMember(MyUser.ADsPath) Then
  ADSI_IsUserInGroup = True
  os_ReturnCode = 1
  os_Message = is_User & " is in " & is_Group
  Call WriteIfDebug(CallerID & " : " & os_Message)
Else
  ADSI_IsUserInGroup = False
  os_ReturnCode = 0
  os_Message = is_User & " is not in " & is_Group
  Call WriteIfDebug(CallerID & " : " & os_Message)
End If

Set MyGroup = Nothing
Set MyUser = Nothing
Set Container = Nothing

Exit Function

errorHandler:
  os_Message = MyAction & ": " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  If Not (MyGroup Is Nothing) Then Set MyGroup = Nothing
  If Not (MyUser Is Nothing) Then Set MyUser = Nothing
  If Not (Container Is Nothing) Then Set Container = Nothing
  Call WriteToLog(CallerID & " : " & os_Message)
  os_ReturnCode = -1
  ADSI_IsUserInGroup = False
End Function

Public Function WebAuthorCheck(is_UserID As Variant, is_Web_ID As Variant, _
  is_Author_Type As String, is_SuperUser As Variant, objContext As ObjectContext) As Boolean
  
  Dim CallerID          As String
  Dim MyWebList         As String
  Dim Message           As String
  
  On Error GoTo errorHandler
  CallerID = sModName & ":WebAuthorCheck " & vbTab & "(" & is_UserID & ")" & vbTab

  If is_SuperUser = "Y" Then
    If ADSI_SuperUser(is_UserID) = False Then
      WebAuthorCheck = False
    Else
      WebAuthorCheck = True
    End If
  Else
    If IsNull(is_Web_ID) Then
      WebAuthorCheck = False
      Exit Function
    Else
      If Not IsNumeric(is_Web_ID) Then
        WebAuthorCheck = False
        Exit Function
      End If
    End If
  
    ' go get list of all webs that you are authorized to use
    MyWebList = GetWebAuthorRights(CStr(is_UserID), CStr(is_Author_Type), objContext)
    ' put comma fore and aft for searching
    MyWebList = "," & MyWebList & ","
    ' search the list for the web you're interested in
    If InStr(1, MyWebList, "," & is_Web_ID & ",") > 0 Then
      WebAuthorCheck = True
    Else
      WebAuthorCheck = False
    End If
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
  WebAuthorCheck = False
  On Error Resume Next
End Function
Friend Function GetWebAuthorRights(is_UserID As String, is_Author_Type As String, objContext As ObjectContext) As String
    
Dim CallerID      As String
Dim objSession    As Session
Dim cmd           As adodb.Command
Dim RS1           As adodb.Recordset
Dim SelectClause  As String
Dim WhereClause   As String
Dim OrderClause   As String
Dim SQL           As String
Dim MyGroupList   As String
Dim Message       As String
Dim MyWebList     As String
Dim SessionKey    As String

On Error GoTo errorHandler
CallerID = sModName & ": GetWebAuthorRights(" & is_UserID & ")"
MyWebList = ""
WhereClause = ""

is_Author_Type = UCase(Trim(is_Author_Type))
Select Case is_Author_Type
  Case "CONT"
    SessionKey = "WEB_CONTENT_RIGHTS"
  Case Else
    SessionKey = "WEB_AUTHOR_RIGHTS"
End Select

' go out to the ASP Session object and get the group list if it's there...
On Error Resume Next
Set objSession = objContext("Session")
MyWebList = objSession(SessionKey)
On Error GoTo errorHandler

If MyWebList > "" Then
  Call WriteIfDebug(CallerID & " : Retrieved web rights list from session: " & SessionKey & ": " & MyWebList)
Else
  Call WriteIfDebug(CallerID & " : Retrieving web author rights list from database ")
  SelectClause = "select Distinct Web.Web_Seq_ID" & _
    " From Web, Web_Author, Web_NT_Group" & _
    " Where Web.Web_Seq_ID = Web_Author.Web_Seq_ID " & _
    " AND Web_Author.Web_NT_Group_Seq_ID = Web_NT_Group.Web_NT_Group_Seq_ID "
  
  If is_Author_Type > "" Then
    WhereClause = WhereClause & " AND Web_Author.Author_Type_Code = '" & is_Author_Type & "'"
  Else
    WhereClause = WhereClause & " AND Web_Author.Author_Type_Code = 'ADMIN'"
  End If
  
  MyGroupList = GetUserGroups(is_UserID, objContext)
  If MyGroupList > "" Then
    Select Case GetDBEngine
    Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
      WhereClause = WhereClause & " AND Web_NT_Group.NT_Group_Txt in (" & LCase(MyGroupList) & ") "
    Case Else
      WhereClause = WhereClause & " AND Lower(Web_NT_Group.NT_Group_Txt) in (" & LCase(MyGroupList) & ") "
    End Select
  Else
    WhereClause = WhereClause & " AND Web_NT_Group.NT_Group_Txt in ('') "
    Message = "Error: No group memberships found for " & is_UserID & "!"
    Call WriteToLog(CallerID & " : " & Message)
  End If
  OrderClause = " Order By Web.Web_Seq_ID"
  
  SQL = SelectClause & WhereClause & OrderClause
  Call WriteIfDebug(CallerID & " : " & SQL)
  
  Set cmd = CtxCreateObject("ADODB.Command")
  With cmd
    .ActiveConnection = GetConn
    .CommandType = adCmdText
    .CommandTimeout = DBTimeout
    .ActiveConnection.CommandTimeout = DBTimeout
  End With
  
  'retrieve list of webs owned by groups this person is in
  Set RS1 = CtxCreateObject("ADODB.Recordset")
  cmd.CommandText = SQL
  RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
  ' build into comma seperated list
  MyWebList = ""
  Do While Not RS1.EOF
    If MyWebList <> "" Then
      MyWebList = MyWebList & "," & RS1("Web_Seq_ID")
    Else
      MyWebList = RS1("Web_Seq_ID")
    End If
    RS1.MoveNext
  Loop
  RS1.Close
  Set RS1 = Nothing
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  
  On Error Resume Next
  objSession(SessionKey) = MyWebList
  On Error GoTo errorHandler
  
  Call WriteIfDebug(CallerID & " : Set objSession(" & SessionKey & ")=" & MyWebList)
End If

Exit_Function:
  If Not (objSession Is Nothing) Then Set objSession = Nothing
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  GetWebAuthorRights = MyWebList
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
  If Not (objSession Is Nothing) Then Set objSession = Nothing
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  GetWebAuthorRights = ""
End Function

Public Function ChannelAuthorCheck(is_UserID As Variant, is_Channel_ID As Variant, _
  is_Author_Type As String, is_SuperUser As Variant, objContext As ObjectContext) As Boolean
  
  Dim CallerID          As String
  Dim MyChannelList     As String
  Dim Message           As String
  
  On Error GoTo errorHandler
  CallerID = sModName & ":ChannelAuthorCheck " & vbTab & "(" & is_UserID & ")" & vbTab

  If is_SuperUser = "Y" Then
    If ADSI_SuperUser(is_UserID) = False Then
      ChannelAuthorCheck = False
    Else
      ChannelAuthorCheck = True
    End If
  Else
    If IsNull(is_Channel_ID) Then
      ChannelAuthorCheck = False
      Exit Function
    Else
      If Not IsNumeric(is_Channel_ID) Then
        ChannelAuthorCheck = False
        Exit Function
      End If
    End If
  
    ' go get list of all Channels that you are authorized to use
    MyChannelList = GetChannelAuthorRights(CStr(is_UserID), CStr(is_Author_Type), objContext)
    ' put comma fore and aft for searching
    MyChannelList = "," & MyChannelList & ","
    ' search the list for the Channel you're interested in
    If InStr(1, MyChannelList, "," & is_Channel_ID & ",") > 0 Then
      ChannelAuthorCheck = True
    Else
      ChannelAuthorCheck = False
    End If
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
  ChannelAuthorCheck = False
  On Error Resume Next
End Function
Friend Function GetChannelAuthorRights(is_UserID As String, is_Author_Type As String, objContext As ObjectContext) As String
    
Dim CallerID      As String
Dim objSession    As Session
Dim cmd           As adodb.Command
Dim RS1           As adodb.Recordset
Dim SelectClause  As String
Dim WhereClause   As String
Dim OrderClause   As String
Dim SQL           As String
Dim MyGroupList   As String
Dim MyChannelList As String
Dim Message       As String
Dim SessionKey    As String

On Error GoTo errorHandler
CallerID = sModName & ": GetChannelAuthorRights(" & is_UserID & ")"
MyChannelList = ""
WhereClause = ""
SessionKey = "CHANNEL_AUTHOR_RIGHTS"

is_Author_Type = UCase(Trim(is_Author_Type))
If is_Author_Type > "" Then
  SessionKey = SessionKey & "_" & is_Author_Type
End If

' go out to the ASP Session object and get the group list if it's there...
On Error Resume Next
Set objSession = objContext("Session")
MyChannelList = objSession(SessionKey)
On Error GoTo errorHandler

If MyChannelList > "" Then
  Call WriteIfDebug(CallerID & " : Retrieved Channel author rights list from session: " & MyChannelList)
Else
  Call WriteIfDebug(CallerID & " : Retrieving Channel author rights list from database ")
  SelectClause = "select Distinct Channel.Channel_Seq_ID" & _
    " From Channel, Channel_Author, Web_NT_Group" & _
    " Where Channel.Channel_Seq_ID = Channel_Author.Channel_Seq_ID " & _
    " AND Channel_Author.Web_NT_Group_Seq_ID = Web_NT_Group.Web_NT_Group_Seq_ID "
  
  If is_Author_Type > "" Then
    WhereClause = WhereClause & " AND Channel_Author.Author_Type_Code = '" & is_Author_Type & "'"
  End If
  
  MyGroupList = GetUserGroups(is_UserID, objContext)
  If MyGroupList > "" Then
    Select Case GetDBEngine
    Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
      WhereClause = WhereClause & " AND Web_NT_Group.NT_Group_Txt in (" & LCase(MyGroupList) & ") "
    Case Else
      WhereClause = WhereClause & " AND Lower(Web_NT_Group.NT_Group_Txt) in (" & LCase(MyGroupList) & ") "
    End Select
  Else
    WhereClause = WhereClause & " AND Web_NT_Group.NT_Group_Txt in ('') "
    Message = "Error: No group memberships found for " & is_UserID & "!"
    Call WriteToLog(CallerID & " : " & Message)
  End If
  OrderClause = " Order By Channel.Channel_Seq_ID"
  
  SQL = SelectClause & WhereClause & OrderClause
  Call WriteIfDebug(CallerID & " : " & SQL)
  
  Set cmd = CtxCreateObject("ADODB.Command")
  With cmd
    .ActiveConnection = GetConn
    .CommandType = adCmdText
    .CommandTimeout = DBTimeout
    .ActiveConnection.CommandTimeout = DBTimeout
  End With
  'retrieve list of channels owned by groups this person is in
  Set RS1 = CtxCreateObject("ADODB.Recordset")
  cmd.CommandText = SQL
  RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
  ' build into comma seperated list
  MyChannelList = ""
  Do While Not RS1.EOF
    If MyChannelList <> "" Then
      MyChannelList = MyChannelList & "," & RS1("Channel_Seq_ID")
    Else
      MyChannelList = RS1("Channel_Seq_ID")
    End If
    RS1.MoveNext
  Loop
  RS1.Close
  Set RS1 = Nothing
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  
  On Error Resume Next
  objSession(SessionKey) = MyChannelList
  On Error GoTo errorHandler
  
  Call WriteIfDebug(CallerID & " : Set objSession(CHANNEL_AUTHOR_RIGHTS)=" & MyChannelList)
End If

Exit_Function:
  If Not (objSession Is Nothing) Then Set objSession = Nothing
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  GetChannelAuthorRights = MyChannelList
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  On Error Resume Next
  Call WriteToLog(CallerID & " : " & Message)
  If Not (objSession Is Nothing) Then Set objSession = Nothing
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  GetChannelAuthorRights = ""
End Function

Friend Function Usage_Log_Recorder(is_UserID As Variant, is_LogType As Variant, ii_Web_Seq_ID As Variant, _
  is_Usage_Type_Code As Variant, is_App_Type_Code As Variant, is_Referrer As Variant, ii_Sort_Order_Nbr As Variant, _
  is_Dscr As Variant, is_Log_Txt As Variant, ii_Counter As Variant, ii_Error_Nbr As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Integer
  
  Dim CallerID       As String
  Dim Text_Message   As String
  Dim ThisQueue      As String
  Dim os_SQL         As String
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Usage_Log_Recorder " & vbTab & "(" & is_UserID & ")" & vbTab
  os_ReturnCode = -1
  ThisQueue = ""

'  Call WriteIfDebug(CallerID & " : Log Entry Requested: " & Text_Message)
  
  Select Case UCase(Trim(is_LogType))
   Case "NONE"
     ' do nothing
     
  Case "DATABASE"
    Select Case GetDBEngine
      Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
        os_SQL = "Insert into Usage_Log(" & _
          " Web_Seq_ID, Usage_Type_Code, App_Type_Code, Referrer_URL_ADDR, Sort_Order_Nbr, Month_Nbr, Day_Nbr, Year_Nbr,  " & _
          " Dscr, Log_Txt, Counter_NBR, Error_Nbr, Process_IND, " & _
          " CREATE_DATE, CREATE_USER_ID, CREATE_PROG_NAME, CHG_LST_DATE, CHG_LST_USER_ID, CHG_LST_PROG_NAME)" & _
          " Values ("
      Case Else
        os_SQL = "Insert into Usage_Log(Usage_Log_Seq_ID, " & _
          " Web_Seq_ID, Usage_Type_Code, App_Type_Code, Referrer_URL_ADDR, Sort_Order_Nbr, Month_Nbr, Day_Nbr, Year_Nbr,  " & _
          " Dscr, Log_Txt, Counter_NBR, Error_Nbr, Process_IND, " & _
          " CREATE_DATE, CREATE_USER_ID, CREATE_PROG_NAME, CHG_LST_DATE, CHG_LST_USER_ID, CHG_LST_PROG_NAME)" & _
          " Values (Usage_Log_S01.NextVal, "
      End Select
      
      os_SQL = os_SQL & _
        ScrubNumberNull(ii_Web_Seq_ID) & ", " & _
        "'" & SQLScrub(is_Usage_Type_Code) & "', " & _
        "'" & SQLScrub(is_App_Type_Code) & "', " & _
       "'" & SQLScrub(NoLongerThan(is_Referrer, 500)) & "', " & _
        ScrubNumberNull(ii_Sort_Order_Nbr) & ", " & _
        Month(Date) & "," & Day(Date) & "," & Year(Date) & "," & "'" & _
        SQLScrub(NoLongerThan(is_Dscr, 255)) & "', " & _
        "'" & SQLScrub(NoLongerThan(is_Log_Txt, 2000)) & "', " & _
        ScrubNumberNull(ii_Counter) & ", " & _
        ScrubNumberNull(ii_Error_Nbr) & ", " & _
        "'N', " & _
        Audit_Fields(is_UserID, "INSERTSQL") & ")"
      Call WriteIfDebug(CallerID & " : " & os_SQL)
      Call ExecSQL(os_SQL)
     
   Case "FILE"
     Text_Message = _
        is_UserID & vbTab & _
        ii_Web_Seq_ID & vbTab & _
        is_Usage_Type_Code & vbTab & _
        is_App_Type_Code & vbTab & _
        is_Referrer & vbTab & _
        ii_Sort_Order_Nbr & vbTab & _
        ii_Counter & vbTab & _
        ii_Error_Nbr & vbTab & _
        is_Dscr & vbTab & _
        is_Log_Txt
     ThisQueue = AsyncLogQueue
     ThisQueue = DateifyFileName(ThisQueue)
     'Call WriteToFile(ThisQueue, Text_Message)
   
   Case "MSMQ", "REMOTE_MSMQ"
    Text_Message = _
      "Date=" & Now() & vbCrLf & _
      "UserID=" & is_UserID & vbCrLf & _
      "Web_Seq_ID=" & ii_Web_Seq_ID & vbCrLf & _
      "Usage_Type_Code=" & is_Usage_Type_Code & vbCrLf & _
      "App_Type_Code=" & is_App_Type_Code & vbCrLf & _
      "Referrer=" & is_Referrer & vbCrLf & _
      "Sort_Order_Nbr=" & ii_Sort_Order_Nbr & vbCrLf & _
      "Counter=" & ii_Counter & vbCrLf & _
      "Error_Nbr=" & ii_Error_Nbr & vbCrLf & _
      "Description=" & is_Dscr & vbCrLf & _
      "Log_Txt=" & is_Log_Txt

    '  early bind variables
    Dim objQInfo As MSMQQueueInfo
    Dim oQueue   As MSMQQueue
    Dim oMessage As MSMQMessage
    
    '  late bind variables
    'Dim objQInfo As Object
    'Dim oQueue   As Object
    'Dim oMessage As Object
    
    ThisQueue = AsyncLogQueue
    If InStr(1, ThisQueue, "\") < 1 Then
      ThisQueue = ".\" & ThisQueue
    End If
    'open the queue
    'Set objQInfo = GetObjectContext.CreateInstance("MSMQ.MSMQQueueInfo")
    Set objQInfo = New MSMQQueueInfo
    objQInfo.PathName = ThisQueue
    'Set oQueue = objQInfo.Open(2, 0)
    Set oQueue = objQInfo.Open(MQ_SEND_ACCESS, MQ_DENY_NONE)
   
    'build/send the message
'    Set oMessage = GetObjectContext.CreateInstance("MSMQ.MSMQMessage")
'    With oMessage
'        .Label = "CCRC Log: " & is_App_Type_Code & ":" & is_Usage_Type_Code
'        .Body = Text_Message
'        .Send oQueue
'    End With
    Set oMessage = New MSMQMessage
    With oMessage
        .Delivery = MQMSG_DELIVERY_RECOVERABLE
        .Label = "CCRC Log: " & is_App_Type_Code & ":" & is_Usage_Type_Code
        .Body = Text_Message
        .Priority = MQ_MAX_PRIORITY
        .Send oQueue, MQ_NO_TRANSACTION
    End With
    Set oMessage = Nothing
    oQueue.Close
    Set oQueue = Nothing
    Set objQInfo = Nothing
    os_Message = "Message delivered to queue " & AsyncLogQueue & " without error"
   
   Case Else
    os_Message = "Invalid log method: " & AsyncLogMethod
    Call WriteIfDebug(CallerID & " : Invalid async log method: " & AsyncLogMethod)
  End Select
  os_ReturnCode = 0
  
Exit_Function:
  Usage_Log_Recorder = os_ReturnCode
  Exit Function

errorHandler:
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  On Error Resume Next
  If ThisQueue > "" Then
    os_Message = os_Message & "; Queue=" & ThisQueue
  End If
  If Not (objQInfo Is Nothing) Then Set objQInfo = Nothing
  If Not (oQueue Is Nothing) Then Set oQueue = Nothing
  If Not (oMessage Is Nothing) Then Set oMessage = Nothing
  Call WriteToLog(CallerID & " : " & os_Message, 2)
  Usage_Log_Recorder = os_ReturnCode
End Function

Friend Sub SetError()
'   Save any previous error information.
    With Err
        lErrorNumber = .Number
        strErrorDescription = .Description
        strErrorSource = .Source
    End With
End Sub

Friend Sub GetError()
'   Put the original error information back.
    With Err
        .Number = lErrorNumber
        .Description = strErrorDescription
        .Source = strErrorSource
    End With
End Sub

Friend Function ToCharDate(ByVal sValue As String) As String
  Dim dDate As Date
  dDate = CDate(sValue)
  
  If Len(Month(dDate)) = 1 Then
    sValue = "0" & Month(dDate) & "/"
  Else
    sValue = Month(dDate) & "/"
  End If
  
  If Len(Day(dDate)) = 1 Then
    sValue = sValue & "0" & Day(dDate) & "/"
  Else
    sValue = sValue & Day(dDate) & "/"
  End If
  
  If Len(Year(dDate)) < 4 Then
    sValue = sValue & "20" & Year(dDate)
  Else
    sValue = sValue & Year(dDate)
  End If
  
  ToCharDate = sValue
End Function
