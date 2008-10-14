VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cReg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit

' Reg Data Types...
Private Const REG_NONE = 0                       ' No value type
Private Const REG_SZ = 1                         ' Unicode nul terminated string
Private Const REG_EXPAND_SZ = 2                  ' Unicode nul terminated string
Private Const REG_BINARY = 3                     ' Free form binary
Private Const REG_DWORD = 4                      ' 32-bit number
Private Const REG_DWORD_LITTLE_ENDIAN = 4        ' 32-bit number (same as REG_DWORD)
Private Const REG_DWORD_BIG_ENDIAN = 5           ' 32-bit number
Private Const REG_LINK = 6                       ' Symbolic Link (unicode)
Private Const REG_MULTI_SZ = 7                   ' Multiple Unicode strings
Private Const REG_RESOURCE_LIST = 8              ' Resource list in the resource map
Private Const REG_FULL_RESOURCE_DESCRIPTOR = 9   ' Resource list in the hardware description
Private Const REG_RESOURCE_REQUIREMENTS_LIST = 10
Private Const REG_CREATED_NEW_KEY = &H1                      ' New Registry Key created
Private Const REG_OPENED_EXISTING_KEY = &H2                      ' Existing Key opened
Private Const REG_WHOLE_HIVE_VOLATILE = &H1                      ' Restore whole hive volatile
Private Const REG_REFRESH_HIVE = &H2                      ' Unwind changes to last flush
Private Const REG_NOTIFY_CHANGE_NAME = &H1                      ' Create or delete (child)
Private Const REG_NOTIFY_CHANGE_ATTRIBUTES = &H2
Private Const REG_NOTIFY_CHANGE_LAST_SET = &H4                      ' Time stamp
Private Const REG_NOTIFY_CHANGE_SECURITY = &H8
Private Const REG_LEGAL_CHANGE_FILTER = (REG_NOTIFY_CHANGE_NAME Or REG_NOTIFY_CHANGE_ATTRIBUTES Or REG_NOTIFY_CHANGE_LAST_SET Or REG_NOTIFY_CHANGE_SECURITY)
'Private Const REG_LEGAL_OPTION = (REG_OPTION_RESERVED Or REG_OPTION_NON_VOLATILE Or REG_OPTION_VOLATILE Or REG_OPTION_CREATE_LINK Or REG_OPTION_BACKUP_RESTORE)

' Reg Create Type Values...
Private Const REG_OPTION_RESERVED = 0           ' Parameter is reserved
Private Const REG_OPTION_NON_VOLATILE = 0       ' Key is preserved when system is rebooted
Private Const REG_OPTION_VOLATILE = 1           ' Key is not preserved when system is rebooted
Private Const REG_OPTION_CREATE_LINK = 2        ' Created key is a symbolic link
Private Const REG_OPTION_BACKUP_RESTORE = 4     ' open for backup or restore

' Reg Key Security Options
Private Const READ_CONTROL = &H20000
Private Const KEY_QUERY_VALUE = &H1
Private Const KEY_SET_VALUE = &H2
Private Const KEY_ALL_ACCESS = &H3F
Private Const KEY_CREATE_SUB_KEY = &H4
Private Const KEY_ENUMERATE_SUB_KEYS = &H8
Private Const KEY_NOTIFY = &H10
Private Const KEY_CREATE_LINK = &H20
Private Const ERR_MORE_DATA = 234

' function prototypes, constants, and type definitions
' for Windows 32-bit Registry API
Private Const HKEY_CLASSES_ROOT = &H80000000
Private Const HKEY_CURRENT_USER = &H80000001
Private Const HKEY_LOCAL_MACHINE = &H80000002
Private Const HKEY_USERS = &H80000003
Private Const HKEY_PERFORMANCE_DATA = &H80000004
Private Const HKEY_CURRENT_CONFIG = &H80000005
Private Const HKEY_DYN_DATA = &H80000006

Enum regKeyRoots
  classes = &H80000000
  current_user = &H80000001
  local_machine = &H80000002
  users = &H80000003
  performance_data = &H80000004
  current_config = &H80000005
  dyn_data = &H80000006
End Enum

Private Type SECURITY_ATTRIBUTES
        nLength As Long
        lpSecurityDescriptor As Long
        bInheritHandle As Long
End Type

Private Type FILETIME
        dwLowDateTime As Long
        dwHighDateTime As Long
End Type


