<%@ Language=VBScript %>
<!--#INCLUDE virtual="/ccrc/utilities/global.inc"-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<html><head><title>Subject List</title>
<link rel="stylesheet" type="text/css" href="../CSS/Main_W.CSS">

<script language="javascript">

function submit_form(action, reporttype) {
//  if ("RUN" == action) { // run report
//    document.all.master.action = "CCRC_Report_Run.aspx";
//  } 
  if ("CONFIG" == action) { // customize report
    document.all.master.action = "CCRC_Report_Config.aspx";
  }
  document.all.REPORT_TYPE.value = reporttype;
  document.all.master.submit();
  return;
}

function iif(exp, truepart, falsepart)
{
  if (exp) 
    iif = truepart
  else
    iif = falsepart
}

</script>

</head>
<body background="../images/APP_WHITE_BACKGROUND.gif" topmargin="0" leftmargin="5">

<%

Dim bgColor

bgColor = "CCCCCC"

Response.Write( "<center>")
    
Response.Write( "<form id=""master"" action="""" method=""post"">")
Response.Write( "  <input type=""hidden"" name=""REPORT_TYPE"" value="""">")
Response.Write( "  <input type=""hidden"" name=""TOTALS_ONLY"" value=""on"">")
Response.Write( "  <br>")
Response.Write( "  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""95%"">")
Response.Write( "    <tr><td colspan=""2"" width=""100%"" class=""HeaderCell"">CCRC Reports</td></tr>")
Response.Write( "    <tr>")
Response.Write( "      <td bgcolor=""#CCCCCC"" Class=""DataRight"">&nbsp;</td>")
Response.Write( "    </tr>")
Response.Write( "  </table>")

Response.Write( "  <table border=""1"" cellpadding=""2"" cellspacing=""0"" width=""95%"" ID=""Table2"">")
Response.Write( "    <tr>")
Response.Write( "      <td class=""TableTitleLeft"">Name</td>")
Response.Write( "      <td class=""TableTitleLeft"">Description</td>")
Response.Write( "      <td class=""TableTitleLeft"">Action</td>")
Response.Write( "    </tr>")

Response.Write( "    <!-- Referral Activity Report -->")
bgColor = iif(bgColor="FFFFFF", "CCCCCC", "FFFFFF")
Response.Write( "    <tr>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"" NOWRAP>Referral Activity</td>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"">An overview of Referral Activity.</td>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"" NOWRAP>")
'Response.Write( "        <a HREF=""javascript:submit_form('RUN', 'Referrals');"">Run</a>&nbsp;")
Response.Write( "        <a HREF=""javascript:submit_form('CONFIG', 'Referrals');"">Customize</a>&nbsp;")
Response.Write( "      </td>")
Response.Write( "    </tr>")

Response.Write( "    <!-- Outcomes Activity Report -->")
bgColor = iif(bgColor="FFFFFF", "CCCCCC", "FFFFFF")
Response.Write( "    <tr>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"" NOWRAP>Outcomes</td>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"">An overview of Outcomes.</td>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"" NOWRAP>")
'Response.Write( "        <a HREF=""javascript:submit_form('RUN', 'Outcomes');"">Run</a>&nbsp;")
Response.Write( "        <a HREF=""javascript:submit_form('CONFIG', 'Outcomes');"">Customize</a>&nbsp;")
Response.Write( "      </td>")
Response.Write( "    </tr>")

Response.Write( "    <!-- Billable Outcomes Report -->")
bgColor = iif(bgColor="FFFFFF", "CCCCCC", "FFFFFF")
Response.Write( "    <tr>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"" NOWRAP>Billable Outcomes</td>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"">An overview of Billable Outcomes.</td>")
Response.Write( "      <td bgcolor=""" & bgColor & """ class=""DataLeft"" NOWRAP>")
'Response.Write( "        <a HREF=""javascript:submit_form('RUN', 'Outcomes');"">Run</a>&nbsp;")
Response.Write( "        <a HREF=""javascript:submit_form('CONFIG', 'BillableOutcomes');"">Customize</a>&nbsp;")
Response.Write( "      </td>")
Response.Write( "    </tr>")

Response.Write( "  </table>")
Response.Write( "</form>")

' -------------------------------------------------------------------------------------------------------------------------


%>
    </center>
<!--#INCLUDE VIRTUAL="/ccrc/reports/CCRC_Report.htm"-->
  </body>
</html>