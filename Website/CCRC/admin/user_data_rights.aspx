<%@ Page Language="VB" aspcompat="true" EnableSessionState=False AutoEventWireup="false"%>

<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#INCLUDE virtual='/ccrc/utilities/formatting.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
	On Error Resume Next

	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
	If RedirectIfError("Calling GetAuthorizedUser().", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If

	Dim profileId, entityId
	profileId = Request.QueryString("profileId")	
	entityId = Request.QueryString("entityId")
	
	Dim objAdmin
	objAdmin = CreateObject("CCRC_Admin.CAdmin")
	If RedirectIfError("Creating object CCRC_Admin.CAdmin.", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	
	Dim rsSelected, rsNotSelected
	rsNotSelected = objAdmin.Get_User_Data_Rights(profileId, entityId, False, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_User_Data_Rights", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If

	rsSelected = objAdmin.Get_User_Data_Rights(profileId, entityId, True, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_User_Data_Rights", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	
	objAdmin = Nothing	
	If RedirectIfError("Getting available data rights list.", eRetPage, eRetBtnText) Then
		rsSelected.Close
		rsNotSelected.Close
		rsSelected = Nothing
		rsNotSelected = Nothing
		Response.Clear 
		Response.End 
	End If
%>
<html>
<head>
<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
<script language="JavaScript" src="/ccrc/utilities/goLink.js"></script>
<SCRIPT LANGUAGE="JavaScript" SRC="/ccrc/utilities/selectbox.js"></SCRIPT>

<script language='JavaScript'>
<!--//custom functions
-->
</script>

<script language="JavaScript">
<!--//standard functions

self.status = 'Please wait. Loading page...';

function onLoad() {
	self.status = '';
	return;
}

function cancelForm() {
	location.replace("user_list.aspx?entityId=" + document.myform.entityId.value);
	return true;
}

function submitForm() {
	var msg = "";

	if(document.myform.SelectedCols.length != 0)
		msg += document.myform.SelectedCols[0].value;

	for(var i = 1; i < document.myform.SelectedCols.length; i++) 
		msg += "|" + document.myform.SelectedCols[i].value;
	document.myform.msg.value = msg;
	document.myform.submit();
	return true;	
}
-->
</script>

</head>
<body bgcolor="#cccccc" border="0" onLoad="onLoad();">
<form id="myform" name="myform"  method="post" action="user_data_rights_process.aspx">
<INPUT TYPE=hidden NAME="msg" ID="msg"> 
<INPUT TYPE=hidden value=<%=profileId%> NAME="profileId" ID="profileId"> 
<INPUT TYPE=hidden value=<%=entityId%> NAME="entityId" ID="entityId">
<table border="0" cellpadding="2" cellspacing="2" width="95%" ID="Table1">
	<tr>
		<td width="45%">
			<b>Available:</b>
		</td>
		<td width="10%">
			&nbsp;
		</td>
		<td width="45%">
			<b>Selected:</b>
		</td>
	</tr>

	<tr>
		<td width="40%" rowspan="3">
			<select name="AvailableCols" multiple size="10" Style="Width:100%"
				onDblClick="moveSelectedOptions(this.form['AvailableCols'],this.form['SelectedCols'],false)" ID="Select1">
<%
If IsValidRs(rsNotSelected) Then
	rsNotSelected.MoveFirst
	Do While Not rsNotSelected.EOF
		Response.Write("<option value='" & rsNotSelected.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value & "'>" & rsNotSelected.Fields("LOCTN_NAME").Value & "</option>")
		rsNotSelected.MoveNext
	Loop
	rsNotSelected.Close
End If
%>
			</select>
		</td>

		<td width="10%" valign="middle" style="VERTICAL-ALIGN: middle" rowspan="3">
			<input type="button" name="Add" value="Add >>" style="width:100px" 
				onClick="moveSelectedOptions(document.forms[0]['AvailableCols'],document.forms[0]['SelectedCols'],false);return false;" ID="Button1">
			<BR>
		
			<input type="button" name="AddAll" value="Add All >>" style="width:100px" 
				onClick="moveAllOptions(document.forms[0]['AvailableCols'],document.forms[0]['SelectedCols'],false);return false;" ID="Button2">
			<BR>
		
			<input type="button" name="Remove" value="<< Remove" style="width:100px" 
				onClick="moveSelectedOptions(document.forms[0]['SelectedCols'],document.forms[0]['AvailableCols'],false);return false;" ID="Button3">
			<BR>
		
			<input type="button" name="RemoveAll" value="<< Remove All" style="width:100px" 
				onClick="moveAllOptions(document.forms[0]['SelectedCols'],document.forms[0]['AvailableCols'],false);return false;" ID="Button4">
			<BR>
		</td>

		<td width="40%" rowspan="3">
			<select name="SelectedCols" multiple size="10" Style="Width:100%" 
				onDblClick="moveSelectedOptions(this.form['SelectedCols'],this.form['AvailableCols'],false)" ID="Select2">
<%
If IsValidRs(rsSelected) Then
	rsSelected.MoveFirst
	Do While Not rsSelected.EOF
		Response.Write("<option value='" & rsSelected.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value & "'>" & rsSelected.Fields("LOCTN_NAME").Value & "</option>")
		rsSelected.MoveNext
	Loop
	rsSelected.Close
End If
%>
			</select>
		</td>
<!--
		<td width="5%" ALIGN="CENTER" VALIGN="TOP" style="VERTICAL-ALIGN: top">
			<br>
			<input type="button" name="Up" value="Up" style="width:50px" 
				onClick="moveOptionUp(this.form['SelectedCols'])" ID="Button5">
		</td>
-->		
	</tr>
<!--
	<tr>
		&nbsp; </td>
	</tr>

	<tr>
		<td width="5%" ALIGN="CENTER" VALIGN="BOTTOM" style="VERTICAL-ALIGN: bottom">
			<input type="button" name="Down" value="Down" style="width:50px" 
				onClick="moveOptionDown(this.form['SelectedCols'])" ID="Button6"><BR>
			<br>
		</td>
	</tr>
-->
</table>
<table ID="Table2">
	<tr>
		<td>
			<input name="cancel" id="cancel" value="Cancel" class="btn" type="button" accesskey="d" onclick="cancelForm();" title="Discard the information">
		</td>
		<td>
			<input name="save" id="save" value="Save" class="btn" type="button" accesskey="s" onclick="submitForm();" title="Save the changed information">
		</td>
	</tr>
</table>

</form>
</body>
</html>
<%	
	If RedirectIfError("", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If

	rsSelected = Nothing
	rsNotSelected = Nothing
%>