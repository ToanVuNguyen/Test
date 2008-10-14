<%@ Page Language="VB" AutoEventWireup="false"  aspcompat="true"  %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/cookies_utils.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/date_utils.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%

    On Error Resume Next
	
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	
	
    Dim errNum, errDesc, errSrc, url, currContext, returnButtonText, ErrorReturnPage
    
   	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	
	errNum = Request.QueryString("errNum")
	errDesc = Request.QueryString("errDesc")
	errSrc = Request.QueryString("errSrc")
	url = Request.QueryString("url")
	currContext = Request.QueryString("currContext")
	returnButtonText = Request.QueryString("ERetBtnText")
	ErrorReturnPage = Request.QueryString("ERetPage") & vbNullString
	
	If returnButtonText = "" Then
	  returnButtonText = "Retry"
	End If

%>
<html>
<head>
<title>Error</title>
<link rel="stylesheet" href="/ccrc/css/main_w.css" type="text/css">
<link rel="stylesheet" href="/ccrc/css/report.css" type="text/css">
<script language="JavaScript" src="utilities/global.js"></script>
<script language='JavaScript'>
<!--<%'//custom client side functions%>

function back_OnClick() {
	location.replace("<%=Request.QueryString("ERetPage")%>");
}

//-->
</script>
<script language="JavaScript">
<!--<%'//standard client side functions%>

var loaded = true;

document.onkeydown = handleKeyDown;
function handleKeyDown() {
	switch (window.event.keyCode) {
		case 8:			//backspace
			window.event.keyCode = 0;
			break;
  }
}

function resetForms() {
	return true;
}

function submitForm() {
	return true;
}

function getData() {
	return true;
}

function validate() {
	return true;
}

//-->
</script>
</head>
<body class="app">
	<table class="data">
		<tr>
			<td colspan="2">
				<span class="question">Warning: An error has occurred</span> 
			</td>
		</tr>
		<tr>
			<td class='labelText' valign='top' width='25%'>&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td class='label' valign='top' width='25%'>User:</td>
			<td class='errorText' valign='top'><%=sDomainUser%></td>
		</tr>
		<tr>
			<td class='label' valign='top' width='25%'>Date:</td>
			<td class='errorText' valign='top'><%=ReformatDatetime(now, "MM/DD/YYYY", "HH:MM AM")%></td>
		</tr>
		<tr>
			<td class='label' valign='top' width='25%'>Error Number:</td>
			<td class='errorText' valign='top'><%=errNum%></td>
		</tr>
		<tr>
			<td class='label' valign='top' width='25%'>Error Source:</td>
			<td valign='top'><pre class='errorText'><%=errSrc%></pre></td>
		</tr>
		<tr>
			<td class='label' valign='top' width='25%'>Error Description:</td>
			<td class='errorText' valign='top'><%=errDesc%></td>
		</tr>
<%If Not isNullOrEmpty(currContext) Then%>
		<tr>
			<td class='label' valign='top' width='25%'>Context:</td>
			<td class='errorText' valign='top'><%=currContext%></td>
		</tr>
<%End If%>
		<tr>
			<td class='label' valign='top' width='25%'>URL:</td>
			<td class='errorText' valign='top'><%="http://" & url%></td>
		</tr>
	</table>
<%If InStr(1,ErrorReturnPage, "/admin/",1) = 0 Then%>
	<table>
		<tr>
			<td align="right">
				<input value="<%=returnButtonText%>" class="btn" type="button" accesskey="b" onclick="back_OnClick();">
			</td>
		</tr>
	</table>
<%End If%>
</body>
</html>
