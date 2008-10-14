<%@ Language=VBScript %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
 
On Error Resume Next

	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	Dim entityId, locationId, locationName, activeInd, phone, formMode
	entityId = Request.QueryString("entityId")	
	locationId = Request.QueryString("locationId")	
	formMode = Request.QueryString("FormMode")

	If formMode = "EDIT" Then
		Dim objCCRCAdmin
		objCCRCAdmin = CreateObject("CCRC_Admin.CAdmin")

		Dim Rs		
		Rs = objCCRCAdmin.Get_Entity_Location(LocationId, sUserId, sSQL, iReturnCode, sMessage)

		If isValidRS(Rs) Then
			locationName = Rs.Fields("LOCTN_NAME").Value
			activeInd = Rs.Fields("ACTIVE_IND").Value
			phone = Rs.Fields("PHONE").Value
		End If			
		Rs.Close        
		Rs = Nothing
		objCCRCAdmin = Nothing
	End If
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Business Entity Location</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
		<script language="JavaScript" src="/ccrc/utilities/validation.js"></script>
		<script language="JavaScript" src="../utilities/detectChanges.js"></script>
		<script language="JavaScript">
			function onLoad() {
				initialValues = buildValues(document.myform);
				document.all.locationName.focus();
			}

			function isDefined(s) {
				return "undefined" != typeof(s);
			}				

			function submitForm() 
			{
				var frm = document.myform;
				if (validate()) 
				{
					if (dataChanged(frm)) 
					{
						location.href = "entity_location_process.aspx?FormMode=" + frm.FormMode.value + "&locationId=" + frm.locationId.value + "&entityId=" + frm.entityId.value + "&locationName=" + frm.locationName.value + "&phone=" + frm.phone.value + "&activeInd=" + frm.activeInd.value
					}
					else
					{
						location.href = "entity_location_list.aspx?entityId=" + frm.entityId.value 
					}
				}
			}
			
			function validate() 
			{
				var pv = new Validator();
				with (document.myform) 
					{
					pv.addField(document.all.locationName);
					pv.addField(document.all.phone);
					pv.addField(document.all.activeInd);
					}
				return pv.validate(document.all.results);
			}		
		</script>
	</head>
	<body class="pageBody" onLoad="onLoad();">
		<br>
		<br>
		<h1>Program</h1>
		<FORM class="pageBody" name="myform" id="myform">
			<INPUT TYPE=hidden VALUE="<%=formMode%>" NAME="FormMode" ID="FormMode"> 
			<INPUT TYPE=hidden VALUE="<%=entityId%>" NAME="entityId" ID="entityId">
			<INPUT TYPE=hidden VALUE="<%=locationId%>" NAME="locationId" ID="locationId">
			<table class="pageBody">
				<tr>
					<td><font class="HelpText" title="">Name:</font></td>
					<td><input detectChanges name="locationName" value="<%=locationName%>" required="true" datatype="anytext" errorlbl="Name" errormsg="Name is required."></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Phone:</font></td>
					<td><input detectChanges name="phone" value="<%=phone%>" required="false" datatype="positiveinteger" errorlbl="Phone" errormsg="Phone is to be a Number." ID="phone"></td>
				</tr>
				<tr>
					<td><font class="HelpText" title="">Active:</font></td>
					<td><%=activeIndSelect("activeInd", activeInd, "true", "Active", "Active is required.")%></td>
				</tr>
			</table>
			<table class="pageBody">
				<tr>
					<td align="right"><input type="button" onclick="javascript:submitForm();" value="Save"></td>
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
 
