<%@ Language=VBScript %>
<!--#INCLUDE virtual="/ccrc/utilities/global.inc"-->
<!--#INCLUDE virtual="/ccrc/utilities/ccrc.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/formatting.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%

'This page contains server side code to validate that the user is a valid
'active user of CCRC.
 	
	On Error Resume Next

	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
	
	Dim entityId
	entityId = Request.QueryString("entityId")	

	Dim objCCRC
	objCCRC = CreateObject("CCRC_Admin.CAdmin")
	If RedirectIfError("Creating object CCRC_Admin.CAdmin.", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If

	Dim Rs
	Rs = objCCRC.Get_Entity_Locations(entityId, "", sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Entity_Locations", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If

	objCCRC = Nothing
	If RedirectIfError("Getting Entity location list.", eRetPage, eRetBtnText) Then
		Rs.Close
		Rs = Nothing
		Response.Clear 
		Response.End 
	End If		
%>
<script language="JavaScript">
	function onLoad() {
	}

	function addLocation() {
		location.href = "entity_location.aspx?FormMode=ADD&entityId=" + document.myform.entityId.value;
		return;
	}

	function edit(BSN_ENTITY_LOCTN_SEQ_ID, BSN_ENTITY_SEQ_ID) {
		location.href = "entity_location.aspx?FormMode=EDIT&LocationId=" + BSN_ENTITY_LOCTN_SEQ_ID + "&entityId=" + BSN_ENTITY_SEQ_ID;
		return;
	}

</script>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Business Entity Location List</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
	</head>
<body bgcolor="#cccccc" border="0" onLoad="onLoad();">
<form id="myform" name="myform">
		<INPUT TYPE=hidden VALUE="<%=entityId%>" NAME="entityId" ID="entityId">
<table border="0" class="pageBody" style="position:absolute;top:8;left:1;width:572;" ID="Table2">
	<tr>
		<td>
			<table border="0" style="width:100%;" ID="Table1">
				<th style="width:100px;">Name</th>
				<th style="width:100px">Action</th>
				<tr><td class='listTextBold'>Locations</td><td><a id='addItem' class='goLink' title='Click here to add an item' onDblClick='return false;' href='javascript:addLocation();'>[Add]</a></td></tr>&nbsp;
			</table>
		</td>
	</tr>
			<%
	If Not Response.IsClientConnected Then 
		rs.Close
		rs = Nothing
		Response.Clear
		'Response.End
	End If

	Response.Write("<tr><td><div style='width: 100%; height: 200px; overflow: auto;'>")
	Response.Write("<table border='0' style=width:100%;>") 
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
				Response.Write(""" class=""listItemText"" width=""120px"">")
				Response.Write(vbCrLf)
					Response.Write(vbTab3)
					Response.Write(rs.Fields("LOCTN_NAME").Value)
					Response.Write(vbCrLf)
				Response.Write(vbTab2)
				Response.Write("</td>")
				Response.Write(vbCrLf)

				Response.Write(vbTab2)
				Response.Write("<td class=""listItemText"" width=""100px"" nowrap>")
				Response.Write(vbCrLf)
					Response.Write(vbTab3)
					Response.Write("<a id=""goLink")
					Response.Write(i)
					Response.Write(""" class=""goLink"" title=""Click here to edit this item"" onDblClick=""return false;"" href='javascript:edit("	)				
					Response.Write(rs.Fields("BSN_ENTITY_LOCTN_SEQ_ID").Value)
					Response.Write(",")
					Response.Write(rs.Fields("BSN_ENTITY_SEQ_ID").Value)
					Response.Write(");'>[Edit]</a>&nbsp;" & vbCrLf)
					
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
    Else
        Response.Write("Sorry")
	End If
	Response.Write("</table></td></tr>")

	rs = nothing

	If RedirectIfError("Building Entity List", eRetPage, eRetBtnText) Then
		Response.Clear
		Response.End 
	End If
%>	
		</table>
	</body>
</form>
</html>
 