' Registry API prototypes
Private Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hkey As Long) As Long
Private Declare Function RegConnectRegistry Lib "advapi32.dll" Alias "RegConnectRegistryA" (ByVal lpMachineName As String, ByVal hkey As Long, phkResult As Long) As Long
'Private Declare Function RegCreateKeyEx Lib "advapi32.dll" Alias "RegCreateKeyExA" (ByVal hkey As Long, ByVal lpSubKey As String, ByVal Reserved As Long, ByVal lpClass As String, ByVal dwOptions As Long, ByVal samDesired As Long, lpSecurityAttributes As SECURITY_ATTRIBUTES, phkResult As Long, lpdwDisposition As Long) As Long
Private Declare Function RegDeleteKey Lib "advapi32.dll" Alias "RegDeleteKeyA" (ByVal hkey As Long, ByVal lpSubKey As String) As Long
Private Declare Function RegDeleteValue Lib "advapi32.dll" Alias "RegDeleteValueA" (ByVal hkey As Long, ByVal lpValueName As String) As Long
Private Declare Function RegOpenKeyEx Lib "advapi32.dll" Alias "RegOpenKeyExA" (ByVal hkey As Long, ByVal lpSubKey As String, ByVal ulOptions As Long, ByVal samDesired As Long, phkResult As Long) As Long
Private Declare Function RegOpenKey Lib "advapi32.dll" Alias "RegOpenKeyA" (ByVal hkey As Long, ByVal lpSubKey As String, phkResult As Long) As Long
Private Declare Function RegQueryInfoKey Lib "advapi32.dll" Alias "RegQueryInfoKeyA" (ByVal hkey As Long, ByVal lpClass As String, lpcbClass As Long, lpReserved As Long, lpcSubKeys As Long, lpcbMaxSubKeyLen As Long, lpcbMaxClassLen As Long, lpcValues As Long, lpcbMaxValueNameLen As Long, lpcbMaxValueLen As Long, lpcbSecurityDescriptor As Long, lpftLastWriteTime As FILETIME) As Long
Private Declare Function RegQueryValueEx Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hkey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, lpType As Long, lpData As Any, lpcbData As Long) As Long         ' Note that if you private Declare the lpData parameter as String, you must pass it By Value.
Private Declare Function RegSetValueExStr Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hkey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, ByVal lpData As String, ByVal cbData As Long) As Long
Private Declare Function RegSetValueExPtr Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hkey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, lpData As Any, ByVal cbData As Long) As Long

Private Declare Function RegEnumKeyEx Lib "advapi32.dll" Alias "RegEnumKeyExA" (ByVal hkey As Long, ByVal dwIndex As Long, ByVal lpName As String, lpcbName As Long, ByVal lpReserved As Long, ByVal lpClass As String, lpcbClass As Long, lpftLastWriteTime As FILETIME) As Long
Private Declare Function RegEnumValueType Lib "advapi32.dll" Alias "RegEnumValueA" (ByVal hkey As Long, ByVal dwIndex As Long, ByVal lpValueName As String, lpcbValueName As Long, ByVal lpReserved As Long, lpType As Long, ByVal null1 As Long, ByVal null2 As Long) As Long
Private Declare Function RegEnumValue Lib "advapi32.dll" Alias "RegEnumValueA" (ByVal hkey As Long, ByVal dwIndex As Long, ByVal lpValueName As String, lpcbValueName As Long, ByVal lpReserved As Long, lpType As Long, lpData As Byte, lpcbData As Long) As Long
Private Declare Function RegEnumValueString Lib "advapi32.dll" Alias "RegEnumValueA" (ByVal hkey As Long, ByVal dwIndex As Long, ByVal lpValueName As String, lpcbValueName As Long, ByVal lpReserved As Long, lpType As Long, lpData As String, lpcbData As Long) As Long

'//----------------------------------------------------------------------
'// PRIVATE CLASS DATA
'//
Private m_hKey As Long
Private sRegEnumOutput As String  ' store output here for registry enumeration
Private lRegEnumLimit As Long     ' character limit of output to spit out - prevent from listing entire registry
Private iEnumIndent As Integer    ' indent level for enumeration string

Friend Function OpenServiceKey(sKey As String) As Boolean
  Dim sNewKey As String
  
  sNewKey = "SYSTEM\CurrentControlSet\Services\" & sKey
  OpenServiceKey = OpenKey(HKEY_LOCAL_MACHINE, sNewKey)

End Function

Friend Function OpenUserKey(sKey As String) As Boolean
  OpenUserKey = OpenKey(HKEY_CURRENT_USER, sKey)
End Function

