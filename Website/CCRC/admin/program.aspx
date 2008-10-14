<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
 On Error Resume Next

	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	Dim programId, programName, startDate, endDate, formMode
	programId = Request.QueryString("programId")	
	formMode = Request.QueryString("FormMode")
	
	If formMode = "EDIT" Then
		Dim objCCRCAdmin
		objCCRCAdmin = CreateObject("CCRC_Admin.CAdmin")
		If RedirectIfError("Creating CCRC_Admin.CAdmin", eRetPage, eRetBtnText) Then
			objCCRCAdmin = Nothing
			Response.Clear 
			Response.End 
		End If

		Dim Rs		
		Rs = objCCRCAdmin.Get_Program(programId, sUserId, sSQL, iReturnCode, sMessage)
		If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Program", eRetPage, eRetBtnText) Then
			objCCRCAdmin = Nothing
			Response.Clear 
			Response.End 
		End If

		If isValidRS(Rs) Then
			programName = Rs.Fields("PROGRAM_NAME").Value
			startDate = Rs.Fields("START_DATE").Value
			endDate = Rs.Fields("END_DATE").Value
		End If			
		Rs.Close
		Rs = Nothing
		objCCRCAdmin = Nothing
	End If
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Program</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
		<script language="JavaScript" src="/ccrc/utilities/validation.js"></script>
		<script language="JavaScript" src="../utilities/detectChanges.js"></script>
		<script language="JavaScript">
			function onLoad() {
				initialValues = buildValues(document.myform);
				document.all.programName.focus();
			}

			function isDefined(s) {
				return "undefined" != typeof(s);
			}				

			function cancelForm() {
				location.replace("program_list.aspx");
			}				

			function submitForm() 
			{
				var frm = document.myform;
				if (validate()) 
				{
					if (dataChanged(frm)) 
					{
						location.href = "program_process.aspx?FormMode=" + frm.FormMode.value + "&programId=" + frm.programId.value + "&programName=" + frm.programName.value + "&startDate=" + frm.startDate.value + "&endDate=" + frm.endDate.value
					}
					else
					{
						location.href = "program_list.aspx";
					}
				}
			}
			
			function validate() 
			{
				var pv = new Validator();
				with (document.myform) 
					{
					pv.addField(document.all.programName);
					pv.addField(document.all.startDate);
					pv.addField(document.all.endDate);
					}
				return pv.validate(document.all.results);
			}		
		</script>
	</head>
	<body onLoad="onLoad();" class="pageBody">
		<br>
		<br>
		<h1>Program</h1>
		<FORM name="myform" id="myform">
			<INPUT TYPE=hidden VALUE="<%=formMode%>" NAME="FormMode" ID="FormMode"> 
			<INPUT TYPE=hidden VALUE="<%=programId%>" NAME="programId" ID="programId">
			<table class="pageBody">
				<tr>
					<td><font class="label" title="">Name:</font></td>
					<td><input detectChanges name="programName" value="<%=programName%>" required="true" datatype="anytext" errorlbl="Name" errormsg="Name is required."></td>
				</tr>
				<tr>
					<td><font class="label" title="">Start Date:</font></td>
					<td><input detectChanges name="startDate" value="<%=startDate%>" required="true" datatype="anytext" errorlbl="Start Date" errormsg="Start Date is required." ID="startDate"></td>
				</tr>
				<tr>
					<td><font class="label" title="">End Date:</font></td>
					<td><input detectChanges name="endDate" value="<%=endDate%>" required="false" datatype="anytext" ID="endDate"></td>
				</tr>
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

