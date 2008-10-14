<%@ Page Language="VB" aspcompat="true" AutoEventWireup="false"%>
<!--#INCLUDE virtual="/ccrc/utilities/global.inc"-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#include virtual="/ccrc/reports/reports.inc"-->
<%
    On Error Resume Next

Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage


    Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)
    
    'Response.Write(sDomain)
    
Dim obj
Dim rsProgram
Dim rsAgency
Dim rsServicer
Dim rsLocation
Dim ProgramIds
Dim AgencyIds
Dim ServicerIds
    Dim LocationIds
    
    ProgramIds = " "
    AgencyIds = " "
    ServicerIds = " "
    LocationIds = " "

Dim orderBy
Dim reportType
Dim reportFormat
Dim startDate
Dim endDate
Dim acctPrd1, acctPrd2

Dim ThisMonthStart
Dim ThisMonthEnd
Dim LastMonthStart
Dim LastMonthEnd
Dim Yesterday
Dim Today

reportType = Request("REPORT_TYPE")
reportFormat = Request("REPORT_FORMAT")
acctPrd1 = Request("ACCT_PRD1")
acctPrd2 = Request("ACCT_PRD2")
startDate = Request("START_DATE")
endDate = Request("END_DATE")

if Request("START_DATE") > "" then
  StartDate = Request("START_DATE")
  EndDate = Request("END_DATE")
else
  dim tMonth = Month(Now())
  dim tDate = Year(Now())
  
  EndDate = tMonth & "/1/" & tDate
  EndDate = DateAdd("m", +1, EndDate)
  EndDate = DateAdd("d", -1, EndDate)
  StartDate = Month(EndDate) & "/1/" & Year(EndDate)
end if

If acctPrd1 = "" Then
        acctPrd1 = Year(Now()) & IIf(Len(Trim(Month(Now()))) = 1, "0" & Month(Now()), Month(Now()))
        acctPrd2 = acctPrd1
        'Response.Write(Len(Trim(Month(Now()))))
        'Response.Write(Month(Now()))
        
        Len(Trim(Month(Now())))
        
        
End If

obj = Server.CreateObject("CCRC_Admin.CAdmin")
If RedirectIfError("Creating object CCRC_Admin.CAdmin.", eRetPage, eRetBtnText) Then
	Response.Clear 
	Response.End 
End If

    ' To Delete - Begin
    'Response.Cookies("CCRCCookie")("profileRoles") = "35"
    
    ' End
