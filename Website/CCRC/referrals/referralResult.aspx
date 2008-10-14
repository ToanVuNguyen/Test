<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true CodeFile="referralBudget.aspx.vb" inherits=referrals_referral %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<%
	On Error Resume Next
    'Response.Cookies("CCRCCookie")("profileRoles") = "SomeRole"
    'Response.Cookies("CCRCCookie")("profileSeqId") = "99"

	err.clear() 
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
    Response.write(sDomainUser)
	'Dim referralResultId, referralId, FormMode, disable, userRoles
	Dim referralResultId, referralId, FormMode, userRoles
	Dim referralResultDate, referralResultType, entityType
	referralResultId = Request.QueryString("referralResultId")	
	referralId = Request.QueryString("referralId")	
	FormMode = Request.QueryString("FormMode")	
	entityType = Request.Cookies("CCRCCookie")("entityTypeCode")
	userRoles = Request.Cookies("CCRCCookie")("profileRoles")
	referralResultDate = Now()

	'disable edits if not a Counselor or Super User
	disable = false	
    If InStr(1,userRoles,"83") = 0 And InStr(1,userRoles,"85") = 0 Then
		disable = true
	End If

	Dim obj
	obj = CreateObject("CCRC_Main.CMain")
	If Len(referralResultId) > 0 Then
		If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
			obj = Nothing
			Response.Clear 
			Response.End 
		End If

		Dim Rs		
		Rs = obj.Get_Referral_Result(referralResultId, sUserId, sSQL, iReturnCode, sMessage)
		If RedirectIfError("Calling CCRC_Main.CMain.Get_Referral_Result", eRetPage, eRetBtnText) Then
			obj = Nothing
			Response.Clear 
			Response.End 
		End If	

		If isValidRS(Rs) Then
			referralResultType = Rs.Fields("REFERRAL_RESULT_TYPE_CODE").Value
			If Not Rs.Fields("REFERRAL_RESULT_DATE").Value Is Nothing Then
				referralResultDate = Rs.Fields("REFERRAL_RESULT_DATE").Value
			End If
			Rs.Close
		End If			
	Else
		'do something
	End If
	referralResultDate = Month(referralResultDate) & "/" & Day(referralResultDate) & "/" & Year(referralResultDate)
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Referral Result</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
			<script language="JavaScript" src="/ccrc/utilities/validation.js"></script>
			<script language="JavaScript" src="/ccrc/utilities/detectChanges.js"></script>
			<SCRIPT LANGUAGE="JavaScript" SRC="/ccrc/utilities/calendar.js"></SCRIPT>
			<script language="JavaScript">
				var bSubmitForm = false;
				document.onkeydown = handleKeyDown;
				function handleKeyDown() {
					switch (window.event.keyCode) {
						case 13: 		//enter
							submitForm();
							break;
						case 8:			//backspace
							window.event.keyCode = 0;
							break;
					}
					return;
				}
				
				function onLoad() {
					document.all.save.focus();
				}

				function unLoad() {
					if(false == bSubmitForm && false == bShowCalendar) {
						if(true == dataChanged(document.myform))
							event.returnValue = "Data has changed.";
					}
					if(true == bShowCalendar)
						bShowCalendar = false;
				}				
		
				function isDefined(s) {
					return "undefined" != typeof(s);
				}		
		
				function submitForm() 
				{
					var frm = document.myform;

					if (bSubmitForm) 
						return true;
					else
						bSubmitForm = true;

					if (validate()) 
					{
						if (dataChanged(frm)) 
						{
							location.href = "referralResult_process.aspx?FormMode=" + frm.FormMode.value + "&referralId=" + frm.referralId.value + "&referralResultType=" + frm.referralResultType.value + "&referralResultDate=" + frm.referralResultDate.value + "&referralResultId=" + frm.referralResultId.value;
							alreadySubmitted == true
						}
						else
						{
							bSubmitForm = false;
						}
					}
					else
					{
						bSubmitForm = false;
						alert(document.all.results.innerText);
					}
					return false;					
				}

				function cancelForm() {
					location.replace("referral.aspx?formMode=EDIT&referralId=" + document.myform.referralId.value);
				}				
				
				function validate() 
				{
					var pv = new Validator();
					with (document.myform) 
					{
						pv.addField(document.myform.referralResultType);
						pv.addField(document.myform.referralResultDate);
					}

					if("3" == document.myform.referralResultType.value)
					{
						document.all.results.innerText = "Selected Referral Result is no longer valid.  Please choose a different one.";
						return false;
					}
					else
						return pv.validate(document.all.results);
				}				
			</script>
	</head>
	<body onload="javascript:onLoad();">
		<h1>Add/Edit Session Outcome</h1>
		<FORM name="myform" id="myform">
			<INPUT TYPE=hidden VALUE="<%=FormMode%>" NAME="FormMode" ID="FormMode"> <INPUT TYPE=hidden VALUE="<%=referralResultId%>" NAME="referralResultId" ID="referralResultId">
			<INPUT TYPE=hidden VALUE="<%=referralId%>" NAME="referralId" ID="referralId"> <INPUT TYPE=hidden VALUE="<%=disable%>" NAME="disable" ID="disable">
			<table>
				<tr>
					<td><font class="label" title="">Referral Result:</font></td>
					<td><%Response.Write (referralResultSelect("referralResultType", referralResultType, disable, "true", "Referral Result", ""))%></td>
				</tr>
				<tr>
					<td><font class="label" title="">Date:</font></td>
					<td>
						<%If disable Then%>
						<%=referralResultDate%>
						<%Else%>
						<input columnName="REFERRAL_RESULT_DATE" columnType="date" required="true" datatype="date" errorlbl="Date" type="text" id="referralResultDate" id="referralResultDate" name="referralResultDate" value="<%=referralResultDate%>">
						<%
						Response.Write ("<A HREF=""javascript:doNothing()"" onClick=""setDateField(document.myform.referralResultDate); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
						Response.Write ("(mm/dd/yyyy)")
							%>
						<%End If%>
						<INPUT TYPE="text" VALUE="1" style="display:none" NAME="Hidden1" ID="Hidden1"> <INPUT TYPE=hidden VALUE="<%=disable%>" NAME="disable" ID="Hidden2">
					</td>
				</tr>
			</table>
				<%If InStr(1,userRoles,"83") <> 0 Or InStr(1,userRoles,"85") <> 0 Then%>
			<table ID="Table2">
				<tr>
					<td></td>
					<td align="right">
						<input name="save" id="save" value="Save" class="btn" type="button" accesskey="s" onclick="submitForm();" title="Save the changed information and continue">
					</td>
					<td>
						<input name="cancel" id="cancel" value="Cancel" class="btn" type="button" accesskey="s" onclick="cancelForm();" title="Cancel all changes">
					</td>
				</tr>
			</table>
				<%End If%>
			<table ID="Table1">
				<tr>
					<td><!-- Validation results section-->
						<div name="results" id="results" class="errorText"></div>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
<%
	
'	Rs = Nothing
	obj = Nothing
%>
