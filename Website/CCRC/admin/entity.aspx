<%@ Page Language="VB" aspcompat=true AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->

<%
    On Error Resume Next

    Dim sDomainUser, sDomain, sUserId As Object, sSQL As Object, iReturnCode As Object, sMessage As Object

	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

    Dim entityId As Object, entityName, entityTypeName, activeInd, phone, fax
	Dim formMode, returnPage, programId, hasContractInd, contactName, contactEmail
    Dim displayInd, agencyType, ffsInd
    Dim entityType As Integer

	entityId = Request.QueryString("entityId")	
	entityType = Request.QueryString("entityType")	
	returnPage = Request.QueryString("returnPage")	
	programId = Request.QueryString("programId")

	Dim obj
	obj = CreateObject("CCRC_Admin.CAdmin")
	If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
        Response.End()
	End If

    'Response.Write(entityId)
    'Response.Write("-")
    'Response.Write(entityType)
    'Response.Write("-")
    'Response.Write(returnPage)
    'Response.Write("-")
    'Response.Write(programId)
    
	Dim Rs		
	Rs = obj.Get_Entity_Type(entityType, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Entity", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
        Response.End()
	End If
	If isValidRS(Rs) Then
		entityTypeName = Rs.Fields("DSCR").Value
	Else
		entityTypeName = "Business Entity"
	End If
	Rs.Close
	
	formMode = Request.QueryString("FormMode")
	If formMode = "ADD" Then
		activeInd = "Y"
	ElseIf formMode = "EDIT" Then
		Rs = obj.Get_Entity(entityId, sUserId, sSQL, iReturnCode, sMessage)
		If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Entity", eRetPage, eRetBtnText) Then
			obj = Nothing
			Response.Clear 
            Response.End()
		End If
		
		If isValidRS(Rs) Then
			entityName = Rs.Fields("BSN_ENTITY_NAME").Value
			entityType = Rs.Fields("BSN_ENTITY_TYPE_CODE").Value
			activeInd = Rs.Fields("ACTIVE_IND").Value
			phone = Rs.Fields("PHONE").Value
			fax = Rs.Fields("FAX").Value
			hasContractInd = Rs.Fields("HAS_CONTRACT_IND").Value
			contactName = Rs.Fields("CONTACT_NAME").Value
			contactEmail = Rs.Fields("CONTACT_EMAIL").Value
			displayInd = Rs.Fields("DISPLAY_IND").Value
			agencyType = Rs.Fields("AGENCY_TYPE_CODE").Value
			ffsInd = Rs.Fields("FFS_IND").Value
			Rs.Close
		End If			
	End If
	Rs = Nothing
	obj = Nothing
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Business Entity</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<script language="JavaScript" src="../utilities/validation.js"></script>
			<script language="JavaScript" src="../utilities/detectChanges.js"></script>
			<script language="JavaScript">
			function onLoad() {
				initialValues = buildValues(document.myform);
				document.all.entityName.focus();
			}

			function editUsers() {
				location.href = "user_list.aspx?entityId=" + document.all.entityId.value;
			}

			function isDefined(s) {
				return "undefined" != typeof(s);
			}				

			function cancelForm() {
				location.replace("entity_list.aspx?entityType=" + document.all.entityType.value);
			}				

			function submitForm() 
			{
				var frm = document.myform;
				var msg = "";

				if (validate()) 
				{
					if (dataChanged(frm)) 
					{
						location.href = "entity_process.aspx?FormMode=" + frm.FormMode.value + "&entityId=" + frm.entityId.value + "&entityName=" + frm.entityName.value + "&entityType=" + frm.entityType.value + "&contactName=" + frm.contactName.value + "&contactEmail=" + frm.contactEmail.value + "&phone=" + frm.phone.value + "&fax=" + frm.fax.value + "&activeInd=" + frm.activeInd.value + "&hasContractInd=" + frm.hasContractInd.value + "&returnPage=" + frm.returnPage.value + "&programId=" + frm.programId.value + "&displayInd=" + frm.displayInd.value + "&agencyType=" + frm.agencyType.value + "&ffsInd=" + frm.ffsInd.value;
					}
					else
					{
						location.href = "entity_list.aspx?entityType=" & frm.entityType.value;
					}
				}
			}
			
			function validate() 
			{
				var pv = new Validator();
				with (document.myform) 
					{
					pv.addField(document.all.entityName);
					pv.addField(document.all.entityType);
					pv.addField(document.all.activeInd);
					
					if ("2" == document.all.entityType.value)
						pv.addField(document.all.ffsInd);

					if ("3" == document.all.entityType.value)
						pv.addField(document.all.agencyType);
					}
				return pv.validate(document.all.results);
			}		
			</script>
	</head>
	<body onLoad="onLoad();">
		<br>
		<br>
		<h1><%=entityTypeName%></h1>
		<FORM name="myform" id="myform">
			<INPUT TYPE=hidden VALUE="<%=formMode%>" NAME="FormMode" ID="FormMode"> <INPUT TYPE=hidden VALUE="<%=entityId%>" NAME="entityId" ID="entityId">
			<INPUT TYPE=hidden VALUE="<%=returnPage%>" NAME="returnPage" ID="returnPage"> <INPUT TYPE=hidden VALUE="<%=programId%>" NAME="programId" ID="programId">
			<%If formMode = "ADD" Then%>
			<INPUT TYPE=hidden VALUE="<%=entityType%>" NAME="entityType" ID="entityType"> <INPUT TYPE=hidden VALUE="<%=activeInd%>" NAME="activeInd" ID="activeInd">
			<%End If%>
			<table class="pageBody">
				<tr>
					<td><font class="HelpText" title="">Name:</font></td>
					<td><input detectChanges name="entityName" value="<%=entityName%>" required="true" datatype="anytext" errorlbl="Name" errormsg="Name is required."></td>
				</tr>
				<%If formMode = "EDIT" Then%>
				<tr>
					<td><font class="HelpText" title="">Entity Type:</font></td>
					<td><%=entityTypeSelect("entityType", entityType, "true", "Entity Type", "Entity type is required.")%></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Active:</font></td>
					<td><%=activeIndSelect("activeInd", activeInd, "true", "Active", "Active is required.")%></td>
				</tr>
				<%End If%>
				<tr>
					<td><font class="HelpText" title="">Phone:</font></td>
					<td><input detectChanges name="phone" value="<%=phone%>" maxlength=20 required="false" datatype="anytext" errorlbl="" errormsg="" ID="phone"></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Fax:</font></td>
					<td><input detectChanges name="fax" value="<%=fax%>" maxlength=20 required="false" datatype="anytext" errorlbl="" errormsg="" ID="fax"></td>
				</tr>
				<% If entityType = "3" Then %>
				<tr>
					<td><font class="HelpText" title="">Agency Type:</font></td>
					<td><%=agencyTypeSelect("agencyType", agencyType, "true", "Agency Type", "Agency type is required.")%></td>
				</tr>
				<% Else %>
				<INPUT TYPE="hidden" VALUE="" NAME="agencyType" ID="agencyType">
				<% End If %>
				<% If entityType = "2" Then %>
				<tr>
					<td><font class="HelpText" title="">Contact Name:</font></td>
					<td><input detectChanges name="contactName" value="<%=contactName%>" required="false" datatype="anytext" errorlbl="" errormsg="" ID="contactName"></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Contact Email:</font></td>
					<td><input detectChanges name="contactEmail" value="<%=contactEmail%>" required="false" datatype="anytext" errorlbl="" errormsg="" ID="contactEmail"></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Fee-For-Service:</font></td>
					<td><%=ffsIndSelect("ffsInd", ffsInd, "true", "Fee-For-Service", "Fee-For-Service is required.")%></td>
				</tr>				
				<tr>
					<td><font class="HelpText" title="">Contract Signed?:</font></td>
					<td><%=yesnoSelect("hasContractInd", hasContractInd, "true", "Has Contract", "Contract signed is required.")%></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Display:</font></td>
					<td><%=displayIndSelect("displayInd", displayInd, "true", "Display", "Display is required.")%></td>
				</tr>
				<% Else %>
					<INPUT TYPE="hidden" VALUE="Y" NAME="hasContractInd" ID="hasContractInd"> 
					<INPUT TYPE="hidden" VALUE="" NAME="contactName" ID="contactName">
					<INPUT TYPE="hidden" VALUE="" NAME="contactEmail" ID="contactEmail"> 
					<INPUT TYPE="hidden" VALUE="N" NAME="displayInd" ID="displayInd">
					<INPUT TYPE="hidden" VALUE="N" NAME="ffsInd" ID="ffsInd">
				<% End If %>
			</table>
			<table>
				<tr>
					<td align="right"><input type="button" onclick="javascript:submitForm();" value="Save"></td>
					<td align="right"><input type="button" onclick="javascript:cancelForm();" value="Cancel" ID="Button1" NAME="Button1"></td>
				</tr>
			</table>
			<table>
				<tr>
					<td><!-- Validation results section-->
						<div name="results" id="results" class="errorText"></div>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>


