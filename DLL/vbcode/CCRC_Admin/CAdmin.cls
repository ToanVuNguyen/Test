VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CAdmin"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private CCom As New cCommon
Private Const sModName = "CCRC Admin"

Private Const SPONSOR = "1"
Private Const SERVICER = "2"
Private Const AGENCY = "3"

Public Function Get_User_Info(is_Domain As Variant, is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_User_Info " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT U.CCRC_USER_SEQ_ID," & _
                "U.ACTIVE_IND USER_ACTIVE_IND, U.USER_ROLE_STRING," & _
                "BE.BSN_ENTITY_SEQ_ID, BE.BSN_ENTITY_TYPE_CODE, BE.ACTIVE_IND BUSINESS_ENTITY_ACTIVE_IND"
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = os_SQL & " FROM CCRC_USER U, BSN_ENTITY BE"
        os_SQL = os_SQL & " WHERE upper(U.NT_DOMAIN) = '" & is_Domain & "'"
        os_SQL = os_SQL & " AND upper(U.NT_USERID) = '" & is_UserID & "'"
        os_SQL = os_SQL & " AND U.BSN_ENTITY_SEQ_ID = BE.BSN_ENTITY_SEQ_ID"
    Else
        os_SQL = os_SQL & " FROM CCRC_USER U INNER JOIN BSN_ENTITY BE ON U.BSN_ENTITY_SEQ_ID = BE.BSN_ENTITY_SEQ_ID"
        os_SQL = os_SQL & " WHERE U.NT_DOMAIN = '" & is_Domain & "'"
        os_SQL = os_SQL & " AND U.NT_USERID = '" & is_UserID & "'"
    End If

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_User_Info = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    'Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_User_ACL(is_QueryReason As Variant, ii_CCRCUserId As Variant, is_Domain As Variant, is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As String

    Dim CallerID      As String
    Dim sACL          As String
    Dim rs            As ADODB.Recordset
    Dim rsPrograms    As ADODB.Recordset
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_User_ACL " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    'create an error in case user does not belong to a ccrc role
    sACL = "x=x"
    
    os_SQL = "SELECT U.USER_ROLE_STRING, BE.BSN_ENTITY_TYPE_CODE, BE.BSN_ENTITY_SEQ_ID, BELCU.BSN_ENTITY_LOCTN_SEQ_ID"
    os_SQL = os_SQL & " FROM CCRC_USER U, BSN_ENTITY BE,"
    os_SQL = os_SQL & " BSN_ENTITY_LOCTN_CCRC_USER BELCU"
    If ii_CCRCUserId = "" Then
      os_SQL = os_SQL & " WHERE UPPER(U.NT_DOMAIN) = '" & is_Domain & "'"
      os_SQL = os_SQL & " AND UPPER(U.NT_USERID) = '" & is_UserID & "'"
    Else
      os_SQL = os_SQL & " WHERE U.CCRC_USER_SEQ_ID = " & ii_CCRCUserId
    End If
    os_SQL = os_SQL & " AND U.BSN_ENTITY_SEQ_ID = BE.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND U.CCRC_USER_SEQ_ID = BELCU.CCRC_USER_SEQ_ID"
    
    Dim str As String
    str = os_SQL
    'Call CCom.WriteToLog("Method Called")
    'Call CCom.WriteToLog(str)
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set rs = CCom.RunSQLReturnRS(os_SQL)
    If Not rs.EOF Then
      rs.MoveFirst
      If InStr(1, rs.Fields("USER_ROLE_STRING").Value, "35") <> 0 Or _
          InStr(1, rs.Fields("USER_ROLE_STRING").Value, "83") <> 0 Then
        'Owner Or Super User
'        sACL = "'%'"
        sACL = ""
      ElseIf InStr(1, rs.Fields("USER_ROLE_STRING").Value, "84") <> 0 Then
        'Sponsor
          'select referrals from programs sponsored by the sponsor
'            sACL = "%|" & rs.Fields("BSN_ENTITY_SEQ_ID").Value & "|%|%|%'"
          sACL = ""
          os_SQL = "SELECT PROGRAM_SEQ_ID FROM PROGRAM_BSN_ENTITY WHERE BSN_ENTITY_SEQ_ID = " & rs.Fields("BSN_ENTITY_SEQ_ID").Value
          Set rsPrograms = CCom.RunSQLReturnRS(os_SQL)
          If Not rsPrograms.EOF Then
            sACL = sACL & " REFERRAL.PROGRAM_SEQ_ID IN ("
              sACL = sACL & "'" & rsPrograms.Fields("PROGRAM_SEQ_ID").Value & "'"
            Do While Not rsPrograms.EOF
              sACL = sACL & ",'" & rsPrograms.Fields("PROGRAM_SEQ_ID").Value & "'"
              rsPrograms.MoveNext
            Loop
            sACL = sACL & ")"
          End If
          rsPrograms.Close
      ElseIf InStr(1, rs.Fields("USER_ROLE_STRING").Value, "34") <> 0 Then
        'Servicer
          'select referrals serviced by the servicer and locations viewable for user
          sACL = " SERVICER_SEQ_ID = " & rs.Fields("BSN_ENTITY_SEQ_ID").Value
          sACL = sACL & " AND REFERRAL.SERVICER_LOCTN_SEQ_ID IN ("
          sACL = sACL & "'" & rs.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value & "'"
          rs.MoveNext
          Do While Not rs.EOF
            sACL = sACL & ",'" & rs.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value & "'"
            rs.MoveNext
          Loop
          sACL = sACL & ")"
      ElseIf InStr(1, rs.Fields("USER_ROLE_STRING").Value, "85") <> 0 Then
        'Agency
          'select referrals counseled by the agency
          If is_QueryReason = "report" Then
'           sACL = "'%|%|%|%|" & rs.Fields("BSN_ENTITY_SEQ_ID").Value & "'"
            sACL = " REFERRAL.AGENCY_SEQ_ID = " & rs.Fields("BSN_ENTITY_SEQ_ID").Value
          Else
'              sACL = "'%'"
            sACL = ""
          End If
      End If
    End If
    Get_User_ACL = sACL
    os_ReturnCode = 0
  
Exit_Function:
    If rs.State = adStateOpen Then rs.Close
    If Not rsPrograms Is Nothing Then
      If rsPrograms.State = adStateOpen Then rsPrograms.Close
    End If
    Set rs = Nothing
    Set rsPrograms = Nothing
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    'Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    On Error Resume Next
    If rs.State = adStateOpen Then rs.Close
    If rsPrograms.State = adStateOpen Then rsPrograms.Close
    Set rs = Nothing
    Set rsPrograms = Nothing
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Entities(entityType As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Entities " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT BE.*, BET.DSCR"
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = os_SQL & " FROM BSN_ENTITY BE, BSN_ENTITY_TYPE BET"
        os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_TYPE_CODE = " & entityType
        os_SQL = os_SQL & " AND BE.BSN_ENTITY_TYPE_CODE = BET.BSN_ENTITY_TYPE_CODE"
    Else
        os_SQL = os_SQL & " FROM BSN_ENTITY BE"
        os_SQL = os_SQL & " INNER JOIN BSN_ENTITY_TYPE BET ON BE.BSN_ENTITY_TYPE_CODE = BET.BSN_ENTITY_TYPE_CODE"
        os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_TYPE_CODE = " & entityType
    End If
    os_SQL = os_SQL & " ORDER BY BE.BSN_ENTITY_NAME"
    
     'Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Entities = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    'Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Program_Entities(programId As Variant, entityType As Variant, _
    isSelected As Variant, isActive As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Program_Entities " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    If Len(programId) = 0 Then
      'RETURN ALL ENTITIES OF THE SPECIFIED TYPE FOR ALL PROGRAMS
      os_SQL = "SELECT PBE.PROGRAM_SEQ_ID, BE.*"
      os_SQL = os_SQL & " FROM BSN_ENTITY BE, PROGRAM_BSN_ENTITY PBE"
      os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_TYPE_CODE = " & entityType
      os_SQL = os_SQL & " AND BE.ACTIVE_IND = '" & isActive & "'"
      os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID = PBE.BSN_ENTITY_SEQ_ID"
      os_SQL = os_SQL & " ORDER BY PBE.PROGRAM_SEQ_ID, BE.BSN_ENTITY_NAME"
    Else
      'RETURN ALL SELECTED OR NON-SELECTED ENTITIES OF THE SPECIFIED TYPE
      'FOR SPECIFIC PROGRAM
      os_SQL = "SELECT BE.*"
      os_SQL = os_SQL & " FROM BSN_ENTITY BE"
      os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_TYPE_CODE = " & entityType
      os_SQL = os_SQL & " AND BE.ACTIVE_IND = '" & isActive & "'"
      If Not IsNull(isSelected) Then
        If Not isSelected Then
          os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID NOT IN (SELECT BSN_ENTITY_SEQ_ID FROM PROGRAM_BSN_ENTITY WHERE PROGRAM_SEQ_ID = " & programId & ")"
        Else
          os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID IN (SELECT BSN_ENTITY_SEQ_ID FROM PROGRAM_BSN_ENTITY WHERE PROGRAM_SEQ_ID = " & programId & ")"
        End If
      End If
      os_SQL = os_SQL & " ORDER BY BE.BSN_ENTITY_NAME"
    End If

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Program_Entities = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Entity(ii_EntityId As Variant, is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Entity " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = "SELECT * FROM BSN_ENTITY BE WHERE BE.BSN_ENTITY_SEQ_ID = " & ii_EntityId
    Else
        os_SQL = "SELECT * FROM BSN_ENTITY BE WHERE BE.BSN_ENTITY_SEQ_ID = " & ii_EntityId
    End If

    'Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Entity = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    'Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Entity_Add(is_EntityName As Variant, ii_EntityType As Variant, _
  is_contactName As Variant, is_contactEmail As Variant, is_phone As Variant, is_fax As Variant, _
  is_hasContractInd As Variant, is_DisplayInd As Variant, is_AgencyType As Variant, is_ffsInd As Variant, _
  is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID          As String
  Dim RS1               As ADODB.Recordset
  Dim cmd               As ADODB.Command
  Dim os_EntitySeqId    As Long
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Entity_Add " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields  As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans

  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = "INSERT INTO BSN_ENTITY("
  Case Else
    os_SQL = "INSERT INTO BSN_ENTITY(BSN_ENTITY_SEQ_ID,"
  End Select

  os_SQL = os_SQL & " BSN_ENTITY_NAME, BSN_ENTITY_TYPE_CODE, ACTIVE_IND, CONTACT_NAME, CONTACT_EMAIL, PHONE, FAX, HAS_CONTRACT_IND, DISPLAY_IND, AGENCY_TYPE_CODE, FFS_IND"
  os_SQL = os_SQL & MyInsertAuditFields
  
  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = os_SQL & ") VALUES ("
  Case Else
    os_SQL = os_SQL & ") VALUES (BSN_ENTITY_S01.NEXTVAL,"
  End Select
  
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_EntityName) & "',"
  os_SQL = os_SQL & ii_EntityType & ","
  os_SQL = os_SQL & "'Y',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_contactName) & "',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_contactEmail) & "',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_phone) & "',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_fax) & "',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_hasContractInd) & "',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_DisplayInd) & "',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_AgencyType) & "'"
  os_SQL = os_SQL & ",'" & CCom.SQLScrub(is_ffsInd) & "'"
  os_SQL = os_SQL & "," & MyInsertAudit & ")"
  'Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
'  cmd.ActiveConnection.CommitTrans
  
  ' get key of last inserted row
  If RS1.State = adStateOpen Then RS1.Close
  os_SQL = "SELECT MAX(BSN_ENTITY_SEQ_ID) FROM BSN_ENTITY WHERE" & MyQueryAudit
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
  If Not RS1.EOF Then
      os_EntitySeqId = RS1(0)
      os_ReturnCode = 0
  Else
      os_Message = "Referral creation failed!"
      os_ReturnCode = -1
  End If
  RS1.Close
  Set RS1 = Nothing
  
  Call Entity_Location_Add(os_EntitySeqId, "Default", "Y", "", is_UserID, os_SQL, os_ReturnCode, os_Message)
    
Exit_Function:
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Entity_Add = os_EntitySeqId
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Entity_Add = -1
  On Error Resume Next
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Entity_Update(ii_EntityId As Variant, is_EntityName As Variant, ii_EntityType As Variant, _
  is_contactName As Variant, is_contactEmail As Variant, is_phone As Variant, is_fax As Variant, _
  is_activeInd As Variant, is_hasContractInd As Variant, is_DisplayInd, is_AgencyType, is_ffsInd, _
  is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":Entity_Update " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
    
  os_SQL = "UPDATE BSN_ENTITY SET "
  os_SQL = os_SQL & " BSN_ENTITY_NAME = '" & CCom.SQLScrub(is_EntityName) & "',"
  os_SQL = os_SQL & " BSN_ENTITY_TYPE_CODE = " & ii_EntityType & ","
  os_SQL = os_SQL & " ACTIVE_IND = '" & CCom.SQLScrub(is_activeInd) & "',"
  os_SQL = os_SQL & " CONTACT_NAME = '" & CCom.SQLScrub(is_contactName) & "',"
  os_SQL = os_SQL & " CONTACT_EMAIL = '" & CCom.SQLScrub(is_contactEmail) & "',"
  os_SQL = os_SQL & " PHONE = '" & CCom.SQLScrub(is_phone) & "',"
  os_SQL = os_SQL & " FAX = '" & CCom.SQLScrub(is_fax) & "',"
  os_SQL = os_SQL & " HAS_CONTRACT_IND = '" & CCom.SQLScrub(is_hasContractInd) & "',"
  os_SQL = os_SQL & " DISPLAY_IND = '" & CCom.SQLScrub(is_DisplayInd) & "',"
  os_SQL = os_SQL & " AGENCY_TYPE_CODE = '" & CCom.SQLScrub(is_AgencyType) & "'"
  os_SQL = os_SQL & ", FFS_IND = '" & CCom.SQLScrub(is_ffsInd) & "'"
  os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
  os_SQL = os_SQL & " WHERE BSN_ENTITY_SEQ_ID = " & ii_EntityId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  Entity_Update = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Entity_Update = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Entity_Delete(ii_EntityId As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As ADODB.Command
  Dim conn         As ADODB.Connection
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Entity_Delete " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New ADODB.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  ' Begin transaction
'  cmd.ActiveConnection.BeginTrans
  os_SQL = "DELETE FROM BSN_ENTITY WHERE BSN_ENTITY_SEQ_ID = " & ii_EntityId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  ' End transaction
'  cmd.ActiveConnection.CommitTrans
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  Entity_Delete = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Entity_Delete = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function User_Add(is_AddedDomain As Variant, is_AddedUserID, _
  ii_EntityId As Variant, is_userRoles As Variant, _
  is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID          As String
  Dim cmd               As ADODB.Command
  Dim os_EntitySeqId    As Long
  Dim arParms()         As String
  Dim arParm()          As String
  Dim iParm             As Integer
  Dim RS1               As ADODB.Recordset
  
  On Error GoTo errorHandler
  CallerID = sModName & ":User_Add " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields  As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans
  
  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = "INSERT INTO CCRC_USER("
  Case Else
    os_SQL = "INSERT INTO CCRC_USER(CCRC_USER_SEQ_ID,"
  End Select
    
  os_SQL = os_SQL & " BSN_ENTITY_SEQ_ID, NT_DOMAIN, NT_USERID, USER_ROLE_STRING, ACTIVE_IND"
  os_SQL = os_SQL & MyInsertAuditFields
  
  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = os_SQL & ") VALUES ("
  Case Else
    os_SQL = os_SQL & ") VALUES (CCRC_USER_S01.NEXTVAL,"
  End Select
  
  os_SQL = os_SQL & ii_EntityId & ",'" & is_AddedDomain & "','" & is_AddedUserID & "','" & is_userRoles & "','Y'"
  os_SQL = os_SQL & "," & MyInsertAudit & ")"
  Dim str As String
  str = os_SQL
  'Call CCom.WriteToLog(str)
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  Set RS1 = New ADODB.Recordset
  os_SQL = "SELECT MAX(CCRC_USER_SEQ_ID) FROM CCRC_USER WHERE" & MyQueryAudit
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
  If Not RS1.EOF Then
      os_EntitySeqId = RS1(0)
      os_ReturnCode = 0
  Else
      os_Message = "User creation failed!"
      os_ReturnCode = -1
  End If
  RS1.Close
  Set RS1 = Nothing
  str = os_EntitySeqId
  'Call CCom.WriteToLog(str)
  os_ReturnCode = 0

Exit_Function:
  If Not (cmd Is Nothing) Then
'    cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  User_Add = os_EntitySeqId
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  User_Add = -1
  On Error Resume Next
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function User_Update(is_ntDomain As Variant, is_ntUserId As Variant, is_activeInd As Variant, ii_LocationId As Variant, _
  is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) _
  As Integer

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":User_Update " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  os_SQL = "UPDATE CCRC_USER SET "
  os_SQL = os_SQL & "ACTIVE_IND = '" & CCom.SQLScrub(is_activeInd) & "',"
  os_SQL = os_SQL & "BSN_ENTITY_LOCTN_SEQ_ID = " & CCom.SQLScrub(ii_LocationId)
  os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
  os_SQL = os_SQL & " WHERE NT_DOMAIN = '" & is_ntDomain & "'"
  os_SQL = os_SQL & " AND NT_USERID = '" & is_ntUserId & "'"
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  User_Update = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  User_Update = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function User_Update_Status(ii_ProfileId As Variant, is_Status As Variant, _
    is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":User_Update_Status " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
    
  os_SQL = "UPDATE CCRC_USER SET "
  os_SQL = os_SQL & "ACTIVE_IND = '" & is_Status & "'"
  os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
  os_SQL = os_SQL & " WHERE CCRC_USER_SEQ_ID = " & ii_ProfileId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  User_Update_Status = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  User_Update_Status = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function User_Delete(ii_ProfileId As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As ADODB.Command
  Dim conn         As ADODB.Connection
  
  On Error GoTo errorHandler
  CallerID = sModName & ":User_Delete " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New ADODB.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  ' Begin transaction
'  cmd.ActiveConnection.BeginTrans
  
  
  os_SQL = "DELETE FROM CCRC_USER WHERE CCRC_USER_SEQ_ID = " & ii_ProfileId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  ' End transaction
'  cmd.ActiveConnection.CommitTrans
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  User_Delete = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  User_Delete = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Users(ii_EntityId, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Users " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT P.*, U.*"
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = os_SQL & " FROM PROFILE P, CCRC_USER U"
        os_SQL = os_SQL & " WHERE U.BSN_ENTITY_SEQ_ID = " & ii_EntityId
        os_SQL = os_SQL & " AND U.NT_DOMAIN = P.NT_DOMAIN"
        os_SQL = os_SQL & " AND U.NT_USERID = P.NT_USERID"
    Else
        os_SQL = os_SQL & " FROM [CCRC_USER] U"
        os_SQL = os_SQL & " INNER JOIN PROFILE P ON U.NT_DOMAIN = P.NT_DOMAIN"
        os_SQL = os_SQL & " AND U.NT_USERID = P.NT_USERID"
        os_SQL = os_SQL & " WHERE U.BSN_ENTITY_SEQ_ID = " & ii_EntityId
    End If
    os_SQL = os_SQL & " ORDER BY P.LAST_NAME, P.FIRST_NAME"

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Users = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Entity_Type_Users(is_Entity_Type As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Entity_Type_Users " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT P.*, U.*, BE.BSN_ENTITY_NAME, BE.PHONE, BE.FAX"
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = os_SQL & " FROM PROFILE P, CCRC_USER U, BSN_ENTITY BE"
        os_SQL = os_SQL & " WHERE U.BSN_ENTITY_SEQ_ID = BE.BSN_ENTITY_SEQ_ID"
        os_SQL = os_SQL & " AND BE.BSN_ENTITY_TYPE_CODE = " & is_Entity_Type
        os_SQL = os_SQL & " AND U.NT_DOMAIN = P.NT_DOMAIN"
        os_SQL = os_SQL & " AND U.NT_USERID = P.NT_USERID"
        os_SQL = os_SQL & " AND U.ACTIVE_IND = 'Y'"
    Else
        os_SQL = os_SQL & " FROM [CCRC_USER] U"
        os_SQL = os_SQL & " INNER JOIN PROFILE P ON U.NT_DOMAIN = P.NT_DOMAIN"
        os_SQL = os_SQL & " AND U.NT_USERID = P.NT_USERID"
        os_SQL = os_SQL & " INNER JOIN BSN_ENTITY BE ON U.BSN_ENTITY_SEQ_ID = BE.BSN_ENTITY_SEQ_ID"
        os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_TYPE_CODE = " & is_Entity_Type
        os_SQL = os_SQL & " AND U.ACTIVE_IND = 'Y'"
    End If
    os_SQL = os_SQL & " ORDER BY BE.BSN_ENTITY_NAME, P.LAST_NAME, P.FIRST_NAME"

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Entity_Type_Users = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function GetCounselorDirectory(is_AgencyType As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":GetCounselorDirectory " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT P.*, U.*, BE.BSN_ENTITY_NAME, BE.PHONE, BE.FAX, BE.AGENCY_TYPE_CODE"
    os_SQL = os_SQL & " FROM PROFILE P, CCRC_USER U, BSN_ENTITY BE"
    os_SQL = os_SQL & " WHERE U.BSN_ENTITY_SEQ_ID = BE.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND BE.BSN_ENTITY_TYPE_CODE = '3'"
    os_SQL = os_SQL & " AND BE.AGENCY_TYPE_CODE = '" & is_AgencyType & "'"
    os_SQL = os_SQL & " AND U.NT_DOMAIN = P.NT_DOMAIN"
    os_SQL = os_SQL & " AND U.NT_USERID = P.NT_USERID"
    os_SQL = os_SQL & " AND U.ACTIVE_IND = 'Y'"
    os_SQL = os_SQL & " ORDER BY BE.BSN_ENTITY_NAME, P.LAST_NAME, P.FIRST_NAME"

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set GetCounselorDirectory = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_User(is_ntDomain As Variant, is_ntUserId As Variant, _
    is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_User " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT P.*, U.*"
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = os_SQL & " FROM CCRC_USER U, PROFILE P"
        os_SQL = os_SQL & " WHERE P.NT_DOMAIN = '" & is_ntDomain & "'"
        os_SQL = os_SQL & " AND P.NT_USERID = '" & is_ntUserId & "'"
        os_SQL = os_SQL & " AND P.NT_DOMAIN = U.NT_DOMAIN"
        os_SQL = os_SQL & " AND P.NT_USERID = U.NT_USERID"
    Else
        os_SQL = os_SQL & " FROM [PROFILE] P"
        os_SQL = os_SQL & " INNER JOIN CCRC_USER U ON P.NT_DOMAIN = U.NT_DOMAIN AND P.NT_USERID = U.NT_USERID"
        os_SQL = os_SQL & " WHERE P.NT_DOMAIN = '" & is_ntDomain & "'"
        os_SQL = os_SQL & " AND P.NT_USERID = '" & is_ntUserId & "'"
    End If
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_User = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Agencies(is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Agencies " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT BSN_ENTITY_SEQ_ID, BSN_ENTITY_NAME FROM BSN_ENTITY WHERE BSN_ENTITY_TYPE_CODE='" & AGENCY & "' ORDER BY BSN_ENTITY_NAME"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Agencies = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Servicers(ii_ReferralId As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Servicers " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    'make sure to get the one the referral is set to
    
    os_SQL = "SELECT BSN_ENTITY.*"
    os_SQL = os_SQL & " FROM BSN_ENTITY, REFERRAL"
    os_SQL = os_SQL & " WHERE BSN_ENTITY_TYPE_CODE='" & SERVICER & "'"
    os_SQL = os_SQL & " AND BSN_ENTITY_SEQ_ID = SERVICER_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL_SEQ_ID = " & ii_ReferralId
    os_SQL = os_SQL & " UNION "
    os_SQL = os_SQL & " SELECT *"
    os_SQL = os_SQL & " FROM BSN_ENTITY"
    os_SQL = os_SQL & " WHERE BSN_ENTITY_TYPE_CODE='" & SERVICER & "'"
    os_SQL = os_SQL & " AND ACTIVE_IND = 'Y'"
    If ii_ReferralId = 0 Then
        os_SQL = os_SQL & " AND DISPLAY_IND = 'Y'"
    End If
    os_SQL = os_SQL & " ORDER BY 2" 'BSN_ENTITY_NAME
        Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Servicers = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Programs(is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Programs " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT P.*"
    os_SQL = os_SQL & " FROM PROGRAM P"
    os_SQL = os_SQL & " ORDER BY P.END_DATE"

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Programs = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Program(ii_ProgramId As Variant, _
    is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Program " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
        
    os_SQL = "SELECT * FROM PROGRAM WHERE PROGRAM_SEQ_ID = " & ii_ProgramId

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Program = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Program_Add(is_ProgramName As Variant, is_StartDate, _
  is_EndDate As Variant, is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID          As String
  Dim RS1               As ADODB.Recordset
  Dim cmd               As ADODB.Command
  Dim arParms()         As String
  Dim arParm()          As String
  Dim iParm             As Integer
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Program_Add " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields  As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans
  
  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = "INSERT INTO PROGRAM("
  Case Else
    os_SQL = "INSERT INTO PROGRAM(PROGRAM_SEQ_ID, "
  End Select
  
  os_SQL = os_SQL & "PROGRAM_NAME, START_DATE, END_DATE" & MyInsertAuditFields
  
  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = os_SQL & ") VALUES ("
  Case Else
    os_SQL = os_SQL & ") VALUES (PROGRAM_S01.NEXTVAL,"
  End Select
  
  os_SQL = os_SQL & "'" & CCom.Scrub(is_ProgramName)
  os_SQL = os_SQL & "'," & CCom.ScrubDate(is_StartDate)
  os_SQL = os_SQL & "," & CCom.ScrubDate(is_EndDate)
  os_SQL = os_SQL & "," & MyInsertAudit & ")"
  
  Dim str As String
  str = os_SQL
  
  'Call CCom.WriteToLog(str)
  
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  os_ReturnCode = 0

Exit_Function:
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
'    cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Program_Add = 0
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Program_Add = -1
  On Error Resume Next
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Program_Entity_Add(ii_ProgramId As Variant, ii_EntityId, _
  is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID          As String
  Dim cmd               As ADODB.Command
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Program_Entity_Add " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyInsertAuditFields  As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans
  
  os_SQL = "INSERT INTO PROGRAM_BSN_ENTITY(PROGRAM_SEQ_ID, BSN_ENTITY_SEQ_ID"
  os_SQL = os_SQL & MyInsertAuditFields & ") VALUES ("
  os_SQL = os_SQL & CCom.Scrub(ii_ProgramId)
  os_SQL = os_SQL & "," & CCom.Scrub(ii_EntityId)
  os_SQL = os_SQL & "," & MyInsertAudit & ")"
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  os_ReturnCode = 0

Exit_Function:
  If Not (cmd Is Nothing) Then
'    cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Program_Entity_Add = 0
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Program_Entity_Add = -1
  On Error Resume Next
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Program_Update(ii_ProgramId As Variant, is_ProgramName As Variant, is_StartDate, _
  is_EndDate As Variant, is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":Program_Update " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
    
  os_SQL = "UPDATE PROGRAM SET PROGRAM_NAME = '" & CCom.Scrub(is_ProgramName) & "'"
  os_SQL = os_SQL & ", START_DATE = " & CCom.ScrubDate(is_StartDate)
  os_SQL = os_SQL & ", END_DATE = " & CCom.ScrubDate(is_EndDate)
  os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
  os_SQL = os_SQL & " WHERE PROGRAM_SEQ_ID = " & ii_ProgramId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  Program_Update = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Program_Update = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Program_Delete(ii_ProgramId As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As ADODB.Command
  Dim conn         As ADODB.Connection
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Program_Delete " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New ADODB.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  ' Begin transaction
'  cmd.ActiveConnection.BeginTrans
  os_SQL = "DELETE FROM PROGRAM WHERE PROGRAM_SEQ_ID = " & ii_ProgramId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  ' End transaction
'  cmd.ActiveConnection.CommitTrans
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  Program_Delete = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Program_Delete = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Program_Entities_Process(is_ProgramId As Variant, is_EntityType, _
  is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant, ParamArray arArgs()) As Long

  Dim CallerID          As String
  Dim cmd               As ADODB.Command
  Dim arParms()         As String
  Dim iParm             As Integer
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Program_Entities_Process " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields  As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans
  
  os_SQL = "DELETE FROM PROGRAM_BSN_ENTITY"
  os_SQL = os_SQL & " WHERE PROGRAM_SEQ_ID = " & is_ProgramId
  os_SQL = os_SQL & " AND BSN_ENTITY_SEQ_ID IN ("
  os_SQL = os_SQL & " SELECT BSN_ENTITY_SEQ_ID FROM BSN_ENTITY"
  os_SQL = os_SQL & " WHERE BSN_ENTITY_TYPE_CODE = " & is_EntityType
  os_SQL = os_SQL & ")"
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
    
  arParms = Split(arArgs(0), "|")
  For iParm = 0 To UBound(arParms)
    os_SQL = ""
    If arParms(iParm) <> "" Then
        os_SQL = "INSERT INTO PROGRAM_BSN_ENTITY (PROGRAM_SEQ_ID, BSN_ENTITY_SEQ_ID"
        os_SQL = os_SQL & MyInsertAuditFields & ") VALUES ("
        os_SQL = os_SQL & is_ProgramId & ", " & arParms(iParm)
        os_SQL = os_SQL & "," & MyInsertAudit & ")"
        Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
        cmd.CommandText = os_SQL
        cmd.Execute , , ADODB.adExecuteNoRecords
    End If
  Next
  
  os_ReturnCode = 0

Exit_Function:
  If Not (cmd Is Nothing) Then
'    cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Program_Entities_Process = 0
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Program_Entities_Process = -1
  On Error Resume Next
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function GetICEProfileEditorServer() As String
    Dim Reg As New cReg
    Reg.OpenSoftwareKey ("RFC\CCRC")
    GetICEProfileEditorServer = Reg.QueryValueStr("ICEProfileEditor")
End Function

Public Function Get_User_Data_Rights(profileId As Variant, entityId As Variant, _
    isSelected As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_User_Data_Rights " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT BEL.*"
    os_SQL = os_SQL & " FROM BSN_ENTITY_LOCTN BEL"
    os_SQL = os_SQL & " WHERE BEL.BSN_ENTITY_SEQ_ID = " & entityId
    os_SQL = os_SQL & " AND BEL.ACTIVE_IND = 'Y'"
    If Not IsNull(isSelected) Then
      If Not isSelected Then
        os_SQL = os_SQL & " AND BEL.BSN_ENTITY_LOCTN_SEQ_ID NOT IN (SELECT BSN_ENTITY_LOCTN_SEQ_ID FROM BSN_ENTITY_LOCTN_CCRC_USER WHERE CCRC_USER_SEQ_ID = " & profileId & ")"
      Else
        os_SQL = os_SQL & " AND BEL.BSN_ENTITY_LOCTN_SEQ_ID IN (SELECT BSN_ENTITY_LOCTN_SEQ_ID FROM BSN_ENTITY_LOCTN_CCRC_USER WHERE CCRC_USER_SEQ_ID = " & profileId & ")"
      End If
    End If
    os_SQL = os_SQL & " ORDER BY BEL.LOCTN_NAME"

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_User_Data_Rights = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function User_Data_Rights_Process(is_ProfileId As Variant, is_EntityId, _
  is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant, ParamArray arArgs()) As Long

  Dim CallerID          As String
  Dim cmd               As ADODB.Command
  Dim arParms()         As String
  Dim iParm             As Integer
  
  On Error GoTo errorHandler
  CallerID = sModName & ":User_Data_Rights_Process " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields  As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans
  
  os_SQL = "DELETE FROM BSN_ENTITY_LOCTN_CCRC_USER"
  os_SQL = os_SQL & " WHERE CCRC_USER_SEQ_ID = " & is_ProfileId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
    
  arParms = Split(arArgs(0), "|")
  For iParm = 0 To UBound(arParms)
    os_SQL = ""
    If arParms(iParm) <> "" Then
        os_SQL = "INSERT INTO BSN_ENTITY_LOCTN_CCRC_USER(BSN_ENTITY_LOCTN_SEQ_ID, CCRC_USER_SEQ_ID" & MyInsertAuditFields & " ) VALUES ("
        os_SQL = os_SQL & arParms(iParm) & ", " & is_ProfileId
        os_SQL = os_SQL & "," & MyInsertAudit & ")"
        Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
        cmd.CommandText = os_SQL
        cmd.Execute , , ADODB.adExecuteNoRecords
    End If
  Next
  
  os_ReturnCode = 0

Exit_Function:
  If Not (cmd Is Nothing) Then
'    cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  User_Data_Rights_Process = 0
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  User_Data_Rights_Process = -1
  On Error Resume Next
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Entity_Locations(ii_EntityId As Variant, is_EntityType As Variant, _
    is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    Dim sWhere        As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Entity_Locations " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    sWhere = "WHERE"
    If ii_EntityId <> "" Then
      sWhere = sWhere & " BSN_ENTITY_LOCTN.BSN_ENTITY_SEQ_ID = " & ii_EntityId
      If is_EntityType <> "" Then
        sWhere = sWhere & " AND BSN_ENTITY.BSN_ENTITY_TYPE_CODE = '" & is_EntityType & "'"
      End If
    ElseIf is_EntityType <> "" Then
      sWhere = sWhere & " BSN_ENTITY.BSN_ENTITY_TYPE_CODE = '" & is_EntityType & "'"
    End If
    
    os_SQL = "SELECT BSN_ENTITY.BSN_ENTITY_NAME, BSN_ENTITY_LOCTN.*" & _
      " FROM BSN_ENTITY, BSN_ENTITY_LOCTN " & sWhere & _
      " AND BSN_ENTITY.BSN_ENTITY_SEQ_ID = BSN_ENTITY_LOCTN.BSN_ENTITY_SEQ_ID" & _
      " AND BSN_ENTITY_LOCTN.ACTIVE_IND = 'Y'" & _
      " ORDER BY BSN_ENTITY_LOCTN.LOCTN_NAME"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Entity_Locations = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Entity_Location(ii_LocationId As Variant, is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Entity_Location " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT * FROM BSN_ENTITY_LOCTN WHERE BSN_ENTITY_LOCTN_SEQ_ID = " & ii_LocationId

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Entity_Location = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Entity_Location_Add(ii_EntityId As Variant, is_locationName As Variant, is_activeInd As Variant, is_phone As Variant, _
  is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID          As String
  Dim RS1               As ADODB.Recordset
  Dim cmd               As ADODB.Command
  Dim os_EntitySeqId    As Long
  Dim arParms()         As String
  Dim arParm()          As String
  Dim iParm             As Integer
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Entity_Location_Add " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields  As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans

  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = "INSERT INTO BSN_ENTITY_LOCTN("
  Case Else
    os_SQL = "INSERT INTO BSN_ENTITY_LOCTN(BSN_ENTITY_LOCTN_SEQ_ID,"
  End Select
  
  os_SQL = os_SQL & " BSN_ENTITY_SEQ_ID, LOCTN_NAME, ACTIVE_IND, PHONE"
  os_SQL = os_SQL & MyInsertAuditFields
  
  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = os_SQL & ") VALUES ("
  Case Else
    os_SQL = os_SQL & ") VALUES (BSN_ENTITY_LOCTN_S01.NEXTVAL,"
  End Select
  
  os_SQL = os_SQL & ii_EntityId & ","
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_locationName) & "',"
  os_SQL = os_SQL & "'Y',"
  os_SQL = os_SQL & "'" & CCom.SQLScrub(is_phone) & "'"
  os_SQL = os_SQL & "," & MyInsertAudit & ")"
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  ' get key of last inserted row
  If RS1.State = adStateOpen Then RS1.Close
  os_SQL = "SELECT MAX(BSN_ENTITY_LOCTN_SEQ_ID) FROM BSN_ENTITY_LOCTN WHERE" & MyQueryAudit
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
  If Not RS1.EOF Then
      os_EntitySeqId = RS1(0)
      os_ReturnCode = 0
  Else
      os_Message = "Referral creation failed!"
      os_ReturnCode = -1
  End If
  RS1.Close
  Set RS1 = Nothing
  
  os_ReturnCode = 0

Exit_Function:
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
'    cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Entity_Location_Add = os_EntitySeqId
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Entity_Location_Add = -1
  On Error Resume Next
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Entity_Location_Update(ii_LocationId As Variant, is_locationName As Variant, is_activeInd As Variant, is_phone As Variant, _
  is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":Entity_Location_Update " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
    
  os_SQL = "UPDATE BSN_ENTITY_LOCTN SET"
  os_SQL = os_SQL & " LOCTN_NAME = '" & CCom.SQLScrub(is_locationName) & "'"
  os_SQL = os_SQL & ", PHONE = '" & CCom.SQLScrub(is_phone) & "'"
  os_SQL = os_SQL & ", ACTIVE_IND = '" & CCom.SQLScrub(is_activeInd) & "'"
  os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
  os_SQL = os_SQL & " WHERE BSN_ENTITY_LOCTN_SEQ_ID = " & ii_LocationId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  Entity_Location_Update = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Entity_Location_Update = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Entity_Location_Delete(ii_LocationId As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As ADODB.Command
  Dim conn         As ADODB.Connection
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Entity_Location_Delete " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New ADODB.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  ' Begin transaction
'  cmd.ActiveConnection.BeginTrans
  os_SQL = "DELETE FROM BSN_ENTITY_LOCTN WHERE BSN_ENTITY_LOCTN_SEQ_ID = " & ii_LocationId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , ADODB.adExecuteNoRecords
  
  ' End transaction
'  cmd.ActiveConnection.CommitTrans
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  Entity_Location_Delete = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Entity_Location_Delete = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
'  cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_User_Programs(ii_EntityId As Variant, is_UserID As Variant, _
    is_activeInd As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    Dim sACL          As String
    Dim rs            As ADODB.Recordset
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_User_Programs " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT P.*"
    os_SQL = os_SQL & " FROM BSN_ENTITY BE, PROGRAM_BSN_ENTITY PBE, PROGRAM P"
    os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_SEQ_ID = " & ii_EntityId
    os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID = PBE.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND PBE.PROGRAM_SEQ_ID = P.PROGRAM_SEQ_ID"
    If is_activeInd = "Y" Then
      os_SQL = os_SQL & " AND (P.END_DATE IS NULL OR P.END_DATE >= " & CCom.ScrubDate(Date) & ")"
    End If
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_User_Programs = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Programs_And_Servicers_And_Locations(ii_ReferralId As Variant, _
    is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    Dim sACL          As String
    Dim rs            As ADODB.Recordset
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Programs_And_Servicers_And_Locations " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT P.PROGRAM_SEQ_ID, P.PROGRAM_NAME,"
    os_SQL = os_SQL & " BE.BSN_ENTITY_SEQ_ID, BE.BSN_ENTITY_NAME,"
    os_SQL = os_SQL & " BE.CONTACT_NAME, BE.CONTACT_EMAIL, BE.PHONE, BE.FAX,"
    os_SQL = os_SQL & " BEL.BSN_ENTITY_LOCTN_SEQ_ID, BEL.LOCTN_NAME"
    os_SQL = os_SQL & " FROM BSN_ENTITY_LOCTN BEL, BSN_ENTITY BE, PROGRAM_BSN_ENTITY PBE, PROGRAM P"
    os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_TYPE_CODE = '" & SERVICER & "'"
    If ii_ReferralId = 0 Then
        os_SQL = os_SQL & " AND BE.DISPLAY_IND = 'Y'"
    End If
    os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID = BEL.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID = PBE.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND PBE.PROGRAM_SEQ_ID = P.PROGRAM_SEQ_ID"
    os_SQL = os_SQL & " ORDER BY P.PROGRAM_NAME, BE.BSN_ENTITY_NAME, BEL.LOCTN_NAME"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Programs_And_Servicers_And_Locations = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Admin_Items_I_Can_See(ii_EntityId As Variant, _
    is_userRoles As Variant, rsPrograms As Variant, rsAgencies As Variant, _
    rsServicers As Variant, rsLocations As Variant, _
    is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    Dim sACL          As String
    Dim rs            As ADODB.Recordset
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Programs_And_Servicers_And_Locations " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    '35 & 85 = owner, super user
    If InStr(1, is_userRoles, "35") > 0 Or InStr(1, is_userRoles, "83") > 0 Then
      Set rsPrograms = Get_Programs(is_UserID, os_SQL, os_ReturnCode, os_Message)
      Set rsAgencies = Get_Agencies(is_UserID, os_SQL, os_ReturnCode, os_Message)
      Set rsServicers = Get_Servicers(1, is_UserID, os_SQL, os_ReturnCode, os_Message)
      Set rsLocations = Get_Entity_Locations("", SERVICER, is_UserID, os_SQL, os_ReturnCode, os_Message)
    Else
      Set rsPrograms = Get_User_Programs(ii_EntityId, is_UserID, "Y", os_SQL, os_ReturnCode, os_Message)
    End If
    If InStr(1, is_userRoles, "84") > 0 Then 'sponsor
      Set rsAgencies = Get_Agencies(is_UserID, os_SQL, os_ReturnCode, os_Message)
      Set rsServicers = Get_Servicers(0, is_UserID, os_SQL, os_ReturnCode, os_Message)
      Set rsLocations = Get_Entity_Locations("", SERVICER, is_UserID, os_SQL, os_ReturnCode, os_Message)
    ElseIf InStr(1, is_userRoles, "34") > 0 Then 'servicer
    ElseIf InStr(1, is_userRoles, "85") > 0 Then 'agency counselor
    End If
    'Programs
    
    os_SQL = "SELECT P.PROGRAM_SEQ_ID, P.PROGRAM_NAME, BE.BSN_ENTITY_SEQ_ID, BE.BSN_ENTITY_NAME, BEL.BSN_ENTITY_LOCTN_SEQ_ID, BEL.LOCTN_NAME"
    os_SQL = os_SQL & " FROM BSN_ENTITY_LOCTN BEL, BSN_ENTITY BE, PROGRAM_BSN_ENTITY PBE, PROGRAM P"
    os_SQL = os_SQL & " WHERE BE.BSN_ENTITY_TYPE_CODE = '" & SERVICER & "'"
    os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID = BEL.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND BE.BSN_ENTITY_SEQ_ID = PBE.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND PBE.PROGRAM_SEQ_ID = P.PROGRAM_SEQ_ID"
    os_SQL = os_SQL & " ORDER BY P.PROGRAM_NAME, BE.BSN_ENTITY_NAME, BEL.LOCTN_NAME"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
'    Set Get_Programs_And_Servicers_And_Locations = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Entity_Type(is_EntityType As Variant, is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As ADODB.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Entity_Type " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT * FROM BSN_ENTITY_TYPE WHERE BSN_ENTITY_TYPE_CODE = '" & is_EntityType & "'"

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Entity_Type = CCom.RunSQLReturnRS(os_SQL)
    os_ReturnCode = 0
  
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
    CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
End Function


