<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false" EnableSessionState=False %>
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

	Dim objAdmin
	objAdmin = CreateObject("CCRC_Admin.CAdmin")
	If RedirectIfError("Creating object CCRC_Admin.CAdmin.", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	
	Dim rs
	rs = objAdmin.Get_Programs(sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Programs", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	
	objAdmin = Nothing	
	If RedirectIfError("Getting program list.", eRetPage, eRetBtnText) Then
		rs.Close
		rs = Nothing
		Response.Clear 
		Response.End 
	End If
%>
<html>
<head>
<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
<script language="JavaScript" src="/ccrc/utilities/goLink.js"></script>
<script language='JavaScript'>
<!--//custom functions

function addProgram() {
	location.href = "program.aspx?FormMode=ADD";
	return;
}

function editProgram(PROGRAM_SEQ_ID) {
	location.href = "program.aspx?FormMode=EDIT&programId=" + PROGRAM_SEQ_ID;
	return;
}

function selectList(PROGRAM_SEQ_ID, ENTITY_TYPE) {
	location.href = "program_entities.aspx?programId=" + PROGRAM_SEQ_ID + "&entityType=" + ENTITY_TYPE;
	return;
}

-->
</script>

<script language="JavaScript">
<!--//standard functions

self.status = 'Please wait. Loading page...';

function onLoad() {
	self.status = '';
	return;
}
-->
</script>

</head>
<body bgcolor="#cccccc" border="0" onLoad="onLoad();">
<table border="0" class="pageBody" style="position:absolute;top:8;left:1;width:625;" ID="Table2">
	<tr>
		<td>
			<table border="0" style="width:100%;" ID="Table1">
				<th style="width:100px;">Name</th>
				<th width="100px">Action</th>
				<tr><td class='listTextBold'>Programs</td><td><a id='addItem' class='goLink' title='Click here to add an item' onDblClick='return false;' href='javascript:addProgram();'>[Add]</a></td></tr>&nbsp;
			</table>
		</td>
	</tr>
<%
	If Not Response.IsClientConnected Then 
		rs.Close
		rs = Nothing
		Response.Clear
		Response.End
	End If

	Response.Write("<tr><td><div style='width: 100%; height: 200px; overflow: auto;'>")
	Response.Write("<table border='0' cellpadding='0' cellspacing='1' style=width:100%;>")
	If IsValidRS(rs) Then

		Dim i
		i = 1
		
		While Not rs.EOF
			Response.Write(vbTab)
			Response.Write("<tr>")
			Response.Write(vbCrLf)

				Response.Write(vbTab2)
				Response.Write("<td id=""ListItem")
				Response.Write(i)
				Response.Write(""" class=""listItemText"" width=""250px"">")
				Response.Write(vbCrLf)
					Response.Write(vbTab3)
            Response.Write(rs.Fields("PROGRAM_NAME").Value)
					Response.Write(vbCrLf)
				Response.Write(vbTab2)
				Response.Write("</td>")
				Response.Write(vbCrLf)

				Response.Write(vbTab2)
				Response.Write("<td class=""listItemText"" width=""250px"" nowrap>")
				Response.Write(vbCrLf)
					Response.Write(vbTab3)
					Response.Write("<a id=""goLink")
					Response.Write(i)
					Response.Write(""" class=""goLink"" title=""Click here to edit this item"" onDblClick=""return false;"" href='javascript:editProgram("	)				
            Response.Write(rs.Fields("PROGRAM_SEQ_ID").Value)
					Response.Write(");'>[Edit]</a>&nbsp;" & vbCrLf)

					
					Response.Write(vbTab3)
					Response.Write("<a id=""agenciesLink")
					Response.Write(i)
					Response.Write(""" class=""goLink"" title=""Click here to see a list of agencies"" onDblClick=""return false;"" href='javascript:selectList(")
            Response.Write(rs.Fields("PROGRAM_SEQ_ID").Value)
					Response.Write(",3")
					Response.Write(");'>[Agencies]</a>&nbsp;" & vbCrLf)

					Response.Write(vbTab3)
					Response.Write("<a id=""servicersLink")
					Response.Write(i)
					Response.Write(""" class=""goLink"" title=""Click here to see a list of servicers"" onDblClick=""return false;"" href='javascript:selectList(")
            Response.Write(rs.Fields("PROGRAM_SEQ_ID").Value)
					Response.Write(",2")
					Response.Write(");'>[Servicers]</a>&nbsp;" & vbCrLf)

					Response.Write(vbTab3)
					Response.Write("<a id=""sponsorsLink")
					Response.Write(i)
					Response.Write(""" class=""goLink"" title=""Click here to see a list of sponsors"" onDblClick=""return false;"" href='javascript:selectList(")					
            Response.Write(rs.Fields("PROGRAM_SEQ_ID").Value)
					Response.Write(",1")
					Response.Write(");'>[Sponsors]</a>&nbsp;" & vbCrLf)
					
					
				Response.Write(vbTab2)
				Response.Write("</td>")
				Response.Write(vbCrLf)

			Response.Write(vbTab)
			Response.Write("</tr>")
			Response.Write(vbCrLf)

			rs.MoveNext

			i = i + 1

		End While
		rs.Close

	End If
	Response.Write("</table></td></tr>")

	rs = nothing

	If RedirectIfError("Building Program List", eRetPage, eRetBtnText) Then
		Response.Clear
		Response.End 
	End If
	
%>
</table>
</body>
</html>
<%	If RedirectIfError("", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If

%>