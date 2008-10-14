<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual="/ccrc/utilities/global.inc"-->
<!--#INCLUDE virtual="/ccrc/utilities/ccrc.inc"-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%

On Error Resume Next

	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	Dim ntDomain, ntUserId, userName, activeInd, formMode, entityId, userLocation
	Dim RsLocations	

	entityId = Request.QueryString("entityId")
	ntDomain = Request.QueryString("ntDomain")
	ntUserId = Request.QueryString("ntUserId")
	formMode = Request.QueryString("FormMode")
	userLocation = Request.QueryString("userLocation")

	If formMode = "EDIT" Then
		Dim obj
		obj = CreateObject("CCRC_Admin.CAdmin")
		If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
			obj = Nothing
			Response.Clear 
			Response.End 
		End If

		Dim Rs		
		Rs = obj.Get_User(ntDomain, ntUserId, sUserId, sSQL, iReturnCode, sMessage)
		If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_User", eRetPage, eRetBtnText) Then
			obj = Nothing
			Response.Clear 
			Response.End 
		End If
		If isValidRS(Rs) Then
			ntDomain = Rs.Fields("NT_DOMAIN").Value
			ntUserId = Rs.Fields("NT_USERID").Value
			userName = buildName(Rs.Fields("FIRST_NAME").Value, Rs.Fields("MID_INIT_NAME").Value, Rs.Fields("LAST_NAME").Value, "")
			activeInd = Rs.Fields("ACTIVE_IND").Value
			userLocation = Rs.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value
		End If			
		Rs.Close
		Rs = Nothing
		
			
		RsLocations = obj.Get_Entity_Locations(entityId, "", sUserId, sSQL, iReturnCode, sMessage)
		If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Entity_Locations", eRetPage, eRetBtnText) Then
			obj = Nothing
			Response.Clear 
			Response.End 
		End If
	End If	
%>
<script language="JavaScript">
function onLoad() {
}
</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>User</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
		<script language="JavaScript">
		function onLoad() {
			document.all.activeInd.focus();
		}

		function isDefined(s) {
			return "undefined" != typeof(s);
		}				

		function submitForm() {
		    alert(document.all.FormMode.value)
			location.href = "user_process.aspx?entityId=" + document.all.entityId.value + "&FormMode=" + document.all.FormMode.value + "&ntDomain=" + document.all.ntDomain.value + "&ntUserId=" + document.all.ntUserId.value + "&activeInd=" + document.all.activeInd.value + "&userLocation=" + document.all.userLocation.value;
		}
			</script>
	</head>
	<body onLoad="onLoad();">
		<br>
		<br>
		<h1>User</h1>
		<FORM name="myform" id="myform">
			<INPUT TYPE=hidden VALUE="<%=FormMode%>" NAME="FormMode" ID="FormMode"> 
			<INPUT TYPE=hidden VALUE="<%=ntDomain%>" NAME="ntDomain" ID="ntDomain">
			<INPUT TYPE=hidden VALUE="<%=ntUserId%>" NAME="ntUserId" ID="ntUserId">
			<INPUT TYPE=hidden VALUE="<%=entityId%>" NAME="entityId" ID="entityId">
			<table>
				<tr>
					<td><font class="HelpText" title="">Name:</font></td>
					<td><%=userName%></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Active:</font></td>
					<td><%=activeIndSelect("activeInd", activeInd, "true", "Active", "")%></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Location:</font></td>
					<td><%=UserLocationSelect("userLocation", userLocation, "true", "Location", "", RsLocations)%></td>
				</tr>
				<tr>
					<td></td>
					<td align="right"><input type="button" value="Save" onclick="javascript:submitForm();"></td>
				</tr>
			</table>
		</form>
	</body>
</html>
<%
	If formMode = "EDIT" Then
		rsLocations.Close
		rsLocations = Nothing
	End If
%>