If InStr(1,Request.Cookies("CCRCCookie")("profileRoles"),"35") > 0 Or _
		InStr(1,Request.Cookies("CCRCCookie")("profileRoles"),"83") > 0 Then 
		'owner or super user
	rsProgram = obj.Get_Programs(sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Programs", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	rsAgency = obj.Get_Agencies(sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Agencies", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	rsServicer = obj.Get_Servicers(0, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Servicers", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
	rsLocation = obj.Get_Entity_Locations("", "2", sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError("Calling CCRC_Admin.CAdmin.Get_Locations", eRetPage, eRetBtnText) Then
		Response.Clear 
		Response.End 
	End If
End If
%>
<script language="javascript">

function validate() 
{
	var pv = new Validator();
	pv.addField(document.all.reportType);
	return pv.validate(document.all.results);
}

function submit_form() {
  if (validate())
    document.all.master.submit();
  return;
}

function SetDates(StartDate, EndDate) {
  document.all.master.START_DATE.value = StartDate;
  document.all.master.END_DATE.value = EndDate;
}

function setReportOptions(s) {
	document.all.ACCT_PRD1.disabled = false;
	document.all.ACCT_PRD2.disabled = false;
	document.all.START_DATE.disabled = false;
	document.all.END_DATE.disabled = false;

	if("1" == s)
	{
		document.all.REPORT_TYPE.value = "Referrals"
		document.all.ACCT_PRD1.value = "";
		document.all.ACCT_PRD1.disabled = true;
		document.all.ACCT_PRD2.value = "";
		document.all.ACCT_PRD2.disabled = true;
		document.all.START_DATE.value = document.all.startDate.value ;
		document.all.END_DATE.value = document.all.endDate.value ;
	}
	else if("2" == s)
	{
		document.all.REPORT_TYPE.value = "Outcomes"
		document.all.ACCT_PRD1.value = "";
		document.all.ACCT_PRD1.disabled = true;
		document.all.ACCT_PRD2.value = "";
		document.all.ACCT_PRD2.disabled = true;
		document.all.START_DATE.value = document.all.startDate.value ;
		document.all.END_DATE.value = document.all.endDate.value ;
	}
	else
	{
		document.all.REPORT_TYPE.value = "Billables"
		document.all.ACCT_PRD1.value = document.all.acctPrd1.value;
		document.all.ACCT_PRD2.value = document.all.acctPrd2.value;
		document.all.START_DATE.value = "";
		document.all.END_DATE.value = "";
		document.all.START_DATE.disabled = true;
		document.all.END_DATE.disabled = true;
	}
}
</script>

<html>
<head>
<title>Subject List</title>
<link rel="stylesheet" type="text/css" href="../CSS/Main_W.CSS">
<SCRIPT LANGUAGE="JavaScript" SRC="/ccrc/utilities/calendar.js"></SCRIPT>
<script language="JavaScript" src="/ccrc/utilities/validation.js"></script>
</head>
<body background="../images/APP_WHITE_BACKGROUND.gif" topmargin="0" leftmargin="5">

<%
 

obj = nothing

Response.Write ( "<br>")
    Response.Write("<form id=""master"" method=""post"" action=""CCRC_Report.aspx"">")
Response.Write ( "  <input type=""hidden"" name=""REPORT_TYPE"" value=""" & ReportType & """>")
Response.Write ( "  <input type=""hidden"" name=""acctPrd1"" value=""" & acctPrd1 & """>")
Response.Write ( "  <input type=""hidden"" name=""acctPrd2"" value=""" & acctPrd2 & """>")
Response.Write ( "  <input type=""hidden"" name=""startDate"" value=""" & startDate & """>")
Response.Write ( "  <input type=""hidden"" name=""endDate"" value=""" & endDate & """>")
Response.Write ( "  <table border=""0"" cellpadding=""0"" cellspacing=""0"">")
Response.Write ( "  <TR>"  )
Response.Write ( "    <TD> ")
Response.Write ( "<input onclick='setReportOptions(1);' type='radio' datatype='optiongroup' required='true' errorlbl='Report Type' errormsg='Report Type must be selected.' id='reportType' name='reportType' value='" & reportType & "' " )
If reportType="1" Then 
	Response.Write ( "checked")
End If 
    'Response.Write ( "><span>Referrals Report</span>")
    'Response.Write ( "    </TD>"  )
    'Response.Write ( "  </TR> ")
    'Response.Write ( "  <TR>"  )
    'Response.Write ( "    <TD> ")
    'Response.Write ( "<input onclick='setReportOptions(2);' type='radio' datatype='optiongroup' required='true' errorlbl='Report Type' errormsg='Report Type must be selected.' id='reportType' name='reportType' value='" & reportType & "' " )
    'If reportType="2" Then 
    '	Response.Write ( "checked")
    'End If 
Response.Write ( "><span>Outcomes Report</span>")
Response.Write ( "    </TD>"  )
Response.Write ( "  </TR> ")
Response.Write ( "  </table>")
Response.Write ( "  <table border=""0"" style=""position:relative;left:50;"" cellpadding=""0"" cellspacing=""0"">")
Response.Write ( "  <TR>"  )
Response.Write ( "    <TD align='right'>Start:</TD>" ) 
Response.Write ( "    <TD>")
WriteTextField ("START_DATE", StartDate, "ADD", "START_DATE", 10, 10, "DATE", "", StartDate)
    Response.Write("      <A HREF=""javascript:doNothing()"" onClick=""setDateField(document.all.master.START_DATE); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
Response.Write ( "      End: " )
WriteTextField ("END_DATE", EndDate, "ADD", "END_DATE", 10, 10, "DATE", "", EndDate)
    Response.Write("      <A HREF=""javascript:doNothing()"" onClick=""setDateField(document.all.master.END_DATE); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
	' calculate this month's start & end date
	ThisMonthStart = Month(Now()) & "/01/" & Year(Now())
	if isdate(Month(ThisMonthStart) & "/31/" & Year(ThisMonthStart)) then
	ThisMonthEnd = Month(ThisMonthStart) & "/31/" & Year(ThisMonthStart)
	else
	if isdate(Month(ThisMonthStart) & "/30/" & Year(ThisMonthStart)) then
		ThisMonthEnd = Month(ThisMonthStart) & "/30/" & Year(ThisMonthStart)
	else
		if isdate(Month(ThisMonthStart) & "/29/" & Year(ThisMonthStart)) then
		ThisMonthEnd = Month(ThisMonthStart) & "/29/" & Year(ThisMonthStart)
		else
		ThisMonthEnd = Month(ThisMonthStart) & "/28/" & Year(ThisMonthStart)
		end if
	end if
	end if
	' calculate last month's start & end date
	if Month(ThisMonthStart) = 1 then
	LastMonthStart = "12/01/" & (Year(ThisMonthStart) - 1)
	else
	LastMonthStart = (Month(ThisMonthStart) - 1) & "/01/" & Year(ThisMonthStart)
	end if
	if isdate(Month(LastMonthStart) & "/31/" & Year(LastMonthStart)) then
	LastMonthEnd = Month(LastMonthStart) & "/31/" & Year(LastMonthStart)
	else  
	if isdate(Month(LastMonthStart) & "/30/" & Year(LastMonthStart)) then
		LastMonthEnd = Month(LastMonthStart) & "/30/" & Year(LastMonthStart)
	else
		if isdate(Month(LastMonthStart) & "/29/" & Year(LastMonthStart)) then
		LastMonthEnd = Month(LastMonthStart) & "/29/" & Year(LastMonthStart)
		else
		LastMonthEnd = Month(LastMonthStart) & "/28/" & Year(LastMonthStart)
		end if
	end if
	end if
    Yesterday = DateAdd("d", -1, Now()).ToString("d")
    Today = Now().ToString("d")
	
Response.Write ( "&nbsp;<a class=""Mini"" href=""javascript:SetDates('" & LastMonthStart & "', '" & LastMonthEnd & "');"">Last Month</a>")
Response.Write ( "&nbsp;<a class=""Mini"" href=""javascript:SetDates('" & ThisMonthStart & "', '" & ThisMonthEnd & "');"">This Month</a>")
Response.Write ( "&nbsp;<a class=""Mini"" href=""javascript:SetDates('" & Yesterday & "', '" & Yesterday & "');"">Yesterday</a>")
Response.Write ( "&nbsp;<a class=""Mini"" href=""javascript:SetDates('" & Today & "', '" & Today & "');"">Today</a>")
Response.Write ( "    </TD>" ) 
Response.Write ( "  </TR>    "  )
Response.Write ( "  </table>")

Response.Write ( "<BR>")

Response.Write ( "  <table border=""0"" cellpadding=""0"" cellspacing=""0"">")
Response.Write ( "  <TR>"  )
Response.Write ( "    <TD> ")
Response.Write ( "<input onclick='setReportOptions(3);' type='radio' datatype='optiongroup' required='true' errorlbl='Report Type' errormsg='Report Type must be selected.' id='reportType' name='reportType' value='" & reportType & "' " )
If reportType="3" Then 
	Response.Write ( "checked")
End If 
Response.Write ( "><span>Completed Counseling Report</span><br>")
Response.Write ( "    </TD>"  )
Response.Write ( "  </TR> ")
Response.Write ( "  </table>")
Response.Write ( "  <table border=""0"" style=""position:relative;left:50;"" cellpadding=""0"" cellspacing=""0"">")
Response.Write ( "  <TR>"  )
Response.Write ( "    <TD>Accounting Periods (Start/End) (YYYYMM):</TD>")
Response.Write ( "    <TD><input type='text' id='ACCT_PRD1' name='ACCT_PRD1' value='" & acctPrd1 & "' required='true' datatype='anytext' errorlbl='Accounting Period' errormsg='Accounting Period is required.'>/</TD>")
Response.Write ( "    <TD><input type='text' id='ACCT_PRD2' name='ACCT_PRD2' value='" & acctPrd2 & "' required='true' datatype='anytext' errorlbl='Accounting Period' errormsg='Accounting Period is required.'></TD>")
Response.Write ( "  </TR>" ) 
Response.Write ( "</TABLE>"  )
Response.Write ( "<BR>")

If InStr(1,Request.Cookies("CCRCCookie")("profileRoles"),"35") > 0 Or _
		InStr(1,Request.Cookies("CCRCCookie")("profileRoles"),"83") > 0 Then 
		'owner or super user
	rsProgram.Sort = "PROGRAM_NAME"
	Response.Write ( "  <table border=""0"" cellpadding=""0"" cellspacing=""0"">")
	Response.Write ( "  <TR>"  )
	Response.Write ( "    <TD>Program:</TD>")
	Response.Write ( "    <TD>" )
	Response.Write ( pickList(rsProgram, "PROGRAM_IDs", ProgramIds, true, "Program", "Program is required.", "PROGRAM_SEQ_ID", "PROGRAM_NAME"))
	Response.Write ( "    </TD>"  )
	Response.Write ( "  </TR>    ")

	rsAgency.Sort = "BSN_ENTITY_NAME"
	Response.Write ( "  <TR>"  )
	Response.Write ( "    <TD>Agency:</TD>")
	Response.Write ( "    <TD>" )
	Response.Write ( pickList(rsAgency, "AGENCY_IDs", AgencyIds, true, "Agency", "Agency is required.", "BSN_ENTITY_SEQ_ID", "BSN_ENTITY_NAME"))
	Response.Write ( "    </TD>"  )
	Response.Write ( "  </TR>    ")

	rsServicer.Sort = "HAS_CONTRACT_IND DESC, BSN_ENTITY_NAME"
	Response.Write ( "  <TR>"  )
	Response.Write ( "    <TD>Servicer:</TD>")
	Response.Write ( "    <TD>" )
	Response.Write ( pickList(rsServicer, "SERVICER_IDs", ServicerIds, true, "Servicer", "Servicer is required.", "BSN_ENTITY_SEQ_ID", "BSN_ENTITY_NAME"))
	Response.Write ( "    </TD>"  )
	Response.Write ( "  </TR>")

	Response.Write ( "<INPUT TYPE='hidden' VALUE='' NAME='LOCATION_IDs' ID='LOCATION_IDs'>")
'	rsLocation.Sort = "BSN_ENTITY_NAME, LOCTN_NAME"
'	Response.Write ( "  <TR>"  
'	Response.Write ( "    <TD>Location:</TD>"
'	Response.Write ( "    <TD>" 
'	Response.Write ( pickList2(rsLocation, "LOCATION_IDs", LocationIds, true, "Location", "Location is required.", "BSN_ENTITY_LOCTN_SEQ_ID", "BSN_ENTITY_NAME", "LOCTN_NAME")
'	Response.Write ( "    </TD>"  
'	Response.Write ( "  </TR><tr><td>&nbsp;</td></tr>"
	
	Response.Write ( "  <TR>"  )
	Response.Write ( "    <TD>Order By: ")
	Response.Write ( "    </TD> ")
	Response.Write ( "    <TD> ")
	writeSelectList("ORDER_BY", OrderBy, "EDIT", Split("PROGRAM,AGENCY,SERVICER", ","), Split("Program,Agency,Servicer", ","))
	Response.Write ( "    </TD>" ) 
	Response.Write ( "  </TR>    ")
Else
        Response.Write("<input type='hidden' name='program_ids' value=''>")
	Response.Write ( "<input type='hidden' name='agency_ids' value=''>")
	Response.Write ( "<input type='hidden' name='servicer_ids' value=''>")
	Response.Write ( "<input type='hidden' name='location_ids' value=''>")
End If
Response.Write ( "</table>")
Response.Write ( "<BR>")

Response.Write ( "  <table border=""0"" cellpadding=""0"" cellspacing=""0"">")
Response.Write ( "  <TR>"  )
Response.Write ( "    <TD width='50px'>Format:</TD>")
Response.Write ( "    <TD> ")
writeSelectList("REPORT_FORMAT", reportFormat, "EDIT", Split("HTML,EXCEL", ","), Split("Html,Excel", ","))
Response.Write ( "    </TD>"  )
Response.Write ( "  </TR>    "  )
%>
				<tr><td><table ID="Table1">
					<tr>
						<td><!-- Validation results section-->
							<div name="results" id="results" class="errorText"></div>
						</td>
					</tr>
				</table></td></tr>
<%
Response.Write ( "</TABLE>" ) 
Response.Write ( "<BR>")
%>
<table>
    </td></tr>
    <tr><td>
      <input type="button" onclick="submit_form();" value="Run Report"> 
    </td></tr>
  </table>
</form>
</body>
</html>