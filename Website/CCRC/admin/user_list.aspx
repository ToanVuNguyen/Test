<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual="/ccrc/utilities/global.inc"-->
<!--#INCLUDE virtual="/ccrc/utilities/ccrc.inc"-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
'Sub Main
    On Error Resume Next

	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
	
	Dim entity
	entity = Request.QueryString("entityId")
	Dim formMode
	formMode = Request.QueryString("FormMode")

	
	Dim objCCRC
	objCCRC = CreateObject("CCRC_Admin.CAdmin")
	If RedirectIfError("Creating object CCRC_Admin.CAdmin.", eRetPage, eRetBtnText) Then
		Response.Clear 
		    Response.End
	End If
	
	Dim iceProfileEditor
	iceProfileEditor = objCCRC.GetICEProfileEditorServer()
	If RedirectIfError("Calling CCRC_Admin.CAdmin.GetICEProfileEditorServer", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	
	dim ccrcWeb
	ccrcWeb = Request.ServerVariables("SERVER_NAME")
	
'	Response.Write(iceProfileEditor
'	Response.End
	Dim Rs			
	Rs = objCCRC.Get_Users(entity, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Users", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
 %>
<script language="JavaScript">
function onLoad() {
}

function addUser() {
//	'location.href = "/CCRC/admin/user_process.aspx?entityId=" + document.all.entityId.value + "&FormMode=ADD&peDomain=IRFC&peUserid=Subbu&peRoles=85,125&userLocation=1";
	location.href = "/admin/AddSecureUser.aspx?returnpage=/Admin/user_list.aspx&FormMode=ADD&entityId=" + document.all.entityId.value; 

}

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>User List</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
	</head>
	<body>
    
		<br>
		<br>
		<h1>Users</h1>
		<FORM name="myform" id="myform">
			<INPUT TYPE=hidden VALUE="<%=iceProfileEditor%>" NAME="iceProfileEditor" ID="iceProfileEditor"> 
			<INPUT TYPE=hidden VALUE="<%=ccrcWeb%>" NAME="ccrcWeb" ID="ccrcWeb"> 
			<INPUT TYPE=hidden VALUE="<%=formMode%>" NAME="FormMode" ID="FormMode"> 
			<INPUT TYPE=hidden VALUE="<%=entity%>" NAME="entityId" ID="entityId">
<table border="0" class="pageBody" style="position:absolute;top:8;left:1;width:572;" ID="Table2">
	<tr>
		<td>
			<table border="1" style="width:100%;" ID="Table1">
				<th style="width:100px;">Name</th>
				<th width="100px">Action</th>
				<tr><td class='listTextBold'>Users</td><td><a id='addItem' class='goLink' title='Click here to add an item' onDblClick='return false;' href='javascript:addUser();'>[Add]</a></td></tr>&nbsp;
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
	Response.Write("<table border='0' style=width:100%;>")
	If IsValidRS(rs) Then
		Dim status, linkLabel
		Dim i
		i = 1
		
					Rs.MoveFirst
					Do While Not Rs.EOF
						Response.Write("<TR>")
            Response.Write(vbTab)
				Response.Write("<td id=""ListItem")
				Response.Write(i)
				Response.Write(""" class=""listItemText"" width=""250px"">")
				Response.Write(vbCrLf)
            Response.Write(vbTab)
            Response.Write(buildName(Rs.Fields(10).Value, Rs.Fields(13).Value, Rs.Fields(9).Value, ""))
            ' Response.Write(buildName(Rs.Fields("FIRST_NAME").Value, Rs.Fields("MID_INIT_NAME").Value, Rs.Fields("LAST_NAME").Value, ""))
				Response.Write("</td>")

            Response.Write(vbTab)
				Response.Write("<td class=""listItemText"" width=""250px"" nowrap>")
				Response.Write(vbCrLf)
            Response.Write(vbTab)
					Response.Write("<a id=""goLink")
					Response.Write(i)
            Response.Write(""" class=""goLink"" title=""Click here to edit this item"" ")
            Response.Write("<A HREF='/Admin/AddSecureUser.aspx?userid=" & Rs.Fields("NT_USERID").Value & "&returnpage=/Admin/user_list.aspx&FormMode=EDIT&entityId=" & Rs.Fields("BSN_ENTITY_SEQ_ID").Value & "&ntUserId=" & Rs.Fields("NT_USERID").Value & "'>[Edit]</A>&nbsp;")
            If Rs.Fields("ACTIVE_IND").Value = "Y" Then
                status = "N"
                linkLabel = "Inactivate"
            Else
                status = "Y"
                linkLabel = "Activate"
            End If
            Response.Write("<A HREF='/admin/user_process.aspx?FormMode=CHANGESTATUS&profileId=" & Rs.Fields("CCRC_USER_SEQ_ID").Value & "&activeInd=" & status & "&entityId=" & Rs.Fields("BSN_ENTITY_SEQ_ID").Value & "'>[" & linkLabel & "]</A>&nbsp;")
            Response.Write("<A HREF='user_data_rights.aspx?profileId=" & Rs.Fields("CCRC_USER_SEQ_ID").Value & "&entityId=" & Rs.Fields("BSN_ENTITY_SEQ_ID").Value & "'>[Data Viewing Rights]</A>")
						Response.Write("</td></TR>")
						Rs.MoveNext
						i = i + 1
					Loop
					Rs.close
					Rs = Nothing
				End If
				%>
			</table>
		</form>
	</body>
</html>
