VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 1  'NoTransaction
END
Attribute VB_Name = "CMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private CCom As New cCommon
Private Const sModName = "CCRC Main"

Public Function Get_Referral(ii_LoanId As Variant, ii_ReferralId As Variant, is_Zip As Variant, _
    ii_CCRCUserId As Variant, is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    Dim CallerID    As String
    Dim RS1          As adodb.Recordset
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Referral " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
'Set RS1 = New ADODB.Recordset
'RS1.Fields.Append "Test", adVarChar
'RS1.AddNew
'RS1!test = "Success2"
'Set Get_Referral = RS1
'Exit Function

    Dim objAdmin As CAdmin
    Dim sACL As String
    Set objAdmin = New CAdmin
    sACL = objAdmin.Get_User_ACL("search", ii_CCRCUserId, "", "", os_SQL, os_ReturnCode, os_Message)
    Set objAdmin = Nothing
    
    'see if a referral already exists
    os_SQL = CCRC_Referral_SQL(ii_LoanId, ii_ReferralId, is_Zip, sACL)
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set RS1 = CCom.RunSQLReturnRS(os_SQL)
    
    'PB: 6/10/2008 : Need not call Dataware house
    'If RS1.EOF Or RS1.BOF Then
      'loan does not exist in CCRC database
      'see if it is in the datawarehouse
     ' RS1.Close
      'os_SQL = DW_Referral_SQL(ii_LoanId, is_Zip)
      'Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
      'Set RS1 = CCom.RunSQLReturnRS(os_SQL)
    'End If
        
''''    If (RS1.EOF Or RS1.BOF) And ii_LoanId <> "" Then
''''      ' a loan was not found so this is a new referral that is not an RFC loan
''''      RS1.Close
''''      os_SQL = "SELECT '" & ii_LoanId & "' LOAN_ID, 'N' INTERNAL_IND"
''''      If CCom.DBEngine = "ORACLE" Then
''''        os_SQL = os_SQL & " FROM DUAL"
''''      End If
''''      Set RS1 = CCom.RunSQLReturnRS(os_SQL)
''''    End If
    
    Set Get_Referral = RS1
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

Private Function GetServicerDefaultLocation(ByVal ServicerId As Variant) As String
    Dim cmd As adodb.Command
    Dim RS1 As adodb.Recordset
    Dim os_SQL As String
    Dim vData As Variant
    Dim CallerID        As String
    
    On Error GoTo errorHandler

    CallerID = sModName & ":GetServicerDefaultLocation "
    
    Call CCom.CtxSetAbort
    #If USING_MTX Then
        Set cmd = CCom.CtxCreateObject("ADODB.Command")
    #Else
        Set cmd = New adodb.Command
    #End If

    Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
    os_SQL = "SELECT BSN_ENTITY_LOCTN_SEQ_ID FROM BSN_ENTITY_LOCTN WHERE BSN_ENTITY_SEQ_ID = " + CStr(ServicerId) + " AND UPPER(LOCTN_NAME) = 'DEFAULT'"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    cmd.ActiveConnection = CCom.GetConn
    cmd.CommandType = adCmdText
    cmd.CommandText = os_SQL
    RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
    If RS1.BOF Or RS1.EOF Then
        Call CCom.WriteIfDebug(CallerID & " : Default location not found")
        RS1.Close
        os_SQL = "SELECT MIN(BSN_ENTITY_LOCTN_SEQ_ID) FROM BSN_ENTITY_LOCTN WHERE BSN_ENTITY_SEQ_ID = " + ServicerId
        cmd.CommandText = os_SQL
        Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
        RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
    End If
    vData = CStr(RS1(0).Value)
    RS1.Close
    Set RS1 = Nothing
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
    GetServicerDefaultLocation = vData
    Exit Function
    
errorHandler:
  CCom.SetError
  On Error Resume Next
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & "An error occurred: " & CStr(Err.Number) & "  " & Err.Description, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Referral_Add(loanId, _
    programId, ServicerId, locationId, newServicer, newLocation, _
    agencyId, counselorId, agencyTrackingId, _
    lastName, firstName, mi, _
    addr, city, state, zip, _
    referralType, referralDate, referralComment, _
    approvedBy, sessionRatingCode, sessionType, _
    referralResultType, referralResultDate, privateInfo, internalInd, _
    summaryRptSentDate, phn, occupants, loanDefaultRsn, _
    actionItems, followupNotes, successStoryInd, firstContactStatusType, _
    contactedServicerInd, referralSourceTypeCode, _
    counselingDurationType, mthlyNetIncomeType, mthlyExpenseType, firstMortgageType, _
    creditScore, mothersMaidenName, privacyConsentInd, priorityTypeCode, _
    secondaryContactNumber, emailAddr, bankruptcyInd, bankruptcyAttorneyName, ownerOccupiedInd, _
    primaryDfltRsnType, secondaryDfltRsnType, _
    hispanicInd, raceType, dupeInd, _
    discussedSolutionWithSrvcr, _
    is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As Long
    '********************************* CANNOT ADD ANYMORE PARAMETER TO THIS FUNCTION **********************************
    '********************************* CANNOT ADD ANYMORE PARAMETER TO THIS FUNCTION **********************************
    '********************************* CANNOT ADD ANYMORE PARAMETER TO THIS FUNCTION **********************************
    '********************************* CANNOT ADD ANYMORE PARAMETER TO THIS FUNCTION **********************************

  Dim CallerID        As String
  Dim RS1             As adodb.Recordset
  Dim cmd             As adodb.Command
  Dim os_ReferralSeqId As Long
  Dim inserted    As Boolean
    
  On Error GoTo errorHandler
  CallerID = sModName & ":Referral_Add " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  'if this is a new Servicer, add it and it's location first
  If newServicer & "" <> "" Then
    Dim objAdmin As CAdmin
    Set objAdmin = New CAdmin
    ServicerId = objAdmin.Entity_Add(newServicer, "2", "", "", "", "", "N", "N", "", "N", is_UserID, os_SQL, os_ReturnCode, os_Message)
'    locationId = objAdmin.Entity_Location_Add(ServicerId, newLocation, "Y", "", is_UserID, os_sql, os_ReturnCode, os_Message)
    Call objAdmin.Program_Entity_Add(programId, ServicerId, is_UserID, os_SQL, os_ReturnCode, os_Message)
    Set objAdmin = Nothing
  End If

  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New adodb.Command
#End If
  Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
'  cmd.ActiveConnection.BeginTrans

    os_SQL = "INSERT INTO REFERRAL("
    If CCom.DBEngine = "ORACLE" Then
      os_SQL = os_SQL & "REFERRAL_SEQ_ID,"
    End If
    
    os_SQL = os_SQL & " LOAN_ID" & _
    ", PROGRAM_SEQ_ID" & _
    ", SERVICER_SEQ_ID" & _
    ", SERVICER_LOCTN_SEQ_ID" & _
    ", AGENCY_SEQ_ID" & _
    ", COUNSELOR_SEQ_ID" & _
    ", AGENCY_REFERRAL_ID" & _
    ", LAST_NAME" & _
    ", FIRST_NAME" & _
    ", MI_NAME" & _
    ", ADDR_LINE_1" & _
    ", CITY" & _
    ", STATE" & _
    ", ZIP" & _
    ", REFERRAL_TYPE_CODE" & _
    ", REFERRAL_DATE" & _
    ", GENERAL_COMMENT" & _
    ", APPROVED_BY" & _
    ", SESSION_RATING_TYPE_CODE" & _
    ", SESSION_TYPE_CODE" & _
    ", PRIVATE_INFO_IND" & _
    ", INTERNAL_IND"

os_SQL = os_SQL & ", SUMMARY_RPT_SENT_DATE" & _
    ", phn" & _
    ", occupants" & _
    ", LOAN_DEFAULT_RSN" & _
    ", ACTION_ITEMS" & _
    ", FOLLOWUP_NOTES" & _
    ", SUCCESS_STORY_IND" & _
    ", FIRST_CONTACT_STATUS_TYPE_CODE" & _
    ", CONTACTED_SERVICER_IND" & _
    ", REFERRAL_SOURCE_TYPE_CODE"
    
os_SQL = os_SQL & ", COUNSELING_DURATION_TYPE_CODE" & _
    ",  MTHLY_NET_INCOME_TYPE_CODE" & _
    ",  MTHLY_EXPENSE_TYPE_CODE" & _
    ",  FIRST_MORTGAGE_TYPE_CODE"

os_SQL = os_SQL & ", credit_Score, mothers_Maiden_Name, privacy_Consent_Ind, priority_Type_Code," & _
    " secondary_Contact_Number, email_Addr, bankruptcy_Ind, bankruptcy_Attorney_Name, owner_Occupied_Ind" & _
    ", primary_dflt_rsn_type_code, secondary_dflt_rsn_type_code" & _
    ", hispanic_ind, race_type_code, dupe_ind,DISCUSSED_SOLUTION_WITH_SRVCR_IND" & _
    MyInsertAuditFields
    os_SQL = os_SQL & ") VALUES ("

    If CCom.DBEngine = "ORACLE" Then
      os_SQL = os_SQL & "REFERRAL_S01.NextVal,"
    End If
    
    Dim servicerLocation As String
    servicerLocation = GetServicerDefaultLocation(ServicerId)
    os_SQL = os_SQL & "'" & loanId & "'," & programId & "," & ServicerId & "," + CStr(servicerLocation) + "," & agencyId & "," & counselorId & ","
    os_SQL = os_SQL & "'" & CCom.SQLScrub(agencyTrackingId) & "','" & CCom.SQLScrub(lastName) & "','" & CCom.SQLScrub(firstName) & "','" & CCom.SQLScrub(mi) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(addr) & "','" & CCom.SQLScrub(city) & "','" & CCom.SQLScrub(state) & "','" & CCom.SQLScrub(zip) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(referralType) & "'," & CCom.ScrubDate(referralDate) & ",'" & CCom.SQLScrub(referralComment) & "','" & CCom.SQLScrub(approvedBy) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(sessionRatingCode) & "','" & CCom.SQLScrub(sessionType) & "','" & CCom.SQLScrub(privateInfo) & "','" & CCom.SQLScrub(internalInd) & "',"
    os_SQL = os_SQL & CCom.ScrubDate(summaryRptSentDate) & ",'" & CCom.SQLScrub(phn) & "'," & occupants & ","
    os_SQL = os_SQL & "'" & CCom.SQLScrub(loanDefaultRsn) & "','" & CCom.SQLScrub(actionItems) & "','" & CCom.SQLScrub(followupNotes) & "','" & CCom.SQLScrub(successStoryInd) & "','" & CCom.SQLScrub(firstContactStatusType) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(contactedServicerInd) & "','" & CCom.SQLScrub(referralSourceTypeCode) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(counselingDurationType) & "','" & CCom.SQLScrub(mthlyNetIncomeType) & "','" & CCom.SQLScrub(mthlyExpenseType) & "','" & CCom.SQLScrub(firstMortgageType) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(creditScore) & "','" & CCom.SQLScrub(mothersMaidenName) & "','" & CCom.SQLScrub(privacyConsentInd) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(priorityTypeCode) & "','" & CCom.SQLScrub(secondaryContactNumber) & "','" & CCom.SQLScrub(emailAddr) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(bankruptcyInd) & "','" & CCom.SQLScrub(bankruptcyAttorneyName) & "','" & CCom.SQLScrub(ownerOccupiedInd) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(primaryDfltRsnType) & "','" & CCom.SQLScrub(secondaryDfltRsnType) & "',"
    os_SQL = os_SQL & "'" & CCom.SQLScrub(hispanicInd) & "','" & CCom.SQLScrub(raceType) & "','" & CCom.SQLScrub(dupeInd) & "',"
    ' New Fields
    os_SQL = os_SQL & "'" & CCom.SQLScrub(discussedSolutionWithSrvcr) & "',"
    os_SQL = os_SQL & MyInsertAudit & ")"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    
    Dim str As String
    str = os_SQL
    Call CCom.WriteToLog("Referral Insert")
    Call CCom.WriteToLog(str)
    
    cmd.CommandText = os_SQL
    cmd.Execute , , adodb.adExecuteNoRecords
    
    ' get key of last inserted row
    If RS1.state = adStateOpen Then RS1.Close
    os_SQL = "SELECT MAX(REFERRAL_SEQ_ID) FROM REFERRAL WHERE LOAN_ID = '" & loanId & "' AND ZIP = '" & zip & "'"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    cmd.CommandText = os_SQL
    RS1.Open cmd, , adOpenForwardOnly, adLockReadOnly
    If Not RS1.EOF Then
        os_ReferralSeqId = RS1(0)
        os_ReturnCode = Referral_Result_Type_History_Add(os_ReferralSeqId, referralResultType, Now, is_UserID, os_SQL, os_ReturnCode, os_Message)
        os_ReturnCode = 0
    Else
        os_Message = "Referral creation failed!"
        os_ReturnCode = -1
    End If
    RS1.Close
    Set RS1 = Nothing
    
Exit_Function:
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
'    cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Referral_Add = os_ReferralSeqId
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Add = -1
  On Error Resume Next
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  'cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Referral_Result_Type_History_Add(il_referralId As Variant, _
  is_TypeCode As Variant, id_Date As Variant, is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID        As String
  Dim RS1             As adodb.Recordset
  Dim cmd             As adodb.Command
  Dim os_ReferralSeqId As Long
  Dim sAcctPrd          As Variant
  
  On Error GoTo errorHandler
  
  CallerID = sModName & ":Referral_Result_Type_History_Add " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

  ' strip off domain if it's there
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  
  ' go get the properly formatted audit date
  Dim CurrentDateTime As String
  CurrentDateTime = CCom.Audit_Date(CurrentUserID, "INSERTSQL", "")
  If IsNull(id_Date) Then id_Date = CurrentDateTime
  If Right(Trim(CurrentDateTime), 1) = "," Then CurrentDateTime = Left(CurrentDateTime, Len(Trim(CurrentDateTime)) - 1)
  
  ' build the audit fields string
  Dim MyInsertAudit   As String
  Dim MyQueryAudit    As String
  Dim MyInsertAuditFields As String
  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_DATE,CREATE_PROG_NAME,CREATE_USER_ID,CHG_LST_DATE,CHG_LST_PROG_NAME,CHG_LST_USER_ID"
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  'cmd.ActiveConnection.BeginTrans

  sAcctPrd = IsBillable(il_referralId, id_Date, is_TypeCode)
      
  Select Case CCom.GetDBEngine
  Case "SQLSERVER", "MSSQL", "ACCESS", "SQL", "SQL7"
    os_SQL = "INSERT INTO REF_RESULT_TYPE_HIST(REFERRAL_SEQ_ID, REFERRAL_RESULT_TYPE_CODE, REFERRAL_RESULT_DATE, ACCT_PRD" & _
      MyInsertAuditFields & ")" & _
      " Values ("
  Case Else
    os_SQL = "INSERT INTO REF_RESULT_TYPE_HIST(REF_RESULT_TYPE_HIST_SEQ_ID, REFERRAL_SEQ_ID, REFERRAL_RESULT_TYPE_CODE, REFERRAL_RESULT_DATE, ACCT_PRD" & _
      MyInsertAuditFields & ")" & _
      " Values (REFERRAL_S01.NextVal, "
  End Select
  id_Date = Format(id_Date, "mm/dd/yyyy") & " " & Format(Now, "hh:mm:ss")
  os_SQL = os_SQL & il_referralId & "," & is_TypeCode & "," & CCom.ScrubDate(id_Date) & "," & sAcctPrd & "," & _
    MyInsertAudit & ")"
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , adodb.adExecuteNoRecords
  
Exit_Function:
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
    'cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  os_ReturnCode = 0
  Referral_Result_Type_History_Add = 0
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Result_Type_History_Add = -1
  On Error Resume Next
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  'cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Referral_Update(referralId, loanId, _
    programId, ServicerId, locationId, newServicer, newLocation, _
    agencyId, counselorId, agencyTrackingId, _
    lastName, firstName, mi, _
    addr, city, state, zip, _
    referralType, referralDate, referralComment, approvedBy, _
    sessionRatingCode, sessionType, _
    referralResultType, referralResultDate, _
    summaryRptSentDate, phn, occupants, loanDefaultRsn, _
    actionItems, followupNotes, successStoryInd, firstContactStatusType, _
    contactedServicerInd, referralSourceTypeCode, _
    counselingDurationType, mthlyNetIncomeType, mthlyExpenseType, firstMortgageType, _
    creditScore, mothersMaidenName, privacyConsentInd, priorityTypeCode, _
    secondaryContactNumber, emailAddr, bankruptcyInd, bankruptcyAttorneyName, ownerOccupiedInd, _
    primaryDfltRsnType, secondaryDfltRsnType, _
    hispanicInd, raceType, dupeInd, _
    discussedSolutionWithSrvcr, _
    is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":Referral_Update " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
        
  'if this is a new Servicer, add it and it's location first
  If newServicer & "" <> "" Then
    Dim objAdmin As CAdmin
    Set objAdmin = New CAdmin
    ServicerId = objAdmin.Entity_Add(newServicer, "2", "", "", "", "", "N", "N", "", "N", is_UserID, os_SQL, os_ReturnCode, os_Message)
'    locationId = objAdmin.Entity_Location_Add(ServicerId, newLocation, "Y", "", is_UserID, os_sql, os_ReturnCode, os_Message)
'    locationId = GetServicerDefaultLocation(ServicerId) - comment by Long Cao
    Call objAdmin.Program_Entity_Add(programId, ServicerId, is_UserID, os_SQL, os_ReturnCode, os_Message)
    Set objAdmin = Nothing
  End If

  locationId = GetServicerDefaultLocation(ServicerId)
      
  os_SQL = "UPDATE REFERRAL SET LOAN_ID = '" & CCom.SQLScrub(loanId) & "'"
  os_SQL = os_SQL & ", PROGRAM_SEQ_ID = " & programId
  os_SQL = os_SQL & ", AGENCY_SEQ_ID = " & agencyId
  os_SQL = os_SQL & ", COUNSELOR_SEQ_ID = " & counselorId
  os_SQL = os_SQL & ", AGENCY_REFERRAL_ID = '" & CCom.SQLScrub(agencyTrackingId) & "'"
  os_SQL = os_SQL & ", SERVICER_SEQ_ID = " & ServicerId
  os_SQL = os_SQL & ", SERVICER_LOCTN_SEQ_ID = " & locationId
  os_SQL = os_SQL & ", LAST_NAME = '" & CCom.SQLScrub(lastName) & "'"
  os_SQL = os_SQL & ", FIRST_NAME = '" & CCom.SQLScrub(firstName) & "'"
  os_SQL = os_SQL & ", MI_NAME = '" & CCom.SQLScrub(mi) & "'"
  os_SQL = os_SQL & ", ADDR_LINE_1 = '" & CCom.SQLScrub(addr) & "'"
  os_SQL = os_SQL & ", CITY = '" & CCom.SQLScrub(city) & "'"
  os_SQL = os_SQL & ", STATE = '" & CCom.SQLScrub(state) & "'"
  os_SQL = os_SQL & ", ZIP = '" & CCom.SQLScrub(zip) & "'"
  os_SQL = os_SQL & ", REFERRAL_TYPE_CODE = '" & CCom.SQLScrub(referralType) & "'"
  os_SQL = os_SQL & ", REFERRAL_DATE = " & CCom.ScrubDate(referralDate)
  os_SQL = os_SQL & ", GENERAL_COMMENT = '" & CCom.SQLScrub(referralComment) & "'"
  os_SQL = os_SQL & ", APPROVED_BY = '" & CCom.SQLScrub(approvedBy) & "'"
  os_SQL = os_SQL & ", SESSION_RATING_TYPE_CODE = '" & CCom.SQLScrub(sessionRatingCode) & "'"
  os_SQL = os_SQL & ", SESSION_TYPE_CODE = '" & CCom.SQLScrub(sessionType) & "'"
  os_SQL = os_SQL & ", SUMMARY_RPT_SENT_DATE = " & CCom.ScrubDate(summaryRptSentDate)
  os_SQL = os_SQL & ", PHN = '" & CCom.SQLScrub(phn) & "'"
  os_SQL = os_SQL & ", OCCUPANTS = " & occupants
  os_SQL = os_SQL & ", LOAN_DEFAULT_RSN = '" & CCom.SQLScrub(loanDefaultRsn) & "'"
  os_SQL = os_SQL & ", ACTION_ITEMS = '" & CCom.SQLScrub(actionItems) & "'"
  os_SQL = os_SQL & ", FOLLOWUP_NOTES = '" & CCom.SQLScrub(followupNotes) & "'"
  os_SQL = os_SQL & ", SUCCESS_STORY_IND = '" & CCom.SQLScrub(successStoryInd) & "'"
  os_SQL = os_SQL & ", FIRST_CONTACT_STATUS_TYPE_CODE = '" & CCom.SQLScrub(firstContactStatusType) & "'"
  os_SQL = os_SQL & ", CONTACTED_SERVICER_IND = '" & CCom.SQLScrub(contactedServicerInd) & "'"
  os_SQL = os_SQL & ", REFERRAL_SOURCE_TYPE_CODE = '" & CCom.SQLScrub(referralSourceTypeCode) & "'"
  os_SQL = os_SQL & ", COUNSELING_DURATION_TYPE_CODE = '" & CCom.SQLScrub(counselingDurationType) & "'"
  os_SQL = os_SQL & ", MTHLY_NET_INCOME_TYPE_CODE = '" & CCom.SQLScrub(mthlyNetIncomeType) & "'"
  os_SQL = os_SQL & ", MTHLY_EXPENSE_TYPE_CODE = '" & CCom.SQLScrub(mthlyExpenseType) & "'"
  os_SQL = os_SQL & ", FIRST_MORTGAGE_TYPE_CODE = '" & CCom.SQLScrub(firstMortgageType) & "'"
  os_SQL = os_SQL & ", credit_score = '" & CCom.SQLScrub(creditScore) & "'"
  os_SQL = os_SQL & ", mothers_maiden_name = '" & CCom.SQLScrub(mothersMaidenName) & "'"
  os_SQL = os_SQL & ", privacy_consent_ind = '" & CCom.SQLScrub(privacyConsentInd) & "'"
  os_SQL = os_SQL & ", priority_type_code = '" & CCom.SQLScrub(priorityTypeCode) & "'"
  os_SQL = os_SQL & ", secondary_contact_number = '" & CCom.SQLScrub(secondaryContactNumber) & "'"
  os_SQL = os_SQL & ", email_addr = '" & CCom.SQLScrub(emailAddr) & "'"
  os_SQL = os_SQL & ", bankruptcy_ind = '" & CCom.SQLScrub(bankruptcyInd) & "'"
  os_SQL = os_SQL & ", bankruptcy_attorney_name = '" & CCom.SQLScrub(bankruptcyAttorneyName) & "'"
  os_SQL = os_SQL & ", owner_occupied_ind = '" & CCom.SQLScrub(ownerOccupiedInd) & "'"
  os_SQL = os_SQL & ", primary_dflt_rsn_type_code = '" & CCom.SQLScrub(primaryDfltRsnType) & "'"
  os_SQL = os_SQL & ", secondary_dflt_rsn_type_code = '" & CCom.SQLScrub(secondaryDfltRsnType) & "'"
  os_SQL = os_SQL & ", hispanic_ind = '" & CCom.SQLScrub(hispanicInd) & "'"
  os_SQL = os_SQL & ", race_type_code = '" & CCom.SQLScrub(raceType) & "'"
  os_SQL = os_SQL & ", dupe_ind = '" & CCom.SQLScrub(dupeInd) & "'"
  ' New Fields
  os_SQL = os_SQL & ", DISCUSSED_SOLUTION_WITH_SRVCR_IND = '" & CCom.SQLScrub(discussedSolutionWithSrvcr) & "'"
  If dupeInd = "Y" Then
    os_SQL = os_SQL & ", invoice_date = NULL"
  End If
  os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
  os_SQL = os_SQL & " WHERE REFERRAL_SEQ_ID = " & referralId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  
  If Len(referralResultType) > 0 And Len(referralResultDate) > 0 Then
    Call Referral_Result_Type_History_Add(referralId, referralResultType, referralResultDate, is_UserID, os_SQL, os_ReturnCode, os_Message)
  End If
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  Referral_Update = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Update = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Referral_Update2(referralId, age, genderType, householdType, householdIncome, incomeCategoryType, intakeScoreType, pitiAmt, _
    armResetInd, propertyForSaleInd, listPriceAmt, realtyCompanyName, fcNoticeRecdInd, incomeEarnersType, summarySentOtherType, _
    secondservicerseqid, secondmortagetypecode, secondloanid, secondpitiamt, secondloanstatustypecode, _
    summarySentOtherDate, _
    hUDOutcome, hUDTermiReason, referralHUDTermdate, workedWithAnotherAgencyInd, _
    is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID     As String
    
  On Error GoTo errorHandler
    
  CallerID = sModName & ":Referral_Update2 " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
  
  ' Putting Null is value is empty
  
        Call CCom.WriteToLog("-1")
    os_SQL = "UPDATE REFERRAL SET"
    os_SQL = os_SQL & " AGE = " & age
    os_SQL = os_SQL & ", GENDER_TYPE_CODE = '" & genderType & "'"
    os_SQL = os_SQL & ", HOUSEHOLD_TYPE_CODE  = '" & householdType & "'"
    os_SQL = os_SQL & ", HOUSEHOLD_INCOME_AMT = " & householdIncome
    os_SQL = os_SQL & ", INCOME_CATEGORY_TYPE_CODE = '" & incomeCategoryType & "'"
    os_SQL = os_SQL & ", INTAKE_SCORE_TYPE_CODE = '" & intakeScoreType & "'"
    os_SQL = os_SQL & ", PITI_AMT = " & pitiAmt
    os_SQL = os_SQL & ", ARM_RESET_IND = '" & armResetInd & "'"
    os_SQL = os_SQL & ", PROPERTY_FOR_SALE_IND = '" & propertyForSaleInd & "'"
    os_SQL = os_SQL & ", LIST_PRICE_AMT = " & listPriceAmt
    os_SQL = os_SQL & ", REALTY_COMPANY_NAME = '" & CCom.SQLScrub(realtyCompanyName) & "'"
    os_SQL = os_SQL & ", FC_NOTICE_RECD_IND = '" & fcNoticeRecdInd & "'"
    os_SQL = os_SQL & ", INCOME_EARNERS_TYPE_CODE = '" & incomeEarnersType & "'"
    os_SQL = os_SQL & ", SUMMARY_SENT_OTHER_TYPE_CODE = '" & summarySentOtherType & "'"
    os_SQL = os_SQL & ", SUMMARY_SENT_OTHER_DATE = " & CCom.ScrubDate(summarySentOtherDate)
     ' New Columns
    If Not secondservicerseqid > 0 Then
        os_SQL = os_SQL & ", SECOND_SERVICER_SEQ_ID = NULL"
    Else
        os_SQL = os_SQL & ", SECOND_SERVICER_SEQ_ID = " & secondservicerseqid
    End If
        Dim str As String
    str = os_SQL
     
    
    Dim strTemp As String
    strTemp = secondmortagetypecode
    If Len(strTemp) = 0 Then
        os_SQL = os_SQL & ", SECOND_MORTGAGE_TYPE_CODE = NULL"
    Else
        os_SQL = os_SQL & ", SECOND_MORTGAGE_TYPE_CODE = '" & secondmortagetypecode & "'"
    End If
     str = os_SQL
     
     'os_SQL = os_SQL & ", SECOND_MORTGAGE_TYPE_CODE = '" & secondmortagetypecode & "'"
    strTemp = secondloanid
    If Len(strTemp) = 0 Then
        os_SQL = os_SQL & ", SECOND_LOAN_ID = NULL"
    Else
        os_SQL = os_SQL & ", SECOND_LOAN_ID = '" & secondloanid & "'"
    End If
    'os_SQL = os_SQL & ", SECOND_LOAN_ID = '" & secondloanid & "'"
     str = os_SQL
     
     If Not secondpitiamt > 0 Then
        os_SQL = os_SQL & ", SECOND_PITI_AMT = NULL"
    Else
        os_SQL = os_SQL & ", SECOND_PITI_AMT = " & secondpitiamt
    End If
    
    os_SQL = os_SQL & ", HUD_OUTCOME_TYPE_CODE = '" & hUDOutcome & "'"
    
    os_SQL = os_SQL & ", WORKED_WITH_ANOTHER_AGENCY_IND = '" & workedWithAnotherAgencyInd & "'"
    
    If Len(hUDTermiReason) = 0 Then
        os_SQL = os_SQL & ", HUD_TERM_REASON_TYPE_CODE = NULL"
    Else
        os_SQL = os_SQL & ", HUD_TERM_REASON_TYPE_CODE = " & hUDTermiReason
    End If
    
    If Len(referralHUDTermdate) = 0 Then
        os_SQL = os_SQL & ", HUD_TERM_DATE = NULL"
    Else
        os_SQL = os_SQL & ", HUD_TERM_DATE = " & CCom.ScrubDate(referralHUDTermdate)
    End If
       
    If Len(secondloanstatustypecode) = 0 Then
        os_SQL = os_SQL & ", SECOND_LOAN_STATUS_TYPE_CODE = NULL"
    Else
        os_SQL = os_SQL & ", SECOND_LOAN_STATUS_TYPE_CODE = '" & secondloanstatustypecode & "'"
    End If
     
    'os_SQL = os_SQL & ", SECOND_LOAN_STATUS_TYPE_CODE = '" & secondloanstatustypecode & "'"

    os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
    os_SQL = os_SQL & " WHERE REFERRAL_SEQ_ID = " & referralId
    
     str = os_SQL
    Call CCom.WriteToLog(str)
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Call CCom.ExecSQL(os_SQL)
  
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  Referral_Update2 = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Update2 = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Referral_Delete(ii_ReferralId As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As adodb.Command
  Dim conn         As adodb.Connection
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Referral_Delete " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New adodb.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  os_SQL = "DELETE FROM REFERRAL WHERE REFERRAL_SEQ_ID = " & ii_ReferralId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , adodb.adExecuteNoRecords
  
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  Referral_Delete = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Delete = -1
  On Error Resume Next
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Referral_Results(ii_ReferralId As Variant, is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Referral_Results " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT RRTH.*, RRT.DSCR"
    
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = os_SQL & " FROM REF_RESULT_TYPE_HIST RRTH, REFERRAL_RESULT_TYPE RRT"
        os_SQL = os_SQL & " WHERE RRTH.REFERRAL_SEQ_ID = " & ii_ReferralId
        os_SQL = os_SQL & " AND RRTH.REFERRAL_RESULT_TYPE_CODE = RRT.REFERRAL_RESULT_TYPE_CODE "
    Else
        os_SQL = os_SQL & " FROM REF_RESULT_TYPE_HIST RRTH"
        os_SQL = os_SQL & " INNER JOIN REFERRAL_RESULT_TYPE RRT ON RRTH.REFERRAL_RESULT_TYPE_CODE = RRT.REFERRAL_RESULT_TYPE_CODE"
        os_SQL = os_SQL & " WHERE RRTH.REFERRAL_SEQ_ID = " & ii_ReferralId
    End If
    os_SQL = os_SQL & " ORDER BY RRTH.REFERRAL_RESULT_DATE DESC"

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Referral_Results = CCom.RunSQLReturnRS(os_SQL)
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

Public Function Get_Referral_Result(ii_ReferralResultId As Variant, is_UserID As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Referral_Result " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT RRTH.*, RRT.DSCR"
    
    If CCom.DBEngine = "ORACLE" Then
        os_SQL = os_SQL & " FROM REF_RESULT_TYPE_HIST RRTH, REFERRAL_RESULT_TYPE RRT"
        os_SQL = os_SQL & " WHERE RRTH.REF_RESULT_TYPE_HIST_SEQ_ID = " & ii_ReferralResultId
        os_SQL = os_SQL & " AND RRTH.REFERRAL_RESULT_TYPE_CODE = RRT.REFERRAL_RESULT_TYPE_CODE "
    Else
        os_SQL = os_SQL & " FROM REF_RESULT_TYPE_HIST RRTH"
        os_SQL = os_SQL & " INNER JOIN REFERRAL_RESULT_TYPE RRT ON RRTH.REFERRAL_RESULT_TYPE_CODE = RRT.REFERRAL_RESULT_TYPE_CODE"
        os_SQL = os_SQL & " WHERE RRTH.REF_RESULT_TYPE_HIST_SEQ_ID = " & ii_ReferralResultId
    End If

    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Referral_Result = CCom.RunSQLReturnRS(os_SQL)
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

Public Function IsBillable(ii_ReferralId As Variant, id_Date As Variant, is_TypeCode As Variant) As String
    Dim CallerID     As String
    Dim RS1 As adodb.Recordset
    Dim sAcctPrd As String
    Dim aYearAgo
    Dim os_SQL As String
    Dim isPayable As Boolean
    Dim okToBill As Boolean
    
    On Error GoTo errorHandler
    
    sAcctPrd = "Null"
    aYearAgo = CCom.ScrubDate(DateAdd("yyyy", -1, id_Date))
  
    os_SQL = "SELECT PAYABLE_IND FROM REFERRAL_RESULT_TYPE WHERE REFERRAL_RESULT_TYPE_CODE = '" & is_TypeCode & "'"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set RS1 = CCom.RunSQLReturnRS(os_SQL)
    isPayable = RS1.Fields(0).Value = "Y"
    RS1.Close
  
    os_SQL = "SELECT COUNT(1) FROM REF_RESULT_TYPE_HIST WHERE REFERRAL_SEQ_ID = " & ii_ReferralId & " AND ACCT_PRD IS NOT NULL AND REFERRAL_RESULT_DATE >= " & aYearAgo
    Set RS1 = CCom.RunSQLReturnRS(os_SQL)
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    okToBill = RS1.Fields(0).Value = 0
    RS1.Close
    
    If isPayable And okToBill Then
        sAcctPrd = Format(Date, "YYYYMM")
    End If
    
    Set RS1 = Nothing
    IsBillable = sAcctPrd
    Exit Function
  
errorHandler:
    CCom.SetError
    IsBillable = ""
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & "An error occurred: " & CStr(Err.Number) & "  " & Err.Description, 2)
    CCom.GetError
    Err.Raise Err.Number, CallerID, Err.Description
  
End Function

'Public Function Referral_Result_Type_History_Update(ii_ReferralResultTypeId As Variant, _
'  is_ReferralResultType, is_ReferralResultDate, ii_ReferralId As Variant, is_UserID As Variant, _
'  os_SQL As Variant, os_ReturnCode As Variant, _
'  os_Message As Variant) As Integer
'
'  Dim CallerID     As String
'  Dim arParms() As String
'  Dim arParm() As String
'  Dim iParm As Integer
'  Dim RS1 As ADODB.Recordset
'  Dim sAcctPrd As String
'  Dim sOldAcctPrd As String
'  On Error GoTo errorHandler
'
'  Dim CurrentUserID   As String
'  CurrentUserID = CCom.Raw_UserID(is_UserID)
'  CallerID = sModName & ":Referral_Result_Type_History_Update " & vbTab & "(" & is_UserID & ")" & vbTab
'  Call CCom.CtxSetAbort
'  Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
'  os_ReturnCode = -1
'
'    os_SQL = "SELECT ACCT_PRD FROM REF_RESULT_TYPE_HIST WHERE REF_RESULT_TYPE_HIST_SEQ_ID = " & ii_ReferralResultTypeId
'    Set RS1 = CCom.RunSQLReturnRS(os_SQL)
'    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
'    sOldAcctPrd = RS1.Fields(0).Value
'    RS1.Close
'
'
'  sAcctPrd = IsBillable(ii_ReferralId, ii_ReferralResultTypeId, is_ReferralResultType)
'
'  is_ReferralResultDate = Format(is_ReferralResultDate, "mm/dd/yyyy") & " " & Format(Now, "hh:mm:ss")
'  os_SQL = "UPDATE REF_RESULT_TYPE_HIST SET "
'  os_SQL = os_SQL & "REFERRAL_RESULT_TYPE_CODE = '" & is_ReferralResultType & "',"
'  os_SQL = os_SQL & "REFERRAL_RESULT_DATE = " & CCom.ScrubDate(is_ReferralResultDate) & ","
'  os_SQL = os_SQL & "ACCT_PRD = " & sAcctPrd
'  os_SQL = os_SQL & " WHERE REF_RESULT_TYPE_HIST_SEQ_ID = " & ii_ReferralResultTypeId
'  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
'  Call CCom.ExecSQL(os_SQL)
'
'  'if updated from a stamped billable to nonbillable, we must check to see if a different
'  'billable record needs to be stamped
'  If Not IsNull(sOldAcctPrd) Then
'    os_SQL = "SELECT * FROM REF_RESULT_TYPE_HIST WHERE REF_RESULT_TYPE_HIST_SEQ_ID <> " & ii_ReferralResultTypeId
'    Set RS1 = CCom.RunSQLReturnRS(os_SQL)
'    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
'    Do While Not RS1.EOF
'        RS1.MoveNext
'    Loop
'    RS1.Close
'  End If
'
'  '
'  os_ReturnCode = 0
'
'Exit_Function:
'  Call CCom.CtxSetComplete
'  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
'  Referral_Result_Type_History_Update = os_ReturnCode
'  Exit Function
'
'errorHandler:
'  CCom.SetError
'  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
'  os_ReturnCode = -1
'  Referral_Result_Type_History_Update = -1
'  On Error Resume Next
'  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
'  Call CCom.CtxSetAbort
'  CCom.GetError
'  Err.Raise Err.Number, CallerID, Err.Description
'End Function

Public Function Referral_Result_Type_History_Delete(ii_ReferralId As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As adodb.Command
  Dim conn         As adodb.Connection
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Referral_Result_Type_History_Delete " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New adodb.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  ' Begin transaction
  'cmd.ActiveConnection.BeginTrans
  
  
  os_SQL = "DELETE FROM REF_RESULT_TYPE_HIST WHERE REF_RESULT_TYPE_HIST_SEQ_ID = " & ii_ReferralId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , adodb.adExecuteNoRecords
  
  ' End transaction
  'cmd.ActiveConnection.CommitTrans
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  Referral_Result_Type_History_Delete = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Result_Type_History_Delete = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  'cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function GetICEProfileEditorServer() As String
    Dim Reg As New cReg
    Reg.OpenSoftwareKey ("RFC\CCRC")
    GetICEProfileEditorServer = Reg.QueryValueStr("ICEProfileEditor")
End Function

Public Function Get_Referrals(is_AcctPrd As Variant, is_StartDate As Variant, _
    is_EndDate As Variant, is_OutcomeType As Variant, is_ReportType As Variant, _
    ii_CCRCUserId As Variant, is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    Dim CallerID        As String
    Dim sFilter         As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Referrals " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    If Len(is_StartDate) > 0 Then
      is_StartDate = CCom.ToCharDate(is_StartDate)
      is_EndDate = CCom.ToCharDate(is_EndDate)
      If UCase(is_ReportType) = "REFERRALS" Then
          'TO_DATE replace by CAST for MS SQL Compitability
          'sFilter = sFilter & "REFERRAL_DATE >= TO_DATE('" & is_StartDate & "','MM/DD/YYYY') AND REFERRAL_DATE <= TO_DATE('" & is_EndDate & "','MM/DD/YYYY')"
           sFilter = sFilter & "REFERRAL_DATE >= convert(datetime,'" & is_StartDate & "',101) AND REFERRAL_DATE <= convert(datetime,'" & is_EndDate & "',101)"
      Else
           ''TO_DATE replace by CAST for MS SQL Compitability
          'sFilter = sFilter & "REFERRAL_RESULT_DATE >= TO_DATE('" & is_StartDate & "','MM/DD/YYYY') AND REFERRAL_RESULT_DATE <= TO_DATE('" & is_EndDate & "','MM/DD/YYYY')"
           sFilter = sFilter & "REFERRAL_RESULT_DATE >= convert(datetime,'" & is_StartDate & "',101) AND REFERRAL_RESULT_DATE <= convert(datetime,'" & is_EndDate & "',101)"
      End If
    ElseIf Len(is_AcctPrd) > 0 Then
      sFilter = "ACCT_PRD = " & CCom.Scrub(is_AcctPrd)
    End If
    
    Dim objAdmin As CAdmin
    Dim sACL As String
    Set objAdmin = New CAdmin
    sACL = objAdmin.Get_User_ACL("report", ii_CCRCUserId, "", "", os_SQL, os_ReturnCode, os_Message)
    If Len(sFilter) > 0 Then
      sFilter = sFilter & " AND "
    End If
    If Len(sACL) > 0 Then
      sFilter = sFilter & sACL & " AND "
    End If
    
    If Len(is_OutcomeType) > 0 Then
      sFilter = sFilter & " REFERRAL_RESULT_TYPE_CODE = '" & is_OutcomeType & "' AND "
    End If
    
    
    os_SQL = "SELECT /*+ CHOOSE */ REFERRAL.*, PROGRAM.PROGRAM_SEQ_ID, PROGRAM.PROGRAM_NAME, PROGRAM.ALLOW_NONCONTRACTED_SVCR_IND,"
    os_SQL = os_SQL & " AGENCY.BSN_ENTITY_SEQ_ID AGENCY_ID, AGENCY.BSN_ENTITY_NAME AGENCY, AGENCY.PHONE,"
    os_SQL = os_SQL & " SERVICER.BSN_ENTITY_SEQ_ID SERVICER_ID, SERVICER.BSN_ENTITY_NAME SERVICER, SERVICER.HAS_CONTRACT_IND,"
    os_SQL = os_SQL & " REFERRAL_RESULT_VW.REFERRAL_RESULT_DATE, REFERRAL_RESULT_VW.REFERRAL_RESULT_TYPE_CODE,"
    os_SQL = os_SQL & " REFERRAL_RESULT_VW.DSCR REFERRAL_RESULT, REFERRAL_RESULT_VW.PAYABLE_IND,"
    os_SQL = os_SQL & " PROFILE.LAST_NAME LAST_NAME, PROFILE.FIRST_NAME FIRST_NAME, PROFILE.MID_INIT_NAME, PROFILE.NAME_SUFFIX_CODE, PROFILE.PRIMARY_PHN,"
    os_SQL = os_SQL & " BSN_ENTITY_LOCTN.BSN_ENTITY_LOCTN_SEQ_ID, BSN_ENTITY_LOCTN.LOCTN_NAME,"
    os_SQL = os_SQL & " SERVICER.BSN_ENTITY_NAME || ' - ' || BSN_ENTITY_LOCTN.LOCTN_NAME SERVICER_LOCATION,"
    os_SQL = os_SQL & " DECODE(SERVICER.HAS_CONTRACT_IND,'N','NON-CONTRACTED SERVICER',SERVICER.BSN_ENTITY_NAME) SERVICER_GROUPING_NAME,"
    If Len(is_StartDate) > 0 Then
      os_SQL = os_SQL & "" & " ACCT_PRD,"
      os_SQL = os_SQL & "'" & is_StartDate & "' START_DATE,"
      os_SQL = os_SQL & "'" & is_EndDate & "' END_DATE"
    Else
      os_SQL = os_SQL & is_AcctPrd & " ACCT_PRD,"
      os_SQL = os_SQL & "" & " START_DATE,"
      os_SQL = os_SQL & "" & " END_DATE"
    End If

    
    os_SQL = os_SQL & " FROM REFERRAL, PROGRAM, REFERRAL_RESULT_VW,"
    os_SQL = os_SQL & " BSN_ENTITY AGENCY, BSN_ENTITY SERVICER, BSN_ENTITY_LOCTN, CCRC_USER, PROFILE"
    
    os_SQL = os_SQL & " WHERE " & sFilter
    os_SQL = os_SQL & " REFERRAL.PROGRAM_SEQ_ID = PROGRAM.PROGRAM_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.AGENCY_SEQ_ID = AGENCY.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_SEQ_ID = SERVICER.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_LOCTN_SEQ_ID = BSN_ENTITY_LOCTN.BSN_ENTITY_LOCTN_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.COUNSELOR_SEQ_ID = CCRC_USER.CCRC_USER_SEQ_ID"
    os_SQL = os_SQL & " AND CCRC_USER.NT_DOMAIN = PROFILE.NT_DOMAIN"
    os_SQL = os_SQL & " AND CCRC_USER.NT_USERID = PROFILE.NT_USERID"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_SEQ_ID = REFERRAL_RESULT_VW.REFERRAL_SEQ_ID"
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Referrals = CCom.RunSQLReturnRS(os_SQL)
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

Public Function Get_Referral_List(programIds As Variant, agencyIds As Variant, _
    servicerIds As Variant, locationIds As Variant, is_acctPrd1 As Variant, is_acctPrd2 As Variant, is_StartDate As Variant, _
    is_EndDate As Variant, is_ReportType As Variant, is_OutcomeType As Variant, _
    hasContract As Variant, actionPlan As Variant, _
    ii_CCRCUserId As Variant, is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    On Error GoTo errorHandler

    Dim CallerID        As String
    Dim sFilter         As String
        
    Call CreateFilters(programIds, agencyIds, servicerIds, locationIds)
    
    CallerID = sModName & ":Get_Referral_List " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    If Len(is_StartDate) > 0 Then
      is_StartDate = CCom.ToCharDate(is_StartDate)
      is_EndDate = CCom.ToCharDate(is_EndDate)
      If UCase(is_ReportType) = "REFERRALS" Then
          'sFilter = sFilter & "REFERRAL_DATE >= TO_DATE('" & is_StartDate & "','MM/DD/YYYY') AND REFERRAL_DATE <= TO_DATE('" & is_EndDate & "','MM/DD/YYYY')"
          
          sFilter = sFilter & "Convert(Varchar(10),REFERRAL_DATE ,101 )  >= '" & is_StartDate & "' AND Convert(Varchar(10),REFERRAL_DATE ,101 ) <= '" & is_EndDate & "'"
          
      Else
          'sFilter = sFilter & "Convert(Varchar(10),REFERRAL_RESULT_DATE,101 )  >= '" & is_StartDate & "'  AND Convert(Varchar(10),REFERRAL_RESULT_DATE,101 ) <= '" & is_EndDate & "'"
          sFilter = sFilter & "Convert(Varchar(10),REFERRAL_RESULT_DATE,101 )  >= '" & is_StartDate & "' AND Convert(Varchar(10),REFERRAL_RESULT_DATE,101 ) <= '" & is_EndDate & "'"
      End If
    ElseIf Len(is_acctPrd1) > 0 Then
      sFilter = "ACCT_PRD >= " & CCom.Scrub(is_acctPrd1) & " AND ACCT_PRD <= " & CCom.Scrub(is_acctPrd2)
    End If
    
    Dim objAdmin As CAdmin
    Dim sACL As String
    Set objAdmin = New CAdmin
    sACL = objAdmin.Get_User_ACL("report", ii_CCRCUserId, "", "", os_SQL, os_ReturnCode, os_Message)
    If Len(sFilter) > 0 Then
      sFilter = sFilter & " AND "
    End If
    If Len(sACL) > 0 Then
      sFilter = sFilter & sACL & " AND "
    End If
    
    If Len(is_OutcomeType) > 0 Then
      sFilter = sFilter & " REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = '" & is_OutcomeType & "' AND "
    End If
    
    If Len(hasContract) > 0 Then
      If hasContract = "CONTRACTED SERVICERS" Then
        sFilter = sFilter & " SERVICER.HAS_CONTRACT_IND = 'Y' AND"
      Else
        sFilter = sFilter & " SERVICER.HAS_CONTRACT_IND = 'N' AND "
      End If
    End If
    
    If Len(actionPlan) > 0 Then
      If actionPlan = "ACTION PLANS" Then
        sFilter = sFilter & " PAYABLE_IND = 'Y' AND"
      Else
        sFilter = sFilter & " PAYABLE_IND = 'N' AND"
      End If
    End If
    
    os_SQL = "SELECT /*+ CHOOSE */ REFERRAL.*, PROGRAM.PROGRAM_SEQ_ID, PROGRAM.PROGRAM_NAME, PROGRAM.ALLOW_NONCONTRACTED_SVCR_IND,"
    os_SQL = os_SQL & " AGENCY.BSN_ENTITY_SEQ_ID AGENCY_ID, AGENCY.BSN_ENTITY_NAME AGENCY, AGENCY.PHONE,"
    os_SQL = os_SQL & " SERVICER.BSN_ENTITY_SEQ_ID SERVICER_ID, SERVICER.BSN_ENTITY_NAME SERVICER, SERVICER.HAS_CONTRACT_IND,"
    os_SQL = os_SQL & " REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE, REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE, REF_RESULT_TYPE_HIST.ACCT_PRD,"
    os_SQL = os_SQL & " REFERRAL_RESULT_TYPE.DSCR REFERRAL_RESULT, REFERRAL_RESULT_TYPE.PAYABLE_IND,"
    os_SQL = os_SQL & " PROFILE.LAST_NAME COUNSELOR_LAST_NAME, PROFILE.FIRST_NAME COUNSELOR_FIRST_NAME, PROFILE.MID_INIT_NAME COUNSELOR_MID_INIT_NAME, PROFILE.NAME_SUFFIX_CODE COUNSELOR_NAME_SUFFIX_CODE, PROFILE.PRIMARY_PHN COUNSELOR_PHONE,"
    os_SQL = os_SQL & " BSN_ENTITY_LOCTN.BSN_ENTITY_LOCTN_SEQ_ID, BSN_ENTITY_LOCTN.LOCTN_NAME,"
    os_SQL = os_SQL & " REFERRAL_TYPE.DSCR REFERRAL_TYPE, FIRST_CONTACT_STATUS_TYPE.DSCR FIRST_CONTACT_STATUS_TYPE,"
    os_SQL = os_SQL & " REFERRAL_TYPE.REFERRAL_TYPE_CODE, FIRST_CONTACT_STATUS_TYPE.FIRST_CONTACT_STATUS_TYPE_CODE,"
    os_SQL = os_SQL & "'" & is_StartDate & "' START_DATE,"
    os_SQL = os_SQL & "'" & is_EndDate & "' END_DATE"

    os_SQL = os_SQL & " FROM REFERRAL, PROGRAM, REF_RESULT_TYPE_HIST, REFERRAL_RESULT_TYPE,"
    os_SQL = os_SQL & " BSN_ENTITY AGENCY, BSN_ENTITY SERVICER, BSN_ENTITY_LOCTN,"
    os_SQL = os_SQL & " CCRC_USER, PROFILE, REFERRAL_TYPE, FIRST_CONTACT_STATUS_TYPE"
    
    os_SQL = os_SQL & " WHERE " & sFilter
    os_SQL = os_SQL & " REFERRAL.PROGRAM_SEQ_ID = PROGRAM.PROGRAM_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.AGENCY_SEQ_ID = AGENCY.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_SEQ_ID = SERVICER.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_LOCTN_SEQ_ID = BSN_ENTITY_LOCTN.BSN_ENTITY_LOCTN_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.COUNSELOR_SEQ_ID = CCRC_USER.CCRC_USER_SEQ_ID"
    os_SQL = os_SQL & " AND CCRC_USER.NT_DOMAIN = PROFILE.NT_DOMAIN"
    os_SQL = os_SQL & " AND CCRC_USER.NT_USERID = PROFILE.NT_USERID"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_SEQ_ID = REF_RESULT_TYPE_HIST.REFERRAL_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_TYPE_CODE = REFERRAL_TYPE.REFERRAL_TYPE_CODE"
    os_SQL = os_SQL & " AND REFERRAL.FIRST_CONTACT_STATUS_TYPE_CODE = FIRST_CONTACT_STATUS_TYPE.FIRST_CONTACT_STATUS_TYPE_CODE"
        
    'only get the lastest referral for the referral report
    If "Referrals" = is_ReportType Then
        os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE = (SELECT MAX(A.REFERRAL_RESULT_DATE) FROM REF_RESULT_TYPE_HIST a WHERE A.REFERRAL_SEQ_ID = REFERRAL.REFERRAL_SEQ_ID)"
    End If
    
    os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE"
    os_SQL = os_SQL & programIds & agencyIds & servicerIds & locationIds
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Referral_List = CCom.RunSQLReturnRS(os_SQL)
    
     Dim str As String
    str = os_SQL
    Call CCom.WriteToLog("Referral List")
    Call CCom.WriteToLog(str)
    
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

Public Sub Get_Referral_Counts(programIds As Variant, agencyIds As Variant, _
    servicerIds As Variant, locationIds As Variant, is_acctPrd1 As Variant, is_acctPrd2 As Variant, is_StartDate As Variant, _
    is_EndDate As Variant, is_ReportType As Variant, orderBy As Variant, ii_CCRCUserId As Variant, _
    is_UserID As Variant, ors_rs As Variant, ors_rs1 As Variant, ors_rs2 As Variant, _
    os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant)

    On Error GoTo errorHandler

    CCom.WriteToLog ("Method Called1")
    Call CreateFilters(programIds, agencyIds, servicerIds, locationIds)
    CCom.WriteToLog ("Method Called2")
    Dim CallerID        As String
    Dim sFilter         As String
    CCom.WriteToLog ("Method Called3")
    CallerID = sModName & ":Get_Referral_Counts " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    CCom.WriteToLog ("Method Called4")
    If Len(is_StartDate) > 0 Then
      is_StartDate = CCom.ToCharDate(is_StartDate)
      is_EndDate = CCom.ToCharDate(is_EndDate)
      If UCase(is_ReportType) = "REFERRALS" Then
           'TO_DATE replace by CAST for MS SQL Compitability
          sFilter = sFilter & "REFERRAL_DATE >= convert(datetime,'" & is_StartDate & "',101)  AND REFERRAL_DATE <= convert(datetime,'" & is_EndDate & "',101)"
      Else
          'TO_DATE replace by CAST for MS SQL Compitability
          sFilter = sFilter & "REFERRAL_RESULT_DATE >= convert(datetime,'" & is_StartDate & "',101)  AND REFERRAL_RESULT_DATE <= convert(datetime,'" & is_EndDate & "',101)"
      End If
    ElseIf Len(is_acctPrd1) > 0 Then
      sFilter = "ACCT_PRD >= " & CCom.Scrub(is_acctPrd1) & " AND ACCT_PRD <= " & CCom.Scrub(is_acctPrd2)
    End If
    CCom.WriteToLog ("Method Called5")
    CCom.WriteToLog (sFilter)
    CCom.WriteToLog (ii_CCRCUserId)
    Dim objAdmin As CAdmin
    Dim sACL As String
    Set objAdmin = New CAdmin
    sACL = objAdmin.Get_User_ACL("report", ii_CCRCUserId, "", "", os_SQL, os_ReturnCode, os_Message)
    If Len(sFilter) > 0 Then
      sFilter = sFilter & " AND "
    End If
    If Len(sACL) > 0 Then
      sFilter = sFilter & sACL & " AND "
    End If
    'CCom.WriteToLog (sACL)
    
    os_SQL = "SELECT CASE BSN_ENTITY.HAS_CONTRACT_IND WHEN 'Y' THEN 'CONTRACTED SERVICERS' ELSE 'NON-CONTRACTED SERVICERS' END HAS_CONTRACT," & _
    " COUNT(1) COUNT" & _
    " FROM REFERRAL, REF_RESULT_TYPE_HIST, REFERRAL_RESULT_TYPE, BSN_ENTITY" & _
    " WHERE " & sFilter & _
    " REFERRAL.REFERRAL_SEQ_ID = REF_RESULT_TYPE_HIST.REFERRAL_SEQ_ID" & _
    " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE" & _
    " AND REFERRAL.SERVICER_SEQ_ID = BSN_ENTITY.BSN_ENTITY_SEQ_ID"
    If "Referrals" = is_ReportType Then
        os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE = (SELECT MAX(A.REFERRAL_RESULT_DATE) FROM REF_RESULT_TYPE_HIST a WHERE A.REFERRAL_SEQ_ID = REFERRAL.REFERRAL_SEQ_ID)"
    End If
    os_SQL = os_SQL & programIds & agencyIds & servicerIds & locationIds & _
    " GROUP BY CASE BSN_ENTITY.HAS_CONTRACT_IND WHEN 'Y' THEN 'CONTRACTED SERVICERS' ELSE 'NON-CONTRACTED SERVICERS' END"
    Dim str As String
    str = os_SQL
    Call CCom.WriteToLog(str)
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set ors_rs = CCom.RunSQLReturnRS(os_SQL)
    
    os_SQL = "SELECT CASE BSN_ENTITY.HAS_CONTRACT_IND WHEN 'Y' THEN 'CONTRACTED SERVICERS' ELSE 'NON-CONTRACTED SERVICERS' END HAS_CONTRACT," & _
    " CASE REFERRAL_RESULT_TYPE.PAYABLE_IND WHEN 'Y' THEN 'ACTION PLANS' ELSE 'OTHER' END  BILLABLE," & _
    " COUNT(1) COUNT" & _
    " FROM REFERRAL, REF_RESULT_TYPE_HIST, REFERRAL_RESULT_TYPE, BSN_ENTITY" & _
    " WHERE " & sFilter & _
    " REFERRAL.REFERRAL_SEQ_ID = REF_RESULT_TYPE_HIST.REFERRAL_SEQ_ID" & _
    " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE" & _
    " AND REFERRAL.SERVICER_SEQ_ID = BSN_ENTITY.BSN_ENTITY_SEQ_ID"
    If "Referrals" = is_ReportType Then
        os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE = (SELECT MAX(A.REFERRAL_RESULT_DATE) FROM REF_RESULT_TYPE_HIST a WHERE A.REFERRAL_SEQ_ID = REFERRAL.REFERRAL_SEQ_ID)"
    End If
    os_SQL = os_SQL & programIds & agencyIds & servicerIds & locationIds & _
    " GROUP BY CASE BSN_ENTITY.HAS_CONTRACT_IND WHEN 'Y' THEN 'CONTRACTED SERVICERS' ELSE 'NON-CONTRACTED SERVICERS' END," & _
    " CASE REFERRAL_RESULT_TYPE.PAYABLE_IND WHEN 'Y' THEN 'ACTION PLANS' ELSE 'OTHER' END"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    str = os_SQL
    Call CCom.WriteToLog(str)
    Set ors_rs1 = CCom.RunSQLReturnRS(os_SQL)
    
    os_SQL = "SELECT  CASE BSN_ENTITY.HAS_CONTRACT_IND WHEN 'Y' THEN 'CONTRACTED SERVICERS' ELSE 'NON-CONTRACTED SERVICERS' END  HAS_CONTRACT," & _
    " CASE REFERRAL_RESULT_TYPE.PAYABLE_IND WHEN 'Y' THEN 'ACTION PLANS' ELSE 'OTHER' END BILLABLE," & _
    " REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE," & _
    " REFERRAL_RESULT_TYPE.DSCR," & _
    " COUNT(1) COUNT" & _
    " FROM REFERRAL, REF_RESULT_TYPE_HIST, REFERRAL_RESULT_TYPE, BSN_ENTITY" & _
    " WHERE " & sFilter & _
    " REFERRAL.REFERRAL_SEQ_ID = REF_RESULT_TYPE_HIST.REFERRAL_SEQ_ID" & _
    " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE" & _
    " AND REFERRAL.SERVICER_SEQ_ID = BSN_ENTITY.BSN_ENTITY_SEQ_ID"
    If "Referrals" = is_ReportType Then
        os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE = (SELECT MAX(A.REFERRAL_RESULT_DATE) FROM REF_RESULT_TYPE_HIST a WHERE A.REFERRAL_SEQ_ID = REFERRAL.REFERRAL_SEQ_ID)"
    End If
    os_SQL = os_SQL & programIds & agencyIds & servicerIds & locationIds & _
    " GROUP BY  CASE BSN_ENTITY.HAS_CONTRACT_IND WHEN 'Y' THEN 'CONTRACTED SERVICERS' ELSE 'NON-CONTRACTED SERVICERS' END," & _
    " CASE REFERRAL_RESULT_TYPE.PAYABLE_IND WHEN 'Y' THEN 'ACTION PLANS' ELSE 'OTHER' END," & _
    " REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE, DSCR"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set ors_rs2 = CCom.RunSQLReturnRS(os_SQL)
    str = os_SQL
    Call CCom.WriteToLog(str)
    
    os_ReturnCode = 0
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Sub

errorHandler:
  CCom.SetError
    os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
    os_ReturnCode = -1
    On Error Resume Next
    Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
    Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Sub

Public Function Get_Referral_Filter_Data(is_AcctPrd As Variant, is_StartDate As Variant, _
    is_EndDate As Variant, is_ReportType As Variant, ii_CCRCUserId As Variant, _
    is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    Dim CallerID        As String
    Dim sFilter         As String
    Dim arProgramIds     As Variant
    Dim arAgencyIds     As Variant
    Dim arServicerIds   As Variant
    Dim arLocationIds     As Variant
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Referral_Filter_Data " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    If Len(is_StartDate) > 0 Then
      is_StartDate = CCom.ToCharDate(is_StartDate)
      is_EndDate = CCom.ToCharDate(is_EndDate)
      If UCase(is_ReportType) = "REFERRALS" Then
          'sFilter = sFilter & "REFERRAL_DATE >= TO_DATE('" & is_StartDate & "','MM/DD/YYYY') AND REFERRAL_DATE <= TO_DATE('" & is_EndDate & "','MM/DD/YYYY')"
            sFilter = sFilter & "REFERRAL_DATE >= convert(datetime,'" & is_StartDate & "',101) AND REFERRAL_DATE <= convert(datetime,'" & is_EndDate & "',101)"
      Else
          'sFilter = sFilter & "REFERRAL_RESULT_DATE >= TO_DATE('" & is_StartDate & "','MM/DD/YYYY') AND REFERRAL_RESULT_DATE <= TO_DATE('" & is_EndDate & "','MM/DD/YYYY')"
          sFilter = sFilter & "REFERRAL_DATE >= convert(datetime,'" & is_StartDate & "',101) AND REFERRAL_RESULT_DATE <= convert(datetime,'" & is_EndDate & "',101)"

      End If
    ElseIf Len(is_AcctPrd) > 0 Then
      sFilter = "ACCT_PRD = " & CCom.Scrub(is_AcctPrd)
    End If
    
    Dim objAdmin As CAdmin
    Dim sACL As String
    Set objAdmin = New CAdmin
    sACL = objAdmin.Get_User_ACL("report", ii_CCRCUserId, "", "", os_SQL, os_ReturnCode, os_Message)
    If Len(sFilter) > 0 Then
      sFilter = sFilter & " AND "
    End If
    If Len(sACL) > 0 Then
      sFilter = sFilter & sACL & " AND "
    End If
    
    os_SQL = "SELECT /*+ CHOOSE */ REFERRAL.*, PROGRAM.PROGRAM_SEQ_ID, PROGRAM.PROGRAM_NAME, PROGRAM.ALLOW_NONCONTRACTED_SVCR_IND,"
    os_SQL = os_SQL & " AGENCY.BSN_ENTITY_SEQ_ID AGENCY_ID, AGENCY.BSN_ENTITY_NAME AGENCY, AGENCY.PHONE,"
    os_SQL = os_SQL & " SERVICER.BSN_ENTITY_SEQ_ID SERVICER_ID, SERVICER.BSN_ENTITY_NAME SERVICER, SERVICER.HAS_CONTRACT_IND,"
    os_SQL = os_SQL & " REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE, REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE,"
    os_SQL = os_SQL & " REFERRAL_RESULT_TYPE.DSCR REFERRAL_RESULT, REFERRAL_RESULT_TYPE.PAYABLE_IND,"
    os_SQL = os_SQL & " PROFILE.LAST_NAME LAST_NAME, PROFILE.FIRST_NAME FIRST_NAME, PROFILE.MID_INIT_NAME, PROFILE.NAME_SUFFIX_CODE, PROFILE.PRIMARY_PHN,"
    os_SQL = os_SQL & " BSN_ENTITY_LOCTN.BSN_ENTITY_LOCTN_SEQ_ID, BSN_ENTITY_LOCTN.LOCTN_NAME,"
    os_SQL = os_SQL & " DECODE(SERVICER.HAS_CONTRACT_IND,'N','NON-CONTRACTED SERVICER LOCATION',SERVICER.BSN_ENTITY_NAME || ' - ' || BSN_ENTITY_LOCTN.LOCTN_NAME) SERVICER_LOCATION,"
    os_SQL = os_SQL & " DECODE(SERVICER.HAS_CONTRACT_IND,'N','NON-CONTRACTED SERVICER',SERVICER.BSN_ENTITY_NAME) SERVICER_GROUPING_NAME,"
    os_SQL = os_SQL & " DECODE(SERVICER.HAS_CONTRACT_IND,'N','NCS',SERVICER.BSN_ENTITY_SEQ_ID) SERVICER_GROUPING_ID"
    
    os_SQL = os_SQL & " FROM REFERRAL, PROGRAM, REF_RESULT_TYPE_HIST, REFERRAL_RESULT_TYPE,"
    os_SQL = os_SQL & " BSN_ENTITY AGENCY, BSN_ENTITY SERVICER, BSN_ENTITY_LOCTN, CCRC_USER, PROFILE"
    
    os_SQL = os_SQL & " WHERE " & sFilter
    os_SQL = os_SQL & " REFERRAL.PROGRAM_SEQ_ID = PROGRAM.PROGRAM_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.AGENCY_SEQ_ID = AGENCY.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_SEQ_ID = SERVICER.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_LOCTN_SEQ_ID = BSN_ENTITY_LOCTN.BSN_ENTITY_LOCTN_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.COUNSELOR_SEQ_ID = CCRC_USER.CCRC_USER_SEQ_ID"
    os_SQL = os_SQL & " AND CCRC_USER.NT_DOMAIN = PROFILE.NT_DOMAIN"
    os_SQL = os_SQL & " AND CCRC_USER.NT_USERID = PROFILE.NT_USERID"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_SEQ_ID = REF_RESULT_TYPE_HIST.REFERRAL_SEQ_ID"
    os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE = "
    os_SQL = os_SQL & " (SELECT MAX(A.REFERRAL_RESULT_DATE) FROM REF_RESULT_TYPE_HIST A"
    os_SQL = os_SQL & " WHERE A.REFERRAL_SEQ_ID = REF_RESULT_TYPE_HIST.REFERRAL_SEQ_ID)"
    os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE"
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Referral_Filter_Data = CCom.RunSQLReturnRS(os_SQL)
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

Public Function Get_LookupValues(is_TableName As Variant, is_OrderBy As Variant, _
    is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As adodb.Recordset

    Dim CallerID      As String
    
    On Error GoTo errorHandler
    CallerID = "mGlobal:Get_LookupValues " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT * FROM " & is_TableName
    If Len(is_OrderBy) > 0 Then
        os_SQL = os_SQL & " ORDER BY " & is_OrderBy
    End If
      Dim str As String
    str = os_SQL
    Call CCom.WriteToLog("Get_LookupValues")
    Call CCom.WriteToLog(str)
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_LookupValues = CCom.RunSQLReturnRS(os_SQL)
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

Private Function CCRC_Referral_SQL(ii_LoanId As Variant, ii_ReferralId As Variant, is_Zip As Variant, sACL As Variant) As String
    On Error GoTo errorHandler
    
    Dim os_SQL As String
    
    os_SQL = os_SQL & "select /*+ CHOOSE */"
    os_SQL = os_SQL & " referral.*"
    os_SQL = os_SQL & " , rst.dscr referral_source_type"
    os_SQL = os_SQL & " , p.program_name"
    os_SQL = os_SQL & " , agency.bsn_entity_name"
    os_SQL = os_SQL & " , counselor.LAST_NAME COUNSELOR_LAST_NAME, counselor.FIRST_NAME COUNSELOR_FIRST_NAME, counselor.MID_INIT_NAME COUNSELOR_MID_INIT_NAME"
    os_SQL = os_SQL & " , counselor.PRIMARY_EMAIL_ADDR, counselor.NAME_SUFFIX_CODE COUNSELOR_NAME_SUFFIX_CODE, counselor.PRIMARY_PHN"
    os_SQL = os_SQL & " , agency.BSN_ENTITY_NAME, agency.PHONE"
    os_SQL = os_SQL & " , SERVICER.BSN_ENTITY_NAME SERVICER"
    os_SQL = os_SQL & " , SERVICER.CONTACT_NAME SERVICER_CONTACT_NAME"
    os_SQL = os_SQL & " , SERVICER.CONTACT_EMAIL SERVICER_CONTACT_EMAIL"
    os_SQL = os_SQL & " , fmt.dscr first_mortgage_type"
    os_SQL = os_SQL & " , rt.dscr referral_type"
    os_SQL = os_SQL & " , fcst.dscr first_contact_status_type"
    os_SQL = os_SQL & " , rrth.referral_result_date"
    os_SQL = os_SQL & " , rrt.dscr referral_result_type"
'    os_SQL = os_SQL & " , CASE r.contacted_servicer_ind WHEN 'U' THEN 'Unknown' WHEN 'Y' THEN 'Yes' ELSE 'No' END contacted_servicer"
    os_SQL = os_SQL & " , referral.contacted_servicer_ind contacted_servicer"
    os_SQL = os_SQL & " , cdt.dscr counseling_duration_type"
    os_SQL = os_SQL & " , mnit.dscr mthly_net_income_type"
    os_SQL = os_SQL & " , met.dscr mthly_expense_type"
    os_SQL = os_SQL & " , pt.dscr priority_type"
    os_SQL = os_SQL & " , race_type.dscr race_type"
    os_SQL = os_SQL & " , pdr.dscr primary_default_reason"
    os_SQL = os_SQL & " , sdr.dscr secondary_default_reason"
    
    os_SQL = os_SQL & " , sso.dscr SUMMARY_SENT_OTHER_TYPE"
    os_SQL = os_SQL & " , ie.dscr income_earners_type"
    os_SQL = os_SQL & " , ic.dscr income_category_type"
    os_SQL = os_SQL & " , ist.dscr intake_score_type"
    os_SQL = os_SQL & " , h.dscr household_type"
    os_SQL = os_SQL & " , g.dscr gender_type"
    os_SQL = os_SQL & " , s2.bsn_entity_name second_servicer_name"
    os_SQL = os_SQL & " , fmt2.dscr second_mortgage_type"
    os_SQL = os_SQL & " , fcst2.dscr second_loan_status_type"
    os_SQL = os_SQL & " , hot.dscr hud_outcome_type"
    os_SQL = os_SQL & " , htrt.dscr hud_term_reason_type"

    
    os_SQL = os_SQL & " from program p"
    os_SQL = os_SQL & " , bsn_entity agency"
    os_SQL = os_SQL & " , profile counselor"
    os_SQL = os_SQL & " , bsn_entity servicer"
    os_SQL = os_SQL & " , ccrc_user cu"
    
    os_SQL = os_SQL & " , REFERRAL"
    os_SQL = os_SQL & " LEFT OUTER JOIN REFERRAL_SOURCE_TYPE rst ON (REFERRAL.referral_source_type_code = rst.referral_source_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_MORTGAGE_TYPE fmt ON (REFERRAL.first_mortgage_type_code = fmt.first_mortgage_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN REFERRAL_TYPE rt ON (REFERRAL.referral_type_code = rt.referral_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_CONTACT_STATUS_TYPE fcst ON (REFERRAL.first_contact_status_type_code = fcst.first_contact_status_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN COUNSELING_DURATION_TYPE cdt ON (REFERRAL.counseling_duration_type_code = cdt.counseling_duration_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN PRIORITY_TYPE pt ON (REFERRAL.PRIORITY_TYPE_CODE = pt.PRIORITY_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN MTHLY_NET_INCOME_TYPE mnit ON (REFERRAL.MTHLY_NET_INCOME_TYPE_CODE = mnit.MTHLY_NET_INCOME_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN (REF_RESULT_TYPE_HIST rrth"
    os_SQL = os_SQL & "   LEFT OUTER JOIN REFERRAL_RESULT_TYPE rrt ON (rrth.REFERRAL_RESULT_TYPE_CODE = rrt.REFERRAL_RESULT_TYPE_CODE))"
    os_SQL = os_SQL & "   ON (REFERRAL.REFERRAL_SEQ_ID = rrth.REFERRAL_SEQ_ID)"
    os_SQL = os_SQL & " LEFT OUTER JOIN MTHLY_EXPENSE_TYPE met ON (REFERRAL.MTHLY_EXPENSE_TYPE_CODE = met.MTHLY_EXPENSE_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN DEFAULT_REASON_TYPE pdr ON (REFERRAL.PRIMARY_DFLT_RSN_TYPE_CODE = pdr.DEFAULT_REASON_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN DEFAULT_REASON_TYPE sdr ON (REFERRAL.SECONDARY_DFLT_RSN_TYPE_CODE = sdr.DEFAULT_REASON_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN RACE_TYPE ON (REFERRAL.RACE_TYPE_CODE = RACE_TYPE.RACE_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN SUMMARY_SENT_OTHER_TYPE sso ON (REFERRAL.SUMMARY_SENT_OTHER_TYPE_CODE = sso.SUMMARY_SENT_OTHER_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN INCOME_EARNERS_TYPE ie ON (REFERRAL.INCOME_EARNERS_TYPE_CODE = ie.INCOME_EARNERS_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN INTAKE_SCORE_TYPE ist ON (REFERRAL.INTAKE_SCORE_TYPE_CODE = ist.INTAKE_SCORE_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN INCOME_CATEGORY_TYPE ic ON (REFERRAL.INCOME_CATEGORY_TYPE_CODE = ic.INCOME_CATEGORY_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN HOUSEHOLD_TYPE h ON (REFERRAL.HOUSEHOLD_TYPE_CODE = h.HOUSEHOLD_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN GENDER_TYPE g ON (REFERRAL.GENDER_TYPE_CODE = g.GENDER_TYPE_CODE)"
    
    os_SQL = os_SQL & " LEFT OUTER JOIN BSN_ENTITY s2 ON (REFERRAL.SECOND_SERVICER_SEQ_ID = s2.BSN_ENTITY_SEQ_ID) "
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_MORTGAGE_TYPE fmt2 ON (REFERRAL.SECOND_MORTGAGE_TYPE_CODE = fmt2.FIRST_MORTGAGE_TYPE_CODE) "
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_CONTACT_STATUS_TYPE fcst2 ON (REFERRAL.SECOND_LOAN_STATUS_TYPE_CODE = fcst2.FIRST_CONTACT_STATUS_TYPE_CODE) "
    os_SQL = os_SQL & " LEFT OUTER JOIN HUD_OUTCOME_TYPE hot ON (REFERRAL.HUD_OUTCOME_TYPE_CODE = hot.HUD_OUTCOME_TYPE_CODE) "
    os_SQL = os_SQL & " LEFT OUTER JOIN HUD_TERM_REASON_TYPE htrt ON (REFERRAL.HUD_TERM_REASON_TYPE_CODE = htrt.HUD_TERM_REASON_TYPE_CODE) "
    
    If ii_ReferralId <> "" And ii_ReferralId <> "0" Then
      os_SQL = os_SQL & " WHERE referral.REFERRAL_SEQ_ID = " & ii_ReferralId
    Else
      os_SQL = os_SQL & " WHERE referral.LOAN_ID = '" & ii_LoanId & "'"
      os_SQL = os_SQL & " AND referral.ZIP = '" & is_Zip & "'"
    End If
    
    If Len(sACL) > 0 Then
      os_SQL = os_SQL & " AND " & sACL
    End If
    
    os_SQL = os_SQL & " AND referral.AGENCY_SEQ_ID = agency.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND referral.SERVICER_SEQ_ID = SERVICER.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND referral.COUNSELOR_SEQ_ID = cu.CCRC_USER_SEQ_ID"
    os_SQL = os_SQL & " and referral.program_seq_id = p.program_seq_id"
    os_SQL = os_SQL & " AND cu.NT_DOMAIN = counselor.NT_DOMAIN"
    os_SQL = os_SQL & " AND cu.NT_USERID = counselor.NT_USERID"
    
    CCRC_Referral_SQL = os_SQL
    
Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
  CCom.SetError
    On Error Resume Next
    Call CCom.WriteToLog("CCRC_Referral_SQL: " & Err.Description, 2)
    Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, "CCRC_Referral_SQL", Err.Description
End Function

Private Function DW_Referral_SQL(ii_LoanId As Variant, is_Zip As Variant) As String
    On Error GoTo errorHandler
    
    Dim os_SQL As String
    
    os_SQL = "SELECT /*+ DRIVING_SITE(DW_LOAN_PROPERTY) */ "
    os_SQL = os_SQL & vbCrLf & " DW_LOAN_BORROWER.LAST_NAME, SUBSTR(DW_LOAN_BORROWER.MI_NAME,1,1) MI_NAME, DW_LOAN_BORROWER.FIRST_NAME,"
    os_SQL = os_SQL & " DW_PR_SVC_LOAN_ID_XREF_VF.DW_PR_SVC_VARCHAR_LOAN_ID LOAN_ID,"
    os_SQL = os_SQL & vbCrLf & " DW_LOAN_PROPERTY.ADDR_LINE_1, DW_LOAN_PROPERTY.CITY, DW_LOAN_PROPERTY.STATE, DW_LOAN_PROPERTY.ZIP,"
    os_SQL = os_SQL & vbCrLf & " 'Y' INTERNAL_IND, 'N' DUPE_IND"
    os_SQL = os_SQL & vbCrLf & " FROM DW_PR_SVC_LOAN_ID_XREF_VF, DW_LOAN_BORROWER, DW_LOAN_PROPERTY"
    os_SQL = os_SQL & vbCrLf & " WHERE DW_PR_SVC_LOAN_ID_XREF_VF.DW_LOAN_ID = DW_LOAN_BORROWER.DW_LOAN_ID "
    os_SQL = os_SQL & vbCrLf & " AND DW_PR_SVC_LOAN_ID_XREF_VF.DW_LOAN_ID = DW_LOAN_PROPERTY.DW_LOAN_ID "
    os_SQL = os_SQL & " AND DW_PR_SVC_LOAN_ID_XREF_VF.DW_PR_SVC_VARCHAR_LOAN_ID = '" & ii_LoanId & "'"
    os_SQL = os_SQL & " AND DW_LOAN_PROPERTY.ZIP = '" & is_Zip & "'"
    os_SQL = os_SQL & " AND DW_LOAN_BORROWER.PRIMARY_IND='Y'"
    
    DW_Referral_SQL = os_SQL

Exit_Function:
    Call CCom.CtxSetComplete
    Exit Function

errorHandler:
  CCom.SetError
    On Error Resume Next
    Call CCom.WriteToLog("DW_Referral_SQL: " & Err.Description, 2)
    Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, "DW_Referral_SQL", Err.Description
End Function

Private Sub CreateFilters(programIds As Variant, agencyIds As Variant, _
    servicerIds As Variant, locationIds As Variant)
    
  On Error GoTo errhandler
  Err.Clear
  Dim sTemp As String
  Dim i As Integer
  
  If Len(programIds) > 0 Then
    'programIds = Split(programIds, ",")
    'sTemp = " AND REFERRAL.PROGRAM_SEQ_ID IN ('" & programIds(0) & "'"
    'For i = 1 To UBound(programIds)
     ' sTemp = sTemp & ",'" & programIds(i) & "'"
    'Next i
    'sTemp = sTemp & ")"
    'programIds = sTemp
    programIds = " AND REFERRAL.PROGRAM_SEQ_ID IN (" & programIds & ")"
  End If
  
  If Len(agencyIds) > 0 Then
'    agencyIds = Split(agencyIds, ",")
'    sTemp = " AND REFERRAL.AGENCY_SEQ_ID IN ('" & agencyIds(0) & "'"
'    For i = 1 To UBound(agencyIds)
'      sTemp = sTemp & ",'" & agencyIds(i) & "'"
'    Next i
'    sTemp = sTemp & ")"
'    agencyIds = sTemp
  agencyIds = " AND REFERRAL.AGENCY_SEQ_ID IN (" & agencyIds & ")"
  End If
  
  If Len(servicerIds) > 0 Then
'    servicerIds = Split(servicerIds, ",")
'    sTemp = " AND REFERRAL.SERVICER_SEQ_ID IN ('" & servicerIds(0) & "'"
'    For i = 1 To UBound(servicerIds)
'      sTemp = sTemp & ",'" & servicerIds(i) & "'"
'    Next i
'    sTemp = sTemp & ")"
'    servicerIds = sTemp
    servicerIds = " AND REFERRAL.SERVICER_SEQ_ID IN (" & servicerIds & ")"
  End If
  
  If Len(locationIds) > 0 Then
'    locationIds = Split(locationIds, ",")
'    sTemp = " AND REFERRAL.SERVICER_LOCTN_SEQ_ID IN ('" & locationIds(0) & "'"
'    For i = 1 To UBound(locationIds)
'      sTemp = sTemp & ",'" & locationIds(i) & "'"
'    Next i
'    sTemp = sTemp & ")"
'    locationIds = sTemp
    locationIds = " AND REFERRAL.SERVICER_LOCTN_SEQ_ID IN (" & locationIds & ")"
  End If
  Exit Sub
  
errhandler:
  CCom.SetError
    On Error Resume Next
    Call CCom.WriteToLog("CreateFilters: " & Err.Description, 2)
    Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, "CreateFilters", Err.Description
End Sub

Public Function test() As Variant
  test = "Success"
End Function

Public Function Get_Referral_List_Excel(programIds As Variant, agencyIds As Variant, _
    servicerIds As Variant, locationIds As Variant, is_acctPrd1 As Variant, is_acctPrd2 As Variant, is_StartDate As Variant, _
    is_EndDate As Variant, is_ReportType As Variant, is_OutcomeType As Variant, _
    hasContract As Variant, actionPlan As Variant, _
    ii_CCRCUserId As Variant, is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    On Error GoTo errorHandler

    Dim CallerID        As String
    Dim sFilter         As String
        
    Call CreateFilters(programIds, agencyIds, servicerIds, locationIds)
    Call CCom.WriteToLog("CreateFilters: " & programIds)
    
    CallerID = sModName & ":Get_Referral_List_Excel " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    If Len(is_StartDate) > 0 Then
      is_StartDate = CCom.ToCharDate(is_StartDate)
      is_EndDate = CCom.ToCharDate(is_EndDate)
      If UCase(is_ReportType) = "REFERRALS" Then
          'sFilter = sFilter & "REFERRAL_DATE >= TO_DATE('" & is_StartDate & "','MM/DD/YYYY') AND REFERRAL_DATE <= TO_DATE('" & is_EndDate & "','MM/DD/YYYY')"
          'TO_DATE replace by CAST for MS SQL Compitability
          sFilter = sFilter & "REFERRAL_DATE >=  convert(datetime,'" & is_StartDate & "',101) AND REFERRAL_DATE <= convert(datetime,'" & is_EndDate & "',101)"
      Else
          'sFilter = sFilter & "REFERRAL_RESULT_DATE >= TO_DATE('" & is_StartDate & "','MM/DD/YYYY') AND REFERRAL_RESULT_DATE <= TO_DATE('" & is_EndDate & "','MM/DD/YYYY')"
          'TO_DATE replace by CAST for MS SQL Compitability
           sFilter = sFilter & "REFERRAL_RESULT_DATE >= convert(datetime,'" & is_StartDate & "',101) AND REFERRAL_RESULT_DATE <= convert(datetime,'" & is_EndDate & "',101)"
      End If
    ElseIf Len(is_acctPrd1) > 0 Then
      sFilter = "ACCT_PRD >= " & CCom.Scrub(is_acctPrd1) & " AND ACCT_PRD <= " & CCom.Scrub(is_acctPrd2)
    End If
    
    Dim objAdmin As CAdmin
    Dim sACL As String
    Set objAdmin = New CAdmin
    sACL = objAdmin.Get_User_ACL("report", ii_CCRCUserId, "", "", os_SQL, os_ReturnCode, os_Message)
    If Len(sFilter) > 0 Then
      sFilter = sFilter & " AND "
    End If
    If Len(sACL) > 0 Then
      sFilter = sFilter & sACL & " AND "
    End If
    
    If Len(is_OutcomeType) > 0 Then
      sFilter = sFilter & " REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = '" & is_OutcomeType & "' AND "
    End If
    
    If Len(hasContract) > 0 Then
      If hasContract = "CONTRACTED SERVICERS" Then
        sFilter = sFilter & " SERVICER.HAS_CONTRACT_IND = 'Y' AND"
      Else
        sFilter = sFilter & " SERVICER.HAS_CONTRACT_IND = 'N' AND "
      End If
    End If
    
    If Len(actionPlan) > 0 Then
      If actionPlan = "ACTION PLANS" Then
        sFilter = sFilter & " PAYABLE_IND = 'Y' AND"
      Else
        sFilter = sFilter & " PAYABLE_IND = 'N' AND"
      End If
    End If
    
    os_SQL = "SELECT /*+ CHOOSE */ REFERRAL.REFERRAL_SEQ_ID, LOAN_ID, REFERRAL.LAST_NAME, REFERRAL.FIRST_NAME, ADDR_LINE_1, CITY, STATE, ZIP, PHN, OCCUPANTS,"
    os_SQL = os_SQL & " REFERRAL_DATE, REFERRAL_TYPE.DSCR REFERRAL_TYPE, REFERRAL_SOURCE_TYPE.DSCR REFERRAL_SOURCE_TYPE, FIRST_CONTACT_STATUS_TYPE.DSCR FIRST_CONTACT_STATUS_TYPE,"
    os_SQL = os_SQL & " PROGRAM_NAME, SERVICER.BSN_ENTITY_NAME [First_Mortgage_Servicer], BSN_ENTITY_LOCTN.LOCTN_NAME, CONTACTED_SERVICER_IND, "
    os_SQL = os_SQL & " DISCUSSED_SOLUTION_WITH_SRVCR_IND , WORKED_WITH_ANOTHER_AGENCY_IND, "
    os_SQL = os_SQL & " APPROVED_BY, PAID_DATE, CHECK_NBR, PAID_AMT, DUPE_IND, INVOICE_DATE,"
    os_SQL = os_SQL & " REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE, REFERRAL_RESULT_TYPE.DSCR REFERRAL_RESULT,"
    os_SQL = os_SQL & " SUMMARY_RPT_SENT_DATE, AGENCY.BSN_ENTITY_NAME AGENCY, AGENCY_REFERRAL_ID,"
    os_SQL = os_SQL & " PROFILE.LAST_NAME COUNSELOR_LAST_NAME, PROFILE.FIRST_NAME COUNSELOR_FIRST_NAME,"
    os_SQL = os_SQL & " SUCCESS_STORY_IND, REFERRAL_RESULT_TYPE.PAYABLE_IND, SERVICER.HAS_CONTRACT_IND, REF_RESULT_TYPE_HIST.ACCT_PRD,"
    os_SQL = os_SQL & " INITIAL_FICO, INITIAL_FICO_DATE, CURRENT_FICO, CURRENT_FICO_DATE,"
    os_SQL = os_SQL & " COUNSELING_DURATION_TYPE.DSCR COUNSELING_DURATION_TYPE, MTHLY_NET_INCOME_TYPE.DSCR MTHLY_NET_INCOME_TYPE, MTHLY_EXPENSE_TYPE.DSCR MTHLY_EXPENSE_TYPE,"
    os_SQL = os_SQL & " FIRST_MORTGAGE_TYPE.DSCR FIRST_MORTGAGE_TYPE,"
    os_SQL = os_SQL & " credit_score, mothers_maiden_name, privacy_consent_ind, priority_type.dscr priority_type,"
    os_SQL = os_SQL & " secondary_contact_number, email_addr, "
    
    os_SQL = os_SQL & " S2.BSN_ENTITY_NAME SECOND_MOTGAGE_SERVICER, "
    os_SQL = os_SQL & " FMT2.DSCR SECOND_MORTGAGE_TYPE, "
    os_SQL = os_SQL & " REFERRAL.SECOND_LOAN_ID,"
    os_SQL = os_SQL & " REFERRAL.SECOND_PITI_AMT, "
    os_SQL = os_SQL & " FCST2.DSCR SECOND_LOAN_STATUS_TYPE, "
    os_SQL = os_SQL & " HOT.DSCR HUD_OUTCOME_TYPE, "
    os_SQL = os_SQL & " HTRT.DSCR HUD_TERM_REASON_TYPE, "
    os_SQL = os_SQL & " REFERRAL.HUD_TERM_DATE, "


    os_SQL = os_SQL & " bankruptcy_ind , bankruptcy_attorney_name, owner_occupied_ind, "
    os_SQL = os_SQL & " HISPANIC_IND, RACE_TYPE.DSCR RACE_TYPE, PRIMARY_RSN.DSCR 'PRIMARY', SECONDARY_RSN.DSCR SECONDARY,"
    os_SQL = os_SQL & " RESULT1, RESULT2, RESULT3,"
    os_SQL = os_SQL & "'" & is_StartDate & "' START_DATE,"
    os_SQL = os_SQL & "'" & is_EndDate & "' END_DATE"
    
    os_SQL = os_SQL & " , sso.dscr SUMMARY_SENT_OTHER_TYPE"
    os_SQL = os_SQL & " , ie.dscr income_earners_type"
    os_SQL = os_SQL & " , ic.dscr income_category_type"
    os_SQL = os_SQL & " , ist.dscr intake_score_type"
    os_SQL = os_SQL & " , h.dscr household_type"
    os_SQL = os_SQL & " , g.dscr gender_type"
    os_SQL = os_SQL & ",REFERRAL.AGE,REFERRAL.HOUSEHOLD_INCOME_AMT,REFERRAL.PITI_AMT,REFERRAL.ARM_RESET_IND,REFERRAL.PROPERTY_FOR_SALE_IND,REFERRAL.LIST_PRICE_AMT"
    os_SQL = os_SQL & ",REFERRAL.REALTY_COMPANY_NAME,REFERRAL.FC_NOTICE_RECD_IND,REFERRAL.SUMMARY_SENT_OTHER_DATE"
    
    os_SQL = os_SQL & " FROM PROGRAM, REF_RESULT_TYPE_HIST, REFERRAL_RESULT_TYPE,"
    os_SQL = os_SQL & " BSN_ENTITY AGENCY, BSN_ENTITY SERVICER, BSN_ENTITY_LOCTN, REFERRAL_RESULTS_VW,"
    os_SQL = os_SQL & " CCRC_USER, PROFILE, REFERRAL_TYPE, FIRST_CONTACT_STATUS_TYPE, REFERRAL_SOURCE_TYPE,"
    os_SQL = os_SQL & " COUNSELING_DURATION_TYPE, FIRST_MORTGAGE_TYPE,"

    os_SQL = os_SQL & " REFERRAL"
    os_SQL = os_SQL & " LEFT OUTER JOIN MTHLY_NET_INCOME_TYPE ON (REFERRAL.MTHLY_NET_INCOME_TYPE_CODE = MTHLY_NET_INCOME_TYPE.MTHLY_NET_INCOME_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN MTHLY_EXPENSE_TYPE ON (REFERRAL.MTHLY_EXPENSE_TYPE_CODE = MTHLY_EXPENSE_TYPE.MTHLY_EXPENSE_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN PRIORITY_TYPE ON (REFERRAL.PRIORITY_TYPE_CODE = PRIORITY_TYPE.PRIORITY_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN DEFAULT_REASON_TYPE PRIMARY_RSN ON (REFERRAL.PRIMARY_DFLT_RSN_TYPE_CODE = PRIMARY_RSN.DEFAULT_REASON_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN DEFAULT_REASON_TYPE SECONDARY_RSN ON (REFERRAL.SECONDARY_DFLT_RSN_TYPE_CODE = SECONDARY_RSN.DEFAULT_REASON_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN RACE_TYPE ON (REFERRAL.RACE_TYPE_CODE = RACE_TYPE.RACE_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN SUMMARY_SENT_OTHER_TYPE sso ON (referral.SUMMARY_SENT_OTHER_TYPE_CODE = sso.SUMMARY_SENT_OTHER_TYPE_CODE) "
    os_SQL = os_SQL & " LEFT OUTER JOIN INCOME_EARNERS_TYPE ie ON (referral.INCOME_EARNERS_TYPE_CODE = ie.INCOME_EARNERS_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN INTAKE_SCORE_TYPE ist ON (referral.INTAKE_SCORE_TYPE_CODE = ist.INTAKE_SCORE_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN INCOME_CATEGORY_TYPE ic ON (referral.INCOME_CATEGORY_TYPE_CODE = ic.INCOME_CATEGORY_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN HOUSEHOLD_TYPE h ON (referral.HOUSEHOLD_TYPE_CODE = h.HOUSEHOLD_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN GENDER_TYPE g ON (referral.GENDER_TYPE_CODE = g.GENDER_TYPE_CODE)  "
    
    os_SQL = os_SQL & " LEFT OUTER JOIN BSN_ENTITY S2 ON (REFERRAL.SECOND_SERVICER_SEQ_ID = S2.BSN_ENTITY_SEQ_ID) "
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_MORTGAGE_TYPE FMT2 ON (REFERRAL.SECOND_MORTGAGE_TYPE_CODE = FMT2.FIRST_MORTGAGE_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_CONTACT_STATUS_TYPE FCST2 ON (REFERRAL.SECOND_LOAN_STATUS_TYPE_CODE = FCST2.FIRST_CONTACT_STATUS_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN HUD_OUTCOME_TYPE HOT ON (REFERRAL.HUD_OUTCOME_TYPE_CODE = HOT.HUD_OUTCOME_TYPE_CODE)  "
    os_SQL = os_SQL & " LEFT OUTER JOIN HUD_TERM_REASON_TYPE HTRT ON (REFERRAL.HUD_TERM_REASON_TYPE_CODE = HTRT.HUD_TERM_REASON_TYPE_CODE)  "

    os_SQL = os_SQL & " WHERE " & sFilter
    os_SQL = os_SQL & " REFERRAL.PROGRAM_SEQ_ID = PROGRAM.PROGRAM_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.AGENCY_SEQ_ID = AGENCY.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_SEQ_ID = SERVICER.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.SERVICER_LOCTN_SEQ_ID = BSN_ENTITY_LOCTN.BSN_ENTITY_LOCTN_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.COUNSELOR_SEQ_ID = CCRC_USER.CCRC_USER_SEQ_ID"
    os_SQL = os_SQL & " AND CCRC_USER.NT_DOMAIN = PROFILE.NT_DOMAIN"
    os_SQL = os_SQL & " AND CCRC_USER.NT_USERID = PROFILE.NT_USERID"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_SEQ_ID = REFERRAL_RESULTS_VW.REFERRAL_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_SEQ_ID = REF_RESULT_TYPE_HIST.REFERRAL_SEQ_ID"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_TYPE_CODE = REFERRAL_TYPE.REFERRAL_TYPE_CODE"
    os_SQL = os_SQL & " AND REFERRAL.FIRST_CONTACT_STATUS_TYPE_CODE = FIRST_CONTACT_STATUS_TYPE.FIRST_CONTACT_STATUS_TYPE_CODE"
    os_SQL = os_SQL & " AND REFERRAL.REFERRAL_SOURCE_TYPE_CODE = REFERRAL_SOURCE_TYPE.REFERRAL_SOURCE_TYPE_CODE"
    os_SQL = os_SQL & " AND REFERRAL.COUNSELING_DURATION_TYPE_CODE = COUNSELING_DURATION_TYPE.COUNSELING_DURATION_TYPE_CODE"
    os_SQL = os_SQL & " AND REFERRAL.FIRST_MORTGAGE_TYPE_CODE = FIRST_MORTGAGE_TYPE.FIRST_MORTGAGE_TYPE_CODE"

        
    'only get the lastest referral for the referral report
    If "Referrals" = is_ReportType Then
        os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_DATE = (SELECT MAX(A.REFERRAL_RESULT_DATE) FROM REF_RESULT_TYPE_HIST a WHERE A.REFERRAL_SEQ_ID = REFERRAL.REFERRAL_SEQ_ID)"
    End If
    
    os_SQL = os_SQL & " AND REF_RESULT_TYPE_HIST.REFERRAL_RESULT_TYPE_CODE = REFERRAL_RESULT_TYPE.REFERRAL_RESULT_TYPE_CODE"
    os_SQL = os_SQL & programIds & agencyIds & servicerIds & locationIds
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Referral_List_Excel = CCom.RunSQLReturnRS(os_SQL)
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

Public Function UpdateDateCounselingSummarySent(referralId As Variant, summaryRptSentDate As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":UpdateDateCounselingSummarySent " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
        
  os_SQL = "UPDATE REFERRAL SET SUMMARY_RPT_SENT_DATE = " & CCom.ScrubDate(summaryRptSentDate)
  os_SQL = os_SQL & "," & CCom.Audit_Fields(is_UserID, "UPDATESQL")
  os_SQL = os_SQL & " WHERE REFERRAL_SEQ_ID = " & referralId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  UpdateDateCounselingSummarySent = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  UpdateDateCounselingSummarySent = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Referrals_AutoEmail(ii_CCRCUserId As Variant, is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

   ' Dim CallerID        As String
   ' Dim sFilter         As String
    
   ' On Error GoTo errorHandler
   ' CallerID = sModName & ":Get_Referrals_AutoEmail " & vbTab & "(" & is_UserID & ")" & vbTab
   ' Call CCom.CtxSetAbort
   ' os_ReturnCode = -1
    
   ' os_SQL = os_SQL & "select /*+ CHOOSE */"
   ' os_SQL = os_SQL & " referral.*"
   ' os_SQL = os_SQL & " , rst.dscr referral_source_type"
   ' os_SQL = os_SQL & " , p.program_name"
   ' os_SQL = os_SQL & " , agency.bsn_entity_name"
   ' os_SQL = os_SQL & " , counselor.LAST_NAME COUNSELOR_LAST_NAME, counselor.FIRST_NAME COUNSELOR_FIRST_NAME, counselor.MID_INIT_NAME COUNSELOR_MID_INIT_NAME"
   ' os_SQL = os_SQL & " , counselor.PRIMARY_EMAIL_ADDR, counselor.NAME_SUFFIX_CODE COUNSELOR_NAME_SUFFIX_CODE, counselor.PRIMARY_PHN"
   ' os_SQL = os_SQL & " , agency.BSN_ENTITY_NAME, agency.PHONE"
   ' os_SQL = os_SQL & " , SERVICER.BSN_ENTITY_NAME SERVICER"
   ' os_SQL = os_SQL & " , SERVICER.CONTACT_NAME SERVICER_CONTACT_NAME"
   ' os_SQL = os_SQL & " , SERVICER.CONTACT_EMAIL SERVICER_CONTACT_EMAIL"
   ' os_SQL = os_SQL & " , fmt.dscr first_mortgage_type"
   ' os_SQL = os_SQL & " , rt.dscr referral_type"
   ' os_SQL = os_SQL & " , fcst.dscr first_contact_status_type"
''    os_SQL = os_SQL & " , rrth.referral_result_date"
''    os_SQL = os_SQL & " , rrt.dscr referral_result_type"
   ' os_SQL = os_SQL & " , referral.contacted_servicer_ind contacted_servicer"
   ' os_SQL = os_SQL & " , cdt.dscr counseling_duration_type"
   ' os_SQL = os_SQL & " , mnit.dscr mthly_net_income_type"
   ' os_SQL = os_SQL & " , met.dscr mthly_expense_type"
   ' os_SQL = os_SQL & " , pt.dscr priority_type"
   ' os_SQL = os_SQL & " , race_type.dscr race_type"
   ' os_SQL = os_SQL & " , pdr.dscr primary_default_reason"
   ' os_SQL = os_SQL & " , sdr.dscr secondary_default_reason"
    
   ' os_SQL = os_SQL & " , sso.dscr SUMMARY_SENT_OTHER_TYPE"
   ' os_SQL = os_SQL & " , ie.dscr income_earners_type"
   ' os_SQL = os_SQL & " , ic.dscr income_category_type"
   ' os_SQL = os_SQL & " , ist.dscr intake_score_type"
   ' os_SQL = os_SQL & " , h.dscr household_type"
   ' os_SQL = os_SQL & " , g.dscr gender_type"
    
   ' os_SQL = os_SQL & " from referral"
   ' os_SQL = os_SQL & " , program p"
   ' os_SQL = os_SQL & " , referral_source_type rst"
   ' os_SQL = os_SQL & " , bsn_entity agency"
   ' os_SQL = os_SQL & " , profile counselor"
    'os_SQL = os_SQL & " , bsn_entity servicer"
   ' os_SQL = os_SQL & " , first_mortgage_type fmt"
   ' os_SQL = os_SQL & " , referral_type rt"
   ' os_SQL = os_SQL & " , first_contact_status_type fcst"
' '   os_SQL = os_SQL & " , ref_result_type_hist rrth"
' '   os_SQL = os_SQL & " , referral_result_type rrt"
   ' os_SQL = os_SQL & " , counseling_duration_type cdt"
   ' os_SQL = os_SQL & " , priority_type pt"
   ' 'os_SQL = os_SQL & " , ccrc_priority_type pt"
   ' os_SQL = os_SQL & " , mthly_net_income_type mnit"
   ' os_SQL = os_SQL & " , mthly_expense_type met"
   ' os_SQL = os_SQL & " , ccrc_user cu"
   ' os_SQL = os_SQL & " , DEFAULT_REASON_TYPE pdr"
   ' os_SQL = os_SQL & " , DEFAULT_REASON_TYPE sdr"
   ' os_SQL = os_SQL & " , race_type"
    
   ' os_SQL = os_SQL & " , SUMMARY_SENT_OTHER_TYPE sso"
   ' os_SQL = os_SQL & " , INCOME_EARNERS_TYPE ie"
   ' os_SQL = os_SQL & " , INTAKE_SCORE_TYPE ist"
   ' os_SQL = os_SQL & " , INCOME_CATEGORY_TYPE ic"
   ' os_SQL = os_SQL & " , HOUSEHOLD_TYPE h"
   ' os_SQL = os_SQL & " , GENDER_TYPE g"
    
   ' os_SQL = os_SQL & " WHERE referral.PRIVACY_CONSENT_IND ='Y'"
   ' os_SQL = os_SQL & " AND (referral.DUPE_IND = 'N' OR referral.DUPE_IND IS NULL)"
   ' os_SQL = os_SQL & " AND referral.SUMMARY_RPT_SENT_DATE IS NULL"
   ' os_SQL = os_SQL & " AND referral.AGENCY_SEQ_ID = agency.BSN_ENTITY_SEQ_ID"
   ' os_SQL = os_SQL & " AND referral.SERVICER_SEQ_ID = SERVICER.BSN_ENTITY_SEQ_ID"
   ' 'os_SQL = os_SQL & " AND SERVICER.AUTO_EMAIL_IND = 'Y'"
   ' os_SQL = os_SQL & " AND referral.COUNSELOR_SEQ_ID = cu.CCRC_USER_SEQ_ID"
   ' os_SQL = os_SQL & " and referral.program_seq_id = p.program_seq_id"
   ' os_SQL = os_SQL & " and referral.referral_source_type_code = rst.referral_source_type_code" ' (+)"
   ' os_SQL = os_SQL & " and referral.first_mortgage_type_code = fmt.first_mortgage_type_code " ' (+)"
   ' os_SQL = os_SQL & " and referral.referral_type_code = rt.referral_type_code " ' (+)"
   ' os_SQL = os_SQL & " and referral.first_contact_status_type_code = fcst.first_contact_status_type_code " ' (+)"
   ' os_SQL = os_SQL & " and referral.counseling_duration_type_code = cdt.counseling_duration_type_code " ' (+)"
   ' os_SQL = os_SQL & " and referral.priority_type_code = pt.priority_type_code " ' (+)"
   ' os_SQL = os_SQL & " and referral.mthly_net_income_type_code = mnit.mthly_net_income_type_code " ' (+)"
   ' os_SQL = os_SQL & " AND cu.NT_DOMAIN = counselor.NT_DOMAIN"
   ' os_SQL = os_SQL & " AND cu.NT_USERID = counselor.NT_USERID"
' '   os_SQL = os_SQL & " AND referral.REFERRAL_SEQ_ID = rrth.REFERRAL_SEQ_ID "' (+)"
''    os_SQL = os_SQL & " AND rrth.REFERRAL_RESULT_TYPE_CODE = rrt.REFERRAL_RESULT_TYPE_CODE "' (+)"
   ' os_SQL = os_SQL & " AND referral.MTHLY_EXPENSE_TYPE_CODE = met.MTHLY_EXPENSE_TYPE_CODE " ' (+)"
    'os_SQL = os_SQL & " AND referral.PRIMARY_DFLT_RSN_TYPE_CODE = pdr.DEFAULT_REASON_TYPE_CODE " ' (+)"
   ' os_SQL = os_SQL & " AND referral.SECONDARY_DFLT_RSN_TYPE_CODE = sdr.DEFAULT_REASON_TYPE_CODE " ' (+)"
    'os_SQL = os_SQL & " AND referral.RACE_TYPE_CODE = RACE_TYPE.RACE_TYPE_CODE " ' (+)"
    
    'os_SQL = os_SQL & " AND referral.SUMMARY_SENT_OTHER_TYPE_CODE = sso.SUMMARY_SENT_OTHER_TYPE_CODE " ' (+)"
    'os_SQL = os_SQL & " AND referral.INCOME_EARNERS_TYPE_CODE = ie.INCOME_EARNERS_TYPE_CODE " ' (+)"
   ' os_SQL = os_SQL & " AND referral.INTAKE_SCORE_TYPE_CODE = ist.INTAKE_SCORE_TYPE_CODE " ' (+)"
   ' os_SQL = os_SQL & " AND referral.INCOME_CATEGORY_TYPE_CODE = ic.INCOME_CATEGORY_TYPE_CODE " ' (+)"
   ' os_SQL = os_SQL & " AND referral.HOUSEHOLD_TYPE_CODE = h.HOUSEHOLD_TYPE_CODE " ' (+)"
   ' os_SQL = os_SQL & " AND referral.GENDER_TYPE_CODE = g.GENDER_TYPE_CODE " ' (+)"
    
   ' Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
   ' Set Get_Referrals_AutoEmail = CCom.RunSQLReturnRS(os_SQL)
   ' os_ReturnCode = 0
   
       Dim CallerID        As String
    Dim sFilter         As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Referrals_AutoEmail " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = os_SQL & "select /*+ CHOOSE */"
    os_SQL = os_SQL & " referral.*"
    os_SQL = os_SQL & " , rst.dscr referral_source_type"
    os_SQL = os_SQL & " , p.program_name"
    os_SQL = os_SQL & " , agency.bsn_entity_name"
    os_SQL = os_SQL & " , counselor.LAST_NAME COUNSELOR_LAST_NAME, counselor.FIRST_NAME COUNSELOR_FIRST_NAME, counselor.MID_INIT_NAME COUNSELOR_MID_INIT_NAME"
    os_SQL = os_SQL & " , counselor.PRIMARY_EMAIL_ADDR, counselor.NAME_SUFFIX_CODE COUNSELOR_NAME_SUFFIX_CODE, counselor.PRIMARY_PHN"
    os_SQL = os_SQL & " , agency.BSN_ENTITY_NAME, agency.PHONE"
    os_SQL = os_SQL & " , SERVICER.BSN_ENTITY_NAME SERVICER"
    os_SQL = os_SQL & " , SERVICER.CONTACT_NAME SERVICER_CONTACT_NAME"
    os_SQL = os_SQL & " , SERVICER.CONTACT_EMAIL SERVICER_CONTACT_EMAIL"
    os_SQL = os_SQL & " , fmt.dscr first_mortgage_type"
    os_SQL = os_SQL & " , rt.dscr referral_type"
    os_SQL = os_SQL & " , fcst.dscr first_contact_status_type"
    os_SQL = os_SQL & " , referral.contacted_servicer_ind contacted_servicer"
    os_SQL = os_SQL & " , cdt.dscr counseling_duration_type"
    os_SQL = os_SQL & " , mnit.dscr mthly_net_income_type"
    os_SQL = os_SQL & " , met.dscr mthly_expense_type"
    os_SQL = os_SQL & " , pt.dscr priority_type"
    os_SQL = os_SQL & " , race_type.dscr race_type"
    os_SQL = os_SQL & " , pdr.dscr primary_default_reason"
    os_SQL = os_SQL & " , sdr.dscr secondary_default_reason"
    os_SQL = os_SQL & " , sso.dscr SUMMARY_SENT_OTHER_TYPE"
    os_SQL = os_SQL & " , ie.dscr income_earners_type"
    os_SQL = os_SQL & " , ic.dscr income_category_type"
    os_SQL = os_SQL & " , ist.dscr intake_score_type"
    os_SQL = os_SQL & " , h.dscr household_type"
    os_SQL = os_SQL & " , g.dscr gender_type"
    
    os_SQL = os_SQL & " from program p"
    os_SQL = os_SQL & " , bsn_entity agency"
    os_SQL = os_SQL & " , profile counselor"
    os_SQL = os_SQL & " , bsn_entity servicer"
    os_SQL = os_SQL & " , ccrc_user cu"
    os_SQL = os_SQL & " , referral"
    os_SQL = os_SQL & " LEFT OUTER JOIN REFERRAL_SOURCE_TYPE rst ON (REFERRAL.referral_source_type_code = rst.referral_source_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_MORTGAGE_TYPE fmt ON (REFERRAL.first_mortgage_type_code = fmt.first_mortgage_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN REFERRAL_TYPE rt ON (REFERRAL.referral_type_code = rt.referral_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN FIRST_CONTACT_STATUS_TYPE fcst ON (REFERRAL.first_contact_status_type_code = fcst.first_contact_status_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN COUNSELING_DURATION_TYPE cdt ON (REFERRAL.counseling_duration_type_code = cdt.counseling_duration_type_code)"
    os_SQL = os_SQL & " LEFT OUTER JOIN PRIORITY_TYPE pt ON (REFERRAL.PRIORITY_TYPE_CODE = pt.PRIORITY_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN MTHLY_NET_INCOME_TYPE mnit ON (REFERRAL.MTHLY_NET_INCOME_TYPE_CODE = mnit.MTHLY_NET_INCOME_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN MTHLY_EXPENSE_TYPE met ON (REFERRAL.MTHLY_EXPENSE_TYPE_CODE = met.MTHLY_EXPENSE_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN DEFAULT_REASON_TYPE pdr ON (REFERRAL.PRIMARY_DFLT_RSN_TYPE_CODE = pdr.DEFAULT_REASON_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN DEFAULT_REASON_TYPE sdr ON (REFERRAL.SECONDARY_DFLT_RSN_TYPE_CODE = sdr.DEFAULT_REASON_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN RACE_TYPE ON (REFERRAL.RACE_TYPE_CODE = RACE_TYPE.RACE_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN SUMMARY_SENT_OTHER_TYPE sso ON (REFERRAL.SUMMARY_SENT_OTHER_TYPE_CODE = sso.SUMMARY_SENT_OTHER_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN INCOME_EARNERS_TYPE ie ON (REFERRAL.INCOME_EARNERS_TYPE_CODE = ie.INCOME_EARNERS_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN INTAKE_SCORE_TYPE ist ON (REFERRAL.INTAKE_SCORE_TYPE_CODE = ist.INTAKE_SCORE_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN INCOME_CATEGORY_TYPE ic ON (REFERRAL.INCOME_CATEGORY_TYPE_CODE = ic.INCOME_CATEGORY_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN HOUSEHOLD_TYPE h ON (REFERRAL.HOUSEHOLD_TYPE_CODE = h.HOUSEHOLD_TYPE_CODE)"
    os_SQL = os_SQL & " LEFT OUTER JOIN GENDER_TYPE g ON (REFERRAL.GENDER_TYPE_CODE = g.GENDER_TYPE_CODE)"

    os_SQL = os_SQL & " WHERE referral.PRIVACY_CONSENT_IND ='Y'"
    os_SQL = os_SQL & " AND (referral.DUPE_IND <> 'Y' OR referral.DUPE_IND IS NULL)"
    os_SQL = os_SQL & " AND referral.SUMMARY_RPT_SENT_DATE IS NULL"
    os_SQL = os_SQL & " AND referral.AGENCY_SEQ_ID = agency.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND AGENCY.AGENCY_TYPE_CODE = '1'"
    os_SQL = os_SQL & " AND referral.SERVICER_SEQ_ID = SERVICER.BSN_ENTITY_SEQ_ID"
    os_SQL = os_SQL & " AND referral.COUNSELOR_SEQ_ID = cu.CCRC_USER_SEQ_ID"
    os_SQL = os_SQL & " and referral.program_seq_id = p.program_seq_id"
    os_SQL = os_SQL & " AND cu.NT_DOMAIN = counselor.NT_DOMAIN"
    os_SQL = os_SQL & " AND cu.NT_USERID = counselor.NT_USERID"
    
    Dim str As String
    str = os_SQL
    Call CCom.WriteToLog("Referral Auto Email")
    Call CCom.WriteToLog(str)
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Referrals_AutoEmail = CCom.RunSQLReturnRS(os_SQL)
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

Public Function ProcessServicerPayments(is_StringData As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As String

  Dim CallerID     As String
  Dim cmd          As adodb.Command
  Dim conn         As adodb.Connection
  Dim arrRows   As Variant
  Dim arrCols   As Variant
  Dim r As Long
  Dim c As Long
  Dim recsEff As Integer
  Dim runLog As String
  
  On Error GoTo errorHandler
  CallerID = sModName & ":ProcessServicerPayments " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New adodb.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  'need logic here
  'if a referral was previously paid, do not update it again.  Report back an error.
  arrRows = Split(is_StringData, "^")
  For r = 0 To UBound(arrRows) - 1
    arrCols = Split(arrRows(r), "~")
    os_SQL = "UPDATE REFERRAL SET PAID_DATE = " & CCom.ScrubDate(arrCols(1)) & _
          ", CHECK_NBR = '" & arrCols(2) & "'" & _
          ", PAID_AMT = '" & arrCols(3) & "'" & _
          " WHERE REFERRAL_SEQ_ID = " & arrCols(0) & _
          " AND PAID_DATE IS NULL"
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    cmd.CommandText = os_SQL
    cmd.Execute recsEff, , adodb.adExecuteNoRecords
    If recsEff = 0 Then
        runLog = runLog & arrCols(4) & "\" & arrCols(5) & " not updated." & "%0D%0A"
    End If
  Next
  
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  ProcessServicerPayments = runLog
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  ProcessServicerPayments = "Error processing servicer payments."
  On Error Resume Next
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function InvoiceSessions(is_AcctPrd As Variant, id_InvoiceDate As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As adodb.Command
  Dim conn         As adodb.Connection
  Dim rs           As adodb.Recordset
  
  On Error GoTo errorHandler
  CallerID = sModName & ":InvoiceSessions " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New adodb.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
    os_SQL = "SELECT R.REFERRAL_SEQ_ID" & _
        " FROM REFERRAL R, REF_RESULT_TYPE_HIST RRTH, BSN_ENTITY BE" & _
        " WHERE RRTH.ACCT_PRD = '" & is_AcctPrd & "'" & _
        " AND RRTH.REFERRAL_SEQ_ID = R.REFERRAL_SEQ_ID" & _
        " AND (DUPE_IND = 'N' OR DUPE_IND IS NULL)" & _
        " AND R.SERVICER_SEQ_ID = BE.BSN_ENTITY_SEQ_ID" & _
        " AND BE.FFS_IND = 'Y'"

    Set rs = CCom.RunSQLReturnRS(os_SQL)
    If Not rs.EOF Then
        rs.MoveFirst
        Do While Not rs.EOF
            os_SQL = "UPDATE REFERRAL SET INVOICE_DATE = " & CCom.ScrubDate(id_InvoiceDate) & _
                  " WHERE REFERRAL_SEQ_ID = " & rs(0)
            Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
            cmd.CommandText = os_SQL
            cmd.Execute , , adodb.adExecuteNoRecords
            rs.MoveNext
        Loop
    End If
    rs.Close
    Set rs = Nothing
    
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  InvoiceSessions = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  InvoiceSessions = -1
  On Error Resume Next
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function ProcessSQL(sSQL As Variant, is_UserID As Variant, os_SQL As Variant, _
    os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID     As String
  Dim arParms() As String
  Dim arParm() As String
  Dim iParm As Integer
    
  On Error GoTo errorHandler
    
  Dim CurrentUserID   As String
  CurrentUserID = CCom.Raw_UserID(is_UserID)
  CallerID = sModName & ":ProcessSQL " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1
        
  os_SQL = sSQL
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  Call CCom.ExecSQL(os_SQL)
  os_ReturnCode = 0

Exit_Function:
  Call CCom.CtxSetComplete
  ProcessSQL = os_ReturnCode
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  ProcessSQL = -1
  On Error Resume Next
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Get_Budget_Items(ii_ReferralId As Variant, is_UserID As Variant, os_SQL As Variant, os_ReturnCode As Variant, _
    os_Message As Variant) As adodb.Recordset

    Dim CallerID    As String
    
    On Error GoTo errorHandler
    CallerID = sModName & ":Get_Budget_Items " & vbTab & "(" & is_UserID & ")" & vbTab
    Call CCom.CtxSetAbort
    os_ReturnCode = -1
    
    os_SQL = "SELECT BUDGET_CATEGORY_TYPE.BUDGET_CATEGORY_TYPE_CODE, BUDGET_CATEGORY_TYPE.DSCR AS BUDGET_CATEGORY_TYPE"
    os_SQL = os_SQL & ", BUDGET_CATEGORY.DSCR AS BUDGET_CATEGORY"
    os_SQL = os_SQL & ", BUDGET_SUBCATEGORY.BUDGET_SUBCATEGORY_SEQ_ID, BUDGET_SUBCATEGORY.DSCR AS BUDGET_SUBCATEGORY"
    'os_SQL = os_SQL & ", NVL(REFERRAL_BUDGET.AMT,0) AS AMT"
    os_SQL = os_SQL & ", IsNull(REFERRAL_BUDGET.AMT,0) AS AMT"

    os_SQL = os_SQL & " FROM ((BUDGET_CATEGORY_TYPE INNER JOIN BUDGET_CATEGORY ON BUDGET_CATEGORY_TYPE.BUDGET_CATEGORY_TYPE_CODE = BUDGET_CATEGORY.BUDGET_CATEGORY_TYPE_CODE)"
    os_SQL = os_SQL & " INNER JOIN BUDGET_SUBCATEGORY ON BUDGET_CATEGORY.BUDGET_CATEGORY_SEQ_ID = BUDGET_SUBCATEGORY.BUDGET_CATEGORY_SEQ_ID)"
    os_SQL = os_SQL & " LEFT JOIN REFERRAL_BUDGET ON BUDGET_SUBCATEGORY.BUDGET_SUBCATEGORY_SEQ_ID = REFERRAL_BUDGET.BUDGET_SUBCATEGORY_SEQ_ID AND REFERRAL_BUDGET.REFERRAL_SEQ_ID=" & ii_ReferralId
    os_SQL = os_SQL & " ORDER BY BUDGET_CATEGORY_TYPE.SORT_ORDER, BUDGET_CATEGORY.SORT_ORDER, BUDGET_SUBCATEGORY.SORT_ORDER"
    
    Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
    Set Get_Budget_Items = CCom.RunSQLReturnRS(os_SQL)
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

Public Function Referral_Budget_Add(il_referralId As Variant, _
  il_SubCategorySeqId As Variant, in_Amt As Variant, is_UserID As Variant, os_SQL As Variant, _
  os_ReturnCode As Variant, os_Message As Variant) As Long

  Dim CallerID        As String
  Dim RS1             As adodb.Recordset
  Dim cmd             As adodb.Command
  Dim os_ReferralSeqId As Long
  Dim sAcctPrd          As Variant
  
  On Error GoTo errorHandler
  
  CallerID = sModName & ":Referral_Budget_Add " & vbTab & "(" & is_UserID & ")" & vbTab
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
  Dim MyInsertAuditFields As String
'  MyInsertAudit = CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'," & CurrentDateTime & ", 'CCRCDCOM', '" & CurrentUserID & "'"
  MyInsertAudit = "'CCRCDCOM'," & CurrentDateTime & ", '" & CurrentUserID & "','CCRCDCOM'," & CurrentDateTime & ",'" & CurrentUserID & "'"
  MyQueryAudit = " CREATE_USER_ID = '" & CurrentUserID & "' "
  MyInsertAuditFields = ",CREATE_PROG_NAME,CREATE_DATE,CREATE_USER_ID,CHG_LST_PROG_NAME,CHG_LST_DATE,CHG_LST_USER_ID"
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
  Set RS1 = CCom.CtxCreateObject("ADODB.Recordset")
  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  'cmd.ActiveConnection.BeginTrans
  
    os_SQL = "INSERT INTO REFERRAL_BUDGET ([REFERRAL_SEQ_ID] ,[BUDGET_SUBCATEGORY_SEQ_ID] ,[AMT] ,[CREATE_PROG_NAME] ,[CREATE_DATE] ,[CREATE_USER_ID] ,[CHG_LST_PROG_NAME] ,[CHG_LST_DATE] ,[CHG_LST_USER_ID]) VALUES("
    os_SQL = os_SQL & il_referralId & "," & il_SubCategorySeqId & "," & in_Amt & "," & MyInsertAudit & ")"
    
  Dim str As String
    str = os_SQL
    Call CCom.WriteToLog("Referral Insert")
    Call CCom.WriteToLog(str)
    
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , adodb.adExecuteNoRecords
  
Exit_Function:
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  If Not (cmd Is Nothing) Then
    'cmd.ActiveConnection.CommitTrans
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  os_ReturnCode = 0
  Referral_Budget_Add = 0
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Budget_Add = -1
  On Error Resume Next
  If Not (RS1 Is Nothing) Then Set RS1 = Nothing
  'cmd.ActiveConnection.RollbackTrans
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function

Public Function Referral_Budget_Delete(ii_ReferralId As Variant, is_UserID As Variant, _
  os_SQL As Variant, os_ReturnCode As Variant, os_Message As Variant) As Integer

  Dim CallerID     As String
  Dim cmd          As adodb.Command
  Dim conn         As adodb.Connection
  
  On Error GoTo errorHandler
  CallerID = sModName & ":Referral_Budget_Delete " & vbTab & "(" & is_UserID & ")" & vbTab
  Call CCom.CtxSetAbort
  os_ReturnCode = -1

#If USING_MTX Then
  Set cmd = CCom.CtxCreateObject("ADODB.Command")
#Else
  Set cmd = New adodb.Command
#End If

  cmd.ActiveConnection = CCom.GetConn
  cmd.CommandType = adCmdText
  
  ' delete records in the table for this referral
  os_SQL = "DELETE FROM REFERRAL_BUDGET WHERE REFERRAL_SEQ_ID = " & ii_ReferralId
  Call CCom.WriteIfDebug(CallerID & " : " & os_SQL)
  cmd.CommandText = os_SQL
  cmd.Execute , , adodb.adExecuteNoRecords
  
  Set cmd.ActiveConnection = Nothing
  Set cmd = Nothing
  os_ReturnCode = 0
  
Exit_Function:
  Referral_Budget_Delete = os_ReturnCode
  Call CCom.CtxSetComplete
  Exit Function

errorHandler:
  CCom.SetError
  os_Message = "An error occurred: " & CStr(Err.Number) & "  " & Err.Description
  os_ReturnCode = -1
  Referral_Budget_Delete = -1
  On Error Resume Next
  If Not (cmd Is Nothing) Then
    Set cmd.ActiveConnection = Nothing
    Set cmd = Nothing
  End If
  Call CCom.WriteToLog(CallerID & " : " & os_Message, 2)
  Call CCom.CtxSetAbort
  CCom.GetError
  Err.Raise Err.Number, CallerID, Err.Description
End Function