Friend Function OpenSystemKey(sKey As String)
  OpenSystemKey = OpenKey(HKEY_LOCAL_MACHINE, sKey)
End Function

Friend Function OpenSoftwareKey(sKey As String)
  Dim sNewKey As String
  sNewKey = "SOFTWARE\" & sKey
  OpenSoftwareKey = OpenKey(HKEY_LOCAL_MACHINE, sNewKey)
End Function
Private Function WriteToFle(filename As String, msg As String)
  ' writes a string to a file - print statement with a ; on the end doesn't write a crlf...
  Dim Message           As String
  Dim FileNumber        As Integer
  
  On Error GoTo errorHandler
  If filename > "" Then
    FileNumber = FreeFile
    Open filename For Append As #FileNumber
    Print #FileNumber, vbCrLf & Now() & vbTab & msg;
    Close #FileNumber
  End If
  Exit Function

errorHandler:
  Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
'  Call SendMail(EmailFrom, EmailErrorsTo, "", "", "", ServerName & ": An error occured while writing to log file!", Message)
End Function
Private Function OpenKey(hkey As Long, sKey As String) As Boolean
  Dim lRet As Long
   
  '// The key should not be in use.
'  Debug.Assert m_hKey = 0
  
  OpenKey = True
  Dim FileNumber        As Integer
  
  FileNumber = WriteToFle("c:\ccommon_.log", sKey)
  
  lRet = RegOpenKey(hkey, sKey, m_hKey)
  If lRet <> 0 Then
'    Debug.Assert False
    OpenKey = False
    Exit Function
  End If
  
  If m_hKey = 0 Then
'    Debug.Assert False
    OpenKey = False
    Exit Function
  End If
  
End Function

Friend Sub CloseKey()
  RegCloseKey m_hKey
  m_hKey = 0
End Sub

Private Sub Class_Initialize()
  m_hKey = 0
End Sub

Private Sub Class_Terminate()
  If m_hKey <> 0 Then
    CloseKey
  End If
End Sub

Friend Function QueryValueStr(sVal2Get As String) As String
  Dim lRet        As Long
  Dim rgData(255) As Byte
  Dim lLen        As Long
  
  lLen = 256
  
'  Debug.Assert m_hKey <> 0
  lRet = RegQueryValueEx(m_hKey, sVal2Get, 0, REG_EXPAND_SZ, rgData(0), lLen)
  If lRet <> 0 Then
'    Debug.Assert False
    QueryValueStr = ""
    Exit Function
  Else
  'new code
    If rgData(0) = 0 Then
      QueryValueStr = ""
      Exit Function
    End If
    QueryValueStr = Left$(StrConv(rgData, vbUnicode), lLen - 1)
  End If
  QueryValueStr = Trim(QueryValueStr)
  
End Function

Friend Function SetValueStr(sValueName As String, sValue As String) As Boolean
  Dim lRet        As Long
  
  lRet = RegSetValueExStr(m_hKey, sValueName, 0, REG_SZ, sValue, Len(sValue))
  If lRet <> 0 Then
'    Debug.Assert False
    SetValueStr = False
  Else
    SetValueStr = True
  End If
  
End Function

