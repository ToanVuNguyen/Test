<%

' db levels
Public Const levelClientSegment = "1"
Public Const levelSeller = "2"
Public Const levelBroker = "3"
Public Const levelGroup = "4"

' additional non-db levels
public const levelUser = "0"
public const levelEngine = "10"

Public Const securityAccessSuperUser = "01"
Public Const securityAccessSellerAdmin = "02"
Public Const securityAccessCompanyAdmin = "03"
Public Const securityAccessUser = "04"
Public Const securityAccessClientSegmentAdmin = "05"
Public Const securityAccessGroupAdmin = "06"

Function CheckUserAccess(levelRequested)
' This function checks to verify the user's access rights 
' vs. the administration level requested.

	Dim accessRightCode 
	
	accessRightCode = getSessVar("accessRightCode")
	
	If levelRequested = levelSeller Then
		If accessRightCode = securityAccessSuperUser Then
			CheckUserAccess = True
		Else
			CheckUserAccess = False		
		End If
	ElseIf levelRequested = levelBroker Then
		If accessRightCode = securityAccessSuperUser Or accessRightCode = securityAccessSellerAdmin Then
			CheckUserAccess = True
		Else
			CheckUserAccess = False		
		End If
	Else
		CheckUserAccess = False
	End If

End Function

%>