Friend Function EnumLocalMachineSubKey(ByVal inSubKey As String) As String
  sRegEnumOutput = App.EXEName & ".DLL Version: " & App.Major & "." & App.Minor & "." & App.Revision & vbCrLf & _
    "As of " & Now & vbCrLf & _
    "Registry Entries for HKEY_LOCAL_MACHINE\SOFTWARE\" & inSubKey & vbCrLf & vbCrLf
  lRegEnumLimit = 10000
  Call DoEnumSubKeys(HKEY_LOCAL_MACHINE, "SOFTWARE\" & inSubKey)
  EnumLocalMachineSubKey = sRegEnumOutput
End Function

Private Sub DoEnumSubKeys(ByVal inMainKey As Long, ByVal inSubKey As String)
  Dim mKey             As Long
  Dim colSubKeys       As Collection
  Dim mBuffer          As String * 256
  Dim mBufSize         As Long
  Dim mClassBuffer     As String * 256
  Dim mClassBufSize    As Long
  Dim typLastWriteTime As FILETIME
  
  Dim mSubKeyName      As String
  Dim mSubKeyValue     As String
  Dim mValType         As Long
  Dim mIndex           As Integer
  Dim i                As Integer
  Dim mResult          As Long
  Dim mStopFlag        As Boolean

  Set colSubKeys = New Collection
  If RegOpenKeyEx(inMainKey, inSubKey, 0&, KEY_ALL_ACCESS, mKey) <> 0 Then
    sRegEnumOutput = sRegEnumOutput & vbCrLf & "Unable to open subkey " & inSubKey
    Exit Sub
  End If

  ListEntryValues inMainKey, inSubKey
  
  ' The above list the current subkey and its own entries and values
  '===================================================================
  ' Enumerate the Subkey's colSubKeys
  mIndex = 0
  Do
    ' lpClassBuffer is a pointer to a buffer that receives the null-terminated
    ' class string of the enumerated subkey. No classes are currently defined;
    ' hence this parameter can be NULL.
    ' lpClassBufSize is a pointer to a variable that specifies the size of
    ' lpClassBuffer, including the terminating null character. When the function
    ' returns, it contains the number of characters stored in the buffer.
    ' The count returned does not include the terminating null character.
    mClassBuffer = ""
    mClassBufSize = 0
    mBufSize = 256
    mSubKeyName = Space$(mBufSize)
  
    mResult = RegEnumKeyEx(mKey, mIndex, mSubKeyName, mBufSize, 0, mClassBuffer, _
      mClassBufSize, typLastWriteTime)
    If mResult <> ERR_MORE_DATA And mResult <> 0 Then
      'sRegEnumOutput = sRegEnumOutput & vbCrLf & APIErrorDescription(mResult)
      Exit Do
    End If
   
    mSubKeyName = Left$(mSubKeyName, InStr(mSubKeyName, Chr$(0)) - 1)
    colSubKeys.Add mSubKeyName
    mIndex = mIndex + 1
  Loop
  
  RegCloseKey mKey
  
  If mIndex > 0 Then
    iEnumIndent = iEnumIndent + 3
  Else
    iEnumIndent = 3
  End If
   
   ' Recursively get information on the keys.
  For i = 1 To colSubKeys.Count
    DoEvents
    ' Registry is a huge database; in case user input is a big subkey,
    ' we don't want to wait for too long.  Hence set a limit here.
    If Len(sRegEnumOutput) > lRegEnumLimit Then
      sRegEnumOutput = sRegEnumOutput & "......(more) ......." & vbCrLf
      mStopFlag = True   ' To spin off the loop of "For mIndex = 1 To colSubKeys.Count"
      Exit For
    End If
    If mStopFlag Then
      Exit For
    End If
    DoEnumSubKeys inMainKey, inSubKey & "\" & colSubKeys(i)
  Next i
End Sub

Private Sub ListEntryValues(ByVal inMainKey As Long, ByVal inSubKey As String)
  Dim mKey             As Long
  Dim mIndex           As Long
  Dim mReturnEntry     As String
  Dim mEntry           As String
  Dim mEntryLength     As Long
  Dim mDataType        As Long
  Dim arrDataByte(1 To 1024) As Byte
  Dim mDataByteLength  As Long
  Dim mDataByteValue   As String
  Dim i                As Integer
  Dim j                As Integer
  Dim y                As Integer
  Dim tmp              As String
  Dim mChr             As String
  Dim mAlignPos        As Integer
  Dim mAlignPos0       As Integer
  Dim mAlignPos1       As Integer
  Dim mResult          As Long
  
  mResult = RegOpenKeyEx(inMainKey, inSubKey, 0&, KEY_ALL_ACCESS, mKey)
  If mResult <> 0 Then
    sRegEnumOutput = sRegEnumOutput & vbCrLf & "Error RegOpenKeyEx for " & inSubKey
    Exit Sub
  End If
  
   '-----------------------------
  tmp = ""
  For i = Len(inSubKey) To 1 Step -1
    mChr = Mid(inSubKey, i, 1)
    If mChr = "\" Then
      Exit For
    End If
    tmp = mChr & tmp
  Next i
  
  ' Display subkey (without its parent) and its own entries (& values),
  ' at a position of iEnumIndent
  sRegEnumOutput = sRegEnumOutput & Space(iEnumIndent) & tmp
  mAlignPos0 = 5
  mAlignPos1 = mAlignPos0 + iEnumIndent + Len(tmp)
  
  'If someflag = True Then    ' Display subkey(s) only
  '  ' Next line for another subkey
  '  sRegEnumOutput = sRegEnumOutput & vbCrLf
  '  Exit Sub
  'End If
  
  mIndex = 0
  Do
    If mIndex = 0 Then
      mAlignPos = mAlignPos0
    Else
      mAlignPos = mAlignPos1
    End If
  
    mEntryLength = 1024
    mDataByteLength = 1024
    mEntry = Space$(mEntryLength)
    mResult = RegEnumValue(mKey, mIndex, mEntry, mEntryLength, 0, _
      mDataType, arrDataByte(1), mDataByteLength)
    If mResult <> 0 Then      ' No more
      Exit Do
    End If
  
    mEntry = Left$(mEntry, mEntryLength)
  
    ' Note if value is "(No value set)" then the following
    ' will not be display, i.e. as if no entry exists.
    If mEntry = "" And mDataByteLength > 0 Then
      mEntry = "[Default]"
    End If
  
    mDataByteValue = ""
    Select Case mDataType
      Case REG_SZ
        For i = 1 To mDataByteLength - 1
          mDataByteValue = mDataByteValue & Chr$(arrDataByte(i))
        Next i
        sRegEnumOutput = sRegEnumOutput & Space(mAlignPos) & mEntry & " = """ & _
        mDataByteValue & """" & vbCrLf
      Case REG_DWORD
        ' For display in original format as in registry
        tmp = ""
        For i = 4 To 1 Step -1
          mChr = Hex(Asc(Chr(arrDataByte(i))))
          If Len(mChr) = 1 Then
            mChr = "0" + mChr
          End If
          tmp = tmp & mChr
        Next
        ' For display of actual value in decimal as in registry
        y = 0
        For i = 1 To Len(tmp) Step 2
          mChr = Mid(tmp, i, 2)
          mChr = ConvNum(mChr, 16, 10)
          j = Val(mChr)
          Select Case i
            Case 1
              j = j * &H1000000   ' 16777216
            Case 2
              j = j * &H10000     ' 65536
            Case 3
              j = j * &H100     ' 256
          End Select
          y = y + j
        Next
        mDataByteValue = CStr(y)
        sRegEnumOutput = sRegEnumOutput & Space(mAlignPos) & mEntry & " = (DWORD) """ & tmp & _
          " (" & mDataByteValue & ")" & """" & vbCrLf
      Case REG_BINARY
        For i = 1 To mDataByteLength
          mChr = Hex$(Asc(Chr(arrDataByte(i))))
          If Len(mChr) = 1 Then
            mChr = "0" & mChr
          End If
          mDataByteValue = mDataByteValue & mChr & " "
        Next
        sRegEnumOutput = sRegEnumOutput & Space(mAlignPos) & mEntry & " = (BINARY) """ & _
        Trim(mDataByteValue) & """" & vbCrLf
      Case Else
        sRegEnumOutput = sRegEnumOutput & Space(mAlignPos) & mEntry & " = (Value not string, numeric or binary type)" & vbCrLf
    End Select
    mIndex = mIndex + 1
  Loop
  RegCloseKey mKey
  ' Next line for another subkey
  sRegEnumOutput = sRegEnumOutput & vbCrLf
End Sub

Private Function ConvNum(inNum As String, inFrom As Integer, inTo As Integer) As String
  Dim mChr   As String
  Dim i      As Integer
  Dim j      As Integer
  Dim k      As Integer
  Dim mTotal As Double
  Dim mMod   As Double
  Dim tmp    As Double
  
  ConvNum = ""
  If inNum = "" Then
    Exit Function
  ElseIf inFrom < 2 Or inFrom > 36 Then
    Exit Function
  ElseIf inTo < 1 Or inTo > 36 Then
    Exit Function
  End If

  ' Convert to decimal
  inNum = UCase$(inNum)
  k = Len(inNum)
  For i = 1 To Len(inNum)
    k = k - 1
    mChr = Mid$(inNum, i, 1)
    j = 0
    If Asc(mChr) > 64 And Asc(mChr) < 91 Then
      j = Asc(mChr) - 55
    End If
    If j = 0 Then
      If Asc(mChr) < 48 Or Asc(mChr) > 57 Then
        Exit Function
      End If
      j = Val(mChr)
    End If
    If j < 0 Or j > inFrom - 1 Then
      Exit Function
    End If
    mTotal = mTotal + j * (inFrom ^ k)
  Next i
  ' In target base
  Do While mTotal > 0
    tmp = CDbl(inTo)
    mMod = mTotal - (Int(mTotal / tmp) * tmp)
    mTotal = (mTotal - mMod) / inTo
    If mMod >= 10 Then
      mChr = Chr$(mMod + 55)
    Else
      mChr = Right$(str$(mMod), Len(str$(mMod)) - 1)
    End If
    ConvNum = mChr & ConvNum
  Loop
  If ConvNum = "" Then
    ConvNum = "0"
  End If
End Function
